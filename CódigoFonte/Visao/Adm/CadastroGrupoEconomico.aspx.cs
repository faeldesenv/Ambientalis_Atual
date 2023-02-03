using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;

public partial class Site_Index : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.VerificarPermissaoExclusao();
                this.HabilitarCamposAdm(true);
                hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                string bd = Utilitarios.Criptografia.Seguranca.RecuperarParametro("bd", Request);
                if (bd.IsNotNullOrEmpty())
                {
                    ddlSistema.SelectedValue = bd;
                    ddlSistema.Enabled = false;
                }

                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();

                this.CarregarCampos();
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                    this.CarregarGrupoEconomico(Convert.ToInt32("" + hfId.Value));

                if (Session["idConfig"].ToString() == "1")
                {
                    visualizacao_conts.Attributes.CssStyle.Add("display", "none");
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

    private void VerificarPermissaoExclusao()
    {
        btnExcluir.Visible = false;
        if (Request["p"] != null)
            if (Request["p"] == "adm")
            {
                btnExcluir.Visible = true;
                WebUtil.AdicionarConfirmacao(btnExcluir, "Deseja realmente excluir este Grupo?");
            }
    }

    private void HabilitarCamposAdm(bool p)
    {
        tbxResponsavel.Enabled = tbxRepresentanteLegal.Enabled = tbxBairro.Enabled = tbxCelular.Enabled = tbxCEP.Enabled = tbxComplemento.Enabled =
            tbxDataNascimento.Enabled = tbxEmail.Enabled = tbxFax.Enabled = tbxGestorEconomico.Enabled = tbxInscricaoEstadual.Enabled = tbxLogradouro.Enabled =
            tbxNacionalidade.Enabled = tbxNumero.Enabled = tbxObservacoes.Enabled = tbxPontoReferencia.Enabled = tbxRamal.Enabled = tbxRG.Enabled =
            tbxSite.Enabled = tbxTelefone.Enabled = chkIsentoICMS.Enabled = rblEstadoCivil.Enabled = rblSexo.Enabled = ddlEstado.Enabled = ddlCidade.Enabled = !p;

        tbxNome.Enabled = tbxCPF.Enabled = tbxLimiteEmpresas.Enabled = tbxLimiteUsuarioEdicao.Enabled =
            tbxRazaoSocial.Enabled = tbxCNPJ.Enabled = ddlSistema.Enabled = chkCancelado.Enabled = rbtnPessoaFisica.Enabled = rbtnPessoaJuridica.Enabled = p;
    }

    #region ________________ Eventos ___________________

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            dgr.CurrentPageIndex = e.NewPageIndex;
            this.CarregarEmpresas(GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void dgr_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        //fazer verificações de exclusao.
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            GrupoEconomico g = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
            foreach (DataGridItem item in dgr.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Empresa f = Empresa.ConsultarPorId(dgr.DataKeys[item.ItemIndex].ToString().ToInt32());
                    g.Empresas.Remove(f);
                    f.Excluir();
                    msg.CriarMensagem("Empresa(s) excluída(s) com sucesso!", "Sucesso", MsgIcons.Sucesso);
                }
            g = g.Salvar();
            this.CarregarEmpresas(GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void dgr_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Response.Redirect("../Empresa/ManterEmpresa.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + dgr.DataKeys[e.Item.ItemIndex].ToString() + "&idCliente=" + hfId.Value), false);
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
        ImageButton ibtn = (ImageButton)sender;

        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Empresa(s) serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
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
            if (ddlTipoControle.SelectedValue.ToInt32() == 0)
            {
                msg.CriarMensagem("Selecione um tipo de controle para poder prosseguir", "Alerta", MsgIcons.Alerta);
                return;
            }
            else
            {
                if (ddlTipoControle.SelectedValue.ToInt32() == 1 && !tbxLimiteEmpresas.Text.IsNotNullOrEmpty())
                {
                    msg.CriarMensagem("Informe o limite de empresas do grupo econômico", "Alerta", MsgIcons.Alerta);
                    return;
                }

                if (ddlTipoControle.SelectedValue.ToInt32() == 2 && !tbxLimiteProcessos.Text.IsNotNullOrEmpty())
                {
                    msg.CriarMensagem("Informe o limite de processos do grupo econômico", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }

            Session["idConfig"] = ddlSistema.SelectedValue;
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        this.Novo();
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
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


    protected void btnAtivarLogus_PreRender(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();
            this.SetDescricaoAtivacao();
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

    protected void btnAtivarLogus_Click(object sender, EventArgs e)
    {
        try
        {
            this.hfIdAdministrador.Value = Administrador.idLogus.ToString();
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
            this.AtivarGrupo();
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

    #region ________________ Métodos ________________

    private void CarregarCampos()
    {
        this.CarregarEstados();
        this.lblAtivadoLogus.Text = "Não";
        this.lblAtivoAmbientalis.Text = "Não";
    }

    private void CarregarGrupoEconomico(int id)
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(id);
        tbxNome.Text = grupo.Nome;
        tbxResponsavel.Text = grupo.Responsavel;
        tbxRepresentanteLegal.Text = grupo.RepresentanteLegal;
        tbxGestorEconomico.Text = grupo.GestorEconomico;
        tbxSite.Text = grupo.Site;
        tbxEmail.Text = grupo.Contato.Email;
        tbxTelefone.Text = grupo.Contato.Telefone;
        tbxCelular.Text = grupo.Contato.Celular;
        tbxRamal.Text = grupo.Contato.Ramal.ToString();
        tbxFax.Text = grupo.Contato.Fax;
        tbxNacionalidade.Text = grupo.Nacionalidade;

        if (grupo.DadosPessoa.GetType() == typeof(DadosFisica))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
            rblSexo.SelectedIndex = ((DadosFisica)grupo.DadosPessoa).Sexo.Equals('M') ? 0 : 1;
            tbxDataNascimento.Text = ((DadosFisica)grupo.DadosPessoa).DataNascimento.EmptyToMinValue();
            tbxCPF.Text = ((DadosFisica)grupo.DadosPessoa).Cpf;
            hfCnpjCpf.Value = ((DadosFisica)grupo.DadosPessoa).Cpf;
            tbxRG.Text = ((DadosFisica)grupo.DadosPessoa).Rg;
            switch (((DadosFisica)grupo.DadosPessoa).EstadoCivil)
            {
                case 's': rblEstadoCivil.SelectedIndex = 0; break;
                case 'c': rblEstadoCivil.SelectedIndex = 1; break;
                case 'p': rblEstadoCivil.SelectedIndex = 2; break;
                case 'd': rblEstadoCivil.SelectedIndex = 3; break;
                case 'v': rblEstadoCivil.SelectedIndex = 4; break;
            }
        }
        else
        {
            rbtnPessoaFisica.Checked = false;
            rbtnPessoaJuridica.Checked = true;
            tbxCNPJ.Text = ((DadosJuridica)grupo.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridica)grupo.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridica)grupo.DadosPessoa).RazaoSocial;
        }

        tbxLogradouro.Text = grupo.Endereco != null ? grupo.Endereco.Rua : "";
        tbxNumero.Text = grupo.Endereco != null ? grupo.Endereco.Numero : "";
        tbxComplemento.Text = grupo.Endereco != null ? grupo.Endereco.Complemento : "";
        tbxBairro.Text = grupo.Endereco != null ? grupo.Endereco.Bairro : "";
        tbxCEP.Text = grupo.Endereco != null ? grupo.Endereco.Cep : "";
        tbxPontoReferencia.Text = grupo.Endereco != null ? grupo.Endereco.PontoReferencia : "";
        this.CarregarEstados();
        ddlEstado.SelectedValue = grupo.Endereco != null && grupo.Endereco.Cidade != null && grupo.Endereco.Cidade.Estado != null ? grupo.Endereco.Cidade.Estado.Id.ToString() : "0";
        this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
        ddlCidade.SelectedValue = grupo.Endereco != null && grupo.Endereco.Cidade != null ? grupo.Endereco.Cidade.Id.ToString() : "0";
        tbxLimiteUsuarioEdicao.Text = grupo.LimiteUsuariosEdicao.ToString();

        if (grupo.GestaoCompartilhada)
            rblTipoPermissaoPor.SelectedValue = "C";
        else
            rblTipoPermissaoPor.SelectedValue = "N";

        chkCancelado.Checked = grupo.Cancelado;

        chkIsentoICMS.Checked = grupo.DadosPessoa.IsentoICMS;
        tbxInscricaoEstadual.Text = grupo.DadosPessoa.InscricaoEstadual;
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;
        tbxObservacoes.Text = grupo.Observacoes;
        this.SetDescricaoAtivacao();
        this.CarregarEmpresas(grupo);
        if (grupo.LimiteEmpresas > 0)
        {
            tbxLimiteEmpresas.Text = grupo.LimiteEmpresas.ToString();
            tbxLimiteProcessos.Text = "Ilimitado";
            tbxLimiteProcessos.Enabled = false;
            ddlTipoControle.SelectedValue = "1";
            ddlTipoControle.Enabled = false;
        }
        else if (grupo.LimiteProcessos > 0)
        {
            tbxLimiteEmpresas.Text = "Ilimitado";
            tbxLimiteEmpresas.Enabled = false;
            tbxLimiteProcessos.Text = grupo.LimiteProcessos.ToString();
            ddlTipoControle.SelectedValue = "2";
            ddlTipoControle.Enabled = false;
        }
    }

    private void SetDescricaoAtivacao()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
        this.lblAtivadoLogus.Text = grupo != null && grupo.AtivoLogus ? "Sim" : "Não";
        this.lblAtivoAmbientalis.Text = grupo != null && grupo.AtivoAmbientalis ? "Sim" : "Não";
        this.btnAtivarLogus.Visible = hfId.Value.ToInt32() > 0 && (grupo != null && !grupo.AtivoLogus);
        this.btnAtivarAmbientalis.Visible = hfId.Value.ToInt32() > 0 && (grupo != null && !grupo.AtivoAmbientalis);
    }

    private void CarregarEmpresas(GrupoEconomico c)
    {
        dgr.DataSource = c.Empresas;
        dgr.DataBind();
    }

    private void CarregarEstados()
    {
        IList<Estado> estados = Estado.ConsultarTodos();
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = estados;
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void Excluir()
    {
        //fazer verificações de exclusao
        if (Convert.ToInt32(hfId.Value) > 0)
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(Convert.ToInt32(hfId.Value));
            if (c.Empresas == null || c.Empresas.Count == 0)
            {
                c.Excluir();
                this.Novo();
            }
            else
            {
                msg.CriarMensagem("Este grupo possui Empresas cadastradas portanto não pode ser excluido!", "Alerta", MsgIcons.AcessoNegado);
            }
        }
        else
            msg.CriarMensagem("Nao há grupo econômico salvo para ser excluido!", "Alerta");
    }

    private void Novo()
    {
        ddlSistema.Enabled = true;
        hfId.Value = "0";
        Response.Redirect("CadastroGrupoEconomico.aspx", false);
        visualizacao_conts.Attributes.CssStyle.Add("display", "block");

    }

    private void Salvar()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
        if (grupo == null)
            grupo = new GrupoEconomico();
        grupo.Nome = tbxNome.Text.Trim();
        grupo.Responsavel = tbxResponsavel.Text.Trim();
        grupo.RepresentanteLegal = tbxRepresentanteLegal.Text.Trim();
        grupo.GestorEconomico = tbxGestorEconomico.Text.Trim();
        grupo.Site = tbxSite.Text.Trim();
        grupo.Contato.Email = tbxEmail.Text.Trim();
        grupo.Contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        grupo.Contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        grupo.Contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        grupo.Contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        grupo.Nacionalidade = tbxNacionalidade.Text.Trim();

        if (rbtnPessoaFisica.Checked)
        {
            DadosFisica df = new DadosFisica();
            if (Utilitarios.Validadores.ValidaCPF(tbxCPF.Text))
            {
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                {
                    if (grupo.DadosPessoa.GetType() == typeof(DadosJuridica))
                    {
                        df = df.Salvar();
                        GrupoEconomico auxiliar = new GrupoEconomico();
                        auxiliar = (GrupoEconomico)grupo.Clone();
                        auxiliar.DadosPessoa = df;
                        if (ValidadorCPFJaCadastrado(auxiliar, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                        {
                            DadosJuridica aux = DadosJuridica.ConsultarPorId(grupo.DadosPessoa.Id);
                            aux.Excluir();
                            grupo.DadosPessoa = df;
                            tbxCNPJ.Text = "";
                            tbxRazaoSocial.Text = "";
                        }
                        else
                        {
                            msg.CriarMensagem("CPF já cadastrado. Informe outro CPF para efetuar o cadastro.", "Alerta", MsgIcons.Alerta);
                            df.Excluir();
                            return;
                        }
                    }
                    else
                    {
                        df = (DadosFisica)grupo.DadosPessoa;
                    }
                }

                if (ValidadorCPFJaCadastrado(grupo, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                {
                    df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
                }
                else
                {
                    msg.CriarMensagem("CPF já cadastrado. Informe outro CPF para efetuar o cadastro.", "Alerta", MsgIcons.Alerta);
                    return;
                }

            }
            else
            {
                msg.CriarMensagem("CPF informado não é valido.", "Alerta", MsgIcons.Alerta);
                return;
            }
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? Utilitarios.WebUtil.MenorValorDataSqlServer2000 : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            grupo.DadosPessoa = df;


        }
        else
        {
            DadosJuridica dj = new DadosJuridica();
            if (Utilitarios.Validadores.ValidaCNPJ(tbxCNPJ.Text))
            {
                if (Convert.ToInt32("0" + hfId.Value) > 0)

                    if (grupo.DadosPessoa.GetType() == typeof(DadosFisica))
                    {
                        dj = dj.Salvar();
                        GrupoEconomico auxiliar = new GrupoEconomico();
                        auxiliar = (GrupoEconomico)grupo.Clone();
                        auxiliar.DadosPessoa = dj;
                        if (ValidadorCNPJJaCadastrado(auxiliar, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                        {
                            DadosFisica aux = DadosFisica.ConsultarPorId(grupo.DadosPessoa.Id);
                            aux.Excluir();
                            grupo.DadosPessoa = dj;
                            tbxCPF.Text = "";
                            tbxDataNascimento.Text = "";
                            tbxRG.Text = "";
                        }
                        else
                        {
                            msg.CriarMensagem("CNPJ já cadastrado. Informe outro CNPJ para efetuar o cadastro.", "Alerta", MsgIcons.Alerta);
                            dj.Excluir();
                            return;
                        }
                    }
                    else
                    {
                        dj = (DadosJuridica)grupo.DadosPessoa;
                    }

                if (ValidadorCNPJJaCadastrado(grupo, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                {
                    dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
                }
                else
                {
                    msg.CriarMensagem("CNPJ já cadastrado. Informe outro CNPJ para efetuar o cadastro.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
            {
                msg.CriarMensagem("CNPJ informado não é valido.", "Alerta", MsgIcons.Alerta);
                return;
            }
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            dj = dj.Salvar();
            grupo.DadosPessoa = dj;
        }

        Endereco endereco = grupo.Endereco != null ? grupo.Endereco : new Endereco();
        endereco.Bairro = tbxBairro.Text;
        endereco.Numero = tbxNumero.Text;
        endereco.Complemento = tbxComplemento.Text;
        endereco.Rua = tbxLogradouro.Text;
        endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        endereco.PontoReferencia = tbxPontoReferencia.Text;
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));

        grupo.Endereco = endereco;
        grupo.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        grupo.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;
        grupo.LimiteEmpresas = ddlTipoControle.SelectedValue.ToInt32() == 1 ? tbxLimiteEmpresas.Text.ToInt32() : 0;
        grupo.LimiteProcessos = ddlTipoControle.SelectedValue.ToInt32() == 2 ? tbxLimiteProcessos.Text.ToInt32() : 0;
        grupo.LimiteUsuariosEdicao = tbxLimiteUsuarioEdicao.Text.ToInt32();

        grupo.GestaoCompartilhada = rblTipoPermissaoPor.SelectedValue == "C";

        if (chkCancelado.Checked && grupo.Ativo)
            grupo.DataCancelamento = DateTime.Now;

        grupo.Cancelado = chkCancelado.Checked;

        grupo.Observacoes = tbxObservacoes.Text;

        Usuario usuario = this.UsuarioLogado;
        if (usuario != null && usuario.Administrador != null)
            grupo.Administrador = usuario.Administrador;
        grupo.DataCadastro = grupo.Id > 0 ? grupo.DataCadastro : DateTime.Now;
        grupo = grupo.Salvar();
        grupo.Emp = grupo.Id;
        grupo = grupo.Salvar();
        hfId.Value = grupo.Id.ToString();
        this.SetDescricaoAtivacao();
        msg.CriarMensagem("Grupo Econômico salvo com sucesso!", "Sucesso");
        ddlSistema.Enabled = false;
        if (grupo.AtivoAmbientalis == false || grupo.AtivoLogus == false)
            msg.CriarMensagem("Para efetivar o cadastro é necessária a ativação do Grupo Econômico", "Atenção", MsgIcons.Alerta);

        try
        {
            if (grupo.Cancelado)
            {
                Venda.CancelarVenda(grupo, Session["idConfig"].ToString().ToInt32());
            }
            else
            {
                Venda.ReativarVenda(grupo, Session["idConfig"].ToString().ToInt32());
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem("Erro durante o cancelamento da venda:\r\n " + ex, "Erro", MsgIcons.Erro);
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    private void CarregarCidades(int idEstado)
    {
        if (idEstado <= 0)
        {
            ddlCidade.Items.Clear();
            return;
        }
        Estado estado = Estado.ConsultarPorId(idEstado);
        ddlCidade.DataValueField = "Id";
        ddlCidade.DataTextField = "Nome";
        ddlCidade.DataSource = estado.Cidades;
        ddlCidade.DataBind();
    }

    private void AtivarGrupo()
    {
        Administrador adm = Administrador.ConsultarPorId(hfIdAdministrador.Value.ToInt32());
        if (adm == null)
        {
            msg.CriarMensagem("Este administrador ainda não definido no sistema", "Informação", MsgIcons.Informacao);
            return;
        }

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());

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
        grupo = grupo.Salvar();
        this.SetDescricaoAtivacao();
        msg.CriarMensagem("Ativação realizada com sucesso!", "Sucesso", MsgIcons.Sucesso);

        Venda.InserirComissoes(grupo, null, this.IdConfig.ToInt32());
    }

    public bool ValidadorCNPJJaCadastrado(GrupoEconomico grupo, String CNPJ)
    {
        if (hfId.Value.ToInt32() <= 0 || hfId.Value == null || hfCnpjCpf.Value != CNPJ)
        {
            if (GrupoEconomico.ConsultarCnpjCpfJaCadastrado(grupo, CNPJ, 2))
            {
                return false;
            }
        }
        return true;
    }

    public bool ValidadorCPFJaCadastrado(GrupoEconomico grupo, String CPF)
    {
        if (hfId.Value.ToInt32() <= 0 || hfId.Value == null || hfCnpjCpf.Value != CPF)
        {
            if (GrupoEconomico.ConsultarCnpjCpfJaCadastrado(grupo, CPF, 1))
            {
                return false;
            }
        }
        return true;
    }


    #endregion

    #region ________________ Bindings ________________

    public String bindTelefone(Object o)
    {
        Empresa e = (Empresa)o;
        if (e != null && e.Id > 0 && e.Contato != null)
            return e.Contato.ContatoTelefones;
        return "";
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

    public String bindRazaoSocial(Object o)
    {
        Empresa e = (Empresa)o;

        if (e.DadosPessoa != null)
            if (e.DadosPessoa.GetType() == typeof(DadosJuridica))
                return ((DadosJuridica)e.DadosPessoa).RazaoSocial;
        return "";
    }

    #endregion

    #region ________________ Trigger Dinâmica ________________

    protected void btnAtivarLogus_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAtivacao);
    }

    #endregion

    protected void ibtnVisualizarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanelVisualizarContratos);
    }

    protected void ibtnVisualizarContrato_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            if (hfId.Value.ToInt32() > 0)
            {
                GrupoEconomico g = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
                if (g.Contratos != null && g.Contratos.Count > 0)
                {
                    this.CarregarContratosDoGrupo(g);
                    modalContratos_extender.Show();
                    MultiView2.ActiveViewIndex = 0;
                    if (g.Contratos != null && g.Contratos[0] != null)
                        this.ExibirContratoOriginal(g.Contratos[0]);
                }
                else
                {
                    msg.CriarMensagem("Este grupo econômico não possui contrato ou aditivos, para visualizar.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }
            else
            {
                msg.CriarMensagem("Salve primeiro o grupo econômico para poder visualizar seu contrato e aditivos", "Alerta", MsgIcons.Alerta);
                return;
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

    private void ExibirContratoOriginal(Contrato contrato)
    {
        if (contrato != null)
        {
            lblContratoOriginalNumero.Text = contrato.NumeroContrato + "/" + contrato.AnoContrato;
            string[] textoContrato = contrato.TextoContrato.Split('#');
            lblContratoOriginalContratante.Text = textoContrato[1];
            lblContratoOriginalEmpresas.Text = textoContrato[2];
            lblContratoOriginalUsuarios.Text = textoContrato[3];
            lblContratoOriginalTotal.Text = textoContrato[4];
            lblCOntratoOriginalData.Text = textoContrato[5];
            lblPrecoMensalidade.Text = textoContrato[6];
            if (textoContrato.Length >= 8)
            {
                lblMesesCarencia.Text = textoContrato[7];
            }
            else
            {
                lblMesesCarencia.Text = " ";
            }

            if (textoContrato.Length >= 9)
            {
                lblModulosContratoOriginal.Text = textoContrato[8];
            }
            else
            {
                lblModulosContratoOriginal.Text = " ";
            }
        }
    }

    private void ExibirAditivoSelecionado(Contrato contrato)
    {
        if (contrato != null && contrato.Aditamento == true)
        {
            string[] textoContrato = contrato.TextoContrato.Split('#');
            lblContOriginalDoAditivoVisualizacao.Text = lblContOriginalDoAditivo2Visualizacao.Text = textoContrato[0];
            lblContratanteDoAditivoVisualizacao.Text = textoContrato[1];
            lblContOriginalAditivoDataVisualizacao.Text = textoContrato[5];
            lblAditivoEmpresasVisualizacao.Text = textoContrato[2];
            lblAditivoUsuariosVisualizacao.Text = textoContrato[3];
            lblAditivoTotalVisualizacao.Text = textoContrato[4];
            lblAditivoDataVisualizacao.Text = textoContrato[6];
        }
    }

    private void CarregarContratosDoGrupo(GrupoEconomico g)
    {
        string[] textoContrato = new string[7];
        ddlContrato.Items.Clear();
        if (g != null)
            foreach (Contrato cont in g.Contratos)
            {
                if (cont.Aditamento == false)
                    ddlContrato.Items.Add(new ListItem("Contrato Original", cont.Id.ToString()));
                else
                {
                    textoContrato = cont.TextoContrato.Split('#');
                    ddlContrato.Items.Add(new ListItem("Aditivo " + textoContrato[6], cont.Id.ToString()));
                }

            }
        this.ExibirContratoOriginal(g.Contratos[0]);
    }

    protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["idConfig"] = ddlSistema.SelectedValue;
        transacao.Abrir();

        try
        {
            if (ddlContrato.SelectedIndex != 0)
            {
                MultiView2.ActiveViewIndex = 1;
                this.ExibirAditivoSelecionado(Contrato.ConsultarPorId(ddlContrato.SelectedValue.ToInt32()));
            }
            else
            {
                MultiView2.ActiveViewIndex = 0;
                this.ExibirContratoOriginal(Contrato.ConsultarPorId(ddlContrato.SelectedValue.ToInt32()));
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

    protected void ddlTipoControle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoControle.SelectedValue.ToInt32() > 0)
            {
                if (ddlTipoControle.SelectedValue.ToInt32() == 1)
                {
                    tbxLimiteProcessos.Text = "Ilimitado";
                    tbxLimiteProcessos.Enabled = false;
                    tbxLimiteEmpresas.Text = "";
                    tbxLimiteEmpresas.Enabled = true;
                }
                else
                {
                    tbxLimiteEmpresas.Text = "Ilimitado";
                    tbxLimiteEmpresas.Enabled = false;
                    tbxLimiteProcessos.Text = "";
                    tbxLimiteProcessos.Enabled = true;
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
}