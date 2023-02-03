using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Text;

public partial class Adm_ManterUsuarioAdm : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    private Administrador UserLogado
    {
        get
        {
            if (Session["UsuarioAdministradorLogado_SistemaAmbiental"] != null)
                return (Administrador)Session["UsuarioAdministradorLogado_SistemaAmbiental"];
            else
                return null;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {       

        if (!IsPostBack)
        {
            try
            {
                hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request);
                string bd = Utilitarios.Criptografia.Seguranca.RecuperarParametro("bd", this.Request);
                if (bd.IsNotNullOrEmpty())
                {
                    ddlSistema.SelectedValue = bd;
                    ddlSistema.Enabled = false;
                }

                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();

                this.CarregarCampos();

                if (hfId.Value.ToInt32() > 0) { 
                    this.CarregarUsuario(hfId.Value);
                    this.mostrarBotaoEnviar();
                }                

                if (ddlSistema.SelectedValue.ToInt32() > 0)
                {
                    ddlAdministrador.Enabled = true;
                }
                else
                {
                    ddlAdministrador.SelectedIndex = 0;
                    ddlAdministrador.Enabled = false;
                    ddlCliente.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                this.GetMBOX<MBOX>().Show(msg);
            }
        }

        this.descricao_Administrador.Visible = this.campo_Administrador.Visible = this.UserLogado != null && this.UserLogado != null;

        ddlCliente.Enabled = this.descricao_Administrador.Visible && ddlAdministrador.SelectedIndex == 0;

    }

    #region ________ Eventos ___________

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Salvar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnEnviarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (hfId.Value.ToInt32() > 0)
            {

                Usuario user = Usuario.ConsultarPorId(hfId.Value.ToInt32());

                Email email = new Email();
                email.Assunto = "Dados de Usuário - Sistema Sustentar";
                email.EmailsDestino.Add(user.Email);

                String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px;'>Dados de Usuário</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; background-color:#E9E9E9; text-align:center; height:auto'>Para acessar o Sistema Sustentar visite o endereço <a href='http://www.sustentar.inf.br' target='_blank'>www.sustentar.inf.br</a> entre em 'Acesso' e preencha com os dados abaixo.
    
            </div>
            <table style='width:100%; margin-top:10px; height:auto;font-family:Arial, Helvetica, sans-serif; font-size:14px;'><tbody><tr>
            <td align='right' width='50%' style='font-weight:bold'>Nome:</td><td align='left' width='50%'>" + user.Nome + @"</td></tr><tr><td align='right' width='50%' style='font-weight:bold'>Login:
            </td><td align='left' width='50%'>" + user.Login + @"</td></tr><tr><td align='right' width='50%' style='font-weight:bold'>Senha:</td>
            <td align='left' width='50%'>" + Utilitarios.Criptografia.Criptografia.Decrypt(user.Senha, true) + @"</td></tr></tbody></table><div style='width:100%; height:20px;'></div></div>";
                email.Mensagem = mensagemEmail;
                if (email.EnviarAutenticado(25, false))
                {
                    msg.CriarMensagem("Senha enviada com sucesso.", "Sucesso");
                }
                else
                {
                    msg.CriarMensagem("Erro ao enviar senha.", "Erro");
                }
            }
            else
            {
                msg.CriarMensagem("Erro ao enviar senha. Salve um usuário.", "Erro");
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }

    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Novo();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Excluir();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlAdministrador_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            ddlCliente.Enabled = ddlAdministrador.SelectedIndex == 0;           
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        btnExcluir.Visible = hfId.Value.ToInt32() > 0;
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir este usuário?");
    }

    protected void btnAlterarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.AlterarSenha();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnAlteracaoSenha_PreRender(object sender, EventArgs e)
    {
        this.HabilitarDesabilitarAlterarSenha();
    }

    protected void btnAlteracaoSenha_Click(object sender, EventArgs e)
    {
        this.modal_alteracao_senha.Show();
    }

    protected void btnAlteracaoSenha_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAdministradorCliente);
    }

    protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            //if (ddlSistema.SelectedValue == "1" && ddlCliente.SelectedValue != "0")
            //{
            //    chkEditar.Checked = false;
            //    chkEditar.Enabled = false;
            //}
            //else
            //{
            //    chkEditar.Enabled = true;
            //}

            if (ddlSistema.SelectedValue.ToInt32() > 0)
            {                
                ddlAdministrador.Enabled = true;
            }
            else 
            {
                ddlAdministrador.SelectedIndex = 0;
                ddlAdministrador.Enabled = false;
                ddlCliente.Enabled = true;
            }

            transacao.Abrir();
            this.CarregarCampos();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (ddlAdministrador.SelectedValue == "0")
            {
                ddlCliente.Enabled = true;
                ddlAdministrador.Enabled = false;

            }
            if (ddlCliente.SelectedValue == "0")
            {
                ddlAdministrador.Enabled = true;
               // chkEditar.Enabled = true;
            }
            else
            {
                //if (ddlSistema.SelectedValue == "1")
                //{
                //    chkEditar.Checked = false;
                //    chkEditar.Enabled = false;
                //}
            }  
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }

    }

    protected void bntGerarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            this.SugestaoSenha();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.InportarDadosDoGrupo();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

    #region ________ Métodos ___________

    private void mostrarBotaoEnviar() {
        if (hfId.Value.ToInt32() > 0)
        {
            btnEnviarSenha.Visible = true;
        }
        else
        {
            btnEnviarSenha.Visible = false;
        }
    }

    private void CarregarUsuario(string id)
    {
        Usuario user = Usuario.ConsultarPorId(id.ToInt32());
        if (user != null)
        {
            hfId.Value = user.Id.ToString();
            if (user.Administrador != null)
            {
                ddlAdministrador.SelectedValue = user.Administrador.Id.ToString();
            }
            else if (user.GrupoEconomico != null)
            {
                ddlCliente.SelectedValue = user.GrupoEconomico.Id.ToString();
            }
            chkUsuarioAdministrador.Checked = user.UsuarioAdministrador;
            tbxNome.Text = user.Nome;
            tbxLogin.Text = user.Login;
            tbxSenha.Text = user.Senha;
            tbxConfirmarSenha.Text = user.Senha;
            tbxEmail.Text = user.Email;
            
            //if (ddlSistema.SelectedValue == "1" && ddlCliente.SelectedValue != "0")
            //    chkEditar.Enabled = chkEditar.Checked = false;
            label_senha.Visible = false;            
        }
    }

    private void Salvar()
    {

        if (tbxSenha.Text.Trim() != tbxConfirmarSenha.Text.Trim())
        {
            msg.CriarMensagem("A confirmação da senha não corresponde a senha", "Informação", MsgIcons.Exclamacao);
            return;
        }

        if (!SenhaAtendeOsPadroes(tbxSenha.Text) && hfId.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
            label_senha.Attributes.CssStyle.Add("color", "Red");
            return;
        }

        Usuario user = Usuario.ConsultarPorId(hfId.Value.ToInt32());

        if (user == null)
            user = new Usuario();
        if (ddlAdministrador.Visible)
        {
            user.Administrador = Administrador.ConsultarPorId(ddlAdministrador.SelectedValue.ToInt32());
        }
        if (ddlCliente.Visible)
            user.GrupoEconomico = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

        if (user.Administrador == null && user.GrupoEconomico == null)
        {
            msg.CriarMensagem("É necessário selecionar ao menos um " +
                (ddlAdministrador.Visible && ddlCliente.Visible ? "administrador ou Grupo Econômico" : ddlAdministrador.Visible ? "administrador" : "Grupo Econômico")
                , "Informação", MsgIcons.Informacao);

            return;
        }

        if (hfId.Value.ToInt32() > 0 && user.Administrador != null && user.Administrador.Id > 0)

            //Deve-se selecionar ao menos um administrador ou cliente
            if (user.Administrador == null && user.GrupoEconomico == null)
            {
                msg.CriarMensagem("É necessário selecionar ao menos um " +
                    (ddlAdministrador.Visible && ddlCliente.Visible ? "administrador ou Grupo Econômico" : ddlAdministrador.Visible ? "administrador" : "Grupo Econômico")
                    , "Informação", MsgIcons.Informacao);

                return;
            }

        if (user.GrupoEconomico != null)
        {
            user.Emp = user.GrupoEconomico.Id;
            if (user.Id == 0 && GrupoEconomico.AtingiuLimiteUsuarios(user))
            {
                msg.CriarMensagem("O número máximo de usuários foi atingido, não é possível salvar.", "Informação", MsgIcons.Informacao);
                return;
            }
        }

        user.Nome = tbxNome.Text.Trim();
        user.Login = tbxLogin.Text.Trim();

        if (user.Id == 0)
            user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim(), true);

        user.Email = tbxEmail.Text.Trim();

        if (Usuario.ExisteUsuarioComEsteLogin(user))
        {
            msg.CriarMensagem("Já existe um usuário com este login, por favor escolha um novo login", "Informação", MsgIcons.Informacao);
            return;
        }
        
        user.UsuarioAdministrador = chkUsuarioAdministrador.Checked;

        user = user.Salvar();
        this.hfId.Value = user.Id.ToString();
        this.HabilitarDesabilitarAlterarSenha();
        this.btnExcluir.Visible = true;
        label_senha.Visible = false;

        if (user.UsuarioAdministrador) 
        {
            IList<ModuloPermissao> modulosPermissao;

            //Setando Permissões
            if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                modulosPermissao = user.GrupoEconomico.ModulosPermissao;
            else
                modulosPermissao = ModuloPermissao.ConsultarTodos();

            //Módulo Geral
            ModuloPermissao moduloGeral = ModuloPermissao.ConsultarPorNome("Geral");
            if (modulosPermissao.Contains(moduloGeral)) 
            {
                //Configurações de permissões para o usuário - modulo geral
                ConfiguracaoPermissaoModulo configuracaoModuloGeral;

                if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloGeral.Id);
                else
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloGeral.Id);

                if (configuracaoModuloGeral == null)
                    configuracaoModuloGeral = new ConfiguracaoPermissaoModulo();

                configuracaoModuloGeral.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral == null)
                    configuracaoModuloGeral.UsuariosEdicaoModuloGeral = new List<Usuario>();

                if (!configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Contains(user))
                    configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Add(user);
                                
                configuracaoModuloGeral.Emp = user.GrupoEconomico != null && user.GrupoEconomico.Id > 0 ? user.GrupoEconomico.Id : 0;
                configuracaoModuloGeral.ModuloPermissao = moduloGeral;
                configuracaoModuloGeral = configuracaoModuloGeral.Salvar();
            }

            //Módulo DNPM
            ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");
            if (modulosPermissao.Contains(moduloDNPM))
            {
                //Configurações de permissões para o usuário - modulo geral
                ConfiguracaoPermissaoModulo configuracaoModuloDNPM;

                if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloDNPM.Id);
                else
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

                if (configuracaoModuloDNPM == null)
                    configuracaoModuloDNPM = new ConfiguracaoPermissaoModulo();

                configuracaoModuloDNPM.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral == null)
                    configuracaoModuloDNPM.UsuariosEdicaoModuloGeral = new List<Usuario>();

                if (!configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(user))
                    configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Add(user);

                configuracaoModuloDNPM.Emp = user.GrupoEconomico != null && user.GrupoEconomico.Id > 0 ? user.GrupoEconomico.Id : 0;
                configuracaoModuloDNPM.ModuloPermissao = moduloDNPM;
                configuracaoModuloDNPM = configuracaoModuloDNPM.Salvar();
            }

            //Módulo Meio Ambiente
            ModuloPermissao moduloMA = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
            if (modulosPermissao.Contains(moduloMA))
            {
                //Configurações de permissões para o usuário - modulo meio ambiente
                ConfiguracaoPermissaoModulo configuracaoModuloMA;

                if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                    configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloMA.Id);
                else
                    configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloMA.Id);

                if (configuracaoModuloMA == null)
                    configuracaoModuloMA = new ConfiguracaoPermissaoModulo();

                configuracaoModuloMA.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                if (configuracaoModuloMA.UsuariosEdicaoModuloGeral == null)
                    configuracaoModuloMA.UsuariosEdicaoModuloGeral = new List<Usuario>();

                if (!configuracaoModuloMA.UsuariosEdicaoModuloGeral.Contains(user))
                    configuracaoModuloMA.UsuariosEdicaoModuloGeral.Add(user);

                configuracaoModuloMA.Emp = user.GrupoEconomico != null && user.GrupoEconomico.Id > 0 ? user.GrupoEconomico.Id : 0;
                configuracaoModuloMA.ModuloPermissao = moduloMA;
                configuracaoModuloMA = configuracaoModuloMA.Salvar();
            }

            //Módulo Contratos
            ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");
            if (modulosPermissao.Contains(moduloContratos))
            {
                //Configurações de permissões para o usuário - modulo contratos
                ConfiguracaoPermissaoModulo configuracaoModuloContratos;

                if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloContratos.Id);
                else
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloContratos.Id);

                if (configuracaoModuloContratos == null)
                    configuracaoModuloContratos = new ConfiguracaoPermissaoModulo();

                configuracaoModuloContratos.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral == null)
                    configuracaoModuloContratos.UsuariosEdicaoModuloGeral = new List<Usuario>();

                if (!configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(user))
                    configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Add(user);

                configuracaoModuloContratos.Emp = user.GrupoEconomico != null && user.GrupoEconomico.Id > 0 ? user.GrupoEconomico.Id : 0;
                configuracaoModuloContratos.ModuloPermissao = moduloContratos;
                configuracaoModuloContratos = configuracaoModuloContratos.Salvar();
            }

            //Módulo Diversos
            ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");
            if (modulosPermissao.Contains(moduloDiversos))
            {
                //Configurações de permissões para o usuário - modulo diversos
                ConfiguracaoPermissaoModulo configuracaoModuloDiversos;

                if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloDiversos.Id);
                else
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

                if (configuracaoModuloDiversos == null)
                    configuracaoModuloDiversos = new ConfiguracaoPermissaoModulo();

                configuracaoModuloDiversos.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral == null)
                    configuracaoModuloDiversos.UsuariosEdicaoModuloGeral = new List<Usuario>();

                if (!configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(user))
                    configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Add(user);

                configuracaoModuloDiversos.Emp = user.GrupoEconomico != null && user.GrupoEconomico.Id > 0 ? user.GrupoEconomico.Id : 0;
                configuracaoModuloDiversos.ModuloPermissao = moduloDiversos;
                configuracaoModuloDiversos = configuracaoModuloDiversos.Salvar();
            }
        }

        

        msg.CriarMensagem("Usuário salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
        this.mostrarBotaoEnviar();
    }

    private bool SenhaAtendeOsPadroes(String senha)
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

    private void Novo()
    {
        Response.Redirect("ManterUsuarioAdm.aspx");
    }

    private void Excluir()
    {
        Usuario user = Usuario.ConsultarPorId(hfId.Value.ToInt32());
        if (user != null)
        {
            user.Excluir();
            this.Novo();
        }
        else
            msg.CriarMensagem("Não há algum usuário carregado", "Informação", MsgIcons.Informacao);
    }

    private void CarregarCampos()
    {
        this.CarregarAdministradores();
        this.CarregarClientes();
    }

    private void CarregarAdministradores()
    {
        ddlAdministrador.DataTextField = "Nome";
        ddlAdministrador.DataValueField = "Id";

        IList<Administrador> administradores = Administrador.ConsultarTodosOrdemAlfabetica();

        Administrador aux = new Administrador();
        aux.Nome = "-- Selecione --";
        administradores.Insert(0, aux);

        ddlAdministrador.DataSource = administradores;
        ddlAdministrador.DataBind();
    }

    private void CarregarClientes()
    {
        ddlCliente.DataTextField = "Nome";
        ddlCliente.DataValueField = "Id";

        IList<GrupoEconomico> clientes = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        GrupoEconomico aux = new GrupoEconomico();
        aux.Nome = "-- Selecione --";
        clientes.Insert(0, aux);

        ddlCliente.DataSource = clientes;
        ddlCliente.DataBind();
    }

    private void AlterarSenha()
    {
        Usuario user = Usuario.ConsultarPorId(hfId.Value.ToInt32());
        if (user != null)
        {
            if (tbxNovaSenha.Text.Trim() != tbxConfirmarNovaSenha.Text.Trim())
            {
                msg.CriarMensagem("A confirmação da senha não corresponde à senha", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!SenhaAtendeOsPadroes(tbxNovaSenha.Text))
            {
                msg.CriarMensagem("A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
                return;
            }

            user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxNovaSenha.Text.Trim(), true);
            user = user.Salvar();
            msg.CriarMensagem("Senha alterada com sucesso", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void HabilitarDesabilitarAlterarSenha()
    {
        this.descricao_campo_alterarSenha.Visible = this.campo_alterarSenha.Visible = this.hfId.Value.ToInt32() > 0;
        this.descricao_campo_senha.Visible = this.campo_senha.Visible =
            this.descricao_campo_confirmar_senha.Visible = this.campo_confirmar_senha.Visible =
            this.hfId.Value.ToInt32() == 0;
    }

    private void SugestaoSenha()
    {
        string senha = this.GerarSenha();
        while (!this.SenhaAtendeOsPadroes(senha))
            senha = this.GerarSenha();
        tbxSugestaoSenha.Text = senha;
    }

    private string GerarSenha()
    {
        string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string senha = "";
        int quantDigitos = 8;
        Random random = new Random();
        for (int i = 0; i < quantDigitos; i++)
        {
            if (random.Next(2) == 1)
                senha += random.Next(0, 10).ToString();
            else
                senha += caracteres[random.Next(0, caracteres.Length)];
        }
        return senha;
    }

    private void InportarDadosDoGrupo()
    {
        GrupoEconomico g = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());
        if (g == null)
            return;

        tbxNome.Text = g.RepresentanteLegal;
        tbxEmail.Text = g.Contato.Email;
    }    

    #endregion
}