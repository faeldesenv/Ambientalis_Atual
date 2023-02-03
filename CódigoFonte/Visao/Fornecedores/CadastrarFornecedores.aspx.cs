using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Fornecedores_CadastrarFornecedores : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();

    public bool UsuarioEditorContratos
    {
        get
        {
            if (Session["UsuarioEditorContratos"] == null)
                return false;
            else
                return (bool)Session["UsuarioEditorContratos"];
        }
        set { Session["UsuarioEditorContratos"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack) 
            {
                this.VerificarPermissoes();
                this.CarregarEstados();
                this.CarregarAtividades();
                hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                if (Convert.ToInt32(hfId.Value) > 0)
                    this.CarregarFornecedor(hfId.Value.ToInt32());
            }
        }
        catch(Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally 
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    private void VerificarPermissoes()
    {
        this.UsuarioEditorContratos = this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloContratos;

        if (this.UsuarioEditorContratos)
        {
            btnAdicionarAtividade.Visible = true;
            btnEditarAtividade.Visible = true;
            ibtnExcluirAtividade.Visible = true;
            btnNovo.Visible = true;
            btnSalvar.Visible = true;
            btnExcluir.Visible = true;
        }
        else
        {
            btnAdicionarAtividade.Visible = false;
            btnEditarAtividade.Visible = false;
            ibtnExcluirAtividade.Visible = false;
            btnNovo.Visible = false;
            btnSalvar.Visible = false;
            btnExcluir.Visible = false;
        }
    }

    #region ________Métodos__________

    private void CarregarAtividades()
    {
        ddlAtividade.DataSource = Atividade.ConsultarTodos();
        ddlAtividade.DataBind();
        ddlAtividade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }
    
    private void CarregarFornecedor(int p)
    {
        Fornecedor fornecedor = Fornecedor.ConsultarPorId(p);
        tbxNome.Text = fornecedor.Nome;
        chkAtivo.Checked = fornecedor.Ativo;
        ddlAtividade.SelectedValue = fornecedor.Atividade.Id.ToString();
        if (fornecedor.DadosPessoa.GetType() == typeof(DadosFisica))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
            rblSexo.SelectedIndex = ((DadosFisica)fornecedor.DadosPessoa).Sexo.Equals('M') ? 0 : 1;
            tbxDataNascimento.Text = ((DadosFisica)fornecedor.DadosPessoa).DataNascimento.EmptyToMinValue();
            tbxCPF.Text = ((DadosFisica)fornecedor.DadosPessoa).Cpf;
            hfCnpjCpf.Value = ((DadosFisica)fornecedor.DadosPessoa).Cpf;
            tbxRG.Text = ((DadosFisica)fornecedor.DadosPessoa).Rg;

            switch (((DadosFisica)fornecedor.DadosPessoa).EstadoCivil)
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
            tbxCNPJ.Text = ((DadosJuridica)fornecedor.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridica)fornecedor.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridica)fornecedor.DadosPessoa).RazaoSocial;
        }
        chkIsentoICMS.Checked = fornecedor.DadosPessoa.IsentoICMS;
        tbxInscricaoEstadual.Text = fornecedor.DadosPessoa.InscricaoEstadual;
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;
        tbxResponsavelTecnico.Text = fornecedor.Responsavel;

        tbxSite.Text = fornecedor.Site;
        tbxEmails.Text = fornecedor.Contato != null ? fornecedor.Contato.Email : "";
        tbxTelefone.Text = fornecedor.Contato != null ? fornecedor.Contato.Telefone : "";
        tbxCelular.Text = fornecedor.Contato != null ? fornecedor.Contato.Celular : "";
        tbxFax.Text = fornecedor.Contato != null ? fornecedor.Contato.Fax : "";
        tbxRamal.Text = fornecedor.Contato != null ? fornecedor.Contato.Ramal.ToString() : "";
        tbxLogradouro.Text = fornecedor.Endereco != null ? fornecedor.Endereco.Rua : "";
        tbxNumero.Text = fornecedor.Endereco != null ? fornecedor.Endereco.Numero : "";
        tbxComplemento.Text = fornecedor.Endereco != null ? fornecedor.Endereco.Complemento : "";
        tbxBairro.Text = fornecedor.Endereco != null ? fornecedor.Endereco.Bairro : "";
        tbxCEP.Text = fornecedor.Endereco != null ? fornecedor.Endereco.Cep : "";
        this.CarregarEstados();
        ddlEstado.SelectedValue = fornecedor.Endereco.Cidade != null && fornecedor.Endereco.Cidade.Estado != null ? fornecedor.Endereco.Cidade.Estado.Id.ToString() : "0";
        if (ddlEstado.SelectedValue.ToInt32() > 0)
        {
            this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
            ddlCidade.SelectedValue = fornecedor.Endereco.Cidade != null ? fornecedor.Endereco.Cidade.Id.ToString() : "0";
        }
        tbxObservacoes.Text = fornecedor.Observacoes;
    }

    private void CarregarEstados()
    {
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarCidades(int p)
    {
        Estado estado = Estado.ConsultarPorId(p);
        ddlCidade.DataValueField = "Id";
        ddlCidade.DataTextField = "Nome";
        ddlCidade.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        ddlCidade.DataBind();
        ddlCidade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void Salvar()
    {
        Fornecedor fornecedor = Fornecedor.ConsultarPorId(hfId.Value.ToInt32());
        if (fornecedor == null)
            fornecedor = new Fornecedor();

        if (rbtnPessoaJuridica.Checked && Fornecedor.ExisteCNPJ(fornecedor, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe um cliente com esse CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (rbtnPessoaFisica.Checked && Fornecedor.ExisteCPF(fornecedor, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe um cliente com esse CPF cadastrado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ddlAtividade.SelectedIndex <= 0)
        {
            msg.CriarMensagem("Selecione uma atividade.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (Validadores.ValidaCNPJ(tbxCNPJ.Text.Trim()) == false && rbtnPessoaJuridica.Checked)
        {
            msg.CriarMensagem("CNPJ inválido.", "Alerta", MsgIcons.Alerta);
            return;
        }
        if (Validadores.ValidaCPF(tbxCPF.Text.Trim()) == false && rbtnPessoaFisica.Checked)
        {
            msg.CriarMensagem("CPF inválido.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (tbxEmails.Text.Trim().Replace(" ", "") != "")
        {
            if (!WebUtil.ValidarEmailInformado(tbxEmails.Text))
            {
                msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                return;
            }
        }

        fornecedor.Nome = tbxNome.Text;
        fornecedor.Ativo = chkAtivo.Checked;
        fornecedor.Atividade = Atividade.ConsultarPorId(ddlAtividade.SelectedValue.ToInt32());

        DadosFisica df = new DadosFisica();
        DadosJuridica dj = new DadosJuridica();

        if (rbtnPessoaFisica.Checked)
        {

            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (fornecedor.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    df = df.Salvar();
                    Fornecedor auxiliar = new Fornecedor();
                    auxiliar = (Fornecedor)fornecedor.Clone();
                    auxiliar.DadosPessoa = df;

                    if (Fornecedor.ExisteCPF(auxiliar, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma pessoa com o CPF cadastrado.", "Alerta", MsgIcons.Alerta);
                        df.Excluir();
                        return;
                    }
                    else
                    {
                        DadosJuridica aux = DadosJuridica.ConsultarPorId(fornecedor.DadosPessoa.Id);
                        aux.Excluir();
                        fornecedor.DadosPessoa = df;
                        tbxCNPJ.Text = "";
                        tbxRazaoSocial.Text = "";
                    }
                }
                else
                {
                    df = (DadosFisica)fornecedor.DadosPessoa;
                }
            }

            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            hfCnpjCpf.Value = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? SqlDate.MinValue : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            fornecedor.DadosPessoa = df;
        }
        else
        {
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (fornecedor.DadosPessoa.GetType() == typeof(DadosFisica))
                {
                    dj = dj.Salvar();
                    Fornecedor auxiliar = new Fornecedor();
                    auxiliar = (Fornecedor)fornecedor.Clone();
                    auxiliar.DadosPessoa = dj;

                    if (Fornecedor.ExisteCNPJ(auxiliar, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma empresa com o CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
                        dj.Excluir();
                        return;
                    }
                    else
                    {
                        DadosFisica aux = DadosFisica.ConsultarPorId(fornecedor.DadosPessoa.Id);
                        aux.Excluir();
                        fornecedor.DadosPessoa = dj;
                        tbxCPF.Text = "";
                        tbxDataNascimento.Text = "";
                        tbxRG.Text = "";
                    }
                }
                else
                {
                    dj = (DadosJuridica)fornecedor.DadosPessoa;
                }
            }
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            hfCnpjCpf.Value = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            fornecedor.DadosPessoa = dj;
        }

        fornecedor.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        fornecedor.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;
        fornecedor.Responsavel = tbxResponsavelTecnico.Text;
        fornecedor.Site = tbxSite.Text;

        Contato contato = fornecedor.Contato != null ? fornecedor.Contato : new Contato();
        contato.Email = tbxEmails.Text.Trim().Replace(";", " ;").Replace("  ", " "); ;
        contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        fornecedor.Contato = contato;

        Endereco endereco = fornecedor.Endereco != null ? fornecedor.Endereco : new Endereco();
        endereco.Rua = tbxLogradouro.Text;
        endereco.Numero = tbxNumero.Text;
        endereco.Complemento = tbxComplemento.Text;
        endereco.Bairro = tbxBairro.Text;
        endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));
        fornecedor.Endereco = endereco;

        fornecedor.Observacoes = tbxObservacoes.Text;
        if (rbtnPessoaFisica.Checked)
            fornecedor.DadosPessoa = df.Salvar();
        else
            fornecedor.DadosPessoa = dj.Salvar();
        fornecedor = fornecedor.Salvar();
        hfId.Value = fornecedor.Id.ToString();
        msg.CriarMensagem("Fornecedor cadastrado com sucesso!", "Sucesso");
    }

    private void SalvarAtividade()
    {
        Atividade atividade = new Atividade();
        atividade = Atividade.ConsultarPorId(ddlAtividade.SelectedValue.ToInt32());
        if (atividade == null)
            atividade = new Atividade();

        atividade.Nome = tbxAtividade.Text;
        atividade = atividade.Salvar();
        ddlAtividade.Items.Add(new ListItem(atividade.Nome, atividade.Id.ToString()));
        msg.CriarMensagem("Atividade salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    #endregion

    #region ________Eventos__________

    protected void btnAdicionarAtividade_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlAtividade.SelectedValue = "0";
            tbxAtividade.Text = "";
            ModalPopAtividade.Show();
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

    protected void btnEditarCentroCusto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Atividade atv = Atividade.ConsultarPorId(ddlAtividade.SelectedValue.ToInt32());
            if (atv != null)
            {
                tbxAtividade.Text = atv.Nome;
                ModalPopAtividade.Show();
            }
            else
            {
                msg.CriarMensagem("Selecione uma Atividade na lista ao lado.", "Alerta", MsgIcons.Alerta);
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

    protected void ibtnExcluirCentroCusto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlAtividade.SelectedIndex <= 0)
            {
                msg.CriarMensagem("Selecione primeiro a Atividade.", "Atenção", MsgIcons.Alerta);
                return;
            }
            Atividade atv = Atividade.ConsultarPorId(ddlAtividade.SelectedValue.ToInt32());
            if (atv != null && atv.Clientes != null && atv.Fornecedores.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir esta Atividade, pois existem Empresas associadas a ela.", "Atenção", MsgIcons.Alerta);
                return;
            }

            atv.Excluir();
            transacao.Recarregar(ref msg);
            msg.CriarMensagem("Atividade excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
            this.CarregarAtividades();
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

    protected void ibtnExcluirAtividade_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((ImageButton)sender, "Deseja realmente excluír esta Atividade?");
    }

    protected void btnSavarAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPAtividade);
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstado.SelectedValue.ToInt32());
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        hfId.Value = "";
        Response.Redirect("CadastrarFornecedores.aspx", false);
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            Fornecedor fornecedor = Fornecedor.ConsultarPorId(hfId.Value.ToInt32());

            if (fornecedor == null) 
            {
                msg.CriarMensagem("Salve primeiro o fornecedor para poder excluí-lo", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (fornecedor.ContratosDiversos != null && fornecedor.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível escluir este fornecedor, pois existem contratos associados a ele", "Alerta", MsgIcons.Alerta);
                return;
            }
            else
            {
                fornecedor.Excluir();
                hfId.Value = "";
                Response.Redirect("CadastrarFornecedores.aspx", false);
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

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((Button)sender, this.UsuarioEditorContratos);

        Button ibtn = (Button)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este Fornecedor serão perdidos. Deseja excluir mesmo assim?");
    }

    protected void btnSavarAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarAtividade();
            ModalPopAtividade.Hide();
            transacao.Recarregar(ref msg);
            this.CarregarAtividades();
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