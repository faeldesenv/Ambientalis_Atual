using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class IGMP_IGPM : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["idEmp"] = 0;
                this.CarregarDatas();
                this.CarregarIGMPs(DateTime.Now.Year);
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

    private void CarregarDatas()
    {

        for (int i = 1; i <= 12; i++)
        {
            DateTime data = new DateTime(1, i, 1);
            ddlMes.Items.Add(new ListItem(data.ToString("MMMM"), i.ToString()));
        }

        int menorAno = DateTime.Now.Year - 10;

        for (int x = menorAno; x <= DateTime.Now.Year + 1; x++)
        {
            ddlAno.Items.Insert(0, new ListItem(x.ToString(), x.ToString()));
        }

        ddlAno.SelectedValue = DateTime.Now.Year.ToString();
    }

    private void CarregarIGMPs()
    {
        this.CarregarIGMPs(0);
    }

    private void CarregarIGMPs(int ano)
    {
        transacao.Abrir();

        IList<IGPMAcumulado> igpms = (ano > 0) ? IGPMAcumulado.FiltrarPorAno(ano) : IGPMAcumulado.ConsultarTodos();
        dgrValoresAcumuladosMes.DataSource = igpms;
        dgrValoresAcumuladosMes.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "<script>ValidaNumeros()</script>", false);

        transacao.Fechar(ref msg);
    }

    private void SalvarIGMP()
    {
        if (!String.IsNullOrEmpty(tbxValorAcumuladoMes.Text))
        {
            DateTime data = new DateTime(ddlAno.SelectedValue.ToInt32(), ddlMes.SelectedValue.ToInt32(), 1);
            IGPMAcumulado igpm = IGPMAcumulado.Filtrar(data);
            if (igpm == null)
                igpm = new IGPMAcumulado();
            else
            { // verificar se é possivel editar um IGPM, caso seja, retirar este else.
                msg.CriarMensagem("Não é permitido editar / sobrescrever um IGPM", "Atenção", MsgIcons.Alerta);
                return;
            }
            igpm.Valor = tbxValorAcumuladoMes.Text.ToDecimal();
            igpm.Data = data;
            igpm = igpm.Salvar();
            this.GerarComissaoVendas(igpm.Data, (igpm.GetValorIGPMAcumulados(igpm.Data) / 100));
            msg.CriarMensagem("IGMP salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
            transacao.Recarregar(ref msg);
            this.CarregarIGMPs(data.Year);
        }
        else
            msg.CriarMensagem("Campo 'Acumulado no Mês' é obrigatório", "Alerta", MsgIcons.Alerta);
        return;
    }

    private void GerarComissaoVendas(DateTime data, decimal valorIGMP)
    {
        IList<Venda> vendas = Venda.ConsultarPorMesAno(data);
        if (vendas != null && vendas.Count > 0)
        {
            foreach (Venda venda in vendas)
            {
                Venda.ReajustarNovasComissoes(venda, Session["IdConfig"].ToString().ToInt32(), valorIGMP);
            }
        }
    }

    private void EnviaEmailRevenda(Revenda revenda, decimal valorIGPM)
    {
        if (revenda != null)
        {
            this.EnviaEmail(revenda.Contato.Email, valorIGPM);
        }
    }

    private void EnviaEmailSupervisor(UsuarioSupervisorComercial user, decimal valorIGPM)
    {
        if (user != null)
            this.EnviaEmail(user.Email, valorIGPM);
    }

    private void EnviaEmail(string emailDestino, decimal valorIGPM)
    {
        Email email = new Email();
        email.AdicionarDestinatario(emailDestino);
        email.Assunto = "Reajuste do valor do IGPM - Sistema Sustentar";
        string mensagem = @"<div style='width:700px; height:auto; border-radius:10px; border:1px solid silver'>
            <div style='float:left; margin-left:20px; margin-top:20px;'><img src='http://sustentar.inf.br/imagens/logo_login.png'></div>
            <div style='float:left; margin-left:5px; font-family:arial; font-size:15px; font-weight:bold; margin-top:30px; text-align:center; width: 308px;'>
                Reajuste do valor do IGPM </div><div style='width:100%; height:20px; clear:both'></div>
            <div style='margin-left:20px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size:13px;padding:7px; background-color:#E9E9E9; height:auto' 
                align='left'><strong>Suas comissões sofreram um reajuste de: </strong>{0}%<br />
                <br />
             </div>
            <div style='width:100%; height:20px;'></div></div>";
        email.Mensagem = String.Format(mensagem, valorIGPM);
        if (!email.EnviarAutenticado(25, false))
            msg.CriarMensagem("Erro ao enviar email: " + email.Erro, "Atenção", MsgIcons.Informacao);
    }


    #endregion

    #region ___________Bindings____________

    public string BindMes(Object obj)
    {
        IGPMAcumulado igmp = (IGPMAcumulado)obj;
        string data = igmp.Data.ToString("MMM/yyyy");
        return data;
    }

    public string BindValorAcumulado(Object obj)
    {
        IGPMAcumulado igmp = (IGPMAcumulado)obj;
        return igmp.GetValorIGPMAcumulados(igmp.Data).ToString("N5");
    }

    #endregion

    #region ___________Eventos_____________

    protected void btnAdicionarComissao_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.SalvarIGMP();
            transacao.Fechar(ref msg);
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

    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarIGMPs(ddlAno.SelectedValue.ToInt32());            
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


    protected void btnAdicionarComissao_PreRender(object sender, EventArgs e)
    {
        string mensagem = "Confima a adição de um novo IGPM com o valor de {0}% em {1}/{2}?";
        WebUtil.AdicionarConfirmacao(((Button)sender), mensagem);
    }

    #endregion

    #region __________ Triggers ___________

    #endregion

}