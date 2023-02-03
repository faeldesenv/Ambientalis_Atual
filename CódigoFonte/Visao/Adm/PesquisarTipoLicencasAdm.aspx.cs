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

public partial class Licenca_PesquisarTipoLicencasAdm : PageBase
{
    private Msg msg = new Msg();
    Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
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

    #region ___________Metodos____________

    public void Pesquisar()
    {
        IList<TipoLicenca> cons = TipoLicenca.FiltrarTiposPadrao(tbxNome.Text);
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgr.DataSource = cons;
        dgr.DataBind();

        lblQuantidade.Text = cons.Count() + " tipo(s) de licença(s) encontrado(s)";
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

    protected void dgr_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Response.Redirect("ManterTipoLicencaAdm.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgr.DataKeys[e.Item.ItemIndex].ToString()), false);
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
            transacao.Abrir();
            foreach (DataGridItem item in dgr.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    TipoLicenca t = TipoLicenca.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                    if (t.Licencas != null && t.Licencas.Count > 0)
                        msg.CriarMensagem("Existem licenças associadas à esse tipo de licença.", "Alerta", MsgIcons.Alerta);
                    else
                    {
                        t.Excluir();
                        msg.CriarMensagem("Licença excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
                    }
                }
            transacao.Fechar(ref msg);
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

    protected void chkSelecionar_CheckedChanged(object sender, EventArgs e)
    {
        bool aux = ((CheckBox)sender).Checked;
        foreach (DataGridItem item in dgr.Items)
            ((CheckBox)item.FindControl("ckbExcluir")).Checked = aux;
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, true);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Licença(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
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

    #endregion

    protected void imgAbrir0_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, true);
    }
}