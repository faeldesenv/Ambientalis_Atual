using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using System;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "licenciamentos", Extends = "Modelo.Regime, Modelo", Name = "Modelo.Licenciamento, Modelo")]
    [Key(Column = "id")]
    public partial class Licenciamento : Regime
    {
        #region ___________ Atributos ___________

        private DateTime dataValidade;
        private String numero;
        private DateTime dataPublicacao;
        private Vencimento entregaLicencaOuProtocolo;
        private IList<Vencimento> vencimentos;
        private IList<Licenca> licencas;
        private Boolean possuePAE;
        private DateTime dataAbertura;

        #endregion

        public Licenciamento(int id) { this.Id = id; }
        public Licenciamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Licenciamento() { }

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

        [Property(Column = "data_validade")]
        public virtual DateTime DataValidade
        {
            get
            {
                if (dataValidade <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataValidade;
            }
            set { dataValidade = value; }
        }

        [Property(Column = "numero")]
        public virtual String Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool PossuePAE
        {
            get { return possuePAE; }
            set { possuePAE = value; }
        }

        [ManyToOne(Name = "EntregaLicencaOuProtocolo", Column = "id_entrega_licenca_ou_protocolo", Class = "Modelo.Vencimento, Modelo", Cascade = "delete")]
        public virtual Vencimento EntregaLicencaOuProtocolo
        {
            get { return entregaLicencaOuProtocolo; }
            set { entregaLicencaOuProtocolo = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_licenciamento")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

        [Bag(Name = "Licencas", Table = "licencas", Cascade = "save-update", Inverse = false)]
        [Key(2, Column = "id_licenciamento")]
        [OneToMany(3, Class = "Modelo.Licenca, Modelo")]
        public virtual IList<Licenca> Licencas
        {
            get { return licencas; }
            set { licencas = value; }
        }

    }
}
