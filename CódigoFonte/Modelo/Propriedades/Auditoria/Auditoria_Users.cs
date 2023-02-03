using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo.Auditoria
{
    [Serializable]
    [Class(Name = "Modelo.Auditoria.Auditoria_Users, Modelo", Table = "auditoria_users")]
    public partial class Auditoria_Users:ObjetoBase
    {
        #region ________ Atributos ________

        private string usuario;
        private DateTime data;
        private string ip;
        private IList<Auditoria> auditorias;

        #endregion

        #region ________ Construtores ________

        public Auditoria_Users(int id) { this.Id = id; }
        public Auditoria_Users(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Auditoria_Users() { }

        #endregion

        #region ________ Propriedades ________

    
        [Property]
        public virtual string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        [Property]
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

        [Property]
        [Column(1, Name="ip", SqlType="nchar(255)")]
        public virtual string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        [Bag(Name="Auditorias", Table="auditoria")]
        [Key(2, Column="id_usuario")]
        [OneToMany(3, Class="Modelo.Auditoria.Auditoria, Modelo")]
        public virtual IList<Auditoria> Auditorias
        {
            get { return auditorias; }
            set { auditorias = value; }
        }

        #endregion
    }
}
