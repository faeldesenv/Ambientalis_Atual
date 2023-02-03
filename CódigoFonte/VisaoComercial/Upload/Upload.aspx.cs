using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using System.Configuration;
using System.IO;
using Modelo;

public partial class Upload_Upload : System.Web.UI.Page
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    public IList<ArquivoFisico> ArquivosUpload
    {
        get
        {
            if (Session["ArquivosUpload"] == null)
                return null;
            return (IList<ArquivoFisico>)Session["ArquivosUpload"];
        }
        set
        {
            Session["ArquivosUpload"] = value;
        }
    }
    public IList<ArquivoFisico> ArquivosUploadExigencias
    {
        get
        {
            if (Session["ArquivosUploadExigencias"] == null)
                return null;
            return (IList<ArquivoFisico>)Session["ArquivosUploadExigencias"];
        }
        set
        {
            Session["ArquivosUploadExigencias"] = value;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        if (Session["idConfig"] == null)
            Response.Redirect("../Acesso/Login.aspx");

        if (Session["UsuarioLogado_SistemaAmbiental"] == null)
            Response.Redirect("../Acesso/Login.aspx");
        else
        {
            base.OnLoad(e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                transacao.Abrir();
                string tipo = Utilitarios.Criptografia.Seguranca.RecuperarParametro("tipo", this.Request).ToString();
                string id = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request) != null ? Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToString() : "";
               
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            if (msg.Mensagem.IsNotNullOrEmpty())
                Alert.Show(msg.Mensagem);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

    }

    public string BindUrlArquivo(Object o)
    {
        return ((ArquivoFisico)o).CaminhoVirtual;
    }

    protected void ibtnExcluir_Click(object sender, ImageClickEventArgs e)
    {
     
    }
}