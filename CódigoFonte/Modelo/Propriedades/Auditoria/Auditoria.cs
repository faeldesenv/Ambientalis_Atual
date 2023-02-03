using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo.Auditoria
{
    [Serializable]
    [Class(Name = "Modelo.Auditoria.Auditoria, Modelo", Table = "auditoria")]
    public partial class Auditoria:ObjetoBase
    {
        #region ________ Atributos ________

        private Int64 transacao;
        private int xtype;
        private string operacao;
        private string tabela;
        private string coluna;
        private string valor_new;
        private string valor_old;
        private Auditoria_Users auditoria_user;
        private string registro;

        #endregion

        #region ________ Construtores ________

        public Auditoria(int id) { this.Id = id; }
        public Auditoria(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Auditoria() { }

        #endregion

        #region ________ Propriedades ________

        /// <summary>
        /// transação do banco de dados que gerou esta auditoria
        /// </summary>
        [Property]
        [Column(1, Name = "transacao", SqlType="bigint")]
        public virtual Int64 Transacao
        {
            get { return transacao; }
            set { transacao = value; }
        }

        /// <summary>
        /// inserção, alteração, exclusão
        /// </summary>
        [Property]
        public virtual string Operacao
        {
            get { return operacao; }
            set { operacao = value; }
        }

        /// <summary>
        /// Tipo da Coluna Ex(int, varvhar, text) Prem representado por numero int=175
        /// </summary>
        [Property]
        public virtual int Xtype
        {
            get { return xtype; }
            set { xtype = value; }
        }

        /// <summary>
        /// o id do objeto alterado
        /// </summary>
        [Property]
        public virtual string Registro
        {
            get { return registro; }
            set { registro = value; }
        }

        /// <summary>
        /// Coluna do bando de dados
        /// </summary>
        [Property]
        public virtual string Coluna
        {
            get { return coluna; }
            set { coluna = value; }
        }

        /// <summary>
        /// tabela do banco de dados
        /// </summary>
        [Property]
        [Column(1, Name = "tabela", SqlType = "nchar(255)")]
        public virtual string Tabela
        {
            get { return tabela; }
            set { tabela = value; }
        }

        /// <summary>
        /// valor que foi gerado com uma inserção ou alteração
        /// </summary>
        [Property]
        [Column(1, Name = "valor_new", SqlType = "nvarchar(max)")]
        public virtual string Valor_new
        {
            get { return valor_new; }
            set { valor_new = value; }
        }

        /// <summary>
        /// valor alterado ou excluído
        /// </summary>
        [Property]
        [Column(1, Name = "valor_old", SqlType = "nvarchar(max)")]
        public virtual string Valor_old
        {
            get { return valor_old; }
            set { valor_old = value; }
        }

        [ManyToOne(Name = "Auditoria_user", Column = "id_usuario", Class = "Modelo.Auditoria.Auditoria_Users, Modelo")]
        public virtual Auditoria_Users Auditoria_user
        {
            get { return auditoria_user; }
            set { auditoria_user = value; }
        }

        #endregion
    }
}
