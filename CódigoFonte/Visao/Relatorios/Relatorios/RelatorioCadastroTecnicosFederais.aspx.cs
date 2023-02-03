using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioCadastroTecnicosFederais : PageBase
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

    //Cadastros tecnicos que o usuário tem acesso
    public IList<CadastroTecnicoFederal> CadastrosTecnicosPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["CadastrosTecnicosPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<CadastroTecnicoFederal>)Session["CadastrosTecnicosPermissaoModuloMeioAmbiente"];
        }
        set { Session["CadastrosTecnicosPermissaoModuloMeioAmbiente"] = value; }
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

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
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
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = CadastroTecnicoFederal.ObterCadastrosTecnicosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
    }

    private void CarregarCampos()
    {
        this.CarregarGruposEconomicos(ddlGrupoEconomicoCTF);
        this.CarregarEmpresas(ddlGrupoEconomicoCTF, ddlEmpresaCTF);
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

    private void CarregarRelatorioCadastroTecnicoFederal()
    {       
        
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDeEntregaRelatorioAnual = tbxDataEntregaRelatorioAnualDe.Text != string.Empty ? Convert.ToDateTime(tbxDataEntregaRelatorioAnualDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteEntregaRelatorioAnual = tbxDataEntregaRelatorioAnualAte.Text != string.Empty ? Convert.ToDateTime(tbxDataEntregaRelatorioAnualAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        DateTime dataDeVencimentoTaxaTrimestral = tbxDataVencimentoTaxaTrimestralDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoTaxaTrimestralDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteVencimentoTaxaTrimestral = tbxDataVencimentoTaxaTrimestralAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoTaxaTrimestralAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        DateTime dataDeVencimentoCertificadoRegularidade = tbxDataVencimentoCertificadoRegularidadeDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoCertificadoRegularidadeDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteVencimentoCertificadoRegularidade = tbxDataVencimentoCertificadoRegularidadeAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoCertificadoRegularidadeAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
       
        IList<CadastroTecnicoFederal> cadastros = CadastroTecnicoFederal.FiltrarRelatorio(ddlGrupoEconomicoCTF.SelectedValue.ToInt32(), ddlEmpresaCTF.SelectedValue.ToInt32(),
            dataDeEntregaRelatorioAnual, dataAteEntregaRelatorioAnual, dataDeVencimentoTaxaTrimestral, dataAteVencimentoTaxaTrimestral, dataDeVencimentoCertificadoRegularidade, dataAteVencimentoCertificadoRegularidade, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.CadastrosTecnicosPermissaoModuloMeioAmbiente);
                
        string descricaoGrupoEconomico = ddlGrupoEconomicoCTF.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoCTF.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaCTF.SelectedIndex == 0 ? "Todas" : ddlEmpresaCTF.SelectedItem.Text;
        string descricaoPeriodoEntregaRelatorio = WebUtil.GetDescricaoDataRelatorio(dataDeEntregaRelatorioAnual, dataAteEntregaRelatorioAnual);
        string descricaoPeriodoTaxaTrimestral = WebUtil.GetDescricaoDataRelatorio(dataDeVencimentoTaxaTrimestral, dataAteVencimentoTaxaTrimestral);
        string descricaoPeriodoCertificadoRegularidade = WebUtil.GetDescricaoDataRelatorio(dataDeVencimentoCertificadoRegularidade, dataAteVencimentoCertificadoRegularidade);        

        grvRelatorio.DataSource = cadastros != null && cadastros.Count > 0 ? cadastros : new List<CadastroTecnicoFederal>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);

        CtrlHeader.InsertFiltroCentro("Data de Entrega do Relatório Anual", descricaoPeriodoEntregaRelatorio);
        CtrlHeader.InsertFiltroCentro("Data de Vencimento da Taxa Trimestral", descricaoPeriodoTaxaTrimestral);

        CtrlHeader.InsertFiltroDireita("Data de Vencimento do Certificado de Regularidade", descricaoPeriodoCertificadoRegularidade);        

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Cadastros Técnicos Federais - CTF");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);        
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoEconomicoCTF_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoCTF, ddlEmpresaCTF);
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    { 
        try
        {
            this.CarregarRelatorioCadastroTecnicoFederal();
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