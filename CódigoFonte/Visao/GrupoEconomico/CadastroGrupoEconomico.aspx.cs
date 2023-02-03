using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;
using System.Drawing;

public partial class GrupoEconomico_CadastroGrupoEconomico : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

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
                string teste = Session["idConfig"].ToString();
                if (teste == "1")
                {
                    ibtnContratarMaisEmpresas.Visible = false;
                    lblVisualizarCont.Visible = false;
                    lblAlterarCont.Visible = false;
                    ibtnVisualizarContrato.Visible = false;
                }
                if (teste == "0" && Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request) == null)
                {
                    if (UsuarioLogado != null && UsuarioLogado.GrupoEconomico != null && UsuarioLogado.GrupoEconomico.Id > 0)
                        hfId.Value = UsuarioLogado.GrupoEconomico.Id.ToString();
                }
                else
                {
                    hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                }

                this.HabilitarCamposAdm(false);
                this.CarregarCampos();
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                    this.CarregarGrupoEconomico(Convert.ToInt32("" + hfId.Value));

                this.UsuarioEditorModuloGeral = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloGeral;

                if (!this.UsuarioEditorModuloGeral)
                    this.DesabilitarCadastro();
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

    private void DesabilitarCadastro()
    {
        ibtnContratarMaisEmpresas.Enabled = ibtnContratarMaisEmpresas.Visible = ibtnContratarMaisProcessos.Enabled = ibtnContratarMaisProcessos.Visible = lblAlterarCont.Visible = 
            lblAlterarProcessos.Visible = btnAdicionarEmpresa.Enabled = btnAdicionarEmpresa.Visible = btnSalvar.Enabled = btnSalvar.Visible = false;
    }

    private void HabilitarCamposAdm(bool p)
    {
        tbxResponsavel.Enabled = tbxRepresentanteLegal.Enabled = tbxBairro.Enabled = tbxCelular.Enabled = tbxCEP.Enabled = tbxComplemento.Enabled =
            tbxDataNascimento.Enabled = tbxEmail.Enabled = tbxFax.Enabled = tbxGestorEconomico.Enabled = tbxInscricaoEstadual.Enabled = tbxLogradouro.Enabled =
            tbxNacionalidade.Enabled = tbxNumero.Enabled = tbxObservacoes.Enabled = tbxPontoReferencia.Enabled = tbxRamal.Enabled = tbxRG.Enabled =
            tbxSite.Enabled = tbxTelefone.Enabled = chkIsentoICMS.Enabled = rblEstadoCivil.Enabled = rblSexo.Enabled = ddlEstado.Enabled = ddlCidade.Enabled = !p;

        tbxNome.Enabled = tbxCPF.Enabled = tbxLimiteEmpresas.Enabled = tbxLimiteUsuarioEdicao.Enabled = tbxLimiteProcessos.Enabled =
            tbxRazaoSocial.Enabled = tbxCNPJ.Enabled = rbtnPessoaFisica.Enabled = rbtnPessoaJuridica.Enabled = p;
    }

    #region _______________ Eventos _________________

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void dgr_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgr.CurrentPageIndex = e.NewPageIndex;
            this.CarregarEmpresas(GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32()));
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
            this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Salvar();
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

    protected void btnAdicionarEmpresa_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Empresa/ManterEmpresa.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("idGrupoEconomico=" + hfId.Value), false);
    }

    protected void btnAdicionarEmpresa_PreRender(object sender, EventArgs e)
    {
        btnAdicionarEmpresa.Visible = hfId.Value.ToInt32() > 0;
    }

    protected void PermissaoUsuario_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((Button)sender, this.UsuarioEditorModuloGeral);
    }

    protected void ddlQuantidadeDeUsuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CalcularPreco();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEstado.SelectedValue == "0")
            {
                lblResult.Text = "Selecione um Estado para prosseguir.";
                return;
            }

            if (ddlCidade.SelectedValue == "0")
            {
                lblResult.Text = "Selecione uma Cidade para prosseguir.";
                return;
            }

            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
            if (grupo != null)
            {
                int limiteEscolhido;
                if (grupo.LimiteEmpresas > 0)
                {
                    limiteEscolhido = this.ObterLimiteEmpresas();
                    if (grupo.Empresas != null && grupo.Empresas.Count > limiteEscolhido)
                    {
                        msg.CriarMensagem("Você possui " + grupo.Empresas.Count + " empresas cadastradas, portanto você não pode reduzir seu limite para " + limiteEscolhido + ". Para reduzir seu limite, exclua as empresas que não forem mais necessárias.", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                }
                else if (grupo.LimiteProcessos > 0)
                {
                    limiteEscolhido = this.ObterLimiteProcessos();
                    if (grupo.GetTotalProcessosDoGrupo > limiteEscolhido)
                    {
                        msg.CriarMensagem("Você possui " + grupo.GetTotalProcessosDoGrupo + " processos cadastrados, portanto você não pode reduzir seu limite para " + limiteEscolhido + ". Para reduzir seu limite, exclua os processos que não forem mais necessários.", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                }

                if (grupo.GetQuantidadeUsuariosDoGrupo(grupo, grupo.Usuarios) > (ddlQuantidadeDeUsuarios.SelectedValue.ToInt32() + 1))
                {
                    msg.CriarMensagem("Você possui " + grupo.GetQuantidadeUsuariosDoGrupo(grupo, grupo.Usuarios) + " usuários cadastrados, portanto você não pode reduzir seu limite para " + (ddlQuantidadeDeUsuarios.SelectedValue.ToInt32() + 1) + ". Para reduzir seu limite, exclua os usuários que não forem mais necessários.", "Alerta", MsgIcons.Alerta);
                    return;
                }
            }

            this.AtualizarContrato();

            lblResult.Text = "";
            MultiView1.ActiveViewIndex = 1;
            ckbConcordo.ForeColor = Color.Black;
            ckbConcordo.Font.Bold = false;
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

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
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

    protected void rblEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CalcularPreco();
    }

    protected void btnConcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (ckbConcordo.Checked)
            {
                this.SalvarNovoContrato();
                GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
                tbxLimiteEmpresas.Text = grupo.LimiteEmpresas > 0 ? grupo.LimiteEmpresas.ToString() : "Ilimitado";
                tbxLimiteUsuarioEdicao.Text = grupo.LimiteUsuariosEdicao.ToString();
                tbxLimiteProcessos.Text = grupo.LimiteProcessos > 0 ? grupo.LimiteProcessos.ToString() : "Ilimitado";
            }
            else
            {
                ckbConcordo.ForeColor = Color.Red;
                ckbConcordo.Font.Bold = true;
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

    protected void ibtnContratarMaisEmpresas_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpConteudoContratar);
    }

    protected void ibtnContratarMaisEmpresas_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() == 0)
            {
                msg.CriarMensagem("Salve primeiro o grupo econômico para poder realizar a contratação", "Alerta", MsgIcons.Alerta);
                return;
            }
            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
            if (grupo.Contratos != null && grupo.Contratos.Count > 0)
            {
                modalContratarmais_extender.Show();
                MultiView1.ActiveViewIndex = 0;

                if (grupo.LimiteEmpresas > 0)
                {
                    por_empresas.Visible = true;
                    por_processos.Visible = false;

                }
                else if (grupo.LimiteProcessos > 0)
                {
                    por_empresas.Visible = false;
                    por_processos.Visible = true;
                }

                lblPlanoAtual.Text = this.ObterPlanoEmpresas(grupo) + " e " + grupo.LimiteUsuariosEdicao + " usuários.";
                rblEmpresas.SelectedIndex = 0;
                ddlQuantidadeDeUsuarios.SelectedIndex = 0;
                lblTextoComercial.Text = "** Acima de 20 empresas e/ou filiais, ou acima de 10 usuários, consulte o setor comercial";
                this.CalcularPreco();
            }
            else
            {
                msg.CriarMensagem("Este grupo econômico não possui contrato, não é possível inserir aditivos", "Alerta", MsgIcons.Alerta);
                return;
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

    private string ObterPlanoEmpresas(GrupoEconomico grupo)
    {
        string retorno = "";
        if (grupo.LimiteEmpresas <= 5)
            retorno = "Até 5 empresas e/ou filiais";
        if (grupo.LimiteEmpresas > 5 && grupo.LimiteEmpresas <= 10)
            retorno = "Até 10 empresas e/ou filiais";
        if (grupo.LimiteEmpresas > 10 && grupo.LimiteEmpresas <= 15)
            retorno = "Até 15 empresas e/ou filiais";
        if (grupo.LimiteEmpresas > 15 && grupo.LimiteEmpresas <= 20)
            retorno = "Até 20 empresas e/ou filiais";
        if (grupo.LimiteEmpresas > 20)
            retorno = "Acima de 20 empresas e/ou filiais";
        return retorno;
    }

    #endregion

    #region ________________ Métodos ________________

    private void CarregarCampos()
    {
        this.CarregarEstados();
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
            tbxCNPJ.Enabled = false;
            tbxRazaoSocial.Enabled = false;
            rbtnPessoaJuridica.Enabled = false;
            tbxCPF.Enabled = false;
        }
        else
        {
            rbtnPessoaFisica.Checked = false;
            rbtnPessoaJuridica.Checked = true;
            tbxCNPJ.Text = ((DadosJuridica)grupo.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridica)grupo.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridica)grupo.DadosPessoa).RazaoSocial;
            tbxCNPJ.Enabled = false;
            tbxRazaoSocial.Enabled = false;
            rbtnPessoaFisica.Enabled = false;
            tbxCPF.Enabled = false;
        }

        tbxLogradouro.Text = grupo.Endereco.Rua;
        tbxNumero.Text = grupo.Endereco.Numero;
        tbxComplemento.Text = grupo.Endereco.Complemento;
        tbxBairro.Text = grupo.Endereco.Bairro;
        tbxCEP.Text = grupo.Endereco.Cep;
        tbxPontoReferencia.Text = grupo.Endereco.PontoReferencia;
        this.CarregarEstados();
        ddlEstado.SelectedValue = grupo.Endereco.Cidade != null && grupo.Endereco.Cidade.Estado != null ? grupo.Endereco.Cidade.Estado.Id.ToString() : "0";
        this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
        ddlCidade.SelectedValue = grupo.Endereco.Cidade != null ? grupo.Endereco.Cidade.Id.ToString() : "0";
        if (grupo.LimiteEmpresas > 0)
        {
            tbxLimiteEmpresas.Text = grupo.LimiteEmpresas.ToString();
            tbxLimiteProcessos.Text = "Ilimitado";
            ibtnContratarMaisEmpresas.Enabled = true;
            ibtnContratarMaisEmpresas.Visible = true;
            ibtnContratarMaisProcessos.Enabled = false;
            ibtnContratarMaisProcessos.Visible = false;
            lblAlterarCont.Visible = true;
            lblAlterarProcessos.Visible = false;
        }
        else if (grupo.LimiteProcessos > 0)
        {
            tbxLimiteEmpresas.Text = "Ilimitado";
            tbxLimiteProcessos.Text = grupo.LimiteProcessos.ToString();
            ibtnContratarMaisProcessos.Enabled = true;
            ibtnContratarMaisProcessos.Visible = true;
            ibtnContratarMaisEmpresas.Enabled = false;
            ibtnContratarMaisEmpresas.Visible = false;
            lblAlterarProcessos.Visible = true;
            lblAlterarCont.Visible = false;
        }

        tbxLimiteUsuarioEdicao.Text = grupo.LimiteUsuariosEdicao.ToString();

        if (grupo.GestaoCompartilhada)
            rblTipoPermissaoPor.SelectedValue = "C";
        else
            rblTipoPermissaoPor.SelectedValue = "N";

        chkIsentoICMS.Checked = grupo.DadosPessoa.IsentoICMS;
        tbxInscricaoEstadual.Text = grupo.DadosPessoa.InscricaoEstadual;
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;
        tbxObservacoes.Text = grupo.Observacoes;
        this.CarregarEmpresas(grupo);
    }

    private void CarregarEmpresas(GrupoEconomico c)
    {
        dgr.DataSource = c.Empresas;
        dgr.DataBind();
    }

    private void CarregarEstados()
    {
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = Estado.ConsultarTodos();
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void Excluir()
    {
        //fazer verificações de exclusao
        if (Convert.ToInt32(hfId.Value) > 0)
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(Convert.ToInt32(hfId.Value));
            c.Excluir();
            this.Novo();
        }
        else
            msg.CriarMensagem("Nao há grupo econômico salvo para ser excluido!", "Alerta");
    }

    private void Novo()
    {
        hfId.Value = "0";
        Response.Redirect("CadastroGrupoEconomico.aspx", false);
    }

    private void Salvar()
    {
        if (tbxEmail.Text.Trim().Replace(" ", "") != "")
        {
            if (!WebUtil.ValidarEmailInformado(tbxEmail.Text))
            {
                msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                return;
            }
        }

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
        if (grupo == null)
            grupo = new GrupoEconomico();
        grupo.Nome = tbxNome.Text.Trim();
        grupo.Responsavel = tbxResponsavel.Text.Trim();
        grupo.RepresentanteLegal = tbxRepresentanteLegal.Text.Trim();
        grupo.GestorEconomico = tbxGestorEconomico.Text.Trim();
        grupo.Site = tbxSite.Text.Trim();
        grupo.Contato.Email = tbxEmail.Text.Trim().Replace(";", " ;").Replace("  ", " ");
        grupo.Contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        grupo.Contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        grupo.Contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        grupo.Contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        grupo.Nacionalidade = tbxNacionalidade.Text.Trim();

        if (rbtnPessoaFisica.Checked)
        {
            DadosFisica df = new DadosFisica();
            if (Convert.ToInt32("0" + hfId.Value) > 0)
                df = (DadosFisica)grupo.DadosPessoa;
            if (Utilitarios.Validadores.ValidaCPF(tbxCPF.Text))
            {
                if (hfId.Value.ToInt32() <= 0 || hfId.Value == null || hfCnpjCpf.Value != tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""))
                {
                    if (GrupoEconomico.ConsultarCnpjCpfJaCadastrado(grupo, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""), 1))
                    {
                        msg.CriarMensagem("CPF já cadastrado. Informe outro CPF para efetuar o cadastro.", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                }
                df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            }
            else
            {
                msg.CriarMensagem("CPF informado não é valido.", "Alerta", MsgIcons.Alerta);
                return;
            }
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? SqlDate.MinValue : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            grupo.DadosPessoa = df;
        }
        else
        {
            DadosJuridica dj = new DadosJuridica();
            if (Convert.ToInt32("0" + hfId.Value) > 0)
                dj = (DadosJuridica)grupo.DadosPessoa;
            if (Utilitarios.Validadores.ValidaCNPJ(tbxCNPJ.Text))
            {
                if (hfId.Value.ToInt32() <= 0 || hfId.Value == null || hfCnpjCpf.Value != tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""))
                {
                    if (GrupoEconomico.ConsultarCnpjCpfJaCadastrado(grupo, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""), 2))
                    {
                        msg.CriarMensagem("CNPJ já cadastrado. Informe outro CNPJ para efetuar o cadastro.", "Alerta", MsgIcons.Alerta);
                        return;
                    }
                }
                dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
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

        grupo.Endereco.Bairro = tbxBairro.Text;
        grupo.Endereco.Numero = tbxNumero.Text;
        grupo.Endereco.Complemento = tbxComplemento.Text;
        grupo.Endereco.Rua = tbxLogradouro.Text;
        grupo.Endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        grupo.Endereco.PontoReferencia = tbxPontoReferencia.Text;
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            grupo.Endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));

        grupo.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        grupo.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;
        grupo.LimiteEmpresas = tbxLimiteEmpresas.Text.ToInt32();
        grupo.LimiteUsuariosEdicao = tbxLimiteUsuarioEdicao.Text.ToInt32();
        grupo.Observacoes = tbxObservacoes.Text;

        Usuario usuario = this.UsuarioLogado;
        if (usuario != null && usuario.Administrador != null)
            grupo.Administrador = usuario.Administrador;
        if (grupo.Id == 0)
            grupo.DataCadastro = DateTime.Now;
        grupo = grupo.Salvar();
        grupo.Emp = grupo.Id;
        grupo = grupo.Salvar();
        hfId.Value = grupo.Id.ToString();
        msg.CriarMensagem("Grupo Econômico cadastrado com sucesso!", "Sucesso");
        if (grupo.AtivoAmbientalis == false || grupo.AtivoLogus == false)
            msg.CriarMensagem("Para efetivar o cadastro é necessária a ativação do Grupo Econômico", "Atenção", MsgIcons.Alerta);

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

    private void AtualizarContrato()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());

        if (grupo != null && grupo.Contratos != null && grupo.Contratos.Count > 0)
        {
            lblNumeroContrato.Text = lblNumeroContrato3.Text = grupo.Contratos[0].NumeroContrato + "/" + grupo.Contratos[0].AnoContrato;
            string[] textoContrato = grupo.Contratos[0].TextoContrato.Split('#');
            lblDataContratoOriginal.Text = textoContrato[5];
        }
        lblUsuarios.Text = ddlQuantidadeDeUsuarios.SelectedItem.Text;
        lblEmpresas.Text = grupo.LimiteEmpresas > 0 ? rblEmpresas.SelectedItem.Text : rblProcessos.SelectedItem.Text;
        lblTotal.Text = lblPreco.Text;
        lblDataAditivo.Text = DateTime.Now.Day + " de " + Contrato.GetNomeMes(DateTime.Now.Month) + " de " + DateTime.Now.Year.ToString();

        if (rbtnPessoaFisica.Checked)
            lblContratante.Text = tbxNome.Text + " residente à " + tbxLogradouro.Text + ", Número:" + tbxNumero.Text + ", Bairro: " + tbxBairro.Text + ", CEP: " + tbxCEP.Text + ", Cidade:" + ddlCidade.SelectedItem.Text + ", Estado:" + ddlEstado.SelectedItem.Text + " , devidamente registrado com o CPF/MF sob n° " + tbxCPF.Text;
        else
            lblContratante.Text = tbxRazaoSocial.Text + " localizada à " + tbxLogradouro.Text + ", Número:" + tbxNumero.Text + ", Bairro: " + tbxBairro.Text + ", CEP: " + tbxCEP.Text + ", Cidade: " + ddlCidade.SelectedItem.Text + ", Estado:" + ddlEstado.SelectedItem.Text + " , devidamente registrado com no CNPJ/MF sob n° " + tbxCNPJ.Text + (tbxInscricaoEstadual.Text != "" ? ", Inscrição Estadual nº: " + tbxInscricaoEstadual.Text : "") + ", representada neste ato por " + tbxRepresentanteLegal.Text;
    }

    private void CalcularPreco()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
        lblPreco.Text = "R$ " + ((grupo.LimiteEmpresas > 0 ? rblEmpresas.SelectedValue.ToInt32() : rblProcessos.SelectedValue.ToInt32()) + (ddlQuantidadeDeUsuarios.SelectedValue.ToInt32() * 55)).ToString("N2");
    }

    private void SalvarNovoContrato()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
        grupo.LimiteEmpresas = grupo.LimiteEmpresas > 0 ? this.ObterLimiteEmpresas() : 0;
        grupo.LimiteProcessos = grupo.LimiteProcessos > 0 ? this.ObterLimiteProcessos() : 0;
        grupo.LimiteUsuariosEdicao = ddlQuantidadeDeUsuarios.SelectedIndex + 1;

        Contrato contrato = new Contrato();
        contrato.AnoContrato = DateTime.Now.Year.ToString();
        contrato.DataAceite = DateTime.Now;
        contrato.Aditamento = true;
        contrato.Mensalidade = Convert.ToDecimal(((grupo.LimiteEmpresas > 0 ? rblEmpresas.SelectedValue.ToInt32() : rblProcessos.SelectedValue.ToInt32()) + (ddlQuantidadeDeUsuarios.SelectedValue.ToInt32() * 50)).ToString("N2"));
        contrato.Carencia = lblMesesCarencia.Text.ToInt32();
        contrato.TextoContrato = lblNumeroContrato.Text + "#" + lblContratante.Text + "#" + lblEmpresas.Text + "#" + lblUsuarios.Text + "#" + lblTotal.Text + "#" + lblDataContratoOriginal.Text + "#" + lblDataAditivo.Text;
        contrato = contrato.Salvar();
        grupo.Contratos.Add(contrato);
        this.SalvarContratosDoGrupo(grupo);
        grupo = grupo.Salvar();

        #region ________________ Recalcullar comissão ________________


        if (Session["idEmp"] != null)
        {
            string emp = Session["idEmp"].ToString().Trim();
            try
            {
                Session["idEmp"] = null;
                Venda.RecalcularComissoes(grupo, this.IdConfig.ToInt32());
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
            Venda.RecalcularComissoes(grupo, this.IdConfig.ToInt32());
        }


        #endregion

        this.EnviarEmail();


        lblResult.Text = "";
        MultiView1.ActiveViewIndex = 2;
    }

    private int ObterLimiteEmpresas()
    {
        int retorno = 0;
        if (rblEmpresas.SelectedIndex == 0)
            retorno = 2;
        if (rblEmpresas.SelectedIndex == 1)
            retorno = 5;
        if (rblEmpresas.SelectedIndex == 2)
            retorno = 10;
        if (rblEmpresas.SelectedIndex == 3)
            retorno = 15;
        if (rblEmpresas.SelectedIndex == 4)
            retorno = 20;
        return retorno;
    }

    private int ObterLimiteProcessos()
    {
        int retorno = 0;
        if (rblProcessos.SelectedIndex == 0)
            retorno = 10;
        if (rblProcessos.SelectedIndex == 1)
            retorno = 15;
        if (rblProcessos.SelectedIndex == 2)
            retorno = 25;
        if (rblProcessos.SelectedIndex == 3)
            retorno = 40;
        if (rblProcessos.SelectedIndex == 4)
            retorno = 60;
        if (rblProcessos.SelectedIndex == 5)
            retorno = 90;
        if (rblProcessos.SelectedIndex == 6)
            retorno = 120;
        if (rblProcessos.SelectedIndex == 7)
            retorno = 160;
        if (rblProcessos.SelectedIndex == 8)
            retorno = 220;
        if (rblProcessos.SelectedIndex == 9)
            retorno = 300;
        if (rblProcessos.SelectedIndex == 10)
            retorno = 400;
        if (rblProcessos.SelectedIndex == 11)
            retorno = 500;
        if (rblProcessos.SelectedIndex == 12)
            retorno = 600;
        if (rblProcessos.SelectedIndex == 13)
            retorno = 700;

        return retorno;
    }

    private void SalvarContratosDoGrupo(GrupoEconomico grupo)
    {
        if (grupo != null && grupo.Contratos != null && grupo.Contratos.Count > 0)
        {
            foreach (Contrato item in grupo.Contratos)
            {
                item.ConsultarPorId();
                item.Salvar();
            }
        }
    }

    private void EnviarEmail()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
        Email email = new Email();
        email.Assunto = "Alteração de Contrato - Sistema Sustentar";        
        String mensagem = "(" + DateTime.Now + ") O grupo econômico " + grupo.Nome + " efetuou uma alteração em seu plano do Sistema Sustentar, para mais detalhes acesse o Sistema.";

        email.Mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png' /></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Alteração de Contrato<br/>Sistema Sustentar</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; background-color:#E9E9E9; text-align:center; height:auto'>O grupo econômico " + grupo.Nome + @" efetuou uma alteração em seu plano do Sistema Sustentar, para mais detalhes acesse o Sistema.</div>
  <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                              <tr>
                                <td colspan='3' align='center'><font size='6'>Alteração de Contrato - Sistema Sustentar</font>
                                  <hr /></td>
                              </tr>
                              <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Nome:</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + tbxNome.Text + @"</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Telefone:</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + tbxTelefone.Text + @"</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                               <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Email:</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + tbxEmail.Text + @"</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>                                                             
                              <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Número do Contrato:</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + lblNumeroContrato.Text + @"</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + (grupo.LimiteEmpresas > 0 ? "Quantidade de empresas:" : "Quantidade de processos:") + @"</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + lblEmpresas.Text + @"</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Quantidade de usuários:</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + lblUsuarios.Text + @"</font></td>
                              </tr>
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>
                              <tr>
                                <td width='190px' align='right' bgcolor='#CCCCCC' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>Data:</font></td>
                                <td colspan='2' align='left' valign='top'><font size='3' face='Arial, Helvetica, sans-serif'>" + DateTime.Now.ToString() + @"</font></td>
                              </tr>                              
                              <tr>
                                <td colspan='3'><hr /></td>
                              </tr>                              
                            </table></div>";

        string[] emailStr = this.EmailContato.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < emailStr.Length; i++)
            email.EmailsDestino.Add(emailStr[i]);
        email.EnviarAutenticado(25, false);

        email.EmailsDestino.Clear();

        //envia para o segundo grupo de emails
        string[] emailStr2 = this.EmailContato2.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < emailStr2.Length; i++)
            email.EmailsDestino.Add(emailStr2[i].Trim());
        email.EnviarAutenticado(25, false);
    }

    #endregion

    #region ________________ Bindings _______________

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
        return e.GetNumeroCNPJeCPFComMascara;
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

    #region -------------- Propriedades -------------
    //verificarquantidade de e-mails antes de terminar
    public string EmailContato
    {
        get
        {
            return "anderson@sustentar.inf.br;piassi@sustentar.inf.br;comercial@sustentar.inf.br;financeiro@sustentar.inf.br;rogerio@sustentar.inf.br;rogerio@logus.inf.br";
        }
    }

    public string EmailContato2
    {
        get
        {
            return "comercial@logus.inf.br;logus@logus.inf.br;rogerio@logus.inf.br;supervisor@sustentar.inf.br;adm@sustentar.inf.br;financeiro@logus.inf.br;marcelo.cornelio@logus.inf.br";
        }
    }
    #endregion


    protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }
    protected void ibtnVisualizarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpdatePanelVisualizarContratos);
    }

    protected void ibtnVisualizarContrato_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() > 0)
            {
                GrupoEconomico g = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
                if (g.Contratos != null && g.Contratos.Count > 0)
                {
                    this.CarregarContratosDoGrupo(g);
                    modalVisualizarContratos_extender.Show();
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
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    private void ExibirContratoOriginal(Contrato contrato)
    {
        if (contrato != null)
        {
            lblContratoOriginalNumeroVisualizacao.Text = contrato.NumeroContrato + "/" + contrato.AnoContrato;
            string[] textoContrato = contrato.TextoContrato.Split('#');
            lblContratoOriginalContratanteVisualizacao.Text = textoContrato[1];
            lblCOntratoOriginalEmpresasVisualizacao.Text = textoContrato[2];
            lblContratoOriginalUsuariosVisualizacao.Text = textoContrato[3];
            lblContratoOriginalTotalVisualizacao.Text = textoContrato[4];
            lblCOntratoOriginalDataVisualizacao.Text = textoContrato[5];
            lblPrecoMensalidadeVisualizacao.Text = textoContrato[6];
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

    protected void btnConcluir_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upLimitesContrato);
    }

    protected void ibtnContratarMaisProcessos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UpConteudoContratar);
    }

    protected void ibtnContratarMaisProcessos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() == 0)
            {
                msg.CriarMensagem("Salve primeiro o grupo econômico para poder realizar a contratação", "Alerta", MsgIcons.Alerta);
                return;
            }
            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfId.Value.ToInt32());
            if (grupo.Contratos != null && grupo.Contratos.Count > 0)
            {
                modalContratarmais_extender.Show();
                MultiView1.ActiveViewIndex = 0;

                if (grupo.LimiteProcessos > 0)
                {
                    por_empresas.Visible = false;
                    por_processos.Visible = true;

                }
                else if (grupo.LimiteProcessos > 0)
                {
                    por_empresas.Visible = true;
                    por_processos.Visible = false;
                }

                lblPlanoAtual.Text = "Até " + grupo.LimiteProcessos + " processos e " + grupo.LimiteUsuariosEdicao + " usuários.";
                rblProcessos.SelectedIndex = 0;
                ddlQuantidadeDeUsuarios.SelectedIndex = 0;
                lblTextoComercial.Text = "** Acima de 700 processos, ou acima de 10 usuários, consulte o setor comercial";
                this.CalcularPreco();
            }
            else
            {
                msg.CriarMensagem("Este grupo econômico não possui contrato, não é possível inserir aditivos", "Alerta", MsgIcons.Alerta);
                return;
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

    protected void rblProcessos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CalcularPreco();
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