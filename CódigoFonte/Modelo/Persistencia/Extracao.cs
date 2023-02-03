using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Extracao : Regime
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Extracao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Extracao classe = new Extracao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Extracao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Extracao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Extracao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Extracao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Extracao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Extracao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Extracao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Extracao> SalvarTodos(IList<Extracao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Extracao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Extracao> SalvarTodos(params Extracao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Extracao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Extracao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Extracao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Extracao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Extracao obj = Activator.CreateInstance<Extracao>();
            return fabrica.GetDAOBase().ConsultarTodos<Extracao>(obj);
        }

     
        /// <summary>
        /// Filtra uma certa quantidade de Extracao
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Extracao> Filtrar(int qtd)
        {
            Extracao estado = new Extracao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Extracao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Extracao Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Extracao</returns>
        public virtual Extracao UltimoInserido()
        {
            Extracao estado = new Extracao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Extracao>(estado);
        }

        public virtual Vencimento GetUltimoVencimento
        {
            get
            {
                if (this.Vencimentos != null && this.Vencimentos.Count > 0)
                {
                    return Vencimentos[Vencimentos.Count - 1];
                }
                return null;
            }
        }

        public override string GetDescricao
        {
            get { return "Extração"; }
        }


        public static bool ConsultarPorProcessoEDataDeAbertura(int idProcesso, DateTime dataAbertura)
        {
            Extracao aux = new Extracao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Eq("DataAbertura", dataAbertura));
            aux.AdicionarFiltro(Filtros.CriarAlias("ProcessoDNPM", "proc"));
            aux.AdicionarFiltro(Filtros.Eq("proc.Id", idProcesso));
            aux.AdicionarFiltro(Filtros.Max("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Extracao existente = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Extracao>(aux);

            return existente != null && existente.Id > 0;
        }
    }
}
