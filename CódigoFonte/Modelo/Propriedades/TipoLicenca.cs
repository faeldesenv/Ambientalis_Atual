using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "tipos_licencas", Name = "Modelo.TipoLicenca, Modelo")]
    public partial class TipoLicenca : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string nome;
        private string sigla;
        private int diasValidadePadrao;
        private int diasAvisoPadrao;
        private IList<Licenca> licencas;
        private IList<CondicionantePadrao> condicionantesPadroes;

        #endregion

        public TipoLicenca(int id) { this.Id = id; }
        public TipoLicenca(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoLicenca() { }

        [Bag(Name = "CondicionantesPadroes", Table = "condicionantes_padroes", Cascade = "delete")]
        [Key(2, Column = "id_tipo_licenca")]
        [OneToMany(3, Class = "Modelo.CondicionantePadrao, Modelo")]
        public virtual IList<CondicionantePadrao> CondicionantesPadroes
        {
            get { return condicionantesPadroes; }
            set { condicionantesPadroes = value; }
        }

        [Property]
        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        [Property(Column = "dias_validade_padrao")]
        public virtual int DiasValidadePadrao
        {
            get { return diasValidadePadrao; }
            set { diasValidadePadrao = value; }
        }

        [Property(Column = "dias_aviso_padrao")]
        public virtual int DiasAvisoPadrao
        {
            get { return diasAvisoPadrao; }
            set { diasAvisoPadrao = value; }
        }

        [Bag(Name = "Licencas", Table = "licencas", Cascade = "save-update", Inverse = false)]
        [Key(2, Column = "id_tipo_licenca")]
        [OneToMany(3, Class = "Modelo.Licenca, Modelo")]
        public virtual IList<Licenca> Licencas
        {
            get { return licencas; }
            set { licencas = value; }
        }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
