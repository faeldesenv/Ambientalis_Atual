using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{    
    [Serializable]
    [Class(Table = "aditivos_contratos", Name = "Modelo.AditivoContrato, Modelo")]
    public partial class AditivoContrato :ObjetoBase
    {
        #region ________Atributos________

        private ContratoDiverso contratoDiverso;
        private DateTime dataAssinatura;
        private string numero;
        private string motivo;
        private bool prorrogouVencimento;
        private bool prorrogouReajuste;
        private DateTime dataVencimento;
        private IList<ArquivoFisico> arquivosFisicos;
        private DateTime dataReajuste;

        #endregion

        public AditivoContrato(int id) { this.Id = id; }
        public AditivoContrato(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public AditivoContrato() { }

        #region ________Propriedades________

        [ManyToOne(Name = "ContratoDiverso", Column = "id_contrato_diverso", Class = "Modelo.ContratoDiverso, Modelo", Lazy = Laziness.False)]
        public virtual ContratoDiverso ContratoDiverso
        {
            get { return contratoDiverso; }
            set { contratoDiverso = value; }
        }

        [Property(Column = "data_assinatura")]
        public virtual DateTime DataAssinatura
        {
            get
            {
                if (dataAssinatura <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAssinatura;
            }
            set { dataAssinatura = value; }
        }

        [Bag(Name = "ArquivosFisicos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_aditivo")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> ArquivosFisicos
        {
            get { return arquivosFisicos; }
            set { arquivosFisicos = value; }
        }

        [Property(Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "motivo")]
        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        [Property(Type = "TrueFalse", Column = "prorrogou_reajuste")]
        public virtual bool ProrrogouReajuste
        {
            get { return prorrogouReajuste; }
            set { prorrogouReajuste = value; }
        }

        [Property(Type = "TrueFalse", Column = "prorrogou_vencimento")]
        public virtual bool ProrrogouVencimento
        {
            get { return prorrogouVencimento; }
            set { prorrogouVencimento = value; }
        }

        [Property(Column = "data_vencimento")]
        public virtual DateTime DataVencimento
        {
            get
            {
                if (dataVencimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataVencimento;
            }
            set { dataVencimento = value; }
        }

        [Property(Column = "data_reajuste")]
        public virtual DateTime DataReajuste
        {
            get
            {
                if (dataReajuste <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataReajuste;
            }
            set { dataReajuste = value; }
        }

        #endregion
    }
}
