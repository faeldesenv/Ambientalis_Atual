using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;
using Utilitarios.Criptografia;

public partial class Site_Index : PageBase
{

    #region ____________________ Sessoes de Permissao____________________

    //Sessoes Modulo Diversos
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
    public IList<Empresa> EmpresasPermissaoModuloDiversos
    {
        get
        {
            if (Session["EmpresaPermissaoModuloDiversos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresaPermissaoModuloDiversos"];
        }
        set { Session["EmpresaPermissaoModuloDiversos"] = value; }
    }

    //Sessoes Modulo DNPM
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

    //Sessoes Modulo Meio Ambiente
    public ConfiguracaoPermissaoModulo ConfiguracaoModuloMeioAmbiente
    {
        get
        {
            if (Session["ConfiguracaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloMeioAmbiente"];
        }
        set { Session["ConfiguracaoModuloMeioAmbiente"] = value; }
    }
    public IList<Empresa> EmpresasPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["EmpresasPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoModuloMeioAmbiente"];
        }
        set { Session["EmpresasPermissaoModuloMeioAmbiente"] = value; }
    }
    public IList<Processo> ProcessosPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["ProcessosPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<Processo>)Session["ProcessosPermissaoModuloMeioAmbiente"];
        }
        set { Session["ProcessosPermissaoModuloMeioAmbiente"] = value; }
    }
    public IList<CadastroTecnicoFederal> CadastrosTecnicosPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["CadastrosTecnicosPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<CadastroTecnicoFederal>)Session["CadastrosTecnicosPermissaoModuloMeioAmbiente"];
        }
        set { Session["CadastrosTecnicosPermissaoModuloMeioAmbiente"] = value; }
    }
    public IList<OutrosEmpresa> OutrosEmpresasPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["OutrosEmpresasPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<OutrosEmpresa>)Session["OutrosEmpresasPermissaoModuloMeioAmbiente"];
        }
        set { Session["OutrosEmpresasPermissaoModuloMeioAmbiente"] = value; }
    }

    //Sessoes Modulo Contratos
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

    #endregion


    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    private int Contador
    {
        get
        {
            if ((int)Session["contador_notificacoes"] < 0)
                Session["contador_notificacoes"] = 0;
            return (int)Session["contador_notificacoes"];
        }
        set { Session["contador_notificacoes"] = value; }
    }

    private int Indice
    {
        get
        {
            if ((int)Session["indice_notificacoes"] < 0)
                Session["indice_notificacoes"] = 0;
            return (int)Session["indice_notificacoes"];
        }
        set { Session["indice_notificacoes"] = value; }
    }

    private bool Parado
    {
        get
        {
            if (Session["parado_bruscamente"] == null)
                Session["parado_bruscamente"] = false;
            return (bool)Session["parado_bruscamente"];
        }
        set { Session["parado_bruscamente"] = value; }
    }

    private IList<Notificacao> Notificacoes
    {
        get
        {
            if (Session["notificacoes_incluir"] == null)
                Session["notificacoes_incluir"] = new List<Notificacao>();
            return Session["notificacoes_incluir"] as List<Notificacao>;
        }
        set { Session["notificacoes_incluir"] = value; }
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            Usuario user = this.UsuarioLogado.ConsultarPorId();
            this.VerificarAvisos();
            if (user.GrupoEconomico != null && user.GrupoEconomico.PrimeiroAcesso)
            {
                tbxLogin.Text = user.Nome;
                lblAuxSenha_ModalPopupExtender.Show();
            }

            if (user != null && user.EmailSeguranca != null && user.EmailSeguranca != "")
                lblEmailSeguranca.Text = "E-mail de Segurança: <b>" + user.EmailSeguranca + "</b>";
            else
                lblEmailSeguranca.Text = "Você ainda não definiu um e-mail de segurança. Informe-o para receber notificações de alteração de suas permissões no sistema";


        }
        base.OnLoad(e);
    }

    private void VerificarAvisos()
    {
        Aviso a = Aviso.ConsultarUltimoAvisoSistema(false);
        if (a != null)
        {
            if (DateTime.Now > a.DataInicio && DateTime.Now < a.DataFim)
                lblAviso.Text = a.Descricao;
        }
        divPopAviso.Visible = lblAviso.Text.IsNotNullOrEmpty();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CarregarSessoesDePermissoes();
            this.CarregarGrid(DateTime.Now);
            this.CarregarEventosDNPM();
        }
    }

    private void CarregarSessoesDePermissoes()
    {
        this.LimparSessoes();
        this.CarregarPermissoesDNPM();
        this.CarregarPermissoesMeioAmbiente();
        this.CarregarPermissoesContratos();
        this.CarregarPermissoesDiversos();
    }


    private void LimparSessoes()
    {
        //Modulo Diversos
        this.EmpresasPermissaoModuloDiversos = null;

        //Modulo DNPM
        this.EmpresasPermissaoModuloDNPM = null;
        this.ProcessosPermissaoModuloDNPM = null;

        //Modulo Meio Ambiente
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoModuloMeioAmbiente = null;
        this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
        this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;

        //Modulo Contratos
        this.EmpresasPermissaoModuloContratos = null;
        this.SetoresPermissaoModuloContratos = null;
    }

    private void CarregarPermissoesDNPM()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDNPM = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDNPM = null;

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            this.ProcessosPermissaoModuloDNPM = ProcessoDNPM.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.ProcessosPermissaoModuloDNPM = null;
    }

    private void CarregarPermissoesMeioAmbiente()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloMeioAmbiente = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloMeioAmbiente = null;

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            this.ProcessosPermissaoModuloMeioAmbiente = Processo.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = CadastroTecnicoFederal.ObterCadastrosTecnicosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
            this.OutrosEmpresasPermissaoModuloMeioAmbiente = OutrosEmpresa.ObterOutrosEmpresaQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        }
        else
        {
            this.ProcessosPermissaoModuloMeioAmbiente = null;
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
            this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;
        }
    }

    private void CarregarPermissoesContratos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloContratos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloContratos = null;

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
            this.SetoresPermissaoModuloContratos = Setor.ObterSetoresQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.SetoresPermissaoModuloContratos = null;
    }

    private void CarregarPermissoesDiversos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Diversos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDiversos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDiversos = null;
    }    
    

    private void CarregarEventosDNPM()
    {
        IList<ProcessoDNPM> lista = EventoDNPM.FiltrarProcessoDNPMComEventosNaoEditados();
       // IList<ProcessoDNPM> lista = new List<ProcessoDNPM>();
        dgrProcessos.DataSource = lista;
        dgrProcessos.DataBind();
    }

    private void TrocarSenha()
    {
        if (Session["UsuarioLogado_SistemaAmbiental"] != null)
        {
            Usuario user = (Usuario)Session["UsuarioLogado_SistemaAmbiental"];
            string senha = tbxSenha.Text.Trim();
            string confirmar = tbxConfirmacaoSenha.Text.Trim();
            if (senha != confirmar)
            {
                msg.CriarMensagem("A confirmação da senha não corresponde a senha", "Informação", MsgIcons.Exclamacao);
                return;
            }

            if (!SenhaAtendeOsPadroes(tbxSenha.Text))
            {
                msg.CriarMensagem("A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
                return;
            }

            user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim(), true);
            user.GrupoEconomico.PrimeiroAcesso = false;
            user.GrupoEconomico = user.GrupoEconomico.Salvar();
            user = user.Salvar();

            lblAuxSenha_ModalPopupExtender.Hide();
            msg.CriarMensagem("Senha alterada com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private bool SenhaAtendeOsPadroes(string senha)
    {
        int contadorNumeros = 0;
        int contadorLetras = 0;
        String aux = "";
        if (senha.Length < 6)
            return false;

        foreach (Char letra in senha)
        {
            aux = letra.ToString();
            if (aux.IsInt32())
                contadorNumeros = contadorNumeros + 1;
            if (aux.IsLetra())
                contadorLetras = contadorLetras + 1;
        }
        if (contadorNumeros >= 2 && contadorLetras >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            this.TrocarSenha();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            MBOX1.Show(msg);
        }
    }

    protected void btnSalvarEmailSeguranca_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario usuario = this.UsuarioLogado.ConsultarPorId();

            if (usuario != null)
            {
                if (!Utilitarios.Validadores.ValidaEmail(tbxEmailSeguranca.Text))
                {
                    msg.CriarMensagem("O E-mail de Segurança informado não é válido. Informe um e-mail valido para prosseguir.", "Informação", MsgIcons.Exclamacao);
                    return;
                }

                usuario.EmailSeguranca = tbxEmailSeguranca.Text;

                usuario = usuario.Salvar();

                lblEmailSeguranca.Text = "E-mail de Segurança: " + usuario.EmailSeguranca;

                lblAuxEmailSeg_ModalPopupExtender.Hide();

                msg.CriarMensagem("E-mail de Segurança alterado com sucesso.", "Sucesso", MsgIcons.Sucesso);
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            MBOX1.Show(msg);
        }
    }

    protected void btnSalvarEmailSeguranca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanel2);
    }

    protected void btnAlterarEmailSeguranca_Click(object sender, EventArgs e)
    {
        tbxEmailSeguranca.Text = "";
        lblAuxEmailSeg_ModalPopupExtender.Show();
    }

    protected void btnAlterarEmailSeguranca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPopSetor);
    }

    protected void btnEnviarVisualizar_Click(object sender, EventArgs e)
    {
        try
        {
            this.EnviarEmailNotificacaoVisualizada();
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

    private void EnviarEmailNotificacaoVisualizada()
    {
        Notificacao notificacao = Notificacao.ConsultarPorId(hfNotificacao.Value.ToInt32());
        if (notificacao == null)
        {
            msg.CriarMensagem("A notificação selecionada não é válida, tente novamente", "Informação", MsgIcons.Informacao);
            return;
        }
        if (notificacao.GetEmpresa == null || !notificacao.GetEmpresa.GrupoEconomico.Ativo || !notificacao.GetEmpresa.GrupoEconomico.AtivoLogus || !notificacao.GetEmpresa.GrupoEconomico.AtivoAmbientalis)
        {
            msg.CriarMensagem("O Grupo Económico desta notificação encontra-se com o estado de inativo. Para que a notificação seja enviada é necessario sua ativação.", "Informação", MsgIcons.Informacao);
            return;
        }

        try
        {
            Email email = Email.CriarEmailNotificacao(notificacao, true, null);
            if (email.emailsAuxiliares != null && email.emailsAuxiliares.Count > 0)
            {
                bool result = false;
                string erro = "";
                foreach (Email item in email.emailsAuxiliares)
                {
                    result = item.EnviarAutenticado(25, false);
                    erro = item.Erro;
                }

                if (result)
                {
                    notificacao.Enviado++;
                    notificacao.DataUltimoEnvio = DateTime.Now;
                    notificacao = notificacao.Salvar();
                    msg.CriarMensagem("Notificação enviada com sucesso", "Sucesso", MsgIcons.Sucesso);
                    this.CarregarGrid(DateTime.Now);
                }
                else
                {
                    msg.CriarMensagem("Erro ao enviar algum email - Verifique se todos os emails estão corretos. ERRO: " + erro, "Atenção", MsgIcons.Informacao);
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem("Ocorreu um erro ao tentar enviar a notificação, por favor tente novamente. Possível erro = " + ex.Message, "informação", MsgIcons.Informacao);
        }
    }

    private void CarregarGrid(DateTime data)
    {
        data = data.Date;
        IList<Notificacao> notificacoes = Notificacao.FiltrarNotificacoes(data, this.UsuarioLogado.ConsultarPorId(), this.ConfiguracaoModuloDiversos, this.EmpresasPermissaoModuloDiversos, this.ConfiguracaoModuloDNPM,
            this.EmpresasPermissaoModuloDNPM, this.ProcessosPermissaoModuloDNPM, this.ConfiguracaoModuloMeioAmbiente, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente,
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente, this.ConfiguracaoModuloContratos, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos);

        //IList<Notificacao> notificacoes = new List<Notificacao>();
       // this.VerificarDescricaoNotificacoes(notificacoes);

        this.dgr.DataSource = notificacoes;
        this.dgr.DataBind();

        lblQtdNotificacoes.Text = notificacoes.Count + " notificação(ões)";
    }

    public string bindingTipoTemplate(Object o)
    {
        Notificacao aux = ((Notificacao)o);
        return aux.GetTipoTemplate;
    }

    public string bindingEnviado(Object objeto)
    {
        return ((Notificacao)objeto).Enviado + " Vez(es)";
    }


    public string MontarParametro(Object objeto)
    {
        return Seguranca.MontarParametros("p=" + ((ProcessoDNPM)objeto).Id.ToString());
    }



    public string bindingEmpresaGrupoEconomico(Object notificacao)
    {
        Notificacao aux = ((Notificacao)notificacao);
        if (aux.Template == TemplateNotificacao.TemplateVencimentoDiverso)
        {
            VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(aux.Vencimento.Id);
            return (vencimento.Diverso.Empresa != null ? vencimento.Diverso.Empresa.Nome : "--") + " / " + (vencimento.Diverso.Empresa.GrupoEconomico != null ? vencimento.Diverso.Empresa.GrupoEconomico.Nome : "--");
        }

        if (aux.Template == TemplateNotificacao.TemplateVencimentoContratoDiverso)
        {
            VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(aux.Vencimento.Id);
            return (vencimento != null && vencimento.ContratoDiverso != null && vencimento.ContratoDiverso.Empresa != null ? vencimento.ContratoDiverso.Empresa.Nome : "--") + " / " + (vencimento != null && vencimento.ContratoDiverso != null && vencimento.ContratoDiverso.Empresa != null && vencimento.ContratoDiverso.Empresa.GrupoEconomico != null ? vencimento.ContratoDiverso.Empresa.GrupoEconomico.Nome : "--");
        }

        if (aux.Template == TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso)
        {
            VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(aux.Vencimento.Id);
            return (vencimento != null && vencimento.Reajuste != null && vencimento.Reajuste.Empresa != null ? vencimento.Reajuste.Empresa.Nome : "--") + " / " + (vencimento != null && vencimento.Reajuste != null && vencimento.Reajuste.Empresa != null && vencimento.Reajuste.Empresa.GrupoEconomico != null ? vencimento.Reajuste.Empresa.GrupoEconomico.Nome : "--");
        }
        else
            return (aux.GetEmpresa != null ? aux.GetEmpresa.Nome : "--") + " / " + (aux.GetGrupoEconomico != null ? aux.GetGrupoEconomico.Nome : "--");
    }

    protected void dgr_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            this.CarregarVisualizacaoNotificacao(Notificacao.ConsultarPorId(dgr.DataKeys[e.Item.ItemIndex].ToString().ToInt32()));
            this.modal_visualizar_Email.Show();
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

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgr.CurrentPageIndex = e.NewPageIndex;
            this.CarregarGrid(DateTime.Now);
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

    private void LimparVisualizacao()
    {
        hfNotificacao.Value = "0";
        lblEmailVisualizar.Text = tbxEmpresaGrupoVisualizar.Text = tbxEmailsVisualizar.Text = tbxAssuntoVisualizar.Text = string.Empty;
    }

    private void CarregarVisualizacaoNotificacao(Notificacao notificacao)
    {
        this.LimparVisualizacao();
        hfNotificacao.Value = notificacao.Id.ToString();
        Email email = Email.CriarEmailNotificacao(notificacao, true, null);
        if (notificacao.Template == TemplateNotificacao.TemplateVencimentoDiverso)
        {
            VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(notificacao.Vencimento.Id);
            if (vencimento != null)
                this.tbxEmpresaGrupoVisualizar.Text = (vencimento.Diverso != null && vencimento.Diverso.Empresa != null ? vencimento.Diverso.Empresa.Nome : "--") + " / " + (vencimento.Diverso != null && vencimento.Diverso.Empresa != null && vencimento.Diverso.Empresa.GrupoEconomico != null ? vencimento.Diverso.Empresa.GrupoEconomico.Nome : "--");
        }
        else if (notificacao.Template == TemplateNotificacao.TemplateVencimentoContratoDiverso)
        {
            VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(notificacao.Vencimento.Id);
            if (vencimento != null)
                this.tbxEmpresaGrupoVisualizar.Text = (vencimento != null && vencimento.ContratoDiverso != null && vencimento.ContratoDiverso.Empresa != null ? vencimento.ContratoDiverso.Empresa.Nome : "--") + " / " + (vencimento != null && vencimento.ContratoDiverso != null && vencimento.ContratoDiverso.Empresa != null && vencimento.ContratoDiverso.Empresa.GrupoEconomico != null ? vencimento.ContratoDiverso.Empresa.GrupoEconomico.Nome : "--");
        }
        else if (notificacao.Template == TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso)
        {
            VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(notificacao.Vencimento.Id);
            if (vencimento != null)
                this.tbxEmpresaGrupoVisualizar.Text = (vencimento != null && vencimento.Reajuste != null && vencimento.Reajuste.Empresa != null ? vencimento.Reajuste.Empresa.Nome : "--") + " / " + (vencimento != null && vencimento.Reajuste != null && vencimento.Reajuste.Empresa != null && vencimento.Reajuste.Empresa.GrupoEconomico != null ? vencimento.Reajuste.Empresa.GrupoEconomico.Nome : "--");
        }
        else
            this.tbxEmpresaGrupoVisualizar.Text = (notificacao.GetEmpresa != null ? notificacao.GetEmpresa.Nome : "--") + " / " + (notificacao.GetGrupoEconomico != null ? notificacao.GetGrupoEconomico.Nome : "--");
        this.tbxEmailsVisualizar.Text = notificacao.Emails;
        TemplateNotificacao template = TemplateNotificacao.GetTemplateNotificao(notificacao);
        this.tbxAssuntoVisualizar.Text = template != null ? template.AssuntoEmail : "Notificação";
        lblEmailVisualizar.Text = email.Mensagem;
    }


    protected void dgr_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "EditCommand", upCamposVisualizar);
    }


}