using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "fornecedores", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Fornecedor, Modelo")]
    [Key(Column = "id")]
    public partial class Fornecedor : Pessoa
    {
        public Fornecedor(int id) { this.Id = id; }
        public Fornecedor(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Fornecedor() { }

        #region ________Atributos________

        private IList<ContratoDiverso> contratosDiversos;
        private Atividade atividade;

        #endregion

        #region _______Propriedades______

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_fornecedor")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        [ManyToOne(Name = "Atividade", Column = "id_atividade", Class = "Modelo.Atividade, Modelo")]
        public virtual Atividade Atividade
        {
            get { return atividade; }
            set { atividade = value; }
        }

        #endregion
    }
}
