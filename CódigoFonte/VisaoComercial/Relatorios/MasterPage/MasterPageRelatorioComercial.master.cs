using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_MasterPage_MasterPageRelatorioComercial : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["idConfig"] == null)
            Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

        if (Session["UsuarioLogado_SistemaComercial"] == null)
            Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        else
        {
            lblUsuario.Text = ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login;            
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
            Alert.Show("ERRO ao tentar se comunicar com o servidor. ERRO:" + ex.Message);       
            
            throw;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        transacao.Fechar(ref msg);
        Alert.Show(msg.Mensagem);        
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        transacao.Fechar(ref msg);
        Alert.Show(msg.Mensagem);        
    }

    #endregion

    #region ______________ Métodos _________________
   

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

    public string BindingCaminhoTela(Object tela)
    {
        Modelo.Menu menu = (Modelo.Menu)tela;
        return menu.UrlPesquisa.Replace("Relatorios/", "");
    }

    

    #endregion
    protected void ibtnResetarPreferencias_Click(object sender, ImageClickEventArgs e)
    {

    }
}
