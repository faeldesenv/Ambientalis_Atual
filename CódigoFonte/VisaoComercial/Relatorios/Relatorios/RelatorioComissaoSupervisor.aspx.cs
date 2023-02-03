using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioComissaoSupervisor : PageBase
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
            ddlComissaoAnoSupervisor.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void CarregarMeses()
    {
        for (int i = 1; i < 13; i++)
        {
            string mes = new DateTime(2000, i, 1).ToString("MMMM").Capitalizar();
            ddlComissaoMesSupervisor.Items.Insert(0, new ListItem(mes, i.ToString()));
        }
        ddlComissaoMesSupervisor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRelatorioComissoesSupervisor()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        int ano = ddlComissaoAnoSupervisor.SelectedValue.ToInt32();
        int mes = ddlComissaoMesSupervisor.SelectedValue.ToInt32();
        IList<Revenda> revendas = Revenda.ConsultarComissao(mes, ano);

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Revenda", typeof(string)));
        dt.Columns.Add(new DataColumn("Venda", typeof(string)));
        dt.Columns.Add(new DataColumn("ComissaoSupervisor", typeof(string)));    
        decimal valorTotal = 0;

        if (revendas.Count > 0)
        {
            foreach (Revenda revend in revendas)
            {
                
                foreach (Prospecto pros in revend.Prospectos)
                {
                    if (pros.Venda != null)
                    {
                        decimal valorComissao = 0;
                        if (mes == 0)
                            foreach (Mensalidade item in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano))
                                valorComissao += item.GetValorSupervisor;
                        else
                            foreach (Mensalidade item in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano && ms.Mes == mes))
                                valorComissao += item.GetValorSupervisor;

                        valorTotal += valorComissao;

                        dr = dt.NewRow();
                        dr["Revenda"] = revend.Nome;
                        dr["Venda"] = pros.Nome;
                        dr["ComissaoSupervisor"] = valorComissao.ToString("0.00");                        
                        dt.Rows.Add(dr);
                    }
                }
            }            
        }

        ViewState["CurrentTable"] = dt;

        grvRelatorio.DataSource = dt;
        grvRelatorio.DataBind();

        lblTotal.Text = valorTotal.ToString("N2");

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoMes = ddlComissaoMesSupervisor.SelectedIndex != 0 ? ddlComissaoMesSupervisor.SelectedItem.Text.Trim() : "Todos";
        string descricaoAno = ddlComissaoAnoSupervisor.SelectedItem.Text.Trim();

        CtrlHeader.InsertFiltroEsquerda("Mês", descricaoMes);

        CtrlHeader.InsertFiltroCentro("Ano", descricaoAno);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Comissão Supervisor");

        RelatorioUtil.OcultarFiltros(this.Page); 
        
    }

    #endregion

    #region ___________________Eventos___________________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioComissoesSupervisor();
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