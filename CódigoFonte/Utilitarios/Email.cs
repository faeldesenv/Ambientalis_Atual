using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web.Mail;
using System.IO;
using Modelo;

namespace Utilitarios
{
    public enum EmailSustentar { Comercial, Notificacao, Revenda, Teste };

    /// <summary>
    /// Summary description for Email
    /// </summary>
    ///
    public class Email
    {
        public delegate void EmailEnviadoEventHandler(Email email);
        public delegate void EmailNaoEnviadoEventHandler(Email email);

        public event EmailEnviadoEventHandler EmailEnviado;
        public event EmailNaoEnviadoEventHandler EmailNaoEnviado;

        private System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

        #region _____________ ATRIBUTOS ____________

        private MailAddressCollection emailsDestino;
        private string assunto;
        private string mensagem;
        private bool bodyHtml = true;
        private string erro;
        private IList<string> anexos;
        public IList<Email> emailsAuxiliares = new List<Email>();

        #endregion

        #region _____________ PROPRIEDADES ____________

        /// <summary>
        /// Se o corpo do email vai ser no formato HTML
        /// </summary>
        public bool BodyHtml
        {
            get { return bodyHtml; }
            set { bodyHtml = value; }
        }

        /// <summary>
        /// Endereço de destino para o envio do email
        /// </summary>
        public MailAddressCollection EmailsDestino
        {
            get
            {
                if (emailsDestino == null)
                    this.emailsDestino = new MailAddressCollection();
                return emailsDestino;
            }
        }

        /// <summary>
        /// Assunto a ser colocado no email
        /// </summary>
        public string Assunto
        {
            get
            {
                return assunto;
            }
            set
            {
                assunto = value;
            }
        }

        /// <summary>
        /// Mensagem a ser colocado no email
        /// </summary>
        public string Mensagem
        {
            get
            {
                return mensagem;
            }
            set
            {
                mensagem = value;
            }
        }

        /// <summary>
        /// Gerada quando ocorre erro no envio
        /// </summary>
        public string Erro
        {
            get { return erro; }
            set { erro = value; }
        }

        #endregion

        #region _____________ CONTRUTOR ____________

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public Email()
        {

        }

        #endregion

        #region _____________ EVENTOS ____________

        void smpt_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                if (this.EmailEnviado != null)
                    this.EmailEnviado(this);
            }
            else
            {
                if (this.EmailNaoEnviado != null)
                {
                    this.Erro = e.Error.Message;
                    this.EmailNaoEnviado(this);
                }
            }
        }

        #endregion

        #region _____________ MÉTODOS ____________

       
        /// <summary>
        /// Envia este e-mail autenticado no servidor SMTP
        /// </summary>
        /// <returns>true caso consiga e false caso não</returns>
        public bool EnviarAutenticado(int porta, bool useSSL)
        {
            try
            {
                if (ConfigurationManager.AppSettings["implantado"].ToString() == "true")
                {
                    this.CriarEmail();
                    if (ConfigurationManager.AppSettings["servidorSMTP"] == null ||
                        ConfigurationManager.AppSettings["userNameEmail"] == null ||
                        ConfigurationManager.AppSettings["passwordEmail"] == null)
                    {
                        this.Erro = "Configuração do servidor SMTP ou de usuário ou de senha está(ão) faltando no arquivo de configurações da aplicação";
                        return false;
                    }

                    SmtpClient cliente = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
                    cliente.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["userNameEmail"].ToString(), ConfigurationManager.AppSettings["passwordEmail"].ToString());
                    cliente.Port = ConfigurationManager.AppSettings["portaEnvio"].ToInt32();
                    cliente.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString());
                    cliente.Send(this.email);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                this.Erro = ex.Message + " - " + ex.InnerException.ToString();
                return false;
            }
        }

        public bool AdicionarAnexo(string caminhoAnexo)
        {
            if (this.anexos == null)
                this.anexos = new List<string>();
            if (File.Exists(caminhoAnexo))
            {
                this.anexos.Add(caminhoAnexo);
                return true;
            }
            return false;
        }

        public void AdicionarDestinatario(string destinatario)
        {
            destinatario = destinatario.Trim();
            if (destinatario.Contains(';'))
            {
                string[] emails = destinatario.Split(';');
                if (emails.Length > 0)
                {
                    for (int i = 0; i < emails.Length; i++)
                    {
                        if (emails[i].Contains(')'))
                        {
                            string[] emailsAuxiliares = emails[i].Split(')');
                            if (emailsAuxiliares.Length > 1)
                            {
                                string auxiliar2 = emailsAuxiliares[1];
                                if (auxiliar2.IsNotNullOrEmpty() && Validadores.ValidaEmail(auxiliar2.Trim()))
                                    this.EmailsDestino.Add(new MailAddress(auxiliar2.Trim().ToLower()));
                            }
                        }
                        else
                        {
                            if (emails[i].IsNotNullOrEmpty() && Validadores.ValidaEmail(emails[i].Trim()))
                            {
                                this.EmailsDestino.Add(new MailAddress(emails[i].Trim().ToLower()));
                            }
                        }
                    }
                }
            }
            else
                if (destinatario.Contains(')'))
                {
                    string[] emails2 = destinatario.Split(')');
                    if (emails2.Length > 1)
                    {
                        string auxiliar2 = emails2[1];
                        if (auxiliar2.IsNotNullOrEmpty() && Validadores.ValidaEmail(auxiliar2))
                        {
                            this.EmailsDestino.Add(new MailAddress(auxiliar2.Trim().ToLower()));
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(destinatario) && Validadores.ValidaEmail(destinatario))
                    this.EmailsDestino.Add(new MailAddress(destinatario.ToLower()));
        }

        private void CriarEmail()
        {
            if (ConfigurationManager.AppSettings["emailContato"] == null)
                throw new Exception("E-mail de contato para envio de e-mail não definido no arquivo de configurações da aplicação");

            this.email = new System.Net.Mail.MailMessage();
            if (this.anexos != null && this.anexos.Count > 0)
                foreach (string caminhoArquivo in this.anexos)
                    this.email.Attachments.Add(new Attachment(caminhoArquivo) { Name = caminhoArquivo.Substring(caminhoArquivo.LastIndexOf('/') + 1) });

            for (int index = this.EmailsDestino.Count - 1; index > -1; index--)
            {
                if (this.EmailsDestino[index].Address == "backup@sustentar.inf.br")
                {
                    MailAddress aux = new MailAddress("backup@sustentar.inf.br");
                    this.email.Bcc.Add(aux);
                    this.EmailsDestino.Remove(this.EmailsDestino[index]);
                }
            }

            this.email.From = new MailAddress(ConfigurationManager.AppSettings["emailContato"], "Notificação Sustentar");
            this.email.To.AddRange<MailAddress>(this.EmailsDestino);
            this.email.Subject = this.Assunto;
            this.email.Body = this.Mensagem;
            this.email.IsBodyHtml = this.BodyHtml;
            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            this.email.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        }

        public static Email CriarEmailNotificacao(Notificacao notificao, bool retirarMensagemVencimentoStatusDiferenteDeCumprido, Dictionary<string, StringBuilder> dic)
        {
            TemplateNotificacao template = TemplateNotificacao.GetTemplateNotificao(notificao);
            Email email = new Email();
            email.Mensagem = notificao.GetMessageTemplate(template, retirarMensagemVencimentoStatusDiferenteDeCumprido);
            //Adicionando os destinatários
            string[] emails = notificao.Emails.Split(';');
            foreach (string aux in emails)
            {
                if (aux.Contains(')'))
                {
                    string[] emails2 = aux.Split(')');
                    if (emails2.Length > 1)
                    {
                        string auxiliar2 = emails2[1];
                        if (auxiliar2.IsNotNullOrEmpty() && Validadores.ValidaEmail(auxiliar2.Trim()))
                        {
                            email.AdicionarDestinatario(auxiliar2.Trim().ToLower());

                            if (dic != null) 
                            {
                                if (!dic.ContainsKey(auxiliar2.Trim()))
                                    dic.Add(auxiliar2.Trim(), new StringBuilder(email.Mensagem + "<div style='margin-bottom:30px;'></div>"));
                                else
                                    dic[auxiliar2.Trim()].Append(email.Mensagem + "<div style='margin-bottom:30px;'></div>");
                                
                            }
                                
                        }
                    }
                }

                if (aux.IsNotNullOrEmpty() && Validadores.ValidaEmail(aux.Trim())) 
                {
                    email.AdicionarDestinatario(aux.Trim().ToLower());

                    if (dic != null) 
                    {
                        if (!dic.ContainsKey(aux.Trim()))
                            dic.Add(aux.Trim(), new StringBuilder(email.Mensagem + "<div style='margin-bottom:30px;'></div>"));
                        else
                            dic[aux.Trim()].Append(email.Mensagem + "<div style='margin-bottom:30px;'></div>");
                        
                    }                        
                }
                    
            }

            email.AdicionarDestinatario("backup@sustentar.inf.br");
            email.Assunto = template != null ? ("Notificação Sustentar: " + template.AssuntoEmail) : " Notificação - Sustentar";
            email.BodyHtml = true;            
            email.DividirEmails(email);
            return email;
        }

        private void DividirEmails(Email email)
        {
            MailAddressCollection destinatoriosEmailAux = new MailAddressCollection();
            if (email.emailsDestino.Count > 10)
            {

                for (int indexDestino = 0; indexDestino < email.emailsDestino.Count; indexDestino++)
                {
                    if (indexDestino % 10 == 0)
                    {
                        this.CriarEmailAuxiliar(email, destinatoriosEmailAux);
                        destinatoriosEmailAux.Clear();
                        destinatoriosEmailAux.Add(email.emailsDestino[indexDestino]);
                    }
                    else
                        destinatoriosEmailAux.Add(email.emailsDestino[indexDestino]);
                }
                this.CriarEmailAuxiliar(email, destinatoriosEmailAux);
            }
            else
            {
                destinatoriosEmailAux = email.emailsDestino;
                this.CriarEmailAuxiliar(email, destinatoriosEmailAux);
            }

        }

        private void CriarEmailAuxiliar(Email email, MailAddressCollection destinatoriosEmailAux)
        {
            if (destinatoriosEmailAux.Count > 0)
            {
                Email emailAux2 = new Email();
                emailAux2.Assunto = email.Assunto;
                emailAux2.BodyHtml = email.BodyHtml;
                emailAux2.Mensagem = email.Mensagem;
                emailAux2.emailsDestino = new MailAddressCollection();
                foreach (MailAddress aux in destinatoriosEmailAux)
                    emailAux2.emailsDestino.Add(aux);

                email.emailsAuxiliares.Add(emailAux2);
            }
        }

        #endregion

        #region _____________ TEMPLATES ____________

        public string GetTemplateBasico(string tituloDoEmail, string nome, string telefone, string email, string numero_contrato, string empresas, string usuarios, string mensagem)
        {
            string template = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
                              <tr>
                                <td colspan='3' align='center'><font size='6'>{0}</font>
                                <hr /></td>
                              </tr>
                              <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Nome:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{1}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Telefone</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{2}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Email:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{3}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Conteúdo:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{4}</font></td>
                              </tr>                              
                              <tr>
                                <td width='11%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Número do Contrato:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{5}</font></td>
                              </tr>
                              <tr>
                                <td width='12%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Quantidade de empresas:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{6}</font></td>
                              </tr>
                              <tr>
                                <td width='11%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Quantidade de usuários:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{7}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Data:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{8}</font></td>
                              </tr>                              
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              
                            </table>";

            template = String.Format(template, tituloDoEmail, nome, telefone, email, mensagem.Replace("\n", "<br>"), numero_contrato, empresas, usuarios, DateTime.Now.ToString());
            return template;
        }

        public string CriarTemplateParaMudancaDeStatusDeVencimento(Historico h)
        {
            return @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:5px; font-family:arial; font-size:15px; font-weight:bold; margin-top:30px; text-align:center; width: 308px;'>
                Notificação de Alteração de Status de Vencimento</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:13px;padding:7px; background-color:#E9E9E9; height:auto' 
                align='left'><strong>Data: </strong>" + h.DataPublicacao.ToString() + @"<br />
                <strong>Alteração: </strong>" + h.Alteracao + @"<br />
                <br />
               <strong>Observação: </strong>" + h.Observacao + @"</div>
            <div style='width:100%; height:20px;'></div></div>";
        }

        public string CriarTemplateParaMudancaDeContrato(Historico h)
        {
            return @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:5px; font-family:arial; font-size:15px; font-weight:bold; margin-top:30px; text-align:center; width: 308px;'>
                Notificação de Alteração em Contrato</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:13px;padding:7px; background-color:#E9E9E9; height:auto' 
                align='left'><strong>Data: </strong>" + h.DataPublicacao.ToString() + @"<br />
                <strong>Alteração: </strong>" + h.Alteracao + @"<br />
                <br />
               <strong>Observação: </strong>" + h.Observacao + @"</div>
            <div style='width:100%; height:20px;'></div></div>";
        }

        public string GetTemplateBasico(string tituloDoEmail, string nome, string telefone, string email, string mensagem)
        {
            string template = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
                              <tr>
                                <td colspan='3' align='center'><font size='6'>{0}</font>
                                <hr /></td>
                              </tr>
                              <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Nome:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{1}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Telefone</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{2}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Email:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{3}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Conteúdo:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{4}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              <tr>
                                <td width='8%' align='left' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Data:</font></td>
                                <td width='92%' colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>{5}</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              
                            </table>";

            template = String.Format(template, tituloDoEmail, nome, telefone, email, mensagem.Replace("\n", "<br>"), DateTime.Now.ToString());
            return template;
        }
        
        #endregion
        
        public string CriarTemplateParaHistoricoGeral(IList<Historico> historicos, string tituloEmail)
        {
            string mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
  <div style='float:left; margin-left:20px; font-family:arial; font-size:22px; font-weight:bold; margin-top:30px; text-align:center; width: 308px;'>
  Registros Históricos </div><div style='width:100%; height:20px; clear:both'></div>
  <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; background-color:#E9E9E9; text-align:left; height:auto'>" + tituloEmail + @".</div>";

            if (historicos != null && historicos.Count > 0)
            {
                mensagem += @"<table style='width:100%; margin-top:10px; height:auto;font-family:Arial, Helvetica, sans-serif; font-size:14px; border:1px solid silver;'><tbody><tr style='border:1px solid silver;'>
            <td align='left' width='20%' style='font-weight:bold; border:1px solid silver;'>Data do Registro</td>
            <td align='left' width='40%' style='font-weight:bold; border:1px solid silver;'>Título</td>
            <td align='left' width='50%' style='font-weight:bold; border:1px solid silver;'>Descrição</td>
            </tr>";

                foreach (Historico historico in historicos)
                {
                    mensagem += @"<tr style='border:1px solid silver;'><td align='left' width='20%' style='border:1px solid silver;'>" + historico.DataPublicacao.ToShortDateString() + @"</td><td align='left' width='40%' style='border:1px solid silver;'>" + historico.Alteracao + @"</td>
                <td align='left' width='50%' style='border:1px solid silver;'>" + historico.Observacao + @"</td></tr>";
                }

                mensagem += @"</tbody></table>";
            }
            else
            {
                mensagem += @"<div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; text-align:left; height:auto'>Não há registros históricos para este item</div>";
            }

            mensagem += @"<div style='width:100%; height:20px;'></div></div>";
            return mensagem;
        }

        public string CriarTemplateParaProrrogacaoDeDeVencimento(Historico h)
        {
            return @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:15px; font-family:arial; font-size:15px; font-weight:bold; margin-top:30px; text-align:center; width: 308px;'>
                Notificação de Prorrogação<br/> de Vencimento</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:13px;padding:7px; background-color:#E9E9E9; height:auto' 
                align='left'><strong>Data: </strong>" + h.DataPublicacao.ToString() + @"<br />
                <strong>Alteração: </strong>" + h.Alteracao + @"<br />
                <br />
               <strong>Observação: </strong>" + h.Observacao + @"</div>
            <div style='width:100%; height:20px;'></div></div>";
        }
    }
}
