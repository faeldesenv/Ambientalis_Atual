using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "modulos_permissoes", Name = "Modelo.ModuloPermissao, Modelo")]
    public partial class ModuloPermissao: ObjetoBase
    {
        public const string ModuloDNPM = "DNPM";
        public const string ModuloMeioAmbiente = "Meio Ambiente";
        public const string ModuloContratos = "Contratos";
        public const string ModuloVencimentoDiverso = "Diversos";
        public const string Permissoes = "Permissões";
        

        #region _______ Contrutores ________

        public ModuloPermissao(int id) { this.Id = id; }
        public ModuloPermissao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ModuloPermissao() { }

        #endregion

        #region _______ Atributos ________

        private string nome;
        private string descricao;
        private IList<Usuario> usuarios;
        private IList<GrupoEconomico> gruposEconomicos;
        private IList<Menu> menus;
        private int prioridade;
        private IList<ConfiguracaoPermissaoModulo> configuracoesPermissoesModulos;
        private IList<EmpresaModuloPermissao> empresasModulosPermissoes;        

        #endregion

        #region _______ Propriedades ________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        
        [Property(Type = "StringClob")]
        [Column(1, Name = "descricao", SqlType = "nvarchar(max)")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Bag(Table = "usuarios_modulos_permissao")]
        [Key(2, Column = "id_modulo_permissao")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> Usuarios
        {
            get { return usuarios; }
            set { usuarios = value; }
        }

        [Bag(Table = "grupos_modulos_permissao")]
        [Key(2, Column = "id_modulo_permissao")]
        [ManyToMany(3, Class = "Modelo.GrupoEconomico, Modelo", Column = "id_grupo")]
        public virtual IList<GrupoEconomico> GruposEconomicos
        {
            get { return gruposEconomicos; }
            set { gruposEconomicos = value; }
        }

        [Bag(Name = "Menus", Table = "menus", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_modulo_permissao")]
        [OneToMany(3, Class = "Modelo.Menu, Modelo")]
        public virtual IList<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        [Property(Column = "prioridade")]
        public virtual int Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        [Bag(Name = "ConfiguracoesPermissoesModulos", Table = "configuracoes_permissoes_modulos")]
        [Key(2, Column = "id_modulo_permissao")]
        [OneToMany(3, Class = "Modelo.ConfiguracaoPermissaoModulo, Modelo")]
        public virtual IList<ConfiguracaoPermissaoModulo> ConfiguracoesPermissoesModulos
        {
            get { return configuracoesPermissoesModulos; }
            set { configuracoesPermissoesModulos = value; }
        }

        [Bag(Name = "EmpresasModulosPermissoes", Table = "empresas_modulos_permissoes", Cascade = "delete")]
        [Key(2, Column = "id_modulo_permissao")]
        [OneToMany(3, Class = "Modelo.EmpresaModuloPermissao, Modelo")]
        public virtual IList<EmpresaModuloPermissao> EmpresasModulosPermissoes
        {
            get { return empresasModulosPermissoes; }
            set { empresasModulosPermissoes = value; }
        }
      
        #endregion

        #region _______ Métodos ________

        
        #endregion
    }
}
