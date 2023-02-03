using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Component(Class = "Modelo.Contato, Modelo")]
    public class Contato
    {
        #region ___________ Atributos ___________
        private string telefone;
        private int ramal;
        private string celular;
        private string email;
        private string fax;
        #endregion

        [Property]
        public virtual string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        [Property]
        [Column(1, SqlType = "text", Name = "email")]
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Property]
        public virtual string Celular
        {
            get { return celular; }
            set { celular = value; }
        }

        [Property]
        public virtual int Ramal
        {
            get { return ramal; }
            set { ramal = value; }
        }

        [Property]
        public virtual string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        public virtual string ContatoTelefones
        {
            get
            {

                string retorno = "";

                if (telefone != null && telefone == fax && telefone != "")
                {

                    if (telefone.Length == 10)
                    {
                        string area = Telefone.ToString().Substring(0, 2);
                        string ddd = Telefone.ToString().Substring(2, 4);
                        string tel = Telefone.ToString().Substring(6, 4);
                        retorno = "TELEFAX: (" + area + ")" + ddd + "-" + tel;
                    }
                    if (celular != null)
                    {
                        if (celular.Length == 10)
                        {
                            string area = Celular.ToString().Substring(0, 2);
                            string ddd = Celular.ToString().Substring(2, 4);
                            string tel = Celular.ToString().Substring(6, 4);
                            retorno = retorno + " | Celular: " + "(" + area + ")" + ddd + "-" + tel;
                        }
                    }
                }
                else
                {
                    if (telefone != null)
                    {
                        if (telefone.Length == 10)
                        {
                            string area = Telefone.ToString().Substring(0, 2);
                            string ddd = Telefone.ToString().Substring(2, 4);
                            string tel = Telefone.ToString().Substring(6, 4);
                            retorno = "TEL: (" + area + ")" + ddd + "-" + tel;
                        }
                    }

                    if (ramal != 0)
                    {
                        retorno = retorno + " | Ramal: " + Ramal + " ";
                    }

                    if (fax != null)
                    {
                        if (fax.Length == 10)
                        {
                            string area = Fax.ToString().Substring(0, 2);
                            string ddd = Fax.ToString().Substring(2, 4);
                            string tel = Fax.ToString().Substring(6, 4);
                            retorno = retorno + " | Fax: " + "(" + area + ")" + ddd + "-" + tel;
                        }
                    }

                    if (celular != null)
                    {
                        if (celular.Length == 10)
                        {
                            string area = Celular.ToString().Substring(0, 2);
                            string ddd = Celular.ToString().Substring(2, 4);
                            string tel = Celular.ToString().Substring(6, 4);
                            retorno = retorno + " | Celular: " + "(" + area + ")" + ddd + "-" + tel;
                        }
                    }
                }
                return retorno;
            }
        }

        public override string ToString()
        {
            return this.Email != "" ? this.Email : "";
        }
    }
}
