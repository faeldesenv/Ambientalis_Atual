using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "regimes", Name = "Modelo.Regime, Modelo")]
    public partial class Regime : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string linkArquivoExigencia;
        private IList<Exigencia> exigencias;
        private ProcessoDNPM processoDNPM;
        private IList<ArquivoFisico> arquivos;
        private IList<Historico> historicos;

        #endregion

        public Regime(int id) { this.Id = id; }
        public Regime(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Regime() { }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_regime")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Property(Column = "link_arquivo_exigencia")]
        public virtual string LinkArquivoExigencia
        {
            get { return linkArquivoExigencia; }
            set { linkArquivoExigencia = value; }
        }

        [Bag(Name = "Exigencias", Table = "exigencias", Cascade = "delete")]
        [Key(2, Column = "id_regime")]
        [OneToMany(3, Class = "Modelo.Exigencia, Modelo")]
        public virtual IList<Exigencia> Exigencias
        {
            get { return exigencias; }
            set { exigencias = value; }
        }

        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processo_dnpm", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_regime")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }
    }
}
