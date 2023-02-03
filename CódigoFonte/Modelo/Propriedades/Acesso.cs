using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "acessos", Name = "Modelo.Acesso, Modelo")]
    public partial class Acesso : ObjetoBase
    {
        #region ________Atributos________

        private Usuario usuario;
        private string ip;
        private DateTime data;

        #endregion

        public Acesso(int id) { this.Id = id; }
        public Acesso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Acesso() { }

        #region ________Propriedades________

        [ManyToOne(Name = "Usuario", Column = "id_usuario", Class = "Modelo.Usuario, Modelo")]
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        [Property]
        [Column(1, Name = "ip", SqlType = "nchar(255)")]
        public virtual string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        [Property(Column="data")]
        public virtual DateTime Data
        {
            get
            {
                if (data <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return data;
            }
            set { data = value; }
        }

        #endregion

    }
}
