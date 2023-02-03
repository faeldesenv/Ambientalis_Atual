using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class OutrosEmpresa : Condicional
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OutrosEmpresa ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OutrosEmpresa classe = new OutrosEmpresa();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OutrosEmpresa>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OutrosEmpresa ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OutrosEmpresa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OutrosEmpresa Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OutrosEmpresa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OutrosEmpresa SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OutrosEmpresa>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OutrosEmpresa> SalvarTodos(IList<OutrosEmpresa> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OutrosEmpresa>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OutrosEmpresa> SalvarTodos(params OutrosEmpresa[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OutrosEmpresa>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OutrosEmpresa>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OutrosEmpresa>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OutrosEmpresa> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OutrosEmpresa obj = Activator.CreateInstance<OutrosEmpresa>();
            return fabrica.GetDAOBase().ConsultarTodos<OutrosEmpresa>(obj);
        }

        public static IList<OutrosEmpresa> ConsultarTodosComoObjetos()
        {
            OutrosEmpresa ee = new OutrosEmpresa();

            ee.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Em.Nome"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OutrosEmpresa> ConsultarOrdemAcendente(int qtd)
        {
            OutrosEmpresa ee = new OutrosEmpresa();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OutrosEmpresa> ConsultarOrdemDescendente(int qtd)
        {
            OutrosEmpresa ee = new OutrosEmpresa();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OutrosEmpresa> Filtrar(int qtd)
        {
            OutrosEmpresa estado = new OutrosEmpresa();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual OutrosEmpresa UltimoInserido()
        {
            OutrosEmpresa estado = new OutrosEmpresa();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OutrosEmpresa>(estado);
        }

        /// <summary>
        /// Consulta todos os Ibamas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Ibamas armazenados ordenados pelo Nome</returns>
        public static IList<OutrosEmpresa> ConsultarTodosOrdemAlfabetica()
        {
            OutrosEmpresa aux = new OutrosEmpresa();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(aux);
        }

        public static IList<OutrosEmpresa> ConsultarPorOrgaoEmpresaGrupoEconomico(OrgaoAmbiental org, Empresa empresa, GrupoEconomico grupo)
        {
            OutrosEmpresa ou = new OutrosEmpresa();

            if (empresa != null)
                ou.AdicionarFiltro(Filtros.Eq("Empresa.Id", empresa.Id));
            else
            {
                ou.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
                ou.AdicionarFiltro(Filtros.CriarAlias("Em.GrupoEconomico", "Cli"));
                ou.AdicionarFiltro(Filtros.Eq("Cli.Id", grupo.Id));
            }

            ou.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", org.Id));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(ou);
        }

        public static IList<OutrosEmpresa> ConsultarPorGrupoEconomico(int grupo)
        {
            OutrosEmpresa ou = new OutrosEmpresa();

            ou.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
            ou.AdicionarFiltro(Filtros.CriarAlias("Em.GrupoEconomico", "Cli"));
            ou.AdicionarFiltro(Filtros.Eq("Cli.Id", grupo));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(ou);
        }

        public static IList<OutrosEmpresa> ConsultarPorOrgaoEmpresaGrupoEconomicoVerificandoPermissoes(OrgaoAmbiental org, Empresa empresa, GrupoEconomico grupo, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<OutrosEmpresa> outrosEmpresaPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<OutrosEmpresa>();

            if (tipoConfigPermissao == 'P' && (outrosEmpresaPermissoes == null || outrosEmpresaPermissoes.Count == 0))
                return new List<OutrosEmpresa>();

            OutrosEmpresa ou = new OutrosEmpresa();

            if (tipoConfigPermissao == 'P')
            {
                Filtro[] filtros = new Filtro[outrosEmpresaPermissoes.Count];
                for (int index = 0; index < outrosEmpresaPermissoes.Count; index++)
                    filtros[index] = Filtros.Eq("Id", outrosEmpresaPermissoes[index].Id);
                ou.AdicionarFiltro(Filtros.Ou(filtros));
            }

            if (empresa != null)
                ou.AdicionarFiltro(Filtros.Eq("Empresa.Id", empresa.Id));
            else
            {
                ou.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));

                if (tipoConfigPermissao == 'E')
                {
                    Filtro[] filtrosEmps = new Filtro[empresasPermissoes.Count];
                    for (int index = 0; index < empresasPermissoes.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("Em.Id", empresasPermissoes[index].Id);
                    ou.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }

                ou.AdicionarFiltro(Filtros.CriarAlias("Em.GrupoEconomico", "Cli"));
                ou.AdicionarFiltro(Filtros.Eq("Cli.Id", grupo.Id));
            }

            ou.AdicionarFiltro(Filtros.Eq("OrgaoAmbiental.Id", org.Id));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(ou);
        }

        public static IList<OutrosEmpresa> Filtrar(Empresa em, string numero)
        {
            OutrosEmpresa oe = new OutrosEmpresa();
            oe.AdicionarFiltro(Filtros.Like("Numero", numero));
            if (em != null && em.Id > 0)
                oe.AdicionarFiltro(Filtros.Eq("Empresa.Id", em.Id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OutrosEmpresa>(oe);
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

        public static IList<Condicional> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDeVencimento, DateTime dataAtehVencimento, int idTipo, int periodico, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<OutrosEmpresa> outrosEmpresaPermissoes)
        {
            if (idTipo != Condicional.VencimentoProcesso)
            {
                if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                    return new List<Condicional>();

                if (tipoConfigPermissao == 'P' && (outrosEmpresaPermissoes == null || outrosEmpresaPermissoes.Count == 0))
                    return new List<Condicional>();

                OutrosEmpresa aux = new OutrosEmpresa();
                aux.AdicionarFiltro(Filtros.Distinct());

                if (tipoConfigPermissao == 'P')
                {
                    IList<OutrosEmpresa> outrosAux = outrosEmpresaPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                    if (outrosAux == null || outrosAux.Count == 0)
                        return new List<Condicional>();

                    Filtro[] filtros = new Filtro[outrosAux.Count];
                    for (int index = 0; index < outrosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", outrosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }

                aux.AdicionarFiltro(Filtros.CriarAlias("Vencimentos", "vencs"));

                if (periodico > 0)
                {
                    aux.AdicionarFiltro(Filtros.Eq("vencs.Periodico", periodico == 1));
                }

                if (aux.GetUltimoVencimento.Id > 0)
                    aux.AdicionarFiltro(Filtros.Eq("vencs.Id", aux.GetUltimoVencimento.Id));
                aux.AdicionarFiltro(Filtros.Between("vencs.Data", dataDeVencimento, dataAtehVencimento));

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
                return this.Empresa != null && this.Empresa.GrupoEconomico != null ? this.Empresa.GrupoEconomico : null;
            }
        }

        public override Empresa GetEmpresa
        {
            get
            {
                return this.Empresa != null ? this.Empresa : null;
            }
        }

        public override OrgaoAmbiental GetOrgaoAmbiental
        {
            get
            {
                return this.OrgaoAmbiental;
            }
        }

        public static IList<OutrosEmpresa> ObterOutrosEmpresaQueOUsuarioPossuiAcesso(Usuario usuario)
        {
            IList<OutrosEmpresa> retorno = new List<OutrosEmpresa>();

            IList<OutrosEmpresa> cadastros = OutrosEmpresa.ConsultarTodos();
            if (cadastros != null && cadastros.Count > 0)
            {
                foreach (OutrosEmpresa processo in cadastros)
                {
                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(usuario)))
                        retorno.Add(processo);
                }
            }

            return retorno;
        }
    }
}
