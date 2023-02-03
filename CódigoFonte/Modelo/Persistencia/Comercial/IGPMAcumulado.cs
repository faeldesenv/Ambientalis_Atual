using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class IGPMAcumulado : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static IGPMAcumulado ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IGPMAcumulado classe = new IGPMAcumulado();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<IGPMAcumulado>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual IGPMAcumulado ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<IGPMAcumulado>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual IGPMAcumulado Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<IGPMAcumulado>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual IGPMAcumulado SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<IGPMAcumulado>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<IGPMAcumulado> SalvarTodos(IList<IGPMAcumulado> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<IGPMAcumulado>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<IGPMAcumulado> SalvarTodos(params IGPMAcumulado[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<IGPMAcumulado>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<IGPMAcumulado>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<IGPMAcumulado>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<IGPMAcumulado> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IGPMAcumulado obj = Activator.CreateInstance<IGPMAcumulado>();
            return fabrica.GetDAOBase().ConsultarTodos<IGPMAcumulado>(obj);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public static IList<IGPMAcumulado> Filtrar(int qtd)
        {
            IGPMAcumulado cidade = new IGPMAcumulado();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<IGPMAcumulado>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual IGPMAcumulado UltimoInserido()
        {
            IGPMAcumulado ArquivoFisico = new IGPMAcumulado();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<IGPMAcumulado>(ArquivoFisico);
        }

        public static IGPMAcumulado Filtrar(DateTime data)
        {
            IGPMAcumulado igpm = new IGPMAcumulado();

            igpm.AdicionarFiltro(Filtros.Between("Data", data, data.AddMonths(1).AddDays(-1)));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<IGPMAcumulado>(igpm);
        }

        public static IList<IGPMAcumulado> FiltrarPorAno(int ano)
        {
            IGPMAcumulado igpm = new IGPMAcumulado();

            DateTime data = new DateTime(ano, 1, 1);
            igpm.AdicionarFiltro(Filtros.Between("Data", data, data.AddYears(1).AddDays(-1)));
            igpm.AdicionarFiltro(Filtros.SetOrderDesc("Data"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<IGPMAcumulado>(igpm);
        }

        public static IList<IGPMAcumulado> FiltrarUltimosDozeMeses(DateTime data)
        {
            IGPMAcumulado igpm = new IGPMAcumulado();

            DateTime dataIni = new DateTime(data.Year, data.Month, 1);
            igpm.AdicionarFiltro(Filtros.Between("Data", dataIni.AddYears(-1), dataIni.AddMonths(1).AddDays(-1)));
            igpm.AdicionarFiltro(Filtros.SetOrderDesc("Data"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<IGPMAcumulado>(igpm);
        }

        public virtual decimal GetValorIGPMAcumulados(DateTime data)
        {
            IList<IGPMAcumulado> igpms = IGPMAcumulado.FiltrarUltimosDozeMeses(data);
            decimal acumulado = 1;
            decimal valorAcumulado = 0;
            decimal valoraux = 1;
            if (igpms != null)
                foreach (IGPMAcumulado igpm in igpms)
                {
                    acumulado = ((igpm.Valor + 100) / 100);
                    valoraux = valoraux * acumulado;
                }
            valorAcumulado = ((valoraux - 1) * 100);
            return valorAcumulado;
        }
    }
}