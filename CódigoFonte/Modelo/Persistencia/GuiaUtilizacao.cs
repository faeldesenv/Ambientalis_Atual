using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class GuiaUtilizacao : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static GuiaUtilizacao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GuiaUtilizacao classe = new GuiaUtilizacao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<GuiaUtilizacao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual GuiaUtilizacao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<GuiaUtilizacao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual GuiaUtilizacao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<GuiaUtilizacao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual GuiaUtilizacao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<GuiaUtilizacao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<GuiaUtilizacao> SalvarTodos(IList<GuiaUtilizacao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<GuiaUtilizacao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<GuiaUtilizacao> SalvarTodos(params GuiaUtilizacao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<GuiaUtilizacao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<GuiaUtilizacao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<GuiaUtilizacao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<GuiaUtilizacao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GuiaUtilizacao obj = Activator.CreateInstance<GuiaUtilizacao>();
            return fabrica.GetDAOBase().ConsultarTodos<GuiaUtilizacao>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<GuiaUtilizacao> ConsultarOrdemAcendente(int qtd)
        {
            GuiaUtilizacao ee = new GuiaUtilizacao();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GuiaUtilizacao>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<GuiaUtilizacao> ConsultarOrdemDescendente(int qtd)
        {
            GuiaUtilizacao ee = new GuiaUtilizacao();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GuiaUtilizacao>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<GuiaUtilizacao> Filtrar(int qtd)
        {
            GuiaUtilizacao estado = new GuiaUtilizacao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GuiaUtilizacao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual GuiaUtilizacao UltimoInserido()
        {
            GuiaUtilizacao estado = new GuiaUtilizacao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<GuiaUtilizacao>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<GuiaUtilizacao> ConsultarTodosOrdemAlfabetica()
        {
            GuiaUtilizacao aux = new GuiaUtilizacao();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GuiaUtilizacao>(aux);
        }

        public virtual Vencimento GetUltimoVencimento
        {
            get
            {
                if (this.Vencimentos != null && this.Vencimentos.Count > 0)
                {
                    return Vencimentos[Vencimentos.Count - 1];
                }
                return null;
            }
        }

        public static IList<GuiaUtilizacao> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDePeriodo, DateTime dataAtehPeriodo, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<GuiaUtilizacao>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<GuiaUtilizacao>();

            GuiaUtilizacao aux = new GuiaUtilizacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("ProcessoDNPM", "proc"));

                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<GuiaUtilizacao>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("proc.Id", processosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            if (tipoConfigPermissao == 'E')
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("ProcessoDNPM", "proc"));
                aux.AdicionarFiltro(Filtros.CriarAlias("proc.Empresa", "empr"));

                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<GuiaUtilizacao>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("empr.Id", empresasAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            aux.AdicionarFiltro(Filtros.CriarAlias("Vencimentos", "venc"));
            aux.AdicionarFiltro(Filtros.Between("venc.Data", dataDePeriodo, dataAtehPeriodo));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<GuiaUtilizacao> guias = fabrica.GetDAOBase().ConsultarComFiltro<GuiaUtilizacao>(aux);
            GuiaUtilizacao.RemoverGuiaDeOutroGrupoEconomico(guias, idGrupoEconomico);
            GuiaUtilizacao.RemoverGuiaDeOutraEmpresa(guias, idEmpresa);

            return guias;
        }

        private static void RemoverGuiaDeOutraEmpresa(IList<GuiaUtilizacao> guias, int idEmpresa)
        {
            if (idEmpresa > 0)
                for (int index = guias.Count - 1; index >= 0; index--)
                {
                    Empresa aux = guias[index].ProcessoDNPM.Empresa;
                    if (aux == null || aux.Id != idEmpresa)
                        guias.RemoveAt(index);
                }
        }

        private static void RemoverGuiaDeOutroGrupoEconomico(IList<GuiaUtilizacao> guias, int idGrupoEconomico)
        {
            if (idGrupoEconomico > 0)
                for (int index = guias.Count - 1; index >= 0; index--)
                {
                    GrupoEconomico aux = guias[index].ProcessoDNPM.Empresa.GrupoEconomico;
                    if (aux == null || aux.Id != idGrupoEconomico)
                        guias.RemoveAt(index);
                }
        }

        public virtual string GetGrupoEconomico 
        {
            get 
            {
                return this.ProcessoDNPM != null && this.ProcessoDNPM.Empresa != null && this.ProcessoDNPM.Empresa.GrupoEconomico != null ? this.ProcessoDNPM.Empresa.GrupoEconomico.Nome : "Não definido";
            }
        }

        public virtual string GetEmpresa
        {
            get
            {
                return this.ProcessoDNPM != null && this.ProcessoDNPM.Empresa != null ? this.ProcessoDNPM.Empresa.Nome : "Não definido";
            }
        }

        public virtual string GetProcesso
        {
            get
            {
                return this.ProcessoDNPM != null ? this.ProcessoDNPM.GetNumeroProcessoComMascara : "--";
            }
        }

        public virtual string GetDataProximoVencimento
        {
            get
            {
                return this.GetUltimoVencimento != null ? this.GetUltimoVencimento.Data.ToShortDateString() : "--";
            }
        }

        public static bool ConsultarPorProcessoEDataDeRequerimento(int idProcesso, DateTime dataRequerimento)
        {
            GuiaUtilizacao aux = new GuiaUtilizacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Eq("DataRequerimento", dataRequerimento));
            aux.AdicionarFiltro(Filtros.CriarAlias("ProcessoDNPM", "proc"));
            aux.AdicionarFiltro(Filtros.Eq("proc.Id", idProcesso));
            aux.AdicionarFiltro(Filtros.Max("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GuiaUtilizacao existente = fabrica.GetDAOBase().ConsultarUnicoComFiltro<GuiaUtilizacao>(aux);

            return existente != null && existente.Id > 0;
        }
    }
}
