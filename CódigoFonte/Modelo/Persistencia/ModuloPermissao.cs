using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class ModuloPermissao: ObjetoBase
    {
        public static ModuloPermissao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ModuloPermissao classe = new ModuloPermissao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ModuloPermissao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ModuloPermissao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ModuloPermissao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ModuloPermissao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ModuloPermissao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ModuloPermissao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ModuloPermissao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ModuloPermissao> SalvarTodos(IList<ModuloPermissao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ModuloPermissao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ModuloPermissao> SalvarTodos(params ModuloPermissao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ModuloPermissao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ModuloPermissao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ModuloPermissao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ModuloPermissao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ModuloPermissao obj = Activator.CreateInstance<ModuloPermissao>();
            return fabrica.GetDAOBase().ConsultarTodos<ModuloPermissao>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ModuloPermissao> ConsultarOrdemAcendente(int qtd)
        {
            ModuloPermissao ee = new ModuloPermissao();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ModuloPermissao>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ModuloPermissao> ConsultarOrdemDescendente(int qtd)
        {
            ModuloPermissao ee = new ModuloPermissao();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ModuloPermissao>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ModuloPermissao> Filtrar(int qtd)
        {
            ModuloPermissao estado = new ModuloPermissao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ModuloPermissao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual ModuloPermissao UltimoInserido()
        {
            ModuloPermissao estado = new ModuloPermissao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ModuloPermissao>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<ModuloPermissao> ConsultarTodosOrdemAlfabetica()
        {
            ModuloPermissao aux = new ModuloPermissao();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ModuloPermissao>(aux);
        }

        public static IList<ModuloPermissao> ConsultarTodosOrdemPrioridade(Usuario usuarioLogado)
        {
            ModuloPermissao aux = new ModuloPermissao();            
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));

            if (usuarioLogado != null && usuarioLogado.GrupoEconomico != null && usuarioLogado.GrupoEconomico.Id > 0) 
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("GruposEconomicos", "grupo"));
                aux.AdicionarFiltro(Filtros.Eq("grupo.Id", usuarioLogado.GrupoEconomico.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ModuloPermissao>(aux);
        }

        public static IList<ModuloPermissao> RecarregarModulos(IList<ModuloPermissao> modulosDaSessao)
        {
            IList<ModuloPermissao> lista = new List<ModuloPermissao>();
            if (modulosDaSessao != null)
            {
                foreach (ModuloPermissao item in modulosDaSessao)
                {
                    ModuloPermissao not = ModuloPermissao.ConsultarPorId(item.Id);
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

        public static ModuloPermissao ConsultarPorNome(string nomeModulo)
        {
            ModuloPermissao modulo = new ModuloPermissao();
            modulo.AdicionarFiltro(Filtros.Distinct());
            modulo.AdicionarFiltro(Filtros.Eq("Nome", nomeModulo));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ModuloPermissao>(modulo);
        }

        public virtual IList<Menu> GetMenusRelatoriosDoModulo
        {
            get
            {
                Menu menu = new Menu();
                menu.AdicionarFiltro(Filtros.Distinct());
                menu.AdicionarFiltro(Filtros.Eq("Relatorio", true));
                menu.AdicionarFiltro(Filtros.CriarAlias("ModuloPermissao", "modPer"));
                menu.AdicionarFiltro(Filtros.Eq("modPer.Id", this.Id));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(menu);
            } 
        }
        
    }
}
