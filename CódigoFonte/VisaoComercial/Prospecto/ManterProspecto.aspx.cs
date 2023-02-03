using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Persistencia.Filtros;
using Utilitarios.Criptografia;
using Modelo;
public partial class Prospecto_ManterProspecto : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                tbxDataCadastro.Text = DateTime.Now.ToShortDateString();
                hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
                this.CarregarEstados();
                this.CarregarRevendas();
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                    this.CarregarCampos(Convert.ToInt32("" + hfId.Value));
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

    #region ___________Metodos_____________

    private void CarregarRevendas()
    {
        ddlRevenda.DataValueField = "Id";
        ddlRevenda.DataTextField = "Nome";
        ddlRevenda.DataSource = Revenda.ConsultarTodos();
        ddlRevenda.DataBind();
        ddlRevenda.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarCampos(int p)
    {
        Prospecto prospecto = Prospecto.ConsultarPorId(p);
        tbxDataCadastro.Text = prospecto.DataCadastro.ToShortDateString();
        ddlRevenda.SelectedValue = prospecto.Revenda.Id.ToString();
        chkAtivo.Checked = prospecto.Ativo;
        
        if (prospecto.DadosPessoa.GetType() == typeof(DadosFisicaComercial))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;            
            tbxCPF.Text = ((DadosFisicaComercial)prospecto.DadosPessoa).Cpf;
            hfCnpjCpf.Value = ((DadosFisicaComercial)prospecto.DadosPessoa).Cpf;
            tbxNome.Text = prospecto.Nome;
        }
        else
        {
            rbtnPessoaFisica.Checked = false;
            rbtnPessoaJuridica.Checked = true;
            tbxCNPJ.Text = ((DadosJuridicaComercial)prospecto.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridicaComercial)prospecto.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridicaComercial)prospecto.DadosPessoa).RazaoSocial;
            tbxNomeFantasia.Text = prospecto.Nome;
        }
        
        tbxResponsavel.Text = prospecto.Responsavel;        
        tbxEmails.Text = prospecto.Contato != null ? prospecto.Contato.Email : "";
        tbxTelefone.Text = prospecto.Contato != null ? prospecto.Contato.Telefone : "";
        tbxCelular.Text = prospecto.Contato != null ? prospecto.Contato.Celular : "";
        tbxFax.Text = prospecto.Contato != null ? prospecto.Contato.Fax : "";
        tbxRamal.Text = prospecto.Contato != null ? prospecto.Contato.Ramal.ToString() : "";
        tbxLogradouro.Text = prospecto.Endereco != null ? prospecto.Endereco.Rua : "";
        tbxNumero.Text = prospecto.Endereco != null ? prospecto.Endereco.Numero : "";
        tbxComplemento.Text = prospecto.Endereco != null ? prospecto.Endereco.Complemento : "";
        tbxBairro.Text = prospecto.Endereco != null ? prospecto.Endereco.Bairro : "";
        tbxCEP.Text = prospecto.Endereco != null ? prospecto.Endereco.Cep : "";
        ddlEstado.SelectedValue = prospecto.Endereco.Cidade != null && prospecto.Endereco.Cidade.Estado != null ? prospecto.Endereco.Cidade.Estado.Id.ToString() : "0";
        this.CarregarCidades();
        ddlCidade.SelectedValue = prospecto.Endereco.Cidade != null ? prospecto.Endereco.Cidade.Id.ToString() : "0";
        tbxObservacoes.Text = prospecto.Observacoes;

        dgrinteracoes.DataSource = prospecto.Interacoes;
        dgrinteracoes.DataBind();
        lblNomeProspecto.Text = "Cliente: " + prospecto.Nome;

    }

    private void CarregarInteracoes()
    {
        dgrinteracoes.DataSource = Prospecto.ConsultarPorId(hfId.Value.ToInt32()).Interacoes;
        dgrinteracoes.DataBind();
    }

    private void SalvarInteracao()
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("É necessario salvar primeiro a Indicação de Cliente!", "Atenção", MsgIcons.Alerta);
            return;
        }

        Interacao interacao = Interacao.ConsultarPorId(hfIdInteracao.Value.ToInt32());
        if (interacao == null)
            interacao = new Interacao();

        interacao.Prospecto = Prospecto.ConsultarPorId(hfId.Value.ToInt32());

        interacao.Data = tbxData.Text.ToDateTime();
        interacao.Tipo = ddlTipo.SelectedValue;
        interacao.Status = ddlStatus.SelectedValue;
        interacao.NomePessoa = tbxNomePessoa.Text;
        interacao.CargoPessoa = tbxCargoPessoa.Text;
        interacao.Descricao = tbxDescricao.Text;
        interacao.Salvar();
        msg.CriarMensagem("Interação salva com Sucesso!", "Sucesso");

    }

    private void CarregarEstados()
    {
        IList<Estado> est = Estado.ConsultarTodos();
        ddlEstado.DataSource = est;
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, "-- Selecione --");
    }

    private void CarregarCidades()
    {
        if (ddlEstado.SelectedIndex == 0)
            return;
        Estado est = Estado.ConsultarPorId(ddlEstado.SelectedValue.ToInt32());
        ddlCidade.DataSource = est.Cidades;
        ddlCidade.DataBind();
        ddlCidade.Items.Insert(0, "-- Selecione --");
    }

    private void ExcluirProspecto()
    {
        Prospecto prospecto = Prospecto.ConsultarPorId(hfId.Value.ToInt32());
        if (prospecto == null)
        {
            msg.CriarMensagem("Não é possível excluir esta Indicação de Clientes", "Atenção");
            return;
        }
        if (prospecto.Excluir())
            Response.Redirect("ManterProspecto.aspx", false);
    }

    private void Salvar()
    {
        if (ddlRevenda.SelectedIndex == 0)
        {
            msg.CriarMensagem("Informe a revenda para prosseguir", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (rbtnPessoaJuridica.Checked && !tbxNomeFantasia.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Informe o nome fantasia para prosseguir", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (rbtnPessoaFisica.Checked && !tbxNome.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Informe o nome para prosseguir", "Alerta", MsgIcons.Alerta);
            return;
        }

        Prospecto prospecto = Prospecto.ConsultarPorId(hfId.Value.ToInt32());
        if (prospecto == null)
        {
            prospecto = new Prospecto();
            prospecto.DataCadastro = DateTime.Now;
        }

        #region ___________ Consultas para verificar existencia de outros prospectos ____________       
        //IMPORTANTE: O EMP ESTA SENDO ALTERADO POIS DEVE-SE CONSULTAR EM TODAS AS BASES. ISSO DEVE SER FEITO SOMENTE PARA ESSAS DUAS CONSULTAS
        object emp = Session["Emp"];
        Session["Emp"] = null;
        if (rbtnPessoaJuridica.Checked && Prospecto.ExisteCNPJ(prospecto, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe uma Indicação de Cliente com esse CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
            Session["Emp"] = emp;
            return;
        }
        if (rbtnPessoaFisica.Checked && Prospecto.ExisteCPF(prospecto, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe uma Indicação de Cliente com esse CPF cadastrado.", "Alerta", MsgIcons.Alerta);
            Session["Emp"] = emp;
            return;
        }
        Session["Emp"] = emp;
        #endregion

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
        
        prospecto.Revenda = Revenda.ConsultarPorId(ddlRevenda.SelectedValue.ToInt32());
        prospecto.Ativo = chkAtivo.Checked;

        DadosFisicaComercial df = new DadosFisicaComercial();
        DadosJuridicaComercial dj = new DadosJuridicaComercial();

        if (rbtnPessoaFisica.Checked)
        {
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (prospecto.DadosPessoa.GetType() == typeof(DadosJuridicaComercial))
                {
                    df = df.Salvar();
                    Prospecto auxiliar = new Prospecto();
                    auxiliar = (Prospecto)prospecto.Clone();
                    auxiliar.DadosPessoa = df;

                    if (Prospecto.ExisteCPF(auxiliar, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Já existe uma Indicação de Cliente com este CPF cadastrado.", "Alerta", MsgIcons.Alerta);
                        df.Excluir();
                        return;
                    }
                    else
                    {
                        DadosJuridicaComercial aux = DadosJuridicaComercial.ConsultarPorId(prospecto.DadosPessoa.Id);
                        aux.Excluir();
                        prospecto.DadosPessoa = df;
                        tbxCNPJ.Text = "";
                        tbxRazaoSocial.Text = "";
                    }
                }
                else
                {
                    df = (DadosFisicaComercial)prospecto.DadosPessoa;
                }
            }

            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");            
            df = df.Salvar();
            prospecto.DadosPessoa = df;
            prospecto.Nome = tbxNome.Text;
        }
        else
        {
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (prospecto.DadosPessoa.GetType() == typeof(DadosFisicaComercial))
                {
                    dj = dj.Salvar();
                    Prospecto auxiliar = new Prospecto();
                    auxiliar = (Prospecto)prospecto.Clone();
                    auxiliar.DadosPessoa = dj;

                    if (Prospecto.ExisteCNPJ(auxiliar, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Já existe uma Indicação de Cliente com este CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
                        dj.Excluir();
                        return;
                    }
                    else
                    {
                        DadosFisicaComercial aux = DadosFisicaComercial.ConsultarPorId(prospecto.DadosPessoa.Id);
                        aux.Excluir();
                        prospecto.DadosPessoa = dj;
                        tbxCPF.Text = "";                       
                    }
                }
                else
                {
                    dj = (DadosJuridicaComercial)prospecto.DadosPessoa;
                }
            }
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            prospecto.Nome = tbxNomeFantasia.Text;
            prospecto.DadosPessoa = dj;
        }

        prospecto.Responsavel = tbxResponsavel.Text;     

        ContatoComercial contato = prospecto.Contato != null ? prospecto.Contato : new ContatoComercial();
        contato.Email = tbxEmails.Text.Trim().Replace(";", " ;").Replace("  ", " "); ;
        contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        prospecto.Contato = contato;

        EnderecoComercial endereco = prospecto.Endereco != null ? prospecto.Endereco : new EnderecoComercial();
        endereco.Rua = tbxLogradouro.Text;
        endereco.Numero = tbxNumero.Text;
        endereco.Complemento = tbxComplemento.Text;
        endereco.Bairro = tbxBairro.Text;
        endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));
        prospecto.Endereco = endereco;

        prospecto.Observacoes = tbxObservacoes.Text;
        if (rbtnPessoaFisica.Checked)
            prospecto.DadosPessoa = df.Salvar();
        else
            prospecto.DadosPessoa = dj.Salvar();
        prospecto.Emp = prospecto.Revenda.Emp;        
        prospecto = prospecto.Salvar(); 

        hfId.Value = prospecto.Id.ToString();
        msg.CriarMensagem("Indicação de Cliente salvo com sucesso!", "Sucesso");
    }

    #endregion

    #region ___________Bindings____________

    public String bindingPessoaCargo(Object o)
    {
        Interacao i = (Interacao)o;
        return i.NomePessoa + " (" + i.CargoPessoa + ")";
    }

    #endregion

    #region ___________Eventos_____________
    
    protected void ibtnExcluir_PreRender(object sender, EventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;
        if (ibtn.Enabled == true)
            WebUtil.AdicionarConfirmacao(ibtn, "Todos os dados referentes a esta(s) Interações serão perdido(s). Deseja excluir mesmo assim?");
    }

    protected void ibtnAddInteracao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("É necessario salvar primeiro a Indicação de Cliente!", "Atenção", MsgIcons.Alerta);
            }
            else
            {
                tbxProspectoInteracao.Text = Prospecto.ConsultarPorId(hfId.Value.ToInt32()).Nome;
                tbxData.Text = "";
                ddlTipo.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
                tbxNomePessoa.Text = "";
                tbxCargoPessoa.Text = "";
                tbxDescricao.Text = "";
                hfIdInteracao.Value = "0";
                btnPopUpinteracao_popupextender.Show();
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

    protected void btnSalvarInteracao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarInteracao();
            transacao.Recarregar(ref msg);
            this.CarregarInteracoes();
            
            if (ddlStatus.SelectedItem.Text == "Adiada") 
            {
                this.RecarregarCampos();                
            } 
            else
                btnPopUpinteracao_popupextender.Hide();
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

    private void RecarregarCampos()
    {
        tbxProspectoInteracao.Text = Prospecto.ConsultarPorId(hfId.Value.ToInt32()).Nome;
        tbxData.Text = "";
        ddlTipo.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        tbxNomePessoa.Text = "";
        tbxCargoPessoa.Text = "";
        tbxDescricao.Text = "";
        hfIdInteracao.Value = "0";
        msg.CriarMensagem("Você salvou a interação como \"Adiada\", crie uma nova interação e agende sua data.", "Sucesso");
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades();
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
        Response.Redirect("ManterProspecto.aspx");
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
            this.ExcluirProspecto();
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

    protected void dgrinteracoes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Interacao inter = Interacao.ConsultarPorId(e.Item.Cells[0].Text.ToInt32());
            tbxData.Text = inter.Data.ToShortDateString();
            ddlTipo.SelectedValue = inter.Tipo;
            ddlStatus.SelectedValue = inter.Status;
            tbxNomePessoa.Text = inter.NomePessoa;
            tbxCargoPessoa.Text = inter.CargoPessoa;
            tbxDescricao.Text = inter.Descricao;
            hfIdInteracao.Value = inter.Id.ToString();
            btnPopUpinteracao_popupextender.Show();
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

    protected void dgrinteracoes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            foreach (DataGridItem item in dgrinteracoes.Items)
                if (((CheckBox)item.FindControl("ckbExcluir")).Checked)
                {
                    Interacao f = Interacao.ConsultarPorId(item.Cells[0].Text.ToInt32());
                    f.Excluir();
                }

            transacao.Recarregar(ref msg);
            this.CarregarInteracoes();
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

    protected void TabContainer4_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer4.ActiveTabIndex == 1 && hfId.Value.ToInt32() > 0)
            {
                lblNomeProspecto.Text = "Cliente: " + Prospecto.ConsultarPorId(hfId.Value.ToInt32()).Nome;
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

    #endregion

    #region __________Pre-Render___________
    #endregion

    #region __________ Triggers ___________

    protected void ibtnAddInteracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upInteracoes);
    }

    protected void btnSalvarInteracao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upInteracao);
    }

    protected void dgrinteracoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "EditCommand", upInteracoes);
    }

    #endregion

}