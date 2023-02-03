using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioRevendas : PageBase
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
        this.CarregarEstados(ddlEstadoRevendas);
        this.CarregarCidades(ddlEstadoRevendas, ddlCidadesRevendas);
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

    private void CarregarRelatorioRevendas()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<Revenda> revendas = Revenda.FiltrarRelatorio(Estado.ConsultarPorId(ddlEstadoRevendas.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidadesRevendas.SelectedValue.ToInt32()), ddlTipoParceiro.SelectedIndex > 0 ? ddlTipoParceiro.SelectedItem.Text : "");

        if (ddlStatusRevendas.SelectedValue.ToInt32() > 0)
            this.RemoverRevendasDeOutrosStatus(revendas, ddlStatusRevendas.SelectedValue.ToInt32() == 1);

        grvRelatorio.DataSource = revendas != null && revendas.Count > 0 ? revendas : new List<Revenda>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoEstado = ddlEstadoRevendas.SelectedIndex != 0 ? ddlEstadoRevendas.SelectedItem.Text.Trim() : "Todos";
        string descricaoCidade = ddlCidadesRevendas.SelectedIndex > 0 ? ddlCidadesRevendas.SelectedItem.Text.Trim() : "Todos";
        string descricaoStatus = ddlStatusRevendas.SelectedIndex > 0 ? ddlStatusRevendas.SelectedItem.Text.Trim() : "Todos";
        string descricaoTipoParceria = ddlTipoParceiro.SelectedIndex > 0 ? ddlTipoParceiro.SelectedItem.Text : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Tipo de Parceria", descricaoTipoParceria);
        CtrlHeader.InsertFiltroEsquerda("Status", descricaoStatus);

        CtrlHeader.InsertFiltroCentro("Estado", descricaoEstado);


        CtrlHeader.InsertFiltroDireita("Cidade", descricaoCidade);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Revendas");

        RelatorioUtil.OcultarFiltros(this.Page);
    }

    private void RemoverRevendasDeOutrosStatus(IList<Revenda> revendas, bool status)
    {
        if (revendas != null && revendas.Count > 0)
        {
            for (int i = revendas.Count - 1; i > -1; i--)
            {
                if (revendas[i] != null && revendas[i].Ativo != status)
                    revendas.Remove(revendas[i]);
            }
        }
    }

    #endregion

    #region ___________________Eventos___________________

    protected void ddlEstadoRevendas_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstadoRevendas, ddlCidadesRevendas);
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
            this.CarregarRelatorioRevendas();
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