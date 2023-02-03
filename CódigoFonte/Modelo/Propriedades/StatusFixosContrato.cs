using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "status_fixos_contratos", Extends = "Modelo.StatusContratoDiverso, Modelo", Name = "Modelo.StatusFixosContrato, Modelo")]
    [Key(Column = "id")]
    public partial class StatusFixosContrato : StatusContratoDiverso
    {
        public StatusFixosContrato(int id) { this.Id = id; }
        public StatusFixosContrato(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public StatusFixosContrato() { }
    }
}
