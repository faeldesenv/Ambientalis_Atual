using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "concessoes_lavras", Name = "Modelo.ConcessaoLavra, Modelo", Extends = "Modelo.Concessao, Modelo")]
    [Key(Column = "id")]
    public partial class ConcessaoLavra : Concessao
    {
        #region ________ Atributos ___________

        private DateTime dataRelatorioReavaliacaoReserva;
        private Vencimento requerimentoImissaoPosse;
        private string numeroPortariaLavra;
        private DateTime data;

        #endregion

        #region ________ Construtores ________

        public ConcessaoLavra(int id) { this.Id = id; }
        public ConcessaoLavra(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ConcessaoLavra() { }

        #endregion

        #region ________ Propriedades ________

        [ManyToOne(Name = "RequerimentoImissaoPosse", Column = "id_vencimento", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento RequerimentoImissaoPosse
        {
            get { return requerimentoImissaoPosse; }
            set { requerimentoImissaoPosse = value; }
        }

        [Property(Column = "Data")]
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

        [Property(Column = "data_reavaliacao_reserva")]
        public virtual DateTime DataRelatorioReavaliacaoReserva
        {
            get
            {
                if (dataRelatorioReavaliacaoReserva <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataRelatorioReavaliacaoReserva;
            }
            set { dataRelatorioReavaliacaoReserva = value; }
        }


        [Property(Column = "numero_portaria_lavra")]
        public virtual string NumeroPortariaLavra
        {
            get { return numeroPortariaLavra; }
            set { numeroPortariaLavra = value; }
        }

        #endregion
    }
}
