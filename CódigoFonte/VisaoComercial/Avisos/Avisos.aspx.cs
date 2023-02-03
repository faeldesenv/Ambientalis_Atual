using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Avisos_Avisos : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.ValidaUsuario();
                this.CarregarAviso();
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

    #region ________ Eventos ___________

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {            
            this.SalvarAviso();
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

    #region ________ Métodos ___________

    private void SalvarAviso()
    {
        if (ddlExibirEm.SelectedValue.ToInt32() == 0)
        {
            msg.CriarMensagem("Selecione o local de exibição do aviso", "Alerta", MsgIcons.Alerta);
            return;
        }

        Aviso a = Aviso.ConsultarUltimoAvisoSistema(true);
        if (a == null)
        {
            a = new Aviso();
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
        Aviso a = Aviso.ConsultarUltimoAvisoSistema(true);
        if (a != null)
        {
            tbxDataInicio.Text = a.DataInicio.EmptyToMinValue();
            tbxDataFim.Text = a.DataFim.EmptyToMinValue();
            Editor1.Content = a.Descricao;
            ddlExibirEm.SelectedValue = a.AvisoComercial ? "2" : "1";
        }
    }

    private bool ValidaUsuario()
    {
        UsuarioComercial user = ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]);
        if (user.GetType() == typeof(UsuarioRevendaComercial))
        {
            Response.Redirect("../Site/Index.aspx", false);
            return false;
        }
        return true;
    }

    #endregion
}