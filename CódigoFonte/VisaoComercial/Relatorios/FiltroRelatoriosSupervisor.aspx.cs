using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Relatorios_FiltroRelatoriosSupervisor : PageBase
{
    Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!this.ValidaUsuario())
                    return;
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

    private bool ValidaUsuario()
    {
        UsuarioComercial user = ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]);
        if (user.GetType() == typeof(UsuarioRevendaComercial))
        {
            Response.Redirect("FiltrosRelatoriosRevendas.aspx", false);
            return false;
        }
        return true;
    }

    private void CarregarCampos()
    {
        //Revendas
        this.CarregarEstados(ddlEstadoRevendas);

        //Prospectos
        this.CarregarEstados(ddlEstadoProspectos);
        this.CarregarRevendas(ddlRevendaProspectos);

        //Vendas
        this.CarregarRevendas(ddlRevendaVenda);
        this.CarregarRevendas(ddlVendasRevendaGrafico_1);
        this.CarregarRevendas(ddlVendasRevendaGrafico_2);
        this.CarregarRevendas(ddlVendasRevendaCidade);
        this.CarregarEstados(ddlVendasEstados);
        this.CarregarAnos();
        this.CarregarMeses();

        //Faturamento
        this.CarregarRevendas(ddlFaturamentoRevenda);
        
        //Utilização por Grupos Econômicos
        this.CarregarGruposEconomicos(ddlGrupoEconomicoRelUtilizacao);
    }

    private void CarregarGruposEconomicos(DropDownList dropGrupoEconomico)
    {
        dropGrupoEconomico.DataValueField = "Id";
        dropGrupoEconomico.DataTextField = "Nome";
        dropGrupoEconomico.DataSource = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        dropGrupoEconomico.DataBind();
        dropGrupoEconomico.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarAnos()
    {
        int anoAtual = DateTime.Now.Year + 1;
        for (int i = anoAtual - 10; i < anoAtual; i++)
        {
            ddlVendasAno_1.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            ddlVendasAno_2.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            ddlComissaoAnoRevenda.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            ddlComissaoAnoSupervisor.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            ddlFaturamentoAno.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void CarregarMeses()
    {
        for (int i = 1; i < 13; i++)
        {
            string mes = new DateTime(2000, i, 1).ToString("MMMM").Capitalizar();
            ddlComissaoMesRevenda.Items.Insert(0, new ListItem(mes, i.ToString()));
            ddlComissaoMesSupervisor.Items.Insert(0, new ListItem(mes, i.ToString()));
            ddlFaturamentoMes.Items.Insert(0, new ListItem(mes, i.ToString()));
        }
        ddlComissaoMesRevenda.Items.Insert(0, new ListItem("-- Todos --", "0"));
        ddlComissaoMesSupervisor.Items.Insert(0, new ListItem("-- Todos --", "0"));
        ddlFaturamentoMes.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void CarregarRelatorioSelecionado()
    {
        switch (mtvFiltrosRelatorios.ActiveViewIndex)
        {
            case 1:
                this.CarregarRelatorioRevendas();
                break;

            case 2:
                this.CarregarRelatorioProspectos();
                break;

            case 3:
                this.CarregarRelatorioVendas();
                break;

            case 4:
                this.CarregarRelatorioVendasGrafico("B");
                break;

            case 5:
                this.CarregarRelatorioVendasGrafico("P");
                break;

            case 6:
                this.CarregarRelatorioVendasCidade();
                break;

            case 7:
                this.CarregarRelatorioComissaoRevenda();
                break;

            case 8:
                this.CarregarRelatorioComissaoSupervisor();
                break;

            case 9:
                this.CarregarRelatorioFaturamentoRevenda();
                break;

            case 10:
                this.CarregarRelatorioUtilizacaoGruposEconomicos();
                break;

            default:
                msg.CriarMensagem("Selecione algum relatório", "Informação", MsgIcons.Informacao);
                break;
        }
    }

    private void CarregarRelatorioUtilizacaoGruposEconomicos()
    {
        Fontes.relatorioUtilizacaoPorGruposEconomicosDataTable fonte = new Fontes.relatorioUtilizacaoPorGruposEconomicosDataTable();

        string descricaoUsuarioLogado = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido";
        string descricaoGrupoEconomico = ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32() > 0 ? ddlGrupoEconomicoRelUtilizacao.SelectedItem.Text : "Todos";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        IList<GrupoEconomico> grupos = new List<GrupoEconomico>();
        if (ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32() > 0)
            grupos.Add(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32()));
        else
            grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();

        if (grupos != null && grupos.Count > 0)
        {
            foreach (GrupoEconomico grupo in grupos)
            {
                fonte.Rows.Add(descricaoUsuarioLogado, descricaoGrupoEconomico, grupo.Nome, grupo.Empresas != null ? grupo.Empresas.Count : 0, grupo.GetTotalProcessosAmbientaisDoGrupo, grupo.GetTotalProcessosMinerariosDoGrupo, grupo.GetTotalContratosDiversosDoGrupo, this.ObterTotalGeralProcAmbientais(grupos), this.ObterTotalGeralProcMinerarios(grupos), this.ObterTotalGeralContratos(grupos), grupos.Count, URLLOgo, this.ObterTotalEmpresas(grupos));
            }
        }
        else
        {
            fonte.Rows.Add(descricaoUsuarioLogado, descricaoGrupoEconomico, null, null, null, null, null, null, null, null, 0, URLLOgo, null);
        }

        Relatorios.CarregarRelatorio("Utilização por Grupos Econômicos", "UtilizacaoGrupoEconomicoComercial", false, fonte);
    }

    private void CarregarRelatorioRevendas()
    {
        IList<Revenda> revendas = Revenda.FiltrarRelatorio(Estado.ConsultarPorId(ddlEstadoRevendas.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidadesRevendas.SelectedValue.ToInt32()), ddlTipoParceiro.SelectedIndex > 0 ? ddlTipoParceiro.SelectedItem.Text : "");

        Fontes.relatorioRevendasDataTable fonte = new Fontes.relatorioRevendasDataTable();
        string descricaoEstado = ddlEstadoRevendas.SelectedIndex != 0 ? ddlEstadoRevendas.SelectedItem.Text.Trim() : "Todos";
        string descricaoCidade = ddlCidadesRevendas.SelectedIndex > 0 ? ddlCidadesRevendas.SelectedItem.Text.Trim() : "Todos";
        string descricaoStatus = ddlStatusRevendas.SelectedIndex > 0 ? ddlStatusRevendas.SelectedItem.Text.Trim() : "Todos";
        string descricaoTipoParceria = ddlTipoParceiro.SelectedIndex > 0 ? ddlTipoParceiro.SelectedItem.Text : "Todos";
        string URLLOgo = WebUtil.GetURLImagemLogoRelatorio;

        if (ddlStatusRevendas.SelectedValue.ToInt32() > 0)
            this.RemoverRevendasDeOutrosStatus(revendas, ddlStatusRevendas.SelectedValue.ToInt32() == 1);

        if (revendas != null && revendas.Count > 0)
        {
            foreach (Revenda revenda in revendas)
            {
                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido", descricaoCidade, descricaoEstado, descricaoStatus, revenda.Nome, revenda.GetNumeroCNPJeCPFComMascara, revenda.Responsavel, revenda.GetUltimoContrato != null && revenda.GetUltimoContrato.Aceito == true ? "ATIVA" : "INATIVA", revenda.Endereco != null && revenda.Endereco.Cidade != null ? revenda.Endereco.Cidade.Nome : "", revenda.Endereco != null && revenda.Endereco.Cidade != null && revenda.Endereco.Cidade.Estado != null ? revenda.Endereco.Cidade.Estado.PegarSiglaEstado() : "", revendas.Count, URLLOgo, descricaoTipoParceria, revenda.TipoParceiro != null ? revenda.TipoParceiro : "");
            }
        }
        else
        {
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido", descricaoCidade, descricaoEstado, descricaoStatus, null, null, null, null, null, null, 0, URLLOgo, descricaoTipoParceria, null);
        }

        Relatorios.CarregarRelatorio("Revendas", "Revendas", false, fonte);
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

    private void CarregarRelatorioVendas()
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
            return;
        }
        else if (ddlVendasRevendaGrafico_2.SelectedValue.ToInt32() > 0 && tipo == "P")
        {
            Response.Redirect("RelatoriosGraficos.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idRev=" + ddlVendasRevendaGrafico_2.SelectedValue + "&ano=" + ddlVendasAno_2.SelectedValue + "&tipo=P"), false);
            return;
        }
        msg.CriarMensagem("Selecione uma revenda.", "Aviso", MsgIcons.Alerta);
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
                decimal valorTotal = this.ValorComissaoPorRevendaMesAno(revend, mes, ano);

                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoMes, descricaoAno, revend.Nome, valorTotal.ToString().ToCurrency(), URLLogo);
            }
        }
        else
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoMes, descricaoAno, null, null, URLLogo);

        Relatorios.CarregarRelatorio("Comissão Por Revenda", "ComissaoRevenda", false, fonte);
    }

    private void CarregarRelatorioComissaoSupervisor()
    {
        int ano = ddlComissaoAnoSupervisor.SelectedValue.ToInt32();
        int mes = ddlComissaoMesSupervisor.SelectedValue.ToInt32();
        IList<Revenda> revendas = Revenda.ConsultarComissao(mes, ano);

        string descricaoMes = ddlComissaoMesSupervisor.SelectedIndex != 0 ? ddlComissaoMesSupervisor.SelectedItem.Text.Trim() : "Todos";
        string descricaoAno = ddlComissaoAnoSupervisor.SelectedItem.Text.Trim();
        string URLLogo = WebUtil.GetURLImagemLogoRelatorio;

        Fontes.relatoriosComissaoSupervisorDataTable fonte = new Fontes.relatoriosComissaoSupervisorDataTable();

        if (revendas.Count > 0)
        {
            foreach (Revenda revend in revendas)
            {
                decimal valorTotal = 0;
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
                        fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoMes, descricaoAno, revend.Nome, pros.Nome, valorComissao.ToString("0.00"), URLLogo, valorTotal.ToString("0.00"));
                    }

                }

            }
        }
        else
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                    descricaoMes, descricaoAno, null, null, null, URLLogo);

        Relatorios.CarregarRelatorio("Comissão Supervisor", "ComissaoSupervisor", false, fonte);
    }

    private void CarregarRelatorioFaturamentoRevenda()
    {
        int ano = ddlFaturamentoAno.SelectedValue.ToInt32();
        int mes = ddlFaturamentoMes.SelectedValue.ToInt32();
        IList<Revenda> revendas = Revenda.ConsultarFaturamento(Revenda.ConsultarPorId(ddlFaturamentoRevenda.SelectedValue.ToInt32()), mes, ano);

        string descricaoRevenda = ddlFaturamentoRevenda.SelectedIndex != 0 ? ddlFaturamentoRevenda.SelectedItem.Text.Trim() : "Todos";
        string descricaoMes = ddlFaturamentoMes.SelectedIndex != 0 ? ddlFaturamentoMes.SelectedItem.Text.Trim() : "Todos";
        string descricaoAno = ddlFaturamentoAno.SelectedItem.Text.Trim();
        string URLLogo = WebUtil.GetURLImagemLogoRelatorio;

        Fontes.relatorioFaturamentoRevendaDataTable fonte = new Fontes.relatorioFaturamentoRevendaDataTable();
        if (revendas.Count > 0)
        {
            foreach (Revenda revend in revendas)
            {
                decimal valorComissao = this.ValorComissaoPorRevendaMesAno(revend, mes, ano);
                decimal valorFaturamento = this.ValorFaturamentoPorRevendaMesAno(revend, mes, ano);

                fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                        descricaoRevenda, descricaoMes, descricaoAno, revend.Nome, valorFaturamento.ToString().ToCurrency(),
                        valorComissao.ToString().ToCurrency(), (valorFaturamento - valorComissao).ToString().ToCurrency(), URLLogo);
            }
        }
        else
            fonte.Rows.Add((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"] != null ? ((UsuarioComercial)Session["UsuarioLogado_SistemaComercial"]).Login : "Usuário não definido",
                        descricaoRevenda, descricaoMes, descricaoAno, null, null, null, null - null, URLLogo);

        Relatorios.CarregarRelatorio("Faturamento Por Revenda", "FaturamentoRevenda", false, fonte);

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

    private DateTime ConverterDataValida(string data)
    {
        return data != string.Empty ? Convert.ToDateTime(data).ToMinHourOfDay() > SqlDate.MinValue ?
            Convert.ToDateTime(data).ToMinHourOfDay() : SqlDate.MinValue : SqlDate.MinValue;
    }

    private decimal ValorComissaoPorRevendaMesAno(Revenda revend, int mes, int ano)
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
        return valorTotal;
    }

    private decimal ValorFaturamentoPorRevendaMesAno(Revenda revend, int mes, int ano)
    {
        decimal valorTotal = 0;
        foreach (Prospecto pros in revend.Prospectos)
        {
            if (pros.Venda != null)
            {
                if (mes == 0)
                    foreach (Mensalidade mens in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano))
                        valorTotal += mens.GetValorMensalidade;
                else
                    foreach (Mensalidade mens in pros.Venda.Mensalidades.Where(ms => ms.Ano == ano && ms.Mes == mes))
                        valorTotal += mens.GetValorMensalidade;
            }

        }
        return valorTotal;
    }

    private int ObterTotalGeralProcAmbientais(IList<GrupoEconomico> grupos)
    {
        int contadorProcessos = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorProcessos = contadorProcessos + grupo.GetTotalProcessosAmbientaisDoGrupo;
        }

        return contadorProcessos;
    }

    private int ObterTotalGeralProcMinerarios(IList<GrupoEconomico> grupos)
    {
        int contadorProcessos = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorProcessos = contadorProcessos + grupo.GetTotalProcessosMinerariosDoGrupo;
        }

        return contadorProcessos;
    }

    private int ObterTotalGeralContratos(IList<GrupoEconomico> grupos)
    {
        int contadorContratos = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorContratos = contadorContratos + grupo.GetTotalContratosDiversosDoGrupo;
        }

        return contadorContratos;
    }

    private int ObterTotalEmpresas(IList<GrupoEconomico> grupos)
    {
        int contadorEmpresas = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            if (grupo.Empresas != null)
                contadorEmpresas = contadorEmpresas + grupo.Empresas.Count;
        }

        return contadorEmpresas;
    }

    #endregion

    #region ___________Eventos_____________

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