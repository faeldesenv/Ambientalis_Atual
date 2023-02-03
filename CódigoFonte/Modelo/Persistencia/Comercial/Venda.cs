using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;
using System.Net.Mail;
using System.Configuration;

namespace Modelo
{
    public partial class Venda : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Venda ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Venda classe = new Venda();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Venda>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Venda ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Venda>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Venda Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Venda>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Venda SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Venda>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Venda> SalvarTodos(IList<Venda> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Venda>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Venda> SalvarTodos(params Venda[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Venda>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Venda>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Venda>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Venda> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Venda obj = Activator.CreateInstance<Venda>();
            return fabrica.GetDAOBase().ConsultarTodos<Venda>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Venda> Filtrar(int qtd)
        {
            Venda cidade = new Venda();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Venda>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual Venda UltimoInserido()
        {
            Venda arquivoFisico = new Venda();
            arquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Venda>(arquivoFisico);
        }

        public static IList<Venda> Filtrar(int idRevenda, int idEstado, int idCidade, DateTime dataDe, DateTime dataAte)
        {
            Venda vendaaa = new Venda();

            vendaaa.AdicionarFiltro(Filtros.Distinct());

            vendaaa.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            vendaaa.AdicionarFiltro(Filtros.SubConsulta("Prospecto"));
            if (idCidade > 0)
                vendaaa.AdicionarFiltro(Filtros.Eq("Endereco.Cidade.Id", idCidade));
            else if (idEstado > 0)
            {
                vendaaa.AdicionarFiltro(Filtros.CriarAlias("Endereco.Cidade", "city"));
                vendaaa.AdicionarFiltro(Filtros.Eq("city.Estado.Id", idEstado));
            }
            if (idRevenda > 0)
                vendaaa.AdicionarFiltro(Filtros.Eq("Revenda.Id", idRevenda));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Venda>(vendaaa);
        }

        public static IList<Venda> FiltrarPorRevenda(int idRevenda)
        {
            Venda vendaaa = new Venda();

            if (idRevenda > 0)
            {
                vendaaa.AdicionarFiltro(Filtros.SubConsulta("Prospecto"));
                vendaaa.AdicionarFiltro(Filtros.Eq("Revenda.Id", idRevenda));
            }

            vendaaa.AdicionarFiltro(Filtros.Distinct());

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Venda>(vendaaa);
        }

        public static IList<Venda> ConsultarPorMesAno(DateTime data)
        {
            Venda venda = new Venda();
            DateTime dataIni = new DateTime(data.Year - 1, data.Month, 1);
            DateTime dataFim = dataIni.AddMonths(1).AddDays(-1).AddHours(23);
            if (data > DateTime.MinValue)
            {
                venda.AdicionarFiltro(Filtros.Between("Data", dataIni, dataFim));
                venda.AdicionarFiltro(Filtros.Eq("Cancelado", false));
            }
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Venda>(venda);
        }

        public virtual Revenda ConsultarRevenda()
        {
            if (this.Id == 0)
                return null;
            else if (this.Prospecto != null)
            {
                Prospecto prop = this.Prospecto;
                if (prop.Revenda != null)
                    return prop.Revenda;
            }
            return null;
        }

        public static void InserirComissoes(GrupoEconomico grupo, Empresa emp, int idConfig)
        {
            if (idConfig > 0)
                return;

            if (grupo.AtivoAmbientalis && grupo.AtivoLogus)
            {
                Prospecto prospecto = Prospecto.ConsultarProspectoSemVendaConsiderandoOsSeisMeses(grupo);
                if (prospecto != null)
                {
                    int mesCarencia = grupo.Contratos != null ? grupo.Contratos.Count > 0 ? grupo.Contratos[0].Carencia : 0 : 0;

                    Venda venda = new Venda();
                    if (emp == null)
                        venda.Data = grupo.Contratos[0].DataAceite;
                    else
                        venda.Data = DateTime.Now;


                    venda.Prospecto = prospecto;
                    if (venda.Mensalidades == null)
                        venda.Mensalidades = new List<Mensalidade>();

                    venda.Carencia = mesCarencia;
                    venda.Emp = venda.Prospecto.Revenda.Id;
                    venda = venda.Salvar();

                    #region _______ Calcular quantidade de Meses executar ______

                    int quantDias = 0;
                    int quantiMeses = 0;
                    if (emp != null)
                    {
                        if (venda.Data.Day == 1)
                            quantiMeses = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(DateTime.Now, grupo.Contratos[0].DataAceite.AddYears(1));
                        else
                            quantiMeses = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(DateTime.Now, grupo.Contratos[0].DataAceite.AddYears(1)) + 1;

                    }
                    else
                    {
                        if (venda.Data.Day == 1)
                            quantiMeses = 12;
                        else
                            quantiMeses = 13;
                    }

                    #endregion

                    #region _________ Se a data de venda for no dia 1 _____________
                    //Se a data de venda for no dia 1: Criar 12 objetos Mensalidade iniciando no mês da data do contrato e terminando no mês anterior 
                    //à data de renovação. Todas as mensalidades terão um PeriodoUso de 1 até o último dia do mês. O valor da mensalidade nominal é zero 
                    //nos primeiros meses, dependendo da quantidade de mêses de carência. Nos outros terá valor total atém completar as 12 mensaldiades. 
                    //   EX. Contratação em 01/07/2012  por R$ 300,00 mensal, com 3 meses de carência: Gerada 12 mensalidades onde as três primeiras terão 
                    //       período de uso com valor zero e as próximas nove com valor R$300,00. 

                    if (venda.Data.Day == 1)
                    {
                        for (int i = 0; i < quantiMeses; i++)
                        {
                            Mensalidade mensalidade = new Mensalidade();
                            mensalidade.Mes = venda.Data.AddMonths(i).Month;
                            mensalidade.Ano = venda.Data.AddMonths(i).Year;
                            mensalidade.Venda = venda;
                            mensalidade.Emp = venda.Prospecto.Revenda.Id;
                            mensalidade = mensalidade.Salvar();

                            PeriodoDeUso periodo = new PeriodoDeUso();
                            periodo.Mensalidade = mensalidade;
                            periodo.DiaInicio = 1;
                            periodo.DiaFim = DateTime.DaysInMonth(venda.Data.AddMonths(i).Year, venda.Data.AddMonths(i).Month);//pegar quantos dias tem o mes                            
                            if (mesCarencia > i)
                                periodo.MensalidadeNominal = 0;
                            else
                                periodo.MensalidadeNominal = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;

                            periodo.Emp = venda.Prospecto.Revenda.Id;
                            periodo.Salvar();
                        }
                    }

                    #endregion

                    #region _________ Se a data de venda for acima do dia 1 _____________
                    //Se a data de venda for maior que dia 1: Criar 13 objetos Mensalidade, utilizando o mesmo exemplo, mas com a data de contratação 
                    //no dia 10/07 e mantendo os 3 meses de carência com valor de 300,00: No 1° mês, gerar um período de uso de 10/07 a 31/07 
                    //(último dia do mês) com valor de zero. No 2° mês gerar uma mensalidade com período de 1/8 a 31/8 (último dia) com valor zero. 
                    //No 3° mês gerar uma mensalidade com perído de 1/9 a 30/9 (último dia) com valor zero. No 4° mês gerar uma mensalidade com dois 
                    //períodos, um do dia 01 a 09 com valor de zero e outro do dia 10 a 31 (último dia) com valor 300,00. do 5° mês até o 12° gerar 
                    //    mensalidades com um período de uso de 1 até ultimo dia do mês e com valor de R$ 300,00. No 13° gerar uma mensalidade com um período 
                    //de uso de 1 até o dia 9 com valor de 300,00. Os e-mails do Rogério, Piassi, Anderson, Supervisor e Revenda, receberão um aviso indicando 
                    //que as comissões foram criadas.

                    if (venda.Data.Day > 1)
                    {
                        for (int i = 0; i < quantiMeses; i++)
                        {
                            Mensalidade mensalidade = new Mensalidade();
                            mensalidade.Mes = venda.Data.AddMonths(i).Month;
                            mensalidade.Ano = venda.Data.AddMonths(i).Year;
                            mensalidade.Venda = venda;
                            mensalidade.Emp = venda.Prospecto.Revenda.Id;
                            mensalidade = mensalidade.Salvar();

                            PeriodoDeUso periodo = new PeriodoDeUso();
                            periodo.Mensalidade = mensalidade;
                            if (mesCarencia > i)
                                periodo.MensalidadeNominal = 0;
                            else
                                periodo.MensalidadeNominal = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;

                            if (i == 0)
                            {
                                periodo.DiaInicio = venda.Data.Day;
                                periodo.DiaFim = DateTime.DaysInMonth(venda.Data.AddMonths(i).Year, venda.Data.AddMonths(i).Month);
                            }
                            else if (i == mesCarencia)
                            {
                                periodo.DiaInicio = 1;
                                periodo.DiaFim = venda.Data.Day - 1;
                                periodo.MensalidadeNominal = 0;
                                periodo.Emp = venda.Prospecto.Revenda.Id;
                                periodo = periodo.Salvar();
                                PeriodoDeUso periodoNovo = new PeriodoDeUso();
                                periodoNovo.MensalidadeNominal = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;
                                periodoNovo.Mensalidade = mensalidade;
                                periodoNovo.DiaInicio = venda.Data.Day;
                                periodoNovo.DiaFim = DateTime.DaysInMonth(venda.Data.AddMonths(i).Year, venda.Data.AddMonths(i).Month);
                                periodoNovo.Emp = venda.Prospecto.Revenda.Id;
                                periodoNovo = periodoNovo.Salvar();
                            }
                            else if (i == (quantiMeses - 1))
                            {
                                periodo.DiaInicio = 1;
                                periodo.DiaFim = venda.Data.Day - 1;
                            }
                            else
                            {
                                periodo.DiaInicio = 1;
                                periodo.DiaFim = DateTime.DaysInMonth(venda.Data.AddMonths(i).Year, venda.Data.AddMonths(i).Month);//pegar quantos dias tem o mes 
                            }

                            periodo.Emp = venda.Prospecto.Revenda.Id;
                            periodo.Salvar();
                        }
                    }

                    #endregion

                    venda.Emp = venda.Prospecto.Revenda.Id;
                    venda = venda.Salvar();
                    prospecto.Venda = venda;
                    prospecto.Emp = venda.Prospecto.Revenda.Id;
                    prospecto = prospecto.Salvar();

                    EmailConfig.CriarMensagemInserir(grupo.Nome, prospecto.Revenda.Nome, prospecto.Revenda.GetUltimoContrato.Comissao.ToString("N2"));
                    EmailConfig.assunto = "Novas de Comissão";
                    EmailConfig.EnviarEmail();
                }

            }
        }

        public static void ReajustarNovasComissoes(Venda venda, int idConfig, decimal igpm)
        {
            if (idConfig > 0)
                return;

            GrupoEconomico grupo = venda.GetGrupoEconomico;

            if (grupo.AtivoAmbientalis && grupo.AtivoLogus)
            {
                decimal mensalidadeReajustada = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade * (igpm + 1);
                grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade = mensalidadeReajustada;
                grupo.Contratos[grupo.Contratos.Count - 1].Salvar();

                DateTime dataVenda = venda.Data.AddYears(venda.Mensalidades.Count / 12);


                #region _______ Calcular quantidade de Meses executar ______

                int quantiMeses = 0;
                if (dataVenda.Day == 1)
                    quantiMeses = 12;
                else
                    quantiMeses = 13;

                #endregion

                #region _________ Se a data de venda for no dia 1 _____________

                if (dataVenda.Day == 1)
                {
                    for (int i = 0; i < quantiMeses; i++)
                    {
                        Mensalidade mensalidade = new Mensalidade();
                        mensalidade.Mes = dataVenda.AddMonths(i).Month;
                        mensalidade.Ano = dataVenda.AddMonths(i).Year;
                        mensalidade.Venda = venda;
                        mensalidade.Emp = venda.Prospecto.Revenda.Id;
                        mensalidade = mensalidade.Salvar();

                        PeriodoDeUso periodo = new PeriodoDeUso();
                        periodo.Mensalidade = mensalidade;
                        periodo.DiaInicio = 1;
                        periodo.DiaFim = DateTime.DaysInMonth(dataVenda.AddMonths(i).Year, dataVenda.AddMonths(i).Month);//pegar quantos dias tem o mes
                        periodo.MensalidadeNominal = mensalidadeReajustada;
                        periodo.Emp = venda.Prospecto.Revenda.Id;
                        periodo.Salvar();
                    }
                }

                #endregion

                #region _________ Se a data de venda for acima do dia 1 _____________

                if (dataVenda.Day > 1)
                {
                    for (int i = 0; i < quantiMeses; i++)
                    {
                        Mensalidade mensalidade = null;
                        if (i == 0)
                        {
                            mensalidade = venda.Mensalidades[venda.Mensalidades.Count - 1];
                        }
                        else
                        {
                            mensalidade = new Mensalidade();
                            mensalidade.Mes = dataVenda.AddMonths(i).Month;
                            mensalidade.Ano = dataVenda.AddMonths(i).Year;
                            mensalidade.Venda = venda;
                            mensalidade.Emp = venda.Prospecto.Revenda.Id;
                            mensalidade = mensalidade.Salvar();
                        }

                        PeriodoDeUso periodo = new PeriodoDeUso();
                        periodo.Mensalidade = mensalidade;

                        periodo.MensalidadeNominal = mensalidadeReajustada;

                        if (i == 0)
                        {
                            periodo.DiaInicio = dataVenda.Day;
                            periodo.DiaFim = DateTime.DaysInMonth(dataVenda.AddMonths(i).Year, dataVenda.AddMonths(i).Month);
                        }
                        else if (i == (quantiMeses - 1))
                        {
                            periodo.DiaInicio = 1;
                            periodo.DiaFim = dataVenda.Day - 1;
                        }
                        else
                        {
                            periodo.DiaInicio = 1;
                            periodo.DiaFim = DateTime.DaysInMonth(dataVenda.AddMonths(i).Year, dataVenda.AddMonths(i).Month);//pegar quantos dias tem o mes 
                        }

                        periodo.Emp = venda.Prospecto.Revenda.Id;
                        periodo.Salvar();
                    }


                #endregion

                    venda.Emp = venda.Prospecto.Revenda.Id;
                    venda = venda.Salvar();
                    EmailConfig.CriarMensagem(grupo.Nome, venda.Prospecto.Revenda.Nome,
                        venda.Prospecto.Revenda.GetUltimoContrato.Comissao.ToString("N2"), "Reajustou");
                    EmailConfig.assunto = "Reajuste de Comissão";
                    EmailConfig.EnviarEmail();
                }

            }
        }

        public static void RecalcularComissoes(GrupoEconomico grupo, int idConfig)
        {
            if (idConfig > 0)
                return;

            if (grupo.AtivoAmbientalis && grupo.AtivoLogus)
            {
                Prospecto prospecto = Prospecto.ConsultarProspectoComVenda(grupo);
                if (prospecto != null && prospecto.Venda != null)
                {
                    int mesCarencia = grupo.Contratos != null ? grupo.Contratos.Count > 0 ? grupo.Contratos[0].Carencia : 0 : 0;

                    #region __________ Se a alteração da mensalidade foi durante o período de carência ______________

                    if (grupo.Contratos[grupo.Contratos.Count - 1].DataAceite.AddMonths(mesCarencia) > DateTime.Now)
                    {
                        foreach (Mensalidade mensalidade in prospecto.Venda.Mensalidades)
                        {
                            foreach (PeriodoDeUso periodo in mensalidade.PeriodosDeUso)
                            {
                                if (periodo.MensalidadeNominal > 0)
                                    periodo.MensalidadeNominal = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;
                            }
                        }
                    }

                    #endregion

                    #region __________ Se a alteração da mensalidade foi fora do período de carência ______________

                    else
                    {
                        decimal novaMensalidade = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;

                        foreach (Mensalidade mensalidade in prospecto.Venda.Mensalidades)
                        {
                            foreach (PeriodoDeUso periodo in mensalidade.PeriodosDeUso)
                            {
                                if (DateTime.Now.Year == periodo.Mensalidade.Ano && DateTime.Now.Month == periodo.Mensalidade.Mes && DateTime.Now.Day >= periodo.DiaInicio && DateTime.Now.Day < periodo.DiaFim)
                                {
                                    if (DateTime.Now.Day == periodo.DiaInicio)
                                    {
                                        periodo.MensalidadeNominal = novaMensalidade;
                                        periodo.Salvar();
                                    }
                                    else
                                    {
                                        int diaFimAntigo = periodo.DiaFim;
                                        periodo.DiaFim = DateTime.Now.Day == 1 ? 1 : DateTime.Now.AddDays(-1).Day;
                                        periodo.Salvar();
                                        PeriodoDeUso novoPeriodo = new PeriodoDeUso();
                                        novoPeriodo.DiaInicio = DateTime.Now.Day;
                                        novoPeriodo.DiaFim = diaFimAntigo;
                                        novoPeriodo.MensalidadeNominal = novaMensalidade;
                                        novoPeriodo.Mensalidade = periodo.Mensalidade;
                                        novoPeriodo.Emp = mensalidade.Venda.Prospecto.Revenda.Id;
                                        novoPeriodo = novoPeriodo.Salvar();
                                    }

                                }
                                else if (new DateTime(periodo.Mensalidade.Ano, periodo.Mensalidade.Mes, periodo.DiaInicio) > DateTime.Now)
                                {
                                    periodo.MensalidadeNominal = novaMensalidade;
                                    periodo.Salvar();
                                }
                            }
                        }
                    }

                    #endregion
                    EmailConfig.CriarMensagem(grupo.Nome, prospecto.Revenda.Nome,
                        prospecto.Revenda.GetUltimoContrato.Comissao.ToString("N2"), "Recalculou");
                    EmailConfig.assunto = "Reajuste de Comissão";
                    EmailConfig.EnviarEmail();
                }
            }
        }

        public static void CancelarVenda(GrupoEconomico grupo, int idConfig)
        {
            if (idConfig > 0)
                return;

            Prospecto p = Prospecto.ConsultarCPFouCNPJ(grupo.DadosPessoa.GetType() == typeof(DadosFisica) ? ((DadosFisica)grupo.DadosPessoa).Cpf : ((DadosJuridica)grupo.DadosPessoa).Cnpj);
            if (p != null)
            {
                if (p.Venda != null)
                {
                    p.Venda.Cancelado = true;
                    p.Venda.Salvar();
                    decimal ValorMensalidade = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;
                    foreach (Mensalidade mensalidade in p.Venda.Mensalidades)
                        if ((mensalidade.Ano == DateTime.Now.Year && mensalidade.Mes >= DateTime.Now.Month) || (mensalidade.Ano > DateTime.Now.Year))
                        {
                            foreach (PeriodoDeUso periodo in mensalidade.PeriodosDeUso)
                            {
                                if (DateTime.Now.Year == periodo.Mensalidade.Ano && DateTime.Now.Month == periodo.Mensalidade.Mes && DateTime.Now.Day >= periodo.DiaInicio && DateTime.Now.Day < periodo.DiaFim)
                                {
                                    if (DateTime.Now.Day == periodo.DiaInicio)
                                    {
                                        periodo.Cancelado = true;
                                        periodo.Salvar();
                                    }
                                    else
                                    {
                                        int diaFimAntigo = periodo.DiaFim;
                                        periodo.DiaFim = DateTime.Now.Day == 1 ? 1 : DateTime.Now.AddDays(-1).Day;
                                        periodo.Salvar();
                                        PeriodoDeUso novoPeriodo = new PeriodoDeUso();
                                        novoPeriodo.DiaInicio = DateTime.Now.Day;
                                        novoPeriodo.DiaFim = diaFimAntigo;
                                        novoPeriodo.Cancelado = true;
                                        novoPeriodo.Mensalidade = periodo.Mensalidade;
                                        novoPeriodo.MensalidadeNominal = ValorMensalidade;
                                        novoPeriodo.Emp = mensalidade.Venda.Prospecto.Revenda.Id;
                                        novoPeriodo = novoPeriodo.Salvar();
                                    }

                                }
                                else
                                {
                                    if (new DateTime(periodo.Mensalidade.Ano, periodo.Mensalidade.Mes, periodo.DiaInicio) > DateTime.Now)
                                    {
                                        periodo.Cancelado = true;
                                        periodo.Salvar();
                                    }
                                }

                            }
                        }
                }
                else
                {
                    if (grupo.Empresas != null)
                        foreach (Empresa emp in grupo.Empresas)
                        {
                            p = Prospecto.ConsultarCPFouCNPJ(grupo.DadosPessoa.GetType() == typeof(DadosFisica) ? ((DadosFisica)grupo.DadosPessoa).Cpf : ((DadosJuridica)grupo.DadosPessoa).Cnpj);
                            if (p.Venda != null)
                            {
                                p.Venda.Cancelado = true;
                                p.Venda.Salvar();
                                foreach (Mensalidade mensalidade in p.Venda.Mensalidades)
                                    if ((mensalidade.Ano == DateTime.Now.Year && mensalidade.Mes > DateTime.Now.Month) || (mensalidade.Ano > DateTime.Now.Year))
                                    {
                                        foreach (PeriodoDeUso periodo in mensalidade.PeriodosDeUso)
                                        {
                                            if (DateTime.Now.Year == periodo.Mensalidade.Ano && DateTime.Now.Month == periodo.Mensalidade.Mes && DateTime.Now.Day >= periodo.DiaInicio && DateTime.Now.Day < periodo.DiaFim)
                                            {
                                                if (DateTime.Now.Day == periodo.DiaInicio)
                                                {
                                                    periodo.Cancelado = true;
                                                    periodo.Salvar();
                                                }
                                                else
                                                {
                                                    int diaFimAntigo = periodo.DiaFim;
                                                    periodo.DiaFim = DateTime.Now.Day == 1 ? 1 : DateTime.Now.AddDays(-1).Day;
                                                    periodo.Salvar();
                                                    PeriodoDeUso novoPeriodo = new PeriodoDeUso();
                                                    novoPeriodo.DiaInicio = DateTime.Now.Day;
                                                    novoPeriodo.DiaFim = diaFimAntigo;
                                                    novoPeriodo.Cancelado = true;
                                                    novoPeriodo.Mensalidade = periodo.Mensalidade;
                                                    novoPeriodo.Emp = mensalidade.Venda.Prospecto.Revenda.Id;
                                                    novoPeriodo = novoPeriodo.Salvar();
                                                }

                                            }
                                            else
                                            {
                                                if (new DateTime(periodo.Mensalidade.Ano, periodo.Mensalidade.Mes, periodo.DiaInicio) > DateTime.Now)
                                                {
                                                    periodo.Cancelado = true;
                                                    periodo.Salvar();
                                                }
                                            }

                                        }
                                    }

                            }
                        }
                }
                EmailConfig.CriarMensagem(grupo.Nome, p.Revenda.Nome, p.Revenda.GetUltimoContrato.Comissao.ToString("N2"), "Cancelou");
                EmailConfig.assunto = "Cancelamento de Comissão";
                EmailConfig.EnviarEmail();
            }
        }

        public static void CancelarVenda(Empresa e, int idConfig)
        {
            if (idConfig > 0)
                return;

            Prospecto p = Prospecto.ConsultarCPFouCNPJ(e.DadosPessoa.GetType() == typeof(DadosFisica) ? ((DadosFisica)e.DadosPessoa).Cpf : ((DadosJuridica)e.DadosPessoa).Cnpj);
            if (p != null)
                if (p.Venda != null)
                {
                    p.Venda.Cancelado = true;
                    p.Venda.Salvar();
                    decimal ValorMensalidade = e.GrupoEconomico.Contratos[e.GrupoEconomico.Contratos.Count - 1].Mensalidade;
                    foreach (Mensalidade mensalidade in p.Venda.Mensalidades)
                        foreach (PeriodoDeUso periodo in mensalidade.PeriodosDeUso)
                        {
                            if (DateTime.Now.Year == periodo.Mensalidade.Ano && DateTime.Now.Month == periodo.Mensalidade.Mes && DateTime.Now.Day >= periodo.DiaInicio && DateTime.Now.Day < periodo.DiaFim)
                            {
                                if (DateTime.Now.Day == periodo.DiaInicio)
                                {
                                    periodo.Cancelado = true;
                                    periodo.Salvar();
                                }
                                else
                                {
                                    int diaFimAntigo = periodo.DiaFim;
                                    periodo.DiaFim = DateTime.Now.Day == 1 ? 1 : DateTime.Now.AddDays(-1).Day;
                                    periodo.Salvar();
                                    PeriodoDeUso novoPeriodo = new PeriodoDeUso();
                                    novoPeriodo.DiaInicio = DateTime.Now.Day;
                                    novoPeriodo.DiaFim = diaFimAntigo;
                                    novoPeriodo.Cancelado = true;
                                    novoPeriodo.Mensalidade = periodo.Mensalidade;
                                    novoPeriodo.MensalidadeNominal = ValorMensalidade;
                                    novoPeriodo.Emp = mensalidade.Venda.Prospecto.Revenda.Id;
                                    novoPeriodo = novoPeriodo.Salvar();
                                }

                            }
                            else
                            {
                                if (new DateTime(periodo.Mensalidade.Ano, periodo.Mensalidade.Mes, periodo.DiaInicio) > DateTime.Now)
                                {
                                    periodo.Cancelado = true;
                                    periodo.Salvar();
                                }
                            }

                        }
                    EmailConfig.CriarMensagem(e.GrupoEconomico.Nome, p.Revenda.Nome, p.Revenda.GetUltimoContrato.Comissao.ToString("N2"), "Cancelou");
                    EmailConfig.assunto = "Cancelamento de Comissão";
                    EmailConfig.EnviarEmail();
                }
        }

        public static void ReativarVenda(GrupoEconomico grupo, int idConfig)
        {
            if (idConfig > 0)
                return;

            Prospecto p = Prospecto.ConsultarProspectoComVenda(grupo);
            if (p != null && p.Venda != null && p.Venda.Cancelado)
            {
                p.Venda.Cancelado = false;
                decimal ValorMensalidade = grupo.Contratos[grupo.Contratos.Count - 1].Mensalidade;
                foreach (Mensalidade m in p.Venda.Mensalidades)
                {
                    if ((m.Ano == DateTime.Now.Year && m.Mes >= DateTime.Now.Month) || (m.Ano > DateTime.Now.Year))
                    {
                        if (m.Mes == DateTime.Now.Month)
                        {
                            foreach (PeriodoDeUso periodo in m.PeriodosDeUso)
                            {
                                if (DateTime.Now.Year == periodo.Mensalidade.Ano && DateTime.Now.Month == periodo.Mensalidade.Mes && DateTime.Now.Day >= periodo.DiaInicio && DateTime.Now.Day <= periodo.DiaFim)
                                {
                                    if (DateTime.Now.Day == periodo.DiaInicio)
                                    {
                                        periodo.Cancelado = false;
                                        periodo.Salvar();
                                    }
                                    else if (DateTime.Now.Day == periodo.DiaFim)
                                    {
                                        periodo.Cancelado = true;
                                        periodo.Salvar();
                                    }
                                    else
                                    {
                                        int diaFimAntigo = periodo.DiaFim;
                                        periodo.DiaFim = DateTime.Now.Day == 1 ? 1 : DateTime.Now.AddDays(-1).Day;
                                        periodo.Salvar();
                                        PeriodoDeUso novoPeriodo = new PeriodoDeUso();
                                        novoPeriodo.Cancelado = false;
                                        novoPeriodo.DiaInicio = DateTime.Now.Day;
                                        novoPeriodo.DiaFim = diaFimAntigo;
                                        novoPeriodo.MensalidadeNominal = ValorMensalidade;
                                        novoPeriodo.Mensalidade = periodo.Mensalidade;
                                        novoPeriodo.Emp = m.Venda.Prospecto.Revenda.Id;
                                        novoPeriodo = novoPeriodo.Salvar();
                                    }

                                }
                            }
                        }
                        else
                        {
                            foreach (PeriodoDeUso periodo in m.PeriodosDeUso)
                            {
                                periodo.Cancelado = false;
                                periodo.Salvar();
                            }
                        }
                    }
                }
                p.Venda.Salvar();
                p.Salvar();
                EmailConfig.CriarMensagem(grupo.Nome, p.Revenda.Nome, p.Revenda.GetUltimoContrato.Comissao.ToString("N2"), "Reativou");
                EmailConfig.assunto = "Rativação de Comissão";
                EmailConfig.EnviarEmail();
            }
        }

        public static IList<Venda> FiltrarRelatorio(Revenda revenda, DateTime dataDe, DateTime dataAte, int status)
        {
            Venda venda = new Venda();
            if (status > 0)
                venda.AdicionarFiltro(Filtros.Eq("Cancelado", status == 1));
            venda.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
            if (revenda != null)
            {
                venda.AdicionarFiltro(Filtros.CriarAlias("Prospecto", "prosp"));
                venda.AdicionarFiltro(Filtros.CriarAlias("prosp.Revenda", "revend"));
                venda.AdicionarFiltro(Filtros.Eq("revend.Id", revenda.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Venda>(venda);
        }

        public virtual GrupoEconomico GetGrupoEconomico
        {
            get
            {
                GrupoEconomico g = GrupoEconomico.ConsultarPorCpfCnpj(this.Prospecto.DadosPessoa.GetType() == typeof(DadosFisicaComercial) ? ((DadosFisicaComercial)this.Prospecto.DadosPessoa).Cpf : ((DadosJuridicaComercial)this.Prospecto.DadosPessoa).Cnpj);
                if (g != null)
                {
                    return g;
                }
                Empresa e = Empresa.ConsultarPorCpfCnpj(this.Prospecto.DadosPessoa.GetType() == typeof(DadosFisicaComercial) ? ((DadosFisicaComercial)this.Prospecto.DadosPessoa).Cpf : ((DadosJuridicaComercial)this.Prospecto.DadosPessoa).Cnpj);
                if (e != null)
                    return e.GrupoEconomico;

                return null;
            }
        }

        public static IList<Venda> FiltrarVendasDoSupervisor()
        {
            Venda vendaaa = new Venda();
            vendaaa.AdicionarFiltro(Filtros.CriarAlias("Prospecto", "P"));
            vendaaa.AdicionarFiltro(Filtros.CriarAlias("P.Revenda", "R"));
            vendaaa.AdicionarFiltro(Filtros.Eq("R.Supervisor", false));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Venda>(vendaaa);
        }

        public virtual decimal GetValorMensalidadeAtual()
        {
            return this.Mensalidades.Single<Mensalidade>(ms => ms.Mes == DateTime.Now.Month && ms.Ano == DateTime.Now.Year).GetValorMensalidade;
        }

        // Estrutura auxiliar para envio de emails
        private struct EmailConfig
        {
            public static string remetente = "revenda@sustentar.inf.br";
            public static string assunto;
            public static string mensagem;

            public static void CriarMensagem(string grupo, string revenda, string comissao, string acao)
            {
                mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png' /></div>
            <div style='float:left; margin-left:30px; margin-rigth:15px; font-family:arial; font-size:18px; font-weight:bold; margin-top:30px; text-align:center;'>Comissões Sustentar</div>
            <div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:10px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; font-size:13px;padding:7px; background-color:#E9E9E9; text-align:left; height:auto'>
            O Grupo <b>"+grupo+@"</b> <br />
            <b><u>"+acao+"</u></b> comissões da revenda <b>"+revenda+@"</b> <br />
             Comissão: <b>"+comissao+@"</b>% <br />
            </div>
            <div style='margin-left:10px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; font-weight:bold; font-size:13px;padding:7px; text-align:center; height:auto'>
                Data: "+DateTime.Now.ToShortDateString()+".</div></div>";
            }

            public static void CriarMensagemInserir(string grupo, string revenda, string comissao)
            {
                mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png' /></div>
            <div style='float:left; margin-left:30px; margin-rigth:15px; font-family:arial; font-size:18px; font-weight:bold; margin-top:30px; text-align:center;'>Comissões Sustentar</div>
            <div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:10px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; font-size:13px;padding:7px; background-color:#E9E9E9; text-align:left; height:auto'>
            O Grupo <b>" + grupo + @"</b> <br />
            <b><u>Criou Novas</u></b> comissões para a revenda <b>" + revenda + @"</b> <br />
             Comissão: <b>" + comissao + @"</b>% <br />
            </div>
            <div style='margin-left:10px; margin-right:10px; font-family:Arial, Helvetica, sans-serif; font-weight:bold; font-size:13px;padding:7px; text-align:center; height:auto'>
                Data: " + DateTime.Now.ToShortDateString() + ".</div></div>";
            }

            public static void EnviarEmail()
            {
                try
                {   
                    List<MailAddress> enderecos = new List<MailAddress>() { 
                        new MailAddress("rogerio@sustentar.inf.br"),
                        new MailAddress("piassi@sustentar.inf.br"),
                        new MailAddress("anderson@sustentar.inf.br"),
                        new MailAddress("supervisor@sustentar.inf.br"),
                    };
                    MailMessage email = new MailMessage()
                    {
                        From = new MailAddress(remetente),
                        Subject = assunto,
                        IsBodyHtml = true,
                        Body = mensagem,
                        BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
                    };

                    enderecos.ForEach(end => email.To.Add(end));

                    SmtpClient smpt = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
                    smpt.UseDefaultCredentials = true;
                    smpt.Send(email);
                }
                catch (Exception) { }
            }
            
        }

        public virtual string GetRevenda
        {
            get
            {
                return this.Prospecto != null && this.Prospecto.Revenda != null ? this.Prospecto.Revenda.Nome : "";
            }
        }

        public virtual string GetProspecto
        {
            get
            {
                return this.Prospecto != null ? this.Prospecto.Nome : "";
            }
        }

        public virtual string GetCNPJeCPFProspecto
        {
            get
            {
                return this.Prospecto != null ? this.Prospecto.GetNumeroCNPJeCPFComMascara : "";
            }
        }

        public virtual string GetDataCadastroProspecto
        {
            get
            {
                return this.Prospecto != null ? this.Prospecto.DataCadastro.ToShortDateString() : "";
            }
        }

        public virtual string GetValorMensalidade
        {
            get
            {
                return this.GetValorMensalidadeAtual().ToString("0.00");
            }
        }

        public virtual string GetCidade
        {
            get
            {
                return this.Prospecto != null && this.Prospecto.Endereco != null && this.Prospecto.Endereco.Cidade != null ? this.Prospecto.Endereco.Cidade.Nome : "";
            }
        }

        public virtual string GetEstado
        {
            get
            {
                return this.Prospecto != null && this.Prospecto.Endereco != null && this.Prospecto.Endereco.Cidade != null && this.Prospecto.Endereco.Cidade.Estado != null ? this.Prospecto.Endereco.Cidade.Estado.PegarSiglaEstado() : "";
            }
        }

        public virtual string GetQtdVendas
        {
            get
            {
                return this.Prospecto != null && this.Prospecto.Endereco != null && this.Prospecto.Endereco.Cidade != null && this.Prospecto.Endereco.Cidade.Estado != null ? this.Prospecto.Endereco.Cidade.Estado.PegarSiglaEstado() : "";
            }
        }

    }
}