using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "setores", Name = "Modelo.Setor, Modelo")]
    public partial class Setor : ObjetoBase
    {
        #region ________Atributos________

        private string nome;
        private IList<ContratoDiverso> contratosDiversos;
        private IList<Usuario> usuariosEdicao;               
        private IList<Usuario> usuariosVisualizacao;        

        #endregion

        public Setor(int id) { this.Id = id; }
        public Setor(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Setor() { }

        #region ________Propriedades________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_setor")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        [Bag(Table = "usuarios_edicao_setores")]
        [Key(2, Column = "id_setor")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        }

        [Bag(Table = "usuarios_visualizacao_setores")]
        [Key(2, Column = "id_setor")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }

        #endregion
    }
}
