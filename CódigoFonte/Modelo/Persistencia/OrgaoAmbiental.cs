using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class OrgaoAmbiental : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OrgaoAmbiental ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoAmbiental classe = new OrgaoAmbiental();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoAmbiental>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OrgaoAmbiental ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OrgaoAmbiental>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OrgaoAmbiental Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OrgaoAmbiental>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OrgaoAmbiental SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OrgaoAmbiental>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoAmbiental> SalvarTodos(IList<OrgaoAmbiental> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoAmbiental>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrgaoAmbiental> SalvarTodos(params OrgaoAmbiental[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrgaoAmbiental>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OrgaoAmbiental>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OrgaoAmbiental>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OrgaoAmbiental> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrgaoAmbiental obj = Activator.CreateInstance<OrgaoAmbiental>();
            return fabrica.GetDAOBase().ConsultarTodos<OrgaoAmbiental>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoAmbiental> ConsultarOrdemAcendente(int qtd)
        {
            OrgaoAmbiental ee = new OrgaoAmbiental();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrgaoAmbiental> ConsultarOrdemDescendente(int qtd)
        {
            OrgaoAmbiental ee = new OrgaoAmbiental();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de OrgaoAmbiental
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OrgaoAmbiental> Filtrar(int qtd)
        {
            OrgaoAmbiental estado = new OrgaoAmbiental();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(estado);
        }

        /// <summary>
        /// Retorna o ultimo OrgaoAmbiental Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo OrgaoAmbiental</returns>
        public virtual OrgaoAmbiental UltimoInserido()
        {
            OrgaoAmbiental estado = new OrgaoAmbiental();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrgaoAmbiental>(estado);
        }

        /// <summary>
        /// Consulta todos os OrgaoAmbientals armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os OrgaoAmbientals armazenados ordenados pelo Nome</returns>
        public static IList<OrgaoAmbiental> ConsultarTodosOrdemAlfabetica()
        {
            OrgaoAmbiental aux = new OrgaoAmbiental();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(aux);
        }

        public static IList<OrgaoAmbiental> ConsultarOrgaosAmbientaisDoGrupoEconomico(GrupoEconomico grupo)
        {
            OrgaoAmbiental org1 = new OrgaoAmbiental();
            org1.AdicionarFiltro(Filtros.CriarAlias("Processos", "P"));
            org1.AdicionarFiltro(Filtros.CriarAlias("P.Empresa", "E"));
            org1.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org1.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoAmbiental> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(org1);

            OrgaoAmbiental org = new OrgaoAmbiental();
            org.AdicionarFiltro(Filtros.CriarAlias("OutrosEmpresas", "O"));
            org.AdicionarFiltro(Filtros.CriarAlias("O.Empresa", "E"));
            org.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            org.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoAmbiental> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(org);

            foreach (OrgaoAmbiental o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoAmbiental> ConsultarOrgaosAmbientaisDaEmpresa(Empresa emp)
        {
            OrgaoAmbiental org1 = new OrgaoAmbiental();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            IList<OrgaoAmbiental> orgaosProcesso = fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(org1);

            OrgaoAmbiental org = new OrgaoAmbiental();
            org.AdicionarFiltro(Filtros.SubConsulta("OutrosEmpresas"));
            org.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            org.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrgaoAmbiental> orgaosOutros = fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(org);

            foreach (OrgaoAmbiental o in orgaosOutros)
                if (!orgaosProcesso.Contains(o))
                    orgaosProcesso.Add(o);

            return orgaosProcesso;
        }

        public static IList<OrgaoAmbiental> ConsultarPorEmpresa(int p)
        {

            OrgaoAmbiental org1 = new OrgaoAmbiental();
            org1.AdicionarFiltro(Filtros.SubConsulta("Processos"));
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return p > 0 ? fabrica1.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(org1) : new List<OrgaoAmbiental>();
        }

        public static IList<OrgaoAmbiental> Filtrar(int tipo, string nome, Estado estado, Cidade cidade)
        {
            OrgaoAmbiental orgao = new OrgaoAmbiental();
            
            if (tipo == 1)
                orgao = new OrgaoFederal();

            if (tipo == 2)
            {
                orgao = new OrgaoEstadual();
                if (estado != null && estado.Id > 0)
                {
                    ((OrgaoEstadual)orgao).AdicionarFiltro(Filtros.CriarAlias("Estado", "oEst"));
                    orgao.AdicionarFiltro(Filtros.Eq("oEst.Id", estado.Id));
                }
            }

            if (tipo == 3)
            {
                orgao = new OrgaoMunicipal();
                if (cidade != null && cidade.Id > 0)
                {
                    ((OrgaoMunicipal)orgao).AdicionarFiltro(Filtros.CriarAlias("Cidade", "oCid"));
                    orgao.AdicionarFiltro(Filtros.Eq("oCid.Id", cidade.Id));
                }
            }
            orgao.AdicionarFiltro(Filtros.Like("Nome", nome));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(orgao);
        }

        public static IList<OrgaoAmbiental> FiltrarOrgaosPadroes(int tipo, string nome, Estado estado, Cidade cidade)
        {
            OrgaoAmbiental orgao = new OrgaoAmbiental();
            if (tipo == 1)
                orgao = new OrgaoFederal();

            if (tipo == 2)
            {
                orgao = new OrgaoEstadual();
                if (estado != null && estado.Id > 0)
                {
                    ((OrgaoEstadual)orgao).AdicionarFiltro(Filtros.CriarAlias("Estado", "oEst"));
                    orgao.AdicionarFiltro(Filtros.Eq("oEst.Id", estado.Id));
                }
            }

            if (tipo == 3)
            {
                orgao = new OrgaoMunicipal();
                if (cidade != null && cidade.Id > 0)
                {
                    ((OrgaoMunicipal)orgao).AdicionarFiltro(Filtros.CriarAlias("Cidade", "oCid"));
                    orgao.AdicionarFiltro(Filtros.Eq("oCid.Id", cidade.Id));
                }
            }

            orgao.AdicionarFiltro(Filtros.Like("Nome", nome));
            orgao.AdicionarFiltro(Filtros.Eq("Emp", 0));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(orgao);
        }

        public static object ConsultarOrgaosPadrao()
        {
            OrgaoAmbiental aux = new OrgaoAmbiental();
            aux.AdicionarFiltro(Filtros.Eq("Emp", 0));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(aux);
        }

        public virtual string GetNomeTipo
        {
            get 
            {
                if (this.GetType() == typeof(OrgaoEstadual))
                    return "Estadual";

                if (this.GetType() == typeof(OrgaoFederal))
                    return "Federal";

                if (this.GetType() == typeof(OrgaoMunicipal))
                    return "Municipal";

                return "Órgão Ambiental"; 
            }
        }

        public virtual string GetCidadeEstado
        {
            get 
            { 
                if (this.GetType() == typeof(OrgaoEstadual))
                {
                    OrgaoEstadual orgEst = (OrgaoEstadual)this;
                    return orgEst.Estado.PegarSiglaEstado();
                }                    

                if (this.GetType() == typeof(OrgaoFederal))
                    return "--";

                if (this.GetType() == typeof(OrgaoMunicipal))
                {
                    OrgaoMunicipal orgMun = (OrgaoMunicipal)this;
                    return orgMun.Cidade.Nome + " - " + orgMun.Cidade.Estado.PegarSiglaEstado();
                }                    

                return "--"; 
            }
            
        }

        public static IList<OrgaoAmbiental> ConsultarComOutros(Empresa e, GrupoEconomico g, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<OutrosEmpresa> outrosEmpsPermissoes)
        {
            OrgaoAmbiental aux = new OrgaoAmbiental();

            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<OrgaoAmbiental>();

            if (tipoConfigPermissao == 'P' && (outrosEmpsPermissoes == null || outrosEmpsPermissoes.Count == 0))
                return new List<OrgaoAmbiental>();

            aux.AdicionarFiltro(Filtros.IsNotNull("OutrosEmpresas"));

            aux.AdicionarFiltro(Filtros.SubConsulta("OutrosEmpresas"));

            if (tipoConfigPermissao == 'P')
            {
                IList<OutrosEmpresa> outrosAux = outrosEmpsPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == g.Id).ToList();

                if (outrosAux == null || outrosAux.Count == 0)
                    return new List<OrgaoAmbiental>();

                if (outrosAux != null && outrosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[outrosAux.Count];
                    for (int index = 0; index < outrosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", outrosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }                
            }

            if (g != null)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Empresa", "empr")); 

                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == g.Id).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<OrgaoAmbiental>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("empr.Id", empresasAux[index].Id);
                        aux.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }                    
                }
                
                aux.AdicionarFiltro(Filtros.CriarAlias("empr.GrupoEconomico", "ge"));
                aux.AdicionarFiltro(Filtros.Eq("ge.Id", g.Id));
            }
            else if (e != null)
                aux.AdicionarFiltro(Filtros.Eq("Empresa.Id", e.Id));

            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrgaoAmbiental>(aux);
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
