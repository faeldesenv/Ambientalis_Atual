using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Data;

public partial class Site_ConsultaDNPM : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.CarregarGruposEconomicos();
                this.CarregarAnos();
                this.CarregarDias();
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
    }

    private void CarregarDias()
    {
        tbxDiaAte.Text = (DateTime.Now.Day).ToString().Length == 1 ? ("0" + (DateTime.Now.Day).ToString()) : (DateTime.Now.Day).ToString();
        tbxDiaDe.Text = (DateTime.Now.Day - 1).ToString().Length == 1 ? ("0" + (DateTime.Now.Day - 1).ToString()) : (DateTime.Now.Day - 1).ToString();

        tbxMesAte.Text = (DateTime.Now.Month).ToString().Length == 1 ? ("0" + (DateTime.Now.Month).ToString()) : (DateTime.Now.Month).ToString();
        tbxMesDe.Text = (DateTime.Now.Month).ToString().Length == 1 ? ("0" + (DateTime.Now.Month).ToString()) : (DateTime.Now.Month).ToString();
    }

    private void CarregarAnos()
    {
        int ano = DateTime.Now.Year;

        for (int i = 0; i <= 20; i++)
        {
            ddlAnosDe.Items.Add((ano - i).ToString());
        }
    }

    #region ___________Metodos_____________

    private void CarregarEmpresa()
    {
        lbxEmpresas.Items.Clear();
        lbxProcessos.Items.Clear();

        GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
        if (c != null && c.Empresas != null)
        {
            foreach (Empresa emp in c.Empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    lbxEmpresas.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    lbxEmpresas.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void Carregarprocessos()
    {
        lbxProcessos.Items.Clear();
        foreach (int n in lbxEmpresas.GetSelectedIndices())
        {
            foreach (ProcessoDNPM processo in Empresa.ConsultarPorId(lbxEmpresas.Items[n].Value.ToInt32()).ProcessosDNPM)
            {
                ListItem l = new ListItem(processo.GetNumeroProcessoComMascara, processo.Id + "-" + processo.Empresa.Nome);
                if (!lbxProcessos.Items.Contains(l))
                    lbxProcessos.Items.Add(l);
            }
        }

    }

    private void CarregarGruposEconomicos()
    {
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataSource = GrupoEconomico.ConsultarGruposAtivos();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void Consultar()
    {
        if (tbxDiaAte.Text.ToInt32() <= 0 || tbxDiaDe.Text.ToInt32() <= 0 || tbxMesAte.Text.ToInt32() <= 0 || tbxMesDe.Text.ToInt32() <= 0 || tbxDiaDe.Text.ToInt32() > 31 || tbxDiaAte.Text.ToInt32() > 31 || tbxMesDe.Text.ToInt32() > 12 || tbxMesAte.Text.ToInt32() > 12 || tbxMesDe.Text.ToInt32() == 2 && tbxDiaDe.Text.ToInt32() > 28 || tbxMesAte.Text.ToInt32() == 2 && tbxDiaAte.Text.ToInt32() > 28)
        {
            msg.CriarMensagem("Insira valores válidos no campo Data de Publicação.", "Aleta", MsgIcons.Alerta);
            return;
        }

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        System.Net.WebClient c = new System.Net.WebClient();

        DataSet dataSet = this.CriarDataSet();


        ListItem item;
        if (lbxProcessos.GetSelectedIndices().Length > 0)
        {
            foreach (int indice in lbxProcessos.GetSelectedIndices())
            {
                item = lbxProcessos.Items[indice];
                string numeroProcesso = item.Text.Remove(7);
                string url = "http://pesquisa.in.gov.br/imprensa/core/consulta.action?edicao.txtPesquisa=" + numeroProcesso +
                    "&edicao.jornal_hidden=2&edicao.jornal=1,1000,1010,3,3000&edicao.dtInicio=" + tbxDiaDe.Text + "/" + tbxMesDe.Text +
                    "&edicao.dtFim=" + tbxDiaAte.Text + "/" + tbxMesAte.Text + "&edicao.ano=" + ddlAnosDe.SelectedValue;
                string text = c.DownloadString(url);


                DataRow row = dataSet.Tables[0].NewRow();
                row["Processo"] = item.Text;
                row["Empresa"] = item.Value.Split('-')[1].ToString();
                row["Link"] = url;

                if (text.Contains("Um item encontrado"))
                {
                    row["Ocorrencia"] = "1(uma) ocorrência Encontrada.";
                }
                else if (text.Contains("itens encontrados"))
                {
                    int i = 1;
                    while (true)
                    {
                        string caracter = text.Substring(text.LastIndexOf("itens encontrados") - i, 1);
                        if (caracter == ">")
                            break;
                        i++;

                        if (i > 50)
                            break;
                    }
                    i = i - 1;
                    row["Ocorrencia"] = text.Substring(text.LastIndexOf("itens encontrados") - i, i) + " ocorrências Encontrados.";
                }
                else
                {
                    row["Ocorrencia"] = "Nenhuma Ocorrencia";
                }

                dataSet.Tables[0].Rows.Add(row);
            }
        }

        dgr.DataSource = dataSet.Tables[0];
        dgr.DataBind();
        msg.CriarMensagem("ATENÇÃO: Os resultados das consultas são gerados pelo site da Imprensa Nacional não sendo de responsabilidade do Sistema SUSTENTAR.<br/> * Por limitações no site do DOU, as consultas são realizadas apenas pelos primeiros números do processo, podendo consultar processos de outros anos que não são da empresa selecionada", "Atenção", MsgIcons.Informacao);

    }

    private DataSet CriarDataSet()
    {
        DataSet dt = new DataSet();
        dt.Tables.Add(new DataTable("Ocorrencias"));

        dt.Tables[0].Columns.Add("Processo");
        dt.Tables[0].Columns.Add("Empresa");
        dt.Tables[0].Columns.Add("Ocorrencia");
        dt.Tables[0].Columns.Add("Link");
        return dt;
    }
    #endregion

    #region ___________Eventos_____________

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresa();
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

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Consultar();
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

    protected void lbxEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            this.Carregarprocessos();
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