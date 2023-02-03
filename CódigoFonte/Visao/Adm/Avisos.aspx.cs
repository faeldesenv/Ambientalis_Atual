using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class AvisosADM : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = 0;
                transacao.Abrir();

                this.CarregarAviso();
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

    #region ________ Eventos ___________

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = 0;
            transacao.Abrir();
            this.SalvarAviso();
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

    #region ________ Métodos ___________

    private void SalvarAviso()
    {
        if (ddlExibirEm.SelectedValue.ToInt32() == 0) 
        {
            msg.CriarMensagem("Selecione o local de exibição do aviso", "Alerta", MsgIcons.Alerta);
            return;
        }

        Aviso a = Aviso.ConsultarUltimoAvisoSistema(false);
        if (a == null)
        {
            a = new Aviso();
            a.Id = 1;
            a = a.Salvar();
        }
        a.DataInicio = tbxDataInicio.Text.ToSqlDateTime();
        a.DataFim = tbxDataFim.Text.ToSqlDateTime().AddHours(23).AddMinutes(59).AddSeconds(59);
        a.Descricao = Editor1.Content;
        a.AvisoComercial = ddlExibirEm.SelectedValue.ToInt32() == 1 ? false : true;
        a.Salvar();

        msg.CriarMensagem("Aviso salvo com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    private void CarregarAviso()
    {
        Aviso a = Aviso.ConsultarUltimoAvisoSistema(false);
        if (a != null)
        {
            tbxDataInicio.Text = a.DataInicio.EmptyToMinValue();
            tbxDataFim.Text = a.DataFim.EmptyToMinValue();
            Editor1.Content = a.Descricao;
            ddlExibirEm.SelectedValue = a.AvisoComercial ? "2" : "1";
        }
    }
    #endregion
}