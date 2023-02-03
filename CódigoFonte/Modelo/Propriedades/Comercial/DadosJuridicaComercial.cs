using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Subclass(Name = "Modelo.DadosJuridicaComercial, Modelo", Extends = "Modelo.DadosPessoaComercial, Modelo", DiscriminatorValue = "Juridica")]
    public partial class DadosJuridicaComercial : DadosPessoaComercial
    {
        #region ___________ Atributos ___________
        private string razaoSocial;
        private string cnpj;

        #endregion

        [Property(Column = "razao_social")]
        public virtual string RazaoSocial
        {
            get { return razaoSocial; }
            set { razaoSocial = value; }
        }

        [Property]
        public virtual string Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }



    }
}
