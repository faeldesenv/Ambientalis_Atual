using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Prospecto : PessoaComercial
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Prospecto ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Prospecto classe = new Prospecto();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Prospecto>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Prospecto ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Prospecto>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Prospecto Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Prospecto>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Prospecto SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Prospecto>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Prospecto> SalvarTodos(IList<Prospecto> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Prospecto>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Prospecto> SalvarTodos(params Prospecto[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Prospecto>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Prospecto>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Prospecto>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Prospecto> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Prospecto obj = Activator.CreateInstance<Prospecto>();
            return fabrica.GetDAOBase().ConsultarTodos<Prospecto>(obj);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Prospecto> Filtrar(int qtd)
        {
            Prospecto cidade = new Prospecto();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Prospecto>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual Prospecto UltimoInserido()
        {
            Prospecto ArquivoFisico = new Prospecto();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Prospecto>(ArquivoFisico);
        }


        public static bool ExisteCNPJ(Prospecto auxiliar, string cnpj)
        {
            Prospecto aux = new Prospecto();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", auxiliar != null ? auxiliar.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cnpj", cnpj));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Prospecto> revendas = fabrica.GetDAOBase().ConsultarComFiltro<Prospecto>(aux);
            return revendas != null && revendas.Count > 0;
        }


        public static bool ExisteCPF(Prospecto auxiliar, string cpf)
        {
            Prospecto aux = new Prospecto();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", auxiliar != null ? auxiliar.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cpf", cpf));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Prospecto> revendas = fabrica.GetDAOBase().ConsultarComFiltro<Prospecto>(aux);
            return revendas != null && revendas.Count > 0;
        }

        public static IList<Prospecto> Filtrar(Revenda revenda, string nome, string cpfCnpj, string responsavel, Estado estado, Cidade cidade, string status, DateTime dateDe, DateTime dateAte)
        {
            Prospecto prospecto = new Prospecto();
            if (revenda != null)
                prospecto.AdicionarFiltro(Filtros.Eq("Revenda", revenda));
            prospecto.AdicionarFiltro(Filtros.Like("Nome", nome.Trim()));
            prospecto.AdicionarFiltro(Filtros.Like("Responsavel", responsavel.Trim()));

            if (!String.IsNullOrEmpty(cpfCnpj))
            {
                prospecto.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
                prospecto.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cpf", cpfCnpj.Trim()), Filtros.Like("dp.Cnpj", cpfCnpj.Trim())));
            }

            if (cidade != null && cidade.Id > 0)
            {
                prospecto.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                prospecto.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                prospecto.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                prospecto.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                prospecto.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                prospecto.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                prospecto.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            if (status != "-- Todos --")
                prospecto.AdicionarFiltro(Filtros.Eq("Ativo", (status == "Ativo")));

            prospecto.AdicionarFiltro(Filtros.Between("DataCadastro", dateDe, dateAte));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Prospecto>(prospecto);
        }

        public virtual String GetNumeroCNPJeCPFComMascara
        {
            get
            {
                if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosJuridicaComercial))
                {
                    string numeroCnpj = ((DadosJuridicaComercial)this.DadosPessoa).Cnpj;
                    return numeroCnpj.Substring(0, 2) + "." + numeroCnpj.Substring(2, 3) + "." + numeroCnpj.Substring(5, 3) + "/" + numeroCnpj.Substring(8, 4) + "-" + numeroCnpj.Substring(12, 2);
                }
                else if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosFisicaComercial))
                {
                    string numeroCpf = ((DadosFisicaComercial)this.DadosPessoa).Cpf;
                    return numeroCpf.Substring(0, 3) + "." + numeroCpf.Substring(3, 3) + "." + numeroCpf.Substring(6, 3) + "-" + numeroCpf.Substring(9, 2); 
                }
                return "";

            }
        }

        public static Prospecto ConsultarCPFouCNPJ(string x)
        {
            Prospecto aux = new Prospecto();
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Cpf", x), Filtros.Eq("Cnpj", x)));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Prospecto>(aux);
        }

        public static IList<Prospecto> FiltrarRelatorio(Revenda prospectorevenda, Cidade cidade, Estado estado, DateTime dataDe, DateTime dataAte, int ativo) //ativo 1 para true e 2 para false
        {
            Prospecto prospecto = new Prospecto();

            if (dataDe != SqlDate.MinValue || dataAte != SqlDate.MaxValue)
                prospecto.AdicionarFiltro(Filtros.Between("DataCadastro", dataDe, dataAte));            
                
            if (ativo > 0)
                prospecto.AdicionarFiltro(Filtros.Eq("Ativo", ativo == 1));

            if (prospectorevenda != null && prospectorevenda.Id > 0)
            {
                prospecto.AdicionarFiltro(Filtros.CriarAlias("Revenda", "rev"));
                prospecto.AdicionarFiltro(Filtros.Eq("rev.Id", prospectorevenda.Id));
            }

            if (cidade != null && cidade.Id > 0)
            {
                prospecto.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                prospecto.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                prospecto.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                prospecto.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                prospecto.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                prospecto.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                prospecto.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Prospecto>(prospecto);
        }

        public static Prospecto ConsultarProspectoSemVendaConsiderandoOsSeisMeses(GrupoEconomico grupo)
        {
            Prospecto p = Prospecto.ConsultarCPFouCNPJ(grupo.DadosPessoa.GetType() == typeof(DadosFisica) ? ((DadosFisica)grupo.DadosPessoa).Cpf : ((DadosJuridica)grupo.DadosPessoa).Cnpj);
            if (p != null && p.Venda == null && p.DataCadastro >= DateTime.Now.AddMonths(-6))
                return p;
            else if (p != null && p.DataCadastro >= DateTime.Now.AddMonths(-6))
                return null;


            if (grupo.Empresas != null)
            {
                foreach (Empresa emp in grupo.Empresas)
                {
                    p = Prospecto.ConsultarCPFouCNPJ(emp.GetType() == typeof(DadosFisica) ? ((DadosFisica)emp.DadosPessoa).Cpf : ((DadosJuridica)emp.DadosPessoa).Cnpj);
                    if (p != null && p.Venda == null && p.DataCadastro >= DateTime.Now.AddMonths(-6))
                        return p;
                }
            }

            return null;            
        }

        public static Prospecto ConsultarProspectoComVenda(GrupoEconomico grupo)
        {
            Prospecto p = Prospecto.ConsultarCPFouCNPJ(grupo.DadosPessoa.GetType() == typeof(DadosFisica) ? ((DadosFisica)grupo.DadosPessoa).Cpf : ((DadosJuridica)grupo.DadosPessoa).Cnpj);
            if (p != null && p.Venda != null)
                return p;

            if (grupo.Empresas != null)
            {
                foreach (Empresa emp in grupo.Empresas)
                {
                    p = Prospecto.ConsultarCPFouCNPJ(emp.GetType() == typeof(DadosFisica) ? ((DadosFisica)emp.DadosPessoa).Cpf : ((DadosJuridica)emp.DadosPessoa).Cnpj);
                    if (p != null && p.Venda != null)
                        return p;
                }
            }

            return null;
        }

        public virtual string GetRevenda 
        {
            get 
            {
                return this.Revenda != null ? this.Revenda.Nome : "Não definida";
            }
        }

        public virtual string GetStatus
        {
            get
            {
                return this.Ativo ? "ATIVO" : "INATIVO";
            }
        }

        public virtual string GetCidade
        {
            get
            {
                return this.Endereco != null && this.Endereco.Cidade != null ? this.Endereco.Cidade.Nome : "";
            }
        }

        public virtual string GetEstado
        {
            get
            {
                return this.Endereco != null && this.Endereco.Cidade != null && this.Endereco.Cidade.Estado != null ? this.Endereco.Cidade.Estado.PegarSiglaEstado() : "";
            }
        }

    }
}