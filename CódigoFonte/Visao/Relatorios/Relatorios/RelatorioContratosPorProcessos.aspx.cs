using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioContratosPorProcessos : PageBase
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
        this.CarregarTiposProcessosConformePermissoes();        
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

        if (ddlTipoProcesso.SelectedValue.ToInt32() > 0) 
        {
            if (ddlTipoProcesso.SelectedValue.ToInt32() == 1) //Processos DNPM
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

    private void CarregarTiposProcessosConformePermissoes()
    {
        ddlTipoProcesso.Items.Clear();
        ddlTipoProcesso.Items.Add(new ListItem("-- Selecione --", "0"));        

        if (Permissoes.UsuarioPossuiAcessoModuloDNPM(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("DNPM")))
            ddlTipoProcesso.Items.Add(new ListItem("Processo Minerário", "1"));

        if (Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
            ddlTipoProcesso.Items.Add(new ListItem("Processo Ambiental", "2"));
    }

    private void CarregarRelatorioContratosPorProcessos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Processo", typeof(string)));
        dt.Columns.Add(new DataColumn("Numero", typeof(string)));
        dt.Columns.Add(new DataColumn("Objeto", typeof(string)));
        dt.Columns.Add(new DataColumn("Status", typeof(string)));
        dt.Columns.Add(new DataColumn("Abertura", typeof(string)));

        if (ddlTipoProcesso.SelectedValue.ToInt32() == 1)   //1 para processos minerarios
        {
            IList<ProcessoDNPM> processosDNPM = ProcessoDNPM.FiltrarRelatorioContratosPorProcessos(ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32(), Empresa.ConsultarPorId(ddlEmpresaContratoPorProcesso.SelectedValue.ToInt32()), tbxNumeroProcesso.Text,
                tbxSubstanciaContratoPorProcesso.Text, this.ConfiguracaoModuloDNPM.Tipo, this.EmpresasPermissaoModuloDNPM, this.ProcessosPermissaoModuloDNPM);
            this.RemoverProcessosMinerariosSemContratos(processosDNPM);            

            if (processosDNPM != null && processosDNPM.Count > 0)
            {
                foreach (ProcessoDNPM processo in processosDNPM)
                {
                    foreach (ContratoDiverso contrato in processo.ContratosDiversos)
                    {
                        if (this.UsuarioLogadoPossuiAcessoAoContratoPorPermissoes(contrato)) 
                        {
                            dr = dt.NewRow();
                            dr["Processo"] = "Processo ANM Nº: " + processo.GetNumeroProcessoComMascara + (processo.Empresa != null ? ", Empresa: " + processo.Empresa.Nome : "") + ", Abertura: " + processo.DataAbertura.ToShortDateString();
                            dr["Numero"] = contrato.Numero;
                            dr["Objeto"] = contrato.Objeto;
                            dr["Status"] = contrato.GetDescricaoStatus;
                            dr["Abertura"] = contrato.GetDataAbertura;
                            dt.Rows.Add(dr);                        
                        }                        
                    }
                }
            }
            
        }
        else    // 2 para processos ambientais
        {
            IList<Processo> processos = Processo.FiltrarRelatorioContratosPorProcessos(ddlGrupoEconomicoContratoPorProcesso.SelectedValue.ToInt32(), Empresa.ConsultarPorId(ddlEmpresaContratoPorProcesso.SelectedValue.ToInt32()),
                tbxNumeroProcesso.Text, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente);

            if (rblTipoProcesso.SelectedValue.ToInt32() > 0)
                this.RemoverProcessosAmbientaisDeOutrosTipos(processos);

            this.RemoverProcessosAmbientaisSemContratos(processos);

            if (processos != null && processos.Count > 0)
            {
                foreach (Processo processo in processos)
                {
                    foreach (ContratoDiverso contrato in processo.ContratosDiversos)
                    {
                        if (this.UsuarioLogadoPossuiAcessoAoContratoPorPermissoes(contrato)) 
                        {
                            dr = dt.NewRow();
                            dr["Processo"] = "Processo Ambiental Nº: " + processo.Numero + (processo.Empresa != null ? ", Empresa: " + processo.Empresa.Nome : "") + ", Abertura: " + processo.DataAbertura.ToShortDateString();
                            dr["Numero"] = contrato.Numero;
                            dr["Objeto"] = contrato.Objeto;
                            dr["Status"] = contrato.GetDescricaoStatus;
                            dr["Abertura"] = contrato.GetDataAbertura;
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

        string descricaoEmpresa = ddlEmpresaContratoPorProcesso.SelectedIndex > 0 ? ddlEmpresaContratoPorProcesso.SelectedItem.Text : "Todas";
        string descricaoTipoProcesso = ddlTipoProcesso.SelectedItem.Text;
        string desSubstanciaOuTipoOrgao = ddlTipoProcesso.SelectedIndex == 1 ? "Substância:" : "Tipo dos Processos Ambientais:";
        string filtroSubstanciaOuTipoOrgao = ddlTipoProcesso.SelectedIndex == 1 ? tbxSubstanciaContratoPorProcesso.Text.IsNotNullOrEmpty() ? tbxSubstanciaContratoPorProcesso.Text : "Todas" : rblTipoProcesso.SelectedItem.Text;
        string descricaoNumeroProcesso = tbxNumeroProcesso.Text.IsNotNullOrEmpty() ? tbxNumeroProcesso.Text : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Tipo de Processo", descricaoTipoProcesso);
        CtrlHeader.InsertFiltroEsquerda("Empresa", descricaoEmpresa);

        CtrlHeader.InsertFiltroCentro("Número do Processo", descricaoNumeroProcesso);

        CtrlHeader.InsertFiltroDireita(desSubstanciaOuTipoOrgao, filtroSubstanciaOuTipoOrgao);        

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Contratos por Processos");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);    
    }

    private bool UsuarioLogadoPossuiAcessoAoContratoPorPermissoes(ContratoDiverso contrato)
    {
        if (this.ConfiguracaoModuloContratos == null)
            Response.Redirect("../../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral == null || this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count > 0 && this.ConfiguracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)))
                return true;
        }

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloContratos == null || this.EmpresasPermissaoModuloContratos.Count == 0)
                return false;

            if (this.EmpresasPermissaoModuloContratos != null && contrato.Empresa != null && this.EmpresasPermissaoModuloContratos.Contains(contrato.Empresa))
                return true;
            else
                return false;
        }
        

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            if (this.SetoresPermissaoModuloContratos == null || this.SetoresPermissaoModuloContratos.Count == 0)
                return false;

            if (this.SetoresPermissaoModuloContratos != null && contrato.Setor != null && this.SetoresPermissaoModuloContratos.Contains(contrato.Setor))
                return true;
            else
                return false;
        }

        return false;
    }

    private void RemoverProcessosMinerariosSemContratos(IList<ProcessoDNPM> processosDNPM)
    {
        if (processosDNPM != null && processosDNPM.Count > 0)
        {
            for (int i = processosDNPM.Count - 1; i > -1; i--)
            {
                if (processosDNPM[i].ContratosDiversos == null || processosDNPM[i].ContratosDiversos.Count == 0)
                    processosDNPM.Remove(processosDNPM[i]);
            }
        }
    }   

    private void RemoverProcessosAmbientaisDeOutrosTipos(IList<Processo> processos)
    {
        if (processos != null && processos.Count > 0)
        {
            for (int i = processos.Count - 1; i > -1; i--)
            {
                if (rblTipoProcesso.SelectedValue.ToInt32() == 1)
                {
                    if (processos[i].OrgaoAmbiental.GetType() != typeof(OrgaoMunicipal))
                        processos.Remove(processos[i]);
                }

                if (rblTipoProcesso.SelectedValue.ToInt32() == 2)
                {
                    if (processos[i].OrgaoAmbiental.GetType() != typeof(OrgaoEstadual))
                        processos.Remove(processos[i]);
                }

                if (rblTipoProcesso.SelectedValue.ToInt32() == 3)
                {
                    if (processos[i].OrgaoAmbiental.GetType() != typeof(OrgaoFederal))
                        processos.Remove(processos[i]);
                }
            }
        }
    }

    private void RemoverProcessosAmbientaisSemContratos(IList<Processo> processos)
    {
        if (processos != null && processos.Count > 0)
        {
            for (int i = processos.Count - 1; i > -1; i--)
            {
                if (processos[i].ContratosDiversos == null || processos[i].ContratosDiversos.Count == 0)
                    processos.Remove(processos[i]);
            }
        }
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoProcesso.SelectedValue.ToInt32() == 0)
            {
                msg.CriarMensagem("Selecione o tipo de processo, para poder prosseguir", "Alerta", MsgIcons.Alerta);
                return;
            }

            this.CarregarRelatorioContratosPorProcessos();
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

    protected void ddlTipoProcesso_SelectedIndexChanged(object sender, EventArgs e)
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

    #endregion    
}