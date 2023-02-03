using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Vendas_Listagem : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarRevendas();
                this.CarregarEstados();
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

    private void CarregarEstados()
    {
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = Estado.ConsultarTodos();
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRevendas()
    {
        ddlRevenda.DataValueField = "Id";
        ddlRevenda.DataTextField = "Nome";
        ddlRevenda.DataSource = Revenda.ConsultarTodos();
        ddlRevenda.DataBind();
        ddlRevenda.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e1)
    {
        try
        {
            Estado e = Estado.ConsultarPorId(ddlEstado.SelectedValue.ToInt32());
            ddlCidade.DataValueField = "Id";
            ddlCidade.DataTextField = "Nome";
            ddlCidade.DataSource = e != null ? e.Cidades != null ? e.Cidades : new List<Cidade>() : new List<Cidade>();
            ddlCidade.DataBind();
            ddlCidade.Items.Insert(0, new ListItem("-- Todos --", "0"));
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
        IList<Venda> vendas = Venda.Filtrar(ddlRevenda.SelectedValue.ToInt32(), ddlEstado.SelectedValue.ToInt32(), ddlCidade.SelectedValue.ToInt32(),
            tbxDataDe.Text.IsDate() ? tbxDataDe.Text.ToSqlDateTime() : SqlDate.MinValue, tbxDataAte.Text.IsDate() ? tbxDataAte.Text.ToSqlDateTime() : SqlDate.MaxValue);
        dgr.DataSource = vendas;
        dgr.DataBind();
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
        return v != null ? v.Prospecto != null ? v.Prospecto.DadosPessoa != null ?
            v.Prospecto.DadosPessoa.GetType() == typeof(DadosFisicaComercial) ? ((DadosFisicaComercial)v.Prospecto.DadosPessoa).Cpf :
            ((DadosJuridicaComercial)v.Prospecto.DadosPessoa).Cnpj : "" : "" : "";
    }

    public string BindCadastro(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Prospecto != null ? v.Prospecto.DataCadastro.EmptyToMinValue() : "" : "";
    }

    public string BindData(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Data.EmptyToMinValue() : "";
    }

    public string BindComissao(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Prospecto != null ? v.Prospecto.Revenda != null ? v.Prospecto.Revenda.Contratos != null ? v.Prospecto.Revenda.Contratos.Count > 0 ?
            v.Prospecto.Revenda.Contratos[v.Prospecto.Revenda.Contratos.Count - 1].Comissao.ToString("N2") : "" : "" : "" : "" : "";
    }

    public string BindSituacao(Object o)
    {
        Venda v = (Venda)o;
        return v != null ? v.Cancelado ? "Cancelada" : "Ativa" : "";
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

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dgr.PageSize = ddlQuantidaItensGrid.SelectedValue.ToInt32();
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