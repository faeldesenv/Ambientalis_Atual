using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class OutrosProcesso : Condicional
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OutrosProcesso ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OutrosProcesso classe = new OutrosProcesso();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OutrosProcesso>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OutrosProcesso ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OutrosProcesso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OutrosProcesso Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OutrosProcesso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OutrosProcesso SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OutrosProcesso>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OutrosProcesso> SalvarTodos(IList<OutrosProcesso> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OutrosProcesso>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OutrosProcesso> SalvarTodos(params OutrosProcesso[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OutrosProcesso>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OutrosProcesso>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OutrosProcesso>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OutrosProcesso> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OutrosProcesso obj = Activator.CreateInstance<OutrosProcesso>();
            return fabrica.GetDAOBase().ConsultarTodos<OutrosProcesso>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OutrosProcesso> ConsultarOrdemAcendente(int qtd)
        {
            OutrosProcesso ee = new OutrosProcesso();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosProcesso>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OutrosProcesso> ConsultarOrdemDescendente(int qtd)
        {
            OutrosProcesso ee = new OutrosProcesso();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosProcesso>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OutrosProcesso> Filtrar(int qtd)
        {
            OutrosProcesso estado = new OutrosProcesso();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosProcesso>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual OutrosProcesso UltimoInserido()
        {
            OutrosProcesso estado = new OutrosProcesso();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OutrosProcesso>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<OutrosProcesso> ConsultarTodosOrdemAlfabetica()
        {
            OutrosProcesso aux = new OutrosProcesso();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosProcesso>(aux);
        }

        public static IList<OutrosProcesso> ConsultarPorProcesso(Processo p)
        {
            OutrosProcesso aux = new OutrosProcesso();
            aux.AdicionarFiltro(Filtros.Eq("Processo.Id", p.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosProcesso>(aux);
        }

        public static IList<OutrosProcesso> Filtrar(Empresa empresa, string numero)
        {
            OutrosProcesso aux = new OutrosProcesso();
            aux.AdicionarFiltro(Filtros.Like("Numero", numero));
            if (empresa != null && empresa.Id > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Processo", "proc"));
                aux.AdicionarFiltro(Filtros.CriarAlias("proc.Empresa", "empr"));
                aux.AdicionarFiltro(Filtros.Eq("empr.Id", empresa.Id));
            }
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosProcesso>(aux);
        }

        public virtual Vencimento SetUltimoVencimento
        {
            set
            {
                if (!(this.Vencimentos != null && this.Vencimentos.Count > 0))
                {
                    this.Vencimentos = new List<Vencimento>();
                    this.Vencimentos.Add(new Vencimento());
                }
                this.Vencimentos[this.Vencimentos.Count - 1] = value;
            }
        }

        public static IList<Condicional> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDeVencimento, DateTime dataAtehVencimento, int idTipo, int periodico, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Processo> processosPermissoes)
        {
            if (idTipo != Condicional.VencimentoGeral)
            {
                OutrosProcesso aux = new OutrosProcesso();
                aux.AdicionarFiltro(Filtros.Distinct());

                aux.AdicionarFiltro(Filtros.CriarAlias("Vencimentos", "vencs"));

                if (periodico > 0)
                {
                    aux.AdicionarFiltro(Filtros.Eq("vencs.Periodico", periodico == 1));
                }

                if (aux.GetUltimoVencimento.Id > 0)
                    aux.AdicionarFiltro(Filtros.Eq("vencs.Id", aux.GetUltimoVencimento.Id));
                aux.AdicionarFiltro(Filtros.Between("vencs.Data", dataDeVencimento, dataAtehVencimento));
                aux.AdicionarFiltro(Filtros.SubConsulta("Processo"));

                if (tipoConfigPermissao == 'P')
                {
                    IList<Processo> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                    if (processosAux == null || processosAux.Count == 0)
                        return new List<Condicional>();

                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }

                aux.AdicionarFiltro(Filtros.SubConsulta("Empresa"));
                if (idEmpresa > 0)
                    aux.AdicionarFiltro(Filtros.Eq("Id", idEmpresa));
                else 
                {
                    if (tipoConfigPermissao == 'E')
                    {
                        IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                        if (empresasAux == null || empresasAux.Count == 0)
                            return new List<Condicional>();

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
                if (idGrupoEconomico > 0)
                    aux.AdicionarFiltro(Filtros.Eq("Id", idGrupoEconomico));

                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                return fabrica.GetDAOBase().ConsultarComFiltro<Condicional>(aux);
            }
            return new List<Condicional>();
        }

        public override GrupoEconomico GetGrupoEconomico
        {
            get
            {
                return this.Processo != null && this.Processo.Empresa != null && this.Processo.Empresa.GrupoEconomico != null ?
                    this.Processo.Empresa.GrupoEconomico : null;
            }
        }

        public override Empresa GetEmpresa
        {
            get
            {
                return this.Processo != null && this.Processo.Empresa != null ? this.Processo.Empresa : null;
            }
        }
        

        public override Processo GetProcesso
        {
            get
            {
                return this.Processo;
            }
        }

        public override OrgaoAmbiental GetOrgaoAmbiental
        {
            get
            {
                return this.Processo != null ? this.Processo.OrgaoAmbiental : null;
            }
        }
    }
}
