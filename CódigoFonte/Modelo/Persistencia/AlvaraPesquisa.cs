using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class AlvaraPesquisa : AutorizacaoPesquisa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static AlvaraPesquisa ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            AlvaraPesquisa classe = new AlvaraPesquisa();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<AlvaraPesquisa>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual AlvaraPesquisa ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<AlvaraPesquisa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual AlvaraPesquisa Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<AlvaraPesquisa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual AlvaraPesquisa SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<AlvaraPesquisa>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<AlvaraPesquisa> SalvarTodos(IList<AlvaraPesquisa> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<AlvaraPesquisa>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<AlvaraPesquisa> SalvarTodos(params AlvaraPesquisa[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<AlvaraPesquisa>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<AlvaraPesquisa>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<AlvaraPesquisa>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<AlvaraPesquisa> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            AlvaraPesquisa obj = Activator.CreateInstance<AlvaraPesquisa>();
            return fabrica.GetDAOBase().ConsultarTodos<AlvaraPesquisa>(obj);
        }
                
        /// <summary>
        /// Filtra uma certa quantidade de Alvará de Pesquisa
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<AlvaraPesquisa> Filtrar(int qtd)
        {
            AlvaraPesquisa estado = new AlvaraPesquisa();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<AlvaraPesquisa>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Alvará de Pesquisa Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Alvará de Pesquisa</returns>
        public virtual AlvaraPesquisa UltimoInserido()
        {
            AlvaraPesquisa estado = new AlvaraPesquisa();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<AlvaraPesquisa>(estado);
        }

        public virtual Vencimento GetUltimaTaxaAnualHectare
        {
            get { 
                if(this.TaxaAnualPorHectare != null && this.TaxaAnualPorHectare.Count > 0){
                    return TaxaAnualPorHectare[TaxaAnualPorHectare.Count - 1];
                }
                return null;
            }
        }

        public virtual Vencimento GetUltimoDIPEM
        {
            get
            {
                if (this.DIPEM != null && this.DIPEM.Count > 0)
                {
                    return this.DIPEM[this.DIPEM.Count - 1];
                }
                return null;
            }
        }

        public override string GetDescricao
        {
            get { return "Alvará de Pesquisa"; }
        }

        public static IList<AlvaraPesquisa> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDeVencimentoRenuncia, DateTime dataAteVencimentoRenuncia, int idEstado, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<ProcessoDNPM> processosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<AlvaraPesquisa>();

            if (tipoConfigPermissao == 'P' && (processosPermissoes == null || processosPermissoes.Count == 0))
                return new List<AlvaraPesquisa>();

            AlvaraPesquisa alvara = new AlvaraPesquisa();
            alvara.AdicionarFiltro(Filtros.CriarAlias("LimiteRenuncia", "limite"));
            alvara.AdicionarFiltro(Filtros.Between("limite.Data", dataDeVencimentoRenuncia, dataAteVencimentoRenuncia));

            alvara.AdicionarFiltro(Filtros.CriarAlias("ProcessoDNPM", "proc"));

            if (tipoConfigPermissao == 'P')
            {
                IList<ProcessoDNPM> processosAux = processosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (processosAux == null || processosAux.Count == 0)
                    return new List<AlvaraPesquisa>();

                if (processosAux != null && processosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[processosAux.Count];
                    for (int index = 0; index < processosAux.Count; index++)
                        filtros[index] = Filtros.Eq("proc.Id", processosAux[index].Id);
                    alvara.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            alvara.AdicionarFiltro(Filtros.CriarAlias("proc.Empresa", "E"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<AlvaraPesquisa>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                    alvara.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<AlvaraPesquisa> alvaras =  fabrica.GetDAOBase().ConsultarComFiltro<AlvaraPesquisa>(alvara);
            AlvaraPesquisa.RemoverRenunciasDeOutroGrupoEconomico(alvaras, idGrupoEconomico);
            AlvaraPesquisa.RemoverRenunciasDeOutraEmpresa(alvaras, idEmpresa);
            AlvaraPesquisa.RemoverRenunciasDeOutroEstado(alvaras, idEstado);
            return alvaras;
        }

        private static void RemoverRenunciasDeOutroGrupoEconomico(IList<AlvaraPesquisa> alvaras, int idGrupoEconomico)
        {
            if (idGrupoEconomico > 0 && alvaras != null && alvaras.Count > 0)
                for (int index = alvaras.Count - 1; index >= 0; index--)
                {
                    GrupoEconomico aux = alvaras[index].ProcessoDNPM.Empresa.GrupoEconomico;
                    if (aux == null || aux.Id != idGrupoEconomico)
                        alvaras.RemoveAt(index);
                }
        }

        private static void RemoverRenunciasDeOutraEmpresa(IList<AlvaraPesquisa> alvaras, int idEmpresa)
        {
            if (idEmpresa > 0 && alvaras != null && alvaras.Count > 0)
                for (int index = alvaras.Count - 1; index >= 0; index--)
                {
                    Empresa aux = alvaras[index].ProcessoDNPM.Empresa;
                    if (aux == null || aux.Id != idEmpresa)
                        alvaras.RemoveAt(index);
                }
        }

        private static void RemoverRenunciasDeOutroEstado(IList<AlvaraPesquisa> alvaras, int idEstado)
        {
            if (idEstado > 0 && alvaras != null && alvaras.Count > 0)
                for (int index = alvaras.Count - 1; index >= 0; index--)
                {
                    Estado estado = alvaras[index].ProcessoDNPM != null &&  alvaras[index].ProcessoDNPM.Cidade != null ? alvaras[index].ProcessoDNPM.Cidade.Estado : null;
                    if (estado == null || estado.Id != idEstado)
                        alvaras.RemoveAt(index);
                }
        }

        public virtual string GetGrupoEconomico 
        {
            get 
            {
                return this.ProcessoDNPM != null && this.ProcessoDNPM.Empresa != null && this.ProcessoDNPM.Empresa.GrupoEconomico != null ? this.ProcessoDNPM.Empresa.GrupoEconomico.Nome : "Não definido";
            }
        }

        public virtual string GetNomeEmpresa
        {
            get
            {
                return this.ProcessoDNPM != null && this.ProcessoDNPM.Empresa != null ? this.ProcessoDNPM.Empresa.Nome + " - " + this.ProcessoDNPM.Empresa.GetNumeroCNPJeCPFComMascara : "Não definido";
            }
        }

        public virtual string GetNumeroProcessoDNPM
        {
            get
            {
                return this.ProcessoDNPM != null ? this.ProcessoDNPM.GetNumeroProcessoComMascara : "--";
            }
        }

        public virtual string GetDataVencimentoRenunciaAlvara
        {
            get
            {
                return this.LimiteRenuncia != null ? this.LimiteRenuncia.Data.ToShortDateString() : "--";
            }
        }

        public virtual string GetEstadoRenuncia
        {
            get
            {
                return this.ProcessoDNPM != null && this.ProcessoDNPM.Cidade != null && this.ProcessoDNPM.Cidade.Estado != null ? this.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : "--";
            }
        }

        public static bool ConsultarPorProcessoEDataDePublicacao(int idProcesso, DateTime dataPublicacao)
        {
            AlvaraPesquisa aux = new AlvaraPesquisa();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Eq("DataPublicacao", dataPublicacao));
            aux.AdicionarFiltro(Filtros.CriarAlias("ProcessoDNPM", "proc"));
            aux.AdicionarFiltro(Filtros.Eq("proc.Id", idProcesso));
            aux.AdicionarFiltro(Filtros.Max("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            AlvaraPesquisa existente = fabrica.GetDAOBase().ConsultarUnicoComFiltro<AlvaraPesquisa>(aux);

            return existente != null && existente.Id > 0;
        }
    }
}
