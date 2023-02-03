using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioVendas : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunasComercial(grvRelatorio, ckbColunas, this.Page);
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Alert.Show(msg.Mensagem);
            }
    }

    #region ___________________Métodos___________________

    private void CarregarCampos()
    {
        this.CarregarRevendas(ddlRevendaVenda);
    }

    private void CarregarRevendas(DropDownList dropRevendas)
    {
        dropRevendas.DataValueField = "Id";
        dropRevendas.DataTextField = "Nome";
        dropRevendas.DataSource = Revenda.ConsultarTodosOrdemAlfabetica();
        dropRevendas.DataBind();
        dropRevendas.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarRelatorioVendas()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = this.ConverterDataValida(tbxVendaDataCadastroDe.Text);
        DateTime dataAte = this.ConverterDataValidaAte(tbxVendaDataCadastroAte.Text);

        IList<Venda> vendas = Venda.FiltrarRelatorio(Revenda.ConsultarPorId(ddlRevendaVenda.SelectedValue.ToInt32()), dataDe, dataAte, ddlVendastatus.SelectedValue.ToInt32());

        grvRelatorio.DataSource = vendas != null && vendas.Count > 0 ? vendas : new List<Venda>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoRevenda = ddlRevendaVenda.SelectedIndex != 0 ? ddlRevendaVenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAte);
        string descricaoStatus = ddlVendastatus.SelectedIndex > 0 ? ddlVendastatus.SelectedItem.Text.Trim() : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Revenda", descricaoRevenda);

        CtrlHeader.InsertFiltroCentro("Data", descricaoDataDeReferência);

        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Vendas");

        RelatorioUtil.OcultarFiltros(this.Page);
    }

    private string GetDescricaoData(DateTime dataDe, DateTime dataAteh)
    {
        string retorno = "Não definido";
        if (dataDe.CompareTo(DateTime.MinValue) > 0 || dataAteh.CompareTo(DateTime.MaxValue) < 0)
        {
            if (dataDe.CompareTo(DateTime.MinValue) > 0 && dataAteh.CompareTo(DateTime.MaxValue) < 0)
                retorno = "de " + dataDe.ToShortDateString() + " até " + dataAteh.ToShortDateString();
            else
                retorno = dataDe.CompareTo(DateTime.MinValue) > 0 ? "após " + dataDe.ToShortDateString() : "antes de " + dataAteh.ToShortDateString();
        }
        return retorno;
    }

    private DateTime ConverterDataValida(string data)
    {
        return data != string.Empty ? Convert.ToDateTime(data).ToMinHourOfDay() > SqlDate.MinValue ?
            Convert.ToDateTime(data).ToMinHourOfDay() : SqlDate.MinValue : SqlDate.MinValue;
    }

    private DateTime ConverterDataValidaAte(string data)
    {
        return data != string.Empty ? Convert.ToDateTime(data).ToMinHourOfDay() > SqlDate.MinValue ?
            Convert.ToDateTime(data).ToMinHourOfDay() : SqlDate.MaxValue : SqlDate.MaxValue;
    }

    #endregion

    #region ___________________Eventos___________________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioVendas();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Alert.Show(msg.Mensagem);
        }
    }    

    #endregion
}