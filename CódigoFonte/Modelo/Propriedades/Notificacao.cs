using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "notificacoes", Name = "Modelo.Notificacao, Modelo")]
    public partial class Notificacao : ObjetoBase
    {
        #region ___________ Atributos ___________

        private int diasAviso;
        private string emails;
        private string template;
        private int enviado;
        private Vencimento vencimento;
        private DateTime dataUltimoEnvio;
        private string modulo;
        private DateTime data;        

        #endregion

        public Notificacao(int id) { this.Id = id; }
        public Notificacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Notificacao() { }
        public Notificacao(string modulo) { this.Modulo = modulo; }


        [Property(Column = "dias_aviso")]
        public virtual int DiasAviso
        {
            get { return diasAviso; }
            set { diasAviso = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "emails")]
        public virtual string Emails
        {
            get { return emails; }
            set { emails = value; }
        }

        [Property]
        public virtual string Template
        {
            get { return template; }
            set { template = value; }
        }

        [Property]
        public virtual int Enviado
        {
            get { return enviado; }
            set { enviado = value; }
        }

        [ManyToOne(Name = "Vencimento", Column = "id_vencimento", Class = "Modelo.Vencimento, Modelo", Lazy = Laziness.False)]
        public virtual Vencimento Vencimento
        {
            get { return vencimento; }
            set { vencimento = value; }
        }

        [Property(Column = "data_ultimo_envio")]
        public virtual DateTime DataUltimoEnvio
        {
            get
            {
                if (dataUltimoEnvio <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataUltimoEnvio;
            }
            set { dataUltimoEnvio = value; }
        }

        [Property(Column = "modulo")]
        public virtual string Modulo
        {
            get { return modulo; }
            set { modulo = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

    }
}
