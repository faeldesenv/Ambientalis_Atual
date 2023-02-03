using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "requerimentos_pesquisas", Name = "Modelo.RequerimentoPesquisa, Modelo", Extends = "Modelo.AutorizacaoPesquisa, Modelo")]
    [Key(Column = "id")]
    public partial class RequerimentoPesquisa : AutorizacaoPesquisa
    {
        #region ________ Atributos ___________

        private DateTime dataRequerimento;

        #endregion

        #region ________ Construtores ________

        public RequerimentoPesquisa(int id) { this.Id = id; }
        public RequerimentoPesquisa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public RequerimentoPesquisa() { }

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "data_requerimento")]
        public virtual DateTime DataRequerimento
        {
            get {
                if (dataRequerimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataRequerimento;
            }
            set { dataRequerimento = value; }
        }

        #endregion
    }
}
