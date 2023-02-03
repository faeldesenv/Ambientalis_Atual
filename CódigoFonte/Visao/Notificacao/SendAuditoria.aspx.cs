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
using Modelo.Auditoria;
using System.IO;
using System.Configuration;

public partial class Notificacao_SendAuditoria : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    private int[] contadorEmails = new int[8];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ServerVariables["remote_addr"] == "189.38.85.36")
        {
            this.EnviarSustentar();
            this.EnviarAmbientalis();
        }
        else
        {
            Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
        }
    }

    private void EnviarSustentar()
    {
        try
        {
            Session["idConfig"] = "0";
            transacao.Abrir();
            this.EfetuarNotificacoes();
        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de Notificação de Auditoria (" + DateTime.Now + ") - Base: Sustentar";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.InnerException;
            mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
            mail.EnviarAutenticado(25, false);
        }
        finally
        {
            transacao.Fechar(ref msg);
        }
    }

    private void EnviarAmbientalis()
    {
        try
        {
            Session["idConfig"] = "1";
            transacao.Abrir();
            this.EfetuarNotificacoes();
        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de Notificação de Auditoria (" + DateTime.Now + ") - Base: Ambientalis";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.InnerException;
            mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
            mail.EnviarAutenticado(25, false);
        }
        finally
        {
            transacao.Fechar(ref msg);
        }
    }

    private void EfetuarNotificacoes()
    {
        StreamWriter sw = null;
        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarGruposAtivos();
        foreach (GrupoEconomico grupo in grupos)
        {
            Session["Emp"] = grupo.Id;
            
            IList<Auditoria> auditorias = Auditoria.Filtrar("", "01/" + DateTime.Now.Month + "/" + DateTime.Now.Year, DateTime.Now.ToShortDateString(), "", "", "", "");
            try
            {
                DirectoryInfo d = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Repositorio/Auditoria/" + grupo.Id);
                if (!d.Exists)
                    d.Create();

                string arquivo = d.FullName + "/" + DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".txt";
                FileStream file = new FileStream(arquivo, FileMode.CreateNew);
                file.Close();
                file.Dispose();
                sw = new StreamWriter(arquivo);

                foreach (Auditoria aud in auditorias)
                {
                    sw.WriteLine("Data:" + aud.GetData + " Usuário:" + aud.GetUsuario + " Operação:" + aud.Operacao + " Registro:" + aud.Tabela + " Campo:" + aud.Coluna + " Valor Antigo:" + aud.Valor_old + " Valor Novo:" + aud.Valor_new);
                }

                sw.Close();
                sw.Dispose();

                Email email = new Email();

                if (grupo.Usuarios != null)
                    foreach (Usuario usr in grupo.Usuarios)
                    {
                        if (usr.UsuarioAdministrador && Validadores.ValidaEmail(usr.Email))
                        {
                            email.AdicionarAnexo(arquivo);
                            email.AdicionarDestinatario(usr.Email);
                            email.Assunto = "Log de Auditoria - Sistema Sustentar";
                            email.Mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
                                            <div style='float:left; margin-left:20px; margin-top:20px;'><img alt='logo' src='http://sustentar.inf.br/imagens/logo_login.png'></div>
                                            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>
                                                Auditoria<br />
                                                SUSTENTAR</div><div style='width:100%; height:20px; clear:both'></div>
                                            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; background-color:#E9E9E9; text-align:center; height:auto'>
                                                Segue em anexo o log de Auditoria referente a este mês.<br />
                                                <br />
                                                Este aquivo contém o registro de todas as <strong>INSERÇÕES</strong>, 
                                                <strong>EXCLUSÕES</strong> e <strong>ALTERAÇÕES</strong> 
                                                realizadas por todos seus usuários neste mês.<br />
                                                <br />
                                                Para solicitar o arquivo de log de outros meses, entre em contato com 
                                                nosso suporte 
                                                no site:<br />
                                                <a href='http://sustentar.inf.br' target='_blank'>www.sustentar.inf.br</a></div>
                                            <div style='width:100%; height:20px;'></div></div>";
                            email.EnviarAutenticado(25, false);
                            break;
                        }
                    }
            }
            catch (Exception)
            {
                continue;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }
    }
}