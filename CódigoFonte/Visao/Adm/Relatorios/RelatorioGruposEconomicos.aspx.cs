using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Adm_Relatorios_RelatorioGruposEconomicos : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();
                this.CarregarCampos();
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

    #region ______________Métodos______________

    private void CarregarCampos()
    {
        this.CarregarAdministradores(ddlAdministradorGruposEconomicos);
    }

    private void CarregarAdministradores(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<Administrador> admins = Administrador.ConsultarTodosOrdemAlfabetica();
        Administrador aux = new Administrador();
        aux.Nome = "-- Selecione --";
        admins.Insert(0, aux);

        drop.DataSource = admins;
        drop.DataBind();
        drop.SelectedIndex = 0;
    }

    public string BindQuantidadeUsuarios(object o)
    {
        GrupoEconomico g = (GrupoEconomico)o;

        IList<Usuario> usuarios = Usuario.ConsultarTodos();
        return g.GetQuantidadeUsuariosDoGrupo(g, usuarios).ToString();

    }

    private void CarregarRelatorioGruposEconomicos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataCadastroRelatorioGruposEconomicos.Text != string.Empty ? Convert.ToDateTime(tbxDataCadastroRelatorioGruposEconomicos.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataCadastroAtehRelatorioGruposEconomicos.Text != string.Empty ? Convert.ToDateTime(tbxDataCadastroAtehRelatorioGruposEconomicos.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        DateTime dataCancelamentoDe = SqlDate.MinValue;
        DateTime dataCancelamentoAte = SqlDate.MaxValue;

        IList<GrupoEconomico> grupos = GrupoEconomico.FiltrarGruposEconomicos(ddlAdministradorGruposEconomicos.SelectedValue.ToInt32(),
            dataDe.ToMinHourOfDay(), dataAteh.ToMaxHourOfDay(), dataCancelamentoDe.ToMinHourOfDay(), dataCancelamentoAte.ToMaxHourOfDay(),
            ddlPossuiUsuarios.SelectedValue.ToInt32(),
            ddlAtivo.SelectedValue.ToInt32(),
            ddlCancelado.SelectedValue.ToInt32());

        grvRelatorio.DataSource = grupos != null && grupos.Count > 0 ? grupos : new List<GrupoEconomico>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoAdministrador = ddlAdministradorGruposEconomicos.SelectedIndex != 0 ? ddlAdministradorGruposEconomicos.SelectedItem.Text.Trim() : "Todos";
        string descricaoDataDeReferência = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAteh);
        string descricaoPossuiUsuariosCadastrados = ddlPossuiUsuarios.SelectedItem.Text.Trim();
        string descricaoDataDeCancelamento = dataCancelamentoDe != SqlDate.MinValue || dataCancelamentoAte != SqlDate.MaxValue ? WebUtil.GetDescricaoDataRelatorio(dataCancelamentoDe, dataCancelamentoAte) : "Não definida";
        string descricaoAtivo = ddlAtivo.SelectedItem.Text;
        string descricaoCancelado = ddlCancelado.SelectedItem.Text;

        CtrlHeader.InsertFiltroEsquerda("Administrador", descricaoAdministrador);
        CtrlHeader.InsertFiltroEsquerda("Data de Cadastro", descricaoDataDeReferência);

        CtrlHeader.InsertFiltroCentro("Data de Cancelamento", descricaoDataDeCancelamento);
        CtrlHeader.InsertFiltroCentro("Possuí usuários cadastrados", descricaoPossuiUsuariosCadastrados);

        CtrlHeader.InsertFiltroDireita("Ativos", descricaoAtivo);
        CtrlHeader.InsertFiltroDireita("Cancelados", descricaoCancelado);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Grupos Econômicos");

        RelatorioUtil.OcultarFiltros(this.Page);       
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.CarregarRelatorioGruposEconomicos();
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

    protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.CarregarAdministradores(ddlAdministradorGruposEconomicos);
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