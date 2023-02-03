using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "outros_processos", Extends = "Modelo.Condicional, Modelo", Name = "Modelo.OutrosProcesso, Modelo")]
    [Key(Column = "id")]
    public partial class OutrosProcesso : Condicional
    {
        #region ___________ Atributos ___________

        private Processo processo;
        private DateTime dataRecebimento;

        #endregion

        public OutrosProcesso(int id) { this.Id = id; }
        public OutrosProcesso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OutrosProcesso() { }

        [ManyToOne(Name = "Processo", Column = "id_processo", Class = "Modelo.Processo, Modelo")]
        public virtual Processo Processo
        {
            get { return processo; }
            set { processo = value; }
        }

        [Property(Column = "data_recebimento")]
        public virtual DateTime DataRecebimento
        {
            get
            {
                if (dataRecebimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataRecebimento;
            }
            set { dataRecebimento = value; }
        }
    }
}
