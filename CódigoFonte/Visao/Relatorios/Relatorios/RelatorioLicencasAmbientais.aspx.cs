using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioLicencasAmbientais : PageBase
{
    private Msg msg = new Msg();

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloMeioAmbiente
    {
        get
        {
            if (Session["ConfiguracaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloMeioAmbiente"];
        }
        set { Session["ConfiguracaoModuloMeioAmbiente"] = value; }
    }

    //Empresas que o usuário tem acesso
    public IList<Empresa> EmpresasPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["EmpresasPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoModuloMeioAmbiente"];
        }
        set { Session["EmpresasPermissaoModuloMeioAmbiente"] = value; }
    }

    //processo que o usuário tem acesso
    public IList<Processo> ProcessosPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["ProcessosPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<Processo>)Session["ProcessosPermissaoModuloMeioAmbiente"];
        }
        set { Session["ProcessosPermissaoModuloMeioAmbiente"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.LimparSessoesPermissoes();
                this.CarregarSessoesPermissoes();

                this.CarregarCampos();
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunas(grvRelatorio, ckbColunas, this.Page);
                
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Alert.Show(msg.Mensagem);
            }
    }

    #region ______________Métodos______________

    private void CarregarCampos()
    {
        this.CarregarGruposEconomicos(ddlGrupoEconomicoLicencaAmbiental);
        this.CarregarEmpresas(ddlGrupoEconomicoLicencaAmbiental, ddlEmpresaLicencaAmbiental);
        this.CarregarTiposLicenca(ddlTipoLicencaAmbiental);
        this.CarregarOrgaosAmbientais(ddlOrgaoAmbientalLicencaAmbiental);
        this.CarregarEstados(ddlEstadoLicencaAmbiental);        
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoModuloMeioAmbiente = null;
    }

    private void CarregarSessoesPermissoes()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloMeioAmbiente == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0 && !this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado))
                Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");
        }

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloMeioAmbiente = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloMeioAmbiente = null;

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            this.ProcessosPermissaoModuloMeioAmbiente = Processo.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.ProcessosPermissaoModuloMeioAmbiente = null;
    }

    private void CarregarGruposEconomicos(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        GrupoEconomico grupoAux = new GrupoEconomico();
        grupoAux.Nome = "-- Todos --";
        grupos.Insert(0, grupoAux);

        drop.DataSource = grupos;
        drop.DataBind();

        drop.SelectedIndex = 0;
    }

    private void CarregarEmpresas(DropDownList ddlGrupoEconomico, DropDownList ddlEmpresa)
    {
        ddlEmpresa.Items.Clear();
        ddlEmpresa.Items.Add(new ListItem("-- Todas --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente : new List<Empresa>();
        }
        else
        {
            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            empresas = grupo != null && grupo.Empresas != null ? grupo.Empresas : new List<Empresa>();
        }

        if (empresas != null && empresas.Count > 0)
        {
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void CarregarTiposLicenca(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<TipoLicenca> tiposLicencas = TipoLicenca.ConsultarTodosOrdemAlfabetica();
        TipoLicenca auxTipoLicenca = new TipoLicenca();
        auxTipoLicenca.Nome = "-- Todos --";
        tiposLicencas.Insert(0, auxTipoLicenca);

        drop.DataSource = tiposLicencas;
        drop.DataBind();
        drop.SelectedIndex = 0;
    }

    private void CarregarOrgaosAmbientais(DropDownList drop)
    {
        IList<OrgaoAmbiental> orgaosAmbientaisProcesso = OrgaoAmbiental.ConsultarTodosOrdemAlfabetica();
        drop.Items.Add(new ListItem("-- Todos --", "0"));
        foreach (OrgaoAmbiental auxOrgaoProcesso in orgaosAmbientaisProcesso)
            drop.Items.Add(new ListItem(auxOrgaoProcesso.GetNomeTipo + " - " + auxOrgaoProcesso.Nome, auxOrgaoProcesso.Id.ToString()));
        drop.SelectedIndex = 0;

    }

    private void CarregarEstados(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<Estado> estados = Estado.ConsultarTodosOrdemAlfabetica();
        Estado estadoAux = new Estado();
        estadoAux.Nome = "-- Todos --";
        estados.Insert(0, estadoAux);

        drop.DataSource = estados;
        drop.DataBind();

        drop.SelectedIndex = 0;
    }

    private void CarregarRelatorioLicencasAmbientais()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDeValidade = tbxDataValidadeDeLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataValidadeDeLicencaAmbiental.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehValidade = tbxDataValidadeAtehLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataValidadeAtehLicencaAmbiental.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        DateTime dataDePrazoLimite = tbxDataLimiteLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataLimiteLicencaAmbiental.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehPrazoLimite = tbxDataLimiteAtehLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataLimiteAtehLicencaAmbiental.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Licenca> licencas = Licenca.FiltrarRelatorio(ddlGrupoEconomicoLicencaAmbiental.SelectedValue.ToInt32(), ddlEmpresaLicencaAmbiental.SelectedValue.ToInt32(), ddlTipoLicencaAmbiental.SelectedValue.ToInt32(),
            dataDeValidade, dataAtehValidade, dataDePrazoLimite, dataAtehPrazoLimite, ddlOrgaoAmbientalLicencaAmbiental.SelectedValue, ddlEstadoLicencaAmbiental.SelectedValue.ToInt32(), 
            this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente);

        if (ddlOrgaoAmbientalLicencaAmbiental.SelectedValue.ToInt32() > 0)
            this.RemoverLicencasDeOutrosOrgaosAmbientais(licencas, ddlOrgaoAmbientalLicencaAmbiental.SelectedValue.ToInt32());

        grvRelatorio.DataSource = licencas != null && licencas.Count > 0 ? licencas : new List<Licenca>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoGrupoEconomico = ddlGrupoEconomicoLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoLicencaAmbiental.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaLicencaAmbiental.SelectedIndex == 0 ? "Todas" : ddlEmpresaLicencaAmbiental.SelectedItem.Text;
        string descricaoEstado = ddlEstadoLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlEstadoLicencaAmbiental.SelectedItem.Text;
        string descricaoTipoLicenca = ddlTipoLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlTipoLicencaAmbiental.SelectedItem.Text;
        string descricaoDataValidade = WebUtil.GetDescricaoDataRelatorio(dataDeValidade, dataAtehValidade);
        string descricaoDataPrazoLimite = WebUtil.GetDescricaoDataRelatorio(dataDePrazoLimite, dataAtehPrazoLimite);
        string descricaoOrgaoAmbiental = ddlOrgaoAmbientalLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlOrgaoAmbientalLicencaAmbiental.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);
        CtrlHeader.InsertFiltroEsquerda("Órgão Ambiental", descricaoOrgaoAmbiental);

        CtrlHeader.InsertFiltroCentro("Tipo de Licença", descricaoTipoLicenca);
        CtrlHeader.InsertFiltroCentro("Data de Validade", descricaoDataValidade);


        CtrlHeader.InsertFiltroDireita("Limite de Renovação", descricaoDataPrazoLimite);
        CtrlHeader.InsertFiltroDireita("Estado", descricaoEstado);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Licenças Ambientais");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);

    }

    private void RemoverLicencasDeOutrosOrgaosAmbientais(IList<Licenca> licencas, int idOrgaoAmbiental)
    {
        if (licencas != null && licencas.Count > 0)
        {
            for (int i = licencas.Count - 1; i > -1; i--)
            {
                if (licencas[i] == null || licencas[i].GetObjetoOrgaoAmbiental == null || licencas[i].GetObjetoOrgaoAmbiental.Id != idOrgaoAmbiental)
                {
                    licencas.Remove(licencas[i]);
                }
            }
        }               
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoEconomicoLicencaAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoLicencaAmbiental, ddlEmpresaLicencaAmbiental);
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioLicencasAmbientais();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Alert.Show(msg.Mensagem);
        }
    }

    #endregion
    
}