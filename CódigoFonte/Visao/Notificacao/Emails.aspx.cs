using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Notificacao_Emails : PageBase
{
    Msg msg = new Msg();

    public bool UsuarioEditorModuloGeral
    {
        get
        {
            if (Session["UsuarioEditorModuloGeral"] == null)
                return false;
            else
                return (bool)Session["UsuarioEditorModuloGeral"];
        }
        set { Session["UsuarioEditorModuloGeral"] = value; }
    }

    #region ____________________ Sessoes de Permissao____________________

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
    public IList<Empresa> EmpresasPermissaoEdicaoModuloDiversos
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloDiversos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloDiversos"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloDiversos"] = value; }
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
    public IList<Empresa> EmpresasPermissaoEdicaoModuloNPM
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloNPM"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloNPM"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloNPM"] = value; }
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
    public IList<ProcessoDNPM> ProcessosPermissaoEdicaoModuloDNPM
    {
        get
        {
            if (Session["ProcessosPermissaoEdicaoModuloDNPM"] == null)
                return null;
            else
                return (IList<ProcessoDNPM>)Session["ProcessosPermissaoEdicaoModuloDNPM"];
        }
        set { Session["ProcessosPermissaoEdicaoModuloDNPM"] = value; }
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
    public IList<Empresa> EmpresasPermissaoEdicaoModuloMeioAmbiente
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloMeioAmbiente"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloMeioAmbiente"] = value; }
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
    public IList<Processo> ProcessosPermissaoEdicaoModuloMeioAmbiente
    {
        get
        {
            if (Session["ProcessosPermissaoEdicaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<Processo>)Session["ProcessosPermissaoEdicaoModuloMeioAmbiente"];
        }
        set { Session["ProcessosPermissaoEdicaoModuloMeioAmbiente"] = value; }
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
    public IList<CadastroTecnicoFederal> CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente
    {
        get
        {
            if (Session["CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<CadastroTecnicoFederal>)Session["CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente"];
        }
        set { Session["CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente"] = value; }
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
    public IList<OutrosEmpresa> OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente
    {
        get
        {
            if (Session["OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente"] == null)
                return null;
            else
                return (IList<OutrosEmpresa>)Session["OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente"];
        }
        set { Session["OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente"] = value; }
    }

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
    public IList<Empresa> EmpresasPermissaoEdicaoModuloContratos
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloContratos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloContratos"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloContratos"] = value; }
    }

    public IList<Setor> SetoresPermissaoEdicaoModuloContratos
    {
        get
        {
            if (Session["SetoresPermissaoEdicaoModuloContratos"] == null)
                return null;
            else
                return (IList<Setor>)Session["SetoresPermissaoEdicaoModuloContratos"];
        }
        set { Session["SetoresPermissaoEdicaoModuloContratos"] = value; }
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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarSessoesDePermissoes();
                this.CarregarGrupos();
                this.CarregarTipos();
                this.Pesquisar();
                hfId.Value = "1";

                this.UsuarioEditorModuloGeral = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloGeral;

                //this.DesabilitarAlteracao(this.UsuarioEditorModuloGeral);
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

    private void DesabilitarAlteracao(bool habilitar)
    {
        ibtnIncluir.Enabled = ibtnIncluir.Visible = ibtnAlterar.Visible = ibtnAlterar.Enabled = ibtnExcluir.Enabled = ibtnExcluir.Visible = habilitar;
    }

    #region __________Métodos__________

    private void CarregarSessoesDePermissoes()
    {
        this.LimparSessoes();
        this.CarregarPermissoesDNPM();
        this.CarregarPermissoesMeioAmbiente();
        this.CarregarPermissoesContratos();
        this.CarregarPermissoesDiversos();
        this.CarregarSessoesDeEmpresasPermissoes();
    }

    private void CarregarSessoesDeEmpresasPermissoes()
    {
        if ((this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) || (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) || (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) || (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA))
        {
            IList<Empresa> empresas = Empresa.ConsultarTodos();            

            if (empresas != null && empresas.Count > 0)
            {
                foreach (Empresa empresa in empresas)
                {
                    //Modulo DNPM
                    if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

                        EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, modulo.Id);

                        if (empresaPermissao != null)
                        {
                            //empresas com permissão de visualização
                            if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || (empresaPermissao.UsuariosVisualizacao != null && empresaPermissao.UsuariosVisualizacao.Count > 0 && empresaPermissao.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                            {
                                if (this.EmpresasPermissaoModuloDNPM == null)
                                    this.EmpresasPermissaoModuloDNPM = new List<Empresa>();

                                if (!this.EmpresasPermissaoModuloDNPM.Contains(empresa))
                                    this.EmpresasPermissaoModuloDNPM.Add(empresa);
                            }


                            //empresas com permissão de edição
                            if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado))
                            {
                                if (this.EmpresasPermissaoEdicaoModuloNPM == null)
                                    this.EmpresasPermissaoEdicaoModuloNPM = new List<Empresa>();

                                if (!this.EmpresasPermissaoEdicaoModuloNPM.Contains(empresa))
                                    this.EmpresasPermissaoEdicaoModuloNPM.Add(empresa);
                            }
                        }
                    }


                    //Modulo Meio Ambiente
                    if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

                        EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, modulo.Id);

                        if (empresaPermissao != null)
                        {
                            //empresas com permissão de visualização
                            if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || (empresaPermissao.UsuariosVisualizacao != null && empresaPermissao.UsuariosVisualizacao.Count > 0 && empresaPermissao.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                            {
                                if (this.EmpresasPermissaoModuloMeioAmbiente == null)
                                    this.EmpresasPermissaoModuloMeioAmbiente = new List<Empresa>();

                                if (!this.EmpresasPermissaoModuloMeioAmbiente.Contains(empresa))
                                    this.EmpresasPermissaoModuloMeioAmbiente.Add(empresa);
                            }


                            //empresas com permissão de edição
                            if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado))
                            {
                                if (this.EmpresasPermissaoEdicaoModuloMeioAmbiente == null)
                                    this.EmpresasPermissaoEdicaoModuloMeioAmbiente = new List<Empresa>();

                                if (!this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(empresa))
                                    this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Add(empresa);
                            }

                        }
                    }


                    //Modulo Contratos
                    if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) 
                    {
                        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Contratos");

                        EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, modulo.Id);

                        if (empresaPermissao != null)
                        {
                            //empresas com permissão de visualização
                            if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || (empresaPermissao.UsuariosVisualizacao != null && empresaPermissao.UsuariosVisualizacao.Count > 0 && empresaPermissao.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                            {
                                if (this.EmpresasPermissaoModuloContratos == null)
                                    this.EmpresasPermissaoModuloContratos = new List<Empresa>();

                                if (!this.EmpresasPermissaoModuloContratos.Contains(empresa))
                                    this.EmpresasPermissaoModuloContratos.Add(empresa);
                            }


                            //empresas com permissão de edição
                            if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado))
                            {
                                if (this.EmpresasPermissaoEdicaoModuloContratos == null)
                                    this.EmpresasPermissaoEdicaoModuloContratos = new List<Empresa>();

                                if (!this.EmpresasPermissaoEdicaoModuloContratos.Contains(empresa))
                                    this.EmpresasPermissaoEdicaoModuloContratos.Add(empresa);
                            }

                        }
                    }

                    //Modulo Diversos
                    if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Diversos");

                        EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, modulo.Id);

                        if (empresaPermissao != null)
                        {
                            //empresas com permissão de visualização
                            if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || (empresaPermissao.UsuariosVisualizacao != null && empresaPermissao.UsuariosVisualizacao.Count > 0 && empresaPermissao.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                            {
                                if (this.EmpresasPermissaoModuloDiversos == null)
                                    this.EmpresasPermissaoModuloDiversos = new List<Empresa>();

                                if (!this.EmpresasPermissaoModuloDiversos.Contains(empresa))
                                    this.EmpresasPermissaoModuloDiversos.Add(empresa);
                            }


                            //empresas com permissão de edição
                            if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado))
                            {
                                if (this.EmpresasPermissaoEdicaoModuloDiversos == null)
                                    this.EmpresasPermissaoEdicaoModuloDiversos = new List<Empresa>();

                                if (!this.EmpresasPermissaoEdicaoModuloDiversos.Contains(empresa))
                                    this.EmpresasPermissaoEdicaoModuloDiversos.Add(empresa);
                            }

                        }
                    }
                }
            }
        }
    }

    private void LimparSessoes()
    {
        //Modulo Diversos
        this.EmpresasPermissaoModuloDiversos = null;

        //Modulo DNPM
        this.EmpresasPermissaoModuloDNPM = null;
        this.ProcessosPermissaoModuloDNPM = null;

        //Modulo Meio Ambiente
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoModuloMeioAmbiente = null;
        this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
        this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;

        //Modulo Contratos
        this.EmpresasPermissaoModuloContratos = null;
        this.SetoresPermissaoModuloContratos = null;
    }

    private void CarregarPermissoesDNPM()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            IList<ProcessoDNPM> processos = ProcessoDNPM.ConsultarTodos();

            if (processos != null && processos.Count > 0)
            {
                foreach (ProcessoDNPM processo in processos)
                {
                    //processos com permissão de visualização
                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                    {
                        if (this.ProcessosPermissaoModuloDNPM == null)
                            this.ProcessosPermissaoModuloDNPM = new List<ProcessoDNPM>();

                        if (!this.ProcessosPermissaoModuloDNPM.Contains(processo))
                            this.ProcessosPermissaoModuloDNPM.Add(processo);
                    }

                    //processos com permissão de edição
                    if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0 && processo.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.ProcessosPermissaoEdicaoModuloDNPM == null)
                            this.ProcessosPermissaoEdicaoModuloDNPM = new List<ProcessoDNPM>();

                        if (!this.ProcessosPermissaoEdicaoModuloDNPM.Contains(processo))
                            this.ProcessosPermissaoEdicaoModuloDNPM.Add(processo);
                    }
                }
            }
        }
        else 
        {
            this.ProcessosPermissaoModuloDNPM = null;
            this.ProcessosPermissaoEdicaoModuloDNPM = null;
        }
            
    }

    private void CarregarPermissoesMeioAmbiente()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            //Processos
            IList<Processo> processos = Processo.ConsultarTodos();
            if (processos != null && processos.Count > 0)
            {
                foreach (Processo processo in processos)
                {
                    //processos com permissão de visualização
                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                    {
                        if (this.ProcessosPermissaoModuloMeioAmbiente == null)
                            this.ProcessosPermissaoModuloMeioAmbiente = new List<Processo>();

                        if (!this.ProcessosPermissaoModuloMeioAmbiente.Contains(processo))
                            this.ProcessosPermissaoModuloMeioAmbiente.Add(processo);
                    }

                    //processos com permissão de edição
                    if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0 && processo.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.ProcessosPermissaoEdicaoModuloMeioAmbiente == null)
                            this.ProcessosPermissaoEdicaoModuloMeioAmbiente = new List<Processo>();

                        if (!this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(processo))
                            this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Add(processo);
                    }

                }
            }

            //Cadastros Técnicos
            IList<CadastroTecnicoFederal> cadastros = CadastroTecnicoFederal.ConsultarTodos();
            if (cadastros != null && cadastros.Count > 0)
            {
                foreach (CadastroTecnicoFederal cadastro in cadastros)
                {
                    //cadastros com permissão de visualização
                    if ((cadastro.UsuariosVisualizacao == null || cadastro.UsuariosVisualizacao.Count == 0) || (cadastro.UsuariosVisualizacao != null && cadastro.UsuariosVisualizacao.Count > 0 && cadastro.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                    {
                        if (this.CadastrosTecnicosPermissaoModuloMeioAmbiente == null)
                            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = new List<CadastroTecnicoFederal>();

                        if (!this.CadastrosTecnicosPermissaoModuloMeioAmbiente.Contains(cadastro))
                            this.CadastrosTecnicosPermissaoModuloMeioAmbiente.Add(cadastro);
                    }

                    //cadastros com permissão de edição
                    if (cadastro.UsuariosEdicao != null && cadastro.UsuariosEdicao.Count > 0 && cadastro.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente == null)
                            this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente = new List<CadastroTecnicoFederal>();

                        if (!this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Contains(cadastro))
                            this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Add(cadastro);
                    }
                }
            }


            //Outros de Empresa
            IList<OutrosEmpresa> outrosEmps = OutrosEmpresa.ConsultarTodos();
            if (outrosEmps != null && outrosEmps.Count > 0)
            {
                foreach (OutrosEmpresa outroEmp in outrosEmps)
                {
                    //outros de empresa com permissão de visualização
                    if ((outroEmp.UsuariosVisualizacao == null || outroEmp.UsuariosVisualizacao.Count == 0) || (outroEmp.UsuariosVisualizacao != null && outroEmp.UsuariosVisualizacao.Count > 0 && outroEmp.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                    {
                        if (this.OutrosEmpresasPermissaoModuloMeioAmbiente == null)
                            this.OutrosEmpresasPermissaoModuloMeioAmbiente = new List<OutrosEmpresa>();

                        if (!this.OutrosEmpresasPermissaoModuloMeioAmbiente.Contains(outroEmp))
                            this.OutrosEmpresasPermissaoModuloMeioAmbiente.Add(outroEmp);
                    }

                    //outros de empresa com permissão de edição
                    if (outroEmp.UsuariosEdicao != null && outroEmp.UsuariosEdicao.Count > 0 && outroEmp.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente == null)
                            this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente = new List<OutrosEmpresa>();

                        if (!this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(outroEmp))
                            this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Add(outroEmp);
                    }

                }
            }            
        }
        else
        {
            this.ProcessosPermissaoModuloMeioAmbiente = null;
            this.ProcessosPermissaoEdicaoModuloMeioAmbiente = null;
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
            this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente = null;
            this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;
            this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente = null;
        }
    }

    private void CarregarPermissoesContratos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);

        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            IList<Setor> setores = Setor.ConsultarTodos();

            if (setores != null && setores.Count > 0)
            {
                foreach (Setor setor in setores)
                {
                    //processos com permissão de visualização
                    if ((setor.UsuariosVisualizacao == null || setor.UsuariosVisualizacao.Count == 0) || (setor.UsuariosVisualizacao != null && setor.UsuariosVisualizacao.Count > 0 && setor.UsuariosVisualizacao.Contains(this.UsuarioLogado)))
                    {
                        if (this.SetoresPermissaoModuloContratos == null)
                            this.SetoresPermissaoModuloContratos = new List<Setor>();

                        if (!this.SetoresPermissaoModuloContratos.Contains(setor))
                            this.SetoresPermissaoModuloContratos.Add(setor);
                    }

                    //processos com permissão de edição
                    if (setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0 && setor.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.SetoresPermissaoEdicaoModuloContratos == null)
                            this.SetoresPermissaoEdicaoModuloContratos = new List<Setor>();

                        if (!this.SetoresPermissaoEdicaoModuloContratos.Contains(setor))
                            this.SetoresPermissaoEdicaoModuloContratos.Add(setor);
                    }
                }
            }
        }
        else 
        {
            this.SetoresPermissaoModuloContratos = null;
            this.SetoresPermissaoEdicaoModuloContratos = null;
        }
            
    }

    private void CarregarPermissoesDiversos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Diversos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);        
    }

    private void CarregarTipos()
    {
        ddlTipo.Items.Clear();

        bool acessoAoDNPM = Permissoes.UsuarioPossuiAcessoModuloDNPM(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("DNPM"));
        bool acessoAoMeioAmbiente = Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Meio Ambiente"));
        bool acessoAContratos = Permissoes.UsuarioPossuiAcessoModuloContratos(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Contratos"));
        bool acessoADiversos = Permissoes.UsuarioPossuiAcessoModuloDiversos(this.UsuarioLogado, ModuloPermissao.ConsultarPorNome("Diversos"));

        IList<TemplateNotificacao> templates = TemplateNotificacao.ConsultarTiposTemplate();

        foreach (TemplateNotificacao item in templates)
        {
            //Modulo DNPM
            if (item.GetModuloPorNomeTemplate() != null && item.GetModuloPorNomeTemplate() == ModuloPermissao.ConsultarPorNome("DNPM") && acessoAoDNPM)
                ddlTipo.Items.Add(new ListItem(item.GetTipo, item.Nome));

            //Modulo Meio AMbiente
            if (item.GetModuloPorNomeTemplate() != null && item.GetModuloPorNomeTemplate() == ModuloPermissao.ConsultarPorNome("Meio Ambiente") && acessoAoMeioAmbiente)
                ddlTipo.Items.Add(new ListItem(item.GetTipo, item.Nome));

            //Modulo Contratos
            if (item.GetModuloPorNomeTemplate() != null && item.GetModuloPorNomeTemplate() == ModuloPermissao.ConsultarPorNome("Contratos") && acessoAContratos)
                ddlTipo.Items.Add(new ListItem(item.GetTipo, item.Nome));

            //Modulo Diversos
            if (item.GetModuloPorNomeTemplate() != null && item.GetModuloPorNomeTemplate() == ModuloPermissao.ConsultarPorNome("Diversos") && acessoADiversos)
                ddlTipo.Items.Add(new ListItem(item.GetTipo, item.Nome));
        }

        ddlTipo.Items.Insert(0, new ListItem("-- Todos --", ""));
    }

    private void CarregarGrupos()
    {
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarEmpresa(int idGrupo)
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(idGrupo);
        ddlEmpresa.DataValueField = "Id";
        ddlEmpresa.DataTextField = "Nome";
        ddlEmpresa.DataSource = grupo != null && grupo.Empresas != null ? grupo.Empresas.OrderBy(x => x.Nome).ToList() : new List<Empresa>();
        ddlEmpresa.DataBind();
        ddlEmpresa.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void Pesquisar()
    {
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;

        //cosultar as configurações por id pra passar pq elas estao na sessão
        this.ConfiguracaoModuloDiversos = this.ConfiguracaoModuloDiversos != null ? ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloDiversos.Id) : null;
        this.ConfiguracaoModuloDNPM = this.ConfiguracaoModuloDNPM != null ? ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloDNPM.Id) : null;
        this.ConfiguracaoModuloMeioAmbiente = this.ConfiguracaoModuloMeioAmbiente != null ? ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloMeioAmbiente.Id) : null;
        this.ConfiguracaoModuloContratos = this.ConfiguracaoModuloContratos != null ? ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloContratos.Id) : null;

        IList<Notificacao> notificacoes = Notificacao.Consultar(tbxEmail.Text, ddlTipo.SelectedIndex == 0 ? "" : ddlTipo.SelectedItem.Value, this.UsuarioLogado.ConsultarPorId(), this.ConfiguracaoModuloDiversos, this.EmpresasPermissaoModuloDiversos, this.ConfiguracaoModuloDNPM,
            this.EmpresasPermissaoModuloDNPM, this.ProcessosPermissaoModuloDNPM, this.ConfiguracaoModuloMeioAmbiente, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente,
            this.CadastrosTecnicosPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente, this.ConfiguracaoModuloContratos, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos);
        
        if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
            this.RemoverNotificacoesDeOutroGrupo(notificacoes, ddlGrupoEconomico.SelectedValue.ToInt32());

        if (ddlEmpresa.SelectedValue.ToInt32() > 0)
            this.RemoverNotificacoesDeOutraEmpresa(notificacoes, ddlEmpresa.SelectedValue.ToInt32());

        dgr.DataSource = notificacoes != null ? notificacoes : new List<Notificacao>();
        dgr.DataBind();
    }

    private void RemoverNotificacoesDeOutraEmpresa(IList<Notificacao> notificacoes, int idEmpresa)
    {
        if (notificacoes != null && notificacoes.Count > 0)
        {
            for (int i = notificacoes.Count - 1; i > -1; i--)
            {
                if (notificacoes[i].GetEmpresa == null || notificacoes[i].GetEmpresa.Id != idEmpresa)
                    notificacoes.Remove(notificacoes[i]);
            }
        }
    }

    private void RemoverNotificacoesDeOutroGrupo(IList<Notificacao> notificacoes, int idGrupo)
    {
        if (notificacoes != null && notificacoes.Count > 0)
        {
            for (int i = notificacoes.Count - 1; i > -1; i--)
            {
                if (notificacoes[i].GetGrupoEconomico == null || notificacoes[i].GetGrupoEconomico.Id != idGrupo)
                    notificacoes.Remove(notificacoes[i]);
            }
        }
    }

    private void IncluirEmail()
    {
        if (tbxIncluirEmail.Text.Contains(';'))
        {
            msg.CriarMensagem("Só é possivel incluir 1(um) e-mail por vez", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!this.ValidarEmailDigitado(tbxIncluirEmail))
        {
            msg.CriarMensagem("Informe um e-mail válido para fazer a inclusão", "Alerta", MsgIcons.Alerta);
            return;
        }

        foreach (DataGridItem item in dgr.Items)
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                Notificacao n = Notificacao.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());

                string[] emailsIncluidos = tbxIncluirEmail.Text.Split(')');
                for (int i = 0; i < emailsIncluidos.Length; i++)
                {
                    if (Validadores.ValidaEmail(emailsIncluidos[i]))
                        n.Emails += ";" + tbxIncluirEmail.Text + ";";
                }
                n.Emails = n.Emails.Replace(";;", ";");
                n.Salvar();
            }

        this.Pesquisar();

        if (ckbIncluirTambem.Checked)
        {
            hfTipoAcao.Value = "Inclusao";
            this.CarregarEmailEmpresa();
        }
    }

    private void AlterarEmails()
    {
        if (tbxEmailRetiradoAlteracao.Text.Contains(';'))
        {
            msg.CriarMensagem("Só é possivel alterar 1(um) e-mail por vez", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (tbxEmailIncluidoAlteracao.Text.Contains(';'))
        {
            msg.CriarMensagem("Só é possivel incluir 1(um) e-mail por vez", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!this.ValidarEmailDigitado(tbxEmailRetiradoAlteracao))
        {
            msg.CriarMensagem("Informe um e-mail válido para ser alterado", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!this.ValidarEmailDigitado(tbxEmailIncluidoAlteracao))
        {
            msg.CriarMensagem("Informe um e-mail válido para ser incluído", "Alerta", MsgIcons.Alerta);
            return;
        }

        foreach (DataGridItem item in dgr.Items)
        {
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                Notificacao n = Notificacao.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                IList<String> emailsAux = new List<String>();

                emailsAux = n.Emails.Split(';');

                for (int j = 0; j < emailsAux.Count; j++)
                {
                    if (tbxEmailRetiradoAlteracao.Text.Contains(')'))
                    {
                        string[] emailsRetirados = tbxEmailRetiradoAlteracao.Text.Split(')');
                        for (int i = 0; i < emailsRetirados.Length; i++)
                        {
                            if (Validadores.ValidaEmail(emailsRetirados[i].Trim()))
                            {
                                if (emailsAux[j].Trim().Contains(emailsRetirados[i].Trim()))
                                {
                                    if (tbxEmailIncluidoAlteracao.Text.Contains(')'))
                                    {
                                        string[] emailIncluido = tbxEmailIncluidoAlteracao.Text.Split(')');
                                        for (int p = 0; p < emailIncluido.Length; p++)
                                        {
                                            if (Validadores.ValidaEmail(emailIncluido[p].Trim()))
                                                emailsAux[j] = tbxEmailIncluidoAlteracao.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (Validadores.ValidaEmail(tbxEmailIncluidoAlteracao.Text.Trim()))
                                            emailsAux[j] = tbxEmailIncluidoAlteracao.Text;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (emailsAux[j].Trim().Contains(tbxEmailRetiradoAlteracao.Text.Trim()))
                        {
                            if (tbxEmailIncluidoAlteracao.Text.Contains(')'))
                            {
                                string[] emailIncluido2 = tbxEmailIncluidoAlteracao.Text.Split(')');
                                for (int q = 0; q < emailIncluido2.Length; q++)
                                {
                                    if (Validadores.ValidaEmail(emailIncluido2[q].Trim()))
                                        emailsAux[j] = tbxEmailIncluidoAlteracao.Text;
                                }
                            }
                            else
                            {
                                if (Validadores.ValidaEmail(tbxEmailIncluidoAlteracao.Text.Trim()))
                                    emailsAux[j] = tbxEmailIncluidoAlteracao.Text;
                            }
                        }
                    }
                }

                if (emailsAux != null && emailsAux.Count > 0)
                {
                    n.Emails = "";
                    for (int i = 0; i < emailsAux.Count; i++)
                    {
                        if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                            n.Emails += emailsAux[i] + ";";
                        else if (emailsAux[i] != "")
                            n.Emails += emailsAux[i];
                    }
                }
            }

        }

        this.Pesquisar();

        if (ckbAlterarTambem.Checked)
        {
            hfTipoAcao.Value = "Alteracao";
            this.CarregarEmailEmpresa();
        }
    }

    private void ExcluirEmails()
    {
        if (tbxExluirEmail.Text.Contains(';'))
        {
            msg.CriarMensagem("Só é possivel excluir 1(um) e-mail por vez", "Alerta", MsgIcons.Alerta);
            return;
        }

        foreach (DataGridItem item in dgr.Items)
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                Notificacao n = Notificacao.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                IList<String> emails = new List<String>();

                emails = n.Emails.Split(';');
                for (int i = 0; i < emails.Count; i++)
                {
                    string[] emailsExcluidos = tbxExluirEmail.Text.Split(')');
                    for (int j = 0; j < emailsExcluidos.Length; j++)
                    {
                        if (emails[i].Trim().Contains(emailsExcluidos[j].Trim()))
                            emails[i] = "";
                    }
                }

                if (emails != null && emails.Count > 0)
                {
                    n.Emails = "";
                    for (int i = 0; i < emails.Count; i++)
                    {
                        if (i != emails.Count - 1 && emails[i] != "")
                            n.Emails += emails[i] + ";";
                        else if (emails[i] != "")
                            n.Emails += emails[i];
                    }
                }

                n.Salvar();
            }

        this.Pesquisar();

        if (ckbExcluirTambem.Checked)
        {
            hfTipoAcao.Value = "Exclusao";
            this.CarregarEmailEmpresa();
        }
    }

    private bool ValidarEmailDigitado(TextBox tbxEmail)
    {
        bool retorno = false;
        string[] emails = tbxEmail.Text.Split(')');
        for (int i = 0; i < emails.Length; i++)
        {
            if (Validadores.ValidaEmail(emails[i].Trim()))
                retorno = true;
        }
        return retorno;
    }

    private string ObterEmailDigitado(TextBox tbxEmail)
    {
        string retorno = "";
        string[] emails = tbxEmail.Text.Split(')');
        for (int i = 0; i < emails.Length; i++)
        {
            if (Validadores.ValidaEmail(emails[i].Trim()))
                retorno = emails[i].Trim();
        }
        return retorno;
    }

    private void AlterarEmaislEmpresas()
    {
        GrupoEconomico grupo = new GrupoEconomico();
        Empresa empresa = new Empresa();
        Consultora consultora = new Consultora();
        foreach (GridViewRow item in grvEmailsEmpresas.Rows)
        {
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                if (((Label)item.Cells[2].FindControl("lblTipo")).Text == "Grupo Econômico")
                {
                    grupo = GrupoEconomico.ConsultarPorId(grvEmailsEmpresas.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (hfTipoAcao.Value == "Inclusao")
                    {
                        this.IncluirEmailGrupoEconomico(grupo, tbxIncluirEmail.Text);
                    }
                    else if (hfTipoAcao.Value == "Alteracao")
                    {
                        this.AlterarEmailGrupoEconomico(grupo, this.ObterEmailDigitado(tbxEmailRetiradoAlteracao), tbxEmailIncluidoAlteracao.Text);
                    }
                    else if (hfTipoAcao.Value == "Exclusao")
                    {
                        this.ExcluirEmailGrupoEconomico(grupo, this.ObterEmailDigitado(tbxExluirEmail));
                    }

                    grupo.Salvar();
                }
                else if (((Label)item.Cells[2].FindControl("lblTipo")).Text == "Empresa")
                {
                    empresa = Empresa.ConsultarPorId(grvEmailsEmpresas.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (hfTipoAcao.Value == "Inclusao")
                    {
                        this.IncluirEmailEmpresa(empresa, tbxIncluirEmail.Text);
                    }
                    else if (hfTipoAcao.Value == "Alteracao")
                    {
                        this.AlterarEmailEmpresa(empresa, this.ObterEmailDigitado(tbxEmailRetiradoAlteracao), tbxEmailIncluidoAlteracao.Text);
                    }
                    else if (hfTipoAcao.Value == "Exclusao")
                    {
                        this.ExcluirEmailEmpresa(empresa, this.ObterEmailDigitado(tbxExluirEmail));
                    }

                    empresa.Salvar();
                }
                else if (((Label)item.Cells[2].FindControl("lblTipo")).Text == "Consultora")
                {
                    consultora = Consultora.ConsultarPorId(grvEmailsEmpresas.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (hfTipoAcao.Value == "Inclusao")
                    {
                        this.IncluirEmailConsultora(consultora, tbxIncluirEmail.Text);
                    }
                    else if (hfTipoAcao.Value == "Alteracao")
                    {
                        this.AlterarEmailConsultora(consultora, this.ObterEmailDigitado(tbxEmailRetiradoAlteracao), tbxEmailIncluidoAlteracao.Text);
                    }
                    else if (hfTipoAcao.Value == "Exclusao")
                    {
                        this.ExcluirEmailConsultora(consultora, this.ObterEmailDigitado(tbxExluirEmail));
                    }

                    consultora.Salvar();
                }
            }
        }

        msg.CriarMensagem("E-mails de Empresas modificados com sucesso", "Sucesso", MsgIcons.Sucesso);

    }

    private void IncluirEmailGrupoEconomico(GrupoEconomico grupo, string email)
    {
        if (grupo.Contato == null)
            grupo.Contato = new Contato();
        grupo.Contato.Email += ";" + email + ";";
        grupo.Contato.Email = grupo.Contato.Email.Replace(";;", ";").Trim();
    }

    private void IncluirEmailEmpresa(Empresa empresa, string email)
    {
        if (empresa.Contato == null)
            empresa.Contato = new Contato();
        empresa.Contato.Email += ";" + email + ";";
        empresa.Contato.Email = empresa.Contato.Email.Replace(";;", ";").Trim();
    }

    private void IncluirEmailConsultora(Consultora consultora, string email)
    {
        if (consultora.Contato == null)
            consultora.Contato = new Contato();
        consultora.Contato.Email += ";" + email + ";";
        consultora.Contato.Email = consultora.Contato.Email.Replace(";;", ";").Trim();
    }

    private void AlterarEmailGrupoEconomico(GrupoEconomico grupo, string emailRetirado, string emailIncluido)
    {
        if (grupo.Contato != null)
        {
            IList<String> emailsAux = new List<String>();
            emailsAux = grupo.Contato.Email.Split(';');
            for (int j = 0; j < emailsAux.Count; j++)
            {
                if (emailsAux[j].Contains(emailRetirado))
                {
                    emailsAux[j] = emailIncluido;
                }
            }

            if (emailsAux != null && emailsAux.Count > 0)
            {
                grupo.Contato.Email = "";
                for (int i = 0; i < emailsAux.Count; i++)
                {
                    if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                        grupo.Contato.Email += emailsAux[i] + ";";
                    else if (emailsAux[i] != "")
                        grupo.Contato.Email += emailsAux[i];
                }
            }
            grupo.Contato.Email = grupo.Contato.Email.Replace(";;", ";").Trim();
        }
    }

    private void AlterarEmailEmpresa(Empresa empresa, string emailRetirado, string emailIncluido)
    {
        if (empresa.Contato != null)
        {
            IList<String> emailsAux = new List<String>();
            emailsAux = empresa.Contato.Email.Split(';');
            for (int j = 0; j < emailsAux.Count; j++)
            {
                if (emailsAux[j].Contains(emailRetirado))
                {
                    emailsAux[j] = emailIncluido;
                }
            }

            if (emailsAux != null && emailsAux.Count > 0)
            {
                empresa.Contato.Email = "";
                for (int i = 0; i < emailsAux.Count; i++)
                {
                    if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                        empresa.Contato.Email += emailsAux[i] + ";";
                    else if (emailsAux[i] != "")
                        empresa.Contato.Email += emailsAux[i];
                }
            }
            empresa.Contato.Email = empresa.Contato.Email.Replace(";;", ";").Trim();
        }
    }

    private void AlterarEmailConsultora(Consultora consultora, string emailRetirado, string emailIncluido)
    {
        if (consultora.Contato != null)
        {
            IList<String> emailsAux = new List<String>();
            emailsAux = consultora.Contato.Email.Split(';');
            for (int j = 0; j < emailsAux.Count; j++)
            {
                if (emailsAux[j].Contains(emailRetirado))
                {
                    emailsAux[j] = emailIncluido;
                }
            }

            if (emailsAux != null && emailsAux.Count > 0)
            {
                consultora.Contato.Email = "";
                for (int i = 0; i < emailsAux.Count; i++)
                {
                    if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                        consultora.Contato.Email += emailsAux[i] + ";";
                    else if (emailsAux[i] != "")
                        consultora.Contato.Email += emailsAux[i];
                }
            }
            consultora.Contato.Email = consultora.Contato.Email.Replace(";;", ";").Trim();
        }
    }

    private void ExcluirEmailGrupoEconomico(GrupoEconomico grupo, string emailRetirado)
    {
        if (grupo.Contato != null)
        {
            IList<String> emailsAux = new List<String>();
            emailsAux = grupo.Contato.Email.Split(';');
            for (int j = 0; j < emailsAux.Count; j++)
            {
                if (emailsAux[j].Contains(emailRetirado))
                {
                    emailsAux[j] = "";
                }
            }

            if (emailsAux != null && emailsAux.Count > 0)
            {
                grupo.Contato.Email = "";
                for (int i = 0; i < emailsAux.Count; i++)
                {
                    if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                        grupo.Contato.Email += emailsAux[i] + ";";
                    else if (emailsAux[i] != "")
                        grupo.Contato.Email += emailsAux[i];
                }
            }
            grupo.Contato.Email = grupo.Contato.Email.Replace(";;", ";").Trim();
        }
    }

    private void ExcluirEmailEmpresa(Empresa empresa, string emailRetirado)
    {
        if (empresa.Contato != null)
        {
            IList<String> emailsAux = new List<String>();
            emailsAux = empresa.Contato.Email.Split(';');
            for (int j = 0; j < emailsAux.Count; j++)
            {
                if (emailsAux[j].Contains(emailRetirado))
                {
                    emailsAux[j] = "";
                }
            }

            if (emailsAux != null && emailsAux.Count > 0)
            {
                empresa.Contato.Email = "";
                for (int i = 0; i < emailsAux.Count; i++)
                {
                    if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                        empresa.Contato.Email += emailsAux[i] + ";";
                    else if (emailsAux[i] != "")
                        empresa.Contato.Email += emailsAux[i];
                }
            }
            empresa.Contato.Email = empresa.Contato.Email.Replace(";;", ";").Trim();
        }
    }

    private void ExcluirEmailConsultora(Consultora consultora, string emailRetirado)
    {
        if (consultora.Contato != null)
        {
            IList<String> emailsAux = new List<String>();
            emailsAux = consultora.Contato.Email.Split(';');
            for (int j = 0; j < emailsAux.Count; j++)
            {
                if (emailsAux[j].Contains(emailRetirado))
                {
                    emailsAux[j] = "";
                }
            }

            if (emailsAux != null && emailsAux.Count > 0)
            {
                consultora.Contato.Email = "";
                for (int i = 0; i < emailsAux.Count; i++)
                {
                    if (i != emailsAux.Count - 1 && emailsAux[i] != "")
                        consultora.Contato.Email += emailsAux[i] + ";";
                    else if (emailsAux[i] != "")
                        consultora.Contato.Email += emailsAux[i];
                }
            }
            consultora.Contato.Email = consultora.Contato.Email.Replace(";;", ";").Trim();
        }
    }

    private void CarregarEmailEmpresa()
    {
        IList<Pessoa> pessoas = this.ConsultarPessoasEmailsInclusao();
        switch (hfTipoAcao.Value)
        {
            case "Inclusao":
                lblMensagemEdicao.Text = "Selecione abaixo as empresas em que deseja inserir o e-mail";
                grvEmailsEmpresas.DataSource = pessoas;
                grvEmailsEmpresas.DataBind();
                break;

            case "Alteracao":
                lblMensagemEdicao.Text = "Selecione abaixo as empresas em que deseja alterar o e-mail";
                this.RemoverPessoasSemOEmail(pessoas, this.ObterEmailDigitado(tbxEmailRetiradoAlteracao));
                break;

            case "Exclusao":
                lblMensagemEdicao.Text = "Selecione abaixo as empresas em que deseja excluir o e-mail";
                this.RemoverPessoasSemOEmail(pessoas, this.ObterEmailDigitado(tbxExluirEmail));
                break;
        }

        if (pessoas != null && pessoas.Count > 0)
        {
            grvEmailsEmpresas.DataSource = pessoas;
            grvEmailsEmpresas.DataBind();
            lblExtenderPopUpEmpresas_ModalPopupExtender.Show();
        }
    }

    private void RemoverPessoasSemOEmail(IList<Pessoa> pessoas, string email)
    {
        if (pessoas != null && pessoas.Count > 0)
        {
            for (int i = pessoas.Count - 1; i > -1; i--)
            {
                if (pessoas[i].Contato == null || !pessoas[i].Contato.Email.Contains(email))
                {
                    pessoas.Remove(pessoas[i]);
                }
            }
        }
    }

    private IList<Pessoa> ConsultarPessoasEmailsInclusao()
    {
        IList<Pessoa> pessoas = new List<Pessoa>();
        pessoas.AddRange<Pessoa>(GrupoEconomico.ConsultarTodosComoPessoas());
        pessoas.AddRange<Pessoa>(Empresa.ConsultarTodosComoPessoas());
        pessoas.AddRange<Pessoa>(Consultora.ConsultarTodosComoPessoas());
        return pessoas;
    }

    #endregion

    #region __________Eventos__________

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            dgr.CurrentPageIndex = 0;
            this.Pesquisar();
            hfId.Value = "1";
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

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgr.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();
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

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() > 0)
            {
                dgr.CurrentPageIndex = 0;
                dgr.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
                this.Pesquisar();
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

    protected void ddlGrupoEconomico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresa(ddlGrupoEconomico.SelectedValue.ToInt32());
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

    protected void ibtnVisualizarContrato_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() > 0)
            {
                this.IncluirEmail();
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

    protected void ibtnAlterar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() > 0)
            {
                this.AlterarEmails();
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

    protected void ibtnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() > 0)
            {
                this.ExcluirEmails();
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

    protected void ibtnIncluir_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEmailsEmpresas);
    }

    protected void grvEmailsEmpresas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvEmailsEmpresas.PageIndex = e.NewPageIndex;
            this.CarregarEmailEmpresa();
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

    protected void ibtnAlterar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEmailsEmpresas);
    }

    protected void ibtnExcluir_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEmailsEmpresas);
    }

    protected void btnAlterarEmailsEmpresas_Click(object sender, EventArgs e)
    {
        try
        {
            this.AlterarEmaislEmpresas();
            lblExtenderPopUpEmpresas_ModalPopupExtender.Hide();
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

    #region _________Bindings__________

    public string bindingEmpresaGrupoEconomico(Object notificacao)
    {
        Notificacao aux = ((Notificacao)notificacao);
        if (aux.Template == TemplateNotificacao.TemplateVencimentoDiverso)
        {
            VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(aux.Vencimento.Id);
            return (vencimento.Diverso.Empresa != null ? vencimento.Diverso.Empresa.Nome : "--") + " / " + (vencimento.Diverso.Empresa.GrupoEconomico != null ? vencimento.Diverso.Empresa.GrupoEconomico.Nome : "--");
        }

        if (aux.Template == TemplateNotificacao.TemplateVencimentoContratoDiverso)
        {
            VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(aux.Vencimento.Id);
            return (vencimento != null && vencimento.ContratoDiverso != null && vencimento.ContratoDiverso.Empresa != null ? vencimento.ContratoDiverso.Empresa.Nome : "--") + " / " + (vencimento != null && vencimento.ContratoDiverso != null && vencimento.ContratoDiverso.Empresa != null && vencimento.ContratoDiverso.Empresa.GrupoEconomico != null ? vencimento.ContratoDiverso.Empresa.GrupoEconomico.Nome : "--");
        }

        if (aux.Template == TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso)
        {
            VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(aux.Vencimento.Id);
            return (vencimento != null && vencimento.Reajuste != null && vencimento.Reajuste.Empresa != null ? vencimento.Reajuste.Empresa.Nome : "--") + " / " + (vencimento != null && vencimento.Reajuste != null && vencimento.Reajuste.Empresa != null && vencimento.Reajuste.Empresa.GrupoEconomico != null ? vencimento.Reajuste.Empresa.GrupoEconomico.Nome : "--");
        }
        else
            return (aux.GetEmpresa != null ? aux.GetEmpresa.Nome : "--") + " / " + (aux.GetGrupoEconomico != null ? aux.GetGrupoEconomico.Nome : "--");
    }

    public string BindTipo(Object o)
    {
        Pessoa p = (Pessoa)o;
        if (p.GetType().Name.Contains("GrupoEconomico"))
        {
            return "Grupo Econômico";
        }

        if (p.GetType().Name.Contains("Empresa"))
        {
            return "Empresa";
        }

        if (p.GetType().Name.Contains("Consultora"))
        {
            return "Consultora";
        }
        return "";
    }

    public string BindEmails(Object o)
    {
        Pessoa p = (Pessoa)o;
        return p.Contato != null ? p.Contato.Email : "";
    }

    public string bindingTipoNotificacao(Object o)
    {
        Notificacao aux = ((Notificacao)o);
        return aux.GetTipoTemplate;
    }

    public bool BindingVisivel(object o)
    {
        Notificacao notificacao = ((Notificacao)o);

        if (notificacao.Modulo == "DNPM")
        {
            if (this.ConfiguracaoModuloDNPM == null)
                return false;

            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloDNPM.Id);

            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            {
                return this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                if (notificacao.GetEmpresa == null)
                    return false;

                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(notificacao.GetEmpresa.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);
                return empresaPermissao != null && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                if (notificacao.GetProcessoDNPM == null)
                    return false;

                return this.ProcessosPermissaoEdicaoModuloDNPM != null && this.ProcessosPermissaoEdicaoModuloDNPM.Count > 0 && this.ProcessosPermissaoEdicaoModuloDNPM.Contains(notificacao.GetProcessoDNPM);
            }
        }

        if (notificacao.Modulo == "Meio Ambiente")
        {
            if (this.ConfiguracaoModuloMeioAmbiente == null)
                return false;

            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloMeioAmbiente.Id);

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            {
                return this.ConfiguracaoModuloMeioAmbiente.ConsultarPorId().UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.ConsultarPorId().UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.ConsultarPorId().UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                if (notificacao.GetEmpresa == null)
                    return false;

                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(notificacao.GetEmpresa.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);
                return empresaPermissao != null && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                //Outros de Empresa
                if (notificacao.Template == TemplateNotificacao.TemplateOutrosEmpresa) 
                {
                    if (notificacao.GetOutroEmpresa == null)
                        return false;

                    return this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(notificacao.GetOutroEmpresa);
                }

                //Cadastro Técnico Federal                        
                if (notificacao.Template == TemplateNotificacao.TemplateRelatorioCTF || notificacao.Template == TemplateNotificacao.TemplatePagamentoCTF || notificacao.Template == TemplateNotificacao.TemplateCertificadoCTF)
                {
                    if (notificacao.GetCadastroTecnico == null)
                        return false;

                    return this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente != null && this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Contains(notificacao.GetCadastroTecnico);
                }

                //Processos De Meio Ambiente                  
                if (notificacao.Template == TemplateNotificacao.TemplateCondicionante || notificacao.Template == TemplateNotificacao.TemplateLicenca || notificacao.Template == TemplateNotificacao.TemplateOutrosProcesso) 
                {
                    if (notificacao.GetProcessoMeioAmbiente == null)
                        return false;

                    return this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(notificacao.GetProcessoMeioAmbiente);
                }
                
            }
        }

        //Modulo Contratos
        if (notificacao.Modulo == "Contratos")
        {
            if (this.ConfiguracaoModuloContratos == null)
                return false;

            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloContratos.Id);

            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            {
                return this.ConfiguracaoModuloContratos.ConsultarPorId().UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloContratos.ConsultarPorId().UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloContratos.ConsultarPorId().UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                if (notificacao.GetEmpresa == null)
                    return false;

                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(notificacao.GetEmpresa.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);
                 return empresaPermissao != null && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
            {
                if (notificacao.GetContrato == null || (notificacao.GetContrato != null && notificacao.GetContrato.Setor == null))
                    return false;

                return this.SetoresPermissaoEdicaoModuloContratos != null && this.SetoresPermissaoEdicaoModuloContratos.Count > 0 && this.SetoresPermissaoEdicaoModuloContratos.Contains(notificacao.GetContrato.Setor);
            }
        }

        //Modulo Diversos
        if (notificacao.Modulo == "Diversos")
        {
            if (this.ConfiguracaoModuloDiversos == null)
                return false;

            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorId(this.ConfiguracaoModuloDiversos.Id);

            if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            {
                return this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
            }

            if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                if (notificacao.GetEmpresa == null)
                    return false;

                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(notificacao.GetEmpresa.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);
                return empresaPermissao != null && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado);
            }            
        }

        return false;
    }

    #endregion


}