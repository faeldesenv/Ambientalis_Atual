using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "avisos", Name = "Modelo.Aviso, Modelo")]
    public partial class Aviso : ObjetoBase
    {
        #region ___________ Atributos ___________

        private DateTime dataInicio;
        private DateTime dataFim;
        private string descricao;
        private bool avisoComercial;

        #endregion

        public Aviso(int id) { this.Id = id; }
        public Aviso(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Aviso() { }

        [Property(Column = "data_inicio")]
        public virtual DateTime DataInicio
        {
            get
            {
                if (dataInicio <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataInicio;
            }
            set { dataInicio = value; }
        }

        [Property(Column = "data_fim")]
        public virtual DateTime DataFim
        {
            get
            {
                if (dataFim <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataFim;
            }
            set { dataFim = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "Descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Type = "TrueFalse", Column = "aviso_comercial")]
        public virtual bool AvisoComercial
        {
            get { return avisoComercial; }
            set { avisoComercial = value; }
        }

    }
}
