using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Configuration;

public partial class Acesso_LoginSuporte : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            Session["idConfig"] = "0";
            try
            {
                transacao.Abrir();
                this.CarregarGrupos();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                if (msg.Mensagem.IsNotNullOrEmpty())
                    Alert.Show(msg.Mensagem);
            }
        }
    }

    private void CarregarGrupos()
    {
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarGruposAtivos();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    #region ________ Eventos ___________

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();
            this.Acessar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            if (msg.Mensagem.IsNotNullOrEmpty())
                Alert.Show(msg.Mensagem);
        }
    }

    #endregion

    #region ________ Métodos ___________

    private void Acessar()
    {
        if (ddlGrupoEconomico.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione uma Empresa", "Alerta", MsgIcons.Alerta);
            return;
        }

        Usuario user = new Usuario();
        user.Senha = tbxSenha.Text.Trim();
        if (user.Senha == "sup123456")
        {
            Session["idEmp"] = ddlGrupoEconomico.SelectedValue;            
            user.GrupoEconomico = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            Session["UsuarioLogado_SistemaAmbiental"] = user;
            Response.Redirect("../Site/Index.aspx");
        }
        else
            msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente", "");
    }

    #endregion

    protected void ddlGrupoEconomico_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["idEmp"] = ddlGrupoEconomico.SelectedValue;
    }
}