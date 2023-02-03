using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class ConfiguracaoPermissaoModulo : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ConfiguracaoPermissaoModulo ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ConfiguracaoPermissaoModulo classe = new ConfiguracaoPermissaoModulo();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ConfiguracaoPermissaoModulo>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ConfiguracaoPermissaoModulo ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ConfiguracaoPermissaoModulo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ConfiguracaoPermissaoModulo Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ConfiguracaoPermissaoModulo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ConfiguracaoPermissaoModulo SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ConfiguracaoPermissaoModulo>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ConfiguracaoPermissaoModulo> SalvarTodos(IList<ConfiguracaoPermissaoModulo> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ConfiguracaoPermissaoModulo>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ConfiguracaoPermissaoModulo> SalvarTodos(params ConfiguracaoPermissaoModulo[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ConfiguracaoPermissaoModulo>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ConfiguracaoPermissaoModulo>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ConfiguracaoPermissaoModulo>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ConfiguracaoPermissaoModulo> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ConfiguracaoPermissaoModulo obj = Activator.CreateInstance<ConfiguracaoPermissaoModulo>();
            return fabrica.GetDAOBase().ConsultarTodos<ConfiguracaoPermissaoModulo>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ConfiguracaoPermissaoModulo> ConsultarOrdemAcendente(int qtd)
        {
            ConfiguracaoPermissaoModulo ee = new ConfiguracaoPermissaoModulo();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ConfiguracaoPermissaoModulo>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ConfiguracaoPermissaoModulo> ConsultarOrdemDescendente(int qtd)
        {
            ConfiguracaoPermissaoModulo ee = new ConfiguracaoPermissaoModulo();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ConfiguracaoPermissaoModulo>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ConfiguracaoPermissaoModulo> Filtrar(int qtd)
        {
            ConfiguracaoPermissaoModulo estado = new ConfiguracaoPermissaoModulo();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ConfiguracaoPermissaoModulo>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual ConfiguracaoPermissaoModulo UltimoInserido()
        {
            ConfiguracaoPermissaoModulo estado = new ConfiguracaoPermissaoModulo();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ConfiguracaoPermissaoModulo>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<ConfiguracaoPermissaoModulo> ConsultarTodosOrdemAlfabetica()
        {
            ConfiguracaoPermissaoModulo aux = new ConfiguracaoPermissaoModulo();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ConfiguracaoPermissaoModulo>(aux);
        }

        /// <summary>
        /// Obtem a configuração do módulo de permissão do grupo logado
        /// </summary>
        /// <returns>a configuração do módulo de permissão do grupo logado</returns>
        public static ConfiguracaoPermissaoModulo ConsultarPorEmpEModulo(int idEmp, int idModulo)
        {
            ConfiguracaoPermissaoModulo aux = new ConfiguracaoPermissaoModulo();            
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Emp", idEmp));

            if (idModulo > 0) 
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("ModuloPermissao", "modPer"));
                aux.AdicionarFiltro(Filtros.Eq("modPer.Id", idModulo));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ConfiguracaoPermissaoModulo>(aux);
        }

        /// <summary>
        /// Usuado somente para a base da Ambientalis que nao possui emp
        /// </summary>
        /// <returns>a configuração do módulo de permissão do usuario da base ambientalis logado</returns>
        public static ConfiguracaoPermissaoModulo ConsultarPorModulo(int idModulo)
        {
            ConfiguracaoPermissaoModulo aux = new ConfiguracaoPermissaoModulo();
            aux.AdicionarFiltro(Filtros.Distinct());            

            if (idModulo > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("ModuloPermissao", "modPer"));
                aux.AdicionarFiltro(Filtros.Eq("modPer.Id", idModulo));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ConfiguracaoPermissaoModulo>(aux);
        }
    }
}
