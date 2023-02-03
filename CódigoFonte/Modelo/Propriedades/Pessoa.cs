using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "pessoas", Name = "Modelo.Pessoa, Modelo")]
    public abstract class Pessoa : ObjetoBase
    {
        #region ___________ Atributos ___________

        private String nome;
        private string site;
        private String responsavel;
        private bool ativo;
        private Endereco endereco = new Endereco();
        private Contato contato = new Contato();
        private DadosPessoa dadosPessoa;
        private string observacoes;
        private bool receberNoticias;
        private string nacionalidade;

        #endregion

        #region __________ Propriedades __________

        [Property]
        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property]
        public virtual String Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        [Property]
        public virtual string Site
        {
            get { return site; }
            set { site = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        [ComponentProperty]
        public virtual Contato Contato
        {
            get { return contato; }
            set { contato = value; }
        }

        [ComponentProperty]
        public virtual Endereco Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        [ManyToOne(Name = "DadosPessoa", Column = "id_dados_pessoa", Class = "Modelo.DadosPessoa, Modelo", Lazy = Laziness.False, Cascade = "delete")]
        public virtual DadosPessoa DadosPessoa
        {
            get { return dadosPessoa; }
            set { dadosPessoa = value; }
        }

        [Property]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [Property(Column = "receber_noticias", Type = "TrueFalse")]
        public virtual bool ReceberNoticias
        {
            get { return receberNoticias; }
            set { receberNoticias = value; }
        }

        [Property]
        public virtual string Nacionalidade
        {
            get { return nacionalidade; }
            set { nacionalidade = value; }
        }

        #endregion
    }
}
