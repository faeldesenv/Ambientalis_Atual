using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "rals", Name = "Modelo.RAL, Modelo")]
    public partial class RAL : ObjetoBase
    {
        #region ___________ Atributos ___________

        private ProcessoDNPM processoDNPM;
        private IList<Vencimento> vencimentos;
        IList<ArquivoFisico> arquivos;
        private IList<Historico> historicos;

        #endregion

        public RAL(int id) { this.Id = id; }
        public RAL(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public RAL() { }


        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processo_dnpm", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_ral")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_ral")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_ral")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

    }
}
