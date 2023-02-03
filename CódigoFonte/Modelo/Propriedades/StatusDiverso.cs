using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "status_diversos", Name = "Modelo.StatusDiverso, Modelo")]
    public partial class StatusDiverso : ObjetoBase
    {
        #region _______Atributos________

        private string nome;
        private TipoDiverso tipoDiverso;        
        private IList<VencimentoDiverso> vencimentosDiversos;        

        #endregion

        public StatusDiverso(int id) { this.Id = id; }
        public StatusDiverso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public StatusDiverso() { }

        #region ______Propriedades_______

        [Property(Column="nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [ManyToOne(Name = "TipoDiverso", Column = "id_tipo_diverso", Class = "Modelo.TipoDiverso, Modelo")]
        public virtual TipoDiverso TipoDiverso
        {
            get { return tipoDiverso; }
            set { tipoDiverso = value; }
        }

        [Bag(Name = "VencimentosDiversos", Table = "vencimentos_diversos", Cascade = "delete")]
        [Key(2, Column = "id_status_diverso")]
        [OneToMany(3, Class = "Modelo.VencimentoDiverso, Modelo")]
        public virtual IList<VencimentoDiverso> VencimentosDiversos
        {
            get { return vencimentosDiversos; }
            set { vencimentosDiversos = value; }
        }

        #endregion
    }
}
