using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioProcessosPorContratos : PageBase
{
    private Msg msg = new Msg();

    //Sessoes Modulo Contratos
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
        this.EmpresasPermissaoModuloContratos = null;
        this.SetoresPermissaoModuloContratos = null;
        this.EmpresasPermissaoModuloDNPM = null;
        this.ProcessosPermissaoModuloDNPM = null;
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoModuloMeioAmbiente = null;
    }

    private void CarregarSessoesPermissoes()
    {
        this.CarregarSessoesModuloContratos();
        this.CarregarSessoesModuloDNPM();
        this.CarregarSessoesModuloMeioAmbiente();
    }

    private void CarregarSessoesModuloContratos()
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
            this.ProcessosPermissaoModuloMeioAmbiente = Processo.ObterProcessosQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        else
            this.ProcessosPermissaoModuloMeioAmbiente = null;
    }

    private void CarregarCampos()
    {
        this.CarregarGruposEconomicos(ddlGrupoEconomicoContratoPorProcesso);
        this.CarregarEmpresasContratosPorProcessos();        
        this.CarregarStatusContratosDiversos(ddlStatusContratoProcessoPorContrato);        
        this.CarregarTiposProcessosAgrupandoPorContratos();
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

    private void CarregarEmpresasContratosPorProcessos()
    {
        ddlEmpresaContratoPorProcesso.Items.Clear();
        ddlEmpresaContratoPorProcesso.Items.Add(new ListItem("-- Todas --", "0"));

        IList<Empresa> empresas;

        if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() > 0)
        {
            if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 1) //Processos DNPM
            {
                //Carregando as empresas de acordo com a configuração de permissão
                if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                {
                    if (ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32() > 0)
                        empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
                    else
                        empresas = this.EmpresasPermissaoModuloDNPM != null ? this.EmpresasPermissaoModuloDNPM : new List<Empresa>();
                }
                else
                {
                    GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32());
                    empresas = grupo != null && grupo.Empresas != null ? grupo.Empresas : new List<Empresa>();
                }
            }
            else  //Processos de meio ambiente
            {
                //Carregando as empresas de acordo com a configuração de permissão
                if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                {
                    if (ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32() > 0)
                        empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
                    else
                        empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente : new List<Empresa>();
                }
                else
                {
                    GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32());
                    empresas = grupo != null && grupo.Empresas != null ? grupo.Empresas : new List<Empresa>();
                }
            }

            if (empresas != null && empresas.Count > 0)
            {
                empresas = empresas.OrderBy(x => x.Nome).ToList();
                foreach (Empresa emp in empresas)
                {
                    if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                        ddlEmpresaContratoPorProcesso.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                    else
                        ddlEmpresaContratoPorProcesso.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                }
            }
        }

    }   

    private void CarregarTiposProcessosAgrupandoPorContratos()
    {
        ddlTipoProcessoProcessosPorContrato.Items.Clear();
        ddlTipoProcessoProcessosPorContrato.Items.Add(new ListItem("-- Selecione --", "0"));

        if (Permissoes.UsuarioPossuiAcessoModuloDNPM(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("DNPM")))
            ddlTipoProcessoProcessosPorContrato.Items.Add(new ListItem("Processo Minerário", "1"));

        if (Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
            ddlTipoProcessoProcessosPorContrato.Items.Add(new ListItem("Processo Ambiental", "2"));
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

    private void CarregarRelatorioProcessosPorContratos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Contrato", typeof(string)));
        dt.Columns.Add(new DataColumn("NumeroProcesso", typeof(string)));
        dt.Columns.Add(new DataColumn("TipoProcesso", typeof(string)));
        dt.Columns.Add(new DataColumn("Empresa", typeof(string)));
        dt.Columns.Add(new DataColumn("Abertura", typeof(string)));        

        IList<ContratoDiverso> contratosDiversos = ContratoDiverso.FiltrarRelatorioContratosPorProcessos(ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32(), tbxNumeroContratoPorProcesso.Text, tbxObjetoContratoPorProcesso.Text, ddlStatusContratoProcessoPorContrato.SelectedValue.ToInt32(), this.ConfiguracaoModuloContratos.Tipo, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos);

        if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 1)
            this.RemoverContratosSemProcessosDNPM(contratosDiversos);
        else
            this.RemoverContratosSemProcessosAmbientais(contratosDiversos);

        if (contratosDiversos != null && contratosDiversos.Count > 0)
        {
            foreach (ContratoDiverso contratoDiverso in contratosDiversos)
            {
                if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 1)
                {
                    foreach (ProcessoDNPM processoDNPM in contratoDiverso.ProcessosDNPM)
                    {
                        if (this.UsuarioLogadoPossuiAcessoAoProcessoDNPMPorPermissoes(processoDNPM)) 
                        {
                            dr = dt.NewRow();
                            dr["Contrato"] = "Contrato Nº: " + contratoDiverso.Numero + (contratoDiverso.Objeto.IsNotNullOrEmpty() ? ", Objeto: " + contratoDiverso.Objeto : "") + (contratoDiverso.StatusContratoDiverso != null ? ", Status: " + contratoDiverso.StatusContratoDiverso.Nome : "");
                            dr["NumeroProcesso"] = processoDNPM.GetNumeroProcessoComMascara;
                            dr["TipoProcesso"] = "Minerário";
                            dr["Empresa"] = processoDNPM.Empresa != null ? processoDNPM.Empresa.Nome : "";
                            dr["Abertura"] = processoDNPM.DataAbertura.ToShortDateString();
                            dt.Rows.Add(dr);
                        }                        
                    }
                }
                else if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 2)
                {
                    foreach (Processo processoAmbiental in contratoDiverso.Processos)
                    {
                        if (this.UsuarioLogadoPossuiAcessoAoProcessoMeioAmbientePorPermissoes(processoAmbiental)) 
                        {
                            dr = dt.NewRow();
                            dr["Contrato"] = "Contrato Nº: " + contratoDiverso.Numero + (contratoDiverso.Objeto.IsNotNullOrEmpty() ? ", Objeto: " + contratoDiverso.Objeto : "") + (contratoDiverso.StatusContratoDiverso != null ? ", Status: " + contratoDiverso.StatusContratoDiverso.Nome : "");
                            dr["NumeroProcesso"] = processoAmbiental.Numero;
                            dr["TipoProcesso"] = "Ambiental";
                            dr["Empresa"] = processoAmbiental.Empresa != null ? processoAmbiental.Empresa.Nome : "";
                            dr["Abertura"] = processoAmbiental.DataAbertura.ToShortDateString();
                            dt.Rows.Add(dr);
                        }                         
                    }
                }
            }
        }

        ViewState["CurrentTable"] = dt;

        grvRelatorio.DataSource = dt;
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoNumeroContrato = tbxNumeroContratoPorProcesso.Text.IsNotNullOrEmpty() ? tbxNumeroContratoPorProcesso.Text : "Todos";
        string descricaoObjeto = tbxObjetoContratoPorProcesso.Text.IsNotNullOrEmpty() ? tbxObjetoContratoPorProcesso.Text : "Todos";
        string descricaoStatus = ddlStatusContratoProcessoPorContrato.SelectedIndex > 0 ? ddlStatusContratoProcessoPorContrato.SelectedItem.Text : "Todos";
        string descricaoTipoProcessoProcessosPorContratos = ddlTipoProcessoProcessosPorContrato.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Número do Contrato", descricaoNumeroContrato);
        CtrlHeader.InsertFiltroEsquerda("Objeto do Contrato", descricaoObjeto);

        CtrlHeader.InsertFiltroCentro("Status do Contrato", descricaoStatus);

        CtrlHeader.InsertFiltroDireita("Tipo de Processo", descricaoTipoProcessoProcessosPorContratos);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Processos por Contratos");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);    
    }

    private bool UsuarioLogadoPossuiAcessoAoProcessoDNPMPorPermissoes(ProcessoDNPM processo)
    {
        if (this.ConfiguracaoModuloDNPM == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral == null || this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)))
                return true;
        }

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloDNPM == null || this.EmpresasPermissaoModuloDNPM.Count == 0)
                return false;

            if (this.EmpresasPermissaoModuloDNPM != null && processo.Empresa != null && this.EmpresasPermissaoModuloDNPM.Contains(processo.Empresa))
                return true;
            else
                return false;
        }


        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (this.ProcessosPermissaoModuloDNPM == null || this.ProcessosPermissaoModuloDNPM.Count == 0)
                return false;

            if (this.ProcessosPermissaoModuloDNPM != null && processo != null && this.ProcessosPermissaoModuloDNPM.Contains(processo))
                return true;
            else
                return false;
        }

        return false;
    }

    private bool UsuarioLogadoPossuiAcessoAoProcessoMeioAmbientePorPermissoes(Processo processo)
    {
        if (this.ConfiguracaoModuloMeioAmbiente == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral == null || this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)))
                return true;
        }

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloMeioAmbiente == null || this.EmpresasPermissaoModuloMeioAmbiente.Count == 0)
                return false;

            if (this.EmpresasPermissaoModuloMeioAmbiente != null && processo.Empresa != null && this.EmpresasPermissaoModuloMeioAmbiente.Contains(processo.Empresa))
                return true;
            else
                return false;
        }


        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (this.ProcessosPermissaoModuloMeioAmbiente == null || this.ProcessosPermissaoModuloMeioAmbiente.Count == 0)
                return false;

            if (this.ProcessosPermissaoModuloMeioAmbiente != null && processo != null && this.ProcessosPermissaoModuloMeioAmbiente.Contains(processo))
                return true;
            else
                return false;
        }

        return false;
    }

    private void RemoverContratosSemProcessosDNPM(IList<ContratoDiverso> contratosDiversos)
    {
        if (contratosDiversos != null && contratosDiversos.Count > 0)
        {
            for (int i = contratosDiversos.Count - 1; i > -1; i--)
            {
                if (contratosDiversos[i].ProcessosDNPM == null || contratosDiversos[i].ProcessosDNPM.Count == 0)
                    contratosDiversos.Remove(contratosDiversos[i]);
            }
        }
    }

    private void RemoverContratosSemProcessosAmbientais(IList<ContratoDiverso> contratosDiversos)
    {
        if (contratosDiversos != null && contratosDiversos.Count > 0)
        {
            for (int i = contratosDiversos.Count - 1; i > -1; i--)
            {
                if (contratosDiversos[i].Processos == null || contratosDiversos[i].Processos.Count == 0)
                    contratosDiversos.Remove(contratosDiversos[i]);
            }
        }
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 0)
            {
                msg.CriarMensagem("Selecione o tipo de processo, para poder prosseguir", "Alerta", MsgIcons.Alerta);
                return;
            }

            this.CarregarRelatorioProcessosPorContratos();
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

    protected void ddlGrupoEconomicoContratoPorProcesso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresasContratosPorProcessos();
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

    protected void ddlTipoProcessoProcessosPorContrato_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresasContratosPorProcessos();
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
}