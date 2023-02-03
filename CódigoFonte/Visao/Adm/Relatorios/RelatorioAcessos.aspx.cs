using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Adm_Relatorios_RelatorioAcessos : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                Session["idConfig"] = ddlSistemaAcessos.SelectedValue;
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
        this.CarregarGruposEconomicos(ddlGrupoAdministradorAcessos);
        this.CarregarUsuariosAcessos();
    }

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

    private void CarregarUsuariosAcessos()
    {
        ddlUsuarioAcessos.DataTextField = "Nome";
        ddlUsuarioAcessos.DataValueField = "Id";

        IList<Usuario> usuarios = new List<Usuario>();

        Administrador admim = Administrador.ConsultarPorId(ddlAdministradorAcessos.SelectedValue.ToInt32());
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoAdministradorAcessos.SelectedValue.ToInt32());

        if (ddlSistemaAcessos.SelectedValue.ToInt32() > 0 && admim != null && admim.Id > 0)
            usuarios = admim.GetUsuarios != null && admim.GetUsuarios.Count > 0 ? admim.GetUsuarios : new List<Usuario>();
        else if (grupo != null && grupo.Id > 0)
            usuarios = grupo.Usuarios != null && grupo.Usuarios.Count > 0 ? grupo.Usuarios : new List<Usuario>();
        else
            usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

        if (usuarios != null && usuarios.Count > 0)
            usuarios = this.RecarregarUsuarios(usuarios);

        Usuario aux = new Usuario();
        aux.Nome = "-- Todos --";
        usuarios.Insert(0, aux);

        ddlUsuarioAcessos.DataSource = usuarios;
        ddlUsuarioAcessos.DataBind();
        ddlUsuarioAcessos.SelectedIndex = 0;
    }

    private IList<Usuario> RecarregarUsuarios(IList<Usuario> usuarios)
    {
        IList<Usuario> lista = new List<Usuario>();
        if (usuarios != null)
        {
            foreach (Usuario item in usuarios)
            {
                Usuario not = Usuario.ConsultarPorId(item.Id);
                if (lista.Contains(not))
                {
                    lista.Remove(not);
                    lista.Add(not);
                }
                else
                {
                    lista.Add(not);
                }
            }
        }
        return lista;
    }

    private void CarregarGruposEconomicosAdmAcessos(DropDownList ddlGrupoAdministradorAcessos)
    {
        if (ddlSistemaAcessos.SelectedValue.ToInt32() > 0)
        {
            administrador_acessos.Visible = true;
            this.CarregarAdministradores(ddlAdministradorAcessos);
        }
        else
        {
            administrador_acessos.Visible = false;
        }

        this.CarregarGruposEconomicos(ddlGrupoAdministradorAcessos);
        this.CarregarUsuariosAcessos();
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

    private void CarregarRelatorioAcessos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        if (tbxDataAcessoDe.Text == string.Empty || tbxDataAcessoAte.Text == string.Empty)
        {
            msg.CriarMensagem("Selecione um período para poder exibir o relatório", "Alerta", MsgIcons.Alerta);
            return;
        }

        DateTime dataDe = tbxDataAcessoDe.Text != string.Empty ? Convert.ToDateTime(tbxDataAcessoDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAteh = tbxDataAcessoAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAcessoAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Acesso> acessos = Acesso.FiltrarRelatorio(ddlAdministradorAcessos.SelectedValue.ToInt32(), ddlGrupoAdministradorAcessos.SelectedValue.ToInt32(),
            ddlUsuarioAcessos.SelectedValue.ToInt32(), dataDe.ToMinHourOfDay(), dataAteh.ToMaxHourOfDay());

        grvRelatorio.DataSource = acessos != null && acessos.Count > 0 ? acessos : new List<Acesso>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoPeriodo = dataDe != SqlDate.MinValue || dataAteh != SqlDate.MaxValue ? WebUtil.GetDescricaoDataRelatorio(dataDe, dataAteh) : "Não definido";        
        string descricaoSistema = ddlSistemaAcessos.SelectedValue.ToInt32() > 0 ? "Ambientalis" : "Sustentar";
        string descricaoGrupoAdministrador = ddlGrupoAdministradorAcessos.SelectedValue.ToInt32() > 0 ? "Grupo Econômico:" : ddlAdministradorAcessos.SelectedValue.ToInt32() > 0 ? "Administrador:" : "Grupo/Administrador:";
        string grupoAdministradorEscolhido = ddlGrupoAdministradorAcessos.SelectedValue.ToInt32() > 0 ? ddlGrupoAdministradorAcessos.SelectedItem.Text : ddlAdministradorAcessos.SelectedValue.ToInt32() > 0 ? ddlAdministradorAcessos.SelectedItem.Text : "Não definido";
        string descricaoUsuario = ddlUsuarioAcessos.SelectedValue.ToInt32() > 0 ? ddlUsuarioAcessos.SelectedItem.Text : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Sistema", descricaoSistema);
        CtrlHeader.InsertFiltroEsquerda(descricaoGrupoAdministrador, grupoAdministradorEscolhido);

        CtrlHeader.InsertFiltroCentro("Usuário", descricaoUsuario);

        CtrlHeader.InsertFiltroDireita("Período", descricaoPeriodo);
        

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Acessos do Sistema");

        RelatorioUtil.OcultarFiltros(this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlSistemaAcessos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistemaAcessos.SelectedValue;
            transacao.Abrir();
            this.CarregarGruposEconomicosAdmAcessos(ddlGrupoAdministradorAcessos);
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

    protected void ddlAdministradorAcessos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            ddlGrupoAdministradorAcessos.Enabled = ddlAdministradorAcessos.SelectedIndex == 0;
            this.CarregarUsuariosAcessos();
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

    protected void ddlGrupoAdministradorAcessos_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (ddlSistemaAcessos.SelectedValue.ToInt32() > 0)
            {
                if (ddlAdministradorAcessos.SelectedValue == "0")
                {
                    ddlGrupoAdministradorAcessos.Enabled = true;
                    ddlAdministradorAcessos.Enabled = false;
                }
                if (ddlGrupoAdministradorAcessos.SelectedValue == "0")
                {
                    ddlAdministradorAcessos.Enabled = true;
                }
            }

            this.CarregarUsuariosAcessos();
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
            this.CarregarRelatorioAcessos();
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