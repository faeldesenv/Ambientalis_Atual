using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "tipos_diversos", Name = "Modelo.TipoDiverso, Modelo")]
    public partial class TipoDiverso: ObjetoBase
    {
        #region ________Atributos________

        private string nome;
        private IList<StatusDiverso> statusDiversos;        
        private IList<Diverso> diversos;

        #endregion

        public TipoDiverso(int id) { this.Id = id; }
        public TipoDiverso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoDiverso() { }

        #region _______Propriedades_______

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Diversos", Table = "diversos", Cascade = "delete")]
        [Key(2, Column = "id_tipo_diverso")]
        [OneToMany(3, Class = "Modelo.Diverso, Modelo")]
        public virtual IList<Diverso> Diversos
        {
            get { return diversos; }
            set { diversos = value; }
        }

        [Bag(Name = "StatusDiversos", Table = "status_diversos", Cascade = "delete")]
        [Key(2, Column = "id_tipo_diverso")]
        [OneToMany(3, Class = "Modelo.StatusDiverso, Modelo")]
        public virtual IList<StatusDiverso> StatusDiversos
        {
            get { return statusDiversos; }
            set { statusDiversos = value; }
        }

        #endregion
    }
}
