using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "menus_comerciais", Name = "Modelo.MenuComercial, Modelo")]
    public partial class MenuComercial : ObjetoBase
    {
        #region ___________ Construtores ___________

        public MenuComercial(int id) { this.Id = id; }
        public MenuComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public MenuComercial() { }

        #endregion

        #region ___________ Atributos ___________

        private string nome;
        private string url;
        private string urlIcone;
        private IList<MenuComercial> menus;
        private int prioridade;
        private string permissao;
        private MenuComercial menuPai;

        #endregion

        #region ___________ Propriedades ___________

        [Property]
        public virtual int Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        // Permissao DE MENU SUP = SUPERVISOR
        // Permissao DE MENU REV = REVENDAS
        // Permissao DE MENU ADM = ADMINISTRADORES
        // Permissao DE MENU TOD = TODOS

        [Property]
        public virtual string Permissao
        {
            get { return permissao; }
            set { permissao = value; }
        }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "url")]
        public virtual string Url
        {
            get { return url; }
            set { url = value; }
        }

        [Property(Column = "url_icone")]
        public virtual string UrlIcone
        {
            get { return urlIcone; }
            set { urlIcone = value; }
        }

        [Bag(Name = "SubMenus", Table = "menus", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_menu")]
        [OneToMany(3, Class = "Modelo.MenuComercial, Modelo")]
        public virtual IList<MenuComercial> SubMenus
        {
            get { return menus; }
            set { menus = value; }
        }

        [ManyToOne(Name = "MenuPai", Column = "id_menu", Class = "Modelo.MenuComercial, Modelo")]
        public virtual MenuComercial MenuPai
        {
            get { return menuPai; }
            set { menuPai = value; }
        }

        #endregion
    }
}
