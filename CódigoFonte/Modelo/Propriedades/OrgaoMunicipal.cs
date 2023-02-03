using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "orgaos_municipais", Extends = "Modelo.OrgaoAmbiental, Modelo", Name = "Modelo.OrgaoMunicipal, Modelo")]
    [Key(Column = "id")]
    public partial class OrgaoMunicipal : OrgaoAmbiental
    {
        #region ___________ Atributos ___________

        private Cidade cidade;

        #endregion

        public OrgaoMunicipal(int id) { this.Id = id; }
        public OrgaoMunicipal(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OrgaoMunicipal() { }

        [ManyToOne(Name = "Cidade", Column = "id_cidade", Class = "Modelo.Cidade, Modelo")]
        public virtual Cidade Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }
    }
}
