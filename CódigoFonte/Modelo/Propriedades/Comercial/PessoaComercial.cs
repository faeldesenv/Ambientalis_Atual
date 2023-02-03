using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "pessoas_comerciais", Name = "Modelo.PessoaComercial, Modelo")]
    public abstract class PessoaComercial : ObjetoBase
    {
        #region ___________ Atributos ___________

        private String nome;
        private string site;
        private String responsavel;
        private bool ativo;
        private EnderecoComercial endereco = new EnderecoComercial();
        private ContatoComercial contato = new ContatoComercial();
        private bool receberNoticias;
        private DadosPessoaComercial dadosPessoa;
        private string observacoes;
        private string nacionalidade;
        private string representanteLegal;
        private string cpfRepresentanteLegal;
        private string nacionalidadeRepresentanteLegal;
        private string gestorEconomico;        

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
        public virtual ContatoComercial Contato
        {
            get { return contato; }
            set { contato = value; }
        }

        [ComponentProperty]
        public virtual EnderecoComercial Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        [ManyToOne(Name = "DadosPessoa", Column = "id_dados_pessoa", Class = "Modelo.DadosPessoaComercial, Modelo", Lazy = Laziness.False, Cascade = "delete")]
        public virtual DadosPessoaComercial DadosPessoa
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

        [Property]
        public virtual string RepresentanteLegal
        {
            get { return representanteLegal; }
            set { representanteLegal = value; }
        }

        [Property]
        public virtual string CpfRepresentanteLegal
        {
            get { return cpfRepresentanteLegal; }
            set { cpfRepresentanteLegal = value; }
        }

        [Property]
        public virtual string NacionalidadeRepresentanteLegal
        {
            get { return nacionalidadeRepresentanteLegal; }
            set { nacionalidadeRepresentanteLegal = value; }
        }

        [Property]
        public virtual string GestorEconomico
        {
            get { return gestorEconomico; }
            set { gestorEconomico = value; }
        }

        #endregion
    }
}
