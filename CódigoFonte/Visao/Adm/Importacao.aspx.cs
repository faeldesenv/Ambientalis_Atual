using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Importacao_Importacao : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = 0;
                transacao.Abrir();
                this.CarregarCampos();
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

    #region _________________ Eventos ______________________

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.ImportarDadosCliente();
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

    protected void dgrOrgaosAmbientais_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            transacao.Abrir();
            dgrOrgaosAmbientais.CurrentPageIndex = e.NewPageIndex;
            this.CarregarOrgaosAmbientais();
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

    protected void dgrTiposLicencas_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            transacao.Abrir();
            dgrTiposLicencas.CurrentPageIndex = e.NewPageIndex;
            this.CarregarTiposLicencas();
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

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    #endregion

    #region _________________ Métodos ______________________

    private void CarregarCampos()
    {
        this.CarregarClientes();
        this.CarregarOrgaosAmbientais();
        this.CarregarTiposLicencas();
    }

    private void CarregarClientes()
    {

        ddlCliente.DataTextField = "Nome";
        ddlCliente.DataValueField = "Id";

        IList<GrupoEconomico> clientes = GrupoEconomico.ConsultarTodosOrdemAlfabetica();

        GrupoEconomico aux = new GrupoEconomico();
        aux.Nome = "-- Selecione --";
        clientes.Insert(0, aux);

        ddlCliente.DataSource = clientes;
        ddlCliente.DataBind();
    }

    private void CarregarOrgaosAmbientais()
    {
        dgrOrgaosAmbientais.DataSource = OrgaoAmbiental.ConsultarOrgaosPadrao();
        dgrOrgaosAmbientais.DataBind();
    }

    private void CarregarTiposLicencas()
    {
        dgrTiposLicencas.DataSource = TipoLicenca.ConsultarTiposPadrao();
        dgrTiposLicencas.DataBind();
    }

    private void ImportarDadosCliente()
    {
        GrupoEconomico cliente = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());
        if (cliente == null)
        {
            msg.CriarMensagem("É necessário selecionar ao menos um cliente", "Informação", MsgIcons.Informacao);
            return;
        }

        IList<OrgaoAmbiental> orgaosSelecionados = this.GetOrgaosAmbientaisSelecionados();
        IList<TipoLicenca> tiposSelecionados = this.GetTiposLicencaSelecionados();

        if (orgaosSelecionados.Count < 1 && tiposSelecionados.Count < 1)
        {
            msg.CriarMensagem("É necessário selecionar ao menos um órgão ambiental ou um tipo de licença para realizar a importação", "Informação", MsgIcons.Informacao);
            return;
        }

        for (int indexOrgao = 0; indexOrgao < orgaosSelecionados.Count; indexOrgao++)
        {
            OrgaoAmbiental orgao = orgaosSelecionados[indexOrgao].CloneObject<OrgaoAmbiental>();
            orgao.Id = 0;
            orgao.Emp = cliente.Id;
            orgao = orgao.Salvar();
        }

        for (int indexTipoLicenca = 0; indexTipoLicenca < tiposSelecionados.Count; indexTipoLicenca++)
        {
            TipoLicenca tipo = tiposSelecionados[indexTipoLicenca].CloneObject<TipoLicenca>();
            tipo.Id = 0;
            tipo.Emp = cliente.Id;
            tipo = tipo.Salvar();
        }

        msg.CriarMensagem("Importação realizada com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.CarregarCampos();
    }

    private IList<OrgaoAmbiental> GetOrgaosAmbientaisSelecionados()
    {
        IList<OrgaoAmbiental> retorno = new List<OrgaoAmbiental>();
        foreach (DataGridItem item in dgrOrgaosAmbientais.Items)
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                OrgaoAmbiental o = OrgaoAmbiental.ConsultarPorId(dgrOrgaosAmbientais.DataKeys[item.ItemIndex].ToString().ToInt32());
                retorno.Add(o);
            }

        return retorno;
    }

    private IList<TipoLicenca> GetTiposLicencaSelecionados()
    {
        IList<TipoLicenca> retorno = new List<TipoLicenca>();
        foreach (DataGridItem item in dgrTiposLicencas.Items)
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
            {
                TipoLicenca o = TipoLicenca.ConsultarPorId(dgrTiposLicencas.DataKeys[item.ItemIndex].ToString().ToInt32());
                retorno.Add(o);
            }

        return retorno;
    }

    #endregion

    #region _________________ DataBinds ______________________

    public string bindTipo(Object o)
    {
        OrgaoAmbiental orgao = (OrgaoAmbiental)o;
        return orgao.GetType().Name.Replace("Orgao", "").Trim();
    }

    public string bindCidadeEstado(Object o)
    {
        OrgaoAmbiental orgao = (OrgaoAmbiental)o;
        if (orgao != null)
        {
            if (orgao.GetType() == typeof(OrgaoFederal))
                return "-";

            if (orgao.GetType() == typeof(OrgaoEstadual))
                return ((OrgaoEstadual)orgao).Estado.PegarSiglaEstado();

            if (orgao.GetType() == typeof(OrgaoMunicipal))
                return ((OrgaoMunicipal)orgao).Cidade.Nome + " - " + ((OrgaoMunicipal)orgao).Cidade.Estado.PegarSiglaEstado();
        } return "";
    }

    #endregion

}