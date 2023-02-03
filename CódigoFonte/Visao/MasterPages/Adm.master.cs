using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using Utilitarios;
using Modelo;

public partial class ADMSiteMaster : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected override void OnLoad(EventArgs e)
    {
        if (Session["idConfig"] == null)
            Response.Redirect("../Adm/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

        if (Session["UsuarioAdministradorLogado_SistemaAmbiental"] == null)
            Response.Redirect("../Adm/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        else
        {
            lblUsuario.Text = "Administrador";
         
            base.OnLoad(e);
        }
    }

    #region ______________ TRANSAÇÕES _________________

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["idConfig"] == null)
                Response.Redirect("../Adm/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

            if (Session["UsuarioAdministradorLogado_SistemaAmbiental"] == null)
                Response.Redirect("../Adm/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

            Page.Unload += new EventHandler(Page_Unload);
            Page.Error += new EventHandler(Page_Error);
        }
        catch (Exception ex)
        {
            MBOX1.Show("ERRO ao tentar se comunicar com o servidor. ERRO:" + ex.Message, "Falha");
            throw;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        transacao.Fechar(ref msg);
        MBOX1.Show(msg);
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        transacao.Fechar(ref msg);
        MBOX1.Show(msg);
    }

    #endregion

    #region ______________ Métodos _________________


    #endregion
}
