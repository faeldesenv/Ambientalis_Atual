using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using System.Configuration;
using Modelo;


public partial class Acesso_Login : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    public void SetIdConfig()
    {            
        Session["idConfig"] = "0";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            this.SetIdConfig();
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
            transacao.DestacarSessao();
            transacao.Fechar(ref msg);
            if (msg.Mensagem.IsNotNullOrEmpty())
                Alert.Show(msg.Mensagem);
        }
    }

    #endregion

    #region ________ Métodos ___________

    private void Acessar()
    {
        Session["idEmp"] = null;

        UsuarioComercial user = new UsuarioComercial();
        user.Login = tbxLogin.Text.Trim();
        user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim(), true);
        user = UsuarioComercial.ValidaUsuario(ref user);

        if (user != null)
        {
            Session["UsuarioLogado_SistemaComercial"] = user;

            if (user.GetType() == typeof(UsuarioRevendaComercial)) 
            {
                Revenda revenda = Revenda.ConsultarPorId(((UsuarioRevendaComercial)user).Revenda.Id);
                if (revenda.GetUltimoContrato == null || revenda.GetUltimoContrato.Aceito == false || revenda.GetUltimoContrato.Desativado) 
                {
                    msg.CriarMensagem("Esta revenda se encontra desativada. Não é possível fazer login no Sistema", "");
                    return;
                }
                Session["idEmp"] = ((UsuarioRevendaComercial)user).Revenda.Id;
            }
          
            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
                Response.Redirect(pagina, false);
            else
                Response.Redirect("../Site/Index.aspx", false);
        }
        else
            msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente", "");
    }

    #endregion
}