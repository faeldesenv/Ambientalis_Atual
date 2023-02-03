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

public partial class Relatorios_MasterPage_MasterPageRelatorios : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();
    protected void Page_Load(object sender, EventArgs e)
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

            lblUsuario.Text = user.Nome;

            ModuloPermissao modulo = this.ObterModuloPermissaoPeloNomeDaTela();

            if (modulo != null && modulo.Id > 0) 
            {
                IList<Modelo.Menu> telas = modulo.GetMenusRelatoriosDoModulo;
                rptRelatorios.DataSource = telas != null ? telas : new List<Modelo.Menu>();
                rptRelatorios.DataBind();
            } 
        }
    }

    

    #region ______________ Eventos _________________

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["UsuarioLogado_SistemaAmbiental"] == null)
                Response.Redirect("../../Account/Login.aspx");

            if (Session["idConfig"] == null)
                Response.Redirect("../../Account/Login.aspx");
                       
            transacao.Abrir();
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
    
    #endregion

    #region ______________ Métodos _________________

    public string BindingCaminhoTela(Object tela)
    {
        Modelo.Menu menu = (Modelo.Menu)tela;
        return menu.UrlPesquisa.Replace("Relatorios/", "");
    }

    private ModuloPermissao ObterModuloPermissaoPeloNomeDaTela()
    {
        string tituloTela = this.Page.AppRelativeVirtualPath.Replace("~", "..").Replace("../Relatorios/", "");

        if (tituloTela.Contains("Empresas") || tituloTela.Contains("PorPeriodo"))
        {
            return ModuloPermissao.ConsultarPorNome("Geral");
        }

        if (tituloTela.Contains("DNPM") || tituloTela.Contains("GuiasDeUtilizacao") || tituloTela.Contains("RelatorioRals") || tituloTela.Contains("RenunciasAlvarasPesquisa"))
        {
            return ModuloPermissao.ConsultarPorNome("DNPM");
        }

        if (tituloTela.Contains("OrgaosAmbientais") || tituloTela.Contains("ProcessosMeioAmbiente") || tituloTela.Contains("LicencasAmbientais") || tituloTela.Contains("OutrosVencimentos") || tituloTela.Contains("CadastroTecnicos") || tituloTela.Contains("Condicionantes"))
        {
            return ModuloPermissao.ConsultarPorNome("Meio Ambiente");
        }

        if (tituloTela.Contains("RelatorioClientes") || tituloTela.Contains("RelatorioFornecedores") || tituloTela.Contains("ContratosDiversos") || tituloTela.Contains("ContratosPorProcessos") || tituloTela.Contains("ProcessosPorContratos"))
        {
            return ModuloPermissao.ConsultarPorNome("Contratos");
        }

        if (tituloTela.Contains("RelatorioVencimentosDiversos"))
        {
            return ModuloPermissao.ConsultarPorNome("Diversos");
        }

        return null;

    }

    private void ResetarPreferencias()
    {
        Usuario usuario = Usuario.ConsultarPorId(WebUtil.UsuarioLogado.Id);
        PreferenciaRelatorio pref = PreferenciaRelatorio.Consultar(this.Page.AppRelativeVirtualPath.Replace("~", "..").Replace("../Relatorios/", ""), usuario);
        if (pref != null)
        {
            pref.Excluir();
            transacao.Recarregar(ref msg);
        }
        WebUtil.RedirectToPage(this.Page);
    }

    #endregion
    
}
