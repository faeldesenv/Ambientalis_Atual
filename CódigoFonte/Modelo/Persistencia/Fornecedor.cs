using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Fornecedor : Pessoa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Fornecedor ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Fornecedor classe = new Fornecedor();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Fornecedor>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Fornecedor ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Fornecedor>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Fornecedor Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Fornecedor>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Fornecedor SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Fornecedor>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Fornecedor> SalvarTodos(IList<Fornecedor> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Fornecedor>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Fornecedor> SalvarTodos(params Fornecedor[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Fornecedor>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Fornecedor>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Fornecedor>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Fornecedor> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Fornecedor obj = Activator.CreateInstance<Fornecedor>();
            return fabrica.GetDAOBase().ConsultarTodos<Fornecedor>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Fornecedor> ConsultarOrdemAcendente(int qtd)
        {
            Fornecedor ee = new Fornecedor();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Fornecedor> ConsultarOrdemDescendente(int qtd)
        {
            Fornecedor ee = new Fornecedor();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Empresa
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Fornecedor> Filtrar(int qtd)
        {
            Fornecedor estado = new Fornecedor();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Empresa Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Empresa</returns>
        public virtual Fornecedor UltimoInserido()
        {
            Fornecedor estado = new Fornecedor();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Fornecedor>(estado);
        }

        /// <summary>
        /// Consulta todos os Empresas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Empresas armazenados ordenados pelo Nome</returns>
        public static IList<Fornecedor> ConsultarTodosOrdemAlfabetica()
        {
            Fornecedor aux = new Fornecedor();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(aux);
        }

        public static bool ExisteCNPJ(Fornecedor auxiliar, string cnpj)
        {
            Fornecedor aux = new Fornecedor();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", auxiliar != null ? auxiliar.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cnpj", cnpj));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Fornecedor> fornecedores = fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(aux);
            return fornecedores != null && fornecedores.Count > 0;
        }


        public static bool ExisteCPF(Fornecedor auxiliar, string cpf)
        {
            Fornecedor aux = new Fornecedor();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", auxiliar != null ? auxiliar.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cpf", cpf));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Fornecedor> fornecedores = fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(aux);
            return fornecedores != null && fornecedores.Count > 0;
        }

        public static IList<Fornecedor> Filtrar(string NomeRazaoSocial, string CNPJCPF, int status, Estado estado, Cidade cidade)
        {
            Fornecedor fornecedor = new Fornecedor();

            if (status > 0)
                fornecedor.AdicionarFiltro(Filtros.Eq("Ativo", status == 1 ? true : false));

            fornecedor.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
            fornecedor.AdicionarFiltro(Filtros.Ou(Filtros.Like("Nome", NomeRazaoSocial), Filtros.Like("dp.RazaoSocial", NomeRazaoSocial)));
            fornecedor.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cnpj", CNPJCPF), Filtros.Like("dp.Cpf", CNPJCPF)));

            if (cidade != null && cidade.Id > 0)
            {
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                fornecedor.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                fornecedor.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(fornecedor);
        }

        public virtual String GetNumeroCNPJeCPFComMascara
        {
            get
            {
                if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    string numeroCnpj = ((DadosJuridica)this.DadosPessoa).Cnpj;
                    return numeroCnpj.Substring(0, 2) + "." + numeroCnpj.Substring(2, 3) + "." + numeroCnpj.Substring(5, 3) + "/" + numeroCnpj.Substring(8, 4) + "-" + numeroCnpj.Substring(12, 2);
                }
                else if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosFisica))
                {
                    string numeroCpf = ((DadosFisica)this.DadosPessoa).Cpf;
                    return numeroCpf.Substring(0, 3) + "." + numeroCpf.Substring(3, 3) + "." + numeroCpf.Substring(6, 3) + "-" + numeroCpf.Substring(9, 2);
                }
                return "";

            }
        }

        public static IList<Fornecedor> FiltrarRelatorio(string nomeRazao, string CnpjCpf, int idStatus, int idEstado, int idCidade, int idAtividade)
        {
            Fornecedor fornecedor = new Fornecedor();

            if (idStatus > 0)
                fornecedor.AdicionarFiltro(Filtros.Eq("Ativo", idStatus == 1 ? true : false));

            fornecedor.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
            fornecedor.AdicionarFiltro(Filtros.Ou(Filtros.Like("Nome", nomeRazao), Filtros.Like("dp.RazaoSocial", nomeRazao)));
            fornecedor.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cnpj", CnpjCpf), Filtros.Like("dp.Cpf", CnpjCpf)));

            if (idCidade > 0)
            {
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                fornecedor.AdicionarFiltro(Filtros.Eq("cid.Id", idCidade));
            }

            if (idEstado > 0 && idCidade <= 0)
            {
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                fornecedor.AdicionarFiltro(Filtros.Eq("es.Id", idEstado));
            }

            if (idAtividade > 0)
            {
                fornecedor.AdicionarFiltro(Filtros.CriarAlias("Atividade", "atv"));
                fornecedor.AdicionarFiltro(Filtros.Eq("atv.Id", idAtividade));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(fornecedor);
        }

        public static IList<Fornecedor> ConsultarTodosAtivos()
        {
            Fornecedor aux = new Fornecedor();
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Fornecedor> retorno = fabrica.GetDAOBase().ConsultarComFiltro<Fornecedor>(aux);
            return retorno;
        }

        public virtual string GetNomeRazaoSocial
        {
            get
            {
                if (this.DadosPessoa != null)
                    return this.DadosPessoa.GetType() == typeof(DadosFisica) ? this.Nome : ((DadosJuridica)this.DadosPessoa).RazaoSocial;

                return "--";
            }
        }

        public virtual string GetDescricaoStatus
        {
            get
            {
                return this.Ativo ? "ATIVO" : "INATIVO";
            }
        }

        public virtual string GetDescricaoAtividade
        {
            get
            {
                return this.Atividade != null ? this.Atividade.Nome : "";
            }
        }

        public virtual string GetCidade
        {
            get
            {
                return this.Endereco != null && this.Endereco.Cidade != null ? this.Endereco.Cidade.Nome : "";
            }
        }

        public virtual string GetSiglaEstado
        {
            get
            {
                return this.Endereco != null && this.Endereco.Cidade != null && this.Endereco.Cidade.Estado != null ? this.Endereco.Cidade.Estado.PegarSiglaEstado() : "";
            }
        }            
    }
}
