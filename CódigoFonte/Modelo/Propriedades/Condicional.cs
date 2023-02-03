using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "condicionais", Name = "Modelo.Condicional, Modelo")]
    public partial class Condicional : ObjetoBase
    {
        public const int VencimentoGeral = 1;
        public const int VencimentoProcesso = 2;

        #region ___________ Atributos ___________

        private string numero;
        private string descricao;
        private string observacoes;
        private int diasPrazo;
        private IList<Vencimento> vencimentos;
        private IList<ArquivoFisico> arquivos;
        private IList<Historico> historicos;

        #endregion

        public Condicional(int id) { this.Id = id; }
        public Condicional(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Condicional() { }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_condicional")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_condicional")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_condicional")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

        [Property(Column = "dias_prazo")]
        public virtual int DiasPrazo
        {
            get { return diasPrazo; }
            set { diasPrazo = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "observacoes")]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [Property(Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

    }
}
