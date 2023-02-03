using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Configuration;

public partial class Acesso_LoginADM : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    public void SetIdConfig()
    {
        Session["idConfig"] = ddlUsuario.SelectedValue;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            this.SetIdConfig();
            try
            {
                transacao.Abrir();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                if (msg.Mensagem.IsNotNullOrEmpty())
                    Alert.Show(msg.Mensagem);
            }
        }
    }

    #region ________ Eventos ___________

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            this.SetIdConfig();
            transacao.Abrir();
            this.Acessar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            if (msg.Mensagem.IsNotNullOrEmpty())
                Alert.Show(msg.Mensagem);
        }
    }

    #endregion

    #region ________ Métodos ___________

    private void Acessar()
    {
        Administrador administrador = new Administrador();
        administrador.SenhaAtivacao = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim(), true);
        administrador.Id = ddlUsuario.SelectedValue == "0" ? 1 : 2;

        if (Administrador.ValidarAdministradorSenhaAtivacao(ref administrador))
        {
            Session["UsuarioAdministradorLogado_SistemaAmbiental"] = administrador;
            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
                Response.Redirect(pagina);
            else
                Response.Redirect("../Adm/Index.aspx", false);
        }
        else
            msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente", "");
    }

    #endregion
}