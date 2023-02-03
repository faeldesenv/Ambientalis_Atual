using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class OrgaoAmbiental_PesquisarOrgaosAmbientaisAdm : PageBase
{
    Transacao transacao = new Transacao();
    Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["idConfig"] = "0";   
    }

    public void CarregarEstados()
    {
        IList<Estado> estados = Estado.ConsultarTodos();
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = estados;
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public void CarregarCidades(int id)
    {
        if (id > 0)
        {
            Estado estado = Estado.ConsultarPorId(id);
            IList<Cidade> cidades = new List<Cidade>();

            if (estado.Cidades != null && estado.Cidades.Count > 0)
            {
                cidades = estado.Cidades;
            }
            ddlCidade.DataValueField = "Id";
            ddlCidade.DataTextField = "Nome";
            ddlCidade.DataSource = cidades;
            ddlCidade.DataBind();
        }
    }

    public void Excluir(IList<OrgaoAmbiental> orgaos)
    {
        if (orgaos != null && orgaos.Count > 0)
        {
            foreach (OrgaoAmbiental orgao in orgaos)
            {
                if (orgao.Processos.Count > 0)
                {
                    msg.CriarMensagem("Alguns orgãos não podem ser excluídos, pois estão associados a processos", "Alerta", MsgIcons.Alerta);
                }
                else
                {
                    orgao.Excluir();
                    msg.CriarMensagem("Orgao(s) excluído(s) com sucesso", "Sucesso", MsgIcons.Sucesso);
                }
            }
        }
    }

    public void Pesquisar()
    {
        IList<OrgaoAmbiental> orgaos = OrgaoAmbiental.FiltrarOrgaosPadroes(ddlTipo.SelectedValue.ToInt32(), tbxNomeOrgao.Text, Estado.ConsultarPorId(ddlEstado.SelectedValue.ToInt32()), Cidade.ConsultarPorId(ddlCidade.SelectedValue.ToInt32()));
        dgr.DataSource = orgaos;
        dgr.DataBind();
        lblQuantidade.Text = orgaos.Count + " orgão(s) ambiental(is) encontrado(s).";
    }

    #region __________Bindings___________

    public string bindTipo(Object o)
    {
        OrgaoAmbiental orgao = (OrgaoAmbiental)o;
        if (orgao != null)
        {
            if (orgao.GetType() == typeof(OrgaoFederal))
                return "Federal";

            if (orgao.GetType() == typeof(OrgaoEstadual))
                return "Estadual";

            if (orgao.GetType() == typeof(OrgaoMunicipal))
                return "Municipal";
        } return "";
    }

    public string bindCidadeEstado(Object o)
    {
        OrgaoAmbiental orgao = (OrgaoAmbiental)o;
        if (orgao != null)
        {
            if (orgao.GetType() == typeof(OrgaoFederal))
                return "-";

            if (orgao.GetType() == typeof(OrgaoEstadual))
                return ((OrgaoEstadual)orgao).Estado.PegarSiglaEstado();

            if (orgao.GetType() == typeof(OrgaoMunicipal))
                return ((OrgaoMunicipal)orgao).Cidade.Nome + " - " + ((OrgaoMunicipal)orgao).Cidade.Estado.PegarSiglaEstado();
        } return "";
    }

    public string bindEditar(Object o)
    {
        OrgaoAmbiental orgao = (OrgaoAmbiental)o;
        return "CadastroOrgaoAmbientalAdm.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + orgao.Id);
    }

    #endregion

    #region ___________Eventos____________

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        transacao.Abrir();
        if (ddlTipo.SelectedIndex == 1 || ddlTipo.SelectedIndex == 0)
        {
            estado_orgao.Visible = false;
            cidades_orgao.Visible = false;
        }

        if (ddlTipo.SelectedIndex == 2)
        {
            estado_orgao.Visible = true;
            cidades_orgao.Visible = false;
            this.CarregarEstados();
        }

        if (ddlTipo.SelectedIndex == 3)
        {
            estado_orgao.Visible = true;
            cidades_orgao.Visible = true;
            this.CarregarEstados();
        }
        transacao.Fechar(ref msg);
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (ddlTipo.SelectedIndex == 3)
                this.CarregarCidades(ddlEstado.SelectedValue.ToInt32());
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            transacao.Abrir();
            IList<OrgaoAmbiental> orgaos = new List<OrgaoAmbiental>();
            foreach (DataGridItem item in dgr.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    OrgaoAmbiental o = OrgaoAmbiental.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                    orgaos.Add(o);
                }
            this.Excluir(orgaos);
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

   
    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, true);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Orgão(s) Ambiental(is) serão perdidos. Deseja excluir mesmo assim?");
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

}