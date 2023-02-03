using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "prospectos", Extends = "Modelo.PessoaComercial, Modelo", Name = "Modelo.Prospecto, Modelo")]
    [Key(Column = "id")]
    public partial class Prospecto : PessoaComercial
    {
        #region ________Atributos_________

        private DateTime dataCadastro;
        private Revenda revenda;
        private IList<Interacao> interacoes;
        private Venda venda;

        #endregion

        public Prospecto(int id) { this.Id = id; }
        public Prospecto(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Prospecto() { }

        #region ________Propriedades_________

        [Property(Column = "data_cadastro")]
        public virtual DateTime DataCadastro
        {
            get
            {
                if (dataCadastro <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataCadastro;
            }
            set { dataCadastro = value; }
        }

        [ManyToOne(Name = "Revenda", Column = "id_revenda", Class = "Modelo.Revenda, Modelo")]
        public virtual Revenda Revenda
        {
            get { return revenda; }
            set { revenda = value; }
        }

        [Bag(Name = "Interacoes", Table = "interacoes", Cascade = "delete")]
        [Key(2, Column = "id_prospecto")]
        [OneToMany(3, Class = "Modelo.Interacao, Modelo")]
        public virtual IList<Interacao> Interacoes
        {
            get { return interacoes; }
            set { interacoes = value; }
        }

        [OneToOne(PropertyRef = "Prospecto", Class = "Modelo.Venda, Modelo", Cascade = "delete")]
        public virtual Venda Venda
        {
            get { return venda; }
            set { venda = value; }
        }

        #endregion
    }
}
