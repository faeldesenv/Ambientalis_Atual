using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Drawing;

public partial class Vendas_ComissoesSupervisor : PageBase
{
    Msg msg = new Msg();
    private Dictionary<int, decimal> totalGeral = new Dictionary<int, decimal>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!this.ValidaUsuario())
                    return;
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

    private bool ValidaUsuario()
    {
        UsuarioComercial user = ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]);
        if (user.GetType() == typeof(UsuarioRevendaComercial))
        {
            Response.Redirect("Comissoes.aspx", false);
            return false;
        }
        return true;
    }

    private void CarregarDDLAno()
    {
        ddlAno.Items.Clear();
        ddlAno.Items.Add(new ListItem("2012", "2012"));
        ddlAno.Items.Add(new ListItem("2013", "2013"));
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
        IList<Venda> vendas = Venda.FiltrarVendasDoSupervisor();

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
        }

        dgr.DataSource = vendas;
        dgr.DataBind();
        this.PintarGrid();
    }

    private void PintarGrid()
    {
        foreach (DataGridItem item in dgr.Items)
        {
            Venda venda = Venda.ConsultarPorId(item.Cells[0].Text.ToInt32());
            for (int x = 0; x < item.Cells.Count; x++)
            {
                foreach (Mensalidade mensalidade in venda.Mensalidades)
                {
                    if (mensalidade.Ano == ddlAno.SelectedValue.ToInt32() && mensalidade.Mes == (x - 3))
                    {
                        if (mensalidade.PagoSupervisor)
                        {
                            item.Cells[x].BackColor = Color.Green;
                            item.Cells[x].ForeColor = Color.White;
                        }
                    }
                }
            }
        }
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
                return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.02));
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
            return (cons[cons.Count - 1].Mensalidade * Convert.ToDecimal(0.02));
        }
        return 0;
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
                val = this.GetValorContrato(v);
            }
            else
            {
                val = mensalidade.GetValorSupervisor;
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
                return this.GetValorContrato(v).ToString("N2") + "*";
            }
            return "Cancelada";
        }


        return "nda";
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

}