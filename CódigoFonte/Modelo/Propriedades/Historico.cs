using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "historicos", Name = "Modelo.Historico, Modelo")]
    public partial class Historico : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string alteracao;
        private string observacao;
        private DateTime dataAlteracao;
        private Regime regime;
        private Condicional condicional;
        private Exigencia exigencia;
        private RAL ral;
        private GuiaUtilizacao guia;
        private CadastroTecnicoFederal cadastroTecnicoFederal;
        private Diverso diverso;
        private ContratoDiverso contratoDiverso;

        [ManyToOne(Name = "Diverso", Column = "id_diverso", Class = "Modelo.Diverso, Modelo")]
        public virtual Diverso Diverso
        {
            get { return diverso; }
            set { diverso = value; }
        }

        #endregion

        public Historico(int id) { this.Id = id; }
        public Historico(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Historico() { }

        [ManyToOne(Name = "ContratoDiverso", Column = "id_contrato_diverso", Class = "Modelo.ContratoDiverso, Modelo")]
        public virtual ContratoDiverso ContratoDiverso
        {
            get { return contratoDiverso; }
            set { contratoDiverso = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "alteracao")]
        public virtual string Alteracao
        {
            get { return alteracao; }
            set { alteracao = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "observacao")]
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        [Property(Column = "data_criacao")]
        public virtual DateTime DataPublicacao
        {
            get
            {
                if (dataAlteracao <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataAlteracao;
            }
            set { dataAlteracao = value; }
        }


        [ManyToOne(Name = "GuiaUtilizacao", Column = "id_guia_utilizacao", Class = "Modelo.GuiaUtilizacao, Modelo")]
        public virtual GuiaUtilizacao GuiaUtilizacao
        {
            get { return guia; }
            set { guia = value; }
        }

        [ManyToOne(Name = "Regime", Column = "id_regime", Class = "Modelo.Regime, Modelo")]
        public virtual Regime Regime
        {
            get { return regime; }
            set { regime = value; }
        }

        [ManyToOne(Name = "Condicional", Column = "id_condicional", Class = "Modelo.Condicional, Modelo")]
        public virtual Condicional Condicional
        {
            get { return condicional; }
            set { condicional = value; }
        }

        [ManyToOne(Name = "Exigencia", Column = "id_exigencia", Class = "Modelo.Exigencia, Modelo")]
        public virtual Exigencia Exigencia
        {
            get { return exigencia; }
            set { exigencia = value; }
        }

        [ManyToOne(Name = "RAL", Column = "id_ral", Class = "Modelo.RAL, Modelo")]
        public virtual RAL RAL
        {
            get { return ral; }
            set { ral = value; }
        }

        [ManyToOne(Name = "CadastroTecnicoFederal", Column = "id_cadastro_tecnico_federal", Class = "Modelo.CadastroTecnicoFederal, Modelo")]
        public virtual CadastroTecnicoFederal CadastroTecnicoFederal
        {
            get { return cadastroTecnicoFederal; }
            set { cadastroTecnicoFederal = value; }
        }

    }
}
