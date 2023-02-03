using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "usuarios_revendas", Extends = "Modelo.UsuarioComercial, Modelo", Name = "Modelo.UsuarioRevendaComercial, Modelo")]
    [Key(Column = "id")]
    public partial class UsuarioRevendaComercial: UsuarioComercial
    {
        #region _________Atributos__________

        private Revenda revenda;        

        #endregion

        public UsuarioRevendaComercial(int id) { this.Id = id; }
        public UsuarioRevendaComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public UsuarioRevendaComercial() { }

        #region _________Propriedades__________

        [OneToOne(Name = "Revenda", Class = "Modelo.Revenda", PropertyRef = "UsuarioRevenda")]
        public virtual Revenda Revenda
        {
            get { return revenda; }
            set { revenda = value; }
        }

        #endregion
    }
}
