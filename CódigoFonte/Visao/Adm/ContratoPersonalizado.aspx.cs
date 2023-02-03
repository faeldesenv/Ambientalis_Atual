using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

public partial class Comercial_ContratoPersonalizado : PageBase
{
    private Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEnviarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkDnpm.Checked == false && chkMeioAmbiente.Checked == false)
            {
                msg.CriarMensagem("Selecione ao menos um módulo para prosseguir.", "Atenção", MsgIcons.Alerta);
                return;
            }

            if (Validadores.ValidaEmail(tbxEmail.Text))
            {
                String mesesCarenciaEmail = "";
                String mesesCarencia = "0";
                if (rbtnCarenciaSim.Checked == true) {
                    if (tbxQuantidadeMesesCarencia.Text != null && tbxQuantidadeMesesCarencia.Text != "" && Validadores.IsNumeric(tbxQuantidadeMesesCarencia.Text))
                    {
                        NumeroPorExtenso numero = new NumeroPorExtenso();
                        numero.SetNumero(tbxQuantidadeMesesCarencia.Text.ToDecimal());
                        mesesCarencia = tbxQuantidadeMesesCarencia.Text + " (" + numero.ToStringNumero() + ")";
                        if (tbxQuantidadeMesesCarencia.Text.ToInt32() != 1)
                        {
                            mesesCarenciaEmail = "<tr><td align='right' width='50%' style='font-weight:bold'>Quantidade de Meses de Carência:</td><td align='left' width='50%'>" + tbxQuantidadeMesesCarencia.Text + " meses.</td></tr>";

                        }
                        else {
                            mesesCarenciaEmail = "<tr><td align='right' width='50%' style='font-weight:bold'>Quantidade de Meses de Carência:</td><td align='left' width='50%'>" + tbxQuantidadeMesesCarencia.Text + "mês.</td></tr>";

                        }
                        
                    }
                    else
                    {
                        msg.CriarMensagem("Digite um número válido para a Quantidade de Meses de Carência.", "Erro", MsgIcons.Erro);
                        return;
                    }
                }
                if ((rblModalidade.SelectedValue == "E" ? Validadores.IsNumeric(tbxQuantidadeEmpresas.Text) : Validadores.IsNumeric(tbxQuantidadeProcessos.Text)) && Validadores.IsNumeric(tbxQuantidadeUsuarios.Text) && Validadores.IsNumeric(tbxValorMensalidade.Text))
                {
                    Email email = new Email();
                    email.Assunto = "Solicitação de Cadastro - Sistema Sustentar";
                    email.EmailsDestino.Add(tbxEmail.Text);                    
                    string modulosContratados = "";
                    if (chkDnpm.Checked && chkMeioAmbiente.Checked)
                        modulosContratados = "ANM e Meio Ambiente";
                    if (!chkDnpm.Checked && chkMeioAmbiente.Checked)
                        modulosContratados = "Meio Ambiente";
                    if (chkDnpm.Checked && !chkMeioAmbiente.Checked)
                        modulosContratados = "ANM";

                    String dadosContrato = Utilitarios.Criptografia.Seguranca.MontarParametros("cont=cp&tipoContratacao=" + rblModalidade.SelectedValue + "&qtdProcessos=" + tbxQuantidadeProcessos.Text.ToInt32() + "&qtdEmpresas=" + tbxQuantidadeEmpresas.Text.ToInt32() + "&qtdUsuarios=" + tbxQuantidadeUsuarios.Text + "&valorMensalidade=" + tbxValorMensalidade.Text + "&mesesCarencia=" + mesesCarencia + "&carencia=sim" + "&moduloContratados=" + modulosContratados);
                    String mensagemEmail = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:85px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px; text-align:center;'>Solicitação de Cadastro<br/>Sistema Sustentar<br/>Condições Especiais</div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:14px;padding:7px; background-color:#E9E9E9; text-align:center; height:auto'>Você solicitou um cadastro no Sistema Sustentar, para concluir o cadastro verifique as informações abaixo. 
    
              </div>
            <table style='width:100%; margin-top:10px; height:auto;font-family:Arial, Helvetica, sans-serif; font-size:14px;'><tbody><tr>
            <td align='right' width='50%' style='font-weight:bold'>" + (rblModalidade.SelectedValue == "E" ? "Quantidade de Empresas e/ou Filiais:" : "Quantidade de Processos:") + @"</td><td align='left' width='50%'>" + ((rblModalidade.SelectedValue == "E" ? tbxQuantidadeEmpresas.Text : tbxQuantidadeProcessos.Text)) + @"</td></tr><tr><td align='right' width='50%' style='font-weight:bold'>Quantidade de Úsuarios:
              </td><td align='left' width='50%'>" + tbxQuantidadeUsuarios.Text + @"</td></tr><tr><td align='right' style='font-weight:bold'>Módulo(s) Contratado(s):</td><td align='left'>" + modulosContratados + @"</td>
            </tr><tr><td align='right' width='50%' style='font-weight:bold'>VALOR TOTAL DA MENSALIDADE:</td>
            <td align='left' width='50%'>R$ " + tbxValorMensalidade.Text + @"</td></tr>" + mesesCarenciaEmail +@"<tr>
            <td colspan='2' align='center' height='40px' valign='middle'><a target='_blank' href='http://sustentar.inf.br/site/website/Site/Cadastro.aspx" + dadosContrato + @"'>Conclua o seu cadastro.</a></td></tr></tbody></table><div style='width:100%; height:20px;'></div></div>";
                    email.Mensagem = mensagemEmail;
                    if (email.EnviarAutenticado(25, false))
                    {
                        msg.CriarMensagem("E-mail enviado com sucesso.", "Sucesso");
                        tbxEmail.Text = null;
                        tbxQuantidadeEmpresas.Text = null;
                        tbxQuantidadeUsuarios.Text = null;
                        tbxValorMensalidade.Text = null;
                        rbtnCarenciaNao.Checked = true;
                        tbxQuantidadeMesesCarencia.Text = null;
                    }
                    else
                    {
                        msg.CriarMensagem("Erro ao enviar e-mail.", "Erro", MsgIcons.Erro);
                    }
                }
                else
                {
                    msg.CriarMensagem("Dados inválidos. Preencha os dados corretamente.", "Erro", MsgIcons.Erro);
                }
            }
            else
            {
                msg.CriarMensagem("E-mail inválido.", "Erro", MsgIcons.Erro);
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally {
            this.GetMBOX<MBOX>().Show(msg);

        }
        
    }

    protected void rblModalidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblModalidade.SelectedIndex == 0)
        {
            tbxQuantidadeEmpresas.Enabled = true;
            tbxQuantidadeProcessos.Text = "";
            tbxQuantidadeProcessos.Enabled = false;
        }
        else
        {
            tbxQuantidadeEmpresas.Text = "";
            tbxQuantidadeEmpresas.Enabled = false;
            tbxQuantidadeProcessos.Enabled = true;
        }
    }
}