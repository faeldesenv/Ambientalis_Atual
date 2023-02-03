using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using System.IO;
using System.Windows.Forms;
using Modelo;

public partial class Adm_CadastroClientesSite : PageBase
{
    private Msg msg = new Msg();
    Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = 0;
                transacao.Abrir();
                this.carregarClientes();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                this.GetMBOX<MBOX>().Show(msg);
            }
        }
    }

    private void carregarCampos()
    {
        throw new NotImplementedException();
    }

    protected void btnUploadImagem_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuiGrupoEconomico.FileContent.Length >= 500000)
            {
                msg.CriarMensagem("O tamanho da imagem ultrapassa 450kb. Favor reduzir o tamanho.", "");
                return;
            }
            if (fuiGrupoEconomico.HasFile)
            {
                string extensao = fuiGrupoEconomico.FileName.Substring(fuiGrupoEconomico.FileName.LastIndexOf('.')).ToLower();
                if (extensao == ".jpeg" || extensao == ".jpg" || extensao == ".gif" || extensao == ".png" || extensao == ".bmp")
                {
                    string nome = "";
                    string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Adm/Repositorio/GruposEconomicos/Logomarcas/";
                    do
                    {
                        nome = Guid.NewGuid().ToString().Substring(0, 10) + extensao;
                    } while (System.IO.File.Exists(path + "/" + nome));

                    //Criar diretorio
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                    if (!dir.Exists)
                        dir.Create();
                    fuiGrupoEconomico.SaveAs(path + nome);
                    imgLogomarca.ImageUrl = "http://" + HttpContext.Current.Request.Url.Authority + "/" + (HttpContext.Current.Request.Url.Authority.Contains("localhost") ? "Visao" : "Sistema") +
                        "/Adm/Repositorio/GruposEconomicos/Logomarcas/" + nome;
             
                }
                else
                {
                    msg.CriarMensagem("O arquivo não é um tipo de imagem válido. Salve nos formatos .jpeg, .jpg, .gif ou .png ou .bmp.", "");
                    return;
                }
            }
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


    private void carregarClientes()
    {
        IList<GrupoEconomicoSite> listaClientes = GrupoEconomicoSite.ConsultarTodos();
        if (listaClientes != null && listaClientes.Count > 0)
        {
            grvClientes.DataSource = listaClientes.OrderBy(i => i.Nome);
            grvClientes.DataBind();
        }
        else {
            barraClientes.Visible = false;
        }

    }

    protected void prerenderExcluirClientes(object sender, EventArgs e)
    {
        Utilitarios.WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluir?");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            GrupoEconomicoSite clienteSite = GrupoEconomicoSite.ConsultarPorId(((ImageButton)sender).CommandArgument.ToInt32());
            if (clienteSite != null)
            {
                if (clienteSite.Excluir())
                {
                    msg.CriarMensagem("Cliente deletado com sucesso", "");
                    transacao.Recarregar(ref msg);
                    carregarClientes();
                }
                else
                {
                    msg.CriarMensagem("Erro ao excluir", "");
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (tbxNome.Text != null && tbxNome.Text != "" && imgLogomarca.ImageUrl != "" && imgLogomarca.ImageUrl != null && imgLogomarca.ImageUrl != "~/imagens/FotoIndisponivelAlbum.png")
            {
                Session["idConfig"] = 0;
                transacao.Abrir();
                GrupoEconomicoSite grupoEconomicoSite = new GrupoEconomicoSite();
                grupoEconomicoSite.Nome = tbxNome.Text;
                grupoEconomicoSite.LinkImagem = imgLogomarca.ImageUrl;
                grupoEconomicoSite.Salvar();
                Session["urlImagemCliente"] = "";
                msg.CriarMensagem("Cliente Salvo Com Sucesso.!", "");
                imgLogomarca.ImageUrl = "~/imagens/FotoIndisponivelAlbum.png";
                tbxNome.Text = "";
                carregarClientes();
            }
            else
            {
                msg.CriarMensagem("Faça o upload da imagem.", "");
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }
}

