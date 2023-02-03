using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class ContratoDiverso : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ContratoDiverso ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ContratoDiverso classe = new ContratoDiverso();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ContratoDiverso>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ContratoDiverso ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ContratoDiverso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ContratoDiverso Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ContratoDiverso>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ContratoDiverso SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ContratoDiverso>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ContratoDiverso> SalvarTodos(IList<ContratoDiverso> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ContratoDiverso>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ContratoDiverso> SalvarTodos(params ContratoDiverso[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ContratoDiverso>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ContratoDiverso>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ContratoDiverso>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ContratoDiverso> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ContratoDiverso obj = Activator.CreateInstance<ContratoDiverso>();
            return fabrica.GetDAOBase().ConsultarTodos<ContratoDiverso>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ContratoDiverso> ConsultarOrdemAcendente(int qtd)
        {
            ContratoDiverso ee = new ContratoDiverso();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ContratoDiverso> ConsultarOrdemDescendente(int qtd)
        {
            ContratoDiverso ee = new ContratoDiverso();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de ContratoDiverso
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ContratoDiverso> Filtrar(int qtd)
        {
            ContratoDiverso estado = new ContratoDiverso();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(estado);
        }

        public virtual VencimentoContratoDiverso GetUltimoVencimento
        {
            get
            {
                if (this.VencimentosContratosDiversos != null && this.VencimentosContratosDiversos.Count > 0)
                {
                    return this.VencimentosContratosDiversos[this.VencimentosContratosDiversos.Count - 1];
                }
                return null;
            }
        }

        public virtual VencimentoContratoDiverso GetUltimoVencimentoReajustes
        {
            get
            {
                if (this.Reajustes != null && this.Reajustes.Count > 0)
                {
                    return this.Reajustes[this.Reajustes.Count - 1];
                }
                return null;
            }
        }

        public static IList<ContratoDiverso> Consultar(int idGrupoEconomico, int idEmpresa, int como, string fornecedorCliente, int idStatus, string numeroContrato, char tipoConfigPermissao, IList<Empresa> empresasPermissao, IList<Setor> setoresPermissao, int valorInicial, int valorFinal)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissao == null || empresasPermissao.Count == 0))
                return new List<ContratoDiverso>();

            if (tipoConfigPermissao == 'S' && (setoresPermissao == null || setoresPermissao.Count == 0))
                return new List<ContratoDiverso>();

            ContratoDiverso contrato = new ContratoDiverso();

            if (idEmpresa > 0)
                contrato.AdicionarFiltro(Filtros.Eq("Empresa.Id", idEmpresa));
            else 
            {
                if (idGrupoEconomico > 0 || (empresasPermissao != null && empresasPermissao.Count > 0)) 
                {
                    contrato.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

                    if (tipoConfigPermissao == 'E')
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasPermissao.Count];
                        for (int index = 0; index < empresasPermissao.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("E.Id", empresasPermissao[index].Id);
                        contrato.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }

                    if (idGrupoEconomico > 0)
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
                        contrato.AdicionarFiltro(Filtros.Eq("C.Id", idGrupoEconomico));
                    }
                }
                
            }                

            if (tipoConfigPermissao == 'S')
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                Filtro[] filtros = new Filtro[setoresPermissao.Count];
                for (int index = 0; index < setoresPermissao.Count; index++)
                    filtros[index] = Filtros.Eq("set.Id", setoresPermissao[index].Id);
                contrato.AdicionarFiltro(Filtros.Ou(filtros));
            }

            if (idStatus > 0)
                contrato.AdicionarFiltro(Filtros.Eq("StatusContratoDiverso.Id", idStatus));

            if (como > 0)
            {
                if (como == 1)
                {
                    contrato.AdicionarFiltro(Filtros.Like("Como", "Contratante"));
                    if (fornecedorCliente != null && fornecedorCliente != "")
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("Fornecedor", "forn"));
                        contrato.AdicionarFiltro(Filtros.Like("forn.Nome", fornecedorCliente));
                    }
                }
                else
                {
                    contrato.AdicionarFiltro(Filtros.Like("Como", "Contratada"));
                    if (fornecedorCliente != null && fornecedorCliente != "")
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("Cliente", "cli"));
                        contrato.AdicionarFiltro(Filtros.Like("cli.Nome", fornecedorCliente));
                    }
                }
            }

            contrato.AdicionarFiltro(Filtros.Like("Numero", numeroContrato));

            contrato.AdicionarFiltro(Filtros.FaixaResultado(valorInicial, valorFinal));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(contrato);
        }

        public static IList<ContratoDiverso> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, int como, int idFornecedor, int idCliente, int idStatusContratoDiverso, DateTime dataVencimentoDe, DateTime dataVencimentoAte, DateTime dataReajusteDe, DateTime dataReajusteAte, int idCentroCusto, int idIndiceFinanceiro, int idSetor, int idFormaPagamento, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Setor> setoresPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ContratoDiverso>();

            if (tipoConfigPermissao == 'S' && (setoresPermissoes == null || setoresPermissoes.Count == 0))
                return new List<ContratoDiverso>();

            ContratoDiverso contrato = new ContratoDiverso();

            if (idEmpresa > 0)
                contrato.AdicionarFiltro(Filtros.Eq("Empresa.Id", idEmpresa));
            else 
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

                if (tipoConfigPermissao == 'E')
                {
                    IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                    if (empresasAux == null || empresasAux.Count == 0)
                        return new List<ContratoDiverso>();

                    if (empresasAux != null && empresasAux.Count > 0)
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                        for (int index = 0; index < empresasAux.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                        contrato.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }
                }

                if (idGrupoEconomico > 0)
                {
                    contrato.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
                    contrato.AdicionarFiltro(Filtros.Eq("C.Id", idGrupoEconomico));
                }
            }
                

            if (idStatusContratoDiverso > 0)
                contrato.AdicionarFiltro(Filtros.Eq("StatusContratoDiverso.Id", idStatusContratoDiverso));

            if (como > 0)
            {
                if (como == 1)
                {
                    contrato.AdicionarFiltro(Filtros.Like("Como", "Contratante"));
                    if (idFornecedor > 0)
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("Fornecedor", "forn"));
                        contrato.AdicionarFiltro(Filtros.Eq("forn.Id", idFornecedor));
                    }
                }
                else
                {
                    contrato.AdicionarFiltro(Filtros.Like("Como", "Contratada"));
                    if (idCliente > 0)
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("Cliente", "cli"));
                        contrato.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
                    }
                }
            }

            contrato.AdicionarFiltro(Filtros.CriarAlias("VencimentosContratosDiversos", "venc"));
            contrato.AdicionarFiltro(Filtros.Between("venc.Data", dataVencimentoDe, dataVencimentoAte));

            contrato.AdicionarFiltro(Filtros.CriarAlias("Reajustes", "reaj"));
            contrato.AdicionarFiltro(Filtros.Between("reaj.Data", dataReajusteDe, dataReajusteAte));

            if (idCentroCusto > 0)
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("CentroCusto", "custo"));
                contrato.AdicionarFiltro(Filtros.Eq("custo.Id", idCentroCusto));
            }

            if (idIndiceFinanceiro > 0)
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("IndiceFinanceiro", "indice"));
                contrato.AdicionarFiltro(Filtros.Eq("indice.Id", idIndiceFinanceiro));
            }

            if (idSetor > 0)
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("Setor", "setor"));
                contrato.AdicionarFiltro(Filtros.Eq("setor.Id", idSetor));
            }
            else 
            {
                if (tipoConfigPermissao == 'S')
                {
                    contrato.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));                    

                    if (setoresPermissoes != null && setoresPermissoes.Count > 0)
                    {
                        Filtro[] filtros = new Filtro[setoresPermissoes.Count];
                        for (int index = 0; index < setoresPermissoes.Count; index++)
                            filtros[index] = Filtros.Eq("set.Id", setoresPermissoes[index].Id);
                        contrato.AdicionarFiltro(Filtros.Ou(filtros));
                    }
                }                
                
            }

            if (idFormaPagamento > 0)
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("FormaRecebimento", "form"));
                contrato.AdicionarFiltro(Filtros.Eq("form.Id", idFormaPagamento));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(contrato);
        }

        public virtual IList<AditivoContrato> GetAditivosComProrrogacaoVencimento
        {
            get
            {
                IList<AditivoContrato> lista = new List<AditivoContrato>();
                foreach (AditivoContrato aditivo in this.AditivosContratos)
                {
                    if (aditivo.ProrrogouVencimento)
                        lista.Add(aditivo);
                }
                return lista;
            }
        }

        public virtual IList<AditivoContrato> GetAditivosComProrrogacaoReajuste
        {
            get
            {
                IList<AditivoContrato> lista = new List<AditivoContrato>();
                foreach (AditivoContrato aditivo in this.AditivosContratos)
                {
                    if (aditivo.ProrrogouReajuste)
                        lista.Add(aditivo);
                }
                return lista;
            }
        }

        public static IList<ContratoDiverso> ConsultarPorNumeroEObjeto(int idGrupoEconomico, string numero, string objeto, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Setor> setoresPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ContratoDiverso>();

            if (tipoConfigPermissao == 'S' && (setoresPermissoes == null || setoresPermissoes.Count == 0))
                return new List<ContratoDiverso>();

            ContratoDiverso contrato = new ContratoDiverso();
            contrato.AdicionarFiltro(Filtros.Distinct());

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<ContratoDiverso>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                    contrato.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            if (numero != "")
                contrato.AdicionarFiltro(Filtros.Like("Numero", numero));

            if (objeto != "")
                contrato.AdicionarFiltro(Filtros.Like("Objeto", objeto));            

            contrato.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));

            if (tipoConfigPermissao == 'S')
            {  
                if (setoresPermissoes != null && setoresPermissoes.Count > 0)
                {
                    Filtro[] filtros = new Filtro[setoresPermissoes.Count];
                    for (int index = 0; index < setoresPermissoes.Count; index++)
                        filtros[index] = Filtros.Eq("set.Id", setoresPermissoes[index].Id);
                    contrato.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(contrato);
        }

        public virtual ContratoDiverso GetUltimoContrato
        {
            get
            {
                if (this.Renovacoes != null && this.Renovacoes.Count > 0)
                {
                    return this.Renovacoes[this.Renovacoes.Count - 1];
                }
                return this;
            }
        }

        public static IList<ContratoDiverso> FiltrarRelatorioContratosPorProcessos(int idGrupoEconomico, string numero, string objeto, int idStatusContratoDiverso, char tipoConfigPermissao, IList<Empresa> empresasPermissoes, IList<Setor> setoresPermissoes)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissoes == null || empresasPermissoes.Count == 0))
                return new List<ContratoDiverso>();

            if (tipoConfigPermissao == 'S' && (setoresPermissoes == null || setoresPermissoes.Count == 0))
                return new List<ContratoDiverso>();

            ContratoDiverso contrato = new ContratoDiverso();
            contrato.AdicionarFiltro(Filtros.Distinct());

            contrato.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

            if (tipoConfigPermissao == 'E')
            {
                IList<Empresa> empresasAux = empresasPermissoes.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == idGrupoEconomico).ToList();

                if (empresasAux == null || empresasAux.Count == 0)
                    return new List<ContratoDiverso>();

                if (empresasAux != null && empresasAux.Count > 0)
                {
                    Filtro[] filtrosEmps = new Filtro[empresasAux.Count];
                    for (int index = 0; index < empresasAux.Count; index++)
                        filtrosEmps[index] = Filtros.Eq("E.Id", empresasAux[index].Id);
                    contrato.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                }
            }

            if (idGrupoEconomico > 0) 
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "grup"));
                contrato.AdicionarFiltro(Filtros.Eq("grup.Id", idGrupoEconomico));
            }

            if (numero != "")
                contrato.AdicionarFiltro(Filtros.Like("Numero", numero));

            if (objeto != "")
                contrato.AdicionarFiltro(Filtros.Like("Objeto", objeto));

            if (idStatusContratoDiverso > 0)
                contrato.AdicionarFiltro(Filtros.Eq("StatusContratoDiverso.Id", idStatusContratoDiverso));

            contrato.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));

            if (tipoConfigPermissao == 'S')
            {
                if (setoresPermissoes != null && setoresPermissoes.Count > 0)
                {
                    Filtro[] filtros = new Filtro[setoresPermissoes.Count];
                    for (int index = 0; index < setoresPermissoes.Count; index++)
                        filtros[index] = Filtros.Eq("set.Id", setoresPermissoes[index].Id);
                    contrato.AdicionarFiltro(Filtros.Ou(filtros));
                }
            }            

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoDiverso>(contrato);
        }

        public virtual string GetGrupoEconomico
        {
            get
            {                
                return this.Empresa != null && this.Empresa.GrupoEconomico != null ? this.Empresa.GrupoEconomico.Nome : "";
            }
        }

        public virtual string GetNomeEmpresa
        {
            get
            {                
                return this.Empresa != null ? this.Empresa.Nome : "";
            }
        }

        public virtual string GetFornecedorCliente
        {
            get
            {                
                return this.Como == "Contratada" ? this.Cliente != null ? this.Cliente.Nome : "" : this.Fornecedor != null ? this.Fornecedor.Nome : "";
            }
        }

        public virtual string GetDescricaoStatus
        {
            get
            {                
                return this.StatusContratoDiverso != null ? this.StatusContratoDiverso.Nome : "";
            }
        }

        public virtual string GetFormaPagamento
        {
            get
            {                
                return this.FormaRecebimento != null ? this.FormaRecebimento.Nome : "";
            }
        }

        public virtual string GetDataAbertura
        {
            get
            {                
                return this.DataAbertura.ToShortDateString();
            }
        }

        public virtual string GetDataVencimento
        {
            get
            {                
                return this.GetUltimoVencimento != null ? this.GetUltimoVencimento.Data.ToShortDateString() : "";
            }
        }

        public virtual string GetDataReajuste
        {
            get
            {
                return this.GetUltimoVencimentoReajustes != null ? this.GetUltimoVencimentoReajustes.Data.ToShortDateString() : "";
            }
        }

        //public virtual string GetDescricaoProcesso
        //{
        //    get
        //    {
        //        if(this.Processos )
        //        return this.GetUltimoVencimentoReajustes != null ? this.GetUltimoVencimentoReajustes.Data.ToShortDateString() : "";
        //    }
        //}            


        public static int FiltrarContador(int idGrupoEconomico, int idEmpresa, int como, string fornecedorCliente, int idStatus, string numeroContrato, char tipoConfigPermissao, IList<Empresa> empresasPermissao, IList<Setor> setoresPermissao)
        {
            if (tipoConfigPermissao == 'E' && (empresasPermissao == null || empresasPermissao.Count == 0))
                return 0;

            if (tipoConfigPermissao == 'S' && (setoresPermissao == null || setoresPermissao.Count == 0))
                return 0;

            ContratoDiverso contrato = new ContratoDiverso();

            if (idEmpresa > 0)
                contrato.AdicionarFiltro(Filtros.Eq("Empresa.Id", idEmpresa));
            else
            {
                if (idGrupoEconomico > 0 || (empresasPermissao != null && empresasPermissao.Count > 0))
                {
                    contrato.AdicionarFiltro(Filtros.CriarAlias("Empresa", "E"));

                    if (tipoConfigPermissao == 'E')
                    {
                        Filtro[] filtrosEmps = new Filtro[empresasPermissao.Count];
                        for (int index = 0; index < empresasPermissao.Count; index++)
                            filtrosEmps[index] = Filtros.Eq("E.Id", empresasPermissao[index].Id);
                        contrato.AdicionarFiltro(Filtros.Ou(filtrosEmps));
                    }

                    if (idGrupoEconomico > 0)
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("E.GrupoEconomico", "C"));
                        contrato.AdicionarFiltro(Filtros.Eq("C.Id", idGrupoEconomico));
                    }
                }

            }

            if (tipoConfigPermissao == 'S')
            {
                contrato.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                Filtro[] filtros = new Filtro[setoresPermissao.Count];
                for (int index = 0; index < setoresPermissao.Count; index++)
                    filtros[index] = Filtros.Eq("set.Id", setoresPermissao[index].Id);
                contrato.AdicionarFiltro(Filtros.Ou(filtros));
            }

            if (idStatus > 0)
                contrato.AdicionarFiltro(Filtros.Eq("StatusContratoDiverso.Id", idStatus));

            if (como > 0)
            {
                if (como == 1)
                {
                    contrato.AdicionarFiltro(Filtros.Like("Como", "Contratante"));
                    if (fornecedorCliente != null && fornecedorCliente != "")
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("Fornecedor", "forn"));
                        contrato.AdicionarFiltro(Filtros.Like("forn.Nome", fornecedorCliente));
                    }
                }
                else
                {
                    contrato.AdicionarFiltro(Filtros.Like("Como", "Contratada"));
                    if (fornecedorCliente != null && fornecedorCliente != "")
                    {
                        contrato.AdicionarFiltro(Filtros.CriarAlias("Cliente", "cli"));
                        contrato.AdicionarFiltro(Filtros.Like("cli.Nome", fornecedorCliente));
                    }
                }
            }

            contrato.AdicionarFiltro(Filtros.Like("Numero", numeroContrato));

            contrato.AdicionarFiltro(Filtros.Count("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            object o = fabrica.GetDAOBase().ConsultarProjecao(contrato);
            if (o == null)
                return 0;
            else
                return Convert.ToInt32(o.ToString());
        }
    }
}

