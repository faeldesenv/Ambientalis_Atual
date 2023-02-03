using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class ArquivoFisico : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ArquivoFisico ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ArquivoFisico classe = new ArquivoFisico();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ArquivoFisico>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ArquivoFisico ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ArquivoFisico>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ArquivoFisico Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ArquivoFisico>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ArquivoFisico SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ArquivoFisico>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ArquivoFisico> SalvarTodos(IList<ArquivoFisico> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ArquivoFisico>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ArquivoFisico> SalvarTodos(params ArquivoFisico[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ArquivoFisico>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ArquivoFisico>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ArquivoFisico>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ArquivoFisico> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ArquivoFisico obj = Activator.CreateInstance<ArquivoFisico>();
            return fabrica.GetDAOBase().ConsultarTodos<ArquivoFisico>(obj);
        }

     
        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ArquivoFisico> Filtrar(int qtd)
        {
            ArquivoFisico ArquivoFisico = new ArquivoFisico();
            if (qtd > 0)
                ArquivoFisico.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ArquivoFisico>(ArquivoFisico);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual ArquivoFisico UltimoInserido()
        {
            ArquivoFisico ArquivoFisico = new ArquivoFisico();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ArquivoFisico>(ArquivoFisico);
        }
    }
}
