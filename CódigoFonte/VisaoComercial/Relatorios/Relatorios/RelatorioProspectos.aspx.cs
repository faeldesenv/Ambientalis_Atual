using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioProspectos : PageBase
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
        this.CarregarEstados(ddlEstadoProspectos);
        this.CarregarRevendas(ddlRevendaProspectos);
        this.CarregarCidades(ddlEstadoProspectos, ddlCidadeProspectos);
    }

    private void CarregarEstados(DropDownList dropEstado)
    {
        dropEstado.DataValueField = "Id";
        dropEstado.DataTextField = "Nome";
        dropEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        dropEstado.DataBind();
        dropEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRevendas(DropDownList dropRevendas)
    {
        dropRevendas.DataValueField = "Id";
        dropRevendas.DataTextField = "Nome";
        dropRevendas.DataSource = Revenda.ConsultarTodosOrdemAlfabetica();
        dropRevendas.DataBind();
        dropRevendas.Items.Insert(0, new ListItem("-- Todas --", "0"));
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

    private void CarregarRelatorioProspectos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = this.ConverterDataValida(tbxDataCadastroProspectosDe.Text);
        DateTime dataAteh = this.ConverterDataValidaAte(tbxDataCadastroProspectosAte.Text);

        IList<Prospecto> prospectos = Prospecto.FiltrarRelatorio(Revenda.ConsultarPorId(ddlRevendaProspectos.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidadeProspectos.SelectedValue.ToInt32()), Estado.ConsultarPorId(ddlEstadoProspectos.SelectedValue.ToInt32()), dataDe, dataAteh, ddlStatus.SelectedValue.ToInt32()); //ddlStatus: 1 para true e 2 para false

        grvRelatorio.DataSource = prospectos != null && prospectos.Count > 0 ? prospectos : new List<Prospecto>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoRevenda = ddlRevendaProspectos.SelectedIndex != 0 ? ddlRevendaProspectos.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAteh);
        string descricaoEstado = ddlEstadoProspectos.SelectedIndex != 0 ? ddlEstadoProspectos.SelectedItem.Text.Trim() : "Todos";
        string descricaoCidade = ddlCidadeProspectos.SelectedIndex > 0 ? ddlCidadeProspectos.SelectedItem.Text.Trim() : "Todos";
        string descricaoStatus = ddlStatus.SelectedIndex > 0 ? ddlStatus.SelectedItem.Text.Trim() : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Revenda", descricaoRevenda);
        CtrlHeader.InsertFiltroEsquerda("Data de Cadastro", descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência);

        CtrlHeader.InsertFiltroCentro("Estado", descricaoEstado);
        CtrlHeader.InsertFiltroCentro("Cidade", descricaoCidade);

        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Prospectos");

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

    protected void ddlEstadoProspectos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstadoProspectos, ddlCidadeProspectos);
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
            this.CarregarRelatorioProspectos();
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