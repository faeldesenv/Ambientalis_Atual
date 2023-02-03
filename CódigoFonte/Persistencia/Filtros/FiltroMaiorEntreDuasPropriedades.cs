using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para a comparação de um valor de uma propriedade com o valor de outra propriedade
    /// </summary>
    [Serializable]
    public class FiltroMaiorEntreDuasPropriedades : Filtro, IFiltroOu
    {
        /// <summary>
        /// O Construtor do Filtro
        /// </summary>
        /// <param name="atributo">O atributo referência</param>
        /// <param name="valor">O valor de referência para valores maiores ou iguais</param>
        public FiltroMaiorEntreDuasPropriedades(String atributo, String valor)
        {
            this.Atributo = atributo;
            this.Valor1 = valor;
        }

        /// <summary>
        /// Adiciona o Filtro
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.Add(Expression.GtProperty(this.Atributo, this.Valor1.ToString()));
        }

        public ICriterion GetExpression()
        {
            return Expression.GtProperty(this.Atributo, this.Valor1.ToString());
        }

        #region IFiltroOu Members

        #endregion
    }
}
