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
    Transacao transacao = new Transacao();

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
            hfIdGrupoEconomico.Value = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idGrupoEconomico", Request);

            if (!IsPostBack)
            {
                this.CarregarEstados();
                this.CarregarGrupoEconomico();
                if (Convert.ToInt32("0" + hfIdGrupoEconomico.Value) > 0)
                    ddlGrupoEconomico.SelectedValue = hfIdGrupoEconomico.Value;
                if (Convert.ToInt32("0" + hfId.Value) > 0)
                    this.CarregarEmpresa(Convert.ToInt32("" + hfId.Value));

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

    private void CarregarGrupoEconomico()
    {
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarEmpresa(int id)
    {
        Empresa e = Empresa.ConsultarPorId(id);

        ddlGrupoEconomico.SelectedValue = e.GrupoEconomico != null ? e.GrupoEconomico.Id.ToString() : "0";
        tbxNome.Text = e.Nome;
        tbxResponsavel.Text = e.Responsavel;
        tbxRepresentanteLegal.Text = e.RepresentanteLegal;
        tbxGestorEconomico.Text = e.GestorEconomico;
        tbxSite.Text = e.Site;
        tbxEmail.Text = e.Contato.Email;
        tbxTelefone.Text = e.Contato.Telefone;
        tbxCelular.Text = e.Contato.Celular;
        tbxRamal.Text = e.Contato.Ramal.ToString();
        tbxFax.Text = e.Contato.Fax;
        tbxNacionalidade.Text = e.Nacionalidade;
        chkAtivo.Checked = e.Ativo;

        if (e.DadosPessoa.GetType() == typeof(DadosFisica))
        {
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
            rblSexo.SelectedIndex = ((DadosFisica)e.DadosPessoa).Sexo.Equals('M') ? 0 : 1;
            tbxDataNascimento.Text = ((DadosFisica)e.DadosPessoa).DataNascimento.EmptyToMinValue();
            tbxCPF.Text = ((DadosFisica)e.DadosPessoa).Cpf;
            hfCnpjCpf.Value = ((DadosFisica)e.DadosPessoa).Cpf;
            tbxRG.Text = ((DadosFisica)e.DadosPessoa).Rg;
            switch (((DadosFisica)e.DadosPessoa).EstadoCivil)
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
            tbxCNPJ.Text = ((DadosJuridica)e.DadosPessoa).Cnpj;
            hfCnpjCpf.Value = ((DadosJuridica)e.DadosPessoa).Cnpj;
            tbxRazaoSocial.Text = ((DadosJuridica)e.DadosPessoa).RazaoSocial;
        }

        tbxLogradouro.Text = e.Endereco.Rua;
        tbxNumero.Text = e.Endereco.Numero;
        tbxComplemento.Text = e.Endereco.Complemento;
        tbxBairro.Text = e.Endereco.Bairro;
        tbxCEP.Text = e.Endereco.Cep;
        tbxPontoReferencia.Text = e.Endereco.PontoReferencia;
        ddlEstado.SelectedValue = e.Endereco.Cidade != null && e.Endereco.Cidade.Estado != null ? e.Endereco.Cidade.Estado.Id.ToString() : "0";
        this.CarregarCidades(Convert.ToInt32(ddlEstado.SelectedValue));
        ddlCidade.SelectedValue = e.Endereco.Cidade != null ? e.Endereco.Cidade.Id.ToString() : "0";

        chkIsentoICMS.Checked = e.DadosPessoa.IsentoICMS;
        tbxInscricaoEstadual.Text = e.DadosPessoa.InscricaoEstadual;
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;

        tbxObservacoes.Text = e.Observacoes;
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
            //verificaçao antes da exclusao
            Empresa e = Empresa.ConsultarPorId(Convert.ToInt32(hfId.Value));
            if ((e.Processos != null && e.Processos.Count > 0) || (e.ProcessosDNPM != null && e.ProcessosDNPM.Count > 0))
                msg.CriarMensagem("Esta Empresa não pode ser excluída pois possui Processos associados!", "Atenção", MsgIcons.Informacao);
            else
            {
                e.Excluir();

                #region ________________ cancelar venda ________________

                if (Session["idEmp"] != null)
                {
                    string emp = Session["idEmp"].ToString().Trim();
                    try
                    {
                        Session["idEmp"] = null;
                        Venda.CancelarVenda(e, this.IdConfig.ToInt32());
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
                    Venda.CancelarVenda(e, this.IdConfig.ToInt32());
                }


                #endregion              

                this.Novo();
                msg.CriarMensagem("Empresa excluida.", "Sucesso");
            }
        }
        else
            msg.CriarMensagem("Nao há empresa salva para ser excluida!", "Alerta", MsgIcons.Alerta);
    }

    private void Novo()
    {
        hfId.Value = "0";
        Response.Redirect("ManterEmpresa.aspx", false);
    }

    private void Salvar()
    {
        if (ddlGrupoEconomico.SelectedIndex < 1)
        {
            msg.CriarMensagem("Selecione um Grupo Econômico!", "Alerta", MsgIcons.Alerta);
            return;
        }

        Empresa empresa = Empresa.ConsultarPorId(Convert.ToInt32("0" + hfId.Value));
        if (empresa == null)
            empresa = new Empresa();

        if (rbtnPessoaJuridica.Checked && Empresa.ExisteCNPJ(empresa, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe uma empresa com o CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
            return;
        }
        if (rbtnPessoaFisica.Checked && Empresa.ExisteCPF(empresa, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")) && hfCnpjCpf.Value != tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""))
        {
            msg.CriarMensagem("Ja existe uma pessoa com o CPF cadastrado.", "Alerta", MsgIcons.Alerta);
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

        if (tbxEmail.Text.Trim().Replace(" ", "") != "")
        {
            if (!WebUtil.ValidarEmailInformado(tbxEmail.Text))
            {
                msg.CriarMensagem("O(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para realizar o cadastro. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". Para inserir nome nos emails, adicione-os entre parênteses: \"(Exemplo) exemplo@sustentar.inf.br\".", "Alerta", MsgIcons.Alerta);
                return;
            }
        }

        empresa.GrupoEconomico = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
        if (empresa.GrupoEconomico.Empresas != null && empresa.GrupoEconomico.Empresas.Count > 0)
        {
            if (empresa.GrupoEconomico.Empresas.Count >= empresa.GrupoEconomico.LimiteEmpresas && empresa.GrupoEconomico.LimiteEmpresas > 0 && hfId.Value.ToInt32() == 0)
            {
                msg.CriarMensagem("O Grupo Econômico selecionado já atingiu seu limite máximo de empresas cadastradas.", "Alerta", MsgIcons.Alerta);
                return;
            }
        }
        empresa.Nome = tbxNome.Text.Trim();
        empresa.Responsavel = tbxResponsavel.Text.Trim();
        empresa.RepresentanteLegal = tbxRepresentanteLegal.Text.Trim();
        empresa.GestorEconomico = tbxGestorEconomico.Text.Trim();
        empresa.Site = tbxSite.Text.Trim();
        empresa.Contato.Email = tbxEmail.Text.Trim().Replace(";", " ;").Replace("  ", " "); ;
        empresa.Contato.Telefone = tbxTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        empresa.Contato.Celular = tbxCelular.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        empresa.Contato.Ramal = string.IsNullOrEmpty(tbxRamal.Text) ? 0 : Convert.ToInt32(tbxRamal.Text.Trim());
        empresa.Contato.Fax = tbxFax.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        empresa.Nacionalidade = tbxNacionalidade.Text.Trim();
        empresa.Ativo = chkAtivo.Checked;

        if (rbtnPessoaFisica.Checked)
        {
            DadosFisica df = new DadosFisica();
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (empresa.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    df = df.Salvar();
                    Empresa auxiliar = new Empresa();
                    auxiliar = (Empresa)empresa.Clone();
                    auxiliar.DadosPessoa = df;
                    if (Empresa.ExisteCPF(auxiliar, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma pessoa com o CPF cadastrado.", "Alerta", MsgIcons.Alerta);
                        df.Excluir();
                        return;
                    }
                    else
                    {
                        DadosJuridica aux = DadosJuridica.ConsultarPorId(empresa.DadosPessoa.Id);
                        aux.Excluir();
                        empresa.DadosPessoa = df;
                        tbxCNPJ.Text = "";
                        tbxRazaoSocial.Text = "";
                    }
                }
                else
                {
                    df = (DadosFisica)empresa.DadosPessoa;
                }
            }

            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            df.DataNascimento = string.IsNullOrEmpty(tbxDataNascimento.Text) ? SqlDate.MinValue : Convert.ToDateTime(tbxDataNascimento.Text);
            df.Rg = tbxRG.Text.Trim();
            df.EstadoCivil = Char.Parse(rblEstadoCivil.SelectedValue);
            df.Sexo = Convert.ToChar(rblSexo.SelectedValue);
            df = df.Salvar();
            empresa.DadosPessoa = df;
        }
        else
        {
            DadosJuridica dj = new DadosJuridica();
            if (Convert.ToInt32("0" + hfId.Value) > 0)
            {
                if (empresa.DadosPessoa.GetType() == typeof(DadosFisica))
                {
                    dj = dj.Salvar();
                    Empresa auxiliar = new Empresa();
                    auxiliar = (Empresa)empresa.Clone();
                    auxiliar.DadosPessoa = dj;
                    if (Empresa.ExisteCNPJ(auxiliar, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "")))
                    {
                        msg.CriarMensagem("Ja existe uma empresa com o CNPJ cadastrado.", "Alerta", MsgIcons.Alerta);
                        dj.Excluir();
                        return;
                    }
                    else
                    {
                        DadosFisica aux = DadosFisica.ConsultarPorId(empresa.DadosPessoa.Id);
                        aux.Excluir();
                        empresa.DadosPessoa = dj;
                        tbxCPF.Text = "";
                        tbxDataNascimento.Text = "";
                        tbxRG.Text = "";
                    }
                }
                else
                {
                    dj = (DadosJuridica)empresa.DadosPessoa;
                }
            }
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            dj = dj.Salvar();
            empresa.DadosPessoa = dj;
        }

        Endereco endereco = empresa.Endereco != null ? empresa.Endereco : new Endereco();
        endereco.Bairro = tbxBairro.Text;
        endereco.Numero = tbxNumero.Text;
        endereco.Complemento = tbxComplemento.Text;
        endereco.Rua = tbxLogradouro.Text;
        endereco.Cep = tbxCEP.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
        endereco.PontoReferencia = tbxPontoReferencia.Text;
        if (ddlEstado.SelectedIndex > 0 && ddlCidade.Items != null)
            endereco.Cidade = Cidade.ConsultarPorId(Convert.ToInt32(ddlCidade.SelectedValue));
        empresa.Endereco = endereco;

        empresa.DadosPessoa.IsentoICMS = chkIsentoICMS.Checked;
        empresa.DadosPessoa.InscricaoEstadual = tbxInscricaoEstadual.Text;

        empresa.Observacoes = tbxObservacoes.Text;

        empresa = empresa.Salvar();
        empresa.Emp = empresa.Id;
        empresa = empresa.Salvar();
        hfId.Value = empresa.Id.ToString();
        msg.CriarMensagem("Empresa salva com sucesso!", "Sucesso");


        #region ________________ Recalcullar comissão ________________

        if (Session["idEmp"] != null)
        {
            string emp = Session["idEmp"].ToString().Trim();
            try
            {
                Session["idEmp"] = null;
                Venda.ReativarVenda(empresa.GrupoEconomico, this.IdConfig.ToInt32());
                Venda.InserirComissoes(empresa.GrupoEconomico, empresa, this.IdConfig.ToInt32());
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
            Venda.InserirComissoes(empresa.GrupoEconomico, empresa, this.IdConfig.ToInt32());
        }


        #endregion

        this.VerificarConfiguracoesDePermissoesPorEmpresa(empresa);

    }

    private void VerificarConfiguracoesDePermissoesPorEmpresa(Empresa empresa)
    {
        //DNPM
        ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");  
      
        ConfiguracaoPermissaoModulo configuracaoModuloDNPM;

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDNPM.Id);
        else
            configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

        if (configuracaoModuloDNPM != null && configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) 
        {
            EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDNPM.Id);

            if (empresaPermissao == null) 
            {
                empresaPermissao = new EmpresaModuloPermissao();

                empresaPermissao.ModuloPermissao = moduloDNPM;
                empresaPermissao.Empresa = empresa;

                empresaPermissao.UsuariosEdicao = new List<Usuario>();

                empresaPermissao.UsuariosEdicao.Add(this.UsuarioLogado);

                empresaPermissao = empresaPermissao.Salvar();
            }
                
        }


        //Meio Ambiente
        ModuloPermissao moduloMA = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

        ConfiguracaoPermissaoModulo configuracaoModuloMA;

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloMA.Id);
        else
            configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

        if (configuracaoModuloMA != null && configuracaoModuloMA.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloMA.Id);

            if (empresaPermissao == null) 
            {
                empresaPermissao = new EmpresaModuloPermissao();

                empresaPermissao.ModuloPermissao = moduloMA;
                empresaPermissao.Empresa = empresa;

                empresaPermissao.UsuariosEdicao = new List<Usuario>();

                empresaPermissao.UsuariosEdicao.Add(this.UsuarioLogado);

                empresaPermissao = empresaPermissao.Salvar();
            }
                
        }


        //Contratos
        ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");

        ConfiguracaoPermissaoModulo configuracaoModuloContratos;

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloContratos.Id);
        else
            configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloContratos.Id);

        if (configuracaoModuloContratos != null && configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloContratos.Id);

            if (empresaPermissao == null) 
            {
                empresaPermissao = new EmpresaModuloPermissao();

                empresaPermissao.ModuloPermissao = moduloContratos;
                empresaPermissao.Empresa = empresa;

                empresaPermissao.UsuariosEdicao = new List<Usuario>();

                empresaPermissao.UsuariosEdicao.Add(this.UsuarioLogado);

                empresaPermissao = empresaPermissao.Salvar();
            }
                
        }


        //Diversos
        ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");

        ConfiguracaoPermissaoModulo configuracaoModuloDiversos;

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDiversos.Id);
        else
            configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

        if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDiversos.Id);

            if (empresaPermissao == null) 
            {
                empresaPermissao = new EmpresaModuloPermissao();

                empresaPermissao.ModuloPermissao = moduloDiversos;
                empresaPermissao.Empresa = empresa;

                empresaPermissao.UsuariosEdicao = new List<Usuario>();

                empresaPermissao.UsuariosEdicao.Add(this.UsuarioLogado);

                empresaPermissao = empresaPermissao.Salvar();
            }
                
        }
    }



    public void LimparCampos()
    {
        tbxResponsavel.Text = "";
        tbxSite.Text = "";
        tbxEmail.Text = "";
        tbxTelefone.Text = "";
        tbxCelular.Text = "";
        tbxRamal.Text = "";
        tbxFax.Text = "";
        tbxNacionalidade.Text = "";
        chkAtivo.Checked = true;
        rbtnPessoaFisica.Checked = false;
        rbtnPessoaJuridica.Checked = true;
        rblSexo.SelectedIndex = 0;
        tbxDataNascimento.Text = "";
        tbxCPF.Text = "";
        tbxRG.Text = "";
        rblEstadoCivil.SelectedIndex = 0;
        tbxCNPJ.Text = "";
        tbxRazaoSocial.Text = "";
        tbxLogradouro.Text = "";
        tbxNumero.Text = "";
        tbxComplemento.Text = "";
        tbxBairro.Text = "";
        tbxCEP.Text = "";
        tbxPontoReferencia.Text = "";
        this.CarregarEstados();
        ddlEstado.SelectedValue = "0";
        ddlCidade.Items.Insert(0, new ListItem("", "0"));
        ddlCidade.SelectedValue = "0";
        chkIsentoICMS.Checked = false;
        tbxInscricaoEstadual.Text = "";
        tbxInscricaoEstadual.Enabled = !chkIsentoICMS.Checked;
        tbxObservacoes.Text = "";
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

    private void ImportarDadosCadastrais()
    {
        if (ddlGrupoEconomico.SelectedIndex > 0)
        {
            GrupoEconomico gc = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            tbxNome.Text = gc.Nome;
            if (gc.DadosPessoa.GetType() == typeof(DadosFisica))
            {
                this.LimparCampos();
                rbtnPessoaFisica.Checked = true;
                rbtnPessoaJuridica.Checked = false;
                rblSexo.SelectedIndex = ((DadosFisica)gc.DadosPessoa).Sexo == 'M' ? 0 : 1;
                tbxCPF.Text = ((DadosFisica)gc.DadosPessoa).Cpf;
                tbxDataNascimento.Text = ((DadosFisica)gc.DadosPessoa).DataNascimento.EmptyToMinValue();
                switch (((DadosFisica)gc.DadosPessoa).EstadoCivil)
                {
                    case 's': rblEstadoCivil.SelectedIndex = 0; break;
                    case 'c': rblEstadoCivil.SelectedIndex = 1; break;
                    case 'p': rblEstadoCivil.SelectedIndex = 2; break;
                    case 'd': rblEstadoCivil.SelectedIndex = 3; break;
                    case 'v': rblEstadoCivil.SelectedIndex = 4; break;
                }
                tbxRG.Text = ((DadosFisica)gc.DadosPessoa).Rg;
            }
            else
            {
                this.LimparCampos();
                rbtnPessoaFisica.Checked = false;
                rbtnPessoaJuridica.Checked = true;
                tbxCNPJ.Text = ((DadosJuridica)gc.DadosPessoa).Cnpj;
                tbxRazaoSocial.Text = ((DadosJuridica)gc.DadosPessoa).RazaoSocial;
            }
            tbxResponsavel.Text = gc.Responsavel;
            tbxRepresentanteLegal.Text = gc.RepresentanteLegal;
            tbxGestorEconomico.Text = gc.GestorEconomico;
            if (gc.DadosPessoa.IsentoICMS)
            {
                chkIsentoICMS.Checked = true;
                tbxInscricaoEstadual.Enabled = false;
            }
            else
            {
                tbxInscricaoEstadual.Text = gc.DadosPessoa.InscricaoEstadual;
            }
            tbxSite.Text = gc.Site;
            tbxTelefone.Text = gc.Contato.Telefone;
            tbxCelular.Text = gc.Contato.Celular;
            tbxFax.Text = gc.Contato.Fax;
            tbxRamal.Text = gc.Contato.Ramal.ToString();
            tbxNacionalidade.Text = gc.Nacionalidade;
        }
    }

    #endregion

    #region ________Eventos___________

    protected void PremissaoUsuario_PreRender(object sender, EventArgs e)
    {
        Permissoes.ValidarControle((Button)sender, this.UsuarioEditorModuloGeral);
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

    protected void btnImportarEmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomico.SelectedIndex > 0)
            {
                GrupoEconomico gc = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                if (tbxEmail.Text.IsNotNullOrEmpty())
                {
                    if (!tbxEmail.Text.Contains(gc.Contato.Email))
                    {
                        tbxEmail.Text += ";" + gc.Contato.Email;
                    }
                }
                else
                {
                    tbxEmail.Text += gc.Contato.Email;
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um Grupo Econômico.", "Alerta", MsgIcons.Alerta);
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

    protected void btnImportarEndereco_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomico.SelectedIndex > 0)
            {
                GrupoEconomico gc = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                if (gc.Endereco != null)
                {
                    tbxLogradouro.Text = gc.Endereco.Rua;
                    tbxNumero.Text = gc.Endereco.Numero;
                    tbxComplemento.Text = gc.Endereco.Complemento;
                    tbxBairro.Text = gc.Endereco.Bairro;
                    tbxCEP.Text = gc.Endereco.Cep;
                    tbxPontoReferencia.Text = gc.Endereco.PontoReferencia;
                    this.CarregarEstados();
                    ddlEstado.SelectedValue = gc.Endereco.Cidade != null ? gc.Endereco.Cidade.Estado.Id.ToString() : "0";
                    if (ddlEstado.SelectedValue != "0")
                    {
                        this.CarregarCidades(ddlEstado.SelectedValue.ToInt32());
                        ddlCidade.SelectedValue = gc.Endereco.Cidade.Id.ToString();
                    }
                }
            }
            else
            {
                msg.CriarMensagem("Selecione um Grupo Econômico.", "Alerta", MsgIcons.Alerta);
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

    protected void btnImportarDadosCadastrais_Click(object sender, EventArgs e)
    {
        try
        {
            this.ImportarDadosCadastrais();
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