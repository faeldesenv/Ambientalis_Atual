using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Clientes_CadastroClientes : PageBase
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
                    this.CarregarCliente(hfId.Value.ToInt32());
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

    #region __________Métodos___________

    private void CarregarAtividades()
    {
        ddlAtividade.DataSource = Atividade.ConsultarTodos();
        ddlAtividade.DataBind();
        ddlAtividade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarCliente(int p)
    {
        Cliente cliente = Cliente.ConsultarPorId(p);
        tbxNome.Text = cliente.Nome;
        chkAtivo.Checked = cliente.Ativo;
        ddlAtividade.SelectedValue = cliente.Atividade.Id.ToString();
        if (cliente.DadosPessoa.GetType() == typeof(DadosFisica))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
            rblSexo.SelectedIndex = ((DadosFisica)cliente.DadosPessoa).Sexo.Equals('M') ? 0 : 1;
            tbxDataNascimento.Text = ((DadosFisica)cliente.DadosPessoa).DataNascimento.EmptyToMinValue();
            tbxCPF.Text = ((DadosFisica)cliente.DadosPessoa).Cpf;
            hfCnpjCpf.Value = ((DadosFisica)cliente.DadosPessoa).Cpf;
            tbxRG.Text = ((DadosFisica)cliente.DadosPessoa).Rg;

            switch (((DadosFisica)cliente.DadosPessoa).EstadoCivil)
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
            tbxCNPJ.Text = ((DadosJuridica)cliente.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridica)cliente.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridica)cliente.DadosPessoa).RazaoSocial;
        }
        chkIsentoICMS.Checked = cliente.DadosPessoa.IsentoICMS;
        tbxInscricaoEstadual.Text = cliente.DadosPessoa.InscricaoEstadual;
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;
        tbxResponsavelTecnico.Text = cliente.Responsavel;

        tbxSite.Text = cliente.Site;
        tbxEmails.Text = cliente.Contato != null ? cliente.Contato.Email : "";
        tbxTelefone.Text = cliente.Contato != null ? cliente.Contato.Telefone : "";
        tbxCelular.Text = cliente.Contato != null ? cliente.Contato.Celular : "";
        tbxFax.Text = cliente.Contato != null ? cliente.Contato.Fax : "";
        tbxRamal.Text = cliente.Contato != null ? cliente.Contato.Ramal.ToString() : "";
        tbxLogradouro.Text = cliente.Endereco != null ? cliente.Endereco.Rua : "";
        tbxNumero.Text = cliente.Endereco != null ? cliente.Endereco.Numero : "";
        tbxComplemento.Text = cliente.Endereco != null ? cliente.Endereco.Complemento : "";
        tbxBairro.Text = cliente.Endereco != null ? cliente.Endereco.Bairro : "";
        tbxCEP.Text = cliente.Endereco != null ? cliente.Endereco.Cep : "";
        this.CarregarEstados();
        ddlEstado.SelectedValue = cliente.Endereco.Cidade != null && cliente.Endereco.Cidade.Estado != null ? cliente.Endereco.Cidade.Estado.Id.ToString() : "0";
        if (ddlEstado.SelectedValue.ToInt32() > 0)
        {
            this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
            ddlCidade.SelectedValue = cliente.Endereco.Cidade != null ? cliente.Endereco.Cidade.Id.ToString() : "0";
        }
        tbxObservacoes.Text = cliente.Observacoes;
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
        Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());
        if (cliente == null)
            cliente = new Cliente();

        if (rbtnPessoaJuridica.Checked && Cliente.ExisteCNPJ(cliente, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe um cliente com esse CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (ddlAtividade.SelectedIndex <= 0)
        {
            msg.CriarMensagem("Selecione uma atividade.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (rbtnPessoaFisica.Checked && Cliente.ExisteCPF(cliente, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe um cliente com esse CPF cadastrado.", "Alerta", MsgIcons.Alerta);
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

        cliente.Nome = tbxNome.Text;
        cliente.Atividade = Atividade.ConsultarPorId(ddlAtividade.SelectedValue.ToInt32());

        DadosFisica df = new DadosFisica();
        DadosJuridica dj = new DadosJuridica();

        if (rbtnPessoaFisica.Checked)
        {

            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (cliente.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    df = df.Salvar();
                    Cliente auxiliar = new Cliente();
                    auxiliar = (Cliente)cliente.Clone();
                    auxiliar.DadosPessoa = df;

                    if (Cliente.ExisteCPF(auxiliar, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma pessoa com o CPF cadastrado.", "Alerta", MsgIcons.Alerta);
                        df.Excluir();
                        return;
                    }
                    else
                    {
                        DadosJuridica aux = DadosJuridica.ConsultarPorId(cliente.DadosPessoa.Id);
                        aux.Excluir();
                        cliente.DadosPessoa = df;
                        tbxCNPJ.Text = "";
                        tbxRazaoSocial.Text = "";
                    }
                }
                else
                {
                    df = (DadosFisica)cliente.DadosPessoa;
                }
            }

            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            hfCnpjCpf.Value = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? SqlDate.MinValue : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            cliente.DadosPessoa = df;
        }
        else
        {
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (cliente.DadosPessoa.GetType() == typeof(DadosFisica))
                {
                    dj = dj.Salvar();
                    Cliente auxiliar = new Cliente();
                    auxiliar = (Cliente)cliente.Clone();
                    auxiliar.DadosPessoa = dj;

                    if (Cliente.ExisteCNPJ(auxiliar, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma empresa com o CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
                        dj.Excluir();
                        return;
                    }
                    else
                    {
                        DadosFisica aux = DadosFisica.ConsultarPorId(cliente.DadosPessoa.Id);
                        aux.Excluir();
                        cliente.DadosPessoa = dj;
                        tbxCPF.Text = "";
                        tbxDataNascimento.Text = "";
                        tbxRG.Text = "";
                    }
                }
                else
                {
                    dj = (DadosJuridica)cliente.DadosPessoa;
                }
            }
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            hfCnpjCpf.Value = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            cliente.DadosPessoa = dj;
        }

        cliente.Ativo = chkAtivo.Checked;
        cliente.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        cliente.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;
        cliente.Responsavel = tbxResponsavelTecnico.Text;
        cliente.Site = tbxSite.Text;

        Contato contato = cliente.Contato != null ? cliente.Contato : new Contato();
        contato.Email = tbxEmails.Text.Trim().Replace(";", " ;").Replace("  ", " "); ;
        contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        cliente.Contato = contato;

        Endereco endereco = cliente.Endereco != null ? cliente.Endereco : new Endereco();
        endereco.Rua = tbxLogradouro.Text;
        endereco.Numero = tbxNumero.Text;
        endereco.Complemento = tbxComplemento.Text;
        endereco.Bairro = tbxBairro.Text;
        endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));
        cliente.Endereco = endereco;

        cliente.Observacoes = tbxObservacoes.Text;
        if (rbtnPessoaFisica.Checked)
            cliente.DadosPessoa = df.Salvar();
        else
            cliente.DadosPessoa = dj.Salvar();
        cliente = cliente.Salvar();
        hfId.Value = cliente.Id.ToString();
        msg.CriarMensagem("Cliente cadastrado com sucesso!", "Sucesso");
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

    #region __________Eventos___________

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
        Response.Redirect("CadastroClientes.aspx", false);
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
            Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());
            if (cliente == null) 
            {
                msg.CriarMensagem("Salve primeiro o cliente para poder excluí-lo", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (cliente.ContratosDiversos != null && cliente.ContratosDiversos.Count > 0)
            {
                msg.CriarMensagem("Não é possível escluir este cliente, pois existem contratos associados a ele", "Alerta", MsgIcons.Alerta);
                return;
            }
            else
            {
                cliente.Excluir();
                hfId.Value = "";
                Response.Redirect("CadastroClientes.aspx", false);
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
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a este Cliente serão perdidos. Deseja excluir mesmo assim?");
    }

    #endregion

}