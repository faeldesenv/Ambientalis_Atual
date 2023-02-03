using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Persistencia.Filtros;
using Utilitarios.Criptografia;

public partial class Cliente_PesquisarClientes : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.IniciarCampos();
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

    #region ___________Metodos____________

    public void Pesquisar()
    {
        IList<GrupoEconomico> clientes = GrupoEconomico.Filtrar(tbxNome.Text, tbxResponsavel.Text, 1, tbxCnpjCpf.Text,
            Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()), 0 , ddlCancelado.SelectedValue.ToInt32(), Estado.ConsultarPorId(ddlEstados.SelectedValue.ToInt32()));
        dgrClientes.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;        
        dgrClientes.DataSource = clientes;
        dgrClientes.DataBind();
        lblQuantidade.Text = clientes.Count() + " Grupo(s) Econômico(s) encontrado(s)";
    }

    public void CarregarEstados()
    {
        ddlEstados.DataValueField = "Id";
        ddlEstados.DataTextField = "Nome";
        ddlEstados.DataSource = Estado.ConsultarTodos();
        ddlEstados.DataBind();
        ddlEstados.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public void CarregarCidades(int p)
    {
        if (p <= 0)
        {
            ddlCidades.Items.Clear();
            return;
        }

        Estado estado = Estado.ConsultarPorId(p);
        ddlCidades.DataValueField = "Id";
        ddlCidades.DataTextField = "Nome";
        ddlCidades.DataSource = estado.Cidades;
        ddlCidades.DataBind();
        ddlCidades.Items.Insert(0, new ListItem("-- Todas as cidades --", "0"));
    }

    private void IniciarCampos()
    {
        this.CarregarEstados();
        this.Pesquisar();
    }


    #endregion

    #region ___________Bindings____________

    public String bindingUrl(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        return "../GrupoEconomico/CadastroGrupoEconomico.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id);
    }

    public String bindingTitulo(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        return c.Nome;
    }

    public String bindingCnpjCpf(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        return c.GetNumeroCNPJeCPFComMascara;
        
        
    }

    public String bindingRazaoSocial(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;

        if (c.DadosPessoa != null)
        {
            if (c.DadosPessoa.GetType() == typeof(DadosJuridica))
            {
                return ((DadosJuridica)c.DadosPessoa).RazaoSocial;
            }
        } return "";
    }

    public String bindingCidade(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        if (c.Endereco != null && c.Endereco.Cidade != null)
        {
            return c.Endereco.Cidade.Nome + " - " + c.Endereco.Cidade.Estado.PegarSiglaEstado();
        } return "";
    }

    public String bindingEstado(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        if (c.Endereco != null && c.Endereco.Cidade != null)
        {
            return c.Endereco.Cidade.Estado.Nome;
        } return "";
    }

    #endregion

    #region __________Eventos___________

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

    protected void ddlEstados_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(Convert.ToInt32(ddlEstados.SelectedValue));
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

    protected void dgrClientes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Response.Redirect("../GrupoEconomico/CadastroGrupoEconomico.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgrClientes.DataKeys[e.Item.ItemIndex]));
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
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

    #endregion

    protected void imgAbrir0_PreRender(object sender, EventArgs e)
    {
        
    }
}