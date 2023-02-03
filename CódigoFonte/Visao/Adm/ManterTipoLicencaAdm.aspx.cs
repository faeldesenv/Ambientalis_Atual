using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;

public partial class Licenca_ManterTipoLicencaAdm : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            if (!IsPostBack)
            {
                transacao.Abrir();                
                hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                if (hfId.Value.ToInt32() > 0)
                    this.CarregarTipoLicenca(hfId.Value.ToInt32());
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

    #region _______Metodos_________

    public void CarregarTipoLicenca(int id)
    {
        TipoLicenca tl = TipoLicenca.ConsultarPorId(id);
        if (tl != null)
        {
            this.hfId.Value = tl.Id.ToString();
            this.tbxNome.Text = tl.Nome;
            this.tbxSigla.Text = tl.Sigla;
            this.tbxValidadePadrao.Text = tl.DiasValidadePadrao.ToString();
            this.CarregarCondicionantesPadroes(tl);
        }
    }

    private void CarregarCondicionantesPadroes(TipoLicenca tl)
    {
        dgrCondicionante.DataSource = tl.CondicionantesPadroes;
        dgrCondicionante.DataBind();
    }

    public void Salvar()
    {
        TipoLicenca tl = TipoLicenca.ConsultarPorId(hfId.Value.ToInt32());
        if (tl == null)
            tl = new TipoLicenca();
        tl.Nome = tbxNome.Text;
        tl.DiasValidadePadrao = tbxValidadePadrao.Text.ToInt32();
        tl.Sigla = tbxSigla.Text;
        tl.Emp = 0;
        tl = tl.Salvar();
        hfId.Value = tl.Id.ToString();
        ibtnAdicionarCondicionante.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAdicionarCondicionante.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/x.png";
        msg.CriarMensagem("Tipo de Licença salva com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    public void Excluir()
    {
        TipoLicenca tipo = TipoLicenca.ConsultarPorId(hfId.Value.ToInt32());
        if (tipo != null)
        {
            if (tipo.Licencas != null && tipo.Licencas.Count > 0)
            {
                msg.CriarMensagem("Este Tipo Licença não pode ser excluída!", "Alerta", MsgIcons.Alerta);
                return;
            }
            tipo.Excluir();
            msg.CriarMensagem("Tipo Licença excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.Novo();
        }
    }

    private void Novo()
    {
        Response.Redirect("../Adm/ManterTipoLicencaAdm.aspx", false);
    }

    private void NovaCondicionante()
    {
        tbxDescricao.Text = tbxValidadePadraoCondicionante.Text = "";
        hfIdCondicionante.Value = "0";
        chkCondicionantePeriódica.Checked = false;
    }

    private void SalvarCondicionante()
    {
        CondicionantePadrao c = CondicionantePadrao.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
        if (c == null)
            c = new CondicionantePadrao();
        c.Descricao = tbxDescricao.Text;
        c.DiasValidade = tbxValidadePadraoCondicionante.Text.ToInt32();
        c.TipoLicenca = TipoLicenca.ConsultarPorId(hfId.Value.ToInt32());
        c.Periodico = chkCondicionantePeriódica.Checked;
        c = c.Salvar();
        hfIdCondicionante.Value = c.Id.ToString();
        msg.CriarMensagem("Condicionante Padrão Salva com Sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void ExcluirCondicionante()
    {
        CondicionantePadrao c = CondicionantePadrao.ConsultarPorId(hfIdCondicionante.Value.ToInt32());
        if (c != null)
        {
            c.Excluir();
            msg.CriarMensagem("Condicionante Padrão excluida.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    #endregion

    #region ________Eventos________

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Salvar();
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

    protected void dgrCondicionante_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            transacao.Abrir();
            dgrCondicionante.CurrentPageIndex = e.NewPageIndex;
            this.CarregarCondicionantesPadroes(TipoLicenca.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void dgrCondicionante_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            transacao.Abrir();
            foreach (DataGridItem item in dgrCondicionante.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    CondicionantePadrao c = CondicionantePadrao.ConsultarPorId(dgrCondicionante.DataKeys[item.ItemIndex].ToString().ToInt32());
                    c.Excluir();
                }
            transacao.Fechar(ref msg);
            transacao.Abrir();
            this.CarregarCondicionantesPadroes(TipoLicenca.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void dgrCondicionante_EditCommand(object source, DataGridCommandEventArgs e)
    {
        transacao.Abrir();
        CondicionantePadrao c = CondicionantePadrao.ConsultarPorId(dgrCondicionante.DataKeys[e.Item.ItemIndex].ToString().ToInt32());
        tbxDescricao.Text = c.Descricao;
        tbxValidadePadraoCondicionante.Text = c.DiasValidade.ToString();
        hfIdCondicionante.Value = c.Id.ToString();
        chkCondicionantePeriódica.Checked = c.Periodico;
        escolherCliente_ModalPopupExtender.Show();
        transacao.Fechar(ref msg);
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes as condicionantes selecionados serão perdido(s). Deseja excluir assim mesmo?");
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        this.Novo();
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        
        try
        {
            transacao.Abrir();
            this.Excluir();
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

    protected void ibtnAdicionarCondicionante_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, true);
        ibtnAdicionarCondicionante.Enabled = hfId.Value.ToInt32() > 0;
        ibtnAdicionarCondicionante.ImageUrl = hfId.Value.ToInt32() > 0 ? "~/imagens/icone_adicionar.png" : "~/imagens/icone_adicionar_semcor.png";
    }

    protected void btnSalvarCondicionanteP_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.SalvarCondicionante();
            transacao.Fechar(ref msg);
            transacao.Abrir();
            this.CarregarCondicionantesPadroes(TipoLicenca.ConsultarPorId(hfId.Value.ToInt32()));
            escolherCliente_ModalPopupExtender.Hide();
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

    protected void btnNovoCondicionanteP_Click(object sender, EventArgs e)
    {
        this.NovaCondicionante();
    }

    protected void btnExcluirCondicionanteP_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.ExcluirCondicionante();
            transacao.Fechar(ref msg);
            transacao.Abrir();
            this.NovaCondicionante();
            this.CarregarCondicionantesPadroes(TipoLicenca.ConsultarPorId(hfId.Value.ToInt32()));
            escolherCliente_ModalPopupExtender.Hide();
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

    protected void dgrCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "EditCommand", upCondicionantes);
    }

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        Button ibtn = (Button)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes ao tipo de linceça selecionado serão perdido(s). Deseja excluir assim mesmo?");
    }

    protected void btnSalvarCondicionanteP_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTabela);
    }

    protected void btnExcluirCondicionanteP_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTabela);
    }

    protected void btnExcluirCondicionanteP_PreRender(object sender, EventArgs e)
    {
        Button ibtn = (Button)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes as condicionantes selecionados serão perdido(s). Deseja excluir assim mesmo?");
    }

    protected void ibtnAdicionarCondicionante_Click(object sender, ImageClickEventArgs e)
    {
        this.NovaCondicionante();
        escolherCliente_ModalPopupExtender.Show();
    }

    #endregion

    protected void PermissaoUsuario_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((Button)sender, true);
    }
    
    protected void btnSalvar_Click2(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Salvar();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.Salvar();
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
    protected void ibtnAdicionarCondicionante_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCondicionantes);
    }

    public string bindPeriodica(Object o)
    {
        CondicionantePadrao c = (CondicionantePadrao)o;
        return c.Periodico != null && c.Periodico ? "Sim" : "Não";
    }
}