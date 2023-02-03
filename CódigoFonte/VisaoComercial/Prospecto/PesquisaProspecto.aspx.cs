using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Prospecto_PesquisaProspecto : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarEstados();
                this.CarregarRevendas();
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

   
    #region _________Métodos___________

    private void CarregarEstados()
    {
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRevendas()
    {
        ddlRevenda.DataValueField = "Id";
        ddlRevenda.DataTextField = "Nome";
        ddlRevenda.DataSource = Revenda.ConsultarTodos();
        ddlRevenda.DataBind();
        ddlRevenda.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarCidades(int idEstado)
    {
        Estado estado = Estado.ConsultarPorId(idEstado);
        ddlCidades.DataValueField = "Id";
        ddlCidades.DataTextField = "Nome";
        ddlCidades.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        ddlCidades.DataBind();
        ddlCidades.Items.Insert(0, new ListItem("--Todas --", "0"));
    }

    private void Pesquisar()
    {
        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Prospecto> prospectos = Prospecto.Filtrar(Revenda.ConsultarPorId(ddlRevenda.SelectedValue.ToInt32()), tbxNome.Text, cbxCpfCnpj.Text, tbxResponsavel.Text, Estado.ConsultarPorId(ddlEstado.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()), ddlStatus.SelectedValue, dataDe, dataAteh);
        dgrClientes.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrClientes.DataSource = prospectos;
        dgrClientes.DataBind();
        lblQuantidade.Text = prospectos.Count() + " Indicação(ões) de Cliente(s) encontrado(s)";
    }

    #endregion

    #region _________Eventos___________

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            dgrClientes.CurrentPageIndex = 0;
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
            dgrClientes.CurrentPageIndex = 0;
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

    protected void dgrClientes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Response.Redirect("ManterProspecto.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgrClientes.DataKeys[e.Item.ItemIndex]));
    }

    protected void dgrClientes_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        //Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Indicação(ões) de Cliente(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void dgrClientes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            int cont = 0;
            foreach (DataGridItem item in dgrClientes.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Prospecto p = Prospecto.ConsultarPorId(dgrClientes.DataKeys[item.ItemIndex].ToString().ToInt32());
                    if (p.Venda != null && p.Venda.Id > 0)
                        msg.CriarMensagem((cont++) + " Indicação de Cliente(s) não pode(m) ser excluído(s) pois possui vendas associadas!", "Atenção", MsgIcons.Informacao);
                    else
                        p.Excluir();
                    msg.CriarMensagem("Indicação de Cliente excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstado.SelectedValue.ToInt32());
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

    #endregion

    #region _________Bindings__________

    public String bindingRevenda(Object o)
    {
        Prospecto c = (Prospecto)o;
        return  c.Revenda != null ? c.Revenda.Nome : "";
    }

    public String bindingUrl(Object o)
    {
        Prospecto c = (Prospecto)o;
        return "ManterProspecto.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id);
    }

    public String bindingDataCadastro(Object o)
    {
        Prospecto c = (Prospecto)o;
        return c.DataCadastro.ToShortDateString();
    }

    public String bindingStatus(Object o)
    {
        Prospecto c = (Prospecto)o;
        return c.Ativo ? "Ativo" : "Inativo";
    }

    public String bindingCnpjCpf(Object o)
    {
        Prospecto r = (Prospecto)o;
        return r.GetNumeroCNPJeCPFComMascara;
    }

    public String bindingRazaoSocial(Object o)
    {
        Prospecto r = (Prospecto)o;
        return r.DadosPessoa.GetType() == typeof(DadosJuridicaComercial) ? ((DadosJuridicaComercial)r.DadosPessoa).RazaoSocial : "";
    }

    public String bindingCidade(Object o)
    {
        Prospecto r = (Prospecto)o;
        return r.Endereco != null && r.Endereco.Cidade != null ? r.Endereco.Cidade.Nome : "";
    }

    public String bindingEstadoSigla(Object o)
    {
        Prospecto r = (Prospecto)o;
        return r.Endereco != null && r.Endereco.Cidade != null && r.Endereco.Cidade.Estado != null ? r.Endereco.Cidade.Estado.PegarSiglaEstado() : "";
    }

    #endregion

}