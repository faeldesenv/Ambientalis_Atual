using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "exigencias", Name = "Modelo.Exigencia, Modelo")]
    public partial class Exigencia : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string descricao;
        private string observacao;
        private int diasPrazo;       
        private DateTime dataPublicacao;
        private string linkArquivo;
        private Regime regime;
        private IList<Vencimento> vencimentos;
        private GuiaUtilizacao guia;
        private IList<ArquivoFisico> arquivos;
        private IList<Historico> historicos;

        #endregion

        // LEIA ANTES DE ALTERAR ALGUMA COISA
        // ------------------------------------------------------------
        //  importante ler antes de inserir um atributo:
        // - Existe um metodo que exclui todos os dias as exigencias que estejam com o guia e o regime nulo 
        //   , portanto se criar um relacionamento para para outra classe deve-se alterar 
        //  o metodo para verificar tambem este relacionamento, senao ele vai ser excluido toda noite.
        //  public static void ExcluirExigenciasAvulsas()
        // ------------------------------------------------------------
        // LEIA ANTES DE ALTERAR ALGUMA COISA
        // ------------------------------------------------------------
        // ------------------------------------------------------------


        public Exigencia(int id) { this.Id = id; }
        public Exigencia(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Exigencia() { }

        [Bag(Name = "Arquivos", Table = "arquivos_fisicos", Cascade = "delete")]
        [Key(2, Column = "id_exigencia")]
        [OneToMany(3, Class = "Modelo.ArquivoFisico, Modelo")]
        public virtual IList<ArquivoFisico> Arquivos
        {
            get { return arquivos; }
            set { arquivos = value; }
        }

        [Property]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "observacao")]
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        [Property(Column = "dias_prazo")]
        public virtual int DiasPrazo
        {
            get { return diasPrazo; }
            set { diasPrazo = value; }
        }

        [Property(Column = "data_publicacao")]
        public virtual DateTime DataPublicacao
        {
            get
            {
                if (dataPublicacao <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataPublicacao;
            }
            set { dataPublicacao = value; }
        }

        [Property(Column = "link_arquivo")]
        public virtual string LinkArquivo
        {
            get { return linkArquivo; }
            set { linkArquivo = value; }
        }

        [Bag(Name = "Vencimentos", Table = "vencimentos", Cascade = "delete")]
        [Key(2, Column = "id_exigencia")]
        [OneToMany(3, Class = "Modelo.Vencimento, Modelo")]
        public virtual IList<Vencimento> Vencimentos
        {
            get { return vencimentos; }
            set { vencimentos = value; }
        }

        [ManyToOne(Name = "GuiaUtilizacao", Column = "id_guia_utilizacao", Class = "Modelo.GuiaUtilizacao, Modelo", Cascade = "delete")]
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

        [Bag(Name = "Historicos", Table = "historicos", Cascade = "delete")]
        [Key(2, Column = "id_exigencia")]
        [OneToMany(3, Class = "Modelo.Historico, Modelo")]
        public virtual IList<Historico> Historicos
        {
            get { return historicos; }
            set { historicos = value; }
        }

    }
}
