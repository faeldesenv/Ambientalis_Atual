using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "grupo_economico_site", Name = "Modelo.GrupoEconomicoSite, Modelo")]
    public partial class GrupoEconomicoSite : ObjetoBase
    {
        #region ________ Atributos ___________

        private string nome;
        private string linkImagem;


        #endregion

        #region ________ Construtores ________

        public GrupoEconomicoSite(int id) { this.Id = id; }
        public GrupoEconomicoSite(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public GrupoEconomicoSite() { }

        #endregion

        #region ________ Propriedades ________

        [Property(Column="nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "link_imagem")]
        public virtual string LinkImagem
        {
            get { return linkImagem; }
            set { linkImagem = value; }
        }

        #endregion
    }
}
