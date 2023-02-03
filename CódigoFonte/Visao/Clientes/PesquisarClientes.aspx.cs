using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Clientes_PesquisarClientes : PageBase
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

    #region _________Métodos_________

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
        ddlCidades.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void Pesquisar()
    {
        IList<Cliente> clientes = Cliente.Filtrar(tbxNomeRazao.Text, tbxCnpjCpf.Text, ddlStatus.SelectedValue.ToInt32(), Estado.ConsultarPorId(ddlEstados.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()));
        dgrClientes.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrClientes.DataSource = clientes;
        dgrClientes.DataBind();
        lblQuantidade.Text = clientes.Count() + " Cliente(s) encontrado(s)";
    }

    #endregion

    #region _________Eventos_________

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

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Cliente(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    #endregion    

    #region _________Bindings________

    public String bindingUrl(Object o)
    {
        Cliente c = (Cliente)o;
        return "../Clientes/CadastroClientes.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id);
    }

    public String bindingTitulo(Object o)
    {
        Cliente c = (Cliente)o;
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
        Cliente c = (Cliente)o;
        if (c.DadosPessoa != null)
            return c.GetNumeroCNPJeCPFComMascara;
        else
            return "";        
    }

    public String bindingCidade(Object o)
    {
        Cliente c = (Cliente)o;
        if (c != null && c.Endereco != null && c.Endereco.Cidade != null)
            return c.Endereco.Cidade.Nome;
        else
            return "";
    }

    public String bindingEstadoSigla(Object o)
    {
        Cliente c = (Cliente)o;
        if (c != null && c.Endereco != null && c.Endereco.Cidade != null && c.Endereco.Cidade.Estado != null)
            return c.Endereco.Cidade.Estado.Nome;
        else
            return "";
    }

    public String bindingStatusCliente(Object o)
    {
        Cliente c = (Cliente)o;
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
    
    
    protected void dgrClientes_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgrClientes.CurrentPageIndex = e.NewPageIndex;
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

    protected void dgrClientes_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgrClientes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Response.Redirect("CadastroClientes.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgrClientes.DataKeys[e.Item.ItemIndex].ToString()), false);
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
    protected void dgrClientes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            msg.CriarMensagem("Cliente(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
            
            
            foreach (DataGridItem item in dgrClientes.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Cliente c = Cliente.ConsultarPorId(dgrClientes.DataKeys[item.ItemIndex].ToString().ToInt32());
                    if ((c.ContratosDiversos != null && c.ContratosDiversos.Count > 0))
                        msg.CriarMensagem("Alguns Clientes não pode(m) ser excluída(s) pois possui Contratos associados!", "Atenção", MsgIcons.Informacao);
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
    protected void imgAbrir0_PreRender(object sender, EventArgs e)
    {
       Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorContratos);
    }
}