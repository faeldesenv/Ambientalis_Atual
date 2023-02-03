using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Substancia, Modelo", Table = "substancias")]
    public partial class Substancia : ObjetoBase
    {
        #region ________________ Atributos __________________

        private string nome;
        private string protocolo;
        private DateTime data;
        private ProcessoDNPM processoDNPM;

        #endregion

        #region ________________ Propriedades __________________

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property]
        public virtual string Protocolo
        {
            get { return protocolo; }
            set { protocolo = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get
            {
                if (data <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return data;
            }
            set { data = value; }
        }

        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processo_dnpm", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }

        #endregion
    }
}
