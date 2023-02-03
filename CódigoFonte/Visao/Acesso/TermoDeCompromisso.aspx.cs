using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Acesso_TermoDeCompromisso : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UsuarioLogado_SistemaAmbiental"] == null)
            Response.Redirect("Login.aspx");
        if (!IsPostBack)
        {
            string id = Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", Request);
            try
            {
                transacao.Abrir();
                this.IniciarTela(id);
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                if (msg.Mensagem != null)
                    Alert.Show(msg.Mensagem);
            }
        }
    }

    private void IniciarTela(string id)
    {
        if (id == null)
        {
            chkAceite.Enabled = btnAceito.Enabled = btnNaoAceito.Enabled = false;
            msg.CriarMensagem("O Grupo Econômico não corresponde a um grupo econômico válido", "Informação", MsgIcons.Informacao);
        }

        GrupoEconomico aux = GrupoEconomico.ConsultarPorId(id.ToInt32());
        if (aux == null)
        {
            msg.CriarMensagem("O Grupo econômico não corresponde a um grupo econômico válido, por favor tente novamente. Se o problema persistir contacte o administrador do sistema", "Informação", MsgIcons.Informacao);
            return;
        }
        if (aux.TermoAceito)
        {
            Response.Redirect("../Site/Index.aspx");
            return;
        }

        string termoCompromisso = Setup.GetValor(Setup.TermoCompromisso);
        if (string.IsNullOrEmpty(termoCompromisso))
        {
            chkAceite.Enabled = btnAceito.Enabled = btnNaoAceito.Enabled = false;
            msg.CriarMensagem("O termo de compromisso ainda não foi definido", "Termo de Compromisso", MsgIcons.Informacao);
            return;
        }
        this.lblTermoCompromisso.Text = termoCompromisso;
        hfIdGrupoEconomico.Value = id;
    }


    #region _________________ Eventos ______________________

    protected void btnAceito_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.AceitarTermoCompromisso();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            if (msg.Mensagem != null)
                Alert.Show(msg.Mensagem);
        }
    }

    protected void btnNaoAceito_Click(object sender, EventArgs e)
    {
        try
        {
            this.RejeitarTermoCompromisso();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            if (msg.Mensagem != null)
                Alert.Show(msg.Mensagem);
        }
    }

    #endregion

    #region _________________ Métodos ______________________

    private void AceitarTermoCompromisso()
    {
        if (!chkAceite.Checked)
        {
            msg.CriarMensagem("Deve-se aceitar o termo de compromisso para aceitá-lo", "Informação", MsgIcons.Informacao);
            return;
        }

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(hfIdGrupoEconomico.Value.ToInt32());

        if (grupo == null)
        {
            msg.CriarMensagem("O Grupo Econômico não é válido, por favor tente novamente", "Informação", MsgIcons.Informacao);
            chkAceite.Checked = false;
            return;
        }

        grupo.TermoAceito = true;
        grupo.Salvar();
        msg.CriarMensagem("Termo de Compromisso aceito", "Informação", MsgIcons.Informacao);
        Response.Redirect("../Site/Index.aspx");
    }

    private void RejeitarTermoCompromisso()
    {
        Response.Redirect("Login.aspx");
    }

    #endregion

}