using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "processos_dnpm", Name = "Modelo.ProcessoDNPM, Modelo")]
    public partial class ProcessoDNPM : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string numero;
        private string identificacao;
        private string tamanhoArea;
        private string substancia;
        private DateTime dataAbertura;
        private string endereco;
        private Cidade cidade;
        private IList<Licenca> licencas;
        private IList<Substancia> substancias;
        private IList<Regime> regimes;
        private RAL ral;
        private Empresa empresa;
        private Consultora consultora;
        private IList<ArquivoFisico> arquivos;
        private string regimeDeCriacao;
        private string observacoes;
        private IList<EventoDNPM> eventosDNPM;
        private IList<GuiaUtilizacao> guias;
        private IList<ContratoDiverso> contratosDiversos;
        private IList<Usuario> usuariosEdicao;
        private IList<Usuario> usuariosVisualizacao;          

        #endregion

        public ProcessoDNPM(int id) { this.Id = id; }
        public ProcessoDNPM(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ProcessoDNPM() { }

        [Bag(Name = "EventosDNPM", Table = "eventos_dnpm", Cascade = "delete")]
        [Key(2, Column = "id_processo_dnpm")]
        [OneToMany(3, Class = "Modelo.EventoDNPM, Modelo")]
        public virtual IList<EventoDNPM> EventosDNPM
        {
            get { return eventosDNPM; }
            set { eventosDNPM = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_processoDNPM")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }
        
        [Bag(Name = "Guias", Table = "licencas", Cascade = "delete")]
        [Key(2, Column = "id_processo_dnpm")]
        [OneToMany(3, Class = "Modelo.GuiaUtilizacao, Modelo")]
        public virtual IList<GuiaUtilizacao> Guias
        {
            get { return guias; }
            set { guias = value; }
        }

        [Bag(Name = "Substancias", Table = "substancias", Cascade = "delete")]
        [Key(2, Column = "id_processo_dnpm")]
        [OneToMany(3, Class = "Modelo.Substancia, Modelo")]
        public virtual IList<Substancia> Substancias
        {
            get { return substancias; }
            set { substancias = value; }
        }

        [OneToOne(Name = "RAL", Class = "Modelo.RAL", Cascade = "delete", PropertyRef = "ProcessoDNPM")]
        public virtual RAL RAL
        {
            get { return ral; }
            set { ral = value; }
        }

        [Property(Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "regime")]
        public virtual string RegimeDeCriacao
        {
            get { return regimeDeCriacao; }
            set { regimeDeCriacao = value; }
        }

        [Property(Column = "identificacao")]
        public virtual string Identificacao
        {
            get { return identificacao; }
            set { identificacao = value; }
        }

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo")]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [ManyToOne(Name = "Consultora", Column = "id_consultora", Class = "Modelo.Consultora, Modelo")]
        public virtual Consultora Consultora
        {
            get { return consultora; }
            set { consultora = value; }
        }

        [Bag(Table = "contratos_diversos_processos_dnpm")]
        [Key(2, Column = "id_processo_dnpm")]
        [ManyToMany(3, Class = "Modelo.ContratoDiverso, Modelo", Column = "id_contrato_diverso")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        [Property(Column = "data_abertura")]
        public virtual DateTime DataAbertura
        {
            get
            {
                if (dataAbertura <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAbertura;
            }
            set { dataAbertura = value; }
        }

        [Property]
        public virtual string TamanhoArea
        {
            get { return tamanhoArea; }
            set { tamanhoArea = value; }
        }


        [Property]
        public virtual string Substancia
        {
            get { return substancia; }
            set { substancia = value; }
        }

        [Property]
        public virtual string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        [ManyToOne(Name = "Cidade", Column = "id_cidade", Class = "Modelo.Cidade, Modelo")]
        public virtual Cidade Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        [Bag(Name = "Licencas", Table = "licencas", Cascade = "save-update", Inverse = false)]
        [Key(2, Column = "id_processo_dnpm")]
        [OneToMany(3, Class = "Modelo.Licenca, Modelo")]
        public virtual IList<Licenca> Licencas
        {
            get { return licencas; }
            set { licencas = value; }
        }

        [Bag(Name = "Regimes", Table = "regimes", Cascade = "delete")]
        [Key(2, Column = "id_processo_dnpm")]
        [OneToMany(3, Class = "Modelo.Regime, Modelo")]
        public virtual IList<Regime> Regimes
        {
            get { return regimes; }
            set { regimes = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "observacoes")]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [Bag(Table = "usuarios_edicao_processos_dnpm")]
        [Key(2, Column = "id_processo_dnpm")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        }    

        [Bag(Table = "usuarios_visualizacao_processos_dnpm")]
        [Key(2, Column = "id_processo_dnpm")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }
    }
}
