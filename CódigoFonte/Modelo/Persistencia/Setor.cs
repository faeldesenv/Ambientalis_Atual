using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Setor : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Setor ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Setor classe = new Setor();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Setor>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Setor ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Setor>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Setor Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Setor>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Setor SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Setor>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Setor> SalvarTodos(IList<Setor> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Setor>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Setor> SalvarTodos(params Setor[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Setor>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Setor>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Setor>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Setor> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Setor obj = Activator.CreateInstance<Setor>();
            return fabrica.GetDAOBase().ConsultarTodos<Setor>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Setor> ConsultarOrdemAcendente(int qtd)
        {
            Setor ee = new Setor();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Setor>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Setor> ConsultarOrdemDescendente(int qtd)
        {
            Setor ee = new Setor();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Setor>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Setor> Filtrar(int qtd)
        {
            Setor estado = new Setor();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Setor>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual Setor UltimoInserido()
        {
            Setor estado = new Setor();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Setor>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<Setor> ConsultarTodosOrdemAlfabetica()
        {
            Setor aux = new Setor();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Setor>(aux);
        }

        public static IList<Setor> RecarregarSetores(IList<Setor> setoresDaSessao)
        {
            IList<Setor> lista = new List<Setor>();
            if (setoresDaSessao != null)
            {
                foreach (Setor item in setoresDaSessao)
                {
                    Setor not = Setor.ConsultarPorId(item.Id);
                    if (lista.Contains(not))
                    {
                        lista.Remove(not);
                        lista.Add(not);
                    }
                    else
                    {
                        lista.Add(not);
                    }

                }
            }
            return lista;
        }

        public static IList<Setor> ConsultarTodosComoObjetos()
        {
            Setor ee = new Setor();

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Setor>(ee);            
        }

        public static IList<Setor> ObterSetoresQueOUsuarioPossuiAcesso(Usuario usuario)
        {
            IList<Setor> retorno = new List<Setor>();

            IList<Setor> setores = Setor.ConsultarTodos();

            if (setores == null || setores.Count == 0)
                return retorno;

            foreach (Setor setor in setores)
            {
                if ((setor.UsuariosVisualizacao == null || setor.UsuariosVisualizacao.Count == 0) || (setor.UsuariosVisualizacao != null && setor.UsuariosVisualizacao.Count > 0 && setor.UsuariosVisualizacao.Contains(usuario)))
                    retorno.Add(setor);
            }

            return retorno;
        }

        public static IList<Setor> ObterSetoresQueOUsuarioPodeEditarDoModulo(Usuario usuario)
        {
            IList<Setor> retorno = new List<Setor>();

            IList<Setor> setores = Setor.ConsultarTodos();

            if (setores == null || setores.Count == 0)
                return retorno;

            foreach (Setor setor in setores)
            {
                if (setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0 && setor.UsuariosEdicao.Contains(usuario))
                    retorno.Add(setor);
            }

            return retorno;
        }

        public static IList<Setor> ConsultarSetoresDoCliente(GrupoEconomico grupoContratos)
        {
            Setor aux = new Setor();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.CriarAlias("ContratosDiversos", "conts"));
            aux.AdicionarFiltro(Filtros.CriarAlias("conts.Empresa", "empre"));
            aux.AdicionarFiltro(Filtros.CriarAlias("empre.GrupoEconomico", "grup"));
            aux.AdicionarFiltro(Filtros.Eq("grup.Id", grupoContratos.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Setor>(aux);
        }
    }
}
