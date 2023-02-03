using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Filtros;
using Persistencia.Fabrica;

namespace Modelo
{
    public partial class Setup : ObjetoBase
    {
        public const string TermoCompromisso = "TermoCompromisso";

        public static string GetValor(string chave)
        {
            Setup aux = new Setup();
            aux.AdicionarFiltro(Filtros.MaxResults(1));
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Chave", chave));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            aux = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Setup>(aux);
            return aux != null ? aux.Valor : string.Empty;
        }
    }
}
