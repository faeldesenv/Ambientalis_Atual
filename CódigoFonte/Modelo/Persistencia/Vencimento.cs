using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Vencimento : ObjetoBase
    {
        public const string OUTROSPROCESSO = "OutrosProcesso";
        public const string OUTROSEMPRESA = "OutrosEmpresa";
        public const string CONDICIONANTE = "Condicionante";
        public const string LICENCA = "Licenca";
        public const string EXIGENCIA = "Exigencia";
        public const string RAL = "Ral";
        public const string GUIA = "GuiaDeUtilizacao";
        public const string REQUERIMENTOLAVRA_ALVARAPESQUISA = "RequerimentoLavraAlvaraPesquisa";
        public const string NOTIFICACAO_PESQUISADNPM = "NotificacaoPesquisaDNPM";
        public const string ALVARAPESQUISA = "AlvaraPesquisa";
        public const string LIMITERENUNCIA = "LimiteRenuncia";
        public const string TAXAANUALPORHECTARE = "TaxaAnualPorHectare";
        public const string REQUERIMENTOLPTOTAL = "RequerimentoLPTotal";
        public const string EXTRACAO = "Extracao";
        public const string ENTREGALICENCAOUPROTOCOLO = "EntregaLicencaOuProtocolo";
        public const string LICENCIAMENTO = "Licenciamento";
        public const string REQUERIMENTOIMISSAOPOSSE = "RequerimentoImissaoPosse";
        public const string ENTREGARELATORIOANUAL = "EntregaRelatorioAnual";
        public const string CERTIFICADOREGULARIDADE = "CertificadoRegularidade";
        public const string TAXATRIMESTRAL = "TaxaTrimestral";
        public const string DIPEMConst = "DIPEM";
        public const string VENCIMENTODIVERSO = "Vencimento Diverso";

        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Vencimento ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Vencimento classe = new Vencimento();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Vencimento>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Vencimento ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Vencimento>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Vencimento Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();

            Vencimento v = fabrica.GetDAOBase().Salvar<Vencimento>(this);

            //salvar todas as notificacoes do vencimento
            if (v.Notificacoes != null) 
            {
                foreach (Notificacao item in v.Notificacoes)
                {
                    item.ConsultarPorId();
                    item.Vencimento = v;
                    item.Salvar();
                }
            }

            return v;
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Vencimento SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Vencimento>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Vencimento> SalvarTodos(IList<Vencimento> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Vencimento>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Vencimento> SalvarTodos(params Vencimento[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Vencimento>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Vencimento>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Vencimento>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Vencimento> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Vencimento obj = Activator.CreateInstance<Vencimento>();
            return fabrica.GetDAOBase().ConsultarTodos<Vencimento>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Vencimento> Filtrar(int qtd)
        {
            Vencimento estado = new Vencimento();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Vencimento>(estado);
        }

        private string resultNotificacao;
        public virtual Notificacao GetProximaNotificacao
        {
            get
            {
                DateTime hj = DateTime.Now;
                hj = new DateTime(hj.Year, hj.Month, hj.Day, 0, 0, 0);
                if (this.Notificacoes != null && this.Notificacoes.Count > 0)
                {
                    Notificacao not = null;
                    foreach (Notificacao n2 in this.Notificacoes)
                    {
                        if (n2.Data > DateTime.Now)
                        {
                            not = n2;
                            break;
                        }
                    }

                    if (not != null)
                    {
                        foreach (Notificacao n in this.Notificacoes)
                        {

                            if (n.Data >= hj && n.Data < SqlDate.MaxValue && n.Data < not.Data)
                            {
                                not = n;
                            }
                        }
                    }
                    else 
                    {
                        resultNotificacao = "Todos as notificações já ocorreram.";
                        return null;
                    }

                    return not;
                }
                else
                {
                    resultNotificacao = "Não Informada.";
                }
                return null;
            }
        }

        public virtual String GetDataProximaNotificacao
        {
            get
            {
                if (this.GetProximaNotificacao == null)
                    return resultNotificacao;

                if (this.GetProximaNotificacao.Data != null)
                    return this.GetProximaNotificacao.Data <= SqlDate.MinValue || this.GetProximaNotificacao.Data >= SqlDate.MaxValue ? resultNotificacao : this.GetProximaNotificacao.Data.ToShortDateString();
                return resultNotificacao;
            }
        }

        public virtual String GetDataVencimento
        {
            get
            {
                if (this.Data != null)
                    return this.Data <= DateTime.MinValue || this.Data >= DateTime.MaxValue ? "Não Informada." : this.Data.ToShortDateString();
                return "Não Informada";
            }
        }

        public static IList<Vencimento> FiltrarRelatorio(int idGrupoEconomico, int idEmpresa, string tipoVencimento, DateTime dataDePeriodo, DateTime dataAtehPeriodo, int idEstado, int idStatus, int periodico)
        {
            Vencimento aux = new Vencimento();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Between("Data", dataDePeriodo, dataAtehPeriodo));
            aux.AdicionarFiltro(Filtros.SetOrderDesc("Data"));
            if (periodico > 0)
            {
                aux.AdicionarFiltro(Filtros.Eq("Periodico", periodico == 1));
            }
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Vencimento> vencimentos = fabrica.GetDAOBase().ConsultarComFiltro<Vencimento>(aux);
            Vencimento.RemoverVencimentosDeOutroGrupoEconomico(vencimentos, idGrupoEconomico);
            Vencimento.RemoverVencimentosDeOutraEmpresa(vencimentos, idEmpresa);
            Vencimento.RemoverVencimentosDeOutroTipo(vencimentos, tipoVencimento);
            Vencimento.RemoverVencimentosDeOutroEstado(vencimentos, idEstado);
            if (idStatus > 0)
                Vencimento.RemoverVencimentosDeStatusDiversos(vencimentos, idStatus);
            return vencimentos;
        }

        private static void RemoverVencimentosDeOutraEmpresa(IList<Vencimento> vencimentos, int idEmpresa)
        {
            if (idEmpresa > 0)
                for (int index = vencimentos.Count - 1; index >= 0; index--)
                {
                    Empresa aux = vencimentos[index].GetEmpresa;
                    if (aux == null || aux.Id != idEmpresa)
                        vencimentos.RemoveAt(index);
                }
        }

        private static void RemoverVencimentosDeOutroEstado(IList<Vencimento> vencimentos, int idEstado)
        {
            if (idEstado > 0)
            {
                for (int index = vencimentos.Count - 1; index >= 0; index--)
                {
                    if (vencimentos[index].GetEstado == null || vencimentos[index].GetEstado.Id != idEstado)
                    {
                        vencimentos.Remove(vencimentos[index]);
                    }
                }
            }
        }

        private static void RemoverVencimentosDeOutroGrupoEconomico(IList<Vencimento> vencimentos, int idGrupoEconomico)
        {
            if (idGrupoEconomico > 0)
                for (int index = vencimentos.Count - 1; index >= 0; index--)
                {
                    GrupoEconomico aux = vencimentos[index].GetGrupoEconomico;
                    if (aux == null || aux.Id != idGrupoEconomico)
                        vencimentos.RemoveAt(index);
                }
        }

        private static void RemoverVencimentosDeOutroTipo(IList<Vencimento> vencimentos, string tipoVencimento)
        {
            if (tipoVencimento.Trim() != "0")
                for (int index = vencimentos.Count - 1; index >= 0; index--)
                {
                    string aux = Vencimento.GetConstanteTipo(vencimentos[index]);
                    if (aux == null || aux != tipoVencimento)
                        vencimentos.RemoveAt(index);
                }
        }

        private static void RemoverVencimentosDeStatusDiversos(IList<Vencimento> vencimentos, int idStatus)
        {
            if (vencimentos != null && vencimentos.Count > 0)
            {
                for (int index = vencimentos.Count - 1; index >= 0; index--)
                {
                    if (vencimentos[index].GetType() == typeof(VencimentoDiverso))
                    {
                        vencimentos.Remove(vencimentos[index]);
                    }
                    else
                    {
                        if (vencimentos[index].Status == null || vencimentos[index].Status.Id != idStatus)
                            vencimentos.Remove(vencimentos[index]);
                    }
                }
            }
        }

        public virtual string GetTipo
        {
            get
            {
                if (this.Condicional != null)
                    return this.Condicional.GetTipo;
                else if (this.Licenca != null)
                    return "Licença";
                else if (this.Exigencia != null)
                    return "Exigência";
                else if (this.Ral != null)
                    return "RAL";
                else if (this.GuiaDeUtilizacao != null)
                    return "Guia de Utilização";
                else if (this.RequerimentoLavraAlvaraPesquisa != null)
                    return "Requerimento de Lavra do Álvara de Pesquisa";
                else if (this.NotificacaoPesquisaDNPM != null)
                    return "Notificação da Pesquisa DNPM";
                else if (this.AlvaraPesquisa != null)
                    return "Alvará de Pesquisa";
                else if (this.TaxaAnualPorHectare != null)
                    return "Taxa anual por hectare";
                else if (this.RequerimentoLPTotal != null)
                    return "Requerimento LP Poligonal";
                else if (this.Extracao != null)
                    return "Extração";
                else if (this.DIPEM != null)
                    return "DIPEM";
                else if (this.EntregaLicencaOuProtocolo != null)
                    return "Entrega de Licença ou Protocolo";
                else if (this.Licenciamento != null)
                    return "Licenciamento";
                else if (this.RequerimentoImissaoPosse != null)
                    return "Requerimento de imissão de posse";
                else if (this.EntregaRelatorioAnual != null)
                    return "Entrega de relatório anual";
                else if (this.CertificadoRegularidade != null)
                    return "Certificado de Regularidade";
                else if (this.TaxaTrimestral != null)
                    return "Taxa trimestral";
                else if (this.LimiteRenuncia != null)
                    return "Limite para Renúncia";
                else if (this.GetType() == typeof(VencimentoDiverso))
                {
                    VencimentoDiverso v = VencimentoDiverso.ConsultarPorId(this.Id);
                    if (v != null && v.Diverso != null)
                        return v.Diverso.TipoDiverso.Nome;
                    else
                        return null;
                }
                return null;
            }
        }

        public static string GetConstanteTipo(Vencimento vencimento)
        {
            if (vencimento.Condicional != null)
                return (vencimento.Condicional.GetTipo == "Condicionante" ? Vencimento.CONDICIONANTE : vencimento.Condicional.GetTipo == "Outros Empresa" ? Vencimento.OUTROSEMPRESA : Vencimento.OUTROSPROCESSO);
            else if (vencimento.Licenca != null)
                return Vencimento.LICENCA;
            else if (vencimento.Exigencia != null)
                return Vencimento.EXIGENCIA;
            else if (vencimento.Ral != null)
                return Vencimento.RAL;
            else if (vencimento.GuiaDeUtilizacao != null)
                return Vencimento.GUIA;
            else if (vencimento.RequerimentoLavraAlvaraPesquisa != null)
                return Vencimento.REQUERIMENTOLAVRA_ALVARAPESQUISA;
            else if (vencimento.NotificacaoPesquisaDNPM != null)
                return Vencimento.NOTIFICACAO_PESQUISADNPM;
            else if (vencimento.AlvaraPesquisa != null)
                return Vencimento.ALVARAPESQUISA;
            else if (vencimento.TaxaAnualPorHectare != null)
                return Vencimento.TAXAANUALPORHECTARE;
            else if (vencimento.RequerimentoLPTotal != null)
                return Vencimento.REQUERIMENTOLPTOTAL;
            else if (vencimento.Extracao != null)
                return Vencimento.EXTRACAO;
            else if (vencimento.DIPEM != null)
                return Vencimento.DIPEMConst;
            else if (vencimento.LimiteRenuncia != null)
                return Vencimento.LIMITERENUNCIA;
            else if (vencimento.EntregaLicencaOuProtocolo != null)
                return Vencimento.ENTREGALICENCAOUPROTOCOLO;
            else if (vencimento.Licenciamento != null)
                return Vencimento.LICENCIAMENTO;
            else if (vencimento.RequerimentoImissaoPosse != null)
                return Vencimento.REQUERIMENTOIMISSAOPOSSE;
            else if (vencimento.EntregaRelatorioAnual != null)
                return Vencimento.ENTREGARELATORIOANUAL;
            else if (vencimento.CertificadoRegularidade != null)
                return Vencimento.CERTIFICADOREGULARIDADE;
            else if (vencimento.TaxaTrimestral != null)
                return Vencimento.TAXATRIMESTRAL;
            else if (vencimento.GetType() == typeof(VencimentoDiverso))
                return Vencimento.VENCIMENTODIVERSO;
            return null;
        }

        public virtual Empresa GetEmpresa
        {
            get
            {
                if (this.Condicional != null)
                    return this.Condicional.GetEmpresa;
                else if (this.Licenca != null)
                    return this.Licenca.Processo != null ? this.Licenca.Processo.Empresa : null;
                else if (this.Exigencia != null && this.Exigencia.Regime != null)
                    return this.Exigencia.Regime != null && this.Exigencia.Regime.ProcessoDNPM != null ? this.Exigencia.Regime.ProcessoDNPM.Empresa : null;
                else if (this.Exigencia != null && this.Exigencia.GuiaUtilizacao != null)
                    return this.Exigencia.GuiaUtilizacao != null && this.Exigencia.GuiaUtilizacao.ProcessoDNPM != null ? this.Exigencia.GuiaUtilizacao.ProcessoDNPM.Empresa : null;
                else if (this.Ral != null)
                    return this.Ral.ProcessoDNPM != null ? this.Ral.ProcessoDNPM.Empresa : null;
                else if (this.GuiaDeUtilizacao != null)
                    return this.GuiaDeUtilizacao.ProcessoDNPM != null ? this.GuiaDeUtilizacao.ProcessoDNPM.Empresa : null;
                else if (this.RequerimentoLavraAlvaraPesquisa != null)
                    return this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null ? this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Empresa : null;
                else if (this.NotificacaoPesquisaDNPM != null)
                    return this.NotificacaoPesquisaDNPM.ProcessoDNPM != null ? this.NotificacaoPesquisaDNPM.ProcessoDNPM.Empresa : null;
                else if (this.AlvaraPesquisa != null)
                    return this.AlvaraPesquisa.ProcessoDNPM != null ? this.AlvaraPesquisa.ProcessoDNPM.Empresa : null;
                else if (this.TaxaAnualPorHectare != null)
                    return this.TaxaAnualPorHectare.ProcessoDNPM != null ? this.TaxaAnualPorHectare.ProcessoDNPM.Empresa : null;
                else if (this.RequerimentoLPTotal != null)
                    return this.RequerimentoLPTotal.ProcessoDNPM != null ? this.RequerimentoLPTotal.ProcessoDNPM.Empresa : null;
                else if (this.Extracao != null)
                    return this.Extracao.ProcessoDNPM != null ? this.Extracao.ProcessoDNPM.Empresa : null;
                else if (this.EntregaLicencaOuProtocolo != null)
                    return this.EntregaLicencaOuProtocolo.ProcessoDNPM != null ? this.EntregaLicencaOuProtocolo.ProcessoDNPM.Empresa : null;
                else if (this.Licenciamento != null)
                    return this.Licenciamento.ProcessoDNPM != null ? this.Licenciamento.ProcessoDNPM.Empresa : null;
                else if (this.RequerimentoImissaoPosse != null)
                    return this.RequerimentoImissaoPosse.ProcessoDNPM != null ? this.RequerimentoImissaoPosse.ProcessoDNPM.Empresa : null;
                else if (this.EntregaRelatorioAnual != null)
                    return this.EntregaRelatorioAnual.Empresa;
                else if (this.CertificadoRegularidade != null)
                    return this.CertificadoRegularidade.Empresa;
                else if (this.DIPEM != null)
                    return this.DIPEM.ProcessoDNPM != null ? this.DIPEM.ProcessoDNPM.Empresa : null;
                else if (this.LimiteRenuncia != null)
                    return this.LimiteRenuncia.ProcessoDNPM != null ? this.LimiteRenuncia.ProcessoDNPM.Empresa : null;
                else if (this.TaxaTrimestral != null)
                    return this.TaxaTrimestral.Empresa;
                else if (this.GetType() == typeof(VencimentoDiverso))
                {
                    VencimentoDiverso v = VencimentoDiverso.ConsultarPorId(this.Id);
                    if (v != null && v.Diverso != null && v.Diverso.Empresa != null)
                        return v.Diverso.Empresa;
                    else
                        return null;
                }
                else if (this.GetType() == typeof(VencimentoContratoDiverso))
                {
                    VencimentoContratoDiverso v = VencimentoContratoDiverso.ConsultarPorId(this.Id);
                    if (v != null && v.ContratoDiverso != null && v.ContratoDiverso.Empresa != null)
                        return v.ContratoDiverso.Empresa;
                    else if (v != null && v.Reajuste != null && v.Reajuste.Empresa != null)
                        return v.Reajuste.Empresa;
                    else
                        return null;
                }
                else return null;
            }
        }

        public virtual GrupoEconomico GetGrupoEconomico
        {
            get
            {
                return this.GetEmpresa != null ? this.GetEmpresa.GrupoEconomico : null;
            }
        }

        public virtual string GetNomeGrupoEconomico
        {
            get
            {
                return this.GetEmpresa != null ? this.GetEmpresa.GrupoEconomico.Nome : "Não definido";
            }
        }

        

        public virtual string GetDescricaoTipo
        {
            get
            {
                if (this.Condicional != null)
                {
                    if (this.Condicional.GetTipo == "Condicionante")
                    {
                        return "Licença: " + ((Condicionante)this.Condicional).Licenca.TipoLicenca.Sigla + ((Condicionante)this.Condicional).Licenca.Numero + " - Condicionante: " + this.Condicional.Numero + " - " + this.Condicional.Descricao;
                    }
                    else if (this.Condicional.GetTipo == "Outros Empresa")
                    {
                        return "Outros Empresa:" + ((OutrosEmpresa)this.Condicional).Numero;
                    }
                    else if (this.Condicional.GetTipo == "Outros Processo")
                    {
                        return "Outros Processo: " + ((OutrosProcesso)this.Condicional).Processo.Numero;
                    }
                    return this.Condicional.Descricao;
                }
                else if (this.Licenca != null)
                    return "Licença: " + this.Licenca.TipoLicenca.Sigla + this.Licenca.Numero + " - " + this.Licenca.Descricao;
                else if (this.Exigencia != null)
                    return "Exigência: " + this.Exigencia.Descricao;
                else if (this.Ral != null)
                    return "RAL do Processo: " + (this.Ral.ProcessoDNPM != null ? this.Ral.ProcessoDNPM.GetNumeroProcessoComMascara : "não definido");
                else if (this.GuiaDeUtilizacao != null)
                    return "Guia de utilização do Processo: " + this.GuiaDeUtilizacao.ProcessoDNPM != null ? this.GuiaDeUtilizacao.ProcessoDNPM.GetNumeroProcessoComMascara : "não definido";
                else if (this.RequerimentoLavraAlvaraPesquisa != null)
                    return "Processo:" + this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.GetNumeroProcessoComMascara + " - Alvara de Pesquisa: " + this.RequerimentoLavraAlvaraPesquisa.Numero;
                else if (this.NotificacaoPesquisaDNPM != null)
                    return "Processo: " + this.NotificacaoPesquisaDNPM.ProcessoDNPM.GetNumeroProcessoComMascara + " - Notificação de Pesquisa: " + this.NotificacaoPesquisaDNPM.Numero;
                else if (this.AlvaraPesquisa != null)
                    return "Processo: " + this.AlvaraPesquisa.ProcessoDNPM.GetNumeroProcessoComMascara + " - Alvara de Pesquisa: " + this.AlvaraPesquisa.Numero;
                else if (this.TaxaAnualPorHectare != null)
                    return "Processo: " + this.TaxaAnualPorHectare.ProcessoDNPM.GetNumeroProcessoComMascara + " - Taxa anual por hectare: " + this.TaxaAnualPorHectare.Numero;
                else if (this.RequerimentoLPTotal != null)
                    return "Processo: " + this.requerimentoLPTotal.ProcessoDNPM.GetNumeroProcessoComMascara + " - Requerimento LP Poligonal: " + this.RequerimentoLPTotal.Numero;
                else if (this.Extracao != null)
                    return "Processo: " + this.Extracao.ProcessoDNPM.GetNumeroProcessoComMascara + " - Extração: " + this.Extracao.NumeroLicenca;
                else if (this.EntregaLicencaOuProtocolo != null)
                    return "Processo: " + this.EntregaLicencaOuProtocolo.ProcessoDNPM.GetNumeroProcessoComMascara + " - Entrega em: " + this.EntregaLicencaOuProtocolo.DataPublicacao.ToShortDateString() + " - Licenciamento:" + this.EntregaLicencaOuProtocolo.Numero;
                else if (this.Licenciamento != null)
                    return "Processo: " + this.Licenciamento.ProcessoDNPM.GetNumeroProcessoComMascara + " - Licenciamento: " + this.Licenciamento.Numero;
                else if (this.RequerimentoImissaoPosse != null)
                    return "Processo: " + this.RequerimentoImissaoPosse.ProcessoDNPM.GetNumeroProcessoComMascara + " - Portaria de Lavra: " + this.RequerimentoImissaoPosse.NumeroPortariaLavra;
                else if (this.EntregaRelatorioAnual != null)
                    return "Licença: " + this.EntregaRelatorioAnual.NumeroLicenca + " - Ofício: " + this.EntregaRelatorioAnual.NumeroOficio;
                else if (this.CertificadoRegularidade != null)
                    return "Licença: " + this.CertificadoRegularidade.NumeroLicenca + " - Ofício: " + this.CertificadoRegularidade.NumeroOficio;
                else if (this.TaxaTrimestral != null)
                    return "Licença: " + this.TaxaTrimestral.NumeroLicenca + " - Ofício: " + this.TaxaTrimestral.NumeroOficio;
                else if (this.DIPEM != null)
                    return "Processo: " + this.DIPEM.ProcessoDNPM.GetNumeroProcessoComMascara + " - Alvara de Pesquisa: " + this.DIPEM.Numero;
                else if (this.LimiteRenuncia != null)
                    return "Processo: " + this.LimiteRenuncia.ProcessoDNPM.GetNumeroProcessoComMascara + " - Alvara de Pesquisa: " + this.LimiteRenuncia.Numero;
                else if (this.GetType() == typeof(VencimentoDiverso))
                {
                    VencimentoDiverso v = VencimentoDiverso.ConsultarPorId(this.Id);
                    if (v != null && v.Diverso != null && v.Diverso.Empresa != null)
                        return v.Diverso.Descricao;
                    else
                        return "";
                }
                else return string.Empty;
            }
        }

        public virtual string getDescricaoStatus
        {
            get
            {
                if (this.GetType() == typeof(VencimentoDiverso))
                {
                    VencimentoDiverso v = VencimentoDiverso.ConsultarPorId(this.Id);
                    if (v != null && v.Diverso != null && v.Diverso.Empresa != null && v.StatusDiverso != null)
                        return v.StatusDiverso.Nome;
                    else
                        return "";
                }
                else
                {
                    return this.Status != null ? this.Status.Nome : " - ";
                }
            }
        }

        public virtual Estado GetEstado
        {
            get
            {
                if (this.Condicional != null)
                {
                    if (this.Condicional.GetTipo == "Condicionante")
                    {
                        return ((Condicionante)this.Condicional).Licenca != null && ((Condicionante)this.Condicional).Licenca.Cidade != null && ((Condicionante)this.Condicional).Licenca.Cidade.Estado != null ? ((Condicionante)this.Condicional).Licenca.Cidade.Estado : null;
                    }
                }
                else if (this.Licenca != null)
                    return this.Licenca.Cidade != null && this.Licenca.Cidade.Estado != null ? this.Licenca.Cidade.Estado : null;
                else if (this.Exigencia != null)
                    return this.Exigencia.Regime != null && this.Exigencia.Regime.ProcessoDNPM != null && this.Exigencia.Regime.ProcessoDNPM.Cidade != null && this.Exigencia.Regime.ProcessoDNPM.Cidade.Estado != null ? this.Exigencia.Regime.ProcessoDNPM.Cidade.Estado : null;
                else if (this.Ral != null)
                    return this.Ral.ProcessoDNPM != null && this.Ral.ProcessoDNPM.Cidade != null && this.Ral.ProcessoDNPM.Cidade.Estado != null ? this.Ral.ProcessoDNPM.Cidade.Estado : null;
                else if (this.GuiaDeUtilizacao != null)
                    return this.GuiaDeUtilizacao.ProcessoDNPM != null && this.GuiaDeUtilizacao.ProcessoDNPM.Cidade != null && this.GuiaDeUtilizacao.ProcessoDNPM.Cidade.Estado != null ? this.GuiaDeUtilizacao.ProcessoDNPM.Cidade.Estado : null;
                else if (this.RequerimentoLavraAlvaraPesquisa != null)
                    return this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null && this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade != null && this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade.Estado != null ? this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade.Estado : null;
                else if (this.NotificacaoPesquisaDNPM != null)
                    return this.NotificacaoPesquisaDNPM.ProcessoDNPM != null && this.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade != null && this.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade.Estado != null ? this.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade.Estado : null;
                else if (this.AlvaraPesquisa != null)
                    return this.AlvaraPesquisa.ProcessoDNPM != null && this.AlvaraPesquisa.ProcessoDNPM.Cidade != null && this.AlvaraPesquisa.ProcessoDNPM.Cidade.Estado != null ? this.AlvaraPesquisa.ProcessoDNPM.Cidade.Estado : null;
                else if (this.TaxaAnualPorHectare != null)
                    return this.TaxaAnualPorHectare.ProcessoDNPM != null && this.TaxaAnualPorHectare.ProcessoDNPM.Cidade != null && this.TaxaAnualPorHectare.ProcessoDNPM.Cidade.Estado != null ? this.TaxaAnualPorHectare.ProcessoDNPM.Cidade.Estado : null;
                else if (this.RequerimentoLPTotal != null)
                    return this.requerimentoLPTotal.ProcessoDNPM != null && this.requerimentoLPTotal.ProcessoDNPM.Cidade != null && this.requerimentoLPTotal.ProcessoDNPM.Cidade.Estado != null ? this.requerimentoLPTotal.ProcessoDNPM.Cidade.Estado : null;
                else if (this.Extracao != null)
                    return this.Extracao.ProcessoDNPM != null && this.Extracao.ProcessoDNPM.Cidade != null && this.Extracao.ProcessoDNPM.Cidade.Estado != null ? this.Extracao.ProcessoDNPM.Cidade.Estado : null;
                else if (this.EntregaLicencaOuProtocolo != null)
                    return this.EntregaLicencaOuProtocolo.ProcessoDNPM != null && this.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade != null && this.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade.Estado != null ? this.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade.Estado : null;
                else if (this.Licenciamento != null)
                    return this.Licenciamento.ProcessoDNPM != null && this.Licenciamento.ProcessoDNPM.Cidade != null && this.Licenciamento.ProcessoDNPM.Cidade.Estado != null ? this.Licenciamento.ProcessoDNPM.Cidade.Estado : null;
                else if (this.RequerimentoImissaoPosse != null)
                    return this.RequerimentoImissaoPosse.ProcessoDNPM != null && this.RequerimentoImissaoPosse.ProcessoDNPM.Cidade != null && this.RequerimentoImissaoPosse.ProcessoDNPM.Cidade.Estado != null ? this.RequerimentoImissaoPosse.ProcessoDNPM.Cidade.Estado : null;
                else if (this.EntregaRelatorioAnual != null)
                    return null;
                else if (this.CertificadoRegularidade != null)
                    return null;
                else if (this.TaxaTrimestral != null)
                    return null;
                else if (this.DIPEM != null)
                    return this.DIPEM.ProcessoDNPM != null && this.DIPEM.ProcessoDNPM.Cidade != null && this.DIPEM.ProcessoDNPM.Cidade.Estado != null ? this.DIPEM.ProcessoDNPM.Cidade.Estado : null;
                else if (this.LimiteRenuncia != null)
                    return this.LimiteRenuncia.ProcessoDNPM != null && this.LimiteRenuncia.ProcessoDNPM.Cidade != null && this.LimiteRenuncia.ProcessoDNPM.Cidade.Estado != null ? this.LimiteRenuncia.ProcessoDNPM.Cidade.Estado : null;
                else if (this.GetType() == typeof(VencimentoDiverso))
                    return null;
                return null;
            }
        }

        public virtual ProrrogacaoPrazo GetUltimaProrrogacao
        {
            get
            {
                if (this.ProrrogacoesPrazo != null && this.ProrrogacoesPrazo.Count > 0)
                {
                    return this.ProrrogacoesPrazo[this.ProrrogacoesPrazo.Count - 1];
                }
                return null;
            }
        }

        public static IList<Vencimento> GetVencimentosAteOntemStatusDiferenteDeConcluido()
        {
            Vencimento aux = new Vencimento();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Between("Data", DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1)));            
            aux.AdicionarFiltro(Filtros.CriarAlias("Status", "stat"));
            aux.AdicionarFiltro(Filtros.Eq("stat.Id", 1));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Vencimento> lista = fabrica.GetDAOBase().ConsultarComFiltro<Vencimento>(aux);


            if (lista != null)
                lista = lista.Where(x => x.GetEmpresa != null && x.GetEmpresa.GrupoEconomico.Ativo && x.GetEmpresa.GrupoEconomico.AtivoLogus && x.GetEmpresa.GrupoEconomico.AtivoAmbientalis && !x.GetEmpresa.GrupoEconomico.Cancelado).ToList();

            return lista;
        }

        public virtual ModuloPermissao GetModulo
        {
            get
            {
                string nomeModulo = "";
                if (this.Condicional != null || this.Licenca != null || this.EntregaRelatorioAnual != null || this.CertificadoRegularidade != null || this.TaxaTrimestral != null)
                {
                    nomeModulo = "Meio Ambiente";
                }
                else if (this.Exigencia != null || this.Ral != null || this.GuiaDeUtilizacao != null || this.RequerimentoLavraAlvaraPesquisa != null || this.NotificacaoPesquisaDNPM != null || this.AlvaraPesquisa != null || this.TaxaAnualPorHectare != null || this.RequerimentoLPTotal != null || this.Extracao != null || this.EntregaLicencaOuProtocolo != null || this.Licenciamento != null || this.RequerimentoImissaoPosse != null || this.DIPEM != null || this.LimiteRenuncia != null)
                {
                    nomeModulo = "DNPM";
                }                
                else if (this.GetType() == typeof(VencimentoDiverso))
                {
                    nomeModulo = "Diversos";
                }
                else if (this.GetType() == typeof(VencimentoDiverso))
                {
                    nomeModulo = "Contratos";
                }
                
                return nomeModulo != "" ? ModuloPermissao.ConsultarPorNome(nomeModulo) : null;
            }
        }

        public virtual string GetNumeroProcessoVencimento 
        { 
            get
            {
                if (this.Condicional != null)
                {
                    if (this.Condicional.GetTipo == "Condicionante")
                    {
                        return ((Condicionante)this.Condicional) != null ? "Ambiental nº: " + ((Condicionante)this.Condicional).GetNumeroProcesso : "";
                    }
                    else if (this.Condicional.GetTipo == "Outros Empresa")
                    {
                        return ((OutrosEmpresa)this.Condicional) != null && ((OutrosEmpresa)this.Condicional).GetProcesso != null ? "Ambiental nº: " + ((OutrosEmpresa)this.Condicional).GetProcesso.Numero : "";
                    }
                    else if (this.Condicional.GetTipo == "Outros Processo")
                    {
                        return ((OutrosProcesso)this.Condicional) != null && ((OutrosProcesso)this.Condicional).Processo != null ? "Ambiental nº: " + ((OutrosProcesso)this.Condicional).Processo.Numero : "";
                    }
                    return this.Condicional.GetProcesso != null ? this.Condicional.GetProcesso.Numero : "";
                }
                else if (this.Licenca != null)
                    return this.Licenca.Processo != null ? "Ambiental nº: " + this.Licenca.Processo.Numero : "";
                else if (this.Exigencia != null)
                    return this.Exigencia != null && this.Exigencia.Regime != null && this.Exigencia.Regime.ProcessoDNPM != null ? "DNPM nº: " + this.Exigencia.Regime.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.Ral != null)
                    return this.Ral.ProcessoDNPM != null ? "DNPM nº: " + this.Ral.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.GuiaDeUtilizacao != null)
                    return this.GuiaDeUtilizacao.ProcessoDNPM != null ? "DNPM nº: " + this.GuiaDeUtilizacao.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.RequerimentoLavraAlvaraPesquisa != null)
                    return this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null ? "DNPM nº: " + this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.NotificacaoPesquisaDNPM != null)
                    return this.NotificacaoPesquisaDNPM.ProcessoDNPM != null ? "DNPM nº: " + this.NotificacaoPesquisaDNPM.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.AlvaraPesquisa != null)
                    return this.AlvaraPesquisa.ProcessoDNPM != null ? "DNPM nº: " + this.AlvaraPesquisa.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.TaxaAnualPorHectare != null)
                    return this.TaxaAnualPorHectare.ProcessoDNPM != null ? "DNPM nº: " + this.TaxaAnualPorHectare.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.RequerimentoLPTotal != null)
                    return this.requerimentoLPTotal.ProcessoDNPM != null ? "DNPM nº: " + this.requerimentoLPTotal.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.Extracao != null)
                    return this.Extracao.ProcessoDNPM != null ? "DNPM nº: " + this.Extracao.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.EntregaLicencaOuProtocolo != null)
                    return this.EntregaLicencaOuProtocolo.ProcessoDNPM != null ? "DNPM nº: " + this.EntregaLicencaOuProtocolo.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.Licenciamento != null)
                    return this.Licenciamento.ProcessoDNPM != null ? "DNPM nº: " + this.Licenciamento.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.RequerimentoImissaoPosse != null)
                    return this.RequerimentoImissaoPosse.ProcessoDNPM != null ? "DNPM nº: " + this.RequerimentoImissaoPosse.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.EntregaRelatorioAnual != null)
                    return "CTF - Entrega Relatório Anual";
                else if (this.CertificadoRegularidade != null)
                    return "CTF - Certificado Regularidade";
                else if (this.TaxaTrimestral != null)
                    return "CTF - Taxa Trimestral";
                else if (this.DIPEM != null)
                    return this.DIPEM.ProcessoDNPM != null ? "DNPM nº: " + this.DIPEM.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.LimiteRenuncia != null)
                    return this.LimiteRenuncia.ProcessoDNPM != null ? "DNPM nº: " + this.LimiteRenuncia.ProcessoDNPM.GetNumeroProcessoComMascara : "";
                else if (this.GetType() == typeof(VencimentoDiverso))
                {
                    VencimentoDiverso v = VencimentoDiverso.ConsultarPorId(this.Id);
                    if (v != null && v.Diverso != null && v.Diverso.Empresa != null)
                        return "Vencimento Diverso";
                    else
                        return "";
                }
                else return string.Empty;
            }
        }

        public virtual ProcessoDNPM GetProcessoDNPM
        {
            get
            {
                if (this.Exigencia != null)
                    return this.Exigencia != null && this.Exigencia.Regime != null && this.Exigencia.Regime.ProcessoDNPM != null ? this.Exigencia.Regime.ProcessoDNPM : null;
                else if (this.Ral != null)
                    return this.Ral.ProcessoDNPM != null ? this.Ral.ProcessoDNPM : null;
                else if (this.GuiaDeUtilizacao != null)
                    return this.GuiaDeUtilizacao.ProcessoDNPM != null ? this.GuiaDeUtilizacao.ProcessoDNPM : null;
                else if (this.RequerimentoLavraAlvaraPesquisa != null)
                    return this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null ? this.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM : null;
                else if (this.NotificacaoPesquisaDNPM != null)
                    return this.NotificacaoPesquisaDNPM.ProcessoDNPM != null ? this.NotificacaoPesquisaDNPM.ProcessoDNPM : null;
                else if (this.AlvaraPesquisa != null)
                    return this.AlvaraPesquisa.ProcessoDNPM != null ? this.AlvaraPesquisa.ProcessoDNPM : null;
                else if (this.TaxaAnualPorHectare != null)
                    return this.TaxaAnualPorHectare.ProcessoDNPM != null ? this.TaxaAnualPorHectare.ProcessoDNPM : null;
                else if (this.RequerimentoLPTotal != null)
                    return this.requerimentoLPTotal.ProcessoDNPM != null ? this.requerimentoLPTotal.ProcessoDNPM : null;
                else if (this.Extracao != null)
                    return this.Extracao.ProcessoDNPM != null ? this.Extracao.ProcessoDNPM : null;
                else if (this.EntregaLicencaOuProtocolo != null)
                    return this.EntregaLicencaOuProtocolo.ProcessoDNPM != null ? this.EntregaLicencaOuProtocolo.ProcessoDNPM : null;
                else if (this.Licenciamento != null)
                    return this.Licenciamento.ProcessoDNPM != null ? this.Licenciamento.ProcessoDNPM : null;
                else if (this.RequerimentoImissaoPosse != null)
                    return this.RequerimentoImissaoPosse.ProcessoDNPM != null ? this.RequerimentoImissaoPosse.ProcessoDNPM : null;
                else if (this.DIPEM != null)
                    return this.DIPEM.ProcessoDNPM != null ? this.DIPEM.ProcessoDNPM : null;
                else if (this.LimiteRenuncia != null)
                    return this.LimiteRenuncia.ProcessoDNPM != null ? this.LimiteRenuncia.ProcessoDNPM : null;

                return null;
            }
        }

        public virtual Processo GetProcessoMeioAmbiente 
        {
            get 
            {
                if (this.Condicional != null)
                {
                    if (this.Condicional.GetTipo == "Condicionante")
                    {
                        return ((Condicionante)this.Condicional) != null && ((Condicionante)this.Condicional).Licenca != null && ((Condicionante)this.Condicional).Licenca.Processo != null ? ((Condicionante)this.Condicional).Licenca.Processo : null;
                    }
                    else if (this.Condicional.GetTipo == "Outros Processo")
                    {
                        return ((OutrosProcesso)this.Condicional) != null && ((OutrosProcesso)this.Condicional).Processo != null ? ((OutrosProcesso)this.Condicional).Processo : null;
                    }
                    return this.Condicional.GetProcesso != null ? this.Condicional.GetProcesso : null;
                }
                else if (this.Licenca != null)
                    return this.Licenca.Processo != null ? this.Licenca.Processo : null;

                return null;
            }
        }

        public virtual OutrosEmpresa GetOutroEmpresa
        {
            get
            {
                if (this.Condicional != null)
                {
                    if (this.Condicional.GetTipo == "Outros Empresa")
                    {
                        return ((OutrosEmpresa)this.Condicional) != null ? ((OutrosEmpresa)this.Condicional) : null;
                    }
                }                

                return null;
            }
        }

        public virtual CadastroTecnicoFederal GetCadastroTecnico
        {
            get
            {
                if (this.EntregaRelatorioAnual != null)
                    return this.EntregaRelatorioAnual;
                else if (this.CertificadoRegularidade != null)
                    return this.CertificadoRegularidade;
                else if (this.TaxaTrimestral != null)
                    return this.TaxaTrimestral;

                return null;
            }
        }

        public virtual string GetTipoProcessoMeioAmbiente
        {
            get
            {
                if (this.Condicional != null)
                {
                    if (this.Condicional.GetTipo == "Condicionante")
                    {
                        return "Processo";
                    }
                    else if (this.Condicional.GetTipo == "Outros Empresa")
                    {
                        return "OutrosEmpresa";
                    }
                    else if (this.Condicional.GetTipo == "Outros Processo")
                    {
                        return "Processo";
                    }
                    return "Processo";
                }
                else if (this.Licenca != null)
                    return "Processo";
                else if (this.EntregaRelatorioAnual != null)
                    return "Cadastro";
                else if (this.CertificadoRegularidade != null)
                    return "Cadastro";
                else if (this.TaxaTrimestral != null)
                    return "Cadastro";

                return "";
            }
        }

        public virtual string GetNomeEmpresa
        {
            get
            {
                return this.GetEmpresa != null ? this.GetEmpresa.Nome : "Não definido";
            }
        }

        public virtual string GetEhPeriodico
        {
            get
            {
                return this.Periodico ? "Sim" : "Não";
            }
        }

        public virtual string GetProrrogacoesPrazo
        {
            get
            {
                return this.ProrrogacoesPrazo != null && this.ProrrogacoesPrazo.Count > 0 ? this.ProrrogacoesPrazo.Count.ToString() : "0";
            }
        }

        public virtual string GetSiglaEstado
        {
            get
            {
                return this.GetEstado != null ? this.GetEstado.PegarSiglaEstado() : "--";
            }
        }

        public static IList<Vencimento> ConsultarParaAtualizar(int ini, int fim)
        {
            Vencimento aux = new Vencimento();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.MaiorOuIgual("Id", ini));
            aux.AdicionarFiltro(Filtros.MenorOuIgual("Id", fim));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Vencimento>(aux);
        }
    }
}
