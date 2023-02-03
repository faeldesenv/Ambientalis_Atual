using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "diversos", Name = "Modelo.Diverso, Modelo")]
    public partial class Diverso: ObjetoBase
    {
        #region ________Atributos_______

        private string descricao;
        private TipoDiverso tipoDiverso;        
        private IList<VencimentoDiverso> vencimentosDiversos;        
        private Empresa empresa;
        private IList<Historico> historicos;
        private IList<ArquivoFisico> arquivos;
        private string detalhamento;
        private IList<Usuario> usuariosEdicao;
        private IList<Usuario> usuariosVisualizacao;                        

        #endregion

        public Diverso(int id) { this.Id = id; }
        public Diverso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Diverso() { }

        #region ________Propriedades_________

        [Property(Type = "StringClob")]
        [Column(1, Name = "detalhamento", SqlType = "nvarchar(max)")]
        public virtual string Detalhamento
        {
            get { return detalhamento; }
            set { detalhamento = value; }
        }

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [ManyToOne(Name = "TipoDiverso", Column = "id_tipo_diverso", Class = "Modelo.TipoDiverso, Modelo")]
        public virtual TipoDiverso TipoDiverso
        {
            get { return tipoDiverso; }
            set { tipoDiverso = value; }
        }

        [Bag(Name = "VencimentosDiversos", Table = "vencimentos_diversos", Cascade = "delete")]
        [Key(2, Column = "id_diverso")]
        [OneToMany(3, Class = "Modelo.VencimentoDiverso, Modelo")]
        public virtual IList<VencimentoDiverso> VencimentosDiversos
        {
            get { return vencimentosDiversos; }
            set { vencimentosDiversos = value; }
        }

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo")]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_diverso")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_diverso")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Bag(Table = "usuarios_edicao_diversos")]
        [Key(2, Column = "id_diverso")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        }        

        [Bag(Table = "usuarios_visualizacao_diversos")]
        [Key(2, Column = "id_diverso")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }

        #endregion
    }
}
