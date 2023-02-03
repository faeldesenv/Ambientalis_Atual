using System;
using System.Web;
using System.Web.SessionState;
using System.Runtime.Remoting.Messaging;
using Persistencia.Fabrica;
using Modelo.Auditoria;
using Modelo;
using System.Configuration;


namespace Utilitarios
{
    public class Transacao
    {
        public bool destacada = false;

        public void Abrir()
        {
            this.BeginTransaction();
        }

        private void SalvarAuditoria(int idconfig)
        {
            if (IsInWebContext())
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["UsuarioLogado_SistemaAmbiental"] != null)
                {
                    Auditoria_Users user = new Auditoria_Users();
                    user.Ip = HttpContext.Current.Request.UserHostAddress;
                    user.Data = DateTime.Now;
                    user.Usuario = ((Usuario)HttpContext.Current.Session["UsuarioLogado_SistemaAmbiental"]).Login;
                    user.Salvar();
                }
            }

        }

        public void Recarregar(ref Msg mensagem)
        {
            this.Fechar(ref mensagem);
            this.Abrir();
        }

        public void Abrir(int id)
        {
            NHibernateSessionManager.Instance.BeginTransaction(id);
        }

        public void Fechar(ref Msg mensagem)
        {
            this.CommitAndCloseSession(ref mensagem);
        }

        public void Fechar(ref Msg mensagem, bool roolBack)
        {
            if (roolBack)
                this.RoolBackAndCloseSession(ref mensagem);
            else
                this.CommitAndCloseSession(ref mensagem);
        }

        private void BeginTransaction()
        {
            if (IsInWebContext())
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["idConfig"] != null)
                {
                    int idconfig = Convert.ToInt32(HttpContext.Current.Session["idConfig"]);
                    NHibernateSessionManager.Instance.BeginTransaction(idconfig);
                    if (ConfigurationManager.AppSettings["AUDITAR"].ToString().ToUpper() == "TRUE")
                        this.SalvarAuditoria(idconfig);
                }
            }
            else
            {
                NHibernateSessionManager.Instance.BeginTransaction(Convert.ToInt32(CallContext.GetData("idConfig")));
            }

        }

        private void RoolBackAndCloseSession(ref Msg mensagem)
        {
            try
            {
                NHibernateSessionManager.Instance.RollbackTransaction();
            }
            catch (Exception ex)
            {
                mensagem = mensagem.CriarMensagem(ex);
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();
            }
        }

        private void CommitAndCloseSession(ref Msg mensagem)
        {
            try
            {
                if (!this.destacada)
                    NHibernateSessionManager.Instance.CommitTransaction();
            }
            catch (Exception ex)
            {
                mensagem = mensagem.CriarMensagem(ex);
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();
            }
        }

        private static bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        public void DestacarSessao()
        {
            this.destacada = true;
            NHibernateSessionManager.Instance.DestacarSessao();
        }

    }
}
