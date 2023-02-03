using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.DadosPessoaComercial, Modelo", Table = "dados_pessoas_comerciais")]
    [Discriminator(3, Column = "Tipo", Type = "String")]
    public abstract class DadosPessoaComercial : ObjetoBase
    {
        private bool isentoICMS;
        private string inscricaoEstadual;

        #region ________Propriedades________

        [Property(Type = "TrueFalse")]
        public virtual bool IsentoICMS
        {
            get { return isentoICMS; }
            set { isentoICMS = value; }
        }

        [Property(Column = "inscricao_estadual")]
        public virtual string InscricaoEstadual
        {
            get { return inscricaoEstadual; }
            set { inscricaoEstadual = value; }
        }

        #endregion
    }
}
