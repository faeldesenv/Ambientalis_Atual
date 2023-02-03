using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioCondicionantes : PageBase
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
                this.CarregarCampos();
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunas(grvRelatorio, ckbColunas, this.Page);

                this.LimparSessoesPermissoes();
                this.CarregarSessoesPermissoes();
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
        this.CarregarGruposEconomicos(ddlGrupoEconomicoCondicionantes);
        this.CarregarEmpresas(ddlGrupoEconomicoCondicionantes, ddlEmpresaCondicionantes);
        this.CarregarOrgaosAmbientais(ddlOrgaosAmbientaisCondicionantes);
        this.CarregarEstados(ddlEstadoCondicionante);
        this.CarregarStatus();
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
        grupoAux.Nome = "-- Selecione --";
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

    private void CarregarStatus()
    {
        ddlStatusCondicionante.DataValueField = "Id";
        ddlStatusCondicionante.DataTextField = "Nome";
        IList<Status> status = Status.ConsultarTodosOrdemAlfabetica();
        Status statusAux = new Status();
        statusAux.Nome = "-- Todos --";
        status.Insert(0, statusAux);
        ddlStatusCondicionante.DataSource = status;
        ddlStatusCondicionante.DataBind();
    }

    private void CarregarRelatorioCondicionantes()
    {        

        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDeVencimento = tbxDataVencimentoDeCondicionantes.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoDeCondicionantes.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehVencimento = tbxDataVencimentoAtehCondicionantes.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoAtehCondicionantes.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
               
        IList<Condicionante> condicionantes = Condicionante.FiltrarRelatorio(ddlGrupoEconomicoCondicionantes.SelectedValue.ToInt32(), ddlEmpresaCondicionantes.SelectedValue.ToInt32(),
            dataDeVencimento, dataAtehVencimento, ddlOrgaosAmbientaisCondicionantes.SelectedValue.ToInt32(), ddlEstadoCondicionante.SelectedValue.ToInt32(), ddlStatusCondicionante.SelectedValue.ToInt32(),
            ddlCondicionantePeriodica.SelectedValue.ToInt32(), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente);

        
        string descricaoGrupoEconomico = ddlGrupoEconomicoCondicionantes.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoCondicionantes.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaCondicionantes.SelectedIndex == 0 ? "Todas" : ddlEmpresaCondicionantes.SelectedItem.Text;
        string descricaoEstado = ddlEstadoCondicionante.SelectedIndex == 0 ? "Todos" : ddlEstadoCondicionante.SelectedItem.Text;
        string descricaoDataVencimento = WebUtil.GetDescricaoDataRelatorio(dataDeVencimento, dataAtehVencimento);
        string descricaoOrgaoAmbiental = ddlOrgaosAmbientaisCondicionantes.SelectedIndex == 0 ? "Todos" : ddlOrgaosAmbientaisCondicionantes.SelectedItem.Text;
        string descricaoStatus = ddlStatusCondicionante.SelectedIndex == 0 ? "Todos" : ddlStatusCondicionante.SelectedItem.Text;
        string descricaoPeriodica = ddlCondicionantePeriodica.SelectedIndex == 0 ? "Todas" : ddlCondicionantePeriodica.SelectedItem.Text;
        string descricaoPrrogacaoPrazo = ddlCondicionanteProrrogacaoPrazo.SelectedIndex == 0 ? "Todas" : ddlCondicionanteProrrogacaoPrazo.SelectedItem.Text;
        

        condicionantes = condicionantes.OrderBy(i => i.Numero).ToList();

        if (ddlStatusCondicionante.SelectedIndex > 0)
            this.RemoverCondicionantesComStatusDiferentes(condicionantes, ddlStatusCondicionante.SelectedValue.ToInt32());

        if (ddlCondicionanteProrrogacaoPrazo.SelectedIndex > 0)
            this.RemoverCondicionantesComOusSemProrrogacoesPrazo(condicionantes, ddlCondicionanteProrrogacaoPrazo.SelectedValue.ToInt32());
               
        grvRelatorio.DataSource = condicionantes != null && condicionantes.Count > 0 ? condicionantes : new List<Condicionante>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);
        CtrlHeader.InsertFiltroEsquerda("Órgão Ambiental", descricaoOrgaoAmbiental);

        CtrlHeader.InsertFiltroCentro("Data de Vencimento", descricaoDataVencimento);
        CtrlHeader.InsertFiltroCentro("Status", descricaoStatus);
        CtrlHeader.InsertFiltroCentro("Períodica", descricaoPeriodica);


        CtrlHeader.InsertFiltroDireita("Estado", descricaoEstado);
        CtrlHeader.InsertFiltroDireita("Prorrogação de prazo", descricaoPrrogacaoPrazo);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Condicionantes");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    private void RemoverCondicionantesComOusSemProrrogacoesPrazo(IList<Condicionante> condicionantes, int condicionantePeriodica)
    {
        if (condicionantePeriodica == 1)
        {
            if (condicionantes != null && condicionantes.Count > 0)
            {
                for (int i = condicionantes.Count - 1; i > -1; i--)
                {
                    if (condicionantes[i].GetUltimoVencimento == null || condicionantes[i].GetUltimoVencimento.ProrrogacoesPrazo == null || condicionantes[i].GetUltimoVencimento.ProrrogacoesPrazo.Count == 0)
                    {
                        condicionantes.Remove(condicionantes[i]);
                    }
                }
            }
        }
        else
        {
            if (condicionantes != null && condicionantes.Count > 0)
            {
                for (int i = condicionantes.Count - 1; i > -1; i--)
                {
                    if (condicionantes[i].GetUltimoVencimento != null && condicionantes[i].GetUltimoVencimento.ProrrogacoesPrazo != null && condicionantes[i].GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
                    {
                        condicionantes.Remove(condicionantes[i]);
                    }
                }
            }
        }
    }

    private void RemoverCondicionantesComStatusDiferentes(IList<Condicionante> condicionantes, int idStatus)
    {
        if (condicionantes != null && condicionantes.Count > 0)
        {
            for (int i = condicionantes.Count - 1; i > -1; i--)
            {
                if (condicionantes[i].GetUltimoVencimento == null || condicionantes[i].GetUltimoVencimento.Status == null || condicionantes[i].GetUltimoVencimento.Status.Id != idStatus)
                    condicionantes.Remove(condicionantes[i]);
            }
        }
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoEconomicoCondicionantes_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoCondicionantes, ddlEmpresaCondicionantes);
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioCondicionantes();
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