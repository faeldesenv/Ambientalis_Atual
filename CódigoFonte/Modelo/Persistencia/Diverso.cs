using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Diverso : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Diverso ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Diverso classe = new Diverso();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Diverso>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Diverso ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Diverso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Diverso Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Diverso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Diverso SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Diverso>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Diverso> SalvarTodos(IList<Diverso> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Diverso>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Diverso> SalvarTodos(params Diverso[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Diverso>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Diverso>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Diverso>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Diverso> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Diverso obj = Activator.CreateInstance<Diverso>();
            return fabrica.GetDAOBase().ConsultarTodos<Diverso>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Diverso> ConsultarOrdemAcendente(int qtd)
        {
            Diverso ee = new Diverso();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Diverso>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Diverso> ConsultarOrdemDescendente(int qtd)
        {
            Diverso ee = new Diverso();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Diverso>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Estado
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Diverso> Filtrar(int qtd)
        {
            Diverso estado = new Diverso();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Diverso>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Estado Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Estado</returns>
        public virtual Diverso UltimoInserido()
        {
            Diverso estado = new Diverso();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Diverso>(estado);
        }

        /// <summary>
        /// Consulta todos os Estados armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Estados armazenados ordenados pelo Nome</returns>
        public static IList<Diverso> ConsultarTodosOrdemAlfabetica()
        {
            Diverso aux = new Diverso();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Diverso>(aux);
        }

        public virtual VencimentoDiverso GetUltimoVencimento
        {
            get
            {
                if (this.VencimentosDiversos != null && this.VencimentosDiversos.Count > 0)
                {
                    return VencimentosDiversos[VencimentosDiversos.Count - 1];
                }
                return null;
            }
        }

        public static IList<Diverso> Filtrar(GrupoEconomico grupo, Empresa empresa, string descricao, TipoDiverso tipo, StatusDiverso status, IList<Empresa> empresasQueOUsuarioTemAcesso, int valorInicial, int valorFinal)
        {
            Diverso diverso = new Diverso();

            if (empresa != null || grupo != null || (empresasQueOUsuarioTemAcesso != null && empresasQueOUsuarioTemAcesso.Count > 0)) 
            { 
            
                diverso.AdicionarFiltro(Filtros.CriarAlias("Empresa", "emp"));

                if (empresa != null && empresa.Id > 0)
                {
                    diverso.AdicionarFiltro(Filtros.Eq("emp.Id", empresa.Id));
                }
                else 
                {
                    if (empresasQueOUsuarioTemAcesso != null && empresasQueOUsuarioTemAcesso.Count > 0) 
                    {
                        Filtro[] filtros = new Filtro[empresasQueOUsuarioTemAcesso.Count];
                        for (int index = 0; index < empresasQueOUsuarioTemAcesso.Count; index++)
                            filtros[index] = Filtros.Eq("emp.Id", empresasQueOUsuarioTemAcesso[index].Id);
                        diverso.AdicionarFiltro(Filtros.Ou(filtros));                
                    }                    
                }

                diverso.AdicionarFiltro(Filtros.CriarAlias("emp.GrupoEconomico", "empgrup"));

                if (grupo != null && grupo.Id > 0) 
                {
                    diverso.AdicionarFiltro(Filtros.Eq("empgrup.Id", grupo.Id));
                }
            }

            if(descricao != null && descricao != "")
                diverso.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (tipo != null && tipo.Id > 0 && status == null) 
            {
                diverso.AdicionarFiltro(Filtros.CriarAlias("TipoDiverso", "tip"));
                diverso.AdicionarFiltro(Filtros.Eq("tip.Id", tipo.Id));
            }

            if (status != null && status.Id > 0) 
            {
                diverso.AdicionarFiltro(Filtros.CriarAlias("VencimentosDiversos", "vencdiv"));
                diverso.AdicionarFiltro(Filtros.CriarAlias("vencdiv.StatusDiverso", "statdiv"));
                diverso.AdicionarFiltro(Filtros.Eq("statdiv.Id", status.Id));
            }

            diverso.AdicionarFiltro(Filtros.FaixaResultado(valorInicial, valorFinal));
            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Diverso>(diverso);
        }

        public static IList<Diverso> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, string descricao, TipoDiverso tipo, StatusDiverso status, DateTime dataDeVencimentoDiverso, DateTime dataAteVencimentoDiverso, int periodico, char tipoConfigPermissao, IList<Empresa> empresasPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<Diverso>();

            Diverso diverso = new Diverso();
            diverso.AdicionarFiltro(Filtros.Distinct());

            diverso.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<Diverso>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                    diverso.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            if (descricao != null && descricao != "")
                diverso.AdicionarFiltro(Filtros.Like("Descricao", descricao));            

            if (tipo != null && tipo.Id > 0 && status == null)
            {
                diverso.AdicionarFiltro(Filtros.CriarAlias("TipoDiverso", "tip"));
                diverso.AdicionarFiltro(Filtros.Eq("tip.Id", tipo.Id));
            }

            diverso.AdicionarFiltro(Filtros.CriarAlias("VencimentosDiversos", "vencdiv"));
            diverso.AdicionarFiltro(Filtros.Between("vencdiv.Data", dataDeVencimentoDiverso, dataAteVencimentoDiverso));

            if (periodico > 0) 
            {
                diverso.AdicionarFiltro(Filtros.Eq("vencdiv.Periodico", periodico == 1));
            }

            if (status != null && status.Id > 0)
            {                
                diverso.AdicionarFiltro(Filtros.CriarAlias("vencdiv.StatusDiverso", "statdiv"));
                diverso.AdicionarFiltro(Filtros.Eq("statdiv.Id", status.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Diverso> diversos = fabrica.GetDAOBase().ConsultarComFiltro<Diverso>(diverso);
            Diverso.RemoverDiversosDeOutroGrupoEconomico(diversos, idGrupoEconomico);
            Diverso.RemoverDiversosDeOutraEmpresa(diversos, idEmpresa);

            return diversos;
        }

        private static void RemoverDiversosDeOutraEmpresa(IList<Diverso> diversos, int idEmpresa)
        {
            if (idEmpresa > 0)
                for (int index = diversos.Count - 1; index >= 0; index--)
                {
                    Empresa aux = diversos[index].Empresa;
                    if (aux == null || aux.Id != idEmpresa)
                        diversos.RemoveAt(index);
                }
        }

        private static void RemoverDiversosDeOutroGrupoEconomico(IList<Diverso> diversos, int idGrupoEconomico)
        {
            if (idGrupoEconomico > 0)
                for (int index = diversos.Count - 1; index >= 0; index--)
                {
                    GrupoEconomico aux = diversos[index].Empresa.GrupoEconomico;
                    if (aux == null || aux.Id != idGrupoEconomico)
                        diversos.RemoveAt(index);
                }
        }

        public virtual string GetGrupoEconomico 
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

        public virtual string GetTipoDiverso
        {
            get
            {
                return this.TipoDiverso != null ? this.TipoDiverso.Nome : "--";
            }
        }

        public virtual string GetStatusAtualVencimento
        {
            get
            {
                return this.GetUltimoVencimento != null && this.GetUltimoVencimento.StatusDiverso != null ? this.GetUltimoVencimento.StatusDiverso.Nome : "--";
            }
        }

        public virtual string GetPeriodico
        {
            get
            {
                return this.GetUltimoVencimento != null && this.GetUltimoVencimento.Periodico ? "Sim" : "Não";
            }
        }

        public virtual string GetDataVencimento
        {
            get
            {
                return this.GetUltimoVencimento != null ? this.GetUltimoVencimento.Data.ToShortDateString() : "--";
            }
        }

        public virtual DateTime GetDataDateVencimento
        {
            get
            {
                return this.GetUltimoVencimento != null ? this.GetUltimoVencimento.Data : SqlDate.MinValue;
            }
        }

        public static int FiltrarContador(GrupoEconomico grupo, Empresa empresa, string descricao, TipoDiverso tipo, StatusDiverso status, IList<Empresa> empresasQueOUsuarioTemAcesso)
        {
            Diverso diverso = new Diverso();

            if (empresa != null || grupo != null || (empresasQueOUsuarioTemAcesso != null && empresasQueOUsuarioTemAcesso.Count > 0))
            {

                diverso.AdicionarFiltro(Filtros.CriarAlias("Empresa", "emp"));

                if (empresa != null && empresa.Id > 0)
                {
                    diverso.AdicionarFiltro(Filtros.Eq("emp.Id", empresa.Id));
                }
                else
                {
                    if (empresasQueOUsuarioTemAcesso != null && empresasQueOUsuarioTemAcesso.Count > 0)
                    {
                        Filtro[] filtros = new Filtro[empresasQueOUsuarioTemAcesso.Count];
                        for (int index = 0; index < empresasQueOUsuarioTemAcesso.Count; index++)
                            filtros[index] = Filtros.Eq("emp.Id", empresasQueOUsuarioTemAcesso[index].Id);
                        diverso.AdicionarFiltro(Filtros.Ou(filtros));
                    }
                }

                diverso.AdicionarFiltro(Filtros.CriarAlias("emp.GrupoEconomico", "empgrup"));

                if (grupo != null && grupo.Id > 0)
                {
                    diverso.AdicionarFiltro(Filtros.Eq("empgrup.Id", grupo.Id));
                }
            }

            if (descricao != null && descricao != "")
                diverso.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (tipo != null && tipo.Id > 0 && status == null)
            {
                diverso.AdicionarFiltro(Filtros.CriarAlias("TipoDiverso", "tip"));
                diverso.AdicionarFiltro(Filtros.Eq("tip.Id", tipo.Id));
            }

            if (status != null && status.Id > 0)
            {
                diverso.AdicionarFiltro(Filtros.CriarAlias("VencimentosDiversos", "vencdiv"));
                diverso.AdicionarFiltro(Filtros.CriarAlias("vencdiv.StatusDiverso", "statdiv"));
                diverso.AdicionarFiltro(Filtros.Eq("statdiv.Id", status.Id));
            }

            diverso.AdicionarFiltro(Filtros.Count("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            object o = fabrica.GetDAOBase().ConsultarProjecao(diverso);
            if (o == null)
                return 0;
            else
                return Convert.ToInt32(o.ToString());
        }
    }
}
