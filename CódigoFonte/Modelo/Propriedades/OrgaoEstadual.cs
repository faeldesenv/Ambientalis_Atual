using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "orgaos_estaduais", Extends = "Modelo.OrgaoAmbiental, Modelo", Name = "Modelo.OrgaoEstadual, Modelo")]
    [Key(Column = "id")]
    public partial class OrgaoEstadual : OrgaoAmbiental
    {
        #region ___________ Atributos ___________

        private Estado estado;

        #endregion

        public OrgaoEstadual(int id) { this.Id = id; }
        public OrgaoEstadual(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OrgaoEstadual() { }

        [ManyToOne(Name = "Estado", Column = "id_estado", Class = "Modelo.Estado, Modelo")]
        public virtual Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
