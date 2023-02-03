using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]    
    [JoinedSubclass(Table = "vencimentos_diversos", Extends = "Modelo.Vencimento, Modelo", Name = "Modelo.VencimentoDiverso, Modelo")]
    [Key(Column = "id")]
    public partial class VencimentoDiverso : Vencimento
    {
        #region _______Atributos_______

        private Diverso diverso;        
        private StatusDiverso statusDiverso;        

        #endregion

        public VencimentoDiverso(int id) { this.Id = id; }
        public VencimentoDiverso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public VencimentoDiverso() { }

        #region ________Propriedades_________

        [ManyToOne(Name = "Diverso", Column = "id_diverso", Class = "Modelo.Diverso, Modelo")]
        public virtual Diverso Diverso
        {
            get { return diverso; }
            set { diverso = value; }
        }

        [ManyToOne(Name = "StatusDiverso", Column = "id_status_diverso", Class = "Modelo.StatusDiverso, Modelo")]
        public virtual StatusDiverso StatusDiverso
        {
            get { return statusDiverso; }
            set { statusDiverso = value; }
        }

        #endregion
    }
}
