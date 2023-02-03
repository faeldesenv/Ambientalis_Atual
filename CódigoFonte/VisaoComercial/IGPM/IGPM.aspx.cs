using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class IGMP_IGPM : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CarregarDatas();
            this.CarregarIGMPs(DateTime.Now.Year);
        }

    }

    #region ___________Metodos_____________

    private void CarregarDatas()
    {

        int menorAno = DateTime.Now.Year - 10;

        for (int x = menorAno; x <= DateTime.Now.Year + 1; x++)
        {
            ddlAno.Items.Insert(0, new ListItem(x.ToString(), x.ToString()));
        }

        ddlAno.SelectedValue = DateTime.Now.Year.ToString();
    }

    private void CarregarIGMPs()
    {
        this.CarregarIGMPs(0);
    }

    private void CarregarIGMPs(int ano)
    {
        IList<IGPMAcumulado> igpms = (ano > 0) ? IGPMAcumulado.FiltrarPorAno(ano) : IGPMAcumulado.ConsultarTodos();
        dgrValoresAcumuladosMes.DataSource = igpms;
        dgrValoresAcumuladosMes.DataBind();
    }

    #endregion

    #region ___________Bindings____________

    public string BindMes(Object obj)
    {
        IGPMAcumulado igmp = (IGPMAcumulado)obj;
        string data = igmp.Data.ToString("MMM/yyyy");
        return data;
    }

    public string BindValorAcumulado(Object obj)
    {
        IGPMAcumulado igmp = (IGPMAcumulado)obj;
        return igmp.GetValorIGPMAcumulados(igmp.Data).ToString("N5");
    }

    #endregion

    #region ___________Eventos_____________

    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarIGMPs(ddlAno.SelectedValue.ToInt32());
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

}