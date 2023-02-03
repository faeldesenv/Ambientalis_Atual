using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "administradores", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Administrador, Modelo")]
    [Key(Column = "id")]
    public partial class Administrador : Pessoa
    {
        #region ___________ Atributos ___________

        private IList<GrupoEconomico> gruposEconomicos;
        private string senhaAtivacao;

        #endregion

        public Administrador(int id) { this.Id = id; }
        public Administrador(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Administrador() { }


        [Bag(Name = "GruposEconomicos", Table = "grupos_economicos")]
        [Key(2, Column = "id_administrador")]
        [OneToMany(3, Class = "Modelo.GrupoEconomico, Modelo")]
        public virtual IList<GrupoEconomico> GruposEconomicos
        {
            get { return gruposEconomicos; }
            set { gruposEconomicos = value; }
        }

        [Property(Column = "senha_ativacao")]
        [Column(SqlType = "Text", Name = "senha_ativacao")]
        public virtual string SenhaAtivacao
        {
            get { return senhaAtivacao; }
            set { senhaAtivacao = value; }
        }

    }
}
