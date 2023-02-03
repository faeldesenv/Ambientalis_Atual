using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;
using Persistencia.Modelo;

public partial class Vencimentos_CadastroVencimentosDiversosContrato : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();
    public IList<int> NotificacoesDoVencimentoDiversoContrato
    {
        get
        {
            if (Session["NotificacoesDoVencimentoDiversoContrato"] == null)
                return null;
            else
                return (IList<int>)Session["NotificacoesDoVencimentoDiversoContrato"];
        }
        set { Session["NotificacoesDoVencimentoDiversoContrato"] = value; }
    }
    public IList<int> NotificacoesDoVencimentoDiversoReajusteContrato
    {
        get
        {
            if (Session["NotificacoesDoVencimentoDiversoReajusteContrato"] == null)
                return null;
            else
                return (IList<int>)Session["NotificacoesDoVencimentoDiversoReajusteContrato"];
        }
        set { Session["NotificacoesDoVencimentoDiversoReajusteContrato"] = value; }
    }    

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloContratos
    {
        get
        {
            if (Session["ConfiguracaoModuloContratos"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloContratos"];
        }
        set { Session["ConfiguracaoModuloContratos"] = value; }
    }

    public IList<Empresa> EmpresasPermissaoEdicaoModuloContratos
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloContratos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloContratos"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloContratos"] = value; }
    }

    public IList<Setor> SetoresPermissaoEdicaoModuloContratos
    {
        get
        {
            if (Session["SetoresPermissaoEdicaoModuloContratos"] == null)
                return null;
            else
                return (IList<Setor>)Session["SetoresPermissaoEdicaoModuloContratos"];
        }
        set { Session["SetoresPermissaoEdicaoModuloContratos"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.VerificarPermissoes();
                this.VerificarPermissoesAProcessos();
                this.NotificacoesDoVencimentoDiversoContrato = null;
                this.NotificacoesDoVencimentoDiversoReajusteContrato = null;

                hfContrato.Value = hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);

                this.CarregarCentroDeCusto();
                this.CarregarFormasRecebimento();
                this.CarregarMoedas();
                this.CarregarStatusDiversos();
                this.CarregarSetor();
                this.CarregarIndices();
                this.CarregarGruposEconomicos();
                this.CarregarEmpresasPesquisaProcesso();
                btnUploadDiverso.Visible = hfId.Value.ToInt32() > 0 ? true : false;
                pnlBotoesProcessos.Visible = hfId.Value.ToInt32() > 0 ? true : false;
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                {
                    lblAcao.Text = "Edição do Contrato";
                    this.CarregarDiverso(hfId.Value.ToInt32());
                    this.HabilitarDesabilitarRenovacao(false);
                }
                else
                {
                    this.HabilitarDesabilitarRenovacao(true);
                    btnRenovar.Visible = false;
                    lblAcao.Text = "Novo Contrato";
                }
                this.VericarSeVisivelContratanteContratada();

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

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDiversos.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

        if (this.ConfiguracaoModuloContratos == null)
        {
            this.HabilitarControls(false);
            return;
        }

        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral == null)
                this.HabilitarControls(false);
            else if (this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                this.HabilitarControls(true);
            else 
                this.HabilitarControls(false);   

            this.EmpresasPermissaoEdicaoModuloContratos = null;
            this.SetoresPermissaoEdicaoModuloContratos = null;
        }

        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.EmpresasPermissaoEdicaoModuloContratos = Empresa.ObterEmpresasQueOUsuarioPodeEditarDoModulo(moduloDiversos, this.UsuarioLogado);
            this.SetoresPermissaoEdicaoModuloContratos = null;
            this.HabilitarControls(true);
        }

        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            this.SetoresPermissaoEdicaoModuloContratos = Setor.ObterSetoresQueOUsuarioPodeEditarDoModulo(this.UsuarioLogado);
            this.EmpresasPermissaoEdicaoModuloContratos = null;
            this.HabilitarControls(true);
        }

    }

    private void VerificarPermissoesAProcessos()
    {
        btnProcessoDNPM.Enabled = btnProcessoDNPM.Visible = this.UsuarioLogado != null && Permissoes.UsuarioPossuiAcessoModuloDNPM(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("DNPM"));
        btnProcessoAmbiental.Enabled = btnProcessoAmbiental.Visible = this.UsuarioLogado != null && Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Meio Ambiente"));
    }

    private void HabilitarControls(bool habilitar)
    {
        btnRenovar.Visible = btnAdicionarFormaRecebimento.Visible = btnEditarFormaRecebimento.Visible = ibtnExcluirFormaRecebimento.Visible = btnAdicionarCentroCusto.Visible = btnEditarCentroCusto.Visible =
            ibtnExcluirCentroCusto.Visible = btnAdicionarSetor.Visible = btnEditarSetor.Visible = ibtnExcluirSetor.Visible = btnAdicionarReajuste.Visible = btnEditarIndice.Visible = ibtnExcluirReajuste.Visible =
            btnAdicionarStatus.Visible = btnEditarStatus.Visible = ibtnExcluirStatus.Visible = btnUploadDiverso.Visible = lkbAditivos.Visible = lkbHistoricos.Visible = lkbAditivos.Visible = lkbProrrogacao.Visible =
            ibtnAddNotificacoes.Visible = ibtnAddNotificacoesReajuste.Visible = btnSalvar.Visible = btnExcluir.Visible = btnRenovar.Visible = btnNovo.Enabled = btnNovo.Visible = btnSelecionarMaisContratos.Enabled =
            btnSelecionarMaisContratos.Visible = btnSelecionarMaisProcessos.Enabled = btnSelecionarMaisProcessos.Visible = btnSalvarAditivo.Enabled = btnSalvarAditivo.Visible = btnSelecionarMaisContratos.Enabled = btnSelecionarMaisContratos.Visible = btnSelecionarMaisProcessos.Visible = btnSelecionarMaisProcessos.Enabled = habilitar;
    }

    private void CarregarFormasRecebimento()
    {
        ddlFormaRecebimento.DataValueField = "Id";
        ddlFormaRecebimento.DataTextField = "Nome";
        ddlFormaRecebimento.DataSource = FormaRecebimento.ConsultarTodos();
        ddlFormaRecebimento.DataBind();
        ddlFormaRecebimento.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void SalvarFormaRecebimento()
    {
        FormaRecebimento forma = new FormaRecebimento();
        forma = FormaRecebimento.ConsultarPorId(ddlFormaRecebimento.SelectedValue.ToInt32());
        if (forma == null)
            forma = new FormaRecebimento();
        forma.Nome = tbxForma.Text;
        forma = forma.Salvar();
        ddlFormaRecebimento.Items.Add(new ListItem(forma.Nome, forma.Id.ToString()));
        msg.CriarMensagem("Forma de Recebimento/Pagamento salva com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void CarregarMoedas()
    {
        ddlMoeda.DataValueField = "Id";
        ddlMoeda.DataTextField = "Nome";
        ddlMoeda.DataSource = Moeda.ConsultarTodos();
        ddlMoeda.DataBind();
        ddlMoeda.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void SalvarSetor()
    {
        Setor setor = new Setor();
        setor = Setor.ConsultarPorId(ddlSetor.SelectedValue.ToInt32());
        if (setor == null)
            setor = new Setor();
        setor.Nome = tbxSetor.Text;
        setor = setor.Salvar();

        Usuario usuario = UsuarioLogado.ConsultarPorId();
        
        usuario = usuario.Salvar();

        UsuarioLogado = usuario;

        ddlSetor.Items.Add(new ListItem(setor.Nome, setor.Id.ToString()));

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR) 
        {
            if (setor.UsuariosEdicao == null)
                setor.UsuariosEdicao = new List<Usuario>();
                       

            if (setor.UsuariosVisualizacao != null && setor.UsuariosVisualizacao.Count > 0)
            {
                if (!setor.UsuariosVisualizacao.Contains(usuario))
                    setor.UsuariosVisualizacao.Add(usuario);
            }

            if (!setor.UsuariosEdicao.Contains(usuario))
                setor.UsuariosEdicao.Add(usuario);

            setor = setor.Salvar();

            if (this.SetoresPermissaoEdicaoModuloContratos == null)
                this.SetoresPermissaoEdicaoModuloContratos = new List<Setor>();

            this.SetoresPermissaoEdicaoModuloContratos.Add(setor);                

        }        

        msg.CriarMensagem("Setor salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        if (contrato != null)
        {
            email.Assunto = "Registros Históricos de Contratos referente a : " + contrato.Numero + ", da empresa " + contrato.Empresa.Nome + " - " + contrato.Empresa.GetNumeroCNPJeCPFComMascara;
            email.Mensagem = email.CriarTemplateParaHistoricoGeral(contrato.Historicos, email.Assunto);
        }

        if (! email.EnviarAutenticado(25, false))
            msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
        else
            msg.CriarMensagem("E-mails enviados com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void VisualizarHistorico(ContratoDiverso d)
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
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        contrato.Excluir();
        this.Novo();
        msg.CriarMensagem("Vencimento excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    public void Novo()
    {
        hfId.Value = "";
        this.NotificacoesDoVencimentoDiversoContrato = null;
        Response.Redirect("CadastroVencimentosDiversos.aspx", false);
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

        ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        h.ContratoDiverso = diverso;
        h = h.Salvar();
        diverso.Historicos.Add(h);
        grvHistoricos.DataSource = diverso.Historicos.OrderByDescending(i => i.Id).ToList();
        grvHistoricos.DataBind();

        tbxTituloObs.Text = "";
        tbxObservacaoObs.Text = "";

    }

    private void CarregarDiverso(int p)
    {
        ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(p);
        if (diverso != null)
        {
            if (this.ConfiguracaoModuloContratos == null)
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                if (diverso.Empresa == null || (this.EmpresasPermissaoEdicaoModuloContratos == null || this.EmpresasPermissaoEdicaoModuloContratos.Count == 0) || (this.EmpresasPermissaoEdicaoModuloContratos != null && !this.EmpresasPermissaoEdicaoModuloContratos.Contains(diverso.Empresa)))
                    Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
            }

            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
            {
                if (diverso.Setor == null || (this.SetoresPermissaoEdicaoModuloContratos == null || this.SetoresPermissaoEdicaoModuloContratos.Count == 0) || (this.SetoresPermissaoEdicaoModuloContratos != null && !this.SetoresPermissaoEdicaoModuloContratos.Contains(diverso.Setor)))
                    Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
            }

            hfId.Value = diverso.Id.ToString();
            lblAcao.Text = "Edição do Contrato [" + diverso.Numero + "] - " + diverso.DataAbertura.ToShortDateString();
            this.CarregarRenovacoes();

            ddlGrupoEconomico.SelectedValue = diverso.Empresa.GrupoEconomico.Id.ToString();
            this.CarregarEmpresas();
            ddlEmpresa.SelectedValue = diverso.Empresa.Id.ToString();
            ddlComo.SelectedValue = diverso.Como;
            this.VericarSeVisivelContratanteContratada();

            this.CarregarClienteFornecedor();
            if (ddlComo.SelectedValue == "Contratante")
            {
                ddlClienteFornecedor.SelectedValue = diverso.Fornecedor.Id.ToString();
            }
            else if (ddlComo.SelectedValue == "Contratada")
            {
                ddlClienteFornecedor.SelectedValue = diverso.Cliente.Id.ToString();
            }

            tbxCodigo.Text = diverso.Numero;
            tbxDataAbertura.Text = diverso.DataAbertura.EmptyToMinValue();
            tbxObjeto.Text = diverso.Objeto;
            tbxValorContrato.Text = diverso.Valor.ToString();

            ddlMoeda.SelectedValue = "0";
            if (diverso.Moeda != null)
                ddlMoeda.SelectedValue = diverso.Moeda.Id.ToString();

            ddlFormaRecebimento.SelectedValue = "0";
            if (diverso.FormaRecebimento != null)
                ddlFormaRecebimento.SelectedValue = diverso.FormaRecebimento.Id.ToString();

            ddlCentroCusto.SelectedValue = diverso.CentroCusto.Id.ToString();
            ddlSetor.SelectedValue = diverso.Setor.Id.ToString();
            ddlIndiceReajuste.SelectedValue = diverso.IndiceFinanceiro.Id.ToString();

            if (diverso.StatusContratoDiverso.GetType() == typeof(StatusEditaveisContrato))
            {
                ddlStatus.SelectedValue = "edit_" + diverso.StatusContratoDiverso.Id.ToString();
            }
            else
            {
                ddlStatus.SelectedValue = "n_" + diverso.StatusContratoDiverso.Id.ToString();
            }


            tbxVencimento.Text = diverso.GetUltimoVencimento.Data.ToString("dd/MM/yyyy");
            tbxVencimentoReajuste.Text = diverso.GetUltimoVencimentoReajustes.Data.ToString("dd/MM/yyyy");

            ibtnAddNotificacoes.Enabled = hfId.Value.ToInt32() > 0;
            ibtnAddNotificacoes.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";


            ibtnAddNotificacoesReajuste.Enabled = hfId.Value.ToInt32() > 0;
            ibtnAddNotificacoesReajuste.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";


            if (diverso.GetUltimoVencimento != null)
            {
                this.NotificacoesDoVencimentoDiversoContrato = this.GetIdDasNotificacoes(diverso.GetUltimoVencimento.Notificacoes);
            }
            if (diverso.GetUltimoVencimentoReajustes != null)
            {
                this.NotificacoesDoVencimentoDiversoReajusteContrato = this.GetIdDasNotificacoes(diverso.GetUltimoVencimentoReajustes.Notificacoes);
            }

            grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoContrato);
            grvNotificacoes.DataBind();

            grvNotificacoesReajuste.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoReajusteContrato);
            grvNotificacoesReajuste.DataBind();            
        }
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

    private void CarregarRenovacoes()
    {
        ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        IList<ContratoDiverso> contratos = new List<ContratoDiverso>();


        if (diverso.Renovacoes != null && diverso.Renovacoes.Count > 0)
        {
            contratos.Add(diverso);
            contratos.AddRange(diverso.Renovacoes);
        }
        else if (diverso.ContratoOriginal != null && diverso.ContratoOriginal.Renovacoes != null)
        {
            contratos.Add(diverso.ContratoOriginal);
            contratos.AddRange(diverso.ContratoOriginal.Renovacoes);
        }
        else
        {
            contratos.Add(diverso);
        }


        rptRenovacoes.DataSource = contratos;
        rptRenovacoes.DataBind();
    }

    private void CarregarNotificacoesDoVencimento()
    {
        if (hfTipo.Value == "Vencimento")
        {
            grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoContrato);
            grvNotificacoes.DataBind();
        }
        else if (hfTipo.Value == "Reajuste")
        {
            grvNotificacoesReajuste.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoReajusteContrato);
            grvNotificacoesReajuste.DataBind();
        }
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
        ddlEmpresa.Items.Add(new ListItem("-- Selecione --", "0"));

        this.VericarSeVisivelContratanteContratada();

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoEdicaoModuloContratos != null ? this.EmpresasPermissaoEdicaoModuloContratos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoEdicaoModuloContratos != null ? this.EmpresasPermissaoEdicaoModuloContratos : new List<Empresa>();
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
                ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + emp.GetNumeroCNPJeCPFComMascara, emp.Id.ToString()));
            }
        }
    }

    private void VericarSeVisivelContratanteContratada()
    {
        if (ddlGrupoEconomico.SelectedIndex <= 0 && ddlEmpresa.SelectedIndex <= 0)
        {
            lblClienteFornecedor.Text = "";
            lxbVisualizarEmpresa.Visible = false;
            imgComo.Visible = false;
            ddlComo.Visible = false;
            ddlClienteFornecedor.Visible = false;
        }
        else
        {
            if (ddlEmpresa.SelectedIndex > 0)
            {
                imgComo.Visible = true;
                ddlComo.Visible = true;
                ddlClienteFornecedor.Visible = true;
                lxbVisualizarEmpresa.Visible = true;

                if (ddlComo.SelectedValue == "Contratante")
                {
                    lblClienteFornecedor.Text = "Fornecedor(Contratada):";
                }
                else if (ddlComo.SelectedValue == "Contratada")
                {
                    lblClienteFornecedor.Text = "Cliente(Contratante):";
                }
                else
                {
                    ddlClienteFornecedor.Visible = false;
                    lxbVisualizarEmpresa.Visible = false;
                    lblClienteFornecedor.Text = "";
                }
            }
            else
            {
                ddlClienteFornecedor.Visible = false;
                lxbVisualizarEmpresa.Visible = false;
                imgComo.Visible = false;
                ddlComo.Visible = false;
                lblClienteFornecedor.Text = "";
            }

        }

    }

    private void CarregarStatusDiversos()
    {
        ddlStatus.Items.Clear();
        IList<StatusFixosContrato> statusfixos = StatusFixosContrato.ConsultarTodos();
        if (statusfixos != null && statusfixos.Count > 0)
        {
            foreach (StatusFixosContrato fixo in statusfixos)
            {
                ddlStatus.Items.Add(new ListItem(fixo.Nome, "n_" + fixo.Id.ToString()));
            }
        }

        IList<StatusEditaveisContrato> statusEditaveis = StatusEditaveisContrato.ConsultarTodos();
        if (statusEditaveis != null && statusEditaveis.Count > 0)
        {
            foreach (StatusEditaveisContrato editavel in statusEditaveis)
            {
                ddlStatus.Items.Add(new ListItem(editavel.Nome, "edit_" + editavel.Id.ToString()));
            }
        }
        ddlStatus.Items.Insert(0, new ListItem("-- Selecione --", "n_0"));
    }

    private void CarregarCentroDeCusto()
    {
        ddlCentroCusto.DataValueField = "Id";
        ddlCentroCusto.DataTextField = "Nome";
        ddlCentroCusto.DataSource = CentroCusto.ConsultarTodos();
        ddlCentroCusto.DataBind();
        ddlCentroCusto.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarIndices()
    {
        ddlIndiceReajuste.DataValueField = "Id";
        ddlIndiceReajuste.DataTextField = "Nome";
        ddlIndiceReajuste.DataSource = IndiceFinanceiro.ConsultarTodos();
        ddlIndiceReajuste.DataBind();
        ddlIndiceReajuste.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarSetor()
    {
        ddlSetor.DataValueField = "Id";
        ddlSetor.DataTextField = "Nome";

        IList<Setor> setores;

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
            setores = this.SetoresPermissaoEdicaoModuloContratos != null ? this.SetoresPermissaoEdicaoModuloContratos : new List<Setor>();
        else
            setores = Setor.ConsultarTodos();

        ddlSetor.DataSource = setores != null ? setores : new List<Setor>();
        ddlSetor.DataBind();
        ddlSetor.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void SalvarCentroDeCusto()
    {
        CentroCusto centro = new CentroCusto();
        centro = CentroCusto.ConsultarPorId(ddlCentroCusto.SelectedValue.ToInt32());
        if (centro == null)
            centro = new CentroCusto();

        centro.Nome = tbxCentroCusto.Text;
        centro = centro.Salvar();
        ddlCentroCusto.Items.Add(new ListItem(centro.Nome, centro.Id.ToString()));
        msg.CriarMensagem("Centro de Custo salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void Salvar()
    {
        if (ddlGrupoEconomico.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione primeiro um grupo econômico.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlEmpresa.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione primeiro uma empresa.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlComo.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione a forma como empresa esta no Contrato.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (!tbxCodigo.Text.Trim().IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Insira um número para o Contrato.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlClienteFornecedor.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione primeiro um " + lblClienteFornecedor.Text.Replace(":", "") + ".", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlStatus.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione o status do vencimento para poder prosseguir.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (!tbxDataAbertura.Text.IsDate())
        {
            msg.CriarMensagem("Data de Abertura inválida.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlCentroCusto.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione um Centro de Custo para poder prosseguir.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlSetor.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione um Setor para poder prosseguir.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlIndiceReajuste.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione um Índice de Reajuste para poder prosseguir.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (!tbxVencimento.Text.IsDate())
        {
            msg.CriarMensagem("Informe a data do vencimento para poder prosseguir.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (!tbxVencimentoReajuste.Text.IsDate())
        {
            msg.CriarMensagem("Informe a data de Reajuste para poder prosseguir.", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (!tbxValorContrato.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Informe um valor do Contrato válido.", "Atenção", MsgIcons.Alerta);
            return;
        }

        ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso == null)
            diverso = new ContratoDiverso();
        if (diverso.ContratoOriginal == null)
            diverso.ContratoOriginal = ContratoDiverso.ConsultarPorId(hfRenovacao.Value.ToInt32());

        diverso.Empresa = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());
        diverso.Como = ddlComo.SelectedValue;

        if (ddlComo.SelectedValue == "Contratante")
        {
            diverso.Fornecedor = Fornecedor.ConsultarPorId(ddlClienteFornecedor.SelectedValue.ToInt32());
        }
        else if (ddlComo.SelectedValue == "Contratada")
        {
            diverso.Cliente = Cliente.ConsultarPorId(ddlClienteFornecedor.SelectedValue.ToInt32());
        }

        this.VerificarAlteracaoDeStatusDoVencimento();

        diverso.StatusContratoDiverso = StatusContratoDiverso.ConsultarPorId(ddlStatus.SelectedValue.Split('_')[1].ToInt32());
        diverso.Numero = tbxCodigo.Text;
        diverso.DataAbertura = tbxDataAbertura.Text.ToDateTime();
        diverso.Objeto = tbxObjeto.Text;
        diverso.Valor = tbxValorContrato.Text;

        diverso.Moeda = Moeda.ConsultarPorId(ddlMoeda.SelectedValue.ToInt32());
        diverso.CentroCusto = CentroCusto.ConsultarPorId(ddlCentroCusto.SelectedValue.ToInt32());
        diverso.Setor = Setor.ConsultarPorId(ddlSetor.SelectedValue.ToInt32());
        diverso.IndiceFinanceiro = IndiceFinanceiro.ConsultarPorId(ddlIndiceReajuste.SelectedValue.ToInt32());
        diverso.FormaRecebimento = FormaRecebimento.ConsultarPorId(ddlFormaRecebimento.SelectedValue.ToInt32());


        VencimentoContratoDiverso vencimento = diverso.GetUltimoVencimento;
        if (vencimento == null)
        {
            vencimento = new VencimentoContratoDiverso();
            diverso.VencimentosContratosDiversos = new List<VencimentoContratoDiverso>();
            diverso.VencimentosContratosDiversos.Add(vencimento);
        }

        if (vencimento.ProrrogacoesPrazo == null || (vencimento.ProrrogacoesPrazo != null && vencimento.ProrrogacoesPrazo.Count == 0))
            vencimento.Data = tbxVencimento.Text.ToSqlDateTime();

        vencimento.Notificacoes = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoContrato);
        vencimento = (VencimentoContratoDiverso)vencimento.Salvar();
        diverso.VencimentosContratosDiversos[diverso.VencimentosContratosDiversos.Count - 1] = vencimento;

        VencimentoContratoDiverso reajuste = diverso.GetUltimoVencimentoReajustes;
        if (reajuste == null)
        {
            reajuste = new VencimentoContratoDiverso();
            diverso.Reajustes = new List<VencimentoContratoDiverso>();
            diverso.Reajustes.Add(reajuste);
        }
        if (reajuste.ProrrogacoesPrazo == null || (reajuste.ProrrogacoesPrazo != null && reajuste.ProrrogacoesPrazo.Count == 0))
            reajuste.Data = tbxVencimentoReajuste.Text.ToSqlDateTime();

        reajuste.Notificacoes = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoReajusteContrato);
        reajuste = (VencimentoContratoDiverso)reajuste.Salvar();
        diverso.Reajustes[diverso.Reajustes.Count - 1] = reajuste;


        diverso = diverso.Salvar();
        hfContrato.Value = hfId.Value = diverso.Id.ToString();

        ibtnAddNotificacoes.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAddNotificacoes.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";

        ibtnAddNotificacoesReajuste.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAddNotificacoesReajuste.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";

        msg.CriarMensagem("Contrato salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
        lblAcao.Text = "Edição do Contrato [" + diverso.Numero + "] - " + diverso.DataAbertura.ToShortDateString();

        if (hfId.Value.ToInt32() > 0)
        {
            btnUploadDiverso.Visible = true;
            pnlBotoesProcessos.Visible = true;            
        }
        else
        {
            btnUploadDiverso.Visible = false;
            pnlBotoesProcessos.Visible = false;
        }
        btnRenovar.Visible = true;
        rptRenovacoes.Visible = true;
        transacao.Recarregar(ref msg);
        this.CarregarRenovacoes();

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

    private void CarregarPopUpNotificacao(bool marcar, string tipo, params int[] dias)
    {
        hfTipo.Value = tipo;
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
        if (this.NotificacoesDoVencimentoDiversoContrato == null)
            this.NotificacoesDoVencimentoDiversoContrato = new List<int>();

        if (this.NotificacoesDoVencimentoDiversoReajusteContrato == null)
            this.NotificacoesDoVencimentoDiversoReajusteContrato = new List<int>();

        string emails = this.GetEmailsSelecionados();
        if (emails.IsNotNullOrEmpty())
        {
            foreach (ListItem l in cblDias.Items)
            {
                if (l.Selected)
                {
                    Notificacao n = new Notificacao(ModuloPermissao.ModuloContratos);
                    n.Emails = emails;
                    n.DiasAviso = l.Value.ToInt32();
                    ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
                    VencimentoContratoDiverso v = new VencimentoContratoDiverso();
                    if (hfTipo.Value == "Vencimento")
                    {
                        v = diverso.GetUltimoVencimento;
                        v.ContratoDiverso = diverso;
                        n.Template = TemplateNotificacao.TemplateVencimentoContratoDiverso;
                        n.Vencimento = v;
                        n = n.Salvar();
                        this.NotificacoesDoVencimentoDiversoContrato.Add(n.Id);
                    }
                    else if (hfTipo.Value == "Reajuste")
                    {
                        v = diverso.GetUltimoVencimentoReajustes;
                        v.Reajuste = diverso;
                        n.Template = TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso;
                        n.Vencimento = v;
                        n = n.Salvar();
                        this.NotificacoesDoVencimentoDiversoReajusteContrato.Add(n.Id);
                    }

                }
            }
        }
    }

    private void SalvarIndiceFinanceiro()
    {
        IndiceFinanceiro indice = new IndiceFinanceiro();
        indice = IndiceFinanceiro.ConsultarPorId(ddlIndiceReajuste.SelectedValue.ToInt32());
        if (indice == null)
            indice = new IndiceFinanceiro();
        indice.Nome = tbxIndice.Text;
        indice = indice.Salvar();
        ddlCentroCusto.Items.Add(new ListItem(indice.Nome, indice.Id.ToString()));
        msg.CriarMensagem("Índice salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void CarregarClienteFornecedor()
    {
        ddlClienteFornecedor.DataTextField = "Nome";
        ddlClienteFornecedor.DataValueField = "Id";

        this.VericarSeVisivelContratanteContratada();
        if (ddlComo.SelectedValue == "Contratante")
        {
            lblFormaPagamentoRecebimento.Text = "Forma de Pagamento";
            ddlClienteFornecedor.DataSource = Fornecedor.ConsultarTodosAtivos();
            ddlClienteFornecedor.DataBind();
        }
        else if (ddlComo.SelectedValue == "Contratada")
        {
            lblFormaPagamentoRecebimento.Text = "Forma de Recebimento";
            ddlClienteFornecedor.DataSource = Cliente.ConsultarTodosAtivos();
            ddlClienteFornecedor.DataBind();
        }

        ddlClienteFornecedor.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    public string BindRenovacao(object o)
    {
        ContratoDiverso c = (ContratoDiverso)o;
        if (c.ContratoOriginal == null)
            return "Contrato <b>ORIGINAL</b> com data de abertura em (" + c.DataAbertura.ToShortDateString() + ")";
        else
            return "<b>RENOVAÇAO</b> do Contrato com data de abertura em (" + c.DataAbertura.ToShortDateString() + ")";
    }

    public string BindCss(object o)
    {
        ContratoDiverso c = (ContratoDiverso)o;
        if (c.Id == hfId.Value.ToInt32())
            return "box_renovacoes_selecionado";
        else
            return "box_renovacoes";
    }

    public string BindLink(object o)
    {
        ContratoDiverso c = (ContratoDiverso)o;
        return "CadastroVencimentosContrato.aspx" + Seguranca.MontarParametros("id=" + c.Id);
    }

    public bool BindBold(object o)
    {
        ContratoDiverso c = (ContratoDiverso)o;
        int id = Seguranca.RecuperarParametro("Id", this.Request).ToInt32();
        if (id == c.Id)
            return true;
        else
            return false;
    }

    private void HabilitarDesabilitarRenovacao(bool p)
    {
        ddlGrupoEconomico.Enabled = p;
        ddlEmpresa.Enabled = p;
        ddlComo.Enabled = p;
        ddlClienteFornecedor.Enabled = p;
        ddlClienteFornecedor.Enabled = p;
    }

    private void SalvarHistoricoDeInsercaoDeAditivo(AditivoContrato ad, Object objetoManipulado)
    {
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        tbxHistoricoAlteracao.Text = "";
        lblAlteracao.Text = "Um novo Aditivo Número(" + ad.Numero + ") foi criado para o Contrato Número(" + ad.ContratoDiverso.Numero + ")";
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

        ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        h.ContratoDiverso = diverso;

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

            email.Assunto = "Alteração Referente ao Contrato (" + diverso.Numero + ") - Sistema Sustentar";
            email.Mensagem = email.CriarTemplateParaMudancaDeContrato(h);
            email.EnviarAutenticado(25, false);
        }

        lblAlteracaoStatus_ModalPopupExtender.Hide();
        msg.CriarMensagem("Histórico registrado com Sucesso", "Sucesso");
    }

    private void VerificarAlteracaoDeStatusDoVencimento()
    {
        string alteracao = "";

        ContratoDiverso diverso = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        if (diverso != null && diverso.StatusContratoDiverso != null && diverso.StatusContratoDiverso.Id.ToString() != ddlStatus.SelectedValue.Split('_')[1])
        {
            alteracao += "(Alteração no Status do Contrato: " + diverso.Numero + ", da Empresa " + diverso.Empresa.Nome + ". De: " + diverso.StatusContratoDiverso.Nome + " - Para: " + ddlStatus.SelectedItem.Text + ")";
        }
        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        chkEnviarEmail.Checked = true;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresasAlteracao, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
    }

    private void SalvarStatus()
    {
        StatusEditaveisContrato status = new StatusEditaveisContrato();
        status = StatusEditaveisContrato.ConsultarPeloNome(ddlStatus.SelectedItem.Text);
        if (status == null)
            status = new StatusEditaveisContrato();
        status.Nome = tbxStatus.Text;
        status = status.Salvar();
        msg.CriarMensagem("Status salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void SalvarAditivos()
    {
        if (tbxNumeroAditivo.Text.ToInt32() <= 0)
        {
            msg.CriarMensagem("É necessário informar um Número para o Aditivo.", "Alerta", MsgIcons.Alerta);
            return;
        }
        if (tbxDataAssinaturaAditivo.Text.ToSqlDateTime() <= SqlDate.MinValue)
        {
            msg.CriarMensagem("É necessário informar uma Data de Assinatura.", "Alerta", MsgIcons.Alerta);
            return;
        }
        if (ckbAlteracaoVencimento.Checked && !tbxDataProrrogacaoVencimento.Text.Trim().IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessário informar uma nova Data de Vencimento.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ckbAlteracaoReajuste.Checked && !tbxDataProrrogacaoReajuste.Text.Trim().IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessário informar uma nova Data de Reajuste.", "Alerta", MsgIcons.Alerta);
            return;
        }

        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());


        AditivoContrato aditivo = new AditivoContrato();
        aditivo.DataAssinatura = tbxDataAssinaturaAditivo.Text.ToDateTime();
        aditivo.Numero = tbxNumeroAditivo.Text;
        aditivo.Motivo = tbxMotivoAditivo.Text;
        aditivo.ContratoDiverso = contrato;
        aditivo.ProrrogouReajuste = ckbAlteracaoReajuste.Checked;
        aditivo.ProrrogouVencimento = ckbAlteracaoVencimento.Checked;
        aditivo.DataVencimento = tbxDataProrrogacaoVencimento.Text.ToDateTime();
        aditivo.DataReajuste = tbxDataProrrogacaoReajuste.Text.ToDateTime();


        if (ckbAlteracaoVencimento.Checked)
        {
            Vencimento vencimento = contrato.GetUltimoVencimento;
            vencimento.Data = aditivo.DataVencimento;
            vencimento.Salvar();
            tbxVencimento.Text = vencimento.Data.ToShortDateString();
        }

        if (ckbAlteracaoReajuste.Checked)
        {
            Vencimento vencimento = contrato.GetUltimoVencimentoReajustes;
            vencimento.Data = aditivo.DataReajuste;
            vencimento.Salvar();
            tbxVencimentoReajuste.Text = vencimento.Data.ToShortDateString();
        }

        aditivo = aditivo.Salvar();

        this.SalvarHistoricoDeInsercaoDeAditivo(aditivo, contrato);

        transacao.Recarregar(ref msg);
        this.CarregarAditivos(true, true);
    }

    private void CarregarAditivos(bool comProrrogacaoVencimento, bool comProrrogacaoReajuste)
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("Salve Primeiro o Contrato.", "Alerta", MsgIcons.Alerta);
            return;
        }

        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());

        if (comProrrogacaoVencimento && comProrrogacaoReajuste)
            gdvAditivos.DataSource = contrato.AditivosContratos;
        else if (comProrrogacaoReajuste)
            gdvAditivos.DataSource = contrato.GetAditivosComProrrogacaoReajuste;
        else if (comProrrogacaoVencimento)
            gdvAditivos.DataSource = contrato.GetAditivosComProrrogacaoVencimento;


        gdvAditivos.DataBind();

        tbxNumeroAditivo.Text = "";
        tbxDataAssinaturaAditivo.Text = "";
        tbxMotivoAditivo.Text = "";
        tbxDataProrrogacaoVencimento.Text = "";
        tbxDataProrrogacaoReajuste.Text = "";
        ckbAlteracaoVencimento.Checked = false;
        ckbAlteracaoReajuste.Checked = false;
        pnlProrrogacaoReajuste.Visible = false;
        pnlProrrogacaoVencimento.Visible = false;

        ModalPopupAditivos.Show();

    }

    private void CriarRenovacao()
    {
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
        if (contrato == null)
            hfRenovacao.Value = "0";
        else if (contrato.ContratoOriginal == null)
            hfRenovacao.Value = contrato.Id.ToString();
        else
            hfRenovacao.Value = contrato.ContratoOriginal.Id.ToString();

        if (contrato.ContratoOriginal != null)
        {
            foreach (ContratoDiverso contra in contrato.ContratoOriginal.Renovacoes)
            {
                if (contra.StatusContratoDiverso.Nome.ToUpper() != "RENOVADO")
                {
                    contra.StatusContratoDiverso = StatusFixosContrato.ConsultarPeloNome("Renovado");
                    contra.Salvar();
                }
            }
            if (contrato.ContratoOriginal.StatusContratoDiverso.Nome.ToUpper() != "RENOVADO")
            {
                contrato.ContratoOriginal.StatusContratoDiverso = StatusFixosContrato.ConsultarPeloNome("Renovado");
                contrato.Salvar();
            }
        }
        else
        {
            if (contrato.StatusContratoDiverso.Nome.ToUpper() != "RENOVADO")
            {
                contrato.StatusContratoDiverso = StatusFixosContrato.ConsultarPeloNome("Renovado");
                contrato.Salvar();
            }
        }

        ddlStatus.SelectedValue = "n_" + StatusFixosContrato.ConsultarPeloNome("Vigente").Id.ToString();

        hfId.Value = "0";

        lblAcao.Text = "Nova renovação de Contrato";

        this.HabilitarDesabilitarRenovacao(false);

        this.NotificacoesDoVencimentoDiversoContrato = new List<int>();
        this.NotificacoesDoVencimentoDiversoReajusteContrato = new List<int>();

        tbxVencimento.Text = "";
        tbxVencimentoReajuste.Text = "";

        btnUploadDiverso.Visible = false;

        ibtnAddNotificacoes.Enabled = false;
        ibtnAddNotificacoes.ImageUrl = "~/imagens/icone_adicionar_semcor.png";


        ibtnAddNotificacoesReajuste.Enabled = false;
        ibtnAddNotificacoesReajuste.ImageUrl = "~/imagens/icone_adicionar_semcor.png";

        grvNotificacoes.DataSource = null;
        grvNotificacoes.DataBind();
        grvNotificacoesReajuste.DataSource = null;
        grvNotificacoesReajuste.DataBind();

        msg.CriarMensagem("Uma nova renovação foi aberta. Para efetivar esta renovação, insira as novas Datas de Vencimento clique em Salvar.", "Atenção", MsgIcons.Informacao);
    }

    public string BindProrrogouReajuste(object o)
    {
        AditivoContrato ad = (AditivoContrato)o;
        if (ad.ProrrogouReajuste)
            return "Para: " + ad.DataReajuste.ToShortDateString();
        else
            return "Não prorrogou";
    }

    public string BindProrrogouVencimento(object o)
    {
        AditivoContrato ad = (AditivoContrato)o;
        if (ad.ProrrogouVencimento)
            return "Para: " + ad.DataVencimento.ToShortDateString();
        else
            return "Não prorrogou";
    }

    /////// Processo DNPM

    public String bindingObjeto(Object o)
    {
        ProcessoDNPM cont = (ProcessoDNPM)o;
        return cont.Substancia;
    }

    public String bindingStatusContrato(Object o)
    {
        ProcessoDNPM cont = (ProcessoDNPM)o;
        return cont.Identificacao;
    }

    private void CarregarPesquisaContratosDNPM()
    {
        IList<ProcessoDNPM> processos = ProcessoDNPM.ConsultarPorSubstanciaNumero(tbxSubstancia.Text, tbxNumeroDNPM.Text.ToInt32());
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
        if (contrato != null)
            processos = processos.Where(pr => !contrato.ProcessosDNPM.Contains(pr)).ToList();
        gdvContratosSelecao.DataSource = processos;
        gdvContratosSelecao.DataBind();
    }

    private void CarregarContratosDNPM()
    {
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
        if (contrato != null)
        {
            IList<ProcessoDNPM> processos = contrato.ProcessosDNPM;
            lblQuantidadeProcessosDNPM.Text = processos.Count + " Processo(s) encontrado(s).";
            gdvContratos.DataSource = processos;
            gdvContratos.DataBind();
        }

    }

    private void SalvarContratosDNPM()
    {
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
        if (contrato != null)
        {
            foreach (GridViewRow item in gdvContratosSelecao.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(gdvContratosSelecao.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    contrato.ProcessosDNPM.Add(processo);
                }
            }
            contrato = contrato.Salvar();
            this.CarregarContratosDNPM();
            msg.CriarMensagem("Processo associado com sucesso", "Sucesso", MsgIcons.Sucesso);
        }
    }

    //////////// Processo Ambiental

    public String bindingEmpresa(Object o)
    {
        Processo cont = (Processo)o;
        return cont.Empresa.Nome;
    }

    public String bindingConsultora(Object o)
    {
        Processo cont = (Processo)o;
        return cont.Consultora != null ? cont.Consultora.Nome : "";
    }

    private void CarregarPesquisaContratosAmbiental()
    {
        IList<Processo> processos = Processo.ConsultarPorEmpresaENumero(tbxNumeroProcessoAmbiental.Text, Empresa.ConsultarPorId(ddlEmpresasConsulta.SelectedValue.ToInt32()));
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
        if (contrato != null)
        {
            processos = processos.Where(pr => !contrato.Processos.Contains(pr)).ToList();
            switch (rblTipoProcesso.SelectedValue.ToInt32())
            {
                case 1:
                    processos = processos.Where(pr => pr.OrgaoAmbiental is OrgaoMunicipal).ToList();
                    break;
                case 2:
                    processos = processos.Where(pr => pr.OrgaoAmbiental is OrgaoEstadual).ToList();
                    break;
                case 3:
                    processos = processos.Where(pr => pr.OrgaoAmbiental is OrgaoFederal).ToList();
                    break;
                default:
                    break;
            }
        }
        gdvProcessosAmbientaisSelecao.DataSource = processos;
        gdvProcessosAmbientaisSelecao.DataBind();
    }

    private void CarregarContratosAmbiental()
    {
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
        if (contrato != null)
        {
            IList<Processo> processos = contrato.Processos;
            lblQuantidadeProcessosAmbiental.Text = processos.Count + " Processo(s) encontrado(s).";
            gdvProcessosAmbientais.DataSource = processos;
            gdvProcessosAmbientais.DataBind();
        }

    }

    private void SalvarContratosAmbiental()
    {
        ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
        if (contrato != null)
        {
            foreach (GridViewRow item in gdvProcessosAmbientaisSelecao.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Processo processo = Processo.ConsultarPorId(gdvProcessosAmbientaisSelecao.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    contrato.Processos.Add(processo);
                }
            }
            contrato = contrato.Salvar();
            this.CarregarContratosAmbiental();
            msg.CriarMensagem("Processo associado com sucesso", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void CarregarEmpresasPesquisaProcesso()
    {
        IList<Empresa> empresas = Empresa.ConsultarTodos();
        if (empresas != null && empresas.Count > 0)
        {
            ddlEmpresasConsulta.DataTextField = "Nome";
            ddlEmpresasConsulta.DataValueField = "Id";
            ddlEmpresasConsulta.DataSource = empresas;
            ddlEmpresasConsulta.DataBind();
            ddlEmpresasConsulta.Items.Insert(0, new ListItem("-- Todos --", "0"));
        }
    }

    public bool BindingVisivel(object contratoDiverso)
    {
        return btnSalvar.Visible;        
    }

    #endregion

    #region __________Eventos____________    

    protected void ddlMoeda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMoeda.SelectedIndex > 0)
            lblSiglaMoeda.Text = "(" + Moeda.ConsultarPorId(ddlMoeda.SelectedValue.ToInt32()).Sigla + ")";
        else
            lblSiglaMoeda.Text = "";
    }

    protected void btnSalvarRecebimento_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarFormaRecebimento();
            ModalPopFormarecebimento.Hide();
            transacao.Recarregar(ref msg);
            this.CarregarFormasRecebimento();
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

    protected void btnAdicionarFormaRecebimento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlFormaRecebimento.SelectedValue = "0";
            tbxForma.Text = "";
            ModalPopFormarecebimento.Show();
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

    protected void btnEditarFormaRecebimento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FormaRecebimento forma = FormaRecebimento.ConsultarPorId(ddlFormaRecebimento.SelectedValue.ToInt32());
            if (forma != null)
            {
                tbxForma.Text = forma.Nome;
                ModalPopFormarecebimento.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione uma Forma de Recebimento na lista ao lado.", "Alerta", MsgIcons.Alerta);
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

    protected void ibtnExcluirFormaRecebimento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlFormaRecebimento.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro a Forma.", "Atenção", MsgIcons.Alerta);
                return;
            }
            FormaRecebimento forma = FormaRecebimento.ConsultarPorId(ddlFormaRecebimento.SelectedValue.ToInt32());
            if (forma != null && forma.ContratosDiversos != null && forma.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir esta Forma, pois existem Contratos associados a ela.", "Atenção", MsgIcons.Alerta);
                return;
            }

            forma.Excluir();
            transacao.Recarregar(ref msg);
            msg.CriarMensagem("Forma excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.CarregarFormasRecebimento();
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

    protected void ibtnExcluirFormaRecebimento_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír esta Forma?");
    }

    protected void ibtnExcluirMoeda_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlMoeda.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro a Moeda.", "Atenção", MsgIcons.Alerta);
                return;
            }
            Moeda moeda = Moeda.ConsultarPorId(ddlMoeda.SelectedValue.ToInt32());
            if (moeda != null && moeda.ContratosDiversos != null && moeda.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir esta Moeda, pois existem Contratos associados a ela.", "Atenção", MsgIcons.Alerta);
                return;
            }

            moeda.Excluir();
            transacao.Recarregar(ref msg);
            msg.CriarMensagem("Moeda excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.CarregarMoedas();
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
            this.CarregarNotificacoesDoVencimento();
            ModalExtenderNotificoes.Hide();
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
            this.CarregarPopUpNotificacao(true, "Vencimento", 5, 10, 15, 30, 60, 90, 120, 160, 180, 220, 360);
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

    protected void btnSavarCentroCusto_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarCentroDeCusto();
            ModalPopCentroCusto.Hide();
            transacao.Recarregar(ref msg);
            this.CarregarCentroDeCusto();
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

    protected void btnAdicionarCentroCusto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlCentroCusto.SelectedValue = "0";
            tbxCentroCusto.Text = "";
            ModalPopCentroCusto.Show();
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

    protected void btnSalvarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarSetor();
            lblPopUpSetorExtender.Hide();
            transacao.Recarregar(ref msg);
            this.CarregarSetor();
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

    protected void btnAdicionarReajuste_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlIndiceReajuste.SelectedValue = "0";
            tbxIndice.Text = "";
            lblPopUpIndiceExtender.Show();
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

    protected void btnAdicionarSetor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlSetor.SelectedValue = "0";
            tbxSetor.Text = "";
            lblPopUpSetorExtender.Show();
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

    protected void btnSalvarIndice_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarIndiceFinanceiro();
            lblPopUpIndiceExtender.Hide();
            transacao.Recarregar(ref msg);
            this.CarregarIndices();
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

    protected void btnEditarCentroCusto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CentroCusto centro = CentroCusto.ConsultarPorId(ddlCentroCusto.SelectedValue.ToInt32());
            if (centro != null)
            {
                tbxCentroCusto.Text = centro.Nome;
                ModalPopCentroCusto.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione um Centro de Custo na lista ao lado.", "Alerta", MsgIcons.Alerta);
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

    protected void btnEditarSetor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Setor setor = Setor.ConsultarPorId(ddlSetor.SelectedValue.ToInt32());
            if (setor != null)
            {
                tbxSetor.Text = setor.Nome;
                lblPopUpSetorExtender.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione um Setor na lista ao lado.", "Alerta", MsgIcons.Alerta);
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

    protected void btnEditarIndice_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            IndiceFinanceiro indice = IndiceFinanceiro.ConsultarPorId(ddlIndiceReajuste.SelectedValue.ToInt32());
            if (indice != null)
            {
                tbxIndice.Text = indice.Nome;
                lblPopUpIndiceExtender.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione um Índice na lista ao lado.", "Alerta", MsgIcons.Alerta);
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

    protected void ibtnExcluirCentroCusto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCentroCusto.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro o Centro de Custo.", "Atenção", MsgIcons.Alerta);
                return;
            }
            CentroCusto centro = CentroCusto.ConsultarPorId(ddlCentroCusto.SelectedValue.ToInt32());
            if (centro != null && centro.ContratosDiversos != null && centro.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir este Centro de Custo, pois existem Contratos associados a ele.", "Atenção", MsgIcons.Alerta);
                return;
            }

            centro.Excluir();
            transacao.Recarregar(ref msg);
            msg.CriarMensagem("Centro de Custo excluído com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.CarregarCentroDeCusto();
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

    protected void ibtnExcluirSetor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlSetor.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro o Setor.", "Atenção", MsgIcons.Alerta);
                return;
            }
            Setor setor = Setor.ConsultarPorId(ddlSetor.SelectedValue.ToInt32());
            if (setor != null && setor.ContratosDiversos != null && setor.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir este Setor, pois existem Contratos associados a ele.", "Atenção", MsgIcons.Alerta);
                return;
            }

            setor.Excluir();
            transacao.Recarregar(ref msg);
            msg.CriarMensagem("Setor excluído com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.CarregarSetor();
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

    protected void ibtnExcluirReajuste_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlIndiceReajuste.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro o Índice.", "Atenção", MsgIcons.Alerta);
                return;
            }
            IndiceFinanceiro indice = IndiceFinanceiro.ConsultarPorId(ddlIndiceReajuste.SelectedValue.ToInt32());
            if (indice != null && indice.ContratosDiversos != null && indice.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir este Índice, pois existem Contratos associados a ele.", "Atenção", MsgIcons.Alerta);
                return;
            }

            indice.Excluir();
            transacao.Recarregar(ref msg);
            msg.CriarMensagem("Índice excluído com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.CarregarIndices();
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

    protected void ibtnExcluirCentroCusto_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír este Centro de Custo?");
    }

    protected void ibtnExcluirSetor_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír este Setor?");
    }

    protected void ibtnExcluirReajuste_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír este Índice?");
    }

    protected void ddlComo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarClienteFornecedor();
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

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.VericarSeVisivelContratanteContratada();
        ddlComo.SelectedIndex = 0;
    }

    protected void btnRenovar_Click(object sender, EventArgs e)
    {
        try
        {
            this.CriarRenovacao();
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

    protected void ibtnExcluir6_PreRender1(object sender, EventArgs e)
    {
        //Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void lkbProrrogacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarAditivos(true, false);
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

    protected void lkbProrrogacaoReajuste_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarAditivos(false, true);
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

    protected void grvNotificacoesReajuste_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacoesReajuste.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacoesReajuste.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao notificacao = new Notificacao(grvNotificacoesReajuste.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.NotificacoesDoVencimentoDiversoReajusteContrato.Remove(notificacao.Id);
                    notificacao.Excluir();
                }
            }

            grvNotificacoesReajuste.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoReajusteContrato);
            grvNotificacoesReajuste.DataBind();
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
                    this.NotificacoesDoVencimentoDiversoContrato.Remove(notificacao.Id);
                    notificacao.Excluir();
                }
            }

            grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoContrato);
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            this.Salvar();
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
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);

        ibtnAddNotificacoes.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAddNotificacoes.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/icone_adicionar_semcor.png";

        ibtnAddNotificacoesReajuste.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAddNotificacoesReajuste.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/icone_adicionar_semcor.png";


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
            this.NotificacoesDoVencimentoDiversoContrato = null;
            this.NotificacoesDoVencimentoDiversoReajusteContrato = null;
            Response.Redirect("CadastroVencimentosContrato.aspx", false);
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
        grvNotificacoes.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoContrato);
        grvNotificacoes.DataBind();
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
            ContratoDiverso d = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
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
        //  Permissoes.ValidarControle((Button)sender, this.UsuarioLogado);
        Button btn = (Button)sender;

        if (btn.Enabled == true)
            WebUtil.AdicionarConfirmacao(btn, "Todos os dados referentes a este Contrato serão perdido(s). Deseja excluir mesmo assim?");
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

    protected void ibtnAddNotificacoesReajuste_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CarregarPopUpNotificacao(true, "Reajuste", 5, 10, 15, 30, 60, 90, 120, 160, 180, 220, 360);
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

    protected void grvNotificacoesReajuste_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacoesReajuste.PageIndex = e.NewPageIndex;
        grvNotificacoesReajuste.DataSource = this.RecarregarNotificacoes(this.NotificacoesDoVencimentoDiversoReajusteContrato);
        grvNotificacoesReajuste.DataBind();
    }

    protected void lxbVisualizarEmpresa_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ddlComo.SelectedIndex < 0)
            {
                msg.CriarMensagem("Selecione uma Empresa ao Lado", "Alerta", MsgIcons.Alerta);
            }
            else
            {
                if (ddlComo.SelectedValue == "Contratada")
                {
                    Response.Redirect("../Clientes/CadastroClientes.aspx" + Seguranca.MontarParametros("id=" + ddlClienteFornecedor.SelectedValue), false);
                }
                else if (ddlComo.SelectedValue == "Contratante")
                {
                    Response.Redirect("../Fornecedores/CadastrarFornecedores.aspx" + Seguranca.MontarParametros("id=" + ddlClienteFornecedor.SelectedValue), false);
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

    protected void btnAdicionarStatus_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlStatus.SelectedValue = "n_0";
            tbxStatus.Text = "";
            ModalPopupStatus.Show();
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

    protected void btnEditarStatus_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedIndex > 0)
            {

                if (ddlStatus.SelectedValue.Split('_')[0] == "edit")
                {
                    StatusEditaveisContrato st = StatusEditaveisContrato.ConsultarPorId(ddlStatus.SelectedValue.Split('_')[1].ToInt32());
                    if (st != null)
                    {
                        tbxStatus.Text = st.Nome;
                        ModalPopupStatus.Show();
                    }
                }
                else
                {
                    msg.CriarMensagem("Este status (" + ddlStatus.SelectedItem.Text + ") não pode ser alterado.", "Alerta", MsgIcons.Alerta);
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um Status", "Alerta", MsgIcons.Alerta);
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

    protected void btnSalvarStatus_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarStatus();
            ModalPopupStatus.Hide();
            transacao.Recarregar(ref msg);
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

    protected void lblSalvarContrato_PreRender(object sender, EventArgs e)
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            ((Label)sender).Text = "Salve primeiro o Contrato.";
        }
        else
        {
            ((Label)sender).Text = "";
        }
    }

    protected void btnSalvarAditivo_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarAditivos();
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

    protected void lkbAditivos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarAditivos(true, true);
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

    protected void ckbAlteracaoVencimento_CheckedChanged(object sender, EventArgs e)
    {
        pnlProrrogacaoVencimento.Visible = ckbAlteracaoVencimento.Checked;
        lblVencimentoAtual.Text = "Data de Vencimento atual: " + tbxVencimento.Text;
    }

    protected void ckbAlteracaoReajuste_CheckedChanged(object sender, EventArgs e)
    {
        pnlProrrogacaoReajuste.Visible = ckbAlteracaoReajuste.Checked;
        lblReajusteAtual.Text = "Data de Reajuste atual: " + tbxVencimentoReajuste.Text;
    }

    protected void lkbAditivos_PreRender(object sender, EventArgs e)
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            ((LinkButton)sender).Text = "Aditivos";
        }
        else
        {
            ContratoDiverso cd = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
            ((LinkButton)sender).Text = "Abrir todos Aditivos do Contrato [" + cd.AditivosContratos.Count + " aditivo(s)]";
        }
    }

    protected void lkbProrrogacao_PreRender(object sender, EventArgs e)
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            ((LinkButton)sender).Text = "";
        }
        else
        {
            ContratoDiverso cd = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
            int contAditivos = 0;
            if (cd.AditivosContratos != null)
            {
                foreach (AditivoContrato ad in cd.AditivosContratos)
                {
                    if (ad.ProrrogouVencimento)
                        contAditivos++;
                }
            }

            ((LinkButton)sender).Text = "Abrir aditivos do contrato com prorrogação do Vencimento(" + contAditivos + " prorrogação(s))";
        }
    }

    protected void lkbProrrogacaoReajuste_PreRender(object sender, EventArgs e)
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            ((LinkButton)sender).Text = "";
        }
        else
        {
            ContratoDiverso cd = ContratoDiverso.ConsultarPorId(hfId.Value.ToInt32());
            int contAditivos = 0;
            if (cd.AditivosContratos != null)
            {
                foreach (AditivoContrato ad in cd.AditivosContratos)
                {
                    if (ad.ProrrogouReajuste)
                        contAditivos++;
                }
            }

            ((LinkButton)sender).Text = "Abrir aditivos do contrato com prorrogação do Reajuste(" + contAditivos + " prorrogação(s))";
        }
    }

    protected void gdvAditivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            foreach (GridViewRow item in ((GridView)sender).Rows)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    AditivoContrato p = AditivoContrato.ConsultarPorId(((GridView)sender).DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (p != null)
                    {
                        p.Excluir();
                    }
                }

            transacao.Recarregar(ref msg);
            this.CarregarAditivos(true, true);
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

    protected void ibtnExcluirStatus_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír este Status?");
    }

    protected void ibtnExcluirStatus_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro o Status.", "Atenção", MsgIcons.Alerta);
                return;
            }


            if (ddlStatus.SelectedValue.Split('_')[0] == "edit")
            {
                StatusEditaveisContrato st = StatusEditaveisContrato.ConsultarPorId(ddlStatus.SelectedValue.Split('_')[1].ToInt32());

                if (st != null && st.ContratosDiversos != null && st.ContratosDiversos.Count > 0)
                {
                    msg.CriarMensagem("Não é possível excluir este Status, pois existem Contratos associados a ele.", "Atenção", MsgIcons.Alerta);
                    return;
                }

                st.Excluir();
                transacao.Recarregar(ref msg);
                msg.CriarMensagem("Status excluído com sucesso!", "Sucesso", MsgIcons.Sucesso);
                this.CarregarIndices();
            }
            else
            {
                msg.CriarMensagem("Este Status (" + ddlStatus.SelectedItem.Text + ") não pode ser excluído.", "Alerta", MsgIcons.Alerta);
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

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedItem.Text.ToUpper() == "RENOVADO" && hfId.Value.ToInt32() > 0)
            {
                this.CriarRenovacao();
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

    protected void btnRenovar_PreRender(object sender, EventArgs e)
    {
        btnRenovar.Visible = !(lblAcao.Text == "Nova renovação de Contrato");
    }

    ////////////////////////// Processo DNPM

    protected void btnPesquisarDNPM_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarPesquisaContratosDNPM();
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

    protected void btnSelecionarMaisContratos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarPesquisaContratosDNPM();
            lblSelecionarContrato_ModalPopupExtender.Show();
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

    protected void btnProcessoDNPM_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarContratosDNPM();
            lblAdicionarContrato_ModalPopupExtender.Show();
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

    protected void btnSalvarContratoDNPM_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarContratosDNPM();
            lblSelecionarContrato_ModalPopupExtender.Hide();
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

    protected void gdvContratosSelecao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvContratosSelecao.PageIndex = e.NewPageIndex;
            this.CarregarPesquisaContratosDNPM();
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

    protected void gdvContratos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
            if (contrato != null)
            {
                foreach (GridViewRow row in gdvContratos.Rows)
                {
                    if (((CheckBox)row.FindControl("ckbExcluir")).Checked)
                    {
                        ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(gdvContratos.DataKeys[row.RowIndex].Value.ToString().ToInt32());
                        contrato.ProcessosDNPM.Remove(processo);
                    }
                }
                contrato.Salvar();
                msg.CriarMensagem("Processo(s) excluido(s) com sucesso", "Sucesso", MsgIcons.Sucesso);
                this.CarregarContratosDNPM();
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

    protected void gdvContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvContratos.PageIndex = e.NewPageIndex;
            this.CarregarContratosDNPM();
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

    protected void ibtnExcluirProcesso_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir o(s) processo(s) associado(s)?");
    }

    ////////////////////////// Processo Ambiental

    protected void gdvProcessosAmbientais_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvProcessosAmbientais.PageIndex = e.NewPageIndex;
            this.CarregarContratosAmbiental();
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

    protected void gdvProcessosAmbientais_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(hfContrato.Value.ToInt32());
            if (contrato != null)
            {
                foreach (GridViewRow row in gdvProcessosAmbientais.Rows)
                {
                    if (((CheckBox)row.FindControl("ckbExcluir")).Checked)
                    {
                        Processo processo = Processo.ConsultarPorId(gdvProcessosAmbientais.DataKeys[row.RowIndex].Value.ToString().ToInt32());
                        contrato.Processos.Remove(processo);
                    }
                }
                contrato.Salvar();
                msg.CriarMensagem("Processo(s) excluido(s) com sucesso", "Sucesso", MsgIcons.Sucesso);
                this.CarregarContratosAmbiental();
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

    protected void btnSelecionarMaisProcessos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarPesquisaContratosAmbiental();
            lblSelecionarProcessoAmbiental_ModalPopupExtender.Show();
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

    protected void btnProcessoAmbiental_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarContratosAmbiental();
            lblAdicionarProcessoAmbiental_ModalPopupExtender.Show();
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

    protected void btnPesquisarProcessosAmbientais_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarPesquisaContratosAmbiental();
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

    protected void btnSalvarProcessosAmbientais_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarContratosAmbiental();
            lblSelecionarProcessoAmbiental_ModalPopupExtender.Hide();
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

    protected void gdvProcessosAmbientaisSelecao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvProcessosAmbientaisSelecao.PageIndex = e.NewPageIndex;
            this.CarregarPesquisaContratosAmbiental();
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

    protected void ibtnExcluirProcessoAmbiental_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir o(s) processo(s) associado(s)?");
    }

    #endregion

    #region ______TriggersDinamicas______

    protected void ckbAlteracaoVencimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "CheckedChanged", upAlteracaoVencimento);
    }

    protected void ckbAlteracaoReajuste_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "CheckedChanged", upAlteracaoVencimento);
    }

    protected void btnSalvarAditivo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void lblUpload2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAditivos);
    }

    protected void lkbAditivos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAditivos);
    }

    protected void btnSalvarStatus_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upStatus);
    }

    protected void btnEditarFormaRecebimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopRecebimento);
    }

    protected void btnAdicionarFormaRecebimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopRecebimento);
    }

    protected void btnAdicionarCentroCusto_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopCusto);
    }

    protected void btnEditarCentroCusto_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopCusto);
    }

    protected void btnEditarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopSetor);
    }

    protected void btnAdicionarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopSetor);
    }

    protected void btnAdicionarReajuste_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopIndice);
    }

    protected void btnEditarIndice_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopIndice);
    }

    protected void lkbProrrogacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAditivos);
    }

    protected void btnSalvarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPSetor);
    }

    protected void btnAddProrrogacaoVencimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void btnSalvarIndice_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPIndice);
    }

    protected void ibtnExcluir6_Init(object sender, EventArgs e)
    {

    }

    protected void btnAlterarStatus_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsGrupo();marcarEmailsEmpresa();</script>", false);
    }

    protected void btnSavarCentroCusto_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPCentroCusto);
    }

    protected void btnSalvarProrrogacaoReajuste_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void Button1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void lkbHistoricos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPHistoricos);
    }

    protected void btnEnviarHistorico_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEnvioHistorico);
    }

    protected void ibtnExcluir5_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void btnAdicionarStatus_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatusCriados);
    }

    protected void btnSalvarNotificacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPNotificacoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPNotificacoesReajuste);
    }

    protected void btnSalvarvencimentos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void ibtnExcluirMoeda_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír esta Moeda?");
    }

    protected void btnSalvarRecebimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPRecebimento);
    }

    protected void btnAdicionarStatus_Init2(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatusCriados);
    }

    protected void btnEditarStatus_Init1(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatusCriados);
    }

    protected void ddlComo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedIndexChanged", upFormaRecebimento);
    }

    protected void lkbProrrogacaoReajuste_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAditivos);
    }

    //// Processo DNPM

    protected void btnProcessoDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(((Button)sender), "Click", UPListagemProcessoDNPM);
    }

    protected void btnSalvarContratoDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(((Button)sender), "Click", UPListagemProcessoDNPM);
    }

    protected void btnSelecionarMaisContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(((Button)sender), "Click", UPSelecaoContratos);
    }

    protected void gdvContratosSelecao_Init(object sender, EventArgs e)
    {

    }

    //////// Processo Ambiental

    protected void btnSalvarProcessosAmbientais_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(((Button)sender), "Click", UPProcessosAmbientais);
    }

    protected void btnSelecionarMaisProcessos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(((Button)sender), "Click", UPProcessosSelecaoAmbiental);
    }

    protected void btnProcessoAmbiental_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(((Button)sender), "Click", UPProcessosAmbientais);
    }

    #endregion

    protected void btnUploadDiverso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadDiverso_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o Contrato para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfId.Value.ToInt32() + "&idEmpresa=" + ddlEmpresa.SelectedValue + "&idCliente=" + ddlGrupoEconomico.SelectedValue + "&tipo=ContratoDiverso"));
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
    protected void gdvAditivos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            AditivoContrato ad = AditivoContrato.ConsultarPorId(((GridView)sender).DataKeys[e.NewEditIndex].Value.ToString().ToInt32());

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + ad.ContratoDiverso.Id + "&idEmpresa=" + ddlEmpresa.SelectedValue + "&idCliente=" + ddlGrupoEconomico.SelectedValue + "&tipo=AditivoContrato&idAditivo=" + ad.Id));
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
    protected void gdvAditivos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }
}