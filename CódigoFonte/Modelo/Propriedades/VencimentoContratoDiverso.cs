using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "vencimentos_contratos_diversos", Extends = "Modelo.Vencimento, Modelo", Name = "Modelo.VencimentoContratoDiverso, Modelo")]
    [Key(Column = "id")]
    public partial class VencimentoContratoDiverso : Vencimento
    {
        #region ________Atributos________

        private ContratoDiverso contratoDiverso;
        private ContratoDiverso reajuste;

        #endregion

        public VencimentoContratoDiverso(int id) { this.Id = id; }
        public VencimentoContratoDiverso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public VencimentoContratoDiverso() { }

        #region ________Propriedades________

        [ManyToOne(Name = "ContratoDiverso", Column = "id_contrato_diverso", Class = "Modelo.ContratoDiverso, Modelo", Lazy = Laziness.False)]
        public virtual ContratoDiverso ContratoDiverso
        {
            get { return contratoDiverso; }
            set { contratoDiverso = value; }
        }

        [ManyToOne(Name = "Reajuste", Column = "id_reajuste", Class = "Modelo.ContratoDiverso, Modelo", Lazy = Laziness.False)]
        public virtual ContratoDiverso Reajuste
        {
            get { return reajuste; }
            set { reajuste = value; }
        }

        #endregion
    }
}
