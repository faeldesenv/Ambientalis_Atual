using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "contratos_comerciais", Name = "Modelo.ContratoComercial, Modelo")]
    public partial class ContratoComercial: ObjetoBase
    {
        #region _______Atributos_______

        private int numero;        
        private int ano;        
        private string texto;        
        private bool aditamento;
        private bool aceito;        
        private decimal comissao;
        private Revenda revenda;
        private bool desativado;

        #endregion

        public ContratoComercial(int id) { this.Id = id; }
        public ContratoComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ContratoComercial() { }

        #region _______Propriedades_______

        [Property(Column="numero")]
        public virtual int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "ano")]
        public virtual int Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        [Property(Type="StringClob")]
        [Column(1, Name = "texto", SqlType = "nvarchar(max)")]
        public virtual string Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        [Property(Type = "TrueFalse", Column = "aditamento")]
        public virtual bool Aditamento
        {
            get { return aditamento; }
            set { aditamento = value; }
        }

        [Property(Type = "TrueFalse", Column = "aceito")]
        public virtual bool Aceito
        {
            get { return aceito; }
            set { aceito = value; }
        }

        [Property(Column = "comissao")]
        public virtual decimal Comissao
        {
            get { return comissao; }
            set { comissao = value; }
        }

        [ManyToOne(Name = "Revenda", Column = "id_revenda", Class = "Modelo.Revenda, Modelo")]
        public virtual Revenda Revenda
        {
            get { return revenda; }
            set { revenda = value; }
        }

        [Property(Type = "TrueFalse", Column = "desativado")]
        public virtual bool Desativado
        {
            get { return desativado; }
            set { desativado = value; }
        }

        #endregion
    }
}
