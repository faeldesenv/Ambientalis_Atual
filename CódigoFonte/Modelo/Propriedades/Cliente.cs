using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "clientes", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Cliente, Modelo")]
    [Key(Column = "id")]
    public partial class Cliente : Pessoa
    {
        public Cliente(int id) { this.Id = id; }
        public Cliente(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Cliente() { }

        #region __________Atributos___________

        private IList<ContratoDiverso> contratosDiversos;
        private Atividade atividade;
        
        #endregion

        #region _______Propriedades_________

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_cliente")]
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
