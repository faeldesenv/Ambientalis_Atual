using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Revenda_Pesquisa : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack) 
            {
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

    #region _________Métodos___________

    private void CarregarEstados()
    {
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
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
        IList<Revenda> revendas = Revenda.Filtrar(tbxNome.Text, tbxResponsavel.Text, tbxCnpjCpf.Text,
            Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()), Estado.ConsultarPorId(ddlEstado.SelectedValue.ToInt32()), ddlTipoParceiro.SelectedIndex != 0 ? ddlTipoParceiro.SelectedItem.Text : "");
        dgrClientes.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        if (ddlStatus.SelectedValue.ToInt32() > 0)
            this.RemoverRevendasDeOutrosStatus(revendas, ddlStatus.SelectedValue.ToInt32() == 1);
        dgrClientes.DataSource = revendas;
        dgrClientes.DataBind();
        lblQuantidade.Text = revendas.Count() + " Revenda(s) encontrada(s)";
    }

    private void RemoverRevendasDeOutrosStatus(IList<Revenda> revendas, bool status)
    {
        if (revendas != null && revendas.Count > 0)
        {            
            for (int i = revendas.Count - 1; i > -1; i--)
            {
                if (revendas[i] != null && revendas[i].Ativo != status)
                    revendas.Remove(revendas[i]);
            }
        }
    }

    #endregion

    #region _________Eventos___________

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

    protected void imgAbrir0_PreRender(object sender, EventArgs e)
    {
        //Permissoes.ValidarControle((ImageButton)sender, this.UsuarioLogado);
    }

    protected void dgrClientes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Response.Redirect("../Revenda/CadastroRevenda.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgrClientes.DataKeys[e.Item.ItemIndex]));
    }

    protected void dgrClientes_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
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

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Revenda(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void dgrClientes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            UsuarioComercial user = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"];
            if (user != null && user.GetType() == typeof(UsuarioRevendaComercial))
            {
                msg.CriarMensagem("Você não possui permissão para executar essa ação", "Alerta", MsgIcons.AcessoNegado);
                return;
            }

            int cont = 0;
            foreach (DataGridItem item in dgrClientes.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Revenda r = Revenda.ConsultarPorId(dgrClientes.DataKeys[item.ItemIndex].ToString().ToInt32());
                    if (r.Prospectos != null && r.Prospectos.Count > 0)
                        msg.CriarMensagem((cont++) + " Revenda(s) não pode(m) ser excluída(s) pois possui Prospectos associados!", "Atenção", MsgIcons.Informacao);
                    else
                        r.Excluir();
                    msg.CriarMensagem("Revenda excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    #endregion

    #region _________Bindings__________

    public String bindingTitulo(Object o)
    {
        Revenda r = (Revenda)o;
        return r.Nome;
    }

    public String bindingUrl(Object o)
    {
        Revenda c = (Revenda)o;
        return "../Revenda/CadastroRevenda.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id);
    }

    public String bindingCnpjCpf(Object o)
    {
        Revenda r = (Revenda)o;
        return r.GetNumeroCNPJeCPFComMascara;
    }

    public String bindingRazaoSocial(Object o)
    {
        Revenda r = (Revenda)o;
        return r.DadosPessoa.GetType() == typeof(DadosJuridicaComercial) ? ((DadosJuridicaComercial)r.DadosPessoa).RazaoSocial : "";
    }

    public String bindingCidade(Object o)
    {
        Revenda r = (Revenda)o;
        return r.Endereco != null && r.Endereco.Cidade != null ? r.Endereco.Cidade.Nome : "";
    }

    public String bindingEstadoSigla(Object o)
    {
        Revenda r = (Revenda)o;
        return r.Endereco != null && r.Endereco.Cidade != null && r.Endereco.Cidade .Estado != null ? r.Endereco.Cidade.Estado.PegarSiglaEstado() : "";
    }

    public String bindingStatusRevenda(Object o)
    {
        Revenda r = (Revenda)o;
        return r.GetUltimoContrato != null && r.GetUltimoContrato.Aceito && !r.GetUltimoContrato.Desativado ? "Sim" : "Não";
    }
    #endregion
    
}