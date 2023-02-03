using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "formas_recebimentos", Name = "Modelo.FormaRecebimento, Modelo")]
    public partial class FormaRecebimento : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string nome;
        private IList<ContratoDiverso> contratosDiversos;

        #endregion

        public FormaRecebimento(int id) { this.Id = id; }
        public FormaRecebimento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public FormaRecebimento() { }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_forma_recebimento")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

    }
}
