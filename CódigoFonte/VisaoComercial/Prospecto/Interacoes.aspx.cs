using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Prospecto_Interacoes : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack) 
            {
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

    #region ___________Métodos_____________

    private void CarregarRevendas()
    {
        IList<Revenda> revendas = Revenda.ConsultarTodosOrdemAlfabetica();
        ddlRevenda.DataValueField = "Id";
        ddlRevenda.DataTextField = "Nome";
        ddlRevenda.DataSource = revendas;
        ddlRevenda.DataBind();
        ddlRevenda.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarProspectos(int idRevenda)
    {
        Revenda revenda = Revenda.ConsultarPorId(idRevenda);
        ddlProspecto.DataValueField = "Id";
        ddlProspecto.DataTextField = "Nome";
        ddlProspecto.DataSource = revenda != null && revenda.Prospectos != null ? revenda.Prospectos : new List<Prospecto>();
        ddlProspecto.DataBind();
        ddlProspecto.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void Pesquisar()
    {
        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        IList<Interacao> interacoes = Interacao.Filtrar(Revenda.ConsultarPorId(ddlRevenda.SelectedValue.ToInt32()), Prospecto.ConsultarPorId(ddlProspecto.SelectedValue.ToInt32()), ddlTipo.SelectedIndex > 0 ? ddlTipo.SelectedItem.Text : "", ddlStatus.SelectedIndex > 0 ? ddlStatus.SelectedItem.Text : "", dataDe, dataAteh);
        dgrinteracoes.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrinteracoes.DataSource = interacoes;
        dgrinteracoes.DataBind();
        lblQuantidade.Text = interacoes.Count() + " Interação(ões) encontrada(s)";
    }

    private void CarregarProspectosPopup()
    {
        ddlProspectoInteracao.DataValueField = "Id";
        ddlProspectoInteracao.DataTextField = "Nome";
        ddlProspectoInteracao.DataSource = Prospecto.ConsultarTodos();
        ddlProspectoInteracao.DataBind();
        ddlProspectoInteracao.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void RecarregarCampos()
    {
        this.CarregarProspectosPopup();
        tbxData.Text = "";
        ddlTipo.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        tbxNomePessoa.Text = "";
        tbxCargoPessoa.Text = "";
        tbxDescricao.Text = "";
        hfIdInteracao.Value = "0";
        msg.CriarMensagem("Você salvou a interação como \"Adiada\", crie uma nova interação e agende sua data.", "Sucesso");
    }

    private void SalvarInteracao()
    {
        Interacao interacao = Interacao.ConsultarPorId(hfIdInteracao.Value.ToInt32());
        if (interacao == null)
            interacao = new Interacao();

        interacao.Prospecto = Prospecto.ConsultarPorId(ddlProspectoInteracao.SelectedValue.ToInt32());
        interacao.Data = tbxData.Text.ToDateTime();
        interacao.Tipo = ddlTipoInteracao.SelectedValue;
        interacao.Status = ddlStatusInteracao.SelectedValue;
        interacao.NomePessoa = tbxNomePessoa.Text;
        interacao.CargoPessoa = tbxCargoPessoa.Text;
        interacao.Descricao = tbxDescricao.Text;
        interacao.Salvar();
        msg.CriarMensagem("Interação salva com Sucesso!", "Sucesso");
    }

    public void Novo()
    {
        this.CarregarProspectosPopup();
        tbxData.Text = "";
        ddlTipo.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        tbxNomePessoa.Text = "";
        tbxCargoPessoa.Text = "";
        tbxDescricao.Text = "";
        hfIdInteracao.Value = "0";
    }

    #endregion

    #region ___________Eventos_____________

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dgrinteracoes.CurrentPageIndex = 0;
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
            dgrinteracoes.CurrentPageIndex = 0;
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

    protected void ddlRevenda_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarProspectos(ddlRevenda.SelectedValue.ToInt32());
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

    protected void dgrinteracoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "EditCommand", upInteracoes);
    }

    protected void dgrinteracoes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            foreach (DataGridItem item in dgrinteracoes.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Interacao f = Interacao.ConsultarPorId(item.Cells[0].Text.ToInt32());
                    f.Excluir();
                }

            msg.CriarMensagem("Interação(ões) excluída(s) com sucesso", "Sucesso", MsgIcons.Sucesso);

            transacao.Recarregar(ref msg);
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

    protected void dgrinteracoes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Interacao inter = Interacao.ConsultarPorId(e.Item.Cells[0].Text.ToInt32());
            this.CarregarProspectosPopup();
            ddlProspectoInteracao.SelectedValue = inter.Prospecto.Id.ToString();
            tbxData.Text = inter.Data.ToShortDateString();
            ddlTipo.SelectedValue = inter.Tipo;
            ddlStatus.SelectedValue = inter.Status;
            tbxNomePessoa.Text = inter.NomePessoa;
            tbxCargoPessoa.Text = inter.CargoPessoa;
            tbxDescricao.Text = inter.Descricao;
            hfIdInteracao.Value = inter.Id.ToString();
            lblPopUpInteracao_popupextender.Show();
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
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Interações serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void dgrinteracoes_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgrinteracoes.CurrentPageIndex = e.NewPageIndex;
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

    protected void dgrinteracoes_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void ibtnAddInteracao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CarregarProspectosPopup();
            tbxData.Text = "";
            ddlTipo.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            tbxNomePessoa.Text = "";
            tbxCargoPessoa.Text = "";
            tbxDescricao.Text = "";
            hfIdInteracao.Value = "0";
            lblPopUpInteracao_popupextender.Show();

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

    protected void btnSalvarInteracao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarInteracao();
            transacao.Recarregar(ref msg);
            this.Pesquisar();

            if (ddlStatusInteracao.SelectedItem.Text == "Adiada")
            {
                this.RecarregarCampos();
            }
            else
                lblPopUpInteracao_popupextender.Hide();
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            this.Novo();
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            Interacao interacao = Interacao.ConsultarPorId(hfIdInteracao.Value.ToInt32());
            if (interacao != null && interacao.Id > 0)
            {
                interacao.Excluir();
                msg.CriarMensagem("Interação excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
                lblPopUpInteracao_popupextender.Hide();
                transacao.Recarregar(ref msg);
                this.Pesquisar();
            }
            else
            {
                msg.CriarMensagem("Salve primeiro a interação para poder excluí-la", "Alerta", MsgIcons.Alerta);
                return;
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

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        Button ibtn = (Button)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta Interação serão perdidos. Deseja excluir mesmo assim?");
    }

    #endregion

    #region ___________Bindings____________

    public String bindingPessoaCargo(Object o)
    {
        Interacao i = (Interacao)o;
        return i.NomePessoa + " (" + i.CargoPessoa + ")";
    }

    public String bindingRevenda(Object o)
    {
        Interacao i = (Interacao)o;
        return i.Prospecto != null && i.Prospecto.Revenda != null ? i.Prospecto.Revenda.Nome : "";
    }

    public String bindingProspecto(Object o)
    {
        Interacao i = (Interacao)o;
        return i.Prospecto != null ? i.Prospecto.Nome : "";
    }

    #endregion

    #region _______TriggerDinamicas________

    protected void ibtnAddInteracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upInteracoes);
    }

    protected void btnSalvarInteracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisa);
    }

    protected void btnExcluir_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisa);
    }

    #endregion

    
}