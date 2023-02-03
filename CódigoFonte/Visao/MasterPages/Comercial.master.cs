using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using Utilitarios;
using Modelo;

public partial class ComercialSiteMaster : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected override void OnLoad(EventArgs e)
    {
        if (Session["UsuarioComercialLogado_SistemaAmbiental"] == null)
            Response.Redirect("../Comercial/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        else
        {
            lblUsuario.Text = "Comercial";
         
            base.OnLoad(e);
        }
    }

    #region ______________ TRANSAÇÕES _________________

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["UsuarioComercialLogado_SistemaAmbiental"] == null)
                Response.Redirect("../Comercial/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            MBOX1.Show("ERRO ao tentar se comunicar com o servidor. ERRO:" + ex.Message, "Falha");
            throw;
        }
    }


    #endregion

    #region ______________ Métodos _________________


    #endregion
}
