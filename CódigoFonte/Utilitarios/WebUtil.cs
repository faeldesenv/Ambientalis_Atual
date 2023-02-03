﻿using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios.Criptografia;
using System.Collections.Generic;
using System.Web;
using Modelo;
using Persistencia.Services;

namespace Utilitarios
{
    public class WebUtil
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public WebUtil()
        {
        }

        /// <summary>
        /// Adiciona uma confirmação a um botão de Imagem (ImageGutton)
        /// </summary>
        /// <param name="btn">O botão</param>
        /// <param name="menssagem">a mensagem de confirmação</param>
        public static void AdicionarConfirmacao(ImageButton btn, string menssagem)
        {
            btn.Attributes.Add("onclick", "javascript: return confirm('" + menssagem + "');");
        }

        public static void AdicionarConfirmacao(LinkButton lbtn, string menssagem)
        {
            lbtn.Attributes.Add("onclick", "javascript: return confirm('" + menssagem + "');");
        }

        public static void AdicionarAlerta(ImageButton ibtn, string mensagem)
        {
            ibtn.Attributes.Add("onclick", "alert('" + mensagem + "')");
        }

        public static DateTime MenorValorDataSqlServer2000
        {
            get { return Convert.ToDateTime("01/01/1754"); }
        }

        public static void AdicionarAlerta(LinkButton lbtn, string mensagem)
        {
            lbtn.Attributes.Add("onclick", "alert('" + mensagem + "')");
        }

        /// <summary>
        /// Adiciona uma confirmação a um botão (Button)
        /// </summary>
        /// <param name="btn">O botão</param>
        /// <param name="menssagem">A mensagem de confirmação</param>
        public static void AdicionarConfirmacao(Button btn, string menssagem)
        {
            btn.Attributes.Add("onclick", "javascript: return confirm('" + menssagem + "');");
        }

        /// <summary>
        /// Método genérico para limar campos de uma coleção de controles
        /// </summary>
        /// <param name="controls">A coleção de controles</param>
        public static void LimparCampos(ControlCollection controls)
        {
            foreach (Control controle in controls)
            {
                switch (controle.GetType().Name)
                {
                    case "Literal":
                        {
                            try
                            {
                                Literal ltr = (Literal)controle;
                                ltr.Text = "0";
                            }
                            catch (Exception)
                            { }
                        } continue;

                    case "TextBox":
                        {
                            try
                            {
                                TextBox txt = (TextBox)controle;
                                txt.Text = "";
                            }
                            catch (Exception)
                            { }
                        } continue;
                    case "DropDownList":
                        {
                            try
                            {
                                DropDownList ddl = (DropDownList)controle;
                                ddl.SelectedItem.Selected = false;
                                ddl.SelectedIndex = 0;
                            }
                            catch (Exception)
                            { }
                        } continue;

                    case "CheckBox":
                        {
                            try
                            {
                                CheckBox ckb = (CheckBox)controle;
                                ckb.Checked = false;
                            }
                            catch (Exception)
                            { }
                        } break;

                    case "CheckBoxList":
                        {
                            try
                            {
                                CheckBoxList ckbl = (CheckBoxList)controle;
                                ckbl.SelectedItem.Selected = false;
                            }
                            catch (Exception)
                            { }
                        } continue;
                    case "RadioButton":
                        {
                            try
                            {
                                RadioButton rbd = (RadioButton)controle;
                                rbd.Checked = false;
                            }
                            catch (Exception)
                            { }
                        } continue;
                    case "RadioButtonList":
                        {
                            try
                            {
                                RadioButtonList rbdl = (RadioButtonList)controle;
                                rbdl.SelectedItem.Selected = false;
                            }
                            catch (Exception)
                            { }
                        } continue;
                }
            }
        }

        public static void SetarCursorAjuda(ImageButton imgButton)
        {
            imgButton.Attributes.Add("onMouseOver", "this.style.cursor='help';");
        }

        public static void TituloTela(Page page)
        {
            page.Title = ConfigurationManager.AppSettings["TituloAplicacao"].ToString();
        }

        public static void Alerta(Page page, String Msg)
        {
            if (Msg != null && Msg != "")
            {
                string mensagem = "	<script language=javascript> ";
                mensagem += "alert( '" + Msg + "')";
                mensagem += "</script>";
                page.ClientScript.RegisterStartupScript(page.GetType(), "alerta", mensagem);
            }
        }

        public static string EstadoObjetoManipulado(string nome)
        {
            try
            {
                string estado;
                if (nome == "" || nome == null)
                {
                    return "  [Novo(a)]   ";
                }
                else
                {
                    return "  " + nome + "   ";
                }
            }
            catch (Exception)
            {
                return "  [Novo(a)]   ";
            }
        }

        public static void CriarEventoOnMouseOverDoGridView(DataGridItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                e.Item.Attributes.Add("onMouseOver", "this.style.backgroundColor='#b5c7de';");
                if (e.Item.ItemIndex % 2 == 0)
                {
                    e.Item.Attributes.Add("onMouseOut", "this.style.backgroundColor='#F7F6F3';");
                }
                else
                {
                    e.Item.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff';");
                }
            }
        }

        public static void CriarEventoOnMouseOverDoGridView(GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() != "Header")
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#b5c7de';");
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#eeede9';");
                }
                else
                {
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#cccccc';");
                }
            }
        }

        #region ________ AdicionarEventoShowModalDialog _______

        public static void AdicionarEventoShowModalDialog(ImageButton ibtn, string urlPagina, string titulo, int larguraJanela, int alturaJanela)
        {
            string caracter = "?";
            if (urlPagina.Contains("?"))
                caracter = "&";
            ibtn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
        }

        public static void AdicionarEventoShowModalDialog(ImageButton ibtn, string urlPagina, string titulo, int larguraJanela, int alturaJanela, string parametros, bool criptografado)
        {
            if (criptografado)
            {
                ibtn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + Seguranca.MontarParametros(parametros) + "&pop=true" + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
            else
            {
                string caracter = "?";
                if (urlPagina.Contains("?"))
                    caracter = "&";
                ibtn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true&" + parametros + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
        }

        public static void AdicionarEventoShowModalDialog(Button btn, string urlPagina, string titulo, int larguraJanela, int alturaJanela)
        {
            string caracter = "?";
            if (urlPagina.Contains("?"))
                caracter = "&";
            btn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
        }

        public static void AdicionarEventoShowModalDialog(Button btn, string urlPagina, string titulo, int larguraJanela, int alturaJanela, string parametros, bool criptografado)
        {
            if (criptografado)
            {
                btn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + Seguranca.MontarParametros(parametros) + "&pop=true" + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
            else
            {
                string caracter = "?";
                if (urlPagina.Contains("?"))
                    caracter = "&";
                btn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true&" + parametros + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
        }

        public static void AdicionarEventoShowModalDialog(RadioButton rb, string urlPagina, string titulo, int larguraJanela, int alturaJanela)
        {
            string caracter = "?";
            if (urlPagina.Contains("?"))
                caracter = "&";
            rb.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
        }

        public static void AdicionarEventoShowModalDialog(RadioButton rb, string urlPagina, string titulo, int larguraJanela, int alturaJanela, string parametros, bool criptografado)
        {
            if (criptografado)
            {
                rb.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + Seguranca.MontarParametros(parametros) + "&pop=true" + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
            else
            {
                string caracter = "?";
                if (urlPagina.Contains("?"))
                    caracter = "&";
                rb.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true&" + parametros + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
        }

        public static void AdicionarEventoShowModalDialog(LinkButton lbtn, string urlPagina, string titulo, int larguraJanela, int alturaJanela)
        {
            string caracter = "?";
            if (urlPagina.Contains("?"))
                caracter = "&";
            lbtn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:no;');");
        }

        public static void AdicionarEventoShowModalDialog(LinkButton lbtn, string urlPagina, string titulo, int larguraJanela, int alturaJanela, string parametros, bool criptografado)
        {
            if (criptografado)
            {
                lbtn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + Seguranca.MontarParametros(parametros) + "&pop=true" + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
            else
            {
                string caracter = "?";
                if (urlPagina.Contains("?"))
                    caracter = "&";
                lbtn.Attributes.Add("onclick", "window.showModalDialog('" + urlPagina + caracter + "pop=true&" + parametros + "','" + titulo + "','dialogWidth:" + larguraJanela.ToString() + "px;dialogHeight:" + alturaJanela.ToString() + "px;center:yes;');");
            }
        }


        #endregion

        #region __________ AdicionarEventoModalDialog _________

        public static void AdicionarEventoShowDialog(ImageButton ibtn, string urlPagina, string titulo, int larguraJanela, int alturaJanela)
        {
            string caracter = "?";
            if (urlPagina.Contains("?"))
                caracter = "&";
            ibtn.Attributes.Add("onclick", "window.open('" + urlPagina + caracter + "pop=true','" + titulo + "','height=" + alturaJanela.ToString() + ", width=" + larguraJanela.ToString() + ", scrollbars=no, resizable=no');");
        }

        #endregion

        public static void AdicionarEventoAbrirJanela(ImageButton ibtn, string urlPagina)
        {
            ibtn.OnClientClick = "window.open('" + urlPagina + "')";
        }

        public static void AdicionarEventoAbrirJanela(ImageButton ibtn, string urlPagina, string parametros, bool criptografado)
        {
            if (criptografado)
            {
                ibtn.OnClientClick = "window.open('" + urlPagina + Seguranca.MontarParametros(parametros) + "')";
            }
            else
            {
                ibtn.OnClientClick = "window.open('" + urlPagina + "?" + parametros + "')";
            }
        }

        public static void FecharJanela(Page page)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered("ClosePopUp"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "ClosePopUp", "<script>if(navigator.appName==\"Microsoft Internet Explorer\") {this.focus();self.opener = this;self.close(); }else { window.open('','_parent',''); window.close(); }</script>");
        }

        public static string GetIdConfig
        {
            get
            {
                if (ConfigurationManager.AppSettings["idConfig"] != null)
                    return ConfigurationManager.AppSettings["idConfig"].ToString();
                else
                    return "--";
            }
        }

        public static string GetPathAplicacao
        {
            get
            {
                if (PathApplication.pathApplication != null)
                    return PathApplication.pathApplication;
                else
                    return "--";
            }
        }

        public static void RedirectToPage(Page page)
        {
            page.Response.Redirect(page.Request.Url.AbsoluteUri, false);
        }

        public static string GetURLImagemLogoRelatorio
        {
            get
            {
                return "../imagens/logo_relatorio.png";
            }
        }

        public static void InserirTriggerDinamica(Control controle, String nomeEvento, UpdatePanel updatePanel)
        {
            string u = controle.UniqueID;
            AsyncPostBackTrigger tr = new AsyncPostBackTrigger();
            tr.ControlID = u;
            tr.EventName = nomeEvento;
            updatePanel.Triggers.Add(tr);
        }

        public static bool ValidarEmailInformado(string emails)
        {
            bool retorno = false;

            if (emails.Contains(";"))
            {
                IList<String> emailsAux = new List<String>();

                emailsAux = emails.Split(new string[]{";"}, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < emailsAux.Count; j++)
                {

                    if (emailsAux[j].Contains(")"))
                    {
                        string[] emailsComNome = emailsAux[j].Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < emailsComNome.Length; i++)
                        {
                            if (Validadores.ValidaEmail(emailsComNome[i].Trim()))
                                retorno = true;
                            else
                                retorno = false;
                        }
                    }
                    else
                    {
                        if (Validadores.ValidaEmail(emailsAux[j].Trim()))
                            retorno = true;
                        else
                            retorno = false;
                    }

                    if (retorno == false)
                        return false;
                }
            }
            else
            {
                if (emails.Contains(")"))
                {
                    string[] emailsComNome = emails.Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < emailsComNome.Length; i++)
                    {
                        if (Validadores.ValidaEmail(emailsComNome[i].Trim()))
                            retorno = true;
                        else
                            retorno = false;
                    }
                }
                else
                {
                    if (Validadores.ValidaEmail(emails.Trim()))
                        retorno = true;
                    else
                        retorno = false;
                }
            }

            return retorno;
        }


        public static string GetImagemLogo
        {
            get
            {
                //Modelo.Pessoas.Empresa empresa = Modelo.Pessoas.Empresa.ConsultarPorId(WebUtil.Fil);
                //return empresa != null && empresa.Imagem != null ? empresa.Imagem.UrlImagemG : "../imagens/notFoto.gif";
                return "";
            }
        }

        public static Modelo.Usuario UsuarioLogado
        {
            get
            {
                return HttpContext.Current.Session["UsuarioLogado_SistemaAmbiental"] as Modelo.Usuario;
            }
            set
            {
                HttpContext.Current.Session["UsuarioLogado_SistemaAmbiental"] = value;
            }
        }

        public static Modelo.UsuarioComercial UsuarioComercialLogado
        {
            get
            {
                return HttpContext.Current.Session["UsuarioLogado_SistemaComercial"] as Modelo.UsuarioComercial;
            }
            set
            {
                HttpContext.Current.Session["UsuarioLogado_SistemaComercial"] = value;
            }
        }

        public static string GetDescricaoDataRelatorio(DateTime dataDe, DateTime dataAteh)
        {
            string retorno = "Todas";
            if (dataDe.CompareTo(SqlDate.MinValue) > 0 || dataAteh.CompareTo(SqlDate.MaxValue) < 0)
            {
                if (dataDe.CompareTo(SqlDate.MinValue) > 0 && dataAteh.CompareTo(SqlDate.MaxValue) < 0)
                    retorno = "de " + dataDe.ToShortDateString() + " até " + dataAteh.ToShortDateString();
                else
                    retorno = dataDe.CompareTo(SqlDate.MinValue) > 0 ? "após " + dataDe.ToShortDateString() : "antes de " + dataAteh.ToShortDateString();
            }
            return retorno;
        }
    }
}