using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "contratos", Name = "Modelo.Contrato, Modelo")]
    public partial class Contrato : ObjetoBase
    {
        #region ________Atributos________

        private int numeroContrato;
        private int carencia;
        private decimal mensalidade;
        private string anoContrato;
        private string textoContrato;
        private bool aditamento;
        private GrupoEconomico grupoEconomico;
        private DateTime dataAceite;

        #endregion

        public Contrato(int id) { this.Id = id; }
        public Contrato(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Contrato() { }

        #region ________Propriedades________

        [Property(Column = "mensalidade")]
        public virtual decimal Mensalidade
        {
            get { return mensalidade; }
            set { mensalidade = value; }
        }

        [Property(Column = "carencia")]
        public virtual int Carencia
        {
            get { return carencia; }
            set { carencia = value; }
        }

        [Property(Column = "numero_contrato")]
        public virtual int NumeroContrato
        {
            get { return numeroContrato; }
            set { numeroContrato = value; }
        }

        [Property(Column = "data_aceite")]
        public virtual DateTime DataAceite
        {
            get
            {
                if (dataAceite <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAceite;
            }
            set { dataAceite = value; }
        }

        [Property(Column = "ano_contrato")]
        public virtual string AnoContrato
        {
            get { return anoContrato; }
            set { anoContrato = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "texto_contrato")]
        public virtual string TextoContrato
        {
            get { return textoContrato; }
            set { textoContrato = value; }
        }

        [Property(Type = "TrueFalse", Column = "aditamento")]
        public virtual bool Aditamento
        {
            get { return aditamento; }
            set { aditamento = value; }
        }

        [ManyToOne(Name = "GrupoEconomico", Column = "id_grupo_economico", Class = "Modelo.GrupoEconomico, Modelo")]
        public virtual GrupoEconomico GrupoEconomico
        {
            get { return grupoEconomico; }
            set { grupoEconomico = value; }
        }

        #endregion
    }
}
