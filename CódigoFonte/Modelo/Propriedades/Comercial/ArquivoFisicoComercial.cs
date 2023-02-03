using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.ArquivoFisicoComercial, Modelo", Table = "arquivos_fisicos_comerciais")]
    public partial class ArquivoFisicoComercial : ObjetoBase
    {
        #region ______ Contrutores _______

        public ArquivoFisicoComercial(int id) { this.Id = id; }
        public ArquivoFisicoComercial(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ArquivoFisicoComercial() { }

        #endregion

        #region _______ Atributos ________

        private string descricao;
        private string caminho;
        private string host;
        private string identificador;
        private string extensao;
        private long tamanho;
        private DateTime dataPublicacao;        

        #endregion

        #region ______ Propriedades ______

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

        #endregion

        #region ________ Métodos _________

        public virtual string CaminhoVirtual
        {
            get { return "http://" + host + "/" + this.caminho + "/" + this.identificador; }
        }

        #endregion
    }
}
