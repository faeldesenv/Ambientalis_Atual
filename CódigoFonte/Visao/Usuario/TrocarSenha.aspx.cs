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
                if (Session["UsuarioLogado_SistemaAmbiental"] != null)
                {
                    Usuario user = (Usuario)Session["UsuarioLogado_SistemaAmbiental"];
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

    private void CarregarUsuario(int id)
    {
        Usuario usuario = Usuario.ConsultarPorId(id);
        if (usuario != null)
        {
            hfId.Value = usuario.Id.ToString();
        }
    }

    private void Salvar()
    {
        Usuario user = Usuario.ConsultarPorId(hfId.Value.ToInt32());
        if (user != null)
        {
            if (Utilitarios.Criptografia.Criptografia.Decrypt(user.Senha.Trim(), true) != tbxSenhaAtual.Text.Trim())
            {
                msg.CriarMensagem("A senha antiga está incorreta", "Informação", MsgIcons.Informacao);
                return;
            }

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