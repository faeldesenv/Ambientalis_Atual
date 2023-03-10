using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "centros_custo", Name = "Modelo.CentroCusto, Modelo")]
    public partial class CentroCusto : ObjetoBase
    {
        #region ________Atributos________

        private string nome;
        private IList<ContratoDiverso> contratosDiversos;

        #endregion

        public CentroCusto(int id) { this.Id = id; }
        public CentroCusto(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public CentroCusto() { }

        #region ________Propriedades________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_centro_custo")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        #endregion
    }
}
