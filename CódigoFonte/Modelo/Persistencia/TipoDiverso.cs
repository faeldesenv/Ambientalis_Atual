using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class TipoDiverso: ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static TipoDiverso ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TipoDiverso classe = new TipoDiverso();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<TipoDiverso>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual TipoDiverso ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<TipoDiverso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual TipoDiverso Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<TipoDiverso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual TipoDiverso SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<TipoDiverso>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TipoDiverso> SalvarTodos(IList<TipoDiverso> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TipoDiverso>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TipoDiverso> SalvarTodos(params TipoDiverso[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TipoDiverso>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<TipoDiverso>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<TipoDiverso>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<TipoDiverso> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TipoDiverso obj = Activator.CreateInstance<TipoDiverso>();
            return fabrica.GetDAOBase().ConsultarTodos<TipoDiverso>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<TipoDiverso> ConsultarOrdemAcendente(int qtd)
        {
            TipoDiverso ee = new TipoDiverso();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoDiverso>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<TipoDiverso> ConsultarOrdemDescendente(int qtd)
        {
            TipoDiverso ee = new TipoDiverso();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoDiverso>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Estado
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<TipoDiverso> Filtrar(int qtd)
        {
            TipoDiverso estado = new TipoDiverso();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoDiverso>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Estado Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Estado</returns>
        public virtual TipoDiverso UltimoInserido()
        {
            TipoDiverso estado = new TipoDiverso();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<TipoDiverso>(estado);
        }

        /// <summary>
        /// Consulta todos os Estados armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Estados armazenados ordenados pelo Nome</returns>
        public static IList<TipoDiverso> ConsultarTodosOrdemAlfabetica()
        {
            TipoDiverso aux = new TipoDiverso();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoDiverso>(aux);
        }

        public static TipoDiverso ObterTipoDoVencimento(VencimentoDiverso vencimento)
        {
            TipoDiverso aux = new TipoDiverso();
            aux.AdicionarFiltro(Filtros.CriarAlias("Diversos", "div"));
            aux.AdicionarFiltro(Filtros.CriarAlias("div.VencimentosDiversos", "venc"));
            if (vencimento != null && vencimento.Id > 0) 
            {
                aux.AdicionarFiltro(Filtros.Eq("venc.Id", vencimento.Id));
            }            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<TipoDiverso>(aux);
        }
    }
}
