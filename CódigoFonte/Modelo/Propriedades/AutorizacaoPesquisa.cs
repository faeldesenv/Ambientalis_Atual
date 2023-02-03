using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "autorizacoes_pesquisas", Name = "Modelo.AutorizacaoPesquisa, Modelo", Extends = "Modelo.Regime, Modelo")]
    [Key(Column = "id")]
    public partial class AutorizacaoPesquisa : Regime
    {
        #region ________ Atributos ___________

        #endregion

        #region ________ Construtores ________

        public AutorizacaoPesquisa(int id) { this.Id = id; }
        public AutorizacaoPesquisa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public AutorizacaoPesquisa() { }

        #endregion

        #region ________ Propriedades ________

        #endregion
    }
}
