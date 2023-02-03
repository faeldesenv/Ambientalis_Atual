using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioRenunciasAlvarasPesquisa : PageBase
{
    private Msg msg = new Msg();

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloDNPM
    {
        get
        {
            if (Session["ConfiguracaoModuloDNPM"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloDNPM"];
        }
        set { Session["ConfiguracaoModuloDNPM"] = value; }
    }

    //Empresas que o usuário tem acesso
    public IList<Empresa> EmpresasPermissaoModuloDNPM
    {
        get
        {
            if (Session["EmpresasPermissaoModuloDNPM"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoModuloDNPM"];
        }
        set { Session["EmpresasPermissaoModuloDNPM"] = value; }
    }

    //Processos que o usuário tem acesso
    public IList<ProcessoDNPM> ProcessosPermissaoModuloDNPM
    {
        get
        {
            if (Session["ProcessosPermissaoModuloDNPM"] == null)
                return null;
            else
                return (IList<ProcessoDNPM>)Session["ProcessosPermissaoModuloDNPM"];
        }
        set { Session["ProcessosPermissaoModuloDNPM"] = value; }
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
        this.CarregarGruposEconomicos(ddlGrupoRenuncias);
        this.CarregarEmpresas(ddlGrupoRenuncias, ddlEmpresaRenuncias);
        this.CarregarEstados(ddlEstadoRenuncias);
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloDNPM = null;
        this.ProcessosPermissaoModuloDNPM = null;
    }

    private void CarregarSessoesPermissoes()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloDNPM == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count > 0 && !this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado))
                Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");
        }

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDNPM = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDNPM = null;

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            this.ProcessosPermissaoModuloDNPM = ProcessoDNPM.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.ProcessosPermissaoModuloDNPM = null;
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
        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM : new List<Empresa>();
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

    private void CarregarRelatorioRenunciasAlvaras()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDeVencimentoRenuncia = tbxDataVencimentoRenunciaDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoRenunciaDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteVencimentoRenuncia = tbxDataVencimentoRenunciaAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoRenunciaAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<AlvaraPesquisa> alvaras = AlvaraPesquisa.FiltrarRelatorio(ddlGrupoRenuncias.SelectedValue.ToInt32(), ddlEmpresaRenuncias.SelectedValue.ToInt32(), dataDeVencimentoRenuncia, dataAteVencimentoRenuncia, 
            ddlEstadoRenuncias.SelectedValue.ToInt32(), this.ConfiguracaoModuloDNPM.Tipo, this.EmpresasPermissaoModuloDNPM, this.ProcessosPermissaoModuloDNPM);

        grvRelatorio.DataSource = alvaras != null && alvaras.Count > 0 ? alvaras : new List<AlvaraPesquisa>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoGrupoEconomico = ddlGrupoRenuncias.SelectedIndex == 0 ? "Todos" : ddlGrupoRenuncias.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaRenuncias.SelectedIndex == 0 ? "Todas" : ddlEmpresaRenuncias.SelectedItem.Text;
        string descricaoDataVencimento = WebUtil.GetDescricaoDataRelatorio(dataDeVencimentoRenuncia, dataAteVencimentoRenuncia);
        string descricaoEstado = ddlEstadoRenuncias.SelectedIndex == 0 ? "Todos" : ddlEstadoRenuncias.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);

        CtrlHeader.InsertFiltroCentro("Período", descricaoDataVencimento);

        CtrlHeader.InsertFiltroDireita("Estado", descricaoEstado);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Renúncias de Álvaras de Pesquisa");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoRenuncias_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoRenuncias, ddlEmpresaRenuncias);        
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioRenunciasAlvaras();
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