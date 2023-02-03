using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Revenda : PessoaComercial
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Revenda ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Revenda classe = new Revenda();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Revenda>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Revenda ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Revenda>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Revenda Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Revenda>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Revenda SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Revenda>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Revenda> SalvarTodos(IList<Revenda> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Revenda>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Revenda> SalvarTodos(params Revenda[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Revenda>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Revenda>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Revenda>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Revenda> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Revenda obj = Activator.CreateInstance<Revenda>();
            return fabrica.GetDAOBase().ConsultarTodos<Revenda>(obj);
        }

        public static IList<Revenda> ConsultarTodosOrdemAlfabetica()
        {
            Revenda aux = new Revenda();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(aux);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Revenda> Filtrar(int qtd)
        {
            Revenda cidade = new Revenda();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual Revenda UltimoInserido()
        {
            Revenda ArquivoFisico = new Revenda();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Revenda>(ArquivoFisico);
        }

        public static bool ExisteCNPJ(Revenda auxiliar, string cnpj)
        {
            Revenda aux = new Revenda();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", auxiliar != null ? auxiliar.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cnpj", cnpj));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Revenda> revendas = fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(aux);
            return revendas != null && revendas.Count > 0;
        }


        public static bool ExisteCPF(Revenda auxiliar, string cpf)
        {
            Revenda aux = new Revenda();
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", auxiliar != null ? auxiliar.Id : 0));
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Eq("Cpf", cpf));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Revenda> revendas = fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(aux);
            return revendas != null && revendas.Count > 0;
        }

        public virtual ContratoComercial GetUltimoContrato
        {
            get
            {
                if (this.Contratos != null && this.Contratos.Count > 0)
                {
                    return Contratos[Contratos.Count - 1];
                }
                return null;
            }
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

        public static IList<Revenda> Filtrar(string nome, string responsavel, string CnpjCpf, Cidade cidade, Estado estado, string tipoParceiro)   ///status: 0 para todos, 1 para ativo, 2 para inativo
        {
            Revenda revenda = new Revenda();
            revenda.AdicionarFiltro(Filtros.Distinct());
            revenda.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));

            if (!string.IsNullOrEmpty(nome))
                revenda.AdicionarFiltro(Filtros.Like("Nome", nome));

            if (!string.IsNullOrEmpty(responsavel))
                revenda.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));

            if (!string.IsNullOrEmpty(CnpjCpf))
            {
                revenda.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
                revenda.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cnpj", CnpjCpf), Filtros.Like("dp.Cpf", CnpjCpf)));
            }

            if (tipoParceiro != "")
            {
                revenda.AdicionarFiltro(Filtros.Eq("TipoParceiro", tipoParceiro));
            }

            if (cidade != null && cidade.Id > 0)
            {
                revenda.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                revenda.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                revenda.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                revenda.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                revenda.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                revenda.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                revenda.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(revenda);
        }

        public static IList<Revenda> FiltrarRelatorio(Estado estado, Cidade cidade, string tipoParceiro)
        {
            Revenda revenda = new Revenda();

            if (cidade != null && cidade.Id > 0)
            {
                revenda.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                revenda.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                revenda.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                revenda.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                revenda.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                revenda.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                revenda.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            if (tipoParceiro != "")
            {
                revenda.AdicionarFiltro(Filtros.Eq("TipoParceiro", tipoParceiro));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(revenda);
        }

        public static IList<Revenda> ConsultarComissao(int mes, int ano)
        {
            return ConsultarFaturamento(null, mes, ano);
        }

        public static IList<Revenda> ConsultarFaturamento(Revenda revendaAux, int mes, int ano)
        {
            Revenda revenda = new Revenda();
            revenda.AdicionarFiltro(Filtros.Distinct());
            if (revendaAux != null)
                revenda.AdicionarFiltro(Filtros.Eq("Id", revendaAux.Id));
            revenda.AdicionarFiltro(Filtros.SubConsulta("Prospectos"));
            revenda.AdicionarFiltro(Filtros.SubConsulta("Venda"));
            revenda.AdicionarFiltro(Filtros.SubConsulta("Mensalidades"));
            if (mes != 0)
                revenda.AdicionarFiltro(Filtros.Eq("Mes", mes));
            if (ano != 0)
                revenda.AdicionarFiltro(Filtros.Eq("Ano", ano));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Revenda>(revenda);
        }

        public virtual string GetTipoParceria 
        {
            get 
            {
                return this.TipoParceiro != null ? this.TipoParceiro : "";
            }
        }

        public virtual string GetStatus 
        {
            get 
            {
                return this.GetUltimoContrato != null && this.GetUltimoContrato.Aceito == true ? "ATIVA" : "INATIVA";
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