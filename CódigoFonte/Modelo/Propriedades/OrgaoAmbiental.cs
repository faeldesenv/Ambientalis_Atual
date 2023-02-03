using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "orgaos_ambientais", Name = "Modelo.OrgaoAmbiental, Modelo")]
    public partial class OrgaoAmbiental : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string nome;
        private IList<OutrosEmpresa> outrosEmpresas;
        private IList<Processo> processos;

        #endregion

        public OrgaoAmbiental(int id) { this.Id = id; }
        public OrgaoAmbiental(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public OrgaoAmbiental() { }

        [Bag(Name = "OutrosEmpresas", Table = "outros_empresas")]
        [Key(2, Column = "id_orgao_ambiental")]
        [OneToMany(3, Class = "Modelo.OutrosEmpresa, Modelo")]
        public virtual IList<OutrosEmpresa> OutrosEmpresas
        {
            get { return outrosEmpresas; }
            set { outrosEmpresas = value; }
        }

        [Bag(Name = "Processos", Table = "processos")]
        [Key(2, Column = "id_orgao_ambiental")]
        [OneToMany(3, Class = "Modelo.Processo, Modelo")]
        public virtual IList<Processo> Processos
        {
            get { return processos; }
            set { processos = value; }
        }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
