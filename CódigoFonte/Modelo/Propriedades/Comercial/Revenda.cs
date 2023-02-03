using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "revendas", Extends = "Modelo.PessoaComercial, Modelo", Name = "Modelo.Revenda, Modelo")]
    [Key(Column = "id")]
    public partial class Revenda : PessoaComercial
    {
        #region __________Atributos___________

        private UsuarioRevendaComercial usuarioRevenda;
        private IList<ContratoComercial> contratos;
        private IList<Prospecto> prospectos;
        private string tipoParceiro;
        private bool supervisor;

        #endregion

        public Revenda(int id) { this.Id = id; }
        public Revenda(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Revenda() { }

        #region __________Propriedades___________

        [ManyToOne(Name = "UsuarioRevenda", Column = "id_usuario_revenda", Class = "Modelo.UsuarioRevendaComercial, Modelo", Cascade = "delete")]
        public virtual UsuarioRevendaComercial UsuarioRevenda
        {
            get { return usuarioRevenda; }
            set { usuarioRevenda = value; }
        }

        [Bag(Name = "Contratos", Table = "contratos", Cascade = "delete")]
        [Key(2, Column = "id_revenda")]
        [OneToMany(3, Class = "Modelo.ContratoComercial, Modelo")]
        public virtual IList<ContratoComercial> Contratos
        {
            get { return contratos; }
            set { contratos = value; }
        }

        [Bag(Name = "Prospectos", Table = "prospectos", Cascade = "delete")]
        [Key(2, Column = "id_revenda")]
        [OneToMany(3, Class = "Modelo.Prospecto, Modelo")]
        public virtual IList<Prospecto> Prospectos
        {
            get { return prospectos; }
            set { prospectos = value; }
        }

        [Property(Column = "tipo_parceiro")]
        public virtual string TipoParceiro
        {
            get { return tipoParceiro; }
            set { tipoParceiro = value; }
        }

        [Property(Column = "supervisor", Type = "TrueFalse")]
        public virtual bool Supervisor
        {
            get { return supervisor; }
            set { supervisor = value; }
        }

        #endregion
    }
}
