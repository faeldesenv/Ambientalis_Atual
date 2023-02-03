using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe para impressão WEB
/// </summary>
public class Impressao
{
    public Impressao()
    {
    }

    /// <summary>
    /// Configura uma página de impressão com o controle passado como parâmetro
    /// </summary>
    /// <param name="ctrl">O controle a ser impresso</param>
    public static void PrintWebControl(Control ctrl)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;

        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// Configura uma página de impressão com o HTML passado como parâmetro
    /// </summary>
    /// <param name="HTML"></param>
    public static void PrintHTML(string HTML)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(HTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }
}