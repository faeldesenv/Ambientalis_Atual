using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Table = "contratos_diversos", Name = "Modelo.ContratoDiverso, Modelo")]
    public partial class ContratoDiverso : ObjetoBase
    {
        #region ________Atributos________

        private string numero;
        private string objeto;
        private string como;
        private DateTime dataAbertura;
        private string valor;
        private StatusContratoDiverso statusContratoDiverso;
        private IList<VencimentoContratoDiverso> vencimentosContratosDiversos;
        private IList<VencimentoContratoDiverso> reajustes;
        private Empresa empresa;
        private IList<Historico> historicos;
        private IList<ArquivoFisico> arquivosFisicos;
        private Cliente cliente;
        private FormaRecebimento formaRecebimento;
        private Fornecedor fornecedor;
        private CentroCusto centroCusto;
        private IndiceFinanceiro indiceFinanceiro;
        private Setor setor;
        private Moeda moeda;
        private ContratoDiverso contratoOriginal;
        private IList<ContratoDiverso> renovacoes;
        private IList<AditivoContrato> aditivosContratos;
        private IList<ProcessoDNPM> processosDNPM;
        private IList<Processo> processos;
        
        #endregion

        public ContratoDiverso(int id) { this.Id = id; }
        public ContratoDiverso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ContratoDiverso() { }

        #region ________Propriedades________

        [Bag(Name = "AditivosContratos", Table = "aditivos_contratos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_contrato_diverso")]
        [OneToMany(3, Class = "Modelo.AditivoContrato, Modelo")]
        public virtual IList<AditivoContrato> AditivosContratos
        {
            get { return aditivosContratos; }
            set { aditivosContratos = value; }
        }

        [Bag(Name = "ArquivosFisicos", Table = "arquivos_fisicos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_contrato_diverso")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> ArquivosFisicos
        {
            get { return arquivosFisicos; }
            set { arquivosFisicos = value; }
        }

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_contrato_diverso")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

        [ManyToOne(Name = "Empresa", Column = "id_empresa", Class = "Modelo.Empresa, Modelo", Lazy = Laziness.False)]
        public virtual Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        [ManyToOne(Name = "Moeda", Column = "id_moeda", Class = "Modelo.Moeda, Modelo", Lazy = Laziness.False)]
        public virtual Moeda Moeda
        {
            get { return moeda; }
            set { moeda = value; }
        }

        [ManyToOne(Name = "StatusContratoDiverso", Column = "id_status_contrato_diverso", Class = "Modelo.StatusContratoDiverso, Modelo", Lazy = Laziness.False)]
        public virtual StatusContratoDiverso StatusContratoDiverso
        {
            get { return statusContratoDiverso; }
            set { statusContratoDiverso = value; }
        }

        [Bag(Name = "VencimentosContratosDiversos", Table = "vencimentos_contratos_diversos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_contrato_diverso")]
        [OneToMany(3, Class = "Modelo.VencimentoContratoDiverso, Modelo")]
        public virtual IList<VencimentoContratoDiverso> VencimentosContratosDiversos
        {
            get { return vencimentosContratosDiversos; }
            set { vencimentosContratosDiversos = value; }
        }

        [Bag(Name = "Reajustes", Table = "vencimentos_contratos_diversos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_reajuste")]
        [OneToMany(3, Class = "Modelo.VencimentoContratoDiverso, Modelo")]
        public virtual IList<VencimentoContratoDiverso> Reajustes
        {
            get { return reajustes; }
            set { reajustes = value; }
        }

        [Bag(Table = "contratos_diversos_processos_dnpm")]
        [Key(2, Column = "id_contrato_diverso")]
        [ManyToMany(3, Class = "Modelo.ProcessoDNPM, Modelo", Column = "id_processo_dnpm")]
        public virtual IList<ProcessoDNPM> ProcessosDNPM
        {
            get { return processosDNPM; }
            set { processosDNPM = value; }
        }

        [Property(Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }


        [Property]
        [Column(1, SqlType = "text", Name = "objeto_contrato")]
        public virtual string Objeto
        {
            get { return objeto; }
            set { objeto = value; }
        }

        [Property(Column = "como")]
        public virtual string Como
        {
            get { return como; }
            set { como = value; }
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

        [Property(Column = "valor")]
        public virtual string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        [ManyToOne(Name = "FormaRecebimento", Column = "id_forma_recebimento", Class = "Modelo.FormaRecebimento, Modelo", Lazy = Laziness.False)]
        public virtual FormaRecebimento FormaRecebimento
        {
            get { return formaRecebimento; }
            set { formaRecebimento = value; }
        }


        [ManyToOne(Name = "Cliente", Column = "id_cliente", Class = "Modelo.Cliente, Modelo")]
        public virtual Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        [ManyToOne(Name = "Fornecedor", Column = "id_fornecedor", Class = "Modelo.Fornecedor, Modelo")]
        public virtual Fornecedor Fornecedor
        {
            get { return fornecedor; }
            set { fornecedor = value; }
        }

        [ManyToOne(Name = "CentroCusto", Column = "id_centro_custo", Class = "Modelo.CentroCusto, Modelo")]
        public virtual CentroCusto CentroCusto
        {
            get { return centroCusto; }
            set { centroCusto = value; }
        }

        [ManyToOne(Name = "IndiceFinanceiro", Column = "id_indice_financeiro", Class = "Modelo.IndiceFinanceiro, Modelo")]
        public virtual IndiceFinanceiro IndiceFinanceiro
        {
            get { return indiceFinanceiro; }
            set { indiceFinanceiro = value; }
        }

        [ManyToOne(Name = "Setor", Column = "id_setor", Class = "Modelo.Setor, Modelo")]
        public virtual Setor Setor
        {
            get { return setor; }
            set { setor = value; }
        }

        [ManyToOne(Name = "ContratoOriginal", Column = "id_contrato", Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual ContratoDiverso ContratoOriginal
        {
            get { return contratoOriginal; }
            set { contratoOriginal = value; }
        }

        [Bag(Name = "Renovacoes", Table = "contratos_diversos", Cascade = "delete")]
        [Key(2, Column = "id_contrato")]
        [OneToMany(3, Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual IList<ContratoDiverso> Renovacoes
        {
            get { return renovacoes; }
            set { renovacoes = value; }
        }

        [Bag(Table = "contratos_diversos_processos")]
        [Key(2, Column = "id_contrato_diverso")]
        [ManyToMany(3, Class = "Modelo.Processo, Modelo", Column = "id_processo")]
        public virtual IList<Processo> Processos
        {
            get { return processos; }
            set { processos = value; }
        }

        #endregion
    }
}
