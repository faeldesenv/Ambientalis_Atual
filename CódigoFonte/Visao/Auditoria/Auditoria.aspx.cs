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
using Modelo.Auditoria;

public partial class Relatorio_auditoria : PageBase
{
    private Msg msg = new Msg();
    Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
        if (!tbxDatade.Text.IsDate() && !tbxDataAte.Text.IsDate())
        {
            msg.CriarMensagem("É necessário informar um intervalo de data para realizar a consulta", "ATENÇÃO", MsgIcons.Informacao);
            return;
        }

        string operacao = "";
        if(ddlOperacao.SelectedIndex > 0)
            operacao = ddlOperacao.SelectedValue;

        Auditoria.ExcluirAuditoriasComSQLDateTimeMinValue();
        IList<Auditoria> cons = Auditoria.Filtrar(tbxNome.Text.Trim(), tbxDatade.Text.Trim(), tbxDataAte.Text.Trim(), operacao.Trim() , tbxRegistro.Text.Trim(), tbxValorAntigo.Text.Trim(), tbxValorNovo.Text.Trim());
        dgrAuditoria.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrAuditoria.DataSource = cons;
        dgrAuditoria.DataBind();

        lblQuantidade.Text = cons.Count() + " registro(s) encontrado(s)";
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
            dgrAuditoria.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
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
    }

    #endregion
    
}