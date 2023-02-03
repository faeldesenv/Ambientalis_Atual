using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "vendas", Name = "Modelo.Venda, Modelo")]
    public partial class Venda : ObjetoBase
    {
        #region _______Atributos_______

        private DateTime data;
        private int carencia;
        private bool cancelado;
        private IList<Mensalidade> mensalidades;
        private Prospecto prospecto;

        #endregion

        public Venda(int id) { this.Id = id; }
        public Venda(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Venda() { }

        #region _______Propriedades_______

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

        [Property]
        public virtual int Carencia
        {
            get { return carencia; }
            set { carencia = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Cancelado
        {
            get { return cancelado; }
            set { cancelado = value; }
        }


        [Bag(Name = "Mensalidades", Table = "mensalidades", Cascade = "delete")]
        [Key(2, Column = "id_venda")]
        [OneToMany(3, Class = "Modelo.Mensalidade, Modelo")]
        public virtual IList<Mensalidade> Mensalidades
        {
            get { return mensalidades; }
            set { mensalidades = value; }
        }

        [ManyToOne(Class = "Modelo.Prospecto, Modelo", Column = "id_prospecto")]
        public virtual Prospecto Prospecto
        {
            get { return prospecto; }
            set { prospecto = value; }
        }

        #endregion
    }
}
