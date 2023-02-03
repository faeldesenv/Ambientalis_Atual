using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Drawing;

public partial class Vendas_Comissoes : PageBase
{

    Msg msg = new Msg();
    private Dictionary<int, decimal> totalGeral = new Dictionary<int, decimal>();

    #region ______________ TRANSAÇÕES _________________
    Transacao transacao = new Transacao();
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = 0;
            transacao.Abrir();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        transacao.Fechar(ref msg);
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        transacao.Fechar(ref msg);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarRevendas();
                this.CarregarDDLAno();
                this.Pesquisar();
            }
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

    private void CarregarDDLAno()
    {
        ddlAno.Items.Clear();
        ddlAno.Items.Add(new ListItem("2012", "2012"));
        ddlAno.Items.Add(new ListItem("2013", "2013"));
        ddlAno.Items.Add(new ListItem("2014", "2014"));
        ddlAno.Items.Add(new ListItem("2015", "2015"));
    }

    private void CarregarRevendas()
    {
        ddlRevenda.DataValueField = "Id";
        ddlRevenda.DataTextField = "Nome";
        ddlRevenda.DataSource = Revenda.ConsultarTodos();
        ddlRevenda.DataBind();
        ddlRevenda.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar();
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

    private void Pesquisar()
    {
        IList<Venda> vendas = Venda.FiltrarPorRevenda(ddlRevenda.SelectedValue.ToInt32());

        IList<Venda> vendasAux = new List<Venda>();

        foreach (Venda v in vendas)
        {

            for (int i = 1; i <= 12; i++)
            {
                DateTime data = new DateTime(ddlAno.SelectedValue.ToInt32(), i, 1);
                Mensalidade m = v.Mensalidades.FirstOrDefault(ii => ii.Ano == data.Year && ii.Mes == data.Month);
                if (m != null)
                {
                    decimal val = 0;
                    if (m.Id == v.Mensalidades[v.Mensalidades.Count - 1].Id && !m.GetCancelada)
                    {
                        val = this.GetValorContrato(v);
                    }
                    else
                    {
                        if (ddlComissao.SelectedValue.ToUpper() == "REVENDA")
                            val = m.GetValorRevenda;
                        else if (ddlComissao.SelectedValue.ToUpper() == "SUPERVISOR")
                            val = m.GetValorSupervisor;
                    }

                    if (!this.totalGeral.ContainsKey(i))
                        this.totalGeral.Add(i, val);
                    else
                        this.totalGeral[i] += val;
                }
                else if (v.Data < new DateTime(ddlAno.SelectedValue.ToInt32(), i, 1) && !v.Cancelado)
                {
                    this.totalGeral.Add(i, this.GetValorContrato(v));
                }
            }

            if (v.Data.Year <= ddlAno.SelectedValue.ToInt32())
                vendasAux.Add(v);

        }

        dgr.DataSource = vendasAux;
        dgr.DataBind();
        this.PintarGrid();
    }

    private void PintarGrid()
    {
        foreach (DataGridItem item in dgr.Items)
        {
            for (int x = 0; x < item.Cells.Count; x++)
            {
                foreach (Control control in item.Cells[x].Controls)
                {
                    if (control.GetType() == typeof(CheckBox))
                    {
                        if (((CheckBox)control).Checked)
                        {
                            item.Cells[x].BackColor = Color.Green;
                            item.Cells[x].ForeColor = Color.White;
                        }
                    }
                }
            }
        }
    }

    public string BindRevenda(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Prospecto != null ? v.Prospecto.Revenda != null ? v.Prospecto.Revenda.Nome : "" : "" : "";
    }

    public string BindCliente(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Prospecto != null ? v.Prospecto.Nome : "" : "";
    }

    public string BindCPF(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Prospecto != null ?
            v.Prospecto.DadosPessoa.GetType() == typeof(DadosFisicaComercial) ? ((DadosFisicaComercial)v.Prospecto.DadosPessoa).Cpf :
            ((DadosJuridicaComercial)v.Prospecto.DadosPessoa).Cnpj : "" : "";
    }

    public bool BindPagoVisivel(Object o, int mes)
    {
        string x = BindMeses(o, mes);
        if (x == "" || x.Replace("R$", "").Replace(",", "").Trim().ToInt32() == 0)
            return false;
        else
            return true;
    }

    public bool BindPago(Object o, int mes)
    {
        Venda v = (Venda)o;
        DateTime data = new DateTime(ddlAno.SelectedValue.ToInt32(), mes, 1);
        Mensalidade mensalidade = v.Mensalidades.FirstOrDefault(i => i.Ano == data.Year && i.Mes == data.Month);
        if (mensalidade != null)
        {
            if (mensalidade.PagoRevenda && ddlComissao.SelectedValue.ToUpper() == "REVENDA")
            {
                return true;
            }
            else if (mensalidade.PagoSupervisor && ddlComissao.SelectedValue.ToUpper() == "SUPERVISOR")
            {
                return true;
            }
        }
        return false;
    }

    public string BindMeses(Object o, int mes)
    {
        Venda v = (Venda)o;
        DateTime data = new DateTime(ddlAno.SelectedValue.ToInt32(), mes, 1);

        Mensalidade mensalidade = v.Mensalidades.FirstOrDefault(i => i.Ano == data.Year && i.Mes == data.Month);
        if (mensalidade != null)
        {
            decimal val = 0;
            bool ultimoMes = false;

            if (mensalidade.Id == v.Mensalidades[v.Mensalidades.Count - 1].Id && !mensalidade.GetCancelada)
            {
                ultimoMes = true;                
                if (ddlComissao.SelectedValue.ToUpper() == "REVENDA")
                {
                    val = this.GetValorContrato(v);
                }
                else if (ddlComissao.SelectedValue.ToUpper() == "SUPERVISOR")
                {
                    val = mensalidade.GetValorSupervisor;
                }
            }
            else
            {
                if (ddlComissao.SelectedValue.ToUpper() == "REVENDA")
                {
                    val = mensalidade.GetValorRevenda;
                }
                else if (ddlComissao.SelectedValue.ToUpper() == "SUPERVISOR")
                {
                    val = mensalidade.GetValorSupervisor;
                }
            }

            if (mensalidade.GetCancelada)
                return "Cancelada";

            if (ultimoMes && v.Data.Day != 1)
                return "R$ " + val.ToString("N2") + "*";
            else
                return "R$ " + val.ToString("N2");
        }

        if ((mensalidade == null && v.Cancelado) || (mensalidade == null && data.Date < new DateTime(v.Data.Year, v.Data.Month, 1)) || (v.Mensalidades == null || v.Mensalidades.Count == 0))
            return "";


        if (mensalidade == null && data.Date >= new DateTime(v.Data.Year, v.Data.Month, 1))
        {
            if (!v.Cancelado)
            {
                return "R$" + this.GetValorContrato(v).ToString("N2") + "*";
            }
            return "Cancelada";
        }


        return "nda";
    }

    public decimal GetValorContrato(Venda v)
    {
        if (Session["idEmp"] != null)
        {
            string emp = Session["idEmp"].ToString().Trim();
            try
            {
                Session["idEmp"] = null;
                IList<Contrato> cons = v.GetGrupoEconomico.Contratos;
                if (ddlComissao.SelectedValue.ToUpper() == "REVENDA")
                {
                    return (cons[cons.Count - 1].Mensalidade * (v.Prospecto.Revenda.GetUltimoContrato.Comissao / 100));
                }
                else
                {
                    if (v.Prospecto.Endereco.Cidade == null)
                        return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.02));

                    if (v.Prospecto.Endereco.Cidade.Estado.Nome.Trim().ToUpper() == "ESPÍRITO SANTO")
                        return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.05));
                    else
                        return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.02));
                }
               
            }
            catch (Exception)
            { }
            finally
            {
                Session["idEmp"] = emp;
            }
        }
        else
        {
            IList<Contrato> cons = v.GetGrupoEconomico.Contratos;
            if (ddlComissao.SelectedValue.ToUpper() == "REVENDA")
            {
                return (cons[cons.Count - 1].Mensalidade * (v.Prospecto.Revenda.GetUltimoContrato.Comissao / 100));
            }
            else
            {
                if (v.Prospecto.Endereco.Cidade == null)
                    return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.02));

                if (v.Prospecto.Endereco.Cidade.Estado.Nome.Trim().ToUpper() == "ESPÍRITO SANTO")
                    return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.05));
                else
                    return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.02));
            }
        }
        return 0;
    }

    public string BindTotalGeral(int mes)
    {
        return "R$ " + (this.totalGeral.ContainsKey(mes) ? totalGeral[mes].ToString("N2") : decimal.Zero.ToString("N2"));
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgr.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Salvar();
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

    public void Salvar()
    {
        foreach (DataGridItem item in dgr.Items)
        {
            Venda venda = Venda.ConsultarPorId(item.Cells[0].Text.ToInt32());

            foreach (Mensalidade mensalidade in venda.Mensalidades)
            {
                if (mensalidade.Ano == ddlAno.SelectedValue.ToInt32())
                {
                    foreach (Control control in item.Cells[mensalidade.Mes + 2].Controls)
                    {
                        if (control.GetType() == typeof(CheckBox))
                        {
                            bool pago = ((CheckBox)control).Checked;
                            if (ddlComissao.SelectedValue.ToUpper() == "REVENDA")
                            {
                                mensalidade.PagoRevenda = pago;
                            }
                            else if (ddlComissao.SelectedValue.ToUpper() == "SUPERVISOR")
                            {
                                mensalidade.PagoSupervisor = pago;
                            }
                            mensalidade.Salvar();
                        }
                    }
                }
            }
        }
        msg.CriarMensagem("Comissões salvas com Sucesso!", "Sucesso");
    }
}