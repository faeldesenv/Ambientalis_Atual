using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "status", Name = "Modelo.Status, Modelo")]
    public partial class Status : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string nome;

        #endregion

        public Status(int id) { this.Id = id; }
        public Status(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Status() { }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

    }
}
