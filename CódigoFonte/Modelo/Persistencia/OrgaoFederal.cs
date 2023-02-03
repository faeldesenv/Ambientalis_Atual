using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class OrgaoFederal : OrgaoAmbiental
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OrgaoFederal ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoFederal classe = new OrgaoFederal();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoFederal>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OrgaoFederal ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoFederal>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OrgaoFederal Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OrgaoFederal>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OrgaoFederal SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OrgaoFederal>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoFederal> SalvarTodos(IList<OrgaoFederal> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoFederal>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoFederal> SalvarTodos(params OrgaoFederal[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoFederal>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OrgaoFederal>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OrgaoFederal>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OrgaoFederal> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoFederal obj = Activator.CreateInstance<OrgaoFederal>();
            return fabrica.GetDAOBase().ConsultarTodos<OrgaoFederal>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoFederal> ConsultarOrdemAcendente(int qtd)
        {
            OrgaoFederal ee = new OrgaoFederal();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoFederal> ConsultarOrdemDescendente(int qtd)
        {
            OrgaoFederal ee = new OrgaoFederal();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de OrgaoFederal
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OrgaoFederal> Filtrar(int qtd)
        {
            OrgaoFederal estado = new OrgaoFederal();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(estado);
        }

        /// <summary>
        /// Retorna o ultimo OrgaoFederal Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo OrgaoFederal</returns>
        public virtual OrgaoFederal UltimoInserido()
        {
            OrgaoFederal estado = new OrgaoFederal();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrgaoFederal>(estado);
        }

        /// <summary>
        /// Consulta todos os OrgaoFederals armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os OrgaoFederals armazenados ordenados pelo Nome</returns>
        public static IList<OrgaoFederal> ConsultarTodosOrdemAlfabetica()
        {
            OrgaoFederal aux = new OrgaoFederal();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(aux);
        }

        public static IList<OrgaoFederal> ConsultarOrgaosAmbientaisDoGrupoEconomico(GrupoEconomico grupo)
        {
            OrgaoFederal org1 = new OrgaoFederal();
            org1.AdicionarFiltro(Filtros.CriarAlias("Processos", "P"));
            org1.AdicionarFiltro(Filtros.CriarAlias("P.Empresa", "E"));
            org1.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org1.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoFederal> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org1);

            OrgaoFederal org = new OrgaoFederal();
            org.AdicionarFiltro(Filtros.CriarAlias("OutrosEmpresas", "O"));
            org.AdicionarFiltro(Filtros.CriarAlias("O.Empresa", "E"));
            org.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoFederal> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org);

            foreach (OrgaoFederal o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }
        public static IList<OrgaoFederal> ConsultarOrgaosAmbientaisDoGrupoEconomicoSTATUS(GrupoEconomico grupo, String status)
        {
            OrgaoFederal org1 = new OrgaoFederal();
            org1.AdicionarFiltro(Filtros.CriarAlias("Processos", "P"));
            org1.AdicionarFiltro(Filtros.CriarAlias("P.Empresa", "E"));
            if (status == "Ativo")
            {
                org1.AdicionarFiltro(Filtros.Eq("E.Ativo", true));
            }
            if (status == "Inativo")
            {
                org1.AdicionarFiltro(Filtros.Eq("E.Ativo", false));
            }
            org1.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org1.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoFederal> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org1);

            OrgaoFederal org = new OrgaoFederal();
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
            IList<OrgaoFederal> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org);

            foreach (OrgaoFederal o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoFederal> ConsultarOrgaosAmbientaisDaEmpresa(Empresa emp)
        {
            OrgaoFederal org1 = new OrgaoFederal();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoFederal> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org1);

            OrgaoFederal org = new OrgaoFederal();
            org.AdicionarFiltro(Filtros.SubConsulta("OutrosEmpresas"));
            org.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoFederal> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org);

            foreach (OrgaoFederal o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoFederal> ConsultarPorEmpresa(int p)
        {

            OrgaoFederal org1 = new OrgaoFederal();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return p > 0 ? fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoFederal>(org1) : new List<OrgaoFederal>();
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
                IList<OutrosEmpresa> outrosAux = outrosEmpresaPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (outrosAux == null || outrosAux.Count == 0)
                    return retorno;

                if (outrosAux != null && outrosAux.Count > 0)
                {

                    Filtro[] filtros = new Filtro[outrosAux.Count];
                    for (int index = 0; index < outrosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", outrosAux[index].Id);
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
