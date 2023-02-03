using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class CadastroTecnicoFederal : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static CadastroTecnicoFederal ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            CadastroTecnicoFederal classe = new CadastroTecnicoFederal();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<CadastroTecnicoFederal>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual CadastroTecnicoFederal ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<CadastroTecnicoFederal>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual CadastroTecnicoFederal Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<CadastroTecnicoFederal>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual CadastroTecnicoFederal SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<CadastroTecnicoFederal>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<CadastroTecnicoFederal> SalvarTodos(IList<CadastroTecnicoFederal> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<CadastroTecnicoFederal>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<CadastroTecnicoFederal> SalvarTodos(params CadastroTecnicoFederal[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<CadastroTecnicoFederal>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<CadastroTecnicoFederal>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<CadastroTecnicoFederal>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<CadastroTecnicoFederal> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            CadastroTecnicoFederal obj = Activator.CreateInstance<CadastroTecnicoFederal>();
            return fabrica.GetDAOBase().ConsultarTodos<CadastroTecnicoFederal>(obj);
        }

        public static IList<CadastroTecnicoFederal> ConsultarTodosComoObjetos()
        {
            CadastroTecnicoFederal ee = new CadastroTecnicoFederal();

            ee.AdicionarFiltro(Filtros.CriarAlias("Empresa", "Em"));
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Em.Nome"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(ee);            
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<CadastroTecnicoFederal> ConsultarOrdemAcendente(int qtd)
        {
            CadastroTecnicoFederal ee = new CadastroTecnicoFederal();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<CadastroTecnicoFederal> ConsultarOrdemDescendente(int qtd)
        {
            CadastroTecnicoFederal ee = new CadastroTecnicoFederal();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de CadastroTecnicoFederal
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<CadastroTecnicoFederal> Filtrar(int qtd)
        {
            CadastroTecnicoFederal estado = new CadastroTecnicoFederal();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(estado);
        }

        /// <summary>
        /// Retorna o ultimo CadastroTecnicoFederal Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo CadastroTecnicoFederal</returns>
        public virtual CadastroTecnicoFederal UltimoInserido()
        {
            CadastroTecnicoFederal estado = new CadastroTecnicoFederal();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<CadastroTecnicoFederal>(estado);
        }

        /// <summary>
        /// Consulta todos os CadastroTecnicoFederals armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os CadastroTecnicoFederals armazenados ordenados pelo Nome</returns>
        public static IList<CadastroTecnicoFederal> ConsultarTodosOrdemAlfabetica()
        {
            CadastroTecnicoFederal aux = new CadastroTecnicoFederal();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(aux);
        }

        public static CadastroTecnicoFederal ConsultarPorEmpresa(int p)
        {
            CadastroTecnicoFederal org1 = new CadastroTecnicoFederal();
            org1.AdicionarFiltro(Filtros.Eq("Empresa.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return fabrica1.GetDAOBase().ConsultarUnicoComFiltro<CadastroTecnicoFederal>(org1);
        }

        public static IList<CadastroTecnicoFederal> ConsultarPorGrupoEconomico(int p)
        {
            CadastroTecnicoFederal org1 = new CadastroTecnicoFederal();
            org1.AdicionarFiltro(Filtros.CriarAlias("Empresa", "empr"));
            org1.AdicionarFiltro(Filtros.CriarAlias("empr.GrupoEconomico", "grup"));
            org1.AdicionarFiltro(Filtros.Eq("grup.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return fabrica1.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(org1);
        }

        public static IList<CadastroTecnicoFederal> ConsultarPorGrupoEconomicoVerificandoPermissoes(int p, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<CadastroTecnicoFederal>();

            if (tipoConfigPermissao == 'P' && (cadastrosTecnicosPermissoes == null || cadastrosTecnicosPermissoes.Count == 0))
                return new List<CadastroTecnicoFederal>();

            CadastroTecnicoFederal org1 = new CadastroTecnicoFederal();

            if (tipoConfigPermissao == 'P')
            {
                IList<CadastroTecnicoFederal> cadastrosAux = cadastrosTecnicosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == p).ToList();

                if (cadastrosAux == null || cadastrosAux.Count == 0)
                    return new List<CadastroTecnicoFederal>();

                if (cadastrosAux != null && cadastrosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[cadastrosAux.Count];
                    for (int index = 0; index < cadastrosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", cadastrosAux[index].Id);
                    org1.AdicionarFiltro(Filtros.Ou(filtros));
                }
                
            }

            org1.AdicionarFiltro(Filtros.CriarAlias("Empresa", "empr"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == p).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<CadastroTecnicoFederal>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("empr.Id", empresasAux[index].Id);
                    org1.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }                
            }

            org1.AdicionarFiltro(Filtros.CriarAlias("empr.GrupoEconomico", "grup"));
            org1.AdicionarFiltro(Filtros.Eq("grup.Id", p));
            org1.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica1 = new FabricaDAONHibernateBase();
            return fabrica1.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(org1);
        }        

        public virtual Vencimento GetUltimoRelatorio
        {
            get
            {
                if (this.EntregaRelatorioAnual != null && this.EntregaRelatorioAnual.Count > 0)
                    return EntregaRelatorioAnual[EntregaRelatorioAnual.Count - 1];
                return new Vencimento();
            }
        }

        public virtual Vencimento GetUltimoPagamento
        {
            get
            {
                if (this.TaxaTrimestral != null && this.TaxaTrimestral.Count > 0)
                    return TaxaTrimestral[TaxaTrimestral.Count - 1];
                return new Vencimento();
            }
        }

        public virtual Vencimento GetUltimoCertificado
        {
            get
            {
                if (this.CertificadoRegularidade != null && this.CertificadoRegularidade.Count > 0)
                    return this.CertificadoRegularidade[this.CertificadoRegularidade.Count - 1];
                return new Vencimento();
            }
        }
        public virtual Vencimento SetUltimoRelatorio
        {
            set
            {
                if (!(this.EntregaRelatorioAnual != null && this.EntregaRelatorioAnual.Count > 0))
                {
                    this.EntregaRelatorioAnual = new List<Vencimento>();
                    this.EntregaRelatorioAnual.Add(new Vencimento());
                }
                this.EntregaRelatorioAnual[this.EntregaRelatorioAnual.Count - 1] = value;
            }
        }

        public virtual Vencimento SetUltimoPagamento
        {
            set
            {
                if (!(this.TaxaTrimestral != null && this.TaxaTrimestral.Count > 0))
                {
                    this.TaxaTrimestral = new List<Vencimento>();
                    this.TaxaTrimestral.Add(new Vencimento());
                }
                this.TaxaTrimestral[this.TaxaTrimestral.Count - 1] = value;
            }
        }

        public virtual Vencimento SetUltimoCertificado
        {
            set
            {
                if (!(this.CertificadoRegularidade != null && this.CertificadoRegularidade.Count > 0))
                {
                    this.CertificadoRegularidade = new List<Vencimento>();
                    this.CertificadoRegularidade.Add(new Vencimento());
                }
                this.CertificadoRegularidade[this.CertificadoRegularidade.Count - 1] = value;
            }
        }

        public static IList<CadastroTecnicoFederal> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, DateTime dataDeEntregaRelatorioAnual, DateTime dataAteEntregaRelatorioAnual, DateTime dataDeVencimentoTaxaTrimestral, DateTime dataAteVencimentoTaxaTrimestral, DateTime dataDeVencimentoCertificadoRegularidade, DateTime dataAteVencimentoCertificadoRegularidade, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<CadastroTecnicoFederal>();

            if (tipoConfigPermissao == 'P' && (cadastrosTecnicosPermissoes == null || cadastrosTecnicosPermissoes.Count == 0))
                return new List<CadastroTecnicoFederal>();

            CadastroTecnicoFederal aux = new CadastroTecnicoFederal();
            aux.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'P')
            {
                IList<CadastroTecnicoFederal> cadastrosAux = cadastrosTecnicosPermissoes.Where(x => x.ConsultarPorId().Empresa != null && x.ConsultarPorId().Empresa.GrupoEconomico != null && x.ConsultarPorId().Empresa.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (cadastrosAux == null || cadastrosAux.Count == 0)
                    return new List<CadastroTecnicoFederal>();

                if (cadastrosAux != null && cadastrosAux.Count > 0)
                {
                    Filtro[] filtros = new Filtro[cadastrosAux.Count];
                    for (int index = 0; index < cadastrosAux.Count; index++)
                        filtros[index] = Filtros.Eq("Id", cadastrosAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));
                }

            }

            if (tipoConfigPermissao == 'E')
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Empresa", "empr"));

                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<CadastroTecnicoFederal>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("empr.Id", empresasAux[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            aux.AdicionarFiltro(Filtros.CriarAlias("EntregaRelatorioAnual", "entRel"));
            aux.AdicionarFiltro(Filtros.Between("entRel.Data", dataDeEntregaRelatorioAnual, dataAteEntregaRelatorioAnual));

            aux.AdicionarFiltro(Filtros.CriarAlias("TaxaTrimestral", "taxTri"));
            aux.AdicionarFiltro(Filtros.Between("taxTri.Data", dataDeVencimentoTaxaTrimestral, dataAteVencimentoTaxaTrimestral));

            aux.AdicionarFiltro(Filtros.CriarAlias("CertificadoRegularidade", "certReg"));
            aux.AdicionarFiltro(Filtros.Between("certReg.Data", dataDeVencimentoCertificadoRegularidade, dataAteVencimentoCertificadoRegularidade));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<CadastroTecnicoFederal> cadastros = fabrica.GetDAOBase().ConsultarComFiltro<CadastroTecnicoFederal>(aux);
            CadastroTecnicoFederal.RemoverCTFDeOutroGrupoEconomico(cadastros, idGrupoEconomico);
            CadastroTecnicoFederal.RemoverCTFDeOutraEmpresa(cadastros, idEmpresa);

            return cadastros;
        }

        private static void RemoverCTFDeOutraEmpresa(IList<CadastroTecnicoFederal> cadastros, int idEmpresa)
        {
            if (idEmpresa > 0)
                for (int index = cadastros.Count - 1; index >= 0; index--)
                {
                    Empresa aux = cadastros[index].Empresa;
                    if (aux == null || aux.Id != idEmpresa)
                        cadastros.RemoveAt(index);
                }
        }

        private static void RemoverCTFDeOutroGrupoEconomico(IList<CadastroTecnicoFederal> cadastros, int idGrupoEconomico)
        {
            if (idGrupoEconomico > 0)
                for (int index = cadastros.Count - 1; index >= 0; index--)
                {
                    GrupoEconomico aux = cadastros[index].Empresa.GrupoEconomico;
                    if (aux == null || aux.Id != idGrupoEconomico)
                        cadastros.RemoveAt(index);
                }
        }

        public virtual string GetGrupoEconomico 
        {
            get 
            {
                return this.Empresa != null && this.Empresa.GrupoEconomico != null ? this.Empresa.GrupoEconomico.Nome : "Não definido";
            }
        }

        public virtual string GetNumeroLicenca
        {
            get
            {
                if (this.NumeroLicenca != null && this.NumeroLicenca != "") {
                    return this.NumeroLicenca;
                }
                return "";
            }
        }

        public virtual string GetNomeEmpresa 
        {
            get 
            {
                return this.Empresa != null ? this.Empresa.Nome : "Não definido";
            }
        }

        public virtual string GetCNPJEmpresa
        {
            get
            {
                return this.Empresa != null ? this.Empresa.GetNumeroCNPJeCPFComMascara : "Não definido";
            }
        }

        public virtual string GetDataEntregaRelatorioAnual 
        {
            get 
            {
                return this.GetUltimoRelatorio != null ? this.GetUltimoRelatorio.GetDataVencimento : "--";
            }
        }

        public virtual string GetDataVencimentoTaxaTrimestral
        {
            get
            {
                return this.GetUltimoPagamento != null ? this.GetUltimoPagamento.GetDataVencimento : "--";
            }
        }

        public virtual string GetDataVencimentoCertificadoRegularidade
        {
            get
            {
                return this.GetUltimoCertificado != null ? this.GetUltimoCertificado.GetDataVencimento : "--";
            }
        }

        public static IList<CadastroTecnicoFederal> ObterCadastrosTecnicosQueOUsuarioPossuiAcesso(Usuario usuario)
        {
            IList<CadastroTecnicoFederal> retorno = new List<CadastroTecnicoFederal>();

            IList<CadastroTecnicoFederal> cadastros = CadastroTecnicoFederal.ConsultarTodos();
            if (cadastros != null && cadastros.Count > 0)
            {
                foreach (CadastroTecnicoFederal processo in cadastros)
                {
                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || (processo.UsuariosVisualizacao != null && processo.UsuariosVisualizacao.Count > 0 && processo.UsuariosVisualizacao.Contains(usuario)))
                        retorno.Add(processo);
                }
            }

            return retorno;
        }

    }
}
