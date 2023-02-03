using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioVendasPorCidade : PageBase
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
        this.CarregarRevendas(ddlVendasRevendaCidade);
        this.CarregarEstados(ddlVendasEstados);
        this.CarregarCidades(ddlVendasEstados, ddlVendasCidade);
    }

    private void CarregarRevendas(DropDownList dropRevendas)
    {
        dropRevendas.DataValueField = "Id";
        dropRevendas.DataTextField = "Nome";
        dropRevendas.DataSource = Revenda.ConsultarTodosOrdemAlfabetica();
        dropRevendas.DataBind();
        dropRevendas.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarEstados(DropDownList dropEstado)
    {
        dropEstado.DataValueField = "Id";
        dropEstado.DataTextField = "Nome";
        dropEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        dropEstado.DataBind();
        dropEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarCidades(DropDownList dropEstado, DropDownList dropCidade)
    {
        Estado estado = Estado.ConsultarPorId(dropEstado.SelectedValue.ToInt32());
        dropCidade.DataValueField = "Id";
        dropCidade.DataTextField = "Nome";
        dropCidade.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        dropCidade.DataBind();
        dropCidade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarRelatorioVendasPorCidade()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Revenda", typeof(string)));
        dt.Columns.Add(new DataColumn("Cidade", typeof(string)));
        dt.Columns.Add(new DataColumn("Estado", typeof(string)));
        dt.Columns.Add(new DataColumn("QtdVendas", typeof(string))); 

        DateTime dataDe = this.ConverterDataValida(tbxVendaDataDe.Text);
        DateTime dataAte = this.ConverterDataValidaAte(tbxVendaDataAte.Text);

        IList<Venda> vendas = Venda.Filtrar(ddlVendasRevendaCidade.SelectedValue.ToInt32(), ddlVendasEstados.SelectedValue.ToInt32(),
            ddlVendasCidade.SelectedValue.ToInt32(), dataDe, dataAte);

        string revenda = "";
        string cidade = "";
        string estado = "";
        int quantidadeVendas = 0;

        if (vendas != null && vendas.Count > 0)
        {
            foreach (Venda venda in vendas)
            {

                if (venda.Prospecto != null)
                {
                    Prospecto props = venda.Prospecto;
                    revenda = props.Revenda != null ? props.Revenda.Nome : "";
                    if (props.Endereco != null)
                    {
                        cidade = props.Endereco != null && props.Endereco.Cidade != null ? props.Endereco.Cidade.Nome : "";
                        estado = props.Endereco != null && props.Endereco.Cidade != null && props.Endereco.Cidade.Estado != null ? props.Endereco.Cidade.Estado.Nome : "";
                    }
                    IList<Venda> vendasAux = vendas.Where(ven => ven.Prospecto != null && ven.Prospecto.Endereco != null && ven.Prospecto.Endereco.Cidade != null && ven.Prospecto.Endereco.Cidade.Nome == cidade).ToList();
                    quantidadeVendas = cidade != "" && vendasAux != null ? vendasAux.Count : 0;
                }               

                dr = dt.NewRow();
                dr["Revenda"] = revenda;
                dr["Cidade"] = cidade;
                dr["Estado"] = estado;
                dr["QtdVendas"] = quantidadeVendas.ToString();
                dt.Rows.Add(dr);
            }
        }

        ViewState["CurrentTable"] = dt;

        grvRelatorio.DataSource = dt;
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoRevenda = ddlVendasRevendaCidade.SelectedIndex != 0 ? ddlVendasRevendaCidade.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAte);
        string descricaoEstado = ddlVendasEstados.SelectedIndex != 0 ? ddlVendasEstados.SelectedItem.Text.Trim() : "Todos";
        string descricaoCidade = ddlVendasCidade.SelectedIndex > 0 ? ddlVendasCidade.SelectedItem.Text.Trim() : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Revenda", descricaoRevenda);
        CtrlHeader.InsertFiltroEsquerda("Data", descricaoDataDeReferência);

        CtrlHeader.InsertFiltroCentro("Estado", descricaoEstado);

        CtrlHeader.InsertFiltroDireita("Cidade", descricaoCidade);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Vendas por Cidades");

        RelatorioUtil.OcultarFiltros(this.Page);
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

    #endregion

    #region ___________________Eventos___________________

    protected void ddlVendaEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlVendasEstados, ddlVendasCidade);
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

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioVendasPorCidade();
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