using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "templates_notificacoes", Name = "Modelo.TemplateNotificacao, Modelo")]
    public partial class TemplateNotificacao : ObjetoBase
    {

        //DNPM
        public const string Exigencia = "Exigencia";
        public const string ValidadeExtracao = "ValidadeExtracao";
        public const string ValidadeLicenciamento = "ValidadeLicenciamento";
        public const string ValidadeEntregaProtocolo = "ValidadeEntregaProtocolo";
        public const string TemplateRequerimentoEmissaoPosse = "RequerimentoEmissaoPosse"; 
        public const string ValidadeAlvaraPesquisa = "ValidadeAlvaraPesquisa";
        public const string DIPEM = "DIPEM";
        public const string LimiteRenuncia = "LimiteRenuncia";
        public const string InicioPesquisa = "InicioPesquisa";
        public const string TemplateTaxaAnualHectare = "TaxaAnualHectare";
        public const string TemplateRequerimentoLavra = "RequerimentoLavra";       
        public const string GuiaUtilizacao = "GuiaUtilizacao";
        public const string TemplateRequerimentoLPTotal = "RequerimentoLPTotal";
        public const string RAL = "RAL";
        

        //MEIO AMBIENTE
        public const string TemplateCondicionante = "Condicionante";
        public const string TemplateLicenca = "Licenca";
        public const string TemplateOutrosEmpresa = "OutrosEmpresa";
        public const string TemplateOutrosProcesso = "OutrosProcesso";
        public const string TemplateRelatorioCTF = "RelatorioCTF";
        public const string TemplatePagamentoCTF = "PagamentoCTF";
        public const string TemplateCertificadoCTF = "CertificadoCTF";

        //VENCIMENTOS DIVERSOS
        public const string TemplateVencimentoDiverso = "VencimentoDiverso";

        //CONTRATOS DIVERSOS
        public const string TemplateVencimentoContratoDiverso = "TemplateVencimentoContratoDiverso";
        public const string TemplateVencimentoRejusteContratoDiverso = "TemplateVencimentoRejusteContratoDiverso";

        #region ___________ Construtores ___________

        public TemplateNotificacao(int id) { this.Id = id; }
        public TemplateNotificacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TemplateNotificacao() { }

        #endregion

        #region ___________ Atributos ___________

        private string nome;
        private string template;
        private string assuntoEmail;
        private string parametrosPossiveis;

        #endregion

        #region ___________ Propriedades ___________

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "template")]
        public virtual string Template
        {
            get { return template; }
            set { template = value; }
        }

        [Property(Column = "assunto_email")]
        public virtual string AssuntoEmail
        {
            get { return assuntoEmail; }
            set { assuntoEmail = value; }
        }

        [Property(Column = "parametros_possiveis")]
        public virtual string ParametrosPossiveis
        {
            get { return parametrosPossiveis; }
            set { parametrosPossiveis = value; }
        }

        #endregion
    }
}
