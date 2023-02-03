using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "cadastros_tecnicos_federais", Name = "Modelo.CadastroTecnicoFederal, Modelo")]
    public partial class CadastroTecnicoFederal : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string senha;
        private string atividade;
        private string observacoes;
        private string numeroLicenca;
        private DateTime validadeLicenca;
        private IList<Vencimento> entregaRelatorioAnual;
        private IList<Vencimento> certificadoRegularidade;
        private IList<Vencimento> taxaTrimestral;
        private Empresa empresa;
        private string numeroOficio;
        private IList<ArquivoFisico> arquivos;
        private Consultora consultora;
        private IList<Historico> historicos;
        private IList<Usuario> usuariosEdicao;
        private IList<Usuario> usuariosVisualizacao;                

        #endregion

        public CadastroTecnicoFederal(int id) { this.Id = id; }
        public CadastroTecnicoFederal(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public CadastroTecnicoFederal() { }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_ctf")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo")]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [Property(Column = "senha")]
        public virtual string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        [Property(Column = "numero_oficio")]
        public virtual string NumeroOficio
        {
            get { return numeroOficio; }
            set { numeroOficio = value; }
        }

        [Property(Column = "atividade")]
        public virtual string Atividade
        {
            get { return atividade; }
            set { atividade = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "observacoes")]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [Property(Column = "numero_licenca")]
        public virtual string NumeroLicenca
        {
            get { return numeroLicenca; }
            set { numeroLicenca = value; }
        }

        [Property(Column = "validade_licenca")]
        public virtual DateTime ValidadeLicenca
        {
            get
            {
                if (validadeLicenca <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return validadeLicenca;
            }
            set { validadeLicenca = value; }
        }

        [Bag(Name = "EntregaRelatorioAnual", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_ctf_entrega_relatorio_anual")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> EntregaRelatorioAnual
        {
            get { return entregaRelatorioAnual; }
            set { entregaRelatorioAnual = value; }
        }

        [Bag(Name = "CertificadoRegularidade", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_ctf_certificado_regularidade")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> CertificadoRegularidade
        {
            get { return certificadoRegularidade; }
            set { certificadoRegularidade = value; }
        }

        [Bag(Name = "TaxaTrimestral", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_ctf_taxa_trimestral")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> TaxaTrimestral
        {
            get { return taxaTrimestral; }
            set { taxaTrimestral = value; }
        }

        [ManyToOne(Name = "Consultora", Column = "id_consultora", Class = "Modelo.Consultora, Modelo")]
        public virtual Consultora Consultora
        {
            get { return consultora; }
            set { consultora = value; }
        }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_cadastro_tecnico_federal")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

        [Bag(Table = "usuarios_edicao_cadastros_tecnicos")]
        [Key(2, Column = "id_cadastro_tecnico_federal")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        }          

        [Bag(Table = "usuarios_visualizacao_cadastros_tecnicos")]
        [Key(2, Column = "id_cadastro_tecnico_federal")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }
    }
}
