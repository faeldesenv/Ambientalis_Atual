using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Configuration;

public partial class Acesso_Login : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    public void SetIdConfig()
    {
        if (Request["v"] != null)
        {
            Session["idConfig"] = Request["v"].ToString();
        }
        else
        {
            Session["idConfig"] = "0";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            this.SetIdConfig();
            try
            {
                transacao.Abrir();
                Notificacao.ExcluirNotificacoesAvulsas();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                MBOX1.Show(msg);
            }
        }
    }

    #region ________ Eventos ___________

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            this.SetIdConfig();
            transacao.Abrir();
            this.Acessar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            MBOX1.Show(msg);
        }
    }

    #endregion

    #region ________ Métodos ___________

    private void Acessar()
    {
        Usuario user = new Usuario();
        user.Login = tbxLogin.Text.Trim();
        user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim(), true);

        if (Usuario.ValidaUsuario(ref user))
        {
            Session["UsuarioLogado_SistemaAmbiental"] = user;
            if (user.GrupoEconomico != null)
            {
                if (!user.GrupoEconomico.Ativo)
                {
                    msg.CriarMensagem("Não é possível realizar o login porque o Grupo Ecônomico do usuário informado ainda não foi ativado, por favor contacte o administrador do sistema para ativação", "Informação", MsgIcons.Informacao);
                    return;
                }
                if (user.GrupoEconomico.Cancelado)
                {
                    msg.CriarMensagem("Não é possível realizar o login com este usuário, por favor contacte o administrador do sistema", "Informação", MsgIcons.Informacao);
                    return;
                }
                if (user.GrupoEconomico.GrupoTeste)
                {
                    if (DateTime.Now.ToMinHourOfDay() < user.GrupoEconomico.InicioTeste.ToMinHourOfDay() || DateTime.Now.ToMaxHourOfDay() > user.GrupoEconomico.FimTeste.ToMaxHourOfDay())
                    {
                        msg.CriarMensagem("O período de testes, deste grupo de teste, está expirado; por favor contacte o administrador do sistema", "Informação", MsgIcons.Informacao);
                        return;
                    }

                    if (user.GrupoEconomico.GetNumeroCNPJeCPFComMascara == "")
                    {
                        hfIdGrupo.Value = user.GrupoEconomico.Id.ToString();
                        modalDadosGrupo_extender.Show();
                        return;
                    }
                    else
                        Session["idEmp"] = user.GrupoEconomico.Id.ToString();
                }
                else
                    Session["idEmp"] = user.GrupoEconomico.Id.ToString();                
            }
            else
                Session["idEmp"] = null;

            if (Session["idEmp"] != null)
                this.SalvarAcesso(user);            

            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
            {
                transacao.Recarregar(ref msg);
                Response.Redirect(pagina);
            }
            else 
            {
                transacao.Recarregar(ref msg);
                Response.Redirect("../Site/Index.aspx");
            }
                
        }
        else
            msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente", "");
    }

    private void SalvarAcesso(Usuario user)
    {
        Acesso acesso = new Acesso();
        acesso.Data = DateTime.Now;
        acesso.Ip = Request.UserHostAddress;
        acesso.Usuario = user;
        acesso = acesso.Salvar();
    }

    #endregion
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {            
            transacao.Abrir();
            this.SalvarDadosGrupo();
            
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            MBOX1.Show(msg);
        }
    }

    private void SalvarDadosGrupo()
    {
        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfIdGrupo.Value.ToInt32());

        if (rbtnPessoaJuridica.Checked && !tbxCNPJ.Text.Trim().IsNotNullOrEmpty())
        {
            lblObrigatório.Text = "Informe um CNPJ para prosseguir.";
            div_obrigatorio.Visible = true;
            return;
        }

        if (rbtnPessoaJuridica.Checked && !tbxRazaoSocial.Text.Trim().IsNotNullOrEmpty())
        {
            lblObrigatório.Text = "Informe uma Razão Social para prosseguir.";
            div_obrigatorio.Visible = true;            
            return;
        }

        if (rbtnPessoaFisica.Checked && !tbxCPF.Text.Trim().IsNotNullOrEmpty())
        {
            lblObrigatório.Text = "Informe um CPF para prosseguir.";
            div_obrigatorio.Visible = true;               
            return;
        }

        if (rbtnPessoaJuridica.Checked && GrupoEconomico.ConsultarCnpjCpfJaCadastrado(grupo, tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", ""), 2))
        {
            lblObrigatório.Text = "Ja existe um grupo com este CNPJ cadastrado.";
            div_obrigatorio.Visible = true;            
            return;
        }

        if (rbtnPessoaFisica.Checked && GrupoEconomico.ConsultarCnpjCpfJaCadastrado(grupo, tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", ""), 1))
        {
            lblObrigatório.Text = "Ja existe um grupo com este CPF cadastrado.";
            div_obrigatorio.Visible = true;           
            return;
        }

        if (Validadores.ValidaCNPJ(tbxCNPJ.Text.Trim()) == false && rbtnPessoaJuridica.Checked)
        {
            lblObrigatório.Text = "CNPJ inválido.";
            div_obrigatorio.Visible = true;             
            return;
        }

        if (Validadores.ValidaCPF(tbxCPF.Text.Trim()) == false && rbtnPessoaFisica.Checked)
        {
            lblObrigatório.Text = "CPF inválido.";
            div_obrigatorio.Visible = true;            
            return;
        }

        div_obrigatorio.Visible = false;

        if (rbtnPessoaFisica.Checked)
        {
            DadosFisica df = new DadosFisica();
            df.Cpf = tbxCPF.Text.Trim().Replace(" ", "").Replace(".", "").Replace("-", "");
            df = df.Salvar();
            grupo.DadosPessoa = df;
        }
        else
        {
            DadosJuridica dj = new DadosJuridica();
            dj.Cnpj = tbxCNPJ.Text.Trim().Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");              
            dj.RazaoSocial = tbxRazaoSocial.Text.Trim();
            dj = dj.Salvar();
            grupo.DadosPessoa = dj;
        }

        grupo = grupo.Salvar();
        this.Acessar();

    }

    protected void btnLogin_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upDadosGrupo);
    }

    protected void rbtnPessoaFisica_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            tabelaPessoaFisica.Visible = true;
            tabelaPessoaJuridica.Visible = false;
            rbtnPessoaFisica.Checked = true;
            rbtnPessoaJuridica.Checked = false;
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            MBOX1.Show(msg);
        }
        
    }

    protected void rbtnPessoaJuridica_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            tabelaPessoaFisica.Visible = false;
            tabelaPessoaJuridica.Visible = true;
            rbtnPessoaFisica.Checked = false;
            rbtnPessoaJuridica.Checked = true;
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            MBOX1.Show(msg);
        }
        
    }
    
    
}