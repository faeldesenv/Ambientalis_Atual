using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utilitarios;
using Microsoft.Reporting.WebForms;
using Utilitarios.Criptografia;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;

public partial class ADMRelatorios_Relatorios : System.Web.UI.Page
{
    #region ___________ EXEMPLO CARREGAMENTO RELATÓRIO _______________

    //Para criar um relatório, siga os seguintes passos?
    // 1 - Crie um dataTable para representar os dados que deseja carregar norelatório
    // 2 - Na tela de relatórios, clique na seta superior direita no componente de relatórios
    // 3 - Escolha a opção "Design a new Report" para começar a criar seu relatório
    // 4 - Feche a tela que irá aparecer de configuração com o banco de dados
    // 5 - Na tela que foi aberta, selecione o DataSource da aplicação = "Fontes"
    // 6 - Na opção de Seleção "Avaliable DataSets" escolhe o dataSet que criou no item 1
    // 7 - Coloque o nome (primeira opção desta mesma tela) exatamente igual ao nome do DataSet selecionado no item 6
    // 8 - Configure seu relatório do jeito que quiser

    // Como Carregar o relatório que acabou de criar? Abaixo um exemplo:

    //Fontes.testeDataTable fonte = new Fontes.testeDataTable();
    //    Teste aux = new Teste("Hugo", "23");
    //    for (int i = 0; i < 100; i++)
    //        fonte.Rows.Add(aux.nome, aux.idade);
    //    Relatorios.CarregarRelatorio("Teste", "Teste", fonte);

    #endregion

    private string titulo;
    private string nomeRelatorio;
    private DataTable[] fontesDeDados;
    private bool retrato = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.rpvRelatorios.LocalReport.EnableExternalImages = true;
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
            this.rpvRelatorios.LocalReport.DataSources.Clear();
            this.rpvRelatorios.Height = new Unit(this.retrato ? "29cm" : "21cm");
            this.rpvRelatorios.Width = new Unit(this.retrato ? "21cm" : "29cm");
            foreach (DataTable fonte in this.fontesDeDados)
                this.rpvRelatorios.LocalReport.DataSources.Add(new ReportDataSource(fonte.TableName, fonte));
            this.rpvRelatorios.LocalReport.ReportPath = WebUtil.GetPathAplicacao + "//Relatorios//Relatorios//" + this.nomeRelatorio;
            this.rpvRelatorios.DataBind();
        }
        else
            Response.Redirect(this.PreviousPage != null ? this.PreviousPage.Request.Url.AbsoluteUri : "../Site/Index.aspx");
    }

    protected void rpvRelatorios_ReportError(object sender, Microsoft.Reporting.WebForms.ReportErrorEventArgs e)
    {
        Alert.Show(e.Exception.Message);
    }

}