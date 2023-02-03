using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;

public partial class OrgaoAmbiental_CadastroOrgaoAmbientalAdm : PageBase
{
    Transacao transacao = new Transacao();
    Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            if (!IsPostBack)
            {          
                transacao.Abrir();
                hfIdOrgao.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                if (hfIdOrgao.Value.ToInt32() > 0)
                    this.CarregarOrgao(hfIdOrgao.Value.ToInt32());
            }
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

    #region _________Metodos___________

    public void CarregarOrgao(int id)
    {
        OrgaoAmbiental orgao = OrgaoAmbiental.ConsultarPorId(id);
        estado_licenca.Visible = false;
        cidade_licenca.Visible = false;
        ddlTipoOrgao.SelectedIndex = 1;

        if (orgao.GetType() == typeof(OrgaoEstadual))
        {
            ddlTipoOrgao.SelectedIndex = 2;
            estado_licenca.Visible = true;
            cidade_licenca.Visible = false;
            this.CarregarEstados();
            ddlEstado.SelectedValue = ((OrgaoEstadual)orgao).Estado.Id.ToString();
        }

        if (orgao.GetType() == typeof(OrgaoMunicipal))
        {
            ddlTipoOrgao.SelectedIndex = 3;
            estado_licenca.Visible = true;
            cidade_licenca.Visible = true;
            this.CarregarEstados();
            ddlEstado.SelectedValue = ((OrgaoMunicipal)orgao).Cidade.Estado.Id.ToString();
            this.CarregarCidades(((OrgaoMunicipal)orgao).Cidade.Estado.Id);
            ddlCidade.SelectedValue = ((OrgaoMunicipal)orgao).Cidade.Id.ToString();

        }
        tbxNomeOrgao.Text = orgao.Nome;
        ddlTipoOrgao.Enabled = false;
    }

    public void CarregarEstados()
    {
        IList<Estado> estados = Estado.ConsultarTodos();
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = estados;
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Selecione", "0"));
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

    public void Salvar()
    {
        OrgaoAmbiental orgao = new OrgaoAmbiental();
        if (hfIdOrgao.Value.ToInt32() > 0)
            orgao = OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32());
        if (ddlTipoOrgao.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione o tipo do orgão ambiental", "Atenção", MsgIcons.Alerta);
            return;
        }

        if (ddlTipoOrgao.SelectedIndex == 1)
            if (hfIdOrgao.Value.ToInt32() <= 0)
                orgao = new OrgaoFederal();

        if (ddlTipoOrgao.SelectedIndex == 2)
        {
            if (hfIdOrgao.Value.ToInt32() <= 0)
                orgao = new OrgaoEstadual();
            ((OrgaoEstadual)orgao).Estado = Estado.ConsultarPorId(ddlEstado.SelectedValue.ToInt32());
        }

        if (ddlTipoOrgao.SelectedIndex == 3)
        {
            if (hfIdOrgao.Value.ToInt32() <= 0)
                orgao = new OrgaoMunicipal();
            ((OrgaoMunicipal)orgao).Cidade = Cidade.ConsultarPorId(ddlCidade.SelectedValue.ToInt32());
        }
        orgao.Nome = tbxNomeOrgao.Text.Trim();
        orgao.Emp = 0;
        orgao = orgao.Salvar();
        hfIdOrgao.Value = orgao.Id.ToString();
        msg.CriarMensagem("Orgão Ambiental cadastrado com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    public void Novo()
    {
        hfIdOrgao.Value = "0";
        Response.Redirect("CadastroOrgaoAmbientalAdm.aspx", false);
    }

    public void Excluir()
    {
        if (hfIdOrgao.Value.ToInt32() > 0)
        {
            OrgaoAmbiental orgao = OrgaoAmbiental.ConsultarPorId(hfIdOrgao.Value.ToInt32());
            if (orgao.Processos.Count == 0)
            {
                orgao.Excluir();
                msg.CriarMensagem("Orgão Ambiental excluído com sucesso", "Secesso", MsgIcons.Sucesso);
            }
            else
            {
                msg.CriarMensagem("Orgão Ambiental não pode ser excluído pois está associado à processo(s)", "Alerta", MsgIcons.Alerta);
            }
        }
        else
        {
            msg.CriarMensagem("Nao há orgão salvo para ser excluido!", "Alerta", MsgIcons.Alerta);
        }
    }

    #endregion

    #region __________Eventos__________

    protected void ddlTipoOrgao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (ddlTipoOrgao.SelectedIndex == 1 || ddlTipoOrgao.SelectedIndex == 0)
            {
                estado_licenca.Visible = false;
                cidade_licenca.Visible = false;
            }

            if (ddlTipoOrgao.SelectedIndex == 2)
            {
                estado_licenca.Visible = true;
                cidade_licenca.Visible = false;
                this.CarregarEstados();
            }

            if (ddlTipoOrgao.SelectedIndex == 3)
            {
                estado_licenca.Visible = true;
                cidade_licenca.Visible = true;
                this.CarregarEstados();
            }
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

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            if (ddlTipoOrgao.SelectedIndex == 3)
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Excluir();
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
    protected void PermissaoUsuario_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((Button)sender, true);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Salvar();

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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Novo();
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
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Excluir();
            
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.Novo();
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }
}