using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    [Serializable]
    public class FiltroCount : Filtro
    {
        public FiltroCount(String atributo)
        {
            this.Atributo = atributo;
        }

        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetProjection(Projections.Count(this.Atributo));
        }
    }
}
