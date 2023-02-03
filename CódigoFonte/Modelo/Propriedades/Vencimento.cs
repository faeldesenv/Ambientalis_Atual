using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "vencimentos", Name = "Modelo.Vencimento, Modelo")]
    public partial class Vencimento : ObjetoBase
    {
        #region ___________ Atributos ___________

        private DateTime data;
        private bool periodico;       
        private IList<Notificacao> notificacoes;
        private Condicional condicional;
        private Licenca licenca;
        private Exigencia exigencia;
        private RAL ral;
        private GuiaUtilizacao guia;
        private AlvaraPesquisa requerimentoLavraAlvaraPesquisa;
        private AlvaraPesquisa limiteRenuncia;
        private AlvaraPesquisa notificacaoPesquisaDNPM;
        private AlvaraPesquisa alvaraPesquisa;
        private AlvaraPesquisa taxaAnualPorHectare;
        private AlvaraPesquisa dipem;
        private AlvaraPesquisa requerimentoLPTotal;
        private Extracao extracao;
        private Licenciamento entregaLicencaOuProtocolo;
        private Licenciamento licenciamento;
        private ConcessaoLavra requerimentoImissaoPosse;
        private CadastroTecnicoFederal entregaRelatorioAnual;
        private CadastroTecnicoFederal certificadoRegularidade;
        private CadastroTecnicoFederal taxaTrimestral;
        private Status status;
        private string protocolo;
        private DateTime dataAtendimento;
        private IList<ProrrogacaoPrazo> prorrogacoes;

        #endregion

        public Vencimento(int id) { this.Id = id; }
        public Vencimento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Vencimento() { }

      

        [ManyToOne(Name = "EntregaRelatorioAnual", Column = "id_ctf_entrega_relatorio_anual", Class = "Modelo.CadastroTecnicoFederal, Modelo")]
        public virtual CadastroTecnicoFederal EntregaRelatorioAnual
        {
            get { return entregaRelatorioAnual; }
            set { entregaRelatorioAnual = value; }
        }

        [ManyToOne(Name = "CertificadoRegularidade", Column = "id_ctf_certificado_regularidade", Class = "Modelo.CadastroTecnicoFederal, Modelo")]
        public virtual CadastroTecnicoFederal CertificadoRegularidade
        {
            get { return certificadoRegularidade; }
            set { certificadoRegularidade = value; }
        }

        [ManyToOne(Name = "TaxaTrimestral", Column = "id_ctf_taxa_trimestral", Class = "Modelo.CadastroTecnicoFederal, Modelo")]
        public virtual CadastroTecnicoFederal TaxaTrimestral
        {
            get { return taxaTrimestral; }
            set { taxaTrimestral = value; }
        }

        [OneToOne(Name = "RequerimentoImissaoPosse", Class = "Modelo.ConcessaoLavra", PropertyRef = "RequerimentoImissaoPosse")]
        public virtual ConcessaoLavra RequerimentoImissaoPosse
        {
            get { return requerimentoImissaoPosse; }
            set { requerimentoImissaoPosse = value; }
        }

        [ManyToOne(Name = "Ral", Column = "id_ral", Class = "Modelo.RAL, Modelo")]
        public virtual RAL Ral
        {
            get { return ral; }
            set { ral = value; }
        }

        [ManyToOne(Name = "GuiaDeUtilizacao", Column = "id_guia_utilizacao", Class = "Modelo.GuiaUtilizacao, Modelo")]
        public virtual GuiaUtilizacao GuiaDeUtilizacao
        {
            get { return guia; }
            set { guia = value; }
        }

        [OneToOne(Name = "EntregaLicencaOuProtocolo", Class = "Modelo.Licenciamento", PropertyRef = "EntregaLicencaOuProtocolo")]
        public virtual Licenciamento EntregaLicencaOuProtocolo
        {
            get { return entregaLicencaOuProtocolo; }
            set { entregaLicencaOuProtocolo = value; }
        }

        [ManyToOne(Name = "Licenciamento", Column = "id_licenciamento", Class = "Modelo.Licenciamento, Modelo")]
        public virtual Licenciamento Licenciamento
        {
            get { return licenciamento; }
            set { licenciamento = value; }
        }

        [ManyToOne(Name = "Extracao", Column = "id_extracao", Class = "Modelo.Extracao, Modelo")]
        public virtual Extracao Extracao
        {
            get { return extracao; }
            set { extracao = value; }
        }

        [OneToOne(Name = "RequerimentoLavraAlvaraPesquisa", Class = "Modelo.AlvaraPesquisa", PropertyRef = "RequerimentoLavra")]
        public virtual AlvaraPesquisa RequerimentoLavraAlvaraPesquisa
        {
            get { return requerimentoLavraAlvaraPesquisa; }
            set { requerimentoLavraAlvaraPesquisa = value; }
        }

        [OneToOne(Name = "NotificacaoPesquisaDNPM", Class = "Modelo.AlvaraPesquisa", PropertyRef = "NotificacaoPesquisaDNPM")]
        public virtual AlvaraPesquisa NotificacaoPesquisaDNPM
        {
            get { return notificacaoPesquisaDNPM; }
            set { notificacaoPesquisaDNPM = value; }
        }

        [OneToOne(Name = "AlvaraPesquisa", Class = "Modelo.AlvaraPesquisa", PropertyRef = "Vencimento")]
        public virtual AlvaraPesquisa AlvaraPesquisa
        {
            get { return alvaraPesquisa; }
            set { alvaraPesquisa = value; }
        }

        [OneToOne(Name = "LimiteRenuncia", Class = "Modelo.AlvaraPesquisa", PropertyRef = "LimiteRenuncia")]
        public virtual AlvaraPesquisa LimiteRenuncia
        {
            get { return limiteRenuncia; }
            set { limiteRenuncia = value; }
        }

        [ManyToOne(Name = "TaxaAnualPorHectare", Column = "id_alvara_pesquisa", Class = "Modelo.AlvaraPesquisa, Modelo")]
        public virtual AlvaraPesquisa TaxaAnualPorHectare
        {
            get { return taxaAnualPorHectare; }
            set { taxaAnualPorHectare = value; }
        }

        [ManyToOne(Name = "DIPEM", Column = "id_dipem", Class = "Modelo.AlvaraPesquisa, Modelo")]
        public virtual AlvaraPesquisa DIPEM
        {
            get { return dipem; }
            set { dipem = value; }
        }

        [OneToOne(Name = "RequerimentoLPTotal", Class = "Modelo.AlvaraPesquisa", PropertyRef = "RequerimentoLPTotal")]
        public virtual AlvaraPesquisa RequerimentoLPTotal
        {
            get { return requerimentoLPTotal; }
            set { requerimentoLPTotal = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get
            {
                if (data <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return data;
            }
            set { data = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Periodico
        {
            get { return periodico; }
            set { periodico = value; }
        }      

        [Bag(Name = "Notificacoes", Table = "notificacoes", Cascade = "delete")]
        [Key(2, Column = "id_vencimento")]
        [OneToMany(3, Class = "Modelo.Notificacao, Modelo")]
        public virtual IList<Notificacao> Notificacoes
        {
            get { return notificacoes; }
            set { notificacoes = value; }
        }

        [Bag(Name = "ProrrogacoesPrazo", Table = "prorrogacoes", Cascade = "delete")]
        [Key(2, Column = "id_vencimento")]
        [OneToMany(3, Class = "Modelo.ProrrogacaoPrazo, Modelo")]
        public virtual IList<ProrrogacaoPrazo> ProrrogacoesPrazo
        {
            get { return prorrogacoes; }
            set { prorrogacoes = value; }
        }

        [ManyToOne(Name = "Condicional", Column = "id_condicional", Class = "Modelo.Condicional, Modelo", Lazy = Laziness.False)]
        public virtual Condicional Condicional
        {
            get { return condicional; }
            set { condicional = value; }
        }

        [ManyToOne(Name = "Licenca", Column = "id_licenca", Class = "Modelo.Licenca, Modelo")]
        public virtual Licenca Licenca
        {
            get { return licenca; }
            set { licenca = value; }
        }

        [ManyToOne(Name = "Exigencia", Column = "id_exigencia", Class = "Modelo.Exigencia, Modelo")]
        public virtual Exigencia Exigencia
        {
            get { return exigencia; }
            set { exigencia = value; }
        }

        [ManyToOne(Name = "Status", Column = "id_status", Class = "Modelo.Status, Modelo", Lazy = Laziness.False)]
        public virtual Status Status
        {
            get { return status; }
            set { status = value; }
        }

        [Property(Column = "protocolo")]
        public virtual string ProtocoloAtendimento
        {
            get { return protocolo; }
            set { protocolo = value; }
        }

        [Property(Column = "data_atendimento")]
        public virtual DateTime DataAtendimento
        {
            get
            {
                if (dataAtendimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAtendimento;
            }
            set { dataAtendimento = value; }
        }

    }
}
