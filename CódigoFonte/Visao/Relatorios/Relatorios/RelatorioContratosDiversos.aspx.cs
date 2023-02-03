using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioContratosDiversos : PageBase
{
    private Msg msg = new Msg();

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloContratos
    {
        get
        {
            if (Session["ConfiguracaoModuloContratos"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloContratos"];
        }
        set { Session["ConfiguracaoModuloContratos"] = value; }
    }

    public IList<Empresa> EmpresasPermissaoModuloContratos
    {
        get
        {
            if (Session["EmpresasPermissaoModuloContratos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoModuloContratos"];
        }
        set { Session["EmpresasPermissaoModuloContratos"] = value; }
    }

    public IList<Setor> SetoresPermissaoModuloContratos
    {
        get
        {
            if (Session["SetoresPermissaoModuloContratos"] == null)
                return null;
            else
                return (IList<Setor>)Session["SetoresPermissaoModuloContratos"];
        }
        set { Session["SetoresPermissaoModuloContratos"] = value; }
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
        this.CarregarGruposEconomicos(ddlGrupoContratosDiversos);
        this.CarregarFornecedores(ddlFornecedorContratosDiversos);
        this.CarregarClientes(ddlClienteContratosDiversos);
        this.CarregarStatusContratosDiversos(ddlStatusContratosDiversos);
        this.CarregarCentrosCusto(ddlCentroCusto);
        this.CarregarIndicesFinanceiros(ddlIndiceContratosDiversos);
        this.CarregarSetores(ddlSetorContratosDiversos);
        this.CarregarFormasDePagamento(ddlFormaPagamentoContratoDiverso);
        this.CarregarEmpresas(ddlGrupoContratosDiversos, ddlEmpresaContratosDiversos);
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloContratos = null;
        this.SetoresPermissaoModuloContratos = null;
    }

    private void CarregarSessoesPermissoes()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloContratos == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count > 0 && !this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado))
                Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");
        }

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.EmpresasPermissaoModuloContratos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);            
        }
        else
            this.EmpresasPermissaoModuloContratos = null;

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            this.SetoresPermissaoModuloContratos = Setor.ObterSetoresQueOUsuarioPossuiAcesso(this.UsuarioLogado);            
        }
        else
            this.SetoresPermissaoModuloContratos = null;
    }

    public string BindTituloFornecedorCliente(object o)
    {
        ContratoDiverso c = (ContratoDiverso)o;
        return c != null ? c.Como : "";
    }

    public string BindFornecedorCliente(object o)
    {
        ContratoDiverso c = (ContratoDiverso)o;
        return c.GetFornecedorCliente;
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

    private void CarregarFornecedores(DropDownList dropFornecedor)
    {
        dropFornecedor.DataValueField = "Id";
        dropFornecedor.DataTextField = "Nome";
        dropFornecedor.DataSource = Fornecedor.ConsultarTodosOrdemAlfabetica() != null ? Fornecedor.ConsultarTodosOrdemAlfabetica() : new List<Fornecedor>();
        dropFornecedor.DataBind();
        dropFornecedor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarClientes(DropDownList dropCliente)
    {
        dropCliente.DataValueField = "Id";
        dropCliente.DataTextField = "Nome";
        dropCliente.DataSource = Cliente.ConsultarTodosOrdemAlfabetica() != null ? Cliente.ConsultarTodosOrdemAlfabetica() : new List<Cliente>();
        dropCliente.DataBind();
        dropCliente.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarStatusContratosDiversos(DropDownList dropStatusContratosDiversos)
    {
        dropStatusContratosDiversos.Items.Clear();
        IList<StatusFixosContrato> statusfixos = StatusFixosContrato.ConsultarTodos();
        if (statusfixos != null && statusfixos.Count > 0)
        {
            foreach (StatusFixosContrato fixo in statusfixos)
            {
                dropStatusContratosDiversos.Items.Add(new ListItem(fixo.Nome, fixo.Id.ToString()));
            }
        }

        IList<StatusEditaveisContrato> statusEditaveis = StatusEditaveisContrato.ConsultarTodos();
        if (statusEditaveis != null && statusEditaveis.Count > 0)
        {
            foreach (StatusEditaveisContrato editavel in statusEditaveis)
            {
                dropStatusContratosDiversos.Items.Add(new ListItem(editavel.Nome, editavel.Id.ToString()));
            }
        }
        dropStatusContratosDiversos.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarCentrosCusto(DropDownList dropCentroCusto)
    {
        dropCentroCusto.DataValueField = "Id";
        dropCentroCusto.DataTextField = "Nome";
        dropCentroCusto.DataSource = CentroCusto.ConsultarTodos() != null ? CentroCusto.ConsultarTodos() : new List<CentroCusto>();
        dropCentroCusto.DataBind();
        dropCentroCusto.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarIndicesFinanceiros(DropDownList dropIndiceFinanceiro)
    {
        dropIndiceFinanceiro.DataValueField = "Id";
        dropIndiceFinanceiro.DataTextField = "Nome";
        dropIndiceFinanceiro.DataSource = IndiceFinanceiro.ConsultarTodos() != null ? IndiceFinanceiro.ConsultarTodos() : new List<IndiceFinanceiro>();
        dropIndiceFinanceiro.DataBind();
        dropIndiceFinanceiro.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarSetores(DropDownList dropSetor)
    {
        dropSetor.DataValueField = "Id";
        dropSetor.DataTextField = "Nome";

        IList<Setor> setores;

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
            setores = this.SetoresPermissaoModuloContratos != null ? this.SetoresPermissaoModuloContratos : new List<Setor>();
        else
            setores = Setor.ConsultarTodos();

        dropSetor.DataSource = setores != null ? setores : new List<Setor>();
        dropSetor.DataBind();
        dropSetor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarFormasDePagamento(DropDownList dropFormaPagamento)
    {
        dropFormaPagamento.DataValueField = "Id";
        dropFormaPagamento.DataTextField = "Nome";
        dropFormaPagamento.DataSource = FormaRecebimento.ConsultarTodosOrdemAlfabetica();
        dropFormaPagamento.DataBind();
        dropFormaPagamento.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarEmpresas(DropDownList ddlGrupoEconomico, DropDownList ddlEmpresa)
    {
        ddlEmpresa.Items.Clear();
        ddlEmpresa.Items.Add(new ListItem("-- Todas --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloContratos != null ? this.EmpresasPermissaoModuloContratos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloContratos != null ? this.EmpresasPermissaoModuloContratos : new List<Empresa>();
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

    private void CarregarRelatorioContratosDiversos()
    {        

        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataVencimentoDe = tbxDataVencimentoContratoDiversoDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoContratoDiversoDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataVencimentoAte = tbxDataVencimentoContratoDiversoAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoContratoDiversoAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        DateTime dataReajusteDe = tbxDataReajusteDe.Text != string.Empty ? Convert.ToDateTime(tbxDataReajusteDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataReajusteAte = tbxDataReajusteAte.Text != string.Empty ? Convert.ToDateTime(tbxDataReajusteAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;                

        IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);

        IList<ContratoDiverso> contratos = ContratoDiverso.FiltrarRelatorio(ddlGrupoContratosDiversos.SelectedValue.ToInt32(), ddlEmpresaContratosDiversos.SelectedValue.ToInt32(), ddlComoContratosDiversos.SelectedValue.ToInt32(), 
            ddlFornecedorContratosDiversos.SelectedValue.ToInt32(), ddlClienteContratosDiversos.SelectedValue.ToInt32(), ddlStatusContratosDiversos.SelectedValue.ToInt32(), dataVencimentoDe, dataVencimentoAte, 
            dataReajusteDe, dataReajusteAte, ddlCentroCusto.SelectedValue.ToInt32(), ddlIndiceContratosDiversos.SelectedValue.ToInt32(), ddlSetorContratosDiversos.SelectedValue.ToInt32(),
            ddlFormaPagamentoContratoDiverso.SelectedValue.ToInt32(), this.ConfiguracaoModuloContratos.Tipo, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos);
        
        string descricaoGrupoEconomico = ddlGrupoContratosDiversos.SelectedIndex == 0 ? "Todos" : ddlGrupoContratosDiversos.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaContratosDiversos.SelectedIndex > 0 ? ddlEmpresaContratosDiversos.SelectedItem.Text : "Todas";
        string filtroTituloComo = ddlComoContratosDiversos.SelectedIndex == 0 ? "Fornecedor/Cliente" : ddlComoContratosDiversos.SelectedIndex == 1 ? "Fornecedor(Contratada):" : "Cliente(Contrante):";
        string filtroComo = ddlComoContratosDiversos.SelectedIndex == 0 ? "Não definido" : ddlComoContratosDiversos.SelectedItem.Text;
        string filtroFornecedorCliente = ddlComoContratosDiversos.SelectedIndex > 0 ? ddlComoContratosDiversos.SelectedValue == "1" ? ddlFornecedorContratosDiversos.SelectedValue.ToInt32() > 0 ? Fornecedor.ConsultarPorId(ddlFornecedorContratosDiversos.SelectedValue.ToInt32()) != null ? Fornecedor.ConsultarPorId(ddlFornecedorContratosDiversos.SelectedValue.ToInt32()).Nome : "Não definido" : "Todos" : ddlClienteContratosDiversos.SelectedValue.ToInt32() > 0 ? Cliente.ConsultarPorId(ddlClienteContratosDiversos.SelectedValue.ToInt32()) != null ? Cliente.ConsultarPorId(ddlClienteContratosDiversos.SelectedValue.ToInt32()).Nome : "Não definido" : "Todos" : "Não definido";  //verifica se o filtro de pesquisa é um cliente ou um fornecedor e preenche a string com o nome do cliente ou fornecedor selecionado de acordo com o filtro escolhido
        string descricaoStatus = ddlStatusContratosDiversos.SelectedIndex > 0 ? ddlStatusContratosDiversos.SelectedItem.Text : "Todos";
        string descricaoDataVencimento = WebUtil.GetDescricaoDataRelatorio(dataVencimentoDe, dataVencimentoAte);
        string descricaoDataReajuste = WebUtil.GetDescricaoDataRelatorio(dataReajusteDe, dataReajusteAte);
        string descricaoCentroCusto = ddlCentroCusto.SelectedIndex == 0 ? "Todos" : ddlCentroCusto.SelectedItem.Text;
        string descricaoIndiceFinanceiro = ddlIndiceContratosDiversos.SelectedIndex == 0 ? "Todos" : ddlIndiceContratosDiversos.SelectedItem.Text;
        string descricaoSetor = ddlSetorContratosDiversos.SelectedIndex == 0 ? "Todos" : ddlSetorContratosDiversos.SelectedItem.Text;
        
        string descricaoFormaPagamento = ddlFormaPagamentoContratoDiverso.SelectedIndex == 0 ? "Todas" : ddlFormaPagamentoContratoDiverso.SelectedItem.Text;
        string colunaFornecedorCliente = ddlComoContratosDiversos.SelectedIndex == 0 ? "Fornecedor/Cliente" : ddlComoContratosDiversos.SelectedIndex == 1 ? "Fornecedor(Contratada)" : "Cliente(Contrante)";

        grvRelatorio.DataSource = contratos != null && contratos.Count > 0 ? contratos : new List<ContratoDiverso>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);
        CtrlHeader.InsertFiltroEsquerda("Como", filtroComo);
        CtrlHeader.InsertFiltroEsquerda(filtroTituloComo, filtroFornecedorCliente);

        CtrlHeader.InsertFiltroCentro("Status", descricaoStatus);
        CtrlHeader.InsertFiltroCentro("Forma de Pagamento", descricaoFormaPagamento);
        CtrlHeader.InsertFiltroCentro("Vencimento", descricaoDataVencimento);
        CtrlHeader.InsertFiltroCentro("Reajuste", descricaoDataReajuste);

        CtrlHeader.InsertFiltroDireita("Centro de Custo", descricaoCentroCusto);
        CtrlHeader.InsertFiltroDireita("Índice Financeiro", descricaoIndiceFinanceiro);
        CtrlHeader.InsertFiltroDireita("Setor", descricaoSetor);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Contratos");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);    
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoContratosDiversos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresas(ddlGrupoContratosDiversos, ddlEmpresaContratosDiversos);
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

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioContratosDiversos();
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