using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class ProcessoDNPM : ObjetoBase
    {
        public const int RequerimentoPesquisa = 1;
        public const int AlvaraPesquisa = 2;
        public const int Extracao = 3;
        public const int Licenciamento = 4;
        public const int RequerimentoLavra = 5;
        public const int ConcessaoLavra = 6;

        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ProcessoDNPM ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ProcessoDNPM classe = new ProcessoDNPM();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ProcessoDNPM>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ProcessoDNPM ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ProcessoDNPM>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ProcessoDNPM Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ProcessoDNPM>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ProcessoDNPM SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ProcessoDNPM>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ProcessoDNPM> SalvarTodos(IList<ProcessoDNPM> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ProcessoDNPM>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ProcessoDNPM> SalvarTodos(params ProcessoDNPM[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ProcessoDNPM>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ProcessoDNPM>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ProcessoDNPM>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ProcessoDNPM> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ProcessoDNPM obj = Activator.CreateInstance<ProcessoDNPM>();
            return fabrica.GetDAOBase().ConsultarTodos<ProcessoDNPM>(obj);
        }

        public static IList<ProcessoDNPM> ConsultarTodosComoObjetos()
        {
            ProcessoDNPM ee = new ProcessoDNPM();

            ee.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Em.Nome"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(ee);           
            
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ProcessoDNPM> ConsultarOrdemAcendente(int qtd)
        {
            ProcessoDNPM ee = new ProcessoDNPM();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ProcessoDNPM> ConsultarOrdemDescendente(int qtd)
        {
            ProcessoDNPM ee = new ProcessoDNPM();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de ProcessoDNPM
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ProcessoDNPM> Filtrar(int qtd)
        {
            ProcessoDNPM estado = new ProcessoDNPM();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(estado);
        }

        /// <summary>
        /// Retorna o ultimo registro Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Processo</returns>
        public virtual ProcessoDNPM UltimoInserido()
        {
            ProcessoDNPM p = new ProcessoDNPM();
            p.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ProcessoDNPM>(p);
        }

        public static IList<ProcessoDNPM> ConsultarProcessosDoCliente(GrupoEconomico grupo)
        {
            ProcessoDNPM p = new ProcessoDNPM();
            p.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));
            p.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            p.AdicionarFiltro(Filtros.Eq("C.Id", grupo.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(p);
        }

        public static IList<ProcessoDNPM> ConsultarProcessosDoClienteOuGrupo(int idGrupo, int idEmpresa, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            ProcessoDNPM p = new ProcessoDNPM();

            p.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupo).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    p.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            if (idEmpresa > 0)
                p.AdicionarFiltro(Filtros.Eq("Empresa.Id", idEmpresa));
            else 
            {
                p.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupo).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<ProcessoDNPM>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                        p.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }

                if (idGrupo > 0)
                {
                    p.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
                    p.AdicionarFiltro(Filtros.Eq("C.Id", idGrupo));
                }
            }
                
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(p);
        }

        public static IList<ProcessoDNPM> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, string regime, int idEstado, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            ProcessoDNPM processo = new ProcessoDNPM();
            processo.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    processo.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            int numero = -1;
            if (regime.Trim() != "")
                processo.AdicionarFiltro(Filtros.Eq("RegimeDeCriacao", Int32.TryParse(regime, out numero) ? "Autorização de pesquisa" : regime));

            if (idEstado > 0)
            {
                processo.AdicionarFiltro(Filtros.CriarAlias("Cidade", "cit"));
                processo.AdicionarFiltro(Filtros.CriarAlias("cit.Estado", "est"));
                processo.AdicionarFiltro(Filtros.Eq("est.Id", idEstado));
            }

            processo.AdicionarFiltro(Filtros.SubConsulta("Empresa"));

            if (idEmpresa > 0)
                processo.AdicionarFiltro(Filtros.Eq("Id", idEmpresa));
            else 
            {
                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<ProcessoDNPM>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("Id", empresasAux[index].Id);
                        processo.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }
            }

            processo.AdicionarFiltro(Filtros.SubConsulta("GrupoEconomico"));
            if (idGrupoEconomico > 0)
                processo.AdicionarFiltro(Filtros.Eq("Id", idGrupoEconomico));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<ProcessoDNPM> processos = fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(processo);
            if (!string.IsNullOrEmpty(regime) && regime.Trim() != "Extração" && regime.Trim() != "Licenciamento")
                for (int index = processos.Count - 1; index >= 0; index--)
                {
                    Regime aux = processos[index].GetRegimeAtual;
                    if (aux == null ||
                        numero == 0 && aux.GetType() != typeof(RequerimentoPesquisa) ||
                        numero == 1 && aux.GetType() != typeof(AlvaraPesquisa) ||
                        numero == 2 && aux.GetType() != typeof(RequerimentoLavra) ||
                        numero == 3 && aux.GetType() != typeof(ConcessaoLavra))
                        processos.RemoveAt(index);
                }
            return processos;
        }

        public virtual Regime GetRegimeAtual
        {
            get
            {
                if (this.Regimes != null)
                {
                    if (this.Regimes.Count == 1)
                        return this.Regimes[0];
                    else
                    {
                        Regime retorno = null;
                        foreach (Regime regime in this.Regimes)
                            if (retorno == null ||
                                (regime.GetType() == typeof(ConcessaoLavra) ||
                                ((retorno.GetType() == typeof(AlvaraPesquisa) || retorno.GetType() == typeof(RequerimentoPesquisa)) && regime.GetType() == typeof(RequerimentoLavra)) ||
                                (retorno.GetType() == typeof(RequerimentoPesquisa) && regime.GetType() == typeof(AlvaraPesquisa))))
                                retorno = regime;
                        return retorno;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Retorna o grupoEconomico do Processo, null caso não exista
        /// </summary>
        public virtual GrupoEconomico GetGrupoEconomico
        {
            get
            {
                return this.Empresa != null && this.Empresa.GrupoEconomico != null ? this.Empresa.GrupoEconomico : null;
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

        public virtual string GetEstado
        {
            get
            {
                return this.Cidade != null && this.Cidade.Estado != null ? this.Cidade.Estado.PegarSiglaEstado() : " - ";
            }
        }      

        public virtual String GetNumeroProcessoComMascara
        {
            get
            {
                if (this.Numero.Length == 10)
                    return numero.Substring(0, 3) + "." + numero.Substring(3, 3) + "/" + numero.Substring(6, 4);
                return numero;
            }
        }

        public static bool VerificarSeExisteProcessoComMesmoNumero(string Numero)
        {
            ProcessoDNPM p = new ProcessoDNPM();
            p.AdicionarFiltro(Filtros.Eq("Numero", Numero));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(p).Count > 0)
                return true;
            else
                return false;
        }

        public virtual string GetDescricaoRegimeAtual
        {
            get
            {
                return this.Regimes != null && this.Regimes.Count > 0 ? this.GetRegimeAtual.GetDescricao : "Ainda não criado";
            }
        }

        public virtual int GetQuantidadeDeEventosNaoModificados
        {
            get
            {
                EventoDNPM aux = new EventoDNPM();
                aux.AdicionarFiltro(Filtros.Distinct());
                aux.AdicionarFiltro(Filtros.Count("Id"));
                aux.AdicionarFiltro(Filtros.Eq("Atualizado", false));
                aux.AdicionarFiltro(Filtros.Eq("Irrelevante", false));
                aux.AdicionarFiltro(Filtros.Eq("ProcessoDNPM.Id", this.Id));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                return Convert.ToInt32("0" + fabrica.GetDAOBase().ConsultarProjecao(aux));
            }
        }

        public static ProcessoDNPM getProcessoPeloNumero(string NumeroProcesso)
        {
            ProcessoDNPM processo = new ProcessoDNPM();
            processo.AdicionarFiltro(Filtros.Distinct());
            processo.AdicionarFiltro(Filtros.Eq("Numero", NumeroProcesso));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ProcessoDNPM>(processo);
        }

        public static IList<ProcessoDNPM> ConsultarPorSubstanciaNumero(string substancia, int numero)
        {
            ProcessoDNPM processo = new ProcessoDNPM();
            processo.AdicionarFiltro(Filtros.Like("Substancia", substancia));
            processo.AdicionarFiltro(Filtros.Like("Numero", numero));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(processo);
        }

        public static IList<ProcessoDNPM> FiltrarRelatorioContratosPorProcessos(int IdGrupo, Empresa empresa, string numero, string substancia, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            ProcessoDNPM processo = new ProcessoDNPM();
            processo.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == IdGrupo).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    processo.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            processo.AdicionarFiltro(Filtros.Like("Substancia", substancia));
            processo.AdicionarFiltro(Filtros.Like("Numero", numero));

            processo.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

            if (empresa != null && empresa.Id > 0)
                processo.AdicionarFiltro(Filtros.Eq("E.Id", empresa.Id));
            else 
            {
                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == IdGrupo).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<ProcessoDNPM>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                        processo.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(processo);
        }

        public static IList<ProcessoDNPM> ConsultarProcessosDoClientePelasPermissoes(GrupoEconomico grupoEconomico, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            ProcessoDNPM p = new ProcessoDNPM();
            p.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == grupoEconomico.Id).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (processosAux != null && processosAux.Count > 0) 
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    p.AdicionarFiltro(Filtros.Ou(filtros));
                }                
            }

            p.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == grupoEconomico.Id).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (empresasAux != null && empresasAux.Count > 0) 
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                    p.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }                
            }

            p.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            p.AdicionarFiltro(Filtros.Eq("C.Id", grupoEconomico.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(p);
        }
        public static IList<ProcessoDNPM> ConsultarProcessosDoClientePelasPermissoesstatusEmpresa(GrupoEconomico grupoEconomico, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes, String status)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<ProcessoDNPM>();

            ProcessoDNPM p = new ProcessoDNPM();
            p.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == grupoEconomico.Id).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    p.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            p.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

            if (status == "Ativo")
            {
                p.AdicionarFiltro(Filtros.Eq("E.Ativo", true));
            }

            if (status == "Inativo")
            {
                p.AdicionarFiltro(Filtros.Eq("E.Ativo", false));
            }




            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == grupoEconomico.Id).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<ProcessoDNPM>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                    p.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

           

            p.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
            p.AdicionarFiltro(Filtros.Eq("C.Id", grupoEconomico.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(p);
        }

        public static IList<ProcessoDNPM> ObterProcessosQueOUsuarioPossuiAcesso(Usuario usuario)
        {
            IList<ProcessoDNPM> retorno = new List<ProcessoDNPM>();

            IList<ProcessoDNPM> processos = ProcessoDNPM.ConsultarTodos();
            if (processos != null && processos.Count > 0)
            {
                foreach (ProcessoDNPM processo in processos)
                {
                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(usuario)))
                        retorno.Add(processo);
                }
            }

            return retorno;
        }

        public static IList<ProcessoDNPM> FiltrarEnternalo(int de, int ate)
        {
            ProcessoDNPM processo = new ProcessoDNPM();            

            processo.AdicionarFiltro(Filtros.FaixaResultado(de, ate));
            processo.AdicionarFiltro(Filtros.SetOrderDesc("Id"));

            processo.AdicionarFiltro(Filtros.CriarAlias("Empresa","EM"));
            processo.AdicionarFiltro(Filtros.CriarAlias("EM.GrupoEconomico", "GE"));
            processo.AdicionarFiltro(Filtros.Eq("GE.AtivoAmbientalis", true));
            processo.AdicionarFiltro(Filtros.Eq("GE.AtivoLogus", true));
            processo.AdicionarFiltro(Filtros.Eq("GE.Cancelado", false));
            processo.AdicionarFiltro(Filtros.Eq("GE.AtivoAmbientalis", true));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ProcessoDNPM>(processo);
        }
    }
}
