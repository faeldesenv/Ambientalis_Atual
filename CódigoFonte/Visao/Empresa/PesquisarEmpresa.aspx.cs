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

    public bool UsuarioEditorModuloGeral
    {
        get
        {
            if (Session["UsuarioEditorModuloGeral"] == null)
                return false;
            else
                return (bool)Session["UsuarioEditorModuloGeral"];
        }
        set { Session["UsuarioEditorModuloGeral"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.UsuarioEditorModuloGeral = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloGeral;

                this.CarregarEstados();
                this.CarregarGruposEconomicos();
                this.Pesquisar();
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

    #region ___________Metodos____________

    public void Pesquisar()
    {
        IList<Empresa> empr = Empresa.Filtrar(tbxNome.Text, tbxResponsavel.Text, ddlStatus.SelectedValue.ToInt32(), tbxCnpjCpf.Text,
            Cidade.ConsultarPorId(ddlCidades.SelectedValue.ToInt32()),
            ddlGrupoEconomico.SelectedValue != "0" ? GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32()) : null, Estado.ConsultarPorId(ddlEstados.SelectedValue.ToInt32()));
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue != "1" ? ddlQuantidaItensGrid.SelectedValue.ToInt32() : int.MaxValue;
        dgr.DataSource = empr;
        dgr.DataBind();

        lblQuantidade.Text = empr.Count() + " Empresa(s) encontrada(s)";
    }

    public void CarregarEstados()
    {
        ddlEstados.DataValueField = "Id";
        ddlEstados.DataTextField = "Nome";
        ddlEstados.DataSource = Estado.ConsultarTodos();
        ddlEstados.DataBind();
        ddlEstados.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public void CarregarGruposEconomicos()
    {
        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = grupos;
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    #endregion

    #region ___________Bindings____________

    public String bindTelefone(Object o)
    {
        Empresa e = (Empresa)o;
        if (e != null && e.Id > 0 && e.Contato != null)
            return e.Contato.ContatoTelefones;
        return "";
    }

    public String bindGrupoEconomico(Object o)
    {
        Empresa e = (Empresa)o;
        return e.GrupoEconomico != null ? e.GrupoEconomico.Nome : "";
    }

    public String bindCpfCnpj(Object o)
    {
        Empresa e = (Empresa)o;

        if (e.DadosPessoa != null)
            if (e.DadosPessoa.GetType() == typeof(DadosJuridica))
                return ((DadosJuridica)e.DadosPessoa).Cnpj;
            else
                return ((DadosFisica)e.DadosPessoa).Cpf;
        return "";
    }

    public String bindTipoPessoa(Object o)
    {
        return ((Empresa)o).DadosPessoa.GetType() == typeof(DadosFisica) ? "Física" : "Jurídica";
    }

    public String bindRazaoSocial(Object o)
    {
        Empresa e = (Empresa)o;

        if (e.DadosPessoa != null)
            if (e.DadosPessoa.GetType() == typeof(DadosJuridica))
                return ((DadosJuridica)e.DadosPessoa).RazaoSocial;
        return "---";
    }

    #endregion

    #region __________Eventos___________

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar();

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

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(Convert.ToInt32(ddlEstados.SelectedValue));
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

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgr.CurrentPageIndex = e.NewPageIndex;
            this.Pesquisar();
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

    protected void dgr_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            msg.CriarMensagem("Empresa excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
            //fazer verificação antes da exclusao.
            int cont = 0;
            foreach (DataGridItem item in dgr.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Empresa f = Empresa.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                    if ((f.Processos != null && f.Processos.Count > 0) || (f.ProcessosDNPM != null && f.ProcessosDNPM.Count > 0))
                        msg.CriarMensagem((cont++) + " Empresa(s) não pode(m) ser excluída(s) pois possui Processos associados!", "Atenção", MsgIcons.Informacao);
                    else
                    {
                        f.Excluir();
                        #region ________________ cancelar venda ________________

                        if (Session["idEmp"] != null)
                        {
                            string emp = Session["idEmp"].ToString().Trim();
                            try
                            {
                                Session["idEmp"] = null;
                                Venda.CancelarVenda(f, this.IdConfig.ToInt32());
                            }
                            catch (Exception)
                            { }
                            finally
                            {
                                Session["idEmp"] = emp;
                            }

                        }
                        else
                        {
                            Venda.CancelarVenda(f, this.IdConfig.ToInt32());
                        }


                        #endregion
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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Response.Redirect("ManterEmpresa.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgr.DataKeys[e.Item.ItemIndex].ToString()), false);
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

    protected void chkSelecionar_CheckedChanged(object sender, EventArgs e)
    {
        bool aux = ((CheckBox)sender).Checked;
        foreach (DataGridItem item in dgr.Items)
            ((CheckBox)item.FindControl("ckbExcluir")).Checked = aux;
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorModuloGeral);
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Empresas serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar();
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

    protected void imgAbrir0_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((ImageButton)sender, this.UsuarioEditorModuloGeral);
    }
}