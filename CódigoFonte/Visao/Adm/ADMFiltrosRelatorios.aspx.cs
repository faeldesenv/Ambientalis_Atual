using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Utilitarios.Criptografia;
using System.Collections;
using Modelo;
using System.Configuration;

public partial class ADMRelatorios_FiltrosRelatorios : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();
                this.CarregarCampos();
                this.CarregarGruposEconomicos(ddlGrupoEconomicoRelUtilizacao);
            }

        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }

    }

    #region _____________________ Eventos _________________________

    protected void trvRelatorios_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            btnExibirRelatorio.Visible = true;
            mtvFiltros.ActiveViewIndex = trvRelatorios.SelectedNode.Value.ToInt32();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            //Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.CarregarRelatorioSelecionado();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.CarregarAdministradores(ddlAdministradorGruposEconomicos);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlSistemaRelUtilizacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistemaRelUtilizacao.SelectedValue;
            transacao.Abrir();
            this.CarregarGruposEconomicos(ddlGrupoEconomicoRelUtilizacao);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlGrupoAdministradorAcessos_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlSistemaAcessos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistemaAcessos.SelectedValue;
            transacao.Abrir();
            this.CarregarGruposEconomicosAdmAcessos(ddlGrupoAdministradorAcessos);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlAdministradorAcessos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            ddlGrupoAdministradorAcessos.Enabled = ddlAdministradorAcessos.SelectedIndex == 0;
            this.CarregarUsuariosAcessos();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlGrupoAdministradorAcessos_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (ddlSistemaAcessos.SelectedValue.ToInt32() > 0) 
            {
                if (ddlAdministradorAcessos.SelectedValue == "0")
                {
                    ddlGrupoAdministradorAcessos.Enabled = true;
                    ddlAdministradorAcessos.Enabled = false;
                }
                if (ddlGrupoAdministradorAcessos.SelectedValue == "0")
                {
                    ddlAdministradorAcessos.Enabled = true;
                }
            }
            
            this.CarregarUsuariosAcessos();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

    #region _____________________ Métodos _________________________

    private void CarregarCampos()
    {
        this.CarregarArvoreRelatorios();
        this.CarregarCamposFiltros();        
    }

    private void CarregarGruposEconomicos(DropDownList dropGrupoEconomico)
    {
        dropGrupoEconomico.DataTextField = "Nome";
        dropGrupoEconomico.DataValueField = "Id";

        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        GrupoEconomico aux = new GrupoEconomico();
        aux.Nome = "-- Todos --";
        grupos.Insert(0, aux);

        dropGrupoEconomico.DataSource = grupos;
        dropGrupoEconomico.DataBind();
        dropGrupoEconomico.SelectedIndex = 0;
    }

    private void CarregarCamposFiltros()
    {
        //Grupos Economicos
        this.CarregarAdministradores(ddlAdministradorGruposEconomicos);

        this.CarregarGruposEconomicos(ddlGrupoAdministradorAcessos);
        this.CarregarUsuariosAcessos();
    }

    private void CarregarAdministradores(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<Administrador> admins = Administrador.ConsultarTodosOrdemAlfabetica();
        Administrador aux = new Administrador();
        aux.Nome = "-- Selecione --";
        admins.Insert(0, aux);

        drop.DataSource = admins;
        drop.DataBind();
        drop.SelectedIndex = 0;
    }

    private void CarregarArvoreRelatorios()
    {
        Modelo.Menu relatorio = new Modelo.Menu();
        relatorio.Nome = "Relatório de Grupos Econômicos";
        relatorio.Relatorio = true;
        relatorio.UrlCadastro = "1";

        Modelo.Menu relatorioUtilizacao = new Modelo.Menu();
        relatorioUtilizacao.Nome = "Relatório de Utilização por Grupos Econômicos";
        relatorioUtilizacao.Relatorio = true;
        relatorioUtilizacao.UrlCadastro = "2";

        Modelo.Menu relatorioAcessos = new Modelo.Menu();
        relatorioAcessos.Nome = "Relatório de Acessos do Sistema";
        relatorioAcessos.Relatorio = true;
        relatorioAcessos.UrlCadastro = "3";

        Modelo.Menu relatorioPendenciasGrupos = new Modelo.Menu();
        relatorioPendenciasGrupos.Nome = "Relatório de Pendências de Ativação de Grupos Econômicos";
        relatorioPendenciasGrupos.Relatorio = true;
        relatorioPendenciasGrupos.UrlCadastro = "4";

        Modelo.Menu relatorioPermissoesPorusuarios = new Modelo.Menu();
        relatorioPermissoesPorusuarios.Nome = "Relatório de Permissões por Usuários";
        relatorioPermissoesPorusuarios.Relatorio = true;
        relatorioPermissoesPorusuarios.UrlCadastro = "5";

        trvRelatorios.Nodes.Clear();
        TreeNode noPai = new TreeNode("Relatórios", "0");
        noPai.ChildNodes.Add(new TreeNode(relatorio.Nome, relatorio.UrlCadastro, "", "Relatorios/RelatorioGruposEconomicos.aspx", "_blank"));
        noPai.ChildNodes.Add(new TreeNode(relatorioUtilizacao.Nome, relatorioUtilizacao.UrlCadastro, "", "Relatorios/RelatorioUtilizacaoPorGrupoEconomico.aspx", "_blank"));
        noPai.ChildNodes.Add(new TreeNode(relatorioAcessos.Nome, relatorioAcessos.UrlCadastro, "", "Relatorios/RelatorioAcessos.aspx", "_blank"));
        noPai.ChildNodes.Add(new TreeNode(relatorioPendenciasGrupos.Nome, relatorioPendenciasGrupos.UrlCadastro, "", "Relatorios/RelatorioPendenciasAtivacaoGruposEconomicos.aspx", "_blank"));
        noPai.ChildNodes.Add(new TreeNode(relatorioPermissoesPorusuarios.Nome, relatorioPermissoesPorusuarios.UrlCadastro, "", "Relatorios/RelatorioPermissoesDeUsuarios.aspx", "_blank"));
        trvRelatorios.Nodes.Add(noPai);
        trvRelatorios.ExpandAll();
    }

    private void CarregarRelatorioSelecionado()
    {
        switch (mtvFiltros.ActiveViewIndex)
        {
            case 1: this.CarregarRelatorioGruposEconomicos();
                break;

            case 2: this.CarregarRelatorioUtilizacaoPorGruposEconomicos();
                break;

            case 3: this.CarregarRelatorioAcessos();
                break;

            default:
                msg.CriarMensagem("Selecione algum relatório", "Informação", MsgIcons.Informacao);
                break;
        }
    }

    private string GetDescricaoData(DateTime dataDe, DateTime dataAteh)
    {
        string retorno = "Não definido";
        if (dataDe.CompareTo(DateTime.MinValue) > 0 || dataAteh.CompareTo(DateTime.MaxValue) < 0)
        {
            if (dataDe.CompareTo(DateTime.MinValue) > 0 && dataAteh.CompareTo(DateTime.MaxValue) < 0)
                retorno = "de " + dataDe.ToShortDateString() + " até " + dataAteh.ToShortDateString();
            else
                retorno = dataDe.CompareTo(DateTime.MinValue) > 0 ? "após " + dataDe.ToShortDateString() : "antes de " + dataAteh.ToShortDateString();
        }
        return retorno;
    }

    private void CarregarGruposEconomicosAdmAcessos(DropDownList ddlGrupoAdministradorAcessos)
    {
        if (ddlSistemaAcessos.SelectedValue.ToInt32() > 0)
        {
            administrador_acessos.Visible = true;
            this.CarregarAdministradores(ddlAdministradorAcessos);
        }
        else
        {
            administrador_acessos.Visible = false;
        }

        this.CarregarGruposEconomicos(ddlGrupoAdministradorAcessos);
        this.CarregarUsuariosAcessos();
    }

    private void CarregarUsuariosAcessos()
    {
        ddlUsuarioAcessos.DataTextField = "Nome";
        ddlUsuarioAcessos.DataValueField = "Id";

        IList<Usuario> usuarios = new List<Usuario>();

        Administrador admim = Administrador.ConsultarPorId(ddlAdministradorAcessos.SelectedValue.ToInt32());
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoAdministradorAcessos.SelectedValue.ToInt32());

        if (ddlSistemaAcessos.SelectedValue.ToInt32() > 0 && admim != null && admim.Id > 0)
            usuarios = admim.GetUsuarios != null && admim.GetUsuarios.Count > 0 ? admim.GetUsuarios : new List<Usuario>();
        else if (grupo != null && grupo.Id > 0)
            usuarios = grupo.Usuarios != null && grupo.Usuarios.Count > 0 ? grupo.Usuarios : new List<Usuario>();
        else
            usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

        if (usuarios != null && usuarios.Count > 0)
            usuarios = this.RecarregarUsuarios(usuarios);

        Usuario aux = new Usuario();
        aux.Nome = "-- Todos --";
        usuarios.Insert(0, aux);

        ddlUsuarioAcessos.DataSource = usuarios;
        ddlUsuarioAcessos.DataBind();
        ddlUsuarioAcessos.SelectedIndex = 0;
    }

    private IList<Usuario> RecarregarUsuarios(IList<Usuario> usuarios)
    {
        IList<Usuario> lista = new List<Usuario>();
        if (usuarios != null)
        {
            foreach (Usuario item in usuarios)
            {
                Usuario not = Usuario.ConsultarPorId(item.Id);
                if (lista.Contains(not))
                {
                    lista.Remove(not);
                    lista.Add(not);
                }
                else
                {
                    lista.Add(not);
                }

            }
        }
        return lista;
    }

    #region _____________________ Carregamenbto dos relatórios _________________________

    /// <summary>
    /// Grupos Econômicos
    /// </summary>
    private void CarregarRelatorioGruposEconomicos()
    {
        DateTime dataDe = tbxDataCadastroRelatorioGruposEconomicos.Text != string.Empty ? Convert.ToDateTime(tbxDataCadastroRelatorioGruposEconomicos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataCadastroAtehRelatorioGruposEconomicos.Text != string.Empty ? Convert.ToDateTime(tbxDataCadastroAtehRelatorioGruposEconomicos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        DateTime dataCancelamentoDe = tbxDataCancelamentoDe.Text != string.Empty ? Convert.ToDateTime(tbxDataCancelamentoDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataCancelamentoAte = tbxDataCancelamentoAte.Text != string.Empty ? Convert.ToDateTime(tbxDataCancelamentoAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        IList<GrupoEconomico> grupos = GrupoEconomico.FiltrarGruposEconomicos(ddlAdministradorGruposEconomicos.SelectedValue.ToInt32(),
            dataDe.ToMinHourOfDay(), dataAteh.ToMaxHourOfDay(), dataCancelamentoDe.ToMinHourOfDay(), dataCancelamentoAte.ToMaxHourOfDay(),
            ddlPossuiUsuarios.SelectedValue.ToInt32(),
            ddlAtivo.SelectedValue.ToInt32(),
            ddlCancelado.SelectedValue.ToInt32());

        Fontes.relatorioGruposEconomicosDataTable fonte = new Fontes.relatorioGruposEconomicosDataTable();
        string descricaoUsuarioLogado = Session["UsuarioAdministradorLogado_SistemaAmbiental"] != null ? ((Administrador)Session["UsuarioAdministradorLogado_SistemaAmbiental"]).Nome : "Não definido";
        string descricaoAdministrador = ddlAdministradorGruposEconomicos.SelectedIndex != 0 ? ddlAdministradorGruposEconomicos.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = dataDe != SqlDate.MinValue || dataAteh != SqlDate.MaxValue ? this.GetDescricaoData(dataDe, dataAteh) : "Não definida";
        string descricaoPossuiUsuariosCadastrados = ddlPossuiUsuarios.SelectedItem.Text.Trim();
        string descricaoAtivo = ddlAtivo.SelectedItem.Text;
        string descricaoCancelado = ddlCancelado.SelectedItem.Text;
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoDataDeCancelamento = dataCancelamentoDe != SqlDate.MinValue || dataCancelamentoAte != SqlDate.MaxValue ? this.GetDescricaoData(dataCancelamentoDe, dataCancelamentoAte) : "Não definida";

        if (grupos.Count > 0)
        {
            IList<Usuario> usuarios = Usuario.ConsultarTodos();
            foreach (GrupoEconomico aux in grupos)
                fonte.Rows.Add(descricaoUsuarioLogado,
                    descricaoAdministrador,
                    descricaoDataDeReferência,
                    descricaoAtivo,
                    descricaoCancelado,
                    aux.Administrador != null ? aux.Administrador.Nome : "Não definido",
                    aux.Nome,
                    aux.DataCadastro.ToString("dd/MM/yyyy"),
                    grupos.Count, aux.Empresas.Count, aux.GetQuantidadeUsuariosDoGrupo(aux, usuarios), descricaoPossuiUsuariosCadastrados, URLLOgo, aux.DataCancelamento != SqlDate.MinValue ? aux.DataCancelamento.ToString("dd/MM/yyyy") : "", descricaoDataDeCancelamento, aux.Contato != null ? aux.Contato.Email : "");
        }
        else
        {
            fonte.Rows.Add(descricaoUsuarioLogado,
                    descricaoAdministrador,
                    descricaoDataDeReferência,
                    descricaoAtivo,
                    descricaoCancelado,
                    null,
                    null,
                    null,
                    0, null, null, descricaoPossuiUsuariosCadastrados, URLLOgo, null, descricaoDataDeCancelamento, null);
        }
        Relatorios.CarregarRelatorioAdministrativo("Grupos Econômicos", "GruposEconomicos", false, fonte);
    }

    private void CarregarRelatorioUtilizacaoPorGruposEconomicos()
    {
        Fontes.relatorioUtilizacaoPorGrupoEconomicoDataTable fonte = new Fontes.relatorioUtilizacaoPorGrupoEconomicoDataTable();

        string descricaoUsuarioLogado = Session["UsuarioAdministradorLogado_SistemaAmbiental"] != null ? ((Administrador)Session["UsuarioAdministradorLogado_SistemaAmbiental"]).Nome : "Não definido";
        string descricaoGrupoEconomico = ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32() > 0 ? ddlGrupoEconomicoRelUtilizacao.SelectedItem.Text : "Todos";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoSistema = ddlSistemaRelUtilizacao.SelectedValue.ToInt32() > 0 ? "Ambientalis" : "Sustentar";

        IList<GrupoEconomico> grupos = new List<GrupoEconomico>();
        if (ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32() > 0)
            grupos.Add(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32()));
        else
            grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();

        if (grupos != null && grupos.Count > 0)
        {
            foreach (GrupoEconomico grupo in grupos)
            {
                fonte.Rows.Add(descricaoUsuarioLogado, descricaoGrupoEconomico, grupo.Nome, grupo.Empresas != null ? grupo.Empresas.Count : 0, grupo.GetTotalProcessosAmbientaisDoGrupo, grupo.GetTotalProcessosMinerariosDoGrupo, grupo.GetTotalContratosDiversosDoGrupo, this.ObterTotalGeralProcAmbientais(grupos), this.ObterTotalGeralProcMinerarios(grupos), this.ObterTotalGeralContratos(grupos), grupos.Count, URLLOgo, this.ObterTotalEmpresas(grupos), descricaoSistema);
            }
        }
        else
        {
            fonte.Rows.Add(descricaoUsuarioLogado, descricaoGrupoEconomico, null, null, null, null, null, null, null, null, 0, URLLOgo, null, descricaoSistema);
        }

        Relatorios.CarregarRelatorioAdministrativo("Utilização por Grupos Econômicos", "UtilizacaoPorGrupoEconomico", false, fonte);
    }

    private int ObterTotalGeralProcAmbientais(IList<GrupoEconomico> grupos)
    {
        int contadorProcessos = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorProcessos = contadorProcessos + grupo.GetTotalProcessosAmbientaisDoGrupo;
        }

        return contadorProcessos;
    }

    private int ObterTotalGeralProcMinerarios(IList<GrupoEconomico> grupos)
    {
        int contadorProcessos = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorProcessos = contadorProcessos + grupo.GetTotalProcessosMinerariosDoGrupo;
        }

        return contadorProcessos;
    }

    private int ObterTotalGeralContratos(IList<GrupoEconomico> grupos)
    {
        int contadorContratos = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorContratos = contadorContratos + grupo.GetTotalContratosDiversosDoGrupo;
        }

        return contadorContratos;
    }

    private int ObterTotalEmpresas(IList<GrupoEconomico> grupos)
    {
        int contadorEmpresas = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            if (grupo.Empresas != null)
                contadorEmpresas = contadorEmpresas + grupo.Empresas.Count;
        }

        return contadorEmpresas;
    }

    private void CarregarRelatorioAcessos()
    {
        if (tbxDataAcessoDe.Text == string.Empty || tbxDataAcessoAte.Text == string.Empty) 
        {
            msg.CriarMensagem("Selecione um período para poder exibir o relatório", "Alerta", MsgIcons.Alerta);
            return;
        }

        DateTime dataDe = tbxDataAcessoDe.Text != string.Empty ? Convert.ToDateTime(tbxDataAcessoDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataAcessoAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAcessoAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        Fontes.relatorioAcessosDataTable fonte = new Fontes.relatorioAcessosDataTable();

        IList<Acesso> acessos = Acesso.FiltrarRelatorio(ddlAdministradorAcessos.SelectedValue.ToInt32(), ddlGrupoAdministradorAcessos.SelectedValue.ToInt32(),
            ddlUsuarioAcessos.SelectedValue.ToInt32(), dataDe.ToMinHourOfDay(), dataAteh.ToMaxHourOfDay());

        string descricaoUsuarioLogado = Session["UsuarioAdministradorLogado_SistemaAmbiental"] != null ? ((Administrador)Session["UsuarioAdministradorLogado_SistemaAmbiental"]).Nome : "Não definido";        
        string descricaoPeriodo = dataDe != SqlDate.MinValue || dataAteh != SqlDate.MaxValue ? this.GetDescricaoData(dataDe, dataAteh) : "Não definido";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoSistema = ddlSistemaAcessos.SelectedValue.ToInt32() > 0 ? "Ambientalis" : "Sustentar";
        string descricaoGrupoAdministrador = ddlGrupoAdministradorAcessos.SelectedValue.ToInt32() > 0 ? "Grupo Econômico:" : ddlAdministradorAcessos.SelectedValue.ToInt32() > 0 ? "Administrador:" : "Grupo/Administrador:";
        string grupoAdministradorEscolhido = ddlGrupoAdministradorAcessos.SelectedValue.ToInt32() > 0 ? ddlGrupoAdministradorAcessos.SelectedItem.Text : ddlAdministradorAcessos.SelectedValue.ToInt32() > 0 ? ddlAdministradorAcessos.SelectedItem.Text : "Não definido";
        string descricaoUsuario = ddlUsuarioAcessos.SelectedValue.ToInt32() > 0 ? ddlUsuarioAcessos.SelectedItem.Text : "Todos";

        if (acessos != null && acessos.Count > 0)
        {
            foreach (Acesso acesso in acessos)
            {
                fonte.Rows.Add(descricaoUsuarioLogado, descricaoSistema, descricaoGrupoAdministrador, grupoAdministradorEscolhido, descricaoUsuario, 
                    descricaoPeriodo, acesso.Data.ToString("dd/MM/yyyy HH:mm"), acesso.GetGrupoAdministrador, acesso.Ip, acesso.Usuario != null ? acesso.Usuario.Nome : "Não definido", 
                    acessos.Count, URLLOgo);
            }
        }
        else 
        {
            fonte.Rows.Add(descricaoUsuarioLogado, descricaoSistema, descricaoGrupoAdministrador, grupoAdministradorEscolhido, descricaoUsuario,
                    descricaoPeriodo, null, null, null, null, 0, URLLOgo);
        }

        Relatorios.CarregarRelatorioAdministrativo("Acessos do Sistema", "Acessos", true, fonte);

    }

    #endregion

    #endregion    
}