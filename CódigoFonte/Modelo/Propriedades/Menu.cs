using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "menus", Name = "Modelo.Menu, Modelo")]
    public partial class Menu : ObjetoBase
    {
        public const int IDMENUINDEX = 1;

        #region ___________ Construtores ___________

        public Menu(int id) { this.Id = id; }
        public Menu(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Menu() { }

        #endregion

        #region ___________ Atributos ___________

        private string nome;
        private string urlCadastro;
        private string urlPesquisa;
        private bool relatorio;
        private IList<GrupoEconomico> gruposEconomicos;
        private GrupoMenu grupoMenu;
        private int prioridade;
        private ModuloPermissao moduloPermissao;        

        #endregion

        #region ___________ Propriedades ___________

        [Property]
        public virtual int Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "url_cadastro")]
        public virtual string UrlCadastro
        {
            get { return urlCadastro; }
            set { urlCadastro = value; }
        }

        [Property(Column = "url_pesquisa")]
        public virtual string UrlPesquisa
        {
            get { return urlPesquisa; }
            set { urlPesquisa = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Relatorio
        {
            get { return relatorio; }
            set { relatorio = value; }
        }

        [Bag(Table = "menus_clientes")]
        [Key(2, Column = "id_menu")]
        [ManyToMany(3, Class = "Modelo.GrupoEconomico, Modelo", Column = "id_grupo_economico")]
        public virtual IList<GrupoEconomico> GruposEconomicos
        {
            get { return gruposEconomicos; }
            set { gruposEconomicos = value; }
        }

        [ManyToOne(Name = "GrupoMenu", Column = "id_grupo", Class = "Modelo.GrupoMenu, Modelo")]
        public virtual GrupoMenu GrupoMenu
        {
            get { return grupoMenu; }
            set { grupoMenu = value; }
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
