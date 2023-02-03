using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Adm_Relatorios_RelatorioPendenciasAtivacaoGruposEconomicos : PageBase
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
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunasAdm(grvRelatorio, ckbColunas, this.Page);
                this.CarregarRelatoriosPendenciasGruposEconomicos();
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

    private void CarregarRelatoriosPendenciasGruposEconomicos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarGruposComPendenciasDeAtivacao();

        grvRelatorio.DataSource = grupos != null && grupos.Count > 0 ? grupos : new List<GrupoEconomico>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Pendências de Ativação de Grupos Econômicos");

        RelatorioUtil.OcultarFiltros(this.Page);     

    }
}