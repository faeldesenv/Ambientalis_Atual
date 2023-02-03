using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;
using System.Text;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using Utilitarios.Criptografia;

public partial class DNPM_Eventos : PageBase
{
    Transacao transacao = new Transacao();
    Msg msg = new Msg();

    private int Contador
    {
        get
        {
            if ((int)Session["contador_eventos"] < 0)
                Session["contador_eventos"] = 0;
            return (int)Session["contador_eventos"];
        }
        set { Session["contador_eventos"] = value; }
    }

    private int Indice
    {
        get
        {
            if ((int)Session["indice_eventos"] < 0)
                Session["indice_eventos"] = 0;
            return (int)Session["indice_eventos"];
        }
        set { Session["indice_eventos"] = value; }
    }

    private IList<ProcessoDNPM> Processos
    {
        get
        {
            if (Session["processos_dnpm_eventos"] == null)
                Session["processos_dnpm_eventos"] = new List<ProcessoDNPM>();
            return Session["processos_dnpm_eventos"] as List<ProcessoDNPM>;
        }
        set { Session["processos_dnpm_eventos"] = value; }
    }

    public bool UsuarioEditor
    {
        get
        {
            if (Session["UsuarioEditorDnpm"] == null)
                return false;
            else
                return (bool)Session["UsuarioEditorDnpm"];
        }
        set { Session["UsuarioEditorDnpm"] = value; }
    }

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloDNPM
    {
        get
        {
            if (Session["ConfiguracaoModuloDNPM"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloDNPM"];
        }
        set { Session["ConfiguracaoModuloDNPM"] = value; }
    }

    //Empresas que o usuário edita 
    public IList<Empresa> EmpresasPermissaoEdicaoModuloNPM
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloNPM"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloNPM"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloNPM"] = value; }
    }

    //Processos que o usuário edita     
    public IList<ProcessoDNPM> ProcessosPermissaoEdicaoModuloDNPM
    {
        get
        {
            if (Session["ProcessosPermissaoEdicaoModuloDNPM"] == null)
                return null;
            else
                return (IList<ProcessoDNPM>)Session["ProcessosPermissaoEdicaoModuloDNPM"];
        }
        set { Session["ProcessosPermissaoEdicaoModuloDNPM"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.VerificarPermissoes();
                this.IniciarComponentes();
                this.CarregarCampos();

                if (Seguranca.RecuperarParametro("p", this.Request) != null)
                {
                    ProcessoDNPM proc = ProcessoDNPM.ConsultarPorId(Seguranca.RecuperarParametro("p",this.Request).ToInt32());
                    ddlGrupoEconomico.SelectedValue = proc.Empresa.GrupoEconomico.Id.ToString();
                    ddlClientes_SelectedIndexChanged(null, null);
                    ddlEmpresa.SelectedValue = proc.Empresa.Id.ToString();
                    this.Pesquisar();
                    lblProcessoDNPM.Text = proc.GetNumeroProcessoComMascara;
                    ddlEmpresa.Enabled = false; ddlGrupoEconomico.Enabled = false;
                }


                if (!this.UsuarioEditor)
                    this.DesabilitarAlteracoes();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                this.GetMBOX<MBOX>().Show(msg);
            }
        }
    }

    #region ___________Metodos_____________

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");
        this.LimparSessoesPermissoes();

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDNPM.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

        if (this.ConfiguracaoModuloDNPM == null)
            Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                this.UsuarioEditor = true;

            this.EmpresasPermissaoEdicaoModuloNPM = null;
            this.ProcessosPermissaoEdicaoModuloDNPM = null;
        }

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.CarregarSessoesEmpresasDeEdicaoEVisualizacao();

            if (this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0)
                this.UsuarioEditor = true;

            this.ProcessosPermissaoEdicaoModuloDNPM = null;
        }

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            this.EmpresasPermissaoEdicaoModuloNPM = null;

            this.CarregarSessoesProcessosDNPMDeEdicaoEVisualizacao();

            if (this.ProcessosPermissaoEdicaoModuloDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM.Count > 0)
                this.UsuarioEditor = true;
        }
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoEdicaoModuloNPM = null;
        this.ProcessosPermissaoEdicaoModuloDNPM = null;
    }

    private void CarregarSessoesEmpresasDeEdicaoEVisualizacao()
    {
        IList<Empresa> empresas = Empresa.ConsultarTodos();

        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

        if (empresas != null && empresas.Count > 0)
        {
            foreach (Empresa empresa in empresas)
            {
                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, modulo.Id);

                if (empresaPermissao != null)
                {
                    //empresas com permissão de edição
                    if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.EmpresasPermissaoEdicaoModuloNPM == null)
                            this.EmpresasPermissaoEdicaoModuloNPM = new List<Empresa>();

                        if (!this.EmpresasPermissaoEdicaoModuloNPM.Contains(empresa))
                            this.EmpresasPermissaoEdicaoModuloNPM.Add(empresa);
                    }
                }
            }
        }
    }

    private void CarregarSessoesProcessosDNPMDeEdicaoEVisualizacao()
    {
        IList<ProcessoDNPM> processos = ProcessoDNPM.ConsultarTodos();

        if (processos != null && processos.Count > 0)
        {
            foreach (ProcessoDNPM processo in processos)
            {
                //processos com permissão de edição
                if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0 && processo.UsuariosEdicao.Contains(this.UsuarioLogado))
                {
                    if (this.ProcessosPermissaoEdicaoModuloDNPM == null)
                        this.ProcessosPermissaoEdicaoModuloDNPM = new List<ProcessoDNPM>();

                    if (!this.ProcessosPermissaoEdicaoModuloDNPM.Contains(processo))
                        this.ProcessosPermissaoEdicaoModuloDNPM.Add(processo);
                }
            }
        }
    }

    private void IniciarComponentes()
    {
        btnBaixarAtualizacoes.Enabled = false;
        btnParar.Text = "Parar";
        this.Contador = 0;
        this.Processos = null;
        this.Indice = 0;
        Timer1.Enabled = false;
    }

    private void DesabilitarAlteracoes()
    {
        btnBaixarAtualizacoes.Enabled = btnBaixarAtualizacoes.Visible = btnSalvarTodos.Enabled = btnSalvarTodos.Visible = false;
    }

    private void SalvarTodos()
    {
        foreach (RepeaterItem item in rptItens.Items)
        {
            DataGrid dgr = (DataGrid)item.FindControl("dgrEventos");
            foreach (DataGridItem row in dgr.Items)
            {
                EventoDNPM e = EventoDNPM.ConsultarPorId(dgr.DataKeys[row.ItemIndex].ToString().ToInt32());
                e.Atualizado = ((CheckBox)row.FindControl("chkAtualizado")).Checked;
                e.Irrelevante = ((CheckBox)row.FindControl("chkIrrelevante")).Checked;
                e = e.Salvar();
            }
        }
        msg.CriarMensagem("Itens atualizados com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    private void Pesquisar()
    {
        IList<ProcessoDNPM> processos = new List<ProcessoDNPM>();

        if (Seguranca.RecuperarParametro("p", this.Request) != null)
        {
            processos.Add(ProcessoDNPM.ConsultarPorId(Seguranca.RecuperarParametro("p",Request).ToInt32()));
        }
        else
            processos = ProcessoDNPM.ConsultarProcessosDoClienteOuGrupo(ddlGrupoEconomico.SelectedValue.ToInt32(), ddlEmpresa.SelectedValue.ToInt32(), this.ConfiguracaoModuloDNPM.Tipo, this.EmpresasPermissaoEdicaoModuloNPM, this.ProcessosPermissaoEdicaoModuloDNPM);


        rptItens.DataSource = processos;
        rptItens.DataBind();
        btnBaixarAtualizacoes.Enabled = processos.Count > 0 && processos != null;
        lblQuantidade.Text = processos.Count + " Processo(s) encontrado(s)";
    }

    private void CarregarCampos()
    {
        IList<GrupoEconomico> gs = GrupoEconomico.ConsultarGruposAtivos();
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = gs != null ? gs : new List<GrupoEconomico>();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        this.ddlClientes_SelectedIndexChanged(null, null);
    }

    private string GetParametrosDNPM(string numeroProcesso)
    {
        try
        {
            return "?numero=" + numeroProcesso.Substring(0, 6) + "&ano=" + numeroProcesso.Substring(6, 4);
        }
        catch (Exception)
        {
            return "";
        }
    }

    private void BaixarAtualizacao(ProcessoDNPM processo)
    {
        transacao.Fechar(ref msg);
        transacao.Abrir();

        /* Adicionando Handler pro evento ServerCertificateValidationCallback 
         * que chama a função CustomValidation*/
        ServicePointManager.ServerCertificateValidationCallback += new
        System.Net.Security.RemoteCertificateValidationCallback(CustomValidation);

        processo = ProcessoDNPM.ConsultarPorId(processo.Id);
        string url = "https://sistemas.dnpm.gov.br/SCM/Extra/site/admin/dadosProcesso.aspx" + this.GetParametrosDNPM(processo.Numero);

        //baixando codigo fonte do site e decodificando
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "GET";
        WebResponse myResponse = myRequest.GetResponse();
        StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
        string source = sr.ReadToEnd();
        sr.Close();
        myResponse.Close();

        source = source.Substring(source.IndexOf("Eventos"));

        String h1Regex = "<td[^>]*?>(?<TagText>.*?)</td>";

        MatchCollection mc = Regex.Matches(this.GetTags(source), h1Regex, RegexOptions.Singleline);
        for (int i = 0; i < mc.Count; i += 2)
        {
            EventoDNPM ev = new EventoDNPM();
            ev.Descricao = mc[i].Groups["TagText"].Value.Replace("\n", "").Replace("\r", "").Trim();
            ev.Data = mc[i + 1].Groups["TagText"].Value.Replace("\n", "").Replace("\r", "").Trim().ToDateTime();

            if (processo.EventosDNPM == null)
                processo.EventosDNPM = new List<EventoDNPM>();
            if (!processo.EventosDNPM.Contains(ev))
            {
                Contador++;
                ev = ev.Salvar();
                processo.EventosDNPM.Add(ev);
            }
        }

        processo = processo.Salvar();
        transacao.Fechar(ref msg);
        transacao.Abrir();
    }

    private string GetTags(string source)
    {
        string table = "";

        int ini = source.IndexOf("<table");
        int fim = source.IndexOf("</table>") + 8 - source.IndexOf("<table");
        if (ini > 0 && fim > 0)
            table = source.Substring(ini, fim);

        return table;
    }

    public string BindEmpresaGE(Object o)
    {
        ProcessoDNPM p = (ProcessoDNPM)o;
        return " - " + p.Empresa.Nome + "(" + p.Empresa.GrupoEconomico.Nome + ")";
    }

    public Color BindColor(Object o)
    {
        EventoDNPM p = (EventoDNPM)o;
        if (p.Irrelevante)
        {
            return Color.Gray;
        }
        return Color.Black;
    }

    public string BindResultado(Object o)
    {
        ProcessoDNPM p = (ProcessoDNPM)o;
        return p != null ? p.EventosDNPM != null && p.EventosDNPM.Count > 0 ? p.EventosDNPM.Count + " Eventos encontrados." : "Este processo ainda não possui eventos baixados." : "Este processo ainda não possui eventos baixados.";
    }

    public IList<EventoDNPM> BindEventos(Object o)
    {
        ProcessoDNPM p = (ProcessoDNPM)o;
        return p.EventosDNPM;
    }

    #endregion

    #region ___________Eventos_____________

    protected void dgrEventos_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Pager)
            if (((CheckBox)e.Item.FindControl("chkAtualizado")).Checked == false && ((CheckBox)e.Item.FindControl("chkIrrelevante")).Checked == false)
            {
                e.Item.BackColor = Color.CadetBlue;
                e.Item.ForeColor = Color.White;
                e.Item.BorderStyle = BorderStyle.Solid;
                e.Item.BorderWidth = 1;
            }

    }

    /* Função que sempre retorna true para a validação do certificado.
     Com isso, mesmo se o certificado estiver com problemas, seu método 
     * continuará e irá acessr o conteúdo HTTP*/
    private static bool CustomValidation(Object sender, X509Certificate cert,
    X509Chain chain, System.Net.Security.SslPolicyErrors error)
    {
        return true;
    }

    protected void btnBaixarAtualizacoes_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.UsuarioEditor)
            {
                if (ddlGrupoEconomico.SelectedValue.ToInt32() <= 0)
                {
                    msg.CriarMensagem("Selecione um grupo econômico para prosseguir.", "Informação", MsgIcons.Informacao);
                    return;
                }

                this.ZerarCampos();
                LinkButton1_ModalPopupExtender.Show();

                IList<ProcessoDNPM> processos = new List<ProcessoDNPM>();
                if (Request["p"] != null)
                {
                    processos.Add(ProcessoDNPM.ConsultarPorId(Criptografia.Decrypt(Request["p"].ToString(), true).ToInt32()));
                }
                else
                    this.Processos = ProcessoDNPM.ConsultarProcessosDoClienteOuGrupo(ddlGrupoEconomico.SelectedValue.ToInt32(), ddlEmpresa.SelectedValue.ToInt32(), this.ConfiguracaoModuloDNPM.Tipo, this.EmpresasPermissaoEdicaoModuloNPM, this.ProcessosPermissaoEdicaoModuloDNPM);


                this.Indice = this.Processos.Count;
                Timer1.Enabled = true;
            }
            else
                msg.CriarMensagem("Você não tem permissão para realizar esta operação.", "Acesso Restrito", MsgIcons.AcessoNegado);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnAtualizacaoProcesso_Click(object sender, EventArgs e)
    {
        //Baixar atualização do item atual
        try
        {
            if (this.UsuarioEditor)
            {
                this.ZerarCampos();
                LinkButton1_ModalPopupExtender.Show();
                this.Processos = new List<ProcessoDNPM>() { ProcessoDNPM.ConsultarPorId(((Button)sender).CommandArgument.ToInt32()) };
                this.Indice = this.Processos.Count;
                Timer1.Enabled = true;
            }
            else
                msg.CriarMensagem("Você não tem permissão para realizar esta operação.", "Acesso Restrito", MsgIcons.AcessoNegado);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrupoEconomico gr = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            ddlEmpresa.DataValueField = "Id";
            ddlEmpresa.DataTextField = "Nome";
            ddlEmpresa.DataSource = gr != null ? gr.Empresas != null ? gr.Empresas : new List<Empresa>() : new List<Empresa>();
            ddlEmpresa.DataBind();
            ddlEmpresa.Items.Insert(0, new ListItem("-- Todas --", "0"));
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() <= 0)
            {
                msg.CriarMensagem("Selecione um grupo econômico para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnSalvarTodos_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.UsuarioEditor)
            {
                this.SalvarTodos();
            }
            else
                msg.CriarMensagem("Você não tem permissão para realizar esta operação.", "Acesso Restrito", MsgIcons.AcessoNegado);

        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            if (this.Processos != null && this.Processos.Count > 0)
            {
                lblProcesso.Text = "Baixando informações: " + (Indice - Processos.Count + 1) + " de " + Indice;
                this.BaixarAtualizacao(Processos[0]);
                this.Processos.RemoveAt(0);
            }
            else
            {
                Timer1.Enabled = false;

                if (Contador > 0)
                    msg.CriarMensagem("Baixado(s) " + Contador + " evento(s).", "Sucesso", MsgIcons.Sucesso);
                else
                    msg.CriarMensagem("Não há eventos para o(s) processo(s) selecionado(s)", "Informação", MsgIcons.Informacao);
                ZerarCampos();
                lblProcesso.Text = "Concluído.";
                btnParar.Text = "Ver Atualizações";
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
            Timer1.Enabled = false;
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    private void ZerarCampos()
    {
        btnParar.Text = "Parar";
        this.Contador = 0;
        this.Processos = null;
        this.Indice = 0;
        lblProcesso.Text = "Processando.";
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnParar.Text == "Parar")
                this.Processos = null;
            else
            {
                LinkButton1_ModalPopupExtender.Hide();
                this.Pesquisar();
                btnParar.Text = "Parar";
                lblProcesso.Text = "Processando";
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

    #region __________Triggers_____________

    protected void btnParar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upGridEventos);
        WebUtil.InserirTriggerDinamica(btnBaixarAtualizacoes, "Click", upAtualizar);
    }

    protected void btnAtualizacaoProcesso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAtualizar);
    }

    #endregion

}