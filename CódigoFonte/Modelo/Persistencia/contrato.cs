using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Contrato : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Contrato ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Contrato classe = new Contrato();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Contrato>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Contrato ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Contrato>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Contrato Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Contrato>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Contrato SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Contrato>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Contrato> SalvarTodos(IList<Contrato> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Contrato>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Contrato> SalvarTodos(params Contrato[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Contrato>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Contrato>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Contrato>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Contrato> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Contrato obj = Activator.CreateInstance<Contrato>();
            return fabrica.GetDAOBase().ConsultarTodos<Contrato>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Contrato> ConsultarOrdemAcendente(int qtd)
        {
            Contrato ee = new Contrato();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Contrato>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Contrato> ConsultarOrdemDescendente(int qtd)
        {
            Contrato ee = new Contrato();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Contrato>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Contrato
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Contrato> Filtrar(int qtd)
        {
            Contrato estado = new Contrato();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Contrato>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Contrato Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Contrato</returns>
        public virtual Contrato UltimoInserido()
        {
            Contrato estado = new Contrato();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Contrato>(estado);
        }

        /// <summary>
        /// Consulta todos os Contratos armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Contratos armazenados ordenados pelo Nome</returns>
        public static IList<Contrato> ConsultarTodosOrdemAlfabetica()
        {
            Contrato aux = new Contrato();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Contrato>(aux);
        }

        public static IList<Contrato> Filtrar()
        {
            Contrato contrato = new Contrato();

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Contrato>(contrato);
        }

        public static int GetUltimoNumeroDeContrato
        {
            get
            {
                Contrato aux = new Contrato();
                aux.AdicionarFiltro(Filtros.Eq("AnoContrato", DateTime.Now.Year.ToString()));
                aux.AdicionarFiltro(Filtros.ValorMaximo("NumeroContrato"));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                object resultado = fabrica.GetDAOBase().ConsultarProjecao(aux);
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
        }

        public static String GetNomeMes(int i)
        {
            string retorno = "";
            if (i > 0)
            {
                switch (i)
                {
                    case 1: retorno = "Janeiro"; break;
                    case 2: retorno = "Fevereiro"; break;
                    case 3: retorno = "Março"; break;
                    case 4: retorno = "Abril"; break;
                    case 5: retorno = "Maio"; break;
                    case 6: retorno = "Junho"; break;
                    case 7: retorno = "Julho"; break;
                    case 8: retorno = "Agosto"; break;
                    case 9: retorno = "Setembro"; break;
                    case 10: retorno = "Outubro"; break;
                    case 11: retorno = "Novembro"; break;
                    case 12: retorno = "Dezembro"; break;
                }
                return retorno;
            }
            else { return retorno; }
        }

    }
}

