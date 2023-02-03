using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Runtime.Serialization;

public partial class Vencimentos_CadastroVencimentosDiversos : PageBase
{
    Msg msg = new Msg();

    public IList<int> NotificacoesDoVencimentoDiverso
    {
        get
        {
            if (Session["NotificacoesDoVencimentoDiverso"] == null)
                return null;
            else
                return (IList<int>)Session["NotificacoesDoVencimentoDiverso"];
        }
        set { Session["NotificacoesDoVencimentoDiverso"] = value; }
    }

    public IList<ItemRenovacao> ItensRenovacao
    {
        get
        {
            if (Session["ItensRenovacaoDiversos"] == null)
                return null;
            else
                return (IList<ItemRenovacao>)Session["ItensRenovacaoDiversos"];
        }
        set { Session["ItensRenovacaoDiversos"] = value; }
    }

    #region _______Inner Class_______

    [Serializable()]
    public class ItemRenovacao : ISerializable
    {
        public int idItem;
        public string tipoItem;
        public int diasRenovacao;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("idItem", idItem);
            info.AddValue("tipoItem", tipoItem);
            info.AddValue("diasRenovacao", diasRenovacao);
        }

        public ItemRenovacao(SerializationInfo info, StreamingContext context)
        {
            idItem = (int)info.GetValue("idItem", typeof(int));
            tipoItem = (String)info.GetValue("tipoItem", typeof(string));
            diasRenovacao = (int)info.GetValue("diasRenovacao", typeof(int));
        }

        public ItemRenovacao()
        { }
    }

    #endregion

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloDiversos
    {
        get
        {
            if (Session["ConfiguracaoModuloDiversos"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloDiversos"];
        }
        set { Session["ConfiguracaoModuloDiversos"] = value; }
    }

    public IList<Empresa> EmpresasPermissaoEdicaoModuloDiversos
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloDiversos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloDiversos"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloDiversos"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.VerificarPermissoes();
                hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                this.CarregarGruposEconomicos();
                this.CarregarTiposDiversos();
                btnUploadDiverso.Visible = hfId.Value.ToInt32() > 0 ? true : false;
                this.NotificacoesDoVencimentoDiverso = new List<int>();
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                    this.CarregarDiverso(Convert.ToInt32("" + hfId.Value));
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

    #region __________Métodos____________

    private void CarregarDiverso(int p)
    {
        Diverso diverso = Diverso.ConsultarPorId(p);
        if (diverso != null)
        {
            if (this.ConfiguracaoModuloDiversos == null)
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

            if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) 
            {
                if(diverso.Empresa == null || (this.EmpresasPermissaoEdicaoModuloDiversos == null || this.EmpresasPermissaoEdicaoModuloDiversos.Count == 0) || (this.EmpresasPermissaoEdicaoModuloDiversos != null && !this.EmpresasPermissaoEdicaoModuloDiversos.Contains(diverso.Empresa)))
                    Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
            }

            this.CarregarGruposEconomicos();
            ddlGrupoEconomico.SelectedValue = diverso.Empresa.GrupoEconomico.Id.ToString();
            this.CarregarEmpresas();
            ddlEmpresa.SelectedValue = diverso.Empresa.Id.ToString();
            this.CarregarTiposDiversos();
            ddlTipoDiverso.SelectedValue = diverso.GetUltimoVencimento.StatusDiverso.TipoDiverso.Id.ToString();
            this.CarregarStatusDiversos();
            ddlStatus.SelectedValue = diverso.GetUltimoVencimento.StatusDiverso.Id.ToString();
            tbxDescricao.Text = diverso.Descricao;
            tbxDetalhamento.Text = diverso.Detalhamento;
            chkPeriodico.Checked = diverso.GetUltimoVencimento.Periodico;
            tbxVencimento.Text = diverso.GetUltimoVencimento.Data.ToString("dd/MM/yyyy");
            ibtnAddNotificacoes.Enabled = hfId.Value.ToInt32() > 0;
            ibtnAddNotificacoes.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";
            visualizacoes.Visible = true;
            this.NotificacoesDoVencimentoDiverso = new List<int>();
            this.NotificacoesDoVencimentoDiverso = this.GetIdDasNotificacoes(diverso.GetUltimoVencimento.Notificacoes);
            grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiverso);
            grvNotificacoes.DataBind();
            
        }
    }

    private void CarregarNotificacoesDoVencimento()
    {
        grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiverso);
        grvNotificacoes.DataBind();
    }

    private void CarregarGruposEconomicos()
    {
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarEmpresas()
    {
        ddlEmpresa.Items.Clear();
        ddlEmpresa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoEdicaoModuloDiversos != null ? this.EmpresasPermissaoEdicaoModuloDiversos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoEdicaoModuloDiversos != null ? this.EmpresasPermissaoEdicaoModuloDiversos : new List<Empresa>();
        }
        else
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            empresas = c.Empresas != null ? c.Empresas : new List<Empresa>();
        }

        if (empresas != null && empresas.Count > 0)
        {
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void CarregarTiposDiversos()
    {
        ddlTipoDiverso.DataValueField = "Id";
        ddlTipoDiverso.DataTextField = "Nome";
        ddlTipoDiverso.DataSource = TipoDiverso.ConsultarTodosOrdemAlfabetica();
        ddlTipoDiverso.DataBind();
        ddlTipoDiverso.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarStatusDiversos()
    {
        TipoDiverso tipo = TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32());
        ddlStatus.DataValueField = "Id";
        ddlStatus.DataTextField = "Nome";
        ddlStatus.DataSource = tipo != null && tipo.StatusDiversos != null ? tipo.StatusDiversos : new List<StatusDiverso>();
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private IList<int> GetIdDasNotificacoes(IList<Notificacao> notificacoes)
    {
        IList<int> retorno = new List<int>();
        foreach (Notificacao item in notificacoes)
        {
            retorno.Add(item.Id);
        }
        return retorno;
    }

    private IList<Notificacao> RecarregarNotificacoes(IList<int> notificacoesDaSessao)
    {
        IList<Notificacao> lista = new List<Notificacao>();
        if (notificacoesDaSessao != null)
        {
            foreach (int item in notificacoesDaSessao)
            {
                Notificacao not = Notificacao.ConsultarPorId(item);
                if (lista.Contains(not))
                {
                    lista.Remove(not);
                    lista.Add(not);
                }
                else
                {
                    lista.Add(not);
                }
            }
        }
        return lista;
    }

    private void VerificarPermissoes()
    {

        ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDiversos.Id);
        else
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

        if (this.ConfiguracaoModuloDiversos == null)
        {
            this.HabilitarControls(false);
            return;
        }

        if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral == null)
                this.HabilitarControls(false);
            else if (this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                this.HabilitarControls(true);
            else
                this.HabilitarControls(false);

            this.EmpresasPermissaoEdicaoModuloDiversos = null;
        }

        if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) 
        {
            this.EmpresasPermissaoEdicaoModuloDiversos = Empresa.ObterEmpresasQueOUsuarioPodeEditarDoModulo(moduloDiversos, this.UsuarioLogado);
            if (this.EmpresasPermissaoEdicaoModuloDiversos != null && this.EmpresasPermissaoEdicaoModuloDiversos.Count > 0)
                this.HabilitarControls(true);
            else
                this.HabilitarControls(false);
        }
        
    }

    private void HabilitarControls(bool habilitar)
    {
        btnAdicionarTipo.Visible = ibtnExcluirTipo.Visible = btnAdicionarStatus.Visible = ibtnExcluirStatus.Visible = lxbVisualizarVencimentos.Visible = lkbHistoricos.Visible = btnUploadDiverso.Visible =
            ibtnAddNotificacoes.Visible = Button1.Visible = btnNovo.Visible = btnExcluir.Visible = habilitar;
    }

    private void SalvarTipoDiverso()
    {
        TipoDiverso tipo = new TipoDiverso();
        tipo.Nome = tbxNomeTipo.Text;
        tipo = tipo.Salvar();
        msg.CriarMensagem("Tipo de Vencimento salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void SalvarStatusDiverso()
    {
        StatusDiverso status = new StatusDiverso();
        status.Nome = tbxNomestatus.Text;
        TipoDiverso tipo = TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32());
        status.TipoDiverso = tipo;
        status = status.Salvar();
        tipo.StatusDiversos.Add(status);
        tipo = tipo.Salvar();
        msg.CriarMensagem("Status salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void Salvar()
    {
        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso == null)
            diverso = new Diverso();
        diverso.Empresa = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());
        diverso.TipoDiverso = TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32());
        diverso.Descricao = tbxDescricao.Text;
        diverso.Detalhamento = tbxDetalhamento.Text;
        VencimentoDiverso vencimento = diverso.GetUltimoVencimento;
        if (vencimento == null)
            vencimento = new VencimentoDiverso();
        this.VerificarAlteracaoDeStatusDoVencimento();
        vencimento.StatusDiverso = StatusDiverso.ConsultarPorId(ddlStatus.SelectedValue.ToInt32());
        vencimento.Data = tbxVencimento.Text.ToSqlDateTime();
        vencimento.Periodico = chkPeriodico.Checked;
        vencimento.Notificacoes = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiverso);
        vencimento = (VencimentoDiverso)vencimento.Salvar();
        if (diverso.VencimentosDiversos == null)
            diverso.VencimentosDiversos = new List<VencimentoDiverso>();
        if (!diverso.VencimentosDiversos.Contains(vencimento))
            diverso.VencimentosDiversos.Add(vencimento);
        diverso = diverso.Salvar();
        hfId.Value = diverso.Id.ToString();
        ibtnAddNotificacoes.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAddNotificacoes.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";
        visualizacoes.Visible = true;
        vencimento.Diverso = diverso;
        vencimento = (VencimentoDiverso)vencimento.Salvar();
        msg.CriarMensagem("Vencimento salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

    }

    private IList<Notificacao> RecarregarNotificacoes(IList<Notificacao> notificacoesDaSessao)
    {
        IList<Notificacao> lista = new List<Notificacao>();
        if (notificacoesDaSessao != null)
        {
            foreach (Notificacao item in notificacoesDaSessao)
            {
                Notificacao not = Notificacao.ConsultarPorId(item.Id);
                if (lista.Contains(not))
                {
                    lista.Remove(not);
                    lista.Add(not);
                }
                else
                {
                    lista.Add(not);
                }
            }
        }
        return lista;
    }

    private void CarregarPopUpNotificacao(bool marcar, params int[] dias)
    {
        cblDias.Items.Clear();
        chkGruposEconomicos.Items.Clear();
        chkEmpresas.Items.Clear();

        cblDias.Items.Add(new ListItem("No dia do vencimento", "0"));
        foreach (int i in dias)
            cblDias.Items.Add(new ListItem(i.ToString(), i.ToString()));


        foreach (ListItem i in cblDias.Items)
            i.Selected = marcar;

        if (cblDias.Items.Count <= 0)
        {
            msg.CriarMensagem("Você não pode inserir notificações em Vencimentos que já ocorreram.", "Alerta", MsgIcons.Alerta);
            return;
        }
        else
        {
            ModalExtenderNotificoes.Show();
        }

        this.CarregarListaEmails(chkEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(chkGruposEconomicos, this.CarregarEmailsCliente().Split(';'));
    }

    private void CarregarListaEmails(CheckBoxList cbl, string[] lista)
    {
        cbl.Items.Clear();
        foreach (string s in lista)
            if (s.IsNotNullOrEmpty())
                cbl.Items.Add(new ListItem(s, s));
        if (cbl.Items.Count <= 0)
        {
            cbl.Items.Add(new ListItem("Não há emails cadastrados.", "0"));
            cbl.Items[0].Enabled = cbl.Items[0].Selected = false;
        }
        foreach (ListItem item in cbl.Items)
        {
            item.Selected = true;
        }
    }

    private string CarregarEmailsCliente()
    {
        string email = "";
        if (ddlGrupoEconomico.SelectedIndex > 0)
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            if (c.Contato != null)
                email = c.Contato.Email.IsNotNullOrEmpty() ? c.Contato.Email + ";" : "";
        }
        return email;
    }

    private string CarregarEmailsEmpresa()
    {
        Empresa p = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());
        if (p != null)
            return p.Contato.Email;
        return "";
    }

    private string GetEmailsSelecionados()
    {
        string emails = "";
        foreach (ListItem l in chkGruposEconomicos.Items)
            if (l.Selected && l.Value != "0")
                emails += l.Value.ToString() + ";";
        foreach (ListItem l in chkEmpresas.Items)
            if (l.Selected && l.Value != "0")
                emails += l.Value.ToString() + ";";
        emails += tbxOutrosEmails.Text.ToString();
        return emails;
    }

    private void SalvarNotificacao()
    {
        if (this.NotificacoesDoVencimentoDiverso == null)
            this.NotificacoesDoVencimentoDiverso = new List<int>();

        string emails = this.GetEmailsSelecionados();
        if (emails.IsNotNullOrEmpty())
        {
            foreach (ListItem l in cblDias.Items)
            {
                if (l.Selected)
                {
                    Notificacao n = new Notificacao(ModuloPermissao.ModuloVencimentoDiverso);
                    n.Emails = emails;
                    n.DiasAviso = l.Value.ToInt32();
                    // criar template da notificação de diversos n.Template = TemplateNotificacao.Exigencia;
                    Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
                    VencimentoDiverso v = diverso.GetUltimoVencimento;
                    n.Vencimento = v;
                    n.Template = TemplateNotificacao.TemplateVencimentoDiverso;
                    n = n.Salvar();
                    this.NotificacoesDoVencimentoDiverso.Add(n.Id);
                }
            }
        }
    }

    private void ExibirDatasVencimentosPeriodicosRenovacao(int diasRenovacao, IList<VencimentoDiverso> vencimentos)
    {
        chkVencimentosPeriodicos.Items.Clear();
        DateTime dataAux = vencimentos[vencimentos.Count - 1].Data;
        while (dataAux <= DateTime.Now)
        {
            dataAux = dataAux.AddDays(+diasRenovacao);
            chkVencimentosPeriodicos.Items.Add(new ListItem("Vencimento: " + dataAux.ToString("dd/MM/yyyy"), dataAux.ToString("dd/MM/yyyy")));
        }
        lblRenovacaoVencimentosPeriodicos_popupextender.Show();
    }

    public int ObterDiasEntreAsRenovacoes(IList<VencimentoDiverso> vencimentos)
    {
        if (vencimentos != null && vencimentos.Count > 0)
        {
            if (vencimentos.Count == 1)
            {
                return 0;
            }
            else
            {
                return vencimentos[vencimentos.Count - 1].Data.Subtract(vencimentos[vencimentos.Count - 2].Data).Days;
            }
        }

        return 0;
    }

    private IList<VencimentoDiverso> RetornarVencimentosDoItemDaRenovacao(string tipoItem, int idItem)
    {
        if (tipoItem.ToUpper() == "VENCIMENTODIVERSO")
        {
            Diverso diverso = Diverso.ConsultarPorId(idItem);
            return diverso.VencimentosDiversos;
        }

        return null;
    }

    private void RenovarVencimentosPeriodicos()
    {
        IList<DateTime> datasDeRenovacao = new List<DateTime>();

        foreach (ListItem l in chkVencimentosPeriodicos.Items)
            if (l.Selected && l.Value != "")
                datasDeRenovacao.Add(Convert.ToDateTime(l.Value.ToString()));

        RenovarPeriodicos.RenovarVencimentosPeriodicos(datasDeRenovacao, hfIdTipoVencimentoPeriodico.Value, hfIdItemVencimentoPeriodico.Value.ToInt32());
        msg.CriarMensagem("Vencimentos renovados com sucesso", "Sucesso", MsgIcons.Sucesso);
        lblRenovacaoVencimentosPeriodicosDatas_popupextender.Hide();
        lblRenovacaoVencimentosPeriodicos_popupextender.Hide();
        this.ExibirUltimoVencimento(hfIdItemVencimentoPeriodico.Value.ToInt32());
    }

    private void ExibirUltimoVencimento(int idDiverso)
    {
        Diverso diverso = Diverso.ConsultarPorId(idDiverso);
        if (diverso != null && diverso.GetUltimoVencimento != null)
        {
            tbxVencimento.Text = diverso.GetUltimoVencimento.Data.ToShortDateString();
            this.NotificacoesDoVencimentoDiverso = this.GetIdDasNotificacoes(diverso.GetUltimoVencimento.Notificacoes);
            grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiverso);
            grvNotificacoes.DataBind();
        }
    }

    private void VisualizarVencimentos()
    {
        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        this.CarregarVencimentos();
        ddlVencimentos.SelectedValue = diverso.GetUltimoVencimento.Id.ToString();
        this.CarregarStatusVencimentosVisualizados(diverso.TipoDiverso);
        ddlStatusVencimentos.SelectedValue = diverso.GetUltimoVencimento.StatusDiverso.Id.ToString();
        this.CarregarNotificoesDoVencimentoVizualizado(diverso.GetUltimoVencimento.Notificacoes);
    }

    private void CarregarNotificoesDoVencimentoVizualizado(IList<Notificacao> notificacoes)
    {
        grvNotificacaoVencimentos.DataSource = notificacoes != null ? notificacoes : new List<Notificacao>();
        grvNotificacaoVencimentos.DataBind();
    }

    private void CarregarStatusVencimentosVisualizados(TipoDiverso tipo)
    {
        ddlStatusVencimentos.DataValueField = "Id";
        ddlStatusVencimentos.DataTextField = "Nome";
        ddlStatusVencimentos.DataSource = tipo != null && tipo.StatusDiversos != null ? tipo.StatusDiversos : new List<StatusDiverso>();
        ddlStatusVencimentos.DataBind();
        ddlStatusVencimentos.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVencimentos()
    {
        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        ddlVencimentos.Items.Clear();

        if (diverso.VencimentosDiversos != null && diverso.VencimentosDiversos.Count > 0)
        {
            for (int i = diverso.VencimentosDiversos.Count - 1; i > -1; i--)
            {
                ddlVencimentos.Items.Add(new ListItem(diverso.VencimentosDiversos[i].Data.ToString("dd/MM/yyyy"), diverso.VencimentosDiversos[i].Id.ToString()));
            }
        }
    }

    private void RemoverVencimento()
    {
        Diverso d = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
        d.VencimentosDiversos.Remove(vencimento);
        d = d.Salvar();
        vencimento.Excluir();
        ddlVencimentos.Items.RemoveAt(ddlVencimentos.SelectedIndex);
        msg.CriarMensagem("Vencimento excluído com Sucesso!", "Sucesso", MsgIcons.Sucesso);
        this.VisualizarVencimentos();
    }

    private void SalvarVencimento()
    {
        VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
        this.VerificarAlteracaoDeStatusHistoricoVencimento(ddlVencimentos.SelectedIndex);
        vencimento.StatusDiverso = StatusDiverso.ConsultarPorId(ddlStatusVencimentos.SelectedValue.ToInt32());
        msg.CriarMensagem("Vencimento salvo com Sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void VerificarAlteracaoDeStatusHistoricoVencimento(int indexVencimento)
    {
        string alteracao = "";

        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso != null && diverso.VencimentosDiversos[indexVencimento] != null && diverso.VencimentosDiversos[indexVencimento].StatusDiverso.Id.ToString() != ddlStatusVencimentos.SelectedValue)
        {
            alteracao += "(Alteração no Status do vencimento referente a " + diverso.Descricao + ", da Empresa " + diverso.Empresa.Nome + ". De: " + diverso.VencimentosDiversos[indexVencimento].StatusDiverso.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + ")";
        }
        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        chkEnviarEmail.Checked = true;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresasAlteracao, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
    }

    private void SalvarHistoricoAlteracaoStatus()
    {
        Historico h = new Historico();
        h.DataPublicacao = DateTime.Now;
        h.Alteracao = lblAlteracao.Text;
        h.Observacao = tbxHistoricoAlteracao.Text;

        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        h.Diverso = diverso;

        h = h.Salvar();

        if (chkEnviarEmail.Checked)
        {
            Email email = new Email();
            foreach (ListItem l in ckbGrupos.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());
            foreach (ListItem l in ckbEmpresasAlteracao.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());

            email.Assunto = "Alteração de Status de Vencimento - Sistema Sustentar";
            email.Mensagem = email.CriarTemplateParaMudancaDeStatusDeVencimento(h);
            email.EnviarAutenticado(25, false);
        }

        lblAlteracaoStatus_ModalPopupExtender.Hide();
        msg.CriarMensagem("Histórico registrado com Sucesso", "Sucesso");
    }

    private void VerificarAlteracaoDeStatusDoVencimento()
    {
        string alteracao = "";

        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso != null && diverso.GetUltimoVencimento != null && diverso.GetUltimoVencimento.StatusDiverso.Id.ToString() != ddlStatus.SelectedValue)
        {
            alteracao += "(Alteração no Status do vencimento referente a " + diverso.Descricao + ", da Empresa " + diverso.Empresa.Nome + ". De: " + diverso.GetUltimoVencimento.StatusDiverso.Nome + " - Para: " + ddlStatus.SelectedItem.Text + ")";
        }
        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        chkEnviarEmail.Checked = true;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresasAlteracao, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
    }

    private void ExcluirTipo(TipoDiverso tipo)
    {
        if (tipo.StatusDiversos != null && tipo.StatusDiversos.Count > 0)
        {
            foreach (StatusDiverso item in tipo.StatusDiversos)
            {
                item.ConsultarPorId();
                item.Excluir();
            }
        }
        tipo.Excluir();
        msg.CriarMensagem("Tipo de vencimento excluído com sucesso", "Sucesso", MsgIcons.Sucesso);

    }

    private void VisualizarHistorico(Diverso d)
    {
        grvHistoricos.DataSource = d.Historicos.OrderByDescending(i => i.Id).ToList(); ;
        grvHistoricos.DataBind();
    }

    public string bindDataRegistro(Object o)
    {
        Historico h = (Historico)o;
        if (h != null)
            return h.DataPublicacao.ToShortDateString();
        return "";
    }

    private void Excluir()
    {
        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso.VencimentosDiversos != null && diverso.VencimentosDiversos.Count > 0)
            this.ExcluirVencimentosDODiverso(diverso);

        if (diverso.Arquivos != null && diverso.Arquivos.Count > 0)
            this.ExcluirArquivosDODiverso(diverso);

        diverso.Excluir();
        this.Novo();
        msg.CriarMensagem("Vencimento excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void ExcluirVencimentosDODiverso(Diverso diverso)
    {
        foreach (VencimentoDiverso item in diverso.VencimentosDiversos)
        {
            item.ConsultarPorId();
            item.Excluir();
        }
    }

    private void ExcluirArquivosDODiverso(Diverso diverso)
    {
        foreach (ArquivoFisico item in diverso.Arquivos)
        {
            item.ConsultarPorId();
            item.Excluir();
        }
    }

    public void Novo()
    {
        hfId.Value = "";
        this.NotificacoesDoVencimentoDiverso = null;
        Response.Redirect("CadastroVencimentosDiversos.aspx", false);
    }

    private void CriarHistorico()
    {
        if (!tbxTituloObs.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario informar um Título", "Alerta", MsgIcons.Alerta);
            return;
        }

        Historico h = new Historico();
        h.DataPublicacao = DateTime.Now;
        h.Alteracao = tbxTituloObs.Text;
        h.Observacao = tbxObservacaoObs.Text;

        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        h.Diverso = diverso;
        h = h.Salvar();
        diverso.Historicos.Add(h);
        grvHistoricos.DataSource = diverso.Historicos.OrderByDescending(i => i.Id).ToList();
        grvHistoricos.DataBind();

        tbxTituloObs.Text = "";
        tbxObservacaoObs.Text = "";

    }

    private void EnviarHistoricoPorEmail()
    {
        Email email = new Email();
        foreach (ListItem l in chkGruposHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        foreach (ListItem l in chkEmpresaHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        email.AdicionarDestinatario(tbxEmailsHistorico.Text.ToString());

        Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso != null)
        {
            email.Assunto = "Registros Históricos do Vencimento Diverso referente a : " + diverso.Descricao + ", da empresa " + diverso.Empresa.Nome + " - " + diverso.Empresa.GetNumeroCNPJeCPFComMascara;
            email.Mensagem = email.CriarTemplateParaHistoricoGeral(diverso.Historicos, email.Assunto);
        }

        if (!email.EnviarAutenticado(25, false))
            msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
        else
            msg.CriarMensagem("E-mails enviados com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    #endregion

    #region __________Eventos____________

    protected void ddlGrupoEconomico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresas();
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

    protected void ddlTipoDiverso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatusDiversos();
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

    protected void btnSalvarTipo_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoDiverso();
            ModalTipoDiverso.Hide();
            this.CarregarTiposDiversos();
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

    protected void grvNotificacoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {            
                grvNotificacoes.PageIndex = 1;
                foreach (GridViewRow item in grvNotificacoes.Rows)
                {
                    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                    {
                        Notificacao notificacao = new Notificacao(grvNotificacoes.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                        this.NotificacoesDoVencimentoDiverso.Remove(notificacao.Id);
                        notificacao.Excluir();
                    }
                }

                grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiverso);
                grvNotificacoes.DataBind();            
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

    protected void btnAdicionarStatus_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlTipoDiverso.SelectedIndex == 0)
            {
                msg.CriarMensagem("Selecione primeiro o tipo de vencimento para poder adicionar um status", "Alerta", MsgIcons.Alerta);
                return;
            }
            ModalStatusDiverso.Show();
            tbxTipoStatus.Text = ddlTipoDiverso.SelectedItem.Text;
            tbxNomestatus.Text = "";
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

    protected void btnSalvarStatus_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarStatusDiverso();
            ModalStatusDiverso.Hide();
            this.CarregarStatusDiversos();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomico.SelectedIndex == 0)
            {
                msg.CriarMensagem("Selecione primeiro o grupo econômico para poder cadastrar um vencimento", "Atenção", MsgIcons.Alerta);
                return;
            }

            if (ddlEmpresa.SelectedIndex == 0)
            {
                msg.CriarMensagem("Selecione primeiro a empresa para poder cadastrar um vencimento", "Atenção", MsgIcons.Alerta);
                return;
            }

            if (ddlTipoDiverso.SelectedIndex == 0)
            {
                msg.CriarMensagem("Selecione o tipo do vencimento para poder prosseguir", "Atenção", MsgIcons.Alerta);
                return;
            }

            if (ddlStatus.SelectedIndex == 0)
            {
                msg.CriarMensagem("Selecione o status do vencimento para poder prosseguir", "Atenção", MsgIcons.Alerta);
                return;
            }

            if (tbxVencimento.Text == null || tbxVencimento.Text == "" || tbxVencimento.Text == " ")
            {
                msg.CriarMensagem("Informe a data do vencimento para poder prosseguir", "Atenção", MsgIcons.Alerta);
                return;
            }

            this.Salvar();

            if (hfId.Value.ToInt32() > 0)
            {
                btnUploadDiverso.Visible = true;                
            }
            else
            {
                btnUploadDiverso.Visible = false;
            }

            if (chkPeriodico.Checked)
            {
                this.ItensRenovacao = new List<ItemRenovacao>();

                Diverso diverso = Diverso.ConsultarPorId(hfId.Value.ToInt32());
                if (diverso != null && diverso.GetUltimoVencimento != null && diverso.GetUltimoVencimento.Data <= DateTime.Now)
                {
                    ItemRenovacao item = new ItemRenovacao();
                    item.idItem = diverso.Id;
                    item.tipoItem = "VENCIMENTODIVERSO";
                    this.ItensRenovacao.Add(item);
                }

                if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
                {
                    rptRenovacoes.DataSource = this.ItensRenovacao;
                    rptRenovacoes.DataBind();
                    lblRenovacaoVencimentosPeriodicos_popupextender.Show();
                }
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

    protected void ibtnAddNotificacoes_Init(object sender, EventArgs e)
    {
        //  Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ibtnAddNotificacoes.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAddNotificacoes.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/icone_adicionar_semcor.png";
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void lxbVisualizarVencimentos_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o vencimento para visualizar o histórico", "Atenção", MsgIcons.Alerta);
                return;
            }
            this.VisualizarVencimentos();
            ModalExtenderVisualizarVencimentos.Show();
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

    protected void ddlVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
            TipoDiverso tipo = TipoDiverso.ObterTipoDoVencimento(vencimento);
            this.CarregarStatusVencimentosVisualizados(tipo);
            ddlStatusVencimentos.SelectedValue = vencimento.StatusDiverso.Id.ToString();
            this.CarregarNotificoesDoVencimentoVizualizado(vencimento.Notificacoes);
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

    protected void grvNotificacaoVencimentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvNotificacaoVencimentos.PageIndex = e.NewPageIndex;
            grvNotificacaoVencimentos.DataSource = VencimentoDiverso.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32()).Notificacoes;
            grvNotificacaoVencimentos.DataBind();
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

    protected void btnSalvarvencimentos_Click(object sender, EventArgs e)
    {
        try
        {
            
                this.SalvarVencimento();
                ModalExtenderVisualizarVencimentos.Hide();
            
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

    protected void btnFecharStatus_Click(object sender, EventArgs e)
    {
        lblAlteracaoStatus_ModalPopupExtender.Hide();
    }

    protected void btnAlterarStatus_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarHistoricoAlteracaoStatus();
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            hfId.Value = "";
            this.NotificacoesDoVencimentoDiverso = null;
            Response.Redirect("CadastroVencimentosDiversos.aspx", false);
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

    protected void grvNotificacoes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacoes.PageIndex = e.NewPageIndex;
        grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiverso);
        grvNotificacoes.DataBind();
    }

    protected void ibtnRemoverVencimento_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            
                if (ddlVencimentos.Items.Count <= 1)
                {
                    msg.CriarMensagem("Não é possivel excluir este vencimento.", "Alerta", MsgIcons.Alerta);
                    return;
                }
                this.RemoverVencimento();
            
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

    protected void ibtnExcluirTipo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
                if (ddlTipoDiverso.SelectedIndex == 0)
                {
                    msg.CriarMensagem("Selecione primeiro um tipo para poder excluí-lo", "Atenção", MsgIcons.Alerta);
                    return;
                }
                TipoDiverso tipo = TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32());

                if (tipo != null && tipo.Diversos != null && tipo.Diversos.Count > 0)
                {
                    msg.CriarMensagem("Não é possível excluir este tipo de vencimento, pois existem vencimentos associados a ele", "Atenção", MsgIcons.Alerta);
                    return;
                }
                else
                    this.ExcluirTipo(tipo);
                this.CarregarTiposDiversos();
            
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

    protected void btnAlterarStatus_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsGrupo();marcarEmailsEmpresa();</script>", false);
    }

    protected void ibtnExcluirTipo_PreRender(object sender, EventArgs e)
    {
        //  Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este Tipo de Vencimento serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnExcluirStatus_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este Status de Vencimento serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnExcluirStatus_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro o status do vencimento para poder excluí-lo", "Atenção", MsgIcons.Alerta);
                return;
            }
            StatusDiverso status = StatusDiverso.ConsultarPorId(ddlStatus.SelectedValue.ToInt32());
            if (status != null && status.VencimentosDiversos != null && status.VencimentosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir este status, pois existem vencimentos associados a ele", "Atenção", MsgIcons.Alerta);
                return;
            }
            TipoDiverso tipo = TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32());
            tipo.StatusDiversos.Remove(status);
            tipo = tipo.Salvar();
            status.Excluir();
            msg.CriarMensagem("Status excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            this.CarregarStatusDiversos();
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

    protected void btnAdicionarTipo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tbxNomeTipo.Text = "";
            ModalTipoDiverso.Show();
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

    protected void lkbHistoricos_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o vencimento para poder visualizar o histórico", "Atenção", MsgIcons.Alerta);
                return;
            }
            Diverso d = Diverso.ConsultarPorId(hfId.Value.ToInt32());
            if (d != null && d.Historicos != null && d.Historicos.Count > 0)
            {
                ModalExtenderHistoricos.Show();
                this.VisualizarHistorico(d);
            }
            else
            {
                ModalExtenderHistoricos.Show();
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Não é possível excluir um vencimento que ainda não foi salvo", "Atenção", MsgIcons.Alerta);
                return;
            }
            this.Excluir();
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

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((Button)sender, this.UsuarioLogado);
        Button btn = (Button)sender;

        if (btn.Enabled == true)
            WebUtil.AdicionarConfirmacao(btn, "Todos os dados referentes a este Vencimento serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnAddObs_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CriarHistorico();
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

    protected void lxbVisualizarVencimentos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void btnEnviarHistorico_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEnvioHistorico);
    }

    protected void btnEnviarHistorico_Click(object sender, EventArgs e)
    {
        try
        {
            chkGruposHistorico.Items.Clear();
            chkEmpresaHistorico.Items.Clear();

            lblEnvioHistorico_popupextender.Show();

            this.CarregarListaEmails(chkEmpresaHistorico, this.CarregarEmailsEmpresa().Split(';'));
            this.CarregarListaEmails(chkGruposHistorico, this.CarregarEmailsCliente().Split(';'));

            tbxEmailsHistorico.Text = "";
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

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            this.EnviarHistoricoPorEmail();
            lblEnvioHistorico_popupextender.Hide();
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

    protected void btnUploadDiverso_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o Vencimento Diverso para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfId.Value.ToInt32() + "&idEmpresa=" + ddlEmpresa.SelectedValue + "&idCliente=" + ddlGrupoEconomico.SelectedValue + "&tipo=Diverso"));
            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnSalvarNotificacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarNotificacao();
            ModalExtenderNotificoes.Hide();
            this.CarregarNotificacoesDoVencimento();
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

    protected void ibtnAddNotificacoes_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void btnRenovarVencimentosPeriodicos_Click(object sender, EventArgs e)
    {
        try
        {
            this.RenovarVencimentosPeriodicos();
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

    protected void rptRenovacoes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "SelComand":

                    TextBox txtName = (TextBox)e.Item.FindControl("tbxDiasRenovacaoPeriodicos");
                    if (txtName != null && txtName.Text.IsNotNullOrEmpty() && txtName.Text.ToInt32() > 0)
                    {
                        string[] splitter = e.CommandArgument.ToString().Split(';');
                        ExibirDatasVencimentosPeriodicosRenovacao(txtName.Text.ToInt32(), RetornarVencimentosDoItemDaRenovacao(splitter[1], splitter[0].ToInt32()));
                        hfIdItemVencimentoPeriodico.Value = splitter[0];
                        hfIdTipoVencimentoPeriodico.Value = splitter[1];
                        hfDiasRenovacaoPeriodicos.Value = txtName.Text;
                        lblRenovacaoVencimentosPeriodicosDatas_popupextender.Show();
                    }
                    else
                    {
                        msg.CriarMensagem("É necessário informar os dias de renovação, para renovar este vencimento", "Alerta", MsgIcons.Alerta);
                        return;
                    }

                    break;
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

    #region ______TriggersDinamicas______

    protected void ibtnExcluir5_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ImageButton ibtn = (ImageButton)sender;

        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void btnSalvarTipo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPTipoDiverso);
    }

    protected void btnSalvarStatus_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPStatus);
    }

    protected void btnAdicionarStatus_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPTituloTipo);
    }

    protected void btnSalvarNotificacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPNotificacoes);
    }

    protected void btnSalvarvencimentos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void Button1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void btnAdicionarTipo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPNomeTipo);
    }

    protected void lkbHistoricos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPHistoricos);
    }

    protected void btnRenovarVencimentosPeriodicos_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsEmpresa();</script>", false);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upDataVencimento);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPNotificacoes);
    }

    protected void rptRenovacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "ItemCommand", upDatasRenovacao);
    }

    #endregion

    #region __________Bindings___________

    public string BindDiasRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return item != null && item.diasRenovacao > 0 ? item.diasRenovacao.ToString() : "";
    }

    public string BindIdItemRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return item != null && item.idItem > 0 ? item.idItem.ToString() : "";
    }

    public string BindTipoItemRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return item != null && item.tipoItem.IsNotNullOrEmpty() ? item.tipoItem : "";
    }

    public string BindNomeItemDaRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return RenovarPeriodicos.NomeItemFormatado(item.tipoItem, item.idItem);
    }

    #endregion

    protected void btnUploadDiverso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }
}