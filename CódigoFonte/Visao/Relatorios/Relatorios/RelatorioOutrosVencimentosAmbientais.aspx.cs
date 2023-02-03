using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioOutrosVencimentosAmbientais : PageBase
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

    //outros de empresa que o usuário tem acesso
    public IList<OutrosEmpresa> OutrosEmpresaPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["OutrosEmpresaPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<OutrosEmpresa>)Session["OutrosEmpresaPermissaoModuloMeioAmbiente"];
        }
        set { Session["OutrosEmpresaPermissaoModuloMeioAmbiente"] = value; }
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
        this.CarregarGruposEconomicos(ddlGrupoEconomicoOutrosVencimentos);
        this.CarregarEmpresas(ddlGrupoEconomicoOutrosVencimentos, ddlEmpresaOutrosVencimentos);
        ddlTipoOutrosVencimentos.Items.Add(new ListItem("-- Todos --", "0"));
        ddlTipoOutrosVencimentos.Items.Add(new ListItem("Sem Processos", Condicional.VencimentoGeral.ToString()));
        ddlTipoOutrosVencimentos.Items.Add(new ListItem("Dentro de Processos", Condicional.VencimentoProcesso.ToString()));
        ddlTipoOutrosVencimentos.SelectedIndex = 0;
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoModuloMeioAmbiente = null;
        this.OutrosEmpresaPermissaoModuloMeioAmbiente = null;
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
        {
            this.ProcessosPermissaoModuloMeioAmbiente = Processo.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
            this.OutrosEmpresaPermissaoModuloMeioAmbiente = OutrosEmpresa.ObterOutrosEmpresaQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        }
        else 
        {
            this.ProcessosPermissaoModuloMeioAmbiente = null;
            this.OutrosEmpresaPermissaoModuloMeioAmbiente = null;
        }
            
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

    private void CarregarRelatorioOutrosVencimentosAmbientais()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDeVencimento = tbxDataVencimentoDeOutrosVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoDeOutrosVencimentos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehVencimento = tbxDataVencimentoAtehOutrosVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoAtehOutrosVencimentos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Condicional> outrosVencimentos = new List<Condicional>();
        outrosVencimentos.AddRange<Condicional>(OutrosEmpresa.FiltrarRelatorio(ddlGrupoEconomicoOutrosVencimentos.SelectedValue.ToInt32(), ddlEmpresaOutrosVencimentos.SelectedValue.ToInt32(),
            dataDeVencimento, dataAtehVencimento, ddlTipoOutrosVencimentos.SelectedValue.ToInt32(), ddlOutrosPeriodicos.SelectedValue.ToInt32(), 
            this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresaPermissaoModuloMeioAmbiente));

        outrosVencimentos.AddRange<Condicional>(OutrosProcesso.FiltrarRelatorio(ddlGrupoEconomicoOutrosVencimentos.SelectedValue.ToInt32(),
            ddlEmpresaOutrosVencimentos.SelectedValue.ToInt32(),
            dataDeVencimento, dataAtehVencimento,
            ddlTipoOutrosVencimentos.SelectedValue.ToInt32(), ddlOutrosPeriodicos.SelectedValue.ToInt32(), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente));

        if (ddlOutrosProrrogacaoPrazo.SelectedValue.ToInt32() > 0)
            this.RemoverOutrosVencimentosComOuSemProrrogacoesPrazo(outrosVencimentos, ddlOutrosProrrogacaoPrazo.SelectedValue.ToInt32());

        grvRelatorio.DataSource = outrosVencimentos != null && outrosVencimentos.Count > 0 ? outrosVencimentos : new List<Condicional>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;
        
        string descricaoGrupoEconomico = ddlGrupoEconomicoOutrosVencimentos.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoOutrosVencimentos.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaOutrosVencimentos.SelectedIndex == 0 ? "Todas" : ddlEmpresaOutrosVencimentos.SelectedItem.Text;
        string descricaoDataVencimento = WebUtil.GetDescricaoDataRelatorio(dataDeVencimento, dataAtehVencimento);
        string descricaoTipo = ddlTipoOutrosVencimentos.SelectedIndex == 0 ? "Todos" : ddlTipoOutrosVencimentos.SelectedItem.Text;
        
        string descricaoPeriodico = ddlOutrosPeriodicos.SelectedIndex == 0 ? "Todos" : ddlOutrosPeriodicos.SelectedItem.Text;
        string descricaoProrrogacao = ddlOutrosProrrogacaoPrazo.SelectedIndex == 0 ? "Todos" : ddlOutrosProrrogacaoPrazo.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);

        CtrlHeader.InsertFiltroCentro("Tipo", descricaoTipo);
        CtrlHeader.InsertFiltroCentro("Data de Vencimento", descricaoDataVencimento);

        CtrlHeader.InsertFiltroDireita("Prorrogação de Prazo", descricaoProrrogacao);
        CtrlHeader.InsertFiltroDireita("Períodicos", descricaoPeriodico);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Outros Vencimentos Ambientais");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    private void RemoverOutrosVencimentosComOuSemProrrogacoesPrazo(IList<Condicional> outrosVencimentos, int prorrogacoes)
    {
        if (prorrogacoes == 1)
        {
            if (outrosVencimentos != null && outrosVencimentos.Count > 0)
            {
                for (int i = outrosVencimentos.Count - 1; i > -1; i--)
                {
                    if (outrosVencimentos[i].GetUltimoVencimento == null || outrosVencimentos[i].GetUltimoVencimento.ProrrogacoesPrazo == null || outrosVencimentos[i].GetUltimoVencimento.ProrrogacoesPrazo.Count == 0)
                    {
                        outrosVencimentos.Remove(outrosVencimentos[i]);
                    }
                }
            }
        }
        else
        {
            if (outrosVencimentos != null && outrosVencimentos.Count > 0)
            {
                for (int i = outrosVencimentos.Count - 1; i > -1; i--)
                {
                    if (outrosVencimentos[i].GetUltimoVencimento != null && outrosVencimentos[i].GetUltimoVencimento.ProrrogacoesPrazo != null && outrosVencimentos[i].GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
                    {
                        outrosVencimentos.Remove(outrosVencimentos[i]);
                    }
                }
            }
        }
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoEconomicoOutrosVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoOutrosVencimentos, ddlEmpresaOutrosVencimentos);
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioOutrosVencimentosAmbientais();
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