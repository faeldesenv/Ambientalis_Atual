using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class RAL : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static RAL ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            RAL classe = new RAL();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<RAL>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual RAL ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<RAL>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual RAL Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<RAL>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual RAL SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<RAL>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<RAL> SalvarTodos(IList<RAL> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<RAL>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<RAL> SalvarTodos(params RAL[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<RAL>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<RAL>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<RAL>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<RAL> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            RAL obj = Activator.CreateInstance<RAL>();
            return fabrica.GetDAOBase().ConsultarTodos<RAL>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<RAL> ConsultarOrdemAcendente(int qtd)
        {
            RAL ee = new RAL();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<RAL>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<RAL> ConsultarOrdemDescendente(int qtd)
        {
            RAL ee = new RAL();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<RAL>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<RAL> Filtrar(int qtd)
        {
            RAL estado = new RAL();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<RAL>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual RAL UltimoInserido()
        {
            RAL estado = new RAL();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<RAL>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<RAL> ConsultarTodosOrdemAlfabetica()
        {
            RAL aux = new RAL();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<RAL>(aux);
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

        public virtual Vencimento GetVencimentoAtual
        {
            get
            {
                Vencimento retorno = new Vencimento();

                if (this.Vencimentos != null && this.Vencimentos.Count > 0)
                {
                    retorno.Data = SqlDate.MinValue;

                    DateTime hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                    foreach (Vencimento item in this.Vencimentos)
                    {
                        if (item.Data > retorno.Data && item.Data <= hoje)
                            retorno = item;
                    }
                }
                return retorno;
            }
        }

        public virtual Vencimento GetMaiorVencimento
        {
            get
            {

                if (this.Vencimentos != null && this.Vencimentos.Count > 0)
                {
                    Vencimento aux = new Vencimento();
                    aux.AdicionarFiltro(Filtros.Distinct());
                    aux.AdicionarFiltro(Filtros.Max("Data"));
                    aux.AdicionarFiltro(Filtros.CriarAlias("Ral", "ral"));
                    aux.AdicionarFiltro(Filtros.Eq("ral.Id", this.Id));

                    FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                    IList<Vencimento> vencimts = fabrica.GetDAOBase().ConsultarComFiltro<Vencimento>(aux);

                    if (vencimts != null && vencimts.Count > 0)
                        return vencimts[0];

                    
                }
                return null;
            }
        }

        public static IList<RAL> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDePeriodo, DateTime dataAtehPeriodo, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<RAL>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<RAL>();

            RAL aux = new RAL();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.CriarAlias("Vencimentos", "venc"));
            aux.AdicionarFiltro(Filtros.Between("venc.Data", dataDePeriodo, dataAtehPeriodo));

            aux.AdicionarFiltro(Filtros.SubConsulta("ProcessoDNPM"));

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<RAL>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", processosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            aux.AdicionarFiltro(Filtros.SubConsulta("Empresa"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<RAL>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("Id", empresasAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<RAL> rals = fabrica.GetDAOBase().ConsultarComFiltro<RAL>(aux);
            RAL.RemoverRalDeOutroGrupoEconomico(rals, idGrupoEconomico);
            RAL.RemoverRalDeOutraEmpresa(rals, idEmpresa);
           
            return rals;
        
        }

        private static void RemoverRalDeOutraEmpresa(IList<RAL> rals, int idEmpresa)
        {
            if (idEmpresa > 0)
                for (int index = rals.Count - 1; index >= 0; index--)
                {
                    Empresa aux = rals[index].ProcessoDNPM.Empresa;
                    if (aux == null || aux.Id != idEmpresa)
                        rals.RemoveAt(index);
                }
        }

        private static void RemoverRalDeOutroGrupoEconomico(IList<RAL> rals, int idGrupoEconomico)
        {
            if (idGrupoEconomico > 0)
                for (int index = rals.Count - 1; index >= 0; index--)
                {
                    GrupoEconomico aux = rals[index].ProcessoDNPM.Empresa.GrupoEconomico;
                    if (aux == null || aux.Id != idGrupoEconomico)
                        rals.RemoveAt(index);
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
                return this.ProcessoDNPM != null && this.ProcessoDNPM.Empresa != null ? this.ProcessoDNPM.Empresa.Nome + " - " + this.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara : "Não definido";
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

    }
}
