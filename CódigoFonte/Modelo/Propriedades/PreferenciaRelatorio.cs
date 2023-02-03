using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "preferencias_relatorio", Name = "Modelo.PreferenciaRelatorio, Modelo")]
    public partial class PreferenciaRelatorio : ObjetoBase
    {
        public PreferenciaRelatorio(int id) { this.Id = id; }
        public PreferenciaRelatorio(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public PreferenciaRelatorio() { }

        #region ________ Atributos ________
                
        private string preferencias;
        private Usuario usuario;
        private Menu menu;        

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "preferencia")]
        public virtual string Preferencia
        {
            get { return preferencias; }
            set { preferencias = value; }
        }

        [ManyToOne(Class = "Modelo.Usuario, Modelo", Name = "Usuario", Column = "id_usuario")]
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        [ManyToOne(Class = "Modelo.Menu, Modelo", Name = "Menu", Column = "id_menu")]
        public virtual Menu Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        #endregion

    }
}
