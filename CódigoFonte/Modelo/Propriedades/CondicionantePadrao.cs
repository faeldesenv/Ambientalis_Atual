using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "condicionantes_padroes", Name = "Modelo.CondicionantePadrao, Modelo")]
    public partial class CondicionantePadrao : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string descricao;
        private int diasValidade;
        private int diasAviso;
        private TipoLicenca tipoLicenca;
        private bool periodico;

        #endregion

        public CondicionantePadrao(int id) { this.Id = id; }
        public CondicionantePadrao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public CondicionantePadrao() { }

        [ManyToOne(Name = "TipoLicenca", Column = "id_tipo_licenca", Class = "Modelo.TipoLicenca, Modelo")]
        public virtual TipoLicenca TipoLicenca
        {
            get { return tipoLicenca; }
            set { tipoLicenca = value; }
        }

        [Property]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "dias_validade")]
        public virtual int DiasValidade
        {
            get { return diasValidade; }
            set { diasValidade = value; }
        }

        [Property(Column = "dias_aviso")]
        public virtual int DiasAviso
        {
            get { return diasAviso; }
            set { diasAviso = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Periodico
        {
            get { return periodico; }
            set { periodico = value; }
        } 

    }
}
