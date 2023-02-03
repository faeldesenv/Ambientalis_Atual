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
            hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
            if (!IsPostBack)
            {
                this.CarregarEstados();
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                    this.CarregarConsultora(Convert.ToInt32("" + hfId.Value));

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
        btnSalvar.Enabled = btnSalvar.Visible = btnNovo.Enabled = btnNovo.Visible = btnExcluir.Enabled = btnExcluir.Visible = false;
    }

    #region ________Metodos__________

    private void CarregarConsultora(int id)
    {
        Consultora c = Consultora.ConsultarPorId(id);

        tbxNome.Text = c.Nome;
        tbxResponsavel.Text = c.Responsavel;
        tbxSite.Text = c.Site;
        tbxEmail.Text = c.Contato.Email;
        tbxTelefone.Text = c.Contato.Telefone;
        tbxCelular.Text = c.Contato.Celular;
        tbxRamal.Text = c.Contato.Ramal.ToString();
        tbxFax.Text = c.Contato.Fax;
        tbxNacionalidade.Text = c.Nacionalidade;
        chkAtivo.Checked = c.Ativo;

        if (c.DadosPessoa.GetType() == typeof(DadosFisica))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
            rblSexo.SelectedIndex = ((DadosFisica)c.DadosPessoa).Sexo.Equals('M') ? 0 : 1;
            tbxDataNascimento.Text = ((DadosFisica)c.DadosPessoa).DataNascimento.EmptyToMinValue();
            tbxCPF.Text = ((DadosFisica)c.DadosPessoa).Cpf;
            tbxRG.Text = ((DadosFisica)c.DadosPessoa).Rg;
            switch (((DadosFisica)c.DadosPessoa).EstadoCivil)
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
            rbtnPessoaJuridica.Checked = true;
            rbtnPessoaFisica.Checked = false;
            tbxCNPJ.Text = ((DadosJuridica)c.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridica)c.DadosPessoa).RazaoSocial;
        }

        tbxLogradouro.Text = c.Endereco.Rua;
        tbxNumero.Text = c.Endereco.Numero;
        tbxComplemento.Text = c.Endereco.Complemento;
        tbxBairro.Text = c.Endereco.Bairro;
        tbxCEP.Text = c.Endereco.Cep;
        tbxPontoReferencia.Text = c.Endereco.PontoReferencia;
        ddlEstado.SelectedValue = c.Endereco.Cidade != null && c.Endereco.Cidade.Estado != null ? c.Endereco.Cidade.Estado.Id.ToString() : "0";
        this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
        ddlCidade.SelectedValue = c.Endereco.Cidade != null ? c.Endereco.Cidade.Id.ToString() : "0";

        chkIsentoICMS.Checked = c.DadosPessoa.IsentoICMS;
        tbxInscricaoEstadual.Text = c.DadosPessoa.InscricaoEstadual;
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;

        tbxObservacoes.Text = c.Observacoes;

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
        if (Convert.ToInt32(hfId.Value) > 0)
        {
            //fazer verificações antes da exclusao.
            Consultora f = Consultora.ConsultarPorId(Convert.ToInt32(hfId.Value));
            f.Excluir();
            this.Novo();
        }
        else
            msg.CriarMensagem("Nao há consultora salva para ser excluido!", "Alerta");
    }

    private void Novo()
    {
        hfId.Value = "0";
        Response.Redirect("ManterConsultora.aspx", false);
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

        Consultora c = new Consultora();
        if (Convert.ToInt32("0" + hfId.Value) > 0)
            c = Consultora.ConsultarPorId(Convert.ToInt32("0" + hfId.Value));
        c.Nome = tbxNome.Text.Trim();
        c.Responsavel = tbxResponsavel.Text.Trim();
        c.Site = tbxSite.Text.Trim();
        c.Contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        c.Contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        c.Contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        c.Contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        c.Nacionalidade = tbxNacionalidade.Text.Trim();
        c.Contato.Email = tbxEmail.Text.Trim().Replace(";", " ;").Replace("  ", " "); ;
        c.Ativo = chkAtivo.Checked;

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

        if (rbtnPessoaFisica.Checked)
        {
            DadosFisica df = new DadosFisica();
            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? SqlDate.MinValue : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            c.DadosPessoa = df;
        }
        else
        {
            DadosJuridica dj = new DadosJuridica();
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            dj = dj.Salvar();
            c.DadosPessoa = dj;
        }

        c.Endereco.Bairro = tbxBairro.Text;
        c.Endereco.Numero = tbxNumero.Text;
        c.Endereco.Complemento = tbxComplemento.Text;
        c.Endereco.Rua = tbxLogradouro.Text;
        c.Endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        c.Endereco.PontoReferencia = tbxPontoReferencia.Text;
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            c.Endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));

        c.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        c.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;

        c.Observacoes = tbxObservacoes.Text;

        c = c.Salvar();
        hfId.Value = c.Id.ToString();
        msg.CriarMensagem("Consultora cadastrada com sucesso!", "Sucesso");
    }

    private void CarregarCidades(int p)
    {
        if (p <= 0)
        {
            ddlCidade.Items.Clear();
            return;
        }
        Estado estado = Estado.ConsultarPorId(p);
        ddlCidade.DataValueField = "Id";
        ddlCidade.DataTextField = "Nome";
        ddlCidade.DataSource = estado.Cidades;
        ddlCidade.DataBind();
    }

    #endregion

    #region ________Eventos___________

    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes aos Clientes selecionados serão perdido(s). Deseja excluir assim mesmo?");
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        this.Novo();
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            this.Excluir();
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

    protected void PermissaoUsuario_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((Button)sender, this.UsuarioEditorModuloGeral);
    }
}