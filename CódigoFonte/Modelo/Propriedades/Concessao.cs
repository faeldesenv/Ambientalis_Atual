using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using System;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "concessoes", Extends = "Modelo.Regime, Modelo", Name = "Modelo.Concessao, Modelo")]
    [Key(Column = "id")]
    public partial class Concessao : Regime
    {
        #region ___________ Atributos ___________
        #endregion

        public Concessao(int id) { this.Id = id; }
        public Concessao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Concessao() { }

    }
}
