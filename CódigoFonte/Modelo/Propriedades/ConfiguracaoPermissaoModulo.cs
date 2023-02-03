using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "configuracoes_permissoes_modulos", Name = "Modelo.ConfiguracaoPermissaoModulo, Modelo")]
    public partial class ConfiguracaoPermissaoModulo: ObjetoBase
    {
        public ConfiguracaoPermissaoModulo(int id) { this.Id = id; }
        public ConfiguracaoPermissaoModulo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ConfiguracaoPermissaoModulo() { }

        #region ________Atributos________

        private char tipo;

        public const char GERAL = 'G';
        public const char POREMPRESA = 'E';
        public const char PORPROCESSO = 'P';
        public const char PORSETOR = 'S';

        private IList<Usuario> usuariosEdicaoModuloGeral;        
        private IList<Usuario> usuariosVisualizacaoModuloGeral;
        private ModuloPermissao moduloPermissao;        

        #endregion

        #region ________Propertys________

        [Property(Column = "tipo")]
        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [Bag(Table = "usuarios_edicao_configuracoes_permissoes_modulos")]
        [Key(2, Column = "id_configuracao_permissao_modulo")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicaoModuloGeral
        {
            get { return usuariosEdicaoModuloGeral; }
            set { usuariosEdicaoModuloGeral = value; }
        }

        [Bag(Table = "usuarios_visualizacao_configuracoes_permissoes_modulos")]
        [Key(2, Column = "id_configuracao_permissao_modulo")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacaoModuloGeral
        {
            get { return usuariosVisualizacaoModuloGeral; }
            set { usuariosVisualizacaoModuloGeral = value; }
        }

        [ManyToOne(Name = "ModuloPermissao", Column = "id_modulo_permissao", Class = "Modelo.ModuloPermissao, Modelo")]
        public virtual ModuloPermissao ModuloPermissao
        {
            get { return moduloPermissao; }
            set { moduloPermissao = value; }
        }

        #endregion
    }
}
