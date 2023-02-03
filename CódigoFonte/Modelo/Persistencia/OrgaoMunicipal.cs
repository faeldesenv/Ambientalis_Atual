using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class OrgaoMunicipal : OrgaoAmbiental
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OrgaoMunicipal ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoMunicipal classe = new OrgaoMunicipal();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoMunicipal>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OrgaoMunicipal ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoMunicipal>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OrgaoMunicipal Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OrgaoMunicipal>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OrgaoMunicipal SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OrgaoMunicipal>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoMunicipal> SalvarTodos(IList<OrgaoMunicipal> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoMunicipal>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoMunicipal> SalvarTodos(params OrgaoMunicipal[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoMunicipal>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OrgaoMunicipal>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OrgaoMunicipal>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OrgaoMunicipal> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoMunicipal obj = Activator.CreateInstance<OrgaoMunicipal>();
            return fabrica.GetDAOBase().ConsultarTodos<OrgaoMunicipal>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoMunicipal> ConsultarOrdemAcendente(int qtd)
        {
            OrgaoMunicipal ee = new OrgaoMunicipal();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoMunicipal> ConsultarOrdemDescendente(int qtd)
        {
            OrgaoMunicipal ee = new OrgaoMunicipal();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de OrgaoMunicipal
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OrgaoMunicipal> Filtrar(int qtd)
        {
            OrgaoMunicipal estado = new OrgaoMunicipal();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(estado);
        }

        /// <summary>
        /// Retorna o ultimo OrgaoMunicipal Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo OrgaoMunicipal</returns>
        public virtual OrgaoMunicipal UltimoInserido()
        {
            OrgaoMunicipal estado = new OrgaoMunicipal();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrgaoMunicipal>(estado);
        }

        /// <summary>
        /// Consulta todos os OrgaoMunicipals armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os OrgaoMunicipals armazenados ordenados pelo Nome</returns>
        public static IList<OrgaoMunicipal> ConsultarTodosOrdemAlfabetica()
        {
            OrgaoMunicipal aux = new OrgaoMunicipal();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(aux);
        }

        public static IList<OrgaoMunicipal> ConsultarOrgaosAmbientaisDoGrupoEconomico(GrupoEconomico grupo)
        {
            OrgaoMunicipal org1 = new OrgaoMunicipal();
            org1.AdicionarFiltro(Filtros.CriarAlias("Processos", "P"));
            org1.AdicionarFiltro(Filtros.CriarAlias("P.Empresa", "E"));
            org1.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org1.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoMunicipal> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org1);

            OrgaoMunicipal org = new OrgaoMunicipal();
            org.AdicionarFiltro(Filtros.CriarAlias("OutrosEmpresas", "O"));
            org.AdicionarFiltro(Filtros.CriarAlias("O.Empresa", "E"));
            org.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoMunicipal> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org);

            foreach (OrgaoMunicipal o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }
        public static IList<OrgaoMunicipal> ConsultarOrgaosAmbientaisDoGrupoEconomicoSTATUS(GrupoEconomico grupo, String status)
        {
            OrgaoMunicipal org1 = new OrgaoMunicipal();
            org1.AdicionarFiltro(Filtros.CriarAlias("Processos", "P"));
            org1.AdicionarFiltro(Filtros.CriarAlias("P.Empresa", "E"));
            if (status == "Ativo") {
                org1.AdicionarFiltro(Filtros.Eq("E.Ativo", true));
            }
            if (status == "Inativo") {
                org1.AdicionarFiltro(Filtros.Eq("E.Ativo", false));
            }

            org1.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org1.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoMunicipal> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org1);

            OrgaoMunicipal org = new OrgaoMunicipal();
            org.AdicionarFiltro(Filtros.CriarAlias("OutrosEmpresas", "O"));
            org.AdicionarFiltro(Filtros.CriarAlias("O.Empresa", "E"));
            if (status == "Ativo")
            {
                org.AdicionarFiltro(Filtros.Eq("E.Ativo", true));
            }
            if (status == "Inativo")
            {
                org.AdicionarFiltro(Filtros.Eq("E.Ativo", false));
            }
                org.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoMunicipal> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org);

            foreach (OrgaoMunicipal o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoMunicipal> ConsultarOrgaosAmbientaisDaEmpresa(Empresa emp)
        {
            OrgaoMunicipal org1 = new OrgaoMunicipal();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoMunicipal> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org1);

            OrgaoMunicipal org = new OrgaoMunicipal();
            org.AdicionarFiltro(Filtros.SubConsulta("OutrosEmpresas"));
            org.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoMunicipal> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org);

            foreach (OrgaoMunicipal o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoMunicipal> ConsultarPorEmpresa(int p)
        {
            OrgaoMunicipal org1 = new OrgaoMunicipal();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return p > 0 ? fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org1) : new List<OrgaoMunicipal>();
        }

        public static IList<OrgaoMunicipal> ConsultarPorCidade(int idCidade)
        {
            OrgaoMunicipal org1 = new OrgaoMunicipal();
            org1.AdicionarFiltro(Filtros.Eq("Cidade.Id", idCidade));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoMunicipal>(org1);
        }        
        
    }
}
