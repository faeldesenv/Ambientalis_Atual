using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "usuarios_supervisores", Extends = "Modelo.UsuarioComercial, Modelo", Name = "Modelo.UsuarioSupervisorComercial, Modelo")]
    [Key(Column = "id")]
    public partial class UsuarioSupervisorComercial: UsuarioComercial
    {
        #region _________Atributos___________

        private string nome;        
        private string email;        

        #endregion

        public UsuarioSupervisorComercial(int id) { this.Id = id; }
        public UsuarioSupervisorComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public UsuarioSupervisorComercial() { }

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
