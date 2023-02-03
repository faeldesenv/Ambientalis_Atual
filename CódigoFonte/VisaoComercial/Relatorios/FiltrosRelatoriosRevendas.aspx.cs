using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Relatorios_FiltrosRelatoriosRevendas : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
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

    #region ___________Metodos_____________

    private void CarregarCampos()
    {
        //Prospectos
        this.CarregarRevendas(ddlRevendaProspectos);
        this.CarregarEstados(ddlEstadoProspectos);

        //Vendas
        this.CarregarRevendas(ddlRevendaVenda);
        this.CarregarRevendas(ddlVendasRevendaGrafico_1);
        this.CarregarRevendas(ddlVendasRevendaGrafico_2);
        this.CarregarRevendas(ddlVendasRevendaCidade);
        this.CarregarEstados(ddlVendasEstados);
        this.CarregarAnos();
        this.CarregarMeses();
    }

    private void CarregarAnos()
    {
        int anoAtual = DateTime.Now.Year + 1;
        for (int i = anoAtual - 10; i < anoAtual; i++)
        {
            ddlVendasAno_1.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            ddlVendasAno_2.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
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

    private void CarregarRevendas(DropDownList dropRevenda)
    {
        dropRevenda.DataValueField = "Id";
        dropRevenda.DataTextField = "Nome";
        dropRevenda.DataSource = Revenda.ConsultarTodosOrdemAlfabetica();
        dropRevenda.DataBind();
        dropRevenda.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarEstados(DropDownList dropEstado)
    {
        dropEstado.DataValueField = "Id";
        dropEstado.DataTextField = "Nome";
        dropEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        dropEstado.DataBind();
        dropEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void CarregarCidades(DropDownList dropEstado, DropDownList dropCidade)
    {
        Estado estado = Estado.ConsultarPorId(dropEstado.SelectedValue.ToInt32());
        dropCidade.DataValueField = "Id";
        dropCidade.DataTextField = "Nome";
        dropCidade.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        dropCidade.DataBind();
        dropCidade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarRelatorioSelecionado()
    {
        switch (mtvFiltrosRelatorios.ActiveViewIndex)
        {
            case 1: this.CarregarRelatorioProspectos();
                break;

            case 2: this.CarregarRelatoriosVendas();
                break;

            case 3:
                this.CarregarRelatorioVendasGrafico("B");
                break;

            case 4:
                this.CarregarRelatorioVendasGrafico("P");
                break;

            case 5:
                this.CarregarRelatorioVendasCidade();
                break;

            case 6:
                this.CarregarRelatorioComissaoRevenda();
                break;

            default:
                msg.CriarMensagem("Selecione algum relatório", "Informação", MsgIcons.Informacao);
                break;
        }
    }

    private void CarregarRelatoriosVendas()
    {
        DateTime dataDe = this.ConverterDataValida(tbxVendaDataCadastroDe.Text);
        DateTime dataAte = this.ConverterDataValida(tbxVendaDataCadastroAte.Text);

        IList<Venda> vendas = Venda.FiltrarRelatorio(Revenda.ConsultarPorId(ddlRevendaVenda.SelectedValue.ToInt32()), dataDe, dataAte, ddlVendastatus.SelectedValue.ToInt32());

        string descricaoRevenda = ddlRevendaVenda.SelectedIndex != 0 ? ddlRevendaVenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAte);
        string descricaoStatus = ddlVendastatus.SelectedIndex > 0 ? ddlVendastatus.SelectedItem.Text.Trim() : "Todos";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        Fontes.relatoriosVendasDataTable fonte = new Fontes.relatoriosVendasDataTable();

        if (vendas.Count > 0)
        {
            foreach (Venda venda in vendas)
            {
                string revenda = "";
                string prospecto = "";
                string prospectoCpfCnpj = "";
                string prospectoData = "";
                string mensalidade = venda.GetValorMensalidadeAtual().ToString("0.00");
                if (venda.Prospecto != null)
                {
                    revenda = venda.Prospecto.Revenda != null ? venda.Prospecto.Revenda.Nome : "";
                    prospecto = venda.Prospecto.Nome;
                    prospectoCpfCnpj = venda.Prospecto.GetNumeroCNPJeCPFComMascara;
                    prospectoData = venda.Prospecto.DataCadastro.ToShortDateString();
                }
                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoRevenda, descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência, descricaoStatus, revenda, prospecto, prospectoCpfCnpj,
                    prospectoData, venda.Data.ToShortDateString(), mensalidade, URLLOgo);
            }
        }
        else
        {
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoRevenda, descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência, descricaoStatus, null, null, null,
                    null, null, null, URLLOgo);
        }


        Relatorios.CarregarRelatorio("Vendas", "Vendas", false, fonte);
    }

    private void CarregarRelatorioVendasGrafico(string tipo)
    {
        if (ddlVendasRevendaGrafico_1.SelectedValue.ToInt32() > 0 && tipo == "B")
        {
            Response.Redirect("RelatoriosGraficos.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idRev=" + ddlVendasRevendaGrafico_1.SelectedValue + "&ano=" + ddlVendasAno_1.SelectedValue + "&tipo=B"), false);
        }
        else if (ddlVendasRevendaGrafico_2.SelectedValue.ToInt32() > 0 && tipo == "P")
        {
            Response.Redirect("RelatoriosGraficos.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idRev=" + ddlVendasRevendaGrafico_2.SelectedValue + "&ano=" + ddlVendasAno_2.SelectedValue + "&tipo=P"), false);
        }
    }

    private void CarregarRelatorioVendasCidade()
    {
        DateTime dataDe = this.ConverterDataValida(tbxVendaDataDe.Text);
        DateTime dataAte = this.ConverterDataValida(tbxVendaDataAte.Text);

        IList<Venda> vendas = Venda.Filtrar(ddlVendasRevendaCidade.SelectedValue.ToInt32(), ddlVendasEstados.SelectedValue.ToInt32(),
            ddlVendasCidade.SelectedValue.ToInt32(), dataDe, dataAte);

        string descricaoRevenda = ddlRevendaVenda.SelectedIndex != 0 ? ddlRevendaVenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAte);
        string descricaoEstado = ddlVendasEstados.SelectedIndex != 0 ? ddlVendasEstados.SelectedItem.Text.Trim() : "Todos";
        string descricaoCidade = ddlVendasCidade.SelectedIndex > 0 ? ddlVendasCidade.SelectedItem.Text.Trim() : "Todos";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        Fontes.relatoriosVendasPorCidadeDataTable fonte = new Fontes.relatoriosVendasPorCidadeDataTable();

        string revenda = "";
        string cidade = "";
        string estado = "";
        int quantidadeVendas = 0;

        if (vendas.Count > 0)
        {
            foreach (Venda venda in vendas)
            {

                if (venda.Prospecto != null)
                {
                    Prospecto props = venda.Prospecto;
                    revenda = props.Revenda != null ? props.Revenda.Nome : "";
                    if (props.Endereco != null)
                    {
                        cidade = props.Endereco != null ? props.Endereco.Cidade.Nome : "";
                        estado = props.Endereco.Cidade.Estado.Nome;
                    }
                    quantidadeVendas = vendas.Where(ven => ven.Prospecto.Endereco.Cidade.Nome == cidade).ToList().Count;
                }
                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoRevenda, descricaoCidade, descricaoEstado, descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência,
                    revenda, cidade, estado, quantidadeVendas, URLLOgo);
            }
        }
        else
        {
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoRevenda, descricaoCidade, descricaoEstado, descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência,
                    null, null, null, 0, URLLOgo);
        }
        Relatorios.CarregarRelatorio("Vendas Por Cidade/Estado", "VendasPorCidade", false, fonte);
    }

    private void CarregarRelatorioProspectos()
    {
        DateTime dataDe = this.ConverterDataValida(tbxDataCadastroProspectosDe.Text);
        DateTime dataAteh = this.ConverterDataValida(tbxDataCadastroProspectosAte.Text);

        IList<Prospecto> prospectos = Prospecto.FiltrarRelatorio(Revenda.ConsultarPorId(ddlRevendaProspectos.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidadeProspectos.SelectedValue.ToInt32()), Estado.ConsultarPorId(ddlEstadoProspectos.SelectedValue.ToInt32()), dataDe, dataAteh, ddlStatus.SelectedValue.ToInt32()); //ddlStatus: 1 para true e 2 para false

        Fontes.relatorioProspectosDataTable fonte = new Fontes.relatorioProspectosDataTable();
        string descricaoRevenda = ddlRevendaProspectos.SelectedIndex != 0 ? ddlRevendaProspectos.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = this.GetDescricaoData(dataDe, dataAteh);
        string descricaoEstado = ddlEstadoProspectos.SelectedIndex != 0 ? ddlEstadoProspectos.SelectedItem.Text.Trim() : "Todos";
        string descricaoCidade = ddlCidadeProspectos.SelectedIndex > 0 ? ddlCidadeProspectos.SelectedItem.Text.Trim() : "Todos";
        string descricaoStatus = ddlStatus.SelectedIndex > 0 ? ddlStatus.SelectedItem.Text.Trim() : "Todos";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;


        if (prospectos.Count > 0)
        {
            foreach (Prospecto aux in prospectos)
                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido", descricaoRevenda, descricaoEstado, descricaoCidade, descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência, descricaoStatus, aux.Nome, aux.GetNumeroCNPJeCPFComMascara, aux.Responsavel, aux.Ativo ? "ATIVO" : "INATIVO", aux.Endereco != null && aux.Endereco.Cidade != null ? aux.Endereco.Cidade.Nome : "", aux.Endereco != null && aux.Endereco.Cidade != null && aux.Endereco.Cidade.Estado != null ? aux.Endereco.Cidade.Estado.PegarSiglaEstado() : "", prospectos.Count, URLLOgo, aux.DataCadastro.ToShortDateString(), aux.Revenda != null ? aux.Revenda.Nome : "");
        }
        else
        {
            fonte.Rows.Add(this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Usuário não definido", descricaoRevenda, descricaoEstado, descricaoCidade, descricaoDataDeReferência == "de 01/01/1753 até 01/01/2753" ? "Não definida" : descricaoDataDeReferência, descricaoStatus, null, null, null, null, null, null, 0, URLLOgo, null, null);
        }
        Relatorios.CarregarRelatorio("Prospectos", "Prospectos", false, fonte);
    }

    private void CarregarRelatorioComissaoRevenda()
    {

        int ano = ddlComissaoAnoRevenda.SelectedValue.ToInt32();
        int mes = ddlComissaoMesRevenda.SelectedValue.ToInt32();
        IList<Revenda> revendas = Revenda.ConsultarComissao(ddlComissaoMesRevenda.SelectedValue.ToInt32(), ddlComissaoAnoRevenda.SelectedValue.ToInt32());

        string descricaoMes = ddlComissaoMesRevenda.SelectedIndex != 0 ? ddlComissaoMesRevenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoAno = ddlComissaoAnoRevenda.SelectedItem.Text.Trim();
        string URLLogo = WebUtil.GetURLImagemLogoRelatorio;

        Fontes.relatoriosComissaoRevendaDataTable fonte = new Fontes.relatoriosComissaoRevendaDataTable();

        if (revendas.Count > 0)
        {
            foreach (Revenda revend in revendas)
            {
                decimal valorTotal = 0;
                foreach (Prospecto pros in revend.Prospectos)
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
                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoMes, descricaoAno, revend.Nome, valorTotal.ToString("0.00"), URLLogo);
            }
        }
        else
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoMes, descricaoAno, null, null, URLLogo);

        Relatorios.CarregarRelatorio("Comissão Por Revenda", "ComissaoRevenda", false, fonte);
    }

    private DateTime ConverterDataValida(string data)
    {
        return data != string.Empty ? Convert.ToDateTime(data).ToMinHourOfDay() > SqlDate.MinValue ?
            Convert.ToDateTime(data).ToMinHourOfDay() : SqlDate.MinValue : SqlDate.MinValue;
    }

    #endregion

    #region ___________Eventos_____________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioSelecionado();
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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void trvRelatorios_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            btnExibirRelatorio.Visible = true;
            mtvFiltrosRelatorios.ActiveViewIndex = trvRelatoriosComcerciais.SelectedNode.Value.ToInt32();

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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

}