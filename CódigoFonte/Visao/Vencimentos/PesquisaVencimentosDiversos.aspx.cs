using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Vencimentos_PesquisaVencimentosDiversos : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();    

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloDiversos
    {
        get
        {
            if (Session["ConfiguracaoModuloDiversos"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloDiversos"];
        }
        set { Session["ConfiguracaoModuloDiversos"] = value; }
    }

    public IList<Empresa> EmpresasPermissaoModuloDiversos
    {
        get
        {
            if (Session["EmpresaPermissaoModuloDiversos"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresaPermissaoModuloDiversos"];
        }
        set { Session["EmpresaPermissaoModuloDiversos"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.VerificarPermissoes();                                

                this.CarregarGruposEconomicos();
                this.CarregarTiposDiversos();
                this.Pesquisar(true);
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

    #region _________Métodos___________

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDiversos.Id);
        else
            this.ConfiguracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
            this.EmpresasPermissaoModuloDiversos = Empresa.ObterEmpresasQueOUsuarioPossuiAcessoDoModulo(moduloDiversos, this.UsuarioLogado);
        else
            this.EmpresasPermissaoModuloDiversos = null;

    }

    private void CarregarGruposEconomicos()
    {
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarEmpresas()
    {
        ddlEmpresa.Items.Clear();
        ddlEmpresa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoModuloDiversos != null ? this.EmpresasPermissaoModuloDiversos.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoModuloDiversos != null ? this.EmpresasPermissaoModuloDiversos : new List<Empresa>();                        
        }            
        else 
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            empresas = c.Empresas != null ? c.Empresas : new List<Empresa>();
        } 

        if (empresas != null && empresas.Count > 0)
        {
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void CarregarTiposDiversos()
    {
        ddlTipoDiverso.DataValueField = "Id";
        ddlTipoDiverso.DataTextField = "Nome";
        ddlTipoDiverso.DataSource = TipoDiverso.ConsultarTodosOrdemAlfabetica();
        ddlTipoDiverso.DataBind();
        ddlTipoDiverso.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarStatusDiversos()
    {
        TipoDiverso tipo = TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32());
        ddlStatusDiverso.DataValueField = "Id";
        ddlStatusDiverso.DataTextField = "Nome";
        ddlStatusDiverso.DataSource = tipo != null && tipo.StatusDiversos != null ? tipo.StatusDiversos : new List<StatusDiverso>();
        ddlStatusDiverso.DataBind();
        ddlStatusDiverso.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void Pesquisar(bool atualizarContador)
    {
        dgr.PageSize = ddlQuantidaItensGrid.SelectedValue.ToInt32();

        StatusDiverso status = StatusDiverso.ConsultarPorId(ddlStatusDiverso.SelectedValue.ToInt32());
        VencimentoDiverso vencimento = new VencimentoDiverso();

        IList<Diverso> diversos = new List<Diverso>();

        if (this.ConfiguracaoModuloDiversos != null && this.ConfiguracaoModuloDiversos.Id > 0) 
        {
            //Configuração do tipo geral e o usuario não possui permissão de visualizar nenhum vencimento diverso
            if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL && this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral != null && this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0 && !this.ConfiguracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Contains(this.UsuarioLogado)) 
            {
                this.CarregarGridDiversos(new List<Diverso>(), atualizarContador);
                return;
            }

            //Configuração do tipo por empresa e o usuario não possui permissão de visualizar nenhum vencimento diverso
            if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA && (this.EmpresasPermissaoModuloDiversos == null || this.EmpresasPermissaoModuloDiversos.Count == 0)) 
            {
                this.CarregarGridDiversos(new List<Diverso>(), atualizarContador);
                return;
            }

            if (atualizarContador)
                hfQuantidadeExibicao.Value = Diverso.FiltrarContador(GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32()), Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()), tbxDescricao.Text, TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32()), status, this.EmpresasPermissaoModuloDiversos).ToString();

            //metodo filtrar pesquisa de acordo com as configurações de permissão
            diversos = Diverso.Filtrar(GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32()), Empresa.ConsultarPorId(ddlEmpresa.SelectedValue.ToInt32()), tbxDescricao.Text, TipoDiverso.ConsultarPorId(ddlTipoDiverso.SelectedValue.ToInt32()), status, this.EmpresasPermissaoModuloDiversos, (dgr.PageIndex * ddlQuantidaItensGrid.SelectedValue.ToInt32()),
           ((dgr.PageIndex + 1) * ddlQuantidaItensGrid.SelectedValue.ToInt32()));

            if (diversos != null && diversos.Count > 0)
            {
                if (status != null && status.Id > 0)
                {
                    for (int i = diversos.Count - 1; i > -1; i--)
                    {
                        vencimento = diversos[i].GetUltimoVencimento;

                        if (vencimento != null && vencimento.StatusDiverso.Id != status.Id)
                        {
                            diversos.Remove(diversos[i]);
                        }
                    }
                }
            }

            this.CarregarGridDiversos(diversos, atualizarContador); 
            
        }               
        
    }

    private void CarregarGridDiversos(IList<Diverso> diversos, bool atualizarContador) 
    {
        List<Diverso> aux = new List<Diverso>();
        if (dgr.PageIndex > 0)
            aux.AddRange(new Diverso[dgr.PageIndex * ddlQuantidaItensGrid.SelectedValue.ToInt32()]);
        aux.AddRange(diversos);
        int quantidadeFaltando = hfQuantidadeExibicao.Value.ToInt32() - ((dgr.PageIndex + 1) * ddlQuantidaItensGrid.SelectedValue.ToInt32());
        if (quantidadeFaltando > 0)
            aux.AddRange(new Diverso[quantidadeFaltando]);

        dgr.DataSource = aux;
        dgr.DataBind();

        lblQuantidade.Text = diversos.Count() < 1 ? "Não foi encontrado nenhum vencimento diverso com estes filtros" : aux.Count + " Diverso(s) Encontrado(s)";
        
    }

    private void CarregarStatusDoDiverso(Diverso d)
    {
        if (d != null && d.TipoDiverso != null)
        {
            ddlStatusRenovacao.DataValueField = "Id";
            ddlStatusRenovacao.DataTextField = "Nome";
            ddlStatusRenovacao.DataSource = d.TipoDiverso.StatusDiversos;
            ddlStatusRenovacao.DataBind();
            ddlStatusRenovacao.Items.Insert(0, new ListItem("-- Todos --", "0"));
        }
    }

    private void SalvarRenovacao()
    {
        if (tbxDataValidadeRenovacao.Visible == true && tbxDataValidadeRenovacao.Text.IsNotNullOrEmpty() && tbxDataValidadeRenovacao.Text.ToSqlDateTime() <= SqlDate.MinValue)
        {
            msg.CriarMensagem("A Data informada deve conter uma data válida.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Diverso d = Diverso.ConsultarPorId(hfIdDiverso.Value.ToInt32());
        this.VerificarAlteracaoDeStatusRenovacao(d);
        VencimentoDiverso v = d.GetUltimoVencimento;
        if (v == null)
        {
            msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
            return;
        }
        VencimentoDiverso novo = new VencimentoDiverso();

        if (tbxDataValidadeRenovacao.Text.IsNotNullOrEmpty())
            novo.Data = tbxDataValidadeRenovacao.Text.ToDateTime();
        else
        {
            msg.CriarMensagem("Insira uma data válida para o vencimento.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ckbUtilizarUltimasNotificacoes.Checked)
        {
            this.CopiarNotificacoes(ref v, ref novo);
        }

        novo.Periodico = v.Periodico;
        novo.StatusDiverso = StatusDiverso.ConsultarPorId(ddlStatusRenovacao.SelectedValue.ToInt32());
        novo = (VencimentoDiverso)novo.Salvar();

        //teste
        foreach (Notificacao n in v.Notificacoes)
        {
            n.Enviado = 1;
            n.Salvar();
        }
        //fim teste


        d.VencimentosDiversos.Add(novo);
        d = d.Salvar();

        ModalRenovacaoExtender.Hide();
    }

    private void CopiarNotificacoes(ref VencimentoDiverso v, ref VencimentoDiverso novo)
    {
        if (novo.Notificacoes == null)
            novo.Notificacoes = new List<Notificacao>();

        foreach (Notificacao n in v.Notificacoes)
        {
            Notificacao nn = new Notificacao(ModuloPermissao.ModuloVencimentoDiverso);
            nn.DiasAviso = n.DiasAviso;
            nn.Template = n.Template;
            nn.Emails = n.Emails;
            nn = nn.Salvar();
            novo.Notificacoes.Add(nn);
        }
    }

    private void VerificarAlteracaoDeStatusRenovacao(Diverso diverso)
    {
        string alteracao = "";


        if (diverso != null && diverso.GetUltimoVencimento != null && diverso.GetUltimoVencimento.StatusDiverso.Id.ToString() != ddlStatusRenovacao.SelectedValue)
        {
            alteracao += "(Alteração no Status do vencimento referente a " + diverso.Descricao + ", da Empresa " + diverso.Empresa.Nome + ". De: " + diverso.GetUltimoVencimento.StatusDiverso.Nome + " - Para: " + ddlStatusRenovacao.SelectedItem.Text + ")";
        }
        if (alteracao.Trim() == "")
            return;

        lblAlteracao.Text = alteracao;
        ModalExtenderAlteracaoRenovacao.Show();

        this.CarregarListaEmails(ckbEmpresasAlteracao, this.CarregarEmailsEmpresa(diverso).Split(';'));
        this.CarregarListaEmails(ckbGrupos, this.CarregarEmailsCliente(diverso).Split(';'));
    }

    private void CarregarListaEmails(CheckBoxList cbl, string[] lista)
    {
        cbl.Items.Clear();
        foreach (string s in lista)
            if (s.IsNotNullOrEmpty())
                cbl.Items.Add(new ListItem(s, s));
        if (cbl.Items.Count <= 0)
        {
            cbl.Items.Add(new ListItem("Não há emails cadastrados.", "0"));
            cbl.Items[0].Enabled = cbl.Items[0].Selected = false;
        }
        foreach (ListItem item in cbl.Items)
        {
            item.Selected = true;
        }
    }

    private string CarregarEmailsCliente(Diverso diverso)
    {
        if (diverso != null && diverso.Empresa != null && diverso.Empresa.GrupoEconomico != null)
        {
            return diverso.Empresa.GrupoEconomico.Contato.Email.IsNotNullOrEmpty() ? diverso.Empresa.GrupoEconomico.Contato.Email + ";" : "";
        }
        return "";
    }

    private string CarregarEmailsEmpresa(Diverso diverso)
    {
        if (diverso != null && diverso.Empresa != null)
        {
            return diverso.Empresa.Contato.Email.IsNotNullOrEmpty() ? diverso.Empresa.Contato.Email + ";" : "";
        }
        return "";
    }

    private void SalvarHistoricoAlteracaoStatus()
    {
        Historico h = new Historico();
        h.DataPublicacao = DateTime.Now;
        h.Alteracao = lblAlteracao.Text;
        h.Observacao = tbxHistoricoAlteracao.Text;

        Diverso diverso = Diverso.ConsultarPorId(hfIdDiverso.Value.ToInt32());
        h.Diverso = diverso;

        h = h.Salvar();

        if (chkEnviarEmail.Checked)
        {
            Email email = new Email();
            foreach (ListItem l in ckbGrupos.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());
            foreach (ListItem l in ckbEmpresasAlteracao.Items)
                if (l.Selected && l.Value != "0")
                    email.AdicionarDestinatario(l.Value.ToString());

            email.Assunto = "Alteração de Status de Vencimento - Sistema Sustentar";
            email.Mensagem = email.CriarTemplateParaMudancaDeStatusDeVencimento(h);
            email.EnviarAutenticado(25, false);
        }

        ModalExtenderAlteracaoRenovacao.Hide();
        msg.CriarMensagem("Histórico registrado com Sucesso", "Sucesso");
    }

    #endregion

    #region _________Bindings__________

    public string bindGrupoEmpresa(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso != null && diverso.Empresa != null)
        {
            return diverso.Empresa.GrupoEconomico.Nome + " - " + diverso.Empresa.Nome;
        }
        return "";
    }

    public string bindTipoDiverso(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso != null && diverso.TipoDiverso != null)
        {
            return diverso.TipoDiverso.Nome;
        }
        return "";
    }

    public string bindStatus(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso != null && diverso.GetUltimoVencimento != null && diverso.GetUltimoVencimento.StatusDiverso != null)
        {
            return diverso.GetUltimoVencimento.StatusDiverso.Nome;
        }
        return "";
    }

    public string bindProximoVencimento(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso != null && diverso.GetUltimoVencimento != null)
        {
            return diverso.GetUltimoVencimento.Data.ToString("dd/MM/yyyy");
        }
        return "";
    }

    public string bindProximaNotificacao(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso != null && diverso.GetUltimoVencimento != null && diverso.GetUltimoVencimento.Notificacoes != null && diverso.GetUltimoVencimento.Notificacoes.Count > 0)
        {
            return diverso.GetUltimoVencimento.GetDataProximaNotificacao;
        }
        return "";
    }

    public bool BindEnableRenovacao(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso.GetUltimoVencimento != null)
            return diverso.GetUltimoVencimento.Periodico;
        else
            return false;
    }

    public string BindImagemRenovacao(Object o)
    {
        Diverso diverso = (Diverso)o;
        if (diverso.VencimentosDiversos != null && diverso.VencimentosDiversos.Count > 0)
            if (diverso.VencimentosDiversos[diverso.VencimentosDiversos.Count - 1].Periodico)
                return "~/imagens/calendar.png";
            else
                return "~/imagens/calendar_d.png";
        return "~/imagens/calendar_d.png";
    }

    public string BindToolTipoRenovacao(Object o)
    {
        Diverso diverso = (Diverso)o;

        if (diverso.VencimentosDiversos[diverso.VencimentosDiversos.Count - 1].Periodico)
        {
            return "Clique para renovar.";
        }
        else
        {
            return "Esta vencimento não é Periodico.";
        }
    }

    public string BindEditarDiverso(Object o)
    {
        Diverso diverso = (Diverso)o;
        return "CadastroVencimentosDiversos.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + diverso.Id.ToString());
    }

    public bool BindingVisivel(object diverso)
    {
        if (this.ConfiguracaoModuloDiversos == null)
            return false;

        if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            Diverso vencimento = (Diverso)diverso;

            if (vencimento.Empresa == null)
                return false;

            EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(vencimento.Empresa.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);
            return empresaPermissao != null && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado);
        }


        if (this.ConfiguracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            return this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0 && this.ConfiguracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado);
        }

        return false;
    }

    #endregion

    #region ____Triggers Dinamicas_____

    protected void btnSalvarRenovacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPPesquisa);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPopStatus);
    }

    protected void btnAlterarStatus_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>marcarEmailsGrupo();marcarEmailsEmpresa();</script>", false);
    }

    protected void dgr_Init1(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowEditing", upRenovacao);
    }

    #endregion

    #region __________Eventos__________

    protected void ddlQuantidaItensGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar(false);
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

    protected void ddlGrupoEconomico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresas();
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

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposDiversos();
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

    protected void ddlTipoDiverso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarStatusDiversos();
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

    protected void ibtnRenovar_PreRender(object sender, EventArgs e)
    {       
        
    }

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {       
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este(s) Vencimento(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnEditar_PreRender(object sender, EventArgs e)
    {
       
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar(true);
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
        //try
        //{
        //    foreach (DataGridItem item in dgr.Items)
        //        if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
        //        {
        //            Diverso d = Diverso.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
        //            d.Excluir();
        //            msg.CriarMensagem("Vencimento(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
        //        }

        //    this.Pesquisar();
        //}
        //catch (Exception ex)
        //{
        //    msg.CriarMensagem(ex);
        //}
        //finally
        //{
        //    this.GetMBOX<MBOX>().Show(msg);
        //}
    }

    protected void dgr_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        //try
        //{
        //    tbxDataValidadeRenovacao.Text = "";
        //    Diverso d = Diverso.ConsultarPorId(dgr.DataKeys[e.Item.ItemIndex].ToString().ToInt32());
        //    if (d != null)
        //    {
        //        this.CarregarStatusDoDiverso(d);
        //        ddlStatusRenovacao.SelectedValue = d.GetUltimoVencimento.StatusDiverso.Id.ToString();
        //        hfIdDiverso.Value = d.Id.ToString();
        //        chkEnviarEmail.Checked = true;
        //        ModalRenovacaoExtender.Show();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg.CriarMensagem(ex);
        //}
        //finally
        //{
        //    this.GetMBOX<MBOX>().Show(msg);
        //}
    }

    protected void btnSalvarRenovacao_Click(object sender, EventArgs e)
    {
        try
        {            
                this.SalvarRenovacao();
                msg.CriarMensagem("Renovação efetuada com Sucesso!", "Sucesso");
                transacao.Recarregar(ref msg);
                this.Pesquisar(false);
                hfIdDiverso.Value = "";
            
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

    protected void btnFecharStatus_Click(object sender, EventArgs e)
    {
        ModalExtenderAlteracaoRenovacao.Hide();
    }

    protected void btnAlterarStatus_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarHistoricoAlteracaoStatus();
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

    protected void dgr_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            tbxDataValidadeRenovacao.Text = "";

            Diverso d = Diverso.ConsultarPorId(dgr.DataKeys[e.NewEditIndex].Value.ToString().ToInt32());
            if (d != null)
            {
                this.CarregarStatusDoDiverso(d);
                ddlStatusRenovacao.SelectedValue = d.GetUltimoVencimento.StatusDiverso.Id.ToString();
                hfIdDiverso.Value = d.Id.ToString();
                chkEnviarEmail.Checked = true;
                ModalRenovacaoExtender.Show();
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

    protected void dgr_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Diverso d = Diverso.ConsultarPorId(dgr.DataKeys[e.RowIndex].Value.ToString().ToInt32());
            if (d != null)
                d.Excluir();

            transacao.Recarregar(ref msg);
            this.Pesquisar(true);
            msg.CriarMensagem("Vencimento(s) excluído(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    protected void dgr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dgr.PageIndex = e.NewPageIndex;
            this.Pesquisar(false);
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
    
}