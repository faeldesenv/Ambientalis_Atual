using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;
using System.Configuration;
using System.IO;
using Persistencia.Modelo;
using System.Runtime.Serialization;

public partial class Site_Index : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    public string PopUpAberto
    {
        get
        {
            if (Session["popUpAbertoProc"] == null)
                return "";
            else
                return Session["popUpAbertoProc"].ToString();
        }
        set { Session["popUpAbertoProc"] = value; }
    }

    public Object objetoUtilizado
    {
        get
        {
            if (Session["objUtil"] == null)
                Session["objUtil"] = new Object();
            return Session["objUtil"];
        }
        set { Session["objUtil"] = value; }
    }

    public IList<ArquivoFisico> arquivosUpload
    {
        get
        {
            if (Session["ArquivosUpload"] == null)
                Session["ArquivosUpload"] = new List<ArquivoFisico>();
            return (IList<ArquivoFisico>)Session["ArquivosUpload"];
        }
        set { Session["ArquivosUpload"] = value; }
    }

    public CadastroTecnicoFederal objetoCTF
    {
        get
        {
            if (Session["objCTF"] == null)
                Session["objCTF"] = new CadastroTecnicoFederal();
            return (CadastroTecnicoFederal)Session["objCTF"];
        }
        set { Session["objCTF"] = value; }
    }

    public IList<Notificacao> notificacoesSalvas
    {
        get
        {
            if (Session["notSalva"] == null)
                Session["notSalva"] = new List<Notificacao>();
            return (IList<Notificacao>)Session["notSalva"];
        }
        set { Session["notSalva"] = value; }
    }

    public IList<ContratoDiverso> ContratosConsultados
    {
        get
        {
            if (Session["ContratosAmbientaisConsultados"] == null)
                return null;
            else
                return (IList<ContratoDiverso>)Session["ContratosAmbientaisConsultados"];
        }
        set { Session["ContratosAmbientaisConsultados"] = value; }
    }

    public IList<ItemRenovacao> ItensRenovacao
    {
        get
        {
            if (Session["ItensRenovacaoMeioAmbiente"] == null)
                return null;
            else
                return (IList<ItemRenovacao>)Session["ItensRenovacaoMeioAmbiente"];
        }
        set { Session["ItensRenovacaoMeioAmbiente"] = value; }
    }

    public bool UsuarioEditorMeioAmbiente
    {
        get
        {
            if (Session["UsuarioEditorMeioAmbiente"] == null)
                return false;
            else
                return (bool)Session["UsuarioEditorMeioAmbiente"];
        }
        set { Session["UsuarioEditorMeioAmbiente"] = value; }
    }

    //Sessoes Permissao Modulo Meio Ambiente
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

    //Empresas que o usuário edita e empresas que o usuário visualiza
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

    //Processos que o usuário edita e processos que o usuário visualiza
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

    //Cadastros tecnicos que o usuário edita e cadastros técnicos que o usuário visualiza
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

    //Outros de empresa que o usuário edita e outros de empresa que o usuário visualiza
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


    //Sessoes Permissao Modulo Contratos
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


    #region _______Inner Class_______

    [Serializable()]
    public class ItemRenovacao : ISerializable
    {
        public int idItem;
        public string tipoItem;
        public int diasRenovacao;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("idItem", idItem);
            info.AddValue("tipoItem", tipoItem);
            info.AddValue("diasRenovacao", diasRenovacao);
        }

        public ItemRenovacao(SerializationInfo info, StreamingContext context)
        {
            idItem = (int)info.GetValue("idItem", typeof(int));
            tipoItem = (String)info.GetValue("tipoItem", typeof(string));
            diasRenovacao = (int)info.GetValue("diasRenovacao", typeof(int));
        }

        public ItemRenovacao()
        { }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {  
        if (!IsPostBack)
        {
            this.VerificarPermissoes();

            this.AdicionarConfirmacoes();

            hfIdGrupoEconomico.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idGrupoEconomico", Request);
            hfIdEmpresa.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idEmpresa", Request);
            hfIdOrgao.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idOrgao", Request);

            rbtnProcessoOutros.Checked = false;
            rbtnGeralOutros.Checked = true;
            mvOutros.ActiveViewIndex = 1;

            try
            {
                this.CarregarEstadosProcesso();
                this.CarregarEstadoCadastroLicenca();
                this.CarregarGrupoEconomicos();

                //Crregadas dados dos popups
                this.CarregarStatusCondicionantePOPUP();
                this.CarregarProcessosPOPUP();

                this.MarcarDropDownList();
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
    }

    #region ___________Metodos_____________

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloMeioAmbiente = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
        this.LimparSessoesPermissoes();

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloMeioAmbiente.Id);
        else
            this.ConfiguracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloMeioAmbiente.Id);

        if (this.ConfiguracaoModuloMeioAmbiente == null)
        {
            this.HabilitarControls(false);
            return;
        }

        if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral == null)
                this.HabilitarControls(false);
            else if (this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                this.HabilitarControls(true);
            else
                this.HabilitarControls(false);

            this.EmpresasPermissaoModuloMeioAmbiente = null;
            this.EmpresasPermissaoEdicaoModuloMeioAmbiente = null;

            this.ProcessosPermissaoModuloMeioAmbiente = null;
            this.ProcessosPermissaoEdicaoModuloMeioAmbiente = null;

            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
            this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente = null;

            this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;
            this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente = null;
        }

        if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.CarregarSessoesEmpresasDeEdicaoEVisualizacao();

            this.ProcessosPermissaoModuloMeioAmbiente = null;
            this.ProcessosPermissaoEdicaoModuloMeioAmbiente = null;

            this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
            this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente = null;

            this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;
            this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente = null;

            this.HabilitarControls(false);
        }

        if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            this.EmpresasPermissaoModuloMeioAmbiente = null;
            this.EmpresasPermissaoEdicaoModuloMeioAmbiente = null;

            this.CarregarSessoesProcessosDeEdicaoEVisualizacao();

            this.CarregarSessoesCadastrosTecnicosDeEdicaoEVisualizacao();

            this.CarregarSessoesOutrosEmpresaDeEdicaoEVisualizacao();

            this.HabilitarControls(false);
        }

        this.CarregarPermissoesDoModuloContratos();

    }

    private void CarregarPermissoesDoModuloContratos()
    {
        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, modulo.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(modulo.Id);         

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

    private void LimparSessoesPermissoes()
    {
        //Sessoes Modulo Meio Ambiente
        this.EmpresasPermissaoModuloMeioAmbiente = null;
        this.EmpresasPermissaoEdicaoModuloMeioAmbiente = null;

        this.ProcessosPermissaoModuloMeioAmbiente = null;
        this.ProcessosPermissaoEdicaoModuloMeioAmbiente = null;

        this.CadastrosTecnicosPermissaoModuloMeioAmbiente = null;
        this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente = null;

        this.OutrosEmpresasPermissaoModuloMeioAmbiente = null;
        this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente = null;


        //Sessoes Modulo Contratos
        this.EmpresasPermissaoModuloContratos = null;
        this.SetoresPermissaoModuloContratos = null;

    }    

    private void CarregarSessoesEmpresasDeEdicaoEVisualizacao()
    {
        IList<Empresa> empresas = Empresa.ConsultarTodos();

        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

        if (empresas != null && empresas.Count > 0)
        {
            foreach (Empresa empresa in empresas)
            {
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
        }

    }

    private void CarregarSessoesProcessosDeEdicaoEVisualizacao()
    {
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
    }

    private void CarregarSessoesCadastrosTecnicosDeEdicaoEVisualizacao()
    {
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
    }

    private void CarregarSessoesOutrosEmpresaDeEdicaoEVisualizacao()
    {
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

    private void HabilitarControls(bool habilitar)
    {
        divOpcoesCadastro.Visible = btnRenovarCertificado.Visible = btnRenovarTaxa.Visible = btnRenovarRelatorio.Visible = habilitar;
    }

    private void VerificarAlteracaoDeStatusHistoricoVencimento(int indexVencimento)
    {
        if (hfTypeVencimento.Value == "")
            return;

        tbxHistoricoAlteracao.Text = "";
        hfIdAlteracao.Value = hfIDVencimento.Value;
        hfTypeAlteracao.Value = hfTypeVencimento.Value;


        string alteracao = "";
        switch (hfTypeVencimento.Value)
        {
            case "EntregaRelatorioAnual":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (cad != null && cad.GetUltimoRelatorio != null && cad.GetUltimoRelatorio.Status != null && cad.GetUltimoRelatorio.Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento do Relatório Anual da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoRelatorio.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + ")";

                    }
                } break;
            case "CertificadoRegularidade":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (cad != null && cad.GetUltimoCertificado != null && cad.GetUltimoCertificado.Status != null && cad.GetUltimoCertificado.Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento do Certificado de Regularidade da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoCertificado.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + ")";
                    }
                } break;
            case "TaxaTrimestral":
                {

                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (cad != null && cad.GetUltimoPagamento != null && cad.GetUltimoPagamento.Status != null && cad.GetUltimoPagamento.Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento da Taxa Trimestral da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoPagamento.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + ")";
                    }
                } break;
            case "Condicionante":
                {
                    Condicionante condicionante = Condicionante.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (condicionante != null && condicionante.GetUltimoVencimento != null && condicionante.GetUltimoVencimento.Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento da Condicionante - Licença(" + condicionante.Licenca.TipoLicenca.Sigla + " - " + condicionante.Licenca.Numero + "). De: " + condicionante.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + condicionante.Licenca.Processo.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "OutrosEmpresa":
                {
                    OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (outros != null && outros.GetUltimoVencimento != null && outros.GetUltimoVencimento.Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento de Outros Vencimentos - Empresa(" + outros.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + outros.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + ")";
                    }
                } break;
            case "OutrosProcesso":
                {
                    OutrosProcesso outros = OutrosProcesso.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (outros != null && outros.GetUltimoVencimento != null && outros.GetUltimoVencimento.Status.Id.ToString() != ddlStatusVencimentos.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento de Outros Vencimentos - Processo(" + outros.Processo.Numero + "). De: " + outros.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusVencimentos.SelectedItem.Text + " - Empresa: " + outros.Processo.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            default:
                return;
        }

        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        chkEnviarEmail.Checked = true;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsGrupoEconomico().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));
    }

    private void VerificarAlteracaoDeStatus(object objetoManipulado)
    {
        if (objetoManipulado == null)
            return;

        tbxHistoricoAlteracao.Text = "";
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        hfTypeAlteracao.Value = objetoManipulado.GetType().Name;

        string alteracao = "";
        switch (objetoManipulado.GetType().Name.ToString())
        {
            case "CadastroTecnicoFederal":
                {
                    CadastroTecnicoFederal cad = (CadastroTecnicoFederal)objetoManipulado;
                    if (cad != null && cad.GetUltimoRelatorio != null && cad.GetUltimoRelatorio.Status != null && cad.GetUltimoRelatorio.Status.Id.ToString() != ddlEstatusRelatorioAnual.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento do Relatório Anual da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoRelatorio.Status.Nome + " - Para: " + ddlEstatusRelatorioAnual.SelectedItem.Text + ")";

                    }
                    if (cad != null && cad.GetUltimoCertificado != null && cad.GetUltimoCertificado.Status != null && cad.GetUltimoCertificado.Status.Id.ToString() != ddlEstatusCertificado.SelectedValue)
                    {
                        if (alteracao.Trim() != "")
                        {
                            alteracao += " e ";
                        }
                        alteracao += " (Alteração no Status do vencimento do Certificado de Regularidade da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoCertificado.Status.Nome + " - Para: " + ddlEstatusCertificado.SelectedItem.Text + ")";

                    }
                    if (cad != null && cad.GetUltimoPagamento != null && cad.GetUltimoPagamento.Status != null && cad.GetUltimoPagamento.Status.Id.ToString() != ddlEstatusTaxaTrimestral.SelectedValue)
                    {
                        if (alteracao.Trim() != "")
                        {
                            alteracao += " e ";
                        }
                        alteracao += " (Alteração no Status do vencimento da Taxa Trimestral da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoPagamento.Status.Nome + " - Para: " + ddlEstatusTaxaTrimestral.SelectedItem.Text + ")";

                    }
                } break;
            case "Condicionante":
                {
                    Condicionante condicionante = (Condicionante)objetoManipulado;
                    if (condicionante != null && condicionante.GetUltimoVencimento != null && condicionante.GetUltimoVencimento.Status != null && condicionante.GetUltimoVencimento.Status.Id.ToString() != ddlStatusCondicionante.SelectedValue)
                    {
                        alteracao += "(Alteração no Status da Condicionante - Licença(" + condicionante.Licenca.TipoLicenca.Sigla + " - " + condicionante.Licenca.Numero + "). De: " + condicionante.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusCondicionante.SelectedItem.Text + " - Empresa: " + condicionante.Licenca.Processo.Empresa.GetNumeroCNPJeCPFComMascara + ")";

                    }
                } break;
            case "OutrosEmpresa":
                {
                    OutrosEmpresa outros = (OutrosEmpresa)objetoManipulado;
                    if (outros != null && outros.GetUltimoVencimento != null && outros.GetUltimoVencimento.Status.Id.ToString() != ddlStatusOutros.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento de Outros Vencimentos - Empresa(" + outros.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + outros.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusOutros.SelectedItem.Text + ")";
                    }
                } break;
            case "OutrosProcesso":
                {
                    OutrosProcesso outros = (OutrosProcesso)objetoManipulado;
                    if (outros != null && outros.GetUltimoVencimento != null && outros.GetUltimoVencimento.Status.Id.ToString() != ddlStatusOutros.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento de Outros Vencimentos - Processo(" + outros.Processo.Numero + "). De: " + outros.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusOutros.SelectedItem.Text + " - Empresa: " + outros.Processo.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            default:
                return;
        }

        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        chkEnviarEmail.Checked = true;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsGrupoEconomico().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));

    }

    private void VerificarAlteracaoDeStatusRenovacao(Object objetoManipulado, string tipoVencimento)
    {
        if (objetoManipulado == null)
            return;

        tbxHistoricoAlteracao.Text = "";
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        hfTypeAlteracao.Value = tipoVencimento;

        string alteracao = "";
        switch (hfTypeAlteracao.Value)
        {
            case "EntregaRelatorioAnual":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (cad != null && cad.GetUltimoRelatorio != null && cad.GetUltimoRelatorio.Status != null && cad.GetUltimoRelatorio.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento do Relatório Anual da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoRelatorio.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + ")";

                    }
                } break;
            case "CertificadoRegularidade":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (cad != null && cad.GetUltimoCertificado != null && cad.GetUltimoCertificado.Status != null && cad.GetUltimoCertificado.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento do Certificado de Regularidade da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoCertificado.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + ")";
                    }
                } break;
            case "TaxaTrimestral":
                {

                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (cad != null && cad.GetUltimoPagamento != null && cad.GetUltimoPagamento.Status != null && cad.GetUltimoPagamento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += " (Alteração no Status do vencimento da Taxa Trimestral da Empresa(" + cad.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + cad.GetUltimoPagamento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + ")";
                    }
                } break;
            case "Condicionante":
                {
                    Condicionante condicionante = Condicionante.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (condicionante != null && condicionante.GetUltimoVencimento != null && condicionante.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento da Condicionante - Licença(" + condicionante.Licenca.TipoLicenca.Sigla + " - " + condicionante.Licenca.Numero + "). De: " + condicionante.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + condicionante.Licenca.Processo.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            case "OutrosEmpresa":
                {
                    OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (outros != null && outros.GetUltimoVencimento != null && outros.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento de Outros Vencimentos - Empresa(" + outros.Empresa.GetNumeroCNPJeCPFComMascara + "). De: " + outros.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + ")";
                    }
                } break;
            case "OutrosProcesso":
                {
                    OutrosProcesso outros = OutrosProcesso.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    if (outros != null && outros.GetUltimoVencimento != null && outros.GetUltimoVencimento.Status.Id.ToString() != ddlStatusRenovacao.SelectedValue)
                    {
                        alteracao += "(Alteração no Status de vencimento de Outros Vencimentos - Processo(" + outros.Processo.Numero + "). De: " + outros.GetUltimoVencimento.Status.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + " - Empresa: " + outros.Processo.Empresa.GetNumeroCNPJeCPFComMascara + ")";
                    }
                } break;
            default:
                return;
        }

        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        chkEnviarEmail.Checked = true;
        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsGrupoEconomico().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));

    }

    private void EnviarHistorico()
    {
        Email email = new Email();
        foreach (ListItem l in chkGruposHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        foreach (ListItem l in chkConsultoraHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        foreach (ListItem l in chkEmpresaHistorico.Items)
            if (l.Selected && l.Value != "0")
                email.AdicionarDestinatario(l.Value.ToString());
        email.AdicionarDestinatario(tbxEmailsHistorico.Text.ToString());

        switch (hfTypeObs.Value)
        {
            case "Condicionante":
                {
                    Condicionante cond = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
                    if (cond != null)
                    {
                        email.Assunto = "Registros Históricos da Condicionante Nº: " + cond.Numero;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(cond.Historicos, email.Assunto);
                    }
                }
                break;

            case "Condicional":
                {
                    string assunto = "";
                    Condicional cad = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());
                    if (cad != null)
                    {
                        if (cad.GetType() == typeof(OutrosEmpresa))
                            assunto = "Registros Históricos do Outro de Órgão Ambiental, referente à Empresa: " + cad.GetEmpresa.Nome + " - " + cad.GetEmpresa.GetNumeroCNPJeCPFComMascara;
                        if (cad.GetType() == typeof(OutrosProcesso))
                            assunto = "Registros Históricos do Outro de um Processo Ambiental, referente ao Processo Nº: " + cad.GetProcesso.Numero;
                        email.Assunto = assunto;
                        email.Mensagem = email.CriarTemplateParaHistoricoGeral(cad.Historicos, email.Assunto);
                    }

                } break;

            case "CadastroTecnicoFederal":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
                    email.Assunto = "Registros Históricos do Cadastro Técnico Federal da Empresa: " + cad.Empresa.Nome + " - " + cad.Empresa.GetNumeroCNPJeCPFComMascara;
                    email.Mensagem = email.CriarTemplateParaHistoricoGeral(cad.Historicos, email.Assunto);

                } break;
        }

        if (! email.EnviarAutenticado(25, false))
            msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
        else
            msg.CriarMensagem("E-mails enviados com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void CriarHistorico()
    {
        if (!tbxTituloObs.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario informar um Título", "Alerta", MsgIcons.Alerta);
            return;
        }

        Historico h = new Historico();
        h.DataPublicacao = DateTime.Now;
        h.Alteracao = tbxTituloObs.Text;
        h.Observacao = tbxObservacaoObs.Text;

        switch (hfTypeObs.Value.ToString())
        {
            case "CadastroTecnicoFederal":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.CadastroTecnicoFederal = cad;
                    h = h.Salvar();
                    cad.Historicos.Add(h);
                    grvHistoricos.DataSource = cad.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();

                } break;
            case "Condicional":
                {
                    Condicional cond = Condicional.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Condicional = cond;
                    h = h.Salvar();
                    cond.Historicos.Add(h);
                    grvHistoricos.DataSource = cond.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            case "Condicionante":
                {
                    Condicional cond = Condicional.ConsultarPorId(hfIDObs.Value.ToInt32());
                    h.Condicional = cond;
                    h = h.Salvar();
                    cond.Historicos.Add(h);
                    grvHistoricos.DataSource = cond.Historicos.OrderByDescending(i => i.Id).ToList();
                    grvHistoricos.DataBind();
                } break;
            default:
                return;
        }

        tbxTituloObs.Text = "";
        tbxObservacaoObs.Text = "";
    }

    private void SalvarHistoricoAlteracaoStatus()
    {
        Historico h = new Historico();
        h.DataPublicacao = DateTime.Now;
        h.Alteracao = lblAlteracao.Text;
        h.Observacao = tbxHistoricoAlteracao.Text;

        switch (hfTypeAlteracao.Value)
        {
            case "EntregaRelatorioAnual":
            case "CertificadoRegularidade":
            case "TaxaTrimestral":
            case "CadastroTecnicoFederal":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.CadastroTecnicoFederal = cad;
                } break;
            case "Condicionante":
            case "OutrosEmpresa":
            case "OutrosProcesso":
                {
                    Condicional cond = Condicional.ConsultarPorId(hfIdAlteracao.Value.ToInt32());
                    h.Condicional = cond;
                } break;
            default:
                return;
        }

        h.Salvar();

        if (chkEnviarEmail.Checked)
        {
            Email email = new Email();
            foreach (ListItem l in ckbGrupos.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());
            foreach (ListItem l in ckbConsultoria.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());
            foreach (ListItem l in ckbEmpresas.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());

            if ((hfTipoProrrogacao.Value.ToUpper() == "CONDICIONANTE" || hfTipoProrrogacao.Value.ToUpper() == "OUTROS") && lblAlteracao.Text.Contains("prorrogaçã"))
            {
                email.Assunto = "Prorrogação de Vencimento de " + (hfTipoProrrogacao.Value.ToUpper() == "CONDICIONANTE" ? "Condicionante" : "Outros") + " - Sistema Sustentar";
                email.Mensagem = email.CriarTemplateParaProrrogacaoDeDeVencimento(h);

            }
            else
            {
                email.Assunto = "Alteração de Status de Vencimento - Sistema Sustentar";
                email.Mensagem = email.CriarTemplateParaMudancaDeStatusDeVencimento(h);
            }

            email.EnviarAutenticado(25, false);
        }

        hfIdAlteracao.Value = "";
        hfTypeAlteracao.Value = "";
        lblAlteracaoStatus_ModalPopupExtender.Hide();
        msg.CriarMensagem("Histórico registrado com Sucesso", "Sucesso");
    }

    private void CarregarProrrogacoes(string tipo)
    {
        hfTipoProrrogacao.Value = tipo.ToUpper();

        if (hfTipoProrrogacao.Value == "CONDICIONANTE")
        {
            Condicionante condi = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
            grvProrrogacoes.DataSource = condi != null && condi.GetUltimoVencimento != null && condi.GetUltimoVencimento.ProrrogacoesPrazo != null ? condi.GetUltimoVencimento.ProrrogacoesPrazo.OrderByDescending(i => i.Id).ToList() : new List<ProrrogacaoPrazo>();
            lkbProrrogacaoCondicionante.Text = "Abrir Prorrogações - [" + condi.GetUltimoVencimento.ProrrogacoesPrazo.Count + "] Prorrogações.";
        }
        else if (hfTipoProrrogacao.Value == "OUTROS")
        {
            Condicional condi = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());
            grvProrrogacoes.DataSource = condi != null && condi.GetUltimoVencimento != null && condi.GetUltimoVencimento.ProrrogacoesPrazo != null ? condi.GetUltimoVencimento.ProrrogacoesPrazo.OrderByDescending(i => i.Id).ToList() : new List<ProrrogacaoPrazo>();
            lkbProrrogacaoOutros.Text = "Abrir Prorrogações - [" + condi.GetUltimoVencimento.ProrrogacoesPrazo.Count + "] Prorrogações.";
        }

        grvProrrogacoes.DataBind();

        ModalPopupExtenderlblProrrogacao.Show();
    }

    private void SalvarProrrogacao()
    {
        Vencimento venc = null;

        if (tbxPrazoAdicional.Text.ToInt32() <= 0)
        {
            msg.CriarMensagem("É necessário informar um prazo adicional.", "Alerta", MsgIcons.Alerta);
            return;
        }
        if (tbxDataProtocoloAdd.Text.ToSqlDateTime() <= SqlDate.MinValue)
        {
            msg.CriarMensagem("É necessário informar uma data de Protocolo.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Condicional condi = new Condicional();

        if (hfTipoProrrogacao.Value.ToUpper() == "CONDICIONANTE")
        {
            condi = new Condicionante();
            condi = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
            venc = condi.GetUltimoVencimento;

        }
        if (hfTipoProrrogacao.Value.ToUpper() == "OUTROS")
        {
            condi = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());
            venc = condi.GetUltimoVencimento;
        }

        venc.Data = tbxDataProtocoloAdd.Text.ToDateTime().AddDays(tbxPrazoAdicional.Text.ToInt32());
        venc = venc.Salvar();

        ProrrogacaoPrazo prorrogacao = new ProrrogacaoPrazo();
        prorrogacao.DataProtocoloAdicional = tbxDataProtocoloAdd.Text.ToDateTime();
        prorrogacao.PrazoAdicional = tbxPrazoAdicional.Text.ToInt32();
        prorrogacao.ProtocoloAdicional = tbxProtocoloAdicional.Text;
        prorrogacao.Vencimento = venc;
        prorrogacao = prorrogacao.Salvar();
        if (condi.GetUltimoVencimento.ProrrogacoesPrazo == null)
            condi.GetUltimoVencimento.ProrrogacoesPrazo = new List<ProrrogacaoPrazo>();
        condi.GetUltimoVencimento.ProrrogacoesPrazo.Add(prorrogacao);

        this.SalvarHistoricoDeInsercaoDeProrrogacao(prorrogacao, condi);

        transacao.Recarregar(ref msg);
        this.CarregarProrrogacoes(hfTipoProrrogacao.Value.ToUpper());
    }

    private void SalvarHistoricoDeInsercaoDeProrrogacao(ProrrogacaoPrazo p, Object objetoManipulado)
    {
        hfIdAlteracao.Value = ((ObjetoBase)objetoManipulado).Id.ToString();
        hfTypeAlteracao.Value = objetoManipulado.GetType().Name;

        tbxHistoricoAlteracao.Text = "";
        if (hfTipoProrrogacao.Value.ToUpper() == "CONDICIONANTE")
        {
            Condicionante condi = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
            lblAlteracao.Text = "Uma nova prorrogação foi criada para a Condicionante nº: " + (condi != null ? condi.Numero : "") + ", Licença nº: " + (condi != null && condi.Licenca != null ? condi.Licenca.Numero : "") + ", Processo nº:" + (condi != null && condi.GetProcesso != null ? condi.GetProcesso.Numero : "") + " - Empresa: " + (condi != null && condi.GetProcesso != null ? condi.GetProcesso.Empresa.GetNumeroCNPJeCPFComMascara : "") + ". Nova data de vencimento: " + p.Vencimento.Data.ToShortDateString();
        }

        if (hfTipoProrrogacao.Value.ToUpper() == "OUTROS")
        {
            Condicional outros = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());

            lblAlteracao.Text = "Uma nova prorrogação foi criada para um Outro de " + (outros.GetType() == typeof(OutrosProcesso) ? "Processo, do Processo nº: " + (outros != null && outros.GetProcesso != null ? outros.GetProcesso.Numero : "") : "Empresa") + ", da Empresa:  " + (outros != null && outros.GetEmpresa != null ? outros.GetEmpresa.GetNumeroCNPJeCPFComMascara : "") + ". Nova data de vencimento: " + p.Vencimento.Data.ToShortDateString();
        }

        lblAlteracaoStatus_ModalPopupExtender.Show();

        this.CarregarListaEmails(ckbEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsGrupoEconomico().Split(';'));
        this.CarregarListaEmails(ckbConsultoria, this.CarregarEmailsConsultora().Split(';'));
    }

    protected void lkbProrrogacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upProrrogacoes);
    }

    public string bindNovaData(object o)
    {
        ProrrogacaoPrazo p = (ProrrogacaoPrazo)o;
        return p.DataProtocoloAdicional.AddDays(p.PrazoAdicional).ToShortDateString();
    }

    private void CarregarConsultoriasProcesso()
    {
        if (ddlConsultoria.Items.Count <= 0)
        {
            ddlConsultoria.DataTextField = "Nome";
            ddlConsultoria.DataValueField = "Id";
            ddlConsultoria.DataSource = Consultora.ConsultarTodos();
            ddlConsultoria.DataBind();
            ddlConsultoria.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void RemoverVencimento()
    {
        if (ddlVencimentos.Items.Count <= 1)
        {
            msg.CriarMensagem("Não é possivel excluir este vencimento.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Vencimento vencimento = Vencimento.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
        vencimento.Excluir();
        ddlVencimentos.Items.RemoveAt(ddlVencimentos.SelectedIndex);
        this.CarregarVencimento();
    }

    private void CarregarStatus(DropDownList ddl)
    {
        if (ddl.Items.Count > 1)
            return;

        ddl.DataTextField = "Nome";
        ddl.DataValueField = "Id";
        IList<Status> lista = Status.ConsultarTodos();
        ddl.DataSource = lista;
        ddl.DataBind();

        ddl.SelectedIndex = 0;
    }

    private void CarregarVencimentos(IList<Vencimento> vencimentos, string type, int idObjetoManipulado)
    {
        hfTypeVencimento.Value = type;
        hfIDVencimento.Value = idObjetoManipulado.ToString();

        this.CarregarStatus(ddlStatusVencimentos);

        ddlVencimentos.DataSource = vencimentos;
        ddlVencimentos.DataBind();

        this.CarregarVencimento();

        btnPopUpVencimentos_popupextender.Show();
    }

    private void SalvarVencimento()
    {
        Vencimento vencimento = Vencimento.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32());
        this.VerificarAlteracaoDeStatusHistoricoVencimento(ddlVencimentos.SelectedIndex);
        vencimento.Status = Status.ConsultarPorId(ddlStatusVencimentos.SelectedValue.ToInt32());
        msg.CriarMensagem("Vencimento salvo com Sucesso!", "Sucesso");
    }

    private void CarregarEstadoCadastroLicenca()
    {
        IList<Estado> estados = Estado.ConsultarTodos();
        ddlLicencaCadastroEstado.DataValueField = "Id";
        ddlLicencaCadastroEstado.DataTextField = "Nome";
        ddlLicencaCadastroEstado.DataSource = estados != null ? estados : new List<Estado>();
        ddlLicencaCadastroEstado.DataBind();
        ddlLicencaCadastroEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarCidadeCadastroLicenca()
    {
        Estado estado = Estado.ConsultarPorId(ddlLicencaCadastroEstado.SelectedValue.ToInt32());
        if (estado != null)
        {
            IList<Cidade> cidades = estado.Cidades;
            ddlLicencaCadastroCidade.DataValueField = "Id";
            ddlLicencaCadastroCidade.DataTextField = "Nome";
            ddlLicencaCadastroCidade.DataSource = cidades != null ? cidades : new List<Cidade>();
            ddlLicencaCadastroCidade.DataBind();
            ddlLicencaCadastroCidade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
        else
        {
            ddlLicencaCadastroCidade.Items.Clear();
            ddlLicencaCadastroCidade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

    }

    private void CarregarVencimento()
    {
        Vencimento vencimento = new Vencimento(ddlVencimentos.SelectedValue);
        vencimento = vencimento.ConsultarPorId();


        ddlStatusVencimentos.SelectedValue = vencimento.Status != null ? vencimento.Status.Id.ToString() : "3";
        grvNotificacaoVencimentos.DataSource = vencimento.Notificacoes;
        grvNotificacaoVencimentos.DataBind();
    }

    private void CarregarPopUpCadastroProcesso()
    {
        this.CarregarOrgaosAmbientaisPOPUP(new List<OrgaoAmbiental>());
        this.CarregarConsultoraPOPUP();
        lkbOpcoesProcessoNovo_ModalPopupExtender.Show();
    }

    private void CarregarOrgaosAmbientaisPOPUP(object lista)
    {
        ddlOrgaoProcesso.DataTextField = "Nome";
        ddlOrgaoProcesso.DataValueField = "Id";
        ddlOrgaoProcesso.DataSource = lista;
        ddlOrgaoProcesso.DataBind();
        ddlOrgaoProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarConsultoraPOPUP()
    {
        IList<Consultora> lista = Consultora.ConsultarTodos();
        ddlConsultoraProcesso.DataSource = lista;
        ddlConsultoraProcesso.DataBind();
        ddlConsultoraProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarStatusCondicionantePOPUP()
    {
        IList<Status> lista = Status.ConsultarTodos();
        ddlStatusCondicionante.DataSource = lista;
        ddlStatusCondicionante.DataBind();
        ddlStatusCondicionante.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarProcessosPOPUP()
    {
        IList<Processo> lista = Processo.ConsultarTodos();
        ddlProcessoLicenca.DataSource = lista;
        ddlProcessoLicenca.DataBind();
        ddlProcessoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarOutrosPOPUP()
    {        
        this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaOutros);        
        this.CarregarConsultoriaGeralOutros();
        this.CarregarStatusOutros();
        this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaGeralOutros);
        this.CarregarOrgaosAmbientaisGeralOutros();
    }

    private void CarregarEstadosProcesso()
    {
        ddlEstadoProcesso.DataSource = Estado.ConsultarTodos();
        ddlEstadoProcesso.DataBind();
        ddlEstadoProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarEmpresasCTF()
    {
        ddlEmpresaCTF.Items.Clear();
        if (ddlGrupoEconomicos.SelectedIndex > 0)
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
            if (c != null && c.Empresas != null)
                foreach (Empresa emp in c.Empresas)
                    if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                        ddlEmpresaCTF.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                    else
                        ddlEmpresaCTF.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            ddlEmpresaCTF.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void AdicionarConfirmacoes()
    {
        WebUtil.AdicionarConfirmacao(lkbOpcoesProcessoExcluir, "Deseja realmente excluír este Processo!");
        WebUtil.AdicionarConfirmacao(lkbOpcoesLicencaExcluir, "Deseja realmente excluír esta Licença!");
        WebUtil.AdicionarConfirmacao(lkbOpcoesExcluirCTF, "Deseja realmente excluír este Cadastro Técnico Federal?");
    }

    private void MarcarDropDownList()
    {
        if (hfIdGrupoEconomico.Value.IsNotNullOrEmpty())
        {
            ddlGrupoEconomicos.SelectedValue = hfIdGrupoEconomico.Value;
        }
        if (hfIdEmpresa.Value.IsNotNullOrEmpty())
        {
            ddlEmpresa.SelectedValue = hfIdEmpresa.Value;
        }
    }

    private void CarregarGrupoEconomicos()
    {
        ddlGrupoEconomicos.DataTextField = "Nome";
        ddlGrupoEconomicos.DataValueField = "Id";
        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarGruposAtivos();
        ddlGrupoEconomicos.DataSource = grupos;
        ddlGrupoEconomicos.DataBind();
        ddlGrupoEconomicos.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarArvore()
    {
        trvProcessos.Nodes.Clear();
        if (hfIdGrupoEconomico.Value.IsNotNullOrEmpty())
        {
            GrupoEconomico cli = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());
            Empresa emp = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());
            this.CarregarEmpresa(cli);

            ddlGrupoEconomicos.SelectedValue = hfIdGrupoEconomico.Value;
            if (hfIdEmpresa.Value.ToInt32() > 0)
                ddlEmpresa.SelectedValue = hfIdEmpresa.Value;

            this.CarregarOrgaos(cli, emp);

            OrgaoAmbiental org = OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32());

            IList<Processo> processos = Processo.ConsultarPorOrgaoEmpresaGrupoEconomico(org, Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()), GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()));

            if (processos.Count > 0)
                if (org.GetType() == typeof(OrgaoMunicipal))
                {//processos municipais
                    OrgaoMunicipal om = (OrgaoMunicipal)org;

                    TreeNode noPai = new TreeNode("<b>Órgão:</b> " + om.Nome + " " + om.Cidade.Nome + " - " + om.Cidade.Estado.PegarSiglaEstado(), "n_" + om.Id);
                    foreach (Processo p in processos)
                    {
                        TreeNode noProc = new TreeNode("<b>Processo:</b> " + p.Numero, "p_" + p.Id);
                        if (p.Licencas != null)
                        {
                            foreach (Licenca l in p.Licencas)
                            {
                                TreeNode noLic = new TreeNode("<b>" + (l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + ":</b> " + l.Numero, "l_" + l.Id);
                                noProc.ChildNodes.Add(noLic);
                            }
                            noProc.ChildNodes.Add(new TreeNode("<b>Outros</b>", "s_1"));
                            noPai.ChildNodes.Add(noProc);
                        }
                    }
                    trvProcessos.Nodes.Add(noPai);

                    trvProcessos.ExpandAll();
                }
                else
                {//outros processos
                    TreeNode noPai = new TreeNode("<b>Orgão:</b> " + org.Nome, "n_" + org.Id);
                    noPai.ChildNodes.Add(new TreeNode("<b>Outros</b>", "o_1"));
                    foreach (Processo p in processos)
                    {
                        TreeNode noProc = new TreeNode("<b>Processo:</b> " + p.Numero, "p_" + p.Id);
                        if (p.Licencas != null)
                            foreach (Licenca l in p.Licencas)
                            {
                                TreeNode noLicenca = new TreeNode("<b>" + (l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + ":</b> " + l.Numero, "l_" + l.Id);
                                noProc.ChildNodes.Add(noLicenca);
                            }
                        noProc.ChildNodes.Add(new TreeNode("<b>Outros</b>", "s_1"));
                        noPai.ChildNodes.Add(noProc);
                    }

                    trvProcessos.Nodes.Add(noPai);
                    trvProcessos.ExpandAll();
                }
        }
    }

    private void CarregarArvore2()
    {
        trvProcessos.Nodes.Clear();
        if (hfIdGrupoEconomico.Value.IsNotNullOrEmpty())
        {
            GrupoEconomico cli = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());
            Empresa emp = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());
            this.CarregarEmpresa(cli);

            ddlGrupoEconomicos.SelectedValue = hfIdGrupoEconomico.Value;
            if (hfIdEmpresa.Value.ToInt32() > 0)
                ddlEmpresa.SelectedValue = hfIdEmpresa.Value;

            this.CarregarOrgaos(cli, emp);

            if (hfIdOrgao.Value.ToInt32() == -1)
            {//processos municipais
                IList<OrgaoMunicipal> oms = new List<OrgaoMunicipal>();
                if (ddlEmpresa.SelectedIndex > 0)
                    oms = OrgaoMunicipal.ConsultarOrgaosAmbientaisDaEmpresa(Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()));
                else
                    oms = OrgaoMunicipal.ConsultarOrgaosAmbientaisDoGrupoEconomico(GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()));

                foreach (OrgaoMunicipal om in oms)
                {
                    TreeNode noPai = new TreeNode("<b>Órgão:</b> " + om.Nome + " " + om.Cidade.Nome + " - " + om.Cidade.Estado.PegarSiglaEstado(), "n_" + om.Id);
                    foreach (Processo p in om.Processos)
                    {
                        TreeNode noProc = new TreeNode("<b>Processo:</b> " + p.Numero, "p_" + p.Id);
                        if (p.Licencas != null)
                        {
                            foreach (Licenca l in p.Licencas)
                            {
                                TreeNode noLic = new TreeNode("<b>" + (l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + ":</b> " + l.Numero, "l_" + l.Id);
                                noProc.ChildNodes.Add(noLic);
                            }
                            noProc.ChildNodes.Add(new TreeNode("<b>Outros</b>", "s_1"));
                            noPai.ChildNodes.Add(noProc);
                        }
                    }
                    trvProcessos.Nodes.Add(noPai);
                }
                trvProcessos.ExpandAll();
            }
            if (hfIdOrgao.Value.ToInt32() == -4)
            { //cadastro tecnico federal
                IList<CadastroTecnicoFederal> ctfs = new List<CadastroTecnicoFederal>();
                if (ddlEmpresa.SelectedIndex > 0)
                    ctfs.Add(CadastroTecnicoFederal.ConsultarPorEmpresa(hfIdEmpresa.Value.ToInt32()));
                else
                    ctfs = CadastroTecnicoFederal.ConsultarPorGrupoEconomico(hfIdGrupoEconomico.Value.ToInt32());

                foreach (CadastroTecnicoFederal ctf in ctfs)
                {
                    TreeNode noPai = new TreeNode("<b>CNPJ:</b> " + ((DadosJuridica)ctf.Empresa.DadosPessoa).Cnpj, "c_" + ctf.Id);
                    trvProcessos.Nodes.Add(noPai);
                }
            }
        }
    }

    private void CarregarOrgaos(GrupoEconomico c, Empresa e)
    {
        trvProcessos.Nodes.Clear();
        //Carregar Processos Municipal
        IList<OrgaoMunicipal> oms = e != null ? OrgaoMunicipal.ConsultarOrgaosAmbientaisDaEmpresa(e) : c != null ?
            OrgaoMunicipal.ConsultarOrgaosAmbientaisDoGrupoEconomicoSTATUS(c, ddlStatusEmpresa.SelectedValue) : new List<OrgaoMunicipal>();
        //for (int i = oms.Count - 1; i >= 0; i--)
        //    if (oms[i].Processos.Count <= 0)
        //        oms.RemoveAt(i);
        oms = this.ContadorProcessos(oms, e);
        dtlMunicipal.DataSource = oms;
        dtlMunicipal.DataBind();
        //Carregar Processos Estadual
        IList<OrgaoEstadual> oes = e != null ? OrgaoEstadual.ConsultarOrgaosAmbientaisDaEmpresa(e) : c != null ?
            OrgaoEstadual.ConsultarOrgaosAmbientaisDoGrupoEconomicoSTATUS(c, ddlStatusEmpresa.SelectedValue) : new List<OrgaoEstadual>();
        //for (int i = oes.Count - 1; i >= 0; i--)
        //    if (oes[i].Processos.Count <= 0)
        //        oes.RemoveAt(i);
        oes = this.ContadorProcessos(oes, e);
        dtlEstadual.DataSource = oes;
        dtlEstadual.DataBind();
        //Carregar Processos Federal
        IList<OrgaoFederal> ofs = e != null ? OrgaoFederal.ConsultarOrgaosAmbientaisDaEmpresa(e) : c != null ?
            OrgaoFederal.ConsultarOrgaosAmbientaisDoGrupoEconomico(c) : new List<OrgaoFederal>();
        //for (int i = ofs.Count - 1; i >= 0; i--)
        //    if (ofs[i].Processos.Count <= 0)
        //        ofs.RemoveAt(i);
        ofs = this.ContadorProcessos(ofs, e);
        dtlFederal.DataSource = ofs;
        dtlFederal.DataBind();
        //Carregar CTF
        OrgaoAmbiental oa = new OrgaoAmbiental();
        oa.Nome = "Processos Federais";
        oa.Id = -4;
        IList<OrgaoAmbiental> oas = new List<OrgaoAmbiental>();
        if (c != null || e != null)
            oas.Add(oa);
        dtlCTF.DataSource = oas;
        dtlCTF.DataBind();
        //Carregar Outros
        OrgaoAmbiental oa1 = new OrgaoAmbiental();
        oa1.Nome = "Outros";
        oa1.Id = -5;
        IList<OrgaoAmbiental> oas1 = new List<OrgaoAmbiental>();
        if (c != null || e != null)
            oas1.Add(oa1);
        dtlOutros.DataSource = oas1;
        dtlOutros.DataBind();

        mvwProcessos.ActiveViewIndex = -1;
    }

    private IList<OrgaoMunicipal> ContadorProcessos(IList<OrgaoMunicipal> oms, Empresa e)
    {
        for (int i = oms.Count - 1; i >= 0; i--)
            if (oms[i].Processos.Count <= 0)
                oms.RemoveAt(i);
            else
                if (e != null)
                {
                    int cont = 0;
                    foreach (Processo p in oms[i].Processos)
                        if (p.Empresa != e)
                            cont++;
                    if (oms[i].Processos.Count <= cont)
                        oms.RemoveAt(i);
                }
        return oms;
    }

    private IList<OrgaoFederal> ContadorProcessos(IList<OrgaoFederal> oms, Empresa e)
    {
        for (int i = oms.Count - 1; i >= 0; i--)
            if (oms[i].Processos.Count <= 0)
                oms.RemoveAt(i);
            else
                if (e != null)
                {
                    int cont = 0;
                    foreach (Processo p in oms[i].Processos)
                        if (p.Empresa != e)
                            cont++;
                    if (oms[i].Processos.Count <= cont)
                        oms.RemoveAt(i);
                }
        return oms;
    }

    private IList<OrgaoEstadual> ContadorProcessos(IList<OrgaoEstadual> oms, Empresa e)
    {
        for (int i = oms.Count - 1; i >= 0; i--)
            if (oms[i].Processos.Count <= 0)
                oms.RemoveAt(i);
            else
                if (e != null)
                {
                    int cont = 0;
                    foreach (Processo p in oms[i].Processos)
                        if (p.Empresa != e)
                            cont++;
                    if (oms[i].Processos.Count <= cont)
                        oms.RemoveAt(i);
                }
        return oms;
    }

    public IList<OrgaoAmbiental> BindDataSource(Object o)
    {
        OrgaoAmbiental oa = (OrgaoAmbiental)o;

        GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
        Empresa e = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());

        IList<OrgaoAmbiental> orgaos = new List<OrgaoAmbiental>();
        IList<OrgaoAmbiental> list = new List<OrgaoAmbiental>();
        IList<OrgaoAmbiental> orgs = new List<OrgaoAmbiental>();

        if (c != null)
            orgaos = OrgaoAmbiental.ConsultarOrgaosAmbientaisDoGrupoEconomico(c);
        if (e != null)
            orgaos = OrgaoAmbiental.ConsultarOrgaosAmbientaisDaEmpresa(e);

        foreach (OrgaoAmbiental o2 in orgaos)
        {
            OrgaoAmbiental o1 = new OrgaoAmbiental();
            if (o2.GetType() == typeof(OrgaoMunicipal))
                o1 = new OrgaoMunicipal();
            if (o2.GetType() == typeof(OrgaoEstadual))
                o1 = new OrgaoEstadual();
            if (o2.GetType() == typeof(OrgaoFederal))
                o1 = new OrgaoFederal();

            IList<Processo> processos = Processo.ConsultarPorOrgaoEmpresaGrupoEconomico(o2, Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()), GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()));

            if (processos.Count > 0)
            {
                o1.Processos = processos;
                o1.Nome = o2.Nome;
                o1.Id = o2.Id;
                orgs.Add(o1);
            }
        }

        if (oa.Id == -1)
        {
            foreach (OrgaoAmbiental org in orgs)
                if (org.GetType() == typeof(OrgaoMunicipal))
                    if (org.Processos != null && org.Processos.Count > 0)
                        list.Add(org);
            return list;
        }
        if (oa.Id == -2)
        {
            foreach (OrgaoAmbiental org in orgs)
                if (org.GetType() == typeof(OrgaoEstadual))
                    if (org.Processos != null && org.Processos.Count > 0)
                        list.Add(org);
            return list;
        }
        if (oa.Id == -3)
        {
            foreach (OrgaoAmbiental org in orgs)
                if (org.GetType() == typeof(OrgaoFederal))
                    if (org.Processos != null && org.Processos.Count > 0)
                        list.Add(org);
            return list;
        }
        if (oa.Id == -4)
        {
            OrgaoAmbiental octf = new OrgaoAmbiental();
            octf.Nome = "Processos Federais";
            octf.Id = -4;
            list.Add(octf);
            return list;
        }
        if (oa.Id == -5)
        {
            OrgaoAmbiental ooo = new OrgaoAmbiental();
            ooo.Nome = "";
            ooo.Id = -5;
            list.Add(ooo);
            return list;
        }

        return list;
    }

    private void CarregarEmpresa(GrupoEconomico c)
    {
        ddlEmpresa.Items.Clear();        

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (c != null && c.Id > 0)
                empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == c.Id).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente : new List<Empresa>();
        }
        else
            empresas = c != null && c.Empresas != null ? c.Empresas : new List<Empresa>();


        if (empresas != null && empresas.Count > 0)
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));                    
                }
                else
                {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));                    
                }
            }

        ddlEmpresa.Items.Insert(0, new ListItem("-- Selecione --", "0"));        
    }
    private void CarregarEmpresaComStatus(GrupoEconomico c, String status)
    {
        ddlEmpresa.Items.Clear();

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (c != null && c.Id > 0)
                empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == c.Id).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloMeioAmbiente != null ? this.EmpresasPermissaoModuloMeioAmbiente : new List<Empresa>();
        }
        else
            empresas = c != null && c.Empresas != null ? c.Empresas : new List<Empresa>();


        if (empresas != null && empresas.Count > 0)
            empresas = empresas.OrderBy(x => x.Nome).ToList();
        foreach (Empresa emp in empresas)
        {
            if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
            {
                if (status == "Todos")
                {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                }
                else if (status == "Ativo" && emp.Ativo == true) {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                }
                else if (status == "Inativo" && emp.Ativo == false) {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                }
                else {

                }
               
            }
            else
            {
                if (status == "Todos")
                {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                }
                else if (status == "Ativo" && emp.Ativo == true)
                {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                }
                else if (status == "Inativo" && emp.Ativo == false)
                {
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                }
                else
                {

                }
               
            }
        }

        ddlEmpresa.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarEmpresasQueOUsuarioEdita(DropDownList dropEmpresa)
    {
        dropEmpresa.Items.Clear();

        IList<Empresa> empresas = null;

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
            {
                empresas = ddlGrupoEconomicos.SelectedValue.ToInt32() > 0 ? GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()).Empresas : Empresa.ConsultarTodos();
            }            
        }            

        
        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            empresas = this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null ? this.EmpresasPermissaoEdicaoModuloMeioAmbiente : new List<Empresa>();


        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO) 
        {
            if(this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null || this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null || this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente != null)
            {
                empresas = ddlGrupoEconomicos.SelectedValue.ToInt32() > 0 ? GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()).Empresas : Empresa.ConsultarTodos();
            }
        }

        if (empresas != null && empresas.Count > 0)
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))                    
                    dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else                    
                    dropEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        
        dropEmpresa.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarConsultoriaGeralOutros()
    {
        if (!(ddlConsultoriaOutros.Items.Count > 0))
        {
            ddlConsultoriaOutros.DataValueField = "Id";
            ddlConsultoriaOutros.DataTextField = "Nome";
            ddlConsultoriaOutros.DataSource = Consultora.ConsultarTodos();
            ddlConsultoriaOutros.DataBind();
            ddlConsultoriaOutros.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void CarregarStatusOutros()
    {
        if (!(ddlStatusOutros.Items.Count > 0))
        {
            ddlStatusOutros.DataValueField = "Id";
            ddlStatusOutros.DataTextField = "Nome";
            ddlStatusOutros.DataSource = Status.ConsultarTodos();
            ddlStatusOutros.DataBind();
        }
    }

    private void CarregarEmpresasGeralOutros()
    {
        if (!(ddlEmpresaGeralOutros.Items.Count > 0))
        {
            foreach (Empresa emp in GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()).Empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresaGeralOutros.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresaGeralOutros.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
            ddlEmpresaGeralOutros.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

    }

    private void CarregarOrgaosAmbientaisGeralOutros()
    {
        if (!(ddlOrgaoGeralOutros.Items.Count > 0))
        {
            ddlOrgaoGeralOutros.DataValueField = "Id";
            ddlOrgaoGeralOutros.DataTextField = "Nome";
            ddlOrgaoGeralOutros.DataSource = OrgaoAmbiental.ConsultarTodos();
            ddlOrgaoGeralOutros.DataBind();
            ddlOrgaoGeralOutros.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void CarregarEmpresasOutros()
    {
        if (!(ddlEmpresaOutros.Items.Count > 0))
        {
            GrupoEconomico g = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
            if (g != null && g.Empresas != null)
            {
                foreach (Empresa emp in g.Empresas)
                {
                    if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                        ddlEmpresaOutros.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                    else
                        ddlEmpresaOutros.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
                }
            }
            ddlEmpresaOutros.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }
    }

    private void CarregarProcessosOutros()
    {
        IList<Processo> processos = Processo.ConsultarPorEmpresaEOrgao(Empresa.ConsultarPorId(ddlEmpresaOutros.SelectedValue.ToInt32()),
            OrgaoAmbiental.ConsultarPorId(ddlOrgaoOutros.SelectedValue.ToInt32()));
        ddlProcessoOutros.DataValueField = "Id";
        ddlProcessoOutros.DataTextField = "Numero";
        ddlProcessoOutros.DataSource = processos;
        ddlProcessoOutros.DataBind();
        ddlProcessoOutros.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarOrgaosAmbientaisOutros()
    {
        IList<OrgaoAmbiental> orgaos = OrgaoAmbiental.ConsultarPorEmpresa(ddlEmpresaOutros.SelectedValue.ToInt32()); ;
        ddlOrgaoOutros.DataValueField = "Id";
        ddlOrgaoOutros.DataTextField = "Nome";
        ddlOrgaoOutros.DataSource = orgaos;
        ddlOrgaoOutros.DataBind();
        ddlOrgaoOutros.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void RadioChange()
    {
        mvOutros.ActiveViewIndex = rbtnProcessoOutros.Checked ? 0 : 1;
        hfTipoOutros.Value = rbtnGeralOutros.Checked ? "emp" : "proc";
    }

    private void ImportarCondicionantes()
    {
        if (trvProcessos.SelectedNode != null)
        {
            Licenca l = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
            if (l != null)
                if (l.TipoLicenca != null)
                {
                    IList<CondicionantePadrao> cps = l.TipoLicenca.CondicionantesPadroes;
                    IList<Condicionante> conds = new List<Condicionante>();
                    foreach (CondicionantePadrao cp in cps)
                    {
                        if (!this.CondicionantePadraoJaAdicionada(cp, l))
                        {
                            Condicionante c = new Condicionante();
                            c.Descricao = cp.Descricao;
                            c.DiasPrazo = cp.DiasValidade;
                            Vencimento v = c.GetUltimoVencimento;
                            v.Data = l.DataRetirada.AddDays(c.DiasPrazo);
                            v.Notificacoes = new List<Notificacao>();
                            v.Periodico = cp.Periodico;
                            v = v.Salvar();
                            c.SetUltimoVencimento = v;
                            c.Licenca = l;
                            c = c.Salvar();
                            conds.Add(c);
                        }
                    }
                    msg.CriarMensagem("Condicionantes importadas com Sucesso! </br></br>Obs.:  É necessario que insira seus vencimentos.", "Sucesso", MsgIcons.Sucesso);
                    if (conds != null && conds.Count > 0)
                    {
                        if (l.Condicionantes != null)
                        {
                            l.Condicionantes.AddRange<Condicionante>(conds);
                            grvCondicionantes.DataSource = l.Condicionantes.OrderBy(i => i.Numero).ToList();
                            grvCondicionantes.DataBind();
                        }
                        else
                        {
                            grvCondicionantes.DataSource = conds.OrderBy(i => i.Numero).ToList();
                            grvCondicionantes.DataBind();
                        }

                    }
                }
                else
                    msg.CriarMensagem("Tipo da Licença não definido. Não é possível importar condicionantes.", "Alerta", MsgIcons.Alerta);
        }
    }

    private bool CondicionantePadraoJaAdicionada(CondicionantePadrao cp, Licenca l)
    {
        bool ja = false;
        if (l.Condicionantes != null)
            foreach (Condicionante c in l.Condicionantes)
                if (cp.Descricao == c.Descricao && cp.DiasValidade == c.DiasPrazo)
                    ja = true;
        return ja;
    }

    private void CarregarCidade(int id)
    {
        Estado estado = Estado.ConsultarPorId(id);
        if (estado != null)
        {
            ddlCidadeProcesso.DataValueField = "Id";
            ddlCidadeProcesso.DataTextField = "Nome";
            ddlCidadeProcesso.DataSource = estado.Cidades;
            ddlCidadeProcesso.DataBind();
        }
        else
        {
            ddlCidadeProcesso.Items.Clear();
        }
        ddlCidadeProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void SalvarCTF()
    {
        if (ddlEmpresaCTF.SelectedIndex <= 0)
        {
            msg.CriarMensagem("Selecione uma empresa.", "Alerta", MsgIcons.Alerta);
            return;
        }

        string mens = "";

        if (this.objetoCTF.GetUltimoRelatorio.Notificacoes == null || this.objetoCTF.GetUltimoRelatorio.Notificacoes.Count <= 0)
            mens += "</br>Não foram cadastradas notificações para o Relatório.";
        if (this.objetoCTF.GetUltimoPagamento.Notificacoes == null || this.objetoCTF.GetUltimoPagamento.Notificacoes.Count <= 0)
            mens += "</br>Não foram cadastradas notificações para a Taxa Trimestral.";
        if (this.objetoCTF.GetUltimoCertificado.Notificacoes == null || this.objetoCTF.GetUltimoCertificado.Notificacoes.Count <= 0)
            mens += "</br>Não foram cadastradas notificações para o Certificado.";

        CadastroTecnicoFederal cadastro = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(cadastro);
        if (cadastro == null)
            cadastro = new CadastroTecnicoFederal();

        Empresa empUsada = cadastro.Empresa;
        Empresa empSel = Empresa.ConsultarPorId(ddlEmpresaCTF.SelectedValue.ToInt32());

        if (empSel.CadastroTecnicoFederal != null) //se tem CTF associado
            if (cadastro.Id > 0) // se for alteração
            {
                if (empUsada != empSel)
                {
                    msg.CriarMensagem("Cadastro Técnico Federal existente.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
            {
                msg.CriarMensagem("Cadastro Técnico Federal existente.", "Alerta", MsgIcons.Alerta);
                return;
            }

        cadastro.Empresa = empSel;
        cadastro.Senha = tbxSenhaCTF.Text;
        cadastro.Atividade = tbxAtividadesCTF.Text;
        cadastro.NumeroLicenca = tbxNumeroLicencaCTF.Text;
        cadastro.ValidadeLicenca = tbxDataValidadeLicenca.Text.ToSqlDateTime();
        cadastro.Observacoes = tbxObservacaoCTF.Text;
        cadastro.Consultora = Consultora.ConsultarPorId(ddlConsultoria.SelectedValue.ToInt32());
        cadastro.Arquivos = this.ReconsultarArquivos(arquivosUpload);

        // salvando vencimento do relatorio
        Vencimento vencimentoRelatorioCTF = cadastro.GetUltimoRelatorio;
        vencimentoRelatorioCTF.Data = tbxDataEntregaRelatorioCTF.Text.ToSqlDateTime();
        vencimentoRelatorioCTF.Status = Status.ConsultarPorId(ddlEstatusRelatorioAnual.SelectedValue.ToInt32());
        if (this.objetoCTF.GetUltimoRelatorio.Notificacoes != null)
            foreach (Notificacao n in this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoRelatorio.Notificacoes))
            {
                if (vencimentoRelatorioCTF.Notificacoes == null)
                    vencimentoRelatorioCTF.Notificacoes = new List<Notificacao>();
                if (!vencimentoRelatorioCTF.Notificacoes.Contains(n))
                    vencimentoRelatorioCTF.Notificacoes.Add(n);
            }

        vencimentoRelatorioCTF = vencimentoRelatorioCTF.Salvar();
        cadastro.SetUltimoRelatorio = vencimentoRelatorioCTF;

        //salvando vencimentos do pagamento trimestral
        Vencimento pagamentoTrimestralCTF = cadastro.GetUltimoPagamento;
        pagamentoTrimestralCTF.Status = Status.ConsultarPorId(ddlEstatusTaxaTrimestral.SelectedValue.ToInt32());
        pagamentoTrimestralCTF.Data = tbxDataPagamentoCTF.Text.ToSqlDateTime();

        if (this.objetoCTF.GetUltimoPagamento.Notificacoes != null)
            foreach (Notificacao n in this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoPagamento.Notificacoes))
            {
                if (pagamentoTrimestralCTF.Notificacoes == null)
                    pagamentoTrimestralCTF.Notificacoes = new List<Notificacao>();
                if (!pagamentoTrimestralCTF.Notificacoes.Contains(n))
                    pagamentoTrimestralCTF.Notificacoes.Add(n);
            }

        pagamentoTrimestralCTF = pagamentoTrimestralCTF.Salvar();
        cadastro.SetUltimoPagamento = pagamentoTrimestralCTF;

        //salvando vencimentos do certificado
        Vencimento certificadoCTF = cadastro.GetUltimoCertificado;
        certificadoCTF.Data = tbxDataEntregaCertificadoCTF.Text.ToSqlDateTime();
        certificadoCTF.Status = Status.ConsultarPorId(ddlEstatusCertificado.SelectedValue.ToInt32());

        if (this.objetoCTF.GetUltimoCertificado.Notificacoes != null)
            foreach (Notificacao n in this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoCertificado.Notificacoes))
            {
                if (certificadoCTF.Notificacoes == null)
                    certificadoCTF.Notificacoes = new List<Notificacao>();
                if (!certificadoCTF.Notificacoes.Contains(n))
                    certificadoCTF.Notificacoes.Add(n);
            }

        certificadoCTF = certificadoCTF.Salvar();
        cadastro.SetUltimoCertificado = certificadoCTF;
        cadastro = cadastro.Salvar();
        HfIdCTF.Value = cadastro.Id.ToString();

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (cadastro.UsuariosEdicao == null)
                cadastro.UsuariosEdicao = new List<Usuario>();

            if (cadastro.UsuariosVisualizacao != null && cadastro.UsuariosVisualizacao.Count > 0)
            {
                if (!cadastro.UsuariosVisualizacao.Contains(this.UsuarioLogado))
                    cadastro.UsuariosVisualizacao.Add(this.UsuarioLogado);
            }

            if (!cadastro.UsuariosEdicao.Contains(this.UsuarioLogado))
                cadastro.UsuariosEdicao.Add(this.UsuarioLogado);

            cadastro = cadastro.Salvar();

            if (this.CadastrosTecnicosPermissaoModuloMeioAmbiente == null)
                this.CadastrosTecnicosPermissaoModuloMeioAmbiente = new List<CadastroTecnicoFederal>();

            if (!this.CadastrosTecnicosPermissaoModuloMeioAmbiente.Contains(cadastro))
                this.CadastrosTecnicosPermissaoModuloMeioAmbiente.Add(cadastro);


            if (this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente == null)
                this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente = new List<CadastroTecnicoFederal>();

            if (!this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Contains(cadastro))
                this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Add(cadastro);

        }

        msg.CriarMensagem("Cadastro Técnico Federal salvo com sucesso.</br>" + mens, "Sucesso", MsgIcons.Sucesso);
        if (!mens.IsNotNullOrEmpty())
            lblCTF_ModalPopupExtender.Hide();
        this.CarregarOrgaos(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()), Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()));

        this.CarregarArvoreCadastroTecnicoFederal();

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (cadastro != null && cadastro.GetUltimoRelatorio != null && cadastro.GetUltimoRelatorio.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = cadastro.Id;
            item.tipoItem = "RELATORIOCTF";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(cadastro.EntregaRelatorioAnual);
            this.ItensRenovacao.Add(item);
        }

        if (cadastro != null && cadastro.GetUltimoPagamento != null && cadastro.GetUltimoPagamento.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = cadastro.Id;
            item.tipoItem = "TAXACTF";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(cadastro.TaxaTrimestral);
            this.ItensRenovacao.Add(item);
        }

        if (cadastro != null && cadastro.GetUltimoCertificado != null && cadastro.GetUltimoCertificado.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = cadastro.Id;
            item.tipoItem = "CERTIFICADOCTF";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(cadastro.CertificadoRegularidade);
            this.ItensRenovacao.Add(item);
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }
    }

    private void SalvarCondicionante()
    {
        if (ddlStatusCondicionante.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione um Status para esta Condicionante!", "Alerta", MsgIcons.Alerta);
            return;
        }

        string mens = "";

        if (this.notificacoesSalvas == null || this.notificacoesSalvas.Count <= 0)
            mens += "</br></br>Não foram cadastradas notificações para a Condicionante.";

        Condicionante c = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
        this.VerificarAlteracaoDeStatus(c);

        if (c == null)
            c = new Condicionante();

        c.Licenca = Licenca.ConsultarPorId(ddlLicencaCondicionante.SelectedValue.ToInt32());
        c.Numero = tbxNumeroCondicionante.Text;
        c.Descricao = tbxDescricaoCondicionante.Text;
        c.Observacoes = tbxObservacoesCondicionante.Text;
        c.DiasPrazo = tbxDiasPrazoCondicionante.Text.ToInt32();
        c.Arquivos = this.ReconsultarArquivos(arquivosUpload);

        if (c.GetUltimoVencimento == null)
            c.SetUltimoVencimento = new Vencimento();

        Vencimento v = c.GetUltimoVencimento;
        v.ProtocoloAtendimento = tbxProtocoloCondicionante.Text;
        v.DataAtendimento = tbxDataAtendimentoCondicionante.Text.ToSqlDateTime();
        v.Status = Status.ConsultarPorId(ddlStatusCondicionante.SelectedValue.ToInt32());
        v.Periodico = cbxPeriodicaCondicionante.Checked;

        if (v.ProrrogacoesPrazo == null || (v.ProrrogacoesPrazo != null && v.ProrrogacoesPrazo.Count == 0))
        {
            v.Data = this.CalcularDataVencimentoCondicionante(c);
        }


        if (this.notificacoesSalvas != null)
        {
            foreach (Notificacao n in this.ReconsultarNotificacoes(this.notificacoesSalvas))
            {
                if (v.Notificacoes == null)
                    v.Notificacoes = new List<Notificacao>();
                if (!v.Notificacoes.Contains(n))
                    v.Notificacoes.Add(n);
            }
        }

        v = v.Salvar();

        if (v.Notificacoes != null && v.Notificacoes.Count > 0)
            this.notificacoesSalvas = v.Notificacoes;
        else
            this.notificacoesSalvas = null;

        c.SetUltimoVencimento = v;

        c = c.Salvar();

        transacao.Recarregar(ref msg);

        c = c.ConsultarPorId();
        grvCondicionantes.DataSource = c.Licenca.Condicionantes;
        grvCondicionantes.DataBind();

        hfIdCondicionante.Value = c.Id.ToString();

        msg.CriarMensagem("Condicionante salva com Sucesso!" + mens, "Sucesso");
        if (!mens.IsNotNullOrEmpty())
            lkbOpcoesCondicionante_ModalPopupExtender.Hide();

        this.ItensRenovacao = new List<ItemRenovacao>();

        if (c != null && c.GetPeriodico && c.GetUltimoVencimento != null && c.GetUltimoVencimento.Data <= DateTime.Now)
        {
            ItemRenovacao item = new ItemRenovacao();
            item.idItem = c.Id;
            item.tipoItem = "CONDICIONANTE";
            item.diasRenovacao = ObterDiasEntreAsRenovacoes(c.Vencimentos);
            this.ItensRenovacao.Add(item);
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }
    }

    private void ExibirDatasVencimentosPeriodicosRenovacao(int diasRenovacao, IList<Vencimento> vencimentos)
    {
        chkVencimentosPeriodicos.Items.Clear();
        DateTime dataAux = vencimentos[vencimentos.Count - 1].Data;
        while (dataAux <= DateTime.Now)
        {
            dataAux = dataAux.AddDays(+diasRenovacao);
            chkVencimentosPeriodicos.Items.Add(new ListItem("Vencimento: " + dataAux.ToString("dd/MM/yyyy"), dataAux.ToString("dd/MM/yyyy")));
        }
        lblRenovacaoVencimentosPeriodicos_popupextender.Show();
    }

    public int ObterDiasEntreAsRenovacoes(IList<Vencimento> vencimentos)
    {
        if (vencimentos != null && vencimentos.Count > 0)
        {
            if (vencimentos.Count == 1)
            {
                return 0;
            }
            else
            {
                return vencimentos[vencimentos.Count - 1].Data.Subtract(vencimentos[vencimentos.Count - 2].Data).Days;
            }
        }

        return 0;
    }

    private void SalvarLicenca()
    {
        if (!tbxNumeroLicenca.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("O campo número da licença deve ser informado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ddlProcessoLicenca.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione um Processo!", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ddlTipoLicenca.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione um Tipo de Licença!", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!tbxDiasValidadeLicenca.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("O campo dias de válidade deve ser informado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!tbxDataRetiradaLicenca.Text.IsDate())
        {
            msg.CriarMensagem("O campo data de retirada deve ser informado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        string mens = "";
        if (this.notificacoesSalvas == null || this.notificacoesSalvas.Count <= 0)
            mens += "</br>Não foram cadastradas notificações para essa licença.";

        Licenca l = new Licenca();

        if (HfIdLicenca.Value.IsNotNullOrEmpty())
        {
            l = Licenca.ConsultarPorId(HfIdLicenca.Value.ToInt32());
        }

        l.Processo = Processo.ConsultarPorId(ddlProcessoLicenca.SelectedValue.ToInt32());
        l.TipoLicenca = TipoLicenca.ConsultarPorId(ddlTipoLicenca.SelectedValue.ToInt32());
        l.Descricao = tbxDescricaoLicenca.Text;
        l.Numero = tbxNumeroLicenca.Text;
        l.DiasValidade = tbxDiasValidadeLicenca.Text.ToInt32();
        l.DataRetirada = tbxDataRetiradaLicenca.Text.ToSqlDateTime();

        if (ddlLicencaCadastroCidade.SelectedValue.ToInt32() > 0)
            l.Cidade = Cidade.ConsultarPorId(ddlLicencaCadastroCidade.SelectedValue.ToInt32());
        else
            l.Cidade = null;

        Vencimento v = l.GetUltimoVencimento;

        v.Data = l.DataRetirada.AddDays(l.DiasValidade);
        l.PrazoLimiteRenovacao = v.Data.AddDays(-120);

        if (this.notificacoesSalvas != null)
            foreach (Notificacao n in this.ReconsultarNotificacoes(this.notificacoesSalvas))
            {
                if (v.Notificacoes == null)
                    v.Notificacoes = new List<Notificacao>();
                if (!v.Notificacoes.Contains(n))
                    v.Notificacoes.Add(n);
            }

        v = v.Salvar();

        l.SetUltimoVencimento = v;

        l.Arquivos = this.ReconsultarArquivos(arquivosUpload);

        l = l.Salvar();

        HfIdLicenca.Value = l.Id.ToString();

        transacao.Fechar(ref msg);
        transacao.Abrir();

        if (hfIdOrgao.Value.ToInt32() > 0)
            this.CarregarArvore();
        else
            this.CarregarArvore2();

        msg.CriarMensagem("Licença salva com Sucesso!</br>" + mens, "Sucesso");
        if (!mens.IsNotNullOrEmpty())
            btnPopUplicenca_ModalPopupExtender.Hide();
    }

    private IList<ArquivoFisico> ReconsultarArquivos(IList<ArquivoFisico> iList)
    {
        IList<ArquivoFisico> aux = new List<ArquivoFisico>();
        foreach (ArquivoFisico ar in iList)
            aux.Add(ArquivoFisico.ConsultarPorId(ar.Id));
        return aux;
    }

    private void SalvarProcesso()
    {
        string obsMsg = "";

        if (ddlEmpresaProcesso.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione uma Empresa!", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (!tbxNumeroProcesso.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario informar um número para o processo.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ddlOrgaoProcesso.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione um Orgão Ambiental!", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ddlTipoOrgaoProcesso.SelectedIndex == 0 && ddlEstadoProcesso.SelectedIndex <= 0)
        {
            msg.CriarMensagem("Selecione um Estado!", "Alerta", MsgIcons.Alerta);
            return;
        }
        else if (ddlTipoOrgaoProcesso.SelectedIndex == 2 && ddlCidadeProcesso.SelectedIndex < 0)
        {
            msg.CriarMensagem("Selecione uma Cidade!", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()).VerificarSeExcedeuLimiteDeProcessosContratados())
        {
            msg.CriarMensagem("Você já atingiu o limite de Processos (Ambientais + Minerários) cadastrados. ", "Atenção", MsgIcons.Informacao);
            return;
        }

        Processo p = new Processo();

        if (HfIdProcesso.Value.IsNotNullOrEmpty())
        {
            p = Processo.ConsultarPorId(HfIdProcesso.Value.ToInt32());
        }
        else
        {
            if (p.Empresa != null && p.Empresa.Id > 0)
            {
                if (p.Empresa.Id.ToString() != ddlEmpresaProcesso.SelectedValue)
                    obsMsg = "<br/><br/>Atenção: Você alterou a Empresa deste Processo. Caso este Processo ou suas licenças tenham alguma notificação, estas forma mantidas com seus destinatários.";
            }
        }

        if (p.Numero != tbxNumeroProcesso.Text.Trim() && Processo.ConsultarPorNumero(tbxNumeroProcesso.Text.Trim()).Count > 0)
        {
            msg.CriarMensagem("Número do Processo já existe.", "Alerta", MsgIcons.Alerta);
            return;
        }

        p.Consultora = Consultora.ConsultarPorId(ddlConsultoraProcesso.SelectedValue.ToInt32());
        p.Empresa = Empresa.ConsultarPorId(ddlEmpresaProcesso.SelectedValue.ToInt32());
        p.OrgaoAmbiental = OrgaoAmbiental.ConsultarPorId(ddlOrgaoProcesso.SelectedValue.ToInt32());
        p.Observacoes = tbxObservacaoProcesso.Text;
        p.Numero = tbxNumeroProcesso.Text.Trim();
        if (tbxDataAberturaProcesso.Text.IsNotNullOrEmpty())
            p.DataAbertura = tbxDataAberturaProcesso.Text.ToSqlDateTime();
        p = p.Salvar();
        if (p.Id != null && p.Id > 0)
        {
            btnAbrirContratos.Visible = this.UsuarioLogado.PossuiPermissaoDeEditarModuloContratos;
        }

        if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            if (p.UsuariosEdicao == null)
                p.UsuariosEdicao = new List<Usuario>();

            if (p.UsuariosVisualizacao != null && p.UsuariosVisualizacao.Count > 0)
            {
                if (!p.UsuariosVisualizacao.Contains(this.UsuarioLogado))
                    p.UsuariosVisualizacao.Add(this.UsuarioLogado);
            }

            if (!p.UsuariosEdicao.Contains(this.UsuarioLogado))
                p.UsuariosEdicao.Add(this.UsuarioLogado);

            p = p.Salvar();

            if (this.ProcessosPermissaoModuloMeioAmbiente == null)
                this.ProcessosPermissaoModuloMeioAmbiente = new List<Processo>();

            if (!this.ProcessosPermissaoModuloMeioAmbiente.Contains(p))
                this.ProcessosPermissaoModuloMeioAmbiente.Add(p);


            if (this.ProcessosPermissaoEdicaoModuloMeioAmbiente == null)
                this.ProcessosPermissaoEdicaoModuloMeioAmbiente = new List<Processo>();            

            if (!this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(p))
                this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Add(p);

        }

        lkbOpcoesProcessoNovo_ModalPopupExtender.Hide();

        this.CarregarOrgaos(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()), Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()));

        msg.CriarMensagem("Processo salvo com Sucesso!" + obsMsg, "Sucesso");
    }

    private void SalvarRenovacao()
    {
        if (tbxDiasValidadeRenovacao.Visible == true && tbxDiasValidadeRenovacao.Text.ToInt32() <= 0)
        {
            msg.CriarMensagem("O valor informado deve ser maior que zero.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (tbxDataValidadeRenovacao.Visible == true && tbxDataValidadeRenovacao.Text.IsNotNullOrEmpty() && tbxDataValidadeRenovacao.Text.ToSqlDateTime() <= SqlDate.MinValue)
        {
            msg.CriarMensagem("A Data informada deve conter uma data válida.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (hfTipoRenovacao.Value.ToUpper() == "CONDICIONANTE")
        {
            Condicionante c = Condicionante.ConsultarPorId(hfIdItemRenovacao.Value.ToInt32());
            this.VerificarAlteracaoDeStatusRenovacao(c, "Condicionante");
            Licenca l = Licenca.ConsultarPorId(c.Licenca.Id);

            Vencimento v = c.GetUltimoVencimento;
            if (v.Data <= SqlDate.MinValue)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            if (tbxDataValidadeRenovacao.Visible)
            {
                if (tbxDataValidadeRenovacao.Text.IsDate())
                    novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
                else
                {
                    msg.CriarMensagem("Insira uma data válida.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
                novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());
            novo.Periodico = v.Periodico;

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                if (novo.Notificacoes == null)
                    novo.Notificacoes = new List<Notificacao>();

                foreach (Notificacao n in v.Notificacoes)
                {
                    Notificacao nn = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                    nn.Template = n.Template;
                    nn.DiasAviso = n.DiasAviso;
                    nn.Emails = n.Emails;
                    nn = nn.Salvar();
                    novo.Notificacoes.Add(nn);
                }

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            c.Vencimentos.Add(novo);
            c = c.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();

        }
        else if (hfTipoRenovacao.Value.ToUpper() == "OUTROS")
        {
            if (hfTipoOutros.Value.ToUpper() == "EMP")
            {
                OutrosEmpresa o = OutrosEmpresa.ConsultarPorId(hfIdItemRenovacao.Value.ToInt32());
                this.VerificarAlteracaoDeStatusRenovacao(o, "OutrosEmpresa");
                Vencimento v = o.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                Vencimento novo = new Vencimento();
                if (tbxDataValidadeRenovacao.Visible)
                {
                    if (tbxDataValidadeRenovacao.Text.IsDate())
                        novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
                    else
                    {
                        msg.CriarMensagem("Insira uma data válida.", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                }
                else
                    novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());
                novo.Periodico = v.Periodico;

                if (ckbUtilizarUltimasNotificacoes.Checked)
                {
                    if (novo.Notificacoes == null)
                        novo.Notificacoes = new List<Notificacao>();

                    foreach (Notificacao n in v.Notificacoes)
                    {
                        Notificacao nn = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                        nn.DiasAviso = n.DiasAviso;
                        nn.Template = n.Template;
                        nn.Emails = n.Emails;
                        nn = nn.Salvar();
                        novo.Notificacoes.Add(nn);
                    }

                    //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                    foreach (Notificacao n in v.Notificacoes)
                    {
                        n.Enviado = 1;
                        n.DataUltimoEnvio = DateTime.Now;
                        n.Salvar();
                    }
                }

                novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
                novo = novo.Salvar();

                o.Vencimentos.Add(novo);
                o = o.Salvar();

                //setando o status de vencimento anterior para prorrogado
                Status statProrrogado = Status.ConsultarPorId(2);
                v.Status = statProrrogado;
                v = v.Salvar();
            }
            else if (hfTipoOutros.Value.ToUpper() == "PROC")
            {
                OutrosProcesso o = OutrosProcesso.ConsultarPorId(hfIdItemRenovacao.Value.ToInt32());
                this.VerificarAlteracaoDeStatusRenovacao(o, "OutrosProcesso");
                Vencimento v = o.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                Vencimento novo = new Vencimento();
                if (tbxDataValidadeRenovacao.Visible)
                {
                    if (tbxDataValidadeRenovacao.Text.IsDate())
                        novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
                    else
                    {
                        msg.CriarMensagem("Insira uma data válida.", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                }
                else
                {
                    novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());
                }

                novo.Periodico = v.Periodico;

                if (ckbUtilizarUltimasNotificacoes.Checked)
                {
                    if (novo.Notificacoes == null)
                        novo.Notificacoes = new List<Notificacao>();

                    foreach (Notificacao n in v.Notificacoes)
                    {
                        Notificacao nn = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                        nn.DiasAviso = n.DiasAviso;
                        nn.Template = n.Template;
                        nn.Emails = n.Emails;
                        nn = nn.Salvar();
                        novo.Notificacoes.Add(nn);
                    }

                    //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                    foreach (Notificacao n in v.Notificacoes)
                    {
                        n.Enviado = 1;
                        n.DataUltimoEnvio = DateTime.Now;
                        n.Salvar();
                    }
                }

                novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
                novo = novo.Salvar();

                o.Vencimentos.Add(novo);
                o = o.Salvar();

                //setando o status de vencimento anterior para prorrogado
                Status statProrrogado = Status.ConsultarPorId(2);
                v.Status = statProrrogado;
                v = v.Salvar();
            }
        }
        else if (hfTipoRenovacao.Value.ToUpper() == "RELATORIO")
        {
            CadastroTecnicoFederal o = CadastroTecnicoFederal.ConsultarPorId(hfIdItemRenovacao.Value.ToInt32());
            this.VerificarAlteracaoDeStatusRenovacao(o, "EntregaRelatorioAnual");
            Vencimento v = o.GetUltimoRelatorio;
            if (v.Data <= SqlDate.MinValue)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            if (tbxDataValidadeRenovacao.Visible)
            {
                if (tbxDataValidadeRenovacao.Text.IsDate())
                    novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
                else
                {
                    msg.CriarMensagem("Insira uma data válida.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
                novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());
            novo.Periodico = v.Periodico;

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                if (novo.Notificacoes == null)
                    novo.Notificacoes = new List<Notificacao>();

                foreach (Notificacao n in v.Notificacoes)
                {
                    Notificacao nn = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                    nn.DiasAviso = n.DiasAviso;
                    nn.Template = n.Template;
                    nn.Emails = n.Emails;
                    nn = nn.Salvar();
                    novo.Notificacoes.Add(nn);
                }

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            o.EntregaRelatorioAnual.Add(novo);
            o = o.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }
        else if (hfTipoRenovacao.Value.ToUpper() == "TAXA")
        {
            CadastroTecnicoFederal o = CadastroTecnicoFederal.ConsultarPorId(hfIdItemRenovacao.Value.ToInt32());
            this.VerificarAlteracaoDeStatusRenovacao(o, "TaxaTrimestral");
            Vencimento v = o.GetUltimoPagamento;
            if (v.Data <= SqlDate.MinValue)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            if (tbxDataValidadeRenovacao.Visible)
            {
                if (tbxDataValidadeRenovacao.Text.IsDate())
                    novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
                else
                {
                    msg.CriarMensagem("Insira uma data válida.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
                novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());
            novo.Periodico = v.Periodico;

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                if (novo.Notificacoes == null)
                    novo.Notificacoes = new List<Notificacao>();

                foreach (Notificacao n in v.Notificacoes)
                {
                    Notificacao nn = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                    nn.DiasAviso = n.DiasAviso;
                    nn.Template = n.Template;
                    nn.Emails = n.Emails;
                    nn = nn.Salvar();
                    novo.Notificacoes.Add(nn);
                }

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            o.TaxaTrimestral.Add(novo);
            o = o.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }
        else if (hfTipoRenovacao.Value.ToUpper() == "CERTIFICADO")
        {
            CadastroTecnicoFederal o = CadastroTecnicoFederal.ConsultarPorId(hfIdItemRenovacao.Value.ToInt32());
            this.VerificarAlteracaoDeStatusRenovacao(o, "CertificadoRegularidade");
            Vencimento v = o.GetUltimoCertificado;
            if (v.Data <= SqlDate.MinValue)
            {
                msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                return;
            }
            Vencimento novo = new Vencimento();
            if (tbxDataValidadeRenovacao.Visible)
            {
                if (tbxDataValidadeRenovacao.Text.IsDate())
                    novo.Data = tbxDataValidadeRenovacao.Text.ToSqlDateTime();
                else
                {
                    msg.CriarMensagem("Insira uma data válida.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
                novo.Data = v.Data.AddDays(tbxDiasValidadeRenovacao.Text.ToInt32());
            novo.Periodico = v.Periodico;

            if (ckbUtilizarUltimasNotificacoes.Checked)
            {
                if (novo.Notificacoes == null)
                    novo.Notificacoes = new List<Notificacao>();

                foreach (Notificacao n in v.Notificacoes)
                {
                    Notificacao nn = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                    nn.DiasAviso = n.DiasAviso;
                    nn.Template = n.Template;
                    nn.Emails = n.Emails;
                    nn = nn.Salvar();
                    novo.Notificacoes.Add(nn);
                }

                //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                foreach (Notificacao n in v.Notificacoes)
                {
                    n.Enviado = 1;
                    n.DataUltimoEnvio = DateTime.Now;
                    n.Salvar();
                }
            }

            novo.Status = Status.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
            novo = novo.Salvar();

            o.CertificadoRegularidade.Add(novo);
            o = o.Salvar();

            //setando o status de vencimento anterior para prorrogado
            Status statProrrogado = Status.ConsultarPorId(2);
            v.Status = statProrrogado;
            v = v.Salvar();
        }

        msg.CriarMensagem("Renovação efetuada com Sucesso" + (ckbUtilizarUltimasNotificacoes.Checked == false ? "<br><br> Edite o item para inserir novas notificações na renovação." : ""), "SUCESSO");

        btnPopUpRenovacao_ModalPopupExtender.Hide();
    }

    private void SalvarOutrosPopUp()
    {
        if (!tbxDataRecebimentoOutros.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("É necessario informar uma data de Abertura/Recebimento.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (tbxDiasValidadeOutros.Text.ToInt32() == 0)
        {
            msg.CriarMensagem("É necessario informar os dias de validade.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (rbtnGeralOutros.Checked)
        {

            if (ddlEmpresaGeralOutros.SelectedValue == "0" || ddlOrgaoGeralOutros.SelectedValue == "0")
            {
                msg.CriarMensagem("Selecione uma Empresa e um Orgão Ambiental.", "Alerta", MsgIcons.Alerta);
                return;
            }

            string mens = "";
            if (this.notificacoesSalvas == null || this.notificacoesSalvas.Count <= 0)
                mens += "Não foram cadastradas notificações para este item.";

            OutrosEmpresa outrosEmpresa = OutrosEmpresa.ConsultarPorId(hfIdOutros.Value.ToInt32());
            this.VerificarAlteracaoDeStatus(outrosEmpresa);
            if (outrosEmpresa == null)
                outrosEmpresa = new OutrosEmpresa();
            outrosEmpresa.Empresa = Empresa.ConsultarPorId(ddlEmpresaGeralOutros.SelectedValue.ToInt32());
            outrosEmpresa.OrgaoAmbiental = OrgaoAmbiental.ConsultarPorId(ddlOrgaoGeralOutros.SelectedValue.ToInt32());
            outrosEmpresa.Descricao = tbxDescricaoOutros.Text;
            outrosEmpresa.Observacoes = tbxObservacoesOutros.Text;
            outrosEmpresa.Consultora = Consultora.ConsultarPorId(ddlConsultoriaOutros.SelectedValue.ToInt32());
            outrosEmpresa.DiasPrazo = tbxDiasValidadeOutros.Text.ToInt32();
            outrosEmpresa.Arquivos = this.ReconsultarArquivos(arquivosUpload);
            outrosEmpresa.DataRecebimento = tbxDataRecebimentoOutros.Text.ToDateTime();

            Vencimento vencimento = outrosEmpresa.GetUltimoVencimento;
            vencimento.ProtocoloAtendimento = tbxProtocoloOutros.Text;
            vencimento.Status = Status.ConsultarPorId(ddlStatusOutros.SelectedValue.ToInt32());

            if (vencimento.ProrrogacoesPrazo == null || (vencimento.ProrrogacoesPrazo != null && vencimento.ProrrogacoesPrazo.Count == 0) && (outrosEmpresa.Vencimentos == null || outrosEmpresa.Vencimentos.Count == 1))
                vencimento.Data = this.CalcularDataVencimentoOutros();

            vencimento.Periodico = cbxPeriodicaOutros.Checked;

            foreach (Notificacao notificacao in this.ReconsultarNotificacoes(this.notificacoesSalvas))
            {
                if (vencimento.Notificacoes == null)
                    vencimento.Notificacoes = new List<Notificacao>();
                if (!vencimento.Notificacoes.Contains(notificacao))
                    vencimento.Notificacoes.Add(notificacao);
            }
            vencimento = vencimento.Salvar();
            outrosEmpresa.SetUltimoVencimento = vencimento;
            outrosEmpresa = outrosEmpresa.Salvar();
            hfIdOutros.Value = outrosEmpresa.Id.ToString();
            hfTipoOutros.Value = "emp";
            msg.CriarMensagem("Item salvo com sucesso.</br>" + mens, "Sucesso", MsgIcons.Sucesso);

            transacao.Recarregar(ref msg);

            OrgaoAmbiental oa = OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32() > 0 ? hfIdOrgao.Value.ToInt32() : ddlOrgaoGeralOutros.SelectedValue.ToInt32());
            grvOutros.DataSource = OutrosEmpresa.ConsultarPorOrgaoEmpresaGrupoEconomico(oa, Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()),
                GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()));
            grvOutros.DataBind();
            if (!mens.IsNotNullOrEmpty())
                divPopUpOutros_ModalPopupExtender.Hide();

            this.ItensRenovacao = new List<ItemRenovacao>();

            if (outrosEmpresa != null && outrosEmpresa.GetPeriodico && outrosEmpresa.GetUltimoVencimento != null && outrosEmpresa.GetUltimoVencimento.Data <= DateTime.Now)
            {
                ItemRenovacao item = new ItemRenovacao();
                item.idItem = outrosEmpresa.Id;
                item.tipoItem = "OUTROSEMPRESA";
                item.diasRenovacao = ObterDiasEntreAsRenovacoes(outrosEmpresa.Vencimentos);
                this.ItensRenovacao.Add(item);
            }

            if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
            {
                rptRenovacoes.DataSource = this.ItensRenovacao;
                rptRenovacoes.DataBind();
                lblRenovacaoVencimentosPeriodicos_popupextender.Show();
            }

            if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                if (outrosEmpresa.UsuariosEdicao == null)
                    outrosEmpresa.UsuariosEdicao = new List<Usuario>();                               

                if (outrosEmpresa.UsuariosVisualizacao != null && outrosEmpresa.UsuariosVisualizacao.Count > 0)
                {
                    if (!outrosEmpresa.UsuariosVisualizacao.Contains(this.UsuarioLogado))
                        outrosEmpresa.UsuariosVisualizacao.Add(this.UsuarioLogado);
                }

                if (!outrosEmpresa.UsuariosEdicao.Contains(this.UsuarioLogado))
                    outrosEmpresa.UsuariosEdicao.Add(this.UsuarioLogado);

                outrosEmpresa = outrosEmpresa.Salvar();

                if (this.OutrosEmpresasPermissaoModuloMeioAmbiente == null)
                    this.OutrosEmpresasPermissaoModuloMeioAmbiente = new List<OutrosEmpresa>();

                if (!this.OutrosEmpresasPermissaoModuloMeioAmbiente.Contains(outrosEmpresa))
                    this.OutrosEmpresasPermissaoModuloMeioAmbiente.Add(outrosEmpresa);


                if (this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente == null)
                    this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente = new List<OutrosEmpresa>();

                if (!this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(outrosEmpresa))
                    this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Add(outrosEmpresa);

            }
        }
        else if (rbtnProcessoOutros.Checked)
        {
            if (ddlProcessoOutros.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione um processo! \r\nEscolha através da Empresa e Orgão Ambiental.", "Alerta", MsgIcons.Alerta);
                return;
            }

            string mens = "";
            if (this.notificacoesSalvas == null || this.notificacoesSalvas.Count <= 0)
                mens += "Não foram cadastradas notificações para este item.";

            OutrosProcesso outrosProcesso = OutrosProcesso.ConsultarPorId(hfIdOutros.Value.ToInt32());
            this.VerificarAlteracaoDeStatus(outrosProcesso);
            if (outrosProcesso == null)
                outrosProcesso = new OutrosProcesso();
            outrosProcesso.Processo = Processo.ConsultarPorId(ddlProcessoOutros.SelectedValue.ToInt32());
            outrosProcesso.Descricao = tbxDescricaoOutros.Text;
            outrosProcesso.Observacoes = tbxObservacoesOutros.Text;
            outrosProcesso.DataRecebimento = tbxDataRecebimentoOutros.Text.ToDateTime();
            outrosProcesso.DiasPrazo = tbxDiasValidadeOutros.Text.ToInt32();
            outrosProcesso.Arquivos = this.ReconsultarArquivos(arquivosUpload);

            Vencimento vencimento = outrosProcesso.GetUltimoVencimento;
            vencimento.ProtocoloAtendimento = tbxProtocoloOutros.Text;
            vencimento.Status = Status.ConsultarPorId(ddlStatusOutros.SelectedValue.ToInt32());

            if (vencimento.ProrrogacoesPrazo == null || (vencimento.ProrrogacoesPrazo != null && vencimento.ProrrogacoesPrazo.Count == 0) && (outrosProcesso.Vencimentos == null || outrosProcesso.Vencimentos.Count == 1))
                vencimento.Data = this.CalcularDataVencimentoOutros();

            vencimento.Periodico = cbxPeriodicaOutros.Checked;

            foreach (Notificacao notificacao in this.ReconsultarNotificacoes(this.notificacoesSalvas))
            {
                if (vencimento.Notificacoes == null)
                    vencimento.Notificacoes = new List<Notificacao>();
                if (!vencimento.Notificacoes.Contains(notificacao))
                    vencimento.Notificacoes.Add(notificacao);
            }

            vencimento = vencimento.Salvar();
            outrosProcesso.SetUltimoVencimento = vencimento;

            outrosProcesso = outrosProcesso.Salvar();
            hfIdOutros.Value = outrosProcesso.Id.ToString();
            hfTipoOutros.Value = "proc";
            msg.CriarMensagem("Item salvo com sucesso.</br>" + mens, "Sucesso", MsgIcons.Sucesso);

            transacao.Recarregar(ref msg);

            grvOutros.DataSource = OutrosProcesso.ConsultarPorProcesso(Processo.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32()));
            grvOutros.DataBind();
            if (!mens.IsNotNullOrEmpty())
                divPopUpOutros_ModalPopupExtender.Hide();

            this.ItensRenovacao = new List<ItemRenovacao>();

            if (outrosProcesso != null && outrosProcesso.GetPeriodico && outrosProcesso.GetUltimoVencimento != null && outrosProcesso.GetUltimoVencimento.Data <= DateTime.Now)
            {
                ItemRenovacao item = new ItemRenovacao();
                item.idItem = outrosProcesso.Id;
                item.tipoItem = "OUTROSPROCESSO";
                item.diasRenovacao = ObterDiasEntreAsRenovacoes(outrosProcesso.Vencimentos);
                this.ItensRenovacao.Add(item);
            }

            if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
            {
                rptRenovacoes.DataSource = this.ItensRenovacao;
                rptRenovacoes.DataBind();
                lblRenovacaoVencimentosPeriodicos_popupextender.Show();
            }
        }

        mvwProcessos.ActiveViewIndex = 2;
        WebUtil.LimparCampos(upPopProcesso.Controls[0].Controls);
        HfIdProcesso.Value = "";
    }

    private DateTime CalcularDataVencimentoOutros()
    {
        return tbxDataRecebimentoOutros.Text.ToSqlDateTime().AddDays(tbxDiasValidadeOutros.Text.ToInt32());
    }

    private void SetarProcessoOutros()
    {
        Processo p = Processo.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32());

        ddlEmpresaOutros.SelectedValue = p.Empresa.Id.ToString();

        this.CarregarOrgaosAmbientaisOutros();
        ddlOrgaoOutros.SelectedValue = p.OrgaoAmbiental.Id.ToString();

        this.CarregarProcessosOutros();
        ddlProcessoOutros.SelectedValue = p.Id.ToString();
    }

    private void SalvarNotificacao()
    {
        string emails = this.GetEmailsSelecionados();
        if (!String.IsNullOrEmpty(emails))
        {
            foreach (ListItem l in cblDias.Items)
                if (l.Selected && l.Enabled)
                {
                    Notificacao n = new Notificacao(ModuloPermissao.ModuloMeioAmbiente);
                    n.Emails = emails;
                    n.DiasAviso = l.Value.ToInt32();
                    if (objetoUtilizado.GetType() == typeof(Condicionante))
                    {
                        n.Template = TemplateNotificacao.TemplateCondicionante;
                        Condicionante condcionante = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
                        n = n.Salvar();
                        if (!this.notificacoesSalvas.Contains(n))
                            this.notificacoesSalvas.Add(n);
                    }
                    else if (objetoUtilizado.GetType() == typeof(Licenca))
                    {
                        n.Template = TemplateNotificacao.TemplateLicenca;
                        n = n.Salvar();
                        if (!this.notificacoesSalvas.Contains(n))
                            this.notificacoesSalvas.Add(n);
                    }
                    else if (objetoUtilizado.GetType() == typeof(OutrosEmpresa))
                    {
                        n.Template = TemplateNotificacao.TemplateOutrosEmpresa;
                        n = n.Salvar();
                        if (!this.notificacoesSalvas.Contains(n))
                            this.notificacoesSalvas.Add(n);
                    }
                    else if (objetoUtilizado.GetType() == typeof(OutrosProcesso))
                    {
                        n.Template = TemplateNotificacao.TemplateOutrosProcesso;
                        n = n.Salvar();
                        if (!this.notificacoesSalvas.Contains(n))
                            this.notificacoesSalvas.Add(n);
                    }
                    else if (objetoUtilizado.GetType() == typeof(CadastroTecnicoFederal))
                    {
                        if (hfTipoVencimentoCTF.Value.ToUpper().Equals("RELATORIO"))
                        {
                            n.Template = TemplateNotificacao.TemplateRelatorioCTF;
                            n = n.Salvar();

                            if (this.objetoCTF.EntregaRelatorioAnual == null)
                                this.objetoCTF.EntregaRelatorioAnual = new List<Vencimento>();
                            if (this.objetoCTF.EntregaRelatorioAnual.Count == 0)
                                this.objetoCTF.EntregaRelatorioAnual.Add(new Vencimento());
                            if (this.objetoCTF.GetUltimoRelatorio.Notificacoes == null)
                                this.objetoCTF.GetUltimoRelatorio.Notificacoes = new List<Notificacao>();
                            if (!this.objetoCTF.GetUltimoRelatorio.Notificacoes.Contains(n))
                                this.objetoCTF.GetUltimoRelatorio.Notificacoes.Add(n);
                        }

                        if (hfTipoVencimentoCTF.Value.ToUpper().Equals("PAGAMENTO"))
                        {
                            n.Template = TemplateNotificacao.TemplatePagamentoCTF;
                            n = n.Salvar();
                            if (this.objetoCTF.TaxaTrimestral == null)
                                this.objetoCTF.TaxaTrimestral = new List<Vencimento>();
                            if (this.objetoCTF.TaxaTrimestral.Count == 0)
                                this.objetoCTF.TaxaTrimestral.Add(new Vencimento());
                            if (this.objetoCTF.GetUltimoPagamento.Notificacoes == null)
                                this.objetoCTF.GetUltimoPagamento.Notificacoes = new List<Notificacao>();
                            if (!this.objetoCTF.GetUltimoPagamento.Notificacoes.Contains(n))
                                this.objetoCTF.GetUltimoPagamento.Notificacoes.Add(n);
                        }
                        if (hfTipoVencimentoCTF.Value.ToUpper().Equals("CERTIFICADO"))
                        {
                            n.Template = TemplateNotificacao.TemplateCertificadoCTF;
                            n = n.Salvar();
                            if (this.objetoCTF.CertificadoRegularidade == null)
                                this.objetoCTF.CertificadoRegularidade = new List<Vencimento>();
                            if (this.objetoCTF.CertificadoRegularidade.Count == 0)
                                this.objetoCTF.CertificadoRegularidade.Add(new Vencimento());
                            if (this.objetoCTF.GetUltimoCertificado.Notificacoes == null)
                                this.objetoCTF.GetUltimoCertificado.Notificacoes = new List<Notificacao>();
                            if (!this.objetoCTF.GetUltimoCertificado.Notificacoes.Contains(n))
                                this.objetoCTF.GetUltimoCertificado.Notificacoes.Add(n);
                        }
                    }
                }
            if (objetoUtilizado.GetType() == typeof(Condicionante))
            {
                grvNotificacaoCondicionante.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
                grvNotificacaoCondicionante.DataBind();
            }
            else if (objetoUtilizado.GetType() == typeof(Licenca))
            {
                grvNotificacaoLicenca.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
                grvNotificacaoLicenca.DataBind();
            }
            else if (objetoUtilizado.GetType() == typeof(OutrosEmpresa))
            {
                grvNotificacaoOutros.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
                grvNotificacaoOutros.DataBind();
            }
            else if (objetoUtilizado.GetType() == typeof(OutrosProcesso))
            {
                grvNotificacaoOutros.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
                grvNotificacaoOutros.DataBind();
            }
            else if (objetoUtilizado.GetType() == typeof(CadastroTecnicoFederal))
            {
                if (hfTipoVencimentoCTF.Value.ToUpper().Equals("RELATORIO"))
                {
                    grvNotificacaoRelatorioCTF.DataSource = this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoRelatorio.Notificacoes);
                    grvNotificacaoRelatorioCTF.DataBind();
                }
                if (hfTipoVencimentoCTF.Value.ToUpper().Equals("PAGAMENTO"))
                {
                    grvNotificacaoPagamentoCTF.DataSource = this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoPagamento.Notificacoes);
                    grvNotificacaoPagamentoCTF.DataBind();
                }
                if (hfTipoVencimentoCTF.Value.ToUpper().Equals("CERTIFICADO"))
                {
                    grvNotificacaoCertificadoCTF.DataSource = this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoCertificado.Notificacoes);
                    grvNotificacaoCertificadoCTF.DataBind();
                }
            }
            msg.CriarMensagem("Notificações Criadas com Sucesso! Salve agora o vencimento para tornar as alterações efetivas!", "Sucesso");
            lblPopUpNotificacoes_ModalPopupExtender.Hide();
        }
        else
        {
            msg.CriarMensagem("Selecione pelo menos um email da lista.", "Alerta", MsgIcons.Alerta);
            return;
        }

    }

    private string GetEmailsSelecionados()
    {
        string emails = "";
        foreach (ListItem l in chkGruposEconomicos.Items)
            if (l.Selected && l.Value != "0")
                emails += l.Value.ToString() + ";";
        foreach (ListItem l in chkCOnsultoras.Items)
            if (l.Selected && l.Value != "0")
                emails += l.Value.ToString() + ";";
        foreach (ListItem l in chkEmpresas.Items)
            if (l.Selected && l.Value != "0")
                emails += l.Value.ToString() + ";";
        emails += tbxOutrosEmails.Text.ToString();
        return emails;
    }

    private void CarregarGridNotificacoesLicenca(Vencimento vencimento)
    {
        grvNotificacaoLicenca.DataSource = vencimento.Notificacoes != null ? vencimento.Notificacoes : new List<Notificacao>();
        grvNotificacaoLicenca.DataBind();
    }

    private void CarregarPopUpNotificacao(bool marcar, params int[] dias)
    {
        cblDias.Items.Clear();
        chkGruposEconomicos.Items.Clear();
        chkCOnsultoras.Items.Clear();
        chkEmpresas.Items.Clear();

        cblDias.Items.Add(new ListItem("No dia do vencimento", "0"));
        foreach (int i in dias)
            cblDias.Items.Add(new ListItem(i.ToString(), i.ToString()));


        foreach (ListItem i in cblDias.Items)
            i.Selected = marcar;

        if (cblDias.Items.Count <= 0)
        {
            msg.CriarMensagem("Você não pode inserir notificações em Vencimentos que já ocorreram.", "Alerta", MsgIcons.Alerta);
            return;
        }
        else
        {
            lblPopUpNotificacoes_ModalPopupExtender.Show();
        }

        this.CarregarListaEmails(chkEmpresas, this.CarregarEmailsEmpresa().Split(';'));
        this.CarregarListaEmails(chkGruposEconomicos, this.CarregarEmailsGrupoEconomico().Split(';'));
        this.CarregarListaEmails(chkCOnsultoras, this.CarregarEmailsConsultora().Split(';'));
    }

    private void CarregarListaEmails(CheckBoxList cbl, string[] lista)
    {
        cbl.Items.Clear();
        foreach (string s in lista)
            if (s.IsNotNullOrEmpty())
                cbl.Items.Add(new ListItem(s, s));
        if (cbl.Items.Count <= 0)
        {
            cbl.Items.Add(new ListItem("Não há emails cadastrados.", "0"));
            cbl.Items[0].Enabled = cbl.Items[0].Selected = false;
        }

        foreach (ListItem item in cbl.Items)
        {
            item.Selected = true;
        }
    }

    private string CarregarEmailsConsultora()
    {
        string email = "";
        if (objetoUtilizado != null)
            if (objetoUtilizado.GetType() == typeof(Condicionante))
            {
                Licenca licencaSelecionaNaArvore = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                email = licencaSelecionaNaArvore != null ? licencaSelecionaNaArvore.Processo != null ? licencaSelecionaNaArvore.Processo.Consultora != null ? licencaSelecionaNaArvore.Processo.Consultora.Contato != null ? licencaSelecionaNaArvore.Processo.Consultora.Contato.Email : "" : "" : "" : "";
            }
            else if (objetoUtilizado.GetType() == typeof(Licenca))
            {
                if (HfIdLicenca.Value.ToInt32() > 0)
                {
                    Licenca licencaSelecionaNaArvore = Licenca.ConsultarPorId(HfIdLicenca.Value.ToInt32());
                    email = licencaSelecionaNaArvore != null ? licencaSelecionaNaArvore.Processo != null ? licencaSelecionaNaArvore.Processo.Consultora != null ? licencaSelecionaNaArvore.Processo.Consultora.Contato != null ? licencaSelecionaNaArvore.Processo.Consultora.Contato.Email : "" : "" : "" : "";
                }
                else
                {
                    Processo p1 = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                    email = p1 != null ? p1.Consultora != null ? p1.Consultora.Contato != null ? p1.Consultora.Contato.Email : "" : "" : "";
                }
            }
            else if (objetoUtilizado.GetType() == typeof(OutrosEmpresa))
            {
                Consultora consultora = Consultora.ConsultarPorId(ddlConsultoriaOutros.SelectedValue.ToInt32());
                if (consultora != null && consultora.Contato != null)
                    email = consultora.Contato.Email.IsNotNullOrEmpty() ? consultora.Contato.Email + ";" : "";
                else
                    email = "";
            }
            else if (objetoUtilizado.GetType() == typeof(OutrosProcesso))
            {
                Processo p1 = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                email = p1 != null ? p1.Consultora != null ? p1.Consultora.Contato != null ? p1.Consultora.Contato.Email : "" : "" : "";
            }
            else if (objetoUtilizado.GetType() == typeof(CadastroTecnicoFederal))
            {
                Consultora consultora = Consultora.ConsultarPorId(ddlConsultoria.SelectedValue.ToInt32());
                if (consultora != null && consultora.Contato != null)
                    email = consultora.Contato.Email.IsNotNullOrEmpty() ? consultora.Contato.Email + ";" : "";
                else
                    email = "";
            }

        return email;
    }

    private string CarregarEmailsGrupoEconomico()
    {
        string email = "";
        if (ddlGrupoEconomicos.SelectedIndex > 0)
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
            if (c.Contato != null)
                email = c.Contato.Email.IsNotNullOrEmpty() ? c.Contato.Email + ";" : "";
        }
        return email;
    }

    private string CarregarEmailsEmpresa()
    {
        string email = "";
        if (ddlEmpresa.SelectedIndex > 0)
        {
            Empresa em = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());
            if (em.Contato != null)
                email = em.Contato.Email.IsNotNullOrEmpty() ? em.Contato.Email + ";" : "";
        }
        else if (ddlEmpresaCTF.SelectedIndex > 0)
        {
            Empresa em = Empresa.ConsultarPorId(ddlEmpresaCTF.SelectedValue.ToInt32());
            if (em.Contato != null)
                email = em.Contato.Email.IsNotNullOrEmpty() ? em.Contato.Email + ";" : "";
        }
        else if (ddlEmpresaGeralOutros.SelectedIndex > 0)
        {
            Empresa em = Empresa.ConsultarPorId(ddlEmpresaGeralOutros.SelectedValue.ToInt32());
            if (em.Contato != null)
                email = em.Contato.Email.IsNotNullOrEmpty() ? em.Contato.Email + ";" : "";
        }
        else if (ddlEmpresaOutros.SelectedIndex > 0)
        {
            Empresa em = Empresa.ConsultarPorId(ddlEmpresaOutros.SelectedValue.ToInt32());
            if (em.Contato != null)
                email = em.Contato.Email.IsNotNullOrEmpty() ? em.Contato.Email + ";" : "";
        }
        else if (ddlEmpresaProcesso.SelectedIndex > 0)
        {
            Empresa em = Empresa.ConsultarPorId(ddlEmpresaProcesso.SelectedValue.ToInt32());
            if (em.Contato != null)
                email = em.Contato.Email.IsNotNullOrEmpty() ? em.Contato.Email + ";" : "";
        }
        else
        {
            if (objetoUtilizado != null)
                if (objetoUtilizado.GetType() == typeof(Condicionante))
                {
                    Licenca licencaSelecionaNaArvore = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                    email = licencaSelecionaNaArvore != null ? licencaSelecionaNaArvore.Processo != null ? licencaSelecionaNaArvore.Processo.Empresa != null ? licencaSelecionaNaArvore.Processo.Empresa.Contato != null ? licencaSelecionaNaArvore.Processo.Empresa.Contato.Email : "" : "" : "" : "";
                }
                else if (objetoUtilizado.GetType() == typeof(Licenca))
                {
                    if (HfIdLicenca.Value.ToInt32() > 0)
                    {
                        Licenca licenca = Licenca.ConsultarPorId(HfIdLicenca.Value.ToInt32());
                        email = licenca != null ? licenca.Processo != null ? licenca.Processo.Empresa != null ? licenca.Processo.Empresa.Contato != null ? licenca.Processo.Empresa.Contato.Email : "" : "" : "" : "";
                    }
                    else
                    {
                        Processo p1 = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                        email = p1 != null ? p1.Empresa != null ? p1.Empresa.Contato != null ? p1.Empresa.Contato.Email : "" : "" : "";
                    }
                }
                else if (objetoUtilizado.GetType() == typeof(OutrosEmpresa))
                {
                    Consultora consultora = Consultora.ConsultarPorId(ddlConsultoriaOutros.SelectedValue.ToInt32());
                    if (consultora != null && consultora.Contato != null)
                        email = consultora.Contato.Email.IsNotNullOrEmpty() ? consultora.Contato.Email + ";" : "";
                    else
                        email = "";
                }
                else if (objetoUtilizado.GetType() == typeof(OutrosProcesso))
                {
                    Processo p1 = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                    email = p1 != null ? p1.Consultora != null ? p1.Consultora.Contato != null ? p1.Consultora.Contato.Email : "" : "" : "";
                }
        }
        return email;
    }

    public string CarregarEmailsEmpresaHistorico()
    {
        switch (hfTypeObs.Value)
        {
            case "Condicionante":
                {
                    Condicionante cad = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
                    return cad.Licenca != null && cad.Licenca.Processo != null && cad.Licenca.Processo.Empresa != null && cad.Licenca.Processo.Empresa.Contato != null ?
                   cad.Licenca.Processo.Empresa.Contato.Email : "";
                }

            case "Condicional":
                {
                    Condicional cad = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());
                    return cad.GetEmpresa != null && cad.GetEmpresa.Contato != null ? cad.GetEmpresa.Contato.Email : "";
                }

            case "CadastroTecnicoFederal":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
                    return cad.Empresa != null && cad.Empresa.Contato != null ? cad.Empresa.Contato.Email : "";
                }

        }
        return "";
    }

    public string CarregarEmailsConsultoraHistorico()
    {
        switch (hfTypeObs.Value)
        {
            case "Condicionante":
                {
                    Condicionante cad = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
                    return cad.Licenca != null && cad.Licenca.Processo != null && cad.Licenca.Processo.Consultora != null && cad.Licenca.Processo.Consultora.Contato != null ?
                   cad.Licenca.Processo.Consultora.Contato.Email : "";
                }

            case "Condicional":
                {
                    Condicional cad = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());
                    return cad.GetConsultora != null && cad.GetConsultora.Contato != null ? cad.GetConsultora.Contato.Email : "";
                }

            case "CadastroTecnicoFederal":
                {
                    CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
                    return cad.Consultora != null && cad.Consultora.Contato != null ? cad.Consultora.Contato.Email : "";
                }

        }
        return "";
    }

    private IList<Notificacao> ReconsultarNotificacoes(IList<Notificacao> iList)
    {
        IList<Notificacao> lista = new List<Notificacao>();
        foreach (Notificacao n in iList)
            lista.Add(Notificacao.ConsultarPorId(n.Id));
        return lista;
    }

    private void AdicionarEventoPopUpCTF()
    {
        bntUploadCTF.Visible = ddlEmpresaCTF.SelectedIndex > 0;        
    }

    private void AdicionarEventoPopUpOutros()
    {
        
    }

    private void ExibirPopUpLicenca()
    {        
        btnPopUplicenca_ModalPopupExtender.Show();
    }

    private void ExibirPopUpCondicionante()
    {        
        lkbOpcoesCondicionante_ModalPopupExtender.Show();
    }

    private int IdSelecionadoArvore(string tipo)
    {
        if (tipo.Equals("l"))
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("L"))
                return trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32();
        }
        if (tipo.Equals("p"))
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("P"))
                return trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32();
            else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("L"))
                return trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32();
        }
        if (tipo.Equals("op"))
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("P"))
                return trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32();
            else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("S"))
                return trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32();
        }
        if (tipo.Equals("oe"))
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("O"))
                return trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32();
        }
        if (tipo.Equals("ctf"))
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper().Equals("C"))
                return trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32();
        }
        return 0;
    }

    private void ExibirPopUpOutros()
    {
        if (ddlEmpresaOutros.SelectedIndex > 0)
        {
            btnUploadOutros.Visible = true;
        }
        this.AdicionarEventoPopUpOutros();
        divPopUpOutros_ModalPopupExtender.Show();
    }

    private void ExibirPopUpCTF()
    {
        this.AdicionarEventoPopUpCTF();
        lblCTF_ModalPopupExtender.Show();
    }

    private void ExibirVisao()
    {
        divOpcoesCadastro.Visible = false;
        opcoesCadastroProcessos.Visible = false;
        opcoesCadastroOutros.Visible = false;
        opcoesCadastroCadTecnico.Visible = false;
        opcoesCadastroLicenca.Visible = false;

        //Verificando permissão de editar configuração do tipo geral (comum a qq vencimento)
        if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if (this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
            {
                divOpcoesCadastro.Visible = true;
                opcoesCadastroProcessos.Visible = true;
                opcoesCadastroOutros.Visible = true;
                opcoesCadastroCadTecnico.Visible = true;
                btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = true;
                opcoesCadastroLicenca.Visible = true;
                lkbCondicionantesPadroes.Visible = lkbCondicionantesPadroes.Enabled = true;
                ibtnAdicionarCondicionanteLicenca.Visible = ibtnAdicionarCondicionanteLicenca.Enabled = true;
                ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = true;                   
            }
            else 
            {
                divOpcoesCadastro.Visible = false;
                opcoesCadastroProcessos.Visible = false;
                opcoesCadastroOutros.Visible = false;
                opcoesCadastroCadTecnico.Visible = false;
                btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = false;
                opcoesCadastroLicenca.Visible = false;
                lkbCondicionantesPadroes.Visible = lkbCondicionantesPadroes.Enabled = false;
                ibtnAdicionarCondicionanteLicenca.Visible = ibtnAdicionarCondicionanteLicenca.Enabled = false;
                ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = false;                   
            }                
        }

        if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "L")
        {            
            mvwProcessos.ActiveViewIndex = 1;
            transacao.Abrir();
            Licenca l = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());

            //Verificando permissão de editar configuração do tipo por empresa da licença
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                Empresa empresaDaLicenca = l.Processo != null && l.Processo.Empresa != null ? l.Processo.Empresa : null;

                if (empresaDaLicenca != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(empresaDaLicenca))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroOutros.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = true;
                    opcoesCadastroLicenca.Visible = true;
                    lkbCondicionantesPadroes.Visible = lkbCondicionantesPadroes.Enabled = true;
                    ibtnAdicionarCondicionanteLicenca.Visible = ibtnAdicionarCondicionanteLicenca.Enabled = true;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = true;                    
                }
                else 
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroOutros.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = false;
                    opcoesCadastroLicenca.Visible = false;
                    lkbCondicionantesPadroes.Visible = lkbCondicionantesPadroes.Enabled = false;
                    ibtnAdicionarCondicionanteLicenca.Visible = ibtnAdicionarCondicionanteLicenca.Enabled = false;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = false;                    
                }                   
            }

            //Verificando permissão de editar configuração do tipo por processo da licença
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                Processo processoDaLicenca = l.Processo != null ? l.Processo : null;

                if (processoDaLicenca != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(processoDaLicenca))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;                    
                    opcoesCadastroLicenca.Visible = true;
                    lkbCondicionantesPadroes.Visible = lkbCondicionantesPadroes.Enabled = true;
                    ibtnAdicionarCondicionanteLicenca.Visible = ibtnAdicionarCondicionanteLicenca.Enabled = true;                    
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;                    
                    opcoesCadastroLicenca.Visible = false;
                    lkbCondicionantesPadroes.Visible = lkbCondicionantesPadroes.Enabled = false;
                    ibtnAdicionarCondicionanteLicenca.Visible = ibtnAdicionarCondicionanteLicenca.Enabled = false;                    
                }                
               
            }

            grvCondicionantes.DataSource = l.Condicionantes.OrderBy(i => i.Numero).ToList();
            grvCondicionantes.DataBind();
            lblEmpresaLicenca.Text = l.Processo != null ? l.Processo.Empresa != null ? l.Processo.Empresa.Nome + " - " + l.Processo.Empresa.GetNumeroCNPJeCPFComMascara : "" : "";
            if (l.Vencimentos != null && l.Vencimentos.Count > 0)
            {
                Vencimento v = l.GetUltimoVencimento;
                if (v.Notificacoes != null && v.Notificacoes.Count > 0)
                {
                    lblDataAviso.Text = v.GetDataProximaNotificacao;
                }
                else
                {
                    lblDataAviso.Text = "Não informado";
                }
                lblDataLimiteRenovacao.Text = l.PrazoLimiteRenovacao.NaoInformadaToMinValue();
                lblDiasValidade.Text = l.DiasValidade.ToString();
                lblDataValidade.Text = v.Data.NaoInformadaToMinValue();
            }

            lblDataRetirada.Text = l.DataRetirada.NaoInformadaToMinValue();
            lblDescricao.Text = l.Descricao;
            lblNumeroLicenca.Text = l.Numero;
            lblLicenca.Text = "Licença: " + l.Numero.ToString();
            if (l.Cidade != null)
            {
                lblEstadoLicenca.Text = l.Cidade.Estado.Nome;
                lblCidadeLicenca.Text = l.Cidade.Nome;
            }
            else
                lblCidadeLicenca.Text = lblEstadoLicenca.Text = "";

            this.arquivosUpload = this.ReconsultarArquivos(l.Arquivos);            
        }

        else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "P")
        {
            opcoesCadastroLicenca.Visible = true;
            mvwProcessos.ActiveViewIndex = 0;
            Processo processo = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());

            //Verificando permissão de editar configuração do tipo por empresa do processo            
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                Empresa empresaDoProcesso = processo.Empresa != null ? processo.Empresa : null;

                if (empresaDoProcesso != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(empresaDoProcesso))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroOutros.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = true;
                    opcoesCadastroLicenca.Visible = true;
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroOutros.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = false;
                    opcoesCadastroLicenca.Visible = false;
                }
            }

            //Verificando permissão de editar configuração do tipo por processo do processo
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                if (processo != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(processo))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroLicenca.Visible = true;
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroLicenca.Visible = false;
                }                
            }

            //EXIBIR DADOS NA COLUNA DA DIREITA
            lblDataAbertura.Text = processo.DataAbertura.NaoInformadaToMinValue();
            lblNumero.Text = processo.Numero.ToString();
            if (processo.Observacoes != null)
                lblObs.Text = processo.Observacoes.ConvertHtmlText();
            lblEmpresaProcesso.Text = processo.Empresa != null ? processo.Empresa.Nome + " - " + processo.Empresa.GetNumeroCNPJeCPFComMascara : "";
            lblConsultoria.Text = processo.Consultora != null ? processo.Consultora.Nome : "";
            lblProcesso.Text = "Processo: " + processo.Numero.ToString();
        }
        else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "O")
        {
            hfTipoOutros.Value = "emp";
            lblTituloOutros.Text = "Outros";
            mvwProcessos.ActiveViewIndex = 2;

            //Verificando permissão de editar configuração do tipo por empresa do outro de empresa
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) 
            {
                if (this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0)
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroOutros.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = true;
                    opcoesCadastroLicenca.Visible = true;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = true;                    
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroOutros.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = false;
                    opcoesCadastroLicenca.Visible = false;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = false;                    
                }
            }

            //Verificando permissão de editar configuração do tipo por processo do outro de empresa
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                //Se o usuario possuir permissão para editar algum tipo de processo liberar o botão correspondente ao tipo
                divOpcoesCadastro.Visible = this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0;                                
                opcoesCadastroOutros.Visible = this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0;
                ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = opcoesCadastroOutros.Visible;                    
                
            }

            grvOutros.DataSource = OutrosEmpresa.ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32()),
                Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()), GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);
            grvOutros.DataBind();
            ibtnAdicionarOutros.Attributes.Add("onmouseover", "tooltip.show('Adiciona um outro vencimento fora do processo')");
        }
        else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "H")
        {
            hfTipoOutros.Value = "emp";
            lblTituloOutros.Text = "Outros";
            mvwProcessos.ActiveViewIndex = 2;

            //Verificando permissão de editar configuração do tipo por empresa do outro de empresa
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                if (this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0)
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroOutros.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = true;
                    opcoesCadastroLicenca.Visible = true;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = true;                    
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroOutros.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = false;
                    opcoesCadastroLicenca.Visible = false;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = false;                    
                }
            }

            //Verificando permissão de editar configuração do tipo por processo do outro de empresa
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                //Se o usuario possuir permissão para editar algum tipo de processo liberar o botão correspondente ao tipo
                divOpcoesCadastro.Visible = this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0;                
                opcoesCadastroOutros.Visible = this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0;
                ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = opcoesCadastroOutros.Visible;                    
                
            }

            hfIdOrgao.Value = trvProcessos.SelectedNode.Value.Split('_')[1];
            grvOutros.DataSource = OutrosEmpresa.ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32()),
                Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()), GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);
            grvOutros.DataBind();
            ibtnAdicionarOutros.Attributes.Add("onmouseover", "tooltip.show('Adiciona um outro vencimento fora do processo')");
        }
        else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "S")
        {
            Processo processo = Processo.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32());

            //Verificando permissão de editar configuração do tipo por empresa do processo            
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                Empresa empresaDoProcesso = processo.Empresa != null ? processo.Empresa : null;

                if (empresaDoProcesso != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(empresaDoProcesso))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroOutros.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = true;
                    opcoesCadastroLicenca.Visible = true;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = true;                   
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroOutros.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = btnRenovarRelatorio.Enabled = btnRenovarTaxa.Visible = btnRenovarTaxa.Enabled = btnRenovarCertificado.Visible = btnRenovarCertificado.Enabled = false;
                    opcoesCadastroLicenca.Visible = false;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = false;                   
                }
            }

            //Verificando permissão de editar configuração do tipo por processo do processo
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                if (processo != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(processo))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroLicenca.Visible = true;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = true;                    
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroLicenca.Visible = false;
                    ibtnAdicionarOutros.Visible = ibtnAdicionarOutros.Enabled = false;                   
                } 
            }

            hfTipoOutros.Value = "proc";
            lblTituloOutros.Text = "Processo: " + processo.Numero + " - Outros";
            mvwProcessos.ActiveViewIndex = 2;
            grvOutros.DataSource = OutrosProcesso.ConsultarPorProcesso(processo);
            grvOutros.DataBind();
            ibtnAdicionarOutros.Attributes.Add("onmouseover", "tooltip.show('Adiciona um outro vencimento dentro do processo')");
        }
        else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "C")
        {
            CadastroTecnicoFederal cadastro = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());

            opcoesCadastroLicenca.Visible = false;

            //Verificando permissão de editar configuração do tipo por empresa do cadastro tecnico federal           
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            {
                Empresa empresaDoCadastro = cadastro.Empresa != null ? cadastro.Empresa : null;

                if (empresaDoCadastro != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(empresaDoCadastro))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroProcessos.Visible = true;
                    opcoesCadastroOutros.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = true;
                    btnRenovarRelatorio.Enabled = true;
                    btnRenovarTaxa.Visible = true;
                    btnRenovarTaxa.Enabled = true;
                    btnRenovarCertificado.Visible = true;
                    btnRenovarCertificado.Enabled = true;
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroProcessos.Visible = false;
                    opcoesCadastroOutros.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = false;
                    btnRenovarRelatorio.Enabled = false;
                    btnRenovarTaxa.Visible = false;
                    btnRenovarTaxa.Enabled = false;
                    btnRenovarCertificado.Visible = false;
                    btnRenovarCertificado.Enabled = false;
                }
            }

            //Verificando permissão de editar configuração do tipo por processo do cadastro tecnico federal           
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO) 
            {
                if (cadastro != null && this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente != null && this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.CadastrosTecnicosPermissaoEdicaoModuloMeioAmbiente.Contains(cadastro))
                {
                    divOpcoesCadastro.Visible = true;
                    opcoesCadastroCadTecnico.Visible = true;
                    btnRenovarRelatorio.Visible = true;
                    btnRenovarRelatorio.Enabled = true;
                    btnRenovarTaxa.Visible = true;
                    btnRenovarTaxa.Enabled = true;
                    btnRenovarCertificado.Visible = true;
                    btnRenovarCertificado.Enabled = true;
                }
                else
                {
                    divOpcoesCadastro.Visible = false;
                    opcoesCadastroCadTecnico.Visible = false;
                    btnRenovarRelatorio.Visible = false;
                    btnRenovarRelatorio.Enabled = false;
                    btnRenovarTaxa.Visible = false;
                    btnRenovarTaxa.Enabled = false;
                    btnRenovarCertificado.Visible = false;
                    btnRenovarCertificado.Enabled = false;
                }
              
            }

            lblEmpresaCTF.Text = cadastro.Empresa.Nome + " - " + cadastro.Empresa.GetNumeroCNPJeCPFComMascara;
            lblSenhaCTF.Text = cadastro.Senha;
            lblAtividadesCTF.Text = cadastro.Atividade;
            lblNumeroLicencaCTF.Text = cadastro.NumeroLicenca;
            lblValidadeLicencaCTF.Text = cadastro.ValidadeLicenca.NaoInformadaToMinValue();
            lblObservacoesCTF.Text = cadastro.Observacoes;

            this.arquivosUpload = this.ReconsultarArquivos(cadastro.Arquivos);            

            this.objetoCTF = cadastro;

            lblDataEntregaCTF.Text = cadastro.GetUltimoRelatorio.Data <= SqlDate.MinValue ? "Não Informado." : cadastro.GetUltimoRelatorio.Data.NaoInformadaToMinValue();
            lblDataProximoAvisoRelAnualCTF.Text = cadastro.GetUltimoRelatorio.Notificacoes != null ? cadastro.GetUltimoRelatorio.GetProximaNotificacao != null ?
                cadastro.GetUltimoRelatorio.GetDataProximaNotificacao : "Não há notificação." : "Não há notificação.";


            lblDataPagamentoCTF.Text = cadastro.GetUltimoPagamento.Data <= SqlDate.MinValue ? "Não Informado." : cadastro.GetUltimoPagamento.Data.NaoInformadaToMinValue();
            lblDataProximoAvisoTaxaCTF.Text = cadastro.GetUltimoPagamento.Notificacoes != null ? cadastro.GetUltimoPagamento.GetProximaNotificacao != null ?
                cadastro.GetUltimoPagamento.GetDataProximaNotificacao : "Não há notificação." : "Não há notificação.";


            lblDataEntregaCertificadoCTF.Text = cadastro.GetUltimoCertificado.Data <= SqlDate.MinValue ? "Não Informado." :
                cadastro.GetUltimoCertificado.Data.NaoInformadaToMinValue();
            lblDataProximoAvisoCertificadoCTF.Text = cadastro.GetUltimoCertificado.Notificacoes != null ? cadastro.GetUltimoCertificado.GetProximaNotificacao != null ?
                cadastro.GetUltimoCertificado.GetDataProximaNotificacao : "Não há notificação." : "Não há notificação.";


            mvwProcessos.ActiveViewIndex = 3;
        }
        else
            mvwProcessos.ActiveViewIndex = -1;
    }

    private void CarregarArvoreCadastroTecnicoFederal()
    {
        hfTipoProcesso.Value = "4";
        trvProcessos.Nodes.Clear();
        lblNomeOrgao.Text = ":: Cadastro Técnico Federal";
        IList<CadastroTecnicoFederal> ctfs = new List<CadastroTecnicoFederal>();
        if (hfIdEmpresa.Value.ToInt32() > 0)
        {
            CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorEmpresa(hfIdEmpresa.Value.ToInt32());
            if (c != null)
                ctfs.Add(c);
        }
        else
            ctfs = CadastroTecnicoFederal.ConsultarPorGrupoEconomicoVerificandoPermissoes(hfIdGrupoEconomico.Value.ToInt32(), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.CadastrosTecnicosPermissaoModuloMeioAmbiente);

        foreach (CadastroTecnicoFederal ctf in ctfs)
        {
            TreeNode noPai = new TreeNode("<b>CNPJ:</b> " + (ctf.Empresa.DadosPessoa.GetType() == typeof(DadosJuridica) ?
                ((DadosJuridica)ctf.Empresa.DadosPessoa).Cnpj : ((DadosFisica)ctf.Empresa.DadosPessoa).Cpf), "c_" + ctf.Id);
            trvProcessos.Nodes.Add(noPai);
        }
        mvwProcessos.ActiveViewIndex = -1;

        divOpcoesCadastro.Visible = this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
    }

    public void PesquisarContratosDiversos()
    {
        IList<ContratoDiverso> contratos = new List<ContratoDiverso>();        

        if (this.ConfiguracaoModuloContratos != null) 
        {
            contratos = ContratoDiverso.ConsultarPorNumeroEObjeto(ddlGrupoEconomicos.SelectedValue.ToInt32(), tbxNumeroContratoDiverso.Text, tbxObjetoContratoDiverso.Text, this.ConfiguracaoModuloContratos.Tipo, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos);
        }        
        
        gdvContratosSelecao.DataSource = contratos;
        gdvContratosSelecao.DataBind();
        lblSemContratos.Visible = contratos != null && contratos.Count > 0 ? false : true;
        this.ContratosConsultados = contratos;
    }

    private void CarregarContratos()
    {
        Processo processo = Processo.ConsultarPorId(HfIdProcesso.Value.ToInt32());
        gvContratos.DataSource = processo != null && processo.ContratosDiversos != null ? processo.ContratosDiversos : new List<ContratoDiverso>();
        gvContratos.DataBind();
        lblQuantidadeContratosProcesso.Text = processo != null && processo.ContratosDiversos != null && processo.ContratosDiversos.Count > 0 ? processo.ContratosDiversos.Count + " contrato(s) associado(s) ao processo." : "Nenhum contrato está associado a este processo.";
    }

    private void VerificarNovaDataVencimentoComExclusaoDeProrrogacao(Condicional condi)
    {
        if (condi.GetType() == typeof(Condicionante))
        {
            Condicionante condicionante = (Condicionante)condi;
            if (condicionante.GetUltimoVencimento.ProrrogacoesPrazo != null && condicionante.GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
            {
                condicionante.GetUltimoVencimento.Data = condicionante.GetUltimoVencimento.GetUltimaProrrogacao.DataProtocoloAdicional.AddDays(condicionante.GetUltimoVencimento.GetUltimaProrrogacao.PrazoAdicional);
            }
            else
            {
                condicionante.GetUltimoVencimento.Data = this.CalcularDataVencimentoCondicionante(condicionante);
            }

            condicionante.Vencimentos[condicionante.Vencimentos.Count - 1] = condicionante.Vencimentos[condicionante.Vencimentos.Count - 1].Salvar();
            condicionante.Salvar();
        }

        if (condi.GetType() == typeof(OutrosEmpresa))
        {
            OutrosEmpresa outrosEmpresa = (OutrosEmpresa)condi;
            if (outrosEmpresa.GetUltimoVencimento.ProrrogacoesPrazo != null && outrosEmpresa.GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
            {
                outrosEmpresa.GetUltimoVencimento.Data = outrosEmpresa.GetUltimoVencimento.GetUltimaProrrogacao.DataProtocoloAdicional.AddDays(outrosEmpresa.GetUltimoVencimento.GetUltimaProrrogacao.PrazoAdicional);
            }
            else
            {
                outrosEmpresa.GetUltimoVencimento.Data = this.CalcularDataVencimentoOutros();
            }

            outrosEmpresa.Vencimentos[outrosEmpresa.Vencimentos.Count - 1] = outrosEmpresa.Vencimentos[outrosEmpresa.Vencimentos.Count - 1].Salvar();
            outrosEmpresa.Salvar();
        }

        if (condi.GetType() == typeof(OutrosProcesso))
        {
            OutrosProcesso outrosProcesso = (OutrosProcesso)condi;
            if (outrosProcesso.GetUltimoVencimento.ProrrogacoesPrazo != null && outrosProcesso.GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
            {
                outrosProcesso.GetUltimoVencimento.Data = outrosProcesso.GetUltimoVencimento.GetUltimaProrrogacao.DataProtocoloAdicional.AddDays(outrosProcesso.GetUltimoVencimento.GetUltimaProrrogacao.PrazoAdicional);
            }
            else
            {
                outrosProcesso.GetUltimoVencimento.Data = this.CalcularDataVencimentoOutros();
            }

            outrosProcesso.Vencimentos[outrosProcesso.Vencimentos.Count - 1] = outrosProcesso.Vencimentos[outrosProcesso.Vencimentos.Count - 1].Salvar();
            outrosProcesso.Salvar();
        }
    }

    private DateTime CalcularDataVencimentoCondicionante(Condicionante c)
    {
        if (c.Vencimentos != null && c.Vencimentos.Count > 1)
        {
            return c.Vencimentos[c.Vencimentos.Count - 2].Data.AddDays(tbxDiasPrazoCondicionante.Text.ToInt32());
        }
        else
        {
            return c.Licenca.DataRetirada.AddDays(tbxDiasPrazoCondicionante.Text.ToInt32());
        }
    }

    private void RenovarVencimentosPeriodicos()
    {
        IList<DateTime> datasDeRenovacao = new List<DateTime>();

        foreach (ListItem l in chkVencimentosPeriodicos.Items)
            if (l.Selected && l.Value != "")
                datasDeRenovacao.Add(Convert.ToDateTime(l.Value.ToString()));

        RenovarPeriodicos.RenovarVencimentosPeriodicos(datasDeRenovacao, hfIdTipoVencimentoPeriodico.Value, hfIdItemVencimentoPeriodico.Value.ToInt32());
        lblRenovacaoVencimentosPeriodicosDatas_popupextender.Hide();
        msg.CriarMensagem("Vencimentos renovados com sucesso", "Sucesso", MsgIcons.Sucesso);
        for (int i = this.ItensRenovacao.Count - 1; i > -1; i--)
        {
            if (this.ItensRenovacao[i].idItem == hfIdItemVencimentoPeriodico.Value.ToInt32() && this.ItensRenovacao[i].tipoItem == hfIdTipoVencimentoPeriodico.Value)
            {
                this.ItensRenovacao.Remove(this.ItensRenovacao[i]);
            }
        }

        if (this.ItensRenovacao != null && this.ItensRenovacao.Count > 0)
        {
            rptRenovacoes.DataSource = this.ItensRenovacao;
            rptRenovacoes.DataBind();
            lblRenovacaoVencimentosPeriodicos_popupextender.Show();
        }
        else
        {
            lblRenovacaoVencimentosPeriodicos_popupextender.Hide();
            this.fecharPopUpsDaRenovacaoPeriodica(hfIdTipoVencimentoPeriodico.Value);
            if (hfIdTipoVencimentoPeriodico.Value.ToUpper() == "RELATORIOCTF" || hfIdTipoVencimentoPeriodico.Value.ToUpper() == "TAXACTF" || hfIdTipoVencimentoPeriodico.Value.ToUpper() == "CERTIFICADOCTF")
            {
                this.CarregarArvoreCadastroTecnicoFederal();
            }
            else
            {
                this.ExibirVisao();
            }
        }
    }

    private IList<Vencimento> RetornarVencimentosDoItemDaRenovacao(string tipoItem, int idItem)
    {
        if (tipoItem.ToUpper() == "CONDICIONANTE")
        {
            Condicionante c = Condicionante.ConsultarPorId(idItem);
            return c.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "OUTROSEMPRESA")
        {
            OutrosEmpresa o = OutrosEmpresa.ConsultarPorId(idItem);
            return o.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "OUTROSPROCESSO")
        {
            OutrosProcesso op = OutrosProcesso.ConsultarPorId(idItem);
            return op.Vencimentos;
        }
        else if (tipoItem.ToUpper() == "RELATORIOCTF")
        {
            CadastroTecnicoFederal cadRel = CadastroTecnicoFederal.ConsultarPorId(idItem);
            return cadRel.EntregaRelatorioAnual;
        }
        else if (tipoItem.ToUpper() == "TAXACTF")
        {
            CadastroTecnicoFederal cadTaxa = CadastroTecnicoFederal.ConsultarPorId(idItem);
            return cadTaxa.TaxaTrimestral;
        }
        else if (tipoItem.ToUpper() == "CERTIFICADOCTF")
        {
            CadastroTecnicoFederal cadCert = CadastroTecnicoFederal.ConsultarPorId(idItem);
            return cadCert.CertificadoRegularidade;
        }

        return null;
    }

    public void fecharPopUpsDaRenovacaoPeriodica(string tipoItem)
    {
        if (tipoItem.ToUpper() == "CONDICIONANTE")
        {
            lkbOpcoesCondicionante_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "OUTROSEMPRESA")
        {
            divPopUpOutros_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "OUTROSPROCESSO")
        {
            divPopUpOutros_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "RELATORIOCTF")
        {
            lblCTF_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "TAXACTF")
        {
            lblCTF_ModalPopupExtender.Hide();
        }
        else if (tipoItem.ToUpper() == "CERTIFICADOCTF")
        {
            lblCTF_ModalPopupExtender.Hide();
        }

    }

    #endregion

    #region ___________Bindings____________

    public string BindQuantidade(Object o)
    {
        if (this.ConfiguracaoModuloMeioAmbiente == null || this.ConfiguracaoModuloMeioAmbiente.Id == 0)
            Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

        if (((OrgaoAmbiental)o).Id == -5)
        {
            IList<OrgaoAmbiental> orgaos = OrgaoAmbiental.ConsultarComOutros(Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()),
                GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);
            return (ddlEmpresa.SelectedValue.ToInt32() > 0 ? orgaos.Where(d => d.OutrosEmpresas.Where(a => a.Empresa.Id == ddlEmpresa.SelectedValue.ToInt32()).ToList().Count > 0).ToList().Count :
                orgaos.ToList().Count) + " Orgãos";
        }
        if (((OrgaoAmbiental)o).Id == -4)
        {
            IList<CadastroTecnicoFederal> ctfs = new List<CadastroTecnicoFederal>();
            if (ddlEmpresa.SelectedIndex > 0)
            {
                CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorEmpresa(hfIdEmpresa.Value.ToInt32());
                if (c != null)
                    ctfs.Add(c);
            }
            else
                ctfs = CadastroTecnicoFederal.ConsultarPorGrupoEconomicoVerificandoPermissoes(hfIdGrupoEconomico.Value.ToInt32(), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.CadastrosTecnicosPermissaoModuloMeioAmbiente);
            return ctfs != null ? ctfs.Count + " Processo(s)" : "0 Processo(s)";
        }

        return Processo.ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes((OrgaoAmbiental)o, Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()),
            GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()), this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente).Count + " Processo(s)";
    }

    public IList<Status> CarregarStatus()
    {
        return Status.ConsultarTodos();
    }

    public string SetarStatus(Object o)
    {
        return ((Condicionante)o).Id.ToString();
    }

    public string bindDataVencimento(Object o)
    {
        Condicionante c = (Condicionante)o;
        return c.GetUltimoVencimento.GetDataVencimento;
    }

    public string bindDataAviso(Object o)
    {
        Condicionante c = (Condicionante)o;
        return c.GetUltimoVencimento.GetDataProximaNotificacao;
    }

    public string bindProrrogacoes(Object o)
    {
        Condicionante c = (Condicionante)o;
        if (c.GetUltimoVencimento != null && c.GetUltimoVencimento.ProrrogacoesPrazo != null && c.GetUltimoVencimento.ProrrogacoesPrazo.Count > 0)
            return c.GetUltimoVencimento.ProrrogacoesPrazo.Count.ToString();
        else
            return "0";
    }

    public string bindEmpresaOutros(Object o)
    {
        if (hfTipoOutros.Value.Equals("emp"))
        {
            OutrosEmpresa c = (OutrosEmpresa)o;
            return c.Empresa != null ? c.Empresa.Nome : "";
        }
        else
        {
            OutrosProcesso c = (OutrosProcesso)o;
            return c.GetEmpresa != null ? c.GetEmpresa.Nome : "";
        }
    }

    public bool BindingVisivelOutrosPorPermissao(Object o)
    {
        if (this.ConfiguracaoModuloMeioAmbiente == null)
            return false;

        if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            return this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);

        if (hfTipoOutros.Value.Equals("emp"))
        {
            OutrosEmpresa c = (OutrosEmpresa)o;
           
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                return c.Empresa != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(c.Empresa);

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                return this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.OutrosEmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(c);
        }
        else
        {
            OutrosProcesso c = (OutrosProcesso)o;

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                return c.Processo != null && c.Processo.Empresa != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(c.Processo.Empresa);

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                return c.Processo != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(c.Processo);
        }

        return false;
    }    

    public string bindDataVencimentoOutros(Object o)
    {
        if (hfTipoOutros.Value.Equals("emp"))
        {
            OutrosEmpresa c = (OutrosEmpresa)o;
            return c.GetUltimoVencimento.GetDataVencimento;
        }
        else
        {
            OutrosProcesso c = (OutrosProcesso)o;
            return c.GetUltimoVencimento.GetDataVencimento;
        }
    }

    public string bindDataAvisoOutros(Object o)
    {
        if (hfTipoOutros.Value.Equals("emp"))
        {
            OutrosEmpresa c = (OutrosEmpresa)o;
            return c.GetUltimoVencimento.GetDataProximaNotificacao;
        }
        else
        {
            OutrosProcesso c = (OutrosProcesso)o;
            return c.GetUltimoVencimento.GetDataProximaNotificacao;
        }
    }

    public string bindStatus(Object o)
    {
        Condicionante c = (Condicionante)o;
        return c.GetUltimoVencimento.Status != null ? c.GetUltimoVencimento.Status.Nome : "";
    }

    public string BindImagemRenovacao(Object o)
    {
        Condicional e = (Condicional)o;
        if (e.Vencimentos != null && e.Vencimentos.Count > 0)
            if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
                return "~/imagens/calendar.png";
            else
                return "~/imagens/calendar_d.png";
        return "~/imagens/calendar_d.png";
    }

    public string BindToolTipoRenovacao(Object o)
    {
        Condicional e = (Condicional)o;

        if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
        {
            return "Clique para renovar.";
        }
        else
        {
            return "Esta condicional não é Periodica.";
        }
    }

    public bool BindEnableRenovacao(Object o)
    {
        Condicional e = (Condicional)o;
        //return e.Vencimentos[e.Vencimentos.Count - 1].Periodico;

        //
        if (e.Vencimentos != null && e.Vencimentos.Count > 0)
        {
            if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
            {
                return e.Vencimentos[e.Vencimentos.Count - 1].Periodico;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool BindingVisivelPorPermissao(Object o)
    {
        if (this.ConfiguracaoModuloMeioAmbiente == null)
            return false;

        if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            return this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);

        if (o.GetType() == typeof(Condicionante)) 
        {
            Condicionante e = (Condicionante)o;

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                return e.GetObjetoEmpresa != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente != null && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.EmpresasPermissaoEdicaoModuloMeioAmbiente.Contains(e.GetObjetoEmpresa);

            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                return e.GetProcesso != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente != null && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Count > 0 && this.ProcessosPermissaoEdicaoModuloMeioAmbiente.Contains(e.GetProcesso);
        }   

        return false;
    }    

    public string BindImagemRenovacaoOutros(Object o)
    {
        if (hfTipoOutros.Value == "emp")
        {
            OutrosEmpresa e = (OutrosEmpresa)o;
            if (e.Vencimentos != null && e.Vencimentos.Count > 0)
                if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
                    return "~/imagens/calendar.png";
                else
                    return "~/imagens/calendar_d.png";
            return "~/imagens/calendar_d.png";
        }
        else
        {
            OutrosProcesso e = (OutrosProcesso)o;
            if (e.Vencimentos != null && e.Vencimentos.Count > 0)
                if (e.Vencimentos[e.Vencimentos.Count - 1].Periodico)
                    return "~/imagens/calendar.png";
                else
                    return "~/imagens/calendar_d.png";
            return "~/imagens/calendar_d.png";
        }
    }

    public string BindToolTipoRenovacaoOutros(Object o)
    {
        if (hfTipoOutros.Value == "emp")
        {
            OutrosEmpresa e = (OutrosEmpresa)o;
            if (e.GetUltimoVencimento.Periodico)
                return "Clique para renovar.";
            else
                return "Este item não é Periodica.";
        }
        else
        {
            OutrosProcesso e = (OutrosProcesso)o;
            if (e.GetUltimoVencimento.Periodico)
                return "Clique para renovar.";
            else
                return "Este item não é Periodica.";
        }
    }

    public bool BindEnableRenovacaoOutros(Object o)
    {
        if (hfTipoOutros.Value == "emp")
        {
            OutrosEmpresa e = (OutrosEmpresa)o;
            return e.GetUltimoVencimento.Periodico;
        }
        else
        {
            OutrosProcesso e = (OutrosProcesso)o;
            return e.GetUltimoVencimento.Periodico;
        }
    }

    public bool BindEnableDownloadAtendimentoCondicionante(Object o)
    {
        return true;
    }

    public bool BindEnableDownloadAtendimento(Object o)
    {
        return true;
    }

    public String bindingObjeto(Object o)
    {
        ContratoDiverso cont = (ContratoDiverso)o;
        return cont.Objeto;
    }

    public String bindingStatusContrato(Object o)
    {
        ContratoDiverso cont = (ContratoDiverso)o;
        return cont.StatusContratoDiverso != null ? cont.StatusContratoDiverso.Nome : "";
    }

    public string BindDiasRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return item != null && item.diasRenovacao > 0 ? item.diasRenovacao.ToString() : "";
    }

    public string BindIdItemRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return item != null && item.idItem > 0 ? item.idItem.ToString() : "";
    }

    public string BindTipoItemRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return item != null && item.tipoItem.IsNotNullOrEmpty() ? item.tipoItem : "";
    }

    public string BindNomeItemDaRenovacao(Object o)
    {
        ItemRenovacao item = (ItemRenovacao)o;
        return RenovarPeriodicos.NomeItemFormatado(item.tipoItem, item.idItem);
    }

    #endregion

    #region ___________Eventos_____________

    protected void lkbObservacoes_Click(object sender, EventArgs e)
    {
        try
        {
            CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
            grvHistoricos.DataSource = cad.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "CadastroTecnicoFederal";
            hfIDObs.Value = cad.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();
            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void ibtnAddObs_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CriarHistorico();
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

    protected void lkbObservacoesCondicionante_Click(object sender, EventArgs e)
    {
        try
        {
            Condicionante cad = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
            grvHistoricos.DataSource = cad.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "Condicionante";
            hfIDObs.Value = cad.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();
            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void lkbObservacoesOutros_Click(object sender, EventArgs e)
    {
        try
        {
            Condicional cad = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());
            grvHistoricos.DataSource = cad.Historicos.OrderByDescending(i => i.Id).ToList();
            grvHistoricos.DataBind();
            hfTypeObs.Value = "Condicional";
            hfIDObs.Value = cad.Id.ToString();
            ModalPopupExtenderhistorico_ModalPopupExtender.Show();
            tbxTituloObs.Text = "";
            tbxObservacaoObs.Text = "";
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

    protected void btnAlterarStatus_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarHistoricoAlteracaoStatus();
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

    protected void btnFecharStatus_Click(object sender, EventArgs e)
    {
        lblAlteracaoStatus_ModalPopupExtender.Hide();
    }

    protected void lkbProrrogacao_Click(object sender, EventArgs e)
    {
        try
        {
            tbxPrazoAdicional.Text = "";
            tbxDataProtocoloAdd.Text = "";
            tbxProtocoloAdicional.Text = "";
            this.CarregarProrrogacoes("CONDICIONANTE");
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

    protected void btnEnviarHistoricoGeral_Click(object sender, EventArgs e)
    {
        try
        {
            chkGruposHistorico.Items.Clear();
            chkEmpresaHistorico.Items.Clear();
            chkConsultoraHistorico.Items.Clear();

            lblEnvioHistoricoTotal_popupextender.Show();

            this.CarregarListaEmails(chkEmpresaHistorico, this.CarregarEmailsEmpresaHistorico().Split(';'));
            this.CarregarListaEmails(chkGruposHistorico, this.CarregarEmailsGrupoEconomico().Split(';'));
            this.CarregarListaEmails(chkConsultoraHistorico, this.CarregarEmailsConsultoraHistorico().Split(';'));
            tbxEmailsHistorico.Text = "";
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

    protected void btnEnviarHistorico_Click(object sender, EventArgs e)
    {
        try
        {
            if (tbxEmailsHistorico.Text.Trim().Replace(" ", "") != "")
            {
                if (!WebUtil.ValidarEmailInformado(tbxEmailsHistorico.Text))
                {
                    msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }

            this.EnviarHistorico();
            lblEnvioHistoricoTotal_popupextender.Hide();
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

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderlblProrrogacao.Hide();
    }

    protected void lkbProrrogacaoOutros_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarProrrogacoes("OUTROS");
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

    protected void lkbVenc0_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "L")
            {
                Licenca regime = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                if (regime.GetUltimoVencimento != null && regime.GetUltimoVencimento.Data != SqlDate.MinValue)
                {
                    lblDataVencimento.Text = regime.GetUltimoVencimento.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoVencimento.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc3_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "C")
            {
                CadastroTecnicoFederal regime = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                if (regime.GetUltimoCertificado != null && regime.GetUltimoCertificado.Data != SqlDate.MinValue)
                {
                    lblDataVencimento.Text = regime.GetUltimoCertificado.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoCertificado.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc1_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "C")
            {
                CadastroTecnicoFederal regime = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                if (regime.GetUltimoPagamento != null && regime.GetUltimoPagamento.Data != SqlDate.MinValue)
                {
                    lblDataVencimento.Text = regime.GetUltimoPagamento.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoPagamento.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void lkbVenc2_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "C")
            {
                CadastroTecnicoFederal regime = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                if (regime.GetUltimoRelatorio != null && regime.GetUltimoRelatorio.Data != SqlDate.MinValue)
                {
                    lblDataVencimento.Text = regime.GetUltimoRelatorio.Data.EmptyToMinValue();
                    grvNotificacoes.DataSource = regime.GetUltimoRelatorio.Notificacoes;
                    grvNotificacoes.DataBind();
                    lblNotficacoes_ModalPopupExtender.Show();
                }
                else
                {
                    msg.CriarMensagem("Este vencimento ainda não foi informado.", "Atenção", MsgIcons.Informacao);
                }
            }
            else
            {
                msg.CriarMensagem("Não foi possível abrir as notificações.", "Atenção", MsgIcons.Informacao);
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

    protected void btnSalvarVencimento_Click(object sender, EventArgs e)
    {
        try
        {            
            this.SalvarVencimento();
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

    protected void lkbVencimentoOutros_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnGeralOutros.Checked)
            {
                OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(hfIdOutros.Value.ToInt32());
                this.CarregarVencimentos(outros.Vencimentos, "OutrosEmpresa", outros.Id);
            }
            else if (rbtnProcessoOutros.Checked)
            {
                OutrosProcesso outros = OutrosProcesso.ConsultarPorId(hfIdOutros.Value.ToInt32());
                this.CarregarVencimentos(outros.Vencimentos, "OutrosProcesso", outros.Id);
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

    protected void ibtnRemoverVencimento_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
            this.RemoverVencimento();
            
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

    protected void ddlVencimentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarVencimento();
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

    protected void grvNotificacaoVencimentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvNotificacaoVencimentos.PageIndex = e.NewPageIndex;
            grvNotificacaoVencimentos.DataSource = Vencimento.ConsultarPorId(ddlVencimentos.SelectedValue.ToInt32()).Notificacoes;
            grvNotificacaoVencimentos.DataBind();
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

    protected void lkbVencimentoCondicionante_Click(object sender, EventArgs e)
    {
        try
        {
            Condicionante condionante = new Condicionante(hfIdCondicionante.Value);
            condionante = condionante.ConsultarPorId();
            this.CarregarVencimentos(condionante.Vencimentos, "Condicionante", condionante.Id);
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

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hfIdEmpresa.Value = ddlEmpresa.SelectedValue;
            if (ddlEmpresa.SelectedIndex > 0)
            {
                Empresa em = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());
                this.CarregarOrgaos(null, em);
            }
            else
            {
                GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
                this.CarregarOrgaos(c, null);
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            trvProcessos.Nodes.Clear();
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlGrupoEconomicos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hfIdOrgao.Value = null;
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
            hfIdGrupoEconomico.Value = ddlGrupoEconomicos.SelectedValue;
            this.CarregarEmpresaComStatus(c, ddlStatusEmpresa.SelectedValue);
       
            this.CarregarOrgaos(c, null);

            //verificando as permissoes
            divOpcoesCadastro.Visible = false;
            if (this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
            {
                if (this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado))
                {
                    divOpcoesCadastro.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            trvProcessos.Nodes.Clear();
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void trvProcessos_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            this.ExibirVisao();
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

    protected void ddlEmpresaOutros_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOrgaosAmbientaisOutros();
            this.CarregarProcessosOutros();
            this.AdicionarEventoPopUpOutros();
            btnUploadOutros.Visible = ddlEmpresaOutros.SelectedIndex > 0;
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

    protected void ddlOrgaoOutros_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarProcessosOutros();
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

    protected void btnSalvarProcesso_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarProcesso();
            this.CarregarProcessosPOPUP();
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

    protected void lkbOpcoesProcessoEditar_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "P")
            {
                this.CarregarPopUpCadastroProcesso();
                this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaProcesso);

                Processo p = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                //CARREGAR DADOS NO POPUP DE EDIÇÃO

                OrgaoAmbiental oa = OrgaoAmbiental.ConsultarPorId(p.OrgaoAmbiental.Id);

                ddlEmpresaProcesso.SelectedValue = p.Empresa.Id.ToString();
                if (oa.GetType() == typeof(OrgaoEstadual))
                {
                    ddlTipoOrgaoProcesso.SelectedIndex = 0;
                    ddlTipoOrgaoProcesso_SelectedIndexChanged(null, null);
                    ddlEstadoProcesso.SelectedValue = ((OrgaoEstadual)p.OrgaoAmbiental).Estado != null ? ((OrgaoEstadual)p.OrgaoAmbiental).Estado.Id.ToString() : "0";
                    ddlEstadoLicenca_SelectedIndexChanged(null, null);
                    ddlOrgaoProcesso.SelectedValue = p.OrgaoAmbiental.Id.ToString();
                }
                if (oa.GetType() == typeof(OrgaoFederal))
                {
                    ddlTipoOrgaoProcesso.SelectedIndex = 1;
                    ddlTipoOrgaoProcesso_SelectedIndexChanged(null, null);
                    ddlOrgaoProcesso.SelectedValue = p.OrgaoAmbiental.Id.ToString();
                }
                if (oa.GetType() == typeof(OrgaoMunicipal))
                {
                    ddlTipoOrgaoProcesso.SelectedIndex = 2;
                    ddlTipoOrgaoProcesso_SelectedIndexChanged(null, null);
                    ddlEstadoProcesso.SelectedValue = ((OrgaoMunicipal)p.OrgaoAmbiental).Cidade != null ? ((OrgaoMunicipal)p.OrgaoAmbiental).Cidade.Estado.Id.ToString() : "0";
                    ddlEstadoLicenca_SelectedIndexChanged(null, null);
                    ddlCidadeProcesso.SelectedValue = ((OrgaoMunicipal)p.OrgaoAmbiental).Cidade != null ? ((OrgaoMunicipal)p.OrgaoAmbiental).Cidade.Id.ToString() : "0";
                    ddlCidadeProcesso_SelectedIndexChanged(null, null);
                    ddlOrgaoProcesso.SelectedValue = p.OrgaoAmbiental.Id.ToString();
                }
                if (p.Consultora != null)
                    ddlConsultoraProcesso.SelectedValue = p.Consultora.Id.ToString();
                tbxDataAberturaProcesso.Text = p.DataAbertura.ToString();
                tbxNumeroProcesso.Text = p.Numero;
                tbxObservacaoProcesso.Text = p.Observacoes;
                HfIdProcesso.Value = p.Id.ToString();
                btnAbrirContratos.Visible = HfIdProcesso.Value.ToInt32() > 0 && this.UsuarioLogado.PossuiPermissaoDeEditarModuloContratos ? true : false;
                lkbOpcoesProcessoNovo_ModalPopupExtender.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione um processo na Árvore abaixo!", "Alerta", MsgIcons.Alerta);
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

    protected void lkbOpcoesProcessoNovo_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomicos.SelectedIndex > 0)
            {
                this.CarregarPopUpCadastroProcesso();

                WebUtil.LimparCampos(upPopProcesso.Controls[0].Controls);
                HfIdProcesso.Value = "";
                this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaProcesso);

                if (ddlEmpresa.SelectedValue.IsNotNullOrEmpty())
                {
                    ddlEmpresaProcesso.SelectedValue = ddlEmpresa.SelectedValue;
                }
                if (hfIdOrgao.Value.IsNotNullOrEmpty() && hfIdOrgao.Value.ToInt32() > 0)
                {
                    OrgaoAmbiental o = OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32());
                    ddlOrgaoProcesso.Items.Add(new ListItem(o.Nome, o.Id.ToString()));
                    ddlOrgaoProcesso.SelectedValue = hfIdOrgao.Value;
                }
                ddlTipoOrgaoProcesso_SelectedIndexChanged(null, null);
                tbxDataAberturaProcesso.Text = DateTime.Now.EmptyToMinValue();
                btnAbrirContratos.Visible = false;

                if (ddlOrgaoProcesso.Items.Count > 0)
                    ddlOrgaoProcesso.SelectedIndex = 0;

                if (ddlEstadoProcesso.Items.Count > 0)
                    ddlEstadoProcesso.SelectedIndex = 0;

                if (ddlCidadeProcesso.Items.Count > 0)
                    ddlCidadeProcesso.SelectedIndex = 0;


            }
            else
            {
                msg.CriarMensagem("Selecione um Grupo Econômico primeiro!", "ATENÇÃO", MsgIcons.Alerta);
            }

            btnAbrirContratos.Visible = false;
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

    protected void lkbOpcoesProcessoExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "P")
            {
                bool x = Processo.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32()).Excluir();
                if (x)
                {
                    msg.CriarMensagem("Processo excluído com Sucesso!", "Sucesso");
                    transacao.Recarregar(ref msg);

                    if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                    {
                        this.VerificarPermissoes();
                    }
                    this.CarregarArvore();
                    this.CarregarOrgaos(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()),
                        Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()));
                    this.CarregarProcessosPOPUP();
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um processo na Árvore abaixo!", "Alerta", MsgIcons.Alerta);
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

    protected void lkbOpcoesLicencaNova_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode == null)
            {
                msg.CriarMensagem("Selecione um processo na árvore.", "Alerta", MsgIcons.Alerta);
                return;
            }
            WebUtil.LimparCampos(upLicenca.Controls[0].Controls);
            arquivosUpload = null;
            notificacoesSalvas = new List<Notificacao>();
            grvNotificacaoLicenca.DataSource = notificacoesSalvas;
            grvNotificacaoLicenca.DataBind();
            if (hfIdGrupoEconomico.Value.IsNotNullOrEmpty() && hfIdOrgao.Value.IsNotNullOrEmpty())
            {
                if (hfIdOrgao.Value.ToInt32() == -1 && trvProcessos.SelectedNode != null)
                {
                    OrgaoMunicipal om = new OrgaoMunicipal();

                    if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() != "P")
                    {
                        msg.CriarMensagem("Selecione um processo na lista abaixo", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                    om = OrgaoMunicipal.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32());
                    ddlProcessoLicenca.Items.Clear();
                    ddlProcessoLicenca.DataSource = om.Processos;
                    ddlProcessoLicenca.DataBind();
                    ddlProcessoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));

                    if (ddlTipoLicenca.Items.Count == 0)
                    {
                        ddlTipoLicenca.DataSource = TipoLicenca.ConsultarTodos();
                        ddlTipoLicenca.DataBind();
                        ddlTipoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
                    }

                    if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "P")
                        if (ddlProcessoLicenca.Items.Count > 1)
                            ddlProcessoLicenca.SelectedValue = trvProcessos.SelectedNode.Value.Split('_')[1].ToString();

                    this.ExibirPopUpLicenca();
                }
                else
                {
                    WebUtil.LimparCampos(upLicenca.Controls[0].Controls);
                    HfIdLicenca.Value = "";

                    IList<Processo> processos = Processo.ConsultarPorOrgaoEmpresaGrupoEconomico(new OrgaoAmbiental(hfIdOrgao.Value.ToInt32()), Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()), GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()));
                    if (processos.Count == 0)
                    {
                        msg.CriarMensagem("É necessário primeiro cadastrar um Processo!", "Alerta", MsgIcons.Alerta);
                        return;
                    }

                    ddlProcessoLicenca.DataSource = processos;
                    ddlProcessoLicenca.DataBind();
                    ddlProcessoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));

                    if (ddlTipoLicenca.Items.Count == 0)
                    {
                        ddlTipoLicenca.DataSource = TipoLicenca.ConsultarTodos();
                        ddlTipoLicenca.DataBind();
                        ddlTipoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
                    }

                    if (trvProcessos.SelectedNode != null)
                    {
                        if (ddlProcessoLicenca.Items.Count > 1)
                            ddlProcessoLicenca.SelectedValue = IdSelecionadoArvore("p").ToString();
                    }
                    this.ExibirPopUpLicenca();
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um Orgão Ambiental!", "Alerta", MsgIcons.Alerta);
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

    protected void btnSalvarLicenca_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarLicenca();
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

    protected void lkbOpcoesLicencaExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "L")
            {
                bool x = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32()).Excluir();
                if (x)
                {
                    msg.CriarMensagem("Licença excluída com Sucesso!", "Sucesso", MsgIcons.Sucesso);
                    transacao.Fechar(ref msg);
                    transacao.Abrir();
                    this.CarregarArvore();
                    mvwProcessos.ActiveViewIndex = -1;
                }
            }
            else
            {
                msg.CriarMensagem("Selecione uma Licença na Árvore abaixo!", "Alerta", MsgIcons.Alerta);
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

    protected void lkbOpcoesLicencaEditar_Click(object sender, EventArgs e)
    {
        try
        {
            WebUtil.LimparCampos(upLicenca.Controls[0].Controls);
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "L")
            {
                Licenca l = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                //CARREGAR DADOS NO POPUP DE EDIÇÃO

                if (l.Processo != null)
                    ddlProcessoLicenca.SelectedValue = l.Processo.Id.ToString();

                tbxDescricaoLicenca.Text = l.Descricao;
                tbxNumeroLicenca.Text = l.Numero;
                tbxDiasValidadeLicenca.Text = l.DiasValidade.ToString();
                tbxDataRetiradaLicenca.Text = l.DataRetirada.EmptyToMinValue();

                ddlLicencaCadastroEstado.SelectedValue = l.Cidade != null ? l.Cidade.Estado.Id.ToString() : "0";
                this.CarregarCidadeCadastroLicenca();
                ddlLicencaCadastroCidade.SelectedValue = l.Cidade != null ? l.Cidade.Id.ToString() : "0";

                this.arquivosUpload = this.ReconsultarArquivos(l.Arquivos);

                if (ddlTipoLicenca.Items.Count == 0)
                {
                    ddlTipoLicenca.DataSource = TipoLicenca.ConsultarTodos();
                    ddlTipoLicenca.DataBind();
                    ddlTipoLicenca.Items.Insert(0, new ListItem("-- Selecione --", "0"));
                }

                notificacoesSalvas = l.GetUltimoVencimento.Notificacoes;

                this.CarregarNotificacoes(l.GetUltimoVencimento, grvNotificacaoLicenca);

                ddlTipoLicenca.SelectedValue = l.TipoLicenca.Id.ToString();

                HfIdLicenca.Value = l.Id.ToString();

                this.ExibirPopUpLicenca();
            }
            else
            {
                msg.CriarMensagem("Selecione uma Licença na Árvore abaixo!", "Alerta", MsgIcons.Alerta);
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

    protected void btnSalvarCondicionante_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarCondicionante();
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

    protected void btnSalvarRenovacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarRenovacao();
            transacao.Recarregar(ref msg);
            this.ExibirVisao();
           
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

    protected void btnSalvarOutros_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarOutrosPopUp();
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

    protected void rbtnProcessoOutros_CheckedChanged(object sender, EventArgs e)
    {
        this.RadioChange();
    }

    protected void rbtnGeralOutros_CheckedChanged(object sender, EventArgs e)
    {
        this.RadioChange();
    }

    protected void cbxPeriodicaOutros_CheckedChanged(object sender, EventArgs e)
    {
        this.RadioChange();
    }

    protected void grvOutros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.CarregarOutrosPOPUP();
            rbtnGeralOutros.Enabled = rbtnProcessoOutros.Enabled = false;
            if (hfTipoOutros.Value.Equals("emp"))
            {
                OutrosEmpresa oe = OutrosEmpresa.ConsultarPorId(grvOutros.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
                if (oe != null)
                {
                    rbtnGeralOutros.Checked = true;
                    rbtnProcessoOutros.Checked = false;
                    this.RadioChange();
                    ddlEmpresaGeralOutros.SelectedValue = oe.Empresa != null ? oe.Empresa.Id.ToString() : "0";
                    ddlOrgaoGeralOutros.SelectedValue = oe.OrgaoAmbiental != null ? oe.OrgaoAmbiental.Id.ToString() : "0";
                    tbxProtocoloOutros.Text = oe.GetUltimoVencimento.ProtocoloAtendimento;
                    tbxDescricaoOutros.Text = oe.Descricao;
                    ddlConsultoriaOutros.SelectedValue = oe.Consultora != null ? oe.Consultora.Id.ToString() : "0";

                    tbxDataProtocoloOutros.Text = oe.GetUltimoVencimento.DataAtendimento.EmptyToMinValue();
                    tbxDiasValidadeOutros.Text = oe.DiasPrazo.ToString();
                    tbxObservacoesOutros.Text = oe.Observacoes;
                    ddlStatusOutros.SelectedValue = oe.GetUltimoVencimento.Status.Id.ToString();
                    this.notificacoesSalvas = oe.GetUltimoVencimento.Notificacoes;
                    tbxDataRecebimentoOutros.Text = oe.DataRecebimento.EmptyToMinValue();
                    this.arquivosUpload = this.ReconsultarArquivos(oe.Arquivos);

                    lkbProrrogacaoOutros.Visible = true;
                    tbxDiasValidadeOutros.Enabled = true;
                    if (oe.Vencimentos != null && oe.Vencimentos.Count > 0)
                    {
                        Vencimento v = oe.Vencimentos[oe.Vencimentos.Count - 1];
                        cbxPeriodicaOutros.Checked = v.Periodico;

                        this.CarregarNotificacoes(v, grvNotificacaoOutros);

                        if (v.ProrrogacoesPrazo != null)
                        {
                            lkbProrrogacaoOutros.Visible = true;
                            lkbProrrogacaoOutros.Text = "Abrir Prorrogações - [" + v.ProrrogacoesPrazo.Count + "] Prorrogações.";
                            if (v.ProrrogacoesPrazo.Count > 0)
                                tbxDiasValidadeOutros.Enabled = false;
                            else
                                tbxDiasValidadeOutros.Enabled = true;
                        }
                        else
                        {
                            lkbProrrogacaoOutros.Text = "Abrir Prorrogações";
                            tbxDiasValidadeOutros.Enabled = true;
                        }
                    }

                    hfTipoOutros.Value = "emp";
                    hfIdOutros.Value = oe.Id.ToString();
                    ExibirPopUpOutros();

                    WebUtil.AdicionarEventoShowModalDialog(btnUploadOutros, "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros(
                        "idEmpresa=" + ddlEmpresaGeralOutros.SelectedValue + "&idCliente=" + ddlGrupoEconomicos.SelectedValue + "&tipo=ProcessoDNPM"),
                        "Upload Arquivo Processo DNPM", 550, 420);
                }
            }
            else if (hfTipoOutros.Value.Equals("proc"))
            {
                OutrosProcesso op = OutrosProcesso.ConsultarPorId(grvOutros.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
                if (op != null)
                {
                    rbtnGeralOutros.Checked = false;
                    rbtnProcessoOutros.Checked = true;
                    this.RadioChange();
                    if (op.Processo != null)
                    {
                        ddlEmpresaOutros.SelectedValue = op.Processo.Empresa != null ? op.Processo.Empresa.Id.ToString() : "0";
                        this.CarregarOrgaosAmbientaisOutros();
                        ddlOrgaoOutros.SelectedValue = op.Processo.OrgaoAmbiental != null ? op.Processo.OrgaoAmbiental.Id.ToString() : "0";
                        this.CarregarProcessosOutros();
                        ddlProcessoOutros.SelectedValue = op.Processo.Id.ToString();
                    }
                    tbxProtocoloOutros.Text = op.GetUltimoVencimento.ProtocoloAtendimento;
                    tbxDescricaoOutros.Text = op.Descricao;

                    tbxDiasValidadeOutros.Text = op.DiasPrazo.ToString();
                    tbxObservacoesOutros.Text = op.Observacoes;
                    ddlStatusOutros.SelectedValue = op.GetUltimoVencimento.Status.Id.ToString();
                    this.notificacoesSalvas = op.GetUltimoVencimento.Notificacoes;
                    tbxDataRecebimentoOutros.Text = op.DataRecebimento.EmptyToMinValue();
                    this.arquivosUpload = this.ReconsultarArquivos(op.Arquivos);

                    if (op.Vencimentos != null && op.Vencimentos.Count > 0)
                    {
                        Vencimento v = op.Vencimentos[op.Vencimentos.Count - 1];
                        cbxPeriodicaOutros.Checked = v.Periodico;

                        this.CarregarNotificacoes(v, grvNotificacaoOutros);

                        if (v.ProrrogacoesPrazo != null)
                        {
                            lkbProrrogacaoOutros.Visible = true;
                            lkbProrrogacaoOutros.Text = "Abrir Prorrogações - [" + v.ProrrogacoesPrazo.Count + "] Prorrogações.";
                            if (v.ProrrogacoesPrazo.Count > 0)
                                tbxDiasValidadeOutros.Enabled = false;
                            else
                                tbxDiasValidadeOutros.Enabled = true;
                        }
                        else
                        {
                            lkbProrrogacaoOutros.Text = "Abrir Prorrogações";
                            tbxDiasValidadeOutros.Enabled = true;
                        }
                    }

                    hfTipoOutros.Value = "proc";
                    hfIdOutros.Value = op.Id.ToString();
                    ExibirPopUpOutros();

                    WebUtil.AdicionarEventoShowModalDialog(btnUploadOutros, "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros(
                        "idEmpresa=" + ddlEmpresaOutros.SelectedValue + "&idCliente=" + ddlGrupoEconomicos.SelectedValue + "&tipo=ProcessoDNPM"),
                        "Upload Arquivo Processo DNPM", 550, 420);
                }
            }
            lkbVencimentoOutros.Visible = true;
            lkbObservacoesOutros.Visible = true;
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

    private void CarregarNotificacoes(Vencimento v, GridView grv)
    {
        IList<Notificacao> ns = v.Notificacoes != null ? v.Notificacoes : new List<Notificacao>();
        grv.DataSource = ns;
        grv.DataBind();
    }

    protected void grvOutros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (hfTipoOutros.Value.Equals("emp"))
            {
                OutrosEmpresa oe = OutrosEmpresa.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
                oe.Excluir();
                
                //codigo antigo com checkbox dava erro no servidor online por isso foi trocado
                //foreach (GridViewRow item in grvOutros.Rows)
                //    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                //    {
                //        OutrosEmpresa oe = OutrosEmpresa.ConsultarPorId(grvOutros.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                //        oe.Excluir();
                //}
                grvOutros.DataSource = OutrosEmpresa.ConsultarPorOrgaoEmpresaGrupoEconomico(OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32()),
                    Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()), GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()));
                grvOutros.DataBind();
            }
            else if (hfTipoOutros.Value.Equals("proc"))
            {
                OutrosProcesso op = OutrosProcesso.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
                op.Excluir();
                //Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
                //codigo antigo .. foi alterado para exclusão por btn pois devido algum problema do servidor ele não funcionava mais online
                //foreach (GridViewRow item in grvOutros.Rows)
                //    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                //    {
                //        OutrosProcesso op = OutrosProcesso.ConsultarPorId(grvOutros.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                //        op.Excluir();
                //    }

                grvOutros.DataSource = OutrosProcesso.ConsultarPorProcesso(Processo.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32()));
                grvOutros.DataBind();
            }

            transacao.Recarregar(ref msg);

            if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
            {
                this.VerificarPermissoes();
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

    protected void grvOutros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            this.CarregarStatus(ddlStatusRenovacao);
            hfTipoRenovacao.Value = "OUTROS";
            if (hfTipoOutros.Value.Equals("emp"))
            {
                OutrosEmpresa oe = OutrosEmpresa.ConsultarPorId(grvOutros.DataKeys[e.RowIndex].Value.ToString().ToInt32());
                if (oe != null)
                {
                    hfIdItemRenovacao.Value = oe.Id.ToString();

                    this.ExibirDataOuDias(false);

                    tbxDiasValidadeRenovacao.Text = oe.DiasPrazo.ToString();

                    btnPopUpRenovacao_ModalPopupExtender.Show();
                }
            }
            else if (hfTipoOutros.Value.Equals("proc"))
            {
                OutrosProcesso op = OutrosProcesso.ConsultarPorId(grvOutros.DataKeys[e.RowIndex].Value.ToString().ToInt32());
                if (op != null)
                {
                    hfIdItemRenovacao.Value = op.Id.ToString();

                    this.ExibirDataOuDias(false);

                    tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();

                    btnPopUpRenovacao_ModalPopupExtender.Show();
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    private void ExibirDataOuDias(bool exibirData)
    {
        lblDataDias.Text = exibirData ? "Data de Renovação:" : "Dias de Renovação:";
        tbxDataValidadeRenovacao.Visible = exibirData;
        tbxDiasValidadeRenovacao.Visible = !exibirData;
    }

    protected void grvCondicionantes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Condicionante c = Condicionante.ConsultarPorId(grvCondicionantes.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            ddlLicencaCondicionante.DataSource = Processo.ConsultarPorId(c.Licenca.Processo.Id).Licencas;
            ddlLicencaCondicionante.DataBind();
            ddlLicencaCondicionante.SelectedValue = c.Licenca != null ? c.Licenca.Id.ToString() : "0";
            ddlStatusCondicionante.SelectedValue = c.GetUltimoVencimento.Status != null ? c.GetUltimoVencimento.Status.Id.ToString() : "0";
            tbxNumeroCondicionante.Text = c.Numero;
            tbxDescricaoCondicionante.Text = c.Descricao;
            tbxProtocoloCondicionante.Text = c.GetUltimoVencimento.ProtocoloAtendimento;
            tbxObservacoesCondicionante.Text = c.Observacoes;
            tbxDiasPrazoCondicionante.Text = c.DiasPrazo.ToString();
            tbxDataAtendimentoCondicionante.Text = c.GetUltimoVencimento.DataAtendimento.EmptyToMinValue();
            notificacoesSalvas = c.GetUltimoVencimento.Notificacoes;

            this.arquivosUpload = this.ReconsultarArquivos(c.Arquivos);

            if (c.Vencimentos != null && c.Vencimentos.Count > 0)
            {
                Vencimento v = c.Vencimentos[c.Vencimentos.Count - 1];
                cbxPeriodicaCondicionante.Checked = v.Periodico;
                this.CarregarNotificacoes(v, grvNotificacaoCondicionante);

                lkbProrrogacaoCondicionante.Visible = true;
                lkbProrrogacaoCondicionante.Enabled = true;
                tbxDiasPrazoCondicionante.Enabled = true;
                if (v.ProrrogacoesPrazo != null)
                {
                    lkbProrrogacaoCondicionante.Text = "Abrir Prorrogações - [" + v.ProrrogacoesPrazo.Count + "] Prorrogações.";
                    if (v.ProrrogacoesPrazo.Count > 0)
                        tbxDiasPrazoCondicionante.Enabled = false;
                    else
                        tbxDiasPrazoCondicionante.Enabled = true;
                }
                else
                {
                    lkbProrrogacaoCondicionante.Text = "Abrir Prorrogações";
                    tbxDiasPrazoCondicionante.Enabled = true;
                }
            }

            hfIdCondicionante.Value = c.Id.ToString();
            lkbVencimentoCondicionante.Visible = true;
            lkbObservacoesCondicionante.Visible = true;
            this.ExibirPopUpCondicionante();
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

    protected void grvCondicionantes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //codigo novo apenas botão
            Condicionante c = Condicionante.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            c.Excluir();

            //codigo antigo com checkbox
            //foreach (GridViewRow item in grvCondicionantes.Rows)
            //    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            //    {
            //        Condicionante c = Condicionante.ConsultarPorId(grvCondicionantes.DataKeys[item.RowIndex].Value.ToString().ToInt32());
            //        c.Excluir();
            //    }

            transacao.Fechar(ref msg);
            transacao.Abrir();
            Licenca l = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
            grvCondicionantes.DataSource = l.Condicionantes;
            grvCondicionantes.DataBind();
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

    protected void grvCondicionantes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Condicionante op = Condicionante.ConsultarPorId(grvCondicionantes.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (op != null)
            {
                this.CarregarStatus(ddlStatusRenovacao);

                hfTipoRenovacao.Value = "CONDICIONANTE";
                hfIdItemRenovacao.Value = op.Id.ToString();

                this.ExibirDataOuDias(false);

                tbxDiasValidadeRenovacao.Text = op.DiasPrazo.ToString();

                btnPopUpRenovacao_ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
    }

    //protected void btnEmailConsultoriaCondicionante_Click(object sender, EventArgs e)
    //{
    //    if (ddlLicencaCondicionante.SelectedIndex > -1)
    //    {
    //        Licenca l = Licenca.ConsultarPorId(ddlLicencaCondicionante.SelectedValue.ToInt32());
    //        if (l.Processo.Consultora != null)
    //            if (!tbxOutrosEmails.Text.Contains(l.Processo.Consultora.Contato.Email))
    //                tbxOutrosEmails.Text += l.Processo.Consultora.Contato.Email + ";";
    //    }
    //}

    //protected void btnEmailGrupoEconomicoCondicionate_Click(object sender, EventArgs e)
    //{
    //    if (ddlGrupoEconomicos.SelectedIndex > 0)
    //    {
    //        GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32());
    //        if (c.Contato != null)
    //            if (!tbxOutrosEmails.Text.Contains(c.Contato.Email))
    //                tbxOutrosEmails.Text += c.Contato.Email.IsNotNullOrEmpty() ? c.Contato.Email + ";" : "";
    //    }
    //}

    //protected void btnEmailEmpresaCondicionante_Click(object sender, EventArgs e)
    //{
    //    if (ddlEmpresa.SelectedIndex > 0)
    //    {
    //        Empresa em = Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32());
    //        if (em.Contato != null)
    //            if (!tbxOutrosEmails.Text.Contains(em.Contato.Email))
    //                tbxOutrosEmails.Text += em.Contato.Email.IsNotNullOrEmpty() ? em.Contato.Email + ";" : "";
    //    }
    //    else
    //    {
    //        if (objetoUtilizado != null)
    //            if (objetoUtilizado.GetType() == typeof(Condicionante))
    //            {
    //                Condicionante c1 = Condicionante.ConsultarPorId(((Condicionante)objetoUtilizado).Id);
    //                string email = c1.Licenca != null ? c1.Licenca.Processo != null ? c1.Licenca.Processo.Empresa != null ? c1.Licenca.Processo.Empresa.Contato != null ? c1.Licenca.Processo.Empresa.Contato.Email : "" : "" : "" : "";
    //                if (!tbxOutrosEmails.Text.Contains(email) && email.IsNotNullOrEmpty())
    //                    tbxOutrosEmails.Text += email + ";";
    //            }
    //            else if (objetoUtilizado.GetType() == typeof(Licenca))
    //            {
    //                Licenca l1 = Licenca.ConsultarPorId(((Licenca)objetoUtilizado).Id);
    //                string email = l1.Processo != null ? l1.Processo.Empresa != null ? l1.Processo.Empresa.Contato != null ? l1.Processo.Empresa.Contato.Email : "" : "" : "";
    //                if (!tbxOutrosEmails.Text.Contains(email) && email.IsNotNullOrEmpty())
    //                    tbxOutrosEmails.Text += email + ";";
    //            }
    //            else if (objetoUtilizado.GetType() == typeof(OutrosEmpresa))
    //            {
    //                OutrosEmpresa oe = OutrosEmpresa.ConsultarPorId(((OutrosEmpresa)objetoUtilizado).Id);
    //                string email = oe.Empresa != null ? oe.Empresa.Contato != null ? oe.Empresa.Contato.Email : "" : "";
    //                if (!tbxOutrosEmails.Text.Contains(email) && email.IsNotNullOrEmpty())
    //                    tbxOutrosEmails.Text += email + ";";
    //            }
    //            else if (objetoUtilizado.GetType() == typeof(OutrosProcesso))
    //            {
    //                OutrosProcesso op = OutrosProcesso.ConsultarPorId(((OutrosProcesso)objetoUtilizado).Id);
    //                string email = op.Processo != null ? op.Processo.Empresa != null ? op.Processo.Empresa.Contato != null ? op.Processo.Empresa.Contato.Email : "" : "" : "";
    //                if (!tbxOutrosEmails.Text.Contains(email) && email.IsNotNullOrEmpty())
    //                    tbxOutrosEmails.Text += email + ";";
    //            }
    //    }
    //}

    protected void lkbCondicionantesPadroes_Click(object sender, EventArgs e)
    {
        try
        {
            this.ImportarCondicionantes();
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

    protected void lkbCondicionantesPadroes_Load(object sender, EventArgs e)
    {
        Licenca l = Licenca.ConsultarPorId(HfIdLicenca.Value.ToInt32());
        if (l != null)
            WebUtil.AdicionarConfirmacao((LinkButton)sender, "Deseja adicionar todas as condicionantes padrões de um(a) " + l.TipoLicenca.Nome + "?");
    }

    protected void ibtnAddNotificacaoCondicionante_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objetoUtilizado = new Condicionante();
            Licenca li = Licenca.ConsultarPorId(trvProcessos.SelectedValue.Split('_')[1].ToInt32());
            Condicionante cond = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);

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

    protected void btnSalvarNotificacao_Click(object sender, EventArgs e)
    {
        try
        {
            if (tbxOutrosEmails.Text.Trim().Replace(" ", "") != "")
            {
                if (!WebUtil.ValidarEmailInformado(tbxOutrosEmails.Text))
                {
                    msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }

            this.SalvarNotificacao();
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

    protected void ibtnAddNotificacaoLicenca_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objetoUtilizado = new Licenca();
            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnAddNotificacaoOutros_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (rbtnGeralOutros.Checked)
            {
                objetoUtilizado = new OutrosEmpresa();
            }
            if (rbtnProcessoOutros.Checked)
            {
                objetoUtilizado = new OutrosProcesso();
            }

            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnAdicionarCondicionanteLicenca_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "L")
            {
                WebUtil.LimparCampos(upCamposCondicionante.Controls[0].Controls);
                arquivosUpload = null;
                hfIdCondicionante.Value = "0";
                Licenca l = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());

                this.notificacoesSalvas = new List<Notificacao>();
                grvNotificacaoCondicionante.DataSource = notificacoesSalvas;
                grvNotificacaoCondicionante.DataBind();

                ddlLicencaCondicionante.DataSource = l.Processo.Licencas;
                ddlLicencaCondicionante.DataBind();
                ddlLicencaCondicionante.SelectedValue = l.Id.ToString();

                lkbVencimentoCondicionante.Visible = false;
                lkbObservacoesCondicionante.Visible = false;

                lkbProrrogacaoCondicionante.Visible = false;
                tbxDiasPrazoCondicionante.Enabled = true;

                this.ExibirPopUpCondicionante();
            }
            else
            {
                msg.CriarMensagem("Selecione uma Licença na árvore abaixo.", "ALERTA", MsgIcons.Alerta);
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

    protected void ddlTipoOrgaoProcesso_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlCidadeProcesso.Items.Clear();
        ddlCidadeProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));

        ddlOrgaoProcesso.Items.Clear();
        ddlOrgaoProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));

        ddlEstadoProcesso.SelectedIndex = 0;

        if (ddlTipoOrgaoProcesso.SelectedIndex == 0)
        {
            divEstadoProcesso.Visible = true;
            divCidadeProcesso.Visible = false;
        }
        else if (ddlTipoOrgaoProcesso.SelectedIndex == 1)
        {
            divEstadoProcesso.Visible = false;
            divCidadeProcesso.Visible = false;
            this.CarregarOrgaosAmbientaisPOPUP(OrgaoFederal.ConsultarTodos());
        }
        else if (ddlTipoOrgaoProcesso.SelectedIndex == 2)
        {
            divEstadoProcesso.Visible = true;
            divCidadeProcesso.Visible = true;
        }
    }

    protected void btnSalvarCTF_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarCTF();
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

    protected void ibtnAddNotificacaoPagamentoCTF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objetoUtilizado = new CadastroTecnicoFederal();
            hfTipoVencimentoCTF.Value = "Pagamento";
            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnAddNotificacaoRelatorioCTF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objetoUtilizado = new CadastroTecnicoFederal();
            hfTipoVencimentoCTF.Value = "Relatorio";
            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);
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

    protected void ibtnAddNotificacaoCertificadoCTF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objetoUtilizado = new CadastroTecnicoFederal();
            hfTipoVencimentoCTF.Value = "Certificado";
            this.CarregarPopUpNotificacao(true, 5, 10, 15, 30, 60, 90, 120, 180, 240, 300);

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

    protected void lkbOpcoesNovaCTF_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomicos.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione um Grupo Econômico.", "Alerta", MsgIcons.Alerta);
                return;
            }
            this.arquivosUpload = null;
            WebUtil.LimparCampos(upPopCadastroTecnicoFederal.Controls[0].Controls);
            HfIdCTF.Value = "0";
            this.CarregarConsultoriasProcesso();
            this.CarregarStatus(ddlEstatusCertificado);
            this.CarregarStatus(ddlEstatusRelatorioAnual);
            this.CarregarStatus(ddlEstatusTaxaTrimestral);
            lkbVencimentoRelatorioAnual.Visible = false;
            lkbVencimentoCertificadoRegularidade.Visible = false;
            lkbVencimentoTaxaTrimestral.Visible = false;
            grvNotificacaoCertificadoCTF.DataSource = grvNotificacaoRelatorioCTF.DataSource = grvNotificacaoPagamentoCTF.DataSource = new List<Notificacao>();
            grvNotificacaoPagamentoCTF.DataBind();
            grvNotificacaoRelatorioCTF.DataBind();
            grvNotificacaoCertificadoCTF.DataBind();
            tbxDataEntregaCertificadoCTF.Text = tbxDataEntregaRelatorioCTF.Text = tbxDataPagamentoCTF.Text = "";
            this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaCTF);
            objetoCTF = new CadastroTecnicoFederal();
            tbxDataEntregaRelatorioCTF.Text = new DateTime(DateTime.Now.Year + 1, 3, 31).ToShortDateString();
            this.ExibirPopUpCTF();
            lkbObservacoes.Visible = false;
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

    protected void lkbOpcoesEditarCTF_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "C")
            {
                this.CarregarStatus(ddlEstatusRelatorioAnual);
                this.CarregarStatus(ddlEstatusCertificado);
                this.CarregarStatus(ddlEstatusTaxaTrimestral);
                this.CarregarConsultoriasProcesso();

                CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                //CARREGAR DADOS NO POPUP DE EDIÇÃO

                this.objetoCTF = c;
                this.CarregarEmpresasQueOUsuarioEdita(ddlEmpresaCTF);

                ddlEmpresaCTF.SelectedValue = c.Empresa != null ? c.Empresa.Id.ToString() : "0";
                tbxSenhaCTF.Text = c.Senha;
                tbxAtividadesCTF.Text = c.Atividade;
                tbxNumeroLicencaCTF.Text = c.NumeroLicenca;
                tbxDataValidadeLicenca.Text = c.ValidadeLicenca.EmptyToMinValue();
                tbxObservacaoCTF.Text = c.Observacoes;

                if (c.Consultora != null)
                    ddlConsultoria.SelectedValue = c.Consultora.Id.ToString();
                else
                    ddlConsultoria.SelectedValue = "0";


                this.arquivosUpload = this.ReconsultarArquivos(c.Arquivos);

                Vencimento rel = c.GetUltimoRelatorio;
                if (rel != null)
                {
                    ddlEstatusRelatorioAnual.SelectedValue = rel.Status != null ? rel.Status.Id.ToString() : "1";
                    tbxDataEntregaRelatorioCTF.Text = rel.Data.EmptyToMinValue();
                    grvNotificacaoRelatorioCTF.DataSource = rel.Notificacoes;
                    grvNotificacaoRelatorioCTF.DataBind();
                    lkbVencimentoRelatorioAnual.Visible = tbxDataEntregaRelatorioCTF.Text.Trim() != "";
                }

                Vencimento pag = c.GetUltimoPagamento;
                if (pag != null)
                {
                    ddlEstatusTaxaTrimestral.SelectedValue = pag.Status != null ? pag.Status.Id.ToString() : "1";
                    tbxDataPagamentoCTF.Text = pag.Data.EmptyToMinValue();
                    grvNotificacaoPagamentoCTF.DataSource = pag.Notificacoes;
                    grvNotificacaoPagamentoCTF.DataBind();
                    lkbVencimentoTaxaTrimestral.Visible = tbxDataPagamentoCTF.Text.Trim() != "";
                }

                Vencimento cer = c.GetUltimoCertificado;
                if (cer != null)
                {
                    ddlEstatusCertificado.SelectedValue = cer.Status != null ? cer.Status.Id.ToString() : ddlEstatusCertificado.SelectedValue;
                    tbxDataEntregaCertificadoCTF.Text = cer.Data.EmptyToMinValue();
                    grvNotificacaoCertificadoCTF.DataSource = cer.Notificacoes;
                    grvNotificacaoCertificadoCTF.DataBind();
                    lkbVencimentoCertificadoRegularidade.Visible = tbxDataEntregaCertificadoCTF.Text.Trim() != "";
                }

                lkbObservacoes.Visible = true;
                HfIdCTF.Value = c.Id.ToString();
                objetoCTF = c;
                this.ExibirPopUpCTF();
            }
            else
            {
                msg.CriarMensagem("Selecione um Cadastro Técnico Federal na Árvore abaixo!", "Alerta", MsgIcons.Alerta);
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

    protected void lkbOpcoesExcluirCTF_Click(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "C")
            {
                CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
                bool x = c.Excluir();
                if (x)
                {
                    msg.CriarMensagem("Cadastro Técnico Federal excluido com sucesso.", "Sucesso", MsgIcons.Sucesso);
                    transacao.Recarregar(ref msg);

                    if (this.ConfiguracaoModuloMeioAmbiente != null && this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO) 
                    {
                        this.VerificarPermissoes();
                    }                    
                    this.CarregarArvore2();
                    this.CarregarOrgaos(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicos.SelectedValue.ToInt32()), Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()));
                    mvwProcessos.ActiveViewIndex = -1;
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um Cadastro Técnico Federal na Árvore abaixo!", "Alerta", MsgIcons.Alerta);
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

    protected void ddlEstadoLicenca_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOrgaoProcesso.Items.Clear();
        ddlOrgaoProcesso.Items.Insert(0, new ListItem("-- Selecione --", "0"));

        if (ddlTipoOrgaoProcesso.SelectedIndex == 0)
        {
            this.CarregarOrgaosAmbientaisPOPUP(OrgaoEstadual.ConsultarPorEstado(ddlEstadoProcesso.SelectedValue.ToInt32()));
        }
        else if (ddlTipoOrgaoProcesso.SelectedIndex == 2)
        {
            this.CarregarCidade(ddlEstadoProcesso.SelectedValue.ToInt32());
        }

    }

    protected void ddlCidadeProcesso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCidadeProcesso.SelectedIndex > 0)
            this.CarregarOrgaosAmbientaisPOPUP(OrgaoMunicipal.ConsultarPorCidade(ddlCidadeProcesso.SelectedValue.ToInt32()));
    }

    protected void ddlLicencaCadastroEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidadeCadastroLicenca();
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

    protected void lkbOpcoesOutrosNovo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CarregarOutrosPOPUP();
            WebUtil.LimparCampos(upPopUpOutros.Controls[0].Controls);
            rbtnGeralOutros.Enabled = rbtnProcessoOutros.Enabled = true;
            tbxDiasValidadeOutros.Enabled = true;
            hfIdOutros.Value = "";
            ddlEmpresaGeralOutros.SelectedIndex = 0;
            arquivosUpload = null;
            lkbProrrogacaoOutros.Visible = false;

            rbtnGeralOutros.Checked = true;
            this.notificacoesSalvas = new List<Notificacao>();
            grvNotificacaoOutros.DataSource = this.notificacoesSalvas;
            grvNotificacaoOutros.DataBind();

            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "O" ||
                trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "H")
            {
                rbtnGeralOutros.Checked = true;
                rbtnProcessoOutros.Checked = false;
                this.RadioChange();
                if (hfIdEmpresa.Value.ToInt32() > 0)
                    ddlEmpresaGeralOutros.SelectedValue = hfIdEmpresa.Value;
                if (hfIdOrgao.Value.ToInt32() > 0)
                    ddlOrgaoGeralOutros.SelectedValue = hfIdOrgao.Value;

                if (ddlEmpresaGeralOutros.SelectedIndex > 0)
                {
                    btnUploadOutros.Visible = true;
                    WebUtil.AdicionarEventoShowModalDialog(btnUploadOutros, "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros(
                        "idEmpresa=" + ddlEmpresaGeralOutros.SelectedValue + "&idCliente=" + ddlGrupoEconomicos.SelectedValue + "&tipo=Outros"),
                        "Upload Arquivo Processo DNPM", 550, 420);
                }
                else
                    btnUploadOutros.Visible = false;
            }
            else if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "S")
            {
                rbtnProcessoOutros.Checked = true;
                rbtnGeralOutros.Checked = false;
                this.RadioChange();
                if (hfIdEmpresa.Value.ToInt32() > 0)
                {
                    ddlEmpresaOutros.SelectedValue = hfIdEmpresa.Value;
                    this.CarregarOrgaosAmbientaisOutros();
                    if (hfIdOrgao.Value.ToInt32() > 0)
                        ddlOrgaoOutros.SelectedValue = hfIdOrgao.Value;
                }
                this.SetarProcessoOutros();

                btnUploadOutros.Visible = false;
            }

            lkbObservacoesOutros.Visible = false;
            lkbVencimentoOutros.Visible = false;
            grvNotificacaoOutros.DataSource = new List<Notificacao>();
            grvNotificacaoOutros.DataBind();
            this.ExibirPopUpOutros();
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

    protected void btnRenovarRelatorio_Click(object sender, EventArgs e)
    {
        hfTipoRenovacao.Value = "RELATORIO";
        hfIdItemRenovacao.Value = trvProcessos.SelectedNode.Value.Split('_')[1].ToString();
        CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToString().ToInt32());
        tbxDataValidadeRenovacao.Text = c.GetUltimoRelatorio.Data.AddYears(1).EmptyToMinValue();
        this.CarregarStatus(ddlStatusRenovacao);
        this.ExibirDataOuDias(true);
        btnPopUpRenovacao_ModalPopupExtender.Show();
    }

    protected void btnRenovarTaxa_Click(object sender, EventArgs e)
    {
        hfTipoRenovacao.Value = "TAXA";
        hfIdItemRenovacao.Value = trvProcessos.SelectedNode.Value.Split('_')[1].ToString();
        tbxDataValidadeRenovacao.Text = "";
        this.CarregarStatus(ddlStatusRenovacao);
        this.ExibirDataOuDias(true);
        btnPopUpRenovacao_ModalPopupExtender.Show();
    }

    protected void btnRenovarCertificado_Click(object sender, EventArgs e)
    {
        hfTipoRenovacao.Value = "CERTIFICADO";
        hfIdItemRenovacao.Value = trvProcessos.SelectedNode.Value.Split('_')[1].ToString();
        tbxDataValidadeRenovacao.Text = "";
        this.CarregarStatus(ddlStatusRenovacao);
        this.ExibirDataOuDias(true);
        btnPopUpRenovacao_ModalPopupExtender.Show();
    }

    protected void grvNotificacaoRelatorioCTF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoRelatorio.Notificacoes);
            g.DataBind();
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

    protected void grvNotificacaoPagamentoCTF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoPagamento.Notificacoes);
            g.DataBind();
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

    protected void grvNotificacaoCertificadoCTF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = this.ReconsultarNotificacoes(this.objetoCTF.GetUltimoCertificado.Notificacoes);
            g.DataBind();
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

    protected void grvNotificacaoOutros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
            g.DataBind();
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

    protected void grvNotificacaoLicenca_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
            g.DataBind();
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

    protected void grvNotificacaoCondicionante_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = this.ReconsultarNotificacoes(this.notificacoesSalvas);
            g.DataBind();
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

    protected void grvNotificacaoCertificadoCTF_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IList<Notificacao> excluir = this.objetoCTF.GetUltimoCertificado.Notificacoes;
            foreach (GridViewRow item in ((GridView)sender).Rows)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (c != null)
                    {
                        excluir.Remove(c);
                        c.Excluir();
                    }
                }
            this.objetoCTF.GetUltimoCertificado.Notificacoes = excluir;
            ((GridView)sender).DataSource = excluir;
            ((GridView)sender).DataBind();
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

    protected void grvNotificacaoPagamentoCTF_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IList<Notificacao> excluir = this.objetoCTF.GetUltimoPagamento.Notificacoes;
            foreach (GridViewRow item in ((GridView)sender).Rows)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    if (c != null)
                    {
                        excluir.Remove(c);
                        c.Excluir();
                    }
                }
            this.objetoCTF.GetUltimoPagamento.Notificacoes = excluir;
            ((GridView)sender).DataSource = excluir;
            ((GridView)sender).DataBind();
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

    protected void grvNotificacaoRelatorioCTF_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IList<Notificacao> excluir = this.objetoCTF.GetUltimoRelatorio.Notificacoes;

            Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (c != null)
            {
                excluir.Remove(c);
                c.Excluir();
            }

            this.objetoCTF.GetUltimoRelatorio.Notificacoes = excluir;
            ((GridView)sender).DataSource = excluir;
            ((GridView)sender).DataBind();
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

    protected void grvNotificacaoOutros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IList<Notificacao> excluir = this.notificacoesSalvas;
            
            //novo
            Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (c != null) {
                if (excluir != null && excluir.Count > 0) {
                    excluir.Remove(c);
                    c.Excluir();
                }
                        
            }

            this.notificacoesSalvas = excluir;
            ((GridView)sender).DataSource = excluir;
            ((GridView)sender).DataBind();


            //antigo
            //foreach (GridViewRow item in ((GridView)sender).Rows)
            //    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            //    {
            //        Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[item.RowIndex].Value.ToString().ToInt32());
            //        if (c != null)
            //        {
            //            excluir.Remove(c);
            //            c.Excluir();
            //        }
            //    }
            //this.notificacoesSalvas = excluir;
            //((GridView)sender).DataSource = excluir;
            //((GridView)sender).DataBind();
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

    protected void grvNotificacaoLicenca_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IList<Notificacao> excluir = this.notificacoesSalvas;

            Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (c != null)
            {
                excluir.Remove(c);
                c.Excluir();
            }

            this.notificacoesSalvas = excluir;
            ((GridView)sender).DataSource = excluir;
            ((GridView)sender).DataBind();
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

    protected void grvNotificacaoCondicionante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IList<Notificacao> excluir = this.ReconsultarNotificacoes(this.notificacoesSalvas);
            Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (c != null)
            {
                excluir.Remove(c);
                c.Excluir();
            }

            //codigo antigo. dava erro no servidor online utilizava checkbox para selecionar e excluir..
            //foreach (GridViewRow item in ((GridView)sender).Rows)
            //    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            //    {
            //        Notificacao c = Notificacao.ConsultarPorId(((GridView)sender).DataKeys[item.RowIndex].Value.ToString().ToInt32());
            //        if (c != null)
            //        {
            //            excluir.Remove(c);
            //            c.Excluir();
            //        }
            //    }
            notificacoesSalvas = excluir;
            ((GridView)sender).DataSource = excluir;
            ((GridView)sender).DataBind();
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

    protected void grvCondicionantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            g.DataSource = Licenca.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32()).Condicionantes;
            g.DataBind();
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

    protected void grvOutros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "O")
            {
                grvOutros.PageIndex = e.NewPageIndex;
                grvOutros.DataSource = OutrosEmpresa.ConsultarPorOrgaoEmpresaGrupoEconomico(OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32()), Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32()), GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32()));
                grvOutros.DataBind();
            }
            else if (trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "S")
            {
                Processo p = Processo.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32());
                grvOutros.PageIndex = e.NewPageIndex;
                grvOutros.DataSource = OutrosProcesso.ConsultarPorProcesso(p);
                grvOutros.DataBind();
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

    protected void ddlEmpresaCTF_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.AdicionarEventoPopUpCTF();
    }

    protected void ddlEmpresaGeralOutros_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.AdicionarEventoPopUpOutros();
        btnUploadOutros.Visible = ddlEmpresaGeralOutros.SelectedIndex > 0;
    }

    protected void dtlMunicipal_EditCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            hfIdOrgao.Value = dtlMunicipal.DataKeys[e.Item.ItemIndex].ToString();
            hfTipoProcesso.Value = "1";
            trvProcessos.Nodes.Clear();
            lblNomeOrgao.Text = ":: Processos Municipais";
            GrupoEconomico cli = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());
            Empresa emp = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());

            OrgaoMunicipal om = OrgaoMunicipal.ConsultarPorId(hfIdOrgao.Value.ToInt32());

            IList<Processo> processos = Processo.ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(om, emp, cli, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente);
            if (processos != null && processos.Count > 0)
            {
                TreeNode noPai = new TreeNode("<b>Órgão:</b> " + om.Nome + " " + om.Cidade.Nome + " - " + om.Cidade.Estado.PegarSiglaEstado(), "n_" + om.Id);
                if (processos != null)
                    foreach (Processo p in processos)
                    {
                        TreeNode noProc = new TreeNode("<b>Processo:</b> " + p.Numero, "p_" + p.Id);
                        if (p.Licencas != null)
                        {
                            foreach (Licenca l in p.Licencas)
                            {
                                TreeNode noLic = new TreeNode("<b>" + (l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + ":</b> " + l.Numero, "l_" + l.Id);
                                noProc.ChildNodes.Add(noLic);
                            }
                            noProc.ChildNodes.Add(new TreeNode("<b>Outros [" + p.OutrosProcessos.Count + " iten(s)]</b>", "s_1"));
                            noPai.ChildNodes.Add(noProc);
                        }
                    }
                trvProcessos.Nodes.Add(noPai);
                trvProcessos.ExpandAll();
            }
            mvwProcessos.ActiveViewIndex = -1;

            divOpcoesCadastro.Visible = this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
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

    protected void dtlEstadual_EditCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            hfIdOrgao.Value = dtlEstadual.DataKeys[e.Item.ItemIndex].ToString();
            hfTipoProcesso.Value = "2";
            trvProcessos.Nodes.Clear();
            lblNomeOrgao.Text = ":: Processos Estaduais";
            GrupoEconomico cli = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());
            Empresa emp = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());

            OrgaoEstadual om = OrgaoEstadual.ConsultarPorId(hfIdOrgao.Value.ToInt32());

            IList<Processo> processos = Processo.ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(om, emp, cli, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente);
            if (processos != null && processos.Count > 0)
            {
                TreeNode noPai = new TreeNode("<b>Órgão:</b> " + om.Nome, "n_" + om.Id);
                int contador = 0;
                if (emp != null)
                    contador = om.OutrosEmpresas.Where(o => o.Empresa == emp).ToList().Count;
                else
                    contador = om.ObterQuantidadeDeOutrosEmpresaDoOrgaoEGrupoEconomicoQueOUsuarioPossuiAcesso(this.UsuarioLogado, cli.Id, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);

                noPai.ChildNodes.Add(new TreeNode("<b>Outros [" + contador + " iten(s)]</b>", "o_1"));
                foreach (Processo p in processos)
                {
                    TreeNode noProc = new TreeNode("<b>Processo:</b> " + p.Numero, "p_" + p.Id);
                    if (p.Licencas != null)
                        foreach (Licenca l in p.Licencas)
                        {
                            TreeNode noLicenca = new TreeNode("<b>" + (l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + ":</b> " + l.Numero, "l_" + l.Id);
                            noProc.ChildNodes.Add(noLicenca);
                        }
                    noProc.ChildNodes.Add(new TreeNode("<b>Outros [" + p.OutrosProcessos.Count + " iten(s)]</b>", "s_1"));
                    noPai.ChildNodes.Add(noProc);
                }

                trvProcessos.Nodes.Add(noPai);
                trvProcessos.ExpandAll();
            }
            mvwProcessos.ActiveViewIndex = -1;

            divOpcoesCadastro.Visible = this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
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

    protected void dtlFederal_EditCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            hfIdOrgao.Value = dtlFederal.DataKeys[e.Item.ItemIndex].ToString();
            hfTipoProcesso.Value = "3";
            trvProcessos.Nodes.Clear();
            lblNomeOrgao.Text = ":: Processos Federais";
            GrupoEconomico cli = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());
            Empresa emp = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());

            OrgaoFederal om = OrgaoFederal.ConsultarPorId(hfIdOrgao.Value.ToInt32());

            IList<Processo> processos = Processo.ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(om, emp, cli, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.ProcessosPermissaoModuloMeioAmbiente);
            if (processos != null && processos.Count > 0)
            {
                TreeNode noPai = new TreeNode("<b>Órgão:</b> " + om.Nome, "n_" + om.Id);
                int contador = 0;
                if (emp != null)
                    contador = om.OutrosEmpresas.Where(o => o.Empresa == emp).ToList().Count;
                else
                    contador = om.ObterQuantidadeDeOutrosEmpresaDoOrgaoEGrupoEconomicoQueOUsuarioPossuiAcesso(this.UsuarioLogado, cli.Id, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);

                noPai.ChildNodes.Add(new TreeNode("<b>Outros [" + contador + " iten(s)]</b>", "o_1"));
                foreach (Processo p in processos)
                {
                    TreeNode noProc = new TreeNode("<b>Processo:</b> " + p.Numero, "p_" + p.Id);
                    if (p.Licencas != null)
                        foreach (Licenca l in p.Licencas)
                        {
                            TreeNode noLicenca = new TreeNode("<b>" + (l.TipoLicenca != null ? l.TipoLicenca.Sigla : "Licença") + ":</b> " + l.Numero, "l_" + l.Id);
                            noProc.ChildNodes.Add(noLicenca);
                        }
                    noProc.ChildNodes.Add(new TreeNode("<b>Outros [" + p.OutrosProcessos.Count + " iten(s)]</b>", "s_1"));
                    noPai.ChildNodes.Add(noProc);
                }

                trvProcessos.Nodes.Add(noPai);
                trvProcessos.ExpandAll();
            }
            mvwProcessos.ActiveViewIndex = -1;

            divOpcoesCadastro.Visible = this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
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

    protected void dtlCTF_EditCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            this.CarregarArvoreCadastroTecnicoFederal();
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

    protected void dtlOutros_EditCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            hfTipoProcesso.Value = "5";
            GrupoEconomico cli = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());
            Empresa emp = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());
            lblNomeOrgao.Text = ":: Outros";
            trvProcessos.Nodes.Clear();
            IList<OrgaoAmbiental> orgaos = OrgaoAmbiental.ConsultarComOutros(emp, cli, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);
            foreach (OrgaoAmbiental om in orgaos)
            {
                int contador = 0;
                if (emp != null)
                    contador = om.OutrosEmpresas.Where(o => o.Empresa == emp).ToList().Count;
                else
                    contador = om.ObterQuantidadeDeOutrosEmpresaDoOrgaoEGrupoEconomicoQueOUsuarioPossuiAcesso(this.UsuarioLogado, cli.Id, this.ConfiguracaoModuloMeioAmbiente.Tipo, this.EmpresasPermissaoModuloMeioAmbiente, this.OutrosEmpresasPermissaoModuloMeioAmbiente);

                TreeNode noPai = new TreeNode("<b>Órgão: " + om.Nome + " [" + contador + " iten(s)]</b>", "h_" + om.Id);
                trvProcessos.Nodes.Add(noPai);
            }
            trvProcessos.ExpandAll();
            mvwProcessos.ActiveViewIndex = -1;

            divOpcoesCadastro.Visible = this.ConfiguracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
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

    protected void lkbOpcoesOutrosNovo_Click1(object sender, EventArgs e)
    {
        try
        {
            if (trvProcessos.SelectedNode != null && trvProcessos.SelectedNode.Value.Split('_')[0].ToUpper() == "S")
            {
                msg.CriarMensagem("Este vencimento será associado à uma empresa e não a um Processo portanto selecione abaixo o outros fora do Processo!", "Alerta", MsgIcons.Alerta);
            }
            else if (ddlGrupoEconomicos.SelectedIndex > 0)
            {
                tbxDiasValidadeOutros.Enabled = true;
                lkbProrrogacaoOutros.Visible = false;
                this.CarregarOutrosPOPUP();
                WebUtil.LimparCampos(upPopUpOutros.Controls[0].Controls);
                rbtnGeralOutros.Enabled = rbtnProcessoOutros.Enabled = true;
                hfIdOutros.Value = "";
                ddlEmpresaGeralOutros.SelectedIndex = 0;
                arquivosUpload = null;

                rbtnGeralOutros.Checked = true;
                this.notificacoesSalvas = new List<Notificacao>();
                grvNotificacaoOutros.DataSource = this.notificacoesSalvas;
                grvNotificacaoOutros.DataBind();

                rbtnGeralOutros.Checked = true;
                rbtnProcessoOutros.Checked = false;
                this.RadioChange();
                if (hfIdEmpresa.Value.ToInt32() > 0)
                    ddlEmpresaGeralOutros.SelectedValue = hfIdEmpresa.Value;
                if (hfIdOrgao.Value.ToInt32() > 0)
                    ddlOrgaoGeralOutros.SelectedValue = hfIdOrgao.Value;

                lkbObservacoesOutros.Visible = false;
                grvNotificacaoOutros.DataSource = new List<Notificacao>();
                grvNotificacaoOutros.DataBind();

                if (ddlEmpresaGeralOutros.SelectedIndex > 0)
                {
                    btnUploadOutros.Visible = true;
                    WebUtil.AdicionarEventoShowModalDialog(btnUploadOutros, "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros(
                        "idEmpresa=" + ddlEmpresaGeralOutros.SelectedValue + "&idCliente=" + ddlGrupoEconomicos.SelectedValue + "&tipo=Outros"),
                        "Upload Arquivo Processo DNPM", 550, 420);
                }
                else
                    btnUploadOutros.Visible = false;
                lkbVencimentoOutros.Visible = false;
                this.ExibirPopUpOutros();
            }
            else
            {
                msg.CriarMensagem("Selecione um Grupo Econômico primeiro!", "Alerta", MsgIcons.Alerta);
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

    protected void lkbVencimentoRelatorioAnual_Click(object sender, EventArgs e)
    {
        try
        {
            CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
            this.CarregarVencimentos(cad.EntregaRelatorioAnual, "EntregaRelatorioAnual", cad.Id);

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

    protected void lkbVencimentoTaxaTrimestral_Click(object sender, EventArgs e)
    {
        try
        {
            CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
            this.CarregarVencimentos(cad.TaxaTrimestral, "TaxaTrimestral", cad.Id);

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

    protected void lkbVencimentoCertificadoRegularidade_Click(object sender, EventArgs e)
    {
        try
        {
            CadastroTecnicoFederal cad = CadastroTecnicoFederal.ConsultarPorId(HfIdCTF.Value.ToInt32());
            this.CarregarVencimentos(cad.CertificadoRegularidade, "CertificadoRegularidade", cad.Id);

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

    protected void btnAddProrrogacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarProrrogacao();
            tbxPrazoAdicional.Text = "";
            tbxDataProtocoloAdd.Text = "";
            tbxProtocoloAdicional.Text = "";
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

    protected void grvProrrogacoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Condicional condi = new Condicional();

            if (hfTipoProrrogacao.Value == "CONDICIONANTE")
            {
                condi = new Condicionante();
                condi = Condicionante.ConsultarPorId(hfIdCondicionante.Value.ToInt32());

            }
            else if (hfTipoProrrogacao.Value == "OUTROS")
                condi = Condicional.ConsultarPorId(hfIdOutros.Value.ToInt32());


            ProrrogacaoPrazo p = ProrrogacaoPrazo.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (p != null)
            {
                if (condi != null && condi.GetUltimoVencimento != null && condi.GetUltimoVencimento.ProrrogacoesPrazo != null)
                {
                    for (int i = condi.GetUltimoVencimento.ProrrogacoesPrazo.Count - 1; i > -1; i--)
                    {
                        if (condi.GetUltimoVencimento.ProrrogacoesPrazo[i].Id == p.Id)
                            condi.GetUltimoVencimento.ProrrogacoesPrazo.Remove(condi.GetUltimoVencimento.ProrrogacoesPrazo[i]);
                    }
                }
                p.Excluir();
            }

            this.VerificarNovaDataVencimentoComExclusaoDeProrrogacao(condi);
            this.CarregarProrrogacoes(hfTipoProrrogacao.Value);
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

    protected void ddlStatusCondicionante_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatusCondicionante.SelectedItem.Text == "Pedido de Prazo" && hfIdCondicionante.Value.ToInt32() > 0)
            {
                tbxPrazoAdicional.Text = "";
                tbxDataProtocoloAdd.Text = "";
                tbxProtocoloAdicional.Text = "";
                this.CarregarProrrogacoes("CONDICIONANTE");
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

    protected void ddlStatusOutros_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatusOutros.SelectedItem.Text == "Pedido de Prazo" && hfIdOutros.Value.ToInt32() > 0)
            {
                tbxPrazoAdicional.Text = "";
                tbxDataProtocoloAdd.Text = "";
                tbxProtocoloAdicional.Text = "";
                this.CarregarProrrogacoes("OUTROS");
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

    protected void btnAbrirContratos_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarContratos();
            lblAbrirContratos_popupextender.Show();
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

    protected void gvContratos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Processo processo = Processo.ConsultarPorId(HfIdProcesso.Value.ToInt32());
        gvContratos.PageIndex = e.NewPageIndex;
        gvContratos.DataSource = processo.ContratosDiversos;
        gvContratos.DataBind();
    }

    protected void gvContratos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Processo processo = Processo.ConsultarPorId(HfIdProcesso.Value.ToInt32());

            ContratoDiverso cont = ContratoDiverso.ConsultarPorId(((GridView)sender).DataKeys[e.RowIndex].Value.ToString().ToInt32());
            processo.ContratosDiversos.Remove(cont);
            msg.CriarMensagem("Contrato(s) excluído(s) com sucesso", "Sucesso", MsgIcons.Sucesso);

            //codigo inutilizado utilizava checkbox agora usa btn
            //foreach (GridViewRow item in gvContratos.Rows)
            //{
            //    if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            //    {
            //        ContratoDiverso cont = ContratoDiverso.ConsultarPorId(gvContratos.DataKeys[item.RowIndex].Value.ToString().ToInt32());
            //        processo.ContratosDiversos.Remove(cont);
            //        msg.CriarMensagem("Contrato(s) excluído(s) com sucesso", "Sucesso", MsgIcons.Sucesso);
            //    }
            //}

            processo = processo.Salvar();
            this.CarregarContratos();
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

    protected void btnSelecionarMaisContratos_Click(object sender, EventArgs e)
    {
        try
        {
            tbxObjetoContratoDiverso.Text = "";
            tbxNumeroContratoDiverso.Text = "";
            lblAbrirSelecaoContratos_popupextender.Show();
            this.PesquisarContratosDiversos();
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

    protected void gdvContratosSelecao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvContratosSelecao.PageIndex = e.NewPageIndex;
        gdvContratosSelecao.DataSource = this.ContratosConsultados;
        gdvContratosSelecao.DataBind();
    }

    protected void lxbPesquisarContratos_Click(object sender, EventArgs e)
    {
        try
        {
            this.PesquisarContratosDiversos();
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

    protected void btnSalvarContratoDiverso_Click(object sender, EventArgs e)
    {
        try
        {
            Processo processo = Processo.ConsultarPorId(HfIdProcesso.Value.ToInt32());
            if (processo.ContratosDiversos == null)
                processo.ContratosDiversos = new List<ContratoDiverso>();

            foreach (GridViewRow item in gdvContratosSelecao.Rows)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    ContratoDiverso cont = ContratoDiverso.ConsultarPorId(gdvContratosSelecao.DataKeys[item.RowIndex].Value.ToString().ToInt32());
                    cont.Salvar();
                    if (!processo.ContratosDiversos.Contains(cont))
                    {
                        processo.ContratosDiversos.Add(cont);
                    }
                }
            }

            processo = processo.Salvar();
            msg.CriarMensagem("Contratos associados com sucesso", "Sucesso", MsgIcons.Sucesso);
            lblAbrirSelecaoContratos_popupextender.Hide();
            this.CarregarContratos();
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

    protected void rptRenovacoes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "SelComand":

                    TextBox txtName = (TextBox)e.Item.FindControl("tbxDiasRenovacaoPeriodicos");
                    if (txtName != null && txtName.Text.IsNotNullOrEmpty() && txtName.Text.ToInt32() > 0)
                    {
                        string[] splitter = e.CommandArgument.ToString().Split(';');
                        ExibirDatasVencimentosPeriodicosRenovacao(txtName.Text.ToInt32(), RetornarVencimentosDoItemDaRenovacao(splitter[1], splitter[0].ToInt32()));
                        hfIdItemVencimentoPeriodico.Value = splitter[0];
                        hfIdTipoVencimentoPeriodico.Value = splitter[1];
                        hfDiasRenovacaoPeriodicos.Value = txtName.Text;
                        lblRenovacaoVencimentosPeriodicosDatas_popupextender.Show();
                    }
                    else
                    {
                        msg.CriarMensagem("É necessário informar os dias de renovação, para renovar este vencimento", "Alerta", MsgIcons.Alerta);
                        return;
                    }

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

    protected void btnRenovarVencimentosPeriodicos_Click(object sender, EventArgs e)
    {
        try
        {
            this.RenovarVencimentosPeriodicos();
            transacao.Recarregar(ref msg);
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

    #region __________Pre-Render___________

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ImageButton ibtn = (ImageButton)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Item(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnExcluirCondicionante_PreRender(object sender, EventArgs e)
    {
        //Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ImageButton ibtn = (ImageButton)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Condicionante(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void btnRenovarRelatorio_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((Button)sender, this.UsuarioLogado);
        if (trvProcessos.SelectedNode != null)
        {
            CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
            btnRenovarRelatorio.Enabled = c.GetUltimoRelatorio.Data > SqlDate.MinValue;
        }
    }

    protected void btnRenovarTaxa_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((Button)sender, this.UsuarioLogado);
        if (trvProcessos.SelectedNode != null)
        {
            CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
            btnRenovarTaxa.Enabled = c.GetUltimoPagamento.Data > SqlDate.MinValue;
        }
    }

    protected void btnRenovarCertificado_PreRender(object sender, EventArgs e)
    {
        //Permissoes.ValidarControle((Button)sender, this.UsuarioLogado);
        if (trvProcessos.SelectedNode != null)
        {
            CadastroTecnicoFederal c = CadastroTecnicoFederal.ConsultarPorId(trvProcessos.SelectedNode.Value.Split('_')[1].ToInt32());
            btnRenovarCertificado.Enabled = c.GetUltimoCertificado.Data > SqlDate.MinValue;
        }
    }

    protected void ibtnExcluirContratos_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja mesmo excluir o(s) contrato(s) associado(s)?");
    }

    #endregion

    #region __________ Triggers ___________

    protected void btnEnviarHistoricoGeral_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEnvioHistorico);
    }

    protected void lkbObservacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upObs);
    }

    protected void btnSalvarVencimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void lkbVenc_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanelNot);
    }

    protected void btnSalvarCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOrgaos);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void lkbVencimentoCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void lkbVencimentoOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }

    protected void btnSalvarLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
    }

    protected void trvProcessos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedNodeChanged", upPopProcesso);
    }

    protected void btnSalvarProcesso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArvore);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOrgaos);
    }

    protected void lkbOpcoesProcessoEditar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopProcesso);
    }

    protected void lkbOpcoesProcessoNovo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopProcesso);
    }

    protected void lkbOpcoesLicencaNova_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upLicenca);
    }

    protected void lkbOpcoesLicencaEditar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upLicenca);
    }

    protected void grvOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upPopUpOutros);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
    }

    protected void lkbOpcoesOutrosNovo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
    }

    protected void btnSalvarOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanel1);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void grvCondicionantes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upCamposCondicionante);
        WebUtil.InserirTriggerDinamica((Control)sender, "RowCancelingEdit", upRenovacao);
    }

    protected void btnSalvarCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanel1);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void ibtnAddNotificacaoCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void btnSalvarNotificacao_Init(object sender, EventArgs e)
    {
        //acertar esses scripts
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsGrupo();marcarEmailsEmpresa();marcarEmailsConsultora();</script>", false);

        if (objetoUtilizado.GetType() == typeof(Condicionante))
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposCondicionante);
        else if (objetoUtilizado.GetType() == typeof(Licenca))
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upLicenca);
        else if (objetoUtilizado.GetType() == typeof(OutrosEmpresa))
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
        else if (objetoUtilizado.GetType() == typeof(OutrosProcesso))
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
        else if (objetoUtilizado.GetType() == typeof(CadastroTecnicoFederal))
            WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopCadastroTecnicoFederal);

    }

    protected void ibtnAddNotificacaoOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAdicionarCondicionanteLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposCondicionante);
    }

    protected void ibtnAddNotificacaoPagamentoCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoRelatorioCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void ibtnAddNotificacaoCertificadoCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopNotificacoes);
    }

    protected void lkbOpcoesNovaCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopCadastroTecnicoFederal);
    }

    protected void lkbOpcoesEditarCTFr_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopCadastroTecnicoFederal);
    }

    protected void btnRenovarRelatorio_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenovacao);
    }

    protected void btnRenovarTaxa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenovacao);
    }

    protected void btnRenovarCertificado_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenovacao);
    }

    protected void lkbOpcoesOutrosNovo_Init1(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
    }

    protected void ibtnAdicionarCondicionanteLicenca_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void ibtnRenovar_PreRender(object sender, EventArgs e)
    {
        //if (this.UsuarioLogado != null && !this.UsuarioLogado.PermissaoEditar)
        //    Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void imgAbrir_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void lkbCondicionantesPadroes_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((LinkButton)sender, this.UsuarioLogado);
    }

    protected void ibtnAdicionarOutros_PreRender(object sender, EventArgs e)
    {
        //  Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void ibtnRenovar_PreRender1(object sender, EventArgs e)
    {
        //if (this.UsuarioLogado != null && !this.UsuarioLogado.PermissaoEditar)
        //    Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void imgAbrir2_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void btnSalvarRenovacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanel1);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void btnAddProrrogacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposCondicionante);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void ibtnExcluir6_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposCondicionante);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
    }

    protected void ddlStatusCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedIndexChanged", upProrrogacoes);
    }

    protected void ddlStatusOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "SelectedIndexChanged", upProrrogacoes);
    }

    protected void imgAbrir2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopUpOutros);
    }

    protected void btnAbrirContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPListagemContratosDiversos);
    }

    protected void gvContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", UPListagemContratosDiversos);
    }

    protected void btnSelecionarMaisContratos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPSelecaoContratos);
    }

    protected void gdvContratosSelecao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "PageIndexChanging", UPSelecaoContratos);
    }

    protected void btnSalvarContratoDiverso_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPListagemContratosDiversos);
    }

    protected void btnRenovarVencimentosPeriodicos_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsEmpresa();</script>", false);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanel1);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentosPeriodicos);
    }

    protected void rptRenovacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "ItemCommand", upDatasRenovacao);
    }

    #endregion

    protected void btnUploadCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadCondicionante_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdCondicionante.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a Condicionante para poder anexar arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            string idCliente = ddlGrupoEconomicos.SelectedValue;
            string idEmpresa = ddlEmpresa.SelectedIndex > 0 ? ddlEmpresa.SelectedValue : Processo.ConsultarPorId(trvProcessos.SelectedNode.Parent.Value.Split('_')[1].ToInt32()).Empresa.Id.ToString();

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + idEmpresa + "&idCliente=" + idCliente + "&tipo=Condicionante"));
            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnUploadLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadLicenca_Click(object sender, EventArgs e)
    {
        try
        {
            if (HfIdLicenca.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a Licença para poder anexar arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + (ddlEmpresa.SelectedIndex > 0 ? ddlEmpresa.SelectedValue : Processo.ConsultarPorId(IdSelecionadoArvore("p")).Empresa.Id.ToString()) +
                "&idCliente=" + ddlGrupoEconomicos.SelectedValue + "&tipo=Licenca"));
            lblUploadArquivos_ModalPopupExtender.Show();
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
    protected void bntUploadCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }
    protected void bntUploadCTF_Click(object sender, EventArgs e)
    {
        try
        {
            if (HfIdCTF.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o Cadastro Técnico Federal para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            string idCliente = ddlGrupoEconomicos.SelectedValue;
            string idEmpresa = ddlEmpresaCTF.SelectedValue;

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + idEmpresa + "&idCliente=" + idCliente + "&tipo=CTF"));
            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnUploadOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void btnUploadOutros_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdOutros.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o Outro de Empresa/Processo para poder anexar arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            string idCliente = ddlGrupoEconomicos.SelectedValue;
            string idEmpresa = ddlEmpresaGeralOutros.SelectedIndex > 0 ? Empresa.ConsultarPorId(ddlEmpresaGeralOutros.SelectedValue.ToInt32()).Id.ToString() :
                ddlEmpresa.SelectedIndex > 0 ? ddlEmpresa.SelectedValue : hfTipoOutros.Value == "emp" ? hfIdOutros.Value.ToInt32() > 0 ?
                OutrosEmpresa.ConsultarPorId(hfIdOutros.Value.ToInt32()).Empresa.Id.ToString() : null : Processo.ConsultarPorId(IdSelecionadoArvore("op")).Empresa.Id.ToString();

            conteudo.Attributes.Add("src", "../Upload/Upload.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idEmpresa=" + idEmpresa + "&idCliente=" + idCliente + "&tipo=Outros"));
            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void lbtnDownloadLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadLicenca_Click(object sender, EventArgs e)
    {
        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.IdSelecionadoArvore("l") + "&tipo=Licenca"));
        lblUploadArquivos_ModalPopupExtender.Show();        
    }

    protected void lbtnDownloadCTF_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }

    protected void lbtnDownloadCTF_Click(object sender, EventArgs e)
    {
        conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + this.IdSelecionadoArvore("ctf") + "&tipo=CTF"));
        lblUploadArquivos_ModalPopupExtender.Show();


    }
    protected void ibtnAbrirDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Condicionante condicionante = Condicionante.ConsultarPorId(((ImageButton)sender).CommandArgument.ToInt32());

            if (condicionante != null) 
            {
                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + condicionante.Id + "&tipo=Condicionante"));
                lblUploadArquivos_ModalPopupExtender.Show();
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
    protected void ibtnAbrirDownload_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }
    protected void ibtnAbrirDownloadOutros_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFrameArquivos);
    }
    protected void ibtnAbrirDownloadOutros_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfTipoOutros.Value == "emp")
            {
                OutrosEmpresa c = OutrosEmpresa.ConsultarPorId(((ImageButton)sender).CommandArgument.ToInt32());

                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id + "&tipo=OutrosEmpresa"));
            }
            else
            {
                OutrosProcesso c = OutrosProcesso.ConsultarPorId(((ImageButton)sender).CommandArgument.ToInt32());

                conteudo.Attributes.Add("src", "../Upload/View.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id + "&tipo=OutrosProcesso"));                        
            }

            lblUploadArquivos_ModalPopupExtender.Show();
           
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
    protected void lkbVencimentoLicenca_Click(object sender, EventArgs e)
    {
        try
        {
            Licenca licenca = new Licenca(HfIdLicenca.Value);
            licenca = licenca.ConsultarPorId();
            this.CarregarVencimentos(licenca.Vencimentos, "Licenca", licenca.Id);
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
    protected void lkbVencimentoLicenca_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVencimentos);
    }
}
