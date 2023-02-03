using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Subclass(Name = "Modelo.DadosFisicaComercial, Modelo", Extends = "Modelo.DadosPessoaComercial, Modelo", DiscriminatorValue = "Fisica")]
    public partial class DadosFisicaComercial : DadosPessoaComercial
    {
        #region ___________ Atributos ___________
       
        private char sexo;
        private string cpf;
        private DateTime dataNascimento;
        private char estadoCivil;
        private string rg;
        private string nomePai;
        private string nomeMae;
        private string naturalidade;

        #endregion

        [Property]
        public virtual string Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        [Property]
        public virtual char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        [Property]
        public virtual string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        [Property(Column = "data_nascimento")]
        public virtual DateTime DataNascimento
        {
            get
            {
                if (dataNascimento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataNascimento;
            }
            set { dataNascimento = value; }
        }

        [Property(Column = "estado_civil")]
        public virtual char EstadoCivil
        {
            get { return estadoCivil; }
            set { estadoCivil = value; }
        }

        [Property(Column = "nome_pai")]
        public virtual string NomePai
        {
            get { return nomePai; }
            set { nomePai = value; }
        }

        [Property(Column = "nome_mae")]
        public virtual string NomeMae
        {
            get { return nomeMae; }
            set { nomeMae = value; }
        }

        [Property]
        public virtual string Naturalidade
        {
            get { return naturalidade; }
            set { naturalidade = value; }
        }
    }
}
