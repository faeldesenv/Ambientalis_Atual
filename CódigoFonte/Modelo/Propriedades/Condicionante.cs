using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "condicionantes", Extends = "Modelo.Condicional, Modelo", Name = "Modelo.Condicionante, Modelo")]
    [Key(Column = "id")]
    public partial class Condicionante : Condicional
    {
        #region ___________ Atributos ___________

        private Licenca licenca;

        #endregion

        public Condicionante(int id) { this.Id = id; }
        public Condicionante(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Condicionante() { }

        [ManyToOne(Name = "Licenca", Column = "id_licenca", Class = "Modelo.Licenca, Modelo")]
        public virtual Licenca Licenca
        {
            get { return licenca; }
            set { licenca = value; }
        }
    }
}
