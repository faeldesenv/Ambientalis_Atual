using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioVencimentosPorPeriodo : PageBase
{
    private Msg msg = new Msg();

    //Sessoes Modulo Diversos
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

    //Sessoes Modulo DNPM
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

    //Sessoes Modulo Meio Ambiente
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
    public IList<OutrosEmpresa> OutrosEmpresasPermissaoModuloMeioAmbiente
    {
        get
        {
            if (Session["OutrosEmpresasPermissaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<OutrosEmpresa>)Session["OutrosEmpresasPermissaoModuloMeioAmbiente"];
        }
        set { Session["OutrosEmpresasPermissaoModuloMeioAmbiente"] = value; }
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

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoModuloDiversos = null;        
        this.EmpresasPermissaoModuloDNPM = null;
        this.ProcessosPermissaoModuloDNPM = null;
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoModuloMeioAmbiente = null;
    }

    private void CarregarSessoesPermissoes()
    {
        this.CarregarSessoesModuloDiversos();
        this.CarregarSessoesModuloDNPM();
        this.CarregarSessoesModuloMeioAmbiente();
    }

    private void CarregarSessoesModuloDiversos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Diversos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);
        
        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDiversos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDiversos = null;
    }

    private void CarregarSessoesModuloDNPM()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDNPM = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDNPM = null;

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            this.ProcessosPermissaoModuloDNPM = ProcessoDNPM.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.ProcessosPermissaoModuloDNPM = null;
    }

    private void CarregarSessoesModuloMeioAmbiente()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloMeioAmbiente = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(modulo, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloMeioAmbiente = null;

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            this.ProcessosPermissaoModuloMeioAmbiente = Processo.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = CadastroTecnicoFederal.ObterCadastrosTecnicosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
            this.OutrosEmpresasPermissaoModuloMeioAmbiente = OutrosEmpresa.ObterOutrosEmpresaQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        }
        else 
        {
            this.ProcessosPermissaoModuloMeioAmbiente = null;
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
            this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;
        }
            
    }

    private void CarregarCampos()
    {
        //Vencimentos por Período
        this.CarregarGruposEconomicos(ddlGrupoEconomicoVencimentos);
        this.CarregarEmpresas(ddlGrupoEconomicoVencimentos, ddlEmpresaVencimentos);
        this.CarregarTiposVencimentos();
        this.CarregarStatusVencimentos();
        this.CarregarEstados(ddlEstadoVencimentoPeriodo);
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

        if (ddlTipoVencimentos.SelectedValue != "0")
        {
            //Modulo Meio Ambiente
            if (ddlTipoVencimentos.SelectedValue == Vencimento.OUTROSPROCESSO || ddlTipoVencimentos.SelectedValue == Vencimento.OUTROSEMPRESA || ddlTipoVencimentos.SelectedValue == Vencimento.CONDICIONANTE
                || ddlTipoVencimentos.SelectedValue == Vencimento.LICENCA || ddlTipoVencimentos.SelectedValue == Vencimento.ENTREGARELATORIOANUAL || ddlTipoVencimentos.SelectedValue == Vencimento.CERTIFICADOREGULARIDADE
                || ddlTipoVencimentos.SelectedValue == Vencimento.TAXATRIMESTRAL) 
            {
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
            }
            else if (ddlTipoVencimentos.SelectedValue == Vencimento.VENCIMENTODIVERSO) //Modulo Diversos
            {
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
            }
            else //Modulo DNPM
            {
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
            }
        }
        else 
        {
            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            empresas = grupo.Empresas != null ? grupo.Empresas : new List<Empresa>();
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

    public void CarregarTiposVencimentos()
    {
        ddlTipoVencimentos.Items.Add(new ListItem("-- Todos ", "0"));        

        this.CarregarTiposVencimentosPorPermissoesModulos();

        ddlTipoVencimentos.SelectedIndex = 0;
    }

    private void CarregarTiposVencimentosPorPermissoesModulos()
    {
        if (Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
        {
            ddlTipoVencimentos.Items.Add(new ListItem("Outros (Processo)", Vencimento.OUTROSPROCESSO));
            ddlTipoVencimentos.Items.Add(new ListItem("Outros (Empresa)", Vencimento.OUTROSEMPRESA));
            ddlTipoVencimentos.Items.Add(new ListItem("Condicionante", Vencimento.CONDICIONANTE));
            ddlTipoVencimentos.Items.Add(new ListItem("Licença", Vencimento.LICENCA));
            ddlTipoVencimentos.Items.Add(new ListItem("Entrega do Relatório Anual", Vencimento.ENTREGARELATORIOANUAL));
            ddlTipoVencimentos.Items.Add(new ListItem("Certificado de Regularidade", Vencimento.CERTIFICADOREGULARIDADE));
            ddlTipoVencimentos.Items.Add(new ListItem("Taxa trimestral", Vencimento.TAXATRIMESTRAL));
        }

        if (Permissoes.UsuarioPossuiAcessoModuloDNPM(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("DNPM")))
        {
            ddlTipoVencimentos.Items.Add(new ListItem("Exigência", Vencimento.EXIGENCIA));
            ddlTipoVencimentos.Items.Add(new ListItem("RAL", Vencimento.RAL));
            ddlTipoVencimentos.Items.Add(new ListItem("Guia", Vencimento.GUIA));
            ddlTipoVencimentos.Items.Add(new ListItem("Requerimento de Lavra (Álvara de Pesquisa)", Vencimento.REQUERIMENTOLAVRA_ALVARAPESQUISA));
            ddlTipoVencimentos.Items.Add(new ListItem("Notificação de Pesquisa da ANM", Vencimento.NOTIFICACAO_PESQUISADNPM));
            ddlTipoVencimentos.Items.Add(new ListItem("Álvara de Pesquisa", Vencimento.ALVARAPESQUISA));
            ddlTipoVencimentos.Items.Add(new ListItem("Taxa anual por hectare", Vencimento.TAXAANUALPORHECTARE));
            ddlTipoVencimentos.Items.Add(new ListItem("DIPEM", Vencimento.DIPEMConst));
            ddlTipoVencimentos.Items.Add(new ListItem("Renúncia do Álvara de Pesquisa", Vencimento.LIMITERENUNCIA));
            ddlTipoVencimentos.Items.Add(new ListItem("Requerimento de LP Total", Vencimento.REQUERIMENTOLPTOTAL));
            ddlTipoVencimentos.Items.Add(new ListItem("Extração", Vencimento.EXTRACAO));
            ddlTipoVencimentos.Items.Add(new ListItem("Entrega de Licença ou Protocolo", Vencimento.ENTREGALICENCAOUPROTOCOLO));
            ddlTipoVencimentos.Items.Add(new ListItem("Licenciamento", Vencimento.LICENCIAMENTO));
            ddlTipoVencimentos.Items.Add(new ListItem("Requerimento de Imissão de Posse", Vencimento.REQUERIMENTOIMISSAOPOSSE));
        }

        if (Permissoes.UsuarioPossuiAcessoModuloDiversos(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Diversos")))
        {
            ddlTipoVencimentos.Items.Add(new ListItem("Vencimento Diverso", Vencimento.VENCIMENTODIVERSO));
        }

    }

    private void CarregarStatusVencimentos()
    {
        ddlStatusVencimentoPorPeriodo.DataValueField = "Id";
        ddlStatusVencimentoPorPeriodo.DataTextField = "Nome";
        IList<Status> status = Status.ConsultarTodosOrdemAlfabetica();
        Status statusAux = new Status();
        statusAux.Nome = "-- Todos --";
        status.Insert(0, statusAux);
        ddlStatusVencimentoPorPeriodo.DataSource = status;
        ddlStatusVencimentoPorPeriodo.DataBind();
    }

    private void CarregarTiposDiversos(DropDownList drop)
    {
        drop.DataValueField = "Id";
        drop.DataTextField = "Nome";
        drop.DataSource = TipoDiverso.ConsultarTodosOrdemAlfabetica();
        drop.DataBind();
        drop.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void CarregarVencimentosPorPeriodo()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);
        DateTime dataDePeriodo = tbxDataDePeriodoVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataDePeriodoVencimentos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehPeriodo = tbxDataAtehPeriodoVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataAtehPeriodoVencimentos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        Fontes.relatorioVencimentosPorPeriodoDataTable fonte = new Fontes.relatorioVencimentosPorPeriodoDataTable();

        IList<Vencimento> vencimentos = Vencimento.FiltrarRelatorio(ddlGrupoEconomicoVencimentos.SelectedValue.ToInt32(), ddlEmpresaVencimentos.SelectedValue.ToInt32(), ddlTipoVencimentos.SelectedValue,
            dataDePeriodo, dataAtehPeriodo, ddlEstadoVencimentoPeriodo.SelectedValue.ToInt32(), ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso" ? 0 : ddlStatusVencimentoPorPeriodo.SelectedValue.ToInt32(), ddlPeriodicosVencimentoPeriodo.SelectedValue.ToInt32());
        

        IList<ModuloPermissao> modulosUsuario = ModuloPermissao.RecarregarModulos(UsuarioLogado.ConsultarPorId().ModulosPermissao);

        if (vencimentos != null && vencimentos.Count > 0)
            vencimentos = this.FiltrarVencimentosPelasPermissoes(vencimentos);

        if (ddlProrrogacaoPrazoVencimentoPeriodo.SelectedValue.ToInt32() > 0)
            this.RemoverVencimentosPorPeriodoComOuSemProrrogacoesPrazo(vencimentos, ddlProrrogacaoPrazoVencimentoPeriodo.SelectedValue.ToInt32());

        if (ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso")
        {
            this.RemoverVencimentosSimples(vencimentos);
        }

        this.RemoverVencimentosRepetidos(vencimentos);
                       

        grvRelatorio.DataSource = vencimentos != null && vencimentos.Count > 0 ? vencimentos : new List<Vencimento>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        string descricaoGrupoEconomico = ddlGrupoEconomicoVencimentos.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoVencimentos.SelectedItem.Text;
        string descricaoEmpresa = ddlEmpresaVencimentos.SelectedIndex == 0 ? "Todas" : ddlEmpresaVencimentos.SelectedItem.Text;
        string descricaoEstado = ddlEstadoVencimentoPeriodo.SelectedIndex == 0 ? "Todos" : ddlEstadoVencimentoPeriodo.SelectedItem.Text;
        string descricaoTipoVencimento = ddlTipoVencimentos.SelectedIndex == 0 ? "Todos" : ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso" ? ddlTipoVencimentoDiversoPeriodo.SelectedIndex == 0 ? "Vencimento Diverso" : ddlTipoVencimentoDiversoPeriodo.SelectedItem.Text : ddlTipoVencimentos.SelectedItem.Text;
        string descricaoStatus = ddlStatusVencimentoPorPeriodo.SelectedIndex == 0 && ddlStatusVencimentoDiversoPeriodo.SelectedIndex <= 0 ? "Todos" : ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso" ? ddlStatusVencimentoDiversoPeriodo.SelectedIndex == 0 ? "Todos" : ddlStatusVencimentoDiversoPeriodo.SelectedItem.Text : ddlStatusVencimentoPorPeriodo.SelectedItem.Text;
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDePeriodo, dataAtehPeriodo);
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoPeriodico = ddlPeriodicosVencimentoPeriodo.SelectedIndex == 0 ? "Todos" : ddlPeriodicosVencimentoPeriodo.SelectedItem.Text;
        string descricaoProrrogacaoPrazo = ddlProrrogacaoPrazoVencimentoPeriodo.SelectedIndex == 0 ? "Todos" : ddlProrrogacaoPrazoVencimentoPeriodo.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupoEconomico);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);
        CtrlHeader.InsertFiltroEsquerda("Data", descricaoPeriodo == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoPeriodo);

        CtrlHeader.InsertFiltroCentro("Tipo", descricaoTipoVencimento);
        CtrlHeader.InsertFiltroCentro("Status", descricaoStatus);
        CtrlHeader.InsertFiltroCentro("Períodicos", descricaoPeriodico);

        CtrlHeader.InsertFiltroDireita("Prorrogação de prazo", descricaoProrrogacaoPrazo);
        CtrlHeader.InsertFiltroDireita("Estado", descricaoEstado);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Vencimentos por período");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    private IList<Vencimento> FiltrarVencimentosPelasPermissoes(IList<Vencimento> vencimentos)
    {
        IList<Vencimento> retorno = new List<Vencimento>();

        foreach (Vencimento vencimento in vencimentos)
        {
            ModuloPermissao moduloVencimento = vencimento.GetModulo;

            if (moduloVencimento != null) 
            {
                if (moduloVencimento == ModuloPermissao.ConsultarPorNome("DNPM") && this.UsuarioLogadoPossuiAcessoAoVencimentoDoModuloDNPM(vencimento)) 
                    retorno.Add(vencimento);

                if (moduloVencimento == ModuloPermissao.ConsultarPorNome("Diversos") && this.UsuarioLogadoPossuiAcessoAoVencimentoDoModuloDiversos(vencimento))
                    retorno.Add(vencimento);

                if (moduloVencimento == ModuloPermissao.ConsultarPorNome("Meio Ambiente") && this.UsuarioLogadoPossuiAcessoAoVencimentoDoModuloMeioAmbiente(vencimento))
                    retorno.Add(vencimento);                
            }                
        }

        return retorno;
    }

    private bool UsuarioLogadoPossuiAcessoAoVencimentoDoModuloMeioAmbiente(Vencimento vencimento)
    {
        if (this.ConfiguracaoModuloMeioAmbiente == null)
            return false;

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral == null || this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)))
                return true;
        }

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloMeioAmbiente == null || this.EmpresasPermissaoModuloMeioAmbiente.Count == 0)
                return false;

            if (this.EmpresasPermissaoModuloMeioAmbiente != null && vencimento.GetEmpresa != null && this.EmpresasPermissaoModuloMeioAmbiente.Contains(vencimento.GetEmpresa))
                return true;
            else
                return false;
        }


        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {

            if (vencimento.GetTipoProcessoMeioAmbiente != "") 
            {
                if (vencimento.GetTipoProcessoMeioAmbiente == "Processo") 
                {
                    if ((this.ProcessosPermissaoModuloMeioAmbiente == null || this.ProcessosPermissaoModuloMeioAmbiente.Count == 0))
                        return false;

                    if (this.ProcessosPermissaoModuloMeioAmbiente != null && vencimento.GetProcessoMeioAmbiente != null && this.ProcessosPermissaoModuloMeioAmbiente.Contains(vencimento.GetProcessoMeioAmbiente))
                        return true;
                    else
                        return false;
                }

                if (vencimento.GetTipoProcessoMeioAmbiente == "OutrosEmpresa") 
                {
                    if ((this.OutrosEmpresasPermissaoModuloMeioAmbiente == null || this.OutrosEmpresasPermissaoModuloMeioAmbiente.Count == 0))
                        return false;

                    if (this.OutrosEmpresasPermissaoModuloMeioAmbiente != null && vencimento.GetOutroEmpresa != null && this.OutrosEmpresasPermissaoModuloMeioAmbiente.Contains(vencimento.GetOutroEmpresa))
                        return true;
                    else
                        return false;
                }

                if (vencimento.GetTipoProcessoMeioAmbiente == "Cadastro") 
                {
                    if ((this.CadastrosTecnicosPermissaoModuloMeioAmbiente == null || this.CadastrosTecnicosPermissaoModuloMeioAmbiente.Count == 0))
                        return false;

                    if (this.CadastrosTecnicosPermissaoModuloMeioAmbiente != null && vencimento.GetCadastroTecnico != null && this.CadastrosTecnicosPermissaoModuloMeioAmbiente.Contains(vencimento.GetCadastroTecnico))
                        return true;
                    else
                        return false;
                }
            }
            
        }

        return false;
    }

    private bool UsuarioLogadoPossuiAcessoAoVencimentoDoModuloDiversos(Vencimento vencimento)
    {
        if (this.ConfiguracaoModuloDiversos == null)
            return false;

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral == null || this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)))
                return true;
        }

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloDiversos == null || this.EmpresasPermissaoModuloDiversos.Count == 0)
                return false;

            if (this.EmpresasPermissaoModuloDiversos != null && vencimento.GetEmpresa != null && this.EmpresasPermissaoModuloDiversos.Contains(vencimento.GetEmpresa))
                return true;
            else
                return false;
        }

        return false;
    }

    private bool UsuarioLogadoPossuiAcessoAoVencimentoDoModuloDNPM(Vencimento vencimento)
    {
        if (this.ConfiguracaoModuloDNPM == null)
            return false;

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral == null || this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)))
                return true;
        }

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloDNPM == null || this.EmpresasPermissaoModuloDNPM.Count == 0)
                return false;

            if (this.EmpresasPermissaoModuloDNPM != null && vencimento.GetEmpresa != null && this.EmpresasPermissaoModuloDNPM.Contains(vencimento.GetEmpresa))
                return true;
            else
                return false;
        }


        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (this.ProcessosPermissaoModuloDNPM == null || this.ProcessosPermissaoModuloDNPM.Count == 0)
                return false;

            if (this.ProcessosPermissaoModuloDNPM != null && vencimento.GetProcessoDNPM != null && this.ProcessosPermissaoModuloDNPM.Contains(vencimento.GetProcessoDNPM))
                return true;
            else
                return false;
        }

        return false;
    }

    private void RemoverVencimentosPorPeriodoComOuSemProrrogacoesPrazo(IList<Vencimento> vencimentos, int prorrogacoes)
    {
        if (prorrogacoes == 1)
        {
            if (vencimentos != null && vencimentos.Count > 0)
            {
                for (int i = vencimentos.Count - 1; i > -1; i--)
                {
                    if (vencimentos[i] == null || vencimentos[i].ProrrogacoesPrazo == null || vencimentos[i].ProrrogacoesPrazo.Count == 0)
                    {
                        vencimentos.Remove(vencimentos[i]);
                    }
                }
            }
        }
        else
        {
            if (vencimentos != null && vencimentos.Count > 0)
            {
                for (int i = vencimentos.Count - 1; i > -1; i--)
                {
                    if (vencimentos[i] != null && vencimentos[i].ProrrogacoesPrazo != null && vencimentos[i].ProrrogacoesPrazo.Count > 0)
                    {
                        vencimentos.Remove(vencimentos[i]);
                    }
                }
            }
        }
    }

    private void RemoverVencimentosSimples(IList<Vencimento> vencimentos)
    {
        if (vencimentos != null && vencimentos.Count > 0)
        {
            VencimentoDiverso aux = new VencimentoDiverso();
            Diverso diverso = new Diverso();

            for (int i = vencimentos.Count - 1; i > -1; i--)
            {
                aux = VencimentoDiverso.ConsultarPorId(vencimentos[i].Id);
                if (aux != null && aux.Id > 0)
                {
                    if (ddlTipoVencimentoDiversoPeriodo.SelectedIndex != 0 || ddlStatusVencimentoDiversoPeriodo.SelectedIndex != 0)
                    {
                        if (aux.Diverso != null)
                        {
                            diverso = aux.Diverso.ConsultarPorId();
                            if (diverso.GetUltimoVencimento != null)
                            {
                                if (diverso.GetUltimoVencimento.Id != aux.Id)
                                {
                                    vencimentos.Remove(vencimentos[i]);
                                }
                                else
                                {
                                    if (ddlTipoVencimentoDiversoPeriodo.SelectedValue.ToInt32() != diverso.TipoDiverso.Id && ddlTipoVencimentoDiversoPeriodo.SelectedValue.ToInt32() > 0)
                                    {
                                        vencimentos.Remove(vencimentos[i]);
                                    }
                                    else
                                        if (ddlStatusVencimentoDiversoPeriodo.SelectedValue.ToInt32() != aux.StatusDiverso.Id && ddlStatusVencimentoDiversoPeriodo.SelectedValue.ToInt32() > 0)
                                        {
                                            vencimentos.Remove(vencimentos[i]);
                                        }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void RemoverVencimentosRepetidos(IList<Vencimento> vencimentos)
    {
        if (vencimentos != null && vencimentos.Count > 0)
        {
            for (int i = vencimentos.Count - 1; i > -1; i--)
            {
                if (vencimentos[i].GetType() == typeof(VencimentoDiverso))
                {
                    VencimentoDiverso v = (VencimentoDiverso)vencimentos[i];
                    Diverso d = v.Diverso;
                    if (d.GetUltimoVencimento != null && d.GetUltimoVencimento.Id != v.Id)
                        vencimentos.Remove(vencimentos[i]);
                }
            }
        }
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlGrupoEconomicoVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoVencimentos, ddlEmpresaVencimentos);
    }

    protected void ddlTipoVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso")
            {                
                this.CarregarTiposDiversos(ddlTipoVencimentoDiversoPeriodo);
            }

            this.CarregarEmpresas(ddlGrupoEconomicoVencimentos, ddlEmpresaVencimentos);
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

    protected void ddlTipoVencimentoDiversoPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatusDiversos(ddlTipoVencimentoDiversoPeriodo, ddlStatusVencimentoDiversoPeriodo);
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
            this.CarregarVencimentosPorPeriodo();
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