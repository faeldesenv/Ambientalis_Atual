using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;
using Utilitarios.Criptografia;

public partial class Adm_Relatorios_MasterPage_MasterPageRelatorios : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["idConfig"] == null)
            Response.Redirect("../Adm/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

        if (Session["UsuarioAdministradorLogado_SistemaAmbiental"] == null)
            Response.Redirect("../Adm/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        else
        {
            lblUsuario.Text = "Administrador";            
        }
    }

    #region ______________ Eventos _________________

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

    public string BindingCaminhoTela(Object tela)
    {
        return "";
    }

    #endregion

    protected void ibtnResetarPreferencias_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.ResetarPreferencias();
        }
        catch (Exception ex)
        {
            Alert.Show(ex.Message);
        }
    }

    private void ResetarPreferencias()
    {
        Usuario usuario = Usuario.ConsultarPorId(WebUtil.UsuarioLogado.Id);
        PreferenciaRelatorio pref = PreferenciaRelatorio.Consultar(this.Page.AppRelativeVirtualPath.Replace("~", ".."), usuario);
        if (pref != null)
        {
            pref.Excluir();
            transacao.Recarregar(ref msg);
        }
        WebUtil.RedirectToPage(this.Page);
    }
}
