using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios.Criptografia;
using Modelo;
using Utilitarios;
using System.Net.Mail;
using System.Text;
using System.Configuration;

public partial class Notificacao_Send : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    private int[] contadorEmails = new int[8];

    public Dictionary<string, StringBuilder> dic = new Dictionary<string, StringBuilder>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();
            Exigencia.ExcluirExigenciasAvulsas();
            Notificacao.ExcluirNotificacoesAvulsas();
            transacao.Recarregar(ref msg);
            this.ObterPrevisaoEmailsDaSemana();

            if (!this.EfetuarNotificacoes("Sustentar") | !this.EfetuarNotificacoesAposDiaVencimento("Sustentar"))
                this.PrevisaoDaSemanaEResultado(false, "Sustentar", "");

            if (this.dic != null && this.dic.Count > 0)
            {
                this.EnviarEmailsRelatorioDiarioNotificacoes(dic);
            }

        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de Notificação (" + DateTime.Now + ") - Base: Sustentar";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + " - " + ex.InnerException;
            mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
            mail.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString()));
        }
        finally
        {
            transacao.Fechar(ref msg);
        }


        try
        {
            this.dic = new Dictionary<string, StringBuilder>();

            Session["idConfig"] = "1";
            transacao.Abrir();
            Exigencia.ExcluirExigenciasAvulsas();
            Notificacao.ExcluirNotificacoesAvulsas();
            transacao.Recarregar(ref msg);
            this.LimparContador();
            this.ObterPrevisaoEmailsDaSemana();
            if (!this.EfetuarNotificacoes("Ambientalis") | !this.EfetuarNotificacoesAposDiaVencimento("Ambientalis"))
                this.PrevisaoDaSemanaEResultado(false, "Ambientalis", "");

            if (this.dic != null && this.dic.Count > 0)
            {
                this.EnviarEmailsRelatorioDiarioNotificacoes(dic);
            }
        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de Notificação (" + DateTime.Now + ") - Base: Ambientalis";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + " - " + ex.InnerException;
            mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
            mail.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString()));
        }
        finally
        {
            transacao.Fechar(ref msg);
        }
    }

    private bool EfetuarNotificacoes(string sistema)
    {
        int contadorEmailsDodia = 0;
        IList<Notificacao> notificacoes = Notificacao.GetNotificacoesNaoEnviadasAteData(DateTime.Now);
        bool ocorreuErro = false;
        string mensagem = string.Empty;        

        for (int index = 0; index < notificacoes.Count; index++)
        {  
            try
            {

                //- Não enviar notificações para condicionantes e exigências com status de cumpridas
                if (notificacoes[index].Vencimento.Status != null && notificacoes[index].Vencimento.Status.Id == 4)
                {
                    notificacoes[index].Enviado++;
                    notificacoes[index].DataUltimoEnvio = DateTime.Now;
                    notificacoes[index] = notificacoes[index].Salvar();
                    continue;
                }

                //- Quando tiver LO não enviar notificações de LI e LP
                if (notificacoes[index].Vencimento.Licenca != null && notificacoes[index].Vencimento.Licenca.TipoLicenca != null)
                {
                    if (notificacoes[index].Vencimento.Licenca.TipoLicenca.Sigla.ToUpper().Trim() == "LI" || notificacoes[index].Vencimento.Licenca.TipoLicenca.Sigla.ToUpper().Trim() == "LP")
                    {
                        if (notificacoes[index].Vencimento.Licenca.Processo.Licencas.Where(p => p.TipoLicenca.Sigla.Trim().ToUpper() == "LO").Count() > 0)
                        {
                            notificacoes[index].Enviado++;
                            notificacoes[index].DataUltimoEnvio = DateTime.Now;
                            notificacoes[index] = notificacoes[index].Salvar();
                            continue;
                        }
                    }
                }

                Email email = Email.CriarEmailNotificacao(notificacoes[index], true, this.dic);

                if (email.Mensagem != "") 
                {
                    if (email.emailsAuxiliares != null && email.emailsAuxiliares.Count > 0)
                    {
                        foreach (Email item in email.emailsAuxiliares)
                        {
                            if (email.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString())))
                            {
                                notificacoes[index].Enviado++;
                                notificacoes[index].DataUltimoEnvio = DateTime.Now;
                                contadorEmailsDodia += item.EmailsDestino.Count;
                                notificacoes[index] = notificacoes[index].Salvar();
                            }
                            else
                            {
                                ocorreuErro = true;
                                if (mensagem == string.Empty)
                                    mensagem += " - " + email.Erro + "- emails(" + GetEmailsSeparadosPorPontoEVirgula(item.EmailsDestino) + ") <br/>";
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                ocorreuErro = true;
                if (mensagem == string.Empty)
                    mensagem += ex.Message + "<br/>";
                continue;
            }
        }        

        this.contadorEmails[0] = contadorEmailsDodia;

        if (ocorreuErro)
            this.PrevisaoDaSemanaEResultado(ocorreuErro, sistema, mensagem);
        return ocorreuErro;
    }

    private bool EfetuarNotificacoesAposDiaVencimento(string sistema)
    {
        int contadorEmailsAux = 0;

        IList<Vencimento> vencimentos = Vencimento.GetVencimentosAteOntemStatusDiferenteDeConcluido();
        bool ocorreuErro = false;
        string mensagem = string.Empty;

        foreach (Vencimento vencimento in vencimentos)
        {
            

            if (vencimento.Notificacoes != null && vencimento.Notificacoes.Count > 0)
            {
                Notificacao not = vencimento.Notificacoes[vencimento.Notificacoes.Count - 1];

                //- Não enviar notificações para condicionantes e exigências com status de cumpridas
                if (not.Vencimento.Status.Id == 4)
                {            
                    not.Enviado++;
                    not.DataUltimoEnvio = DateTime.Now;
                    not.Salvar();
                    continue;
                }

                Email email = Email.CriarEmailNotificacao(not, false, this.dic);

                try
                {
                    if (email.emailsAuxiliares != null && email.emailsAuxiliares.Count > 0)
                    {
                        foreach (Email emailAux in email.emailsAuxiliares)
                        {
                            if (email.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString())))
                            {
                                contadorEmailsAux += emailAux.EmailsDestino.Count;
                            }
                            else
                            {
                                ocorreuErro = true;
                                if (mensagem == string.Empty)
                                    mensagem += " - " + email.Erro + "- emails(" + GetEmailsSeparadosPorPontoEVirgula(emailAux.EmailsDestino) + ") <br/>";
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    ocorreuErro = true;
                    if (mensagem == string.Empty)
                        mensagem += ex.Message + "<br/>";
                    continue;
                }

            }
        }

        this.contadorEmails[0] += contadorEmailsAux;
        if (ocorreuErro)
            this.PrevisaoDaSemanaEResultado(ocorreuErro, sistema, mensagem);

        return ocorreuErro;
    }

    private void EnviarEmailsRelatorioDiarioNotificacoes(Dictionary<string, StringBuilder> dic)
    {
        bool ocorreuErro = false;
        string mensagem = "";

        foreach (KeyValuePair<string, StringBuilder> relatorio in dic)
        {
            try
            {
                Email email = new Email();
                email.AdicionarDestinatario(relatorio.Key);
                email.Assunto = "Relatório Diário de Notificações do Sistema Sustentar";
                email.Mensagem = relatorio.Value.ToString();
                email.BodyHtml = true;

                if (!email.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString())))
                {
                    ocorreuErro = true;
                    if (mensagem == string.Empty)
                        mensagem += " - " + email.Erro + "- emails(" + GetEmailsSeparadosPorPontoEVirgula(email.EmailsDestino) + ") <br/>";
                }

            }
            catch (Exception ex)
            {
                ocorreuErro = true;
                if (mensagem == string.Empty)
                    mensagem += ex.Message + "<br/>";
                continue;
            }

        }

        string result = "";
        if (ocorreuErro)
            result = "Alguma(s) relatório(s) diário(s) de notificação(ões) não foram enviadas, por favor tente novamente!<br/> Possível erro: " + mensagem + "</br>";
        else
        {
            result = "Relatórios Diários de Notificação(ões) enviados com sucesso!</br>";
        }

        lblResult.Text += "<br/><br/>" + result;
    }

    private string GetEmailsSeparadosPorPontoEVirgula(MailAddressCollection mailAddressCollection)
    {
        string saida = "";
        if (mailAddressCollection != null)
        {
            foreach (MailAddress Mail in mailAddressCollection)
            {
                saida += Mail.Address + ";";
            }
        }
        return saida;
    }

    private void ObterPrevisaoEmailsDaSemana()
    {
        try
        {
            IList<Notificacao> notificacoes = Notificacao.GetNotificacoesNaoEnviadasNaProximaSemana(DateTime.Now, DateTime.Now.AddDays(7));
            for (int p = 1; p < this.contadorEmails.Length; p++)
            {
                foreach (Notificacao notificacao in notificacoes)
                {
                    if (notificacao.Data.ToString("dd/MM/yyyy") == DateTime.Now.AddDays(p).ToString("dd/MM/yyyy"))
                    {
                        Email emailDoDia = Email.CriarEmailNotificacao(notificacao, true, null);
                        this.contadorEmails[p] = emailDoDia.EmailsDestino.Count;
                    }
                }
            }
        }
        catch (Exception)
        {
        }

    }



    private void PrevisaoDaSemanaEResultado(bool ocorreuErro, string sistema, string mensagem)
    {
        string result = "";
        if (ocorreuErro)
            result = "Alguma(s) notificação(ões) não foram enviadas, por favor tente novamente!<br/> Possível erro: " + mensagem + "</br>";
        else
        {
            result = "Notificação(ões) enviada(s) com sucesso!" + "<br/>" + "Quantidade de destinatário(s) por dia:" + "</br>" + "Hoje: " + DateTime.Now.ToString("dd/MM/yyyy") + " - " + contadorEmails[0] + " destinatário(s)." + "</br>";
            for (int dia = 1; dia < this.contadorEmails.Length; dia++)
            {
                result += DateTime.Now.AddDays(dia).ToString("dd/MM/yyyy") + " - previsão de " + this.contadorEmails[dia] + " destinatário(s)." + "</br>";
            }
        }

        Email mail = new Email();
        mail.Assunto = "Resultado de Notificação (" + DateTime.Now + ") - Base: " + sistema;
        mail.BodyHtml = true;
        mail.Mensagem = result;
        mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
        mail.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString()));
        lblResult.Text = result;
    }

    private void LimparContador()
    {
        for (int p = 0; p < this.contadorEmails.Length; p++)
        {
            this.contadorEmails[p] = 0;
        }
    }
}