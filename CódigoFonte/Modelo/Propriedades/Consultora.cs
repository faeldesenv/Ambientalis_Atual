using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "consultoras", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Consultora, Modelo")]
    [Key(Column = "id")]
    public partial class Consultora : Pessoa
    {
        #region ___________ Atributos ___________

        private IList<Processo> processos;
        private IList<OutrosEmpresa> outrosEmpresas;
        private IList<ProcessoDNPM> processosDNPM;
        private IList<CadastroTecnicoFederal> cadastrosTecnicosFederais;

        #endregion

        public Consultora(int id) { this.Id = id; }
        public Consultora(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Consultora() { }

        [Bag(Name = "ProcessosDNPM", Table = "processos_dnpm", Cascade="save-update", Inverse = false)]
        [Key(2, Column = "id_consultora")]
        [OneToMany(3, Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual IList<ProcessoDNPM> ProcessosDNPM
        {
            get { return processosDNPM; }
            set { processosDNPM = value; }
        }

        [Bag(Name = "Processos", Table = "processos", Cascade = "save-update", Inverse = false)]
        [Key(2, Column = "id_consultora")]
        [OneToMany(3, Class = "Modelo.Processo, Modelo")]
        public virtual IList<Processo> Processos
        {
            get { return processos; }
            set { processos = value; }
        }

        [Bag(Name = "OutrosEmpresas", Table = "outros_empresas", Cascade = "save-update", Inverse = false)]
        [Key(2, Column = "id_consultora")]
        [OneToMany(3, Class = "Modelo.OutrosEmpresa, Modelo")]
        public virtual IList<OutrosEmpresa> OutrosEmpresas
        {
            get { return outrosEmpresas; }
            set { outrosEmpresas = value; }
        }

        [Bag(Name = "CadastrosTecnicosFederais", Table = "cadastros_tecnicos_federais", Cascade = "save-update", Inverse = false)]
        [Key(2, Column = "id_consultora")]
        [OneToMany(3, Class = "Modelo.CadastroTecnicoFederal, Modelo")]
        public virtual IList<CadastroTecnicoFederal> CadastrosTecnicosFederais
        {
            get { return cadastrosTecnicosFederais; }
            set { cadastrosTecnicosFederais = value; }
        }

    }
}
