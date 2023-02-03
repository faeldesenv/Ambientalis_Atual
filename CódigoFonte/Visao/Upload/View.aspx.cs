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
                arquivosAux = outrosP.Arquivos;
                break;
            case "CTF":
                CadastroTecnicoFederal ctf = CadastroTecnicoFederal.ConsultarPorId(id.ToInt32());
                arquivosAux = ctf.Arquivos;
                break;
            case "Condicionante":
                Condicionante condicionante = Condicionante.ConsultarPorId(id.ToInt32());
                arquivosAux = condicionante.Arquivos;
                break;
        }

        dgrArquivos.DataSource = arquivosAux;
        dgrArquivos.DataBind();
    }

    public string BindUrlArquivo(Object o)
    {
        return ((ArquivoFisico)o).CaminhoVirtual;
    }

}