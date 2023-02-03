using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "requerimentos_lavras", Name = "Modelo.RequerimentoLavra, Modelo", Extends = "Modelo.Concessao, Modelo")]
    [Key(Column = "id")]
    public partial class RequerimentoLavra : Concessao
    {
        #region ________ Atributos ___________

        private DateTime data;

        #endregion

        #region ________ Construtores ________

        public RequerimentoLavra(int id) { this.Id = id; }
        public RequerimentoLavra(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public RequerimentoLavra() { }

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "data")]
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

        #endregion
    }
}
