using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Mensalidade : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Mensalidade ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Mensalidade classe = new Mensalidade();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Mensalidade>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Mensalidade ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Mensalidade>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Mensalidade Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Mensalidade>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Mensalidade SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Mensalidade>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Mensalidade> SalvarTodos(IList<Mensalidade> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Mensalidade>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Mensalidade> SalvarTodos(params Mensalidade[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Mensalidade>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Mensalidade>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Mensalidade>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Mensalidade> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Mensalidade obj = Activator.CreateInstance<Mensalidade>();
            return fabrica.GetDAOBase().ConsultarTodos<Mensalidade>(obj);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Mensalidade> Filtrar(int qtd)
        {
            Mensalidade cidade = new Mensalidade();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Mensalidade>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual Mensalidade UltimoInserido()
        {
            Mensalidade ArquivoFisico = new Mensalidade();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Mensalidade>(ArquivoFisico);
        }

        public virtual PeriodoDeUso GetUltimoPeriodoUso
        {
            get
            {
                if (this.PeriodosDeUso != null && this.PeriodosDeUso.Count > 0)
                {
                    return this.PeriodosDeUso.Last();
                }
                return null;
            }
        }

        // (mensalidadeNominal / ultimo dia do mês) * ( ultimo dia do periodo - primeiro dia do periodo + 1) * percentual da revenda.
        public virtual Decimal GetValorMensalidade
        {
            get
            {
                if (this.periodosDeUso == null)
                    return 0;

                decimal contador = 0;
                foreach (PeriodoDeUso periodo in this.PeriodosDeUso)
                {
                    contador += periodo.GetValorPeriodo;
                }
                return contador;
            }
        }

        public virtual Decimal GetValorRevenda
        {
            get
            {
                if (this.GetValorMensalidade <= 0)//mes de carencia
                    return 0;

                //só entra se é a primeira mensalidade depois da carencia
                if (this.Venda.Mensalidades[this.Venda.Carencia].Id == this.Id)
                {
                    return this.GetValorMensalidade / venda.Data.Day;//100% do valor para primeiro mes refrente aos dias usados
                }

                if (this.Venda.Mensalidades[this.Venda.Carencia + 1].Id == this.Id && venda.Data.Day != 1)// proximo mes da carencia
                {
                    //100% do valor referente aos dias que faltaram para completar os 30 dias
                    decimal contador = 0;
                    foreach (PeriodoDeUso periodo in this.PeriodosDeUso)
                    {
                        if (periodo.DiaInicio < this.Venda.Data.Day && periodo.DiaFim >= Venda.Data.Day)
                        {
                            contador += ((periodo.MensalidadeNominal / 31) * (this.Venda.Data.Day - periodo.DiaInicio + 1));
                            contador += ((periodo.MensalidadeNominal / 31) * (periodo.DiaFim - this.Venda.Data.Day)) * (this.Venda.Prospecto.Revenda.GetUltimoContrato.Comissao / 100);
                        }
                        else if (periodo.DiaInicio < this.Venda.Data.Day)
                        {
                            contador += periodo.GetValorPeriodo;
                        }
                        else
                        {
                            contador += periodo.GetValorPeriodo * (this.Venda.Prospecto.Revenda.GetUltimoContrato.Comissao / 100);
                        }
                    }
                    return contador;
                }
                else
                {
                    return this.GetValorMensalidade * (this.Venda.Prospecto.Revenda.GetUltimoContrato.Comissao / 100);
                }

            }
        }



        public virtual Decimal GetValorSupervisor
        {
            get
            {
                if (this.GetValorMensalidade <= 0)//mes de carencia
                    return 0;

                //só entra se é a primeira mensalidade depois da carencia
                if (this.Venda.Mensalidades[this.Venda.Carencia].Id == this.Id)
                {
                    return 0;//0% pois o valor é destinado 100% para a revenda 
                }

                if (this.Venda.Prospecto.Endereco.Cidade == null)
                    return this.GetValorMensalidade * Convert.ToDecimal(0.02);

                if (this.Venda.Prospecto.Endereco.Cidade.Estado.Nome.Trim().ToUpper() == "ESPÍRITO SANTO")
                    return this.GetValorMensalidade * Convert.ToDecimal(0.05);
                else
                    return this.GetValorMensalidade * Convert.ToDecimal(0.02);
            }
        }

        public virtual bool GetCancelada
        {
            get
            {
                foreach (PeriodoDeUso periodo in this.PeriodosDeUso)
                {
                    if (!periodo.Cancelado)
                        return false;
                }
                return true;
            }
        }



    }
}