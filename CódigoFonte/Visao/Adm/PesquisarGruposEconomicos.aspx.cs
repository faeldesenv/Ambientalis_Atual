using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Persistencia.Filtros;
using Utilitarios.Criptografia;

public partial class Cliente_PesquisarClientes : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();

                this.IniciarCampos();
                this.Pesquisar();
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

    protected void btnAtivarLogus_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAtivacao);
    }

    #region ___________Metodos____________

    public void Pesquisar()
    {
        IList<GrupoEconomico> teste = GrupoEconomico.ConsultarTodos();
        IList<GrupoEconomico> clientes = GrupoEconomico.Filtrar(tbxNome.Text, tbxResponsavel.Text, ddlStatus.SelectedValue.ToInt32(), tbxCnpjCpf.Text,
            Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()), ddlPendenciasAtivacao.SelectedValue.ToInt32(), ddlCancelado.SelectedValue.ToInt32(), Estado.ConsultarPorId(ddlEstados.SelectedValue.ToInt32()));
        dgrClientes.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgrClientes.DataSource = clientes;
        dgrClientes.DataBind();
        lblQuantidade.Text = clientes.Count() + " Grupo(s) Econômico(s) encontrado(s)";
    }

    public void CarregarEstados()
    {
        ddlEstados.DataValueField = "Id";
        ddlEstados.DataTextField = "Nome";
        ddlEstados.DataSource = Estado.ConsultarTodos();
        ddlEstados.DataBind();
        ddlEstados.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public void CarregarCidades(int p)
    {
        if (p <= 0)
        {
            ddlCidades.Items.Clear();
            return;
        }

        Estado estado = Estado.ConsultarPorId(p);
        ddlCidades.DataValueField = "Id";
        ddlCidades.DataTextField = "Nome";
        ddlCidades.DataSource = estado.Cidades;
        ddlCidades.DataBind();
        ddlCidades.Items.Insert(0, new ListItem("-- Todas as cidades --", "0"));
    }

    private void IniciarCampos()
    {
        this.CarregarPendenciasAtivacao();
        this.CarregarEstados();
    }

    private void CarregarPendenciasAtivacao()
    {
        ddlPendenciasAtivacao.Items.Clear();
        ddlPendenciasAtivacao.Items.Add(new ListItem("-- Todos --", "0"));
        ddlPendenciasAtivacao.Items.Add(new ListItem("Logus", "1"));
        ddlPendenciasAtivacao.Items.Add(new ListItem("Ambientalis", "2"));
        ddlPendenciasAtivacao.SelectedIndex = 0;
    }

    private void AtivarGrupo(int idGrupoEconomico)
    {
        Administrador adm = Administrador.ConsultarPorId(hfIdAdministrador.Value.ToInt32());
        if (adm == null)
        {
            msg.CriarMensagem("Este administrador ainda não definido no sistema", "Informação", MsgIcons.Informacao);
            return;
        }

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(idGrupoEconomico);

        if (grupo == null)
        {
            msg.CriarMensagem("Grupo econômico não definido, por favor tente novamente", "Informação", MsgIcons.Informacao);
            return;
        }

        if (adm.SenhaAtivacao.Trim() != Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenhaAtivacao.Text.Trim(), true))
        {
            msg.CriarMensagem("Senha de ativação informada está incorreta", "Informação", MsgIcons.Informacao);
            return;
        }

        if (adm.Id == Administrador.idLogus)
            grupo.AtivoLogus = true;
        else grupo.AtivoAmbientalis = true;
        grupo.Salvar();

        msg.CriarMensagem("Ativação realizada com sucesso!", "Sucesso", MsgIcons.Sucesso);
        this.Pesquisar();


        Venda.InserirComissoes(grupo, null, this.IdConfig.ToInt32());
    }

    private void SetDescricaoAtivacao(int idGrupo, Button btn)
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(idGrupo);

        if (btn.Text.ToUpper() == "LOGUS")
        {
            btn.Visible = idGrupo > 0 && (grupo != null && !grupo.AtivoLogus);
        }
        else
        {
            btn.Visible = idGrupo > 0 && (grupo != null && !grupo.AtivoAmbientalis);
        }
    }

    #endregion

    #region ___________Bindings____________

    public String bindingUrl(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        return "../Adm/CadastroGrupoEconomico.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + c.Id);
    }

    public String bindingTitulo(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        return c.Nome;
    }

    public String bindingCnpjCpf(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;

        return c.GetNumeroCNPJeCPFComMascara;
    }

    public String bindingRazaoSocial(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;

        if (c.DadosPessoa != null)
        {
            if (c.DadosPessoa.GetType() == typeof(DadosJuridica))
            {
                return ((DadosJuridica)c.DadosPessoa).RazaoSocial;
            }
        } return "";
    }

    public String bindingCidade(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        if (c.Endereco != null && c.Endereco.Cidade != null)
        {
            return c.Endereco.Cidade.Nome + " - " + c.Endereco.Cidade.Estado.PegarSiglaEstado();
        } return "";
    }

    public String bindingEstado(Object o)
    {
        GrupoEconomico c = (GrupoEconomico)o;
        if (c.Endereco != null && c.Endereco.Cidade != null)
        {
            return c.Endereco.Cidade.Estado.Nome;
        } return "";
    }

    #endregion

    #region __________Eventos___________


    protected void btnAdministradorLogus_Click(object sender, EventArgs e)
    {
        hfAdministradorSelecionado.Value = Administrador.idLogus.ToString();
        this.modal_ativacao.Show();
    }

    protected void btnAdministradorAmbientalis_Click(object sender, EventArgs e)
    {
        hfAdministradorSelecionado.Value = Administrador.idAmbientalis.ToString();
        this.modal_ativacao.Show();
    }

    protected void ibtnLiberar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.liberar_modal.Show();
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

    protected void ibtnLiberar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEscolhaAdm);
    }

    protected void btnAtivarLogus_PreRender(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.SetDescricaoAtivacao(((Button)sender).CommandArgument.ToInt32(), ((Button)sender));
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

    protected void btnAtivarLogus_DataBinding1(object sender, EventArgs e)
    {

    }

    protected void btnAtivarLogus_Click(object sender, EventArgs e)
    {
        try
        {
            this.hfIdAdministrador.Value = Administrador.idLogus.ToString();
            this.hfIGrupo.Value = ((Button)sender).CommandArgument;
            this.modal_ativacao.Show();
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

    protected void btnAtivarAmbientalis_Click(object sender, EventArgs e)
    {
        try
        {
            this.hfIdAdministrador.Value = Administrador.idAmbientalis.ToString();
            this.hfIGrupo.Value = ((Button)sender).CommandArgument;
            this.modal_ativacao.Show();
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

    protected void btnAtivar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            if (hfAdministradorSelecionado.Value != "0")
            {
                this.AtivarGruposEmMassa();
            }
            else
            {
                this.AtivarGrupo(this.hfIGrupo.Value.ToInt32());
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

    private void AtivarGruposEmMassa()
    {
        IList<GrupoEconomico> GruposParaAtivacao = new List<GrupoEconomico>();

        foreach (DataGridItem item in dgrClientes.Items)
        {
            if (((CheckBox)item.FindControl("ckbLiberar")).Checked)
            {
                GrupoEconomico cli = GrupoEconomico.ConsultarPorId(dgrClientes.DataKeys[item.ItemIndex].ToString().ToInt32());
                GruposParaAtivacao.Add(cli);
            }
        }

        Administrador adm = Administrador.ConsultarPorId(hfAdministradorSelecionado.Value.ToInt32());
        if (adm == null)
        {
            msg.CriarMensagem("Este administrador ainda não definido no sistema", "Informação", MsgIcons.Informacao);
            return;
        }
        foreach (GrupoEconomico grupo in GruposParaAtivacao)
        {
            if (grupo == null)
            {
                msg.CriarMensagem("Grupo econômico não definido, por favor tente novamente", "Informação", MsgIcons.Informacao);
                return;
            }

            if (adm.SenhaAtivacao.Trim() != Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenhaAtivacao.Text.Trim(), true))
            {
                msg.CriarMensagem("Senha de ativação informada está incorreta", "Informação", MsgIcons.Informacao);
                return;
            }

            if (adm.Id == Administrador.idLogus)
                grupo.AtivoLogus = true;
            else grupo.AtivoAmbientalis = true;
            grupo.Salvar();

            msg.CriarMensagem("Ativação realizada com sucesso!", "Sucesso", MsgIcons.Sucesso);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>DesmarcarLiberarClientes();</script>", false);
            hfAdministradorSelecionado.Value = "0";


            Venda.InserirComissoes(grupo, null, this.IdConfig.ToInt32());
        }
    }


    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            dgrClientes.CurrentPageIndex = 0;
            this.Pesquisar();


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

    protected void ddlEstados_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.CarregarCidades(Convert.ToInt32(ddlEstados.SelectedValue));
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

    protected void dgrClientes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Response.Redirect("../Adm/CadastroGrupoEconomico.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgrClientes.DataKeys[e.Item.ItemIndex] + "&bd=" + ddlSistema.SelectedValue));
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            dgrClientes.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();

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

    protected void dgrClientes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            //fazer verificação antes da exclusao
            IList<GrupoEconomico> clientes = new List<GrupoEconomico>();

            foreach (DataGridItem item in dgrClientes.Items)
            {
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    GrupoEconomico cli = GrupoEconomico.ConsultarPorId(dgrClientes.DataKeys[item.ItemIndex].ToString().ToInt32());
                    cli.Excluir();
                    msg.CriarMensagem("Grupo econômico excluído com sucesso!", "Sucesso");
                }
            }
            this.Pesquisar();
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

    protected void dgrClientes_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e != null && e.Item.ItemType.ToString() == "Item")
            if (e.Item.DataItem.GetType() == typeof(GrupoEconomico))
            {
                GrupoEconomico g = (GrupoEconomico)e.Item.DataItem;
                if (g.Cancelado)
                    //e.Item.BackColor = System.Drawing.Color.FromName("#f30505");
                    e.Item.BackColor = System.Drawing.Color.DarkSalmon;
            }
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;
        // FachadaSeguranca.Instancia.VerificarPermissao(ref i, EAcoes.xxx);

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes aos Clientes selecionados serão perdido(s). Deseja excluir assim mesmo?");
    }

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            this.Pesquisar();
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

    #endregion


}