using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "processos", Name = "Modelo.Processo, Modelo")]
    public partial class Processo : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string numero;
        private string observacoes;
        private DateTime dataAbertura;
        private Empresa empresa;
        private OrgaoAmbiental orgaoAmbiental;
        private IList<Licenca> licencas;
        private IList<OutrosProcesso> outrosProcessos;
        private Consultora consultora;
        private IList<ContratoDiverso> contratosDiversos;
        private IList<Usuario> usuariosEdicao;
        private IList<Usuario> usuariosVisualizacao;        

        #endregion

        public Processo(int id) { this.Id = id; }
        public Processo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Processo() { }

        [ManyToOne(Name = "Consultora", Column = "id_consultora", Class = "Modelo.Consultora, Modelo")]
        public virtual Consultora Consultora
        {
            get { return consultora; }
            set { consultora = value; }
        }

        [Bag(Name = "OutrosProcessos", Table = "outros_processos", Cascade = "delete")]
        [Key(2, Column = "id_processo")]
        [OneToMany(3, Class = "Modelo.OutrosProcesso, Modelo")]
        public virtual IList<OutrosProcesso> OutrosProcessos
        {
            get { return outrosProcessos; }
            set { outrosProcessos = value; }
        }

        [Bag(Name = "Licencas", Table = "licencas", Cascade = "delete")]
        [Key(2, Column = "id_processo")]
        [OneToMany(3, Class = "Modelo.Licenca, Modelo")]
        public virtual IList<Licenca> Licencas
        {
            get { return licencas; }
            set { licencas = value; }
        }

        [ManyToOne(Name = "OrgaoAmbiental", Column = "id_orgao_ambiental", Class = "Modelo.OrgaoAmbiental, Modelo", Fetch = FetchMode.Join)]
        public virtual OrgaoAmbiental OrgaoAmbiental
        {
            get { return orgaoAmbiental; }
            set { orgaoAmbiental = value; }
        }

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo")]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [Property]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "observacoes")]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
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

        [Bag(Table = "contratos_diversos_processos")]
        [Key(2, Column = "id_processo")]
        [ManyToMany(3, Class = "Modelo.ContratoDiverso, Modelo", Column = "id_contrato_diverso")]
        public virtual IList<ContratoDiverso> ContratosDiversos
        {
            get { return contratosDiversos; }
            set { contratosDiversos = value; }
        }

        [Bag(Table = "usuarios_edicao_processos")]
        [Key(2, Column = "id_processo")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosEdicao
        {
            get { return usuariosEdicao; }
            set { usuariosEdicao = value; }
        }       

        [Bag(Table = "usuarios_visualizacao_processos")]
        [Key(2, Column = "id_processo")]
        [ManyToMany(3, Class = "Modelo.Usuario, Modelo", Column = "id_usuario")]
        public virtual IList<Usuario> UsuariosVisualizacao
        {
            get { return usuariosVisualizacao; }
            set { usuariosVisualizacao = value; }
        }
    }
}
