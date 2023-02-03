using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Configuration;

public partial class Acesso_LoginADM : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();          
        }
    }

    #region ________ Eventos ___________

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            this.Acessar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            if (msg.Mensagem.IsNotNullOrEmpty())
                Alert.Show(msg.Mensagem);
        }
    }

    #endregion

    #region ________ Métodos ___________

    private void Acessar()
    {
        String LoginUsuario = "comercial";
        String SenhaUsuario = "sustentar";

        if (tbxUsuario.Text == LoginUsuario && tbxSenha.Text == SenhaUsuario)
        {
            Session["UsuarioComercialLogado_SistemaAmbiental"] = "usuarioComercial";
            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
                Response.Redirect(pagina);
            else
                Response.Redirect("Index.aspx", false);
                
        }
        else { 
            msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente", "");
        }
            
    }

    #endregion
}