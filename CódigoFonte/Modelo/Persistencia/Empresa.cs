using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Empresa : Pessoa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Empresa ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Empresa classe = new Empresa();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Empresa>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Empresa ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Empresa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Empresa Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Empresa>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Empresa SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Empresa>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Empresa> SalvarTodos(IList<Empresa> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Empresa>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Empresa> SalvarTodos(params Empresa[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Empresa>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Empresa>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Empresa>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Empresa> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Empresa obj = Activator.CreateInstance<Empresa>();
            return fabrica.GetDAOBase().ConsultarTodos<Empresa>(obj);
        }

        public static IList<object> ConsultarTodosComoObjetos()
        {
            Empresa ee = new Empresa();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Empresa> empresas = fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(ee);

            IList<object> retorno = new List<object>();
            foreach (Empresa item in empresas)
            {
                retorno.Add((object)item);
            }
            return retorno;
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Empresa> ConsultarOrdemAcendente(int qtd)
        {
            Empresa ee = new Empresa();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Empresa> ConsultarOrdemDescendente(int qtd)
        {
            Empresa ee = new Empresa();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Empresa
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Empresa> Filtrar(int qtd)
        {
            Empresa estado = new Empresa();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Empresa Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Empresa</returns>
        public virtual Empresa UltimoInserido()
        {
            Empresa estado = new Empresa();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Empresa>(estado);
        }

        /// <summary>
        /// Consulta todos os Empresas armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Empresas armazenados ordenados pelo Nome</returns>
        public static IList<Empresa> ConsultarTodosOrdemAlfabetica()
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(aux);
        }

        public static IList<Empresa> Filtrar(string nome, string site, string responsavel)
        {
            Empresa Empresa = new Empresa();
            if (!string.IsNullOrEmpty(nome))
                Empresa.AdicionarFiltro(Filtros.Like("Nome", nome));
            if (!string.IsNullOrEmpty(responsavel))
                Empresa.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));
            if (!string.IsNullOrEmpty(site))
                Empresa.AdicionarFiltro(Filtros.Like("Site", site));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(Empresa);
        }

        public static IList<Empresa> Filtrar(string nome, string responsavel, int status, string cpfCnpj, Cidade cidade, GrupoEconomico grupo, Estado estado)
        {
            Empresa empr = new Empresa();
            empr.AdicionarFiltro(Filtros.Distinct());
            empr.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            empr.AdicionarFiltro(Filtros.Like("Nome", nome));
            empr.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));

            if (status > 0)
                empr.AdicionarFiltro(Filtros.Eq("Ativo", status == 1 ? true : false));

            empr.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
            empr.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cnpj", cpfCnpj), Filtros.Like("dp.Cpf", cpfCnpj)));

            if (cidade != null && cidade.Id > 0)
            {
                empr.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                empr.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                empr.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                empr.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                empr.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                empr.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                empr.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            if (grupo != null && grupo.Id > 0)
            {
                empr.AdicionarFiltro(Filtros.CriarAlias("GrupoEconomico", "grpEco"));
                empr.AdicionarFiltro(Filtros.Eq("grpEco.Id", grupo.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(empr);
        }

        public static bool ExisteCNPJ(Empresa empresa, string cnpj)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", empresa != null ? empresa.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cnpj", cnpj));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Empresa> empresas = fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(aux);
            return empresas != null && empresas.Count > 0;
        }

        public static bool ExisteCPF(Empresa empresa, string cpf)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", empresa != null ? empresa.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cpf", cpf));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Empresa> empresas = fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(aux);
            return empresas != null && empresas.Count > 0;
        }

        public static IList<Empresa> FiltrarRelatorio(int idGrupoEconomico)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.Distinct());
            if (idGrupoEconomico > 0)
                aux.AdicionarFiltro(Filtros.Eq("GrupoEconomico.Id", idGrupoEconomico));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(aux);
        }

        public static IList<Empresa> FiltrarRelatorioStatus(int idGrupoEconomico, String status)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.Distinct());
            if (idGrupoEconomico > 0)
                aux.AdicionarFiltro(Filtros.Eq("GrupoEconomico.Id", idGrupoEconomico));

            if (status == "Ativo")
                aux.AdicionarFiltro(Filtros.Eq("Ativo", true));

            if (status == "Inativo")
                aux.AdicionarFiltro(Filtros.Eq("Ativo", false));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(aux);
        }

        public static IList<Empresa> GetEmpresaDoGrupoEconomico(int idGrupoEconomico)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.Distinct());            
            aux.AdicionarFiltro(Filtros.Eq("GrupoEconomico.Id", idGrupoEconomico));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Empresa>(aux);
        }

        public virtual string ObterCnpjCpf(DadosPessoa dados)
        {
            string CnpjCpf = "";
            CnpjCpf = dados.GetType() == typeof(DadosJuridica) ? ((DadosJuridica)dados).Cnpj : ((DadosFisica)dados).Cpf;
            return CnpjCpf;
        }

        public virtual String GetEnderecoCompleto
        {
            get
            {
                if (this.Endereco != null)
                {
                    string retorno = "";
                    if (this.Endereco.Rua != null && this.Endereco.Rua != "")
                    {
                        retorno = retorno + this.Endereco.Rua;
                    }
                    if (this.Endereco.Numero != null && this.Endereco.Numero != "")
                    {
                        retorno = retorno + ", N°. " + this.Endereco.Numero;
                    }
                    if (this.Endereco.Bairro != null && this.Endereco.Bairro != "")
                    {
                        retorno = retorno + ", " + this.Endereco.Bairro + "; ";
                    }
                    
                    if (this.Endereco.Cep != null && this.Endereco.Cep != "")
                    {
                        retorno = retorno + " Cep: " + this.Endereco.Cep + "; ";
                    }
                    if (this.Endereco.PontoReferencia != null && this.Endereco.PontoReferencia != "")
                    {
                        retorno = retorno + " Referência: " + this.Endereco.PontoReferencia + "; ";
                    }
                    if (this.Endereco.Complemento != null && this.Endereco.Complemento != "")
                    {
                        retorno = retorno + " Complemento: " + this.Endereco.Complemento + ".";
                    }

                    return retorno;
                }
                return "";

            }
        }

        public virtual String GetEnderecoEstado
        {
            get
            {
                if (this.Endereco != null && this.Endereco.Cidade != null && this.Endereco.Cidade.Estado != null && this.Endereco.Cidade.Estado.Nome != null && this.Endereco.Cidade.Estado.Nome != "")
                {
                    return this.Endereco.Cidade.Estado.Nome;
                }
                return "";

            }
        }

        public virtual String GetEnderecoCidade
        {
            get
            {
                if (this.Endereco != null && this.Endereco.Cidade != null && this.Endereco.Cidade.Nome != null && this.Endereco.Cidade.Nome != "")
                {
                    return this.Endereco.Cidade.Nome;
                }
                return "";

            }
        }

        public virtual String GetNumeroCNPJeCPFComMascara
        {
            get
            {
                if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosJuridica)) 
                {
                    string numeroCnpj = ((DadosJuridica)this.DadosPessoa).Cnpj;
                    return numeroCnpj.Substring(0, 2) + "." + numeroCnpj.Substring(2, 3) + "." +numeroCnpj.Substring(5, 3) + "/" + numeroCnpj.Substring(8, 4) + "-" + numeroCnpj.Substring(12, 2); 
                }
                else if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosFisica))
                {
                    string numeroCpf = ((DadosFisica)this.DadosPessoa).Cpf;
                    return numeroCpf.Substring(0, 3) + "." + numeroCpf.Substring(3, 3) + "." + numeroCpf.Substring(6, 3) + "-" + numeroCpf.Substring(9, 2);           
                }
                return "";
                
            }
        }

        public virtual String GetAtivo
        {
            get
            {
                if (this.Ativo == true)
                {
                    return "Ativo";
                }
                else {
                    return "Inativo";
                }

            }
        }

        public virtual String GetNomeGrupoEconomico
        {
            get
            {                
                return this.GrupoEconomico != null ? this.GrupoEconomico.Nome : "";
                
            }
        }        

        public static IList<Pessoa> ConsultarTodosComoPessoas()
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pessoa>((Pessoa)aux);
        }

        internal static Empresa ConsultarPorCpfCnpj(string CpfCnpj)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Cpf", CpfCnpj), Filtros.Eq("Cnpj", CpfCnpj)));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Empresa>(aux);
        }

        public static IList<Empresa> ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(ModuloPermissao moduloDiversos, Usuario usuario)
        {
            IList<Empresa> retorno = new List<Empresa>();

            IList<Empresa> empresas = Empresa.ConsultarTodos();

            if (empresas == null || empresas.Count == 0)
                return retorno;

            foreach (Empresa empresa in empresas)
            {
                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDiversos.Id);

                if (empresaPermissao != null)
                {
                    if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || (empresaPermissao.UsuariosVisualizacao != null && empresaPermissao.UsuariosVisualizacao.Count > 0 && empresaPermissao.UsuariosVisualizacao.Contains(usuario)))
                        retorno.Add(empresa);
                }
            }

            return retorno;
        }

        public static IList<Empresa> ObterEmpresasQueOUsuarioPodeEditarDoModulo(ModuloPermissao moduloDiversos, Usuario usuario)
        {
            IList<Empresa> retorno = new List<Empresa>();

            IList<Empresa> empresas = Empresa.ConsultarTodos();

            if (empresas == null || empresas.Count == 0)
                return retorno;

            foreach (Empresa empresa in empresas)
            {
                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDiversos.Id);

                if (empresaPermissao != null)
                {
                    if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(usuario))
                        retorno.Add(empresa);
                }
            }

            return retorno;
        }

        public static Empresa ConsultarPorCNPJCPF(string cpfCnpj)
        {
            Empresa aux = new Empresa();
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Cpf", cpfCnpj), Filtros.Eq("Cnpj", cpfCnpj)));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Empresa>(aux);
        }
    }
}
