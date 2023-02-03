using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using Utilitarios;
using Modelo;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected override void OnLoad(EventArgs e)
    {
        if (Session["idConfig"] == null)
            Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);

        if (Session["idConfig"].ToString().ToInt32() == 0)
        {
            if (Session["idEmp"] == null || Session["idEmp"].ToString().ToInt32() <= 0)
                Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        }

        if (Session["UsuarioLogado_SistemaAmbiental"] == null)
            Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
        else
        {
            Usuario user = (Usuario)Session["UsuarioLogado_SistemaAmbiental"];
            if (user.GrupoEconomico != null)
                user.GrupoEconomico = user.GrupoEconomico.ConsultarPorId();

            if (!Permissoes.UsuarioPossuiAcessoUrl(user, this.Request.Url.LocalPath))
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
            else
            {
                lblUsuario.Text = user.Nome;
                if (!IsPostBack)
                {
                    this.CriarECarregarMenus(user);
                }
                base.OnLoad(e);
            }

        }
    }

    #region ______________ TRANSAÇÕES _________________

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["idConfig"] == null) 
            {
                if (Session["UsuarioLogado_SistemaAmbiental"] != null)
                {
                    Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri + "&temusuario");
                }
                else 
                {
                    Response.Redirect("../Acesso/Login.aspx?page=" + this.Request.Url.AbsoluteUri);
                }
            }
                

            if (Session["UsuarioLogado_SistemaAmbiental"] == null)
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

    private void CriarECarregarMenus(Usuario user)
    {
        if (Session["menu_string"] == null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<ul id='menu'>");

            //IList<ModuloPermissao> modulosUsuarioLogado = ModuloPermissao.RecarregarModulos(user.ConsultarPorId().ModulosPermissao);
            //IList<Modelo.Menu> menusParaCarregar = Modelo.Menu.ConsultarModulosUsuarioOrdemPrioridade(user, modulosUsuarioLogado);

            IList<Modelo.Menu> menusParaCarregar = Modelo.Menu.ConsultarTodosQueNaoForemRelatoriosPorOrdemPrioridade();

            for (int i = 0; i < menusParaCarregar.Count; i++)
            {
                //Menu de relatórios
                if (i == menusParaCarregar.Count - 1)
                {
                    builder.Append(Modelo.Menu.GetHtmlMenuRelatorio(user));
                }

                if (!menusParaCarregar[i].Relatorio && menusParaCarregar[i].Id != Modelo.Menu.IDMENUINDEX)
                {
                    if ((menusParaCarregar[i].Nome == "ANM") || (menusParaCarregar[i].Nome == "Meio Ambiente") || (menusParaCarregar[i].Nome == "Contratos") || (menusParaCarregar[i].Nome == "Diversos") || (menusParaCarregar[i].Nome == "Permissões"))
                    {
                        if (menusParaCarregar[i].Nome == "ANM" && Permissoes.UsuarioPossuiAcessoModuloDNPM(user, ModuloPermissao.ConsultarPorNome("DNPM")))
                            builder.Append(menusParaCarregar[i].GetHtmlMenu(user));

                        else if (menusParaCarregar[i].Nome == "Meio Ambiente" && Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(user, ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
                            builder.Append(menusParaCarregar[i].GetHtmlMenu(user));

                        else if (menusParaCarregar[i].Nome == "Contratos" && Permissoes.UsuarioPossuiAcessoModuloContratos(user, ModuloPermissao.ConsultarPorNome("Contratos")))
                            builder.Append(menusParaCarregar[i].GetHtmlMenu(user));

                        else if (menusParaCarregar[i].Nome == "Diversos" && Permissoes.UsuarioPossuiAcessoModuloDiversos(user, ModuloPermissao.ConsultarPorNome("Diversos")))
                            builder.Append(menusParaCarregar[i].GetHtmlMenu(user));

                        else if (menusParaCarregar[i].Nome == "Permissões" && user.UsuarioAdministrador)
                            builder.Append(menusParaCarregar[i].GetHtmlMenu(user));
                    }
                    else
                    {
                        if (Permissoes.UsuarioPossuiAcessoModuloGeral(user, ModuloPermissao.ConsultarPorNome("Geral")))
                            builder.Append(menusParaCarregar[i].GetHtmlMenu(user));
                    }

                }

            }

            builder.Append("<div style='width:100%; height:4px; clear:both'></div>");
            builder.Append("</ul>");
            Session["menu_string"] = lblMenuDinamico.Text = builder.ToString();
        }
        else
        {
            lblMenuDinamico.Text = Session["menu_string"].ToString();
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
