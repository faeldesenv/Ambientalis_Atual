using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Permissoes_Permissoes : PageBase
{
    private Msg msg = new Msg();

    public GrupoEconomico grupoLogado
    {
        get
        {
            if (this.UsuarioLogado != null && this.UsuarioLogado.Id > 0 && this.UsuarioLogado.GrupoEconomico != null)
                return this.UsuarioLogado.GrupoEconomico.ConsultarPorId();

            return null;
        }
    }

    public IList<Empresa> EmpresasPermissoes
    {
        get
        {
            if (Session["EmpresasPermissoes"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissoes"];
        }
        set { Session["EmpresasPermissoes"] = value; }
    }    

    public IList<ProcessoDNPM> ProcessosPermissoesDNPM
    {
        get
        {
            if (Session["ProcessosPermissoesDNPM"] == null)
                return null;
            else
                return (IList<ProcessoDNPM>)Session["ProcessosPermissoesDNPM"];
        }
        set { Session["ProcessosPermissoesDNPM"] = value; }
    }    

    public IList<Processo> ProcessosPermissoes
    {
        get
        {
            if (Session["ProcessosPermissoes"] == null)
                return null;
            else
                return (IList<Processo>)Session["ProcessosPermissoes"];
        }
        set { Session["ProcessosPermissoes"] = value; }
    }
    public IList<CadastroTecnicoFederal> CadastrosTecnicosPermissoes
    {
        get
        {
            if (Session["CadastrosTecnicosPermissoes"] == null)
                return null;
            else
                return (IList<CadastroTecnicoFederal>)Session["CadastrosTecnicosPermissoes"];
        }
        set { Session["CadastrosTecnicosPermissoes"] = value; }
    }
    public IList<OutrosEmpresa> OutrosEmpresasPermissoes
    {
        get
        {
            if (Session["OutrosEmpresasPermissoes"] == null)
                return null;
            else
                return (IList<OutrosEmpresa>)Session["OutrosEmpresasPermissoes"];
        }
        set { Session["OutrosEmpresasPermissoes"] = value; }
    }    

    public IList<Setor> SetoresPermissoes
    {
        get
        {
            if (Session["SetoresPermissoes"] == null)
                return null;
            else
                return (IList<Setor>)Session["SetoresPermissoes"];
        }
        set { Session["SetoresPermissoes"] = value; }
    }    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (this.UsuarioLogado != null && this.UsuarioLogado.UsuarioAdministrador)
                {
                    this.ExibirPermissoesDoUsuarioGrupoLogado();
                    this.CarregarConfiguracoesPermissoes();
                }
                else
                {
                    Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
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

    #region ____________________ Métodos _____________________

    private void ExibirPermissoesDoUsuarioGrupoLogado()
    {
        //Base sustentar
        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
        {
            if (this.grupoLogado.ModulosPermissao != null && this.grupoLogado.ModulosPermissao.Count > 0)
            {
                permissoes_modulo_geral.Visible = this.grupoLogado.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Geral"));
                permissoes_modulo_dnpm.Visible = this.grupoLogado.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("DNPM"));
                permissoes_modulo_meio_ambiente.Visible = this.grupoLogado.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Meio Ambiente"));
                permissoes_modulo_contratos.Visible = this.grupoLogado.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Contratos"));
                permissoes_modulo_diversos.Visible = this.grupoLogado.ModulosPermissao.Contains(ModuloPermissao.ConsultarPorNome("Diversos"));

                lblTituloUsuariosEdicaoModuloGeral.Text = lblTituloUsuariosEdicaoModuloDNPM.Text = lblTituloUsuariosEdicaoModuloMeioAmbiente.Text = lblTituloUsuariosEdicaoModuloContratos.Text = lblTituloUsuariosEdicaoModuloDiversos.Text = this.grupoLogado.GestaoCompartilhada ? "Usuário Nomeado/Consultor" : "Usuários Editores";
            }
            else
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

        }
        else if (this.UsuarioLogado != null && this.UsuarioLogado.UsuarioAdministrador)   //Base da Ambientalis
        {
            permissoes_modulo_geral.Visible = true;
            permissoes_modulo_dnpm.Visible = true;
            permissoes_modulo_meio_ambiente.Visible = true;
            permissoes_modulo_contratos.Visible = true;
            permissoes_modulo_diversos.Visible = true;

            lblTituloUsuariosEdicaoModuloGeral.Text = lblTituloUsuariosEdicaoModuloDNPM.Text = lblTituloUsuariosEdicaoModuloMeioAmbiente.Text = lblTituloUsuariosEdicaoModuloContratos.Text = lblTituloUsuariosEdicaoModuloDiversos.Text = "Usuários Editores";
        }
    }

    private void CarregarConfiguracoesPermissoes()
    {
        this.CarregarSessoes();

        if (permissoes_modulo_geral.Visible)
        {
            this.CarregarConfiguracaoPermissaoModuloGeral();
        }

        if (permissoes_modulo_dnpm.Visible)
        {
            this.CarregarConfiguracaoPermissaoModuloDNPM();
        }

        if (permissoes_modulo_meio_ambiente.Visible)
        {
            this.CarregarConfiguracaoPermissaoModuloMeioAmbiente();
        }

        if (permissoes_modulo_contratos.Visible)
        {
            this.CarregarConfiguracaoPermissaoModuloContratos();
        }

        if (permissoes_modulo_diversos.Visible)
        {
            this.CarregarConfiguracaoPermissaoModuloDiversos();
        }
    }

    private void CarregarSessoes()
    {
        this.EmpresasPermissoes = Empresa.ConsultarTodosOrdemAlfabetica();
        this.ProcessosPermissoesDNPM = ProcessoDNPM.ConsultarTodosComoObjetos();
        this.ProcessosPermissoes = Processo.ConsultarTodosComoObjetos();
        this.CadastrosTecnicosPermissoes = CadastroTecnicoFederal.ConsultarTodosComoObjetos();
        this.OutrosEmpresasPermissoes = OutrosEmpresa.ConsultarTodosComoObjetos();
        this.SetoresPermissoes = Setor.ConsultarTodosComoObjetos();
    }    

    private void CarregarConfiguracaoPermissaoModuloGeral()
    {
        //limpando os valores dos campos
        lblUsuarioEdicaoModuloGeral.Text = "";
        lblUsuariosVisualizacaoModuloGeral.Text = "";

        ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Geral").Id);
        else
            configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Geral").Id);

        if (configuracaoModuloGeral != null && configuracaoModuloGeral.Id > 0)
        {

            if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 0)
            {
                foreach (Usuario item in configuracaoModuloGeral.UsuariosEdicaoModuloGeral)
                {
                    if (item == configuracaoModuloGeral.UsuariosEdicaoModuloGeral[configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count - 1])
                        lblUsuarioEdicaoModuloGeral.Text += item.Nome;
                    else
                        lblUsuarioEdicaoModuloGeral.Text += item.Nome + ", ";
                }
            }
            else
                lblUsuarioEdicaoModuloGeral.Text = "Não definido";

            if (configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Count > 0)
            {
                foreach (Usuario item in configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral)
                {
                    if (item == configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral[configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Count - 1])
                        lblUsuariosVisualizacaoModuloGeral.Text += item.Nome;
                    else
                        lblUsuariosVisualizacaoModuloGeral.Text += item.Nome + ", ";
                }
            }
            else
                lblUsuariosVisualizacaoModuloGeral.Text = "Todos";
        }
    }

    private void CarregarConfiguracaoPermissaoModuloDNPM()
    {
        ConfiguracaoPermissaoModulo configuracaoModuloDnpm = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);
        else
            configuracaoModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("DNPM").Id);

        if (configuracaoModuloDnpm != null && configuracaoModuloDnpm.Id > 0)
        {
            ddlTipoConfiguracaoModuloDNPM.SelectedValue = configuracaoModuloDnpm.Tipo != Char.MinValue ? configuracaoModuloDnpm.Tipo.ToString() : "G";
            switch (configuracaoModuloDnpm.Tipo)
            {
                case 'G':

                    //setando as divs que irão ou não aparecer
                    div_dnpm_tipo_geral.Visible = true;
                    div_dnpm_outros_tipos.Visible = false;
                    div_dnpm_tipo_geral_edit.Visible = true;
                    div_dnpm_outros_tipos_edit.Visible = false;

                    dnpm_grid_visualizacao_empresa.Visible = false;
                    dnpm_grid_visualizacao_processos.Visible = false;
                    dnpm_grid_edicao_empresas.Visible = false;
                    dnpm_grid_edicao_processos.Visible = false;

                    lblUsuarioEdicaoDNPMGeral.Text = "";
                    lblUsuariosVisualizacaoDNPMGeral.Text = "";

                    if (configuracaoModuloDnpm.UsuariosEdicaoModuloGeral != null && configuracaoModuloDnpm.UsuariosEdicaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloDnpm.UsuariosEdicaoModuloGeral)
                        {
                            if (item == configuracaoModuloDnpm.UsuariosEdicaoModuloGeral[configuracaoModuloDnpm.UsuariosEdicaoModuloGeral.Count - 1])
                                lblUsuarioEdicaoDNPMGeral.Text += item.Nome;
                            else
                                lblUsuarioEdicaoDNPMGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuarioEdicaoDNPMGeral.Text = "Não definido";

                    if (configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral)
                        {
                            if (item == configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral[configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral.Count - 1])
                                lblUsuariosVisualizacaoDNPMGeral.Text += item.Nome;
                            else
                                lblUsuariosVisualizacaoDNPMGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuariosVisualizacaoDNPMGeral.Text = "Todos";

                    break;


                case 'E':

                    //setando as divs que irão ou não aparecer
                    div_dnpm_tipo_geral.Visible = false;
                    div_dnpm_outros_tipos.Visible = true;
                    div_dnpm_tipo_geral_edit.Visible = false;
                    div_dnpm_outros_tipos_edit.Visible = true;

                    dnpm_grid_visualizacao_empresa.Visible = true;
                    dnpm_grid_visualizacao_processos.Visible = false;
                    dnpm_grid_edicao_empresas.Visible = true;
                    dnpm_grid_edicao_processos.Visible = false;

                    this.CarregarEmpresas("DNPM");

                    break;


                case 'P':

                    //setando as divs que irão ou não aparecer
                    div_dnpm_tipo_geral.Visible = false;
                    div_dnpm_outros_tipos.Visible = true;
                    div_dnpm_tipo_geral_edit.Visible = false;
                    div_dnpm_outros_tipos_edit.Visible = true;

                    dnpm_grid_visualizacao_empresa.Visible = false;
                    dnpm_grid_visualizacao_processos.Visible = true;
                    dnpm_grid_edicao_empresas.Visible = false;
                    dnpm_grid_edicao_processos.Visible = true;

                    this.CarregarProcessos("DNPM");

                    break;
            }

        }
    }

    private void CarregarConfiguracaoPermissaoModuloMeioAmbiente()
    {
        ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);
        else
            configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);

        if (configuracaoModuloMeioAmbiente != null && configuracaoModuloMeioAmbiente.Id > 0)
        {
            ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue = configuracaoModuloMeioAmbiente.Tipo != Char.MinValue ? configuracaoModuloMeioAmbiente.Tipo.ToString() : "G";

            switch (configuracaoModuloMeioAmbiente.Tipo)
            {
                case 'G':

                    //setando as divs que irão ou não aparecer
                    div_meio_ambiente_tipo_geral.Visible = true;
                    div_meio_ambiente_outros_tipos.Visible = false;
                    div_meio_ambiente_tipo_geral_edit.Visible = true;
                    div_meio_ambiente_outros_tipos_edit.Visible = false;

                    meio_ambiente_visualizacao_empresas.Visible = false;
                    meio_ambiente_visualizacao_processos.Visible = false;
                    meio_ambiente_edicao_empresas.Visible = false;
                    meio_ambiente_edicao_processos.Visible = false;

                    lblUsuarioEdicaoMeioAmbienteGeral.Text = "";
                    lblUsuariosVisualizacaoMeioAmbienteGeral.Text = "";


                    if (configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral)
                        {
                            if (item == configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral[configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count - 1])
                                lblUsuarioEdicaoMeioAmbienteGeral.Text += item.Nome;
                            else
                                lblUsuarioEdicaoMeioAmbienteGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuarioEdicaoMeioAmbienteGeral.Text = "Não definido";

                    if (configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral)
                        {
                            if (item == configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral[configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count - 1])
                                lblUsuariosVisualizacaoMeioAmbienteGeral.Text += item.Nome;
                            else
                                lblUsuariosVisualizacaoMeioAmbienteGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuariosVisualizacaoMeioAmbienteGeral.Text = "Todos";

                    break;


                case 'E':

                    //setando as divs que irão ou não aparecer
                    div_meio_ambiente_tipo_geral.Visible = false;
                    div_meio_ambiente_outros_tipos.Visible = true;
                    div_meio_ambiente_tipo_geral_edit.Visible = false;
                    div_meio_ambiente_outros_tipos_edit.Visible = true;

                    meio_ambiente_visualizacao_empresas.Visible = true;
                    meio_ambiente_visualizacao_processos.Visible = false;
                    meio_ambiente_edicao_empresas.Visible = true;
                    meio_ambiente_edicao_processos.Visible = false;

                    this.CarregarEmpresas("Meio Ambiente");

                    break;


                case 'P':

                    //setando as divs que irão ou não aparecer
                    div_meio_ambiente_tipo_geral.Visible = false;
                    div_meio_ambiente_outros_tipos.Visible = true;
                    div_meio_ambiente_tipo_geral_edit.Visible = false;
                    div_meio_ambiente_outros_tipos_edit.Visible = true;

                    meio_ambiente_visualizacao_empresas.Visible = false;
                    meio_ambiente_visualizacao_processos.Visible = true;
                    meio_ambiente_edicao_empresas.Visible = false;
                    meio_ambiente_edicao_processos.Visible = true;

                    this.CarregarProcessos("Meio Ambiente");

                    break;
            }

        }
    }

    private void CarregarConfiguracaoPermissaoModuloContratos()
    {
        ConfiguracaoPermissaoModulo configuracaoModuloContratos = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);
        else
            configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Contratos").Id);

        if (configuracaoModuloContratos != null && configuracaoModuloContratos.Id > 0)
        {
            ddlTipoConfiguracaoModuloContratos.SelectedValue = configuracaoModuloContratos.Tipo != Char.MinValue ? configuracaoModuloContratos.Tipo.ToString() : "G";

            switch (configuracaoModuloContratos.Tipo)
            {
                case 'G':

                    //setando as divs que irão ou não aparecer
                    div_contratos_tipo_geral.Visible = true;
                    div_contratos_outros_tipos.Visible = false;
                    div_contratos_tipo_geral_edit.Visible = true;
                    div_contratos_outros_tipos_edit.Visible = false;

                    contratos_edicao_empresas.Visible = false;
                    contratos_edicao_setores.Visible = false;
                    contratos_visualizacao_empresas.Visible = false;
                    contratos_visualizacao_setores.Visible = false;

                    lblUsuarioEdicaoContratosGeral.Text = "";
                    lblUsuariosVisualizacaoContratosGeral.Text = "";


                    if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloContratos.UsuariosEdicaoModuloGeral)
                        {
                            if (item == configuracaoModuloContratos.UsuariosEdicaoModuloGeral[configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count - 1])
                                lblUsuarioEdicaoContratosGeral.Text += item.Nome;
                            else
                                lblUsuarioEdicaoContratosGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuarioEdicaoContratosGeral.Text = "Não definido";

                    if (configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral)
                        {
                            if (item == configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral[configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count - 1])
                                lblUsuariosVisualizacaoContratosGeral.Text += item.Nome;
                            else
                                lblUsuariosVisualizacaoContratosGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuariosVisualizacaoContratosGeral.Text = "Todos";

                    break;


                case 'E':

                    //setando as divs que irão ou não aparecer
                    div_contratos_tipo_geral.Visible = false;
                    div_contratos_outros_tipos.Visible = true;
                    div_contratos_tipo_geral_edit.Visible = false;
                    div_contratos_outros_tipos_edit.Visible = true;

                    contratos_edicao_empresas.Visible = true;
                    contratos_edicao_setores.Visible = false;
                    contratos_visualizacao_empresas.Visible = true;
                    contratos_visualizacao_setores.Visible = false;

                    this.CarregarEmpresas("Contratos");

                    break;


                case 'S':

                    //setando as divs que irão ou não aparecer
                    div_contratos_tipo_geral.Visible = false;
                    div_contratos_outros_tipos.Visible = true;
                    div_contratos_tipo_geral_edit.Visible = false;
                    div_contratos_outros_tipos_edit.Visible = true;

                    contratos_edicao_empresas.Visible = false;
                    contratos_edicao_setores.Visible = true;
                    contratos_visualizacao_empresas.Visible = false;
                    contratos_visualizacao_setores.Visible = true;

                    this.CarregarSetores();

                    break;
            }

        }
    }

    private void CarregarConfiguracaoPermissaoModuloDiversos()
    {
        ConfiguracaoPermissaoModulo configuracaoModuloDiversos = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);
        else
            configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Diversos").Id);

        if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.Id > 0)
        {
            ddlTipoConfiguracaoModuloDiversos.SelectedValue = configuracaoModuloDiversos.Tipo != Char.MinValue ? configuracaoModuloDiversos.Tipo.ToString() : "G";

            switch (configuracaoModuloDiversos.Tipo)
            {
                case 'G':

                    //setando as divs que irão ou não aparecer
                    div_diversos_tipo_geral.Visible = true;
                    div_diversos_outros_tipos.Visible = false;
                    div_diversos_tipo_geral_edit.Visible = true;
                    div_diversos_outros_tipos_edit.Visible = false;

                    lblUsuarioEdicaoDiversosGeral.Text = "";
                    lblUsuariosVisualizacaoDiversosGeral.Text = "";


                    if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloDiversos.UsuariosEdicaoModuloGeral)
                        {
                            if (item == configuracaoModuloDiversos.UsuariosEdicaoModuloGeral[configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count - 1])
                                lblUsuarioEdicaoDiversosGeral.Text += item.Nome;
                            else
                                lblUsuarioEdicaoDiversosGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuarioEdicaoDiversosGeral.Text = "Não definido";

                    if (configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0)
                    {
                        foreach (Usuario item in configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral)
                        {
                            if (item == configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral[configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count - 1])
                                lblUsuariosVisualizacaoDiversosGeral.Text += item.Nome;
                            else
                                lblUsuariosVisualizacaoDiversosGeral.Text += item.Nome + ", ";
                        }
                    }
                    else
                        lblUsuariosVisualizacaoDiversosGeral.Text = "Todos";

                    break;


                case 'E':

                    //setando as divs que irão ou não aparecer
                    div_diversos_tipo_geral.Visible = false;
                    div_diversos_outros_tipos.Visible = true;
                    div_diversos_tipo_geral_edit.Visible = false;
                    div_diversos_outros_tipos_edit.Visible = true;

                    this.CarregarEmpresas("Diversos");

                    break;
            }

        }
    }

    private void AlterarConfiguracaoPermissoesModuloDNPM()
    {
        switch (ddlTipoConfiguracaoModuloDNPM.SelectedValue)
        {
            case "G":

                //setando as divs que irão ou não aparecer
                div_dnpm_tipo_geral.Visible = true;
                div_dnpm_outros_tipos.Visible = false;
                div_dnpm_tipo_geral_edit.Visible = true;
                div_dnpm_outros_tipos_edit.Visible = false;

                dnpm_grid_visualizacao_empresa.Visible = false;
                dnpm_grid_visualizacao_processos.Visible = false;
                dnpm_grid_edicao_empresas.Visible = false;
                dnpm_grid_edicao_processos.Visible = false;

                ConfiguracaoPermissaoModulo configuracaoModuloDnpm = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);
                else
                    configuracaoModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("DNPM").Id);


                if (configuracaoModuloDnpm.UsuariosEdicaoModuloGeral != null && configuracaoModuloDnpm.UsuariosEdicaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloDnpm.UsuariosEdicaoModuloGeral)
                    {
                        if (item == configuracaoModuloDnpm.UsuariosEdicaoModuloGeral[configuracaoModuloDnpm.UsuariosEdicaoModuloGeral.Count - 1])
                            lblUsuarioEdicaoDNPMGeral.Text += item.Nome;
                        else
                            lblUsuarioEdicaoDNPMGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuarioEdicaoDNPMGeral.Text = "Não definido";


                if (configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral)
                    {
                        if (item == configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral[configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral.Count - 1])
                            lblUsuariosVisualizacaoDNPMGeral.Text += item.Nome;
                        else
                            lblUsuariosVisualizacaoDNPMGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuariosVisualizacaoDNPMGeral.Text = "Todos";

                break;


            case "E":

                //setando as divs que irão ou não aparecer
                div_dnpm_tipo_geral.Visible = false;
                div_dnpm_outros_tipos.Visible = true;
                div_dnpm_tipo_geral_edit.Visible = false;
                div_dnpm_outros_tipos_edit.Visible = true;

                dnpm_grid_visualizacao_empresa.Visible = true;
                dnpm_grid_visualizacao_processos.Visible = false;
                dnpm_grid_edicao_empresas.Visible = true;
                dnpm_grid_edicao_processos.Visible = false;


                this.CarregarEmpresas("DNPM");

                break;


            case "P":

                //setando as divs que irão ou não aparecer
                div_dnpm_tipo_geral.Visible = false;
                div_dnpm_outros_tipos.Visible = true;
                div_dnpm_tipo_geral_edit.Visible = false;
                div_dnpm_outros_tipos_edit.Visible = true;

                dnpm_grid_visualizacao_empresa.Visible = false;
                dnpm_grid_visualizacao_processos.Visible = true;
                dnpm_grid_edicao_empresas.Visible = false;
                dnpm_grid_edicao_processos.Visible = true;

                this.CarregarProcessos("DNPM");

                break;
        }
    }

    private void AlterarConfiguracaoPermissoesModuloMeioAmbiente()
    {
        switch (ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue)
        {
            case "G":

                //setando as divs que irão ou não aparecer
                div_meio_ambiente_tipo_geral.Visible = true;
                div_meio_ambiente_outros_tipos.Visible = false;
                div_meio_ambiente_tipo_geral_edit.Visible = true;
                div_meio_ambiente_outros_tipos_edit.Visible = false;

                meio_ambiente_visualizacao_empresas.Visible = false;
                meio_ambiente_visualizacao_processos.Visible = false;
                meio_ambiente_edicao_empresas.Visible = false;
                meio_ambiente_edicao_processos.Visible = false;

                ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);
                else
                    configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);


                if (configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral)
                    {
                        if (item == configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral[configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count - 1])
                            lblUsuarioEdicaoMeioAmbienteGeral.Text += item.Nome;
                        else
                            lblUsuarioEdicaoMeioAmbienteGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuarioEdicaoMeioAmbienteGeral.Text = "Não definido";


                if (configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral)
                    {
                        if (item == configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral[configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count - 1])
                            lblUsuariosVisualizacaoMeioAmbienteGeral.Text += item.Nome;
                        else
                            lblUsuariosVisualizacaoMeioAmbienteGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuariosVisualizacaoMeioAmbienteGeral.Text = "Todos";

                break;


            case "E":

                //setando as divs que irão ou não aparecer
                div_meio_ambiente_tipo_geral.Visible = false;
                div_meio_ambiente_outros_tipos.Visible = true;
                div_meio_ambiente_tipo_geral_edit.Visible = false;
                div_meio_ambiente_outros_tipos_edit.Visible = true;

                meio_ambiente_visualizacao_empresas.Visible = true;
                meio_ambiente_visualizacao_processos.Visible = false;
                meio_ambiente_edicao_empresas.Visible = true;
                meio_ambiente_edicao_processos.Visible = false;

                this.CarregarEmpresas("Meio Ambiente");

                break;


            case "P":

                //setando as divs que irão ou não aparecer
                div_meio_ambiente_tipo_geral.Visible = false;
                div_meio_ambiente_outros_tipos.Visible = true;
                div_meio_ambiente_tipo_geral_edit.Visible = false;
                div_meio_ambiente_outros_tipos_edit.Visible = true;

                meio_ambiente_visualizacao_empresas.Visible = false;
                meio_ambiente_visualizacao_processos.Visible = true;
                meio_ambiente_edicao_empresas.Visible = false;
                meio_ambiente_edicao_processos.Visible = true;

                this.CarregarProcessos("Meio Ambiente");

                break;
        }
    }

    private void AlterarConfiguracaoPermissoesModuloContratos()
    {
        switch (ddlTipoConfiguracaoModuloContratos.SelectedValue)
        {
            case "G":

                //setando as divs que irão ou não aparecer
                div_contratos_tipo_geral.Visible = true;
                div_contratos_outros_tipos.Visible = false;
                div_contratos_tipo_geral_edit.Visible = true;
                div_contratos_outros_tipos_edit.Visible = false;

                contratos_edicao_empresas.Visible = false;
                contratos_edicao_setores.Visible = false;
                contratos_visualizacao_empresas.Visible = false;
                contratos_visualizacao_setores.Visible = false;

                ConfiguracaoPermissaoModulo configuracaoModuloContratos = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);
                else
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Contratos").Id);


                if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloContratos.UsuariosEdicaoModuloGeral)
                    {
                        if (item == configuracaoModuloContratos.UsuariosEdicaoModuloGeral[configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count - 1])
                            lblUsuarioEdicaoContratosGeral.Text += item.Nome;
                        else
                            lblUsuarioEdicaoContratosGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuarioEdicaoContratosGeral.Text = "Não definido";


                if (configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral)
                    {
                        if (item == configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral[configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count - 1])
                            lblUsuariosVisualizacaoContratosGeral.Text += item.Nome;
                        else
                            lblUsuariosVisualizacaoContratosGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuariosVisualizacaoContratosGeral.Text = "Todos";

                break;


            case "E":

                //setando as divs que irão ou não aparecer
                div_contratos_tipo_geral.Visible = false;
                div_contratos_outros_tipos.Visible = true;
                div_contratos_tipo_geral_edit.Visible = false;
                div_contratos_outros_tipos_edit.Visible = true;

                contratos_edicao_empresas.Visible = true;
                contratos_edicao_setores.Visible = false;
                contratos_visualizacao_empresas.Visible = true;
                contratos_visualizacao_setores.Visible = false;

                this.CarregarEmpresas("Contratos");

                break;


            case "S":

                //setando as divs que irão ou não aparecer
                div_contratos_tipo_geral.Visible = false;
                div_contratos_outros_tipos.Visible = true;
                div_contratos_tipo_geral_edit.Visible = false;
                div_contratos_outros_tipos_edit.Visible = true;

                contratos_edicao_empresas.Visible = false;
                contratos_edicao_setores.Visible = true;
                contratos_visualizacao_empresas.Visible = false;
                contratos_visualizacao_setores.Visible = true;

                this.CarregarSetores();

                break;
        }
    }

    private void AlterarConfiguracaoPermissoesModuloDiversos()
    {
        switch (ddlTipoConfiguracaoModuloDiversos.SelectedValue)
        {
            case "G":

                //setando as divs que irão ou não aparecer
                div_diversos_tipo_geral.Visible = true;
                div_diversos_outros_tipos.Visible = false;
                div_diversos_tipo_geral_edit.Visible = true;
                div_diversos_outros_tipos_edit.Visible = false;

                ConfiguracaoPermissaoModulo configuracaoModuloDiversos = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);
                else
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Diversos").Id);


                if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloDiversos.UsuariosEdicaoModuloGeral)
                    {
                        if (item == configuracaoModuloDiversos.UsuariosEdicaoModuloGeral[configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count - 1])
                            lblUsuarioEdicaoDiversosGeral.Text += item.Nome;
                        else
                            lblUsuarioEdicaoDiversosGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuarioEdicaoDiversosGeral.Text = "Não definido";


                if (configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0)
                {
                    foreach (Usuario item in configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral)
                    {
                        if (item == configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral[configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count - 1])
                            lblUsuariosVisualizacaoDiversosGeral.Text += item.Nome;
                        else
                            lblUsuariosVisualizacaoDiversosGeral.Text += item.Nome + ", ";
                    }
                }
                else
                    lblUsuariosVisualizacaoDiversosGeral.Text = "Todos";

                break;


            case "E":

                //setando as divs que irão ou não aparecer
                div_diversos_tipo_geral.Visible = false;
                div_diversos_outros_tipos.Visible = true;
                div_diversos_tipo_geral_edit.Visible = false;
                div_diversos_outros_tipos_edit.Visible = true;

                this.CarregarEmpresas("Diversos");

                break;
        }
    }

    private void CarregarEmpresas(string modulo)
    {

        if (modulo == "DNPM")
        {
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataBind();


            grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataBind();
        }

        if (modulo == "Meio Ambiente")
        {
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataBind();


            grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataBind();
        }

        if (modulo == "Contratos")
        {
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataBind();


            grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataBind();
        }

        if (modulo == "Diversos")
        {
            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataBind();

            grvConfiguracaoUsuariosEdicaoModuloDiversos.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoModuloDiversos.DataBind();
        }
    }

    private void CarregarProcessos(string modulo)
    {

        if (modulo == "DNPM")
        {
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataSource = this.ProcessosPermissoesDNPM;
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataBind();


            grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataSource = this.ProcessosPermissoesDNPM;
            grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataBind();
        }

        if (modulo == "Meio Ambiente")
        {
            //Processos
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataSource = this.ProcessosPermissoes;
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataBind();

            grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataSource = this.ProcessosPermissoes;
            grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataBind();

            //Cadastros
            grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataSource = this.CadastrosTecnicosPermissoes;
            grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataBind();

            grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataSource = this.CadastrosTecnicosPermissoes;
            grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataBind();

            //Outros
            grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataSource = this.OutrosEmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataBind();

            grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataSource = this.OutrosEmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataBind();
        }

    }

    private void CarregarSetores()
    {
        grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataSource = this.SetoresPermissoes;
        grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataBind();


        grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataSource = this.SetoresPermissoes;
        grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataBind();
    }

    private void CarregarMarcarUsuariosVisualizacao()
    {
        ckblUsuariosVisualizacao.Items.Clear();

        IList<Usuario> usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

        IList<int> idsUsuariosVisualizadores = this.ObterUsuariosVisualizadores();

        if (usuarios != null && usuarios.Count > 0)
        {
            //Significa que é pra marcar a opção todos
            if (idsUsuariosVisualizadores != null && idsUsuariosVisualizadores.Count > 0 && idsUsuariosVisualizadores.Count == 1 && idsUsuariosVisualizadores[0] == 0)
                ckbTodosUsuariosVisualizacao.Checked = true;
            else
                ckbTodosUsuariosVisualizacao.Checked = false;

            for (int i = 0; i < usuarios.Count; i++)
            {
                ListItem novoItem = new ListItem(usuarios[i].Nome, usuarios[i].Id.ToString());

                if (idsUsuariosVisualizadores != null && idsUsuariosVisualizadores.Count > 0 && idsUsuariosVisualizadores.Contains(usuarios[i].Id))
                    novoItem.Selected = true;

                ckblUsuariosVisualizacao.Items.Add(novoItem);
            }
        }
    }

    private void CarregarMarcarUsuarioEdicao()
    {
        //ver isso com calma
        rblUsuarioEdicao.Items.Clear();
        ckblUsuariosEdicao.Items.Clear();

        lblTituloUsuarioEdicao.Text = this.grupoLogado != null && this.grupoLogado.GestaoCompartilhada ? "Usuário Nomeado/Consultor do Módulo" : "Usuários Editores do Módulo";

        IList<Usuario> usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

        IList<int> idsUsuariosEditores = this.ObterUsuariosEditores();

        if (usuarios != null && usuarios.Count > 0)
        {
            for (int i = 0; i < usuarios.Count; i++)
            {
                ListItem novoItem = new ListItem(usuarios[i].Nome, usuarios[i].Id.ToString());

                if (idsUsuariosEditores != null && idsUsuariosEditores.Count > 0 && idsUsuariosEditores.Contains(usuarios[i].Id))
                    novoItem.Selected = true;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0 && this.grupoLogado.ConsultarPorId().GestaoCompartilhada)
                {
                    rblUsuarioEdicao.Items.Add(novoItem);
                    gestao_comum.Visible = false;
                    gestao_compartilhada.Visible = true;
                }
                else
                {
                    ckblUsuariosEdicao.Items.Add(novoItem);
                    gestao_comum.Visible = true;
                    gestao_compartilhada.Visible = false;
                }
            }
        }

    }

    private void CarregarUsuariosEdicao()
    {
        rblUsuarioEdicao.Items.Clear();

        IList<Usuario> usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

        if (usuarios != null && usuarios.Count > 0)
        {
            foreach (Usuario usuario in usuarios)
            {
                rblUsuarioEdicao.Items.Add(new ListItem(usuario.Nome, usuario.Id.ToString()));
            }
        }
    }

    private IList<int> ObterUsuariosVisualizadores()
    {
        //se a lista de usuarios de visualização for nula retorna o array com a primeira posicao preenchida com o valor zero
        IList<int> ids = new List<int>();

        if (lblNomeModulo.Text == "Geral")
        {
            ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

            if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());
            else
                configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

            if (configuracaoModuloGeral != null && configuracaoModuloGeral.Id > 0)
            {
                if (configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Count > 0)
                {
                    foreach (Usuario usuario in configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral)
                    {
                        ids.Add(usuario.Id);
                    }
                }
                else
                {
                    ids.Add(0);
                    return ids;
                }
            }
        }

        if (lblNomeModulo.Text == "DNPM")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloDnpm = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());
                    else
                        configuracaoModuloModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (configuracaoModuloModuloDnpm != null && configuracaoModuloModuloDnpm.Id > 0)
                    {
                        if (configuracaoModuloModuloDnpm.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloModuloDnpm.UsuariosVisualizacaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloDnpm.UsuariosVisualizacaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoVisualizacao.Value.ToInt32(), hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoDNPM != null && empresaPermissaoDNPM.Id > 0)
                    {
                        if (empresaPermissaoDNPM.UsuariosVisualizacao != null && empresaPermissaoDNPM.UsuariosVisualizacao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoDNPM.UsuariosVisualizacao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "P":

                    ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    if (processo != null && processo.Id > 0)
                    {
                        if (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0)
                        {
                            foreach (Usuario usuario in processo.UsuariosVisualizacao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;
            }
        }

        if (lblNomeModulo.Text == "Meio Ambiente")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloMeioAmbiente = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());
                    else
                        configuracaoModuloModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (configuracaoModuloModuloMeioAmbiente != null && configuracaoModuloModuloMeioAmbiente.Id > 0)
                    {
                        if (configuracaoModuloModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoVisualizacao.Value.ToInt32(), hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoMeioAmbiente != null && empresaPermissaoMeioAmbiente.Id > 0)
                    {
                        if (empresaPermissaoMeioAmbiente.UsuariosVisualizacao != null && empresaPermissaoMeioAmbiente.UsuariosVisualizacao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoMeioAmbiente.UsuariosVisualizacao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;

                case "P":

                    if (hfTipoObjetoVisualizacao.Value.Contains("Processo"))
                    {
                        Processo processo = Processo.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                        if (processo != null && processo.Id > 0)
                        {
                            if (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0)
                            {
                                foreach (Usuario usuario in processo.UsuariosVisualizacao)
                                {
                                    ids.Add(usuario.Id);
                                }
                            }
                            else
                            {
                                ids.Add(0);
                                return ids;
                            }
                        }
                    }

                    if (hfTipoObjetoVisualizacao.Value.Contains("Cadastro"))
                    {
                        CadastroTecnicoFederal cadastroTec = CadastroTecnicoFederal.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                        if (cadastroTec != null && cadastroTec.Id > 0)
                        {
                            if (cadastroTec.UsuariosVisualizacao != null && cadastroTec.UsuariosVisualizacao.Count > 0)
                            {
                                foreach (Usuario usuario in cadastroTec.UsuariosVisualizacao)
                                {
                                    ids.Add(usuario.Id);
                                }
                            }
                            else
                            {
                                ids.Add(0);
                                return ids;
                            }

                        }
                    }

                    if (hfTipoObjetoVisualizacao.Value.Contains("Outros"))
                    {
                        OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                        if (outros != null && outros.Id > 0)
                        {
                            if (outros.UsuariosVisualizacao != null && outros.UsuariosVisualizacao.Count > 0)
                            {
                                foreach (Usuario usuario in outros.UsuariosVisualizacao)
                                {
                                    ids.Add(usuario.Id);
                                }
                            }
                            else
                            {
                                ids.Add(0);
                                return ids;
                            }
                        }
                    }

                    break;
            }
        }

        if (lblNomeModulo.Text == "Contratos")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloContratos = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());
                    else
                        configuracaoModuloModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (configuracaoModuloModuloContratos != null && configuracaoModuloModuloContratos.Id > 0)
                    {
                        if (configuracaoModuloModuloContratos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloModuloContratos.UsuariosVisualizacaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloContratos.UsuariosVisualizacaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoVisualizacao.Value.ToInt32(), hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoContratos != null && empresaPermissaoContratos.Id > 0)
                    {
                        if (empresaPermissaoContratos.UsuariosVisualizacao != null && empresaPermissaoContratos.UsuariosVisualizacao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoContratos.UsuariosVisualizacao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;

                case "S":

                    Setor setor = Setor.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    if (setor != null && setor.Id > 0)
                    {
                        if (setor.UsuariosVisualizacao != null && setor.UsuariosVisualizacao.Count > 0)
                        {
                            foreach (Usuario usuario in setor.UsuariosVisualizacao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;
            }
        }

        if (lblNomeModulo.Text == "Diversos")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloDiversos = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());
                    else
                        configuracaoModuloModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (configuracaoModuloModuloDiversos != null && configuracaoModuloModuloDiversos.Id > 0)
                    {
                        if (configuracaoModuloModuloDiversos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloDiversos.UsuariosVisualizacaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoVisualizacao.Value.ToInt32(), hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoDiversos != null && empresaPermissaoDiversos.Id > 0)
                    {
                        if (empresaPermissaoDiversos.UsuariosVisualizacao != null && empresaPermissaoDiversos.UsuariosVisualizacao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoDiversos.UsuariosVisualizacao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;
            }
        }

        return ids;

    }

    private IList<int> ObterUsuariosEditores()
    {
        //se a lista de usuarios de edição for nula retorna o array com a primeira posicao preenchida com o valor zero
        IList<int> ids = new List<int>();

        if (lblNomeModuloEdicao.Text == "Geral")
        {
            ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

            if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());
            else
                configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoEdicao.Value.ToInt32());

            if (configuracaoModuloGeral != null && configuracaoModuloGeral.Id > 0)
            {
                if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 0)
                {
                    foreach (Usuario usuario in configuracaoModuloGeral.UsuariosEdicaoModuloGeral)
                    {
                        ids.Add(usuario.Id);
                    }
                }
                else
                {
                    ids.Add(0);
                    return ids;
                }
            }
        }

        if (lblNomeModuloEdicao.Text == "DNPM")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloDnpm = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());
                    else
                        configuracaoModuloModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (configuracaoModuloModuloDnpm != null && configuracaoModuloModuloDnpm.Id > 0)
                    {
                        if (configuracaoModuloModuloDnpm.UsuariosEdicaoModuloGeral != null && configuracaoModuloModuloDnpm.UsuariosEdicaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloDnpm.UsuariosEdicaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoEdicao.Value.ToInt32(), hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoDNPM != null && empresaPermissaoDNPM.Id > 0)
                    {
                        if (empresaPermissaoDNPM.UsuariosEdicao != null && empresaPermissaoDNPM.UsuariosEdicao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoDNPM.UsuariosEdicao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "P":

                    ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    if (processo != null && processo.Id > 0)
                    {
                        if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
                        {
                            foreach (Usuario usuario in processo.UsuariosEdicao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;
            }
        }

        if (lblNomeModuloEdicao.Text == "Meio Ambiente")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloMeioAmbiente = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());
                    else
                        configuracaoModuloModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (configuracaoModuloModuloMeioAmbiente != null && configuracaoModuloModuloMeioAmbiente.Id > 0)
                    {
                        if (configuracaoModuloModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && configuracaoModuloModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloMeioAmbiente.UsuariosEdicaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoEdicao.Value.ToInt32(), hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoMeioAmbiente != null && empresaPermissaoMeioAmbiente.Id > 0)
                    {
                        if (empresaPermissaoMeioAmbiente.UsuariosEdicao != null && empresaPermissaoMeioAmbiente.UsuariosEdicao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoMeioAmbiente.UsuariosEdicao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;

                case "P":

                    if (hfTipoObjetoEdicao.Value.Contains("Processo"))
                    {
                        Processo processo = Processo.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                        if (processo != null && processo.Id > 0)
                        {
                            if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
                            {
                                foreach (Usuario usuario in processo.UsuariosEdicao)
                                {
                                    ids.Add(usuario.Id);
                                }
                            }
                            else
                            {
                                ids.Add(0);
                                return ids;
                            }
                        }
                    }

                    if (hfTipoObjetoEdicao.Value.Contains("Cadastro"))
                    {
                        CadastroTecnicoFederal cadastroTec = CadastroTecnicoFederal.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                        if (cadastroTec != null && cadastroTec.Id > 0)
                        {
                            if (cadastroTec.UsuariosEdicao != null && cadastroTec.UsuariosEdicao.Count > 0)
                            {
                                foreach (Usuario usuario in cadastroTec.UsuariosEdicao)
                                {
                                    ids.Add(usuario.Id);
                                }
                            }
                            else
                            {
                                ids.Add(0);
                                return ids;
                            }

                        }
                    }

                    if (hfTipoObjetoEdicao.Value.Contains("Outros"))
                    {
                        OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                        if (outros != null && outros.Id > 0)
                        {
                            if (outros.UsuariosEdicao != null && outros.UsuariosEdicao.Count > 0)
                            {
                                foreach (Usuario usuario in outros.UsuariosEdicao)
                                {
                                    ids.Add(usuario.Id);
                                }
                            }
                            else
                            {
                                ids.Add(0);
                                return ids;
                            }
                        }
                    }

                    break;
            }
        }

        if (lblNomeModuloEdicao.Text == "Contratos")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloContratos = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());
                    else
                        configuracaoModuloModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (configuracaoModuloModuloContratos != null && configuracaoModuloModuloContratos.Id > 0)
                    {
                        if (configuracaoModuloModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloModuloContratos.UsuariosEdicaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloContratos.UsuariosEdicaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoEdicao.Value.ToInt32(), hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoContratos != null && empresaPermissaoContratos.Id > 0)
                    {
                        if (empresaPermissaoContratos.UsuariosEdicao != null && empresaPermissaoContratos.UsuariosEdicao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoContratos.UsuariosEdicao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;

                case "S":

                    Setor setor = Setor.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    if (setor != null && setor.Id > 0)
                    {
                        if (setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0)
                        {
                            foreach (Usuario usuario in setor.UsuariosEdicao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }
                    }

                    break;
            }
        }

        if (lblNomeModuloEdicao.Text == "Diversos")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":
                    ConfiguracaoPermissaoModulo configuracaoModuloModuloDiversos = null;

                    if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                        configuracaoModuloModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());
                    else
                        configuracaoModuloModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (configuracaoModuloModuloDiversos != null && configuracaoModuloModuloDiversos.Id > 0)
                    {
                        if (configuracaoModuloModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0)
                        {
                            foreach (Usuario usuario in configuracaoModuloModuloDiversos.UsuariosEdicaoModuloGeral)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;

                case "E":

                    EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(hfIdObjetoEdicao.Value.ToInt32(), hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoDiversos != null && empresaPermissaoDiversos.Id > 0)
                    {
                        if (empresaPermissaoDiversos.UsuariosEdicao != null && empresaPermissaoDiversos.UsuariosEdicao.Count > 0)
                        {
                            foreach (Usuario usuario in empresaPermissaoDiversos.UsuariosEdicao)
                            {
                                ids.Add(usuario.Id);
                            }
                        }
                        else
                        {
                            ids.Add(0);
                            return ids;
                        }

                    }

                    break;
            }
        }

        return ids;

    }

    private void SalvarUsuariosVisualizacao()
    {
        ConfiguracaoPermissaoModulo configuracaoModulo = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModulo = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());
        else
            configuracaoModulo = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

        if (configuracaoModulo == null)
            configuracaoModulo = new ConfiguracaoPermissaoModulo();

        ModuloPermissao moduloPermissao = ModuloPermissao.ConsultarPorId(hfIdModuloPermissaoVisualizacao.Value.ToInt32());

        configuracaoModulo.ModuloPermissao = moduloPermissao;

        ListItemCollection itensSelecionados = ckblUsuariosVisualizacao.GetSelectedItems();

        if (lblNomeModulo.Text == "Geral")
        {
            configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

            configuracaoModulo.UsuariosVisualizacaoModuloGeral = null;

            if (itensSelecionados != null && itensSelecionados.Count > 0)
            {
                configuracaoModulo.UsuariosVisualizacaoModuloGeral = new List<Usuario>();

                foreach (ListItem item in itensSelecionados)
                {
                    configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                }
            }
        }


        if (lblNomeModulo.Text == "DNPM")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosVisualizacaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosVisualizacaoModuloGeral = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoDNPM == null)
                        empresaPermissaoDNPM = new EmpresaModuloPermissao();

                    empresaPermissaoDNPM.UsuariosVisualizacao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoDNPM.UsuariosVisualizacao = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            empresaPermissaoDNPM.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    empresaPermissaoDNPM.Empresa = empresa;
                    empresaPermissaoDNPM.ModuloPermissao = moduloPermissao;

                    empresaPermissaoDNPM = empresaPermissaoDNPM.Salvar();

                    break;


                case "P":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.PORPROCESSO;

                    ProcessoDNPM processoDnpm = ProcessoDNPM.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    processoDnpm.UsuariosVisualizacao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        processoDnpm.UsuariosVisualizacao = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            processoDnpm.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    processoDnpm = processoDnpm.Salvar();

                    break;
            }

        }


        if (lblNomeModulo.Text == "Meio Ambiente")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosVisualizacaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosVisualizacaoModuloGeral = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoMeioAmbiente == null)
                        empresaPermissaoMeioAmbiente = new EmpresaModuloPermissao();

                    empresaPermissaoMeioAmbiente.UsuariosVisualizacao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoMeioAmbiente.UsuariosVisualizacao = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            empresaPermissaoMeioAmbiente.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    empresaPermissaoMeioAmbiente.Empresa = empresa;
                    empresaPermissaoMeioAmbiente.ModuloPermissao = moduloPermissao;

                    empresaPermissaoMeioAmbiente = empresaPermissaoMeioAmbiente.Salvar();

                    break;


                case "P":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.PORPROCESSO;


                    if (hfTipoObjetoVisualizacao.Value.Contains("Processo"))
                    {
                        Processo processo = Processo.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                        processo.UsuariosVisualizacao = null;

                        if (itensSelecionados != null && itensSelecionados.Count > 0)
                        {
                            processo.UsuariosVisualizacao = new List<Usuario>();

                            foreach (ListItem item in itensSelecionados)
                            {
                                processo.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                            }
                        }

                        processo = processo.Salvar();
                    }


                    if (hfTipoObjetoVisualizacao.Value.Contains("Cadastro"))
                    {
                        CadastroTecnicoFederal cadastroTec = CadastroTecnicoFederal.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                        cadastroTec.UsuariosVisualizacao = null;

                        if (itensSelecionados != null && itensSelecionados.Count > 0)
                        {
                            cadastroTec.UsuariosVisualizacao = new List<Usuario>();

                            foreach (ListItem item in itensSelecionados)
                            {
                                cadastroTec.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                            }
                        }

                        cadastroTec = cadastroTec.Salvar();
                    }


                    if (hfTipoObjetoVisualizacao.Value.Contains("Outros"))
                    {
                        OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                        outros.UsuariosVisualizacao = null;

                        if (itensSelecionados != null && itensSelecionados.Count > 0)
                        {
                            outros.UsuariosVisualizacao = new List<Usuario>();

                            foreach (ListItem item in itensSelecionados)
                            {
                                outros.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                            }
                        }

                        outros = outros.Salvar();
                    }

                    break;
            }

        }


        if (lblNomeModulo.Text == "Contratos")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosVisualizacaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosVisualizacaoModuloGeral = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoContratos == null)
                        empresaPermissaoContratos = new EmpresaModuloPermissao();

                    empresaPermissaoContratos.UsuariosVisualizacao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoContratos.UsuariosVisualizacao = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            empresaPermissaoContratos.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    empresaPermissaoContratos.Empresa = empresa;
                    empresaPermissaoContratos.ModuloPermissao = moduloPermissao;

                    empresaPermissaoContratos = empresaPermissaoContratos.Salvar();

                    break;


                case "S":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.PORSETOR;

                    Setor setor = Setor.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    setor.UsuariosVisualizacao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        setor.UsuariosVisualizacao = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            setor.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    setor = setor.Salvar();

                    break;
            }
        }

        if (lblNomeModulo.Text == "Diversos")
        {
            switch (hfTipoConfiguracaoVisualizacao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosVisualizacaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosVisualizacaoModuloGeral = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoVisualizacao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoVisualizacao.Value.ToInt32());

                    if (empresaPermissaoDiversos == null)
                        empresaPermissaoDiversos = new EmpresaModuloPermissao();

                    empresaPermissaoDiversos.UsuariosVisualizacao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoDiversos.UsuariosVisualizacao = new List<Usuario>();

                        foreach (ListItem item in itensSelecionados)
                        {
                            empresaPermissaoDiversos.UsuariosVisualizacao.Add(Usuario.ConsultarPorId(item.Value.ToInt32()));
                        }
                    }

                    empresaPermissaoDiversos.Empresa = empresa;
                    empresaPermissaoDiversos.ModuloPermissao = moduloPermissao;

                    empresaPermissaoDiversos = empresaPermissaoDiversos.Salvar();

                    break;

            }
        }

        configuracaoModulo = configuracaoModulo.Salvar();

        msg.CriarMensagem("Permissões de usuários salvas com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void CarregarConfiguracoesVisualizacoes()
    {
        if (lblNomeModulo.Text == "Geral")
            this.CarregarConfiguracaoPermissaoModuloGeral();

        if (lblNomeModulo.Text == "DNPM")
            this.CarregarConfiguracaoPermissaoModuloDNPM();

        if (lblNomeModulo.Text == "Meio Ambiente")
            this.CarregarConfiguracaoPermissaoModuloMeioAmbiente();

        if (lblNomeModulo.Text == "Contratos")
            this.CarregarConfiguracaoPermissaoModuloContratos();

        if (lblNomeModulo.Text == "Diversos")
            this.CarregarConfiguracaoPermissaoModuloDiversos();
    }

    private void CarregarConfiguracoesEdicoes()
    {
        if (lblNomeModuloEdicao.Text == "Geral")
            this.CarregarConfiguracaoPermissaoModuloGeral();

        if (lblNomeModuloEdicao.Text == "DNPM")
            this.CarregarConfiguracaoPermissaoModuloDNPM();

        if (lblNomeModuloEdicao.Text == "Meio Ambiente")
            this.CarregarConfiguracaoPermissaoModuloMeioAmbiente();

        if (lblNomeModuloEdicao.Text == "Contratos")
            this.CarregarConfiguracaoPermissaoModuloContratos();

        if (lblNomeModuloEdicao.Text == "Diversos")
            this.CarregarConfiguracaoPermissaoModuloDiversos();
    }

    private void SalvarUsuarioEdicao()
    {
        ConfiguracaoPermissaoModulo configuracaoModulo = null;

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            configuracaoModulo = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());
        else
            configuracaoModulo = ConfiguracaoPermissaoModulo.ConsultarPorModulo(hfIdModuloPermissaoEdicao.Value.ToInt32());

        if (configuracaoModulo == null)
            configuracaoModulo = new ConfiguracaoPermissaoModulo();

        ModuloPermissao moduloPermissao = ModuloPermissao.ConsultarPorId(hfIdModuloPermissaoEdicao.Value.ToInt32());

        configuracaoModulo.ModuloPermissao = moduloPermissao;

        ListItemCollection itensSelecionados = new ListItemCollection();

        if (this.grupoLogado != null && this.grupoLogado.Id > 0 && this.grupoLogado.GestaoCompartilhada)
            itensSelecionados.Add(rblUsuarioEdicao.SelectedItem);
        else
            itensSelecionados = ckblUsuariosEdicao.GetSelectedItems();

        if (lblNomeModuloEdicao.Text == "Geral")
        {
            configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

            configuracaoModulo.UsuariosEdicaoModuloGeral = null;

            if (itensSelecionados != null && itensSelecionados.Count > 0)
            {
                configuracaoModulo.UsuariosEdicaoModuloGeral = new List<Usuario>();

                //percorrendo os usuarios editores selecionados
                foreach (ListItem item in itensSelecionados)
                {
                    Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                    configuracaoModulo.UsuariosEdicaoModuloGeral.Add(usuario);

                    //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                    if (configuracaoModulo.UsuariosVisualizacaoModuloGeral != null && configuracaoModulo.UsuariosVisualizacaoModuloGeral.Count > 0)
                    {
                        if (!configuracaoModulo.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                            configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(usuario);
                    }
                }

            }

        }


        if (lblNomeModuloEdicao.Text == "DNPM")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosEdicaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosEdicaoModuloGeral = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            configuracaoModulo.UsuariosEdicaoModuloGeral.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (configuracaoModulo.UsuariosVisualizacaoModuloGeral != null && configuracaoModulo.UsuariosVisualizacaoModuloGeral.Count > 0)
                            {
                                if (!configuracaoModulo.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                                    configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(usuario);
                            }
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoDNPM == null)
                        empresaPermissaoDNPM = new EmpresaModuloPermissao();

                    empresaPermissaoDNPM.UsuariosEdicao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoDNPM.UsuariosEdicao = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            empresaPermissaoDNPM.UsuariosEdicao.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (empresaPermissaoDNPM.UsuariosEdicao != null && empresaPermissaoDNPM.UsuariosEdicao.Count > 0)
                            {
                                if (!empresaPermissaoDNPM.UsuariosEdicao.Contains(usuario))
                                    empresaPermissaoDNPM.UsuariosEdicao.Add(usuario);
                            }
                        }

                    }

                    empresaPermissaoDNPM.Empresa = empresa;
                    empresaPermissaoDNPM.ModuloPermissao = moduloPermissao;

                    empresaPermissaoDNPM = empresaPermissaoDNPM.Salvar();

                    break;


                case "P":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.PORPROCESSO;

                    ProcessoDNPM processoDnpm = ProcessoDNPM.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    processoDnpm.UsuariosEdicao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        processoDnpm.UsuariosEdicao = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            processoDnpm.UsuariosEdicao.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (processoDnpm.UsuariosEdicao != null && processoDnpm.UsuariosEdicao.Count > 0)
                            {
                                if (!processoDnpm.UsuariosEdicao.Contains(usuario))
                                    processoDnpm.UsuariosEdicao.Add(usuario);
                            }
                        }
                    }

                    processoDnpm = processoDnpm.Salvar();

                    break;
            }

        }


        if (lblNomeModuloEdicao.Text == "Meio Ambiente")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosEdicaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosEdicaoModuloGeral = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            configuracaoModulo.UsuariosEdicaoModuloGeral.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (configuracaoModulo.UsuariosVisualizacaoModuloGeral != null && configuracaoModulo.UsuariosVisualizacaoModuloGeral.Count > 0)
                            {
                                if (!configuracaoModulo.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                                    configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(usuario);
                            }
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoMeioAmbiente == null)
                        empresaPermissaoMeioAmbiente = new EmpresaModuloPermissao();

                    empresaPermissaoMeioAmbiente.UsuariosEdicao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoMeioAmbiente.UsuariosEdicao = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            empresaPermissaoMeioAmbiente.UsuariosEdicao.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (empresaPermissaoMeioAmbiente.UsuariosEdicao != null && empresaPermissaoMeioAmbiente.UsuariosEdicao.Count > 0)
                            {
                                if (!empresaPermissaoMeioAmbiente.UsuariosEdicao.Contains(usuario))
                                    empresaPermissaoMeioAmbiente.UsuariosEdicao.Add(usuario);
                            }
                        }
                    }

                    empresaPermissaoMeioAmbiente.Empresa = empresa;
                    empresaPermissaoMeioAmbiente.ModuloPermissao = moduloPermissao;

                    empresaPermissaoMeioAmbiente = empresaPermissaoMeioAmbiente.Salvar();

                    break;


                case "P":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.PORPROCESSO;


                    if (hfTipoObjetoEdicao.Value.Contains("Processo"))
                    {
                        Processo processo = Processo.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                        processo.UsuariosEdicao = null;

                        if (itensSelecionados != null && itensSelecionados.Count > 0)
                        {
                            processo.UsuariosEdicao = new List<Usuario>();

                            //percorrendo os usuarios editores selecionados
                            foreach (ListItem item in itensSelecionados)
                            {
                                Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                                processo.UsuariosEdicao.Add(usuario);

                                //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                                if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
                                {
                                    if (!processo.UsuariosEdicao.Contains(usuario))
                                        processo.UsuariosEdicao.Add(usuario);
                                }
                            }
                        }

                        processo = processo.Salvar();
                    }


                    if (hfTipoObjetoEdicao.Value.Contains("Cadastro"))
                    {
                        CadastroTecnicoFederal cadastroTec = CadastroTecnicoFederal.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                        cadastroTec.UsuariosEdicao = null;

                        if (itensSelecionados != null && itensSelecionados.Count > 0)
                        {
                            cadastroTec.UsuariosEdicao = new List<Usuario>();

                            //percorrendo os usuarios editores selecionados
                            foreach (ListItem item in itensSelecionados)
                            {
                                Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                                cadastroTec.UsuariosEdicao.Add(usuario);

                                //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                                if (cadastroTec.UsuariosEdicao != null && cadastroTec.UsuariosEdicao.Count > 0)
                                {
                                    if (!cadastroTec.UsuariosEdicao.Contains(usuario))
                                        cadastroTec.UsuariosEdicao.Add(usuario);
                                }
                            }
                        }

                        cadastroTec = cadastroTec.Salvar();
                    }


                    if (hfTipoObjetoEdicao.Value.Contains("Outros"))
                    {
                        OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                        outros.UsuariosEdicao = null;

                        if (itensSelecionados != null && itensSelecionados.Count > 0)
                        {
                            outros.UsuariosEdicao = new List<Usuario>();

                            //percorrendo os usuarios editores selecionados
                            foreach (ListItem item in itensSelecionados)
                            {
                                Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                                outros.UsuariosEdicao.Add(usuario);

                                //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                                if (outros.UsuariosEdicao != null && outros.UsuariosEdicao.Count > 0)
                                {
                                    if (!outros.UsuariosEdicao.Contains(usuario))
                                        outros.UsuariosEdicao.Add(usuario);
                                }
                            }
                        }

                        outros = outros.Salvar();
                    }

                    break;
            }

        }


        if (lblNomeModuloEdicao.Text == "Contratos")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosEdicaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosEdicaoModuloGeral = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            configuracaoModulo.UsuariosEdicaoModuloGeral.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (configuracaoModulo.UsuariosVisualizacaoModuloGeral != null && configuracaoModulo.UsuariosVisualizacaoModuloGeral.Count > 0)
                            {
                                if (!configuracaoModulo.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                                    configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(usuario);
                            }
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoContratos == null)
                        empresaPermissaoContratos = new EmpresaModuloPermissao();

                    empresaPermissaoContratos.UsuariosEdicao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoContratos.UsuariosEdicao = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            empresaPermissaoContratos.UsuariosEdicao.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (empresaPermissaoContratos.UsuariosEdicao != null && empresaPermissaoContratos.UsuariosEdicao.Count > 0)
                            {
                                if (!empresaPermissaoContratos.UsuariosEdicao.Contains(usuario))
                                    empresaPermissaoContratos.UsuariosEdicao.Add(usuario);
                            }
                        }
                    }

                    empresaPermissaoContratos.Empresa = empresa;
                    empresaPermissaoContratos.ModuloPermissao = moduloPermissao;

                    empresaPermissaoContratos = empresaPermissaoContratos.Salvar();

                    break;


                case "S":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.PORSETOR;

                    Setor setor = Setor.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    setor.UsuariosEdicao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        setor.UsuariosEdicao = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            setor.UsuariosEdicao.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0)
                            {
                                if (!setor.UsuariosEdicao.Contains(usuario))
                                    setor.UsuariosEdicao.Add(usuario);
                            }
                        }
                    }

                    setor = setor.Salvar();

                    break;
            }
        }

        if (lblNomeModuloEdicao.Text == "Diversos")
        {
            switch (hfTipoConfiguracaoEdicao.Value)
            {
                case "G":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                    configuracaoModulo.UsuariosEdicaoModuloGeral = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        configuracaoModulo.UsuariosEdicaoModuloGeral = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            configuracaoModulo.UsuariosEdicaoModuloGeral.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (configuracaoModulo.UsuariosVisualizacaoModuloGeral != null && configuracaoModulo.UsuariosVisualizacaoModuloGeral.Count > 0)
                            {
                                if (!configuracaoModulo.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                                    configuracaoModulo.UsuariosVisualizacaoModuloGeral.Add(usuario);
                            }
                        }
                    }

                    break;


                case "E":

                    configuracaoModulo.Tipo = ConfiguracaoPermissaoModulo.POREMPRESA;

                    Empresa empresa = Empresa.ConsultarPorId(hfIdObjetoEdicao.Value.ToInt32());

                    EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, hfIdModuloPermissaoEdicao.Value.ToInt32());

                    if (empresaPermissaoDiversos == null)
                        empresaPermissaoDiversos = new EmpresaModuloPermissao();

                    empresaPermissaoDiversos.UsuariosEdicao = null;

                    if (itensSelecionados != null && itensSelecionados.Count > 0)
                    {
                        empresaPermissaoDiversos.UsuariosEdicao = new List<Usuario>();

                        //percorrendo os usuarios editores selecionados
                        foreach (ListItem item in itensSelecionados)
                        {
                            Usuario usuario = Usuario.ConsultarPorId(item.Value.ToInt32());

                            empresaPermissaoDiversos.UsuariosEdicao.Add(usuario);

                            //Se o usuarios não estiver na lista de usuários de visualização ele deve ser adicionado
                            if (empresaPermissaoDiversos.UsuariosEdicao != null && empresaPermissaoDiversos.UsuariosEdicao.Count > 0)
                            {
                                if (!empresaPermissaoDiversos.UsuariosEdicao.Contains(usuario))
                                    empresaPermissaoDiversos.UsuariosEdicao.Add(usuario);
                            }
                        }
                    }

                    empresaPermissaoDiversos.Empresa = empresa;
                    empresaPermissaoDiversos.ModuloPermissao = moduloPermissao;

                    empresaPermissaoDiversos = empresaPermissaoDiversos.Salvar();

                    break;

            }
        }

        configuracaoModulo = configuracaoModulo.Salvar();

        msg.CriarMensagem("Permissões de usuários salvas com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void SalvarPermissoes()
    {
        IList<ModuloPermissao> modulosDoGrupo = new List<ModuloPermissao>();

        if (this.grupoLogado != null && this.grupoLogado.Id > 0)
            modulosDoGrupo = this.grupoLogado.ConsultarPorId().ModulosPermissao;
        else
            modulosDoGrupo = ModuloPermissao.ConsultarTodos();

        ModuloPermissao moduloAux = null;

        if (modulosDoGrupo != null && modulosDoGrupo.Count > 0)
        {
            Dictionary<string, StringBuilder> conteudoEmailsParaUsuarios = new Dictionary<string, StringBuilder>();

            IList<Usuario> usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

            //Adicionando todos os usuários na lista
            foreach (Usuario usuario in usuarios)
            {
                conteudoEmailsParaUsuarios.Add(usuario.Id.ToString(), new StringBuilder().Append(""));
            }

            IList<Usuario> usuariosAux;


            //Salvando Configuracao Modulo Geral
            moduloAux = ModuloPermissao.ConsultarPorNome("Geral");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, ModuloPermissao.ConsultarPorNome("Geral").Id);
                else
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Geral").Id);

                if (configuracaoModuloGeral == null)
                    configuracaoModuloGeral = new ConfiguracaoPermissaoModulo();

                configuracaoModuloGeral.Tipo = ConfiguracaoPermissaoModulo.GERAL;

                configuracaoModuloGeral.ModuloPermissao = moduloAux;

                configuracaoModuloGeral = configuracaoModuloGeral.Salvar();

                //Pegando o usuario com permissao de visualizar e editar o modulo geral
                if (configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Count == 0)
                    usuariosAux = usuarios;
                else
                    usuariosAux = configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral;

                foreach (Usuario usuario in usuariosAux)
                {
                    if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Contains(usuario))
                        conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Módulo Geral</strong> - Permissão de Edição e Visualização<br />");
                    else
                        conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Módulo Geral</strong> - Permissão de Visualização<br />");
                }
            }

            //Salvando Configuracao Modulo DNPM
            moduloAux = ModuloPermissao.ConsultarPorNome("DNPM");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloDNPM = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, moduloAux.Id);
                else
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloDNPM == null)
                    configuracaoModuloDNPM = new ConfiguracaoPermissaoModulo();

                configuracaoModuloDNPM.Tipo = Convert.ToChar(ddlTipoConfiguracaoModuloDNPM.SelectedValue);

                if (ddlTipoConfiguracaoModuloDNPM.SelectedValue == "G")
                {
                    //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                    if (configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count == 0)
                        usuariosAux = usuarios;
                    else
                        usuariosAux = configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral;

                    foreach (Usuario usuario in usuariosAux)
                    {
                        if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(usuario))
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo DNPM</strong> - Permissão de Edição e Visualização<br />");
                        else
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo DNPM</strong> - Permissão de Visualização<br />");
                    }
                }

                //Salvando todas as empresas permissoes do modulo DNPM
                if (ddlTipoConfiguracaoModuloDNPM.SelectedValue == "E")
                {
                    if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
                    {
                        foreach (Empresa empresa in this.EmpresasPermissoes)
                        {
                            empresa.ConsultarPorId();
                            EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);

                            if (empresaPermissaoDNPM == null)
                                empresaPermissaoDNPM = new EmpresaModuloPermissao();

                            empresaPermissaoDNPM.Empresa = empresa;
                            empresaPermissaoDNPM.ModuloPermissao = ModuloPermissao.ConsultarPorNome("DNPM");

                            empresaPermissaoDNPM = empresaPermissaoDNPM.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                            if (empresaPermissaoDNPM.UsuariosVisualizacao == null || empresaPermissaoDNPM.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = empresaPermissaoDNPM.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes da primeira empresa
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo DNPM (Controle por Empresa)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo DNPM (Controle por Empresa)</strong><br />");

                                if (empresaPermissaoDNPM.UsuariosEdicao != null && empresaPermissaoDNPM.UsuariosEdicao.Count > 0 && empresaPermissaoDNPM.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");

                            }
                        }
                    }
                }

                //Salvando todos os processos do modulo DNPM
                if (ddlTipoConfiguracaoModuloDNPM.SelectedValue == "P")
                {
                    if (this.ProcessosPermissoesDNPM != null && this.ProcessosPermissoesDNPM.Count > 0)
                    {
                        foreach (ProcessoDNPM objetoProcesso in this.ProcessosPermissoesDNPM)
                        {
                            ProcessoDNPM processoAux = ProcessoDNPM.ConsultarPorId(objetoProcesso.Id);

                            processoAux = processoAux.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                            if (processoAux.UsuariosVisualizacao == null || processoAux.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = processoAux.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes do primeiro processo
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo DNPM (Controle por Processo)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo DNPM (Controle por Processo)</strong><br />");

                                if (processoAux.UsuariosEdicao != null && processoAux.UsuariosEdicao.Count > 0 && processoAux.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Processo:</strong> " + processoAux.GetNumeroProcessoComMascara + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Processo:</strong> " + processoAux.GetNumeroProcessoComMascara + " - Permissão de Visualização<br />");
                            }
                        }
                    }
                }

                configuracaoModuloDNPM.ModuloPermissao = moduloAux;

                configuracaoModuloDNPM = configuracaoModuloDNPM.Salvar();

                this.LimparListasUsuariosOutrosTiposConfiguracaoDNPM(configuracaoModuloDNPM, ddlTipoConfiguracaoModuloDNPM.SelectedValue);
            }

            //Salvando Configuracao Modulo Meio Ambiente
            moduloAux = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, moduloAux.Id);
                else
                    configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloMeioAmbiente == null)
                    configuracaoModuloMeioAmbiente = new ConfiguracaoPermissaoModulo();

                configuracaoModuloMeioAmbiente.Tipo = Convert.ToChar(ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue);

                if (ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue == "G")
                {
                    //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                    if (configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count == 0)
                        usuariosAux = usuarios;
                    else
                        usuariosAux = configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral;

                    foreach (Usuario usuario in usuariosAux)
                    {
                        if (configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(usuario))
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Meio Ambiente</strong> - Permissão de Edição e Visualização<br />");
                        else
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Meio Ambiente</strong> - Permissão de Visualização<br />");
                    }
                }

                //Salvando todas as empresas permissoes do modulo de meio ambiente
                if (ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue == "E")
                {
                    if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
                    {
                        foreach (Empresa empresa in this.EmpresasPermissoes)
                        {
                            empresa.ConsultarPorId();
                            EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);

                            if (empresaPermissaoMeioAmbiente == null)
                                empresaPermissaoMeioAmbiente = new EmpresaModuloPermissao();

                            empresaPermissaoMeioAmbiente.Empresa = empresa;
                            empresaPermissaoMeioAmbiente.ModuloPermissao = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

                            empresaPermissaoMeioAmbiente = empresaPermissaoMeioAmbiente.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                            if (empresaPermissaoMeioAmbiente.UsuariosVisualizacao == null || empresaPermissaoMeioAmbiente.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = empresaPermissaoMeioAmbiente.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes da primeira empresa
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Empresa)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Meio Ambiente (Controle por Empresa)</strong><br />");

                                if (empresaPermissaoMeioAmbiente.UsuariosEdicao != null && empresaPermissaoMeioAmbiente.UsuariosEdicao.Count > 0 && empresaPermissaoMeioAmbiente.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");

                            }
                        }
                    }
                }

                //Salvando todos os processos do modulo de meio ambiente
                if (ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue == "P")
                {
                    //Processos
                    if (this.ProcessosPermissoes != null && this.ProcessosPermissoes.Count > 0)
                    {
                        foreach (Processo objetoProcesso in this.ProcessosPermissoes)
                        {
                            Processo processoAux = Processo.ConsultarPorId(objetoProcesso.Id);

                            processoAux = processoAux.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                            if (processoAux.UsuariosVisualizacao == null || processoAux.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = processoAux.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes do primeiro processo
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />");

                                if (processoAux.UsuariosEdicao != null && processoAux.UsuariosEdicao.Count > 0 && processoAux.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Processo:</strong> " + processoAux.Numero + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Processo:</strong> " + processoAux.Numero + " - Permissão de Visualização<br />");
                            }
                        }
                    }


                    //Cadastros Tecnicos
                    if (this.CadastrosTecnicosPermissoes != null && this.CadastrosTecnicosPermissoes.Count > 0)
                    {
                        foreach (CadastroTecnicoFederal objetoProcesso in this.CadastrosTecnicosPermissoes)
                        {
                            CadastroTecnicoFederal cadastroAux = CadastroTecnicoFederal.ConsultarPorId(objetoProcesso.Id);

                            cadastroAux = cadastroAux.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                            if (cadastroAux.UsuariosVisualizacao == null || cadastroAux.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = cadastroAux.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes do primeiro cadastro
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />");

                                if (cadastroAux.UsuariosEdicao != null && cadastroAux.UsuariosEdicao.Count > 0 && cadastroAux.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Cadastro Técnico Federal:</strong> " + cadastroAux.GetNomeEmpresa + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Cadastro Técnico Federal:</strong> " + cadastroAux.GetNomeEmpresa + " - Permissão de Visualização<br />");
                            }
                        }
                    }


                    //Outros de Empresa
                    if (this.OutrosEmpresasPermissoes != null && this.OutrosEmpresasPermissoes.Count > 0)
                    {
                        foreach (OutrosEmpresa objetoProcesso in this.OutrosEmpresasPermissoes)
                        {
                            OutrosEmpresa outrosAux = OutrosEmpresa.ConsultarPorId(objetoProcesso.Id);

                            outrosAux = outrosAux.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                            if (outrosAux.UsuariosVisualizacao == null || outrosAux.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = outrosAux.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes do primeiro outro de empresa
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />");

                                if (outrosAux.UsuariosEdicao != null && outrosAux.UsuariosEdicao.Count > 0 && outrosAux.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Outro de Empresa:</strong> " + outrosAux.GetNomeEmpresa + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Outro de Empresa:</strong> " + outrosAux.GetNomeEmpresa + " - Permissão de Visualização<br />");
                            }
                        }
                    }

                }

                configuracaoModuloMeioAmbiente.ModuloPermissao = moduloAux;

                configuracaoModuloMeioAmbiente = configuracaoModuloMeioAmbiente.Salvar();

                this.LimparListasUsuariosOutrosTiposConfiguracaoMeioAmbiente(configuracaoModuloMeioAmbiente, ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue);
            }

            //Salvando Configuracao Modulo Contratos
            moduloAux = ModuloPermissao.ConsultarPorNome("Contratos");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloContratos = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, moduloAux.Id);
                else
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloContratos == null)
                    configuracaoModuloContratos = new ConfiguracaoPermissaoModulo();

                configuracaoModuloContratos.Tipo = Convert.ToChar(ddlTipoConfiguracaoModuloContratos.SelectedValue);

                if (ddlTipoConfiguracaoModuloContratos.SelectedValue == "G")
                {
                    //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                    if (configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count == 0)
                        usuariosAux = usuarios;
                    else
                        usuariosAux = configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral;

                    foreach (Usuario usuario in usuariosAux)
                    {
                        if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(usuario))
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Contratos</strong> - Permissão de Edição e Visualização<br />");
                        else
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Contratos</strong> - Permissão de Visualização<br />");
                    }
                }


                //Salvando todas as empresas permissoes do modulo contratos
                if (ddlTipoConfiguracaoModuloContratos.SelectedValue == "E")
                {
                    if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
                    {
                        foreach (Empresa empresa in this.EmpresasPermissoes)
                        {
                            empresa.ConsultarPorId();
                            EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);

                            if (empresaPermissaoContratos == null)
                                empresaPermissaoContratos = new EmpresaModuloPermissao();

                            empresaPermissaoContratos.Empresa = empresa;
                            empresaPermissaoContratos.ModuloPermissao = ModuloPermissao.ConsultarPorNome("Contratos");

                            empresaPermissaoContratos = empresaPermissaoContratos.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                            if (empresaPermissaoContratos.UsuariosVisualizacao == null || empresaPermissaoContratos.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = empresaPermissaoContratos.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes da primeira empresa
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Contratos (Controle por Empresa)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Contratos (Controle por Empresa)</strong><br />");

                                if (empresaPermissaoContratos.UsuariosEdicao != null && empresaPermissaoContratos.UsuariosEdicao.Count > 0 && empresaPermissaoContratos.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");
                            }
                        }
                    }
                }


                //Salvando todos os setores permissoes do modulo contratos
                if (ddlTipoConfiguracaoModuloContratos.SelectedValue == "S")
                {
                    if (this.SetoresPermissoes != null && this.SetoresPermissoes.Count > 0)
                    {
                        foreach (Setor setor in this.SetoresPermissoes)
                        {
                            Setor setorAux = Setor.ConsultarPorId(setor.Id);

                            setorAux = setorAux.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                            if (setorAux.UsuariosVisualizacao == null || setorAux.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = setorAux.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes do primeiro setor
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Contratos (Controle por Setor)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Contratos (Controle por Setor)</strong><br />");

                                if (setorAux.UsuariosEdicao != null && setorAux.UsuariosEdicao.Count > 0 && setorAux.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Setor:</strong> " + setorAux.Nome + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Setor:</strong> " + setorAux.Nome + " - Permissão de Visualização<br />");
                            }
                        }
                    }
                }

                configuracaoModuloContratos.ModuloPermissao = moduloAux;

                configuracaoModuloContratos = configuracaoModuloContratos.Salvar();

                this.LimparListasUsuariosOutrosTiposConfiguracaoContratos(configuracaoModuloContratos, ddlTipoConfiguracaoModuloContratos.SelectedValue);
            }

            //Salvando Configuracao Modulo Diversos
            moduloAux = ModuloPermissao.ConsultarPorNome("Diversos");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloDiversos = null;

                if (this.grupoLogado != null && this.grupoLogado.Id > 0)
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.grupoLogado.Id, moduloAux.Id);
                else
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloDiversos == null)
                    configuracaoModuloDiversos = new ConfiguracaoPermissaoModulo();

                configuracaoModuloDiversos.Tipo = Convert.ToChar(ddlTipoConfiguracaoModuloDiversos.SelectedValue);

                if (ddlTipoConfiguracaoModuloDiversos.SelectedValue == "G")
                {
                    //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                    if (configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count == 0)
                        usuariosAux = usuarios;
                    else
                        usuariosAux = configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral;

                    foreach (Usuario usuario in usuariosAux)
                    {
                        if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(usuario))
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Diversos</strong> - Permissão de Edição e Visualização<br />");
                        else
                            conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Diversos</strong> - Permissão de Visualização<br />");
                    }
                }

                //Salvando todas as empresas permissoes do modulo diversos
                if (ddlTipoConfiguracaoModuloDiversos.SelectedValue == "E")
                {
                    if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
                    {
                        foreach (Empresa empresa in this.EmpresasPermissoes)
                        {
                            empresa.ConsultarPorId();
                            EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);

                            if (empresaPermissaoDiversos == null)
                                empresaPermissaoDiversos = new EmpresaModuloPermissao();

                            empresaPermissaoDiversos.Empresa = empresa;
                            empresaPermissaoDiversos.ModuloPermissao = ModuloPermissao.ConsultarPorNome("Diversos");

                            empresaPermissaoDiversos = empresaPermissaoDiversos.Salvar();

                            //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                            if (empresaPermissaoDiversos.UsuariosVisualizacao == null || empresaPermissaoDiversos.UsuariosVisualizacao.Count == 0)
                                usuariosAux = usuarios;
                            else
                                usuariosAux = empresaPermissaoDiversos.UsuariosVisualizacao;

                            foreach (Usuario usuario in usuariosAux)
                            {
                                //Adiciona o titulo somente antes da primeira empresa
                                if (conteudoEmailsParaUsuarios[usuario.Id.ToString()].ToString().Contains("<br /><strong>Módulo Diversos (Controle por Empresa)</strong><br />") == false)
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<br /><strong>Módulo Diversos (Controle por Empresa)</strong><br />");

                                if (empresaPermissaoDiversos.UsuariosEdicao != null && empresaPermissaoDiversos.UsuariosEdicao.Count > 0 && empresaPermissaoDiversos.UsuariosEdicao.Contains(usuario))
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                else
                                    conteudoEmailsParaUsuarios[usuario.Id.ToString()].Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");

                            }
                        }
                    }
                }

                configuracaoModuloDiversos.ModuloPermissao = moduloAux;

                configuracaoModuloDiversos = configuracaoModuloDiversos.Salvar();

                this.LimparListasUsuariosOutrosTiposConfiguracaoDiversos(configuracaoModuloDiversos, ddlTipoConfiguracaoModuloDiversos.SelectedValue);
            }


            //Enviando email para os Usuários com suas respectivas permissoes
            if (conteudoEmailsParaUsuarios != null && conteudoEmailsParaUsuarios.Count > 0)
            {
                foreach (KeyValuePair<string, StringBuilder> aaa in conteudoEmailsParaUsuarios)
                {
                    Usuario usuario = Usuario.ConsultarPorId(aaa.Key.ToInt32());

                    if (usuario != null && aaa.Value != null && aaa.Value.ToString() != "")
                    {
                        Email email = new Email();
                        email.Assunto = "Permissões de Usuário - Sistema Sustentar";

                        if (usuario.Email != null && usuario.Email != "")
                            email.EmailsDestino.Add(usuario.Email);

                        if (usuario.EmailSeguranca != null && usuario.EmailSeguranca != "")
                            email.EmailsDestino.Add(usuario.EmailSeguranca);

                        String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
                        <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
                        <div style='float:left; margin-left:65px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>
                        Alteração nas Permissões de Usuário<br/>Sistema Sustentar</div>
                        <div style='width:100%; height:20px; clear:both'></div>
                        <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; height:auto'>
                        Prezado " + usuario.Nome + @".</div>
                        <div style='margin-left:30px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; text-align:justify;font-size:14px;padding:7px; height:auto'>
                        Houve uma alteração em suas permissões de acesso às funcionalidades do Sistema Sustentar:<br />
                        <br /><strong>Permissões: </strong><br /><br />
                        " + aaa.Value.ToString() + @"<br />
                        Qualquer dúvida, entre em contato com nosso suporte em www.sustentar.inf.br<br /><br />
                        Ass. Equipe Sustentar.</div>
                        <div style='width:100%; height:20px;'></div></div>";

                        email.Mensagem = mensagemEmail;

                        if (email.EmailsDestino != null && email.EmailsDestino.Count > 0)
                            email.EnviarAutenticado(25, false);

                    }
                }
            }

            msg.CriarMensagem("Permissões salvas com sucesso", "Sucesso", MsgIcons.Sucesso);
        }

    }

    private void LimparListasUsuariosOutrosTiposConfiguracaoDNPM(ConfiguracaoPermissaoModulo configuracaoModuloDNPM, string tipoConfiguracao)
    {
        if (configuracaoModuloDNPM.Tipo != ConfiguracaoPermissaoModulo.GERAL)
        {
            configuracaoModuloDNPM.UsuariosEdicaoModuloGeral = null;
            configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral = null;
        }

        if (configuracaoModuloDNPM.Tipo != ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
            {
                foreach (object empresa in this.EmpresasPermissoes)
                {
                    Empresa empresaAux = (Empresa)empresa;

                    empresaAux = empresaAux.ConsultarPorId();

                    EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresaAux.Id, configuracaoModuloDNPM.ModuloPermissao.Id);

                    if (empresaPermissaoDNPM != null && empresaPermissaoDNPM.Id > 0)
                    {
                        empresaPermissaoDNPM.UsuariosEdicao = null;
                        empresaPermissaoDNPM.UsuariosVisualizacao = null;

                        empresaPermissaoDNPM = empresaPermissaoDNPM.Salvar();
                    }
                }
            }

        }


        if (configuracaoModuloDNPM.Tipo != ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (this.ProcessosPermissoesDNPM != null && this.ProcessosPermissoesDNPM.Count > 0)
            {
                foreach (object objetoProcesso in this.ProcessosPermissoesDNPM)
                {
                    ProcessoDNPM processoDnpm = (ProcessoDNPM)objetoProcesso;

                    processoDnpm = processoDnpm.ConsultarPorId();

                    if (processoDnpm != null && processoDnpm.Id > 0)
                    {
                        processoDnpm.UsuariosEdicao = null;
                        processoDnpm.UsuariosVisualizacao = null;

                        processoDnpm = processoDnpm.Salvar();
                    }
                }
            }
        }

        configuracaoModuloDNPM = configuracaoModuloDNPM.Salvar();
    }

    private void LimparListasUsuariosOutrosTiposConfiguracaoMeioAmbiente(ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente, string tipoConfiguracao)
    {

        if (configuracaoModuloMeioAmbiente.Tipo != ConfiguracaoPermissaoModulo.GERAL)
        {
            configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral = null;
            configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral = null;
        }

        if (configuracaoModuloMeioAmbiente.Tipo != ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
            {
                foreach (object empresa in this.EmpresasPermissoes)
                {
                    Empresa empresaAux = (Empresa)empresa;

                    empresaAux = empresaAux.ConsultarPorId();

                    EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresaAux.Id, configuracaoModuloMeioAmbiente.ModuloPermissao.Id);

                    if (empresaPermissaoMeioAmbiente != null && empresaPermissaoMeioAmbiente.Id > 0)
                    {
                        empresaPermissaoMeioAmbiente.UsuariosEdicao = null;
                        empresaPermissaoMeioAmbiente.UsuariosVisualizacao = null;

                        empresaPermissaoMeioAmbiente = empresaPermissaoMeioAmbiente.Salvar();
                    }
                }
            }

        }

        if (configuracaoModuloMeioAmbiente.Tipo != ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (this.ProcessosPermissoes != null && this.ProcessosPermissoes.Count > 0)
            {
                foreach (object objetoProcesso in this.ProcessosPermissoes)
                {
                    if (objetoProcesso.GetType() == typeof(Processo))
                    {
                        Processo processo = (Processo)objetoProcesso;

                        processo = processo.ConsultarPorId();

                        if (processo != null && processo.Id > 0)
                        {
                            processo.UsuariosEdicao = null;
                            processo.UsuariosVisualizacao = null;

                            processo.Salvar();
                        }
                    }


                    if (objetoProcesso.GetType() == typeof(CadastroTecnicoFederal))
                    {
                        CadastroTecnicoFederal cadastro = (CadastroTecnicoFederal)objetoProcesso;

                        cadastro = cadastro.ConsultarPorId();

                        if (cadastro != null && cadastro.Id > 0)
                        {
                            cadastro.UsuariosEdicao = null;
                            cadastro.UsuariosVisualizacao = null;

                            cadastro.Salvar();
                        }
                    }


                    if (objetoProcesso.GetType() == typeof(OutrosEmpresa))
                    {
                        OutrosEmpresa outro = (OutrosEmpresa)objetoProcesso;

                        outro = outro.ConsultarPorId();

                        if (outro != null && outro.Id > 0)
                        {
                            outro.UsuariosEdicao = null;
                            outro.UsuariosVisualizacao = null;

                            outro.Salvar();
                        }
                    }

                }
            }
        }

        configuracaoModuloMeioAmbiente = configuracaoModuloMeioAmbiente.Salvar();
    }

    private void LimparListasUsuariosOutrosTiposConfiguracaoContratos(ConfiguracaoPermissaoModulo configuracaoModuloContratos, string tipoConfiguracao)
    {
        if (configuracaoModuloContratos.Tipo != ConfiguracaoPermissaoModulo.GERAL)
        {
            configuracaoModuloContratos.UsuariosEdicaoModuloGeral = null;
            configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral = null;
        }

        if (configuracaoModuloContratos.Tipo != ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
            {
                foreach (object empresa in this.EmpresasPermissoes)
                {
                    Empresa empresaAux = (Empresa)empresa;

                    empresaAux = empresaAux.ConsultarPorId();

                    EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresaAux.Id, configuracaoModuloContratos.ModuloPermissao.Id);

                    if (empresaPermissaoContratos != null && empresaPermissaoContratos.Id > 0)
                    {
                        empresaPermissaoContratos.UsuariosEdicao = null;
                        empresaPermissaoContratos.UsuariosVisualizacao = null;

                        empresaPermissaoContratos = empresaPermissaoContratos.Salvar();
                    }
                }
            }

        }

        if (configuracaoModuloContratos.Tipo != ConfiguracaoPermissaoModulo.PORSETOR)
        {
            if (this.SetoresPermissoes != null && this.SetoresPermissoes.Count > 0)
            {
                foreach (object objeto in this.SetoresPermissoes)
                {
                    Setor setor = (Setor)objeto;

                    setor = setor.ConsultarPorId();

                    if (setor != null && setor.Id > 0)
                    {
                        setor.UsuariosEdicao = null;
                        setor.UsuariosVisualizacao = null;

                        setor.Salvar();
                    }
                }
            }
        }

        configuracaoModuloContratos = configuracaoModuloContratos.Salvar();
    }

    private void LimparListasUsuariosOutrosTiposConfiguracaoDiversos(ConfiguracaoPermissaoModulo configuracaoModuloDiversos, string tipoConfiguracao)
    {
        if (configuracaoModuloDiversos.Tipo != ConfiguracaoPermissaoModulo.GERAL)
        {
            configuracaoModuloDiversos.UsuariosEdicaoModuloGeral = null;
            configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral = null;
        }

        if (configuracaoModuloDiversos.Tipo != ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissoes != null && this.EmpresasPermissoes.Count > 0)
            {
                foreach (object empresa in this.EmpresasPermissoes)
                {
                    Empresa empresaAux = (Empresa)empresa;

                    empresaAux = empresaAux.ConsultarPorId();

                    EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresaAux.Id, configuracaoModuloDiversos.ModuloPermissao.Id);

                    if (empresaPermissaoDiversos != null && empresaPermissaoDiversos.Id > 0)
                    {
                        empresaPermissaoDiversos.UsuariosEdicao = null;
                        empresaPermissaoDiversos.UsuariosVisualizacao = null;

                        empresaPermissaoDiversos = empresaPermissaoDiversos.Salvar();
                    }
                }
            }
        }

        configuracaoModuloDiversos = configuracaoModuloDiversos.Salvar();
    }

    #endregion

    #region ____________________ Bindings ____________________

    //Usuarios visualização DNPM
    public string BindEmpresaVisualizacaoModuloDNPM(object o)
    {
        if (o.GetType() == typeof(ProcessoDNPM))
        {
            ProcessoDNPM processoAux = (ProcessoDNPM)o;

            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(processoAux.Id);

            return processo != null && processo.Empresa != null ? processo.Empresa.Nome + " - " + processo.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(Empresa))
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        return "";
    }

    public string BindProcessoVisualizacaoModuloDNPM(object o)
    {
        ProcessoDNPM processoAux = (ProcessoDNPM)o;

        ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(processoAux.Id);

        return processo != null ? processo.GetNumeroProcessoComMascara : "";
    }

    public string BindUsuariosVisualizacaoModuloDNPM(object o)
    {
        if (o.GetType() == typeof(ProcessoDNPM))
        {
            ProcessoDNPM processoAux = (ProcessoDNPM)o;

            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(processoAux.Id);

            string retorno = "";

            if (processo != null && processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0)
            {
                foreach (Usuario item in processo.UsuariosVisualizacao)
                {
                    if (item == processo.UsuariosVisualizacao[processo.UsuariosVisualizacao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        if (o.GetType() == typeof(Empresa))
        {

            string retorno = "";

            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            if (empresa != null)
            {
                EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);

                if (empresaPermissaoDNPM != null && empresaPermissaoDNPM.UsuariosVisualizacao != null && empresaPermissaoDNPM.UsuariosVisualizacao.Count > 0)
                {
                    foreach (Usuario item in empresaPermissaoDNPM.UsuariosVisualizacao)
                    {
                        if (item == empresaPermissaoDNPM.UsuariosVisualizacao[empresaPermissaoDNPM.UsuariosVisualizacao.Count - 1])
                            retorno += item.Nome;
                        else
                            retorno += item.Nome + ", ";
                    }
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        return "Todos";
    }


    //Usuarios Edição DNPM
    public string BindEmpresaEdicaoModuloDNPM(object o)
    {
        if (o.GetType() == typeof(ProcessoDNPM))
        {
            ProcessoDNPM processoAux = (ProcessoDNPM)o;

            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(processoAux.Id);

            return processo != null && processo.Empresa != null ? processo.Empresa.Nome + " - " + processo.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(Empresa))
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        return "";
    }

    public string BindProcessoEdicaoModuloDNPM(object o)
    {
        ProcessoDNPM processo = (ProcessoDNPM)o;

        ProcessoDNPM processoAux = ProcessoDNPM.ConsultarPorId(processo.Id);

        return processoAux != null ? processoAux.GetNumeroProcessoComMascara : "";
    }

    public string BindUsuariosEdicaoModuloDNPM(object o)
    {
        string retorno = "";

        if (o.GetType() == typeof(ProcessoDNPM))
        {
            ProcessoDNPM processoAux = (ProcessoDNPM)o;

            ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(processoAux.Id);

            if (processo != null && processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
            {
                foreach (Usuario item in processo.UsuariosEdicao)
                {
                    if (item == processo.UsuariosEdicao[processo.UsuariosEdicao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

        }

        if (o.GetType() == typeof(Empresa))
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            if (empresa != null)
            {
                EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);

                if (empresaPermissaoDNPM != null && empresaPermissaoDNPM.UsuariosEdicao != null && empresaPermissaoDNPM.UsuariosEdicao.Count > 0)
                {
                    foreach (Usuario item in empresaPermissaoDNPM.UsuariosEdicao)
                    {
                        if (item == empresaPermissaoDNPM.UsuariosEdicao[empresaPermissaoDNPM.UsuariosEdicao.Count - 1])
                            retorno += item.Nome;
                        else
                            retorno += item.Nome + ", ";
                    }
                }

            }
        }

        return retorno == "" ? "Não definido" : retorno;
    }


    //Usuarios visualização Meio Ambiente
    public string BindEmpresaVisualizacaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            Processo processoAux = (Processo)o;
            
            Processo processo = Processo.ConsultarPorId(processoAux.Id);

            return processo != null && processo.Empresa != null ? processo.Empresa.Nome + " - " + processo.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            CadastroTecnicoFederal cadAux = (CadastroTecnicoFederal)o;

            CadastroTecnicoFederal cadastroTecFed = CadastroTecnicoFederal.ConsultarPorId(cadAux.Id);

            return cadastroTecFed != null && cadastroTecFed.Empresa != null ? cadastroTecFed.Empresa.Nome + " - " + cadastroTecFed.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            OutrosEmpresa outrosEmpAux = (OutrosEmpresa)o;

            OutrosEmpresa outrosEmp = OutrosEmpresa.ConsultarPorId(outrosEmpAux.Id);

            return outrosEmp != null && outrosEmp.Empresa != null ? outrosEmp.Empresa.Nome + " - " + outrosEmp.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(Empresa))
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        return "";
    }

    public string BindProcessoVisualizacaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            Processo processoAux = (Processo)o;

            Processo processo = Processo.ConsultarPorId(processoAux.Id);

            return processo != null ? processo.Numero : "";
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            return "Cadastro Técnico Federal";
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            return "Outros";
        }

        return "";
    }

    public string BindTipoProcessoVisualizacaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            return "Processo";
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            return "Cadastro Técnico Federal";
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            return "Outros";
        }

        return "";
    }

    public string BindUsuariosVisualizacaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            string retorno = "";

            Processo processoAux = (Processo)o;

            Processo processo = Processo.ConsultarPorId(processoAux.Id);

            if (processo != null && processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0)
            {
                foreach (Usuario item in processo.UsuariosVisualizacao)
                {
                    if (item == processo.UsuariosVisualizacao[processo.UsuariosVisualizacao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            string retorno = "";

            CadastroTecnicoFederal cadAux = (CadastroTecnicoFederal)o;

            CadastroTecnicoFederal cadastroTecFed = CadastroTecnicoFederal.ConsultarPorId(cadAux.Id);

            if (cadastroTecFed != null && cadastroTecFed.UsuariosVisualizacao != null && cadastroTecFed.UsuariosVisualizacao.Count > 0)
            {
                foreach (Usuario item in cadastroTecFed.UsuariosVisualizacao)
                {
                    if (item == cadastroTecFed.UsuariosVisualizacao[cadastroTecFed.UsuariosVisualizacao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            string retorno = "";

            OutrosEmpresa outrosEmpAux = (OutrosEmpresa)o;

            OutrosEmpresa outrosEmp = OutrosEmpresa.ConsultarPorId(outrosEmpAux.Id);

            if (outrosEmp != null && outrosEmp.UsuariosVisualizacao != null && outrosEmp.UsuariosVisualizacao.Count > 0)
            {
                foreach (Usuario item in outrosEmp.UsuariosVisualizacao)
                {
                    if (item == outrosEmp.UsuariosVisualizacao[outrosEmp.UsuariosVisualizacao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        if (o.GetType() == typeof(Empresa))
        {
            string retorno = "";

            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            if (empresa != null)
            {
                EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);

                if (empresaPermissaoMeioAmbiente != null && empresaPermissaoMeioAmbiente.UsuariosVisualizacao != null && empresaPermissaoMeioAmbiente.UsuariosVisualizacao.Count > 0)
                {
                    foreach (Usuario item in empresaPermissaoMeioAmbiente.UsuariosVisualizacao)
                    {
                        if (item == empresaPermissaoMeioAmbiente.UsuariosVisualizacao[empresaPermissaoMeioAmbiente.UsuariosVisualizacao.Count - 1])
                            retorno += item.Nome;
                        else
                            retorno += item.Nome + ", ";
                    }
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        return "Todos";
    }


    //Usuarios Edição Meio Ambiente
    public string BindEmpresaEdicaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            Processo processoAux = (Processo)o;

            Processo processo = Processo.ConsultarPorId(processoAux.Id);

            return processo != null && processo.Empresa != null ? processo.Empresa.Nome + " - " + processo.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            CadastroTecnicoFederal cadAux = (CadastroTecnicoFederal)o;

            CadastroTecnicoFederal cadastroTecFed = CadastroTecnicoFederal.ConsultarPorId(cadAux.Id);

            return cadastroTecFed != null && cadastroTecFed.Empresa != null ? cadastroTecFed.Empresa.Nome + " - " + cadastroTecFed.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            OutrosEmpresa outrosEmpAux = (OutrosEmpresa)o;

            OutrosEmpresa outrosEmp = OutrosEmpresa.ConsultarPorId(outrosEmpAux.Id);

            return outrosEmp != null && outrosEmp.Empresa != null ? outrosEmp.Empresa.Nome + " - " + outrosEmp.Empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        if (o.GetType() == typeof(Empresa))        
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
        }

        return "";
    }

    public string BindProcessoEdicaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            Processo processoAux = (Processo)o;

            Processo processo = Processo.ConsultarPorId(processoAux.Id);
            return processo != null ? processo.Numero : "";
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            return "Cadastro Técnico Federal";
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            return "Outros";
        }

        return "";
    }

    public string BindTipoProcessoEdicaoModuloMeioAmbiente(object o)
    {
        if (o.GetType() == typeof(Processo))
        {
            return "Processo";
        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            return "Cadastro Técnico Federal";
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            return "Outros";
        }

        return "";
    }

    public string BindUsuariosEdicaoModuloMeioAmbiente(object o)
    {
        string retorno = "";

        if (o.GetType() == typeof(Processo))
        {
            Processo processoAux = (Processo)o;

            Processo processo = Processo.ConsultarPorId(processoAux.Id);

            if (processo != null && processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
            {
                foreach (Usuario item in processo.UsuariosEdicao)
                {
                    if (item == processo.UsuariosEdicao[processo.UsuariosEdicao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Não definido" : retorno;

        }

        if (o.GetType() == typeof(CadastroTecnicoFederal))
        {
            CadastroTecnicoFederal cadAux = (CadastroTecnicoFederal)o;

            CadastroTecnicoFederal cadastroTecFed = CadastroTecnicoFederal.ConsultarPorId(cadAux.Id);

            if (cadastroTecFed != null && cadastroTecFed.UsuariosEdicao != null && cadastroTecFed.UsuariosEdicao.Count > 0)
            {
                foreach (Usuario item in cadastroTecFed.UsuariosEdicao)
                {
                    if (item == cadastroTecFed.UsuariosEdicao[cadastroTecFed.UsuariosEdicao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Não definido" : retorno;
        }

        if (o.GetType() == typeof(OutrosEmpresa))
        {
            OutrosEmpresa outrosEmpAux = (OutrosEmpresa)o;

            OutrosEmpresa outrosEmp = OutrosEmpresa.ConsultarPorId(outrosEmpAux.Id);

            if (outrosEmp != null && outrosEmp.UsuariosEdicao != null && outrosEmp.UsuariosEdicao.Count > 0)
            {
                foreach (Usuario item in outrosEmp.UsuariosEdicao)
                {
                    if (item == outrosEmp.UsuariosEdicao[outrosEmp.UsuariosEdicao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Não definido" : retorno;
        }

        if (o.GetType() == typeof(Empresa))
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            if (empresa != null)
            {
                EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);

                if (empresaPermissaoMeioAmbiente != null && empresaPermissaoMeioAmbiente.UsuariosEdicao != null && empresaPermissaoMeioAmbiente.UsuariosEdicao.Count > 0)
                {
                    foreach (Usuario item in empresaPermissaoMeioAmbiente.UsuariosEdicao)
                    {
                        if (item == empresaPermissaoMeioAmbiente.UsuariosEdicao[empresaPermissaoMeioAmbiente.UsuariosEdicao.Count - 1])
                            retorno += item.Nome;
                        else
                            retorno += item.Nome + ", ";
                    }
                }

                return retorno == "" ? "Não definido" : retorno;
            }

        }

        return "Não definido";
    }


    //Usuarios visualização Contratos
    public string BindEmpresaVisualizacaoModuloContratos(object o)
    {
        Empresa empresaAux = (Empresa)o;
        Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);
        return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
    }

    public string BindSetoresVisualizacaoModuloContratos(object o)
    {
        Setor setorAux = (Setor)o;
        Setor setor = Setor.ConsultarPorId(setorAux.Id);
        return setor != null ? setor.Nome : "";
    }

    public string BindUsuariosVisualizacaoModuloContratos(object o)
    {
        if (o.GetType() == typeof(Setor))
        {
            string retorno = "";

            Setor setorAux = (Setor)o;
            Setor setor = Setor.ConsultarPorId(setorAux.Id);

            if (setor != null && setor.UsuariosVisualizacao != null && setor.UsuariosVisualizacao.Count > 0)
            {
                foreach (Usuario item in setor.UsuariosVisualizacao)
                {
                    if (item == setor.UsuariosVisualizacao[setor.UsuariosVisualizacao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        if (o.GetType() == typeof(Empresa))
        {
            string retorno = "";

            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            if (empresa != null)
            {
                EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);

                if (empresaPermissaoContratos != null && empresaPermissaoContratos.UsuariosVisualizacao != null && empresaPermissaoContratos.UsuariosVisualizacao.Count > 0)
                {
                    foreach (Usuario item in empresaPermissaoContratos.UsuariosVisualizacao)
                    {
                        if (item == empresaPermissaoContratos.UsuariosVisualizacao[empresaPermissaoContratos.UsuariosVisualizacao.Count - 1])
                            retorno += item.Nome;
                        else
                            retorno += item.Nome + ", ";
                    }
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        return "Todos";
    }


    //Usuarios Edição Contratos
    public string BindEmpresaEdicaoModuloContratos(object o)
    {
        Empresa empresaAux = (Empresa)o;
        Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);
        return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
    }

    public string BindSetorEdicaoModuloContratos(object o)
    {
        Setor setorAux = (Setor)o;
        Setor setor = Setor.ConsultarPorId(setorAux.Id);
        return setor != null ? setor.Nome : "";
    }

    public string BindUsuariosEdicaoModuloContratos(object o)
    {
        string retorno = "";

        if (o.GetType() == typeof(Setor))
        {
            Setor setorAux = (Setor)o;
            Setor setor = Setor.ConsultarPorId(setorAux.Id);

            if (setor != null && setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0)
            {
                foreach (Usuario item in setor.UsuariosEdicao)
                {
                    if (item == setor.UsuariosEdicao[setor.UsuariosEdicao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }

        if (o.GetType() == typeof(Empresa))
        {
            Empresa empresaAux = (Empresa)o;

            Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

            if (empresa != null)
            {
                EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);

                if (empresaPermissaoContratos != null && empresaPermissaoContratos.UsuariosEdicao != null && empresaPermissaoContratos.UsuariosEdicao.Count > 0)
                {
                    foreach (Usuario item in empresaPermissaoContratos.UsuariosEdicao)
                    {
                        if (item == empresaPermissaoContratos.UsuariosEdicao[empresaPermissaoContratos.UsuariosEdicao.Count - 1])
                            retorno += item.Nome;
                        else
                            retorno += item.Nome + ", ";
                    }
                }

                return retorno == "" ? "Todos" : retorno;
            }
        }

        return "Não definido";
    }


    //Usuarios visualização Diversos
    public string BindEmpresaVisualizacaoModuloDiversos(object o)
    {
        Empresa empresaAux = (Empresa)o;

        Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

        return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";

    }

    public string BindUsuariosVisualizacaoModuloDiversos(object o)
    {
        string retorno = "";

        Empresa empresaAux = (Empresa)o;

        Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

        if (empresa != null)
        {
            EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);

            if (empresaPermissaoDiversos != null && empresaPermissaoDiversos.UsuariosVisualizacao != null && empresaPermissaoDiversos.UsuariosVisualizacao.Count > 0)
            {
                foreach (Usuario item in empresaPermissaoDiversos.UsuariosVisualizacao)
                {
                    if (item == empresaPermissaoDiversos.UsuariosVisualizacao[empresaPermissaoDiversos.UsuariosVisualizacao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }
        }

        return retorno == "" ? "Todos" : retorno;

    }


    //Usuarios Edição Diversos
    public string BindEmpresaEdicaoModuloDiversos(object o)
    {
        Empresa empresaAux = (Empresa)o;

        Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

        return empresa != null ? empresa.Nome + " - " + empresa.GetNumeroCNPJeCPFComMascara : "";
    }

    public string BindUsuariosEdicaoModuloDiversos(object o)
    {
        string retorno = "";

        Empresa empresaAux = (Empresa)o;

        Empresa empresa = Empresa.ConsultarPorId(empresaAux.Id);

        if (empresa != null)
        {
            EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);

            if (empresaPermissaoDiversos != null && empresaPermissaoDiversos.UsuariosEdicao != null && empresaPermissaoDiversos.UsuariosEdicao.Count > 0)
            {
                foreach (Usuario item in empresaPermissaoDiversos.UsuariosEdicao)
                {
                    if (item == empresaPermissaoDiversos.UsuariosEdicao[empresaPermissaoDiversos.UsuariosEdicao.Count - 1])
                        retorno += item.Nome;
                    else
                        retorno += item.Nome + ", ";
                }
            }

            return retorno == "" ? "Todos" : retorno;
        }


        return "Não definido";
    }

    public string BindTituloUsuariosEditores(object o)
    {
        if (this.grupoLogado != null && this.grupoLogado.Id > 0 && this.grupoLogado.GestaoCompartilhada)
            return "Usuário Nomeado/Consultor";

        return "Usuários Editores";
    }


    #endregion

    #region ____________________ Eventos _____________________

    #region _______________ SelectIndexChange _______________

    protected void ddlTipoConfiguracaoModuloDNPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.AlterarConfiguracaoPermissoesModuloDNPM();
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

    protected void ddlTipoConfiguracaoModuloMeioAmbiente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.AlterarConfiguracaoPermissoesModuloMeioAmbiente();
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

    protected void ddlTipoConfiguracaoModuloContratos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.AlterarConfiguracaoPermissoesModuloContratos();
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

    protected void ddlTipoConfiguracaoModuloDiversos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.AlterarConfiguracaoPermissoesModuloDiversos();
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

    protected void ddlQuantidaItensGridVisualizacaoDNPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlTipoConfiguracaoModuloDNPM.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.PageSize = ddlQuantidaItensGridVisualizacaoDNPM.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataSource = this.EmpresasPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataBind();

                    break;


                case "P":

                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.PageSize = ddlQuantidaItensGridVisualizacaoDNPM.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataSource = this.ProcessosPermissoesDNPM;
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataBind();

                    break;

                default:

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataSource = new List<Empresa>();
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataSource = new List<ProcessoDNPM>();
                    break;

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

    protected void ddlQuantidaItensGridVisualizacaoMA_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
            switch (ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.PageSize = ddlQuantidaItensGridVisualizacaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataSource = this.EmpresasPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataBind();

                    break;


                case "P":

                    //Processos
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.PageSize = ddlQuantidaItensGridVisualizacaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataSource = this.ProcessosPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataBind();

                    //Cadastros Tecnicos
                    grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.PageSize = ddlQuantidaItensGridVisualizacaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataSource = this.CadastrosTecnicosPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataBind();

                    //Outros de Empresa
                    grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.PageSize = ddlQuantidaItensGridVisualizacaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataSource = this.OutrosEmpresasPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataBind();

                    break;
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

    protected void ddlQuantidaItensGridVisualizacaoContratos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlTipoConfiguracaoModuloContratos.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.PageSize = ddlQuantidaItensGridVisualizacaoContratos.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataSource = this.EmpresasPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataBind();

                    break;


                case "S":

                    grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.PageIndex = 0;
                    grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.PageSize = ddlQuantidaItensGridVisualizacaoContratos.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataSource = this.SetoresPermissoes;
                    grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataBind();

                    break;

                default:

                    grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataSource = new List<Empresa>();
                    grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataSource = new List<Setor>();
                    break;

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

    protected void ddlQuantidaItensGridVisualizacaoDiversos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.PageIndex = 0;
            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.PageSize = ddlQuantidaItensGridVisualizacaoDiversos.SelectedValue.ToInt32();

            switch (ddlTipoConfiguracaoModuloDiversos.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataSource = this.EmpresasPermissoes;
                    break;
            }

            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataBind();
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

    protected void ddlQuantidaItensGridEdicaoDNPM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlTipoConfiguracaoModuloDNPM.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.PageSize = ddlQuantidaItensGridEdicaoDNPM.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataSource = this.EmpresasPermissoes;
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataBind();

                    break;


                case "P":

                    grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.PageSize = ddlQuantidaItensGridEdicaoDNPM.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataSource = this.ProcessosPermissoesDNPM;
                    grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataBind();

                    break;

                default:

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataSource = new List<Empresa>();
                    grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataSource = new List<ProcessoDNPM>();
                    break;

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

    protected void ddlQuantidaItensGridEdicaoMA_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.PageSize = ddlQuantidaItensGridEdicaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataSource = this.EmpresasPermissoes;
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataBind();

                    break;


                case "P":

                    //Processos
                    grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.PageSize = ddlQuantidaItensGridEdicaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataSource = this.ProcessosPermissoes;
                    grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataBind();

                    //Cadastros Tecnicos
                    grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.PageSize = ddlQuantidaItensGridEdicaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataSource = this.CadastrosTecnicosPermissoes;
                    grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataBind();

                    //Outros de Empresa
                    grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.PageSize = ddlQuantidaItensGridEdicaoMA.SelectedValue.ToInt32();
                    grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataSource = this.OutrosEmpresasPermissoes;
                    grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataBind();

                    break;
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

    protected void ddlQuantidaItensGridEdicaoContratos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlTipoConfiguracaoModuloContratos.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.PageSize = ddlQuantidaItensGridEdicaoContratos.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataSource = this.EmpresasPermissoes;
                    grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataBind();

                    break;


                case "S":

                    grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.PageIndex = 0;
                    grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.PageSize = ddlQuantidaItensGridEdicaoContratos.SelectedValue.ToInt32();

                    grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataSource = this.SetoresPermissoes;
                    grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataBind();

                    break;

                default:

                    grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataSource = new List<Empresa>();
                    grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataSource = new List<Setor>();
                    break;

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

    protected void ddlQuantidaItensGridEdicaoDiversos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoModuloDiversos.PageIndex = 0;
            grvConfiguracaoUsuariosEdicaoModuloDiversos.PageSize = ddlQuantidaItensGridEdicaoDiversos.SelectedValue.ToInt32();

            switch (ddlTipoConfiguracaoModuloDiversos.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosEdicaoModuloDiversos.DataSource = this.EmpresasPermissoes;

                    break;

            }

            grvConfiguracaoUsuariosEdicaoModuloDiversos.DataBind();
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

    #region _______________ Clicks __________________________

    protected void ibtnEditarUsuariosVisualizacaoModGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Geral";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Geral").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = "G";

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();

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

    protected void ibtnEditarUsuarioEdicaoModGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Geral";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Geral").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = "G";

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuariosVisualizacaoDNPMGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "DNPM";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuarioEdicaoDNPMGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "DNPM";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuariosVisualizacaoMeioAmbienteGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Meio Ambiente";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuarioEdicaoMeioAmbienteGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Meio Ambiente";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuariosVisualizacaoContratosGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Contratos";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Contratos").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloContratos.SelectedValue;

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuarioEdicaoContratosGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Contratos";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Contratos").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloContratos.SelectedValue;

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuariosVisualizacaoDiversosGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Diversos";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Diversos").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloDiversos.SelectedValue;

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void ibtnEditarUsuarioEdicaoDiversosGeral_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Diversos";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Diversos").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloDiversos.SelectedValue;

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void btnSavarUsuariosVisualizacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarUsuariosVisualizacao();
            lblUsuariosVisualizacaoExtender_popupextender.Hide();
            this.CarregarSessoes();
            this.CarregarConfiguracoesVisualizacoes();
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

    protected void btnSalvarUsuarioEdicao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarUsuarioEdicao();
            lblUsuariosEdicaoExtender_popupextender.Hide();
            this.CarregarSessoes();
            this.CarregarConfiguracoesEdicoes();
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

    protected void btnSalvarPermissoes_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarPermissoes();
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

    #region _______________ PageIndexChangings ______________

    //Diversos
    protected void grvConfiguracaoUsuariosVisualizacaoModuloDiversos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.PageIndex = e.NewPageIndex;

            switch (ddlTipoConfiguracaoModuloDiversos.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataSource = this.EmpresasPermissoes;

                    break;

            }

            grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoModuloDiversos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoModuloDiversos.PageIndex = e.NewPageIndex;

            switch (ddlTipoConfiguracaoModuloDiversos.SelectedValue)
            {
                case "E":

                    grvConfiguracaoUsuariosEdicaoModuloDiversos.DataSource = this.EmpresasPermissoes;

                    break;

            }

            grvConfiguracaoUsuariosEdicaoModuloDiversos.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataSource = this.ProcessosPermissoesDNPM;
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataSource = this.ProcessosPermissoesDNPM;
            grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataSource = this.SetoresPermissoes;
            grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoSetoresModuloContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataSource = this.SetoresPermissoes;
            grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataSource = this.ProcessosPermissoes;
            grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataSource = this.CadastrosTecnicosPermissoes;
            grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataSource = this.OutrosEmpresasPermissoes;
            grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataSource = this.OutrosEmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataSource = this.CadastrosTecnicosPermissoes;
            grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataSource = this.ProcessosPermissoes;
            grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataBind();
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

    protected void grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.PageIndex = e.NewPageIndex;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataSource = this.EmpresasPermissoes;
            grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataBind();
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

    #region _______________ RowEditings _____________________

    protected void grvConfiguracaoUsuariosVisualizacaoModuloDNPM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "DNPM";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoModuloDiversos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Diversos";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Diversos").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloDiversos.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoModuloDiversos.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoModuloDiversos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Diversos";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Diversos").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloDiversos.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoModuloDiversos.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "DNPM";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "DNPM";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "DNPM";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "DNPM";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("DNPM").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloDNPM.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Contratos";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Contratos").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloContratos.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Contratos";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Contratos").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloContratos.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Contratos";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Contratos").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloContratos.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoSetoresModuloContratos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Contratos";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Contratos").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloContratos.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoSetoresModuloContratos.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Meio Ambiente";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Meio Ambiente";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            hfTipoObjetoVisualizacao.Value = "Processo";

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Meio Ambiente";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            hfTipoObjetoVisualizacao.Value = "Cadastro Técnico Federal";

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModulo.Text = "Meio Ambiente";
            hfIdModuloPermissaoVisualizacao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoVisualizacao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoVisualizacao.Value = grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            hfTipoObjetoVisualizacao.Value = "Outros";

            this.CarregarMarcarUsuariosVisualizacao();

            lblUsuariosVisualizacaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Meio Ambiente";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Meio Ambiente";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            hfTipoObjetoEdicao.Value = "Processo";

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Meio Ambiente";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            hfTipoObjetoEdicao.Value = "Cadastro Técnico Federal";

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    protected void grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblNomeModuloEdicao.Text = "Meio Ambiente";
            hfIdModuloPermissaoEdicao.Value = ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id.ToString();
            hfTipoConfiguracaoEdicao.Value = ddlTipoConfiguracaoModuloMeioAmbiente.SelectedValue;
            hfIdObjetoEdicao.Value = grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente.DataKeys[e.NewEditIndex].Value.ToString();

            hfTipoObjetoEdicao.Value = "Outros";

            this.CarregarMarcarUsuarioEdicao();

            lblUsuariosEdicaoExtender_popupextender.Show();
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

    #endregion

    #region ____________________ Trigers _____________________

    protected void btnSavarUsuariosVisualizacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModGeral);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModDNPM);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModMeioAmbiente);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModContratos);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModDiversos);
    }

    protected void btnSalvarUsuarioEdicao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModGeral);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModDNPM);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModMeioAmbiente);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModContratos);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPConfigsModDiversos);
    }

    protected void grvConfiguracaoUsuariosVisualizacaoModuloDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModDNPM);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuariosVisualizacao);
    }

    protected void grvConfiguracaoUsuariosEdicaoModuloDNPM_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModDNPM);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuarioEdicao);
    }

    protected void grvConfiguracaoUsuariosVisualizacaoModuloMeioAmbiente_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModMeioAmbiente);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuariosVisualizacao);
    }

    protected void grvConfiguracaoUsuariosEdicaoModuloMeioAmbiente_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModMeioAmbiente);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuarioEdicao);
    }

    protected void grvConfiguracaoUsuariosVisualizacaoModuloContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModContratos);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuariosVisualizacao);
    }

    protected void grvConfiguracaoUsuariosEdicaoModuloContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModContratos);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuarioEdicao);
    }

    protected void grvConfiguracaoUsuariosVisualizacaoModuloDiversos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModDiversos);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuariosVisualizacao);
    }

    protected void grvConfiguracaoUsuariosEdicaoModuloDiversos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPConfigsModDiversos);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", UPUsuarioEdicao);
    }

    protected void ibtnEditarUsuariosVisualizacaoModGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuariosVisualizacao);
    }

    protected void ibtnEditarUsuarioEdicaoModGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuarioEdicao);
    }

    protected void ibtnEditarUsuariosVisualizacaoDNPMGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuariosVisualizacao);
    }

    protected void ibtnEditarUsuarioEdicaoDNPMGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuarioEdicao);
    }

    protected void ibtnEditarUsuariosVisualizacaoMeioAmbienteGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuariosVisualizacao);
    }

    protected void ibtnEditarUsuarioEdicaoMeioAmbienteGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuarioEdicao);
    }

    protected void ibtnEditarUsuariosVisualizacaoContratosGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuariosVisualizacao);
    }

    protected void ibtnEditarUsuarioEdicaoContratosGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuarioEdicao);
    }

    protected void ibtnEditarUsuariosVisualizacaoDiversosGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuariosVisualizacao);
    }

    protected void ibtnEditarUsuarioEdicaoDiversosGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPUsuarioEdicao);
    }

    #endregion    
    
}