using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.IO;

public partial class Arquivos_CadastroArquivos : PageBase
{
    Msg msg = new Msg();
    UsuarioComercial user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UsuarioLogado_SistemaComercial"] != null)
            {
                this.user = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"];
                if (this.user != null)
                    this.HabilitaBotoes(user is UsuarioSupervisorComercial || user is UsuarioAdministradorComercial);
            }
            else
            {
                msg.CriarMensagem("Ocorreu um erro", "Erro", MsgIcons.Erro);
                Response.Redirect("../Acesso/Login.aspx");
            }
            this.CarregarArquivos();
        }
    }

    #region ___________Metodos_____________

    private void HabilitaBotoes(bool confirmacao)
    {
        if (!confirmacao)
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "<script>DesabilitaBotoes()</script>", false);
    }

    private void CarregarArquivos()
    {
        IList<ArquivoFisicoComercial> arquivos = ArquivoFisicoComercial.ConsultarTodos();
        rptArquivos.DataSource = arquivos;
        rptArquivos.DataBind();
    }

    private void SalvarArquivos()
    {
        if (fulArquivo.HasFile && (Session["UsuarioLogado_SistemaComercial"] is UsuarioSupervisorComercial || Session["UsuarioLogado_SistemaComercial"] is UsuarioAdministradorComercial))
        {
            string extensao = fulArquivo.FileName.Substring(fulArquivo.FileName.LastIndexOf('.'));
            string nome = "";
            string subPath = "/Repositorio/Arquivos";
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + subPath;

            do
            {
                nome = Guid.NewGuid().ToString().Substring(0, 10) + extensao;
            } while (System.IO.File.Exists(path + "/" + nome));

            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();

            fulArquivo.SaveAs(path + "/" + nome);

            ArquivoFisicoComercial arquivo = new ArquivoFisicoComercial();
            arquivo.Descricao = tbxDescricao.Text;
            arquivo.DataPublicacao = DateTime.Now;
            arquivo.Tamanho = fulArquivo.FileBytes.Length;
            arquivo.Caminho = (HttpContext.Current.Request.Url.Authority.Contains("localhost") ? "VisaoComercial" : "Comercial")
                + subPath + "/" + nome;
            arquivo.Host = HttpContext.Current.Request.Url.Authority;
            arquivo.Identificador = fulArquivo.FileName;

            arquivo.Salvar();
            msg.CriarMensagem("Arquivo salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
            this.CarregarArquivos();

        }
    }

    private void ExcluirArquivo(int id)
    {
        ArquivoFisicoComercial arquivo = ArquivoFisicoComercial.ConsultarPorId(id);
        if (arquivo != null)
            if (!arquivo.Excluir())
            {
                msg.CriarMensagem("Erro ao excluir.", "Erro", MsgIcons.Erro);
                return;
            }
        this.CarregarArquivos();
    }

    #endregion

    #region ___________Bindings____________

    #endregion

    #region ___________Eventos_____________

    protected void btnUpLoadArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarArquivos();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ibtnExcluirArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExcluirArquivo((((ImageButton)sender).CommandArgument).ToInt32());
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    #endregion

    #region __________Pre-Render___________

    protected void ibtnExcluirArquivo_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír o arquivo?");
    }

    #endregion

    #region __________ Triggers ___________

    #endregion

}