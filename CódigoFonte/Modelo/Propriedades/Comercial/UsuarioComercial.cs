using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "usuarios_comerciais", Name = "Modelo.UsuarioComercial, Modelo")]
    public partial class UsuarioComercial: ObjetoBase
    {
        #region _________Atributos__________

        private string login;
        private string senha;        

        #endregion

        public UsuarioComercial(int id) { this.Id = id; }
        public UsuarioComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public UsuarioComercial() { }

        #region _________Propriedades__________

        [Property]
        public virtual string Login
        {
            get { return login; }
            set { login = value; }
        }

        [Property]
        public virtual string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        #endregion
    }
}
