using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "indices_financeiros", Name = "Modelo.IndiceFinanceiro, Modelo")]
    public partial class IndiceFinanceiro : ObjetoBase
    {
        #region ________Atributos________

        private string nome;
        private IList<ContratoDiverso> contratosDiversos;

        #endregion

        public IndiceFinanceiro(int id) { this.Id = id; }
        public IndiceFinanceiro(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public IndiceFinanceiro() { }

        #region ________Propriedades________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_indice_financeiro")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        #endregion
    }
}
