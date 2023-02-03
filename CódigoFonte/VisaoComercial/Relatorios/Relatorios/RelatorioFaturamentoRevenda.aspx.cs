using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioFaturamentoRevenda : PageBase
{
    private Msg msg = new Msg();
    decimal valorFaturamento = 0;
    decimal valorComissao = 0;

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
        this.CarregarRevendas(ddlFaturamentoRevenda);
    }

    private void CarregarRevendas(DropDownList dropRevendas)
    {
        dropRevendas.DataValueField = "Id";
        dropRevendas.DataTextField = "Nome";
        dropRevendas.DataSource = Revenda.ConsultarTodosOrdemAlfabetica();
        dropRevendas.DataBind();
        dropRevendas.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarAnos()
    {
        int anoAtual = DateTime.Now.Year + 1;
        for (int i = anoAtual - 10; i < anoAtual; i++)            
            ddlFaturamentoAno.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));        
    }

    private void CarregarMeses()
    {
        for (int i = 1; i < 13; i++)
        {
            string mes = new DateTime(2000, i, 1).ToString("MMMM").Capitalizar();            
            ddlFaturamentoMes.Items.Insert(0, new ListItem(mes, i.ToString()));
        }
        
        ddlFaturamentoMes.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public string BindFaturamento(object o)
    {
        Revenda revenda = (Revenda)o;

        int ano = ddlFaturamentoAno.SelectedValue.ToInt32();
        int mes = ddlFaturamentoMes.SelectedValue.ToInt32();

        this.valorFaturamento = 0;
        this.valorComissao = 0;

        if (revenda.Prospectos != null && revenda.Prospectos.Count > 0)
        {
            foreach (Prospecto pros in revenda.Prospectos)
            {
                if (pros.Venda != null)
                {
                    if (mes == 0)
                    {
                        foreach (Mensalidade mens in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano))
                        {
                            this.valorFaturamento += mens.GetValorMensalidade;
                            this.valorComissao += mens.GetValorRevenda;
                        }
                    }
                    else
                    {
                        foreach (Mensalidade mens in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano && ms.Mes == mes))
                        {
                            this.valorFaturamento += mens.GetValorMensalidade;
                            this.valorComissao += mens.GetValorRevenda;
                        }
                    }
                }
            }
        }

        return this.valorFaturamento.ToString("N2");
    }

    public string BindComissao(object o)
    {
        Revenda revenda = (Revenda)o;
        return this.valorComissao.ToString("N2");
    }

    public string BindFaturamentoLiquido(object o)
    {
        Revenda revenda = (Revenda)o;
        return (this.valorFaturamento - this.valorComissao).ToString("N2");
    }

    private void CarregarRelatorioFaturamentoPorRevenda()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        int ano = ddlFaturamentoAno.SelectedValue.ToInt32();
        int mes = ddlFaturamentoMes.SelectedValue.ToInt32();
        IList<Revenda> revendas = Revenda.ConsultarFaturamento(Revenda.ConsultarPorId(ddlFaturamentoRevenda.SelectedValue.ToInt32()), mes, ano);

        grvRelatorio.DataSource = revendas != null && revendas.Count > 0 ? revendas : new List<Revenda>();
        grvRelatorio.DataBind();        

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoRevenda = ddlFaturamentoRevenda.SelectedIndex != 0 ? ddlFaturamentoRevenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoMes = ddlFaturamentoMes.SelectedIndex != 0 ? ddlFaturamentoMes.SelectedItem.Text.Trim() : "Todos";
        string descricaoAno = ddlFaturamentoAno.SelectedItem.Text.Trim();

        CtrlHeader.InsertFiltroEsquerda("Revenda", descricaoMes);

        CtrlHeader.InsertFiltroCentro("Ano", descricaoAno);

        CtrlHeader.InsertFiltroDireita("Mês", descricaoMes);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Faturamento Por Revenda");

        RelatorioUtil.OcultarFiltros(this.Page); 
    }

    #endregion

    #region ___________________Eventos___________________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioFaturamentoPorRevenda();
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