using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Acesso_LoginDemostracao : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    public void SetIdConfig()
    {
        if (Request["v"] != null)
        {
            Session["idConfig"] = "0";
        }
        else
        {
            Session["idConfig"] = "0";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            this.SetIdConfig();
            try
            {
                if (Request["xyzfsdahfiuh2132dsfa1"] != "fasd21f3as2d1f3a2sdfs45fd6a5sd4f6as")
                    Response.Redirect("PermissaoInsufuciente.aspx", false);
                else
                {
                    transacao.Abrir();
                    this.Acessar();
                }

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

    private void Acessar()
    {
        Session["idEmp"] = "7";
        Usuario user = new Usuario()
        {
            Login = "revenda",
            Senha = Utilitarios.Criptografia.Criptografia.Encrypt("revenda2000", true),
        };

        if (Usuario.ValidaUsuario(ref user))
        {
            Session["UsuarioLogado_SistemaAmbiental"] = user;
            if (user.GrupoEconomico != null)
            {
                if (!user.GrupoEconomico.Ativo)
                {
                    msg.CriarMensagem("Não é possível realizar o login porque o Grupo Ecônomico do usuário informado ainda não foi ativado, por favor contacte o administrador do sistema para ativação", "Informação", MsgIcons.Informacao);
                    return;
                }
                if (user.GrupoEconomico.Cancelado)
                {
                    msg.CriarMensagem("Não é possível realizar o login com este usuário, por favor contacte o administrador do sistema", "Informação", MsgIcons.Informacao);
                    return;
                }
                Session["idEmp"] = user.GrupoEconomico.Id.ToString();
            }
            else
                Session["idEmp"] = null;

            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
                Response.Redirect(pagina);
            else
                Response.Redirect("../Site/Index.aspx");
        }
        else
            msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente", "");
    }
}