using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Utilitarios.Criptografia;
using System.Collections;
using Modelo;
using System.Configuration;

public partial class Relatorios_FiltrosRelatorios : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CarregarCampos();
        }
    }
    
    #region _____________________ Eventos _________________________

    protected void trvRelatorios_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {            

            if (trvRelatorios.SelectedNode.Value.Contains("Modulo_") || trvRelatorios.SelectedNode.Value.Contains("Raiz"))
            {
                btnExibirRelatorio.Visible = false;
                mtvFiltros.ActiveViewIndex = 0;
                return;
            }

            //btnExibirRelatorio.Visible = true;

            //mtvFiltros.ActiveViewIndex = trvRelatorios.SelectedNode.Value.ToInt32();
            ////2 = Órgãos Ambientais
            ////13 = Pendencias Grupos Econômicos
            //if (mtvFiltros.ActiveViewIndex == 2 ||
            //    mtvFiltros.ActiveViewIndex == 13)
            //    this.CarregarRelatorioSelecionado();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlEmpresaContratosDiversos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmpresaContratosDiversos.SelectedValue.ToInt32() > 0)
            {
                empresa_como.Visible = true;
            }
            else
            {
                empresa_como.Visible = false;
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioSelecionado();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlGrupoEconomicoProcessosMeioAmbiente_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoProcessosMeioAmbiente, ddlEmpresaProcessosMeioAmbiente);
    }

    protected void ddlGrupoEconomicoLicencaAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoLicencaAmbiental, ddlEmpresaLicencaAmbiental);
    }

    protected void ddlGrupoEconomicoCondicionantes_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoCondicionantes, ddlEmpresaCondicionantes);
    }

    protected void ddlGrupoEconomicoProcessoDNPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoProcessoDNPM, ddlEmpresaProcessoDNPM);
    }

    protected void ddlGrupoEconomicoVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoVencimentos, ddlEmpresaVencimentos);
    }

    protected void ddlGrupoEconomicoRal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoRal, ddlEmpresaRal);
    }

    protected void ddlGrupoEconomicoGuiaUtilizacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoGuiaUtilizacao, ddlEmpresaGuiaUtilizacao);
    }

    protected void ddlGrupoEconomicoCTF_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoCTF, ddlEmpresaCTF);
    }

    protected void ddlGrupoEconomicoVencimentoDiverso_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresaVencimentoDiverso(ddlGrupoEconomicoVencimentoDiverso);
    }

    protected void ddlTipoVencimentoDiverso_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarStatusDiversos(ddlTipoVencimentoDiverso, ddlStatusVencimentoDiverso);
    }

    protected void ddlTipoVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso")
            {
                vencimentos_diversos_periodo.Visible = true;
                vencimentos_por_periodo_simples.Visible = false;
                this.CarregarTiposDiversos(ddlTipoVencimentoDiversoPeriodo);
            }
            else
            {
                vencimentos_diversos_periodo.Visible = false;
                vencimentos_por_periodo_simples.Visible = true;
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlGrupoRenuncias_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresas(ddlGrupoRenuncias, ddlEmpresaRenuncias);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlEstadoFornecedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstadoFornecedor, ddlCidadeFornecedor);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlComoContratosDiversos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlComoContratosDiversos.SelectedValue.ToInt32() > 0)
            {
                if (ddlComoContratosDiversos.SelectedItem.Text == "Contratada")
                {
                    cliente_contrato_diverso.Visible = true;
                    fornecedor_contrato.Visible = false;
                }
                else if (ddlComoContratosDiversos.SelectedItem.Text == "Contratante")
                {
                    cliente_contrato_diverso.Visible = false;
                    fornecedor_contrato.Visible = true;

                }
            }
            else
            {
                cliente_contrato_diverso.Visible = false;
                fornecedor_contrato.Visible = false;
            }

        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlGrupoEconomicoOutrosVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEmpresas(ddlGrupoEconomicoOutrosVencimentos, ddlEmpresaOutrosVencimentos);
    }

    protected void ddlAgrupor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAgrupor.SelectedValue.ToInt32() > 0)
            {
                if (ddlAgrupor.SelectedValue.ToInt32() == 1)
                {
                    contratos_por_processos.Visible = true;
                    processos_por_contratos.Visible = false;
                }
                else
                {
                    contratos_por_processos.Visible = false;
                    processos_por_contratos.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlTipoProcesso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoProcesso.SelectedValue.ToInt32() > 0)
            {
                if (ddlTipoProcesso.SelectedValue.ToInt32() == 1)
                {
                    pesquisa_substancia.Visible = true;
                    pesquisa_tipo_orgao_processo.Visible = false;
                }
                else
                {
                    pesquisa_substancia.Visible = false;
                    pesquisa_tipo_orgao_processo.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

    #region _____________________ Métodos _________________________

    private void CarregarCampos()
    {
        Exigencia.ExcluirExigenciasAvulsas();
        Notificacao.ExcluirNotificacoesAvulsas();
        if (!Permissoes.UsuarioPossuiAlgumRelatorio(this.UsuarioLogado))
        {
            msg.CriarMensagem("O usuário não possui permissão de acesso a algum relatório", "Informação", MsgIcons.Informacao);
            return;
        }

        IList<ModuloPermissao> modulosUsuario = this.ObterModulosQueOUsuarioTemAcesso(UsuarioLogado.ConsultarPorId());

        this.CarregarArvoreRelatorios(Modelo.Menu.FiltrarRelatoriosDoUsuario(this.UsuarioLogado, modulosUsuario));
        this.CarregarCamposFiltros();
    }

    private IList<ModuloPermissao> ObterModulosQueOUsuarioTemAcesso(Usuario usuario)
    {
        IList<ModuloPermissao> retorno = new List<ModuloPermissao>();

        //Adicionando modulo geral
        ModuloPermissao moduloGeral = ModuloPermissao.ConsultarPorNome("Geral");
        if (Permissoes.UsuarioPossuiAcessoModuloGeral(usuario, moduloGeral))
            retorno.Add(moduloGeral);

        //Adicionando modulo DNPM
        ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");
        if (Permissoes.UsuarioPossuiAcessoModuloDNPM(usuario, moduloDNPM))
            retorno.Add(moduloDNPM);

        //Adicionando modulo meio ambiente
        ModuloPermissao moduloMeioAmbiente = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
        if (Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(usuario, moduloMeioAmbiente))
            retorno.Add(moduloMeioAmbiente);

        //Adicionando modulo contratos
        ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");
        if (Permissoes.UsuarioPossuiAcessoModuloContratos(usuario, moduloContratos))
            retorno.Add(moduloContratos);

        //Adicionando modulo diversos
        ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");
        if (Permissoes.UsuarioPossuiAcessoModuloDiversos(usuario, moduloDiversos))
            retorno.Add(moduloDiversos);

        return retorno;
    }

    private void CarregarCamposFiltros()
    {
        //Grupos Economicos
        this.CarregarAdministradores(ddlAdministradorGruposEconomicos);

        //Empresas
        this.CarregarGruposEconomicos(ddlGrupoEconomicoEmpresas);

        //Meio Ambiente
        this.CarregarGruposEconomicos(ddlGrupoEconomicoProcessosMeioAmbiente);
        this.CarregarEmpresas(ddlGrupoEconomicoProcessosMeioAmbiente, ddlEmpresaProcessosMeioAmbiente);

        //Processos
        this.CarregarOrgaosAmbientais(ddlOrgaoAmbientalProcessosMeioAmbiente);

        //Licenças Ambientais
        this.CarregarGruposEconomicos(ddlGrupoEconomicoLicencaAmbiental);
        this.CarregarEmpresas(ddlGrupoEconomicoLicencaAmbiental, ddlEmpresaLicencaAmbiental);
        this.CarregarTiposLicenca(ddlTipoLicencaAmbiental);
        this.CarregarOrgaosAmbientais(ddlOrgaoAmbientalLicencaAmbiental);
        this.CarregarEstados(ddlEstadoLicencaAmbiental);
        this.CarregarStatus();

        //Condicionantes
        this.CarregarGruposEconomicos(ddlGrupoEconomicoCondicionantes);
        this.CarregarEmpresas(ddlGrupoEconomicoCondicionantes, ddlEmpresaCondicionantes);
        this.CarregarOrgaosAmbientais(ddlOrgaosAmbientaisCondicionantes);
        this.CarregarEstados(ddlEstadoCondicionante);

        //Outros vencimentos
        this.CarregarGruposEconomicos(ddlGrupoEconomicoOutrosVencimentos);
        this.CarregarEmpresas(ddlGrupoEconomicoOutrosVencimentos, ddlEmpresaOutrosVencimentos);
        ddlTipoOutrosVencimentos.Items.Add(new ListItem("-- Todos --", "0"));
        ddlTipoOutrosVencimentos.Items.Add(new ListItem("Sem Processos", Condicional.VencimentoGeral.ToString()));
        ddlTipoOutrosVencimentos.Items.Add(new ListItem("Dentro de Processos", Condicional.VencimentoProcesso.ToString()));
        ddlTipoOutrosVencimentos.SelectedIndex = 0;

        //Processos DNPM
        this.CarregarGruposEconomicos(ddlGrupoEconomicoProcessoDNPM);
        this.CarregarEmpresas(ddlGrupoEconomicoProcessoDNPM, ddlEmpresaProcessoDNPM);
        this.CarregarRegimes();
        this.CarregarEstados(ddlEstadoProcessoDNPM);
        //Regime + Concessão de Lavra

        //Vencimentos por Período
        this.CarregarGruposEconomicos(ddlGrupoEconomicoVencimentos);
        this.CarregarEmpresas(ddlGrupoEconomicoVencimentos, ddlEmpresaVencimentos);
        this.CarregarTiposVencimentos();
        this.CarregarStatusVencimentos();
        this.CarregarEstados(ddlEstadoVencimentoPeriodo);

        //Rals
        this.CarregarGruposEconomicos(ddlGrupoEconomicoRal);
        this.CarregarEmpresas(ddlGrupoEconomicoRal, ddlEmpresaRal);

        //Guias de Utilização
        this.CarregarGruposEconomicos(ddlGrupoEconomicoGuiaUtilizacao);
        this.CarregarEmpresas(ddlGrupoEconomicoGuiaUtilizacao, ddlEmpresaGuiaUtilizacao);

        //Cadastro Tecnico Federal
        this.CarregarGruposEconomicos(ddlGrupoEconomicoCTF);
        this.CarregarEmpresas(ddlGrupoEconomicoCTF, ddlEmpresaCTF);

        //VencimentosDiversos
        this.CarregarGruposEconomicos(ddlGrupoEconomicoVencimentoDiverso);
        this.CarregarEmpresas(ddlGrupoEconomicoVencimentoDiverso, ddlEmpresaVencimentoDiverso);
        this.CarregarTiposDiversos(ddlTipoVencimentoDiverso);

        //Renúncias de álvaras de pesquisa
        this.CarregarGruposEconomicos(ddlGrupoRenuncias);
        this.CarregarEmpresas(ddlGrupoRenuncias, ddlEmpresaRenuncias);
        this.CarregarEstados(ddlEstadoRenuncias);

        //Clientes
        this.CarregarEstados(ddlEstadoCliente);
        this.CarregarAtividades(ddlAtividadeCliente);

        //Fornecedores
        this.CarregarEstados(ddlEstadoFornecedor);
        this.CarregarAtividades(ddlAtividadeFornecedor);

        //Contratos Diversos
        this.CarregarGruposEconomicos(ddlGrupoContratosDiversos);
        this.CarregarFornecedores(ddlFornecedorContratosDiversos);
        this.CarregarClientes(ddlClienteContratosDiversos);
        this.CarregarStatusContratosDiversos(ddlStatusContratosDiversos);
        this.CarregarCentrosCusto(ddlCentroCusto);
        this.CarregarIndicesFinanceiros(ddlIndiceContratosDiversos);
        this.CarregarSetores(ddlSetorContratosDiversos);
        this.CarregarFormasDePagamento(ddlFormaPagamentoContratoDiverso);

        //Contratos por Processos
        this.CarregarEmpresasContratosPorProcessos();
        this.CarregarStatusContratosDiversos(ddlStatusContratoProcessoPorContrato);
        this.CarregarTiposProcessosConformePermissoes();
        this.CarregarTiposProcessosAgrupandoPorContratos();
    }

    private void CarregarTiposProcessosAgrupandoPorContratos()
    {

        ddlTipoProcessoProcessosPorContrato.Items.Clear();
        ddlTipoProcessoProcessosPorContrato.Items.Add(new ListItem("-- Selecione --", "0"));
        
       IList<ModuloPermissao> modulos = ModuloPermissao.RecarregarModulos(UsuarioLogado.ConsultarPorId().ModulosPermissao);
            
       if (modulos != null && modulos.Count > 0 && modulos.Contains(ModuloPermissao.ConsultarPorNome("DNPM")))
            ddlTipoProcessoProcessosPorContrato.Items.Add(new ListItem("Processo Minerário", "1"));
        
       if (modulos != null && modulos.Count > 0 && modulos.Contains(ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
            ddlTipoProcessoProcessosPorContrato.Items.Add(new ListItem("Processo Ambiental", "2")); 
        
    }

    private void CarregarTiposProcessosConformePermissoes()
    {
        ddlTipoProcesso.Items.Clear();
        ddlTipoProcesso.Items.Add(new ListItem("-- Selecione --", "0"));

        IList<ModuloPermissao> modulos = ModuloPermissao.RecarregarModulos(UsuarioLogado.ConsultarPorId().ModulosPermissao);
          
        if (modulos != null && modulos.Count > 0 && modulos.Contains(ModuloPermissao.ConsultarPorNome("DNPM")))
             ddlTipoProcesso.Items.Add(new ListItem("Processo Minerário", "1"));

        if (modulos != null && modulos.Count > 0 && modulos.Contains(ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
             ddlTipoProcesso.Items.Add(new ListItem("Processo Ambiental", "2"));       
        
    }

    private void CarregarEmpresasContratosPorProcessos()
    {
        ddlEmpresaContratoPorProcesso.DataValueField = "Id";
        ddlEmpresaContratoPorProcesso.DataTextField = "Nome";
        ddlEmpresaContratoPorProcesso.DataSource = Empresa.ConsultarTodosOrdemAlfabetica();
        ddlEmpresaContratoPorProcesso.DataBind();
        ddlEmpresaContratoPorProcesso.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarFormasDePagamento(DropDownList dropFormaPagamento)
    {
        dropFormaPagamento.DataValueField = "Id";
        dropFormaPagamento.DataTextField = "Nome";
        dropFormaPagamento.DataSource = FormaRecebimento.ConsultarTodosOrdemAlfabetica();
        dropFormaPagamento.DataBind();
        dropFormaPagamento.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarAtividades(DropDownList dropAtividade)
    {
        dropAtividade.DataValueField = "Id";
        dropAtividade.DataTextField = "Nome";
        dropAtividade.DataSource = Atividade.ConsultarTodosOrdemAlfabetica();
        dropAtividade.DataBind();
        dropAtividade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarSetores(DropDownList dropSetor)
    {
        dropSetor.DataValueField = "Id";
        dropSetor.DataTextField = "Nome";
        IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);
        dropSetor.DataSource = setoresUsuario != null ? setoresUsuario : new List<Setor>();
        dropSetor.DataBind();
        dropSetor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarIndicesFinanceiros(DropDownList dropIndiceFinanceiro)
    {
        dropIndiceFinanceiro.DataValueField = "Id";
        dropIndiceFinanceiro.DataTextField = "Nome";
        dropIndiceFinanceiro.DataSource = IndiceFinanceiro.ConsultarTodos() != null ? IndiceFinanceiro.ConsultarTodos() : new List<IndiceFinanceiro>();
        dropIndiceFinanceiro.DataBind();
        dropIndiceFinanceiro.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarCentrosCusto(DropDownList dropCentroCusto)
    {
        dropCentroCusto.DataValueField = "Id";
        dropCentroCusto.DataTextField = "Nome";
        dropCentroCusto.DataSource = CentroCusto.ConsultarTodos() != null ? CentroCusto.ConsultarTodos() : new List<CentroCusto>();
        dropCentroCusto.DataBind();
        dropCentroCusto.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void CarregarClientes(DropDownList dropCliente)
    {
        dropCliente.DataValueField = "Id";
        dropCliente.DataTextField = "Nome";
        dropCliente.DataSource = Cliente.ConsultarTodosOrdemAlfabetica() != null ? Cliente.ConsultarTodosOrdemAlfabetica() : new List<Cliente>();
        dropCliente.DataBind();
        dropCliente.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarFornecedores(DropDownList dropFornecedor)
    {
        dropFornecedor.DataValueField = "Id";
        dropFornecedor.DataTextField = "Nome";
        dropFornecedor.DataSource = Fornecedor.ConsultarTodosOrdemAlfabetica() != null ? Fornecedor.ConsultarTodosOrdemAlfabetica() : new List<Fornecedor>();
        dropFornecedor.DataBind();
        dropFornecedor.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void CarregarRegimes()
    {
        ddlRegimeAtualProcessoDNPM.Items.Clear();
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("-- Todos --", ""));
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("Autorização de Pesquisa / Requerimento", "0"));
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("Autorização de Pesquisa / Alvará", "1"));
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("Concessão de Lavra / Requerimento", "2"));
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("Concessão de Lavra / Concessão", "3"));
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("Licenciamento", "Licenciamento"));
        ddlRegimeAtualProcessoDNPM.Items.Add(new ListItem("Extração", "Extração"));
        ddlRegimeAtualProcessoDNPM.SelectedIndex = 0;
    }

    private void CarregarTiposLicenca(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<TipoLicenca> tiposLicencas = TipoLicenca.ConsultarTodosOrdemAlfabetica();
        TipoLicenca auxTipoLicenca = new TipoLicenca();
        auxTipoLicenca.Nome = "-- Todos --";
        tiposLicencas.Insert(0, auxTipoLicenca);

        drop.DataSource = tiposLicencas;
        drop.DataBind();
        drop.SelectedIndex = 0;
    }

    private void CarregarAdministradores(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<Administrador> admins = Administrador.ConsultarTodosOrdemAlfabetica();
        Administrador aux = new Administrador();
        aux.Nome = "-- Todos --";
        admins.Insert(0, aux);

        drop.DataSource = admins;
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

    private void CarregarArvoreRelatorios(IList<Modelo.Menu> relatorios)
    {
        trvRelatorios.Nodes.Clear();
        TreeNode noPai = new TreeNode("Relatórios", "Raiz");        

        foreach (ModuloPermissao modulo in ModuloPermissao.ConsultarTodosOrdemPrioridade(this.UsuarioLogado))
        {
            
            TreeNode nodeModulo = new TreeNode(modulo.Nome, "Modulo_" + modulo.Id.ToString());

            //ADAPTAÇÃO PARA NAO TROCAR O DNPM EM PERMISSOES E TER QUE TROCAR TODAS AS TELAS
            if (modulo.Nome == "DNPM")
            {
                nodeModulo = new TreeNode("ANM", "Modulo_" + modulo.Id.ToString());
            }
            
            foreach (Modelo.Menu relatorio in relatorios)
            {
                if (modulo.Menus.Contains(relatorio))
                    nodeModulo.ChildNodes.Add(new TreeNode(relatorio.Nome, relatorio.UrlCadastro, "", relatorio.UrlPesquisa, "_blank"));
            }
            if (nodeModulo.ChildNodes != null && nodeModulo.ChildNodes.Count > 0)
                noPai.ChildNodes.Add(nodeModulo);
        }

        trvRelatorios.Nodes.Add(noPai);
        trvRelatorios.ExpandAll();
    }

    private void CarregarRelatorioSelecionado()
    {
        switch (mtvFiltros.ActiveViewIndex)
        {
            case 1: this.CarregarRelatorioGruposEconomicos();
                break;
            case 2: this.CarregarRelatorioOrgaosAmbientais();
                break;
            case 3: this.CarregarRelatorioEmpresas();
                break;
            case 4: this.CarregarRelatorioProcessosMeioAmbiente();
                break;
            case 5: this.CarregarRelatorioLicencasAmbientais();
                break;
            case 6: this.CarregarRelatorioCondicionantes();
                break;
            case 7: this.CarregarRelatorioOutrosVencimentos();
                break;
            case 8: this.CarregarRelatorioProcessosDNPM();
                break;
            case 9: this.CarregarVencimentosPorPeriodo();
                break;
            case 10: this.CarregarRelatorioRal();
                break;
            case 11: this.CarregarRelatorioGuiaUtilizacao();
                break;
            case 12: this.CarregarRelatorioCadastroTecnicoFederal();
                break;
            case 13: this.CarregarRelatorioPendenciasGruposEconomicos();
                break;
            case 14: this.CarregarRelatorioNotificacoesEnviadas();
                break;
            case 15: this.CarregarRelatorioVencimentosDiversos();
                break;
            case 16: this.CarregarRelatorioRenunciasAlvaras();
                break;
            case 17: this.CarregarRelatorioClientes();
                break;
            case 18: this.CarregarRelatorioFornecedores();
                break;
            case 19: this.CarregarRelatorioContratosDiversos();
                break;
            case 20: this.CarregarRelatorioContratosPorProcessos();
                break;
            default:
                msg.CriarMensagem("Selecione algum relatório", "Informação", MsgIcons.Informacao);
                break;
        }
    }

    private void CarregarEmpresas(DropDownList ddlGrupoEconomico, DropDownList ddlEmpresa)
    {
        ddlEmpresa.DataTextField = "Nome";
        ddlEmpresa.DataValueField = "Id";

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
        IList<Empresa> empresas = grupo != null && grupo.Empresas != null && grupo.Empresas.Count > 0 ? grupo.Empresas : new List<Empresa>();
        Empresa aux = new Empresa();
        aux.Nome = "-- Todas --";
        empresas.Insert(0, aux);
        ddlEmpresa.DataSource = empresas;
        ddlEmpresa.DataBind();
    }

    private void CarregarEmpresaVencimentoDiverso(DropDownList ddlGrupoEconomico)
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
        IList<Empresa> empresas = grupo.Empresas != null ? grupo.Empresas : new List<Empresa>();
        ddlEmpresaVencimentoDiverso.DataTextField = "Nome";
        ddlEmpresaVencimentoDiverso.DataValueField = "Id";
        ddlEmpresaVencimentoDiverso.DataSource = empresas;
        ddlEmpresaVencimentoDiverso.DataBind();
        ddlEmpresaVencimentoDiverso.Items.Insert(0, new ListItem("-- Todas --", "0"));

    }

    public void CarregarTiposVencimentos()
    {
        ddlTipoVencimentos.Items.Add(new ListItem("-- Todos ", "0"));

        IList<ModuloPermissao> modulosUsuario = ModuloPermissao.RecarregarModulos(UsuarioLogado.ConsultarPorId().ModulosPermissao);

        this.CarregarTiposVencimentosPorPermissoesModulos(modulosUsuario);
               
        ddlTipoVencimentos.SelectedIndex = 0;
    }

    private void CarregarTiposVencimentosPorPermissoesModulos(IList<ModuloPermissao> modulosUsuario)
    {
        if (modulosUsuario == null || modulosUsuario.Count == 0)
            return;

        if (modulosUsuario.Contains(ModuloPermissao.ConsultarPorNome("Meio Ambiente")))
        {
            ddlTipoVencimentos.Items.Add(new ListItem("Outros (Processo)", Vencimento.OUTROSPROCESSO));
            ddlTipoVencimentos.Items.Add(new ListItem("Outros (Empresa)", Vencimento.OUTROSEMPRESA));
            ddlTipoVencimentos.Items.Add(new ListItem("Condicionante", Vencimento.CONDICIONANTE));
            ddlTipoVencimentos.Items.Add(new ListItem("Licença", Vencimento.LICENCA));
            ddlTipoVencimentos.Items.Add(new ListItem("Entrega do Relatório Anual", Vencimento.ENTREGARELATORIOANUAL));
            ddlTipoVencimentos.Items.Add(new ListItem("Certificado de Regularidade", Vencimento.CERTIFICADOREGULARIDADE));
            ddlTipoVencimentos.Items.Add(new ListItem("Taxa trimestral", Vencimento.TAXATRIMESTRAL));
        }

        if (modulosUsuario.Contains(ModuloPermissao.ConsultarPorNome("DNPM"))) 
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

        if (modulosUsuario.Contains(ModuloPermissao.ConsultarPorNome("Diversos")))
        {
            ddlTipoVencimentos.Items.Add(new ListItem("Vencimento Diverso", Vencimento.VENCIMENTODIVERSO));
        }
        
    }

    private void CarregarTodosTiposVencimentos(){
        ddlTipoVencimentos.Items.Add(new ListItem("Outros (Processo)", Vencimento.OUTROSPROCESSO));
        ddlTipoVencimentos.Items.Add(new ListItem("Outros (Empresa)", Vencimento.OUTROSEMPRESA));
        ddlTipoVencimentos.Items.Add(new ListItem("Condicionante", Vencimento.CONDICIONANTE));
        ddlTipoVencimentos.Items.Add(new ListItem("Licença", Vencimento.LICENCA));
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
        ddlTipoVencimentos.Items.Add(new ListItem("Entrega do Relatório Anual", Vencimento.ENTREGARELATORIOANUAL));
        ddlTipoVencimentos.Items.Add(new ListItem("Certificado de Regularidade", Vencimento.CERTIFICADOREGULARIDADE));
        ddlTipoVencimentos.Items.Add(new ListItem("Taxa trimestral", Vencimento.TAXATRIMESTRAL));
        ddlTipoVencimentos.Items.Add(new ListItem("Vencimento Diverso", Vencimento.VENCIMENTODIVERSO));
    }

    private string GetDescricaoData(DateTime dataDe, DateTime dataAteh)
    {
        string retorno = "Todas";
        if (dataDe.CompareTo(SqlDate.MinValue) > 0 || dataAteh.CompareTo(SqlDate.MaxValue) < 0)
        {
            if (dataDe.CompareTo(SqlDate.MinValue) > 0 && dataAteh.CompareTo(SqlDate.MaxValue) < 0)
                retorno = "de " + dataDe.ToShortDateString() + " até " + dataAteh.ToShortDateString();
            else
                retorno = dataDe.CompareTo(SqlDate.MinValue) > 0 ? "após " + dataDe.ToShortDateString() : "antes de " + dataAteh.ToShortDateString();
        }
        return retorno;
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

    private void CarregarCidades(DropDownList dropEstado, DropDownList dropCidade)
    {
        Estado estado = Estado.ConsultarPorId(dropEstado.SelectedValue.ToInt32());
        dropCidade.DataValueField = "Id";
        dropCidade.DataTextField = "Nome";
        dropCidade.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        dropCidade.DataBind();
        dropCidade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    #region _____________________ Carregamenbto dos relatórios _________________________

    /// <summary>
    /// Grupos Econômicos
    /// </summary>
    private void CarregarRelatorioGruposEconomicos()
    {
        DateTime dataDe = tbxDataCadastroRelatorioGruposEconomicos.Text != string.Empty ? Convert.ToDateTime(tbxDataCadastroRelatorioGruposEconomicos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataCadastroAtehRelatorioGruposEconomicos.Text != string.Empty ? Convert.ToDateTime(tbxDataCadastroAtehRelatorioGruposEconomicos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        DateTime dataCancelamentoDe = SqlDate.MinValue;
        DateTime dataCancelamentoAte = SqlDate.MaxValue;
        IList<GrupoEconomico> grupos = GrupoEconomico.FiltrarGruposEconomicos(ddlAdministradorGruposEconomicos.SelectedValue.ToInt32(), dataDe.ToMinHourOfDay(), dataAteh.ToMaxHourOfDay(), dataCancelamentoDe.ToMinHourOfDay(), dataCancelamentoAte.ToMaxHourOfDay(), ddlPossuiUsuarios.SelectedValue.ToInt32(), 0, 0);

        Fontes.relatorioGruposEconomicosDataTable fonte = new Fontes.relatorioGruposEconomicosDataTable();
        string descricaoAdministrador = ddlAdministradorGruposEconomicos.SelectedIndex != 0 ? ddlAdministradorGruposEconomicos.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAteh);
        string descricaoPossuiUsuariosCadastrados = ddlPossuiUsuarios.SelectedItem.Text.Trim();
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoDataDeCancelamento = dataCancelamentoDe != SqlDate.MinValue || dataCancelamentoAte != SqlDate.MaxValue ? this.GetDescricaoData(dataCancelamentoDe, dataCancelamentoAte) : "Não definida";

        if (grupos.Count > 0)
        {
            IList<Usuario> usuarios = Usuario.ConsultarTodos();
            foreach (GrupoEconomico aux in grupos)
                fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Usuário não definido",
                    descricaoAdministrador,
                    descricaoDataDeReferência,
                    aux.Administrador != null ? aux.Administrador.Nome : "Não definido",
                    aux.Nome,
                    aux.DataCadastro.ToString("dd/MM/yyyy"),
                    grupos.Count, aux.Empresas.Count, aux.GetQuantidadeUsuariosDoGrupo(aux, usuarios), descricaoPossuiUsuariosCadastrados, URLLOgo, aux.DataCancelamento != SqlDate.MinValue ? aux.DataCancelamento.ToString("dd/MM/yyyy") : "", descricaoDataDeCancelamento, aux.Contato != null ? aux.Contato.Email : "");
        }
        else
        {
            fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Usuário não definido",
                    descricaoAdministrador,
                    descricaoDataDeReferência,
                    null,
                    null,
                    null,
                    0, null, null, descricaoPossuiUsuariosCadastrados, URLLOgo, null, descricaoDataDeCancelamento, null);
        }
        Relatorios.CarregarRelatorio("Grupos Econômicos", "GruposEconomicos", true, fonte);
    }

    /// <summary>
    /// Orgãos Ambientais
    /// </summary>
    private void CarregarRelatorioOrgaosAmbientais()
    {
        IList<OrgaoAmbiental> orgaos = OrgaoAmbiental.ConsultarTodosOrdemAlfabetica();
        Fontes.relatorioOrgaosAmbientaisDataTable fonte = new Fontes.relatorioOrgaosAmbientaisDataTable();
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        if (orgaos.Count > 0)
            foreach (OrgaoAmbiental orgao in orgaos)
            {
                fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido",
                    orgao.GetNomeTipo,
                    orgao.Nome,
                    orgao.GetCidadeEstado,
                    orgaos.Count, URLLOgo);
            }
        else
            fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido",
                null, null, null,
                0, URLLOgo);

        Relatorios.CarregarRelatorio("Orgãos Ambientais", "OrgaosAmbientais", true, fonte);
    }

    /// <summary>
    /// Empresas
    /// </summary>
    private void CarregarRelatorioEmpresas()
    {
        IList<Empresa> empresas = Empresa.FiltrarRelatorio(ddlGrupoEconomicoEmpresas.SelectedValue.ToInt32());
        Fontes.relatorioEmpresasDataTable fonte = new Fontes.relatorioEmpresasDataTable();
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        if (empresas.Count > 0)
            foreach (Empresa empresa in empresas)
            {
                fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido",
                    ddlGrupoEconomicoEmpresas.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoEmpresas.SelectedItem.Text.Trim(),
                    empresa.GrupoEconomico != null ? empresa.GrupoEconomico.Nome : "Não definido",
                    empresa.Nome,
                    empresa.DadosPessoa.GetType() == typeof(DadosJuridica) ? ((DadosJuridica)empresa.DadosPessoa).Cnpj : ((DadosFisica)empresa.DadosPessoa).Cpf,
                    empresas.Count, URLLOgo);
            }
        else
            fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido",
                  ddlGrupoEconomicoEmpresas.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoEmpresas.SelectedItem.Text.Trim(),
                  null, null, null,
                  0, URLLOgo);

        Relatorios.CarregarRelatorio("Empresas", "Empresas", true, fonte);

    }

    /// <summary>
    /// Processos de Meio Ambiente
    /// </summary>
    private void CarregarRelatorioProcessosMeioAmbiente()
    {
        //IList<Processo> processos = Processo.FiltrarRelatorio(ddlGrupoEconomicoProcessosMeioAmbiente.SelectedValue, ddlEmpresaProcessosMeioAmbiente.SelectedValue, ddlOrgaoAmbientalProcessosMeioAmbiente.SelectedValue);
        //Fontes.relatorioProcessosMeioAmbienteDataTable fonte = new Fontes.relatorioProcessosMeioAmbienteDataTable();
        //string descricaoGrupoEconomico = ddlGrupoEconomicoProcessosMeioAmbiente.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoProcessosMeioAmbiente.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaProcessosMeioAmbiente.SelectedIndex == 0 ? "Todos" : ddlEmpresaProcessosMeioAmbiente.SelectedItem.Text;
        //string descricaoOrgaoAmbiental = ddlOrgaoAmbientalProcessosMeioAmbiente.SelectedIndex == 0 ? "Todos" : ddlOrgaoAmbientalProcessosMeioAmbiente.SelectedItem.Text;
        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        //if (processos.Count > 0)
        //    foreach (Processo aux in processos)
        //        fonte.Rows.Add(descricaoUsuario,
        //            descricaoGrupoEconomico,
        //            descricaoEmpresa,
        //            descricaoOrgaoAmbiental,
        //            aux.Empresa != null && aux.Empresa.GrupoEconomico != null ? aux.Empresa.GrupoEconomico.Nome : "Não definido",
        //            aux.Empresa != null ? aux.Empresa.Nome : "Não definido",
        //            aux.Numero,
        //            aux.OrgaoAmbiental != null ? aux.OrgaoAmbiental.GetNomeTipo + " - " + aux.OrgaoAmbiental.Nome : "Não definido",
        //            processos.Count, URLLOgo);
        //else
        //    fonte.Rows.Add(descricaoUsuario,
        //        descricaoGrupoEconomico, descricaoEmpresa, descricaoOrgaoAmbiental,
        //        null, null, null, null,
        //        0, URLLOgo);

        //Relatorios.CarregarRelatorio("Processos de Meio Ambiente", "ProcessosMeioAmbiente", true, fonte);
    }

    /// <summary>
    /// Licenças Ambientais
    /// </summary>
    private void CarregarRelatorioLicencasAmbientais()
    {
        //DateTime dataDeValidade = tbxDataValidadeDeLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataValidadeDeLicencaAmbiental.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehValidade = tbxDataValidadeAtehLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataValidadeAtehLicencaAmbiental.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //DateTime dataDePrazoLimite = tbxDataLimiteLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataLimiteLicencaAmbiental.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehPrazoLimite = tbxDataLimiteAtehLicencaAmbiental.Text != string.Empty ? Convert.ToDateTime(tbxDataLimiteAtehLicencaAmbiental.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //IList<Licenca> licencas = Licenca.FiltrarRelatorio(ddlGrupoEconomicoLicencaAmbiental.SelectedValue.ToInt32(), ddlEmpresaLicencaAmbiental.SelectedValue.ToInt32(), ddlTipoLicencaAmbiental.SelectedValue.ToInt32(),
        //    dataDeValidade, dataAtehValidade, dataDePrazoLimite, dataAtehPrazoLimite,
        //    ddlOrgaoAmbientalLicencaAmbiental.SelectedValue, ddlEstadoLicencaAmbiental.SelectedValue.ToInt32());

        //Fontes.relatorioLicencasAmbientaisDataTable fonte = new Fontes.relatorioLicencasAmbientaisDataTable();
        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoLicencaAmbiental.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaLicencaAmbiental.SelectedIndex == 0 ? "Todas" : ddlEmpresaLicencaAmbiental.SelectedItem.Text;
        //string descricaoEstado = ddlEstadoLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlEstadoLicencaAmbiental.SelectedItem.Text;
        //string descricaoTipoLicenca = ddlTipoLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlTipoLicencaAmbiental.SelectedItem.Text;
        //string descricaoDataValidade = this.GetDescricaoData(dataDeValidade, dataAtehValidade);
        //string descricaoDataPrazoLimite = this.GetDescricaoData(dataDePrazoLimite, dataAtehPrazoLimite);
        //string descricaoOrgaoAmbiental = ddlOrgaoAmbientalLicencaAmbiental.SelectedIndex == 0 ? "Todos" : ddlOrgaoAmbientalLicencaAmbiental.SelectedItem.Text;
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        //if (licencas.Count > 0)
        //    foreach (Licenca aux in licencas)
        //        fonte.Rows.Add(descricaoUsuario,
        //            descricaoGrupoEconomico,
        //            descricaoEmpresa,
        //            descricaoTipoLicenca, descricaoDataValidade,
        //            descricaoDataPrazoLimite,
        //            descricaoOrgaoAmbiental,
        //            aux.Processo != null && aux.Processo.Empresa != null && aux.Processo.Empresa.GrupoEconomico != null ? aux.Processo.Empresa.GrupoEconomico.Nome : "Não definido",
        //            aux.Processo != null && aux.Processo.Empresa != null ? aux.Processo.Empresa.Nome : "Não definido",
        //            (aux.TipoLicenca != null ? aux.TipoLicenca.Sigla + " - " : string.Empty) + aux.Numero,
        //            aux.DataRetirada.ToShortDateString(),
        //            aux.DiasValidade,
        //            aux.GetUltimoVencimento.Data.ToShortDateString(),
        //            aux.PrazoLimiteRenovacao.ToShortDateString(),
        //            aux.Processo != null ? aux.Processo.Numero : "--",
        //            aux.Processo != null && aux.Processo.OrgaoAmbiental != null ? aux.Processo.OrgaoAmbiental.GetNomeTipo + " - " + aux.Processo.OrgaoAmbiental.Nome : "--",
        //            licencas.Count, URLLOgo, descricaoEstado, aux.Cidade != null && aux.Cidade.Estado != null ? aux.Cidade.Estado.PegarSiglaEstado() : " - ");
        //else
        //    fonte.Rows.Add(descricaoUsuario,
        //        descricaoGrupoEconomico,
        //        descricaoEmpresa,
        //        descricaoTipoLicenca,
        //        descricaoDataValidade,
        //        descricaoDataPrazoLimite,
        //        descricaoOrgaoAmbiental,
        //        null, null, null, null, null, null, null, null, null,
        //        0, URLLOgo, descricaoEstado, null);
        //Relatorios.CarregarRelatorio("Licenças Ambientais", "LicencasAmbientais", false, fonte);
    }

    /// <summary>
    /// Condicionantes
    /// </summary>
    private void CarregarRelatorioCondicionantes()
    {
        //DateTime dataDeVencimento = tbxDataVencimentoDeCondicionantes.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoDeCondicionantes.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehVencimento = tbxDataVencimentoAtehCondicionantes.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoAtehCondicionantes.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioCondicionantesDataTable fonte = new Fontes.relatorioCondicionantesDataTable();
        //IList<Condicionante> condicionantes = Condicionante.FiltrarRelatorio(
        //    ddlGrupoEconomicoCondicionantes.SelectedValue.ToInt32(), ddlEmpresaCondicionantes.SelectedValue.ToInt32(),
        //    dataDeVencimento, dataAtehVencimento,
        //    ddlOrgaosAmbientaisCondicionantes.SelectedValue.ToInt32(), ddlEstadoCondicionante.SelectedValue.ToInt32(), ddlStatusCondicionante.SelectedValue.ToInt32(), ddlCondicionantePeriodica.SelectedValue.ToInt32());

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoCondicionantes.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoCondicionantes.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaCondicionantes.SelectedIndex == 0 ? "Todas" : ddlEmpresaCondicionantes.SelectedItem.Text;
        //string descricaoEstado = ddlEstadoCondicionante.SelectedIndex == 0 ? "Todos" : ddlEstadoCondicionante.SelectedItem.Text;
        //string descricaoDataVencimento = this.GetDescricaoData(dataDeVencimento, dataAtehVencimento);
        //string descricaoOrgaoAmbiental = ddlOrgaosAmbientaisCondicionantes.SelectedIndex == 0 ? "Todos" : ddlOrgaosAmbientaisCondicionantes.SelectedItem.Text;
        //string descricaoStatus = ddlStatusCondicionante.SelectedIndex == 0 ? "Todos" : ddlStatusCondicionante.SelectedItem.Text;
        //string descricaoPeriodica = ddlCondicionantePeriodica.SelectedIndex == 0 ? "Todas" : ddlCondicionantePeriodica.SelectedItem.Text;
        //string descricaoPrrogacaoPrazo = ddlCondicionanteProrrogacaoPrazo.SelectedIndex == 0 ? "Todas" : ddlCondicionanteProrrogacaoPrazo.SelectedItem.Text;
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        //condicionantes = condicionantes.OrderBy(i => i.Numero).ToList();

        //if (ddlStatusCondicionante.SelectedIndex > 0)
        //    this.RemoverCondicionantesComStatusDiferentes(condicionantes, ddlStatusCondicionante.SelectedValue.ToInt32());

        //if (ddlCondicionanteProrrogacaoPrazo.SelectedIndex > 0)
        //    this.RemoverCondicionantesComOusSemProrrogacoesPrazo(condicionantes, ddlCondicionanteProrrogacaoPrazo.SelectedValue.ToInt32());

        //if (condicionantes.Count > 0)
        //{
        //    foreach (Condicionante aux in condicionantes)
        //        fonte.Rows.Add(descricaoUsuario,
        //            descricaoGrupoEconomico, descricaoEmpresa, descricaoDataVencimento, descricaoOrgaoAmbiental,
        //            aux.GetGrupoEconomico, aux.GetEmpresa, aux.Numero + " - " + aux.Descricao, aux.GetDescricaoLicenca,
        //            aux.GetUltimoVencimento.Id > 0 ? aux.GetUltimoVencimento.Data.ToShortDateString() : "--",
        //            aux.GetNumeroProcesso, aux.GetOrgaoAmbiental, condicionantes.Count, URLLOgo, descricaoEstado, aux.Licenca != null && aux.Licenca.Cidade != null && aux.Licenca.Cidade.Estado != null ? aux.Licenca.Cidade.Estado.PegarSiglaEstado() : " - ", descricaoStatus, aux.GetUltimoVencimento.Id > 0 && aux.GetUltimoVencimento.Status != null ? aux.GetUltimoVencimento.Status.Nome : "--", descricaoPeriodica, descricaoPrrogacaoPrazo, aux.GetUltimoVencimento != null && aux.GetUltimoVencimento.ProrrogacoesPrazo != null ? aux.GetUltimoVencimento.ProrrogacoesPrazo.Count : 0, aux.GetUltimoVencimento != null ? aux.GetUltimoVencimento.Periodico ? "Sim" : "Não" : "");

        //}
        //else
        //    fonte.Rows.Add(descricaoUsuario,
        //        descricaoGrupoEconomico, descricaoEmpresa, descricaoDataVencimento, descricaoOrgaoAmbiental,
        //        null, null, null, null, null, null, null,
        //        0, URLLOgo, descricaoEstado, null, descricaoStatus, null, descricaoPeriodica, descricaoPrrogacaoPrazo, null, null);

        //Relatorios.CarregarRelatorio("Condicionantes", "Condicionantes", false, fonte);
    }

    /// <summary>
    /// Outros Vencimentos
    /// </summary>
    private void CarregarRelatorioOutrosVencimentos()
    {
        //DateTime dataDeVencimento = tbxDataVencimentoDeOutrosVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoDeOutrosVencimentos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehVencimento = tbxDataVencimentoAtehOutrosVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoAtehOutrosVencimentos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioOutrosVencimentosDataTable fonte = new Fontes.relatorioOutrosVencimentosDataTable();
        //IList<Condicional> outrosVencimentos = new List<Condicional>();
        //outrosVencimentos.AddRange<Condicional>(OutrosEmpresa.FiltrarRelatorio(ddlGrupoEconomicoOutrosVencimentos.SelectedValue.ToInt32(),
        //    ddlEmpresaOutrosVencimentos.SelectedValue.ToInt32(),
        //    dataDeVencimento, dataAtehVencimento,
        //    ddlTipoOutrosVencimentos.SelectedValue.ToInt32(), ddlOutrosPeriodicos.SelectedValue.ToInt32()));

        //outrosVencimentos.AddRange<Condicional>(OutrosProcesso.FiltrarRelatorio(ddlGrupoEconomicoOutrosVencimentos.SelectedValue.ToInt32(),
        //    ddlEmpresaOutrosVencimentos.SelectedValue.ToInt32(),
        //    dataDeVencimento, dataAtehVencimento,
        //    ddlTipoOutrosVencimentos.SelectedValue.ToInt32(), ddlOutrosPeriodicos.SelectedValue.ToInt32()));

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoOutrosVencimentos.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoOutrosVencimentos.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaOutrosVencimentos.SelectedIndex == 0 ? "Todas" : ddlEmpresaOutrosVencimentos.SelectedItem.Text;
        //string descricaoDataVencimento = this.GetDescricaoData(dataDeVencimento, dataAtehVencimento);
        //string descricaoTipo = ddlTipoOutrosVencimentos.SelectedIndex == 0 ? "Todos" : ddlTipoOutrosVencimentos.SelectedItem.Text;
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        //string descricaoPeriodico = ddlOutrosPeriodicos.SelectedIndex == 0 ? "Todos" : ddlOutrosPeriodicos.SelectedItem.Text;
        //string descricaoProrrogacao = ddlOutrosProrrogacaoPrazo.SelectedIndex == 0 ? "Todos" : ddlOutrosProrrogacaoPrazo.SelectedItem.Text;

        //if (ddlOutrosProrrogacaoPrazo.SelectedValue.ToInt32() > 0)
        //    this.RemoverOutrosVencimentosComOuSemProrrogacoesPrazo(outrosVencimentos, ddlOutrosProrrogacaoPrazo.SelectedValue.ToInt32());


        //if (outrosVencimentos.Count > 0)
        //    foreach (Condicional aux in outrosVencimentos)
        //        fonte.Rows.Add(descricaoUsuario,
        //            descricaoGrupoEconomico, descricaoEmpresa, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataVencimento, descricaoTipo,
        //            aux.GetGrupoEconomico != null ? aux.GetGrupoEconomico.Nome : "Não definido",
        //            aux.GetEmpresa != null ? aux.GetEmpresa.Nome : "Não definida",
        //            aux.GetTipo,
        //            aux.Descricao,
        //            aux.GetUltimoVencimento.Id > 0 ? aux.GetUltimoVencimento.Data.ToShortDateString() : "--",
        //            aux.GetPeriodico ? "Sim" : "Não",
        //            aux.GetProcesso != null ? aux.GetProcesso.Numero : "--",
        //            aux.GetOrgaoAmbiental != null ? aux.GetOrgaoAmbiental.GetNomeTipo + " - " + aux.GetOrgaoAmbiental.Nome : "--",
        //            outrosVencimentos.Count, URLLOgo, descricaoPeriodico, descricaoProrrogacao, aux.GetUltimoVencimento != null && aux.GetUltimoVencimento.ProrrogacoesPrazo != null ? aux.GetUltimoVencimento.ProrrogacoesPrazo.Count : 0);
        //else

        //    fonte.Rows.Add(descricaoUsuario,
        //        descricaoGrupoEconomico, descricaoEmpresa, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataVencimento, descricaoTipo,
        //        null, null, null, null, null, null, null, null,
        //        0, URLLOgo, descricaoPeriodico, descricaoProrrogacao, null);

        //Relatorios.CarregarRelatorio("Outros Vencimentos Ambientais", "OutrosVencimentosAmbientais", false, fonte);
    }

    /// <summary>
    /// Processos DNPM
    /// </summary>
    private void CarregarRelatorioProcessosDNPM()
    {
        //Fontes.relatorioProcessosDNPMDataTable fonte = new Fontes.relatorioProcessosDNPMDataTable();
        //IList<ProcessoDNPM> processos = ProcessoDNPM.FiltrarRelatorio(
        //    ddlGrupoEconomicoProcessoDNPM.SelectedValue.ToInt32(),
        //    ddlEmpresaProcessoDNPM.SelectedValue.ToInt32(),
        //    ddlRegimeAtualProcessoDNPM.SelectedValue, ddlEstadoProcessoDNPM.SelectedValue.ToInt32());

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoProcessoDNPM.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoProcessoDNPM.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaProcessoDNPM.SelectedIndex == 0 ? "Todas" : ddlEmpresaProcessoDNPM.SelectedItem.Text;
        //string descricaoEstado = ddlEstadoProcessoDNPM.SelectedIndex == 0 ? "Todos" : ddlEstadoProcessoDNPM.SelectedItem.Text;
        //string descricaoRegimeAtual = ddlRegimeAtualProcessoDNPM.SelectedIndex == 0 ? "Todos" : ddlRegimeAtualProcessoDNPM.SelectedItem.Text.Trim();
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        //if (processos.Count > 0)
        //    foreach (ProcessoDNPM processo in processos)
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoRegimeAtual,
        //            processo.GetGrupoEconomico != null ? processo.GetGrupoEconomico.Nome : "Não definido",
        //            processo.Empresa != null ? processo.Empresa.Nome : "Não definida",
        //            processo.GetNumeroProcessoComMascara,
        //            processo.GetDescricaoRegimeAtual,
        //            processos.Count, URLLOgo, descricaoEstado, processo.Cidade != null && processo.Cidade.Estado != null ? processo.Cidade.Estado.PegarSiglaEstado() : " - ");
        //else
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoRegimeAtual,
        //        null, null, null, null, 0, URLLOgo, descricaoEstado, null);

        //Relatorios.CarregarRelatorio("Processos DNPM", "ProcessosDNPM", true, fonte);
    }

    /// <summary>
    /// Vencimentos por período
    /// </summary>
    private void CarregarVencimentosPorPeriodo()
    {
        //DateTime dataDePeriodo = tbxDataDePeriodoVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataDePeriodoVencimentos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehPeriodo = tbxDataAtehPeriodoVencimentos.Text != string.Empty ? Convert.ToDateTime(tbxDataAtehPeriodoVencimentos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioVencimentosPorPeriodoDataTable fonte = new Fontes.relatorioVencimentosPorPeriodoDataTable();
        //IList<Vencimento> vencimentos = Vencimento.FiltrarRelatorio(ddlGrupoEconomicoVencimentos.SelectedValue.ToInt32(), ddlEmpresaVencimentos.SelectedValue.ToInt32(), ddlTipoVencimentos.SelectedValue,
        //    dataDePeriodo, dataAtehPeriodo, ddlEstadoVencimentoPeriodo.SelectedValue.ToInt32(), ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso" ? 0 : ddlStatusVencimentoPorPeriodo.SelectedValue.ToInt32(), ddlPeriodicosVencimentoPeriodo.SelectedValue.ToInt32());

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoVencimentos.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoVencimentos.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaVencimentos.SelectedIndex == 0 ? "Todas" : ddlEmpresaVencimentos.SelectedItem.Text;
        //string descricaoEstado = ddlEstadoVencimentoPeriodo.SelectedIndex == 0 ? "Todos" : ddlEstadoVencimentoPeriodo.SelectedItem.Text;
        //string descricaoTipoVencimento = ddlTipoVencimentos.SelectedIndex == 0 ? "Todos" : ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso" ? ddlTipoVencimentoDiversoPeriodo.SelectedIndex == 0 ? "Vencimento Diverso" : ddlTipoVencimentoDiversoPeriodo.SelectedItem.Text : ddlTipoVencimentos.SelectedItem.Text;
        //string descricaoStatus = ddlStatusVencimentoPorPeriodo.SelectedIndex == 0 && ddlStatusVencimentoDiversoPeriodo.SelectedIndex <= 0 ? "Todos" : ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso" ? ddlStatusVencimentoDiversoPeriodo.SelectedIndex == 0 ? "Todos" : ddlStatusVencimentoDiversoPeriodo.SelectedItem.Text : ddlStatusVencimentoPorPeriodo.SelectedItem.Text;
        //string descricaoPeriodo = this.GetDescricaoData(dataDePeriodo, dataAtehPeriodo);
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        //string descricaoPeriodico = ddlPeriodicosVencimentoPeriodo.SelectedIndex == 0 ? "Todos" : ddlPeriodicosVencimentoPeriodo.SelectedItem.Text;
        //string descricaoProrrogacaoPrazo = ddlProrrogacaoPrazoVencimentoPeriodo.SelectedIndex == 0 ? "Todos" : ddlProrrogacaoPrazoVencimentoPeriodo.SelectedItem.Text;

        //IList<ModuloPermissao> modulosUsuario = ModuloPermissao.RecarregarModulos(UsuarioLogado.ConsultarPorId().ModulosPermissao);

        //if (vencimentos != null && vencimentos.Count > 0)
        //    vencimentos = this.FiltrarVencimentosPorModulos(vencimentos, modulosUsuario);

        //if (ddlProrrogacaoPrazoVencimentoPeriodo.SelectedValue.ToInt32() > 0)
        //    this.RemoverVencimentosPorPeriodoComOuSemProrrogacoesPrazo(vencimentos, ddlProrrogacaoPrazoVencimentoPeriodo.SelectedValue.ToInt32());

        //if (ddlTipoVencimentos.SelectedItem.Text == "Vencimento Diverso")
        //{
        //    this.RemoverVencimentosSimples(vencimentos);
        //}

        //this.RemoverVencimentosRepetidos(vencimentos);
        
        //if (vencimentos.Count > 0)
            
        //    foreach (Vencimento vencimento in vencimentos)
        //    { 
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoTipoVencimento == "-- Selecione --" ? "Todos" : descricaoTipoVencimento, descricaoPeriodo == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoPeriodo,
        //                vencimento.GetGrupoEconomico != null ? vencimento.GetGrupoEconomico.Nome : "Não definido",
        //                vencimento.GetEmpresa != null ? vencimento.GetEmpresa.Nome : "Não definido",
        //                vencimento.GetTipo, vencimento.Data.ToShortDateString(), vencimento.GetDescricaoTipo != null ? vencimento.GetDescricaoTipo : "" , vencimentos.Count, URLLOgo, descricaoStatus, vencimento.getDescricaoStatus != null ? vencimento.getDescricaoStatus : "", descricaoEstado, vencimento.GetEstado != null ? vencimento.GetEstado.PegarSiglaEstado() : " - ", descricaoPeriodico, descricaoProrrogacaoPrazo, vencimento.Periodico ? "Sim" : "Não", vencimento.ProrrogacoesPrazo != null ? vencimento.ProrrogacoesPrazo.Count : 0, vencimento.GetNumeroProcessoVencimento);                
        //    } 
        //else
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoTipoVencimento, descricaoPeriodo == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoPeriodo,
        //        null, null, null, null, null, 0, URLLOgo, descricaoStatus, null, descricaoEstado, null, descricaoPeriodico, descricaoProrrogacaoPrazo, null, null, null);

        //Relatorios.CarregarRelatorio("Vencimentos por período", "VencimentosPorPeriodo", false, fonte);
    }   

    private IList<Vencimento> FiltrarVencimentosPorModulos(IList<Vencimento> vencimentos, IList<ModuloPermissao> modulosUsuario)
    {
        IList<Vencimento> retorno = new List<Vencimento>();

        if (modulosUsuario != null && modulosUsuario.Count == 1 && modulosUsuario[0] == ModuloPermissao.ConsultarPorNome("Geral"))
            return new List<Vencimento>();

        foreach (Vencimento vencimento in vencimentos)
        {
            if (vencimento.GetModulo != null && modulosUsuario.Contains(vencimento.GetModulo))
                retorno.Add(vencimento);
        }

        return retorno;
        
    }

    /// <summary>
    /// Pendências Grupos Econômicos
    /// </summary>
    private void CarregarRelatorioPendenciasGruposEconomicos()
    {
        Fontes.relatorioPendenciasGruposEconomicosDataTable fonte = new Fontes.relatorioPendenciasGruposEconomicosDataTable();
        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        string caminhoLogo = WebUtil.GetURLImagemLogoRelatorio;

        if (grupos != null && grupos.Count > 0)
            foreach (GrupoEconomico grupo in grupos)
                fonte.Rows.Add(caminhoLogo, descricaoUsuario, grupo.Nome, grupo.AtivoLogus ? "Sim" : "Não", grupo.AtivoAmbientalis ? "Sim" : "Não", grupo.TermoAceito ? "Sim" : "Não", grupos.Count);
        else
            fonte.Rows.Add(caminhoLogo, descricaoUsuario, null, null, null, null, 0);

        Relatorios.CarregarRelatorio("Pendências de Ativação de Grupos Econômicos", "PendenciasAtivacaoGruposEconomicos", true, fonte);
    }

    /// <summary>
    /// Notificação Enviadas
    /// </summary>
    private void CarregarRelatorioNotificacoesEnviadas()
    {
        DateTime dataDePeriodo = tbxDataDeNotificacaoEnviada.Text != string.Empty ? Convert.ToDateTime(tbxDataDeNotificacaoEnviada.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehPeriodo = tbxDataAtehNotificacaoEnviada.Text != string.Empty ? Convert.ToDateTime(tbxDataAtehNotificacaoEnviada.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        Fontes.relatorioNotificacoesEnviadasDataTable fonte = new Fontes.relatorioNotificacoesEnviadasDataTable();
        IList<Notificacao> notificacoes = Notificacao.FiltrarNotificacoes(dataDePeriodo, dataAtehPeriodo);
        string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        string descricaoPeriodo = this.GetDescricaoData(dataDePeriodo, dataAtehPeriodo);
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoNotificacao = string.Empty;
        if (notificacoes.Count > 0)
            foreach (Notificacao aux in notificacoes)
            {
                descricaoNotificacao = "Notificação" + (aux.Vencimento.GetTipo.IsNotNullOrEmpty() ? " do Vencimento de " + aux.Vencimento.GetTipo : string.Empty) + (aux.GetEmpresa != null ? " da Empresa " + aux.GetEmpresa.Nome : string.Empty);
                fonte.Rows.Add(URLLOgo, descricaoUsuario, descricaoPeriodo, descricaoNotificacao, aux.Data.ToShortDateString(), notificacoes.Count);
            }
        else
            fonte.Rows.Add(URLLOgo, descricaoUsuario, descricaoPeriodo, null, null, 0);

        Relatorios.CarregarRelatorio("Notificações Enviadas", "NotificacoesEnviadas", true, fonte);
    }

    //RALS
    private void CarregarRelatorioRal()
    {
        //DateTime dataDePeriodo = tbxDataVencimentoRalDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoRalDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehPeriodo = tbxDataVencimentoRalAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoRalAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioRALDataTable fonte = new Fontes.relatorioRALDataTable();
        //IList<RAL> rals = RAL.FiltrarRelatorio(ddlGrupoEconomicoRal.SelectedValue.ToInt32(), ddlEmpresaRal.SelectedValue.ToInt32(), dataDePeriodo, dataAtehPeriodo);

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoRal.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoRal.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaRal.SelectedIndex == 0 ? "Todas" : ddlEmpresaRal.SelectedItem.Text;
        //string descricaoPeriodo = this.GetDescricaoData(dataDePeriodo, dataAtehPeriodo);
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        //if (rals.Count > 0)
        //{
        //    foreach (RAL ral in rals)
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoPeriodo,
        //            ral.ProcessoDNPM.Empresa.GrupoEconomico != null ? ral.ProcessoDNPM.Empresa.GrupoEconomico.Nome : "Não definido",
        //            ral.ProcessoDNPM.Empresa != null ? ral.ProcessoDNPM.Empresa.Nome : "Não definido",
        //            ral.ProcessoDNPM != null ? ral.ProcessoDNPM.GetNumeroProcessoComMascara : "Não definido",
        //            ral.GetUltimoVencimento.Data.ToShortDateString(), rals.Count, URLLOgo);
        //    }
        //}
        //else
        //{
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoPeriodo, null, null, null, null, 0, URLLOgo);
        //}
        //Relatorios.CarregarRelatorio("Relatórios Anuais de Lavra - RAL", "Rals", false, fonte);
    }

    //Guias de Utilização
    private void CarregarRelatorioGuiaUtilizacao()
    {
        //DateTime dataDePeriodo = tbxDataVencimentoDeGuiaUtilizacao.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoDeGuiaUtilizacao.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAtehPeriodo = tbxDataVencimentoAteGuiaUtilizacao.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoAteGuiaUtilizacao.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioGuiaUtilizacaoDataTable fonte = new Fontes.relatorioGuiaUtilizacaoDataTable();
        //IList<GuiaUtilizacao> guias = GuiaUtilizacao.FiltrarRelatorio(ddlGrupoEconomicoGuiaUtilizacao.SelectedValue.ToInt32(), ddlEmpresaGuiaUtilizacao.SelectedValue.ToInt32(), dataDePeriodo, dataAtehPeriodo);

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoGuiaUtilizacao.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoGuiaUtilizacao.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaGuiaUtilizacao.SelectedIndex == 0 ? "Todas" : ddlEmpresaGuiaUtilizacao.SelectedItem.Text;
        //string descricaoPeriodo = this.GetDescricaoData(dataDePeriodo, dataAtehPeriodo);
        //string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        //if (guias.Count > 0)
        //{
        //    foreach (GuiaUtilizacao guia in guias)
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoPeriodo,
        //            guia.ProcessoDNPM.Empresa.GrupoEconomico != null ? guia.ProcessoDNPM.Empresa.GrupoEconomico.Nome : "Não definido",
        //            guia.ProcessoDNPM.Empresa != null ? guia.ProcessoDNPM.Empresa.Nome : "Não definido",
        //            guia.ProcessoDNPM != null ? guia.ProcessoDNPM.GetNumeroProcessoComMascara : "Não definido",
        //            guia.GetUltimoVencimento.Data.ToShortDateString(), guias.Count, URLLOgo);
        //    }
        //}
        //else
        //{
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoPeriodo, null, null, null, null, 0, URLLOgo);
        //}
        //Relatorios.CarregarRelatorio("Guias de Utilização", "GuiasDeUtilizacao", false, fonte);
    }

    //CadastrosTecnicos Federais
    private void CarregarRelatorioCadastroTecnicoFederal()
    {
        //DateTime dataDeEntregaRelatorioAnual = tbxDataEntregaRelatorioAnualDe.Text != string.Empty ? Convert.ToDateTime(tbxDataEntregaRelatorioAnualDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAteEntregaRelatorioAnual = tbxDataEntregaRelatorioAnualAte.Text != string.Empty ? Convert.ToDateTime(tbxDataEntregaRelatorioAnualAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        //DateTime dataDeVencimentoTaxaTrimestral = tbxDataVencimentoTaxaTrimestralDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoTaxaTrimestralDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAteVencimentoTaxaTrimestral = tbxDataVencimentoTaxaTrimestralAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoTaxaTrimestralAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        //DateTime dataDeVencimentoCertificadoRegularidade = tbxDataVencimentoCertificadoRegularidadeDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoCertificadoRegularidadeDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAteVencimentoCertificadoRegularidade = tbxDataVencimentoCertificadoRegularidadeAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoCertificadoRegularidadeAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioCadastroTecnicoFederalDataTable fonte = new Fontes.relatorioCadastroTecnicoFederalDataTable();
        //IList<CadastroTecnicoFederal> cadastros = CadastroTecnicoFederal.FiltrarRelatorio(ddlGrupoEconomicoCTF.SelectedValue.ToInt32(), ddlEmpresaCTF.SelectedValue.ToInt32(),
        //    dataDeEntregaRelatorioAnual, dataAteEntregaRelatorioAnual, dataDeVencimentoTaxaTrimestral, dataAteVencimentoTaxaTrimestral, dataDeVencimentoCertificadoRegularidade, dataAteVencimentoCertificadoRegularidade);

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoCTF.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoCTF.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaCTF.SelectedIndex == 0 ? "Todas" : ddlEmpresaCTF.SelectedItem.Text;
        //string descricaoPeriodoEntregaRelatorio = this.GetDescricaoData(dataDeEntregaRelatorioAnual, dataAteEntregaRelatorioAnual);
        //string descricaoPeriodoTaxaTrimestral = this.GetDescricaoData(dataDeVencimentoTaxaTrimestral, dataAteVencimentoTaxaTrimestral);
        //string descricaoPeriodoCertificadoRegularidade = this.GetDescricaoData(dataDeVencimentoCertificadoRegularidade, dataAteVencimentoCertificadoRegularidade);
        //string logomarca = WebUtil.GetURLImagemLogoRelatorio;

        //if (cadastros.Count > 0)
        //{
        //    foreach (CadastroTecnicoFederal cadastro in cadastros)
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoPeriodoEntregaRelatorio, descricaoPeriodoTaxaTrimestral, descricaoPeriodoCertificadoRegularidade,
        //            cadastro.Empresa.GrupoEconomico != null ? cadastro.Empresa.GrupoEconomico.Nome : "Não definido",
        //            cadastro.Empresa != null ? cadastro.Empresa.Nome : "Não definido",
        //            cadastro.GetUltimoRelatorio.GetDataVencimento,
        //            cadastro.GetUltimoPagamento.GetDataVencimento,
        //            cadastro.GetUltimoCertificado.GetDataVencimento, cadastros.Count, logomarca);
        //    }
        //}
        //else
        //{
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoPeriodoEntregaRelatorio, descricaoPeriodoTaxaTrimestral, descricaoPeriodoCertificadoRegularidade, null, null, null, null, null, 0, logomarca);
        //}
        //Relatorios.CarregarRelatorio("Cadastros Técnicos Federais - CTF", "CadastrosTecnicosFederais", false, fonte);
    }

    //Vencimentos Diversos
    private void CarregarRelatorioVencimentosDiversos()
    {
        //DateTime dataDeVencimentoDiverso = tbxDataDeVencimentoDiverso.Text != string.Empty ? Convert.ToDateTime(tbxDataDeVencimentoDiverso.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAteVencimentoDiverso = tbxDataAteVencimentoDiverso.Text != string.Empty ? Convert.ToDateTime(tbxDataAteVencimentoDiverso.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        //StatusDiverso status = StatusDiverso.ConsultarPorId(ddlStatusVencimentoDiverso.SelectedValue.ToInt32());
        //VencimentoDiverso vencimento = new VencimentoDiverso();

        //Fontes.relatorioVencimentosDiversosDataTable fonte = new Fontes.relatorioVencimentosDiversosDataTable();
        //IList<Diverso> diversos = Diverso.FiltrarRelatorio(ddlGrupoEconomicoVencimentoDiverso.SelectedValue.ToInt32(), ddlEmpresaVencimentoDiverso.SelectedValue.ToInt32(), tbxDescricaoVencimentoDiverso.Text,
        //    TipoDiverso.ConsultarPorId(ddlTipoVencimentoDiverso.SelectedValue.ToInt32()), status, dataDeVencimentoDiverso, dataAteVencimentoDiverso, ddlPeriodicosVencimentosDiverso.SelectedValue.ToInt32());

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoEconomicoVencimentoDiverso.SelectedIndex == 0 ? "Todos" : ddlGrupoEconomicoVencimentoDiverso.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaVencimentoDiverso.SelectedIndex == 0 ? "Todas" : ddlEmpresaVencimentoDiverso.SelectedItem.Text;
        //string filtroDescricao = tbxDescricaoVencimentoDiverso.Text.IsNotNullOrEmpty() ? tbxDescricaoVencimentoDiverso.Text : "Não definido";
        //string descricaoDataVencimento = this.GetDescricaoData(dataDeVencimentoDiverso, dataAteVencimentoDiverso);
        //string descricaoTipo = ddlTipoVencimentoDiverso.SelectedIndex == 0 ? "Todos" : ddlTipoVencimentoDiverso.SelectedItem.Text;
        //string descricaoStatus = ddlTipoVencimentoDiverso.SelectedIndex != 0 && ddlStatusVencimentoDiverso.SelectedIndex == 0 || ddlStatusVencimentoDiverso.SelectedIndex == -1 ? "Todos" : ddlStatusVencimentoDiverso.SelectedItem.Text;
        //string logomarca = WebUtil.GetURLImagemLogoRelatorio;
        //string descricaoPeriodico = ddlPeriodicosVencimentosDiverso.SelectedIndex == 0 ? "Todos" : ddlPeriodicosVencimentosDiverso.SelectedItem.Text;

        //if (diversos != null && diversos.Count > 0)
        //{
        //    if (status != null && status.Id > 0)
        //    {
        //        for (int i = diversos.Count - 1; i > -1; i--)
        //        {
        //            vencimento = diversos[i].GetUltimoVencimento;

        //            if (vencimento != null && vencimento.StatusDiverso.Id != status.Id)
        //            {
        //                diversos.Remove(diversos[i]);
        //            }
        //        }
        //    }

        //    foreach (Diverso diverso in diversos)
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, filtroDescricao, descricaoTipo, descricaoStatus, diverso.Empresa.GrupoEconomico.Nome,
        //            diverso.Empresa.Nome, diverso.Descricao, diverso.TipoDiverso.Nome, diverso.GetUltimoVencimento.StatusDiverso.Nome, diverso.GetUltimoVencimento.Periodico ? "Sim" : "Não",
        //            diverso.GetUltimoVencimento.Data.ToShortDateString(), diversos.Count, logomarca, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataVencimento, descricaoPeriodico);
        //    }
        //}
        //else
        //{
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, filtroDescricao, descricaoTipo, descricaoStatus, null, null, null, null, null, null, null, 0, logomarca, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataVencimento, descricaoPeriodico);
        //}
        //Relatorios.CarregarRelatorio("Vencimentos Diversos", "VencimentosDiversos", false, fonte);
    }

    //Renúncias de Alvaras
    private void CarregarRelatorioRenunciasAlvaras()
    {
        //DateTime dataDeVencimentoRenuncia = tbxDataVencimentoRenunciaDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoRenunciaDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataAteVencimentoRenuncia = tbxDataVencimentoRenunciaAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoRenunciaAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioRenunciasAlvarasDataTable fonte = new Fontes.relatorioRenunciasAlvarasDataTable();

        //IList<AlvaraPesquisa> alvaras = AlvaraPesquisa.FiltrarRelatorio(ddlGrupoRenuncias.SelectedValue.ToInt32(), ddlEmpresaRenuncias.SelectedValue.ToInt32(), dataDeVencimentoRenuncia,
        //    dataAteVencimentoRenuncia, ddlEstadoRenuncias.SelectedValue.ToInt32());

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoRenuncias.SelectedIndex == 0 ? "Todos" : ddlGrupoRenuncias.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaRenuncias.SelectedIndex == 0 ? "Todas" : ddlEmpresaRenuncias.SelectedItem.Text;
        //string descricaoDataVencimento = this.GetDescricaoData(dataDeVencimentoRenuncia, dataAteVencimentoRenuncia);
        //string descricaoEstado = ddlEstadoRenuncias.SelectedIndex == 0 ? "Todos" : ddlEstadoRenuncias.SelectedItem.Text;
        //string logomarca = WebUtil.GetURLImagemLogoRelatorio;

        //if (alvaras != null && alvaras.Count > 0)
        //{
        //    foreach (AlvaraPesquisa alvara in alvaras)
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definido" : descricaoDataVencimento, alvara.ProcessoDNPM.Empresa.GrupoEconomico.Nome,
        //            alvara.ProcessoDNPM.Empresa.Nome, alvara.ProcessoDNPM.Numero, alvara.Numero, alvara.LimiteRenuncia.Data.ToShortDateString(),
        //            alvara.ProcessoDNPM.Cidade != null && alvara.ProcessoDNPM.Cidade.Estado != null ? alvara.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ",
        //            alvaras.Count, logomarca, descricaoEstado);
        //    }
        //}
        //else
        //{
        //    fonte.Rows.Add(descricaoUsuario, descricaoGrupoEconomico, descricaoEmpresa, descricaoDataVencimento, null, null, null, null, null, null, 0, logomarca, descricaoEstado);
        //}

        //Relatorios.CarregarRelatorio("Renúncias de Álvaras de Pesquisa", "RenunciasAlvarasPesquisa", true, fonte);
    }

    //Clientes
    private void CarregarRelatorioClientes()
    {

        Fontes.relatorioClientesDataTable fonte = new Fontes.relatorioClientesDataTable();

        IList<Cliente> clientes = Cliente.FiltrarRelatorio(tbxNomeRazaoCliente.Text, tbxCnpjCpfCliente.Text, ddlStatusCliente.SelectedValue.ToInt32(), ddlEstadoCliente.SelectedValue.ToInt32(), ddlCidadeCliente.SelectedValue.ToInt32(), ddlAtividadeCliente.SelectedValue.ToInt32());

        string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        string descricaoNomeRazao = tbxNomeRazaoCliente.Text.IsNotNullOrEmpty() ? tbxNomeRazaoCliente.Text : "Não definido";
        string descricaoCnpjCpf = tbxCnpjCpfCliente.Text.IsNotNullOrEmpty() ? tbxCnpjCpfCliente.Text : "Não definido";
        string descriStatus = ddlStatusCliente.SelectedValue == "0" ? "Todos" : ddlStatusCliente.SelectedItem.Text;
        string descricaoEstado = ddlEstadoCliente.SelectedIndex == 0 ? "Todos" : ddlEstadoCliente.SelectedItem.Text;
        string descricaoCidade = ddlCidadeCliente.SelectedIndex <= 0 ? "Todos" : ddlCidadeCliente.SelectedItem.Text;
        string logomarca = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoAtividade = ddlAtividadeCliente.SelectedIndex == 0 ? "Todas" : ddlAtividadeCliente.SelectedItem.Text;

        if (clientes != null && clientes.Count > 0)
        {
            foreach (Cliente cliente in clientes)
            {
                fonte.Rows.Add(descricaoUsuario, descricaoNomeRazao, descricaoCnpjCpf, descriStatus, descricaoEstado,
                    descricaoCidade, cliente.DadosPessoa.GetType() == typeof(DadosFisica) ? cliente.Nome : ((DadosJuridica)cliente.DadosPessoa).RazaoSocial, cliente.GetNumeroCNPJeCPFComMascara, cliente.Ativo ? "ATIVO" : "INATIVO", cliente.Endereco != null && cliente.Endereco.Cidade != null ? cliente.Endereco.Cidade.Nome : "", cliente.Endereco != null && cliente.Endereco.Cidade != null && cliente.Endereco.Cidade.Estado != null ? cliente.Endereco.Cidade.Estado.PegarSiglaEstado() : "",
                    clientes.Count, logomarca, descricaoAtividade, cliente.Atividade != null ? cliente.Atividade.Nome : "");
            }
        }
        else
        {
            fonte.Rows.Add(descricaoUsuario, descricaoNomeRazao, descricaoCnpjCpf, descriStatus, descricaoEstado, descricaoCidade, null, null, null, null, null,
                    0, logomarca, descricaoAtividade, null);
        }

        Relatorios.CarregarRelatorio("Clientes", "Clientes", false, fonte);
    }

    //Fornecedores
    private void CarregarRelatorioFornecedores()
    {

        Fontes.relatorioFornecedoresDataTable fonte = new Fontes.relatorioFornecedoresDataTable();

        IList<Fornecedor> fornecedores = Fornecedor.FiltrarRelatorio(tbxNomeRazaoFornecedor.Text, tbxCnpjCpfFornecedor.Text, ddlStatusFornecedor.SelectedValue.ToInt32(), ddlEstadoFornecedor.SelectedValue.ToInt32(), ddlCidadeFornecedor.SelectedValue.ToInt32(), ddlAtividadeFornecedor.SelectedValue.ToInt32());

        string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        string descricaoNomeRazao = tbxNomeRazaoFornecedor.Text.IsNotNullOrEmpty() ? tbxNomeRazaoFornecedor.Text : "Não definido";
        string descricaoCnpjCpf = tbxCnpjCpfFornecedor.Text.IsNotNullOrEmpty() ? tbxCnpjCpfFornecedor.Text : "Não definido";
        string descriStatus = ddlStatusFornecedor.SelectedValue == "0" ? "Todos" : ddlStatusFornecedor.SelectedItem.Text;
        string descricaoEstado = ddlEstadoFornecedor.SelectedIndex == 0 ? "Todos" : ddlEstadoFornecedor.SelectedItem.Text;
        string descricaoCidade = ddlCidadeFornecedor.SelectedIndex <= 0 ? "Todos" : ddlCidadeFornecedor.SelectedItem.Text;
        string logomarca = WebUtil.GetURLImagemLogoRelatorio;
        string descricaoAtividade = ddlAtividadeFornecedor.SelectedIndex == 0 ? "Todas" : ddlAtividadeFornecedor.SelectedItem.Text;

        if (fornecedores != null && fornecedores.Count > 0)
        {
            foreach (Fornecedor fornecedor in fornecedores)
            {
                fonte.Rows.Add(descricaoUsuario, descricaoNomeRazao, descricaoCnpjCpf, descriStatus, descricaoEstado,
                    descricaoCidade, fornecedor.DadosPessoa.GetType() == typeof(DadosFisica) ? fornecedor.Nome : ((DadosJuridica)fornecedor.DadosPessoa).RazaoSocial, fornecedor.GetNumeroCNPJeCPFComMascara, fornecedor.Ativo ? "ATIVO" : "INATIVO", fornecedor.Endereco != null && fornecedor.Endereco.Cidade != null ? fornecedor.Endereco.Cidade.Nome : "", fornecedor.Endereco != null && fornecedor.Endereco.Cidade != null && fornecedor.Endereco.Cidade.Estado != null ? fornecedor.Endereco.Cidade.Estado.PegarSiglaEstado() : "",
                    fornecedores.Count, logomarca, descricaoAtividade, fornecedor.Atividade != null ? fornecedor.Atividade.Nome : "");
            }
        }
        else
        {
            fonte.Rows.Add(descricaoUsuario, descricaoNomeRazao, descricaoCnpjCpf, descriStatus, descricaoEstado, descricaoCidade, null, null, null, null, null,
                    0, logomarca, descricaoAtividade, null);
        }

        Relatorios.CarregarRelatorio("Fornecedores", "Fornecedores", false, fonte);
    }

    //ContratosDiversos
    private void CarregarRelatorioContratosDiversos()
    {
        //DateTime dataVencimentoDe = tbxDataVencimentoContratoDiversoDe.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoContratoDiversoDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataVencimentoAte = tbxDataVencimentoContratoDiversoAte.Text != string.Empty ? Convert.ToDateTime(tbxDataVencimentoContratoDiversoAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        //DateTime dataReajusteDe = tbxDataReajusteDe.Text != string.Empty ? Convert.ToDateTime(tbxDataReajusteDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        //DateTime dataReajusteAte = tbxDataReajusteAte.Text != string.Empty ? Convert.ToDateTime(tbxDataReajusteAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        //Fontes.relatorioContratosDiversosDataTable fonte = new Fontes.relatorioContratosDiversosDataTable();

        //IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);

        //IList<ContratoDiverso> contratos = ContratoDiverso.FiltrarRelatorio(ddlGrupoContratosDiversos.SelectedValue.ToInt32(), ddlEmpresaContratosDiversos.SelectedValue.ToInt32(), ddlComoContratosDiversos.SelectedValue.ToInt32(), ddlFornecedorContratosDiversos.SelectedValue.ToInt32(), ddlClienteContratosDiversos.SelectedValue.ToInt32(), ddlStatusContratosDiversos.SelectedValue.ToInt32(), dataVencimentoDe, dataVencimentoAte, dataReajusteDe, dataReajusteAte, ddlCentroCusto.SelectedValue.ToInt32(), ddlIndiceContratosDiversos.SelectedValue.ToInt32(), ddlSetorContratosDiversos.SelectedValue.ToInt32(), ddlFormaPagamentoContratoDiverso.SelectedValue.ToInt32(), setoresUsuario, UsuarioLogado.ConsultarPorId());

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string descricaoGrupoEconomico = ddlGrupoContratosDiversos.SelectedIndex == 0 ? "Todos" : ddlGrupoContratosDiversos.SelectedItem.Text;
        //string descricaoEmpresa = ddlEmpresaContratosDiversos.SelectedIndex > 0 ? ddlEmpresaContratosDiversos.SelectedItem.Text : "Todas";
        //string filtroTituloComo = ddlComoContratosDiversos.SelectedIndex == 0 ? "Fornecedor/Cliente" : ddlComoContratosDiversos.SelectedIndex == 1 ? "Fornecedor(Contratada):" : "Cliente(Contrante):";
        //string filtroComo = ddlComoContratosDiversos.SelectedIndex == 0 ? "Não definido" : ddlComoContratosDiversos.SelectedItem.Text;
        //string filtroFornecedorCliente = ddlComoContratosDiversos.SelectedIndex > 0 ? ddlComoContratosDiversos.SelectedValue == "1" ? ddlFornecedorContratosDiversos.SelectedValue.ToInt32() > 0 ? Fornecedor.ConsultarPorId(ddlFornecedorContratosDiversos.SelectedValue.ToInt32()) != null ? Fornecedor.ConsultarPorId(ddlFornecedorContratosDiversos.SelectedValue.ToInt32()).Nome : "Não definido" : "Todos" : ddlClienteContratosDiversos.SelectedValue.ToInt32() > 0 ? Cliente.ConsultarPorId(ddlClienteContratosDiversos.SelectedValue.ToInt32()) != null ? Cliente.ConsultarPorId(ddlClienteContratosDiversos.SelectedValue.ToInt32()).Nome : "Não definido" : "Todos" : "Não definido";  //verifica se o filtro de pesquisa é um cliente ou um fornecedor e preenche a string com o nome do cliente ou fornecedor selecionado de acordo com o filtro escolhido
        //string descricaoStatus = ddlStatusContratosDiversos.SelectedIndex > 0 ? ddlStatusContratosDiversos.SelectedItem.Text : "Todos";
        //string descricaoDataVencimento = this.GetDescricaoData(dataVencimentoDe, dataVencimentoAte);
        //string descricaoDataReajuste = this.GetDescricaoData(dataReajusteDe, dataReajusteAte);
        //string descricaoCentroCusto = ddlCentroCusto.SelectedIndex == 0 ? "Todos" : ddlCentroCusto.SelectedItem.Text;
        //string descricaoIndiceFinanceiro = ddlIndiceContratosDiversos.SelectedIndex == 0 ? "Todos" : ddlIndiceContratosDiversos.SelectedItem.Text;
        //string descricaoSetor = ddlSetorContratosDiversos.SelectedIndex == 0 ? "Todos" : ddlSetorContratosDiversos.SelectedItem.Text;
        //string logomarca = WebUtil.GetURLImagemLogoRelatorio;
        //string descricaoFormaPagamento = ddlFormaPagamentoContratoDiverso.SelectedIndex == 0 ? "Todas" : ddlFormaPagamentoContratoDiverso.SelectedItem.Text;
        //string colunaFornecedorCliente = ddlComoContratosDiversos.SelectedIndex == 0 ? "Fornecedor/Cliente" : ddlComoContratosDiversos.SelectedIndex == 1 ? "Fornecedor(Contratada)" : "Cliente(Contrante)";

        //if (contratos != null && contratos.Count > 0)
        //{
        //    foreach (ContratoDiverso contrato in contratos)
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoEmpresa, filtroComo, filtroFornecedorCliente, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataVencimento,
        //            descricaoStatus, descricaoCentroCusto, descricaoIndiceFinanceiro, descricaoSetor, descricaoGrupoEconomico, contrato.Empresa != null && contrato.Empresa.GrupoEconomico != null ? contrato.Empresa.GrupoEconomico.Nome : "",
        //            contrato.Empresa != null ? contrato.Empresa.Nome : "", contrato.Como, contrato.Numero, contrato.Como == "Contratada" ? contrato.Cliente != null ? contrato.Cliente.Nome : "" : contrato.Fornecedor != null ? contrato.Fornecedor.Nome : "",
        //            contrato.StatusContratoDiverso != null ? contrato.StatusContratoDiverso.Nome : "", contrato.DataAbertura.ToShortDateString(), contrato.GetUltimoVencimento != null ? contrato.GetUltimoVencimento.Data.ToShortDateString() : "",
        //            contrato.GetUltimoVencimentoReajustes != null ? contrato.GetUltimoVencimentoReajustes.Data.ToShortDateString() : "", contratos.Count, logomarca, filtroTituloComo, descricaoDataReajuste == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataReajuste, descricaoFormaPagamento, contrato.FormaRecebimento != null ? contrato.FormaRecebimento.Nome : "", colunaFornecedorCliente);
        //    }
        //}
        //else
        //{
        //    fonte.Rows.Add(descricaoUsuario, descricaoEmpresa, filtroComo, filtroFornecedorCliente, descricaoDataVencimento == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataVencimento,
        //            descricaoStatus, descricaoCentroCusto, descricaoIndiceFinanceiro, descricaoSetor, descricaoGrupoEconomico, null, null, null, null, null, null, null, null, null, 0, logomarca, filtroTituloComo, descricaoDataReajuste == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataReajuste, descricaoFormaPagamento, null, colunaFornecedorCliente);
        //}
        //Relatorios.CarregarRelatorio("Contratos", "ContratosDiversos", false, fonte);
    }    

    //Contratos por Processos
    private void CarregarRelatorioContratosPorProcessos()
    {
        //if (ddlAgrupor.SelectedValue.ToInt32() == 0)
        //{
        //    msg.CriarMensagem("Selecione o método de agrupamento (Processos ou Contratos), para poder prosseguir", "Alerta", MsgIcons.Alerta);
        //    return;
        //}

        //string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        //string logomarca = WebUtil.GetURLImagemLogoRelatorio;
        
        ////string descricaoStatus = ddlStatusContratosDiversos.SelectedIndex > 0 ? ddlStatusContratosDiversos.SelectedItem.Text : "Todos";

        //if (ddlAgrupor.SelectedValue.ToInt32() == 1)     //1 = Agrupa por Processos
        //{
        //    if (ddlTipoProcesso.SelectedValue.ToInt32() == 0)
        //    {
        //        msg.CriarMensagem("Selecione o tipo de processo, para poder prosseguir", "Alerta", MsgIcons.Alerta);
        //        return;
        //    }

        //    Fontes.relatorioContratosPorProcessoDataTable fonte = new Fontes.relatorioContratosPorProcessoDataTable();

        //    string descricaoEmpresa = ddlEmpresaContratoPorProcesso.SelectedIndex > 0 ? ddlEmpresaContratoPorProcesso.SelectedItem.Text : "Todas";
        //    string descricaoTipoProcesso = ddlTipoProcesso.SelectedItem.Text;
        //    string desSubstanciaOuTipoOrgao = ddlTipoProcesso.SelectedIndex == 1 ? "Substância:" : "Tipo dos Processos Ambientais:";
        //    string filtroSubstanciaOuTipoOrgao = ddlTipoProcesso.SelectedIndex == 1 ? tbxSubstanciaContratoPorProcesso.Text.IsNotNullOrEmpty() ? tbxSubstanciaContratoPorProcesso.Text : "Todas" : rblTipoProcesso.SelectedItem.Text;
        //    string descricaoNumeroProcesso = tbxNumeroProcesso.Text.IsNotNullOrEmpty() ? tbxNumeroProcesso.Text : "Todos";

        //    if (ddlTipoProcesso.SelectedValue.ToInt32() == 1)   //1 para processos minerarios
        //    {
        //        IList<ProcessoDNPM> processosDNPM = ProcessoDNPM.FiltrarRelatorioContratosPorProcessos(Empresa.ConsultarPorId(ddlEmpresaContratoPorProcesso.SelectedValue.ToInt32()), tbxNumeroProcesso.Text, tbxSubstanciaContratoPorProcesso.Text);
        //        this.RemoverProcessosMinerariosSemContratos(processosDNPM);

        //        if (processosDNPM != null && processosDNPM.Count > 0)
        //            processosDNPM = this.ObterSomenteProcessosDnpmQueOUsuarioTemPermissaoAoSetor(processosDNPM);

        //        if (processosDNPM != null && processosDNPM.Count > 0)
        //        {
        //            foreach (ProcessoDNPM processo in processosDNPM)
        //            {
        //                foreach (ContratoDiverso contrato in processo.ContratosDiversos)
        //                {
        //                    fonte.Rows.Add(descricaoUsuario, descricaoTipoProcesso, descricaoNumeroProcesso, descricaoEmpresa, desSubstanciaOuTipoOrgao, filtroSubstanciaOuTipoOrgao,
        //                        "Processo DNPM Nº: " + processo.GetNumeroProcessoComMascara + (processo.Empresa != null ? ", Empresa: " + processo.Empresa.Nome : "") + ", Abertura: " + processo.DataAbertura.ToShortDateString(), contrato.Numero, contrato.Objeto, contrato.StatusContratoDiverso != null ? contrato.StatusContratoDiverso.Nome : "",
        //                        contrato.DataAbertura.ToShortDateString(), this.ObterTotalDeContratosDosProcessosDNPM(processosDNPM), logomarca);
        //                }
        //            }
        //        }
        //        else 
        //        {
        //            fonte.Rows.Add(descricaoUsuario, descricaoTipoProcesso, descricaoNumeroProcesso, descricaoEmpresa, desSubstanciaOuTipoOrgao, filtroSubstanciaOuTipoOrgao,
        //                        null, null, null, null, null, 0, logomarca);
        //        }
        //    }
        //    else    // 2 para processos ambientais
        //    {
        //        IList<Processo> processos = Processo.FiltrarRelatorioContratosPorProcessos(Empresa.ConsultarPorId(ddlEmpresaContratoPorProcesso.SelectedValue.ToInt32()), tbxNumeroProcesso.Text);
                
        //        if (rblTipoProcesso.SelectedValue.ToInt32() > 0)
        //            this.RemoverProcessosAmbientaisDeOutrosTipos(processos);

        //        this.RemoverProcessosAmbientaisSemContratos(processos);

        //        if (processos != null && processos.Count > 0)
        //            processos = this.ObterSomenteProcessosAmbientaisQueOUsuarioTemPermissaoAoSetor(processos);

        //        if (processos != null && processos.Count > 0)
        //        {
        //            foreach (Processo processo in processos)
        //            {
        //                foreach (ContratoDiverso contrato in processo.ContratosDiversos)
        //                {
        //                    fonte.Rows.Add(descricaoUsuario, descricaoTipoProcesso, descricaoNumeroProcesso, descricaoEmpresa, desSubstanciaOuTipoOrgao, filtroSubstanciaOuTipoOrgao,
        //                        "Processo Ambiental Nº: " + processo.Numero + (processo.Empresa != null ? ", Empresa: " + processo.Empresa.Nome : "") + ", Abertura: " + processo.DataAbertura.ToShortDateString(), contrato.Numero, contrato.Objeto, contrato.StatusContratoDiverso != null ? contrato.StatusContratoDiverso.Nome : "",
        //                        contrato.DataAbertura.ToShortDateString(), this.ObterTotalDeContratosDosProcessosAmbientais(processos), logomarca);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            fonte.Rows.Add(descricaoUsuario, descricaoTipoProcesso, descricaoNumeroProcesso, descricaoEmpresa, desSubstanciaOuTipoOrgao, filtroSubstanciaOuTipoOrgao,
        //                        null, null, null, null, null, 0, logomarca);
        //        }

        //    }

        //    Relatorios.CarregarRelatorio("Contratos por Processos", "ContratosPorProcessos", false, fonte);

        //}
        //else    // 2 = Agrupa por Contratos
        //{
        //    if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 0)
        //    {
        //        msg.CriarMensagem("Selecione o tipo de processo, para poder prosseguir", "Alerta", MsgIcons.Alerta);
        //        return;
        //    }

        //    Fontes.relatorioProcessosPorContratoDataTable fonte = new Fontes.relatorioProcessosPorContratoDataTable();

        //    string descricaoNumeroContrato = tbxNumeroContratoPorProcesso.Text.IsNotNullOrEmpty() ? tbxNumeroContratoPorProcesso.Text : "Todos";
        //    string descricaoObjeto = tbxObjetoContratoPorProcesso.Text.IsNotNullOrEmpty() ? tbxObjetoContratoPorProcesso.Text : "Todos";
        //    string descricaoStatus = ddlStatusContratoProcessoPorContrato.SelectedIndex > 0 ? ddlStatusContratoProcessoPorContrato.SelectedItem.Text : "Todos";
        //    string descricaoTipoProcessoProcessosPorContratos = ddlTipoProcessoProcessosPorContrato.SelectedItem.Text;

        //    IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);

        //    IList<ContratoDiverso> contratosDiversos = ContratoDiverso.FiltrarRelatorioContratosPorProcessos(tbxNumeroContratoPorProcesso.Text, tbxObjetoContratoPorProcesso.Text, ddlStatusContratoProcessoPorContrato.SelectedValue.ToInt32(), setoresUsuario);

        //    if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 1)
        //        this.RemoverContratosSemProcessosDNPM(contratosDiversos);
        //    else
        //        this.RemoverContratosSemProcessosAmbientais(contratosDiversos);

        //    if (contratosDiversos != null && contratosDiversos.Count > 0)
        //    {
        //        foreach (ContratoDiverso contratoDiverso in contratosDiversos)
        //        {
        //            if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 1)
        //            {
        //                foreach (ProcessoDNPM processoDNPM in contratoDiverso.ProcessosDNPM)
        //                {
        //                    fonte.Rows.Add(descricaoUsuario, descricaoNumeroContrato, descricaoObjeto, "Contrato Nº: " + contratoDiverso.Numero + (contratoDiverso.Objeto.IsNotNullOrEmpty() ? ", Objeto: " + contratoDiverso.Objeto : "") + (contratoDiverso.StatusContratoDiverso != null ? ", Status: " + contratoDiverso.StatusContratoDiverso.Nome : ""), processoDNPM.GetNumeroProcessoComMascara, "Minerário",
        //                        processoDNPM.Empresa != null ? processoDNPM.Empresa.Nome : "", processoDNPM.DataAbertura.ToShortDateString(), this.ObterTotalDeProcessosDNPMDosContratos(contratosDiversos), logomarca,
        //                        descricaoStatus, descricaoTipoProcessoProcessosPorContratos);
        //                }
        //            }
        //            else if (ddlTipoProcessoProcessosPorContrato.SelectedValue.ToInt32() == 2)
        //            {
        //                foreach (Processo processoAmbiental in contratoDiverso.Processos)
        //                {
        //                    fonte.Rows.Add(descricaoUsuario, descricaoNumeroContrato, descricaoObjeto, "Contrato Nº: " + contratoDiverso.Numero + (contratoDiverso.Objeto.IsNotNullOrEmpty() ? ", Objeto: " + contratoDiverso.Objeto : "") + (contratoDiverso.StatusContratoDiverso != null ? ", Status: " + contratoDiverso.StatusContratoDiverso.Nome : ""), processoAmbiental.Numero, "Ambiental",
        //                        processoAmbiental.Empresa != null ? processoAmbiental.Empresa.Nome : "", processoAmbiental.DataAbertura.ToShortDateString(), this.ObterTotalDeProcessosAmbientaisDosContratos(contratosDiversos), logomarca,
        //                        descricaoStatus, descricaoTipoProcessoProcessosPorContratos);
        //                }
        //            }
        //        }
        //    }
        //    else 
        //    {
        //        fonte.Rows.Add(descricaoUsuario, descricaoNumeroContrato, descricaoObjeto, null, null, null, null, null, 0, logomarca, descricaoStatus, descricaoTipoProcessoProcessosPorContratos);
        //    }

        //    Relatorios.CarregarRelatorio("Processos por Contratos", "ProcessosPorContratos", true, fonte);
        //}
    }    

    private IList<ProcessoDNPM> ObterSomenteProcessosDnpmQueOUsuarioTemPermissaoAoSetor(IList<ProcessoDNPM> processosDNPM)
    {
        IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);

        IList<ProcessoDNPM> processosAux = new List<ProcessoDNPM>();

        foreach (ProcessoDNPM processo in processosDNPM)
        {
            foreach (ContratoDiverso contrato in processo.ContratosDiversos)
            {
                if (contrato != null && contrato.Setor != null && setoresUsuario.Contains(contrato.Setor))
                    processosAux.Add(processo);
            }
        }

        return processosAux;
    }

    private IList<Processo> ObterSomenteProcessosAmbientaisQueOUsuarioTemPermissaoAoSetor(IList<Processo> processos)
    {
        IList<Setor> setoresUsuario = Setor.RecarregarSetores(UsuarioLogado.ConsultarPorId().Setores);

        IList<Processo> processosAux = new List<Processo>();

        foreach (Processo processo in processos)
        {
            foreach (ContratoDiverso contrato in processo.ContratosDiversos)
            {
                if (contrato != null && contrato.Setor != null && setoresUsuario.Contains(contrato.Setor))
                    processosAux.Add(processo);
            }
        }

        return processosAux;
    }

    #endregion

    #region ___________________________MetodosUtilitarios_______________________________

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

    private void RemoverVencimentosDiversosComOusSemProrrogacoesPrazo(IList<Diverso> diversos, int prorrogacoes)
    {
        if (prorrogacoes == 1)
        {
            if (diversos != null && diversos.Count > 0)
            {
                for (int i = diversos.Count - 1; i > -1; i--)
                {
                    if (diversos[i].GetUltimoVencimento == null || diversos[i].GetUltimoVencimento.ProrrogacoesPrazo == null || diversos[i].GetUltimoVencimento.ProrrogacoesPrazo.Count == 0)
                    {
                        diversos.Remove(diversos[i]);
                    }
                }
            }
        }
        else
        {
            if (diversos != null && diversos.Count > 0)
            {
                for (int i = diversos.Count - 1; i > -1; i--)
                {
                    if (diversos[i].GetUltimoVencimento != null && diversos[i].GetUltimoVencimento.ProrrogacoesPrazo != null && diversos[i].GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
                    {
                        diversos.Remove(diversos[i]);
                    }
                }
            }
        }
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

    private int ObterTotalDeContratosDosProcessosDNPM(IList<ProcessoDNPM> processosDNPM)
    {
        int contatorContratos = 0;

        if (processosDNPM != null && processosDNPM.Count > 0)
        {
            for (int i = 0; i < processosDNPM.Count; i++)
            {
                contatorContratos = contatorContratos + processosDNPM[i].ContratosDiversos.Count;
            }
        }

        return contatorContratos;
    }

    private int ObterTotalDeContratosDosProcessosAmbientais(IList<Processo> processos)
    {
        int contatorContratos = 0;

        if (processos != null && processos.Count > 0)
        {
            for (int i = 0; i < processos.Count; i++)
            {
                contatorContratos = contatorContratos + processos[i].ContratosDiversos.Count;
            }
        }

        return contatorContratos;
    }

    private int ObterTotalDeProcessosDNPMDosContratos(IList<ContratoDiverso> contratos)
    {
        int contatorProcessos = 0;

        if (contratos != null && contratos.Count > 0)
        {
            for (int i = 0; i < contratos.Count; i++)
            {
                contatorProcessos = contatorProcessos + contratos[i].ProcessosDNPM.Count;
            }
        }

        return contatorProcessos;
    }

    private int ObterTotalDeProcessosAmbientaisDosContratos(IList<ContratoDiverso> contratos)
    {
        int contatorProcessos = 0;

        if (contratos != null && contratos.Count > 0)
        {
            for (int i = 0; i < contratos.Count; i++)
            {
                contatorProcessos = contatorProcessos + contratos[i].Processos.Count;
            }
        }

        return contatorProcessos;
    }

    #endregion

    #endregion

}