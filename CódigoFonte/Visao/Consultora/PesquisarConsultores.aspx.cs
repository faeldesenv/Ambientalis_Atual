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
    private Msg msg = new Msg();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.UsuarioEditorModuloGeral = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloGeral;
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

    #region ___________Metodos____________

    public void Pesquisar()
    {
        IList<Consultora> cons = Consultora.Filtrar(tbxNome.Text, tbxResponsavel.Text, ddlStatus.SelectedValue.ToInt32(), tbxCnpjCpf.Text,
            Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()), Estado.ConsultarPorId(ddlEstados.SelectedValue.ToInt32()));
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;        
        dgr.DataSource = cons;
        dgr.DataBind();

        lblQuantidade.Text = cons.Count() + " Consultora(s) encontrada(s)";
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

    #endregion

    #region ___________Bindings____________

    public String bindTelefone(Object o)
    {
        Consultora c = (Consultora)o;
        if (c != null && c.Id > 0 && c.Contato != null)
            return c.Contato.ContatoTelefones;
        return "";
    }

    public String bindCpfCnpj(Object o)
    {
        Consultora c = (Consultora)o;

        if (c.DadosPessoa != null)
        {
            if (c.DadosPessoa.GetType() == typeof(DadosJuridica))
            {
                return ((DadosJuridica)c.DadosPessoa).Cnpj;
            }
            else
            {
                return ((DadosFisica)c.DadosPessoa).Cpf;
            }
        } return "";
    }

    public String bindRazaoSocial(Object o)
    {
        Consultora c = (Consultora)o;

        if (c.DadosPessoa != null)
        {
            if (c.DadosPessoa.GetType() == typeof(DadosJuridica))
            {
                return ((DadosJuridica)c.DadosPessoa).RazaoSocial;
            }
        } return "";
    }

    public String bindCidade(Object o)
    {
        Consultora c = (Consultora)o;
        if (c.Endereco != null && c.Endereco.Cidade != null)
        {
            return c.Endereco.Cidade.Nome + " - " + c.Endereco.Cidade.Estado.PegarSiglaEstado();
        } return "";
    }

    #endregion

    #region __________Eventos___________

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
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

    protected void dgr_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Response.Redirect("ManterConsultora.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgr.DataKeys[e.Item.ItemIndex].ToString()), false);
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
        try
        {
            foreach (DataGridItem item in dgr.Items)
                //verificação para exclusao.
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Consultora c = Consultora.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                    c.Excluir();
                    msg.CriarMensagem("Consultora excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    protected void chkSelecionar_CheckedChanged(object sender, EventArgs e)
    {
        bool aux = ((CheckBox)sender).Checked;
        foreach (DataGridItem item in dgr.Items)
            ((CheckBox)item.FindControl("ckbExcluir")).Checked = aux;
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorModuloGeral);

        ImageButton ibtn = (ImageButton)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Consultora(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
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

    #endregion

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
    protected void PermissaoUsuario_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorModuloGeral);
    }
}