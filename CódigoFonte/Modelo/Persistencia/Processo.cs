using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Processo : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Processo ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Processo classe = new Processo();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Processo>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Processo ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Processo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Processo Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Processo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Processo SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Processo>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Processo> SalvarTodos(IList<Processo> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Processo>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Processo> SalvarTodos(params Processo[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Processo>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Processo>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Processo>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Processo> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Processo obj = Activator.CreateInstance<Processo>();
            return fabrica.GetDAOBase().ConsultarTodos<Processo>(obj);
        }

        public static IList<Processo> ConsultarTodosComoObjetos()
        {
            Processo ee = new Processo();

            ee.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Em.Nome"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Processo> ConsultarOrdemAcendente(int qtd)
        {
            Processo ee = new Processo();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Processo> ConsultarOrdemDescendente(int qtd)
        {
            Processo ee = new Processo();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Processo
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Processo> Filtrar(int qtd)
        {
            Processo estado = new Processo();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Processo Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Processo</returns>
        public virtual Processo UltimoInserido()
        {
            Processo estado = new Processo();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Processo>(estado);
        }

        /// <summary>
        /// Consulta todos os Processos armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Processos armazenados ordenados pelo Nome</returns>
        public static IList<Processo> ConsultarTodosOrdemAlfabetica()
        {
            Processo aux = new Processo();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(aux);
        }

        public static IList<Processo> ConsultarPorOrgaoEmpresaGrupoEconomico(OrgaoAmbiental org, Empresa empresa, GrupoEconomico grupo)
        {
            Processo proc = new Processo();

            if (empresa != null)
                proc.AdicionarFiltro(Filtros.Eq("Empresa.Id", empresa.Id));
            else
            {
                proc.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
                proc.AdicionarFiltro(Filtros.CriarAlias("Em.GrupoEconomico", "Cli"));
                proc.AdicionarFiltro(Filtros.Eq("Cli.Id", grupo.Id));
            }

            proc.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", org.Id));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(proc);
        }

        public static IList<Processo> ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(OrgaoAmbiental org, Empresa empresa, GrupoEconomico grupo, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Processo> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<Processo>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<Processo>();

            Processo proc = new Processo();

            if (tipoConfigPermissao == 'P')
            {
                IList<Processo> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == grupo.Id).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<Processo>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    proc.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            if (empresa != null)
                proc.AdicionarFiltro(Filtros.Eq("Empresa.Id", empresa.Id));
            else
            {
                proc.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));

                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == grupo.Id).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<Processo>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("Em.Id", empresasAux[index].Id);
                        proc.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }

                proc.AdicionarFiltro(Filtros.CriarAlias("Em.GrupoEconomico", "Cli"));
                proc.AdicionarFiltro(Filtros.Eq("Cli.Id", grupo.Id));
            }

            proc.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", org.Id));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(proc);
        }

        public static IList<Processo> Filtrar(String numero, String obs, Empresa e, DateTime dataDe, DateTime dataAte)
        {
            Processo proc = new Processo();

            proc.AdicionarFiltro(Filtros.Like("Numero", numero));
            proc.AdicionarFiltro(Filtros.Like("Observacoes", obs));

            if (e != null)
                proc.AdicionarFiltro(Filtros.Eq("Empresa.Id", e.Id));

            proc.AdicionarFiltro(Filtros.Between("DataAbertura", dataDe, dataAte));

            proc.AdicionarFiltro(Filtros.FetchJoin("Licencas"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(proc);
        }

        public static IList<Processo> ConsultarPorEmpresaEOrgao(Empresa emp, OrgaoAmbiental org)
        {
            Processo proc = new Processo();

            if (org != null && org.Id > 0)
                proc.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", org.Id));
            if (emp != null && emp.Id > 0)
                proc.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(proc);
        }

        public static IList<Processo> ConsultarPorGrupoEconomicoEOrgao(GrupoEconomico grupo, OrgaoAmbiental org)
        {
            Processo proc = new Processo();

            if (org != null && org.Id > 0)
                proc.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", org.Id));
            if (grupo != null && grupo.Id > 0)
            {
                proc.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Emp"));
                proc.AdicionarFiltro(Filtros.Eq("Emp.GrupoEconomico.Id", grupo.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(proc);
        }

        /// <summary>
        /// Filtra Processos de acorodo com Grupo economico, Empresa e Orgao Ambiental
        /// </summary>
        /// <param name="idGrupoEconomico">O id do grupo economico</param>
        /// <param name="idEmpresa">O id da empresa</param>
        /// <param name="idOrgaoAmbiental">O id do órgão Ambiental</param>
        /// <returns>Uma lista de Processos de acordo com os filtros passados como parâmetro</returns>
        public static IList<Processo> FiltrarRelatorio(string idGrupoEconomico, string idEmpresa, string idOrgaoAmbiental, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Processo> processosPermissoes)
        {
            int grupoEconomico = Convert.ToInt32(idGrupoEconomico);
            int empresa = Convert.ToInt32(idEmpresa);
            int orgaoAmbiental = Convert.ToInt32(idOrgaoAmbiental);

            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<Processo>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<Processo>();

            Processo aux = new Processo();
            aux.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<Processo> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == grupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<Processo>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            if (orgaoAmbiental > 0)
                aux.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", orgaoAmbiental));

            aux.AdicionarFiltro(Filtros.SubConsulta("Empresa"));

            if (empresa > 0)
                aux.AdicionarFiltro(Filtros.Eq("Id", empresa));
            else 
            {
                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == grupoEconomico).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<Processo>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("Id", empresasAux[index].Id);
                        aux.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }
            }

            aux.AdicionarFiltro(Filtros.SubConsulta("GrupoEconomico"));
            if (grupoEconomico > 0)
                aux.AdicionarFiltro(Filtros.Eq("Id", grupoEconomico));


            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(aux);
        }

        public static IList<Processo> ConsultarPorNumero(string p)
        {
            Processo aux = new Processo();
            aux.AdicionarFiltro(Filtros.Eq("Numero", p));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(aux);
        }

        public static IList<Processo> ConsultarPorEmpresaENumero(string numero, Empresa emp)
        {
            Processo aux = new Processo();
            if (!String.IsNullOrEmpty(numero))
                aux.AdicionarFiltro(Filtros.Like("Numero", numero));
            if (emp != null && emp.Id > 0)
                aux.AdicionarFiltro(Filtros.Eq("Empresa.Id", emp.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(aux);
        }

        public static IList<Processo> FiltrarRelatorioContratosPorProcessos(int grupoEconomico, Empresa empresa, string numero, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Processo> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<Processo>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<Processo>();

            Processo processo = new Processo();
            processo.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<Processo> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == grupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<Processo>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    processo.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            processo.AdicionarFiltro(Filtros.Like("Numero", numero));

            processo.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

            if (empresa != null && empresa.Id > 0)
                processo.AdicionarFiltro(Filtros.Eq("E.Id", empresa.Id));
            else 
            {
                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == grupoEconomico).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<Processo>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("Id", empresasAux[index].Id);
                        processo.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }

                if (grupoEconomico > 0) 
                {
                    processo.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "grup"));
                    processo.AdicionarFiltro(Filtros.Eq("grup.Id", grupoEconomico));
                }
            }            

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(processo);
        }

        public static IList<Processo> ConsultarProcessosDoCliente(GrupoEconomico grupo)
        {
            Processo p = new Processo();
            p.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));
            p.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            p.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Processo>(p);
        }

        public virtual string GetDescricaoOrgaoAmbiental
        {
            get
            {
                return this.OrgaoAmbiental != null ? this.OrgaoAmbiental.GetNomeTipo + " - " + this.OrgaoAmbiental.Nome : "Não definido";
            }
        }

        public virtual string GetNomeGrupoEconomico
        {
            get
            {
                return this.Empresa != null && this.Empresa.GrupoEconomico != null ? this.Empresa.GrupoEconomico.Nome : "Não definido";
            }
        }

        public virtual string GetNomeEmpresa
        {
            get
            {
                return this.Empresa != null ? this.Empresa.Nome + " - " + this.Empresa.GetNumeroCNPJeCPFComMascara : "Não definido";
            }
        }

        public static IList<Processo> ObterProcessosQueOUsuarioPossuiAcesso(Usuario usuario)
        {
            IList<Processo> retorno = new List<Processo>();

            IList<Processo> processos = Processo.ConsultarTodos();
            if (processos != null && processos.Count > 0)
            {
                foreach (Processo processo in processos)
                {
                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(usuario)))
                        retorno.Add(processo);
                }
            }

            return retorno;
        }
    }
}
