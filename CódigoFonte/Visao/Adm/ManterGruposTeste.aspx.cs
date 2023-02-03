using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Adm_ManterGruposTeste : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    #region -------------- Propriedades -------------
    //verificarquantidade de e-mails antes de terminar
    public string EmailContato
    {
        get
        {
            return "anderson@sustentar.inf.br;piassi@sustentar.inf.br;rogerio@sustentar.inf.br;rogerio@logus.inf.br;marcelo.cornelio@logus.inf.br";
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = "0";
                transacao.Abrir();

                this.Pesquisar();
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
    }

    #region ___________Bindings____________


    public String bindingEmail(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        return c.Contato != null ? c.Contato.Email : "";
    }

    public String bindTelefone(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;

        if (c != null && c.Id > 0 && c.Contato != null)
            return c.Contato.Telefone;
        return "";
    }

    public String bindingModulos(Object o)
    {
        string retorno = "";
        GrupoEconomico c = (GrupoEconomico)o;

        if (c != null && c.Id > 0 && c.ModulosPermissao != null && c.ModulosPermissao.Count > 0)
        {
            foreach (ModuloPermissao item in c.ModulosPermissao)
            {
                if (item == c.ModulosPermissao[c.ModulosPermissao.Count - 1])
                    retorno += item.Nome;
                else
                    retorno += item.Nome + ", ";
            }
        }

        return retorno;
    }

    public String bindingLogin(Object o)
    {        
        GrupoEconomico c = (GrupoEconomico)o;

        if (c != null && c.Id > 0 && c.Usuarios != null && c.Usuarios.Count > 0)
        {
            return c.Usuarios[0].Login;
        }

        return "";
    }    

    #endregion

    #region __________Eventos___________

    protected void ibtnNovoGrupoTeste_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastroGrupoTeste);
    }

    protected void btnSalvar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upGruposTeste);
    }

    protected void dgrGruposTeste_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "EditCommand", upCadastroGrupoTeste);
    }

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();

            this.Pesquisar();
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

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes ao(s) grupo(s) de teste selecionados serão perdido(s). Deseja excluir assim mesmo?");
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
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

    protected void ibtnNovoGrupoTeste_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hfIdGrupo.Value = "0";
            tbxNome.Text = tbxEmail.Text = tbxConfirmarSenha.Text = tbxContato.Text = tbxFimTeste.Text = tbxInicioTeste.Text = tbxLogin.Text = tbxSenha.Text = tbxTelefone.Text = "";
            chkContratos.Checked = chkDiversos.Checked = chkDnpm.Checked = chkMeioAmbiente.Checked = false;
            senha_alteracao.Visible = false;
            senha_cadastro.Visible = true;
            modalCadastroGrupo_extender.Show();
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

    protected void btnAlteracaoSenha_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfIdGrupo.Value.ToInt32());
            hfIdUsuario.Value = grupo.Usuarios != null && grupo.Usuarios.Count > 0 ? grupo.Usuarios[0].Id.ToString() : "0";
            modal_alteracao_senha.Show();
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

    protected void dgrGruposTeste_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();

            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(dgrGruposTeste.DataKeys[e.Item.ItemIndex].ToString().ToInt32());

            this.Editar(grupo);

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

    protected void dgrGruposTeste_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();
            dgrGruposTeste.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();

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

    protected void dgrGruposTeste_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();

            IList<GrupoEconomico> clientes = new List<GrupoEconomico>();

            foreach (DataGridItem item in dgrGruposTeste.Items)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    GrupoEconomico cli = GrupoEconomico.ConsultarPorId(dgrGruposTeste.DataKeys[item.ItemIndex].ToString().ToInt32());
                    cli.Excluir();
                    msg.CriarMensagem("Grupo econômico excluído com sucesso!", "Sucesso");
                }
            }
            this.Pesquisar();
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

    #region __________Métodos___________

    private void Pesquisar()
    {
        IList<GrupoEconomico> clientes = GrupoEconomico.FiltrarGruposTeste();
        dgrGruposTeste.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrGruposTeste.DataSource = clientes;
        dgrGruposTeste.DataBind();
        lblQuantidade.Text = clientes.Count() + " Grupo(s) Econômico(s) encontrado(s)";
    }

    private void Editar(GrupoEconomico grupo)
    {
        hfIdGrupo.Value = grupo.Id.ToString();
        tbxNome.Text = grupo.Nome;

        tbxEmail.Text = grupo.Contato != null ? grupo.Contato.Email : "";
        tbxTelefone.Text = grupo.Contato != null ? grupo.Contato.Telefone : "";
        tbxContato.Text = grupo.Responsavel;
        tbxInicioTeste.Text = grupo.InicioTeste.ToShortDateString();
        tbxFimTeste.Text = grupo.FimTeste.ToShortDateString();

        chkDnpm.Checked = grupo.ModulosPermissao != null && grupo.ModulosPermissao.Count > 0 && grupo.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("DNPM"));
        chkMeioAmbiente.Checked = grupo.ModulosPermissao != null && grupo.ModulosPermissao.Count > 0 && grupo.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Meio Ambiente"));
        chkContratos.Checked = grupo.ModulosPermissao != null && grupo.ModulosPermissao.Count > 0 && grupo.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Contratos"));
        chkDiversos.Checked = grupo.ModulosPermissao != null && grupo.ModulosPermissao.Count > 0 && grupo.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Diversos"));
        tbxLogin.Text = grupo.Usuarios != null && grupo.Usuarios.Count > 0 ? grupo.Usuarios[0].Login : "";

        senha_alteracao.Visible = true;
        senha_cadastro.Visible = false;
        modalCadastroGrupo_extender.Show();

    }

    private void Salvar()
    {
        if (!chkContratos.Checked && !chkDiversos.Checked && !chkDnpm.Checked && !chkMeioAmbiente.Checked)
        {
            msg.CriarMensagem("Selecione ao menos um módulo para salvar o grupo de teste", "Informação", MsgIcons.Exclamacao);
            return;
        }

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfIdGrupo.Value.ToInt32());

        if (grupo == null)
            grupo = new GrupoEconomico();

        grupo.Nome = tbxNome.Text;
        grupo.GrupoTeste = true;
        grupo.AtivoAmbientalis = grupo.AtivoLogus = true;

        if (grupo.Contato == null)
            grupo.Contato = new Contato();

        grupo.Contato.Email = tbxEmail.Text;
        grupo.Contato.Telefone = tbxTelefone.Text;
        grupo.Responsavel = tbxContato.Text;
        grupo.InicioTeste = tbxInicioTeste.Text.ToDateTime();
        grupo.FimTeste = tbxFimTeste.Text.ToDateTime();
        grupo.LimiteUsuariosEdicao = 10;

        if (grupo.Usuarios == null)
            grupo.Usuarios = new List<Usuario>();

        Usuario usuario = grupo.Usuarios != null && grupo.Usuarios.Count > 0 ? grupo.Usuarios[0].ConsultarPorId() : new Usuario();

        usuario.Login = usuario.Nome = tbxLogin.Text;
        if (usuario.Id <= 0) 
        {
            if (tbxSenha.Text.Trim() != tbxConfirmarSenha.Text.Trim())
            {
                msg.CriarMensagem("A confirmação da senha não corresponde a senha", "Informação", MsgIcons.Exclamacao);
                return;
            }

            if (!SenhaAtendeOsPadroes(tbxSenha.Text) && hfIdGrupo.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
                return;
            }

            usuario.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim(), true);
        }            

        if (Usuario.ExisteUsuarioComEsteLogin(usuario))
        {
            msg.CriarMensagem("Já existe um usuário com este login, por favor escolha um novo login", "Informação", MsgIcons.Informacao);
            return;
        }

       grupo = grupo.Salvar();
       usuario.UsuarioAdministrador = true;
       usuario = usuario.Salvar();

        //modulos permissão
        grupo.ModulosPermissao = new List<ModuloPermissao>();       

        ModuloPermissao moduloGeral = ModuloPermissao.ConsultarPorNome("Geral");
        grupo.ModulosPermissao.Add(moduloGeral);

        //Configurações de permissões para o usuário - modulo geral
        ConfiguracaoPermissaoModulo configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(grupo.Id, moduloGeral.Id);

        if (configuracaoModuloGeral == null)
            configuracaoModuloGeral = new ConfiguracaoPermissaoModulo();

        configuracaoModuloGeral.Tipo = ConfiguracaoPermissaoModulo.GERAL;

        if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral == null)
            configuracaoModuloGeral.UsuariosEdicaoModuloGeral = new List<Usuario>();

        if (!configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Contains(usuario))
            configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Add(usuario);
        
        configuracaoModuloGeral.Emp = grupo.Id;
        configuracaoModuloGeral.ModuloPermissao = moduloGeral;
        configuracaoModuloGeral = configuracaoModuloGeral.Salvar();
        
                
        ModuloPermissao moduloPermissoes = ModuloPermissao.ConsultarPorNome("Permissões");
        grupo.ModulosPermissao.Add(moduloPermissoes);        

        if (chkDnpm.Checked) 
        {
            ModuloPermissao moduloDnpm = ModuloPermissao.ConsultarPorNome("DNPM");
            grupo.ModulosPermissao.Add(moduloDnpm);

            //Configurações de permissões para o usuário - modulo DNPM
            ConfiguracaoPermissaoModulo configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(grupo.Id, moduloDnpm.Id);

            if (configuracaoModuloDNPM == null)
                configuracaoModuloDNPM = new ConfiguracaoPermissaoModulo();

            configuracaoModuloDNPM.Tipo = ConfiguracaoPermissaoModulo.GERAL;

            if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral == null)
                configuracaoModuloDNPM.UsuariosEdicaoModuloGeral = new List<Usuario>();

            if (!configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(usuario))
                configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Add(usuario);

            configuracaoModuloDNPM.Emp = grupo.Id;
            configuracaoModuloDNPM.ModuloPermissao = moduloDnpm;
            configuracaoModuloDNPM = configuracaoModuloDNPM.Salvar();
        }

        if (chkMeioAmbiente.Checked) 
        {
            ModuloPermissao moduloMeioAmbiente = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
            grupo.ModulosPermissao.Add(moduloMeioAmbiente);

            //Configurações de permissões para o usuário - modulo Meio Ambiente
            ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(grupo.Id, moduloMeioAmbiente.Id);

            if (configuracaoModuloMeioAmbiente == null)
                configuracaoModuloMeioAmbiente = new ConfiguracaoPermissaoModulo();

            configuracaoModuloMeioAmbiente.Tipo = ConfiguracaoPermissaoModulo.GERAL;

            if (configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral == null)
                configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral = new List<Usuario>();

            if (!configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(usuario))
                configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Add(usuario);

            configuracaoModuloMeioAmbiente.Emp = grupo.Id;
            configuracaoModuloMeioAmbiente.ModuloPermissao = moduloMeioAmbiente;
            configuracaoModuloMeioAmbiente = configuracaoModuloMeioAmbiente.Salvar();
        }

        if (chkContratos.Checked) 
        {
            ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");
            grupo.ModulosPermissao.Add(moduloContratos);

            //Configurações de permissões para o usuário - modulo Contratos
            ConfiguracaoPermissaoModulo configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(grupo.Id, moduloContratos.Id);

            if (configuracaoModuloContratos == null)
                configuracaoModuloContratos = new ConfiguracaoPermissaoModulo();

            configuracaoModuloContratos.Tipo = ConfiguracaoPermissaoModulo.GERAL;

            if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral == null)
                configuracaoModuloContratos.UsuariosEdicaoModuloGeral = new List<Usuario>();

            if (!configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(usuario))
                configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Add(usuario);

            configuracaoModuloContratos.Emp = grupo.Id;
            configuracaoModuloContratos.ModuloPermissao = moduloContratos;
            configuracaoModuloContratos = configuracaoModuloContratos.Salvar();
        }

        if (chkDiversos.Checked) 
        {
            ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");
            grupo.ModulosPermissao.Add(moduloDiversos);

            //Configurações de permissões para o usuário - modulo Diversos
            ConfiguracaoPermissaoModulo configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(grupo.Id, moduloDiversos.Id);

            if (configuracaoModuloDiversos == null)
                configuracaoModuloDiversos = new ConfiguracaoPermissaoModulo();

            configuracaoModuloDiversos.Tipo = ConfiguracaoPermissaoModulo.GERAL;

            if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral == null)
                configuracaoModuloDiversos.UsuariosEdicaoModuloGeral = new List<Usuario>();

            if (!configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(usuario))
                configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Add(usuario);

            configuracaoModuloDiversos.Emp = grupo.Id;
            configuracaoModuloDiversos.ModuloPermissao = moduloDiversos;
            configuracaoModuloDiversos = configuracaoModuloDiversos.Salvar();
        }  

        usuario = usuario.Salvar();
        grupo.Usuarios.Add(usuario);
        grupo.DataCadastro = grupo.Id > 0 ? grupo.DataCadastro : DateTime.Now;

        bool ouveAlteracao = this.OuveAlteracaoNosDadosDoGrupo(grupo);

        grupo = grupo.Salvar();
        grupo.Emp = grupo.Id;
        grupo = grupo.Salvar();
        usuario.Emp = grupo.Emp;
        usuario = usuario.Salvar();

        msg.CriarMensagem("Grupo Econômico de teste salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        if (hfIdGrupo.Value.ToInt32() > 0 && ouveAlteracao)
        {
            this.EnviarEmailAlteracaoParaOGrupo();
            this.EnviarEmailAlteracaoParaEquipe();
        }
        else 
        {
            this.EnviarEmailParaOGrupo();
            this.EnviarEmailParaEquipe();
        }

        modalCadastroGrupo_extender.Hide();
        this.Pesquisar();

    }

    private void EnviarEmailAlteracaoParaOGrupo()
    {
        Email email = new Email();
        email.Assunto = "Cadastro de Grupo Econômico de Teste - Sistema Sustentar";
        email.EmailsDestino.Add(tbxEmail.Text);
        string modulos = (chkDnpm.Checked ? "ANM" : "");
        modulos += (chkMeioAmbiente.Checked ? modulos.Contains("ANM") ? ", Meio Ambiente" : "Meio Ambiente" : "");
        modulos += (chkContratos.Checked ? modulos.Contains("ANM") || modulos.Contains("Meio Ambiente") ? ", Contratos " : "Contratos" : "");
        modulos += (chkDiversos.Checked ? modulos.Contains("ANM") || modulos.Contains("Meio Ambiente") || modulos.Contains("Contratos") ? ", Diversos" : "Diversos" : "");
        String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Alteração de Grupo Econômico de Teste<br/>Sistema Sustentar</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; height:auto'>
                Prezado " + tbxNome.Text + @".
            </div>
            <div style='margin-left:30px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; text-align:justify;font-size:14px;padding:7px; height:auto'>
            Houve uma alteração nos dados de acesso do seu grupo econômico de teste do Sistema Sustentar:<br />
                Módulos: " + modulos + @".<br />
                Período: de " + tbxInicioTeste.Text + @" à " + tbxFimTeste.Text + @".<br />
                Para logar no sistema acesse www.sustentar.inf.br e utilize o login " + tbxLogin.Text + @" e a senha " + tbxSenha.Text + @".<br />
                Qualquer dúvida, entre em contato com nosso suporte em www.sustentar.inf.br<br /><br />
                Ass. Equipe Sustentar.
        </div>
        <div style='width:100%; height:20px;'>
        </div>
    </div>";
        email.Mensagem = mensagemEmail;
        if (email.EnviarAutenticado(25, false))
        {
            msg.CriarMensagem("E-mail enviado com sucesso.", "Sucesso");
        }
    }

    private bool OuveAlteracaoNosDadosDoGrupo(GrupoEconomico grupo)
    {
        return grupo.InicioTeste.ToShortDateString() != tbxInicioTeste.Text || grupo.FimTeste.ToShortDateString() != tbxFimTeste.Text || grupo.Contato.Email.Split(';')[0].Trim() != tbxEmail.Text || grupo.Usuarios[0].Login != tbxLogin.Text || Utilitarios.Criptografia.Criptografia.Decrypt(grupo.Usuarios[0].Senha, true) != tbxSenha.Text;
    }

    private void EnviarEmailParaOGrupo()
    {
        Email email = new Email();
        email.Assunto = "Cadastro de Grupo Econômico de Teste - Sistema Sustentar";
        email.EmailsDestino.Add(tbxEmail.Text);
        string modulos = (chkDnpm.Checked ? "ANM" : "");
        modulos += (chkMeioAmbiente.Checked ? modulos.Contains("ANM") ? ", Meio Ambiente" : "Meio Ambiente" : "");
        modulos += (chkContratos.Checked ? modulos.Contains("ANM") || modulos.Contains("Meio Ambiente") ? ", Contratos " : "Contratos" : "");
        modulos += (chkDiversos.Checked ? modulos.Contains("ANM") || modulos.Contains("Meio Ambiente") || modulos.Contains("Contratos") ? ", Diversos" : "Diversos" : "");
        String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Cadastro de Grupo Econômico de Teste<br/>Sistema Sustentar</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; height:auto'>
                Prezado " + tbxNome.Text + @".
            </div>
            <div style='margin-left:30px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; text-align:justify;font-size:14px;padding:7px; height:auto'>
            Você foi cadastrado para acessar o(s) módulos " + modulos + @" do sistema Sustentar por um período de testes que vai de " + tbxInicioTeste.Text + @" à " + tbxFimTeste.Text + @".<br />
                Para logar no sistema acesse www.sustentar.inf.br e utilize o login " + tbxLogin.Text + @" e a senha " + tbxSenha.Text + @".<br />
                Qualquer dúvida, entre em contato com nosso suporte em www.sustentar.inf.br<br /><br />
                Ass. Equipe Sustentar.
        </div>
        <div style='width:100%; height:20px;'>
        </div>
    </div>";
        email.Mensagem = mensagemEmail;
        if (email.EnviarAutenticado(25, false))
        {
            msg.CriarMensagem("E-mail enviado com sucesso.", "Sucesso");
        }
    }

    private void EnviarEmailParaEquipe()
    {
        if (Validadores.ValidaEmail(tbxEmail.Text)) 
        {
            Email email = new Email();
            email.Assunto = "Cadastro de Grupo Econômico de Teste - Sistema Sustentar";          
            String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Cadastro de Grupo Econômico de Teste<br/>Sistema Sustentar</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; height:auto'>
                O Grupo " + tbxNome.Text + @", foi cadastrado no sistema como um grupo de teste do período de " + tbxInicioTeste.Text + @" até " + tbxFimTeste.Text + @".
            </div>        
        <div style='width:100%; height:20px;'>
        </div></div> ";
            email.Mensagem = mensagemEmail;

            string[] emailStr = this.EmailContato.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < emailStr.Length; i++)
                email.EmailsDestino.Add(emailStr[i]);            

            if (email.EnviarAutenticado(25, false))
            {
                msg.CriarMensagem("E-mail enviado com sucesso.", "Sucesso");
            }
        }
    }

    private void EnviarEmailAlteracaoParaEquipe()
    {
        if (Validadores.ValidaEmail(tbxEmail.Text))
        {
            Email email = new Email();
            email.Assunto = "Cadastro de Grupo Econômico de Teste - Sistema Sustentar";
            String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Alteração de Grupo Econômico de Teste<br/>Sistema Sustentar</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; height:auto'>
                O Grupo " + tbxNome.Text + @", teve seus dados alterados no sistema: período de " + tbxInicioTeste.Text + @" até " + tbxFimTeste.Text + @".
            </div>        
        <div style='width:100%; height:20px;'>
        </div></div> ";
            email.Mensagem = mensagemEmail;

            string[] emailStr = this.EmailContato.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < emailStr.Length; i++)
                email.EmailsDestino.Add(emailStr[i]);

            if (email.EnviarAutenticado(25, false))
            {
                msg.CriarMensagem("E-mail enviado com sucesso.", "Sucesso");
            }
        }
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

    #endregion
    
    protected void btnAlterarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.AlterarSenha();
            modal_alteracao_senha.Hide();
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

    private void AlterarSenha()
    {
        Usuario user = Usuario.ConsultarPorId(hfIdUsuario.Value.ToInt32());
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
            
            this.EnviarEmailAlteracaoSenhaParaOGrupo();            
        }
    }

    private void EnviarEmailAlteracaoSenhaParaOGrupo()
    {
        Email email = new Email();
        email.Assunto = "Alteração no Cadastro de Grupo Econômico de Teste - Sistema Sustentar";
        email.EmailsDestino.Add(tbxEmail.Text);
        string modulos = (chkDnpm.Checked ? "ANM" : "");
        modulos += (chkMeioAmbiente.Checked ? modulos.Contains("ANM") ? ", Meio Ambiente" : "Meio Ambiente" : "");
        modulos += (chkContratos.Checked ? modulos.Contains("ANM") || modulos.Contains("Meio Ambiente") ? ", Contratos " : "Contratos" : "");
        modulos += (chkDiversos.Checked ? modulos.Contains("ANM") || modulos.Contains("Meio Ambiente") || modulos.Contains("Contratos") ? ", Diversos" : "Diversos" : "");
        String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Alteração de Grupo Econômico de Teste<br/>Sistema Sustentar</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; height:auto'>
                Prezado " + tbxNome.Text + @".
            </div>
            <div style='margin-left:30px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; text-align:justify;font-size:14px;padding:7px; height:auto'>
            Houve uma alteração nos dados de acesso do seu grupo econômico de teste do Sistema Sustentar:<br />
                Módulos: " + modulos + @".<br />
                Período: de " + tbxInicioTeste.Text + @" à " + tbxFimTeste.Text + @".<br />
                Para logar no sistema acesse www.sustentar.inf.br e utilize o login " + tbxLogin.Text + @" e a senha " + tbxNovaSenha.Text + @".<br />
                Qualquer dúvida, entre em contato com nosso suporte em www.sustentar.inf.br<br /><br />
                Ass. Equipe Sustentar.
        </div>
        <div style='width:100%; height:20px;'>
        </div>
    </div>";
        email.Mensagem = mensagemEmail;
        if (email.EnviarAutenticado(25, false))
        {
            msg.CriarMensagem("E-mail enviado com sucesso.", "Sucesso");
        }
    }

    protected void btnAlteracaoSenha_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAlterarSenha);
    }
    
}