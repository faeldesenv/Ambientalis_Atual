using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Filtros;
using Persistencia.Fabrica;

namespace Modelo
{
    public partial class GrupoEconomicoSite:ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static GrupoEconomicoSite ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GrupoEconomicoSite classe = new GrupoEconomicoSite();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<GrupoEconomicoSite>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual GrupoEconomicoSite ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<GrupoEconomicoSite>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual GrupoEconomicoSite Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<GrupoEconomicoSite>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual GrupoEconomicoSite SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<GrupoEconomicoSite>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<GrupoEconomicoSite> SalvarTodos(IList<GrupoEconomicoSite> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<GrupoEconomicoSite>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<GrupoEconomicoSite> SalvarTodos(params GrupoEconomicoSite[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<GrupoEconomicoSite>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<GrupoEconomicoSite>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<GrupoEconomicoSite>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<GrupoEconomicoSite> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GrupoEconomicoSite obj = Activator.CreateInstance<GrupoEconomicoSite>();
            return fabrica.GetDAOBase().ConsultarTodos<GrupoEconomicoSite>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de GrupoEconomicoSite
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<GrupoEconomicoSite> Filtrar(int qtd)
        {
            GrupoEconomicoSite estado = new GrupoEconomicoSite();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomicoSite>(estado);
        }

        /// <summary>
        /// Retorna o ultimo GrupoEconomicoSite Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo GrupoEconomicoSite</returns>
        public virtual GrupoEconomicoSite UltimoInserido()
        {
            GrupoEconomicoSite estado = new GrupoEconomicoSite();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<GrupoEconomicoSite>(estado);
        }
    }
}
