using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "outros_empresas", Extends = "Modelo.Condicional, Modelo", Name = "Modelo.OutrosEmpresa, Modelo")]
    [Key(Column = "id")]
    public partial class OutrosEmpresa : Condicional
    {
        #region ___________ Atributos ___________

        private Consultora consultora;
        private Empresa empresa;
        private OrgaoAmbiental orgaoAmbiental;
        private DateTime dataRecebimento;
        private IList<Usuario> usuariosEdicao;
        private IList<Usuario> usuariosVisualizacao;        

        #endregion

        public OutrosEmpresa(int id) { this.Id = id; }
        public OutrosEmpresa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OutrosEmpresa() { }

        [ManyToOne(Name = "Consultora", Column = "id_consultora", Class = "Modelo.Consultora, Modelo")]
        public virtual Consultora Consultora
        {
            get { return consultora; }
            set { consultora = value; }
        }

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo")]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [ManyToOne(Name = "OrgaoAmbiental", Column = "id_orgao_ambiental", Class = "Modelo.OrgaoAmbiental, Modelo")]
        public virtual OrgaoAmbiental OrgaoAmbiental
        {
            get { return orgaoAmbiental; }
            set { orgaoAmbiental = value; }
        }

        [Property(Column = "data_recebimento")]
        public virtual DateTime DataRecebimento
        {
            get
            {
                if (dataRecebimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataRecebimento;
            }
            set { dataRecebimento = value; }
        }

        [Bag(Table = "usuarios_edicao_outros_empresa")]
        [Key(2, Column = "id_outros_empresa")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        }            

        [Bag(Table = "usuarios_visualizacao_outros_empresa")]
        [Key(2, Column = "id_outros_empresa")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }
    }
}
