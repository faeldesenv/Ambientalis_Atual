using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Utilitarios;
using Utilitarios.Criptografia;

/// <summary>
/// Classe Responsável por manipular os relatórios da aplicação
/// </summary>
public static class Relatorios
{
    public static void CarregarRelatorio(string titulo, string nomeRelatorio, bool retrato, params DataTable[] fontes)
    {
        if (HttpContext.Current.Session["relatorio_sistema_ambiental" + nomeRelatorio] != null)
            HttpContext.Current.Session["relatorio_sistema_ambiental" + nomeRelatorio] = null;
        HttpContext.Current.Session["relatorio_sistema_ambiental" + nomeRelatorio] = fontes;

        HttpContext.Current.Response.Redirect("../Relatorios/Relatorios.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("titulo_relatorio=" + titulo +
            "&nome_relatorio=" + nomeRelatorio +
            "&orientacao_relatorio=" + (retrato ? "Retrato" : "Paisagem") +
            "&fontes_de_dados=" + "relatorio_sistema_ambiental" + nomeRelatorio));
    }

    public static void CarregarRelatorioAdministrativo(string titulo, string nomeRelatorio, bool retrato, params DataTable[] fontes)
    {
        if (HttpContext.Current.Session["relatorio_sistema_ambiental" + nomeRelatorio] != null)
            HttpContext.Current.Session["relatorio_sistema_ambiental" + nomeRelatorio] = null;
        HttpContext.Current.Session["relatorio_sistema_ambiental" + nomeRelatorio] = fontes;

        HttpContext.Current.Response.Redirect("../Adm/ADMRelatorios.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("titulo_relatorio=" + titulo +
            "&nome_relatorio=" + nomeRelatorio +
            "&orientacao_relatorio=" + (retrato ? "Retrato" : "Paisagem") +
            "&fontes_de_dados=" + "relatorio_sistema_ambiental" + nomeRelatorio));
    }
}