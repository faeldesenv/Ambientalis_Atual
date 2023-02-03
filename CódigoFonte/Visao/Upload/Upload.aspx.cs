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
                if (tipo.IsNotNullOrEmpty() && id.ToInt32() > 0)
                {
                    this.CarregarGrid(tipo, id);
                    fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = false;

                    if (tipo == "RAL")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "Diverso")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "GuiaUtilizacao")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "ProcessoDNPM")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "ConcessaoLavra")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "RequerimentoLavra")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "Licenciamento")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "Extracao")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "AlvaraPesquisa")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "RequerimentoPesquisa")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "ContratoDiverso" || tipo == "AditivoContrato")
                        fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                }
                else
                {
                    fluUpload.Enabled = tbxDescricao.Enabled = btnUpload.Enabled = true;

                    if (tipo == "Exigencia")
                    {
                        dgrArquivos.DataSource = this.ArquivosUploadExigencias;
                    }
                    else
                    {
                        dgrArquivos.DataSource = this.ArquivosUpload;
                    }
                    dgrArquivos.DataBind();
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
            if (msg.Mensagem.IsNotNullOrEmpty())
                Alert.Show(msg.Mensagem);
        }
    }

    private void CarregarGrid(string tipo, string id)
    {
        IList<ArquivoFisico> arquivosAux = new List<ArquivoFisico>();
        switch (tipo)
        {
            case "Exigencia":
                Exigencia exigencia = Exigencia.ConsultarPorId(id.ToInt32());
                arquivosAux = exigencia.Arquivos;
                break;
            case "ProcessoDNPM":
                ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(id.ToInt32());
                arquivosAux = processo.Arquivos;
                break;
            case "RequerimentoPesquisa":
                RequerimentoPesquisa requerimentoP = RequerimentoPesquisa.ConsultarPorId(id.ToInt32());
                arquivosAux = requerimentoP.Arquivos;
                break;
            case "AlvaraPesquisa":
                AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(id.ToInt32());
                arquivosAux = alvara.Arquivos;
                break;
            case "Extracao":
                Extracao extracao = Extracao.ConsultarPorId(id.ToInt32());
                arquivosAux = extracao.Arquivos;
                break;
            case "Licenciamento":
                Licenciamento licenciamento = Licenciamento.ConsultarPorId(id.ToInt32());
                arquivosAux = licenciamento.Arquivos;
                break;
            case "RequerimentoLavra":
                RequerimentoLavra requerimemtoL = RequerimentoLavra.ConsultarPorId(id.ToInt32());
                arquivosAux = requerimemtoL.Arquivos;
                break;
            case "ConcessaoLavra":
                ConcessaoLavra concessaoL = ConcessaoLavra.ConsultarPorId(id.ToInt32());
                arquivosAux = concessaoL.Arquivos;
                break;
            case "GuiaUtilizacao":
                GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(id.ToInt32());
                arquivosAux = guia.Arquivos;
                break;
            case "Licenca":
                Licenca licenca = Licenca.ConsultarPorId(id.ToInt32());
                arquivosAux = licenca.Arquivos;
                break;
            case "OutrosEmpresa":
                OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(id.ToInt32());
                arquivosAux = outros.Arquivos;
                break;
            case "OutrosProcesso":
                OutrosProcesso outrosP = OutrosProcesso.ConsultarPorId(id.ToInt32());
                break;
            case "CTF":
                CadastroTecnicoFederal ctf = CadastroTecnicoFederal.ConsultarPorId(id.ToInt32());
                arquivosAux = ctf.Arquivos;
                break;
            case "Condicionante":
                Condicionante condicionante = Condicionante.ConsultarPorId(id.ToInt32());
                arquivosAux = condicionante.Arquivos;
                break;
            case "RAL":
                RAL RAL = RAL.ConsultarPorId(id.ToInt32());
                arquivosAux = RAL.Arquivos;
                break;
            case "Diverso":
                Diverso diverso = Diverso.ConsultarPorId(id.ToInt32());
                arquivosAux = diverso.Arquivos;
                break;
            case "ContratoDiverso":
                ContratoDiverso c = ContratoDiverso.ConsultarPorId(id.ToInt32());
                arquivosAux = c.ArquivosFisicos;
                if (c.AditivosContratos != null)
                    foreach (AditivoContrato adt in c.AditivosContratos)
                    {
                        arquivosAux.AddRange<ArquivoFisico>(adt.ArquivosFisicos);
                    }
                break;
            case "AditivoContrato":
                AditivoContrato aditivo = AditivoContrato.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("idAditivo", this.Request).ToInt32());
                arquivosAux = aditivo.ArquivosFisicos;
                break;

        }

        dgrArquivos.DataSource = arquivosAux;
        dgrArquivos.DataBind();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

        try
        {
            if (fluUpload.HasFile)
            {
                transacao.Abrir();

                string id = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request) != null ? Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToString() : "";
                string idCliente = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idCliente", this.Request).ToString();
                string idEmpresa = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idEmpresa", this.Request).ToString();
                string tipo = Utilitarios.Criptografia.Seguranca.RecuperarParametro("tipo", this.Request).ToString();

                string path = "";

                if (tipo == "Diverso" || tipo == "ContratoDiverso" || tipo == "AditivoContrato")
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Repositorio/" + idCliente + "/" + idEmpresa + "/" + tipo + "/" + id;
                }
                else
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Repositorio/" + idCliente + "/" + idEmpresa + "/" + tipo;
                }

                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                string caminhoCompleto = path + "/" + fluUpload.FileName;
                string novoCaminho = caminhoCompleto;
                FileInfo f;
                int cont = 0;

                while (true)
                {
                    f = new FileInfo(novoCaminho);
                    if (!f.Exists)
                        break;
                    else
                    {
                        novoCaminho = caminhoCompleto.Replace(f.Extension, "") + "_" + cont.ToString() + f.Extension;
                        cont++;
                    }
                }

                ArquivoFisico arq = new ArquivoFisico();
                if (tipo == "Diverso" || tipo == "ContratoDiverso" || tipo == "AditivoContrato")
                    arq.Caminho = "Sistema/Repositorio/" + idCliente + "/" + idEmpresa + "/" + tipo + "/" + id;
                else
                    arq.Caminho = "Sistema/Repositorio/" + idCliente + "/" + idEmpresa + "/" + tipo;
                arq.DataPublicacao = DateTime.Now;
                arq.Descricao = tbxDescricao.Text;
                arq.Extensao = f.Extension;
                arq.Identificador = f.Name;
                arq.Host = Request.Url.Authority;
                arq = arq.Salvar();

                if (tipo == "Diverso")
                {
                    Diverso d = Diverso.ConsultarPorId(id.ToInt32());
                    if (d.Arquivos == null)
                        d.Arquivos = new List<ArquivoFisico>();
                    d.Arquivos.Add(arq);
                    d.Salvar();
                    dgrArquivos.DataSource = d.Arquivos;
                }
                else if (tipo == "ContratoDiverso")
                {
                    ContratoDiverso d = ContratoDiverso.ConsultarPorId(id.ToInt32());
                    if (d.ArquivosFisicos == null)
                        d.ArquivosFisicos = new List<ArquivoFisico>();
                    d.ArquivosFisicos.Add(arq);
                    d.Salvar();
                    dgrArquivos.DataSource = d.ArquivosFisicos;
                }
                else if (tipo == "Exigencia")
                {
                    if (this.ArquivosUploadExigencias == null)
                        this.ArquivosUploadExigencias = new List<ArquivoFisico>();
                    this.ArquivosUploadExigencias.Add(arq);
                    dgrArquivos.DataSource = this.ArquivosUploadExigencias;
                }
                else if (tipo == "RAL")
                {
                    RAL ral = RAL.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());

                    if (ral.Arquivos == null)
                        ral.Arquivos = new List<ArquivoFisico>();
                    ral.Arquivos.Add(arq);
                    ral.Salvar();
                    dgrArquivos.DataSource = ral.Arquivos;
                }
                else if (tipo == "AditivoContrato")
                {
                    AditivoContrato aditivo = AditivoContrato.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("idAditivo", this.Request).ToInt32());
                    if (aditivo != null)
                    {
                        aditivo.ArquivosFisicos.Add(arq);
                        aditivo.Salvar();
                        dgrArquivos.DataSource = aditivo.ArquivosFisicos;
                    }
                }
                else if (tipo == "ContratoDiverso")
                {
                    ContratoDiverso contrato = ContratoDiverso.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (contrato != null)
                    {
                        contrato.ArquivosFisicos.Add(arq);
                        contrato.Salvar();
                        dgrArquivos.DataSource = contrato.ArquivosFisicos;
                    }
                }
                else if (tipo == "ProcessoDNPM")
                {
                    ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (processo != null)
                    {
                        arq.ProcessoDNPM = processo;
                        arq = arq.Salvar();
                        processo.Arquivos.Add(arq);
                        processo.Salvar();
                        dgrArquivos.DataSource = processo.Arquivos;
                    }
                }
                else if (tipo == "ConcessaoLavra")
                {
                    ConcessaoLavra concessaoLavra = ConcessaoLavra.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (concessaoLavra != null)
                    {
                        arq.Regime = concessaoLavra;
                        arq = arq.Salvar();
                        concessaoLavra.Arquivos.Add(arq);
                        concessaoLavra.Salvar();
                        dgrArquivos.DataSource = concessaoLavra.Arquivos;
                    }
                }
                else if (tipo == "RequerimentoLavra")
                {
                    RequerimentoLavra requerimentoLavra = RequerimentoLavra.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (requerimentoLavra != null)
                    {
                        arq.Regime = requerimentoLavra;
                        arq = arq.Salvar();
                        requerimentoLavra.Arquivos.Add(arq);
                        requerimentoLavra.Salvar();
                        dgrArquivos.DataSource = requerimentoLavra.Arquivos;
                    }
                }
                else if (tipo == "Licenciamento")
                {
                    Licenciamento licenciamento = Licenciamento.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (licenciamento != null)
                    {
                        arq.Regime = licenciamento;
                        arq = arq.Salvar();
                        licenciamento.Arquivos.Add(arq);
                        licenciamento.Salvar();
                        dgrArquivos.DataSource = licenciamento.Arquivos;
                    }
                }
                else if (tipo == "Extracao")
                {
                    Extracao extracao = Extracao.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (extracao != null)
                    {
                        arq.Regime = extracao;
                        arq = arq.Salvar();
                        extracao.Arquivos.Add(arq);
                        extracao.Salvar();
                        dgrArquivos.DataSource = extracao.Arquivos;
                    }
                }
                else if (tipo == "AlvaraPesquisa")
                {
                    AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (alvara != null)
                    {
                        arq.Regime = alvara;
                        arq = arq.Salvar();
                        alvara.Arquivos.Add(arq);
                        alvara.Salvar();
                        dgrArquivos.DataSource = alvara.Arquivos;
                    }
                }
                else if (tipo == "RequerimentoPesquisa")
                {
                    RequerimentoPesquisa requerimentoPesquisa = RequerimentoPesquisa.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (requerimentoPesquisa != null)
                    {
                        arq.Regime = requerimentoPesquisa;
                        arq = arq.Salvar();
                        requerimentoPesquisa.Arquivos.Add(arq);
                        requerimentoPesquisa.Salvar();
                        dgrArquivos.DataSource = requerimentoPesquisa.Arquivos;
                    }
                }
                else if (tipo == "GuiaUtilizacao")
                {
                    GuiaUtilizacao guiaUtilizacao = GuiaUtilizacao.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32());
                    if (guiaUtilizacao != null)
                    {
                        arq.GuiaUtilizacao = guiaUtilizacao;
                        arq = arq.Salvar();
                        guiaUtilizacao.Arquivos.Add(arq);
                        guiaUtilizacao.Salvar();
                        dgrArquivos.DataSource = guiaUtilizacao.Arquivos;
                    }
                }
                else if (tipo != "Diverso" && tipo != "Exigencia" && tipo != "RAL")
                {
                    if (this.ArquivosUpload == null)
                        this.ArquivosUpload = new List<ArquivoFisico>();
                    this.ArquivosUpload.Add(arq);
                    dgrArquivos.DataSource = this.ArquivosUpload;
                }

                fluUpload.SaveAs(novoCaminho);
                dgrArquivos.DataBind();
                tbxDescricao.Text = "";
            }
            else
            {
                msg.CriarMensagem("Nenhum arquivo selecionado.", "Alerta", MsgIcons.Alerta);
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

    public string BindUrlArquivo(Object o)
    {
        return ((ArquivoFisico)o).CaminhoVirtual;
    }

    protected void ibtnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            transacao.Abrir();
            string id = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request) != null ? Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToString() : "";
            string tipo = Utilitarios.Criptografia.Seguranca.RecuperarParametro("tipo", this.Request).ToString();
            foreach (DataGridItem item in dgrArquivos.Items)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")) != null && ((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    ArquivoFisico arq = ArquivoFisico.ConsultarPorId(item.Cells[0].Text.ToInt32());
                    arq.Excluir();

                    if (tipo == "Diverso" && id.ToInt32() > 0)
                    {
                        Diverso d = Diverso.ConsultarPorId(id.ToInt32());
                        if (d.Arquivos != null && d.Arquivos.Count > 0)
                        {
                            d.Arquivos.Remove(arq);
                        }
                        d.Salvar();
                    }
                    else if (tipo == "Exigencia")
                        this.ArquivosUploadExigencias.Remove(arq);

                    else if (tipo != "Diverso" && tipo != "AditivoContrato" && tipo != "ContratoDiverso")
                        this.ArquivosUpload.Remove(arq);
                }
            }

            transacao.Recarregar(ref msg);

            if (tipo == "Diverso")
            {
                this.ArquivosUpload = new List<ArquivoFisico>();
                this.ArquivosUpload = Diverso.ConsultarPorId(id.ToInt32()).Arquivos != null ? Diverso.ConsultarPorId(id.ToInt32()).Arquivos : new List<ArquivoFisico>();
            }

            if (tipo == "ContratoDiverso" || tipo == "AditivoContrato")
            {
                if (tipo == "ContratoDiverso")
                {
                    dgrArquivos.DataSource = ContratoDiverso.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request).ToInt32()).ArquivosFisicos;
                    dgrArquivos.DataBind();
                }

                if (tipo == "AditivoContrato")
                {
                    dgrArquivos.DataSource = AditivoContrato.ConsultarPorId(Utilitarios.Criptografia.Seguranca.RecuperarParametro("idAditivo", this.Request).ToInt32()).ArquivosFisicos;
                    dgrArquivos.DataBind();
                }

            }
            else
            {
                dgrArquivos.DataSource = this.ArquivosUpload;
                dgrArquivos.DataBind();
            }

            msg.CriarMensagem("Arquivo(s) excluído(s) com sucesso", "Sucesso", MsgIcons.Sucesso);


        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
        }
    }
}