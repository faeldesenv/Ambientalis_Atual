using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "status_editaveis_contratos", Extends = "Modelo.StatusContratoDiverso, Modelo", Name = "Modelo.StatusEditaveisContrato, Modelo")]
    [Key(Column = "id")]
    public partial class StatusEditaveisContrato : StatusContratoDiverso
    {
        public StatusEditaveisContrato(int id) { this.Id = id; }
        public StatusEditaveisContrato(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public StatusEditaveisContrato() { }
    }
}
