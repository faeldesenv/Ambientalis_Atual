using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "orgaos_federais", Extends = "Modelo.OrgaoAmbiental, Modelo", Name = "Modelo.OrgaoFederal, Modelo")]
    [Key(Column = "id")]
    public partial class OrgaoFederal : OrgaoAmbiental
    {
        #region ___________ Atributos ___________

        #endregion

        public OrgaoFederal(int id) { this.Id = id; }
        public OrgaoFederal(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OrgaoFederal() { }

    }
}
