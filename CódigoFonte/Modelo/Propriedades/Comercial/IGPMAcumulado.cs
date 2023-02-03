using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "igmp_acumulados", Name = "Modelo.IGPMAcumulado, Modelo")]
    public partial class IGPMAcumulado : ObjetoBase
    {
        #region _______Atributos_______

        private DateTime data;
        private decimal valor;

        #endregion

        public IGPMAcumulado(int id) { this.Id = id; }
        public IGPMAcumulado(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public IGPMAcumulado() { }

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
        public virtual decimal Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        #endregion
    }
}
