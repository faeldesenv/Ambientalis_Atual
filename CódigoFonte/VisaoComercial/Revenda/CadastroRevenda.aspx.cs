using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Revenda_Cadastro : PageBase
{
    Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UsuarioComercial user = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"];
                if (user != null && user.GetType() == typeof(UsuarioRevendaComercial))
                {
                    hfId.Value = ((UsuarioRevendaComercial)user).Revenda.Id.ToString();
                    this.CarregarRevenda(Convert.ToInt32("" + hfId.Value));
                    this.DesabilitarCadastroRevenda();
                }
                else
                {
                    hfId.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);

                    this.CarregarEstados();
                    if (Convert.ToInt32("0" + hfId.Value) > 0)
                        this.CarregarRevenda(Convert.ToInt32("" + hfId.Value));
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

    #region ___________Métodos___________

    private void CarregarEstados()
    {
        IList<Estado> estados = Estado.ConsultarTodosOrdemAlfabetica();
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataSource = estados;
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void DesabilitarCadastroRevenda()
    {
        btnNovo.Enabled = btnNovo.Visible = btnExcluir.Enabled = btnExcluir.Visible = btnCriarContrato.Enabled = btnCriarContrato.Visible = btnCriarNovoContrato.Enabled = btnCriarNovoContrato.Visible =
            ddlTipoParceiro.Enabled = rbtnPessoaFisica.Enabled = rbtnPessoaJuridica.Enabled = tbxRazaoSocial.Enabled = tbxCNPJ.Enabled = tbxCPF.Enabled = tbxRepresentanteEmpresa.Enabled =
            tbxCPFRepresentanteLegal.Enabled = chkContratoAtivo.Enabled = false;
    }

    private void CarregarCidades(int p)
    {
        Estado estado = Estado.ConsultarPorId(p);
        ddlCidade.DataValueField = "Id";
        ddlCidade.DataTextField = "Nome";
        ddlCidade.DataSource = estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        ddlCidade.DataBind();
        ddlCidade.Items.Insert(0, new ListItem("--Selecione --", "0"));
    }

    private void CarregarRevenda(int p)
    {
        Revenda revenda = Revenda.ConsultarPorId(p);
        ddlTipoParceiro.SelectedIndex = revenda.TipoParceiro != null && revenda.TipoParceiro != "" ? revenda.TipoParceiro == "Revenda / Agente de Negócios" ? 1 : 2 : 0; //1 pra revenda, 2 pra consultoria e 0 pra todos
        chkContratoAtivo.Checked = revenda.Ativo && revenda.GetUltimoContrato != null && revenda.GetUltimoContrato.Aceito && !revenda.GetUltimoContrato.Desativado;
        tbxNome.Text = revenda.Nome;
        if (revenda.DadosPessoa.GetType() == typeof(DadosFisicaComercial))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
            rblSexo.SelectedIndex = ((DadosFisicaComercial)revenda.DadosPessoa).Sexo.Equals('M') ? 0 : 1;
            tbxDataNascimento.Text = ((DadosFisicaComercial)revenda.DadosPessoa).DataNascimento.EmptyToMinValue();
            tbxCPF.Text = ((DadosFisicaComercial)revenda.DadosPessoa).Cpf;
            hfCnpjCpf.Value = ((DadosFisicaComercial)revenda.DadosPessoa).Cpf;
            tbxRG.Text = ((DadosFisicaComercial)revenda.DadosPessoa).Rg;

            switch (((DadosFisicaComercial)revenda.DadosPessoa).EstadoCivil)
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
            tbxCNPJ.Text = ((DadosJuridicaComercial)revenda.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridicaComercial)revenda.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridicaComercial)revenda.DadosPessoa).RazaoSocial;
            chkIsentoICMS.Checked = revenda.DadosPessoa.IsentoICMS;
            tbxInscricaoEstadual.Text = revenda.DadosPessoa.InscricaoEstadual;
            tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;
        }

        tbxRepresentanteEmpresa.Text = revenda.RepresentanteLegal;
        tbxCPFRepresentanteLegal.Text = revenda.CpfRepresentanteLegal;
        tbxNacionalidadePresentanteLegal.Text = revenda.NacionalidadeRepresentanteLegal;
        tbxResponsavelTecnico.Text = revenda.Responsavel;
        tbxGestorEconomico.Text = revenda.GestorEconomico;
        tbxSite.Text = revenda.Site;
        tbxEmails.Text = revenda.Contato != null ? revenda.Contato.Email : "";
        tbxTelefone.Text = revenda.Contato != null ? revenda.Contato.Telefone : "";
        tbxCelular.Text = revenda.Contato != null ? revenda.Contato.Celular : "";
        tbxFax.Text = revenda.Contato != null ? revenda.Contato.Fax : "";
        tbxRamal.Text = revenda.Contato != null ? revenda.Contato.Ramal.ToString() : "";
        tbxLogradouro.Text = revenda.Endereco != null ? revenda.Endereco.Rua : "";
        tbxNumero.Text = revenda.Endereco != null ? revenda.Endereco.Numero : "";
        tbxComplemento.Text = revenda.Endereco != null ? revenda.Endereco.Complemento : "";
        tbxBairro.Text = revenda.Endereco != null ? revenda.Endereco.Bairro : "";
        tbxCEP.Text = revenda.Endereco != null ? revenda.Endereco.Cep : "";
        this.CarregarEstados();
        ddlEstado.SelectedValue = revenda.Endereco.Cidade != null && revenda.Endereco.Cidade.Estado != null ? revenda.Endereco.Cidade.Estado.Id.ToString() : "0";
        if (ddlEstado.SelectedValue.ToInt32() > 0)
        {
            this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
            ddlCidade.SelectedValue = revenda.Endereco.Cidade != null ? revenda.Endereco.Cidade.Id.ToString() : "0";
        }
        tbxObservacoes.Text = revenda.Observacoes;
        if (revenda.GetUltimoContrato != null && revenda.GetUltimoContrato.Desativado)
        {
            lblSituaçãoRevenda.Text = "INATIVA";
            lblContrato.Text = "DESATIVADO";
            chkContratoAtivo.Checked = false;
            contrato_naocriado.Visible = false;
            visualizar_contrato_criado.Visible = false;
            criar_novo_contrato.Visible = false;

        } else 
            if (revenda.Contratos != null && revenda.Contratos.Count > 0)
            {
                if (revenda.GetUltimoContrato.Aceito && revenda.Ativo == true)
                {
                    lblSituaçãoRevenda.Text = "ATIVA";
                    lblContrato.Text = "ACEITO";
                    contrato_naocriado.Visible = false;
                    criar_novo_contrato.Visible = false;
                    visualizar_contrato_criado.Visible = true;
                }
                else
                {
                    lblSituaçãoRevenda.Text = "INATIVA";
                    lblContrato.Text = "CRIADO";
                    visualizar_contrato_criado.Visible = true;
                    criar_novo_contrato.Visible = true;
                    contrato_naocriado.Visible = false;
                }
            }
            else
            {
                lblSituaçãoRevenda.Text = "INATIVA";
                lblContrato.Text = "NÂO CRIADO";
                contrato_naocriado.Visible = true;
                visualizar_contrato_criado.Visible = false;
                criar_novo_contrato.Visible = false;
            }
        
    }

    private void Salvar()
    {
        if (ddlTipoParceiro.SelectedIndex == 0)
        {
            msg.CriarMensagem("Selecione um tipo de parceria para prosseguir.", "Alerta", MsgIcons.Alerta);
            return;
        }

        Revenda revenda = Revenda.ConsultarPorId(hfId.Value.ToInt32());

        if (revenda != null && revenda.Id > 0 && revenda.GetUltimoContrato != null && revenda.GetUltimoContrato.Aceito) 
        {
            
            revenda.GetUltimoContrato.ConsultarPorId();
            revenda.GetUltimoContrato.Desativado = !chkContratoAtivo.Checked;
            revenda.GetUltimoContrato.Salvar();
        }
        else if (chkContratoAtivo.Checked) 
        {
            msg.CriarMensagem("Não épossível ativar ou desativar um contrato que ainda não foi criado ou não foi aceito.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (revenda == null)
            revenda = new Revenda();

        revenda.TipoParceiro = ddlTipoParceiro.SelectedItem.Text;

        if (rbtnPessoaJuridica.Checked && Revenda.ExisteCNPJ(revenda, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe uma revenda com esse CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
            return;
        }

        if (rbtnPessoaFisica.Checked && Revenda.ExisteCPF(revenda, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe uma revenda com esse CPF cadastrado.", "Alerta", MsgIcons.Alerta);
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

        revenda.Nome = tbxNome.Text;

        DadosFisicaComercial df = new DadosFisicaComercial();
        DadosJuridicaComercial dj = new DadosJuridicaComercial();

        if (rbtnPessoaFisica.Checked)
        {

            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (revenda.DadosPessoa.GetType() == typeof(DadosJuridicaComercial))
                {
                    df = df.Salvar();
                    Revenda auxiliar = new Revenda();
                    auxiliar = (Revenda)revenda.Clone();
                    auxiliar.DadosPessoa = df;

                    if (Revenda.ExisteCPF(auxiliar, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma pessoa com o CPF cadastrado.", "Alerta", MsgIcons.Alerta);
                        df.Excluir();
                        return;
                    }
                    else
                    {
                        DadosJuridicaComercial aux = DadosJuridicaComercial.ConsultarPorId(revenda.DadosPessoa.Id);
                        aux.Excluir();
                        revenda.DadosPessoa = df;
                        tbxCNPJ.Text = "";
                        tbxRazaoSocial.Text = "";
                    }
                }
                else
                {
                    df = (DadosFisicaComercial)revenda.DadosPessoa;
                }
            }

            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            hfCnpjCpf.Value = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? DateTime.MinValue : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            revenda.DadosPessoa = df;
        }
        else
        {
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (revenda.DadosPessoa.GetType() == typeof(DadosFisicaComercial))
                {
                    dj = dj.Salvar();
                    Revenda auxiliar = new Revenda();
                    auxiliar = (Revenda)revenda.Clone();
                    auxiliar.DadosPessoa = dj;

                    if (Revenda.ExisteCNPJ(auxiliar, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma empresa com o CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
                        dj.Excluir();
                        return;
                    }
                    else
                    {
                        DadosFisicaComercial aux = DadosFisicaComercial.ConsultarPorId(revenda.DadosPessoa.Id);
                        aux.Excluir();
                        revenda.DadosPessoa = dj;
                        tbxCPF.Text = "";
                        tbxDataNascimento.Text = "";
                        tbxRG.Text = "";
                    }
                }
                else
                {
                    dj = (DadosJuridicaComercial)revenda.DadosPessoa;
                }
            }
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            hfCnpjCpf.Value = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            revenda.DadosPessoa = dj;
        }

        revenda.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        revenda.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;
        revenda.RepresentanteLegal = tbxRepresentanteEmpresa.Text;
        revenda.CpfRepresentanteLegal = tbxCPFRepresentanteLegal.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        revenda.NacionalidadeRepresentanteLegal = tbxNacionalidadePresentanteLegal.Text;
        revenda.Responsavel = tbxResponsavelTecnico.Text;
        revenda.GestorEconomico = tbxGestorEconomico.Text;
        revenda.Site = tbxSite.Text;

        ContatoComercial contato = revenda.Contato != null ? revenda.Contato : new ContatoComercial();
        contato.Email = tbxEmails.Text.Trim().Replace(";", " ;").Replace("  ", " "); ;
        contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        revenda.Contato = contato;

        EnderecoComercial endereco = revenda.Endereco != null ? revenda.Endereco : new EnderecoComercial();
        endereco.Rua = tbxLogradouro.Text;
        endereco.Numero = tbxNumero.Text;
        endereco.Complemento = tbxComplemento.Text;
        endereco.Bairro = tbxBairro.Text;
        endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));
        revenda.Endereco = endereco;

        revenda.Observacoes = tbxObservacoes.Text;
        if (rbtnPessoaFisica.Checked)
            revenda.DadosPessoa = df.Salvar();
        else
            revenda.DadosPessoa = dj.Salvar();
        revenda = revenda.Salvar();
        revenda.Emp = revenda.Id;
        revenda = revenda.Salvar();
        hfId.Value = revenda.Id.ToString();
        msg.CriarMensagem("Revenda cadastrada com sucesso!", "Sucesso");
        if (revenda.GetUltimoContrato != null && revenda.GetUltimoContrato.Desativado)
        {
            lblSituaçãoRevenda.Text = "INATIVA";
            lblContrato.Text = "DESATIVADO";
            contrato_naocriado.Visible = false;
            visualizar_contrato_criado.Visible = false;
            criar_novo_contrato.Visible = false;
        }
        else
        {
            if (revenda.Contratos != null && revenda.Contratos.Count > 0)
            {
                if (revenda.GetUltimoContrato.Aceito && revenda.Ativo)
                {
                    lblSituaçãoRevenda.Text = "ATIVA";
                    lblContrato.Text = "ACEITO";
                    contrato_naocriado.Visible = false;
                    criar_novo_contrato.Visible = false;
                    visualizar_contrato_criado.Visible = true;
                }
                else
                {
                    lblSituaçãoRevenda.Text = "INATIVA";
                    lblContrato.Text = "CRIADO";
                    visualizar_contrato_criado.Visible = true;
                    criar_novo_contrato.Visible = true;
                    contrato_naocriado.Visible = false;
                }
            }
            else
            {
                lblSituaçãoRevenda.Text = "INATIVA";
                lblContrato.Text = "NÂO CRIADO";
                contrato_naocriado.Visible = hfId.Value.ToInt32() > 0 ? true : false;
                visualizar_contrato_criado.Visible = false;
                criar_novo_contrato.Visible = false;
            }
        }

    }

    private void Excluir()
    {
        Revenda revenda = Revenda.ConsultarPorId(hfId.Value.ToInt32());
        if (revenda.Prospectos != null && revenda.Prospectos.Count > 0)
        {
            msg.CriarMensagem("Não é possível excluir essa revenda, pois existem prospectos associados a ela", "Alerta", MsgIcons.Alerta);
            return;
        }
        else
        {
            revenda.Excluir();
            msg.CriarMensagem("Revenda excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            hfId.Value = "";
            Response.Redirect("CadastroRevenda.aspx", false);
        }
    }

    private void AtualizarContrato()
    {
        if (ddlTipoParceiro.SelectedValue.ToInt32() == 1) //1 = Revenda
        {
            this.ExibirContratoRevenda();

        }
        else if (ddlTipoParceiro.SelectedValue.ToInt32() == 2)  //2 = Consultoria
        {
            this.ExibirContratoConsultora();
        }
    }

    private void ExibirContratoRevenda()
    {
        Revenda revenda = Revenda.ConsultarPorId(hfId.Value.ToInt32());
        NumeroPorcentagemPorExtenso numero = new NumeroPorcentagemPorExtenso();
        numero.SetNumero(Convert.ToDecimal(tbxComissaoContrato.Text));
        lblTextoContrato.Text = @"<div id='imprimir_contrato_original'>                                       
                                       <div class='paragrafo titulo' style='font-size: 11pt;'>" + (rbtnPessoaFisica.Checked ? "CONTRATO PARTICULAR DE PARCERIA COMERCIAL - AGENTE DE NEGÓCIOS DE SERVIÇO WEB" : "CONTRATO PARTICULAR DE PARCERIA E REPRESENTAÇÃO E REVENDA COMERCIAL DE SERVIÇO WEB") + @"
                                       <div>Nº " + (revenda != null && revenda.GetUltimoContrato != null ? revenda.GetUltimoContrato.Numero : ContratoComercial.GetUltimoNumeroDeContrato != 0 ? ContratoComercial.GetUltimoNumeroDeContrato + 1 : 101).ToString() + "/" + DateTime.Now.Year.ToString() + @"</div></div><div class='paragrafo'>
O presente contrato, regido pelas condições e cláusulas descritas abaixo, constitui total entendimento entre <strong>AMBIENTALIS ASSESSORIA E SERVIÇOS LTDA</strong>, localizada à Rod. Fued Nemer, s/n – km 02, Santa Bárbara, Castelo/ES – CEP: 29.360-000, 
devidamente registrada no CNPJ/MF sob n° 11.259.526/0001-07, Inscrição Estadual n° Isenta; <strong>LOGUS SISTEMAS LTDA - EPP</strong>, localizada à Rua Hyercem Machado, n° 26, Bairro Gilberto Machado, Cachoeiro de Itapemirim/ES – CEP: 29.303-312, devidamente registrada no CNPJ/MF sob n° 36.420.818/0001-00, Inscrição Estadual n° Isenta, doravante denominadas " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTES</strong>" : "<strong>REPRESENTADAS</strong>") + @"
e do outro lado; " + (rbtnPessoaFisica.Checked ? tbxNome.Text + ", pessoa física, residente e domiciliado à " : tbxRazaoSocial.Text + ", localizada à ") + tbxLogradouro.Text + ", Número:" + tbxNumero.Text + ", Bairro: " + tbxBairro.Text + ", CEP: " + tbxCEP.Text + ", Cidade: " + ddlCidade.SelectedItem.Text + ", Estado: " +
                   ddlEstado.SelectedItem.Text + (rbtnPessoaFisica.Checked ? ", portador do RG n° " + tbxRG.Text + " inscrito no CPF/MF sob n° " + tbxCPF.Text + ", doravante denominado <strong>CONCESSIONÁRIO</strong>." : ", devidamente registrada no CNPJ/MF sob n° " + tbxCNPJ.Text + ", Inscrição Estadual n° " + (chkIsentoICMS.Checked ? "Isenta" : tbxInscricaoEstadual.Text) + ", representada neste ato por " + tbxRepresentanteEmpresa.Text + ", inscrito no CPF/MF sob n° " + tbxCPFRepresentanteLegal.Text + "; doravante denominada <strong>REPRESENTANTE - REVENDA</strong>.") + @"</div>
<br /><div class='paragrafo clausula'>" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" através deste instrumento está licenciado para indicar o sistema de programa de computador denominado <strong>SISTEMA SUSTENTAR</strong> produzido pela <strong>LOGUS SISTEMAS LTDA – EPP</strong>, ora denominada <strong>DESENVOLVEDORA - " + (rbtnPessoaFisica.Checked ? "CONCEDENTE" : "REPRESENTADA") + @"</strong>, de acordo com os termos e condições gerais aqui descritas.</div><br />
<div class='paragrafo'><strong>CLÁUSULA PRIMEIRA - DO OBJETO</strong><br /></div><br />
<div class='paragrafo'><strong>1.</strong> &nbsp;O objeto deste presente instrumento é a autorização da  " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong> ao <strong>CONCESSIONÁRIO</strong>" : "<strong>REPRESENTADA</strong> à <strong>REPRESENTANTE</strong>") + @" para que este promova a indicação/cadastramento no site www.sustentar.inf.br de novos clientes, e prospectos, em caráter não exclusivo, de assinatura do <strong>SERVIÇO WEB - SISTEMA SUSTENTAR</strong> para o <strong>USUÁRIO</strong> monitorar/acompanhar seus processos administrativos ambientais e/ou minerários, de forma intransferível e não exclusiva, obrigando-se os usuários a o utilizarem unicamente para as atividades a que se destina - Lei 9.609, de fevereiro de 1.998.</div>
<br /><div class='paragrafo'><strong>1.1.</strong> &nbsp;Os termos e condições deste instrumento de <strong>PARCEIRA COMERCIAL</strong> estão devidamente <strong>Registrados no 1º Oficial de Registro de Títulos e Documentos de Cachoeiro de Itapemirim/ES, sob o protocolo número 25.537 e microfilme de número 19.593. Livro B</strong>, cujo teor " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" declara conhecer e concordar na sua totalidade.</div><br />
<div class='paragrafo'><strong>1.2.</strong> &nbsp;O sistema <strong>SUSTENTAR</strong> tem como finalidade viabilizar o acompanhamento por meio eletrônico, site: www.sustentar.inf.br dos processos administrativos ambientais e minerários realizados unicamente pelo <strong>USUÁRIO</strong>.</div><br />
<div class='paragrafo'><strong>CLÁUSULA SEGUNDA – DA PARCERIA E REPRESENTAÇÃO COMERCIAL</strong><br /></div><br />
<div class='paragrafo clausula'><strong>2.</strong> &nbsp;Liberar, após a confirmação da assinatura deste presente instrumento, a permissão de utilização " + (rbtnPessoaFisica.Checked ? "do <strong>CONCESSIONÁRIO</strong>" : "da <strong>REPRESENTANTE</strong>") + @" ao site www.sustentar.inf.br para indicar possíveis vendas, cadastrando o CPF e/ou CNPJ do cliente.</div>
<div class='paragrafo'><strong>2.1.</strong> &nbsp;Fornecer " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" o “login” e “senha de administrador”, que lhe dará acesso ao endereço eletrônico, para acompanhar seus clientes cadastrados, pagamentos e comissões.</div>
<div class='paragrafo'><strong>2.2.</strong> &nbsp;Cabe " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" manter sigilo absoluto sobre a senha de acesso, comprometendo-se desde já a não repassá-la a terceiros.</div>
<div class='paragrafo'><strong>2.3.</strong> &nbsp; " + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" assume total responsabilidade pelas informações pessoais fornecidas ao sistema.</div><br />
<div class='paragrafo'><strong>CLÁUSULA TERCEIRA – DA COMISSÃO E PAGAMENTOS</strong><br /></div><br />
<div class='paragrafo'><strong>3.</strong> &nbsp;Considerando os contratos efetivamente celebrados em virtude da indicação do sistema <strong>SUSTENTAR</strong>, " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" receberá os seguintes valores:</div><br />
<div class='paragrafo'>
<table width='100%' style='border:1px solid silver;'><tr style='border:1px solid silver;'><td align='center' width='50%' style='background-color: Silver; border:1px solid silver;'><strong>PRAZO</strong></td><td align='center' width='50%' style='background-color: Silver; border:1px solid silver;'><strong>COMISSÃO</strong></td></tr>
<tr style='border:1px solid silver;'><td align='center' style='border:1px solid silver;'><strong>1º mês</strong></td><td align='center' style='border:1px solid silver;'><strong>100% (cem por cento)</strong></td></tr>
<tr style='border:1px solid silver;'><td align='center' style='border:1px solid silver;'><strong>Demais meses</strong></td><td align='center' style='border:1px solid silver;'><strong>" + tbxComissaoContrato.Text + @"% (" + numero.ToString() + @" por cento)</strong></td></tr>
<tr style='border:1px solid silver;'><td colspan='2' style='border:1px solid silver;'>&nbsp;</td></tr>
<tr style='border:1px solid silver;'><td colspan='2' style='border:1px solid silver;'>O percentual da comissão recairá sobre o valor pago mensalmente pelos clientes, enquanto vigente o contrato</td></tr>
<tr style='border:1px solid silver;'><td colspan='2' style='border:1px solid silver;'>No caso de carência para início dos pagamentos pelos clientes, os repasses   de comissionamentos deverão respeitar os respectivos prazos.</td></tr></table></div><br />
<div class='paragrafo'><strong>3.1.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" deverá emitir Nota Fiscal ou RPA à " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" das comissões efetivamente devidas, no prazo de 15 (quinze) dias após o pagamento do cliente.</div>
<div class='paragrafo'><strong>3.2.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" recebe nesta data a tabela de preços de comercialização. Ficando desde já <strong>expressamente proibido alterar a tabela de preços</strong>.</div><br />
<div class='paragrafo'><strong>CLÁUSULA QUARTA - DOS DIREITOS E OBRIGAÇÕES</strong><br /></div><br />
<div class='paragrafo'><strong>4.</strong> &nbsp;O registro de oportunidades de vendas do sistema <strong>SUSTENTAR</strong> terá o prazo máximo de <strong>06 (seis) meses</strong>, após este período essas indicações estarão livres para que outros <strong>REPRESENTANTES</strong> trabalhem nelas.</div>
<div class='paragrafo'><strong>4.1.</strong> &nbsp;É expressamente vedado " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" o uso do nome, marca ou logotipo da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" em suas notas fiscais, faturas e outros impressos fiscais, quaisquer que sejam, sem prévio e expresso consentimento da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @", salvo caso de Marketing, Publicidade e Propaganda da marca.</div>
<div class='paragrafo'><strong>4.2.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" se obriga a representar a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" de maneira exclusiva, no que tange ao gênero do negócio sob pena de dar causa à rescisão do presente contrato nos termos da cláusula 7.1.  Igualmente, se obriga a zelar pelo bom nome da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @", de sua marca e de seus produtos, abstendo-se de praticar qualquer ato que possa, de alguma maneira, lhe prejudicar a boa reputação.</div><br />
<div class='paragrafo'><strong>CLÁUSULA QUINTA - DAS LIMITAÇÕES DOS SERVIÇOS</strong><br /></div><br />
<div class='paragrafo'><strong>5.</strong> &nbsp;Constitui infração, e é expressamente proibida a comercialização, reprodução, modificação ou distribuição, de parte ou totalidade das informações e todo o conteúdo contido em ambos os serviços, sem prévia e expressa autorização.</div>
<div class='paragrafo'><strong>5.1.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" se compromete a não interferir de qualquer forma com o serviço prestado pela " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @".</div>
<div class='paragrafo'><strong>5.2.</strong> &nbsp;Esta parceria comercial não confere " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" a propriedade intelectual do sistema, nem mesmo o direito de utilizar quaisquer marcas ou logotipos utilizados pela " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @".</div>
<div class='paragrafo'><strong>5.3.</strong> &nbsp;Em razão do presente contrato, não há quaisquer vínculos de natureza empregatícia, societária ou associativa entre as partes, sendo " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" único e exclusivo responsável por todas as obrigações, ônus e encargos advindos da administração de seu negócio, quer de natureza fiscal, trabalhista, previdenciária etc., de seus empregados ou prepostos, bem como por ações que venham a ser propostas em decorrência de suas falhas ou omissões.</div><br />
<div class='paragrafo'><strong>CLÁUSULA SEXTA – DA VIGÊNCIA</strong><br /></div><br />
<div class='paragrafo'><strong>6.</strong> &nbsp;O prazo de vigência do presente contrato é indeterminado, iniciando-se automaticamente na data de sua assinatura.</div><br />
<div class='paragrafo'><strong>CLÁUSULA SÉTIMA – DA RESCISÃO</strong><br /></div><br />
<div class='paragrafo'><strong>7.</strong> &nbsp;O presente contrato poderá ser rescindido por qualquer das partes contratantes, a qualquer momento, independentemente de multas, indenizações ou qualquer cláusula penal, desde que se faça mediante prévia comunicação por escrito a outra parte, com prazo mínimo de <strong>30 (trinta) dias de antecedência</strong>, exceto na ocorrência de caso fortuito ou força maior, nos termos do artigo 393 do Código Civil Brasileiro. </div>
<div class='paragrafo'><strong>7.1.</strong> &nbsp;Assiste a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" à possibilidade de rescindir unilateralmente esta avença, caso " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" viole alguma das disposições ora descritas.</div>
<div class='paragrafo'><strong>7.2.</strong> &nbsp;Caso o cliente rescinda o contrato com a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" a comissão do " + (rbtnPessoaFisica.Checked ? "<strong>CONCESSIONÁRIO</strong>" : "<strong>REPRESENTANTE</strong>") + @" será automaticamente cancelada.</div><br />
<div class='paragrafo'><strong>CLÁUSULA OITAVA – DISPOSIÇÕES GERAIS</strong><br /></div><br />
<div class='paragrafo'><strong>8.</strong> &nbsp;A " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" reserva-se o direito de alterar qualquer um dos serviços ou produtos oferecidos ou incluir novos, não implicando em qualquer infração a este contrato, independente de comunicação " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @".</div>
<div class='paragrafo'><strong>8.1.</strong> &nbsp;Considerando que a presente concessão foi dada após e em consequência da análise da situação da pessoa " + (rbtnPessoaFisica.Checked ? "do <strong>CONCESSIONÁRIO</strong>" : "da <strong>REPRESENTANTE</strong>") + @", <strong>é expressamente vedado</strong> transferir ou ceder para quem quer que seja e a que título for, no todo ou em parte, os direitos e obrigações aqui ajustados, exceto se houver prévia e expressa anuência da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @".</div>
<div class='paragrafo'><strong>8.2.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" se obriga, não só durante a sua vigência, mas para todo sempre, a tratar como absolutamente sigilosas todas as informações e documentações que receber da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" em decorrência do presente contrato.</div>
<div class='paragrafo'><strong>8.3.</strong> &nbsp;Fica expressamente acordado que todos os aditamentos/alterações, totais ou parciais a este contrato, bem como quaisquer avisos ou comunicações que uma parte fizer à outra, somente terão valor se feitos por escrito. Este contrato prevalecerá sobre qualquer acordo entre as partes feito anteriormente.</div>
<div class='paragrafo'><strong>8.4.</strong> &nbsp;A eventual aceitação, por uma das partes, do não cumprimento de qualquer cláusula deste contrato deverá ser interpretada como mera liberalidade, não implicando novação ou desistência de exigência de seu cumprimento e do direito de pleitear futuramente sua execução.</div><br />
<div class='paragrafo'><strong>CLÁUSULA NONA - DO FORO</strong><br /></div><br />
<div class='paragrafo'><strong>9.</strong> &nbsp;As condições gerais descritas se regem pela Lei brasileira e quaisquer controvérsias relativas a elas, serão dirimidas no Foro da Comarca de Cachoeiro de Itapemirim/ES, à exclusão de qualquer outro, sendo este eleito pelas partes deste contrato, para solucionar quaisquer controvérsias provindas do presente contrato, que venham a ocorrer a qualquer tempo.</div>
<div class='paragrafo'><strong>9.1.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" declara estar de acordo com todos os termos e condições descritos acima e assume a responsabilidade de cumprir todas as cláusulas presentes neste contrato.</div>
<br /><br /><div class='paragrafo titulo'>Cachoeiro de Itapemirim/ES. " + DateTime.Now.Day + " de " + ContratoComercial.GetNomeMes(DateTime.Now.Month) + " de " + DateTime.Now.Year.ToString() + "</div></div>";

    }

    private void ExibirContratoConsultora()
    {
        Revenda revenda = Revenda.ConsultarPorId(hfId.Value.ToInt32());
        NumeroPorcentagemPorExtenso numero = new NumeroPorcentagemPorExtenso();
        numero.SetNumero(Convert.ToDecimal(tbxComissaoContrato.Text));
        lblTextoContrato.Text = @"<div id='imprimir_contrato_original'>                                       
                                       <div class='paragrafo titulo' style='font-size: 11pt;'>" + (rbtnPessoaFisica.Checked ? "CONTRATO PARTICULAR DE PARCERIA COMERCIAL DE SERVIÇO WEB" : "CONTRATO PARTICULAR DE PARCERIA E REPRESENTAÇÃO COMERCIAL DE SERVIÇO WEB") + @"
                                       <div>Nº " + (revenda != null && revenda.GetUltimoContrato != null ? revenda.GetUltimoContrato.Numero : ContratoComercial.GetUltimoNumeroDeContrato != 0 ? ContratoComercial.GetUltimoNumeroDeContrato + 1 : 101).ToString() + "/" + DateTime.Now.Year.ToString() + @"</div></div><div class='paragrafo'>
O presente contrato, regido pelas condições e cláusulas descritas abaixo, constitui total entendimento entre <strong>AMBIENTALIS ASSESSORIA E SERVIÇOS LTDA</strong>, localizada à Rod. Fued Nemer, s/n – km 02, Santa Bárbara, Castelo/ES – CEP: 29.360-000, 
devidamente registrada no CNPJ/MF sob n° 11.259.526/0001-07, Inscrição Estadual n° Isenta; <strong>LOGUS SISTEMAS LTDA - EPP</strong>, localizada à Rua Hyercem Machado, n° 26, Bairro Gilberto Machado, Cachoeiro de Itapemirim/ES – CEP: 29.303-312, devidamente registrada no CNPJ/MF sob n° 36.420.818/0001-00, Inscrição Estadual n° Isenta, doravante denominadas " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTES</strong>" : "<strong>REPRESENTADAS</strong>") + @"
e do outro lado; " + (rbtnPessoaFisica.Checked ? tbxNome.Text + ", pessoa física, consultor na área ambiental e/ou minerária, residente e domiciliado à " : tbxRazaoSocial.Text + ", pessoa jurídica de direito privado, localizada à ") + tbxLogradouro.Text + ", Número:" + tbxNumero.Text + ", Bairro: " + tbxBairro.Text + ", CEP: " + tbxCEP.Text + ", Cidade: " + ddlCidade.SelectedItem.Text + ", Estado: " +
                   ddlEstado.SelectedItem.Text + (rbtnPessoaFisica.Checked ? ", portador do RG n° " + tbxRG.Text + " inscrito no CPF/MF sob n° " + tbxCPF.Text + ", doravante denominado <strong>CONCESSIONÁRIO</strong>." : ", devidamente registrada no CNPJ/MF sob n° " + tbxCNPJ.Text + ", Inscrição Estadual n° " + (chkIsentoICMS.Checked ? "Isenta" : tbxInscricaoEstadual.Text) + ", representada neste ato por " + tbxRepresentanteEmpresa.Text + ", inscrito no CPF/MF sob n° " + tbxCPFRepresentanteLegal.Text + "; doravante denominada <strong>REPRESENTANTE</strong>.") + @"</div>
<br /><div class='paragrafo clausula'>" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" através deste instrumento está licenciado para indicar o sistema de programa de computador denominado <strong>SISTEMA SUSTENTAR</strong> produzido pela <strong>LOGUS SISTEMAS LTDA – EPP</strong>, ora denominada <strong>DESENVOLVEDORA - " + (rbtnPessoaFisica.Checked ? "CONCEDENTE" : "REPRESENTADA") + @"</strong>, de acordo com os termos e condições gerais aqui descritas.</div>
<div class='paragrafo'>Neste ato " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" declara atuar " + (rbtnPessoaFisica.Checked ? "como <strong>CONSULTOR NA ÁREA AMBIENTAL E/OU MINERÁRIA</strong>" : "no ramo de <strong>CONSULTORIA AMBIENTAL E/OU MINERÁRIA</strong>") + @", outrossim que está apta em representar a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" na indicação e possível comercialização de seus sistemas de gerenciamento online.</div><br />
<div class='paragrafo'><strong>CLÁUSULA PRIMEIRA - DO OBJETO</strong><br /></div><br />
<div class='paragrafo'><strong>1.</strong> &nbsp;O objeto deste presente instrumento é a autorização da  " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong> ao <strong>CONCESSIONÁRIO</strong>" : "<strong>REPRESENTADA</strong> à <strong>REPRESENTANTE</strong>") + @" para que este promova a indicação/cadastramento no site www.sustentar.inf.br de novos clientes, e prospectos, em caráter não exclusivo, de assinatura do <strong>SERVIÇO WEB - SISTEMA SUSTENTAR</strong> para o <strong>USUÁRIO</strong> monitorar/acompanhar seus processos administrativos ambientais e/ou minerários, de forma intransferível e não exclusiva, obrigando-se os usuários a o utilizarem unicamente para as atividades a que se destina - Lei 9.609, de fevereiro de 1.998.</div>
<br /><div class='paragrafo'><strong>1.1.</strong> &nbsp;Os termos e condições deste instrumento de <strong>PARCEIRA COMERCIAL</strong> estão devidamente <strong>Registrados no 1º Oficial de Registro de Títulos e Documentos de Cachoeiro de Itapemirim/ES, sob o protocolo ___________ e microfilme de número _________</strong>, cujo teor " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" declara conhecer e concordar na sua totalidade.</div><br />
<div class='paragrafo'><strong>1.2.</strong> &nbsp;O sistema <strong>SUSTENTAR</strong> tem como finalidade viabilizar o acompanhamento por meio eletrônico, site: www.sustentar.inf.br dos processos administrativos ambientais e minerários realizados unicamente pelo <strong>USUÁRIO</strong>.</div><br />
<div class='paragrafo'><strong>CLÁUSULA SEGUNDA – DA PARCERIA E REPRESENTAÇÃO COMERCIAL</strong><br /></div><br />
<div class='paragrafo clausula'><strong>2.</strong> &nbsp;Liberar, após a confirmação da assinatura deste presente instrumento, a permissão de utilização " + (rbtnPessoaFisica.Checked ? "do <strong>CONCESSIONÁRIO</strong>" : "da <strong>REPRESENTANTE</strong>") + @" ao site www.sustentar.inf.br para indicar possíveis vendas, cadastrando o CPF e/ou CNPJ do cliente.</div>
<div class='paragrafo'><strong>2.1.</strong> &nbsp;Fornecer " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" o “login” e “senha de administrador”, que lhe dará acesso ao endereço eletrônico, para acompanhar seus clientes cadastrados, pagamentos e comissões.</div>
<div class='paragrafo'><strong>2.2.</strong> &nbsp;Cabe " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" manter sigilo absoluto sobre a senha de acesso, comprometendo-se desde já a não repassá-la a terceiros.</div>
<div class='paragrafo'><strong>2.3.</strong> &nbsp; " + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" assume total responsabilidade pelas informações pessoais fornecidas ao sistema.</div>
<div class='paragrafo'><strong>2.4.</strong> &nbsp; Através desta parceria a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" concede, sem custo, " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" <strong>01 (um)</strong> usuário do sistema <strong>SUSTENTAR</strong> para uso próprio, enquanto vigorar este contrato.</div><br />
<div class='paragrafo'><strong>CLÁUSULA TERCEIRA – DA COMISSÃO E PAGAMENTOS</strong><br /></div><br />
<div class='paragrafo'><strong>3.</strong> &nbsp;Considerando os contratos efetivamente celebrados em virtude da indicação do sistema <strong>SUSTENTAR</strong>, " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" receberá os seguintes valores:</div><br />
<div class='paragrafo'>
<table width='100%' style='border:1px solid silver;'><tr style='border:1px solid silver;'><td align='center' width='50%' style='background-color: Silver; border:1px solid silver;'><strong>PRAZO</strong></td><td align='center' width='50%' style='background-color: Silver; border:1px solid silver;'><strong>COMISSÃO</strong></td></tr>
<tr style='border:1px solid silver;'><td align='center' style='border:1px solid silver;'>1º mês</td><td align='center' style='border:1px solid silver;'>100% (cem por cento)</td></tr>
<tr style='border:1px solid silver;'><td align='center' style='border:1px solid silver;'>Demais meses</td><td align='center' style='border:1px solid silver;'><strong>" + tbxComissaoContrato.Text + @"% (" + numero.ToString() + @" por cento)</strong></td></tr>
<tr style='border:1px solid silver;'><td colspan='2' style='border:1px solid silver;'>&nbsp;</td></tr>
<tr style='border:1px solid silver;'><td colspan='2' style='border:1px solid silver;'>O percentual da comissão recairá sobre o valor pago mensalmente pelos clientes, enquanto vigente o contrato</td></tr>
<tr style='border:1px solid silver;'><td colspan='2' style='border:1px solid silver;'>No caso de carência para início dos pagamentos pelos clientes, os repasses   de comissionamentos deverão respeitar os respectivos prazos.</td></tr></table></div><br />
<div class='paragrafo'><strong>3.1.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" deverá emitir Nota Fiscal ou RPA à " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" das comissões efetivamente devidas, no prazo de 15 (quinze) dias após o pagamento do cliente.</div>
<div class='paragrafo'><strong>3.2.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" recebe nesta data a tabela de preços de comercialização. Ficando desde já <strong>expressamente proibido alterar a tabela de preços</strong>.</div><br />
<div class='paragrafo'><strong>CLÁUSULA QUARTA - DOS DIREITOS E OBRIGAÇÕES</strong><br /></div><br />
<div class='paragrafo'><strong>4.</strong> &nbsp;O registro de oportunidades de vendas do sistema <strong>SUSTENTAR</strong> terá o prazo máximo de <strong>06 (seis) meses</strong>, após este período essas indicações estarão livres para que outros <strong>REPRESENTANTES</strong> trabalhem nelas.</div>
<div class='paragrafo'><strong>4.1.</strong> &nbsp;É expressamente vedado " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" o uso do nome, marca ou logotipo da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" em suas notas fiscais, faturas e outros impressos fiscais, quaisquer que sejam, sem prévio e expresso consentimento da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @", salvo caso de Marketing, Publicidade e Propaganda da marca.</div>
<div class='paragrafo'><strong>4.2.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" se obriga a representar a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" de maneira exclusiva, no que tange ao gênero do negócio sob pena de dar causa à rescisão do presente contrato nos termos da cláusula 7.1.  Igualmente, se obriga a zelar pelo bom nome da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @", de sua marca e de seus produtos, abstendo-se de praticar qualquer ato que possa, de alguma maneira, lhe prejudicar a boa reputação.</div><br />
<div class='paragrafo'><strong>CLÁUSULA QUINTA - DAS LIMITAÇÕES DOS SERVIÇOS</strong><br /></div><br />
<div class='paragrafo'><strong>5.</strong> &nbsp;Constitui infração, e é expressamente proibida a comercialização, reprodução, modificação ou distribuição, de parte ou totalidade das informações e todo o conteúdo contido em ambos os serviços, sem prévia e expressa autorização.</div>
<div class='paragrafo'><strong>5.1.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" se compromete a não interferir de qualquer forma com o serviço prestado pela " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @".</div>
<div class='paragrafo'><strong>5.2.</strong> &nbsp;Esta parceria comercial não confere " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" a propriedade intelectual do sistema, nem mesmo o direito de utilizar quaisquer marcas ou logotipos utilizados pela " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @".</div>
<div class='paragrafo'><strong>5.3.</strong> &nbsp;Em razão do presente contrato, não há quaisquer vínculos de natureza empregatícia, societária ou associativa entre as partes, sendo " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" único e exclusivo responsável por todas as obrigações, ônus e encargos advindos da administração de seu negócio, quer de natureza fiscal, trabalhista, previdenciária etc., de seus empregados ou prepostos, bem como por ações que venham a ser propostas em decorrência de suas falhas ou omissões.</div><br />
<div class='paragrafo'><strong>CLÁUSULA SEXTA – DA VIGÊNCIA</strong><br /></div><br />
<div class='paragrafo'><strong>6.</strong> &nbsp;O prazo de vigência do presente contrato é indeterminado, iniciando-se automaticamente na data de sua assinatura.</div><br />
<div class='paragrafo'><strong>CLÁUSULA SÉTIMA – DA RESCISÃO</strong><br /></div><br />
<div class='paragrafo'><strong>7.</strong> &nbsp;O presente contrato poderá ser rescindido por qualquer das partes contratantes, a qualquer momento, independentemente de multas, indenizações ou qualquer cláusula penal, desde que se faça mediante prévia comunicação por escrito a outra parte, com prazo mínimo de <strong>30 (trinta) dias de antecedência</strong>, exceto na ocorrência de caso fortuito ou força maior, nos termos do artigo 393 do Código Civil Brasileiro.</div>
<div class='paragrafo'><strong>7.1.</strong> &nbsp;Assiste a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" à possibilidade de rescindir unilateralmente esta avença, caso " + (rbtnPessoaFisica.Checked ? "o <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @" viole alguma das disposições ora descritas.</div>
<div class='paragrafo'><strong>7.2.</strong> &nbsp;Caso o cliente rescinda o contrato com a " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" a comissão do " + (rbtnPessoaFisica.Checked ? "<strong>CONCESSIONÁRIO</strong>" : "<strong>REPRESENTANTE</strong>") + @" será automaticamente cancelada.</div>
<div class='paragrafo'><strong>7.3.</strong> &nbsp;Na ocorrência de rescisão nos termos das cláusulas 7. e 7.1., cancelar-se-á o usuário do sistema <strong>SUSTENTAR</strong> concedido ao " + (rbtnPessoaFisica.Checked ? "<strong>CONCESSIONÁRIO</strong>" : "<strong>REPRESENTANTE</strong>") + @" conforme cláusula 2.4.</div><br />
<div class='paragrafo'><strong>CLÁUSULA OITAVA – DISPOSIÇÕES GERAIS</strong><br /></div><br />
<div class='paragrafo'><strong>8.</strong> &nbsp;A " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" reserva-se o direito de alterar qualquer um dos serviços ou produtos oferecidos ou incluir novos, não implicando em qualquer infração a este contrato, independente de comunicação " + (rbtnPessoaFisica.Checked ? "ao <strong>CONCESSIONÁRIO</strong>" : "a <strong>REPRESENTANTE</strong>") + @".</div>
<div class='paragrafo'><strong>8.1.</strong> &nbsp;Considerando que a presente concessão foi dada após e em consequência da análise da situação da pessoa " + (rbtnPessoaFisica.Checked ? "do <strong>CONCESSIONÁRIO</strong>" : "da <strong>REPRESENTANTE</strong>") + @", <strong>é expressamente vedado</strong> transferir ou ceder para quem quer que seja e a que título for, no todo ou em parte, os direitos e obrigações aqui ajustados, exceto se houver prévia e expressa anuência da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @".</div>
<div class='paragrafo'><strong>8.2.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" se obriga, não só durante a sua vigência, mas para todo sempre, a tratar como absolutamente sigilosas todas as informações e documentações que receber da " + (rbtnPessoaFisica.Checked ? "<strong>CONCEDENTE</strong>" : "<strong>REPRESENTADA</strong>") + @" em decorrência do presente contrato.</div>
<div class='paragrafo'><strong>8.3.</strong> &nbsp;Fica expressamente acordado que todos os aditamentos/alterações, totais ou parciais a este contrato, bem como quaisquer avisos ou comunicações que uma parte fizer à outra, somente terão valor se feitos por escrito. Este contrato prevalecerá sobre qualquer acordo entre as partes feito anteriormente.</div>
<div class='paragrafo'><strong>8.4.</strong> &nbsp;A eventual aceitação, por uma das partes, do não cumprimento de qualquer cláusula deste contrato deverá ser interpretada como mera liberalidade, não implicando novação ou desistência de exigência de seu cumprimento e do direito de pleitear futuramente sua execução.</div><br />
<div class='paragrafo'><strong>CLÁUSULA NONA - DO FORO</strong><br /></div><br />
<div class='paragrafo'><strong>9.</strong> &nbsp;As condições gerais descritas se regem pela Lei brasileira e quaisquer controvérsias relativas a elas, serão dirimidas no Foro da Comarca de Cachoeiro de Itapemirim/ES, à exclusão de qualquer outro, sendo este eleito pelas partes deste contrato, para solucionar quaisquer controvérsias provindas do presente contrato, que venham a ocorrer a qualquer tempo.</div>
<div class='paragrafo'><strong>9.1.</strong> &nbsp;" + (rbtnPessoaFisica.Checked ? "O <strong>CONCESSIONÁRIO</strong>" : "A <strong>REPRESENTANTE</strong>") + @" declara estar de acordo com todos os termos e condições descritos acima e assume a responsabilidade de cumprir todas as cláusulas presentes neste contrato.</div>
<br /><br /><div class='paragrafo titulo'>Cachoeiro de Itapemirim/ES. " + DateTime.Now.Day + " de " + ContratoComercial.GetNomeMes(DateTime.Now.Month) + " de " + DateTime.Now.Year.ToString() + "</div></div>";
    }

    private void CriarEnviarContrato()
    {
        Revenda revenda = Revenda.ConsultarPorId(hfId.Value.ToInt32());
        ContratoComercial contrato = new ContratoComercial();
        contrato.Aditamento = revenda.GetUltimoContrato != null ? true : false;
        contrato.Ano = DateTime.Now.Year;
        contrato.Numero = revenda.GetUltimoContrato != null ? revenda.GetUltimoContrato.Numero : ContratoComercial.GetUltimoNumeroDeContrato != 0 ? ContratoComercial.GetUltimoNumeroDeContrato + 1 : 101;
        contrato.Comissao = Convert.ToDecimal(tbxComissaoContrato.Text);
        contrato.Texto = lblTextoContrato.Text;
        contrato.Revenda = revenda;
        contrato = contrato.Salvar();
        if (revenda.Contratos == null)
            revenda.Contratos = new List<ContratoComercial>();
        revenda.Contratos.Add(contrato);
        revenda = revenda.Salvar();

        Email email = new Email();
        email.AdicionarDestinatario(revenda.Contato.Email);
        String dadosContrato = Utilitarios.Criptografia.Seguranca.MontarParametros("idRevenda=" + revenda.Id);
        email.Assunto = "Termos do Contrato de Revenda";
        email.Mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'><div style='float:left; margin-left:20px; margin-top:20px;'>
        <img src='http://sustentar.inf.br/imagens/logo_login.png'></div><div style='float:left; margin-left:20px; font-family:arial; font-size:22px; font-weight:bold; margin-top:30px; text-align:center; width: 287px;'>
  Contrato de Revenda <br/> Termos </div><div style='width:100%; height:20px; clear:both'></div>
  <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; background-color:#E9E9E9; text-align:left; height:auto'>Seu cadastro foi Aprovado.<br />
      <br />Você solicitou um cadastro para " + (ddlTipoParceiro.SelectedValue.ToInt32() == 1 ? rbtnPessoaFisica.Checked ? "Agente de negócios" : "Revenda" : rbtnPessoaFisica.Checked ? "Consultor" : "Consultoria") + @" do Sistema Sustentar, para concluir seu cadastro verifique os termos do contrato, abaixo.<br /></div>
  <table style='width:100%; margin-top:10px; height:auto;font-family:Arial, Helvetica, sans-serif; font-size:14px;'><tbody><tr>
            <td align='right' width='10%' style='font-weight:bold'>" + (ddlTipoParceiro.SelectedValue.ToInt32() == 1 ? rbtnPessoaFisica.Checked ? "Agente de negócios:" : "Revenda:" : rbtnPessoaFisica.Checked ? "Consultor:" : "Consultoria:") + @"</td>
            <td align='left' width='50%'>" + revenda.Nome + @"</td></tr><tr>
              <td align='right' width='10%' style='font-weight:bold'>" + (rbtnPessoaFisica.Checked ? "CPF:" : "CNPJ:") + @"
              </td>
              <td align='left' width='50%'>" + revenda.GetNumeroCNPJeCPFComMascara + @"</td>
            </tr><tr>
              <td align='right' style='font-weight:bold'>Comissão(%):</td>
              <td align='left' width='10%'>" + tbxComissaoContrato.Text + @"</td>
            </tr>
            <tr>
            <td colspan='2' align='center' height='40px' valign='middle'><a target='_blank' href='http://sustentar.inf.br/site/website/Site/TrabalheConoscoContrato.aspx" + dadosContrato + @"'>Visualizar e assinar o contrato.</a></td></tr></tbody></table><div style='width:100%; height:20px;'></div>            
  </div>";

        if (!email.EnviarAutenticado(25, false))
            msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
        else
            msg.CriarMensagem("E-mails enviados com sucesso", "Sucesso", MsgIcons.Sucesso);

        if (revenda.Contratos != null && revenda.Contratos.Count > 0)
        {
            lblContrato.Text = "CRIADO";
            criar_novo_contrato.Visible = true;
            visualizar_contrato_criado.Visible = true;
            contrato_naocriado.Visible = false;
        }

    }

    private void VisualizarContrato()
    {
        Revenda revenda = Revenda.ConsultarPorId(hfId.Value.ToInt32());
        if (revenda.GetUltimoContrato != null && revenda.GetUltimoContrato.Id > 0)
        {
            lblPopupEnviarContrato_popupextender.Show();
            lblTextoContrato.Text = revenda.GetUltimoContrato.Texto;
            if (revenda.GetUltimoContrato.Aceito)
                enviar_contrato.Visible = false;
        }
        else
        {
            msg.CriarMensagem("Esta revenda ainda não possui contrato criado", "Alerta", MsgIcons.Alerta);
        }
    }

    #endregion

    #region ___________Eventos___________

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
        try
        {
            hfId.Value = "";
            Response.Redirect("CadastroRevenda.aspx", false);
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
            if (ddlCidade.SelectedValue.ToInt32() == 0)
            {
                msg.CriarMensagem("Selecione um Estado para prosseguir", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ddlEstado.SelectedValue.ToInt32() == 0)
            {
                msg.CriarMensagem("Selecione uma Cidade para prosseguir", "Alerta", MsgIcons.Alerta);
                return;
            }

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
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a revenda para poder excluí-la", "Alerta", MsgIcons.Alerta);
                return;
            }

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

    protected void btnCriarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a revenda para poder criar um contrato", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (ddlTipoParceiro.SelectedValue.ToInt32() == 0)
            {
                msg.CriarMensagem("Selecione o tipo de parceria para poder criar um contrato", "Alerta", MsgIcons.Alerta);
                return;
            }

            tbxComissaoContrato.Text = "";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>AddMacara();</script>", false);
            lblPopupContratoComissao_popupextender.Show();
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

    protected void btnSalvarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPTextoContrato);
    }

    protected void btnSalvarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            if (!tbxComissaoContrato.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Você deve inserir um valor de porcentagem para criar o contrato", "Alerta", MsgIcons.Alerta);
                return;
            }

            if (tbxComissaoContrato.Text.Contains(',') || tbxComissaoContrato.Text.Contains('.'))
            {
                msg.CriarMensagem("O percentual de porcentagem deve ser um número inteiro", "Alerta", MsgIcons.Alerta);
                return;
            }
            lblPopupEnviarContrato_popupextender.Show();
            this.AtualizarContrato();

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

    protected void btnEnviarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            this.CriarEnviarContrato();
            lblPopupContratoComissao_popupextender.Hide();
            lblPopupEnviarContrato_popupextender.Hide();
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

    protected void btnVisualizarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPTextoContrato);
    }

    protected void btnCriarNovoContrato_Click(object sender, EventArgs e)
    {
        try
        {
            lblPopupContratoComissao_popupextender.Show();
            tbxComissaoContrato.Text = "";
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

    protected void btnCriarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPComissao);
    }

    protected void btnCriarNovoContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPComissao);
    }

    protected void btnExcluir_Init(object sender, EventArgs e)
    {
        //verificar como vai ficar esquema de permissão pra ver se libera ou não o acesso ao botão
    }

    protected void btnEnviarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", UPFormalizacaoParceria);
    }

    protected void btnVisualizarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            this.VisualizarContrato();
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