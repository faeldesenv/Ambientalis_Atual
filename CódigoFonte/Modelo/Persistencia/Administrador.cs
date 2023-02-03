using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Administrador : Pessoa
    {
        public const int idLogus = 1;
        public const int idAmbientalis = 2;

        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Administrador ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Administrador classe = new Administrador();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Administrador>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Administrador ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Administrador>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Administrador Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Administrador>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Administrador SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Administrador>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Administrador> SalvarTodos(IList<Administrador> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Administrador>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Administrador> SalvarTodos(params Administrador[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Administrador>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Administrador>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Administrador>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Administrador> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Administrador obj = Activator.CreateInstance<Administrador>();
            return fabrica.GetDAOBase().ConsultarTodos<Administrador>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Administrador> ConsultarOrdemAcendente(int qtd)
        {
            Administrador ee = new Administrador();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Administrador>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Administrador> ConsultarOrdemDescendente(int qtd)
        {
            Administrador ee = new Administrador();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Administrador>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Administrador> Filtrar(int qtd)
        {
            Administrador estado = new Administrador();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Administrador>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual Administrador UltimoInserido()
        {
            Administrador estado = new Administrador();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Administrador>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<Administrador> ConsultarTodosOrdemAlfabetica()
        {
            Administrador aux = new Administrador();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Administrador>(aux);
        }

        public static bool ValidarAdministradorSenhaAtivacao(ref Administrador adm)
        {
            Administrador aux = new Administrador();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("SenhaAtivacao", adm.SenhaAtivacao));
            aux.AdicionarFiltro(Filtros.Eq("Id", adm.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Administrador> users = fabrica.GetDAOBase().ConsultarComFiltro<Administrador>(aux);
            if (users != null && users.Count > 0)
            {
                adm = users[0];
                return true;
            }
            return false;
        }


        public virtual IList<Usuario> GetUsuarios 
        {
            get 
            {
                Usuario aux = new Usuario();
                aux.AdicionarFiltro(Filtros.Distinct());
                aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
                aux.AdicionarFiltro(Filtros.CriarAlias("Administrador", "admin"));
                aux.AdicionarFiltro(Filtros.Eq("admin.Id", this.Id));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(aux);
            }
        }
    }
}
