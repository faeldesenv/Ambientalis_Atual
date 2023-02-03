using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "empresas", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Empresa, Modelo")]
    [Key(Column = "id")]
    public partial class Empresa : Pessoa
    {
        #region ___________ Atributos ___________

        private GrupoEconomico grupoEconomico;
        private IList<Processo> processos;
        private IList<OutrosEmpresa> outrosEmpresas;
        private IList<ProcessoDNPM> processosDNPM;
        private CadastroTecnicoFederal cadastroTecnicoFederal;
        private string representanteLegal;
        private string gestorEconomico;
        private IList<Diverso> diversos;
        private IList<ContratoDiverso> contratosDiversos;
        private IList<EmpresaModuloPermissao> empresasModulosPermissoes;                

        #endregion

        public Empresa(int id) { this.Id = id; }
        public Empresa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Empresa() { }

        [Bag(Name = "ContratosDiversos", Table = "contratos_diversos")]
        [Key(2, Column = "id_empresa")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        [OneToOne(Name = "CadastroTecnicoFederal", Class = "Modelo.CadastroTecnicoFederal, Modelo", Cascade = "delete", PropertyRef = "Empresa")]
        public virtual CadastroTecnicoFederal CadastroTecnicoFederal
        {
            get { return cadastroTecnicoFederal; }
            set { cadastroTecnicoFederal = value; }
        }

        [Bag(Name = "ProcessosDNPM", Table = "processos_dnpm", Cascade = "delete")]
        [Key(2, Column = "id_empresa")]
        [OneToMany(3, Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual IList<ProcessoDNPM> ProcessosDNPM
        {
            get { return processosDNPM; }
            set { processosDNPM = value; }
        }

        [ManyToOne(Name = "GrupoEconomico", Column = "id_grupo_economico", Class = "Modelo.GrupoEconomico, Modelo")]
        public virtual GrupoEconomico GrupoEconomico
        {
            get { return grupoEconomico; }
            set { grupoEconomico = value; }
        }

        [Bag(Name = "OutrosEmpresas", Table = "outros_empresas", Cascade = "delete")]
        [Key(2, Column = "id_empresa")]
        [OneToMany(3, Class = "Modelo.OutrosEmpresa, Modelo")]
        public virtual IList<OutrosEmpresa> OutrosEmpresas
        {
            get { return outrosEmpresas; }
            set { outrosEmpresas = value; }
        }

        [Bag(Name = "Processos", Table = "processos", Cascade = "delete")]
        [Key(2, Column = "id_empresa")]
        [OneToMany(3, Class = "Modelo.Processo, Modelo")]
        public virtual IList<Processo> Processos
        {
            get { return processos; }
            set { processos = value; }
        }

        [Property(Column = "representante_legal")]
        public virtual string RepresentanteLegal
        {
            get { return representanteLegal; }
            set { representanteLegal = value; }
        }

        [Property(Column = "gestor_economico")]
        public virtual string GestorEconomico
        {
            get { return gestorEconomico; }
            set { gestorEconomico = value; }
        }

        [Bag(Name = "Diversos", Table = "diversos", Cascade = "delete")]
        [Key(2, Column = "id_empresa")]
        [OneToMany(3, Class = "Modelo.Diverso, Modelo")]
        public virtual IList<Diverso> Diversos
        {
            get { return diversos; }
            set { diversos = value; }
        }

        [Bag(Name = "EmpresasModulosPermissoes", Table = "empresas_modulos_permissoes", Cascade = "delete")]
        [Key(2, Column = "id_empresa")]
        [OneToMany(3, Class = "Modelo.EmpresaModuloPermissao, Modelo")]
        public virtual IList<EmpresaModuloPermissao> EmpresasModulosPermissoes
        {
            get { return empresasModulosPermissoes; }
            set { empresasModulosPermissoes = value; }
        }
    }
}
