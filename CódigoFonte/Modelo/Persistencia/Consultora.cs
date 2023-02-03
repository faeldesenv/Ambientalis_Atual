using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Consultora : Pessoa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Consultora ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Consultora classe = new Consultora();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Consultora>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Consultora ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Consultora>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Consultora Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Consultora>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Consultora SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Consultora>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Consultora> SalvarTodos(IList<Consultora> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Consultora>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Consultora> SalvarTodos(params Consultora[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Consultora>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Consultora>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Consultora>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Consultora> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Consultora obj = Activator.CreateInstance<Consultora>();
            return fabrica.GetDAOBase().ConsultarTodos<Consultora>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Consultora> ConsultarOrdemAcendente(int qtd)
        {
            Consultora ee = new Consultora();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Consultora>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Consultora> ConsultarOrdemDescendente(int qtd)
        {
            Consultora ee = new Consultora();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Consultora>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Consultora
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Consultora> Filtrar(int qtd)
        {
            Consultora estado = new Consultora();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Consultora>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Consultora Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Consultora</returns>
        public virtual Consultora UltimoInserido()
        {
            Consultora estado = new Consultora();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Consultora>(estado);
        }

        /// <summary>
        /// Consulta todos os Consultoras armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Consultoras armazenados ordenados pelo Nome</returns>
        public static IList<Consultora> ConsultarTodosOrdemAlfabetica()
        {
            Consultora aux = new Consultora();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Consultora>(aux);
        }

        public static IList<Consultora> Filtrar(string nome, string site, string responsavel)
        {
            Consultora Consultora = new Consultora();
            if (!string.IsNullOrEmpty(nome))
                Consultora.AdicionarFiltro(Filtros.Like("Nome", nome));
            if (!string.IsNullOrEmpty(responsavel))
                Consultora.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));
            if (!string.IsNullOrEmpty(site))
                Consultora.AdicionarFiltro(Filtros.Like("Site", site));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Consultora>(Consultora);
        }

        public static IList<Consultora> Filtrar(string nome, string responsavel, int status, string cpfCnpj, Cidade cidade, Estado estado)
        {
            Consultora cons = new Consultora();
            cons.AdicionarFiltro(Filtros.Like("Nome", nome));
            cons.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));

            if (status > 0)
                cons.AdicionarFiltro(Filtros.Eq("Ativo", status == 1 ? true : false));

            cons.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
            cons.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cnpj", cpfCnpj), Filtros.Like("dp.Cpf", cpfCnpj)));

            if (cidade != null && cidade.Id > 0)
            {
                cons.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                cons.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                cons.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null) 
            {
                cons.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                cons.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                cons.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                cons.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Consultora>(cons);
        }

        public static IList<Pessoa> ConsultarTodosComoPessoas()
        {
            Consultora aux = new Consultora();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pessoa>((Pessoa)aux);
        }
    }
}
