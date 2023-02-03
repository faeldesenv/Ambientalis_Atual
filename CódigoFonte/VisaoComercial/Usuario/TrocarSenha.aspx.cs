using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Usuario_TrocarSenha : PageBase
{
    Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (Session["UsuarioLogado_SistemaComercial"] != null)
                {
                    UsuarioComercial user = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"];
                    if (user != null)
                        this.CarregarUsuario(user.Id);
                }
                else
                {
                    msg.CriarMensagem("Ocorreu um erro", "Erro", MsgIcons.Erro);
                    Response.Redirect("../Acesso/Login.aspx");
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

    #region _________Métodos__________

    /// <summary>
    /// Caso o usuário seja supervisor, o campo de seleção de revendas é habilitado e as revendas são carregadas
    /// </summary>
    /// <param name="supervisor">Booleano que indica se o usuário é supervisor</param>
    private void VerificaUsuario(bool supervisor)
    {
        pnlCamposSupervisor.Visible = supervisor;
        if (supervisor)
        {
            rbtnSupervisor.Checked = true;
            this.CarregarRevendas();
        }
    }

    private void CarregarUsuario(int id)
    {
        UsuarioComercial usuario = UsuarioComercial.ConsultarPorId(id);
        if (usuario != null)
        {
            // Caso o usuário seja Supervisor é retornado true
            this.VerificaUsuario(usuario is UsuarioSupervisorComercial);
            hfId.Value = usuario.Id.ToString();
        }
    }

    private void CarregarRevendas()
    {
        IList<Revenda> revendas = Revenda.ConsultarTodos();
        ddlRevenda.DataValueField = "Id";
        ddlRevenda.DataTextField = "Nome";
        ddlRevenda.DataSource = revendas != null ? revendas : new List<Revenda>();
        ddlRevenda.DataBind();
        ddlRevenda.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void Salvar()
    {
        if (rbtnRevendas.Checked)
        {
            if (ddlRevenda.SelectedValue.ToInt32() > 0)
            {
                Revenda revenda = Revenda.ConsultarPorId(ddlRevenda.SelectedValue.ToInt32());
                if (revenda != null)
                    this.SalvarUsuario(revenda.UsuarioRevenda);
            }
            else
                msg.CriarMensagem("Selecione uma Revenda", "Alerta", MsgIcons.Alerta);
        }
        else 
            this.SalvarUsuario(UsuarioComercial.ConsultarPorId(hfId.Value.ToInt32()));

    }

    private void SalvarUsuario(UsuarioComercial user)
    {
        if (user != null)
        {
            //if (Utilitarios.Criptografia.Criptografia.Decrypt(user.Senha.Trim(), true) != tbxSenhaAtual.Text.Trim())
            //{
            //    msg.CriarMensagem("A senha antiga está incorreta", "Informação", MsgIcons.Informacao);
            //    return;
            //}

            if (tbxNovaSenha.Text.Trim() != tbxConfirmaNovaSenha.Text.Trim())
            {
                msg.CriarMensagem("A confirmação da senha não corresponde à senha", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!SenhaAtendeOsPadroes(tbxNovaSenha.Text))
            {
                msg.CriarMensagem("A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
                return;
            }

            user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxNovaSenha.Text.Trim(), true);
            user = user.Salvar();
            msg.CriarMensagem("Senha alterada com sucesso", "Sucesso", MsgIcons.Sucesso);
        }
        else
            msg.CriarMensagem("Erro ao alterar senha", "Erro", MsgIcons.Erro);
    }

    private bool SenhaAtendeOsPadroes(string senha)
    {
        int contadorNumeros = 0;
        int contadorLetras = 0;
        String aux = "";
        if (senha.Length < 6)
            return false;

        foreach (Char letra in senha)
        {
            aux = letra.ToString();
            if (aux.IsInt32())
                contadorNumeros = contadorNumeros + 1;
            if (aux.IsLetra())
                contadorLetras = contadorLetras + 1;
        }
        if (contadorNumeros >= 2 && contadorLetras >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SugestaoSenha()
    {
        string senha = this.GerarSenha();
        while (!this.SenhaAtendeOsPadroes(senha))
            senha = this.GerarSenha();
        //tbxSugestaoSenha.Text = senha;
    }

    private string GerarSenha()
    {
        string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string senha = "";
        int quantDigitos = 8;
        Random random = new Random();
        for (int i = 0; i < quantDigitos; i++)
        {
            if (random.Next(2) == 1)
                senha += random.Next(0, 10).ToString();
            else
                senha += caracteres[random.Next(0, caracteres.Length)];
        }
        return senha;
    }

    #endregion

    #region __________Eventos_________

    protected void btnAlterarSenha_Click(object sender, EventArgs e)
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

    #endregion
}