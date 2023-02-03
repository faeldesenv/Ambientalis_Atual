using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using System.IO;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.ArquivoFisico, Modelo", Table = "arquivos_fisicos")]
    public partial class ArquivoFisico : ObjetoBase
    {
        #region _______ Contrutores ________

        public ArquivoFisico(int id) { this.Id = id; }
        public ArquivoFisico(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ArquivoFisico() { }

        #endregion

        #region _______ Atributos ________

        private string descricao;
        private string caminho;
        private string host;
        private string identificador;
        private string extensao;
        private long tamanho;
        private DateTime dataPublicacao;

        private ProcessoDNPM processoDNPM;
        private Regime regime;
        private Exigencia exigencia;
        private GuiaUtilizacao guiaUtilizacao;
        private CadastroTecnicoFederal cadastroTecnicoFederal;
        private Licenca licenca;
        private Condicional condicional;
        private Diverso diverso;
        private ContratoDiverso contratoDiverso;

        #endregion

        #region _______ Propriedades ________

        [ManyToOne(Name = "ContratoDiverso", Column = "id_contrato_diverso", Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual ContratoDiverso ContratoDiverso
        {
            get { return contratoDiverso; }
            set { contratoDiverso = value; }
        }

        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processoDNPM", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }

        [ManyToOne(Name = "Regime", Column = "id_regime", Class = "Modelo.Regime, Modelo")]
        public virtual Regime Regime
        {
            get { return regime; }
            set { regime = value; }
        }

        [ManyToOne(Name = "Exigencia", Column = "id_exigencia", Class = "Modelo.Exigencia, Modelo")]
        public virtual Exigencia Exigencia
        {
            get { return exigencia; }
            set { exigencia = value; }
        }

        [ManyToOne(Name = "GuiaUtilizacao", Column = "id_guia_utilizacao", Class = "Modelo.GuiaUtilizacao, Modelo")]
        public virtual GuiaUtilizacao GuiaUtilizacao
        {
            get { return guiaUtilizacao; }
            set { guiaUtilizacao = value; }
        }

        [ManyToOne(Name = "CadastroTecnicoFederal", Column = "id_ctf", Class = "Modelo.CadastroTecnicoFederal, Modelo")]
        public virtual CadastroTecnicoFederal CadastroTecnicoFederal
        {
            get { return cadastroTecnicoFederal; }
            set { cadastroTecnicoFederal = value; }
        }

        [ManyToOne(Name = "Licenca", Column = "id_licenca", Class = "Modelo.Licenca, Modelo")]
        public virtual Licenca Licenca
        {
            get { return licenca; }
            set { licenca = value; }
        }

        [ManyToOne(Name = "Condicional", Column = "id_condicional", Class = "Modelo.Condicional, Modelo")]
        public virtual Condicional Condicional
        {
            get { return condicional; }
            set { condicional = value; }
        }

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "caminho")]
        public virtual string Caminho
        {
            get { return caminho; }
            set { caminho = value; }
        }

        [Property(Column = "host")]
        public virtual string Host
        {
            get { return host; }
            set { host = value; }
        }

        [Property(Column = "identificador")]
        public virtual string Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }

        [Property(Column = "extensao")]
        public virtual string Extensao
        {
            get { return extensao; }
            set { extensao = value; }
        }

        [Property(Column = "tamanho")]
        public virtual long Tamanho
        {
            get { return tamanho; }
            set { tamanho = value; }
        }

        [Property(Column = "data_publicacao")]
        public virtual DateTime DataPublicacao
        {
            get
            {
                if (dataPublicacao <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataPublicacao;
            }
            set { dataPublicacao = value; }
        }

        public virtual string TamanhoMega
        {
            get { return (this.tamanho / 1048576).ToString() + " Mb"; }
        }

        [ManyToOne(Name = "Diverso", Column = "id_diverso", Class = "Modelo.Diverso, Modelo")]
        public virtual Diverso Diverso
        {
            get { return diverso; }
            set { diverso = value; }
        }

        #endregion

        #region _______ Métodos ________

        public virtual string CaminhoVirtual
        {
            get { return "http://" + host + "/" + this.caminho + "/" + this.identificador; }
        }

        #endregion

    }
}
