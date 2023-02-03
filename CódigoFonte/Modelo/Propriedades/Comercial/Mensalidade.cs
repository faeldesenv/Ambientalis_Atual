using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "mensalidades", Name = "Modelo.Mensalidade, Modelo")]
    public partial class Mensalidade : ObjetoBase
    {
        #region _______Atributos_______
        
        private int mes;
        private int ano;
        private bool pagoRevenda;
        private bool pagoSupervisor;
        private IList<PeriodoDeUso> periodosDeUso;
        private Venda venda;        

        #endregion

        public Mensalidade(int id) { this.Id = id; }
        public Mensalidade(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Mensalidade() { }

        #region _______Propriedades_______

       
        [Property(Column = "mes")]
        public virtual int Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        [Property(Column = "ano")]
        public virtual int Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        [Bag(Name = "PeriodosDeUso", Table = "periodos_de_uso", Cascade = "delete")]
        [Key(2, Column = "id_mensalidade")]
        [OneToMany(3, Class = "Modelo.PeriodoDeUso, Modelo")]
        public virtual IList<PeriodoDeUso> PeriodosDeUso
        {
            get { return periodosDeUso; }
            set { periodosDeUso = value; }
        }

        [ManyToOne(Name = "Venda", Column = "id_venda", Class = "Modelo.Venda, Modelo", Cascade = "delete")]
        public virtual Venda Venda
        {
            get { return venda; }
            set { venda = value; }
        }

        [Property(Type = "TrueFalse", Column="pago_revenda")]
        public virtual bool PagoRevenda
        {
            get { return pagoRevenda; }
            set { pagoRevenda = value; }
        }

        [Property(Type = "TrueFalse", Column = "pago_supervisor")]
        public virtual bool PagoSupervisor
        {
            get { return pagoSupervisor; }
            set { pagoSupervisor = value; }
        }

        #endregion
    }
}
