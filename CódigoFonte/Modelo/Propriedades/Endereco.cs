using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Component(Class = "Modelo.Endereco, Modelo")]
    public class Endereco
    {
        #region ___________ Atributos ___________

        private Cidade cidade;
        private string bairro;
        private string rua;
        private string numero;
        private string complemento;
        private string pontoReferencia;
        private string cep;

        #endregion

        [Property]
        public virtual string Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        [ManyToOne(Name = "Cidade", Column = "id_cidade", Class = "Modelo.Cidade, Modelo")]
        public virtual Cidade Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        [Property]
        public virtual string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        [Property]
        public virtual string Rua
        {
            get { return rua; }
            set { rua = value; }
        }

        [Property]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property]
        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        [Property(Column = "referencia")]
        public virtual string PontoReferencia
        {
            get { return pontoReferencia; }
            set { pontoReferencia = value; }
        }

        //Propriedade não persistente em banco
        public virtual string EnderecoCompleto
        {
            get
            {
                string retorno = "";
                if (rua != null && rua != "")
                {
                    retorno = retorno + Rua;
                }
                if (numero != null && numero != "")
                {
                    retorno = retorno + ", N°. " + Numero;
                }
                if (bairro != null && bairro != "")
                {
                    retorno = retorno + ", " + Bairro + "; ";
                }
                if (cidade != null && cidade.Nome != "")
                {
                    retorno = retorno + Cidade.Nome + " - " + Cidade.Estado.Nome + "; ";
                }
                if (cep != null && cep != "")
                {
                    retorno = retorno + " Cep: " + Cep + "; ";
                }
                if (pontoReferencia != null && pontoReferencia != "")
                {
                    retorno = retorno + " Referência: " + pontoReferencia + "; ";
                }
                if (complemento != null && complemento != "")
                {
                    retorno = retorno + " Complemento: " + complemento + ".";
                }

                return retorno;
            }
        }

    }
}
