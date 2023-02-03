using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Notificacao_Default : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request["id"] != null)
            {
                Session["idConfig"] = "0";
                transacao.Abrir();

                Notificacao notific = Notificacao.ConsultarPorId(Request["id"].ToInt32());
                Email email = Email.CriarEmailNotificacao(notific, true, null);
                lblEmail.Text = email.Mensagem;
            }
        }
        catch (Exception ex)
        {
            string x = "";
        }
        finally
        {
            transacao.Fechar(ref msg);
        }
    }
}