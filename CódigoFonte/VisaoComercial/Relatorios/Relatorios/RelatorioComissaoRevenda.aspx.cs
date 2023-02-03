using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioComissaoRevenda : PageBase
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
        this.CarregarAnos();
        this.CarregarMeses();
    }

    private void CarregarAnos()
    {
        int anoAtual = DateTime.Now.Year + 1;
        for (int i = anoAtual - 10; i < anoAtual; i++)
        {
            ddlComissaoAnoRevenda.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void CarregarMeses()
    {
        for (int i = 1; i < 13; i++)
        {
            string mes = new DateTime(2000, i, 1).ToString("MMMM").Capitalizar();
            ddlComissaoMesRevenda.Items.Insert(0, new ListItem(mes, i.ToString()));
        }
        ddlComissaoMesRevenda.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public string BindTotalComissaoRevenda(object o)
    {
        Revenda revenda = (Revenda)o;

        decimal valorTotal = 0;

        if (revenda.Prospectos != null && revenda.Prospectos.Count > 0)
        {
            int ano = ddlComissaoAnoRevenda.SelectedValue.ToInt32();
            int mes = ddlComissaoMesRevenda.SelectedValue.ToInt32();

            foreach (Prospecto pros in revenda.Prospectos)
            {
                if (pros.Venda != null)
                {
                    if (mes == 0)
                        foreach (Mensalidade mens in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano))
                            valorTotal += mens.GetValorRevenda;
                    else
                        foreach (Mensalidade mens in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano && ms.Mes == mes))
                            valorTotal += mens.GetValorRevenda;
                }
            }
        }

        return valorTotal.ToString("N2");

    }

    private void CarregarRelatorioComissoesRevendas()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        int ano = ddlComissaoAnoRevenda.SelectedValue.ToInt32();
        int mes = ddlComissaoMesRevenda.SelectedValue.ToInt32();
        IList<Revenda> revendas = Revenda.ConsultarComissao(ddlComissaoMesRevenda.SelectedValue.ToInt32(), ddlComissaoAnoRevenda.SelectedValue.ToInt32());

        grvRelatorio.DataSource = revendas != null && revendas.Count > 0 ? revendas : new List<Revenda>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoMes = ddlComissaoMesRevenda.SelectedIndex != 0 ? ddlComissaoMesRevenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoAno = ddlComissaoAnoRevenda.SelectedItem.Text.Trim();

        CtrlHeader.InsertFiltroEsquerda("Mês", descricaoMes);

        CtrlHeader.InsertFiltroCentro("Ano", descricaoAno);        

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Comissões Por Revenda");

        RelatorioUtil.OcultarFiltros(this.Page);        
    }

    #endregion

    #region ___________________Eventos___________________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioComissoesRevendas();
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