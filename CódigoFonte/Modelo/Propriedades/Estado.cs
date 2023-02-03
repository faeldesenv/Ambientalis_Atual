using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "estados", Name = "Modelo.Estado, Modelo")]
    public partial class Estado : ObjetoBase
    {
        #region ________ Atributos ___________

        private string nome;
        private IList<Cidade> cidades;        

        #endregion

        #region ________ Construtores ________

        public Estado(int id) { this.Id = id; }
        public Estado(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Estado() { }

        #endregion

        #region ________ Propriedades ________

        [Bag(Name = "Cidades", Table = "cidades")]
        [Cache(1, Region = "Longo", Usage = CacheUsage.NonStrictReadWrite)]
        [Key(2, Column = "id_estado")]
        [OneToMany(3, Class = "Modelo.Cidade, Modelo")]
        public virtual IList<Cidade> Cidades
        {
            get { return cidades; }
            set { cidades = value; }
        }

        [Property]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        #endregion

        #region ________ Métodos _____________


        public virtual string PegarSiglaEstado()
        {
            switch (this.Id)
            {
                case 1:
                    return "AC";
                case 2:
                    return "AL";
                case 3:
                    return "AP";
                case 4:
                    return "AM";
                case 5:
                    return "BA";
                case 6:
                    return "CE";
                case 7:
                    return "DF";
                case 8:
                    return "ES";
                case 9:
                    return "GO";
                case 10:
                    return "MA";
                case 11:
                    return "MT";
                case 12:
                    return "MS";
                case 13:
                    return "MG";
                case 14:
                    return "PA";
                case 15:
                    return "PB";
                case 16:
                    return "PR";
                case 17:
                    return "PE";
                case 18:
                    return "PI";
                case 19:
                    return "RJ";
                case 20:
                    return "RN";
                case 21:
                    return "RS";
                case 22:
                    return "RO";
                case 23:
                    return "RR";
                case 24:
                    return "SC";
                case 25:
                    return "SP";
                case 26:
                    return "SE";
                case 27:
                    return "TO";
                default: return "";
            }
        }

        #endregion
    }
}
