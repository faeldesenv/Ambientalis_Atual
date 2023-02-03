using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.EventoDNPM, Modelo", Table = "eventos_dnpm")]
    public partial class EventoDNPM : ObjetoBase
    {
        #region ________________ Atributos __________________

        private string descricao;
        private DateTime data;
        private bool atualizado;
        private bool irrelevante;
        private ProcessoDNPM processoDNPM;

        #endregion

        public EventoDNPM(int id) { this.Id = id; }
        public EventoDNPM(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public EventoDNPM() { }

        #region ________________ Propriedades __________________

        [Property]
        [Column(1, SqlType = "text", Name = "Descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Atualizado
        {
            get { return atualizado; }
            set { atualizado = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Irrelevante
        {
            get { return irrelevante; }
            set { irrelevante = value; }
        }

        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processo_dnpm", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }
        #endregion

        public override bool Equals(object obj)
        {
            return this.Descricao == ((EventoDNPM)obj).Descricao && this.Data.Date == ((EventoDNPM)obj).Data.Date;
        }

    }
}
