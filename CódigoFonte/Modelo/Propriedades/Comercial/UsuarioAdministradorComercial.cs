using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "usuarios_administradores_comerciais", Extends = "Modelo.UsuarioComercial, Modelo", Name = "Modelo.UsuarioAdministradorComercial, Modelo")]
    [Key(Column = "id")]
    public partial class UsuarioAdministradorComercial: UsuarioComercial
    {
        #region _________Atributos___________

        private string nome;        
        private string email;        

        #endregion

        public UsuarioAdministradorComercial(int id) { this.Id = id; }
        public UsuarioAdministradorComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public UsuarioAdministradorComercial() { }

        #region _________Propriedades___________

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property]
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        #endregion
    }
}
