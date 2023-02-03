using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using System;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "extracoes", Extends = "Modelo.Regime, Modelo", Name = "Modelo.Extracao, Modelo")]
    [Key(Column = "id")]
    public partial class Extracao : Regime
    {
        #region ___________ Atributos ___________

        private IList<Vencimento> vencimentos;
        private Vencimento prazoLicencaAmbiental;
        private DateTime dataPublicacao;
        private string numeroLicenca;
        private string numeroExtracao;
        private DateTime validadeLicenca;
        private DateTime dataAbertura;

        #endregion

        public Extracao(int id) { this.Id = id; }
        public Extracao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Extracao() { }

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

        [Property(Column = "data_abertura")]
        public virtual DateTime DataAbertura
        {
            get
            {
                if (dataAbertura <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAbertura;
            }
            set { dataAbertura = value; }
        }

        [Property(Column = "validade_licenca")]
        public virtual DateTime ValidadeLicenca
        {
            get
            {
                if (validadeLicenca <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return validadeLicenca;
            }
            set { validadeLicenca = value; }
        }

        [Property(Column = "numero_licenca")]
        public virtual string NumeroLicenca
        {
            get { return numeroLicenca; }
            set { numeroLicenca = value; }
        }

        [Property(Column = "numero_extracao")]
        public virtual string NumeroExtracao
        {
            get { return numeroExtracao; }
            set { numeroExtracao = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade="delete")]
        [Key(2, Column = "id_extracao")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

    }
}
