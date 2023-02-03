using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Notificacao_testeemail3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try {
            string from = "notificacaosustentar@logus.inf.br"; // E-mail de remetente cadastrado no painel
            string to = "giovanicolodetti@gmail.com";   // E-mail do destinatário
            string user = "logus"; // Usuário de autenticação do servidor SMTP
            string pass = "mHdFsJuY9583"; // Senha de autenticação do servidor SMTP

            MailMessage message = new MailMessage(from, to, "SMTP Locaweb Teste", "Eu sou o corpo da mensagem");
            SmtpClient smtp = new SmtpClient("smtplw.com.br", 587);
            smtp.Credentials = new NetworkCredential(user, pass);
            smtp.Send(message);
            Label1.Text = "Mensagem Enviada.";
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message + "<br />" + ex.InnerException.ToString() + "<br />" + ex.StackTrace.ToString();
        }
       
    }
}