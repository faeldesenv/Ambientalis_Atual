using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioClientes : PageBase
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
            
            if(this.EmpresasPermissaoModuloContratos == null || this.EmpresasPermissaoModuloContratos.Count == 0)
                Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");
        }        

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR) 
        {
            this.SetoresPermissaoModuloContratos = Setor.ObterSetoresQueOUsuarioPossuiAcesso(this.UsuarioLogado);

            if (this.EmpresasPermissaoModuloContratos == null || this.EmpresasPermissaoModuloContratos.Count == 0)
                Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");
        }
    }

    private void CarregarCampos()
    {
        this.CarregarEstados(ddlEstadoCliente);
        this.CarregarAtividades(ddlAtividadeCliente);
        this.CarregarCidades(ddlEstadoCliente, ddlCidadeCliente);
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

    private void CarregarAtividades(DropDownList dropAtividade)
    {
        dropAtividade.DataValueField = "Id";
        dropAtividade.DataTextField = "Nome";
        dropAtividade.DataSource = Atividade.ConsultarTodosOrdemAlfabetica();
        dropAtividade.DataBind();
        dropAtividade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarCidades(DropDownList dropEstado, DropDownList dropCidade)
    {
        Estado estado = Estado.ConsultarPorId(dropEstado.SelectedValue.ToInt32());
        dropCidade.DataValueField = "Id";
        dropCidade.DataTextField = "Nome";
        dropCidade.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        dropCidade.DataBind();
        dropCidade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarRelatorioClientes()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<Cliente> clientes = Cliente.FiltrarRelatorio(tbxNomeRazaoCliente.Text, tbxCnpjCpfCliente.Text, ddlStatusCliente.SelectedValue.ToInt32(), ddlEstadoCliente.SelectedValue.ToInt32(), ddlCidadeCliente.SelectedValue.ToInt32(), ddlAtividadeCliente.SelectedValue.ToInt32());

        string descricaoNomeRazao = tbxNomeRazaoCliente.Text.IsNotNullOrEmpty() ? tbxNomeRazaoCliente.Text : "Não definido";
        string descricaoCnpjCpf = tbxCnpjCpfCliente.Text.IsNotNullOrEmpty() ? tbxCnpjCpfCliente.Text : "Não definido";
        string descriStatus = ddlStatusCliente.SelectedValue == "0" ? "Todos" : ddlStatusCliente.SelectedItem.Text;
        string descricaoEstado = ddlEstadoCliente.SelectedIndex == 0 ? "Todos" : ddlEstadoCliente.SelectedItem.Text;
        string descricaoCidade = ddlCidadeCliente.SelectedIndex <= 0 ? "Todos" : ddlCidadeCliente.SelectedItem.Text;
        string descricaoAtividade = ddlAtividadeCliente.SelectedIndex == 0 ? "Todas" : ddlAtividadeCliente.SelectedItem.Text;

        grvRelatorio.DataSource = clientes != null && clientes.Count > 0 ? clientes : new List<Cliente>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Nome/Razão Social", descricaoNomeRazao);
        CtrlHeader.InsertFiltroEsquerda("CPF/CNPJ", descricaoCnpjCpf);

        CtrlHeader.InsertFiltroCentro("Atividade", descricaoAtividade);
        CtrlHeader.InsertFiltroCentro("Status", descriStatus);

        CtrlHeader.InsertFiltroDireita("Estado", descricaoEstado);
        CtrlHeader.InsertFiltroDireita("Cidade", descricaoCidade);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Clientes");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlEstadoCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstadoCliente, ddlCidadeCliente);
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
            this.CarregarRelatorioClientes();
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