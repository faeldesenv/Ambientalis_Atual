using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "atividades", Name = "Modelo.Atividade, Modelo")]
    public partial class Atividade : ObjetoBase
    {
        #region ___________ Atributos ___________

        private string nome;
        private IList<Cliente> clientes;
        private IList<Fornecedor> fornecedores;

        #endregion

        public Atividade(int id) { this.Id = id; }
        public Atividade(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Atividade() { }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Fornecedores", Table = "fornecedores")]
        [Key(2, Column = "id_atividade")]
        [OneToMany(3, Class = "Modelo.Fornecedor, Modelo")]
        public virtual IList<Fornecedor> Fornecedores
        {
            get { return fornecedores; }
            set { fornecedores = value; }
        }

        [Bag(Name = "Clientes", Table = "clientes")]
        [Key(2, Column = "id_atividade")]
        [OneToMany(3, Class = "Modelo.Cliente, Modelo")]
        public virtual IList<Cliente> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

    }
}
