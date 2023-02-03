using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "alvaras_pesquisas", Name = "Modelo.AlvaraPesquisa, Modelo", Extends = "Modelo.AutorizacaoPesquisa, Modelo")]
    [Key(Column = "id")]
    public partial class AlvaraPesquisa : AutorizacaoPesquisa
    {
        #region ________ Atributos ___________

        private DateTime dataPublicacao;
        private int anosValidade;
        private DateTime dataEntregaRelatorio;
        private DateTime dataAprovacaoRelatorio;
        private Vencimento limiteRenuncia;
        private Vencimento requerimentoLavra;
        private Vencimento notificacaoPesquisaDNPM;
        private Vencimento vencimento;
        private IList<Vencimento> taxaAnualPorHectare;
        private IList<Vencimento> dipem;
        private Vencimento requerimentoLPTotal;
        private string numero;

        #endregion

        #region ________ Construtores ________

        public AlvaraPesquisa(int id) { this.Id = id; }
        public AlvaraPesquisa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public AlvaraPesquisa() { }

        #endregion

        #region ________ Propriedades ________

        [ManyToOne(Name = "RequerimentoLavra", Column = "id_requerimento_lavra", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento RequerimentoLavra
        {
            get { return requerimentoLavra; }
            set { requerimentoLavra = value; }
        }

        [ManyToOne(Name = "NotificacaoPesquisaDNPM", Column = "id_notificacao_pesquisa", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento NotificacaoPesquisaDNPM
        {
            get { return notificacaoPesquisaDNPM; }
            set { notificacaoPesquisaDNPM = value; }
        }

        [ManyToOne(Name = "Vencimento", Column = "id_vencimento", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento Vencimento
        {
            get { return vencimento; }
            set { vencimento = value; }
        }

        [ManyToOne(Name = "LimiteRenuncia", Column = "id_limite_renuncia", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento LimiteRenuncia
        {
            get { return limiteRenuncia; }
            set { limiteRenuncia = value; }
        }

        [Bag(Name = "TaxaAnualPorHectare", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_alvara_pesquisa")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> TaxaAnualPorHectare
        {
            get { return taxaAnualPorHectare; }
            set { taxaAnualPorHectare = value; }
        }

        [Bag(Name = "DIPEM", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_dipem")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> DIPEM
        {
            get { return dipem; }
            set { dipem = value; }
        }

        [ManyToOne(Name = "RequerimentoLPTotal", Column = "id_lp_total", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento RequerimentoLPTotal
        {
            get { return requerimentoLPTotal; }
            set { requerimentoLPTotal = value; }
        }

        [Property(Column = "data_aprovacao_relatorio")]
        public virtual DateTime DataAprovacaoRelatorio
        {
            get
            {
                if (dataAprovacaoRelatorio <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAprovacaoRelatorio;
            }
            set { dataAprovacaoRelatorio = value; }
        }

        [Property(Column = "data_entrega_relatorio")]
        public virtual DateTime DataEntregaRelatorio
        {
            get
            {
                if (dataEntregaRelatorio <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataEntregaRelatorio;
            }
            set { dataEntregaRelatorio = value; }
        }

        [Property(Column = "anos_validade")]
        public virtual int AnosValidade
        {
            get { return anosValidade; }
            set { anosValidade = value; }
        }

        [Property(Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "data_publicacao")]
        public virtual DateTime DataPublicacao
        {
            get
            {
                if (dataPublicacao <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataPublicacao;
            }
            set { dataPublicacao = value; }
        }

        #endregion
    }
}
