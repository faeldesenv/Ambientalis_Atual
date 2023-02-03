using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioNotificacoesEnviadas : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {                
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunas(grvRelatorio, ckbColunas, this.Page);
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

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioNotificacoesEnviadas();
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

    private void CarregarRelatorioNotificacoesEnviadas()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDePeriodo = tbxDataDeNotificacaoEnviada.Text != string.Empty ? Convert.ToDateTime(tbxDataDeNotificacaoEnviada.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAtehPeriodo = tbxDataAtehNotificacaoEnviada.Text != string.Empty ? Convert.ToDateTime(tbxDataAtehNotificacaoEnviada.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Notificacao> notificacoes = Notificacao.FiltrarNotificacoes(dataDePeriodo, dataAtehPeriodo);

        grvRelatorio.DataSource = notificacoes != null && notificacoes.Count > 0 ? notificacoes : new List<Notificacao>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoUsuario = this.UsuarioLogado != null ? this.UsuarioLogado.Nome : "Não definido";
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDePeriodo, dataAtehPeriodo);

        CtrlHeader.InsertFiltroEsquerda("Data de Envio", descricaoPeriodo);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Notificações Enviadas");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }
}