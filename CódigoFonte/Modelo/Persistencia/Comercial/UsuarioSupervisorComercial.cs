using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class UsuarioSupervisorComercial : UsuarioComercial
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static UsuarioSupervisorComercial ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            UsuarioSupervisorComercial classe = new UsuarioSupervisorComercial();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<UsuarioSupervisorComercial>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual UsuarioSupervisorComercial ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<UsuarioSupervisorComercial>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual UsuarioSupervisorComercial Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<UsuarioSupervisorComercial>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual UsuarioSupervisorComercial SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<UsuarioSupervisorComercial>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<UsuarioSupervisorComercial> SalvarTodos(IList<UsuarioSupervisorComercial> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<UsuarioSupervisorComercial>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<UsuarioSupervisorComercial> SalvarTodos(params UsuarioSupervisorComercial[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<UsuarioSupervisorComercial>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<UsuarioSupervisorComercial>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<UsuarioSupervisorComercial>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<UsuarioSupervisorComercial> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            UsuarioSupervisorComercial obj = Activator.CreateInstance<UsuarioSupervisorComercial>();
            return fabrica.GetDAOBase().ConsultarTodos<UsuarioSupervisorComercial>(obj);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<UsuarioSupervisorComercial> Filtrar(int qtd)
        {
            UsuarioSupervisorComercial cidade = new UsuarioSupervisorComercial();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<UsuarioSupervisorComercial>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual UsuarioSupervisorComercial UltimoInserido()
        {
            UsuarioSupervisorComercial ArquivoFisico = new UsuarioSupervisorComercial();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<UsuarioSupervisorComercial>(ArquivoFisico);
        }

    }
}