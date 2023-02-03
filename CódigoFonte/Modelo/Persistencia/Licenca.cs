using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Licenca : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Licenca ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Licenca classe = new Licenca();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Licenca>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Licenca ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Licenca>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Licenca Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Licenca>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Licenca SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Licenca>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Licenca> SalvarTodos(IList<Licenca> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Licenca>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Licenca> SalvarTodos(params Licenca[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Licenca>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Licenca>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Licenca>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Licenca> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Licenca obj = Activator.CreateInstance<Licenca>();
            return fabrica.GetDAOBase().ConsultarTodos<Licenca>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Licenca> ConsultarOrdemAcendente(int qtd)
        {
            Licenca ee = new Licenca();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Licenca>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Licenca> ConsultarOrdemDescendente(int qtd)
        {
            Licenca ee = new Licenca();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Licenca>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Licenca
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Licenca> Filtrar(int qtd)
        {
            Licenca estado = new Licenca();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Licenca>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Licenca Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Licenca</returns>
        public virtual Licenca UltimoInserido()
        {
            Licenca estado = new Licenca();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Licenca>(estado);
        }

        /// <summary>
        /// Consulta todos os Licencas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Licencas armazenados ordenados pelo Nome</returns>
        public static IList<Licenca> ConsultarTodosOrdemAlfabetica()
        {
            Licenca aux = new Licenca();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Licenca>(aux);
        }

        public virtual Vencimento GetUltimoVencimento
        {
            get
            {
                if (this.Vencimentos != null && this.Vencimentos.Count > 0)
                    return this.Vencimentos[this.Vencimentos.Count - 1];
                return new Vencimento();
            }
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

        public static IList<Licenca> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, int idTipoLicenca, DateTime dataValidadeMin, DateTime dataValidadeMax, DateTime dataPrazoLimiteDe, DateTime dataPrazoLimiteAteh, string idOrgaoAmbiental, int idEstado, char tipoConfigPermissao, IList<Modelo.Empresa> empresasPermissoes, IList<Processo> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<Licenca>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<Licenca>();

            Licenca aux = new Licenca();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Between("PrazoLimiteRenovacao", dataPrazoLimiteDe, dataPrazoLimiteAteh));

            if (idTipoLicenca > 0)
                aux.AdicionarFiltro(Filtros.Eq("TipoLicenca.Id", idTipoLicenca));

            aux.AdicionarFiltro(Filtros.CriarAlias("Vencimentos", "vencs"));
            if (aux.GetUltimoVencimento.Id > 0)
                aux.AdicionarFiltro(Filtros.Eq("vencs.Id", aux.GetUltimoVencimento.Id));
            aux.AdicionarFiltro(Filtros.Between("vencs.Data", dataValidadeMin, dataValidadeMax));

            if (idEstado > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Cidade", "cit"));
                aux.AdicionarFiltro(Filtros.CriarAlias("cit.Estado", "est"));
                aux.AdicionarFiltro(Filtros.Eq("est.Id", idEstado));
            }

            aux.AdicionarFiltro(Filtros.SubConsulta("Processo"));

            if (tipoConfigPermissao == 'P')
            {
                IList<Processo> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<Licenca>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }
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
                        return new List<Licenca>();

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
            return fabrica.GetDAOBase().ConsultarComFiltro<Licenca>(aux);
        }

        public virtual string GetGrupoEconomico
        {
            get
            {
                return this.Processo != null && this.Processo.Empresa != null && this.Processo.Empresa.GrupoEconomico != null ? this.Processo.Empresa.GrupoEconomico.Nome : "Não definido";
            }
        }

        public virtual string GetEmpresa
        {
            get
            {
                return this.Processo != null && this.Processo.Empresa != null ? this.Processo.Empresa.Nome : "Não definido";
            }
        }

        public virtual string GetDescricaoLicenca
        {
            get
            {
                return (this.TipoLicenca != null ? this.TipoLicenca.Sigla + " - " : string.Empty) + this.Numero;
            }
        }

        public virtual string GetValidade
        {
            get
            {
                return this.GetUltimoVencimento != null ? this.GetUltimoVencimento.Data.ToShortDateString() : "";
            }
        }

        public virtual string GetNumeroProcesso
        {
            get
            {
                return this.Processo != null ? this.Processo.Numero : "--";
            }
        }

        public virtual string GetOrgaoAmbiental
        {
            get
            {
                return this.Processo != null && this.Processo.OrgaoAmbiental != null ? this.Processo.OrgaoAmbiental.GetNomeTipo + " - " + this.Processo.OrgaoAmbiental.Nome : "--";
            }
        }

        public virtual OrgaoAmbiental GetObjetoOrgaoAmbiental
        {
            get
            {
                return this.Processo != null && this.Processo.OrgaoAmbiental != null ? this.Processo.OrgaoAmbiental : null;
            }
        }

        public virtual string GetEstado
        {
            get
            {
                return this.Cidade != null && this.Cidade.Estado != null ? this.Cidade.Estado.PegarSiglaEstado() : " - ";
            }
        }
    }
}
