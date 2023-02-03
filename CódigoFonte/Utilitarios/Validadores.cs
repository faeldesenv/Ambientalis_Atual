using System;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Utilitarios
{
    /// <summary>
    /// Classe com varios m�todos de validacao
    /// </summary>
    public class Validadores
    {
        /// <summary>
        /// Verifica se uma string � compat�vel com uma data v�lida
        /// </summary>
        /// <param name="m_data">A string que representa a data</param>
        /// <returns>true caso a string seja data e false caso contr�rio</returns>
        public static bool IsDate(string m_data)
        {
            DateTime m_dataTemp = new DateTime();

            try
            {
                m_dataTemp = DateTime.Parse(m_data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se uma string � compat�vel com um n�mero v�lido
        /// </summary>
        /// <param name="m_numero">A string que representa o n�mero</param>
        /// <returns>true caso a string seja um n�mero e fase caso contr�rio</returns>
        public static bool IsNumeric(string m_numero)
        {
            decimal m_numTemp = 0;

            try
            {
                m_numTemp = decimal.Parse(m_numero);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se uma string representa um CPF v�lido
        /// </summary>
        /// <param name="CPF">A string que representa o CPF</param>
        /// <returns>true caso a string seja um cpf v�lido e false caso contr�rio</returns>
        public static bool ValidaCPF(string CPF)
        {
            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "");
            CPF = CPF.Replace("-", "");
            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            // Pega a string passada no parametro
            string cpf = CPF;

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] n = new int[11];

            bool retorno = false;

            // Limpa a string
            cpf = CPF.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");

            // Se o tamanho for < 11 entao retorna como inv�lido
            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "2222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }
            try
            {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1));
                n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1));
                n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1));
                n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1));
                n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
                soma += (peso1[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
                soma += (peso2[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            resto = soma % 11;

            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao � v�lido
            if (calculado == digitado)
                retorno = true;
            else
                retorno = false;

            return retorno;
        }

        /// <summary>
        /// Verifica se uma string representa um CNPJ v�lido
        /// </summary>
        /// <param name="cnpj">A string qwue representa o CNPJ</param>
        /// <returns>true caso a string seja um CNPJ v�lido e false caso contr�rio</returns>
        public static bool ValidaCNPJ(string cnpj)
        {
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("-", "");
            cnpj = cnpj.Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            string l, inx, dig;
            int s1, s2, i, d1, d2, v, m1, m2;
            inx = cnpj.Substring(12, 2);
            cnpj = cnpj.Substring(0, 12);
            s1 = 0;
            s2 = 0;
            m2 = 2;
            for (i = 11; i >= 0; i--)
            {
                l = cnpj.Substring(i, 1);
                v = Convert.ToInt16(l);
                m1 = m2;
                m2 = m2 < 9 ? m2 + 1 : 2;
                s1 += v * m1;
                s2 += v * m2;
            }
            s1 %= 11;
            d1 = s1 < 2 ? 0 : 11 - s1;
            s2 = (s2 + 2 * d1) % 11;
            d2 = s2 < 2 ? 0 : 11 - s2;
            dig = d1.ToString() + d2.ToString();

            return (inx == dig);

        }

        /// <summary>
        /// Verifica se uma string representa um e-mail v�lido
        /// </summary>
        /// <param name="email">A string que representa o e-mail</param>
        /// <returns>true caso seja um e-mail v�lido e false caso contr�rio</returns>
        public static bool ValidaEmail(string email)
        {
            bool valido = false;

            string expressoesEmail = @"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$";
            Match match = Regex.Match(email, expressoesEmail);

            if (match.Success)
            {
                valido = true;
            }

            try
            {
                new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }


            return valido;
        }

        /// <summary>
        /// Verifica se um determinado arquivo � do tipo imagem
        /// </summary>
        /// <param name="caminhoArquivo">O caminho completo do arquivo</param>
        /// <returns>true caso a extens�o seja do tipo imagem e false caso contr�rio</returns>
        public static bool EhImagem(string caminhoArquivo)
        {
            string fileExtension = System.IO.Path.GetExtension(caminhoArquivo).ToLower();
            foreach (string ext in new string[] { ".gif", ".jpeg", ".jpg", ".png" })
                if (fileExtension == ext)
                    return true;
            return false;
        }
    }
}