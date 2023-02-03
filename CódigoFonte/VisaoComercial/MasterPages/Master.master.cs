using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Collections.Generic;
using System.Text;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected override void OnLoad(EventArgs e)
    {
        if (Session["idConfig"] == null)
            Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

        if (Session["UsuarioLogado_SistemaComercial"] == null)
            Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        else
        {
            lblUsuario.Text = ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login;
            base.OnLoad(e);
        }
    }

    #region ______________ TRANSAÇÕES _________________

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["idConfig"] == null)
                Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

            if (Session["UsuarioLogado_SistemaComercial"] == null)
                Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

            transacao.Abrir();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.VerificarPermissao();
        try
        {
            if (!IsPostBack)
            {
                this.CarregarMenu();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    private void VerificarPermissao()
    {
        IList<MenuComercial> menus = MenuComercial.GetMenus(((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]));
        foreach (MenuComercial menu in menus)
        {
            if (this.Request.Url.LocalPath.Contains(menu.Url))
                return;

            foreach (MenuComercial item in menu.SubMenus)
            {
                if (this.Request.Url.LocalPath.Contains(menu.Url))
                    return;
            }
        }
        Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
    }

    private void CarregarMenu()
    {
        lblMenu.Text = "";
        if (Session["UsuarioLogado_SistemaComercial"] == null)
            return;

        IList<MenuComercial> menus = MenuComercial.GetMenus(((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]));

        StringBuilder builder = new StringBuilder();
        builder.Append("<ul id='menu'>");

        foreach (MenuComercial menu in menus)
        {
            if (menu.Nome.ToUpper() == "INDEX")
                continue;

            if (menu.Nome == "Relatórios" || menu.Nome == "Orientações" || menu.Nome == "Avisos" || menu.Nome == "Demostração")
            {
                if (menu.Url.IsNotNullOrEmpty())
                {
                    if (menu.Nome.Equals("Demostração"))
                    {
                        builder.Append("<li class='GrupoEconômico'> <a target=\"_blank\" href='" + menu.Url + "'>");
                        builder.Append("<img alt='' src='" + menu.UrlIcone + "' />");
                        builder.Append("<div class='menu_texto'>" + menu.Nome + "</div>");
                        builder.Append("</a></li>");
                    }
                    else
                    {
                        builder.Append("<li class='GrupoEconômico'> <a href='" + menu.Url + "'>");
                        builder.Append("<img alt='' src='" + menu.UrlIcone + "' />");
                        builder.Append("<div class='menu_texto'>" + menu.Nome + "</div>");
                        builder.Append("</a></li>");
                    }
                }
            }
            else
            {

                builder.Append("<li class='GrupoEconômico'>");
                builder.Append("<img alt='' src='" + menu.UrlIcone + "' />");
                builder.Append("<div class='menu_texto'>" + menu.Nome + "</div>");

                if (menu.SubMenus != null && menu.SubMenus.Count > 0)
                {
                    builder.Append("<ul class='menu_ul'>");
                    foreach (MenuComercial item in menu.SubMenus)
                    {
                        if (item.Permissao == "TOD" || Session["UsuarioLogado_SistemaComercial"].GetType() == typeof(UsuarioAdministradorComercial) || Session["UsuarioLogado_SistemaComercial"].GetType() == typeof(UsuarioSupervisorComercial))
                            builder.Append("<li onclick=\"document.location='" + item.Url + "'\">" + item.Nome + "</li>");
                    }
                    builder.Append("</ul>");
                }

                builder.Append("</li>");
            }
        }
        builder.Append("</ul>");

        lblMenu.Text = builder.ToString();
    }

    #endregion
}
