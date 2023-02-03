using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "interacoes", Name = "Modelo.Interacao, Modelo")]
    public partial class Interacao : ObjetoBase
    {
        #region ___________ Atributos ___________

        private DateTime data;
        private string tipo;
        private string status;
        private string nomePessoa;
        private string cargoPessoa;
        private string descricao;
        private Prospecto prospecto;

        #endregion

        public Interacao(int id) { this.Id = id; }
        public Interacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Interacao() { }


        [Property]
        [Column(1, SqlType = "text", Name = "emails")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property]
        public virtual string CargoPessoa
        {
            get { return cargoPessoa; }
            set { cargoPessoa = value; }
        }

        [Property]
        public virtual string NomePessoa
        {
            get { return nomePessoa; }
            set { nomePessoa = value; }
        }

        [Property]
        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }

        [Property]
        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [Property]
        public virtual DateTime Data
        {
            get
            {
                if (data <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return data;
            }
            set { data = value; }
        }

        [ManyToOne(Name = "Prospecto", Column = "id_prospecto", Class = "Modelo.Prospecto, Modelo")]
        public virtual Prospecto Prospecto
        {
            get { return prospecto; }
            set { prospecto = value; }
        }
    }
}
