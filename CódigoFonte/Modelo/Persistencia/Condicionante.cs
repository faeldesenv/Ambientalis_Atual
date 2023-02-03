using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Condicionante : Condicional
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Condicionante ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Condicionante classe = new Condicionante();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Condicionante>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Condicionante ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Condicionante>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Condicionante Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Condicionante>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Condicionante SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Condicionante>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Condicionante> SalvarTodos(IList<Condicionante> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Condicionante>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Condicionante> SalvarTodos(params Condicionante[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Condicionante>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Condicionante>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Condicionante>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Condicionante> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Condicionante obj = Activator.CreateInstance<Condicionante>();
            return fabrica.GetDAOBase().ConsultarTodos<Condicionante>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Condicionante> ConsultarOrdemAcendente(int qtd)
        {
            Condicionante ee = new Condicionante();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Condicionante>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Condicionante> ConsultarOrdemDescendente(int qtd)
        {
            Condicionante ee = new Condicionante();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Condicionante>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Condicionante
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Condicionante> Filtrar(int qtd)
        {
            Condicionante estado = new Condicionante();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Condicionante>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Condicionante Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Condicionante</returns>
        public virtual Condicionante UltimoInserido()
        {
            Condicionante estado = new Condicionante();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Condicionante>(estado);
        }

        /// <summary>
        /// Consulta todos os Condicionantes armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Condicionantes armazenados ordenados pelo Nome</returns>
        public static IList<Condicionante> ConsultarTodosOrdemAlfabetica()
        {
            Condicionante aux = new Condicionante();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Condicionante>(aux);
        }

        //public virtual void CalcularDatas()
        //{
        //    if (this.Licenca != null)
        //        this.DataVencimento = this.licenca.DataRetirada.AddDays(this.DiasPrazo);
        //    this.DataVencimento = this.DataVencimento.AddDays(this.PrazoAdicional);
        //    this.DataAviso = this.DataVencimento.AddDays(-this.DiasAviso);
        //}

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

        public static IList<Condicionante> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDeVencimento, DateTime dataAtehVencimento, int idOrgaoAmbiental, int idEstado, int idStatus, int condionantePeriodica, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Processo> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<Condicionante>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<Condicionante>();

            Condicionante aux = new Condicionante();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.CriarAlias("Vencimentos", "vencs"));

            if (condionantePeriodica > 0) 
            {
                aux.AdicionarFiltro(Filtros.Eq("vencs.Periodico", condionantePeriodica == 1)); 
            }  
           
            if (idStatus > 0) 
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("vencs.Status", "stats"));
                aux.AdicionarFiltro(Filtros.Eq("stats.Id", idStatus));                
            }

            if (aux.GetUltimoVencimento.Id > 0)
                aux.AdicionarFiltro(Filtros.Eq("vencs.Id", aux.GetUltimoVencimento.Id));
            aux.AdicionarFiltro(Filtros.Between("vencs.Data", dataDeVencimento, dataAtehVencimento));

            aux.AdicionarFiltro(Filtros.CriarAlias("Licenca", "lic"));

            if (idEstado > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("lic.Cidade", "cit"));
                aux.AdicionarFiltro(Filtros.CriarAlias("cit.Estado", "est"));
                aux.AdicionarFiltro(Filtros.Eq("est.Id", idEstado));
            }

            aux.AdicionarFiltro(Filtros.CriarAlias("lic.Processo", "proc"));

            if (tipoConfigPermissao == 'P')
            {
                IList<Processo> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<Condicionante>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("proc.Id", processosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            if (idOrgaoAmbiental > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("proc.OrgaoAmbiental", "org"));
                aux.AdicionarFiltro(Filtros.Eq("org.Id", idOrgaoAmbiental));
            }

            if (idGrupoEconomico > 0 || idEmpresa > 0) 
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("proc.Empresa", "emp"));

                if (idEmpresa > 0)
                    aux.AdicionarFiltro(Filtros.Eq("emp.Id", idEmpresa));
                else 
                {
                    if (tipoConfigPermissao == 'E')
                    { 
                        IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                        if (empresasAux == null || empresasAux.Count == 0)
                            return new List<Condicionante>();

                        if (empresasAux != null && empresasAux.Count > 0)
                        {
                            Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                            for (int index = 0; index < empresasAux.Count; index++)
                                filtrosEmps[index] = Filtros.Eq("emp.Id", empresasAux[index].Id);
                            aux.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                        }
                    }
                }

                if (idGrupoEconomico > 0)
                {
                    aux.AdicionarFiltro(Filtros.CriarAlias("emp.GrupoEconomico", "C"));
                    aux.AdicionarFiltro(Filtros.Eq("C.Id", idGrupoEconomico));
                }
            }
            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Condicionante>(aux);
        }

        public virtual string GetGrupoEconomico
        {
            get
            {
                return this.Licenca != null && this.Licenca.Processo != null && this.Licenca.Processo.Empresa != null && this.Licenca.Processo.Empresa.GrupoEconomico != null ?
                    this.Licenca.Processo.Empresa.GrupoEconomico.Nome : "--";
            }
        }

        public virtual string GetEmpresa
        {
            get
            {
                return this.Licenca != null && this.Licenca.Processo != null && this.Licenca.Processo.Empresa != null ?
                   this.Licenca.Processo.Empresa.Nome : "--";
            }
        }

        public virtual Empresa GetObjetoEmpresa
        {
            get
            {
                return this.Licenca != null && this.Licenca.Processo != null && this.Licenca.Processo.Empresa != null ?
                   this.Licenca.Processo.Empresa : null;
            }
        }

        public virtual string GetDescricaoLicenca
        {
            get
            {
                return this.Licenca != null ? (this.Licenca.TipoLicenca != null ? this.Licenca.TipoLicenca.Sigla + " - " : "") + this.Licenca.Numero : "--";
            }
        }

        public virtual string GetNumeroProcesso
        {
            get
            {
                return this.Licenca != null && this.Licenca.Processo != null ?
                    this.Licenca.Processo.Numero : "--";
            }
        }

        public virtual string GetOrgaoAmbiental
        {
            get
            {
                return this.Licenca != null && this.Licenca.Processo != null && this.Licenca.Processo.OrgaoAmbiental != null ?
                    this.Licenca.Processo.OrgaoAmbiental.GetNomeTipo + " - " + this.Licenca.Processo.OrgaoAmbiental.Nome : "--";
            }
        }

        public virtual Processo GetProcesso
        {
            get
            {
                return this.Licenca != null && this.Licenca.Processo != null ?
                    this.Licenca.Processo : null;

            }
        }

        public virtual string GetDescricaoCondicionante
        {
            get
            {
                return this.Numero + " - " + this.Descricao;

            }
        }

        public virtual string GetStatus
        {
            get
            {
                return this.GetUltimoVencimento.Id > 0 && this.GetUltimoVencimento.Status != null ? this.GetUltimoVencimento.Status.Nome : "--";

            }
        }

        public virtual string GetDataVencimento
        {
            get
            {
                return this.GetUltimoVencimento.Id > 0 ? this.GetUltimoVencimento.Data.ToShortDateString() : "--";

            }
        }

        public virtual string GetPeriodica
        {
            get
            {
                return this.GetUltimoVencimento != null ? this.GetUltimoVencimento.Periodico ? "Sim" : "Não" : "";

            }
        }

        public virtual string GetQtdProrrogacoes
        {
            get
            {
                return this.GetUltimoVencimento != null && this.GetUltimoVencimento.ProrrogacoesPrazo != null ? this.GetUltimoVencimento.ProrrogacoesPrazo.Count.ToString() : "0";

            }
        }

        public virtual string GetEstadoCondicionante
        {
            get
            {
                return this.Licenca != null && this.Licenca.Cidade != null && this.Licenca.Cidade.Estado != null ? this.Licenca.Cidade.Estado.PegarSiglaEstado() : " - ";

            }
        }

        

        

        

        

        

        
    }
}
