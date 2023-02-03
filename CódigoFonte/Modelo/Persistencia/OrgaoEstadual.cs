using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class OrgaoEstadual : OrgaoAmbiental
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OrgaoEstadual ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoEstadual classe = new OrgaoEstadual();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoEstadual>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OrgaoEstadual ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoEstadual>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OrgaoEstadual Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OrgaoEstadual>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OrgaoEstadual SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OrgaoEstadual>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoEstadual> SalvarTodos(IList<OrgaoEstadual> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoEstadual>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoEstadual> SalvarTodos(params OrgaoEstadual[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoEstadual>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OrgaoEstadual>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OrgaoEstadual>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OrgaoEstadual> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoEstadual obj = Activator.CreateInstance<OrgaoEstadual>();
            return fabrica.GetDAOBase().ConsultarTodos<OrgaoEstadual>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoEstadual> ConsultarOrdemAcendente(int qtd)
        {
            OrgaoEstadual ee = new OrgaoEstadual();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoEstadual> ConsultarOrdemDescendente(int qtd)
        {
            OrgaoEstadual ee = new OrgaoEstadual();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de OrgaoEstadual
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OrgaoEstadual> Filtrar(int qtd)
        {
            OrgaoEstadual estado = new OrgaoEstadual();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(estado);
        }

        /// <summary>
        /// Retorna o ultimo OrgaoEstadual Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo OrgaoEstadual</returns>
        public virtual OrgaoEstadual UltimoInserido()
        {
            OrgaoEstadual estado = new OrgaoEstadual();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrgaoEstadual>(estado);
        }

        /// <summary>
        /// Consulta todos os OrgaoEstaduals armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os OrgaoEstaduals armazenados ordenados pelo Nome</returns>
        public static IList<OrgaoEstadual> ConsultarTodosOrdemAlfabetica()
        {
            OrgaoEstadual aux = new OrgaoEstadual();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(aux);
        }

        public static IList<OrgaoEstadual> ConsultarOrgaosAmbientaisDoGrupoEconomicoSTATUS(GrupoEconomico grupo, String status)
        {
            OrgaoEstadual org1 = new OrgaoEstadual();
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
            IList<OrgaoEstadual> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org1);

            OrgaoEstadual org = new OrgaoEstadual();
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
            IList<OrgaoEstadual> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org);

            foreach (OrgaoEstadual o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }
        public static IList<OrgaoEstadual> ConsultarOrgaosAmbientaisDoGrupoEconomico(GrupoEconomico grupo)
        {
            OrgaoEstadual org1 = new OrgaoEstadual();
            org1.AdicionarFiltro(Filtros.CriarAlias("Processos", "P"));
            org1.AdicionarFiltro(Filtros.CriarAlias("P.Empresa", "E"));

            org1.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org1.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoEstadual> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org1);

            OrgaoEstadual org = new OrgaoEstadual();
            org.AdicionarFiltro(Filtros.CriarAlias("OutrosEmpresas", "O"));
            org.AdicionarFiltro(Filtros.CriarAlias("O.Empresa", "E"));

            org.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoEstadual> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org);

            foreach (OrgaoEstadual o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoEstadual> ConsultarOrgaosAmbientaisDaEmpresaSTATUS(Empresa emp, String status)
        {
            OrgaoEstadual org1 = new OrgaoEstadual();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            if (status == "Ativo") {
                org1.AdicionarFiltro(Filtros.Eq("Empresa.Ativo", true));
            }
            if (status == "Inativo") {
                org1.AdicionarFiltro(Filtros.Eq("Empresa.Ativo", false));
            }
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoEstadual> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org1);

            OrgaoEstadual org = new OrgaoEstadual();
            org.AdicionarFiltro(Filtros.SubConsulta("OutrosEmpresas"));
            org.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            if (status == "Ativo")
            {
                org.AdicionarFiltro(Filtros.Eq("Empresa.Ativo", true));
            }
            if (status == "Inativo")
            {
                org.AdicionarFiltro(Filtros.Eq("Empresa.Ativo", false));
            }
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoEstadual> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org);

            foreach (OrgaoEstadual o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoEstadual> ConsultarOrgaosAmbientaisDaEmpresa(Empresa emp)
        {
            OrgaoEstadual org1 = new OrgaoEstadual();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoEstadual> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org1);

            OrgaoEstadual org = new OrgaoEstadual();
            org.AdicionarFiltro(Filtros.SubConsulta("OutrosEmpresas"));
            org.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoEstadual> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org);

            foreach (OrgaoEstadual o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoEstadual> ConsultarPorEmpresa(int p)
        {
            OrgaoEstadual org1 = new OrgaoEstadual();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return p > 0 ? fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org1) : new List<OrgaoEstadual>();
        }

        public static IList<OrgaoEstadual> ConsultarPorEstado(int idEstado)
        {
            OrgaoEstadual org1 = new OrgaoEstadual();
            org1.AdicionarFiltro(Filtros.Eq("Estado.Id", idEstado));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoEstadual>(org1);
        }



        public virtual int ObterQuantidadeDeOutrosEmpresaDoOrgaoEGrupoEconomicoQueOUsuarioPossuiAcesso(Usuario usuario, int idGrupoEconomico, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<OutrosEmpresa> outrosEmpresaPermissoes)
        {
            int retorno = 0;

            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return retorno;

            if (tipoConfigPermissao == 'P' && (outrosEmpresaPermissoes == null || outrosEmpresaPermissoes.Count == 0))
                return retorno;

            OutrosEmpresa org = new OutrosEmpresa();
            org.AdicionarFiltro(Filtros.Distinct());

            org.AdicionarFiltro(Filtros.CriarAlias("OrgaoAmbiental", "orgAmb"));
            org.AdicionarFiltro(Filtros.Eq("orgAmb.Id", this.Id));

            if (tipoConfigPermissao == 'P')
            {
                IList<OutrosEmpresa> outrosEmpsAux = outrosEmpresaPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (outrosEmpsAux == null || outrosEmpsAux.Count == 0)
                    return retorno;

                if (outrosEmpsAux != null && outrosEmpsAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[outrosEmpsAux.Count];
                    for (int index = 0; index < outrosEmpsAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", outrosEmpsAux[index].Id);
                    org.AdicionarFiltro(Filtros.Ou(filtros));
                }                
            }

            org.AdicionarFiltro(Filtros.CriarAlias("Empresa", "empr"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return retorno;

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("empr.Id", empresasAux[index].Id);
                    org.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
                
            }

            org.AdicionarFiltro(Filtros.CriarAlias("empr.GrupoEconomico", "grup"));
            org.AdicionarFiltro(Filtros.Eq("grup.Id", idGrupoEconomico));
            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OutrosEmpresa> outros = fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(org);

            if (outros != null && outros.Count > 0)
                retorno = outros.Count;

            return retorno;
        }
    }
}
