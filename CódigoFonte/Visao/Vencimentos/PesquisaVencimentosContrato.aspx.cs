using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Vencimentos_PesquisaVencimentosContrato : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    public Usuario usuarioLogado
    {
        get
        {
            if (Session["UsuarioLogado_SistemaAmbiental"] == null)
                return null;
            return (Usuario)Session["UsuarioLogado_SistemaAmbiental"];
        }
        set { Session["UsuarioLogado_SistemaAmbiental"] = value; }
    }

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.VerificarPermissoes();
                this.CarregarGrupos();
                this.CarregarStatus();
                this.Pesquisar(true);
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

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloContratos.Id);
        else
            this.ConfiguracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloContratos.Id);

        if (this.ConfiguracaoModuloContratos != null)
        {
            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                this.EmpresasPermissaoModuloContratos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(moduloContratos, this.UsuarioLogado);

            if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
                this.SetoresPermissaoModuloContratos = Setor.ObterSetoresQueOUsuarioPossuiAcesso(this.UsuarioLogado);
        }

    }

    private void CarregarGrupos()
    {
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    #region _________Métodos___________

    private void CarregarEmpresas(int idGrupoEconomico)
    {
        ddlEmpresa.Items.Clear();
        ddlEmpresa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloContratos != null && this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloContratos != null ? this.EmpresasPermissaoModuloContratos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloContratos != null ? this.EmpresasPermissaoModuloContratos : new List<Empresa>();
        }
        else
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(idGrupoEconomico);
            empresas = c.Empresas != null ? c.Empresas : new List<Empresa>();
        }

        if (empresas != null && empresas.Count > 0)
        {
            foreach (Empresa emp in empresas)
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
        }
    }

    private void CarregarStatus()
    {
        ddlStatus.Items.Clear();
        IList<StatusFixosContrato> statusfixos = StatusFixosContrato.ConsultarTodos();
        if (statusfixos != null && statusfixos.Count > 0)
        {
            foreach (StatusFixosContrato fixo in statusfixos)
            {
                ddlStatus.Items.Add(new ListItem(fixo.Nome, "n_" + fixo.Id.ToString()));
            }
        }

        IList<StatusEditaveisContrato> statusEditaveis = StatusEditaveisContrato.ConsultarTodos();
        if (statusEditaveis != null && statusEditaveis.Count > 0)
        {
            foreach (StatusEditaveisContrato editavel in statusEditaveis)
            {
                ddlStatus.Items.Add(new ListItem(editavel.Nome, "edit_" + editavel.Id.ToString()));
            }
        }
        ddlStatus.Items.Insert(0, new ListItem("-- Todos --", "n_0"));
    }

    private void Pesquisar(bool atualizarContador)
    {
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue.ToInt32();

        if (this.ConfiguracaoModuloContratos == null)
        {
            this.CarregarGridContratos(new List<ContratoDiverso>(), atualizarContador);
            return;
        }

        //Configuração é por empresa e o usuário não possui permissão de visualizar nenhuma empresa
        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (this.EmpresasPermissaoModuloContratos == null || this.EmpresasPermissaoModuloContratos.Count == 0)
            {
                this.CarregarGridContratos(new List<ContratoDiverso>(), atualizarContador);
                return;
            }

        }
        //Configuração é por setor e o usuário não possui permissão de visualizar nenhum setor
        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            if (this.SetoresPermissaoModuloContratos == null || this.SetoresPermissaoModuloContratos.Count == 0)
            {
                this.CarregarGridContratos(new List<ContratoDiverso>(), atualizarContador);
                return;
            }
        }

        if (atualizarContador)
            hfQuantidadeExibicao.Value = ContratoDiverso.FiltrarContador(
               ddlGrupoEconomico.SelectedValue.ToInt32(), ddlEmpresa.SelectedValue.ToInt32(), ddlComo.SelectedValue.ToInt32(), tbxFornecedorCliente.Text, ddlStatus.SelectedValue.Split('_')[1].ToInt32(), tbxNumeroContrato.Text, this.ConfiguracaoModuloContratos.Tipo, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos).ToString();

        IList<ContratoDiverso> contratos = ContratoDiverso.Consultar(ddlGrupoEconomico.SelectedValue.ToInt32(), ddlEmpresa.SelectedValue.ToInt32(), ddlComo.SelectedValue.ToInt32(), tbxFornecedorCliente.Text, ddlStatus.SelectedValue.Split('_')[1].ToInt32(), tbxNumeroContrato.Text, this.ConfiguracaoModuloContratos.Tipo, this.EmpresasPermissaoModuloContratos, this.SetoresPermissaoModuloContratos, (dgr.PageIndex * ddlQuantidaItensGrid.SelectedValue.ToInt32()),
           ((dgr.PageIndex + 1) * ddlQuantidaItensGrid.SelectedValue.ToInt32()));

        this.CarregarGridContratos(contratos, atualizarContador);

    }

    private void CarregarGridContratos(IList<ContratoDiverso> contratos, bool atualizarContador)
    {
        List<ContratoDiverso> aux = new List<ContratoDiverso>();
        if (dgr.PageIndex > 0)
            aux.AddRange(new ContratoDiverso[dgr.PageIndex * ddlQuantidaItensGrid.SelectedValue.ToInt32()]);
        aux.AddRange(contratos);
        int quantidadeFaltando = hfQuantidadeExibicao.Value.ToInt32() - ((dgr.PageIndex + 1) * ddlQuantidaItensGrid.SelectedValue.ToInt32());
        if (quantidadeFaltando > 0)
            aux.AddRange(new ContratoDiverso[quantidadeFaltando]);

        dgr.DataSource = aux;
        dgr.DataBind();

        lblQuantidade.Text = contratos.Count() < 1 ? "Não foi encontrado nenhum contrato com estes filtros" : aux.Count + " Contrato(s) Encontrado(s)";
    }

    #endregion

    #region _________Bindings__________

    public string BindGrupoEmpresa(Object o)
    {
        ContratoDiverso diverso = (ContratoDiverso)o;
        if (diverso != null && diverso.Empresa != null && diverso.Empresa.GrupoEconomico != null)
            return diverso.Empresa.GrupoEconomico.Nome + "/" + diverso.Empresa.Nome;
        return "";
    }

    public string BindFornecedorCliente(Object o)
    {
        ContratoDiverso diverso = (ContratoDiverso)o;
        if (diverso != null && diverso.Cliente != null && diverso.Fornecedor == null)
            return diverso.Cliente.Nome;
        else if (diverso != null && diverso.Fornecedor != null && diverso.Cliente == null)
            return diverso.Fornecedor.Nome;
        return "";
    }

    public string BindStatus(Object o)
    {
        ContratoDiverso diverso = (ContratoDiverso)o;
        if (diverso != null && diverso.StatusContratoDiverso != null)
            return diverso.StatusContratoDiverso.Nome;
        return "";
    }

    public string BindDataAbertura(Object o)
    {
        ContratoDiverso diverso = (ContratoDiverso)o;
        if (diverso != null && diverso.DataAbertura != SqlDate.MinValue)
            return diverso.DataAbertura.ToShortDateString();
        return "";
    }

    public string BindEditarContrato(Object o)
    {
        ContratoDiverso diverso = (ContratoDiverso)o;
        return "CadastroVencimentosContrato.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + diverso.Id.ToString());
    }

    public bool BindingVisivel(object contratoDiverso)
    {
        if (this.ConfiguracaoModuloContratos == null)
            return false;

        ContratoDiverso contrato = (ContratoDiverso)contratoDiverso;

        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (contrato.Empresa == null)
                return false;

            EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(contrato.Empresa.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);
            return empresaPermissao != null && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado);
        }

        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
        {
            if (contrato.Setor == null)
                return false;

            if ((this.SetoresPermissaoModuloContratos == null || this.SetoresPermissaoModuloContratos.Count == 0) || (this.SetoresPermissaoModuloContratos != null && this.SetoresPermissaoModuloContratos.Count > 0 && !this.SetoresPermissaoModuloContratos.Contains(contrato.Setor)))
                return false;

            Setor setor = contrato.Setor.ConsultarPorId();

            return setor != null && setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0 && setor.UsuariosEdicao.Contains(this.UsuarioLogado);
        }

        if (this.ConfiguracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            return this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
        }

        return true;
    }

    #endregion

    #region __________Eventos__________

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dgr.PageSize = (ddlQuantidaItensGrid.SelectedValue.ToInt32() != 1) ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : 10000;
            this.Pesquisar(false);
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

    protected void ibtnRenovar_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
    }

    protected void ibtnRenovarReajuste_PreRender(object sender, EventArgs e)
    {
        //  Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Contrato(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnEditar_PreRender(object sender, EventArgs e)
    {
        // Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            dgr.PageIndex = 0;
            this.Pesquisar(true);
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

    protected void dgr_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        //try
        //{
        //    foreach (DataGridItem item in dgr.Items)
        //        if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
        //        {
        //            ContratoDiverso d = ContratoDiverso.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
        //            if (d != null)
        //                d.Excluir();
        //        }
        //    transacao.Recarregar(ref msg);
        //    this.Pesquisar(true);
        //    msg.CriarMensagem("Contrato(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
        //}
        //catch (Exception ex)
        //{
        //    msg.CriarMensagem(ex);
        //}
        //finally
        //{
        //    this.GetMBOX<MBOX>().Show(msg);
        //}
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        //try
        //{
        //    dgr.CurrentPageIndex = e.NewPageIndex;
        //    this.Pesquisar(false);
        //}
        //catch (Exception ex)
        //{
        //    msg.CriarMensagem(ex);
        //}
        //finally
        //{
        //    this.GetMBOX<MBOX>().Show(msg);
        //}
    }

    #endregion

    protected void btnAlterarStatus_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsGrupo();marcarEmailsEmpresa();</script>", false);
    }
    protected void ddlGrupoEconomico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresas(ddlGrupoEconomico.SelectedValue.ToInt32());
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

    protected void dgr_Init(object sender, EventArgs e)
    {

    }
    protected void ddlComo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlComo.SelectedIndex > 0)
            {
                tbxFornecedorCliente.Enabled = true;
                lblFornecedorCliente.Text = ddlComo.SelectedValue.ToInt32() == 1 ? "Fornecedor(Contratada):" : "Cliente(Contratante):";
            }
            else
            {
                tbxFornecedorCliente.Text = "";
                tbxFornecedorCliente.Enabled = false;
                lblFornecedorCliente.Text = "Fornecedor/Cliente";
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

    protected void dgr_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {            
            ContratoDiverso d = ContratoDiverso.ConsultarPorId(dgr.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (d != null)
                d.Excluir();

            transacao.Recarregar(ref msg);
            this.Pesquisar(true);
            msg.CriarMensagem("Contrato(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
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
    protected void dgr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dgr.PageIndex = e.NewPageIndex;
            this.Pesquisar(false);
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