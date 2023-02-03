using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "licencas", Name = "Modelo.Licenca, Modelo")]
    public partial class Licenca : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string descricao;
        private string numero;
        private int diasValidade;
        private DateTime prazoLimiteRenovacao;
        private DateTime dataRetirada;
        private TipoLicenca tipoLicenca;
        private Processo processo;
        private IList<Condicionante> condicionantes;
        private IList<Vencimento> vencimentos;
        private GuiaUtilizacao guia;
        private ProcessoDNPM processoDNPM;
        private IList<ArquivoFisico> arquivos;
        private Cidade cidade;

        #endregion

        public Licenca(int id) { this.Id = id; }
        public Licenca(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Licenca() { }

        [ManyToOne(Name = "Cidade", Class="Modelo.Cidade, Modelo", Column="id_cidade")]
        public virtual Cidade Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_licenca")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Property(Column = "prazo_limite_renovacao")]
        public virtual DateTime PrazoLimiteRenovacao
        {
            get
            {
                if (prazoLimiteRenovacao <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return prazoLimiteRenovacao;
            }
            set { prazoLimiteRenovacao = value; }
        }

        [Property(Column = "dias_validade")]
        public virtual int DiasValidade
        {
            get { return diasValidade; }
            set { diasValidade = value; }
        }

        [Bag(Name = "Condicionantes", Table = "condicionantes", Cascade = "delete")]
        [Key(2, Column = "id_licenca")]
        [OneToMany(3, Class = "Modelo.Condicionante, Modelo")]
        public virtual IList<Condicionante> Condicionantes
        {
            get { return condicionantes; }
            set { condicionantes = value; }
        }

        [ManyToOne(Name = "Processo", Column = "id_processo", Class = "Modelo.Processo, Modelo")]
        public virtual Processo Processo
        {
            get { return processo; }
            set { processo = value; }
        }

        [ManyToOne(Name = "TipoLicenca", Column = "id_tipo_licenca", Class = "Modelo.TipoLicenca, Modelo")]
        public virtual TipoLicenca TipoLicenca
        {
            get { return tipoLicenca; }
            set { tipoLicenca = value; }
        }

        [Property]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "data_retirada")]
        public virtual DateTime DataRetirada
        {
            get
            {
                if (dataRetirada <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataRetirada;
            }
            set { dataRetirada = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_licenca")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

        [ManyToOne(Name = "GuiaUtilizacao", Column = "id_guia_utilizacao", Class = "Modelo.GuiaUtilizacao, Modelo")]
        public virtual GuiaUtilizacao GuiaUtilizacao
        {
            get { return guia; }
            set { guia = value; }
        }

        [ManyToOne(Name = "ProcessoDNPM", Column = "id_processo_dnpm", Class = "Modelo.ProcessoDNPM, Modelo")]
        public virtual ProcessoDNPM ProcessoDNPM
        {
            get { return processoDNPM; }
            set { processoDNPM = value; }
        }
    }
}
