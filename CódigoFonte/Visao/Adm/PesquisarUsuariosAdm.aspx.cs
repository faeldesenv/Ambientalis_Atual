using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Adm_PesquisarUsuariosAdm : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    private IList<Usuario> Usuarios
    {
        get
        {
            if (Session["usuarios_pesquisar_usuarios"] == null)
                Session["usuarios_pesquisar_usuarios"] = new List<Usuario>();
            return (IList<Usuario>)Session["usuarios_pesquisar_usuarios"];
        }
        set { Session["usuarios_pesquisar_usuarios"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                transacao.Abrir();
                this.Pesquisar();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                this.GetMBOX<MBOX>().Show(msg);
            }
        }
    }

    #region _____________ Eventos _______________

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Excluir();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            transacao.Abrir();
            Response.Redirect("ManterUsuarioAdm.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgr.DataKeys[e.Item.ItemIndex].ToString() + "&bd="+ddlSistema.SelectedValue), false);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            transacao.Abrir();
            dgr.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_PreRender(object sender, EventArgs e)
    {
        
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Usuários(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    #endregion

    #region _____________ Métodos _______________

    private void Pesquisar()
    {
        this.Usuarios = Usuario.Filtrar(tbxNome.Text.Trim(), tbxLogin.Text.Trim(), tbxEmail.Text.Trim());
        this.BindingUsuarios();
    }

    private void BindingUsuarios()
    {        
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgr.DataSource = this.Usuarios;
        dgr.DataBind();

        lblStatus.Text = this.Usuarios.Count + " item(s) encontrado(s)";
    }

    private void Excluir()
    {
        foreach (DataGridItem item in dgr.Items)
            //verificação para exclusao.
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                Usuario user = Usuario.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                user.Excluir();
                this.Usuarios.Remove(user);
                msg.CriarMensagem("Usuário(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
            }

        this.BindingUsuarios();
    }

    #endregion

    protected void imgAbrir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, true);
    }
    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if(this.Usuarios != null && this.Usuarios.Count > 0)
             this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }
    protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
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