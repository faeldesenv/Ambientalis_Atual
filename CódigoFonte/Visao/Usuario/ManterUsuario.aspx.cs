using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Usuario_ManterUsuario : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {                    
            this.CarregarCampos();

            string id = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request);
            if (!string.IsNullOrEmpty(id))
                this.CarregarUsuario(id);
        }

        this.descricao_Administrador.Visible = this.campo_Administrador.Visible =
            this.UsuarioLogado != null && this.UsuarioLogado.Administrador != null;

        ddlCliente.Enabled = this.descricao_Administrador.Visible && ddlAdministrador.SelectedIndex == 0;
        if (!ddlCliente.Enabled)
            ddlCliente.SelectedValue = this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null ? this.UsuarioLogado.GrupoEconomico.Id.ToString() : "0";

    }

    #region ________ Eventos ___________

    protected void btnSalvar_Click(object sender, EventArgs e)
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            this.Novo();
        }
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

    protected void ddlAdministrador_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCliente.Enabled = ddlAdministrador.SelectedIndex == 0;
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
            this.AlterarSenha();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
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

    #endregion

    #region ________ Métodos ___________

    private void CarregarUsuario(string id)
    {
        Usuario user = Usuario.ConsultarPorId(id.ToInt32());
        if (user != null)
        {
            hfId.Value = user.Id.ToString();
            if (user.Administrador != null)
                ddlAdministrador.SelectedValue = user.Administrador.Id.ToString();
            else if (user.GrupoEconomico != null)
                ddlCliente.SelectedValue = user.GrupoEconomico.Id.ToString();
            tbxNome.Text = user.Nome;
            tbxLogin.Text = user.Login;
            tbxSenha.Text = user.Senha;
            tbxConfirmarSenha.Text = user.Senha;
            tbxEmail.Text = user.Email;           
            chkUsuarioAdministrador.Checked = user.UsuarioAdministrador;
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
            user.Administrador = Administrador.ConsultarPorId(ddlAdministrador.SelectedValue.ToInt32());
        if (ddlCliente.Visible)
            user.GrupoEconomico = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

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
                msg.CriarMensagem("O número máximo de usuários foi atingido, não é possível salvar", "Informação", MsgIcons.Informacao);
                return;
            }
        }

        user.UsuarioAdministrador = chkUsuarioAdministrador.Checked;
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
        
        user = user.Salvar();
        this.hfId.Value = user.Id.ToString();
        this.HabilitarDesabilitarAlterarSenha();
        this.btnExcluir.Visible = true;
        label_senha.Visible = false;
        msg.CriarMensagem("Usuário salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    private void Novo()
    {
        Response.Redirect("ManterUsuario.aspx");
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
        this.CarregarClientes();
        this.CarregarAdministradores();
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
            if (Utilitarios.Criptografia.Criptografia.Decrypt(user.Senha.Trim(), true) != tbxSenhaAntiga.Text.Trim())
            {
                msg.CriarMensagem("A senha antiga está incorreta", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tbxNovaSenha.Text.Trim() != tbxConfirmarNovaSenha.Text.Trim())
            {
                msg.CriarMensagem("A confirmação da senha não corresponde à senha", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!SenhaAtendeOsPadroes(tbxNovaSenha.Text))
            {
                msg.CriarMensagem("A senha deve ter no minímo 6 dígitos, sendo no mínimo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
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

    #endregion
    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            if (ddlAdministrador.SelectedValue == "0")
            {
                ddlCliente.Enabled = true;
                ddlAdministrador.Enabled = false;
            }
            if (ddlCliente.SelectedValue == "0")
            {
                ddlAdministrador.Enabled = true;                
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
}