using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Fornecedores_PesquisarFornecedores : PageBase
{
    Msg msg = new Msg();

    public bool UsuarioEditorContratos
    {
        get
        {
            if (Session["UsuarioEditorContratos"] == null)
                return false;
            else
                return (bool)Session["UsuarioEditorContratos"];
        }
        set { Session["UsuarioEditorContratos"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.UsuarioEditorContratos = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloContratos;

                this.CarregarEstados();
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

    #region ________Métodos__________

    private void CarregarEstados()
    {
        ddlEstados.DataValueField = "Id";
        ddlEstados.DataTextField = "Nome";
        ddlEstados.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        ddlEstados.DataBind();
        ddlEstados.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarCidades(int p)
    {
        Estado estado = Estado.ConsultarPorId(p);
        ddlCidades.DataValueField = "Id";
        ddlCidades.DataTextField = "Nome";
        ddlCidades.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        ddlCidades.DataBind();
        ddlCidades.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void Pesquisar()
    {
        IList<Fornecedor> fornecedores = Fornecedor.Filtrar(tbxNomeRazao.Text, tbxCnpjCpf.Text, ddlStatus.SelectedValue.ToInt32(), Estado.ConsultarPorId(ddlEstados.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()));
        dgrFornecedores.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrFornecedores.DataSource = fornecedores;
        dgrFornecedores.DataBind();
        lblQuantidade.Text = fornecedores.Count() + " Fornecedor(es) encontrado(s)";
    }

    #endregion

    #region ________Eventos__________

    protected void dgrFornecedores_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgrFornecedores.CurrentPageIndex = e.NewPageIndex;
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

    protected void dgrFornecedores_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgrFornecedores_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Response.Redirect("CadastrarFornecedores.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgrFornecedores.DataKeys[e.Item.ItemIndex].ToString()), false);
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

    protected void dgrFornecedores_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            msg.CriarMensagem("Fornecedor(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);            
            
            foreach (DataGridItem item in dgrFornecedores.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Fornecedor c = Fornecedor.ConsultarPorId(dgrFornecedores.DataKeys[item.ItemIndex].ToString().ToInt32());
                    if ((c.ContratosDiversos != null && c.ContratosDiversos.Count > 0))
                        msg.CriarMensagem("Alguns Fornecedores não pode(m) ser excluída(s) pois possui Contratos associados!", "Atenção", MsgIcons.Informacao);
                    else
                        c.Excluir();
                }

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

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstados.SelectedValue.ToInt32());
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

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
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

    protected void imgAbrir0_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Fornecedor(es) serão perdido(s). Deseja excluir mesmo assim?");
    }

    #endregion

    #region _________Bindings________

    public String bindingUrl(Object o)
    {
        Fornecedor c = (Fornecedor)o;
        return "../Fornecedores/CadastrarFornecedores.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id);
    }

    public String bindingTitulo(Object o)
    {
        Fornecedor c = (Fornecedor)o;
        if (c.DadosPessoa != null && c.DadosPessoa.GetType() == typeof(DadosJuridica))
        {
            return ((DadosJuridica)c.DadosPessoa).RazaoSocial;
        }
        else
        {
            return c.Nome;
        }
    }

    public String bindingCnpjCpf(Object o)
    {
        Fornecedor c = (Fornecedor)o;
        if (c.DadosPessoa != null)
            return c.GetNumeroCNPJeCPFComMascara;
        else
            return "";
    }

    public String bindingCidade(Object o)
    {
        Fornecedor c = (Fornecedor)o;
        if (c != null && c.Endereco != null && c.Endereco.Cidade != null)
            return c.Endereco.Cidade.Nome;
        else
            return "";
    }

    public String bindingEstadoSigla(Object o)
    {
        Fornecedor c = (Fornecedor)o;
        if (c != null && c.Endereco != null && c.Endereco.Cidade != null && c.Endereco.Cidade.Estado != null)
            return c.Endereco.Cidade.Estado.Nome;
        else
            return "";
    }

    public String bindingStatusCliente(Object o)
    {
        Fornecedor c = (Fornecedor)o;
        if (c != null && c.Ativo)
            return "Ativo";
        else
            return "Inativo";
    }

    public bool BindingVisivel(object diverso)
    {
        return this.UsuarioEditorContratos;
    }

    #endregion
    
}