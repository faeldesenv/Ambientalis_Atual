using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Usuario_ManterUsuarioADM : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = "0";
                transacao.Abrir();
                if (Session["UsuarioAdministradorLogado_SistemaAmbiental"] != null)
                {
                    Administrador user = (Administrador)Session["UsuarioAdministradorLogado_SistemaAmbiental"];
                    if (user != null)
                        this.CarregarAdministrador(user.Id);
                }
                else
                {
                    msg.CriarMensagem("Ocorreu um erro", "Erro", MsgIcons.Erro);
                    Response.Redirect("../Adm/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                this.GetMBOX<MBOX>().Show(msg);
            }
        }
    }

    #region ________ Eventos ___________
    
    protected void btnAlterarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                transacao.Abrir(0);
                this.AlterarSenha();
            }
            finally
            {
                transacao.Fechar(ref msg);
            }

            try
            {
                transacao.Abrir(1);
                this.AlterarSenha();
            }
            finally
            {
                transacao.Fechar(ref msg);
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

    #region ________ Métodos ___________

    private void CarregarAdministrador(int id)
    {
        Administrador adm = Administrador.ConsultarPorId(id);
        if (adm != null)
        {
            hfId.Value = adm.Id.ToString();
        }
    }
   
    private void AlterarSenha()
    {
        Administrador adm = Administrador.ConsultarPorId(hfId.Value.ToInt32());
        if (adm != null)
        {
            if (Utilitarios.Criptografia.Criptografia.Decrypt(adm.SenhaAtivacao.Trim(), true) != tbxSenhaAntiga.Text.Trim())
            {
                msg.CriarMensagem("A senha antiga está incorreta", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tbxNovaSenha.Text.Trim() != tbxConfirmarNovaSenha.Text.Trim())
            {
                msg.CriarMensagem("A confirmação da senha não corresponde à senha", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!SenhaAtendeOsPadroes(tbxNovaSenha.Text))
            {
                msg.CriarMensagem("A senha deve ter no mínimo 6 digitos, com no minímo 2 números e 2 letras", "Informação", MsgIcons.Exclamacao);
                return;
            }

            adm.SenhaAtivacao = Utilitarios.Criptografia.Criptografia.Encrypt(tbxNovaSenha.Text.Trim(), true);
            adm = adm.Salvar();
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
}