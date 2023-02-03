using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "usuarios", Name = "Modelo.Usuario, Modelo")]
    public partial class Usuario : ObjetoBase
    {
        #region ___________ Construtores ___________

        public Usuario(int id) { this.Id = id; }
        public Usuario(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Usuario() { }

        #endregion

        #region ___________ Atributos ___________

        private string nome;
        private string login;
        private string senha;
        private string email;        
        private GrupoEconomico grupoEconomico;
        private Administrador administrador;
        private bool usuarioAdministrador;
        private IList<ModuloPermissao> modulosPermissao;
        private IList<Setor> setores;
        private IList<Acesso> acessos;
        private string emailSeguranca;        

        #endregion

        #region ___________ Propriedades ___________

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Unique = true)]
        public virtual string Login
        {
            get { return login; }
            set { login = value; }
        }

        [Property]
        public virtual string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        [Property(Column = "e_mail")]
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }        

        [ManyToOne(Name = "GrupoEconomico", Column = "id_grupo_economico", Class = "Modelo.GrupoEconomico, Modelo")]
        public virtual GrupoEconomico GrupoEconomico
        {
            get { return grupoEconomico; }
            set { grupoEconomico = value; }
        }

        [ManyToOne(Name = "Administrador", Column = "id_administrador", Class = "Modelo.Administrador, Modelo")]
        public virtual Administrador Administrador
        {
            get { return administrador; }
            set { administrador = value; }
        }

        [Property(Column = "usuario_administrador", Type = "TrueFalse")]
        public virtual bool UsuarioAdministrador
        {
            get { return usuarioAdministrador; }
            set { usuarioAdministrador = value; }
        }

        [Bag(Table = "usuarios_modulos_permissao")]
        [Key(2, Column = "id_usuario")]
        [ManyToMany(3, Class = "Modelo.ModuloPermissao, Modelo", Column = "id_modulo_permissao")]
        public virtual IList<ModuloPermissao> ModulosPermissao
        {
            get { return modulosPermissao; }
            set { modulosPermissao = value; }
        }

        [Bag(Table = "usuarios_setores")]
        [Key(2, Column = "id_usuario")]
        [ManyToMany(3, Class = "Modelo.Setor, Modelo", Column = "id_setor")]
        public virtual IList<Setor> Setores
        {
            get { return setores; }
            set { setores = value; }
        }

        [Bag(Name = "Acessos", Table = "acessos")]
        [Key(2, Column = "id_usuario")]
        [OneToMany(3, Class = "Modelo.Acesso, Modelo")]
        public virtual IList<Acesso> Acessos
        {
            get { return acessos; }
            set { acessos = value; }
        }

        [Property(Column = "email_seguranca")]
        public virtual string EmailSeguranca
        {
            get { return emailSeguranca; }
            set { emailSeguranca = value; }
        }

        #endregion
    }
}
