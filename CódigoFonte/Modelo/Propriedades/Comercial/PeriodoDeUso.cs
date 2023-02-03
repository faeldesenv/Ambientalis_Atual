using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "periodos_de_uso", Name = "Modelo.PeriodoDeUso, Modelo")]
    public partial class PeriodoDeUso : ObjetoBase
    {
        #region ___________ Atributos ___________

        private int diaInicio;
        private int diaFim;
        private decimal mensalidadeNominal;
        private Mensalidade mensalidade;
        private bool cancelado;

        #endregion

        public PeriodoDeUso(int id) { this.Id = id; }
        public PeriodoDeUso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public PeriodoDeUso() { }


        [Property(Column="dia_inicio")]
        public virtual int DiaInicio
        {
            get { return diaInicio; }
            set { diaInicio = value; }
        }

        [Property(Column="dia_fim")]
        public virtual int DiaFim
        {
            get { return diaFim; }
            set { diaFim = value; }
        }

        [Property(Column="mensalidade_nominal")]
        public virtual decimal MensalidadeNominal
        {
            get { return mensalidadeNominal; }
            set { mensalidadeNominal = value; }
        }

        [ManyToOne(Name = "Mensalidade", Column = "id_mensalidade", Class = "Modelo.Mensalidade, Modelo", Cascade = "delete")]
        public virtual Mensalidade Mensalidade
        {
            get { return mensalidade; }
            set { mensalidade = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Cancelado
        {
            get { return cancelado; }
            set { cancelado = value; }
        }

       
    }
}
