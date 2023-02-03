using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;
using System.Configuration;
using System.IO;
using System.Threading;
using Persistencia.Modelo;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

public partial class Site_DNPM : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    public enum PopUpAbertoENUM { NENHUM, REQUERIMENTOPESQUISA, REQUERIMENTOLAVRA, GUIA, RAL, ALVARAPESQUISA, EXTRACAO, LICENCIAMENTO, CONCESSAOLAVRA };
    public enum PopUpNotificacaoAberta { NENHUM, RAL, LIMITERENUNCIA, GUIAUTILIZACAONOT, EXTRACAOVALIDADE, LICENCIAMENTOVALIDADE, LICENCIAMENTOENTREGALICENCAPROTOCOLO, DIPEM, CONCESSAODELAVRA, ALVARADEPESQUISAVALIDADE, ALVARADEPESQUISAINICIOPESQUISA, ALVARADEPESQUISATAXAANUALHECTARE, ALVARADEPESQUISAREQUERIMENTODELAVRA, ALVARADEPESQUISAREQUERIMENTOLPPOLIGONAL };

    public IList<int> SubstanciaSelecionadas
    {
        get
        {
            if (Session["SubstanciaSelecionadas"] == null)
                return null;
            else
                return (IList<int>)Session["SubstanciaSelecionadas"];
        }
        set { Session["SubstanciaSelecionadas"] = value; }
    }
    public IList<Exigencia> ExigenciasSelecionadas
    {
        get
        {
            if (Session["ExigenciasSelecionadas"] == null)
                return null;
            else
                return (IList<Exigencia>)Session["ExigenciasSelecionadas"];
        }
        set { Session["ExigenciasSelecionadas"] = value; }
    }
    public PopUpNotificacaoAberta NotificacaoAberta
    {
        get
        {
            if (Session["PopUpNotificacaoAberta"] == null)
                return PopUpNotificacaoAberta.NENHUM;
            return (PopUpNotificacaoAberta)Session["PopUpNotificacaoAberta"];
        }
        set { Session["PopUpNotificacaoAberta"] = value; }
    }
    public IList<Notificacao> NotificacoesSelecionadas
    {
        get
        {
            if (Session["NotificacoesSelecionadas"] == null)
                return null;
            else
                return (IList<Notificacao>)Session["NotificacoesSelecionadas"];
        }
        set { Session["NotificacoesSelecionadas"] = value; }
    }
    public IList<Notificacao> NotificacoesDeExigencia
    {
        get
        {
            if (Session["NotificacoesDeExigencia"] == null)
                return null;
            else
                return (IList<Notificacao>)Session["NotificacoesDeExigencia"];
        }
        set { Session["NotificacoesDeExigencia"] = value; }
    }
    public AlvaraPesquisa SessaoAlvaraPesquisa
    {
        get
        {
            if (Session["SessaoAlvaraPesquisa"] == null)
                return null;
            else
                return (AlvaraPesquisa)Session["SessaoAlvaraPesquisa"];
        }
        set { Session["SessaoAlvaraPesquisa"] = value; }
    }
    public Extracao SessaoExtracao
    {
        get
        {
            if (Session["SessaoExtracao"] == null)
                return null;
            else
                return (Extracao)Session["SessaoExtracao"];
        }
        set { Session["SessaoExtracao"] = value; }
    }
    public Licenciamento SessaoLicenciamento
    {
        get
        {
            if (Session["SessaoLicenciamento"] == null)
                return null;
            else
                return (Licenciamento)Session["SessaoLicenciamento"];
        }
        set { Session["SessaoLicenciamento"] = value; }
    }
    public PopUpAbertoENUM PopUpAberto
    {
        get
        {
            if (Session["poUpAberto"] == null)
                return PopUpAbertoENUM.NENHUM;
            return (PopUpAbertoENUM)Session["poUpAberto"];
        }
        set { Session["poUpAberto"] = value; }
    }
    public String CaminhoUpload
    {
        get
        {
            if (Session["CaminhoUpload"] == null)
                return "";
            else
                return (String)Session["CaminhoUpload"];
        }
        set { Session["CaminhoUpload"] = value; }
    }
    public IList<ArquivoFisico> ArquivosUpload
    {
        get
        {
            if (Session["ArquivosUpload"] == null)
                return null;
            return (IList<ArquivoFisico>)Session["ArquivosUpload"];
        }
        set
        {
            Session["ArquivosUpload"] = value;
        }
    }
    public IList<ArquivoFisico> ArquivosUploadExigencias
    {
        get
        {
            if (Session["ArquivosUploadExigencias"] == null)
                return null;
            return (IList<ArquivoFisico>)Session["ArquivosUploadExigencias"];
        }
        set
        {
            Session["ArquivosUploadExigencias"] = value;
        }
    }
    public IList<int> IdLicencasSelecionadasDNPM
    {
        get
        {
            if (Session["LicencasSelecionadasDNPM"] == null)
                return null;
            else
                return (IList<int>)Session["LicencasSelecionadasDNPM"];
        }
        set { Session["LicencasSelecionadasDNPM"] = value; }
    }
    public IList<int> IdContratosConsultados
    {
        get
        {
            if (Session["ContratosConsultados"] == null)
                return null;
            else
                return (IList<int>)Session["ContratosConsultados"];
        }
        set { Session["ContratosConsultados"] = value; }
    }
    public IList<ItemRenovacao> ItensRenovacao
    {
        get
        {
            if (Session["ItensRenovacaoDNPM"] == null)
                return null;
            else
                return (IList<ItemRenovacao>)Session["ItensRenovacaoDNPM"];
        }
        set { Session["ItensRenovacaoDNPM"] = value; }
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

    //Empresas que o usuário edita e empresas que o usuário visualiza
    public IList<Empresa> EmpresasPermissaoModuloDNPM
    {
        get
        {
            if (Session["EmpresasPermissaoModuloDNPM"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoModuloDNPM"];
        }
        set { Session["EmpresasPermissaoModuloDNPM"] = value; }
    }
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

    //Processos que o usuário edita e processos que o usuário visualiza
    public IList<ProcessoDNPM> ProcessosPermissaoModuloDNPM
    {
        get
        {
            if (Session["ProcessosPermissaoModuloDNPM"] == null)
                return null;
            else
                return (IList<ProcessoDNPM>)Session["ProcessosPermissaoModuloDNPM"];
        }
        set { Session["ProcessosPermissaoModuloDNPM"] = value; }
    }
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

    //Sessoes Permissao Modulo Contratos
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

    public IList<Empresa> EmpresasPermissaoModuloContratos
    {
        get
        {
            if (Session["EmpresasPermissaoModuloContratos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoModuloContratos"];
        }
        set { Session["EmpresasPermissaoModuloContratos"] = value; }
    }

    public IList<Setor> SetoresPermissaoModuloContratos
    {
        get
        {
            if (Session["SetoresPermissaoModuloContratos"] == null)
                return null;
            else
                return (IList<Setor>)Session["SetoresPermissaoModuloContratos"];
        }
        set { Session["SetoresPermissaoModuloContratos"] = value; }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.VerificarPermissoes();
            this.AdicionarConfirmacoes();
            this.LimparSessoes();

            try
            {
                this.CarregarClientes();
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
            if (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral == null)
                this.HabilitarControls(false);
            else if (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                this.HabilitarControls(true);
            else
                this.HabilitarControls(false);

            this.EmpresasPermissaoModuloDNPM = null;
            this.EmpresasPermissaoEdicaoModuloNPM = null;

            this.ProcessosPermissaoModuloDNPM = null;
            this.ProcessosPermissaoEdicaoModuloDNPM = null;
        }

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.CarregarSessoesEmpresasDeEdicaoEVisualizacao();

            this.ProcessosPermissaoModuloDNPM = null;
            this.ProcessosPermissaoEdicaoModuloDNPM = null;

            this.HabilitarControls(false);
        }

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            this.EmpresasPermissaoModuloDNPM = null;
            this.EmpresasPermissaoEdicaoModuloNPM = null;

            this.CarregarSessoesProcessosDNPMDeEdicaoEVisualizacao();

            this.HabilitarControls(false);
        }

        this.CarregarPermissoesDoModuloContratos();
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
                    //empresas com permissão de visualização
                    if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || (empresaPermissao.UsuariosVisualizacao != null && empresaPermissao.UsuariosVisualizacao.Count > 0 && empresaPermissao.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                    {
                        if (this.EmpresasPermissaoModuloDNPM == null)
                            this.EmpresasPermissaoModuloDNPM = new List<Empresa>();

                        if (!this.EmpresasPermissaoModuloDNPM.Contains(empresa))
                            this.EmpresasPermissaoModuloDNPM.Add(empresa);
                    }


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
                //processos com permissão de visualização
                if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                {
                    if (this.ProcessosPermissaoModuloDNPM == null)
                        this.ProcessosPermissaoModuloDNPM = new List<ProcessoDNPM>();

                    if (!this.ProcessosPermissaoModuloDNPM.Contains(processo))
                        this.ProcessosPermissaoModuloDNPM.Add(processo);
                }

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

    private void HabilitarControls(bool habilitar)
    {
        barraOpcoes.Visible = btnRenovarValidadeLicenciamento0.Visible = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento.Visible = btnRenovarValidadeExtracao.Visible = btnRenovarValidadeRAL.Visible = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Visible = btnSalvarExigencias0.Enabled = btnSalvarExigencias0.Visible = habilitar;
    }

    private void CarregarPermissoesDoModuloContratos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.EmpresasPermissaoModuloContratos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        }
        else
            this.EmpresasPermissaoModuloContratos = null;

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            this.SetoresPermissaoModuloContratos = Setor.ObterSetoresQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        }
        else
            this.SetoresPermissaoModuloContratos = null;
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloDNPM = null;
        this.EmpresasPermissaoEdicaoModuloNPM = null;
        this.ProcessosPermissaoModuloDNPM = null;
        this.ProcessosPermissaoEdicaoModuloDNPM = null;

        this.EmpresasPermissaoModuloContratos = null;
        this.SetoresPermissaoModuloContratos = null;
    }

    private void SalvarProrrogacao()
    {
        if (tbxPrazoAdicional.Text.ToInt32() <= 0)
        {
            msg.CriarMensagem("É necessário informar um prazo adicional.", "Alerta", MsgIcons.Alerta);
            return;
        }
        if (tbxDataProtocoloAdicional.Text.ToSqlDateTime() <= SqlDate.MinValue)
        {
            msg.CriarMensagem("É necessário informar uma data de Protocolo.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Exigencia exigencia = Exigencia.ConsultarPorId(hfIdExigencia.Value.ToInt32());
        Vencimento venc = exigencia.GetUltimoVencimento != null ? exigencia.GetUltimoVencimento : new Vencimento();

        venc.Data = tbxDataProtocoloAdicional.Text.ToDateTime().AddDays(tbxPrazoAdicional.Text.ToInt32());
        venc = venc.Salvar();

        ProrrogacaoPrazo prorrogacao = new ProrrogacaoPrazo();
        prorrogacao.DataProtocoloAdicional = tbxDataProtocoloAdicional.Text.ToDateTime();
        prorrogacao.PrazoAdicional = tbxPrazoAdicional.Text.ToInt32();
        prorrogacao.ProtocoloAdicional = tbxProtocoloAdicional.Text;
        prorrogacao.Vencimento = venc;
        prorrogacao = prorrogacao.Salvar();
        if (exigencia.GetUltimoVencimento.ProrrogacoesPrazo == null)
            exigencia.GetUltimoVencimento.ProrrogacoesPrazo = new List<ProrrogacaoPrazo>();
        exigencia.GetUltimoVencimento.ProrrogacoesPrazo.Add(prorrogacao);
        exigencia = exigencia.Salvar();

        this.SalvarHistoricoDeInsercaoDeProrrogacao(prorrogacao, exigencia);

        transacao.Recarregar(ref msg);
        this.CarregarProrrogacoes();
        lkbProrrogacao.Text = "Abrir Prorrogações - [" + exigencia.GetUltimoVencimento.ProrrogacoesPrazo.Count + "] Prorrogações.";
    }

    private void LimparSessoes()
    {
        this.SessaoAlvaraPesquisa = new AlvaraPesquisa();
        this.SessaoExtracao = new Extracao();
        this.ExigenciasSelecionadas = null;
        this.NotificacoesSelecionadas = null;
        this.IdLicencasSelecionadasDNPM = null;
        this.SubstanciaSelecionadas = null;
        this.NotificacaoAberta = PopUpNotificacaoAberta.NENHUM;
        this.SessaoLicenciamento = new Licenciamento();
        this.PopUpAberto = PopUpAbertoENUM.NENHUM;
        this.ArquivosUpload = null;
        this.ArquivosUploadExigencias = null;
        this.IdContratosConsultados = null;
    }

    private void SalvarHistoricoDeInsercaoDeProrrogacao(ProrrogacaoPrazo p, Object objetoManipulado)
    {
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        hfTypeAlteracao.Value = objetoManipulado.GetType().Name;

        tbxHistoricoAlteracao.Text = "";
        ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        lblAlteracao.Text = "Uma nova prorrogação foi criada para uma exigência. Processo nº " + (processo != null ? processo.Numero : "") + " - Empresa: " + (processo != null ? processo.Empresa.GetNumeroCNPJeCPFComMascara : "") + ". Nova Data de vencimento:" + p.Vencimento.Data.ToShortDateString();
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));
    }

    private void CarregarProrrogacoes()
    {
        if (hfIdExigencia.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("Salve Primeiro a Exigência", "Alerta", MsgIcons.Alerta);
            return;
        }

        Exigencia exigencia = Exigencia.ConsultarPorId(hfIdExigencia.Value.ToInt32());
        grvProrrogacoes.DataSource = exigencia != null && exigencia.GetUltimoVencimento != null && exigencia.GetUltimoVencimento.ProrrogacoesPrazo != null ? exigencia.GetUltimoVencimento.ProrrogacoesPrazo.OrderByDescending(i => i.Id).ToList() : new List<ProrrogacaoPrazo>();
        grvProrrogacoes.DataBind();
        ModalPopupExtenderlblProrrogacao.Show();
    }

    public string bindNovaData(object o)
    {
        ProrrogacaoPrazo p = (ProrrogacaoPrazo)o;
        return p.DataProtocoloAdicional.AddDays(p.PrazoAdicional).ToShortDateString();
    }

    public void EnviarHistoricoPorEmail()
    {
        Email email = new Email();
        foreach (ListItem l in chkGruposHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        foreach (ListItem l in chkConsultoraHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        foreach (ListItem l in chkEmpresaHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        email.AdicionarDestinatario(tbxEmailsHistorico.Text.ToString());

        email.Assunto = "";

        switch (hfTypeObs.Value)
        {
            case "GuiaUtilizacao":
                {
                    GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIdGuiaUtilizacao.Value.ToInt32());
                    if (guia != null)
                    {
                        email.Assunto = "Registros Históricos da Guia de Utilização Nº: " + guia.Numero;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(guia.Historicos, email.Assunto);
                    }
                }
                break;

            case "RequerimentoPesquisa":
                {
                    RequerimentoPesquisa requerimento = RequerimentoPesquisa.ConsultarPorId(hfRequerimentoPesquisa.Value.ToInt32());
                    if (requerimento != null)
                    {
                        email.Assunto = "Registros Históricos do Requerimento de Pesquisa";
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(requerimento.Historicos, email.Assunto);
                    }
                }
                break;

            case "AlvaraPesquisa":
                {
                    AlvaraPesquisa alv = AlvaraPesquisa.ConsultarPorId(hfAlvaraPesquisa.Value.ToInt32());
                    if (alv != null)
                    {
                        email.Assunto = "Registros Históricos do Álvara de Pesquisa Nº: " + alv.Numero;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(alv.Historicos, email.Assunto);
                    }
                }
                break;

            case "RequerimentoLavra":
                {
                    RequerimentoLavra requerimento = RequerimentoLavra.ConsultarPorId(hfIdRequerimentoLavra.Value.ToInt32());
                    if (requerimento != null)
                    {
                        email.Assunto = "Registros Históricos do Requerimento de Lavra";
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(requerimento.Historicos, email.Assunto);
                    }
                }
                break;

            case "ConcessaoLavra":
                {
                    ConcessaoLavra concessao = ConcessaoLavra.ConsultarPorId(hfIdConcessaoLavra.Value.ToInt32());
                    if (concessao != null)
                    {
                        email.Assunto = "Registros Históricos da Concessão de Lavra Nº: " + concessao.NumeroPortariaLavra;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(concessao.Historicos, email.Assunto);
                    }
                }
                break;

            case "Licenciamento":
                {
                    Licenciamento lic = Licenciamento.ConsultarPorId(hfIdLicenciamento.Value.ToInt32());
                    if (lic != null)
                    {
                        email.Assunto = "Registros Históricos do Licenciamento Nº: " + lic.Numero;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(lic.Historicos, email.Assunto);
                    }
                }
                break;

            case "Extracao":
                {
                    Extracao ext = Extracao.ConsultarPorId(hfIdExtracao.Value.ToInt32());
                    if (ext != null)
                    {
                        email.Assunto = "Registros Históricos da Extração Nº: " + ext.NumeroExtracao;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(ext.Historicos, email.Assunto);
                    }
                }
                break;

            case "Exigencia":
                {
                    Exigencia exig = Exigencia.ConsultarPorId(hfIdExigencia.Value.ToInt32());
                    if (exig != null)
                    {
                        email.Assunto = "Registros Históricos da Exigência: " + exig.Descricao;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(exig.Historicos, email.Assunto);
                    }
                }
                break;
        }

        if (!email.EnviarAutenticado(25, false))
            msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
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

        switch (hfTypeObs.Value.ToString())
        {
            case "GuiaUtilizacao":
                {
                    GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.GuiaUtilizacao = guia;
                    h = h.Salvar();
                    guia.Historicos.Add(h);
                    grvHistoricos.DataSource = guia.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();

                } break;
            case "AlvaraPesquisa":
                {
                    AlvaraPesquisa alv = AlvaraPesquisa.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Regime = alv;
                    h = h.Salvar();
                    alv.Historicos.Add(h);
                    grvHistoricos.DataSource = alv.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "Licenciamento":
                {
                    Licenciamento lic = Licenciamento.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Regime = lic;
                    h = h.Salvar();
                    lic.Historicos.Add(h);
                    grvHistoricos.DataSource = lic.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "Extracao":
                {
                    Extracao ext = Extracao.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Regime = ext;
                    h = h.Salvar();
                    ext.Historicos.Add(h);
                    grvHistoricos.DataSource = ext.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "Exigencia":
                {
                    Exigencia exig = Exigencia.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Exigencia = exig;
                    h = h.Salvar();
                    exig.Historicos.Add(h);
                    grvHistoricos.DataSource = exig.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "RequerimentoPesquisa":
                {
                    RequerimentoPesquisa req = RequerimentoPesquisa.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Regime = req;
                    h = h.Salvar();
                    req.Historicos.Add(h);
                    grvHistoricos.DataSource = req.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "RequerimentoLavra":
                {
                    RequerimentoLavra req = RequerimentoLavra.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Regime = req;
                    h = h.Salvar();
                    req.Historicos.Add(h);
                    grvHistoricos.DataSource = req.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "ConcessaoLavra":
                {
                    ConcessaoLavra conc = ConcessaoLavra.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Regime = conc;
                    h = h.Salvar();
                    conc.Historicos.Add(h);
                    grvHistoricos.DataSource = conc.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            default:
                return;
        }

        tbxTituloObs.Text = "";
        tbxObservacaoObs.Text = "";

    }

    private IList<Licenca> ReconsultarLicencas()
    {
        IList<Licenca> listaAux = new List<Licenca>();
        if (this.IdLicencasSelecionadasDNPM != null && this.IdLicencasSelecionadasDNPM.Count > 0)
        {
            for (int i = 0; i < this.IdLicencasSelecionadasDNPM.Count; i++)
            {
                Licenca l = Licenca.ConsultarPorId(this.IdLicencasSelecionadasDNPM[i]);
                if (l != null && !listaAux.Contains(l))
                    listaAux.Add(l);
            }
        }
        return listaAux;
    }

    private void RemoverVencimento()
    {
        if (ddlVencimentos.Items.Count <= 1)
        {
            msg.CriarMensagem("Não é possivel excluir este vencimento.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Vencimento vencimento = Vencimento.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
        vencimento.Excluir();
        ddlVencimentos.Items.RemoveAt(ddlVencimentos.SelectedIndex);
        this.CarregarVencimento();
    }

    private void PrimeiraOpcaoDDL(params DropDownList[] ddls)
    {
        foreach (DropDownList ddl in ddls)
            ddl.SelectedValue = "0";
    }

    private void LimparDDL(params DropDownList[] ddls)
    {
        foreach (DropDownList ddl in ddls)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("-- Selecione --", "0"));
        }
    }

    private void CarregarOrgaosLicenca(object lista)
    {
        ddlOrgaoLicenca.DataTextField = "Nome";
        ddlOrgaoLicenca.DataValueField = "Id";
        ddlOrgaoLicenca.DataSource = lista;
        ddlOrgaoLicenca.DataBind();
        ddlOrgaoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        ddlOrgaoLicenca.SelectedIndex = 0;
    }

    private void CarregarCidade(int id)
    {
        if (id > 0)
        {
            IList<Cidade> cidades = Estado.ConsultarPorId(id).Cidades;
            ddlCidadeLicenca.DataValueField = "Id";
            ddlCidadeLicenca.DataTextField = "Nome";
            ddlCidadeLicenca.DataSource = cidades;
            ddlCidadeLicenca.DataBind();
        }
        else
        {
            ddlCidadeLicenca.Items.Clear();
        }
        ddlCidadeLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        ddlCidadeLicenca.SelectedIndex = 0;
    }

    private void CarregarEstadosLicenca()
    {
        ddlEstadoLicenca.DataValueField = "Id";
        ddlEstadoLicenca.DataTextField = "Nome";
        ddlEstadoLicenca.DataSource = Estado.ConsultarTodos();
        ddlEstadoLicenca.DataBind();
        ddlEstadoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void NovoLicenciamento()
    {

        lkbObservacoesLicenciamento.Visible = false;
        tbxPublicacaoLicenciamento.Enabled = true;

        this.CarregarStatus(ddlEstatusVencimentoLicenciamento);

        lkbVencimentoLicenciamento.Visible = false;

        this.PopUpAberto = PopUpAbertoENUM.LICENCIAMENTO;
        btnPopUpSelecionarRegime_ModalPopupExtender.Hide();
        lblPopUpLicenciamento_ModalPopupExtender.Show();

        tbxPublicacaoLicenciamento.Text = "";
        tbxValidadeLicenciamento.Text = "";
        tbxNumeroLicenciamento.Text = "";

        hfIdLicenciamento.Value = "0";

        ProcessoDNPM p = new ProcessoDNPM();
        p.Id = this.RetornarIdDoProcessoSelecionadoNaArvore();
        p = p.ConsultarPorId();
        tbxDataAberturaLicenciamento.Text = p.DataAbertura.EmptyToMinValue();

        grvLicencasLicenciamento.DataSource = null;
        grvLicencasLicenciamento.DataBind();

        grvNotificacaoLicenciamento.DataSource = null;
        grvNotificacaoLicenciamento.DataBind();

        grvNotificacaoEntregaLicencieamento.DataSource = null;
        grvNotificacaoEntregaLicencieamento.DataBind();

        grvNotificacaoValidadeLicenciamento.DataSource = null;
        grvNotificacaoValidadeLicenciamento.DataBind();

        this.ArquivosUpload = null;
        this.SessaoUploadsLicenciamento = new List<ArquivoFisico>();
    }

    private void NovoRequerimentoPesquisa()
    {
        hfRequerimentoPesquisa.Value = "0";
        lkbObservacoesRequerimento.Visible = false;

        this.PopUpAberto = PopUpAbertoENUM.REQUERIMENTOPESQUISA;

        ProcessoDNPM p = new ProcessoDNPM();
        p.Id = this.RetornarIdDoProcessoSelecionadoNaArvore();
        p = p.ConsultarPorId();
        tbxDataEntradaRequerimentoPesquisa.Text = p.DataAbertura.EmptyToMinValue();

        grvExigenciasRequerimentoPesquisa.DataSource = null;
        grvExigenciasRequerimentoPesquisa.DataBind();

        btnPopUpRequerimentoPesquisa_popupextender.Show();
        btnPopUpSelecionarRegime_ModalPopupExtender.Hide();

        this.SessaoUploadsRequerimentoPesquisa = new List<ArquivoFisico>();
        this.ArquivosUpload = new List<ArquivoFisico>();
    }

    private void NovoRequerimentoLavra()
    {
        lkbObservacoesRequerimentoLavra.Visible = false;

        this.PopUpAberto = PopUpAbertoENUM.REQUERIMENTOLAVRA;
        btnPopUpSelecionarRegime_ModalPopupExtender.Hide();
        lblPopUpRequerimentoLavra_ModalPopupExtender.Show();

        tbxDataEntradaRequerimentoLavra.Text = "";

        grvExigenciasRequerimentoLavra.DataSource = null;
        grvExigenciasRequerimentoLavra.DataBind();

        this.ArquivosUpload = null;
        this.SessaoUploadsRequerimentoLavra = new List<ArquivoFisico>();
    }

    private void NovaExtracao()
    {
        this.CarregarStatus(ddlEstatusVencimentoExtracao);
        lkbObservacoesExtracao.Visible = false;

        lkbVencimentoExtracao.Visible = false;

        tbxPublicacaoExtracao.Text = "";
        tbxnumeroLicencaExtracao.Text = "";
        tbxValidadeLicencaExtracao.Text = "";

        hfIdExtracao.Value = "0";

        grvExigenciaExtracao.DataSource = null;
        grvExigenciaExtracao.DataBind();

        tbxValidadeExtracao.Text = "";

        ProcessoDNPM p = new ProcessoDNPM();
        p.Id = this.RetornarIdDoProcessoSelecionadoNaArvore();
        p = p.ConsultarPorId();
        tbxDataAberturaExtracao.Text = p.DataAbertura.EmptyToMinValue();

        grvNotificacaoValidadeExtracao.DataSource = null;
        grvNotificacaoValidadeExtracao.DataBind();

        this.PopUpAberto = PopUpAbertoENUM.EXTRACAO;
        btnPopUpSelecionarRegime_ModalPopupExtender.Hide();
        lblPopUpExtracao_ModalPopupExtender.Show();

        this.ArquivosUpload = null;
        this.SessaoUploadsExtracao = new List<ArquivoFisico>();
    }

    public IList<ArquivoFisico> RecarregarArquivos(IList<ArquivoFisico> lista)
    {
        IList<ArquivoFisico> listaAux = new List<ArquivoFisico>();
        if (lista != null && lista.Count > 0)
        {
            foreach (ArquivoFisico item in lista)
            {
                listaAux.Add(ArquivoFisico.ConsultarPorId(item.Id));
            }
        }
        return listaAux;
    }

    public IList<ArquivoFisico> RecarregarArquivosProcessoDNPM(IList<ArquivoFisico> lista, ProcessoDNPM processoDNPM)
    {
        IList<ArquivoFisico> listaAux = new List<ArquivoFisico>();
        if (lista != null && lista.Count > 0)
        {
            foreach (ArquivoFisico item in lista)
            {
                ArquivoFisico arqFisc = ArquivoFisico.ConsultarPorId(item.Id);
                arqFisc.ProcessoDNPM = processoDNPM;
                arqFisc = arqFisc.Salvar();

                listaAux.Add(arqFisc);
            }
        }
        return listaAux;
    }

    public IList<ContratoDiverso> RecarregarContratos()
    {
        IList<ContratoDiverso> listaAux = new List<ContratoDiverso>();
        IList<int> lista = this.IdContratosConsultados;

        if (lista != null && lista.Count > 0)
        {
            foreach (int item in lista)
            {
                listaAux.Add(ContratoDiverso.ConsultarPorId(item));
            }
        }
        return listaAux;
    }

    private void AddNotificacaoValidadeAlvaraPesquisa()
    {
        this.NotificacaoAberta = PopUpNotificacaoAberta.ALVARADEPESQUISAVALIDADE;
        this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
    }

    private void AddNotificacaoNotificacaoPesquisa()
    {
        this.NotificacaoAberta = PopUpNotificacaoAberta.ALVARADEPESQUISAINICIOPESQUISA;
        this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
    }

    private void AddNotificacaoRequerimentoLavra()
    {
        this.NotificacaoAberta = PopUpNotificacaoAberta.ALVARADEPESQUISAREQUERIMENTODELAVRA;
        this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
    }

    private void AddNotificacaoLPPoligonal()
    {
        this.NotificacaoAberta = PopUpNotificacaoAberta.ALVARADEPESQUISAREQUERIMENTOLPPOLIGONAL;
        this.CarregarPopUpNotificacao(true, false);
    }

    private void AddNotificacaoDIPEM()
    {
        this.NotificacaoAberta = PopUpNotificacaoAberta.DIPEM;
        this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
    }

    private void ExcluirRegime()
    {
        if (trvProcessos.SelectedValue.Contains("RP_"))
        {
            RequerimentoPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
        }
        else if (trvProcessos.SelectedValue.Contains("ALP_"))
        {
            AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
        }
        else if (trvProcessos.SelectedValue.Contains("EX_"))
        {
            Extracao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
        }
        else if (trvProcessos.SelectedValue.Contains("LI_"))
        {
            Licenciamento.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
        }
        else if (trvProcessos.SelectedValue.Contains("RL_"))
        {
            RequerimentoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
        }
        else if (trvProcessos.SelectedValue.Contains("CO_"))
        {
            ConcessaoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
        }
        else
        {
            msg.CriarMensagem("Selecione primeiro o REGIME que deseja excluir.", "Alerta", MsgIcons.Alerta);
            return;
        }
        mvwProcessos.ActiveViewIndex = -1;

        transacao.Recarregar(ref msg);

        this.CarregarArvore();

    }

    private void ExcluirProcesso()
    {
        hfIdProcessoDNPM.Value = "0";
        mvwProcessos.ActiveViewIndex = -1;

        if (trvProcessos.SelectedValue.Contains("PROC_"))
        {
            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());

            if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                processo.UsuariosEdicao = null;
                processo.UsuariosVisualizacao = null;
            }

            RAL ral = processo.RAL != null ? RAL.ConsultarPorId(processo.RAL.Id) : null;

            if (ral != null)
            {
                ral.ProcessoDNPM = null;
                ral = ral.Salvar();
                processo.RAL = null;
                processo = processo.Salvar();

                transacao.Recarregar(ref msg);

                ral.Excluir();
            }

            processo.Excluir();

            transacao.Recarregar(ref msg);
            this.VerificarPermissoes();
            this.CarregarArvore();
        }
        else
        {
            msg.CriarMensagem("Selecione primeiro o Processo ANM que deseja excluir.", "Alerta", MsgIcons.Alerta);
        }
    }

    private void ExcluirGuiaUtilizacao()
    {
        if (trvProcessos.SelectedValue.Contains("GUIA_"))
        {
            mvwProcessos.ActiveViewIndex = -1;
            hfIdProcessoDNPM.Value = "0";
            GuiaUtilizacao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).Excluir();
            transacao.Recarregar(ref msg);
            this.CarregarArvore();
        }
        else
        {
            msg.CriarMensagem("Selecione um GUIA.", "Alerta", MsgIcons.Alerta);
        }
    }

    private void ExcluirRAL()
    {
        if (trvProcessos.SelectedValue.Contains("RAL_"))
        {
            mvwProcessos.ActiveViewIndex = -1;
            RAL ral = RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            ral.Excluir();
            transacao.Recarregar(ref msg);
            this.CarregarArvore();
        }
        else
        {
            msg.CriarMensagem("Selecione um RAL.", "Alerta", MsgIcons.Alerta);
        }
    }

    private void AdicionarLicencasAssociadas()
    {
        if (IdLicencasSelecionadasDNPM == null)
            this.IdLicencasSelecionadasDNPM = new List<int>();

        IList<Licenca> licencas = this.ReconsultarLicencas();

        foreach (TreeNode tn in tvwLicencas.Nodes)
        {
            if (tn.ChildNodes != null)
                foreach (TreeNode nodeLicenca in tn.ChildNodes)
                {
                    if (nodeLicenca.Checked)
                        if (nodeLicenca.Value.ToUpper().Contains('L'))
                        {
                            Licenca l = Licenca.ConsultarPorId(nodeLicenca.Value.Split('_')[1].ToInt32());
                            if (l != null)
                            {
                                licencas.Remove(l);
                                licencas.Add(l);
                                this.IdLicencasSelecionadasDNPM.Remove(l.Id);
                                this.IdLicencasSelecionadasDNPM.Add(l.Id);
                            }
                        }
                }
        }

        if (hfTipo.Value.ToUpper() == "LICENCIAMENTO")
        {
            this.CarregarLicencasDoLicenciamento(licencas);
        }
        else if (hfTipo.Value.ToUpper() == "PROCESSO")
        {
            this.CarregarLicencasDoProcessoDNPM(licencas);
        }

        btnSelecionarLicencaHide_ModalPopupExtender.Hide();
    }

    private void CarregarLicencasDoLicenciamento(IList<Licenca> ls)
    {
        grvLicencasLicenciamento.DataSource = ls;
        grvLicencasLicenciamento.DataBind();
    }

    private int RetornarIdSelecionadoNaArvore()
    {
        return trvProcessos.SelectedValue.Split('_')[1].ToInt32();
    }

    private int RetornarIdDoProcessoSelecionadoNaArvore()
    {
        if (hfIdProcessoDNPM.Value.IsNotNullOrEmpty() && hfIdProcessoDNPM.Value != "0")
        {
            return hfIdProcessoDNPM.Value.ToInt32();
        }
        if (trvProcessos.SelectedValue.Contains("PROC_"))
        {
            return trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32();
        }
        if (trvProcessos.SelectedValue.Contains("RAL_") || trvProcessos.SelectedValue.Contains("GUIA_") || trvProcessos.SelectedValue.Contains("EX_") || trvProcessos.SelectedValue.Contains("LI_"))
        {
            return trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32();
        }
        if (trvProcessos.SelectedValue.Contains("RP_") || trvProcessos.SelectedValue.Contains("ALP_") || trvProcessos.SelectedValue.Contains("RL_") || trvProcessos.SelectedValue.Contains("CO_"))
        {
            return trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32();
        }
        return 0;
    }

    private void AdicionarConfirmacoes()
    {
        WebUtil.AdicionarConfirmacao(lkbOpcoesProcessoExcluir, "Deseja realmente excluír este Processo?");
        WebUtil.AdicionarConfirmacao(lkbRegimeExcluir, "Deseja realmente excluír este Regime?");
        WebUtil.AdicionarConfirmacao(lkbOpcoesGuiaExcluir, "Deseja realmente excluír esta GUIA?");
        WebUtil.AdicionarConfirmacao(lkbOpcoesRALExcluir, "Deseja realmente excluír esta RAL?");
    }

    private void ExibirVisoes()
    {
        if (trvProcessos.SelectedNode == null)
            return;

        if (trvProcessos.SelectedNode != null)
            trvProcessos.SelectedNode.Expand();

        mvwProcessos.ActiveViewIndex = -1;

        barraOpcoes.Visible = false;

        if (trvProcessos.SelectedValue == "DNPM")
            return;


        hfIdEmpresa.Value = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore()).Empresa.Id.ToString();

        this.LimparSessoes();

        if (trvProcessos.SelectedValue.Contains("DNPM"))
        {
            return;
        }
        if (trvProcessos.SelectedValue.Contains("PROC_"))
        {
            this.ExibirProcessos(ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore()));
            return;
        }
        if (trvProcessos.SelectedValue.Contains("RAL_"))
        {
            this.ExibirRAL(RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()));
            return;
        }
        if (trvProcessos.SelectedValue.Contains("GUIA_"))
        {
            this.ExibirGuiaUtilizacao(GuiaUtilizacao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()));
            return;
        }
        else
        {
            if (trvProcessos.SelectedValue.Contains("RP_"))
            {
                RequerimentoPesquisa regime = RequerimentoPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                this.ExibirRequerimentoPesquisa(regime);
                return;
            }
            else if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                this.ExibirAlvaraPesquisa(regime);
                return;
            }
            else if (trvProcessos.SelectedValue.Contains("EX_"))
            {
                Extracao regime = Extracao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                this.ExibirExtracao(regime);
            }
            else if (trvProcessos.SelectedValue.Contains("LI_"))
            {
                Licenciamento regime = Licenciamento.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                this.ExibirLicenciamento(regime);
            }
            else if (trvProcessos.SelectedValue.Contains("RL_"))
            {
                RequerimentoLavra regime = RequerimentoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                this.ExibirRequerimentoLavra(regime);
            }
            else if (trvProcessos.SelectedValue.Contains("CO_"))
            {
                ConcessaoLavra regime = ConcessaoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                this.ExibirConcessaoLavra(regime);
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>$('.proc_tool').hover(function(){tooltip.show('Ao clicar no processo, seus dados serão exibidos');}, function(){tooltip.hide();});</script>", false);
    }

    private void TestarPermissoes()
    {
        //Verificando permissão de editar configuração do tipo geral (comum a qq vencimento)
        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
            {
                barraOpcoes.Visible = true;
                btnRenovarAlvara.Enabled = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento0.Enabled = btnRenovarValidadeLicenciamento0.Visible = btnRenovarValidadeLicenciamento.Enabled = btnRenovarValidadeLicenciamento.Visible =
                    btnRenovarValidadeExtracao.Enabled = btnRenovarValidadeExtracao.Visible = btnUploadRAL.Enabled = btnUploadRAL.Visible = btnRenovarValidadeRAL.Enabled = btnRenovarValidadeRAL.Visible =
                    ibtnAddLicencaProcesso0.Enabled = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Enabled = btnRenovarValidadeGuia.Visible = lkbVencimentoRAL.Enabled = lkbVencimentoRAL.Visible = true;

            }
            else
            {
                barraOpcoes.Visible = false;
                btnRenovarAlvara.Enabled = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento0.Enabled = btnRenovarValidadeLicenciamento0.Visible = btnRenovarValidadeLicenciamento.Enabled = btnRenovarValidadeLicenciamento.Visible =
                    btnRenovarValidadeExtracao.Enabled = btnRenovarValidadeExtracao.Visible = btnUploadRAL.Enabled = btnUploadRAL.Visible = btnRenovarValidadeRAL.Enabled = btnRenovarValidadeRAL.Visible =
                    ibtnAddLicencaProcesso0.Enabled = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Enabled = btnRenovarValidadeGuia.Visible = lkbVencimentoRAL.Enabled = lkbVencimentoRAL.Visible = false;
            }
        }

        //Verificando permissão de editar configuração do tipo por empresa
        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            Empresa empresaDoObjetoSelecionado = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());

            if (empresaDoObjetoSelecionado != null && this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 && this.EmpresasPermissaoEdicaoModuloNPM.Contains(empresaDoObjetoSelecionado))
            {
                barraOpcoes.Visible = true;
                btnRenovarAlvara.Enabled = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento0.Enabled = btnRenovarValidadeLicenciamento0.Visible = btnRenovarValidadeLicenciamento.Enabled = btnRenovarValidadeLicenciamento.Visible =
                    btnRenovarValidadeExtracao.Enabled = btnRenovarValidadeExtracao.Visible = btnUploadRAL.Enabled = btnUploadRAL.Visible = btnRenovarValidadeRAL.Enabled = btnRenovarValidadeRAL.Visible =
                    ibtnAddLicencaProcesso0.Enabled = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Enabled = btnRenovarValidadeGuia.Visible = lkbVencimentoRAL.Enabled = lkbVencimentoRAL.Visible = true;
            }
            else
            {
                barraOpcoes.Visible = false;
                btnRenovarAlvara.Enabled = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento0.Enabled = btnRenovarValidadeLicenciamento0.Visible = btnRenovarValidadeLicenciamento.Enabled = btnRenovarValidadeLicenciamento.Visible =
                    btnRenovarValidadeExtracao.Enabled = btnRenovarValidadeExtracao.Visible = btnUploadRAL.Enabled = btnUploadRAL.Visible = btnRenovarValidadeRAL.Enabled = btnRenovarValidadeRAL.Visible =
                    ibtnAddLicencaProcesso0.Enabled = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Enabled = btnRenovarValidadeGuia.Visible = lkbVencimentoRAL.Enabled = lkbVencimentoRAL.Visible = false;
            }
        }

        //Verificando permissão de editar configuração do tipo por processo
        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());

            if (processo != null && this.ProcessosPermissaoEdicaoModuloDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM.Count > 0 && this.ProcessosPermissaoEdicaoModuloDNPM.Contains(processo))
            {
                barraOpcoes.Visible = true;
                btnRenovarAlvara.Enabled = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento0.Enabled = btnRenovarValidadeLicenciamento0.Visible = btnRenovarValidadeLicenciamento.Enabled = btnRenovarValidadeLicenciamento.Visible =
                    btnRenovarValidadeExtracao.Enabled = btnRenovarValidadeExtracao.Visible = btnUploadRAL.Enabled = btnUploadRAL.Visible = btnRenovarValidadeRAL.Enabled = btnRenovarValidadeRAL.Visible =
                    ibtnAddLicencaProcesso0.Enabled = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Enabled = btnRenovarValidadeGuia.Visible = lkbVencimentoRAL.Enabled = lkbVencimentoRAL.Visible = true;
            }
            else
            {
                barraOpcoes.Visible = false;
                btnRenovarAlvara.Enabled = btnRenovarAlvara.Visible = btnRenovarValidadeLicenciamento0.Enabled = btnRenovarValidadeLicenciamento0.Visible = btnRenovarValidadeLicenciamento.Enabled = btnRenovarValidadeLicenciamento.Visible =
                    btnRenovarValidadeExtracao.Enabled = btnRenovarValidadeExtracao.Visible = btnUploadRAL.Enabled = btnUploadRAL.Visible = btnRenovarValidadeRAL.Enabled = btnRenovarValidadeRAL.Visible =
                    ibtnAddLicencaProcesso0.Enabled = ibtnAddLicencaProcesso0.Visible = btnRenovarValidadeGuia.Enabled = btnRenovarValidadeGuia.Visible = lkbVencimentoRAL.Enabled = lkbVencimentoRAL.Visible = false;
            }

        }
    }

    private void CarregarClientes()
    {
        ddlClientes.DataTextField = "Nome";
        ddlClientes.DataValueField = "Id";
        ddlClientes.DataSource = GrupoEconomico.ConsultarGruposAtivos();
        ddlClientes.DataBind();
        ddlClientes.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarArvore()
    {
        TreeNode noSelecionado = trvProcessos.SelectedNode;

        trvProcessos.Nodes.Clear();
        if (ddlClientes.SelectedValue.IsNotNullOrEmpty())
        {
            IList<ProcessoDNPM> processos = new List<ProcessoDNPM>();

            if (ddlClientes.SelectedIndex > 0)
            {
                if (ddlEmpresa.SelectedIndex > 0)
                {

                    if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                        processos = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()).ProcessosDNPM.Where(x => this.ProcessosPermissaoModuloDNPM.Contains(x)).ToList();
                    else
                        processos = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()).ProcessosDNPM;
                }
                else
                {
                    processos = ProcessoDNPM.ConsultarProcessosDoClientePelasPermissoesstatusEmpresa(GrupoEconomico.ConsultarPorId(ddlClientes.SelectedValue.ToInt32()), this.ConfiguracaoModuloDNPM.Tipo, this.EmpresasPermissaoModuloDNPM, this.ProcessosPermissaoModuloDNPM, ddlStatusEmpresa.SelectedValue);
                }
            }

            TreeNode nodeRaiz = new TreeNode("Processos ANM", "DNPM");
            nodeRaiz.Expand();
            trvProcessos.Nodes.Add(nodeRaiz);

            if (processos != null && processos.Count > 0)
            {

                foreach (ProcessoDNPM processo in processos)
                {
                    if (!processo.Numero.Contains(tbxNumeroProcesso.Text))
                    {
                        continue;
                    }

                    TreeNode node = new TreeNode("<b class='proc_tool'>" + processo.GetNumeroProcessoComMascara + "</b>", "PROC_" + processo.Id.ToString());
                    this.TestarExpand(node, noSelecionado);
                    TreeNode nodeRegime = null;
                    if (processo.Regimes != null && processo.Regimes.Count > 0)
                    {
                        trvProcessos.Nodes[0].ChildNodes.Add(node);

                        foreach (Regime regime in processo.Regimes)
                        {

                            if (regime.GetType() == typeof(RequerimentoPesquisa))
                            {
                                nodeRegime = new TreeNode("Requerimento de Pesquisa", "RP_" + regime.Id.ToString());
                                this.TestarExpand(nodeRegime, noSelecionado);
                                node.ChildNodes.Add(nodeRegime);
                            }

                            else if (regime.GetType() == typeof(AlvaraPesquisa))
                            {
                                nodeRegime = new TreeNode("Alvará de Pesquisa", "ALP_" + regime.Id.ToString());
                                this.TestarExpand(nodeRegime, noSelecionado);
                                node.ChildNodes.Add(nodeRegime);
                            }

                            else if (regime.GetType() == typeof(Extracao))
                            {
                                nodeRegime = new TreeNode("Extração", "EX_" + regime.Id.ToString());
                                this.TestarExpand(nodeRegime, noSelecionado);
                                node.ChildNodes.Add(nodeRegime);
                            }

                            else if (regime.GetType() == typeof(Licenciamento))
                            {
                                nodeRegime = new TreeNode("Licenciamento", "LI_" + regime.Id.ToString());
                                this.TestarExpand(nodeRegime, noSelecionado);
                                node.ChildNodes.Add(nodeRegime);
                            }

                            else if (regime.GetType() == typeof(RequerimentoLavra))
                            {
                                nodeRegime = new TreeNode("Requerimento de Lavra", "RL_" + regime.Id.ToString());
                                this.TestarExpand(nodeRegime, noSelecionado);
                                node.ChildNodes.Add(nodeRegime);
                            }

                            else if (regime.GetType() == typeof(ConcessaoLavra))
                            {
                                nodeRegime = new TreeNode("Concessão de Lavra", "CO_" + regime.Id.ToString());
                                this.TestarExpand(nodeRegime, noSelecionado);
                                node.ChildNodes.Add(nodeRegime);
                            }
                        }
                    }
                    else
                    {
                        trvProcessos.Nodes[0].ChildNodes.Add(node);
                    }

                    if (processo.RAL != null)
                    {
                        nodeRegime = new TreeNode("RAL", "RAL_" + processo.RAL.Id.ToString());
                        this.TestarExpand(nodeRegime, noSelecionado);
                        node.ChildNodes.Add(nodeRegime);
                    }

                    if (processo.Guias != null)
                    {
                        foreach (GuiaUtilizacao guia in processo.Guias)
                        {
                            nodeRegime = new TreeNode("Guia de Utilização - " + guia.Numero, "GUIA_" + guia.Id.ToString());
                            this.TestarExpand(nodeRegime, noSelecionado);
                            node.ChildNodes.Add(nodeRegime);
                        }
                    }
                }

            }

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>$('.proc_tool').hover(function(){tooltip.show('Ao clicar no processo, seus dados serão exibidos');}, function(){tooltip.hide();});</script>", false);

    }

    private void TestarExpand(TreeNode nodeTeste, TreeNode noSelecionado)
    {
        if (noSelecionado != null && noSelecionado.ValuePath.Contains(nodeTeste.Value))
        {
            nodeTeste.Selected = true;
            nodeTeste.Expand();
        }
    }

    private bool TestarExisteRegimeNoProcessoAutorizacaoPesquisa(ProcessoDNPM processo)
    {
        foreach (Regime r in processo.Regimes)
        {
            if (r.GetType() == typeof(RequerimentoPesquisa) || r.GetType() == typeof(AlvaraPesquisa))
            {
                return true;
            }
        }
        return false;
    }

    private bool TestarExisteRegimeNoProcessoConcessaoLavra(ProcessoDNPM processo)
    {
        foreach (Regime r in processo.Regimes)
        {
            if (r.GetType() == typeof(RequerimentoLavra) || r.GetType() == typeof(ConcessaoLavra))
            {
                return true;
            }
        }
        return false;
    }

    private void CarregarEmpresas()
    {
        ddlEmpresaLicenca.DataValueField = "Id";
        ddlEmpresaLicenca.DataTextField = "Nome";
        ddlEmpresaLicenca.DataSource = GrupoEconomico.ConsultarPorId(ddlClientes.SelectedValue.ToInt32()).Empresas;
        ddlEmpresaLicenca.DataBind();

        if (ddlEmpresa.SelectedIndex > 0)
        {
            ddlEmpresaLicenca.SelectedValue = ddlEmpresa.SelectedValue;
        }
    }

    private void CarregarEmpresasQueOUsuarioTemAcesso(DropDownList dropEmpresa, int idGrupoEconomico)
    {
        dropEmpresa.Items.Clear();
        dropEmpresa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (idGrupoEconomico > 0)
                empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM : new List<Empresa>();
        }
        else
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(idGrupoEconomico);
            empresas = c.Empresas != null ? c.Empresas : new List<Empresa>();
        }

        if (empresas != null && empresas.Count > 0)
        {
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void CarregarEmpresasQueOUsuarioTemAcessoComFiltroStatus(DropDownList dropEmpresa, int idGrupoEconomico, String status)
    {
        dropEmpresa.Items.Clear();
        dropEmpresa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (idGrupoEconomico > 0)
                empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM : new List<Empresa>();
        }
        else
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(idGrupoEconomico);
            empresas = c.Empresas != null ? c.Empresas : new List<Empresa>();
        }

        if (empresas != null && empresas.Count > 0)
        {
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    if (status == "Ativo" && emp.Ativo == true)
                    {
                        dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                    }
                    else if (status == "Inativo" && emp.Ativo == false) {
                        dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                    }
                    else if(status == "Todos")
                    {
                        dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                    }
                    else
                    {

                    }
                    
                }else
                {
                    if (status == "Ativo" && emp.Ativo == true)
                    {
                        dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                    }
                    else if (status == "Inativo" && emp.Ativo == false)
                    {
                        dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                    } else if (status == "Todos") {
                        dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                    }
                    else
                    {
                    }
                    
                }
            }
        }
    }

    private void ExibirProcessos(ProcessoDNPM p)
    {
        this.TestarPermissoes();

        mvwProcessos.ActiveViewIndex = 0;

        frameprocesso.Attributes.Add("src", "https://sistemas.dnpm.gov.br/SCM/Extra/site/admin/dadosProcesso.aspx" + GetParametrosDNPM(p.Numero));

        lblNumeroProcesso.Text = p.GetNumeroProcessoComMascara;
        lblSubstancia.Text = p.Substancia;
        lblDataAbertura.Text = p.DataAbertura.EmptyToMinValue();
        if (p.Consultora != null)
        {
            lblConsultoria.Text = p.Consultora.Nome;
        }
        else
        {
            lblConsultoria.Text = "Consultoria não informada";
        }
        lblEmpresa.Text = p.Empresa.Nome;
        lblIdentificacaoArea.Text = p.Identificacao;
        lblTamanho.Text = p.TamanhoArea + " hectares";
        lblEndereco.Text = p.Endereco;
        lblObs.Text = p.Observacoes;
        if (p.Cidade != null)
        {
            lblCidade.Text = p.Cidade.Nome;
            lblEstado.Text = p.Cidade.Estado.Nome;
        }
        else
        {
            lblCidade.Text = "Cidade não informada";
            lblEstado.Text = "Estado não informada";
        }
        grvLicencasVisao.DataSource = p.Licencas;
        grvLicencasVisao.DataBind();
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

    private void ExibirRequerimentoPesquisa(RequerimentoPesquisa rp)
    {
        this.TestarPermissoes();

        mvwProcessos.ActiveViewIndex = 1;
        lblDataEntrada.Text = rp.DataRequerimento.EmptyToMinValue();

        grvRequerimentosPesquisa.DataSource = rp.Exigencias;
        grvRequerimentosPesquisa.DataBind();
    }

    private void ExibirAlvaraPesquisa(AlvaraPesquisa ap)
    {
        mvwProcessos.ActiveViewIndex = 2;
        lblDataDePublicacao.Text = ap.DataPublicacao.EmptyToMinValue();

        if (ap.DataEntregaRelatorio != null && ap.DataEntregaRelatorio != SqlDate.MinValue)
            lblDataEntregaRelatorioPesquisa.Text = ap.DataEntregaRelatorio.EmptyToMinValue();
        else
            lblDataEntregaRelatorioPesquisa.Text = "Data não Informada.";

        if (ap.DataAprovacaoRelatorio != null && ap.DataAprovacaoRelatorio != SqlDate.MinValue)
            lblDataAprovacaoRelatorioPesquisa.Text = ap.DataAprovacaoRelatorio.EmptyToMinValue();
        else
            lblDataAprovacaoRelatorioPesquisa.Text = "Data não Informada.";


        //testar revovação da taxa anual por hectare
        if (ap.DataEntregaRelatorio <= SqlDate.MinValue)
        {
            btnRenovarAlvara.Visible = true;
            lblTaxaAnualHectare.Text = "";
        }
        else if (ap.DataPublicacao.AddYears(1) > ap.DataEntregaRelatorio)
        {
            btnRenovarAlvara.Visible = false;
            lblTaxaAnualHectare.Text = "Não é necessario renovar esta taxa, pois o relatório foi entregue em menos de  1 ano a partir da data de publicação.";
        }
        else
        {
            btnRenovarAlvara.Visible = true;
            lblTaxaAnualHectare.Text = "";
        }

        this.TestarPermissoes();

        grvAlvaraPesquisa.DataSource = ap.Exigencias;
        grvAlvaraPesquisa.DataBind();


        if (ap.Vencimento != null)
        {
            lblValidadeAlvara.Text = ap.Vencimento.Data.EmptyToMinValue() + " = " + ap.AnosValidade + " anos após a publicação";
            lblDataUltimaNotificacaoAlvaraPesquisaValidade.Text = ap.Vencimento.GetDataProximaNotificacao;
            lblDataLimiteRenovacaoAlvara.Text = ap.Vencimento.Data.AddDays(-60).EmptyToMinValue() + " = 60 dias antes do vencimento.";
        }
        else
        {
            lblValidadeAlvara.Text = "Data não Informada.";
            lblDataUltimaNotificacaoAlvaraPesquisaValidade.Text = "Data não Informada.";
            lblDataLimiteRenovacaoAlvara.Text = "Data não Informada.";
        }

        if (ap.NotificacaoPesquisaDNPM != null)
        {
            lblNotificacaoPesquisaAlvara.Text = ap.NotificacaoPesquisaDNPM.Data.EmptyToMinValue() + " = 60 dias após publicação";
            lblDataUltimaNotificacaoAlvaraPesquisaNotificacaoPesquisa.Text = ap.NotificacaoPesquisaDNPM.GetDataProximaNotificacao;
        }
        else
        {
            lblNotificacaoPesquisaAlvara.Text = "Data não Informada.";
            lblDataUltimaNotificacaoAlvaraPesquisaNotificacaoPesquisa.Text = "Data não Informada.";
        }

        if (ap.GetUltimaTaxaAnualHectare != null && ap.GetUltimaTaxaAnualHectare.Data != SqlDate.MinValue)
        {
            lblTaxaAnualHectareAlvara.Text = ap.GetUltimaTaxaAnualHectare.Data.EmptyToMinValue();
            lblDataUltimaNotificacaoAlvaraPesquisaTaxaAnualHectare.Text = ap.GetUltimaTaxaAnualHectare.GetDataProximaNotificacao;
        }
        else
        {
            lblTaxaAnualHectareAlvara.Text = "Data não Informada.";
            lblDataUltimaNotificacaoAlvaraPesquisaTaxaAnualHectare.Text = "Data não Informada.";
        }

        if (ap.RequerimentoLavra != null)
        {
            lblRequerimentoLavraAlvara.Text = ap.RequerimentoLavra.Data.EmptyToMinValue() + " = 1 ano após aprovação do relatório de pesquisa";
            lblDataUltimaNotificacaoAlvaraPesquisaRequerimentoLavra.Text = ap.RequerimentoLavra.GetDataProximaNotificacao;
        }
        else
        {
            lblRequerimentoLavraAlvara.Text = "Somente é criada após a aprovação do Relatório de Pesquisa.";
            lblDataUltimaNotificacaoAlvaraPesquisaRequerimentoLavra.Text = "Data não Informada.";
        }

        if (ap.RequerimentoLPTotal != null && ap.RequerimentoLPTotal.Data != SqlDate.MinValue)
        {
            lblRequerimentoLPTotalAlvara.Text = "Relatório aprovado em " + ap.DataAprovacaoRelatorio.EmptyToMinValue() + " - Já é possível requerer a LP Poligonal  ";
            lblDataUltimaNotificacaoAlvaraPesquisaRequerimentoLPTotal.Text = ap.RequerimentoLPTotal.GetDataProximaNotificacao;
        }
        else
        {
            lblRequerimentoLPTotalAlvara.Text = "Somente é criada após a aprovação do Relatório de Pesquisa.";
            lblDataUltimaNotificacaoAlvaraPesquisaRequerimentoLPTotal.Text = "Data não informada";
        }

        if (ap.DIPEM != null && ap.GetUltimoDIPEM != null)
        {
            lblVencimentoDIPEM.Text = ap.GetUltimoDIPEM.Data.EmptyToMinValue();
            lblDataUltimaNotificacaoDIPEM.Text = ap.GetUltimoDIPEM.GetDataProximaNotificacao;
        }
        else
        {
            lblVencimentoDIPEM.Text = "Não Informado.";
            lblDataUltimaNotificacaoDIPEM.Text = "Data não informada";
        }

        if (ap.LimiteRenuncia != null && ap.LimiteRenuncia != null)
        {
            lblDataLimiteRenuncia.Text = ap.LimiteRenuncia.Data.EmptyToMinValue();
            lblproximaNotificacaoLimiteRenuncia.Text = ap.LimiteRenuncia.GetDataProximaNotificacao;
        }
        else
        {
            lblDataLimiteRenuncia.Text = "Não Informado.";
            lblproximaNotificacaoLimiteRenuncia.Text = "Data não informada";
        }
    }

    private void ExibirRequerimentoLavra(RequerimentoLavra rl)
    {
        mvwProcessos.ActiveViewIndex = 3;
        this.TestarPermissoes();
        lblDataAberturaLavra.Text = rl.Data.EmptyToMinValue();

        if (rl.Exigencias != null)
        {
            grvDataRequerimentoLavraExigencias.DataSource = rl.Exigencias;
            grvDataRequerimentoLavraExigencias.DataBind();
        }
    }

    private void ExibirConcessaoLavra(ConcessaoLavra cl)
    {
        mvwProcessos.ActiveViewIndex = 4;
        this.TestarPermissoes();
        if (cl.Data != null && cl.Data != SqlDate.MinValue)
            lblDataPublicacaoConcessaoLavra.Text = cl.Data.EmptyToMinValue();
        else
            lblDataPublicacaoConcessaoLavra.Text = "";

        lblNumeroPortariaLavra.Text = cl.NumeroPortariaLavra;
        lblReavaliacaoReserva.Text = cl.DataRelatorioReavaliacaoReserva.EmptyToMinValue();

        if (cl.Exigencias != null)
        {
            grvConcessaoLavraExigencias.DataSource = cl.Exigencias;
            grvConcessaoLavraExigencias.DataBind();
        }
        if (cl.RequerimentoImissaoPosse != null)
        {
            lblRequerimentoImissaoConcessaoLavra.Text = cl.RequerimentoImissaoPosse.Data.EmptyToMinValue() + " = 90 dias após a publicação da portaria de lavra.";
            lblDataUltimaNotificacaoConcessaoLavra.Text = cl.RequerimentoImissaoPosse.GetDataProximaNotificacao;
        }
        else
        {
            lblRequerimentoImissaoConcessaoLavra.Text = "Requerimento não efetuado";
            lblDataUltimaNotificacaoConcessaoLavra.Text = "Data não informada";
        }
    }

    private void ExibirLicenciamento(Licenciamento l)
    {

        mvwProcessos.ActiveViewIndex = 5;
        this.TestarPermissoes();
        lblNumeroLicenciamento.Text = l.Numero;

        if (l.DataPublicacao != SqlDate.MinValue)
            lblDataPublicacaoLicenciamento.Text = l.DataPublicacao.EmptyToMinValue();
        else
            lblDataPublicacaoLicenciamento.Text = "Data não informada";


        if (l.DataAbertura != SqlDate.MinValue)
            lblDataAberturaLicenciamento.Text = l.DataAbertura.EmptyToMinValue();
        else
            lblDataAberturaLicenciamento.Text = "Data não informada";

        if (l.Exigencias != null)
        {
            grvExigenciasLicenciamento.DataSource = l.Exigencias;
            grvExigenciasLicenciamento.DataBind();
        }
        if (l.GetUltimoVencimento != null)
        {
            lblValidadeLicenciamento.Text = l.GetUltimoVencimento.Data.EmptyToMinValue();
            lblDatalimiteVencimentoLicenca.Text = l.GetUltimoVencimento.Data.EmptyToMinValue();
            // lblDatalimiteVencimentoLicenca.Text = l.GetUltimoVencimento.Data.AddDays(-60).EmptyToMinValue() + " = 60 dias antes do vencimento";
        }
        else
        {
            lblValidadeLicenciamento.Text = "Licenciamento não efetuado.";
            lblDatalimiteVencimentoLicenca.Text = "Licenciamento não efetuado.";
        }

        if (l.GetUltimoVencimento != null)
        {
            lblDataUltimaNotificacaoLicenciamentoValidade.Text = l.GetUltimoVencimento.GetDataProximaNotificacao;
        }

        if (l.EntregaLicencaOuProtocolo != null)
        {
            lblDataLimiteEntregaLicencaProtocoloLicenciamento.Text = l.EntregaLicencaOuProtocolo.Data.EmptyToMinValue() + " = 60 dias após a publicação";
            lblDataUltimaNotificacaoLicenciamentoEntregaLicencaProtocolo.Text = l.EntregaLicencaOuProtocolo.GetDataProximaNotificacao;

        }
        else
        {
            lblDataLimiteEntregaLicencaProtocoloLicenciamento.Text = "Data não informada";
        }

        if (l.Licencas != null)
        {
            grvLicenciamentoLicencaAssociadas.DataSource = l.Licencas;
            grvLicenciamentoLicencaAssociadas.DataBind();
        }
    }

    private void ExibirExtracao(Extracao e)
    {

        mvwProcessos.ActiveViewIndex = 6;
        this.TestarPermissoes();
        if (e.DataPublicacao != SqlDate.MinValue)
            lblDataPublicacaoExtracao.Text = e.DataPublicacao.EmptyToMinValue();
        else
            lblDataPublicacaoExtracao.Text = "Não Informada.";

        lblNumerolicencaExtracao.Text = e.NumeroLicenca;

        if (e.ValidadeLicenca != SqlDate.MinValue)
            lblValidadeLicencaExtracao.Text = e.ValidadeLicenca.EmptyToMinValue();
        else
            lblValidadeLicencaExtracao.Text = "Não Informada.";

        if (e.DataAbertura != SqlDate.MinValue)
            lblDataAberturaExtracao.Text = e.DataAbertura.EmptyToMinValue();
        else
            lblDataAberturaExtracao.Text = "Não Informada.";

        lblNumeroExtracao.Text = e.NumeroExtracao;

        grvExigenciasExtracao.DataSource = e.Exigencias;
        grvExigenciasExtracao.DataBind();

        if (e.Vencimentos != null && e.Vencimentos.Count > 0)
        {
            lblValidadeExtracao.Text = e.GetUltimoVencimento.GetDataVencimento;
            lblDataUltimaNotificacaoExtracaoValidade.Text = e.GetUltimoVencimento.GetDataProximaNotificacao;
        }
        else
        {
            lblValidadeExtracao.Text = "Extração não efetuada";
            lblDataUltimaNotificacaoExtracaoValidade.Text = "Não Informada";
        }
    }

    private void ExibirRAL(RAL r)
    {
        mvwProcessos.ActiveViewIndex = 7;

        this.TestarPermissoes();

        if (r.GetMaiorVencimento != null)
        {
            lblDataVencimentoRAL.Text = r.GetMaiorVencimento.Data.EmptyToMinValue();
            if (r.GetMaiorVencimento.Notificacoes != null)
            {
                grvNotificacoesRAL.DataSource = r.GetMaiorVencimento.Notificacoes;
                grvNotificacoesRAL.DataBind();
            }
        }
        else
        {
            lblDataVencimentoRAL.Text = "Data não informada";
        }
    }

    private void ExibirGuiaUtilizacao(GuiaUtilizacao g)
    {
        mvwProcessos.ActiveViewIndex = 8;
        this.TestarPermissoes();


        if (g.DataRequerimento != SqlDate.MinValue)
            lblDataRequerimentoGuiaUtilizacao.Text = g.DataRequerimento.EmptyToMinValue();
        else
            lblDataRequerimentoGuiaUtilizacao.Text = "Data não Informada.";

        if (g.DataLimiteRequerimento != SqlDate.MinValue)
            lblDataLimiteGuiaUtilizacao.Text = g.DataLimiteRequerimento.EmptyToMinValue() + " = 60 dias antes do Vencimento.";
        else
            lblDataLimiteGuiaUtilizacao.Text = "Data não Informada.";


        if (g.DataEmissao != SqlDate.MinValue)
            lblDataEmissaoGuiaUtilizacao.Text = g.DataEmissao.EmptyToMinValue();
        else
            lblDataEmissaoGuiaUtilizacao.Text = "Data não Informada.";

        grvExigenciasGuiaUtilizacao.DataSource = g.Exigencias;
        grvExigenciasGuiaUtilizacao.DataBind();


        if (g.GetUltimoVencimento != null && g.GetUltimoVencimento.Data != SqlDate.MinValue)
            lblDataValidadeGuiaUtilizacao.Text = g.GetUltimoVencimento.Data.EmptyToMinValue();
        else
            lblDataValidadeGuiaUtilizacao.Text = "Data não informada.";

        if (g.GetUltimoVencimento != null)
            lblDataUltimaNotificacaoGuiaUtilizacao.Text = g.GetUltimoVencimento.GetDataProximaNotificacao;
        else
            lblDataUltimaNotificacaoGuiaUtilizacao.Text = "Data não informada.";
    }

    private void CarregarPopUpSelecionarLicenca()
    {
        this.CarregarEmpresas();
        this.ddlTipoOrgaoLicenca_SelectedIndexChanged(null, null);
    }

    private void CarregarArvoreSelecionarLicenca()
    {
        tvwLicencas.Nodes.Clear();

        if (ddlOrgaoLicenca.SelectedIndex <= 0)
            return;

        OrgaoAmbiental o = OrgaoAmbiental.ConsultarPorId(ddlOrgaoLicenca.SelectedValue.ToInt32());
        Empresa emp = Empresa.ConsultarPorId(ddlEmpresaLicenca.SelectedValue.ToInt32());

        if (o == null || emp == null)
        {
            msg.CriarMensagem("Selecione um Orgão Ambiental e uma Empresa", "Alerta", MsgIcons.Alerta);
            return;
        }
        lblResultLicencas.Text = "Nenum processo foi encontardo neste Orgão, para esta Empresa.";
        IList<Processo> procs = Processo.ConsultarPorEmpresaEOrgao(emp, o);

        if (procs != null && procs.Count > 0)
        {
            lblResultLicencas.Text = procs.Count + " processo(s) encontardo(s) neste Orgão, para esta Empresa.";

            foreach (Processo p in procs)
            {
                if (p.Licencas != null && p.Licencas.Count > 0)
                {
                    TreeNode noPai = new TreeNode("Processo: " + p.Numero, "p_" + p.Id);
                    foreach (Licenca l in p.Licencas)
                    {
                        TreeNode noLic = new TreeNode((l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + " " + l.Numero, "l_" + l.Id);
                        noPai.ChildNodes.Add(noLic);
                    }
                    tvwLicencas.Nodes.Add(noPai);
                }
            }
        }

        tvwLicencas.ExpandAll();

    }

    private void CarregarEmpresasProcessoDNPM(int idCliente)
    {
        ddlEmpresaDNPM.Items.Clear();

        GrupoEconomico c = GrupoEconomico.ConsultarPorId(idCliente);

        if (c != null && c.Empresas != null)
        {
            c.Empresas = c.Empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in c.Empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresaDNPM.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresaDNPM.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }

        ddlEmpresaDNPM.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarEmpresasQueOUsuarioEdita(DropDownList dropEmpresa)
    {
        dropEmpresa.Items.Clear();

        IList<Empresa> empresas = null;

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
            {
                empresas = ddlClientes.SelectedValue.ToInt32() > 0 ? GrupoEconomico.ConsultarPorId(ddlClientes.SelectedValue.ToInt32()).Empresas : Empresa.ConsultarTodos();
            }
        }

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            empresas = this.EmpresasPermissaoEdicaoModuloNPM != null ? this.EmpresasPermissaoEdicaoModuloNPM : new List<Empresa>();


        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (this.ProcessosPermissaoEdicaoModuloDNPM != null)
            {
                empresas = ddlClientes.SelectedValue.ToInt32() > 0 ? GrupoEconomico.ConsultarPorId(ddlClientes.SelectedValue.ToInt32()).Empresas : Empresa.ConsultarTodos();
            }
        }

        if (empresas != null && empresas.Count > 0)
            empresas = empresas.OrderBy(x => x.Nome).ToList();
        foreach (Empresa emp in empresas)
        {
            if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
            else
                dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
        }

        dropEmpresa.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarEstadosProcessoDNPM()
    {
        if (ddlEstadoProcessoDNPM.Items.Count <= 0)
        {
            ddlEstadoProcessoDNPM.DataTextField = "Nome";
            ddlEstadoProcessoDNPM.DataValueField = "Id";
            ddlEstadoProcessoDNPM.DataSource = Estado.ConsultarTodos();
            ddlEstadoProcessoDNPM.DataBind();
            ddlEstadoProcessoDNPM.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void CarregarConsultoriasProcessoDNPM()
    {
        if (ddlConsultoriaDNPM.Items.Count <= 0)
        {
            ddlConsultoriaDNPM.DataTextField = "Nome";
            ddlConsultoriaDNPM.DataValueField = "Id";
            ddlConsultoriaDNPM.DataSource = Consultora.ConsultarTodos();
            ddlConsultoriaDNPM.DataBind();
            ddlConsultoriaDNPM.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void CarregarCidadeProcessoDNPM(int id)
    {
        ddlCidadeProcessoDNPM.DataTextField = "Nome";
        ddlCidadeProcessoDNPM.DataValueField = "Id";
        ddlCidadeProcessoDNPM.DataSource = id > 0 ? Estado.ConsultarPorId(id).Cidades : new List<Cidade>();
        ddlCidadeProcessoDNPM.DataBind();
        ddlCidadeProcessoDNPM.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarLicencasDoProcessoDNPM(IList<Licenca> ls)
    {
        grvLicencasDNPM.DataSource = ls;
        grvLicencasDNPM.DataBind();
    }

    private void CarregarProcessoDNPM(int id)
    {
        if (id > 0)
        {
            ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(id);
            tbxNumeroProcessoDNPM.Text = p.Numero;
            tbxDataAberturaDNPM.Text = p.DataAbertura.EmptyToMinValue();
            ddlConsultoriaDNPM.SelectedValue = p.Consultora.Id.ToString();
            tbxIdentificacaoAreaDNPM.Text = p.Identificacao;
            tbxSubstancia.Text = p.Substancia;
            tbxTamanhoAreaDNPM.Text = p.TamanhoArea;
            tbxEnderecoDNPM.Text = p.Endereco;
            ddlEstadoProcessoDNPM.SelectedValue = p.Cidade != null ? p.Cidade.Estado.Id.ToString() : null;
            this.CarregarCidadeProcessoDNPM(ddlEstadoProcessoDNPM.SelectedValue.ToInt32());
            this.CarregarSessaoLicencas(p.Licencas);
            this.CarregarLicencasDoProcessoDNPM(p.Licencas);
        }
        else
        {
            this.NovoProcessoDNPM();
            msg.CriarMensagem("processo inválido", "Alerta", MsgIcons.Alerta);
        }
    }

    private void CarregarSessaoLicencas(IList<Licenca> lista)
    {
        if (this.IdLicencasSelecionadasDNPM == null)
            this.IdLicencasSelecionadasDNPM = new List<int>();

        foreach (Licenca item in lista)
        {
            this.IdLicencasSelecionadasDNPM.Add(item.Id);
        }
    }

    private void NovoProcessoDNPM()
    {
        WebUtil.LimparCampos(upCadastrarProcessosDNPM.Controls);
    }

    private void ExcluirProcessoDNPM(int id)
    {
        ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(id);
        p.Excluir();
    }

    /* Função que sempre retorna true para a validação do certificado.
  Com isso, mesmo se o certificado estiver com problemas, seu método 
  * continuará e irá acessr o conteúdo HTTP*/
    private static bool CustomValidation(Object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
    {
        return true;
    }

    private void SalvarProcessoDNPM()
    {
        string obsMsg = "";

        if (ddlEmpresaDNPM.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione uma Empresa.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!tbxDataAberturaDNPM.Text.IsDate())
        {
            msg.CriarMensagem("O Campo data de abertura deve ser informado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!tbxNumeroProcessoDNPM.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("O Campo número deve ser informado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (GrupoEconomico.ConsultarPorId(ddlClientes.SelectedValue.ToInt32()).VerificarSeExcedeuLimiteDeProcessosContratados())
        {
            msg.CriarMensagem("Você já atingiu o limite de Processos (Ambientais + Minerários) cadastrados.", "Atenção", MsgIcons.Informacao);
            return;
        }

        ProcessoDNPM processoDNPM = ProcessoDNPM.ConsultarPorId(hfIdProcessoDNPM.Value.ToInt32());
        if (processoDNPM == null)
        {
            if (ProcessoDNPM.VerificarSeExisteProcessoComMesmoNumero(tbxNumeroProcessoDNPM.Text))
            {
                msg.CriarMensagem("Já existe um outro processo com este mesmo número.", "Alerta", MsgIcons.Alerta);
                return;
            }
            processoDNPM = new ProcessoDNPM();
        }
        else
        {
            if (processoDNPM.Empresa.Id.ToString() != ddlEmpresaDNPM.SelectedValue)
                obsMsg = "<br/><br/>Atenção: Você alterou a Empresa deste Processo. Caso este Processo ou seus regimes tenham alguma notificação, estas forma mantidas com seus destinatários.";
        }

        processoDNPM.Empresa = Empresa.ConsultarPorId(ddlEmpresaDNPM.SelectedValue.ToInt32());

        if (!TestarRegularidadeCNPJdoProcesso(tbxNumeroProcessoDNPM.Text, processoDNPM.Empresa.ObterCnpjCpf(processoDNPM.Empresa.DadosPessoa)))
            return;

        processoDNPM.Numero = tbxNumeroProcessoDNPM.Text;
        processoDNPM.RegimeDeCriacao = ddlRegimeDNPM.SelectedValue;
        processoDNPM.DataAbertura = tbxDataAberturaDNPM.Text.ToSqlDateTime();
        processoDNPM.Consultora = ddlConsultoriaDNPM.SelectedValue.ToInt32() > 0 ? Consultora.ConsultarPorId(ddlConsultoriaDNPM.SelectedValue.ToInt32()) : null;
        processoDNPM.TamanhoArea = tbxTamanhoAreaDNPM.Text;
        processoDNPM.Identificacao = tbxIdentificacaoAreaDNPM.Text;
        processoDNPM.Substancia = tbxSubstancia.Text;

        processoDNPM.Endereco = tbxEnderecoDNPM.Text;
        processoDNPM.Cidade = ddlCidadeProcessoDNPM.Items.Count > 0 ? Cidade.ConsultarPorId(ddlCidadeProcessoDNPM.SelectedValue.ToInt32()) : null;

        processoDNPM.Licencas = this.ReconsultarLicencas();
        
        processoDNPM.Substancias = this.RecarregarSubstancias();
        processoDNPM.Observacoes = tbxObservacoesProcessoDNPM.Text;

        //processoDNPM = processoDNPM.Salvar();

        //processoDNPM.Arquivos = this.RecarregarArquivos(this.SessaoUploadsProcessoDNPM);

        //processoDNPM.Arquivos = this.RecarregarArquivosProcessoDNPM(this.SessaoUploadsProcessoDNPM, processoDNPM);

        processoDNPM = processoDNPM.Salvar();


        if (processoDNPM.Id != null && processoDNPM.Id > 0)
        {
            btnAbrirContratos.Visible = this.UsuarioLogado.PossuiPermissaoDeEditarModuloContratos;
            ibtnAddLicencaProcesso.Visible = this.UsuarioLogado.PossuiPermissaoDeEditarModuloMeioAmbiente;
        }
        string edicao = hfIdProcessoDNPM.Value;
        hfIdProcessoDNPM.Value = processoDNPM.Id.ToString();
        msg.CriarMensagem("Processo ANM salvo com sucesso" + obsMsg, "Sucesso", MsgIcons.Sucesso);

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            processoDNPM.UsuariosEdicao = new List<Usuario>();

            if (processoDNPM.UsuariosVisualizacao != null && processoDNPM.UsuariosVisualizacao.Count > 0)
            {
                if (!processoDNPM.UsuariosVisualizacao.Contains(this.UsuarioLogado))
                    processoDNPM.UsuariosVisualizacao.Add(this.UsuarioLogado);
            }

            processoDNPM.UsuariosEdicao.Add(this.UsuarioLogado.ConsultarPorId());

            processoDNPM = processoDNPM.Salvar();

            if (this.ProcessosPermissaoModuloDNPM == null)
                this.ProcessosPermissaoModuloDNPM = new List<ProcessoDNPM>();

            if (!this.ProcessosPermissaoModuloDNPM.Contains(processoDNPM))
                this.ProcessosPermissaoModuloDNPM.Add(processoDNPM);


            if (this.ProcessosPermissaoEdicaoModuloDNPM == null)
                this.ProcessosPermissaoEdicaoModuloDNPM = new List<ProcessoDNPM>();

            if (!this.ProcessosPermissaoEdicaoModuloDNPM.Contains(processoDNPM))
                this.ProcessosPermissaoEdicaoModuloDNPM.Add(processoDNPM);

        }

        btnPopUpCadastroProcessoDNPM_ModalPopupExtender.Hide();

        if (edicao.ToInt32() <= 0)
        {
            if (ddlRegimeDNPM.SelectedValue == "Autorização de pesquisa")
            {
                this.NovoRequerimentoPesquisa();
                tbxDataEntradaRequerimentoPesquisa.Text = processoDNPM.DataAbertura.EmptyToMinValue();
                btnPopUpRequerimentoPesquisa_popupextender.Show();
            }
            else if (ddlRegimeDNPM.SelectedValue == "Extração")
            {
                this.NovaExtracao();
                tbxDataAberturaExtracao.Text = processoDNPM.DataAbertura.EmptyToMinValue();
                lblPopUpExtracao_ModalPopupExtender.Show();
            }
            else if (ddlRegimeDNPM.SelectedValue == "Licenciamento")
            {
                this.NovoLicenciamento();
                tbxDataAberturaLicenciamento.Text = processoDNPM.DataAbertura.EmptyToMinValue();
                lblPopUpLicenciamento_ModalPopupExtender.Show();
            }
        }
        transacao.Recarregar(ref msg);
        this.CarregarArvore();
    }

    private bool TestarRegularidadeCNPJdoProcesso(string numero, string cnpjOuCPF)
    {
        try
        {
            return true;

            ///* Adicionando Handler pro evento ServerCertificateValidationCallback 
            // * que chama a função CustomValidation*/
            //ServicePointManager.ServerCertificateValidationCallback += new
            //System.Net.Security.RemoteCertificateValidationCallback(CustomValidation);

            //string url = "https://sistemas.dnpm.gov.br/SCM/Extra/site/admin/dadosProcesso.aspx" + this.GetParametrosDNPM(numero);

            ////baixando codigo fonte do site e decodificando
            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            //myRequest.Method = "GET";
            //WebResponse myResponse = myRequest.GetResponse();
            //StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            //string text = sr.ReadToEnd();

            //sr.Close();
            //myResponse.Close();

            //if (text.Contains(cnpjOuCPF.ToFormatedCNPJ()) || text.Contains(cnpjOuCPF.ToFormatedCPF()))
            //{
            //    return true;
            //}
            //else
            //{
            //    msg.CriarMensagem("Este Processo não corresponde a um Processo válido no site do DNPM ou não pertence a este titular.", "Alerta", MsgIcons.Alerta);
            //    return false;
            //}

            
        }
        catch (Exception)
        {
            msg.CriarMensagem("Erro ao validar processo junto ao Site do ANM, tente novamente mais tarde.", "Alerta", MsgIcons.Alerta);
            return false;
        }
    }

    private void SalvarRequerimentoPesquisa()
    {
        RequerimentoPesquisa rp = RequerimentoPesquisa.ConsultarPorId(hfRequerimentoPesquisa.Value.ToInt32());
        if (rp == null)
        {
            rp = new RequerimentoPesquisa();
            rp.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        }

        rp.Exigencias = this.RecarregarExigencias();
        rp.DataRequerimento = tbxDataEntradaRequerimentoPesquisa.Text.ToSqlDateTime();
        //rp.Arquivos = this.RecarregarArquivos(this.SessaoUploadsRequerimentoPesquisa);
        rp = rp.Salvar();

        msg.CriarMensagem("Requerimento salvo com Sucesso!", "Sucesso");
        btnPopUpRequerimentoPesquisa_popupextender.Hide();

        transacao.Recarregar(ref msg);
        this.CarregarArvore();
        this.ExibirVisoes();
    }

    private IList<Exigencia> RecarregarExigencias()
    {
        IList<Exigencia> lista = new List<Exigencia>();
        if (this.ExigenciasSelecionadas != null)
        {
            foreach (Exigencia item in this.ExigenciasSelecionadas)
            {
                lista.Add(Exigencia.ConsultarPorId(item.Id));
            }
        }
        return lista;
    }

    private IList<Substancia> RecarregarSubstancias()
    {
        IList<Substancia> lista = new List<Substancia>();
        if (this.SubstanciaSelecionadas != null)
        {
            foreach (int item in this.SubstanciaSelecionadas)
            {
                lista.Add(Substancia.ConsultarPorId(item));
            }
        }
        return lista;
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

    private void SalvarRequerimentoLavra()
    {
        RequerimentoLavra rp = RequerimentoLavra.ConsultarPorId(hfIdRequerimentoLavra.Value.ToInt32());
        if (rp == null)
        {
            rp = new RequerimentoLavra();
            rp.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        }

        rp.Exigencias = this.RecarregarExigencias();
        rp.Data = (tbxDataEntradaRequerimentoLavra.Text).ToSqlDateTime();
       // rp.Arquivos = this.RecarregarArquivos(this.SessaoUploadsRequerimentoLavra);
        rp = rp.Salvar();

        lblPopUpRequerimentoLavra_ModalPopupExtender.Hide();
        msg.CriarMensagem("Requerimento salvo com Sucesso!", "Sucesso");
        lblPopUpRequerimentoLavra_ModalPopupExtender.Hide();

        transacao.Recarregar(ref msg);
        this.CarregarArvore();
    }

    private void SalvarConcessaoLavra()
    {
        ConcessaoLavra rp = ConcessaoLavra.ConsultarPorId(hfIdConcessaoLavra.Value.ToInt32());
        if (rp == null)
        {
            rp = new ConcessaoLavra();
            rp.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        }

        string obsMensagem = "</br></br>OBS: Você ainda não inseriu as notificações para esta Concessão.";
        if (this.NotificacoesSelecionadas != null && this.NotificacoesSelecionadas.Count > 0)
        {
            obsMensagem = "";
        }

        rp.Exigencias = this.RecarregarExigencias();
        rp.DataRelatorioReavaliacaoReserva = tbxDataApresentacaoRelatorio.Text.ToSqlDateTime();
        rp.NumeroPortariaLavra = tbxNumeroPortariaLavra.Text;

        bool criouRAL = false;
        if (tbxPublicacaoConcessaoLavra.Text.IsDate())
        {
            if (rp.RequerimentoImissaoPosse == null)
                rp.RequerimentoImissaoPosse = new Vencimento();

            rp.Data = tbxPublicacaoConcessaoLavra.Text.ToSqlDateTime();
            rp.RequerimentoImissaoPosse.Data = rp.Data.AddDays(ddlRequerimentoImissaoPosse.SelectedValue.ToInt32());//90 dias apos a data de publicação 
            rp.RequerimentoImissaoPosse.Notificacoes = this.RecarregarNotificacoes(this.NotificacoesSelecionadas);
            rp.RequerimentoImissaoPosse = rp.RequerimentoImissaoPosse.Salvar();

            criouRAL = this.CriarRAL(("15/03/" + rp.Data.AddYears(1).Year).ToSqlDateTime(), rp.RequerimentoImissaoPosse.Notificacoes);
        }

        //rp = rp.Salvar();

        //rp.Arquivos = this.RecarregarArquivos(this.SessaoUploadsConcessaoLavra);

        rp = rp.Salvar();

        hfIdConcessaoLavra.Value = rp.Id.ToString();

        if (criouRAL)
            msg.CriarMensagem("Concessão salva com Sucesso!</br></br>OBS.: Um Ral com as mesmas notificações foi criado para o mesmo Processo desta Concessão." + obsMensagem, "Sucesso");
        else
            msg.CriarMensagem("Concessão salva com Sucesso!" + obsMensagem, "Sucesso");

        if (obsMensagem == "")
            lblPopUpConcessaoLavra_ModalPopupExtender.Hide();

        transacao.Recarregar(ref msg);
        this.CarregarArvore();

        if (criouRAL)
        {
            this.ItensRenovacao = new List<ItemRenovacao>();

            RAL ral = RAL.ConsultarPorId(hfIdRalVencimentosPeriodicos.Value.ToInt32());
            if (ral != null && ral.GetUltimoVencimento != null && ral.GetUltimoVencimento.Data <= DateTime.Now)
            {
                ItemRenovacao item = new ItemRenovacao();
                item.idItem = ral.Id;
                item.tipoItem = "RAL";
                item.diasRenovacao = ObterDiasEntreAsRenovacoes(ral.Vencimentos);
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

    private void SalvarLicenciamento()
    {
        if (!tbxDataAberturaLicenciamento.Text.IsDate())
        {
            msg.CriarMensagem("É necessario cadastrar uma data de Abertura.", "Aleta", MsgIcons.Alerta);
            return;
        }
        if (!tbxValidadeLicenciamento.Text.IsDate())
        {
            msg.CriarMensagem("É necessario cadastrar uma data de Validade.", "Aleta", MsgIcons.Alerta);
            return;
        }

        Licenciamento licenciamento = Licenciamento.ConsultarPorId(hfIdLicenciamento.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(licenciamento);
        if (licenciamento == null)
        {
            licenciamento = new Licenciamento();
            licenciamento.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
            licenciamento.Vencimentos = new List<Vencimento>();
            licenciamento.Vencimentos.Add(new Vencimento());

            if (tbxPublicacaoLicenciamento.Text.IsDate())
                licenciamento.EntregaLicencaOuProtocolo = new Vencimento();
        }

        string obsMensagem = "</br></br>OBS: Você ainda não inseriu todas as notificações para este Alvará.";
        if (this.SessaoLicenciamento != null && this.SessaoLicenciamento.GetUltimoVencimento != null && this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes != null && this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes.Count > 0 &&
            this.SessaoLicenciamento.EntregaLicencaOuProtocolo != null && this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes != null && this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes.Count > 0)
        {
            obsMensagem = "";
        }

        licenciamento.Numero = tbxNumeroLicenciamento.Text;
        licenciamento.DataAbertura = tbxDataAberturaLicenciamento.Text.ToSqlDateTime();
        licenciamento.DataPublicacao = tbxPublicacaoLicenciamento.Text.ToSqlDateTime();
        licenciamento.PossuePAE = ckbPossuiPAE.Checked;

        licenciamento.Licencas = this.ReconsultarLicencas();
        licenciamento.Exigencias = this.RecarregarExigencias();

        // VENCIMENTO

        Vencimento ultimoVencimento = licenciamento.GetUltimoVencimento;

        if (ultimoVencimento == null)
            ultimoVencimento = new Vencimento();


        ultimoVencimento.Data = tbxValidadeLicenciamento.Text.ToSqlDateTime();
        ultimoVencimento.Status = Status.ConsultarPorId(ddlEstatusVencimentoLicenciamento.SelectedValue.ToInt32());

        if (this.SessaoLicenciamento != null && this.SessaoLicenciamento.Vencimentos != null)
        {
            if (this.SessaoLicenciamento.GetUltimoVencimento != null && this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes != null)
                ultimoVencimento.Notificacoes = this.RecarregarNotificacoes(this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes);
        }

        if (licenciamento.Vencimentos == null || licenciamento.Vencimentos.Count == 0)
        {
            licenciamento.Vencimentos = new List<Vencimento>();

            licenciamento.Vencimentos.Add(ultimoVencimento.Salvar());
        }
        else
            licenciamento.Vencimentos[licenciamento.Vencimentos.Count - 1] = ultimoVencimento.Salvar();


        //ENTRGA LICENCA OU PROTOCOLO
        if (tbxPublicacaoLicenciamento.Text.IsDate())
        {
            if (licenciamento.EntregaLicencaOuProtocolo == null)
                licenciamento.EntregaLicencaOuProtocolo = new Vencimento();

            licenciamento.EntregaLicencaOuProtocolo.Data = licenciamento.DataPublicacao.AddDays(ddlEntregaLicencaOuProtocolo.SelectedValue.ToInt32());
            if (this.SessaoLicenciamento != null && this.SessaoLicenciamento.EntregaLicencaOuProtocolo != null)
                licenciamento.EntregaLicencaOuProtocolo.Notificacoes = this.RecarregarNotificacoes(this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes);
            licenciamento.EntregaLicencaOuProtocolo = licenciamento.EntregaLicencaOuProtocolo.Salvar();
        }

        //licenciamento.Arquivos = this.RecarregarArquivos(this.SessaoUploadsLicenciamento);
        licenciamento = licenciamento.Salvar();

        hfIdLicenciamento.Value = licenciamento.Id.ToString();

        bool criouRAL = false;
        if (licenciamento.DataPublicacao != null && licenciamento.DataPublicacao > SqlDate.MinValue)
        {
            if (ckbPossuiPAE.Checked)
                criouRAL = this.CriarRAL(("15/03/" + licenciamento.DataPublicacao.AddYears(1).Year).ToSqlDateTime(), licenciamento.GetUltimoVencimento.Notificacoes);
            else
                criouRAL = this.CriarRAL(("31/03/" + licenciamento.DataPublicacao.AddYears(1).Year).ToSqlDateTime(), licenciamento.GetUltimoVencimento.Notificacoes);

            if (criouRAL)
                msg.CriarMensagem("Licenciamento salvo com Sucesso!</br></br>OBS.: Um Ral foi criado com as mesmas notificações para o mesmo Processo deste Licenciamento." + obsMensagem, "Sucesso");
            else
                msg.CriarMensagem("Licenciamento salvo com Sucesso!" + obsMensagem, "Sucesso");
        }

        if (obsMensagem == "")
            lblPopUpLicenciamento_ModalPopupExtender.Hide();

        transacao.Recarregar(ref msg);
        this.CarregarArvore();

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (licenciamento != null && licenciamento.GetUltimoVencimento != null && licenciamento.GetUltimoVencimento.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = licenciamento.Id;
            item.tipoItem = "VALIDADELICENCIAMENTO";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(licenciamento.Vencimentos);
            this.ItensRenovacao.Add(item);
        }

        if (criouRAL)
        {
            RAL ral = RAL.ConsultarPorId(hfIdRalVencimentosPeriodicos.Value.ToInt32());
            if (ral != null && ral.GetUltimoVencimento != null && ral.GetUltimoVencimento.Data <= DateTime.Now)
            {
                ItemRenovacao item = new ItemRenovacao();
                item.idItem = ral.Id;
                item.tipoItem = "RAL";
                item.diasRenovacao = ObterDiasEntreAsRenovacoes(ral.Vencimentos);
                this.ItensRenovacao.Add(item);
            }
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }

    }

    private void SalvarAlvaraPesquisa()
    {

        if (!tbxDataPublicacaoAlvaraPesquisa.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario cadastrar uma data de publicação.", "Aleta", MsgIcons.Alerta);
            return;
        }
        if (!tbxValidadeAlvaraPesquisa.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario cadastrar uma Validade.", "Aleta", MsgIcons.Alerta);
            return;
        }
        if (tbxValidadeAlvaraPesquisa.Text.ToInt32() == 0)
        {
            msg.CriarMensagem("É necessario cadastrar uma Validade válida e maior que Zero.", "Aleta", MsgIcons.Alerta);
            return;
        }

        AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(hfAlvaraPesquisa.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(alvara);
        if (alvara == null)
        {
            alvara = new AlvaraPesquisa();
            alvara.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
            alvara.NotificacaoPesquisaDNPM = new Vencimento();
            alvara.LimiteRenuncia = new Vencimento();

            alvara.TaxaAnualPorHectare = new List<Vencimento>();
            Vencimento v = new Vencimento();
            alvara.TaxaAnualPorHectare.Add(v);

            alvara.DIPEM = new List<Vencimento>();
            Vencimento v2 = new Vencimento();
            alvara.DIPEM.Add(v2);

            if (tbxValidadeAlvaraPesquisa.Text.IsNotNullOrEmpty())
                alvara.Vencimento = new Vencimento();
        }

        string obsMensagem = "</br></br>OBS: Você ainda não inseriu todas as notificações para este Alvará.";
        if (this.SessaoAlvaraPesquisa.Vencimento != null && this.SessaoAlvaraPesquisa.Vencimento.Notificacoes != null && this.SessaoAlvaraPesquisa.Vencimento.Notificacoes.Count > 0 &&
            this.SessaoAlvaraPesquisa.GetUltimoDIPEM != null && this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes != null && this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes.Count > 0 &&
            this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare != null && this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes != null && this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes.Count > 0 &&
            this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM != null && this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes != null && this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes.Count > 0 &&
            this.SessaoAlvaraPesquisa.RequerimentoLavra != null && this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes != null && this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes.Count > 0 &&
            this.SessaoAlvaraPesquisa.LimiteRenuncia != null && this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes != null && this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes.Count > 0 &&
            this.SessaoAlvaraPesquisa.RequerimentoLPTotal != null && this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes != null && this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes.Count > 0
            )
        {
            obsMensagem = "";
        }

        alvara.Exigencias = this.RecarregarExigencias();
        alvara.Numero = tbxNumeroAlvaraPesquisa.Text;

        //DATAS
        alvara.DataPublicacao = tbxDataPublicacaoAlvaraPesquisa.Text.ToSqlDateTime();


        //VENCIMENTO
        if (tbxValidadeAlvaraPesquisa.Text.IsNotNullOrEmpty())
        {
            Vencimento validadeAlvara = new Vencimento();
            if (alvara.Vencimento != null)
                validadeAlvara = alvara.Vencimento;

            alvara.AnosValidade = tbxValidadeAlvaraPesquisa.Text.ToInt32();

            validadeAlvara.Data = alvara.DataPublicacao.AddYears(alvara.AnosValidade);
            if (this.SessaoAlvaraPesquisa.Vencimento != null)
                validadeAlvara.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.Vencimento.Notificacoes);
            alvara.Vencimento = validadeAlvara.Salvar();
        }

        //LIMITE RENUNCIA
        if (tbxValidadeAlvaraPesquisa.Text.IsNotNullOrEmpty())
        {
            Vencimento limiteRenuncia = new Vencimento();
            if (alvara.LimiteRenuncia != null)
                limiteRenuncia = alvara.LimiteRenuncia;

            limiteRenuncia.Data = alvara.DataPublicacao.AddMonths((alvara.AnosValidade * 12) / 3);

            if (this.SessaoAlvaraPesquisa.LimiteRenuncia != null)
                limiteRenuncia.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes);
            alvara.LimiteRenuncia = limiteRenuncia.Salvar();

            lblDataRenuncia.Text = alvara.LimiteRenuncia.Data.EmptyToMinValue();
        }

        //DIPEM
        if (tbxDataPublicacaoAlvaraPesquisa.Text.IsNotNullOrEmpty())
        {
            if (hfAlvaraPesquisa.Value == "" || hfAlvaraPesquisa.Value == "0")
            {
                alvara.GetUltimoDIPEM.Data = ("30/04/" + (alvara.DataPublicacao.Year + 1)).ToSqlDateTime();
            }
            alvara.GetUltimoDIPEM.Status = Status.ConsultarPorId(ddlEstatusDipem.SelectedValue.ToInt32());
            if (this.SessaoAlvaraPesquisa.GetUltimoDIPEM != null)
            {
                alvara.GetUltimoDIPEM.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes);
            }
            alvara.DIPEM[alvara.DIPEM.Count - 1] = alvara.GetUltimoDIPEM.Salvar();
        }


        if (tbxDataAprovacaoRelatorioPesquisa.Text.IsNotNullOrEmpty())
            alvara.DataAprovacaoRelatorio = tbxDataAprovacaoRelatorioPesquisa.Text.ToSqlDateTime();

        if (tbxDataEntragaRelatorioPesquisa.Text.IsNotNullOrEmpty())
            alvara.DataEntregaRelatorio = tbxDataEntragaRelatorioPesquisa.Text.ToSqlDateTime();


        // TAXA ANUAL HECTARE
        if (hfAlvaraPesquisa.Value == "" || hfAlvaraPesquisa.Value == "0")
        {
            alvara.GetUltimaTaxaAnualHectare.Data = this.CalcularTaxaHectare();
        }
        alvara.GetUltimaTaxaAnualHectare.Status = Status.ConsultarPorId(ddlEstatusTaxaAnual.SelectedValue.ToInt32());
        if (this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare != null)
            alvara.GetUltimaTaxaAnualHectare.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes);
        alvara.TaxaAnualPorHectare[alvara.TaxaAnualPorHectare.Count - 1] = alvara.GetUltimaTaxaAnualHectare.Salvar();

        // NOTIFICAÇÃO DE PESQUISA
        Vencimento notificacaoPesquisa = new Vencimento();
        if (alvara.NotificacaoPesquisaDNPM != null)
            notificacaoPesquisa = alvara.NotificacaoPesquisaDNPM;

        notificacaoPesquisa.Data = alvara.DataPublicacao.AddDays(ddlPrazoNotificacaoPesquisa.SelectedValue.ToInt32());
        if (this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM != null)
            notificacaoPesquisa.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes);
        alvara.NotificacaoPesquisaDNPM = notificacaoPesquisa.Salvar();

        //REQUERIMENTO DE LAVRA
        if (alvara.DataAprovacaoRelatorio != null && alvara.DataAprovacaoRelatorio != SqlDate.MinValue)
        {
            if (alvara.RequerimentoLavra == null)
            {
                alvara.RequerimentoLavra = new Vencimento();
            }
            alvara.RequerimentoLavra.Data = alvara.DataAprovacaoRelatorio.AddYears(1);
            if (this.SessaoAlvaraPesquisa.RequerimentoLavra != null)
                alvara.RequerimentoLavra.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes);
            alvara.RequerimentoLavra = alvara.RequerimentoLavra.Salvar();
        }


        //REQUERIMENTO LP Poligonal
        if (alvara.DataAprovacaoRelatorio != null && alvara.DataAprovacaoRelatorio != SqlDate.MinValue)
        {
            if (alvara.RequerimentoLPTotal == null)
            {
                alvara.RequerimentoLPTotal = new Vencimento();
                alvara.RequerimentoLPTotal.Data = DateTime.Now;
            }

            if (this.SessaoAlvaraPesquisa.RequerimentoLPTotal != null)
                alvara.RequerimentoLPTotal.Notificacoes = this.RecarregarNotificacoes(this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes);
            alvara.RequerimentoLPTotal = alvara.RequerimentoLPTotal.Salvar();
        }

        //alvara.Arquivos = this.RecarregarArquivos(this.SessaoUploadsAlvaraPesquisa);

        alvara = alvara.Salvar();

        hfAlvaraPesquisa.Value = alvara.Id.ToString();

        msg.CriarMensagem("Alvará salvo com Sucesso!" + obsMensagem, "Sucesso");

        if (obsMensagem == "")
            btnPopUpAlvaraPesquisa_ModalPopupExtender.Hide();

        transacao.Recarregar(ref msg);
        this.CarregarArvore();

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (alvara != null && alvara.GetUltimaTaxaAnualHectare != null && alvara.GetUltimaTaxaAnualHectare.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = alvara.Id;
            item.tipoItem = "TAXAANUALALVARA";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(alvara.TaxaAnualPorHectare);
            this.ItensRenovacao.Add(item);
        }

        if (alvara != null && alvara.GetUltimoDIPEM != null && alvara.GetUltimoDIPEM.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = alvara.Id;
            item.tipoItem = "DIPEM";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(alvara.DIPEM);
            this.ItensRenovacao.Add(item);
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }
    }

    private DateTime CalcularTaxaHectare()
    {
        DateTime dataPublicao = tbxDataPublicacaoAlvaraPesquisa.Text.ToSqlDateTime();
        if (dataPublicao.Month > 6)
        {
            for (int i = 31; i > 0; i--)
            {
                DateTime j = new DateTime(dataPublicao.Year + 1, 1, i);
                if (j.DayOfWeek != DayOfWeek.Sunday && j.DayOfWeek != DayOfWeek.Saturday)
                {
                    return j;
                }
            }
        }
        else
        {
            for (int i = 31; i > 0; i--)
            {
                DateTime j = new DateTime(dataPublicao.Year, 7, i);
                if (j.DayOfWeek != DayOfWeek.Sunday && j.DayOfWeek != DayOfWeek.Saturday)
                {
                    return j;
                }
            }
        }
        return new DateTime();
    }

    private void SalvarExtracao()
    {
        if (!tbxValidadeExtracao.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario cadastrar uma data de Vencimendo.", "Aleta", MsgIcons.Alerta);
            return;
        }

        Extracao extracao = Extracao.ConsultarPorId(hfIdExtracao.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(extracao);
        Vencimento vencimento = new Vencimento();

        if (extracao == null)
        {
            extracao = new Extracao();
            extracao.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());

            extracao.Vencimentos = new List<Vencimento>();
            extracao.Vencimentos.Add(vencimento);
        }

        string obsMensagem = "</br></br>OBS: Você ainda não inseriu as notificações para este Licenciamento.";
        if (this.SessaoExtracao != null && this.SessaoExtracao.GetUltimoVencimento != null && this.SessaoExtracao.GetUltimoVencimento.Notificacoes != null && this.SessaoExtracao.GetUltimoVencimento.Notificacoes.Count > 0)
        {
            obsMensagem = "";
        }

        extracao.NumeroExtracao = tbxNumeroExtracao.Text;
        extracao.Exigencias = this.RecarregarExigencias();
        extracao.DataPublicacao = tbxPublicacaoExtracao.Text.ToSqlDateTime();
        extracao.DataAbertura = tbxDataAberturaExtracao.Text.ToSqlDateTime();
        extracao.NumeroLicenca = tbxnumeroLicencaExtracao.Text;
        extracao.ValidadeLicenca = tbxValidadeLicencaExtracao.Text.ToSqlDateTime();

        Vencimento ultimoVencimento = extracao.GetUltimoVencimento;

        if (ultimoVencimento == null)
            ultimoVencimento = new Vencimento();

        if (this.SessaoExtracao != null && this.SessaoExtracao.Vencimentos != null && this.SessaoExtracao.Vencimentos.Count > 0)
            ultimoVencimento.Notificacoes = this.RecarregarNotificacoes(this.SessaoExtracao.GetUltimoVencimento.Notificacoes);

        ultimoVencimento.Data = tbxValidadeExtracao.Text.ToSqlDateTime();
        ultimoVencimento.Status = Status.ConsultarPorId(ddlEstatusVencimentoExtracao.SelectedValue.ToInt32());

        if (extracao.Vencimentos == null || extracao.Vencimentos.Count == 0)
        {
            extracao.Vencimentos = new List<Vencimento>();

            extracao.Vencimentos.Add(ultimoVencimento.Salvar());
        }
        else
            extracao.Vencimentos[extracao.Vencimentos.Count - 1] = ultimoVencimento.Salvar();


       // extracao.Arquivos = this.RecarregarArquivos(this.SessaoUploadsExtracao);
        extracao = extracao.Salvar();
        hfIdExtracao.Value = extracao.Id.ToString();

        msg.CriarMensagem("Regime de Extração salvo com Sucesso!" + obsMensagem, "Sucesso");


        if (obsMensagem == "")
            lblPopUpExtracao_ModalPopupExtender.Hide();

        transacao.Recarregar(ref msg);
        this.CarregarArvore();

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (extracao != null && extracao.GetUltimoVencimento != null && extracao.GetUltimoVencimento.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = extracao.Id;
            item.tipoItem = "EXTRACAO";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(extracao.Vencimentos);
            this.ItensRenovacao.Add(item);
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }
    }

    private void SalvarGuia()
    {
        if (!tbxDataRequerimentoGuia.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario cadastrar uma data de Requerimento.", "Aleta", MsgIcons.Alerta);
            return;
        }

        string obsMensagem = "Guia de Utilização salva com Sucesso!</br></br>OBS: Você ainda não inseriu as notificações para esta Guia.";
        if (this.NotificacoesSelecionadas != null && this.NotificacoesSelecionadas.Count > 0)
        {
            obsMensagem = "";
        }

        GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIdGuiaUtilizacao.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(guia);
        Vencimento vencimento = new Vencimento();
        if (guia == null)
        {
            guia = new GuiaUtilizacao();
            guia.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());

            if (tbxDataVencimentoGuia.Text.IsNotNullOrEmpty())
            {
                guia.Vencimentos = new List<Vencimento>();
                guia.Vencimentos.Add(vencimento);
            }
        }
        else if (guia.GetUltimoVencimento == null && tbxDataVencimentoGuia.Text.IsNotNullOrEmpty())
        {
            guia.Vencimentos = new List<Vencimento>();
            guia.Vencimentos.Add(vencimento);
        }

        if (tbxDataVencimentoGuia.Text.IsDate())
            guia.DataLimiteRequerimento = tbxDataVencimentoGuia.Text.ToSqlDateTime().AddDays(-60);

        vencimento = guia.GetUltimoVencimento;
        if (vencimento != null)
        {
            if (tbxDataVencimentoGuia.Text.IsNotNullOrEmpty())
                vencimento.Data = tbxDataVencimentoGuia.Text.ToSqlDateTime();
            vencimento.Status = Status.ConsultarPorId(ddlStatusGuiaUtilizacao.SelectedValue.ToInt32());
            vencimento.Notificacoes = this.RecarregarNotificacoes(this.NotificacoesSelecionadas);
            guia.Vencimentos[guia.Vencimentos.Count - 1] = vencimento.Salvar();
        }

        guia.Exigencias = this.RecarregarExigencias();
        guia.DataRequerimento = tbxDataRequerimentoGuia.Text.ToSqlDateTime();
        guia.Numero = tbxNumeroGuia.Text;

        bool criouRAL = false;
        if (tbxDataEmissaoGuia.Text.Trim().IsNotNullOrEmpty())
        {
            guia.DataEmissao = tbxDataEmissaoGuia.Text.ToSqlDateTime();

            if (guia.GetUltimoVencimento != null)
                criouRAL = this.CriarRAL(("15/03/" + guia.DataEmissao.AddYears(1).Year).ToSqlDateTime(), guia.GetUltimoVencimento.Notificacoes);
            else
                criouRAL = this.CriarRAL(("15/03/" + guia.DataEmissao.AddYears(1).Year).ToSqlDateTime(), null);

            if (criouRAL)
                msg.CriarMensagem("Guia de Utilização salva com Sucesso!</br></br>OBS.: Um Ral foi criado  com as mesmas notificações para o mesmo Processo desta Guia." + obsMensagem, "Sucesso");
            else
                msg.CriarMensagem("Guia de Utilização salva com Sucesso!" + obsMensagem, "Sucesso");
        }


       // guia.Arquivos = this.RecarregarArquivos(this.SessaoUploadsGuiaUtlizacao);
        guia = guia.Salvar();
        hfIdGuiaUtilizacao.Value = guia.Id.ToString();

        if (obsMensagem == "" && msg.Mensagem == "")
        {
            btnPopUpGuia_ModalPopupExtender.Hide();
        }
        else if (criouRAL == false && obsMensagem.IsNotNullOrEmpty())
        {
            msg.CriarMensagem(obsMensagem, "Aleta", MsgIcons.Alerta);
        }
        else
        {
            msg.CriarMensagem("Guia criada com Sucesso!", "Sucesso", MsgIcons.Sucesso);
        }

        transacao.Recarregar(ref msg);
        this.CarregarArvore();

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (guia != null && guia.GetUltimoVencimento != null && guia.GetUltimoVencimento.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = guia.Id;
            item.tipoItem = "GUIAUTILIZACAO";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(guia.Vencimentos);
            this.ItensRenovacao.Add(item);
        }

        if (criouRAL)
        {
            RAL ral = RAL.ConsultarPorId(hfIdRalVencimentosPeriodicos.Value.ToInt32());
            if (ral != null && ral.GetUltimoVencimento != null && ral.GetUltimoVencimento.Data <= DateTime.Now)
            {
                ItemRenovacao item = new ItemRenovacao();
                item.idItem = ral.Id;
                item.tipoItem = "RAL";
                item.diasRenovacao = ObterDiasEntreAsRenovacoes(ral.Vencimentos);
                this.ItensRenovacao.Add(item);
            }
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }

    }

    private void VerificarAlteracaoDeStatus(object objetoManipulado)
    {
        if (objetoManipulado == null)
            return;

        tbxHistoricoAlteracao.Text = "";
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        hfTypeAlteracao.Value = objetoManipulado.GetType().Name;
        string processo = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore()).GetNumeroProcessoComMascara;

        string alteracao = "";
        switch (objetoManipulado.GetType().Name.ToString())
        {
            case "GuiaUtilizacao":
                {
                    GuiaUtilizacao guia = (GuiaUtilizacao)objetoManipulado;
                    if (guia != null && guia.GetUltimoVencimento != null && guia.GetUltimoVencimento.Status.Id.ToString() != ddlStatusGuiaUtilizacao.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento da Guia de Utilização do Processo(" + processo + "). De: " + guia.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusGuiaUtilizacao.SelectedItem.Text + " - Empresa: " + guia.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "AlvaraPesquisa":
                {
                    AlvaraPesquisa alvara = (AlvaraPesquisa)objetoManipulado;
                    if (alvara != null && alvara.GetUltimaTaxaAnualHectare != null && alvara.GetUltimaTaxaAnualHectare.Status.Id.ToString() != ddlEstatusTaxaAnual.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Taxa Anual por Hectare - Processo(" + processo + "). De: " + alvara.GetUltimaTaxaAnualHectare.Status.Nome + " - Para: " + ddlEstatusTaxaAnual.SelectedItem.Text + " - Empresa: " + alvara.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                    if (alvara != null && alvara.GetUltimoDIPEM != null && alvara.GetUltimoDIPEM.Status.Id.ToString() != ddlEstatusDipem.SelectedValue)
                    {
                        if (alteracao.Trim() != "")
                        {
                            alteracao += " e ";
                        }
                        alteracao += "(Alteração no Status da DIPEM - Processo(" + processo + "). De: " + alvara.GetUltimoDIPEM.Status.Nome + " - Para: " + ddlEstatusDipem.SelectedItem.Text + " - Empresa: " + alvara.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "Licenciamento":
                {
                    Licenciamento licen = (Licenciamento)objetoManipulado;
                    if (licen != null && licen.GetUltimoVencimento != null && licen.GetUltimoVencimento.Status.Id.ToString() != ddlEstatusVencimentoLicenciamento.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do Licenciamento - Processo(" + processo + "). De: " + licen.GetUltimoVencimento.Status.Nome + " - Para: " + ddlEstatusVencimentoLicenciamento.SelectedItem.Text + " - Empresa: " + licen.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "Extracao":
                {
                    Extracao extracao = (Extracao)objetoManipulado;
                    if (extracao != null && extracao.GetUltimoVencimento != null && extracao.GetUltimoVencimento.Status.Id.ToString() != ddlEstatusVencimentoExtracao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Extração - Processo(" + processo + "). De: " + extracao.GetUltimoVencimento.Status.Nome + " - Para: " + ddlEstatusVencimentoExtracao.SelectedItem.Text + " - Empresa: " + extracao.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "Exigencia":
                {
                    Exigencia exig = (Exigencia)objetoManipulado;
                    if (exig != null && exig.GetUltimoVencimento != null && exig.GetUltimoVencimento.Status.Id.ToString() != ddlStatusExigencia.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Exigência(" + tbxDescricaoExigencias.Text + ") - Processo(" + processo + "). De: " + exig.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusExigencia.SelectedItem.Text + " - Empresa: " + (ProcessoDNPM.getProcessoPeloNumero(processo.Replace(".", "").Replace("/", "")) != null ? ProcessoDNPM.getProcessoPeloNumero(processo.Replace(".", "").Replace("/", "")).Empresa.GetNumeroCNPJeCPFComMascara : "") + ")";
                    }
                } break;
            default:
                return;
        }


        if (alteracao.Trim() == "")
            return;
        tbxHistoricoAlteracao.Text = "";
        lblAlteracao.Text = alteracao;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));

    }

    private void VerificarAlteracaoDeStatusRenovacao(Object objetoManipulado)
    {
        if (objetoManipulado == null)
            return;

        tbxHistoricoAlteracao.Text = "";
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        hfTypeAlteracao.Value = objetoManipulado.GetType().Name;
        string processo = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore()).GetNumeroProcessoComMascara;

        string alteracao = "";
        switch (objetoManipulado.GetType().Name.ToString())
        {
            case "GuiaUtilizacao":
                {
                    GuiaUtilizacao guia = (GuiaUtilizacao)objetoManipulado;
                    if (guia != null && guia.GetUltimoVencimento != null && guia.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do vencimento da Guia de Utilização do Processo(" + processo + "). De: " + guia.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + guia.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "AlvaraPesquisa":
                {
                    AlvaraPesquisa alvara = (AlvaraPesquisa)objetoManipulado;
                    if (hfTipoRenovacao.Value.ToUpper() == "TAXAANUALHECTARE" && alvara != null && alvara.GetUltimaTaxaAnualHectare != null && alvara.GetUltimaTaxaAnualHectare.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Taxa Anual por Hectare - Processo(" + processo + "). De: " + alvara.GetUltimaTaxaAnualHectare.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + alvara.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                    if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTODIPEM" && alvara != null && alvara.GetUltimoDIPEM != null && alvara.GetUltimoDIPEM.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        if (alteracao.Trim() != "")
                        {
                            alteracao += " e ";
                        }
                        alteracao += "(Alteração no Status da DIPEM - Processo(" + processo + "). De: " + alvara.GetUltimoDIPEM.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + alvara.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "Licenciamento":
                {
                    Licenciamento licen = (Licenciamento)objetoManipulado;
                    if (licen != null && licen.GetUltimoVencimento != null && licen.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do Licenciamento - Processo(" + processo + "). De: " + licen.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + licen.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "Extracao":
                {
                    Extracao extracao = (Extracao)objetoManipulado;
                    if (extracao != null && extracao.GetUltimoVencimento != null && extracao.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Extração - Processo(" + processo + "). De: " + extracao.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + extracao.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "Exigencia":
                {
                    Exigencia exig = (Exigencia)objetoManipulado;
                    if (exig != null && exig.GetUltimoVencimento != null && exig.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Exigência - Processo(" + processo + "). De: " + exig.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + (ProcessoDNPM.getProcessoPeloNumero(processo.Replace(".", "").Replace("/", "")) != null ? ProcessoDNPM.getProcessoPeloNumero(processo.Replace(".", "").Replace("/", "")).Empresa.GetNumeroCNPJeCPFComMascara : "") + ")";
                    }
                } break;
            case "RAL":
                {
                    RAL ral = (RAL)objetoManipulado;
                    if (ral != null && ral.GetUltimoVencimento != null && ral.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do RAL - Processo(" + processo + "). De: " + ral.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + ral.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            default:
                return;
        }

        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));

    }

    private void VerificarAlteracaoDeStatusHistoricoVencimento(int indexVencimento)
    {
        if (hfTypeVencimento.Value == "")
            return;

        tbxHistoricoAlteracao.Text = "";
        hfIdAlteracao.Value = hfIDVencimento.Value;
        hfTypeAlteracao.Value = hfTypeVencimento.Value;

        string processo = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore()).GetNumeroProcessoComMascara;

        string alteracao = "";
        switch (hfTypeVencimento.Value)
        {
            case "GuiaUtilizacao":
                {
                    GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (guia != null && guia.Vencimentos[indexVencimento] != null && guia.Vencimentos[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do vencimento da Guia de Utilização do Processo(" + processo + "). De: " + guia.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + guia.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "TaxaAnualPorHectare":
                {
                    AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (alvara != null && alvara.TaxaAnualPorHectare[indexVencimento] != null && alvara.TaxaAnualPorHectare[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Taxa Anual por Hectare - Processo(" + processo + "). De: " + alvara.TaxaAnualPorHectare[indexVencimento].Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + alvara.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "DIPEM":
                {
                    AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (alvara != null && alvara.DIPEM[indexVencimento] != null && alvara.DIPEM[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da DIPEM - Processo(" + processo + "). De: " + alvara.DIPEM[indexVencimento].Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + alvara.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "Licenciamento":
                {
                    Licenciamento licen = Licenciamento.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (licen != null && licen.Vencimentos[indexVencimento] != null && licen.Vencimentos[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do Licenciamento - Processo(" + processo + "). De: " + licen.Vencimentos[indexVencimento].Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + licen.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "Extracao":
                {
                    Extracao extracao = Extracao.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (extracao != null && extracao.Vencimentos[indexVencimento] != null && extracao.Vencimentos[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Extração - Processo(" + processo + "). De: " + extracao.Vencimentos[indexVencimento].Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + extracao.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "Exigencia":
                {
                    Exigencia exig = Exigencia.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (exig != null && exig.Vencimentos[indexVencimento] != null && exig.Vencimentos[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Exigência - Processo(" + processo + "). De: " + exig.Vencimentos[indexVencimento].Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + (ProcessoDNPM.getProcessoPeloNumero(processo.Replace(".", "").Replace("/", "")) != null ? ProcessoDNPM.getProcessoPeloNumero(processo.Replace(".", "").Replace("/", "")).Empresa.GetNumeroCNPJeCPFComMascara : "") + ")";

                    }
                } break;
            case "RAL":
                {
                    RAL ral = RAL.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (ral != null && ral.Vencimentos[indexVencimento] != null && ral.Vencimentos[indexVencimento].Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status do RAL - Processo(" + processo + "). De: " + ral.Vencimentos[indexVencimento].Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + ral.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            default:
                return;
        }

        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));
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

    private void SalvarHistoricoAlteracaoStatus()
    {
        Historico h = new Historico();
        h.DataPublicacao = DateTime.Now;
        h.Alteracao = lblAlteracao.Text;
        h.Observacao = tbxHistoricoAlteracao.Text;

        switch (hfTypeAlteracao.Value)
        {
            case "GuiaUtilizacao":
                {
                    GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.GuiaUtilizacao = guia;
                } break;
            case "DIPEM":
            case "TaxaAnualPorHectare":
            case "AlvaraPesquisa":
                {
                    AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.Regime = alvara;
                } break;
            case "Licenciamento":
                {
                    Licenciamento lic = Licenciamento.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.Regime = lic;
                } break;
            case "Extracao":
                {
                    Extracao ext = Extracao.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.Regime = ext;
                } break;
            case "Exigencia":
                {
                    Exigencia exig = Exigencia.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.Exigencia = exig;
                } break;
            case "RAL":
                {
                    RAL ral = RAL.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.RAL = ral;
                } break;
            default:
                return;
        }

        h.Salvar();

        if (chkEnviarEmail.Checked)
        {

            Email email = new Email();
            foreach (ListItem l in ckbGrupos.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());
            foreach (ListItem l in ckbConsultoria.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());
            foreach (ListItem l in ckbEmpresas.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());

            if (hfTypeAlteracao.Value == "Exigencia" && lblAlteracao.Text.Contains("prorrogação"))
            {
                email.Assunto = "Prorrogação de Vencimento de exigência - Sistema Sustentar";
                email.Mensagem = email.CriarTemplateParaProrrogacaoDeDeVencimento(h);
            }
            else
            {
                email.Assunto = "Alteração de Status de Vencimento - Sistema Sustentar";
                email.Mensagem = email.CriarTemplateParaMudancaDeStatusDeVencimento(h);
            }

            if (!email.EnviarAutenticado(25, false))
                msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
        }

        hfIdAlteracao.Value = "";
        hfTypeAlteracao.Value = "";
        lblAlteracaoStatus_ModalPopupExtender.Hide();
        if (msg.Ico != MsgIcons.Informacao)
            msg.CriarMensagem("Histórico registrado com Sucesso", "Sucesso");
    }

    private void CarregarRequerimentoPesquisa(int id)
    {
        if (id > 0)
        {
            RequerimentoPesquisa rp = RequerimentoPesquisa.ConsultarPorId(id);
            tbxDataEntradaRequerimentoPesquisa.Text = rp.DataRequerimento.EmptyToMinValue();
            this.CarregarExigencias(rp.Exigencias, grvExigenciasRequerimentoPesquisa);
        }
        else
        {
            msg.CriarMensagem("Requerimento inválido", "Alerta", MsgIcons.Alerta);
        }
    }

    private void CarregarExigencias(IList<Exigencia> iList, GridView grv)
    {
        grv.DataSource = iList;
        grv.DataBind();
    }

    private void EditarExigencia(int id)
    {
        if (id > 0)
        {
            this.NovaExigencia();

            this.CarregarStatus(ddlStatusExigencia);
            lkbVencimentoExigencia.Visible = true;
            lkbObservacoesExigencia.Visible = true;

            Exigencia e = Exigencia.ConsultarPorId(id);
            hfIdExigencia.Value = id.ToString();
            tbxDataPublicacaoExigencias.Text = e.DataPublicacao.EmptyToMinValue();

            tbxDescricaoExigencias.Text = e.Descricao;

            this.ArquivosUploadExigencias = this.RecarregarArquivos(e.Arquivos);

            tbxDiasPrazo.Text = e.DiasPrazo.ToString();

            if (e.GetUltimoVencimento != null)
            {
                ddlStatusExigencia.SelectedValue = e.GetUltimoVencimento.Status.Id.ToString();
                tbxDataAtendimentoExigencia.Text = e.GetUltimoVencimento.DataAtendimento.EmptyToMinValue();
                tbxProtocoloExigencia.Text = e.GetUltimoVencimento.ProtocoloAtendimento;

                chkPeriodicaExigencia.Checked = e.GetUltimoVencimento.Periodico;

                lkbProrrogacao.Visible = true;
                if (e.GetUltimoVencimento.ProrrogacoesPrazo != null)
                {
                    lkbProrrogacao.Text = "Abrir Prorrogações - [" + e.GetUltimoVencimento.ProrrogacoesPrazo.Count + "] Prorrogações.";
                    if (e.GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
                        tbxDiasPrazo.Enabled = false;
                    else
                        tbxDiasPrazo.Enabled = true;
                }
                else
                {
                    lkbProrrogacao.Text = "Abrir Prorrogações";
                    tbxDiasPrazo.Enabled = true;
                }

                this.NotificacoesDeExigencia = e.GetUltimoVencimento.Notificacoes;
                this.CarregarNotificacoes(e.GetUltimoVencimento.Notificacoes);
            }
            else
            {
                this.NotificacoesDeExigencia = null;
                grvNotificacoesExigencias.DataSource = null;
                grvNotificacoesExigencias.DataBind();
            }
        }
        else
        {
            msg.CriarMensagem("Exigência inválida", "Alerta", MsgIcons.Alerta);
        }
    }

    private void CarregarNotificacoes(IList<Notificacao> iList)
    {
        grvNotificacoesExigencias.DataSource = iList;
        grvNotificacoesExigencias.DataBind();
    }

    private void NovaExigencia()
    {
        lblExigenciaCondicionante.Text = "Exigencias";

        lkbProrrogacao.Visible = false;
        tbxDiasPrazo.Enabled = true;
        lkbProrrogacao.Text = "Abrir Prorrogações";
        lkbObservacoesExigencia.Visible = false;

        lkbVencimentoExigencia.Visible = false;
        this.CarregarStatus(ddlStatusExigencia);
        hfIdExigencia.Value = "0";
        tbxDataPublicacaoExigencias.Text = "";
        this.NotificacoesDeExigencia = null;
        this.ArquivosUploadExigencias = null;
        grvNotificacoesExigencias.DataSource = null;
        grvNotificacoesExigencias.DataBind();
        WebUtil.LimparCampos(upExigencias.Controls[0].Controls);
    }

    private void SalvarExigencia()
    {
        if (!tbxDataPublicacaoExigencias.Text.IsDate())
        {
            msg.CriarMensagem("É necessario informar uma data de Publicação.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Exigencia e = Exigencia.ConsultarPorId(hfIdExigencia.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(e);
        Vencimento vencimento = new Vencimento();
        if (e == null)
        {
            e = new Exigencia();
            e.Vencimentos = new List<Vencimento>();
            e.Vencimentos.Add(vencimento);
        }
        else
        {
            vencimento = e.GetUltimoVencimento;
        }

        if (vencimento == null)
        {
            vencimento = new Vencimento();
            e.Vencimentos = new List<Vencimento>();
            e.Vencimentos.Add(vencimento);
        }


        e.DataPublicacao = tbxDataPublicacaoExigencias.Text.ToSqlDateTime();
        e.Descricao = tbxDescricaoExigencias.Text;
        e.DiasPrazo = tbxDiasPrazo.Text.ToInt32();
        e.Arquivos = this.RecarregarArquivos(this.ArquivosUploadExigencias);


        if (vencimento.ProrrogacoesPrazo == null || (vencimento.ProrrogacoesPrazo != null && vencimento.ProrrogacoesPrazo.Count == 0))
            vencimento.Data = this.CalcularDataVencimentoExigencia();


        vencimento.Periodico = chkPeriodicaExigencia.Checked;
        vencimento.ProtocoloAtendimento = tbxProtocoloExigencia.Text;
        vencimento.Status = Status.ConsultarPorId(ddlStatusExigencia.SelectedValue.ToInt32());

        if (tbxDataAtendimentoExigencia.Text.IsNotNullOrEmpty())
            vencimento.DataAtendimento = tbxDataAtendimentoExigencia.Text.ToSqlDateTime();

        vencimento.Notificacoes = this.RecarregarNotificacoes(this.NotificacoesDeExigencia);

        vencimento = vencimento.Salvar();

        e.Vencimentos[e.Vencimentos.Count - 1] = vencimento;

        e = e.Salvar();

        if (this.ExigenciasSelecionadas == null)
            this.ExigenciasSelecionadas = new List<Exigencia>();

        this.ExigenciasSelecionadas.Remove(e);
        this.ExigenciasSelecionadas.Add(e);

        this.CarregarExigenciasNoGrid();
        btnPopUpCadastroExigencia_ModalPopupExtender.Hide();

        this.SalvarItemDaExigencia(e);

        this.NotificacoesDeExigencia = null;

        grvNotificacoesExigencias.DataSource = null;
        grvNotificacoesExigencias.DataBind();

        lkbObservacoesExigencia.Visible = true;

        msg.CriarMensagem("Registro criado com sucesso!", "Sucesso", MsgIcons.Sucesso);

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (e != null && e.GetUltimoVencimento != null && e.GetUltimoVencimento.Periodico && e.GetUltimoVencimento.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = e.Id;
            item.tipoItem = "EXIGENCIA";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(e.Vencimentos);
            this.ItensRenovacao.Add(item);
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }

    }

    private void CarregarExigenciasNoGrid()
    {
        if (this.PopUpAberto == PopUpAbertoENUM.REQUERIMENTOPESQUISA)
        {
            grvExigenciasRequerimentoPesquisa.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasRequerimentoPesquisa.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.ALVARAPESQUISA)
        {
            grvExigenciasAlvaraPesquisa.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasAlvaraPesquisa.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.REQUERIMENTOLAVRA)
        {
            grvExigenciasRequerimentoLavra.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasRequerimentoLavra.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.CONCESSAOLAVRA)
        {
            grvExigenciasConcessaoLavra.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasConcessaoLavra.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.CONCESSAOLAVRA)
        {
            grvExigenciaExtracao.DataSource = this.ExigenciasSelecionadas;
            grvExigenciaExtracao.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.EXTRACAO)
        {
            grvExigenciaExtracao.DataSource = this.ExigenciasSelecionadas;
            grvExigenciaExtracao.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.LICENCIAMENTO)
        {
            grvNotificacaoLicenciamento.DataSource = this.ExigenciasSelecionadas;
            grvNotificacaoLicenciamento.DataBind();
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.GUIA)
        {
            grvExigenciasGuia.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasGuia.DataBind();
            return;
        }
    }

    private void EditarGuiaDeUtilizacao()
    {
        if (trvProcessos.SelectedValue.Contains("GUIA_"))
        {
            this.CarregarStatus(ddlStatusGuiaUtilizacao);

            hfIdProcessoDNPM.Value = "0";
            GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            lkbObservacoes.Visible = (guia != null);

            if (guia.GetUltimoVencimento != null)
            {
                lkbStstusGuia.Visible = true;

                tbxDataVencimentoGuia.Text = guia.GetUltimoVencimento.Data.EmptyToMinValue();
                ddlStatusGuiaUtilizacao.SelectedValue = guia.GetUltimoVencimento.Status.Id.ToString();

                grvNotificacoesGuia.DataSource = guia.GetUltimoVencimento.Notificacoes;
                grvNotificacoesGuia.DataBind();

                this.NotificacoesSelecionadas = guia.GetUltimoVencimento.Notificacoes;
            }
            else
            {
                grvNotificacoesGuia.DataSource = null;
                grvNotificacoesGuia.DataBind();
                this.NotificacoesSelecionadas = new List<Notificacao>();
                lkbStstusGuia.Visible = false;
            }

            hfIdGuiaUtilizacao.Value = guia.Id.ToString();
            tbxDataRequerimentoGuia.Text = guia.DataRequerimento.EmptyToMinValue();
            tbxNumeroGuia.Text = guia.Numero;
            tbxDataEmissaoGuia.Text = guia.DataEmissao.EmptyToMinValue();

            grvExigenciasGuia.DataSource = guia.Exigencias;
            grvExigenciasGuia.DataBind();

            this.ExigenciasSelecionadas = guia.Exigencias;
            this.ArquivosUpload = this.RecarregarArquivos(guia.Arquivos);
            this.SessaoUploadsGuiaUtlizacao = this.RecarregarArquivos(guia.Arquivos);

            this.PopUpAberto = PopUpAbertoENUM.GUIA;
            btnPopUpGuia_ModalPopupExtender.Show();
        }
        else
        {
            msg.CriarMensagem("Selecione primeiro o Guia de Utilização que deseja editar.", "Alerta", MsgIcons.Alerta);
        }
    }

    private void EditarProcesso()
    {
        hfIdProcessoDNPM.Value = "0";
        if (trvProcessos.SelectedValue.Contains("PROC_"))
        {
            this.CarregarConsultoriasProcessoDNPM();
            this.CarregarEstadosProcessoDNPM();
            this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaDNPM);
            if (ddlEmpresa.SelectedIndex > 0)
                ddlEmpresaDNPM.SelectedValue = ddlEmpresa.SelectedValue;



            this.IdLicencasSelecionadasDNPM = null;
            grvLicencasDNPM.DataSource = null;
            grvLicencasDNPM.DataBind();

            ProcessoDNPM processoDNPM = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
            hfIdProcessoDNPM.Value = processoDNPM.Id.ToString();
            tbxNumeroProcessoDNPM.Text = processoDNPM.Numero;
            ddlRegimeDNPM.SelectedValue = processoDNPM.RegimeDeCriacao;
            tbxDataAberturaDNPM.Text = processoDNPM.DataAbertura.EmptyToMinValue();
            ddlEmpresaDNPM.SelectedValue = processoDNPM.Empresa.Id.ToString();
            if (processoDNPM.Consultora != null)
            {
                ddlConsultoriaDNPM.SelectedValue = processoDNPM.Consultora.Id.ToString();
            }

            tbxSubstancia.Text = processoDNPM.Substancia;
            tbxIdentificacaoAreaDNPM.Text = processoDNPM.Identificacao;
            tbxTamanhoAreaDNPM.Text = processoDNPM.TamanhoArea;

            tbxEnderecoDNPM.Text = processoDNPM.Endereco;
            if (processoDNPM.Cidade != null)
            {
                ddlEstadoProcessoDNPM.SelectedValue = processoDNPM.Cidade.Estado.Id.ToString();
                this.CarregarCidadeProcessoDNPM(processoDNPM.Cidade.Estado.Id);
                ddlCidadeProcessoDNPM.SelectedValue = processoDNPM.Cidade.Id.ToString();

            }

            this.SubstanciaSelecionadas = null;
            if (processoDNPM.Substancias.Count > 0)
                this.SetarSessaoSubstancias(processoDNPM.Substancias);


            this.IdLicencasSelecionadasDNPM = null;
            if (processoDNPM.Licencas.Count > 0)
                this.CarregarSessaoLicencas(processoDNPM.Licencas);
            grvLicencasDNPM.DataSource = processoDNPM.Licencas;
            grvLicencasDNPM.DataBind();


            tbxObservacoesProcessoDNPM.Text = processoDNPM.Observacoes;
            btnUploadProcessoDNPM.Visible = true;
            this.ArquivosUpload = this.RecarregarArquivos(processoDNPM.Arquivos);

            ddlRegimeDNPM.Enabled = false;

            this.SessaoUploadsProcessoDNPM = this.RecarregarArquivos(processoDNPM.Arquivos);
            btnAbrirContratos.Visible = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloContratos;
            ibtnAddLicencaProcesso.Visible = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloMeioAmbiente;
            btnPopUpCadastroProcessoDNPM_ModalPopupExtender.Show();
        }
        else
        {
            msg.CriarMensagem("Selecione primeiro o Processo ANM que deseja Editar.", "Alerta", MsgIcons.Alerta);
        }
    }

    private void SetarSessaoSubstancias(IList<Substancia> subs)
    {
        if (this.SubstanciaSelecionadas == null)
            this.SubstanciaSelecionadas = new List<int>();

        foreach (Substancia item in subs)
        {
            this.SubstanciaSelecionadas.Add(item.Id);
        }
    }

    private void EditarRegime()
    {
        if (trvProcessos.SelectedValue.Contains("RP_"))
        {
            this.PopUpAberto = PopUpAbertoENUM.REQUERIMENTOPESQUISA;
            RequerimentoPesquisa regime = RequerimentoPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.ExigenciasSelecionadas = regime.Exigencias;
            this.EditarRequerimentoPesquisa(regime);
            this.ArquivosUpload = this.RecarregarArquivos(regime.Arquivos);
            this.SessaoUploadsRequerimentoPesquisa = this.ArquivosUpload;
            return;
        }
        else if (trvProcessos.SelectedValue.Contains("ALP_"))
        {
            this.CarregarStatus(ddlEstatusTaxaAnual);
            this.CarregarStatus(ddlEstatusDipem);

            this.PopUpAberto = PopUpAbertoENUM.ALVARAPESQUISA;
            AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.ExigenciasSelecionadas = regime.Exigencias;
            this.SessaoAlvaraPesquisa = regime;
            this.EditarAlvaraPesquisa(regime);
            this.ArquivosUpload = this.RecarregarArquivos(regime.Arquivos);
            this.SessaoUploadsAlvaraPesquisa = this.ArquivosUpload;
            return;
        }
        else if (trvProcessos.SelectedValue.Contains("EX_"))
        {
            this.PopUpAberto = PopUpAbertoENUM.EXTRACAO;
            Extracao regime = Extracao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.ExigenciasSelecionadas = regime.Exigencias;
            this.SessaoExtracao = regime;
            this.EditarExtracao(regime);
            this.ArquivosUpload = this.RecarregarArquivos(regime.Arquivos);
            this.SessaoUploadsExtracao = this.ArquivosUpload;

            return;
        }
        else if (trvProcessos.SelectedValue.Contains("LI_"))
        {
            this.CarregarStatus(ddlEstatusVencimentoLicenciamento);
            tbxPublicacaoLicenciamento.Enabled = false;
            this.PopUpAberto = PopUpAbertoENUM.LICENCIAMENTO;
            Licenciamento regime = Licenciamento.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.ExigenciasSelecionadas = regime.Exigencias;
            this.SessaoLicenciamento = regime;
            this.EditarLicenciamento(regime);
            this.ArquivosUpload = this.RecarregarArquivos(regime.Arquivos);
            this.SessaoUploadsLicenciamento = this.ArquivosUpload;

            return;
        }
        else if (trvProcessos.SelectedValue.Contains("RL_"))
        {
            this.PopUpAberto = PopUpAbertoENUM.REQUERIMENTOLAVRA;
            RequerimentoLavra regime = RequerimentoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.ExigenciasSelecionadas = regime.Exigencias;
            this.EditarRequerimentoLavra(regime);
            this.ArquivosUpload = this.RecarregarArquivos(regime.Arquivos);
            this.SessaoUploadsRequerimentoLavra = this.ArquivosUpload;
            return;
        }
        else if (trvProcessos.SelectedValue.Contains("CO_"))
        {
            this.PopUpAberto = PopUpAbertoENUM.CONCESSAOLAVRA;
            ConcessaoLavra regime = ConcessaoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.ExigenciasSelecionadas = regime.Exigencias;
            if (regime.RequerimentoImissaoPosse != null)
                this.NotificacoesSelecionadas = regime.RequerimentoImissaoPosse.Notificacoes;
            this.EditarConcessaoLavra(regime);
            this.ArquivosUpload = this.RecarregarArquivos(regime.Arquivos);
            this.SessaoUploadsConcessaoLavra = this.ArquivosUpload;

            return;
        }
        else
        {
            msg.CriarMensagem("Selecione um Regime na Árvore", "Alerta", MsgIcons.Alerta);
            return;
        }
    }

    private void EditarConcessaoLavra(ConcessaoLavra regime)
    {
        hfIdConcessaoLavra.Value = regime.Id.ToString();
        tbxNumeroPortariaLavra.Text = regime.NumeroPortariaLavra;
        tbxPublicacaoConcessaoLavra.Text = regime.Data.EmptyToMinValue();
        tbxDataApresentacaoRelatorio.Text = regime.DataRelatorioReavaliacaoReserva.EmptyToMinValue();
        if (regime.Exigencias != null)
        {
            grvExigenciasConcessaoLavra.DataSource = regime.Exigencias;
            grvExigenciasConcessaoLavra.DataBind();
        }
        else
        {
            grvExigenciasConcessaoLavra.DataSource = null;
            grvExigenciasConcessaoLavra.DataBind();
        }

        if (regime.RequerimentoImissaoPosse != null && regime.RequerimentoImissaoPosse.Notificacoes != null)
        {
            grvNotificacaoConcessaoLavra.DataSource = regime.RequerimentoImissaoPosse.Notificacoes;
            grvNotificacaoConcessaoLavra.DataBind();
        }
        else
        {
            grvNotificacaoConcessaoLavra.DataSource = null;
            grvNotificacaoConcessaoLavra.DataBind();
        }

        lkbObservacoesConcessao.Visible = true;
        lblPopUpConcessaoLavra_ModalPopupExtender.Show();
    }

    private void EditarRequerimentoLavra(RequerimentoLavra regime)
    {
        hfIdRequerimentoLavra.Value = regime.Id.ToString();
        tbxDataEntradaRequerimentoLavra.Text = regime.Data.EmptyToMinValue();
        if (regime.Exigencias != null)
        {
            grvExigenciasRequerimentoLavra.DataSource = regime.Exigencias;
            grvExigenciasRequerimentoLavra.DataBind();
        }

        lkbObservacoesRequerimentoLavra.Visible = true;
        lblPopUpRequerimentoLavra_ModalPopupExtender.Show();
    }

    private void EditarLicenciamento(Licenciamento regime)
    {
        lkbObservacoesLicenciamento.Visible = true;
        lkbVencimentoLicenciamento.Visible = true;
        hfIdLicenciamento.Value = regime.Id.ToString();
        tbxDataAberturaLicenciamento.Text = regime.DataAbertura.EmptyToMinValue();
        tbxPublicacaoLicenciamento.Text = regime.DataPublicacao.EmptyToMinValue();
        tbxNumeroLicenciamento.Text = regime.Numero;
        if (regime.PossuePAE == true)
        {
            ckbPossuiPAE.Checked = true;
        }
        else
        {
            ckbPossuiPAE.Checked = false;
        }
        if (regime.Licencas != null)
        {
            grvLicencasLicenciamento.DataSource = regime.Licencas;
            grvLicencasLicenciamento.DataBind();
            this.CarregarSessaoLicencas(regime.Licencas);
        }
        if (regime.Exigencias != null)
        {
            grvNotificacaoLicenciamento.DataSource = regime.Exigencias;
            grvNotificacaoLicenciamento.DataBind();
        }

        if (regime.GetUltimoVencimento != null)
        {
            tbxValidadeLicenciamento.Text = regime.GetUltimoVencimento.Data.EmptyToMinValue();
            ddlEstatusVencimentoLicenciamento.SelectedValue = regime.GetUltimoVencimento.Status.Id.ToString();
        }
        if (regime.GetUltimoVencimento != null && regime.GetUltimoVencimento.Notificacoes != null)
        {
            grvNotificacaoValidadeLicenciamento.DataSource = regime.GetUltimoVencimento.Notificacoes;
            grvNotificacaoValidadeLicenciamento.DataBind();
        }
        if (regime.EntregaLicencaOuProtocolo != null && regime.EntregaLicencaOuProtocolo.Notificacoes != null)
        {
            grvNotificacaoEntregaLicencieamento.DataSource = regime.EntregaLicencaOuProtocolo.Notificacoes;
            grvNotificacaoEntregaLicencieamento.DataBind();
        }
        lblPopUpLicenciamento_ModalPopupExtender.Show();
    }

    private void EditarExtracao(Extracao regime)
    {
        lkbObservacoesExtracao.Visible = true;
        this.CarregarStatus(ddlEstatusVencimentoExtracao);

        hfIdExtracao.Value = regime.Id.ToString();

        lkbVencimentoExtracao.Visible = true;

        tbxNumeroExtracao.Text = regime.NumeroExtracao;

        tbxPublicacaoExtracao.Text = regime.DataPublicacao.EmptyToMinValue();
        tbxDataAberturaExtracao.Text = regime.DataAbertura.EmptyToMinValue();
        tbxnumeroLicencaExtracao.Text = regime.NumeroLicenca;
        tbxValidadeLicencaExtracao.Text = regime.ValidadeLicenca.EmptyToMinValue();

        grvExigenciaExtracao.DataSource = regime.Exigencias;
        grvExigenciaExtracao.DataBind();

        grvNotificacaoValidadeExtracao.DataSource = null;
        tbxValidadeExtracao.Text = "";
        if (regime.GetUltimoVencimento != null)
        {
            ddlEstatusVencimentoExtracao.SelectedValue = regime.GetUltimoVencimento.Status.Id.ToString();
            tbxValidadeExtracao.Text = regime.GetUltimoVencimento.Data.EmptyToMinValue();
            if (regime.GetUltimoVencimento.Notificacoes != null)
            {
                grvNotificacaoValidadeExtracao.DataSource = regime.GetUltimoVencimento.Notificacoes;

            }
        }
        grvNotificacaoValidadeExtracao.DataBind();
        lblPopUpExtracao_ModalPopupExtender.Show();
    }

    private void EditarAlvaraPesquisa(AlvaraPesquisa regime)
    {
        lkbObservacoesAlvara.Visible = true;

        hfAlvaraPesquisa.Value = regime.Id.ToString();
        tbxNumeroAlvaraPesquisa.Text = regime.Numero;

        lkbStstusDipem.Visible = true;
        lkbStstusTaxaAnual.Visible = true;

        grvExigenciasAlvaraPesquisa.DataSource = this.ExigenciasSelecionadas;
        grvExigenciasAlvaraPesquisa.DataBind();

        if (regime.DataPublicacao != SqlDate.MinValue)
            tbxDataPublicacaoAlvaraPesquisa.Text = regime.DataPublicacao.EmptyToMinValue();
        else
            tbxDataPublicacaoAlvaraPesquisa.Text = "";

        if (regime.DataEntregaRelatorio != SqlDate.MinValue)
            tbxDataEntragaRelatorioPesquisa.Text = regime.DataEntregaRelatorio.EmptyToMinValue();
        else
            tbxDataEntragaRelatorioPesquisa.Text = "";

        if (regime.DataAprovacaoRelatorio != SqlDate.MinValue)
            tbxDataAprovacaoRelatorioPesquisa.Text = regime.DataAprovacaoRelatorio.EmptyToMinValue();
        else
            tbxDataAprovacaoRelatorioPesquisa.Text = "";

        if (regime.AnosValidade != 0)
            tbxValidadeAlvaraPesquisa.Text = regime.AnosValidade.ToString();
        else
            tbxValidadeAlvaraPesquisa.Text = "";

        grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = null;
        if (regime.Vencimento != null && regime.Vencimento.Notificacoes != null)
        {
            grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = regime.Vencimento.Notificacoes;
        }
        grvAlvaraPesquisaValidadeNotificacoesPopUp.DataBind();

        grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = null;
        if (regime.NotificacaoPesquisaDNPM != null && regime.NotificacaoPesquisaDNPM.Notificacoes != null)
        {
            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = regime.NotificacaoPesquisaDNPM.Notificacoes;
        }
        grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataBind();

        //TAXA ANUAL HECTARE
        grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = null;
        if (regime.GetUltimaTaxaAnualHectare != null && regime.GetUltimaTaxaAnualHectare.Data != SqlDate.MinValue)
        {
            ddlEstatusTaxaAnual.SelectedValue = regime.GetUltimaTaxaAnualHectare.Status.Id.ToString();
            lblDataTaxaHectare.Text = regime.GetUltimaTaxaAnualHectare.Data.EmptyToMinValue();
            if (regime.GetUltimaTaxaAnualHectare.Notificacoes != null)
            {
                grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = regime.GetUltimaTaxaAnualHectare.Notificacoes;
            }
        }
        else
        {
            lblDataTaxaHectare.Text = "";
        }
        grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataBind();

        //REQUERIMENTO LAVRA
        grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = null;
        if (regime.RequerimentoLavra != null && regime.RequerimentoLavra.Data != SqlDate.MinValue)
        {
            lblDataLimiteRequerimentoLavra.Text = regime.RequerimentoLavra.Data.EmptyToMinValue();
            if (regime.RequerimentoLavra.Notificacoes != null)
            {
                grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = regime.RequerimentoLavra.Notificacoes;
            }
        }
        else
        {
            lblDataLimiteRequerimentoLavra.Text = "";
        }
        grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataBind();

        //LIMITE RENUNCIA
        gdvRenuncia.DataSource = null;
        if (regime.LimiteRenuncia != null && regime.LimiteRenuncia.Data != SqlDate.MinValue)
        {
            lblDataRenuncia.Text = regime.LimiteRenuncia.Data.EmptyToMinValue();
            if (regime.LimiteRenuncia.Notificacoes != null)
            {
                gdvRenuncia.DataSource = regime.LimiteRenuncia.Notificacoes;
            }
        }
        else
        {
            lblDataRenuncia.Text = "Data gerada automaticamente";
        }
        gdvRenuncia.DataBind();

        //LP TOTAL
        grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = null;
        if (regime.RequerimentoLPTotal != null && regime.RequerimentoLPTotal.Data != SqlDate.MinValue)
        {
            lblDataLimiteRequerimentoLpTotal.Text = "Relatório aprovado em " + regime.DataAprovacaoRelatorio.EmptyToMinValue() + " - Já é possível requerer a LP Poligonal";
            if (regime.RequerimentoLPTotal.Notificacoes != null)
            {
                grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = regime.RequerimentoLPTotal.Notificacoes;
            }
        }
        else
        {
            lblDataLimiteRequerimentoLpTotal.Text = "Relatório ainda não aprovado.";
        }
        grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataBind();

        //DIPEM
        grvAlvaraPesquisaDIPEM.DataSource = null;
        if (regime.DIPEM != null && regime.GetUltimoDIPEM != null && regime.GetUltimoDIPEM.Data != SqlDate.MinValue)
        {
            lblDataDIPEM.Text = regime.GetUltimoDIPEM.Data.EmptyToMinValue();
            ddlEstatusDipem.SelectedValue = regime.GetUltimoDIPEM.Status.Id.ToString();
            if (regime.GetUltimoDIPEM.Notificacoes != null)
            {
                grvAlvaraPesquisaDIPEM.DataSource = regime.GetUltimoDIPEM.Notificacoes;
            }
        }
        else
        {
            lblDataDIPEM.Text = "";
        }
        grvAlvaraPesquisaDIPEM.DataBind();
        btnPopUpAlvaraPesquisa_ModalPopupExtender.Show();
    }

    private void EditarRequerimentoPesquisa(RequerimentoPesquisa regime)
    {
        this.ExigenciasSelecionadas = regime.Exigencias;
        hfRequerimentoPesquisa.Value = regime.Id.ToString();
        tbxDataEntradaRequerimentoPesquisa.Text = regime.DataRequerimento.EmptyToMinValue();
        if (regime.Exigencias != null)
        {
            grvExigenciasRequerimentoPesquisa.DataSource = regime.Exigencias;
            grvExigenciasRequerimentoPesquisa.DataBind();
        }
        lkbObservacoesRequerimento.Visible = true;
        btnPopUpRequerimentoPesquisa_popupextender.Show();
    }

    private bool CriarRAL(DateTime dataVencimento, IList<Notificacao> notificacoes)
    {
        ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        if (p != null)
        {
            RAL ral = new RAL();
            if (p.RAL != null)
            {
                return false;
            }
            ral.ProcessoDNPM = p;
            ral.Vencimentos = new List<Vencimento>();
            Vencimento v = new Vencimento();
            v.Status = Status.ConsultarPorId(1);
            v.Notificacoes = this.CopiarNotificacoesRAL(notificacoes);
            v.Data = dataVencimento;
            ral.Vencimentos.Add(v.Salvar());
            ral = ral.Salvar();

            hfIdRalVencimentosPeriodicos.Value = ral.Id.ToString();

            return true;
        }
        return false;
    }

    private IList<Notificacao> CopiarNotificacoesRAL(IList<Notificacao> notificacoes)
    {
        if (notificacoes == null)
            return null;

        IList<Notificacao> lista = new List<Notificacao>();
        foreach (Notificacao n in notificacoes)
        {
            Notificacao nova = new Notificacao(ModuloPermissao.ModuloDNPM);
            nova.Template = TemplateNotificacao.RAL;
            nova.DiasAviso = n.DiasAviso;
            nova.Emails = n.Emails;
            nova = nova.Salvar();
            lista.Add(nova);
        }
        return lista;
    }

    private string GetEmailsSelecionados()
    {
        string emails = "";
        foreach (ListItem l in chkGruposEconomicos.Items)
            if (l.Selected && l.Value != "0")
                emails += l.Value.ToString() + ";";
        foreach (ListItem l in chkCOnsultoras.Items)
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
        if (this.NotificacoesSelecionadas == null)
            this.NotificacoesSelecionadas = new List<Notificacao>();
        if (this.NotificacoesDeExigencia == null)
            this.NotificacoesDeExigencia = new List<Notificacao>();

        string emails = this.GetEmailsSelecionados();
        if (emails.IsNotNullOrEmpty())
        {
            foreach (ListItem l in cblDias.Items)
            {
                if (l.Selected)
                {
                    Notificacao n = new Notificacao(ModuloPermissao.ModuloDNPM);
                    n.Emails = emails;
                    n.DiasAviso = l.Value.ToInt32();
                    if (EhExigencia.Value == "SIM")
                    {
                        n.Template = TemplateNotificacao.Exigencia;
                        n = n.Salvar();
                        this.NotificacoesDeExigencia.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.EXTRACAOVALIDADE)
                    {
                        n.Template = TemplateNotificacao.ValidadeExtracao;
                        n = n.Salvar();

                        if (this.SessaoExtracao == null)
                        {
                            this.SessaoExtracao = new Extracao();
                        }

                        if (this.SessaoExtracao.Vencimentos == null)
                        {
                            this.SessaoExtracao.Vencimentos = new List<Vencimento>();
                            Vencimento v = new Vencimento();
                            v.Notificacoes = new List<Notificacao>();
                            this.SessaoExtracao.Vencimentos.Add(v);
                        }

                        this.SessaoExtracao.GetUltimoVencimento.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.LICENCIAMENTOVALIDADE)
                    {
                        n.Template = TemplateNotificacao.ValidadeLicenciamento;
                        n = n.Salvar();

                        if (this.SessaoLicenciamento == null)
                            this.SessaoLicenciamento = new Licenciamento();

                        if (this.SessaoLicenciamento.Vencimentos == null)
                            this.SessaoLicenciamento.Vencimentos = new List<Vencimento>();

                        if (this.SessaoLicenciamento.Vencimentos.Count == 0)
                            this.SessaoLicenciamento.Vencimentos.Add(new Vencimento());

                        if (this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes == null)
                            this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes = new List<Notificacao>();

                        this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.LICENCIAMENTOENTREGALICENCAPROTOCOLO)
                    {
                        n.Template = TemplateNotificacao.ValidadeEntregaProtocolo;
                        n = n.Salvar();

                        if (this.SessaoLicenciamento == null)
                            this.SessaoLicenciamento = new Licenciamento();

                        if (this.SessaoLicenciamento.EntregaLicencaOuProtocolo == null)
                            this.SessaoLicenciamento.EntregaLicencaOuProtocolo = new Vencimento();

                        if (this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes == null)
                            this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes = new List<Notificacao>();

                        this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.CONCESSAODELAVRA)
                    {
                        n.Template = TemplateNotificacao.TemplateRequerimentoEmissaoPosse;
                        n = n.Salvar();
                        this.NotificacoesSelecionadas.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAVALIDADE)
                    {
                        n.Template = TemplateNotificacao.ValidadeAlvaraPesquisa;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.Vencimento == null)
                            this.SessaoAlvaraPesquisa.Vencimento = new Vencimento();

                        if (this.SessaoAlvaraPesquisa.Vencimento.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.Vencimento.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.Vencimento.Notificacoes.Add(n);

                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.DIPEM)
                    {
                        n.Template = TemplateNotificacao.DIPEM;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.DIPEM == null)
                            this.SessaoAlvaraPesquisa.DIPEM = new List<Vencimento>();

                        if (this.SessaoAlvaraPesquisa.DIPEM.Count == 0)
                            this.SessaoAlvaraPesquisa.DIPEM.Add(new Vencimento());

                        if (this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes.Add(n);

                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAINICIOPESQUISA)
                    {
                        n.Template = TemplateNotificacao.InicioPesquisa;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM == null)
                            this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM = new Vencimento();

                        if (this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.LIMITERENUNCIA)
                    {
                        n.Template = TemplateNotificacao.LimiteRenuncia;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.LimiteRenuncia == null)
                            this.SessaoAlvaraPesquisa.LimiteRenuncia = new Vencimento();

                        if (this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISATAXAANUALHECTARE)
                    {
                        n.Template = TemplateNotificacao.TemplateTaxaAnualHectare;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.TaxaAnualPorHectare == null)
                            this.SessaoAlvaraPesquisa.TaxaAnualPorHectare = new List<Vencimento>();

                        if (this.SessaoAlvaraPesquisa.TaxaAnualPorHectare.Count == 0)
                            this.SessaoAlvaraPesquisa.TaxaAnualPorHectare.Add(new Vencimento());

                        if (this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAREQUERIMENTODELAVRA)
                    {
                        n.Template = TemplateNotificacao.TemplateRequerimentoLavra;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.RequerimentoLavra == null)
                            this.SessaoAlvaraPesquisa.RequerimentoLavra = new Vencimento();

                        if (this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.GUIAUTILIZACAONOT)
                    {
                        n.Template = TemplateNotificacao.GuiaUtilizacao;
                        n = n.Salvar();
                        this.NotificacoesSelecionadas.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAREQUERIMENTOLPPOLIGONAL)
                    {
                        n.Template = TemplateNotificacao.TemplateRequerimentoLPTotal;
                        n = n.Salvar();

                        if (this.SessaoAlvaraPesquisa == null)
                            this.SessaoAlvaraPesquisa = new AlvaraPesquisa();

                        if (this.SessaoAlvaraPesquisa.RequerimentoLPTotal == null)
                            this.SessaoAlvaraPesquisa.RequerimentoLPTotal = new Vencimento();

                        if (this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes == null)
                            this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes = new List<Notificacao>();

                        this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes.Add(n);
                    }
                    else if (this.NotificacaoAberta == PopUpNotificacaoAberta.RAL)
                    {
                        RAL ral = RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());

                        n.Template = TemplateNotificacao.RAL;
                        n = n.Salvar();

                        Vencimento v = ral.GetUltimoVencimento;

                        if (v.Notificacoes == null)
                            v.Notificacoes = new List<Notificacao>();

                        v.Notificacoes.Add(n);

                        n.Vencimento = v;
                        n = n.Salvar();

                        v = v.Salvar();
                        ral.Vencimentos[ral.Vencimentos.Count - 1] = v;
                        ral.Salvar();

                        msg.CriarMensagem("Notificações Criadas com Sucesso! Salve agora o vencimento para tornar as alterações efetivas!", "Sucesso");
                    }
                }
            }
            msg.CriarMensagem("Notificações Criadas com Sucesso! Salve agora o vencimento para tornar as alterações efetivas!", "Sucesso");
            this.CarregarGridNotificacoes();
            btnPopUpbtnNotificacoPop_ModalPopupExtender.Hide();
        }
        else
        {
            msg.CriarMensagem("Selecione pelo menos um email da lista.", "Alerta", MsgIcons.Alerta);
        }
    }

    private void CarregarGridNotificacoes()
    {
        if (EhExigencia.Value == "SIM")
        {
            grvNotificacoesExigencias.DataSource = this.NotificacoesDeExigencia;
            grvNotificacoesExigencias.DataBind();
        }
        else if (this.SessaoExtracao != null && this.NotificacaoAberta == PopUpNotificacaoAberta.EXTRACAOVALIDADE)
        {
            if (this.SessaoExtracao.GetUltimoVencimento != null)
                grvNotificacaoValidadeExtracao.DataSource = this.SessaoExtracao.GetUltimoVencimento.Notificacoes;
            else
                grvNotificacaoValidadeExtracao.DataSource = null;

            grvNotificacaoValidadeExtracao.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.LICENCIAMENTOVALIDADE)
        {
            if (this.SessaoLicenciamento != null && this.SessaoLicenciamento.GetUltimoVencimento != null)
                grvNotificacaoValidadeLicenciamento.DataSource = this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes;
            else
                grvNotificacaoValidadeLicenciamento.DataSource = null;
            grvNotificacaoValidadeLicenciamento.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.LICENCIAMENTOENTREGALICENCAPROTOCOLO)
        {
            if (this.SessaoLicenciamento != null && this.SessaoLicenciamento.EntregaLicencaOuProtocolo != null)
                grvNotificacaoEntregaLicencieamento.DataSource = this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes;
            else
                grvNotificacaoEntregaLicencieamento.DataSource = null;
            grvNotificacaoEntregaLicencieamento.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.CONCESSAODELAVRA)
        {
            grvNotificacaoConcessaoLavra.DataSource = this.NotificacoesSelecionadas;
            grvNotificacaoConcessaoLavra.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAVALIDADE)
        {
            if (SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.Vencimento != null)
                grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = this.SessaoAlvaraPesquisa.Vencimento.Notificacoes;
            else
                grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = null;
            grvAlvaraPesquisaValidadeNotificacoesPopUp.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.LIMITERENUNCIA)
        {
            if (SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.LimiteRenuncia != null)
                gdvRenuncia.DataSource = this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes;
            else
                gdvRenuncia.DataSource = null;
            gdvRenuncia.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAINICIOPESQUISA)
        {
            if (SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM != null)
                grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes;
            else
                grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = null;

            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISATAXAANUALHECTARE)
        {
            if (this.SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare != null)
                grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes;
            else
                grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = null;

            grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAREQUERIMENTODELAVRA)
        {
            if (this.SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.RequerimentoLavra != null)
                grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes;
            else
                grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = null;

            grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.DIPEM)
        {
            if (this.SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.DIPEM != null && this.SessaoAlvaraPesquisa.GetUltimoDIPEM != null)
                grvAlvaraPesquisaDIPEM.DataSource = this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes;
            else
                grvAlvaraPesquisaDIPEM.DataSource = null;

            grvAlvaraPesquisaDIPEM.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.GUIAUTILIZACAONOT)
        {
            grvNotificacoesGuia.DataSource = this.NotificacoesSelecionadas;
            grvNotificacoesGuia.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.ALVARADEPESQUISAREQUERIMENTOLPPOLIGONAL)
        {
            if (this.SessaoAlvaraPesquisa != null && this.SessaoAlvaraPesquisa.RequerimentoLPTotal != null)
                grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes;
            else
                grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = null;

            grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataBind();
        }
        else if (this.NotificacaoAberta == PopUpNotificacaoAberta.RAL)
        {
            transacao.Recarregar(ref msg);
            grvNotificacoesRAL.DataSource = RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()).GetUltimoVencimento.Notificacoes;
            grvNotificacoesRAL.DataBind();
        }
    }

    private void CarregarPopUpNotificacao(bool marcar, bool exigencia, params int[] dias)
    {
        if (exigencia)
        {
            EhExigencia.Value = "SIM";
        }
        else
        {
            EhExigencia.Value = "NÃO";
        }

        cblDias.Items.Clear();
        chkGruposEconomicos.Items.Clear();
        chkCOnsultoras.Items.Clear();
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
            btnPopUpbtnNotificacoPop_ModalPopupExtender.Show();
        }

        this.CarregarListaEmails(chkEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(chkGruposEconomicos, this.CarregarEmailsCliente().Split(';'));
        this.CarregarListaEmails(chkCOnsultoras, this.CarregarEmailsConsultora().Split(';'));
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

    private string CarregarEmailsConsultora()
    {
        ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        if (p != null && p.Consultora != null)
            return p.Consultora.Contato.Email;
        return "";
    }

    private string CarregarEmailsCliente()
    {
        string email = "";
        if (ddlClientes.SelectedIndex > 0)
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlClientes.SelectedValue.ToInt32());
            if (c.Contato != null)
                email = c.Contato.Email.IsNotNullOrEmpty() ? c.Contato.Email + ";" : "";
        }
        return email;
    }

    private string CarregarEmailsEmpresa()
    {
        ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
        if (p != null && p.Empresa != null)
            return p.Empresa.Contato.Email;
        return "";
    }

    private void CarregarVencimento()
    {
        Vencimento vencimento = new Vencimento(ddlVencimentos.SelectedValue);
        vencimento = vencimento.ConsultarPorId();
        ddlStatusVencimentos.SelectedValue = vencimento.Status.Id.ToString();
        grvNotificacaoVencimentos.DataSource = vencimento.Notificacoes;
        grvNotificacaoVencimentos.DataBind();
    }

    private void CarregarVencimentos(IList<Vencimento> vencimentos, string type, int idObjetoManipulado)
    {
        if (vencimentos == null || vencimentos.Count == 0)
        {
            msg.CriarMensagem("Nenhum vencimento foi criado até o momento.", "Alerta", MsgIcons.Alerta);
            return;
        }

        hfTypeVencimento.Value = type;
        hfIDVencimento.Value = idObjetoManipulado.ToString();

        this.CarregarStatus(ddlStatusVencimentos);

        ddlVencimentos.DataSource = vencimentos.OrderByDescending(x => x.Data);
        ddlVencimentos.DataBind();

        this.CarregarVencimento();

        btnPopUpVencimentos_popupextender.Show();
    }

    private void CarregarStatus(DropDownList ddl)
    {
        if (ddl.Items.Count > 1)
            return;

        ddl.DataTextField = "Nome";
        ddl.DataValueField = "Id";
        IList<Status> lista = Status.ConsultarTodos();
        ddl.DataSource = lista;
        ddl.DataBind();

        ddl.SelectedIndex = 0;
    }

    private void SalvarVencimento()
    {
        Vencimento vencimento = Vencimento.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
        this.VerificarAlteracaoDeStatusHistoricoVencimento(ddlVencimentos.SelectedIndex);
        vencimento.Status = Status.ConsultarPorId(ddlStatusVencimentos.SelectedValue.ToInt32());
        msg.CriarMensagem("Vencimento salvo com Sucesso!", "Sucesso");
    }

    private void AddNotificacaoLimiteRenuncia()
    {
        this.NotificacaoAberta = PopUpNotificacaoAberta.LIMITERENUNCIA;
        this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
    }

    private void CarregarContratos()
    {
        ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(hfIdProcessoDNPM.Value.ToInt32());
        gvContratos.DataSource = processo != null && processo.ContratosDiversos != null ? processo.ContratosDiversos : new List<ContratoDiverso>();
        gvContratos.DataBind();
        lblQuantidadeContratosProcesso.Text = processo != null && processo.ContratosDiversos != null && processo.ContratosDiversos.Count > 0 ? processo.ContratosDiversos.Count + " contrato(s) associado(s) ao processo." : "Nenhum contrato está associado a este processo.";
    }

    //alterar esse metodo abaixo
    public void PesquisarContratosDiversos()
    {
        IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);

        IList<ContratoDiverso> contratos = ContratoDiverso.ConsultarPorNumeroEObjeto(ddlClientes.SelectedValue.ToInt32(), tbxNumeroContratoDiverso.Text, tbxObjetoContratoDiverso.Text, this.ConfiguracaoModuloContratos.Tipo, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos);

        this.SetarSessaoContratos(contratos);
        gdvContratosSelecao.DataSource = contratos != null ? contratos : new List<ContratoDiverso>();
        gdvContratosSelecao.DataBind();
        lblSemContratos.Visible = contratos != null && contratos.Count > 0 ? false : true;
    }

    private void SetarSessaoContratos(IList<ContratoDiverso> contratos)
    {
        if (contratos == null)
        {
            this.IdContratosConsultados = null;
        }
        else
        {
            if (IdContratosConsultados == null)
                this.IdContratosConsultados = new List<int>();
            foreach (ContratoDiverso item in contratos)
            {
                this.IdContratosConsultados.Add(item.Id);
            }
        }
    }

    private void RenovarVencimentosPeriodicos()
    {
        IList<DateTime> datasDeRenovacao = new List<DateTime>();

        foreach (ListItem l in chkVencimentosPeriodicos.Items)
            if (l.Selected && l.Value != "")
                datasDeRenovacao.Add(Convert.ToDateTime(l.Value.ToString()));

        RenovarPeriodicos.RenovarVencimentosPeriodicos(datasDeRenovacao, hfIdTipoVencimentoPeriodico.Value, hfIdItemVencimentoPeriodico.Value.ToInt32());
        lblRenovacaoVencimentosPeriodicosDatas_popupextender.Hide();
        msg.CriarMensagem("Vencimentos renovados com sucesso", "Sucesso", MsgIcons.Sucesso);
        for (int i = this.ItensRenovacao.Count - 1; i > -1; i--)
        {
            if (this.ItensRenovacao[i].idItem == hfIdItemVencimentoPeriodico.Value.ToInt32() && this.ItensRenovacao[i].tipoItem == hfIdTipoVencimentoPeriodico.Value)
            {
                this.ItensRenovacao.Remove(this.ItensRenovacao[i]);
            }
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }
        else
        {
            lblRenovacaoVencimentosPeriodicos_popupextender.Hide();
            this.fecharPopUpsDaRenovacaoPeriodica(hfIdTipoVencimentoPeriodico.Value);
        }
    }

    public void fecharPopUpsDaRenovacaoPeriodica(string tipoItem)
    {
        if (tipoItem.ToUpper() == "EXIGENCIA")
        {
            btnPopUpCadastroExigencia_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "GUIAUTILIZACAO")
        {
            btnPopUpGuia_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "TAXAANUALALVARA")
        {
            btnPopUpAlvaraPesquisa_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "DIPEM")
        {
            btnPopUpAlvaraPesquisa_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "VALIDADELICENCIAMENTO")
        {
            lblPopUpLicenciamento_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "EXTRACAO")
        {
            lblPopUpExtracao_ModalPopupExtender.Hide();
        }
    }

    private void ExibirDatasVencimentosPeriodicosRenovacao(int diasRenovacao, IList<Vencimento> vencimentos)
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

    public int ObterDiasEntreAsRenovacoes(IList<Vencimento> vencimentos)
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

    private IList<Vencimento> RetornarVencimentosDoItemDaRenovacao(string tipoItem, int idItem)
    {
        if (tipoItem.ToUpper() == "EXIGENCIA")
        {
            Exigencia e = Exigencia.ConsultarPorId(idItem);
            return e.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "GUIAUTILIZACAO")
        {
            GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(idItem);
            return guia.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "TAXAANUALALVARA")
        {
            AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(idItem);
            return alvara.TaxaAnualPorHectare;
        }
        else if (tipoItem.ToUpper() == "DIPEM")
        {
            AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(idItem);
            return alvara.DIPEM;
        }
        else if (tipoItem.ToUpper() == "VALIDADELICENCIAMENTO")
        {
            Licenciamento licenciamento = Licenciamento.ConsultarPorId(idItem);
            return licenciamento.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "EXTRACAO")
        {
            Extracao extracao = Extracao.ConsultarPorId(idItem);
            return extracao.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "RAL")
        {
            RAL ral = RAL.ConsultarPorId(idItem);
            return ral.Vencimentos;
        }

        return null;
    }

    private void SalvarItemDaExigencia(Exigencia e)
    {
        if (e != null)
        {
            if (hfItemExigencia.Value.ToUpper() == "GUIAUTILIZACAO")
            {
                GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIdGuiaUtilizacao.Value.ToInt32());
                e.GuiaUtilizacao = guia;
                e = e.Salvar();
            }
            else if (hfItemExigencia.Value.ToUpper() == "REQPESQUISA")
            {
                RequerimentoPesquisa requerimento = RequerimentoPesquisa.ConsultarPorId(hfRequerimentoPesquisa.Value.ToInt32());
                e.Regime = requerimento;
                e = e.Salvar();
            }
            else if (hfItemExigencia.Value.ToUpper() == "ALVARAPESQUISA")
            {
                AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(hfAlvaraPesquisa.Value.ToInt32());
                e.Regime = alvara;
                e = e.Salvar();
            }
            else if (hfItemExigencia.Value.ToUpper() == "REQLAVRA")
            {
                RequerimentoLavra requerimento = RequerimentoLavra.ConsultarPorId(hfIdRequerimentoLavra.Value.ToInt32());
                e.Regime = requerimento;
                e = e.Salvar();
            }
            else if (hfItemExigencia.Value.ToUpper() == "CONCESSAOLAVRA")
            {
                ConcessaoLavra concessao = ConcessaoLavra.ConsultarPorId(hfIdConcessaoLavra.Value.ToInt32());
                e.Regime = concessao;
                e = e.Salvar();
            }
            else if (hfItemExigencia.Value.ToUpper() == "LICENCIAMENTO")
            {
                Licenciamento licenciamento = Licenciamento.ConsultarPorId(hfIdLicenciamento.Value.ToInt32());
                e.Regime = licenciamento;
                e = e.Salvar();
            }
            else if (hfItemExigencia.Value.ToUpper() == "EXTRACAO")
            {
                Extracao extracao = Extracao.ConsultarPorId(hfIdExtracao.Value.ToInt32());
                e.Regime = extracao;
                e = e.Salvar();
            }
        }
    }

    #endregion

    #region ___________Bindings____________

    public string BindOrgao(Object o)
    {
        Licenca l = (Licenca)o;
        l = l.ConsultarPorId();
        return l.Processo != null ? l.Processo.OrgaoAmbiental != null ? l.Processo.OrgaoAmbiental.Nome : "" : "";
    }

    public string BindTipo(Object o)
    {
        Licenca l = (Licenca)o;
        l = l.ConsultarPorId();
        return l.TipoLicenca != null ? l.TipoLicenca.Sigla : "";
    }

    public string BindDataValidadeLicenca(Object o)
    {
        Licenca l = (Licenca)o;
        if (l.GetUltimoVencimento != null)
            return l.GetUltimoVencimento.Data.EmptyToMinValue();

        return "Não informada";
    }

    public string BindDatadeVencimento(Object o)
    {
        Exigencia e = (Exigencia)o;
        return e.GetUltimoVencimento.Data.EmptyToMinValue();
    }

    public string BindLinkExigencias(Object o)
    {
        Exigencia e = ((Exigencia)o);
        return e.LinkArquivo;
    }

    public bool BindLinkExigenciasEnable(Object o)
    {
        Exigencia e = ((Exigencia)o);
        if (e.LinkArquivo != null && e.LinkArquivo != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string BindAnexoRequerimentoPesquisa(Object o)
    {
        return ((Exigencia)o).LinkArquivo;
    }

    public string BindVencimentoRequerimentoPesquisa(Object o)
    {
        Exigencia e = (Exigencia)o;
        return e.GetUltimoVencimento.Data.EmptyToMinValue();
    }

    public bool BindEnableRenovacao(Object o)
    {
        Exigencia e = (Exigencia)o;
        if (e.GetUltimoVencimento != null)
            return e.GetUltimoVencimento.Periodico;
        else
            return false;
    }

    public bool BindVisibleEdicaoExigencia(Object o)
    {
        if (this.ConfiguracaoModuloDNPM == null)
            return false;

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            return this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);

        Exigencia e = (Exigencia)o;
        if (e.Regime != null && e.Regime.ProcessoDNPM != null)
        {
            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                return e.Regime.ProcessoDNPM.Empresa != null && this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 && this.EmpresasPermissaoEdicaoModuloNPM.Contains(e.Regime.ProcessoDNPM.Empresa);

            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                return e.Regime.ProcessoDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM.Count > 0 && this.ProcessosPermissaoEdicaoModuloDNPM.Contains(e.Regime.ProcessoDNPM);

            return false;
        }
        else
            return false;
    }

    public bool BindVisibleEdicaoExigenciaGuiaUtilizacao(Object o)
    {
        if (this.ConfiguracaoModuloDNPM == null)
            return false;

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            return this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);

        Exigencia e = (Exigencia)o;
        if (e.GuiaUtilizacao != null && e.GuiaUtilizacao.ProcessoDNPM != null)
        {
            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                return e.GuiaUtilizacao.ProcessoDNPM.Empresa != null && this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 && this.EmpresasPermissaoEdicaoModuloNPM.Contains(e.GuiaUtilizacao.ProcessoDNPM.Empresa);

            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                return e.GuiaUtilizacao.ProcessoDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM.Count > 0 && this.ProcessosPermissaoEdicaoModuloDNPM.Contains(e.GuiaUtilizacao.ProcessoDNPM);

            return false;
        }
        else
            return false;
    }

    public bool BindVisibleEdicaoNotificacaoRal(Object o)
    {
        if (this.ConfiguracaoModuloDNPM == null)
            return false;

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            return this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);

        Notificacao n = (Notificacao)o;
        if (n.Vencimento != null && n.Vencimento.Ral != null && n.Vencimento.Ral.ProcessoDNPM != null)
        {
            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                return n.Vencimento.Ral.ProcessoDNPM.Empresa != null && this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 && this.EmpresasPermissaoEdicaoModuloNPM.Contains(n.Vencimento.Ral.ProcessoDNPM.Empresa);

            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                return n.Vencimento.Ral.ProcessoDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM.Count > 0 && this.ProcessosPermissaoEdicaoModuloDNPM.Contains(n.Vencimento.Ral.ProcessoDNPM);

            return false;
        }
        else
            return false;
    }

    public string BindImagemRenovacao(Object o)
    {
        Exigencia e = (Exigencia)o;
        if (e.Vencimentos != null && e.Vencimentos.Count > 0)
            if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
                return "~/imagens/calendar.png";
            else
                return "~/imagens/calendar_d.png";
        return "~/imagens/calendar_d.png";
    }

    public string BindToolTipoRenovacao(Object o)
    {
        Exigencia e = (Exigencia)o;

        if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
        {
            return "Clique para renovar.";
        }
        else
        {
            return "Esta condicional não é Periodica.";
        }
    }

    public String bindingObjeto(Object o)
    {
        ContratoDiverso cont = (ContratoDiverso)o;
        return cont.Objeto;
    }

    public String bindingStatusContrato(Object o)
    {
        ContratoDiverso cont = (ContratoDiverso)o;
        return cont.StatusContratoDiverso != null ? cont.StatusContratoDiverso.Nome : "";
    }

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

    #region __________Pre-Render___________

    protected void ibtnRenovar_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void ibtnRenovar_PreRender1(object sender, EventArgs e)
    {
        
    }

    protected void ibtnRenovar_PreRender2(object sender, EventArgs e)
    {
        
    }

    protected void ibtnRenovar_PreRender3(object sender, EventArgs e)
    {
        
    }

    protected void ibtnRenovar_PreRender4(object sender, EventArgs e)
    {
        
    }

    protected void ibtnRenovar_PreRender5(object sender, EventArgs e)
    {
        
    }

    protected void ibtnAddLicencaProcesso0_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void ibtnRenovar_PreRender6(object sender, EventArgs e)
    {
        
    }

    protected void btnRenovarValidadeGuia_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void btnRenovarValidadeRAL_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void btnRenovarValidadeExtracao_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void btnRenovarValidadeLicenciamento_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void btnRenovarAlvara_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void btnRenovarValidadeLicenciamento0_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir as licenças associadas?");
    }

    protected void ibtnExcluir14_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir30_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir4_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) licença(s) associada(s)?");
    }

    protected void ibtnExcluir5_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir16_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir31_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir32_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir33_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir33_PreRender1(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir34_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir35_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir6_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir10_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir9_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir3_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) licença(s) associada(s)?");
    }

    protected void ibtnExcluir8_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir11_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir12_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir0_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) exigência(s) selecionada(s)?");
    }

    protected void ibtnExcluir2_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluir1_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir a(s) notificações(s) selecionada(s)?");
    }

    protected void ibtnExcluirContratos_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir o(s) contrato(s) associado(s)?");
    }

    #endregion

    #region __________ Triggers ___________

    protected void lkbProrrogacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upProrrogacoes);
    }

    protected void btnEnviarHistoricoPorEmail_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEnvioHistorico);
    }

    protected void lkbObservacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upObs);
    }

    protected void btnSalvarExigencias0_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void lkbSubstancias_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSubstancias);
    }

    protected void lkbStstusGuia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void lkbStstusDipem_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void btnSalvarRenovacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void ibtnAtualizarVisao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
    }

    protected void ibtnAddLicencaProcesso0_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void grvExigenciasGuiaUtilizacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void grvRequerimentosPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void grvAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void grvDataRequerimentoLavraExigencias_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void grvConcessaoLavraExigencias_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void grvExigenciasLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void grvExigenciasExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPFrameArquivos);
    }

    protected void btnSalvarProcessoDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRequerimentoPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpRequerimentoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpExtracao);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
    }

    protected void btnSalvarRequerimentoPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
    }

    protected void tbnSalvarAlvaraDePesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
    }

    protected void btnSalvarRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
    }

    protected void btnSalvarConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
    }

    protected void btnSalvarLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void btnSalvarExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void btnSalvarGuia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void ddlEmpresaDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedIndexChanged", upUploadProcesso);
    }

    protected void ibtnAddNotificacaoDIPEM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void lkbOpcoesProcessoEditar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastrarProcessosDNPM);
    }

    protected void lkbRegimeEditar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRequerimentoPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpRequerimentoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpConcessaoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpExtracao);
    }

    protected void lkbOpcoesGuiaEditar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopGuiaUtilizacao);
    }

    private void CriarTriggersExigenciasNotificacoes(Control sender)
    {
        //IMPORTANTE SETAR O ATRIBUTO POPUPABERTO QUANDO CLICAR NO MENU DE OPÇOES

        if (this.PopUpAberto == PopUpAbertoENUM.GUIA)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upPopGuiaUtilizacao);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.REQUERIMENTOPESQUISA)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upRequerimentoPesquisa);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.REQUERIMENTOLAVRA)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upPopUpRequerimentoLavra);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.ALVARAPESQUISA)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upAlvaraPesquisa);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.CONCESSAOLAVRA)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upPopUpConcessaoLavra);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.EXTRACAO)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upPopUpExtracao);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.LICENCIAMENTO)
        {
            WebUtil.InserirTriggerDinamica(sender, "Click", upPopUpLicenciamento);
            return;
        }
    }

    protected void btnSalvarExigencias_Init(object sender, EventArgs e)
    {
        this.CriarTriggersExigenciasNotificacoes((Control)sender);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void btnSalvarNotificacaoPop_Init(object sender, EventArgs e)
    {
        this.CriarTriggersExigenciasNotificacoes((Control)sender);
    }

    protected void lkbOpcoesGuiaNovo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopGuiaUtilizacao);
    }

    protected void trvProcessos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedNodeChanged", upVisoes);
    }

    protected void ibtnNotificacoesPopGuia_Init1(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoValidadeAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoRALPop_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void grvNotificacoesExigencias_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void lkbRegimeNovo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSelecionarRegime);
    }

    protected void btnNovoRequerimentoPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRequerimentoPesquisa);
    }

    protected void btnNovoAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
    }

    protected void btnNovoRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpRequerimentoLavra);
    }

    protected void btnNovoConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpConcessaoLavra);
    }

    protected void btnNovoExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpExtracao);
    }

    protected void ibtnAddLicencaProcesso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSelecionarLicenca);
    }

    protected void btnNovoLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
    }

    protected void lkbOpcoesProcessoNovo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastrarProcessosDNPM);
    }

    protected void btnAdicionarExigenciaRequerimentoPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void btnAdicionarNotificacaoRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarNotificacaoValidadeExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarNotificacaoEntregaExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarExigenciasExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void btnAdicionarNotificacaoValidadeLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarNotificacaoEntregaLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarExigenciasLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void btnSelecionarLicencasAssociadasLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSelecionarLicenca);
    }

    protected void btnAdicionarNotificacaoConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarExigenciaConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void btnAdicionarLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastrarProcessosDNPM);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSelecionarLicenca);
    }

    protected void ibtnAddNotificacaoInicioPesquisa0_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoTaxaAnualHectareAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoRequerimentoLavraAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnAdicionarNotificacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnSalvarNotificacao_Init(object sender, EventArgs e)
    {
        //IMPORTANTE SETAR O ATRIBUTO POPUPABERTO QUANDO CLICAR NO MENU DE OPÇOES
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsGrupo();marcarEmailsEmpresa();marcarEmailsConsultora();</script>", false);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);


        if (this.PopUpAberto == PopUpAbertoENUM.EXTRACAO)
        {
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpExtracao);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.LICENCIAMENTO)
        {
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpLicenciamento);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.CONCESSAOLAVRA)
        {
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpConcessaoLavra);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.ALVARAPESQUISA)
        {
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAlvaraPesquisa);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.GUIA)
        {
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopGuiaUtilizacao);
            return;
        }
        else if (this.PopUpAberto == PopUpAbertoENUM.RAL)
        {
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
            return;
        }


    }

    protected void ibtnAddNotificacaoValidadeLPTotal_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void lkbVencimentoExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void lkbVencimentoLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void lkbVencimentoExigencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void lkbStstusTaxaAnual_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void gdvContratosSelecao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPSelecaoContratos);
    }

    protected void btnSalvarContratoDiverso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPListagemContratosDiversos);
    }

    protected void btnSelecionarMaisContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPSelecaoContratos);
    }

    protected void gvContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", UPListagemContratosDiversos);
    }

    protected void btnAbrirContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPListagemContratosDiversos);
    }

    protected void rptRenovacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "ItemCommand", upDatasRenovacao);
    }

    protected void btnAdicionarExigenciaAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void btnRenovarVencimentosPeriodicos_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsEmpresa();</script>", false);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void btnUploadGuiaUtilizacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadProcessoDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadRequerimentopesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadExigencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadProcesso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadRequerimentoPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadAlvaraPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadGUIA_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadRAL_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    #endregion

    #region ___________Eventos_____________

    protected void btnAddProrrogacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarProrrogacao();
            tbxPrazoAdicional.Text = "";
            tbxDataProtocoloAdicional.Text = "";
            tbxProtocoloAdicional.Text = "";
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

    protected void grvProrrogacoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(hfIdExigencia.Value.ToInt32());

            foreach (GridViewRow item in ((GridView)sender).Rows)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    ProrrogacaoPrazo p = ProrrogacaoPrazo.ConsultarPorId(((GridView)sender).DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (p != null)
                    {
                        if (exigencia != null && exigencia.GetUltimoVencimento != null && exigencia.GetUltimoVencimento.ProrrogacoesPrazo != null)
                        {
                            for (int i = exigencia.GetUltimoVencimento.ProrrogacoesPrazo.Count - 1; i > -1; i--)
                            {
                                if (exigencia.GetUltimoVencimento.ProrrogacoesPrazo[i].Id == p.Id)
                                    exigencia.GetUltimoVencimento.ProrrogacoesPrazo.Remove(exigencia.GetUltimoVencimento.ProrrogacoesPrazo[i]);
                            }
                        }
                        p.Excluir();
                    }
                }
            exigencia = this.VerificarNovaDataVencimentoComExclusaoDeProrrogacao(exigencia);
            grvProrrogacoes.DataSource = exigencia.GetUltimoVencimento.ProrrogacoesPrazo.OrderByDescending(i => i.Id).ToList(); ;
            grvProrrogacoes.DataBind();
            lkbProrrogacao.Text = "Abrir Prorrogações - [" + exigencia.GetUltimoVencimento.ProrrogacoesPrazo.Count + "] Prorrogações.";
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

    private Exigencia VerificarNovaDataVencimentoComExclusaoDeProrrogacao(Exigencia exigencia)
    {
        if (exigencia.GetUltimoVencimento.ProrrogacoesPrazo != null && exigencia.GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
        {
            exigencia.GetUltimoVencimento.Data = exigencia.GetUltimoVencimento.GetUltimaProrrogacao.DataProtocoloAdicional.AddDays(exigencia.GetUltimoVencimento.GetUltimaProrrogacao.PrazoAdicional);
        }
        else
        {
            exigencia.GetUltimoVencimento.Data = this.CalcularDataVencimentoExigencia();
        }

        exigencia.Vencimentos[exigencia.Vencimentos.Count - 1] = exigencia.Vencimentos[exigencia.Vencimentos.Count - 1].Salvar();
        return exigencia.Salvar();
    }

    private DateTime CalcularDataVencimentoExigencia()
    {
        return tbxDataPublicacaoExigencias.Text.ToSqlDateTime().AddDays(tbxDiasPrazo.Text.ToInt32());
    }

    protected void btnAddProrrogacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void ibtnExcluir6_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void ddlStatusExigencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatusExigencia.SelectedItem.Text == "Pedido de Prazo" && hfIdExigencia.Value.ToInt32() > 0)
            {
                tbxPrazoAdicional.Text = "";
                tbxDataProtocoloAdicional.Text = "";
                tbxProtocoloAdicional.Text = "";
                this.CarregarProrrogacoes();
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

    protected void lxbPesquisarContratos_Click(object sender, EventArgs e)
    {
        try
        {
            this.PesquisarContratosDiversos();
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

    protected void ddlStatusExigencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedIndexChanged", upProrrogacoes);
    }

    protected void ibtnExcluir6_PreRender1(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir este(s) item(ns)?");
    }

    protected void btnEnviarHistoricoPorEmail_Click(object sender, EventArgs e)
    {
        try
        {
            chkGruposHistorico.Items.Clear();
            chkEmpresaHistorico.Items.Clear();
            chkConsultoraHistorico.Items.Clear();

            lblPopEnviarHistorio_popupextender.Show();

            this.CarregarListaEmails(chkEmpresaHistorico, this.CarregarEmailsEmpresa().Split(';'));
            this.CarregarListaEmails(chkGruposHistorico, this.CarregarEmailsCliente().Split(';'));
            this.CarregarListaEmails(chkConsultoraHistorico, this.CarregarEmailsConsultora().Split(';'));
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

    protected void btnEnviarHistorico_Click(object sender, EventArgs e)
    {
        try
        {
            if (tbxEmailsHistorico.Text.Trim().Replace(" ", "") != "")
            {
                if (!WebUtil.ValidarEmailInformado(tbxEmailsHistorico.Text))
                {
                    msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }

            this.EnviarHistoricoPorEmail();
            lblPopEnviarHistorio_popupextender.Hide();
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

    protected void lkbProrrogacao_Click(object sender, EventArgs e)
    {
        try
        {
            tbxPrazoAdicional.Text = "";
            tbxDataProtocoloAdicional.Text = "";
            tbxProtocoloAdicional.Text = "";
            this.CarregarProrrogacoes();
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

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderlblProrrogacao.Hide();
    }

    protected void ibtnAddProrrogacao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.SalvarProrrogacao();
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

    protected void lkbObservacoesRequerimento_Click(object sender, EventArgs e)
    {
        try
        {
            RequerimentoPesquisa requerimento = RequerimentoPesquisa.ConsultarPorId(hfRequerimentoPesquisa.Value.ToInt32());
            grvHistoricos.DataSource = requerimento.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "RequerimentoPesquisa";
            hfIDObs.Value = requerimento.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesRequerimentoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            RequerimentoLavra requerimento = RequerimentoLavra.ConsultarPorId(hfIdRequerimentoLavra.Value.ToInt32());
            grvHistoricos.DataSource = requerimento.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "RequerimentoLavra";
            hfIDObs.Value = requerimento.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesConcessao_Click(object sender, EventArgs e)
    {
        try
        {
            ConcessaoLavra concessao = ConcessaoLavra.ConsultarPorId(hfIdConcessaoLavra.Value.ToInt32());
            grvHistoricos.DataSource = concessao.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "ConcessaoLavra";
            hfIDObs.Value = concessao.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void ibtnNotificacaoRenuncia_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.AddNotificacaoLimiteRenuncia();
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

    protected void gdvRenuncia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            gdvRenuncia.PageIndex = 1;
            foreach (GridViewRow item in gdvRenuncia.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(gdvRenuncia.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            gdvRenuncia.DataSource = this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes;
            gdvRenuncia.DataBind();

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

    protected void gdvRenuncia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaDIPEM.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaDIPEM.DataSource = this.SessaoAlvaraPesquisa.LimiteRenuncia.Notificacoes;
        grvAlvaraPesquisaDIPEM.DataBind();
    }

    protected void lkbLimiteRenucia_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.LimiteRenuncia != null)
                {
                    lblDataVencimento.Text = regime.LimiteRenuncia.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.LimiteRenuncia.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbObservacoes_Click(object sender, EventArgs e)
    {
        try
        {
            GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(hfIdGuiaUtilizacao.Value.ToInt32());
            if (guia.Historicos != null && guia.Historicos.Count > 0)
                grvHistoricos.DataSource = guia.Historicos.OrderByDescending(i => i.Id).ToList();
            else
                grvHistoricos.DataSource = null;

            grvHistoricos.DataBind();
            hfTypeObs.Value = "GuiaUtilizacao";
            hfIDObs.Value = guia.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesAlvara_Click(object sender, EventArgs e)
    {
        try
        {
            AlvaraPesquisa alv = AlvaraPesquisa.ConsultarPorId(hfAlvaraPesquisa.Value.ToInt32());
            grvHistoricos.DataSource = alv.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "AlvaraPesquisa";
            hfIDObs.Value = alv.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesLicenciamento_Click(object sender, EventArgs e)
    {
        try
        {
            Licenciamento lic = Licenciamento.ConsultarPorId(hfIdLicenciamento.Value.ToInt32());
            grvHistoricos.DataSource = lic.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "Licenciamento";
            hfIDObs.Value = lic.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesExtracao_Click(object sender, EventArgs e)
    {
        try
        {
            Extracao ext = Extracao.ConsultarPorId(hfIdExtracao.Value.ToInt32());
            grvHistoricos.DataSource = ext.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "Extracao";
            hfIDObs.Value = ext.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesExigencia_Click(object sender, EventArgs e)
    {
        try
        {
            Exigencia exig = Exigencia.ConsultarPorId(hfIdExigencia.Value.ToInt32());
            grvHistoricos.DataSource = exig.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "Exigencia";
            hfIDObs.Value = exig.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();

            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void ibtnSubstancias_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!tbxDataSubstancia.Text.IsDate())
                return;

            Substancia s = new Substancia();
            if (tbxDataSubstancia.Text.Trim() != "")
                s.Data = tbxDataSubstancia.Text.ToSqlDateTime();
            s.Protocolo = tbxProtocoloSubstancia.Text;
            s.Nome = tbxNomeSubstancia.Text;

            s.ProcessoDNPM = ProcessoDNPM.ConsultarPorId(hfIdProcessoDNPM.Value.ToInt32());

            s = s.Salvar();

            if (this.SubstanciaSelecionadas == null)
                this.SubstanciaSelecionadas = new List<int>();

            this.SubstanciaSelecionadas.Add(s.Id);

            gdvSubstancias.DataSource = this.RecarregarSubstancias(); ;
            gdvSubstancias.DataBind();

            tbxNomeSubstancia.Text = "";
            tbxDataSubstancia.Text = "";
            tbxProtocoloSubstancia.Text = "";
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

    protected void lkbSubstancias_Click(object sender, EventArgs e)
    {
        try
        {
            tbxNomeSubstancia.Text = "";
            tbxDataSubstancia.Text = "";
            tbxProtocoloSubstancia.Text = "";
            if (this.SubstanciaSelecionadas != null)
                gdvSubstancias.DataSource = this.RecarregarSubstancias();
            else
                gdvSubstancias.DataSource = null;
            gdvSubstancias.DataBind();


            lblSubstancias_ModalPopupExtender.Show();
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

    protected void gdvSubstancias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Substancia subs = new Substancia();
            subs.Id = gdvSubstancias.DataKeys[e.RowIndex].Value.ToString().ToInt32();
            subs.Excluir();

            this.SubstanciaSelecionadas.Remove(subs.Id);
            gdvSubstancias.DataSource = this.RecarregarSubstancias();
            gdvSubstancias.DataBind();
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

    protected void grvNotificacoesRAL_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Notificacao n = new Notificacao(grvNotificacoesRAL.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            n.Excluir();
            transacao.Recarregar(ref msg);
            this.ExibirRAL(RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore()));
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

    protected void lkbVenc_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanelNot);
    }

    protected void lkbVenc0_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.Vencimento != null)
                {
                    lblDataVencimento.Text = regime.Vencimento.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.Vencimento.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.NotificacaoPesquisaDNPM != null)
                {
                    lblDataVencimento.Text = regime.NotificacaoPesquisaDNPM.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.NotificacaoPesquisaDNPM.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc1_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.GetUltimaTaxaAnualHectare != null)
                {
                    lblDataVencimento.Text = regime.GetUltimaTaxaAnualHectare.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimaTaxaAnualHectare.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc2_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.RequerimentoLavra != null)
                {
                    lblDataVencimento.Text = regime.RequerimentoLavra.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.RequerimentoLavra.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc3_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.RequerimentoLPTotal != null)
                {
                    lblDataVencimento.Text = regime.RequerimentoLPTotal.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.RequerimentoLPTotal.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc4_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("ALP_"))
            {
                AlvaraPesquisa regime = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.GetUltimoDIPEM != null)
                {
                    lblDataVencimento.Text = regime.GetUltimoDIPEM.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoDIPEM.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc5_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("CO_"))
            {
                ConcessaoLavra regime = ConcessaoLavra.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.RequerimentoImissaoPosse != null)
                {
                    lblDataVencimento.Text = regime.RequerimentoImissaoPosse.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.RequerimentoImissaoPosse.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc6_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("LI_"))
            {
                Licenciamento regime = Licenciamento.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.GetUltimoVencimento != null)
                {
                    lblDataVencimento.Text = regime.GetUltimoVencimento.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoVencimento.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc7_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("LI_"))
            {
                Licenciamento regime = Licenciamento.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.EntregaLicencaOuProtocolo != null)
                {
                    lblDataVencimento.Text = regime.EntregaLicencaOuProtocolo.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.EntregaLicencaOuProtocolo.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc8_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("EX_"))
            {
                Extracao regime = Extracao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (regime.GetUltimoVencimento != null)
                {
                    lblDataVencimento.Text = regime.GetUltimoVencimento.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoVencimento.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc9_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedValue.Contains("GUIA_"))
            {
                GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                if (guia.GetUltimoVencimento != null)
                {
                    lblDataVencimento.Text = guia.GetUltimoVencimento.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = guia.GetUltimoVencimento.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void ibtnRemoverVencimento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
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

    protected void ibtnAtualizarVisao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hfIdProcessoDNPM.Value = "0";
            this.ExibirVisoes();
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

    protected void ddlCidadeLicenca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCidadeLicenca.SelectedIndex > 0)
                this.CarregarOrgaosLicenca(OrgaoMunicipal.ConsultarPorCidade(ddlCidadeLicenca.SelectedValue.ToInt32()));
            else
            {
                ddlOrgaoLicenca.Items.Clear();
                ddlOrgaoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

    protected void ddlOrgaoLicenca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarArvoreSelecionarLicenca();
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

    protected void ddlTipoOrgaoLicenca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.LimparDDL(ddlOrgaoLicenca);

            if (ddlEstadoLicenca.Items.Count <= 1)
            {
                ddlEstadoLicenca.DataSource = Estado.ConsultarTodos();
                ddlEstadoLicenca.DataBind();
                ddlEstadoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
            }
            ddlEstadoLicenca.SelectedIndex = 0;

            ddlCidadeLicenca.Items.Clear();
            ddlCidadeLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            if (ddlTipoOrgaoLicenca.SelectedIndex == 0)
            {
                divEstadoProcesso.Visible = true;
                divCidadeProcesso.Visible = false;
            }
            else if (ddlTipoOrgaoLicenca.SelectedIndex == 1)
            {
                divEstadoProcesso.Visible = false;
                divCidadeProcesso.Visible = false;
                this.CarregarOrgaosLicenca(OrgaoFederal.ConsultarTodos());
            }
            else if (ddlTipoOrgaoLicenca.SelectedIndex == 2)
            {
                divEstadoProcesso.Visible = true;
                divCidadeProcesso.Visible = true;
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

    protected void ibtnAbrirDownloadExigenciasExtracao_PreRender(object sender, EventArgs e)
    {

    }

    protected void ddlEstadoLicenca_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            ddlOrgaoLicenca.Items.Clear();
            ddlOrgaoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));

            if (ddlTipoOrgaoLicenca.SelectedIndex == 0)
                this.CarregarOrgaosLicenca(OrgaoEstadual.ConsultarPorEstado(ddlEstadoLicenca.SelectedValue.ToInt32()));
            else if (ddlTipoOrgaoLicenca.SelectedIndex == 2)
                this.CarregarCidade(ddlEstadoLicenca.SelectedValue.ToInt32());


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

    protected void btnRenovarValidadeLicenciamento0_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatus(ddlStatusRenovacao);
            hfTipoRenovacao.Value = "VENCIMENTODIPEM";
            tbxDiasValidadeRenovacao.Visible = true;
            tbxDataValidadeRenovacao.Visible = false;
            tbxDataValidadeRenovacao.Enabled = true;
            tbxDiasValidadeRenovacao.Text = "1";
            lblValidadeRenovacao.Text = "Renovar por mais(anos):";
            btnPopUpRenovacao_ModalPopupExtender.Show();
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

    protected void lbtnDownloadProcesso_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdDoProcessoSelecionadoNaArvore() + "&tipo=ProcessoDNPM"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadRequerimentoPesquisa_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=RequerimentoPesquisa"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadAlvaraPesquisa_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=AlvaraPesquisa"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadRequerimentoLavra_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=RequerimentoLavra"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadConcessaoLavra_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=ConcessaoLavra"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadLicenciamento_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=Licenciamento"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadExtracao_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=Extracao"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void lbtnDownloadGUIA_Click(object sender, EventArgs e)
    {
        this.ArquivosUpload = null;

        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.RetornarIdSelecionadoNaArvore() + "&tipo=GuiaUtilizacao"));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ibtnExigenciaGuia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void ibtnAddNotificacaoRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void ibtnExigenciaGuia_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NovaExigencia();
            lblExigenciaCondicionante.Text = "Condicionantes";
            hfItemExigencia.Value = "GUIAUTILIZACAO";
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void btnSelecionarLicencasAssociadasLicenciamento_Click(object sender, ImageClickEventArgs e)
    {
        hfTipo.Value = "LICENCIAMENTO";
        this.CarregarPopUpSelecionarLicenca();
        btnSelecionarLicencaHide_ModalPopupExtender.Show();
    }

    protected void btnAdicionarExigenciaConcessaoLavra_Click(object sender, ImageClickEventArgs e)
    {
        btnPopUpCadastroExigencia_ModalPopupExtender.Show();
    }

    protected void grvExigenciasRequerimentoPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExigenciasRequerimentoPesquisa.PageIndex = e.NewPageIndex;
        grvExigenciasRequerimentoPesquisa.DataSource = this.ExigenciasSelecionadas;
        grvExigenciasRequerimentoPesquisa.DataBind();
    }

    protected void lkbOpcoesProcessoExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExcluirProcesso();
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

    protected void btnAdicionarExigenciasLicenciamento_Click(object sender, ImageClickEventArgs e)
    {
        this.NovaExigencia();
        hfItemExigencia.Value = "LICENCIAMENTO";
        btnPopUpCadastroExigencia_ModalPopupExtender.Show();
    }

    protected void btnAdicionarExigenciasExtracao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NovaExigencia();
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
            hfItemExigencia.Value = "EXTRACAO";
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
            if(ddlClientes.SelectedIndex > 0)
            {
                this.CarregarEmpresasQueOUsuarioTemAcessoComFiltroStatus(ddlEmpresa, ddlClientes.SelectedValue.ToInt32(), ddlStatusEmpresa.SelectedValue.ToString());
                mvwProcessos.ActiveViewIndex = -1;
                this.CarregarArvore();

                //verificando as permissoes
                barraOpcoes.Visible = false;
                if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
                {
                    if (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                    {
                        barraOpcoes.Visible = true;
                    }
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

    protected void trvProcessos_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            hfIdProcessoDNPM.Value = "0";
            this.ExibirVisoes();
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

    protected void lkbOpcoesGuiaNovo_Click(object sender, EventArgs e)
    {
        try
        {
            hfIdGuiaUtilizacao.Value = "0";
            hfIdProcessoDNPM.Value = "0";
            lkbObservacoes.Visible = false;
            if (!trvProcessos.SelectedValue.Contains("ANM"))
            {
                ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(this.RetornarIdDoProcessoSelecionadoNaArvore());
                if (p != null)
                {
                    lkbStstusGuia.Visible = false;

                    this.PopUpAberto = PopUpAbertoENUM.GUIA;
                    this.CarregarStatus(ddlStatusGuiaUtilizacao);
                    tbxDataRequerimentoGuia.Text = "";
                    tbxNumeroGuia.Text = "";
                    tbxDataEmissaoGuia.Text = "";
                    tbxDataVencimentoGuia.Text = "";

                    grvExigenciasGuia.DataSource = null;
                    grvExigenciasGuia.DataBind();

                    grvNotificacoesGuia.DataSource = null;
                    grvNotificacoesGuia.DataBind();

                    this.ArquivosUpload = null;
                    this.SessaoUploadsGuiaUtlizacao = new List<ArquivoFisico>();
                    this.NotificacoesSelecionadas = new List<Notificacao>();
                    this.ExigenciasSelecionadas = new List<Exigencia>();

                    btnPopUpGuia_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Selecione um Processo!", "Alerta", MsgIcons.Alerta);
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um Processo!", "Alerta", MsgIcons.Alerta);
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

    protected void ibtnAddNotificacaoRequerimentoLavra_Click(object sender, ImageClickEventArgs e)
    {
        this.NovaExigencia();
        hfItemExigencia.Value = "REQLAVRA";
        btnPopUpCadastroExigencia_ModalPopupExtender.Show();
    }

    protected void btnNovoRequerimentoPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoRequerimentoPesquisa();
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

    protected void btnNovoAlvaraPesquisa_Click(object sender, EventArgs e)
    {
        try
        {

            lkbObservacoesAlvara.Visible = false;

            this.CarregarStatus(ddlEstatusTaxaAnual);
            this.CarregarStatus(ddlEstatusDipem);

            lkbStstusDipem.Visible = false;
            lkbStstusTaxaAnual.Visible = false;

            this.PopUpAberto = PopUpAbertoENUM.ALVARAPESQUISA;

            tbxNumeroAlvaraPesquisa.Text = "";
            tbxDataPublicacaoAlvaraPesquisa.Text = "";
            tbxDataEntragaRelatorioPesquisa.Text = "";
            tbxDataAprovacaoRelatorioPesquisa.Text = "";

            lblDataDIPEM.Text = "Data gerada automaticamente";
            lblDataRenuncia.Text = "Data gerada automaticamente";

            hfAlvaraPesquisa.Value = "0";

            lblDataLimiteRequerimentoLpTotal.Text = "Data gerada automaticamente";
            lblDataLimiteRequerimentoLavra.Text = "Data gerada automaticamente";

            lblDataTaxaHectare.Text = "Data gerada automaticamente";
            tbxValidadeAlvaraPesquisa.Text = "";

            grvAlvaraPesquisaDIPEM.DataSource = null;
            grvAlvaraPesquisaDIPEM.DataBind();

            gdvRenuncia.DataSource = null;
            gdvRenuncia.DataBind();

            grvExigenciasAlvaraPesquisa.DataSource = null;
            grvExigenciasAlvaraPesquisa.DataBind();

            grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = null;
            grvAlvaraPesquisaValidadeNotificacoesPopUp.DataBind();

            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = null;
            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataBind();

            grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = null;
            grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataBind();

            grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = null;
            grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataBind();

            grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = null;
            grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataBind();

            this.ArquivosUpload = new List<ArquivoFisico>();
            this.SessaoUploadsAlvaraPesquisa = new List<ArquivoFisico>();

            btnPopUpSelecionarRegime_ModalPopupExtender.Hide();
            btnPopUpAlvaraPesquisa_ModalPopupExtender.Show();
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

    protected void btnNovoRequerimentoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoRequerimentoLavra();
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

    protected void btnNovoConcessaoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            lkbObservacoesConcessao.Visible = false;
            hfIdConcessaoLavra.Value = "0";

            tbxNumeroPortariaLavra.Text = "";
            tbxDataApresentacaoRelatorio.Text = "";

            tbxPublicacaoConcessaoLavra.Text = "";
            grvExigenciasConcessaoLavra.DataSource = null;
            grvExigenciasConcessaoLavra.DataBind();

            grvNotificacaoConcessaoLavra.DataSource = null;
            grvNotificacaoConcessaoLavra.DataBind();

            this.PopUpAberto = PopUpAbertoENUM.CONCESSAOLAVRA;

            this.ArquivosUpload = null;
            this.SessaoUploadsConcessaoLavra = new List<ArquivoFisico>();

            btnPopUpSelecionarRegime_ModalPopupExtender.Hide();
            lblPopUpConcessaoLavra_ModalPopupExtender.Show();
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

    protected void btnNovoExtracao_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaExtracao();
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

    protected void btnNovoLicenciamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoLicenciamento();
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

    protected void lkbRegimeNovo_Click(object sender, EventArgs e)
    {
        try
        {
            hfAlvaraPesquisa.Value = hfIdLicenciamento.Value = hfIdExtracao.Value = "0";


            if (trvProcessos.SelectedNode != null)
            {
                hfIdProcessoDNPM.Value = "0";
                this.LimparSessoes();

                if (!trvProcessos.SelectedValue.Contains("ANM"))
                {
                    if (trvProcessos.SelectedValue.Contains("RL_") || trvProcessos.SelectedValue.Contains("CO_") || trvProcessos.SelectedValue.Contains("RP_") || trvProcessos.SelectedValue.Contains("ALP_"))
                    {
                        mvBotoesRegime.ActiveViewIndex = 0;
                    }
                    else if (trvProcessos.SelectedNode.Value.Contains("LI_"))
                    {
                        mvBotoesRegime.ActiveViewIndex = 2;
                    }
                    else if (trvProcessos.SelectedNode.Value.Contains("EX_"))
                    {
                        mvBotoesRegime.ActiveViewIndex = 1;
                    }
                    else if (trvProcessos.SelectedNode.Value.Contains("PROC_"))
                    {
                        ProcessoDNPM p = ProcessoDNPM.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
                        if (p.RegimeDeCriacao == "Autorização de pesquisa")
                        {
                            mvBotoesRegime.ActiveViewIndex = 0;
                        }
                        else if (p.RegimeDeCriacao == "Extração")
                        {
                            mvBotoesRegime.ActiveViewIndex = 1;
                        }
                        else if (p.RegimeDeCriacao == "Licenciamento")
                        {
                            mvBotoesRegime.ActiveViewIndex = 2;
                        }
                    }
                    else if (trvProcessos.SelectedNode.Value.Contains("RAL_") || trvProcessos.SelectedNode.Value.Contains("GUIA_"))
                    {
                        foreach (TreeNode node in trvProcessos.SelectedNode.Parent.ChildNodes)
                        {
                            if (node.Value.Contains("RP_") || node.Value.Contains("ALP_") || node.Value.Contains("RL_") || node.Value.Contains("CO_"))
                            {
                                mvBotoesRegime.ActiveViewIndex = 0;
                                break;
                            }
                            else if (node.Value.Contains("LI_"))
                            {
                                mvBotoesRegime.ActiveViewIndex = 2;
                                break;
                            }
                            else if (node.Value.Contains("EX_"))
                            {
                                mvBotoesRegime.ActiveViewIndex = 1;
                            }
                        }

                    }
                    else
                    {
                        msg.CriarMensagem("Selecione um Processo ANM abaixo", "Alerta", MsgIcons.Alerta);
                    }

                }
                else
                {
                    msg.CriarMensagem("Selecione um Processo ANM abaixo", "Alerta", MsgIcons.Alerta);
                }

                if (!msg.Mensagem.IsNotNullOrEmpty())
                    btnPopUpSelecionarRegime_ModalPopupExtender.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione um Processo ANM abaixo", "Alerta", MsgIcons.Alerta);
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

    protected void lkbOpcoesProcessoNovo_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlClientes.SelectedIndex > 0)
            {
                ddlRegimeDNPM.Enabled = true;
                this.CarregarConsultoriasProcessoDNPM();
                this.CarregarEstadosProcessoDNPM();
                this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaDNPM);
                if (ddlEmpresa.SelectedIndex > 0)
                    ddlEmpresaDNPM.SelectedValue = ddlEmpresa.SelectedValue;

                this.SubstanciaSelecionadas = null;
                tbxSubstancia.Text = "";

                this.IdLicencasSelecionadasDNPM = null;
                grvLicencasDNPM.DataSource = null;
                grvLicencasDNPM.DataBind();

                WebUtil.LimparCampos(upCadastrarProcessosDNPM.Controls[0].Controls);

                hfIdProcessoDNPM.Value = "0";
                this.ArquivosUpload = null;
                this.SessaoUploadsProcessoDNPM = new List<ArquivoFisico>();
                btnUploadProcessoDNPM.Visible = false;


                btnAbrirContratos.Visible = false;
                ibtnAddLicencaProcesso.Visible = this.UsuarioLogado.PossuiPermissaoDeEditarModuloMeioAmbiente;
                btnPopUpCadastroProcessoDNPM_ModalPopupExtender.Show();
            }
            else
            {
                msg.CriarMensagem("É necessario selecionar um Grupo Econômico antes.", "Alerta", MsgIcons.Alerta);
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

    protected void rblOrgaosAmbientais_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarArvoreSelecionarLicenca();
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

    protected void ddlEstadoProcessoDNPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidadeProcessoDNPM(ddlEstadoProcessoDNPM.SelectedValue.ToInt32());
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

    protected void btnAdicionarLicenca_Click(object sender, EventArgs e)
    {
        try
        {
            this.AdicionarLicencasAssociadas();
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

    protected void btnExcluirProcessoDNPM_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExcluirProcessoDNPM(hfIdProcessoDNPM.Value.ToInt32());
            this.NovoProcessoDNPM();
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

    protected void btnSalvarProcessoDNPM_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarProcessoDNPM();
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

    protected void grvOutros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            foreach (GridViewRow item in grvLicencasDNPM.Rows)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Licenca l = Licenca.ConsultarPorId(grvLicencasDNPM.DataKeys[item.DataItemIndex].Value.ToString().ToInt32());
                    IdLicencasSelecionadasDNPM.Remove(l.Id);
                    l.Excluir();
                }
            this.CarregarLicencasDoProcessoDNPM(this.ReconsultarLicencas());
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

    protected void btnAdicionarExigenciaRequerimentoPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NovaExigencia();
            hfItemExigencia.Value = "REQPESQUISA";
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void btnSalvarRequerimentoPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarRequerimentoPesquisa();
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

    protected void btnSalvarExigencias_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarExigencia();
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

    protected void ibtnAddLicencaProcesso_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hfTipo.Value = "PROCESSO";
            this.CarregarPopUpSelecionarLicenca();
            btnSelecionarLicencaHide_ModalPopupExtender.Show();
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

    protected void btnSalvarRequerimentoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarRequerimentoLavra();
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

    protected void btnSalvarGuia_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarGuia();
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

    protected void btnAdicionarExigenciaRequerimentoLavra_Click(object sender, EventArgs e)
    {
        btnPopUpCadastroExigencia_ModalPopupExtender.Show();
    }

    protected void btnAdicionarExigenciasExtracao_Click(object sender, EventArgs e)
    {
        btnPopUpCadastroExigencia_ModalPopupExtender.Show();
    }

    protected void btnAdicionarExigenciaRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExigencias);
    }

    protected void btnAdicionarExigenciasLicenciamento_Click(object sender, EventArgs e)
    {
        btnPopUpCadastroExigencia_ModalPopupExtender.Show();
    }

    protected void tbnSalvarAlvaraDePesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarAlvaraPesquisa();
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

    protected void btnSalvarConcessaoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarConcessaoLavra();
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

    protected void btnSalvarLicenciamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarLicenciamento();
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

    protected void btnSalvarExtracao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarExtracao();
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

    protected void ibtnAtualizarArvore_Click(object sender, EventArgs e)
    {
        try
        {
            mvwProcessos.ActiveViewIndex = -1;
            this.CarregarArvore();
            
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

    protected void lkbRegimeExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExcluirRegime();
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

    protected void lkbOpcoesProcessoEditar_Click(object sender, EventArgs e)
    {
        try
        {
            this.EditarProcesso();
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

    protected void lkbRegimeEditar_Click(object sender, EventArgs e)
    {
        try
        {
            hfIdProcessoDNPM.Value = "0";
            this.CaminhoUpload = null;
            this.ExigenciasSelecionadas = null;
            this.NotificacoesSelecionadas = null;
            this.EditarRegime();
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

    protected void lkbOpcoesGuiaEditar_Click(object sender, EventArgs e)
    {
        try
        {
            this.EditarGuiaDeUtilizacao();
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

    protected void btnAdicionarNotificacao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CarregarPopUpNotificacao(true, true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void btnAdicionarNotificacaoValidadeExtracao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NotificacaoAberta = PopUpNotificacaoAberta.EXTRACAOVALIDADE;
            this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);

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

    protected void btnAdicionarNotificacaoValidadeLicenciamento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NotificacaoAberta = PopUpNotificacaoAberta.LICENCIAMENTOVALIDADE;
            this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);

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

    protected void btnAdicionarNotificacaoEntregaLicenciamento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NotificacaoAberta = PopUpNotificacaoAberta.LICENCIAMENTOENTREGALICENCAPROTOCOLO;
            this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void btnAdicionarNotificacaoConcessaoLavra_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NotificacaoAberta = PopUpNotificacaoAberta.CONCESSAODELAVRA;
            this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnNotificacoesPopGuia_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (tbxDataVencimentoGuia.Text.IsNotNullOrEmpty())
            {
                this.NotificacaoAberta = PopUpNotificacaoAberta.GUIAUTILIZACAONOT;
                this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
            }
            else
            {
                msg.CriarMensagem("Para inserir notificações é necessario informar a data do vencimento", "Alerta", MsgIcons.Alerta);
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

    protected void ibtnAddNotificacaoValidadeAlvaraPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.AddNotificacaoValidadeAlvaraPesquisa();
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

    protected void ibtnAddNotificacaoRequerimentoLavraAlvaraPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.AddNotificacaoRequerimentoLavra();
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

    protected void ibtnAddNotificacaoTaxaAnualHectareAlvaraPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NotificacaoAberta = PopUpNotificacaoAberta.ALVARADEPESQUISATAXAANUALHECTARE;

            this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnAddNotificacaoInicioPesquisa0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.AddNotificacaoNotificacaoPesquisa();
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
            if (tbxOutrosEmails.Text.Trim().Replace(" ", "") != "")
            {
                if (!WebUtil.ValidarEmailInformado(tbxOutrosEmails.Text))
                {
                    msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }

            this.SalvarNotificacao();
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

    protected void ibtnAddNotificacaoValidadeLPTotal_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.AddNotificacaoLPPoligonal();
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

    protected void ibtnAddNotificacaoDIPEM_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.AddNotificacaoDIPEM();
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

    protected void lkbOpcoesGuiaExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExcluirGuiaUtilizacao();
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

    protected void lkbOpcoesRALExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExcluirRAL();
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

    protected void ddlEmpresaDNPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpresaDNPM.SelectedIndex > 0)
        {
            btnUploadProcessoDNPM.Visible = true;
            WebUtil.AdicionarEventoShowModalDialog(btnUploadProcessoDNPM, "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + ddlEmpresaDNPM.SelectedValue + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=ProcessoDNPM"), "Upload Arquivo Processo DNPM", 550, 420);
        }
        else
        {
            btnUploadProcessoDNPM.Visible = false;
        }
    }

    protected void ibtnAddLicencaProcesso0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NotificacaoAberta = PopUpNotificacaoAberta.RAL;
            this.PopUpAberto = PopUpAbertoENUM.RAL;
            this.CarregarPopUpNotificacao(true, false, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnAtualizarArvore_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mvwProcessos.ActiveViewIndex = -1;
            this.CarregarArvore();
            trvProcessos.Nodes[0].Select();
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

    protected void lkbStstusDipem_Click(object sender, EventArgs e)
    {
        try
        {
            AlvaraPesquisa alvara = new AlvaraPesquisa(hfAlvaraPesquisa.Value);
            alvara = alvara.ConsultarPorId();
            this.CarregarVencimentos(alvara.DIPEM, "DIPEM", alvara.Id);
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

    protected void lkbStstusGuia_Click(object sender, EventArgs e)
    {
        try
        {
            GuiaUtilizacao guia = new GuiaUtilizacao(hfIdGuiaUtilizacao.Value);
            guia = guia.ConsultarPorId();
            this.CarregarVencimentos(guia.Vencimentos, "GuiaUtilizacao", guia.Id);
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

    protected void lkbStstusTaxaAnual_Click(object sender, EventArgs e)
    {
        try
        {
            AlvaraPesquisa alvara = new AlvaraPesquisa(hfAlvaraPesquisa.Value);
            alvara = alvara.ConsultarPorId();
            this.CarregarVencimentos(alvara.TaxaAnualPorHectare, "TaxaAnualPorHectare", alvara.Id);
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

    protected void lkbVencimentoExtracao_Click(object sender, EventArgs e)
    {
        try
        {
            Extracao extracao = new Extracao(hfIdExtracao.Value);
            extracao = extracao.ConsultarPorId();
            this.CarregarVencimentos(extracao.Vencimentos, "Extracao", extracao.Id);
            hfTypeVencimento.Value = "Extracao";
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

    protected void lkbVencimentoExigencia_Click(object sender, EventArgs e)
    {
        try
        {
            Exigencia exigencia = new Exigencia(hfIdExigencia.Value);
            exigencia = exigencia.ConsultarPorId();
            this.CarregarVencimentos(exigencia.Vencimentos, "Exigencia", exigencia.Id);
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
            this.CarregarVencimento();
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

    protected void btnSalvarExigencias0_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarVencimento();
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

    protected void lkbVencimentoLicenciamento_Click1(object sender, EventArgs e)
    {
        try
        {
            Licenciamento licenciamento = new Licenciamento(hfIdLicenciamento.Value);
            licenciamento = licenciamento.ConsultarPorId();
            this.CarregarVencimentos(licenciamento.Vencimentos, "Licenciamento", licenciamento.Id);
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

    protected void lkbVencimentoRAL_Click(object sender, EventArgs e)
    {
        try
        {
            RAL ral = new RAL(this.RetornarIdSelecionadoNaArvore());
            ral = ral.ConsultarPorId();
            this.CarregarVencimentos(ral.Vencimentos, "RAL", ral.Id);
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

    protected void btnAbrirContratos_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarContratos();
            btnAbrirContratos_ModalPopupExtender.Show();
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

    protected void btnSalvarContratoDiverso_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(hfIdProcessoDNPM.Value.ToInt32());
            if (processo.ContratosDiversos == null)
                processo.ContratosDiversos = new List<ContratoDiverso>();

            foreach (GridViewRow item in gdvContratosSelecao.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    ContratoDiverso cont = ContratoDiverso.ConsultarPorId(gdvContratosSelecao.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    cont.Salvar();
                    if (!processo.ContratosDiversos.Contains(cont))
                    {
                        processo.ContratosDiversos.Add(cont);
                    }
                }
            }

            processo = processo.Salvar();
            msg.CriarMensagem("Contratos associados com sucesso", "Sucesso", MsgIcons.Sucesso);
            lblAbrirSelecaoContratos_popupextender.Hide();
            this.CarregarContratos();
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
            tbxObjetoContratoDiverso.Text = "";
            tbxNumeroContratoDiverso.Text = "";
            lblAbrirSelecaoContratos_popupextender.Show();
            this.PesquisarContratosDiversos();
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
            transacao.Recarregar(ref msg);
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

    protected void btnAdicionarExigenciaAlvaraPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NovaExigencia();
            hfItemExigencia.Value = "ALVARAPESQUISA";
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void btnAdicionarExigenciaConcessaoLavra_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.NovaExigencia();
            hfItemExigencia.Value = "CONCESSAOLAVRA";
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    #region _______PageIndexChanging_______

    protected void grvExigenciasGuia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExigenciasGuia.PageIndex = e.NewPageIndex;
        grvExigenciasGuia.DataSource = this.RecarregarExigencias();
        grvExigenciasGuia.DataBind();
    }

    protected void grvNotificacoesGuia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacoesGuia.PageIndex = e.NewPageIndex;
        grvNotificacoesGuia.DataSource = this.NotificacoesSelecionadas;
        grvNotificacoesGuia.DataBind();
    }

    protected void grvLicencasDNPM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvLicencasDNPM.PageIndex = e.NewPageIndex;
        grvLicencasDNPM.DataSource = this.ReconsultarLicencas();
        grvLicencasDNPM.DataBind();
    }

    protected void grvExigenciasAlvaraPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExigenciasAlvaraPesquisa.PageIndex = e.NewPageIndex;
        grvExigenciasAlvaraPesquisa.DataSource = this.SessaoAlvaraPesquisa.Exigencias;
        grvExigenciasAlvaraPesquisa.DataBind();
    }

    protected void grvAlvaraPesquisaValidadeNotificacoesPopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaValidadeNotificacoesPopUp.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = this.SessaoAlvaraPesquisa.Vencimento.Notificacoes;
        grvAlvaraPesquisaValidadeNotificacoesPopUp.DataBind();
    }

    protected void grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes;
        grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataBind();
    }

    protected void grvAlvaraPesquisaTaxaAnualHectareNotificacoes0_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes;
        grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataBind();
    }

    protected void grvAlvaraPesquisaRequerimentoLavraNotificacoes0_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaRequerimentoLavraNotificacoes0.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes;
        grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataBind();
    }

    protected void grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes;
        grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataBind();
    }

    protected void grvAlvaraPesquisaDIPEM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAlvaraPesquisaDIPEM.PageIndex = e.NewPageIndex;
        grvAlvaraPesquisaDIPEM.DataSource = this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes;
        grvAlvaraPesquisaDIPEM.DataBind();
    }

    protected void grvExigenciasRequerimentoLavra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExigenciasRequerimentoLavra.PageIndex = e.NewPageIndex;
        grvExigenciasRequerimentoLavra.DataSource = this.RecarregarExigencias();
        grvExigenciasRequerimentoLavra.DataBind();
    }

    protected void grvExigenciasConcessaoLavra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExigenciasConcessaoLavra.PageIndex = e.NewPageIndex;
        grvExigenciasConcessaoLavra.DataSource = this.RecarregarExigencias();
        grvExigenciasConcessaoLavra.DataBind();
    }

    protected void grvNotificacaoConcessaoLavra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacaoConcessaoLavra.PageIndex = e.NewPageIndex;
        grvNotificacaoConcessaoLavra.DataSource = this.NotificacoesSelecionadas;
        grvNotificacaoConcessaoLavra.DataBind();

    }

    protected void grvLicencasLicenciamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvLicencasLicenciamento.PageIndex = e.NewPageIndex;
        grvLicencasLicenciamento.DataSource = this.ReconsultarLicencas();
        grvLicencasLicenciamento.DataBind();
    }

    protected void grvNotificacaoLicenciamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacaoLicenciamento.PageIndex = e.NewPageIndex;
        grvNotificacaoLicenciamento.DataSource = this.RecarregarExigencias();
        grvNotificacaoLicenciamento.DataBind();
    }

    protected void grvNotificacaoValidadeLicenciamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacaoValidadeLicenciamento.PageIndex = e.NewPageIndex;
        grvNotificacaoValidadeLicenciamento.DataSource = this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes;
        grvNotificacaoValidadeLicenciamento.DataBind();
    }

    protected void grvNotificacaoEntregaLicencieamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacaoEntregaLicencieamento.PageIndex = e.NewPageIndex;
        grvNotificacaoEntregaLicencieamento.DataSource = this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes;
        grvNotificacaoEntregaLicencieamento.DataBind();
    }

    protected void grvExigenciaExtracao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExigenciaExtracao.PageIndex = e.NewPageIndex;
        grvExigenciaExtracao.DataSource = this.RecarregarExigencias();
        grvExigenciaExtracao.DataBind();
    }

    protected void grvNotificacaoValidadeExtracao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacaoValidadeExtracao.PageIndex = e.NewPageIndex;
        grvNotificacaoValidadeExtracao.DataSource = this.SessaoExtracao.GetUltimoVencimento.Notificacoes;
        grvNotificacaoValidadeExtracao.DataBind();
    }

    protected void grvNotificacoesExigencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvNotificacoesExigencias.PageIndex = e.NewPageIndex;
        grvNotificacoesExigencias.DataSource = this.NotificacoesDeExigencia;
        grvNotificacoesExigencias.DataBind();
    }

    protected void grvNotificacaoVencimentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvNotificacaoVencimentos.PageIndex = e.NewPageIndex;
            grvNotificacaoVencimentos.DataSource = Vencimento.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32()).Notificacoes;
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

    protected void gdvContratosSelecao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvContratosSelecao.PageIndex = e.NewPageIndex;
        gdvContratosSelecao.DataSource = this.RecarregarContratos();
        gdvContratosSelecao.DataBind();
    }

    protected void gvContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(hfIdProcessoDNPM.Value.ToInt32());
        gvContratos.PageIndex = e.NewPageIndex;
        gvContratos.DataSource = processo.ContratosDiversos;
        gvContratos.DataBind();
    }

    #endregion

    #region _______TriggersPaginacao________

    protected void grvExigenciasGuia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopGuiaUtilizacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopGuiaUtilizacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvNotificacoesGuia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopGuiaUtilizacao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopGuiaUtilizacao);
    }

    protected void grvLicencasDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upCadastrarProcessosDNPM);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upCadastrarProcessosDNPM);
    }

    protected void grvExigenciasRequerimentoPesquisa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upRequerimentoPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upRequerimentoPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvAlvaraPesquisaValidadeNotificacoesPopUp_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
    }

    protected void grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
    }

    protected void grvAlvaraPesquisaTaxaAnualHectareNotificacoes0_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
    }

    protected void grvAlvaraPesquisaRequerimentoLavraNotificacoes0_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
    }

    protected void grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
    }

    protected void grvAlvaraPesquisaDIPEM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
    }

    protected void grvExigenciasRequerimentoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpRequerimentoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpRequerimentoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvExigenciasConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpConcessaoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpConcessaoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvNotificacaoConcessaoLavra_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpConcessaoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpConcessaoLavra);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvLicencasLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpLicenciamento);
    }

    protected void grvNotificacaoLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvNotificacaoEntregaLicencieamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpLicenciamento);
    }

    protected void grvNotificacaoValidadeLicenciamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpLicenciamento);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpLicenciamento);
    }

    protected void grvExigenciaExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpExtracao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpExtracao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    protected void grvNotificacaoValidadeExtracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upPopUpExtracao);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upPopUpExtracao);
    }

    protected void grvNotificacoesExigencias_Init1(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upExigencias);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upExigencias);
    }

    protected void grvExigenciasAlvaraPesquisa_Init1(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upAlvaraPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upExigencias);
    }

    #endregion

    #region ________RowDeletingPopups_______

    protected void grvLicencasDNPM_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvLicencasDNPM.PageIndex = 1;
            foreach (GridViewRow item in grvLicencasDNPM.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Licenca l = new Licenca(grvLicencasDNPM.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.IdLicencasSelecionadasDNPM.Remove(l.Id);
                }
            }
            grvLicencasDNPM.DataSource = this.ReconsultarLicencas();
            grvLicencasDNPM.DataBind();
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

    protected void grvExigenciasRequerimentoPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvExigenciasRequerimentoPesquisa.PageIndex = 1;
            foreach (GridViewRow item in grvExigenciasRequerimentoPesquisa.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvExigenciasRequerimentoPesquisa.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.ExigenciasSelecionadas.Remove(exig);
                    exig.Excluir();
                }
            }

            grvExigenciasRequerimentoPesquisa.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasRequerimentoPesquisa.DataBind();
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

    protected void grvExigenciasGuia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvExigenciasGuia.PageIndex = 1;
            foreach (GridViewRow item in grvExigenciasGuia.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvExigenciasGuia.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.ExigenciasSelecionadas.Remove(exig);
                    exig.Excluir();
                }
            }

            grvExigenciasGuia.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasGuia.DataBind();
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

    protected void grvExigenciasAlvaraPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvExigenciasAlvaraPesquisa.PageIndex = 1;
            foreach (GridViewRow item in grvExigenciasAlvaraPesquisa.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvExigenciasAlvaraPesquisa.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.Exigencias.Remove(exig);
                    exig.Excluir();
                }
            }

            grvExigenciasAlvaraPesquisa.DataSource = this.SessaoAlvaraPesquisa.Exigencias;
            grvExigenciasAlvaraPesquisa.DataBind();
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

    protected void grvExigenciasRequerimentoLavra_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvExigenciasRequerimentoLavra.PageIndex = 1;
            foreach (GridViewRow item in grvExigenciasRequerimentoLavra.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvExigenciasRequerimentoLavra.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.ExigenciasSelecionadas.Remove(exig);
                    exig.Excluir();
                }
            }

            grvExigenciasRequerimentoLavra.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasRequerimentoLavra.DataBind();
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

    protected void grvExigenciasConcessaoLavra_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvExigenciasConcessaoLavra.PageIndex = 1;
            foreach (GridViewRow item in grvExigenciasConcessaoLavra.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvExigenciasConcessaoLavra.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.ExigenciasSelecionadas.Remove(exig);
                    exig.Excluir();
                }
            }

            grvExigenciasConcessaoLavra.DataSource = this.ExigenciasSelecionadas;
            grvExigenciasConcessaoLavra.DataBind();
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

    protected void grvNotificacaoConcessaoLavra_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacaoConcessaoLavra.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacaoConcessaoLavra.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao notificacao = new Notificacao(grvNotificacaoConcessaoLavra.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.NotificacoesSelecionadas.Remove(notificacao);
                    notificacao.Excluir();
                }
            }

            grvNotificacaoConcessaoLavra.DataSource = this.NotificacoesSelecionadas;
            grvNotificacaoConcessaoLavra.DataBind();
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

    protected void grvLicencasLicenciamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvLicencasLicenciamento.PageIndex = 1;
            foreach (GridViewRow item in grvLicencasLicenciamento.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Licenca licenca = new Licenca(grvLicencasLicenciamento.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.IdLicencasSelecionadasDNPM.Remove(licenca.Id);
                }
            }

            grvLicencasLicenciamento.DataSource = this.ReconsultarLicencas();
            grvLicencasLicenciamento.DataBind();
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

    protected void grvNotificacaoLicenciamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacaoLicenciamento.PageIndex = 1;
            foreach (GridViewRow item in grvLicencasLicenciamento.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvNotificacaoLicenciamento.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.ExigenciasSelecionadas.Remove(exig);
                    exig.Excluir();
                }
            }

            grvNotificacaoLicenciamento.DataSource = this.ExigenciasSelecionadas;
            grvNotificacaoLicenciamento.DataBind();
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

    protected void grvNotificacaoEntregaLicencieamento_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacaoEntregaLicencieamento.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacaoEntregaLicencieamento.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvNotificacaoEntregaLicencieamento.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvNotificacaoEntregaLicencieamento.DataSource = this.SessaoLicenciamento.EntregaLicencaOuProtocolo.Notificacoes;
            grvNotificacaoEntregaLicencieamento.DataBind();
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

    protected void grvNotificacaoValidadeLicenciamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacaoValidadeLicenciamento.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacaoValidadeLicenciamento.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvNotificacaoValidadeLicenciamento.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvNotificacaoValidadeLicenciamento.DataSource = this.SessaoLicenciamento.GetUltimoVencimento.Notificacoes;
            grvNotificacaoValidadeLicenciamento.DataBind();
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

    protected void grvExigenciaExtracao_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvExigenciaExtracao.PageIndex = 1;
            foreach (GridViewRow item in grvExigenciaExtracao.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Exigencia exig = new Exigencia(grvExigenciaExtracao.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.ExigenciasSelecionadas.Remove(exig);
                    exig.Excluir();
                }
            }

            grvExigenciaExtracao.DataSource = this.ExigenciasSelecionadas;
            grvExigenciaExtracao.DataBind();
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

    protected void grvNotificacaoValidadeExtracao_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacaoValidadeExtracao.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacaoValidadeExtracao.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvNotificacaoValidadeExtracao.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoExtracao.GetUltimoVencimento.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvNotificacaoValidadeExtracao.DataSource = this.SessaoExtracao.GetUltimoVencimento.Notificacoes;
            grvNotificacaoValidadeExtracao.DataBind();
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

    protected void grvNotificacoesGuia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacoesGuia.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacoesGuia.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvNotificacoesGuia.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.NotificacoesSelecionadas.Remove(not);
                    not.Excluir();
                }
            }

            grvNotificacoesGuia.DataSource = this.NotificacoesSelecionadas;
            grvNotificacoesGuia.DataBind();
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

    protected void grvAlvaraPesquisaValidadeNotificacoesPopUp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvAlvaraPesquisaValidadeNotificacoesPopUp.PageIndex = 1;
            foreach (GridViewRow item in grvAlvaraPesquisaValidadeNotificacoesPopUp.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvAlvaraPesquisaValidadeNotificacoesPopUp.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.Vencimento.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvAlvaraPesquisaValidadeNotificacoesPopUp.DataSource = this.SessaoAlvaraPesquisa.Vencimento.Notificacoes;
            grvAlvaraPesquisaValidadeNotificacoesPopUp.DataBind();
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

    protected void grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.PageIndex = 1;
            foreach (GridViewRow item in grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.NotificacaoPesquisaDNPM.Notificacoes;
            grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0.DataBind();
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

    protected void grvAlvaraPesquisaTaxaAnualHectareNotificacoes0_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.PageIndex = 1;
            foreach (GridViewRow item in grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.GetUltimaTaxaAnualHectare.Notificacoes;
            grvAlvaraPesquisaTaxaAnualHectareNotificacoes0.DataBind();

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

    protected void grvAlvaraPesquisaRequerimentoLavraNotificacoes0_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvAlvaraPesquisaRequerimentoLavraNotificacoes0.PageIndex = 1;
            foreach (GridViewRow item in grvAlvaraPesquisaRequerimentoLavraNotificacoes0.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataSource = this.SessaoAlvaraPesquisa.RequerimentoLavra.Notificacoes;
            grvAlvaraPesquisaRequerimentoLavraNotificacoes0.DataBind();

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

    protected void grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.PageIndex = 1;
            foreach (GridViewRow item in grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataSource = this.SessaoAlvaraPesquisa.RequerimentoLPTotal.Notificacoes;
            grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp.DataBind();

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

    protected void grvAlvaraPesquisaDIPEM_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvAlvaraPesquisaDIPEM.PageIndex = 1;
            foreach (GridViewRow item in grvAlvaraPesquisaDIPEM.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao not = new Notificacao(grvAlvaraPesquisaDIPEM.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes.Remove(not);
                    not.Excluir();
                }
            }

            grvAlvaraPesquisaDIPEM.DataSource = this.SessaoAlvaraPesquisa.GetUltimoDIPEM.Notificacoes;
            grvAlvaraPesquisaDIPEM.DataBind();

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

    protected void grvNotificacoesExigencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grvNotificacoesExigencias.PageIndex = 1;
            foreach (GridViewRow item in grvNotificacoesExigencias.Rows)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao l = Notificacao.ConsultarPorId(grvNotificacoesExigencias.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    this.NotificacoesDeExigencia.Remove(l);
                    l.Excluir();
                }
            grvNotificacoesExigencias.DataSource = this.NotificacoesDeExigencia;
            grvNotificacoesExigencias.DataBind();

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

    protected void gvContratos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(hfIdProcessoDNPM.Value.ToInt32());

            foreach (GridViewRow item in gvContratos.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    ContratoDiverso cont = ContratoDiverso.ConsultarPorId(gvContratos.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    processo.ContratosDiversos.Remove(cont);
                    msg.CriarMensagem("Contrato(s) excluído(s) com sucesso", "Sucesso", MsgIcons.Sucesso);
                }
            }

            processo = processo.Salvar();
            this.CarregarContratos();
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

    #region ________ RENOVAÇÕES __________

    protected void grvRequerimentosPesquisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvRequerimentosPesquisa.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvRequerimentosPesquisa.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvAlvaraPesquisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvAlvaraPesquisa.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvAlvaraPesquisa.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvDataRequerimentoLavraExigencias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvDataRequerimentoLavraExigencias.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvDataRequerimentoLavraExigencias.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvConcessaoLavraExigencias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvConcessaoLavraExigencias.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvConcessaoLavraExigencias.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvExigenciasLicenciamento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvExigenciasLicenciamento.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvExigenciasLicenciamento.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvExigenciasExtracao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvExigenciasExtracao.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvExigenciasExtracao.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvExigenciasGuiaUtilizacao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Exigencia op = Exigencia.ConsultarPorId(grvExigenciasGuiaUtilizacao.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);
                hfTipoRenovacao.Value = "VENCIMENTOEXIGENCIA";

                hfIdExigenciaRenovacao.Value = grvExigenciasGuiaUtilizacao.DataKeys[e.RowIndex].Value.ToString();

                tbxDiasValidadeRenovacao.Visible = true;
                tbxDataValidadeRenovacao.Visible = false;
                tbxDataValidadeRenovacao.Enabled = true;
                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();
                lblValidadeRenovacao.Text = "Renovar por mais(dias):";

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void btnSalvarRenovacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarRenovacao();
            transacao.Recarregar(ref msg);
            this.ExibirVisoes();

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

    private void SalvarRenovacao()
    {
        if (tbxDiasValidadeRenovacao.Visible == true && tbxDiasValidadeRenovacao.Text.ToInt32() <= 0)
        {
            msg.CriarMensagem("O valor informado deve ser maior que zero.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (tbxDataValidadeRenovacao.Visible == true && tbxDataValidadeRenovacao.Text.IsNotNullOrEmpty() && tbxDataValidadeRenovacao.Text.ToSqlDateTime() <= SqlDate.MinValue)
        {
            msg.CriarMensagem("A Data informada deve conter uma data válida.", "Alerta", MsgIcons.Alerta);
            return;
        }


        #region __________ TAXA ANUAL HECTARE ____________

        if (hfTipoRenovacao.Value.ToUpper() == "TAXAANUALHECTARE")
        {
            AlvaraPesquisa a = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.VerificarAlteracaoDeStatusRenovacao(a);
            Vencimento v = a.GetUltimaTaxaAnualHectare;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();

            if (tbxDiasValidadeRenovacao.Text.ToInt32() != 0)
                novo.Data = new DateTime(v.Data.Year+tbxDiasValidadeRenovacao.Text.ToInt32(), v.Data.Month, v.Data.Day);
            else
            {
                msg.CriarMensagem("Insira um valor válido.", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            a.TaxaAnualPorHectare.Add(novo);
            a = a.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        #region __________ VENCIMENTO LICENCIAMENTO ____________

        else if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTOLICENCIAMENTO")
        {
            Licenciamento l = Licenciamento.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.VerificarAlteracaoDeStatusRenovacao(l);
            Vencimento v = l.GetUltimoVencimento;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();

            if (tbxDataValidadeRenovacao.Text.IsNotNullOrEmpty())
                novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
            else
            {
                msg.CriarMensagem("Insira um valor válido.", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            l.Vencimentos.Add(novo);
            l = l.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        #region __________ VENCIMENTO EXTRAÇÃO ____________

        else if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTOEXTRACAO")
        {
            Extracao e = Extracao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.VerificarAlteracaoDeStatusRenovacao(e);
            Vencimento v = e.GetUltimoVencimento;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();

            if (tbxDataValidadeRenovacao.Text.IsDate())
                novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
            else
            {
                msg.CriarMensagem("Insira um valor válido.", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            e.Vencimentos.Add(novo);
            e = e.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        #region __________ VENCIMENTO RAL ____________

        else if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTORAL")
        {
            RAL r = RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.VerificarAlteracaoDeStatusRenovacao(r);
            Vencimento v = r.GetMaiorVencimento;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação.", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            if (tbxDataValidadeRenovacao.Text.IsNotNullOrEmpty())
                novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
            else
            {
                msg.CriarMensagem("Insira um valor válido.", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            r.Vencimentos.Add(novo);
            r = r.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        #region __________ VENCIMENTO GUIA ____________

        else if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTOGUIA")
        {
            GuiaUtilizacao g = GuiaUtilizacao.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.VerificarAlteracaoDeStatusRenovacao(g);
            Vencimento v = g.GetUltimoVencimento;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            if (tbxDataValidadeRenovacao.Text.IsDate())
                novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
            else
            {
                msg.CriarMensagem("Insira um valor válido.", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            g.DataLimiteRequerimento = novo.Data.AddDays(-60);

            g.Vencimentos.Add(novo);
            g = g.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        #region __________ VENCIMENTO DIPEM ____________

        else if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTODIPEM")
        {
            AlvaraPesquisa al = AlvaraPesquisa.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            this.VerificarAlteracaoDeStatusRenovacao(al);
            Vencimento v = al.GetUltimoDIPEM;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
           // novo.Data = v.Data.AddYears(tbxDiasValidadeRenovacao.Text.ToInt32());
            novo.Data = new DateTime(v.Data.Year + tbxDiasValidadeRenovacao.Text.ToInt32(), v.Data.Month, v.Data.Day);

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }



            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            al.DIPEM.Add(novo);
            al = al.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        #region __________ VENCIMENTO EXIGENCIA ____________

        else if (hfTipoRenovacao.Value.ToUpper() == "VENCIMENTOEXIGENCIA")
        {
            if (tbxDiasValidadeRenovacao.Text.ToInt32() <= 0)
            {
                msg.CriarMensagem("Insira uma quantidade de dias válida.", "Alera", MsgIcons.Alerta);
                return;
            }

            Exigencia exig = Exigencia.ConsultarPorId(hfIdExigenciaRenovacao.Value.ToInt32());
            this.VerificarAlteracaoDeStatusRenovacao(exig);
            Vencimento v = exig.GetUltimoVencimento;
            if (v == null)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                this.CopiarNotificacoes(ref v, ref novo);

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            


            novo.Periodico = v.Periodico;
            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            exig.DiasPrazo = tbxDiasValidadeRenovacao.Text.ToInt32();

            exig.Vencimentos.Add(novo);
            exig = exig.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        #endregion

        btnPopUpRenovacao_ModalPopupExtender.Hide();
        msg.CriarMensagem("Renovação efetuada com Sucesso!", "Sucesso");
    }

    private void CopiarNotificacoes(ref Vencimento v, ref Vencimento novo)
    {
        if (novo.Notificacoes == null)
            novo.Notificacoes = new List<Notificacao>();

        foreach (Notificacao n in v.Notificacoes)
        {
            Notificacao nn = new Notificacao(ModuloPermissao.ModuloDNPM);
            nn.DiasAviso = n.DiasAviso;
            nn.Template = n.Template;
            nn.Emails = n.Emails;
            nn = nn.Salvar();
            novo.Notificacoes.Add(nn);
        }
    }

    protected void btnRenovarAlvara_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatus(ddlStatusRenovacao);
            tbxDiasValidadeRenovacao.Visible = true;
            tbxDataValidadeRenovacao.Visible = false;
            tbxDataValidadeRenovacao.Enabled = true;
            lblValidadeRenovacao.Text = "Validade(Anos):";
            tbxDiasValidadeRenovacao.Text = "1";

            hfTipoRenovacao.Value = "TAXAANUALHECTARE";
            btnPopUpRenovacao_ModalPopupExtender.Show();
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

    protected void btnRenovarValidadeLicenciamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatus(ddlStatusRenovacao);
            hfTipoRenovacao.Value = "VENCIMENTOLICENCIAMENTO";

            tbxDiasValidadeRenovacao.Visible = false;
            tbxDataValidadeRenovacao.Visible = true;
            tbxDataValidadeRenovacao.Enabled = true;
            lblValidadeRenovacao.Text = "Próxima data de vencimento:";

            btnPopUpRenovacao_ModalPopupExtender.Show();
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

    protected void btnRenovarValidadeExtracao_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatus(ddlStatusRenovacao);
            hfTipoRenovacao.Value = "VENCIMENTOEXTRACAO";

            tbxDiasValidadeRenovacao.Visible = false;
            tbxDataValidadeRenovacao.Visible = true;
            tbxDataValidadeRenovacao.Enabled = true;
            lblValidadeRenovacao.Text = "Próxima data de vencimento:";
            btnPopUpRenovacao_ModalPopupExtender.Show();
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

    protected void btnRenovarValidadeRAL_Click(object sender, EventArgs e)
    {
        try
        {
            RAL r = RAL.ConsultarPorId(this.RetornarIdSelecionadoNaArvore());
            if (r != null)
            {
                Vencimento v = r.GetMaiorVencimento;
                if (v == null)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação.", "Alera", MsgIcons.Alerta);
                }
                else
                {
                    this.CarregarStatus(ddlStatusRenovacao);
                    tbxDiasValidadeRenovacao.Visible = false;
                    tbxDataValidadeRenovacao.Visible = true;
                    tbxDataValidadeRenovacao.Enabled = false;
                    lblValidadeRenovacao.Text = "Próxima data de vencimento:";
                    tbxDataValidadeRenovacao.Text = r.GetMaiorVencimento.Data.AddYears(1).ToShortDateString();
                    hfTipoRenovacao.Value = "VENCIMENTORAL";
                    btnPopUpRenovacao_ModalPopupExtender.Show();
                }
            }
            else
            {
                msg.CriarMensagem("Selecione primeiro na arvore à esquerda um RAL.", "Alera", MsgIcons.Alerta);
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

    protected void btnRenovarValidadeGuia_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatus(ddlStatusRenovacao);
            tbxDiasValidadeRenovacao.Visible = false;
            tbxDataValidadeRenovacao.Visible = true;
            tbxDataValidadeRenovacao.Enabled = true;
            lblValidadeRenovacao.Text = "Próxima data de vencimento:";

            hfTipoRenovacao.Value = "VENCIMENTOGUIA";
            btnPopUpRenovacao_ModalPopupExtender.Show();
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

    protected void btnRenovar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenovacao);
    }

    #endregion

    #region _________SessoesUpload________

    public IList<ArquivoFisico> SessaoUploadsGuiaUtlizacao
    {
        get { return (IList<ArquivoFisico>)Session["UploadsGuiaUtlizacao"]; }
        set { Session["UploadsGuiaUtlizacao"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsProcessoDNPM
    {
        get { return (IList<ArquivoFisico>)Session["UploadsProcessoDNPM"]; }
        set { Session["UploadsProcessoDNPM"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsRequerimentoPesquisa
    {
        get { return (IList<ArquivoFisico>)Session["UploadsRequerimentoPesquisa"]; }
        set { Session["UploadsRequerimentoPesquisa"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsAlvaraPesquisa
    {
        get { return (IList<ArquivoFisico>)Session["UploadsAlvaraPesquisa"]; }
        set { Session["UploadsAlvaraPesquisa"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsRequerimentoLavra
    {
        get { return (IList<ArquivoFisico>)Session["UploadsRequerimentoLavra"]; }
        set { Session["UploadsRequerimentoLavra"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsConcessaoLavra
    {
        get { return (IList<ArquivoFisico>)Session["UploadsConcessaoLavra"]; }
        set { Session["UploadsConcessaoLavra"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsLicenciamento
    {
        get { return (IList<ArquivoFisico>)Session["UploadsLicenciamento"]; }
        set { Session["UploadsLicenciamento"] = value; }
    }

    public IList<ArquivoFisico> SessaoUploadsExtracao
    {
        get { return (IList<ArquivoFisico>)Session["UploadsExtracao"]; }
        set { Session["UploadsExtracao"] = value; }
    }

    #endregion
    #region _____EventosBotoesUpload______

    protected void btnUploadGuiaUtilizacao_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdGuiaUtilizacao.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a Guia de Utilização para poder anexar arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsGuiaUtlizacao == null)
                this.SessaoUploadsGuiaUtlizacao = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsGuiaUtlizacao = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfIdGuiaUtilizacao.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=GuiaUtilizacao"));
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

    protected void btnUploadProcessoDNPM_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdProcessoDNPM.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o processo para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsProcessoDNPM == null)
                this.SessaoUploadsProcessoDNPM = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsProcessoDNPM = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfIdProcessoDNPM.Value.ToInt32() + "&idEmpresa=" + ddlEmpresaDNPM.SelectedValue + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=ProcessoDNPM"));
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

    protected void btnUploadRequerimentopesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfRequerimentoPesquisa.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o requerimento de pesquisa para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsRequerimentoPesquisa == null)
                this.SessaoUploadsRequerimentoPesquisa = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsRequerimentoPesquisa = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfRequerimentoPesquisa.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=RequerimentoPesquisa"));
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

    protected void btnUploadAlvaraPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfAlvaraPesquisa.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o alvará de pesquisa para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsAlvaraPesquisa == null)
                this.SessaoUploadsAlvaraPesquisa = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsAlvaraPesquisa = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfAlvaraPesquisa.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=AlvaraPesquisa"));
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

    protected void btnUploadRequerimentoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdRequerimentoLavra.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o requerimento de lavra para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsRequerimentoLavra == null)
                this.SessaoUploadsRequerimentoLavra = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsRequerimentoLavra = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfIdRequerimentoLavra.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=RequerimentoLavra"));
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

    protected void btnUploadConcessaoLavra_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdConcessaoLavra.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a concessão de lavra para poder anexar arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsConcessaoLavra == null)
                this.SessaoUploadsConcessaoLavra = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsConcessaoLavra = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfIdConcessaoLavra.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=ConcessaoLavra"));
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

    protected void btnUploadLicenciamento_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdLicenciamento.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o licenciamento para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsLicenciamento == null)
                this.SessaoUploadsLicenciamento = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsLicenciamento = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfIdLicenciamento.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=Licenciamento"));
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

    protected void btnUploadExtracao_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdExtracao.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a extração para poder anexar arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (this.SessaoUploadsExtracao == null)
                this.SessaoUploadsExtracao = new List<ArquivoFisico>();

            if (this.ArquivosUpload != null)
                this.SessaoUploadsExtracao = this.ArquivosUpload;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfIdExtracao.Value + "&idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=Extracao"));
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

    protected void btnUploadExigencia_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdExigencia.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a exigência para poder anexar arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=Exigencia"));
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

    protected void btnUploadRAL_Click(object sender, EventArgs e)
    {
        conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + hfIdEmpresa.Value + "&idCliente=" + ddlClientes.SelectedValue + "&tipo=RAL&id=" + this.RetornarIdSelecionadoNaArvore()));
        lblUploadArquivos_ModalPopupExtender.Show();
    }

    #endregion
    #region _______RowEditingExigenciasgrids_____

    protected void grvExigenciasGuia_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvExigenciasGuia.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvExigenciasRequerimentoPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvExigenciasRequerimentoPesquisa.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvExigenciasAlvaraPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvExigenciasAlvaraPesquisa.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvExigenciasRequerimentoLavra_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvExigenciasRequerimentoLavra.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvExigenciasConcessaoLavra_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvExigenciasConcessaoLavra.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvNotificacaoLicenciamento_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvNotificacaoLicenciamento.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvExigenciaExtracao_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarExigencia(grvExigenciaExtracao.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            btnPopUpCadastroExigencia_ModalPopupExtender.Show();
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

    protected void grvRequerimentosPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvRequerimentosPesquisa.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvAlvaraPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvAlvaraPesquisa.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvDataRequerimentoLavraExigencias_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvDataRequerimentoLavraExigencias.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvConcessaoLavraExigencias_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvConcessaoLavraExigencias.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvExigenciasLicenciamento_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvExigenciasLicenciamento.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvExigenciasExtracao_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvExigenciasExtracao.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    protected void grvExigenciasGuiaUtilizacao_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Exigencia exigencia = Exigencia.ConsultarPorId(grvExigenciasGuiaUtilizacao.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (exigencia != null)
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + exigencia.Id + "&tipo=Exigencia"));
                lblUploadArquivos_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    #endregion

}