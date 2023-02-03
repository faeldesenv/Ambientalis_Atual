using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Setup, Modelo", Table = "setups")]
    public partial class Setup : ObjetoBase
    {
        #region ________________ Atributos __________________

        private string chave;
        private string valor;

        #endregion

        #region ________________ Propriedades __________________

        [Property]
        public virtual string Chave
        {
            get { return chave; }
            set { chave = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "Valor")]
        public virtual string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        #endregion
    }
}
