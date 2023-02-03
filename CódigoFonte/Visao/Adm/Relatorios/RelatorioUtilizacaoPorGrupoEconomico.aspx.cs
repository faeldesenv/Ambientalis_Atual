using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Adm_Relatorios_RelatorioUtilizacaoPorGrupoEconomico : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                Session["idConfig"] = "0";
                transacao.Abrir();
                this.CarregarGruposEconomicos(ddlGrupoEconomicoRelUtilizacao);
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunasAdm(grvRelatorio, ckbColunas, this.Page);                
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

    #region _____________________ Métodos _________________________

    private void CarregarGruposEconomicos(DropDownList dropGrupoEconomico)
    {
        dropGrupoEconomico.DataTextField = "Nome";
        dropGrupoEconomico.DataValueField = "Id";

        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        GrupoEconomico aux = new GrupoEconomico();
        aux.Nome = "-- Todos --";
        grupos.Insert(0, aux);

        dropGrupoEconomico.DataSource = grupos;
        dropGrupoEconomico.DataBind();
        dropGrupoEconomico.SelectedIndex = 0;
    }

    private void CarregarRelatorioUtilizacaoGruposEconomicos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<GrupoEconomico> grupos = new List<GrupoEconomico>();
        if (ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32() > 0)
            grupos.Add(GrupoEconomico.ConsultarPorId(ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32()));
        else
            grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();

        grvRelatorio.DataSource = grupos != null && grupos.Count > 0 ? grupos : new List<GrupoEconomico>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        this.ObterTotais(grupos);

        string descricaoGrupoEconomico = ddlGrupoEconomicoRelUtilizacao.SelectedValue.ToInt32() > 0 ? ddlGrupoEconomicoRelUtilizacao.SelectedItem.Text : "Todos";        
        string descricaoSistema = ddlSistemaRelUtilizacao.SelectedValue.ToInt32() > 0 ? "Ambientalis" : "Sustentar";

        CtrlHeader.InsertFiltroEsquerda("Sistema", descricaoSistema);

        CtrlHeader.InsertFiltroCentro("Grupo Econômico", descricaoGrupoEconomico);   

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Utilização por Grupos Econômicos");

        RelatorioUtil.OcultarFiltros(this.Page);
    }

    private void ObterTotais(IList<GrupoEconomico> grupos)
    {
        int contadorContratos = 0;
        int contadorProcessosAmbientais = 0;
        int contadorProcessosDNPM = 0;
        int contadorEmpresas = 0;

        foreach (GrupoEconomico grupo in grupos)
        {
            contadorContratos = contadorContratos + grupo.GetTotalContratosDiversosDoGrupo;
            contadorProcessosAmbientais = contadorProcessosAmbientais + grupo.GetTotalProcessosAmbientaisDoGrupo;
            contadorProcessosDNPM = contadorProcessosDNPM + grupo.GetTotalProcessosMinerariosDoGrupo;
            if (grupo.Empresas != null)
                contadorEmpresas = contadorEmpresas + grupo.Empresas.Count;
        }

        lblTotalEmpresas.Text = contadorEmpresas.ToString();
        lblTotalProcAmbientais.Text = contadorProcessosAmbientais.ToString();
        lblTotalProcMinerarios.Text = contadorProcessosDNPM.ToString();
        lblTotalContratos.Text = contadorContratos.ToString();
    }   

    #endregion

    #region _____________________ Eventos _________________________

    protected void ddlSistemaRelUtilizacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistemaRelUtilizacao.SelectedValue;
            transacao.Abrir();
            this.CarregarGruposEconomicos(ddlGrupoEconomicoRelUtilizacao);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            Alert.Show(msg.Mensagem);
        }
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.CarregarRelatorioUtilizacaoGruposEconomicos();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            Alert.Show(msg.Mensagem);
        }
    }    

    #endregion    
}