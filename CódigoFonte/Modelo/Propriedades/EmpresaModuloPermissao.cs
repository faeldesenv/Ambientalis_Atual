using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "empresas_modulos_permissoes", Name = "Modelo.EmpresaModuloPermissao, Modelo")]
    public partial class EmpresaModuloPermissao: ObjetoBase
    {
        #region ________Atributos________

        private Empresa empresa;
        private ModuloPermissao moduloPermissao;
        private IList<Usuario> usuariosEdicao;
        private IList<Usuario> usuariosVisualizacao;              

        #endregion

        #region ________Propriedades________

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo")]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [ManyToOne(Name = "ModuloPermissao", Column = "id_modulo_permissao", Class = "Modelo.ModuloPermissao, Modelo")]
        public virtual ModuloPermissao ModuloPermissao
        {
            get { return moduloPermissao; }
            set { moduloPermissao = value; }
        }

        [Bag(Table = "usuarios_edicao_empresas_modulos_permissoes")]
        [Key(2, Column = "id_empresa_modulo_permissao")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        } 

        [Bag(Table = "usuarios_visualizacao_empresas_modulos_permissoes")]
        [Key(2, Column = "id_empresa_modulo_permissao")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }

        #endregion
    }
}
