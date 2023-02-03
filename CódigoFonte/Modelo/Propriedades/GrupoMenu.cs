using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "grupos_menus", Name = "Modelo.GrupoMenu, Modelo")]
    public partial class GrupoMenu : ObjetoBase
    {
        private IList<Menu> menus;

        #region ___________ Construtores ___________

        public GrupoMenu(int id) { this.Id = id; }
        public GrupoMenu(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public GrupoMenu() { }

        #endregion

        #region ___________ Atributos ___________

        private string nome;

        #endregion

        #region ___________ Propriedades ___________


        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Menus", Table = "menus", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_grupo")]
        [OneToMany(3, Class = "Modelo.Menu, Modelo")]
        public virtual IList<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        #endregion
    }
}
