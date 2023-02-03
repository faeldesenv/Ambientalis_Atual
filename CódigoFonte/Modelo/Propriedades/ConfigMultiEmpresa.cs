using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "config_multi_empresas", Name = "Persistencia.ConfigMultiEmpresa, Persistencia")]
    public class ConfigMultiEmpresa : ObjetoBase
    {
        private string entidade;
        private bool visaoGlobal;

        [Property]
        public virtual String Entidade
        {
            get { return entidade; }
            set { entidade = value; }
        }

        [Property(Type = "TrueFalse", Column = "visao_global")]
        public virtual bool VisaoGlobal
        {
            get { return visaoGlobal; }
            set { visaoGlobal = value; }
        }
    }
}