using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioVencimentosDiversos : PageBase
{
    private Msg msg = new Msg();

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloDiversos
    {
        get
        {
            if (Session["ConfiguracaoModuloDiversos"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloDiversos"];
        }
        set { Session["ConfiguracaoModuloDiversos"] = value; }
    }

    public IList<Empresa> EmpresasPermissaoModuloDiversos
    {
        get
        {
            if (Session["EmpresaPermissaoModuloDiversos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresaPermissaoModuloDiversos"];
        }
        set { Session["EmpresaPermissaoModuloDiversos"] = value; }
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
        this.CarregarGruposEconomicos(ddlGrupoEconomicoVencimentoDiverso);
        this.CarregarEmpresas(ddlGrupoEconomicoVencimentoDiverso, ddlEmpresaVencimentoDiverso);
        this.CarregarTiposDiversos(ddlTipoVencimentoDiverso);
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloDiversos = null;        
    }

    private void CarregarSessoesPermissoes()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Diversos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloDiversos == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0 && !this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado))
                Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");
        }

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDiversos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDiversos = null;        
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
        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloDiversos != null ? this.EmpresasPermissaoModuloDiversos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloDiversos != null ? this.EmpresasPermissaoModuloDiversos : new List<Empresa>();
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

    private void CarregarTiposDiversos(DropDownList drop)
    {
        drop.DataValueField = "Id";
        drop.DataTextField = "Nome";
        drop.DataSource = TipoDiverso.ConsultarTodosOrdemAlfabetica();
        drop.DataBind();
        drop.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarEmpresaVencimentoDiverso(DropDownList ddlGrupoEconomico)
    {
        ddlEmpresaVencimentoDiverso.Items.Clear();
        ddlEmpresaVencimentoDiverso.Items.Add(new ListItem("-- Todas --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloDiversos != null ? this.EmpresasPermissaoModuloDiversos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloDiversos != null ? this.EmpresasPermissaoModuloDiversos : new List<Empresa>();
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
                    ddlEmpresaVencimentoDiverso.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresaVencimentoDiverso.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void CarregarStatusDiversos(DropDownList dropTipoDiverso, DropDownList dropStatusDiverso)
    {
        TipoDiverso tipo = TipoDiverso.ConsultarPorId(dropTipoDiverso.SelectedValue.ToInt32());
        dropStatusDiverso.DataValueField = "Id";
        dropStatusDiverso.DataTextField = "Nome";
        dropStatusDiverso.DataSource = tipo != null && tipo.StatusDiversos != null ? tipo.StatusDiversos : new List<StatusDiverso>();
        dropStatusDiverso.DataBind();
        dropStatusDiverso.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRelatorioVencimentosDiversos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDeVencimentoDiverso = tbxDataDeVencimentoDiverso.Text != string.Empty ? Convert.ToDateTime(tbxDataDeVencimentoDiverso.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteVencimentoDiverso = tbxDataAteVencimentoDiverso.Text != string.Empty ? Convert.ToDateTime(tbxDataAteVencimentoDiverso.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        StatusDiverso status = StatusDiverso.ConsultarPorId(ddlStatusVencimentoDiverso.SelectedValue.ToInt32());
        VencimentoDiverso vencimento = new VencimentoDiverso();

        IList<Diverso> diversos = Diverso.FiltrarRelatorio(ddlGrupoEconomicoVencimentoDiverso.SelectedValue.ToInt32(), ddlEmpresaVencimentoDiverso.SelectedValue.ToInt32(), tbxDescricaoVencimentoDiverso.Text,
            TipoDiverso.ConsultarPorId(ddlTipoVencimentoDiverso.SelectedValue.ToInt32()), status, dataDeVencimentoDiverso, dataAteVencimentoDiverso, ddlPeriodicosVencimentosDiverso.SelectedValue.ToInt32(),
            this.ConfiguracaoModuloDiversos.Tipo, this.EmpresasPermissaoModuloDiversos);

        if (ddlStatusVencimentoDiverso.SelectedValue.ToInt32() > 0 && diversos != null && diversos.Count > 0) 
            this.RemoverVencimentosDeOutrosStatus(diversos);

        if (diversos != null && diversos.Count > 0)
        {
            diversos = diversos.OrderByDescending(x => x.GetDataDateVencimento).ToList();
        }

        grvRelatorio.DataSource = diversos != null && diversos.Count > 0 ? diversos : new List<Diverso>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;
        
        string descricaoGrupoEconomico = ddlGrupoEconomicoVencimentoDiverso.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoVencimentoDiverso.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaVencimentoDiverso.SelectedIndex == 0 ? "Todas" : ddlEmpresaVencimentoDiverso.SelectedItem.Text;
        string filtroDescricao = tbxDescricaoVencimentoDiverso.Text.IsNotNullOrEmpty() ? tbxDescricaoVencimentoDiverso.Text : "Não definido";
        string descricaoDataVencimento = WebUtil.GetDescricaoDataRelatorio(dataDeVencimentoDiverso, dataAteVencimentoDiverso);
        string descricaoTipo = ddlTipoVencimentoDiverso.SelectedIndex == 0 ? "Todos" : ddlTipoVencimentoDiverso.SelectedItem.Text;
        string descricaoStatus = ddlTipoVencimentoDiverso.SelectedIndex != 0 && ddlStatusVencimentoDiverso.SelectedIndex == 0 || ddlStatusVencimentoDiverso.SelectedIndex == -1 ? "Todos" : ddlStatusVencimentoDiverso.SelectedItem.Text;        
        string descricaoPeriodico = ddlPeriodicosVencimentosDiverso.SelectedIndex == 0 ? "Todos" : ddlPeriodicosVencimentosDiverso.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);
        CtrlHeader.InsertFiltroEsquerda("Descrição", filtroDescricao);

        CtrlHeader.InsertFiltroCentro("Tipo de Vencimento", descricaoTipo);
        CtrlHeader.InsertFiltroCentro("Status do Vencimento", descricaoStatus);

        CtrlHeader.InsertFiltroDireita("Data de Vencimento", descricaoDataVencimento);
        CtrlHeader.InsertFiltroDireita("Períodicos", descricaoPeriodico);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Vencimentos Diversos");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    private void RemoverVencimentosDeOutrosStatus(IList<Diverso> diversos)
    {
        VencimentoDiverso vencimento = new VencimentoDiverso();

        for (int i = diversos.Count - 1; i > -1; i--) 
        {
            vencimento = diversos[i].GetUltimoVencimento;

            if (vencimento != null && vencimento.StatusDiverso.Id != ddlStatusVencimentoDiverso.SelectedValue.ToInt32())
            {
                diversos.Remove(diversos[i]);
            }
        }
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoEconomicoVencimentoDiverso_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresaVencimentoDiverso(ddlGrupoEconomicoVencimentoDiverso);
    }

    protected void ddlTipoVencimentoDiverso_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarStatusDiversos(ddlTipoVencimentoDiverso, ddlStatusVencimentoDiverso);
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioVencimentosDiversos();
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