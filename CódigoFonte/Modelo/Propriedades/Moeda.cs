using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "moedas", Name = "Modelo.Moeda, Modelo")]
    public partial class Moeda : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string nome;
        private string sigla;
        private IList<ContratoDiverso> contratosDiversos;

        #endregion

        public Moeda(int id) { this.Id = id; }
        public Moeda(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Moeda() { }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_moeda")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        [Property(Column = "sigla")]
        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

    }
}
