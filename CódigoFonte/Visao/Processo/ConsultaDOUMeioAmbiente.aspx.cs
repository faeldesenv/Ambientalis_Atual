using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Data;

public partial class Processo_ConsultaDOUMeioAmbiente : PageBase
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

    #region ________Métodos_________

    private void CarregarGruposEconomicos()
    {
        ddlGrupoEconômico.DataTextField = "Nome";
        ddlGrupoEconômico.DataValueField = "Id";
        ddlGrupoEconômico.DataSource = GrupoEconomico.ConsultarGruposAtivos();
        ddlGrupoEconômico.DataBind();
        ddlGrupoEconômico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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
            ddlAnos.Items.Add((ano - i).ToString());
        }
    }

    private void CarregarEmpresa()
    {
        lbxEmpresas.Items.Clear();
        lbxProcessos.Items.Clear();

        GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconômico.SelectedValue.ToInt32());
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
        lbxEmpresas.Items.Clear();

        GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconômico.SelectedValue.ToInt32());
        if (c != null && c.Empresas != null)
        {
            foreach (Empresa empresa in c.Empresas)
            {
                foreach (Processo processo in empresa.Processos)
                {
                    if (processo.OrgaoAmbiental.GetType() == typeof(OrgaoFederal))
                    {
                        ListItem l = new ListItem(processo.Numero, processo.Id + "-" + processo.Empresa.Nome);
                        if (!lbxProcessos.Items.Contains(l))
                            lbxProcessos.Items.Add(l);
                    }
                }
            }
        }

        if (lbxProcessos.Items.Count <= 0)
        {
            lblresult.Text = "Não existe processos federais cadastrados para esta Empresa";
        }
        else
        {
            lblresult.Text = "";
        }
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

    private void Consultar()
    {
        if (tbxDiaAte.Text.ToInt32() <= 0 || tbxDiaDe.Text.ToInt32() <= 0 || tbxMesAte.Text.ToInt32() <= 0 || tbxMesDe.Text.ToInt32() <= 0 || tbxDiaDe.Text.ToInt32() > 31 || tbxDiaAte.Text.ToInt32() > 31 || tbxMesDe.Text.ToInt32() > 12 || tbxMesAte.Text.ToInt32() > 12 || tbxMesDe.Text.ToInt32() == 2 && tbxDiaDe.Text.ToInt32() > 28 || tbxMesAte.Text.ToInt32() == 2 && tbxDiaAte.Text.ToInt32() > 28)
        {
            msg.CriarMensagem("Insira valores válidos no campo Data de Publicação.", "Aleta", MsgIcons.Alerta);
            return;
        }

        if (!rbtnCnPJ.Checked && !rbtnNumeroProcesso.Checked)
        {
            msg.CriarMensagem("É necessario selecionar o tipo de pesquisa(CNPJ ou Número do Processo de Meio Ambiente), para prosseguir.", "Aleta", MsgIcons.Alerta);
            return;
        }

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        System.Net.WebClient c = new System.Net.WebClient();

        DataSet dataSet = this.CriarDataSet();

        ListItem item;

        if (rbtnCnPJ.Checked)
        {
            if (lbxEmpresas.GetSelectedIndices().Length > 0)
            {

                foreach (int indice in lbxEmpresas.GetSelectedIndices())
                {
                    //terminar esse metodo amnha
                    item = lbxEmpresas.Items[indice];
                    Empresa emp = Empresa.ConsultarPorId(item.Value.ToInt32());
                    string numeroCnpj = emp.GetNumeroCNPJeCPFComMascara;
                    string url = "http://pesquisa.in.gov.br/imprensa/core/consulta.action?edicao.txtPesquisa=" + numeroCnpj +
                    "&edicao.jornal_hidden=2&edicao.jornal=1,1000,1010,3,3000&edicao.dtInicio=" + tbxDiaDe.Text + "/" + tbxMesDe.Text +
                    "&edicao.dtFim=" + tbxDiaAte.Text + "/" + tbxMesAte.Text + "&edicao.ano=" + ddlAnos.SelectedValue;
                    string text = c.DownloadString(url);

                    DataRow row = dataSet.Tables[0].NewRow();
                    row["Processo"] = " - ";
                    row["Empresa"] = item.Text;
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

        }
        else
        {

            if (lbxProcessos.GetSelectedIndices().Length > 0)
            {
                foreach (int indice in lbxProcessos.GetSelectedIndices())
                {
                    item = lbxProcessos.Items[indice];
                    string numeroProcesso = item.Text;
                    string url = "http://pesquisa.in.gov.br/imprensa/core/consulta.action?edicao.txtPesquisa=" + numeroProcesso +
                        "&edicao.jornal_hidden=2&edicao.dtInicio=" + tbxDiaDe.Text + "/" + tbxMesDe.Text +
                        "&edicao.dtFim=" + tbxDiaAte.Text + "/" + tbxMesAte.Text + "&edicao.ano=" + ddlAnos.SelectedValue;
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
        }
        msg.CriarMensagem("ATENÇÃO: Os resultados das consultas são gerados pelo site da Imprensa Nacional não sendo de responsabilidade do Sistema SUSTENTAR.", "Atenção", MsgIcons.Informacao);

    }

    #endregion

    #region _________Eventos_________

    protected void ddlGrupoEconômico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconômico.SelectedIndex == 0)
            {
                lbxEmpresas.Items.Clear();
                lbxProcessos.Items.Clear();

            }

            if (rbtnCnPJ.Checked)
            {
                this.CarregarEmpresa();
            }
            else
            {
                this.Carregarprocessos();
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



    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconômico.SelectedIndex == 0)
            {
                msg.CriarMensagem("Selecione primeiro o grupo econômico para realizar a consulta", "Atênçao", MsgIcons.Alerta);
                return;
            }

            if (!rbtnCnPJ.Checked && !rbtnNumeroProcesso.Checked)
            {
                msg.CriarMensagem("Selecione o tipo de consulta (CNPJ ou Número do Processo Ambiental), para prosseguir", "Atênçao", MsgIcons.Alerta);
                return;
            }

            if (lbxEmpresas.GetSelectedIndices().Length <= 0 && lbxProcessos.GetSelectedIndices().Length <= 0)
            {
                msg.CriarMensagem("Selecione primeiro a(s) empresa(s) ou processo(s) para realizar a consulta", "Atênçao", MsgIcons.Alerta);
                return;
            }

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

    #endregion

    protected void rbtnNumeroProcesso_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (rbtnNumeroProcesso.Checked)
            {
                this.Carregarprocessos();
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


    protected void rbtnCnPJ_CheckedChanged(object sender, EventArgs e)
    {

        try
        {

            if (rbtnCnPJ.Checked)
            {
                this.CarregarEmpresa();
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
}