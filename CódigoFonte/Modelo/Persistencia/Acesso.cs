using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Acesso : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Acesso ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Acesso classe = new Acesso();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Acesso>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Acesso ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Acesso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Acesso Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Acesso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Acesso SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Acesso>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Acesso> SalvarTodos(IList<Acesso> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Acesso>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Acesso> SalvarTodos(params Acesso[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Acesso>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Acesso>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Acesso>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Acesso> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Acesso obj = Activator.CreateInstance<Acesso>();
            return fabrica.GetDAOBase().ConsultarTodos<Acesso>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Acesso> ConsultarOrdemAcendente(int qtd)
        {
            Acesso ee = new Acesso();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Acesso>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Acesso> ConsultarOrdemDescendente(int qtd)
        {
            Acesso ee = new Acesso();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Acesso>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Acesso> Filtrar(int qtd)
        {
            Acesso estado = new Acesso();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Acesso>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual Acesso UltimoInserido()
        {
            Acesso estado = new Acesso();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Acesso>(estado);
        }

        public static IList<Acesso> FiltrarRelatorio(int idAdministrador, int idGrupoEconomico, int idUsuario, DateTime dataDe, DateTime dataAte)
        {
            Acesso acesso = new Acesso();
            acesso.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
            acesso.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            if (idUsuario > 0 || idAdministrador > 0 || idGrupoEconomico > 0) 
            {
                acesso.AdicionarFiltro(Filtros.CriarAlias("Usuario", "usu"));

                if (idUsuario > 0)
                    acesso.AdicionarFiltro(Filtros.Eq("usu.Id", idUsuario));
                else if (idGrupoEconomico > 0) 
                {
                    acesso.AdicionarFiltro(Filtros.CriarAlias("usu.GrupoEconomico", "grup"));
                    acesso.AdicionarFiltro(Filtros.Eq("grup.Id", idGrupoEconomico));
                }
                else if (idAdministrador > 0) 
                {
                    acesso.AdicionarFiltro(Filtros.CriarAlias("usu.Administrador", "adm"));
                    acesso.AdicionarFiltro(Filtros.Eq("adm.Id", idAdministrador));
                }                
            }
            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Acesso>(acesso);
        }

        public virtual string GetGrupoAdministrador
        { 
            get 
            {
                if (this.Usuario != null && this.Usuario.GrupoEconomico != null && this.Usuario.GrupoEconomico.Id > 0 && this.Usuario.Administrador == null)
                    return this.Usuario.GrupoEconomico.Nome;
                else if (this.Usuario != null && this.Usuario.Administrador != null && this.Usuario.Administrador.Id > 0 && this.Usuario.GrupoEconomico == null)
                    return this.Usuario.Administrador.Nome;

                return "Não definido";
            }
        }

        public virtual string GetUsuario 
        {
            get 
            {
                return this.Usuario != null ? this.Usuario.Nome : "Não definido";
            }
        }
        
    }
}
