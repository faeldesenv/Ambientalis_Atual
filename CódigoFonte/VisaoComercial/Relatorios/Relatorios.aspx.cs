using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using System.Data;
using Microsoft.Reporting.WebForms;
using Modelo;

public partial class Relatorios_Relatorios : System.Web.UI.Page
{
    private string titulo;
    private string nomeRelatorio;
    private DataTable[] fontesDeDados;
    private bool retrato = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.rpvRelatoriosComercial.LocalReport.EnableExternalImages = true;
        if (!IsPostBack)
        {
            this.titulo = Utilitarios.Criptografia.Seguranca.RecuperarParametro("titulo_relatorio", Request);
            this.nomeRelatorio = Utilitarios.Criptografia.Seguranca.RecuperarParametro("nome_relatorio", Request);
            string aux = Utilitarios.Criptografia.Seguranca.RecuperarParametro("orientacao_relatorio", Request);
            this.retrato = aux.Trim() == "Retrato";
            if (this.nomeRelatorio.IsNotNullOrEmpty())
                this.nomeRelatorio = this.nomeRelatorio.EndsWith(".rdlc") ? this.nomeRelatorio : this.nomeRelatorio + ".rdlc";
            this.fontesDeDados = (DataTable[])Session[Utilitarios.Criptografia.Seguranca.RecuperarParametro("fontes_de_dados", Request)];
            this.CarregarRelatorio();
        }
    }

    private void CarregarRelatorio()
    {
        if (this.fontesDeDados != null)
        {
            this.Title = this.titulo;
            this.rpvRelatoriosComercial.LocalReport.DataSources.Clear();
            this.rpvRelatoriosComercial.Height = new Unit(this.retrato ? "29cm" : "21cm");
            this.rpvRelatoriosComercial.Width = new Unit(this.retrato ? "21cm" : "29cm");
            foreach (DataTable fonte in this.fontesDeDados)
                this.rpvRelatoriosComercial.LocalReport.DataSources.Add(new ReportDataSource(fonte.TableName, fonte));
            this.rpvRelatoriosComercial.LocalReport.ReportPath = WebUtil.GetPathAplicacao + "//Relatorios//Relatorios//" + this.nomeRelatorio;
            this.rpvRelatoriosComercial.DataBind();
        }
        else
            Response.Redirect(this.PreviousPage != null ? this.PreviousPage.Request.Url.AbsoluteUri : "../Site/Index.aspx");
    }

    protected void rpvRelatoriosComercial_ReportError(object sender, Microsoft.Reporting.WebForms.ReportErrorEventArgs e)
    {
        Alert.Show(e.Exception.Message);
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        UsuarioComercial user = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"];
        if(user != null && user.GetType() == typeof(UsuarioSupervisorComercial) || user.GetType() == typeof(UsuarioAdministradorComercial))
          Response.Redirect("FiltroRelatoriosSupervisor.aspx", false);
        else
            Response.Redirect("FiltrosRelatoriosRevendas.aspx", false);
    }
}