using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios.Criptografia;
using Modelo;
using Utilitarios;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

public partial class Notificacao_AtualizarEventos : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    private int[] contadorEmails = new int[8];

    public Dictionary<string, StringBuilder> dic = new Dictionary<string, StringBuilder>();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (TestarParametros())
            {
                Session["idConfig"] = Request["idConfig"].ToString();
                int de = Request["de"].ToInt32();
                int ate = Request["ate"].ToInt32();

                if (Request["idEmp"] != null)
                    Session["idEmp"] = Request["idEmp"];
                else
                    Session["idEmp"] = null;

                transacao.Abrir();
                this.AtualizarEventosDNPM(de, ate);
            }
            else
            {
                Email mail = new Email();
                mail.Assunto = "ERRO ao atualizar Eventos ANM (" + DateTime.Now + ")";
                mail.BodyHtml = true;
                mail.Mensagem = lblResult.Text;
                mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
                mail.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString()));
            }
        }
        catch (Exception ex)
        {
            lblResult.Text = DateTime.Now + " - " + ex.Message;
            Email mail = new Email();
            mail.Assunto = "ERRO ao atualizar Eventos ANM (" + DateTime.Now + ") - Base: " + Request["idConfig"].ToString();
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + " - " + ex.InnerException;
            mail.EmailsDestino.Add("notificacao@sustentar.inf.br");
            mail.EnviarAutenticado(ConfigurationManager.AppSettings["portaEnvio"].ToInt32(), Convert.ToBoolean(ConfigurationManager.AppSettings["usarSSL"].ToString()));
        }
        finally
        {
            transacao.Fechar(ref msg);
        }



    }

    private bool TestarParametros()
    {
        if (Request["idConfig"] == null)
        {
            lblResult.Text += "Parametro idConfig NULL <br/>";
            return false;
        }

        if (Request["de"] == null)
        {
            lblResult.Text += "Parametro de: NULL <br/>";
            return false;
        }

        if (Request["ate"] == null)
        {
            lblResult.Text += "Parametro até: NULL <br/>";
            return false;
        }

        return true;
    }

    private void AtualizarEventosDNPM(int de, int ate)
    {
        Session["idEmp"] = null;
        IList<ProcessoDNPM> lista = ProcessoDNPM.FiltrarEnternalo(de, ate);
        string log = "";

        log += "Processos Consultados: " + lista.Count + "<br/><br/>";

        foreach (ProcessoDNPM processo in lista)
        {
            try
            {
                Session["idEmp"] = processo.Emp;
                this.AtualizarProcesso(processo);
                log += DateTime.Now + " [SUCESSO] Processo: " + processo.Id + "<br/>";
            }
            catch (Exception ex)
            {
                log += DateTime.Now + "[ERRO] Processo: " + processo.Id + "ERRO: " + ex.Message + "<br/>";
            }
        }

        log += "FIM<br/><br/>";
        lblResult.Text = log;
    }


    private void AtualizarProcesso(ProcessoDNPM processoDNPM)
    {
        ProcessoDNPM processo = ProcessoDNPM.ConsultarPorId(processoDNPM.Id);

        /* Adicionando Handler pro evento ServerCertificateValidationCallback 
         * que chama a função CustomValidation*/
        ServicePointManager.ServerCertificateValidationCallback += new
        System.Net.Security.RemoteCertificateValidationCallback(CustomValidation);

        string url = "https://sistemas.dnpm.gov.br/SCM/Extra/site/admin/dadosProcesso.aspx" + this.GetParametrosDNPM(processo.Numero);

        //baixando codigo fonte do site e decodificando
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "GET";
        WebResponse myResponse = myRequest.GetResponse();
        StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
        string source = sr.ReadToEnd();
        sr.Close();
        myResponse.Close();

        String h1Regex = "<td[^>]*?>(?<TagText>.*?)</td>";

        //Atualizando a cidade e estado
        string sourceMunicipios = source.Substring(source.IndexOf("Municípios"), (source.IndexOf("Condição de propriedade do solo") - source.IndexOf("Municípios")));

        MatchCollection mcMunicipio = Regex.Matches(this.GetTags(sourceMunicipios), h1Regex, RegexOptions.Singleline);

        string municioEstado = mcMunicipio[0].Groups["TagText"].Value.Replace("\n", "").Replace("\r", "").Trim();

        string municio = municioEstado.Split('/')[0].Replace("\n", "").Replace("\r", "").Replace("  ", " ").Trim();

        Estado estado = Estado.ConsultarPorId(this.RetornoNomeEstadoPelaSigla(municioEstado.Split('/')[1].Replace("\n", "").Replace("\r", "").Replace("  ", " ").Trim()));

        Cidade cidade = Cidade.ConsultarPorNome(municio, estado);

        if (cidade != null && cidade.Id > 0)
            processo.Cidade = cidade;


        //atualizando regimes e eventos
        source = source.Substring(source.IndexOf("Eventos"));

        MatchCollection mc = Regex.Matches(this.GetTags(source), h1Regex, RegexOptions.Singleline);

        int idLicenciamento = 0;
        int idExtracao = 0;
        int idGuia = 0;
        int idRal = 0;

        for (int i = mc.Count - 1; i > -1; i -= 2)
        {
            EventoDNPM evento = new EventoDNPM();
            evento.Data = mc[i].Groups["TagText"].Value.Replace("\n", "").Replace("\r", "").Trim().ToDateTime();
            evento.Descricao = mc[i - 1].Groups["TagText"].Value.Replace("\n", "").Replace("\r", "").Trim();

            string codigoEvento = evento.Descricao.Split('-')[0].Replace("  ", " ").Replace(" ", "").Trim();
            string descricaoEvento = evento.Descricao.Split('-')[1].Replace("  ", " ").Replace(" ", "").Trim();

            if (codigoEvento.ToInt32() > 0)
            {
                //Requerimento de Pesquisa - codigo 100 ou codigo 1274
                if (codigoEvento.ToInt32() == 100 || codigoEvento.ToInt32() == 1274)
                {
                    if (!RequerimentoPesquisa.ConsultarPorProcessoEDataDeRequerimento(processo.Id, evento.Data))
                    {
                        RequerimentoPesquisa requerimentoPesquisa = new RequerimentoPesquisa();
                        requerimentoPesquisa.DataRequerimento = evento.Data;
                        requerimentoPesquisa.ProcessoDNPM = processo;

                        requerimentoPesquisa = requerimentoPesquisa.Salvar();

                        evento.Atualizado = true;
                    }

                }


                //Requerimento de Lavra - codigo 350 ou 1275
                if (codigoEvento.ToInt32() == 350 || codigoEvento.ToInt32() == 1275)
                {
                    if (!RequerimentoLavra.ConsultarPorProcessoEData(processo.Id, evento.Data))
                    {
                        RequerimentoLavra requerimentoLavra = new RequerimentoLavra();
                        requerimentoLavra.Data = evento.Data;
                        requerimentoLavra.ProcessoDNPM = processo;

                        requerimentoLavra = requerimentoLavra.Salvar();

                        evento.Atualizado = true;
                    }

                }

                //Alvará de Pesquisa - codigo 322 ou codigo 323 ou codigo 321
                if (codigoEvento.ToInt32() == 322 || codigoEvento.ToInt32() == 323 || codigoEvento.ToInt32() == 321)
                {
                    if (!AlvaraPesquisa.ConsultarPorProcessoEDataDePublicacao(processo.Id, evento.Data))
                    {
                        AlvaraPesquisa alvaraPesquisa = new AlvaraPesquisa();
                        alvaraPesquisa.DataPublicacao = evento.Data;

                        alvaraPesquisa.AnosValidade = codigoEvento.ToInt32() == 322 ? 2 : codigoEvento.ToInt32() == 323 ? 3 : 1;

                        Vencimento vencimento = new Vencimento();
                        vencimento.Data = evento.Data.AddYears(+alvaraPesquisa.AnosValidade);

                        vencimento = vencimento.Salvar();

                        alvaraPesquisa.Vencimento = vencimento;

                        alvaraPesquisa.ProcessoDNPM = processo;

                        alvaraPesquisa = alvaraPesquisa.Salvar();

                        //Vencimentos do Alvara  

                        //LIMITE RENUNCIA
                        alvaraPesquisa.LimiteRenuncia = new Vencimento();
                        Vencimento limiteRenuncia = new Vencimento();
                        if (alvaraPesquisa.LimiteRenuncia != null)
                            limiteRenuncia = alvaraPesquisa.LimiteRenuncia;

                        limiteRenuncia.Data = alvaraPesquisa.DataPublicacao.AddMonths((alvaraPesquisa.AnosValidade * 12) / 3);
                        alvaraPesquisa.LimiteRenuncia = limiteRenuncia.Salvar();

                        //DIPEM
                        alvaraPesquisa.DIPEM = new List<Vencimento>();
                        Vencimento v2 = new Vencimento();
                        v2.Data = ("30/04/" + (alvaraPesquisa.DataPublicacao.Year + 1)).ToSqlDateTime();
                        v2.Status = Status.ConsultarPorId(1);
                        v2 = v2.Salvar();

                        alvaraPesquisa.DIPEM.Add(v2);


                        // TAXA ANUAL HECTARE
                        alvaraPesquisa.TaxaAnualPorHectare = new List<Vencimento>();
                        Vencimento v = new Vencimento();
                        v.Data = this.CalcularTaxaHectare(alvaraPesquisa.DataPublicacao);
                        v.Status = Status.ConsultarPorId(1);
                        v = v.Salvar();

                        alvaraPesquisa.TaxaAnualPorHectare.Add(v);

                        evento.Atualizado = true;
                    }

                }

                //Alvará de Pesquisa sem vencimento codigo 176
                if (codigoEvento.ToInt32() == 176)
                {
                    if (!AlvaraPesquisa.ConsultarPorProcessoEDataDePublicacao(processo.Id, evento.Data))
                    {
                        AlvaraPesquisa alvaraPesquisa = new AlvaraPesquisa();
                        alvaraPesquisa.DataPublicacao = evento.Data;

                        alvaraPesquisa.ProcessoDNPM = processo;

                        alvaraPesquisa = alvaraPesquisa.Salvar();

                        evento.Atualizado = true;
                    }

                }

                //Concessão de Lavra - codigo 1278 ou 400
                if (codigoEvento.ToInt32() == 1278 || codigoEvento.ToInt32() == 400 || codigoEvento.ToInt32() == 481)
                {
                    if (!ConcessaoLavra.ConsultarPorProcessoEData(processo.Id, evento.Data))
                    {
                        ConcessaoLavra concessaoLavra = new ConcessaoLavra();
                        concessaoLavra.Data = evento.Data;
                        concessaoLavra.ProcessoDNPM = processo;

                        concessaoLavra = concessaoLavra.Salvar();

                        //se nao existir ral criar um
                        if (processo != null)
                        {
                            RAL ral;

                            if (idRal > 0)
                                ral = RAL.ConsultarPorId(idRal);
                            else
                            {
                                if (processo.RAL != null)
                                    ral = processo.RAL;
                                else
                                    ral = new RAL();
                            }


                            ral.ProcessoDNPM = processo;

                            if (ral.Vencimentos == null)
                                ral.Vencimentos = new List<Vencimento>();

                            Vencimento v = new Vencimento();
                            v.Status = Status.ConsultarPorId(1);
                            v.Data = new DateTime(evento.Data.AddYears(1).Year, 3, 15);
                            ral.Vencimentos.Add(v.Salvar());
                            ral = ral.Salvar();
                            idRal = ral.Id;
                        }

                        evento.Atualizado = true;
                    }

                }


                //Guia de Utilização - codigo 624
                if (codigoEvento.ToInt32() == 624 || codigoEvento.ToInt32() == 283)
                {
                    if (!GuiaUtilizacao.ConsultarPorProcessoEDataDeRequerimento(processo.Id, evento.Data))
                    {
                        idGuia = 0;
                        GuiaUtilizacao guiaUtilizacao = new GuiaUtilizacao();
                        guiaUtilizacao.DataRequerimento = evento.Data;
                        guiaUtilizacao.ProcessoDNPM = processo;

                        guiaUtilizacao = guiaUtilizacao.Salvar();
                        idGuia = guiaUtilizacao.Id;

                        evento.Atualizado = true;
                    }
                }

                //Data Emissao da guia de utilização
                if (codigoEvento.ToInt32() == 625 || codigoEvento.ToInt32() == 285)
                {
                    if (idGuia > 0)
                    {
                        GuiaUtilizacao guiaUtilizacao = GuiaUtilizacao.ConsultarPorId(idGuia);

                        if (guiaUtilizacao != null)
                        {
                            guiaUtilizacao.DataEmissao = evento.Data;
                            guiaUtilizacao.ProcessoDNPM = processo;

                            guiaUtilizacao = guiaUtilizacao.Salvar();

                            idGuia = guiaUtilizacao.Id;

                            //se nao existir ral criar um
                            if (processo != null)
                            {
                                RAL ral;

                                if (idRal > 0)
                                    ral = RAL.ConsultarPorId(idRal);
                                else
                                {
                                    if (processo.RAL != null)
                                        ral = processo.RAL;
                                    else
                                        ral = new RAL();
                                }

                                ral.ProcessoDNPM = processo;

                                if (ral.Vencimentos == null)
                                    ral.Vencimentos = new List<Vencimento>();

                                Vencimento v = new Vencimento();
                                v.Status = Status.ConsultarPorId(1);
                                v.Data = new DateTime(evento.Data.AddYears(1).Year, 3, 15);
                                ral.Vencimentos.Add(v.Salvar());
                                ral = ral.Salvar();
                                idRal = ral.Id;
                            }

                            evento.Atualizado = true;
                        }
                    }
                }


                //Licenciamento - codigo 700 ou codigo 787
                if (codigoEvento.ToInt32() == 700 || codigoEvento.ToInt32() == 787)
                {
                    if (!Licenciamento.ConsultarPorProcessoEDataDeAbertura(processo.Id, evento.Data))
                    {
                        Licenciamento licenciamento = new Licenciamento();
                        licenciamento.DataAbertura = evento.Data;
                        licenciamento.ProcessoDNPM = processo;

                        licenciamento = licenciamento.Salvar();

                        idLicenciamento = licenciamento.Id;

                        evento.Atualizado = true;
                    }
                }

                //Data de publicação do Licenciamento - codigo 730 
                if (codigoEvento.ToInt32() == 730)
                {
                    if (idLicenciamento > 0)
                    {
                        Licenciamento licenciamento = Licenciamento.ConsultarPorId(idLicenciamento);

                        if (licenciamento != null)
                        {
                            licenciamento.DataPublicacao = evento.Data;
                            licenciamento.ProcessoDNPM = processo;

                            licenciamento = licenciamento.Salvar();

                            idLicenciamento = licenciamento.Id;

                            evento.Atualizado = true;
                        }
                    }
                }

                //Extração - codigo 820
                if (codigoEvento.ToInt32() == 820)
                {
                    if (!Extracao.ConsultarPorProcessoEDataDeAbertura(processo.Id, evento.Data))
                    {
                        Extracao extracao = new Extracao();
                        extracao.DataAbertura = evento.Data;
                        extracao.ProcessoDNPM = processo;

                        extracao = extracao.Salvar();

                        idExtracao = extracao.Id;

                        evento.Atualizado = true;
                    }
                }

                //Data de publicação da extração - codigo 924
                if (codigoEvento.ToInt32() == 924)
                {
                    if (idExtracao > 0)
                    {
                        Extracao extracao = Extracao.ConsultarPorId(idExtracao);

                        if (extracao != null)
                        {
                            extracao.DataPublicacao = evento.Data;
                            extracao.ProcessoDNPM = processo;

                            extracao = extracao.Salvar();

                            evento.Atualizado = true;
                        }
                    }
                }
            }

            if (processo.EventosDNPM == null)
                processo.EventosDNPM = new List<EventoDNPM>();

            if (!processo.EventosDNPM.Contains(evento))
            {
                evento = evento.Salvar();
                processo.EventosDNPM.Add(evento);
            }

            processo = processo.Salvar();
        }
    }

    private static bool CustomValidation(Object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
    {
        return true;
    }

    private int RetornoNomeEstadoPelaSigla(string sigla)
    {
        switch (sigla)
        {
            case "AC":
                return 1;
            case "AL":
                return 2;
            case "AP":
                return 3;
            case "AM":
                return 4;
            case "BA":
                return 5;
            case "CE":
                return 6;
            case "DF":
                return 7;
            case "ES":
                return 8;
            case "GO":
                return 9;
            case "MA":
                return 10;
            case "MT":
                return 11;
            case "MS":
                return 12;
            case "MG":
                return 13;
            case "PA":
                return 14;
            case "PB":
                return 15;
            case "PR":
                return 16;
            case "PE":
                return 17;
            case "PI":
                return 18;
            case "RJ":
                return 19;
            case "RN":
                return 20;
            case "RS":
                return 21;
            case "RO":
                return 22;
            case "RR":
                return 23;
            case "SC":
                return 24;
            case "SP":
                return 25;
            case "SE":
                return 26;
            case "TO":
                return 27;
            default: return 0;
        }
    }

    private string GetTags(string source)
    {
        string table = "";

        int ini = source.IndexOf("<table");
        int fim = source.IndexOf("</table>") + 8 - source.IndexOf("<table");
        if (ini > 0 && fim > 0)
            table = source.Substring(ini, fim);

        return table;
    }

    private string GetParametrosDNPM(string numeroProcesso)
    {
        try
        {
            return "?numero=" + numeroProcesso.Substring(0, 6) + "&ano=" + numeroProcesso.Substring(6, 4);
        }
        catch (Exception)
        {
            return "";
        }
    }

    private DateTime CalcularTaxaHectare(DateTime dataPublicao)
    {

        if (dataPublicao.Month > 6)
        {
            for (int i = 31; i > 0; i--)
            {
                DateTime j = new DateTime(dataPublicao.Year + 1, 1, i);
                if (j.DayOfWeek != DayOfWeek.Sunday && j.DayOfWeek != DayOfWeek.Saturday)
                {
                    return j;
                }
            }
        }
        else
        {
            for (int i = 31; i > 0; i--)
            {
                DateTime j = new DateTime(dataPublicao.Year, 7, i);
                if (j.DayOfWeek != DayOfWeek.Sunday && j.DayOfWeek != DayOfWeek.Saturday)
                {
                    return j;
                }
            }
        }
        return new DateTime();
    }

}