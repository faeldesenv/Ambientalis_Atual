using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "prorrogacoes", Name = "Modelo.ProrrogacaoPrazo, Modelo")]
    public partial class ProrrogacaoPrazo : ObjetoBase
    {
        #region ___________ Atributos ___________

        private int prazoAdicional;
        private string protocoloAdicional;
        private DateTime dataProtocoloAdicional;
        private Vencimento vencimento;

        #endregion

        public ProrrogacaoPrazo(int id) { this.Id = id; }
        public ProrrogacaoPrazo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ProrrogacaoPrazo() { }


        [Property(Column = "data_protocolo_adicional")]
        public virtual DateTime DataProtocoloAdicional
        {
            get
            {
                if (dataProtocoloAdicional <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataProtocoloAdicional;
            }
            set { dataProtocoloAdicional = value; }
        }

        [Property(Column = "protocolo_adicional")]
        public virtual string ProtocoloAdicional
        {
            get { return protocoloAdicional; }
            set { protocoloAdicional = value; }
        }

        [Property(Column = "prazo_adicional")]
        public virtual int PrazoAdicional
        {
            get { return prazoAdicional; }
            set { prazoAdicional = value; }
        }

        [ManyToOne(Name = "Vencimento", Column = "id_vencimento", Class = "Modelo.Vencimento, Modelo")]
        public virtual Vencimento Vencimento
        {
            get { return vencimento; }
            set { vencimento = value; }
        }
        
    }
}
