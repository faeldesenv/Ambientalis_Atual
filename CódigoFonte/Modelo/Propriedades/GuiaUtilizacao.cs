using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "guias_utilizacao", Name = "Modelo.GuiaUtilizacao, Modelo")]
    public partial class GuiaUtilizacao : ObjetoBase
    {
        #region ___________ Atributos ___________

        private DateTime dataLimiteRequerimento;
        private DateTime dataRequerimento;
        private IList<Exigencia> exigencias;
        private ProcessoDNPM processoDNPM;
        private DateTime dataEmissao;
        private IList<Vencimento> vencimentos;
        private IList<ArquivoFisico> arquivos;
        private IList<Historico> historicos;
        private string numero;

        #endregion

        public GuiaUtilizacao(int id) { this.Id = id; }
        public GuiaUtilizacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public GuiaUtilizacao() { }


        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processo_dnpm", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }

        [Property(Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "data_requerimento")]
        public virtual DateTime DataRequerimento
        {
            get
            {
                if (dataRequerimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataRequerimento;
            }
            set { dataRequerimento = value; }
        }

        [Property(Column = "data_limite")]
        public virtual DateTime DataLimiteRequerimento
        {
            get
            {
                if (dataLimiteRequerimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataLimiteRequerimento;
            }
            set { dataLimiteRequerimento = value; }
        }

        [Property(Column = "data_emissao")]
        public virtual DateTime DataEmissao
        {
            get
            {
                if (dataEmissao <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataEmissao;
            }
            set { dataEmissao = value; }
        }

        [Bag(Name = "Exigencias", Table = "exigencias", Cascade = "delete")]
        [Key(2, Column = "id_guia_utilizacao")]
        [OneToMany(3, Class = "Modelo.Exigencia, Modelo")]
        public virtual IList<Exigencia> Exigencias
        {
            get { return exigencias; }
            set { exigencias = value; }
        }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_guia_utilizacao")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_guia_utilizacao")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_guia_utilizacao")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

    }
}
