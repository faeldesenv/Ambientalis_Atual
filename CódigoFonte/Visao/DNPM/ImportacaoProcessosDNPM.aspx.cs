using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class DNPM_ImportacaoProcessosDNPM : PageBase
{
    Transacao transacao = new Transacao();
    Msg msg = new Msg();

    private IList<string> Processos
    {
        get
        {
            if (Session["processos_dnpm_importar"] == null)
                Session["processos_dnpm_importar"] = new List<string>();
            return Session["processos_dnpm_importar"] as List<string>;
        }
        set { Session["processos_dnpm_importar"] = value; }
    }

    private ProcessoDNPM ProcessoImportado
    {
        get
        {
            if (Session["processo_dnpm_importado"] == null)
                Session["processo_dnpm_importado"] = new ProcessoDNPM();
            return Session["processo_dnpm_importado"] as ProcessoDNPM;
        }
        set { Session["processo_dnpm_importado"] = value; }
    }

    public ConfiguracaoPermissaoModulo ConfiguracaoModuloDNPM
    {
        get
        {
            if (Session["ConfiguracaoModuloDNPM"] == null)
                return null;
            else
                return (ConfiguracaoPermissaoModulo)Session["ConfiguracaoModuloDNPM"];
        }
        set { Session["ConfiguracaoModuloDNPM"] = value; }
    }

    //Empresas que o usuário edita    
    public IList<Empresa> EmpresasPermissaoEdicaoModuloNPM
    {
        get
        {
            if (Session["EmpresasPermissaoEdicaoModuloNPM"] == null)
                return null;
            else
                return (IList<Empresa>)Session["EmpresasPermissaoEdicaoModuloNPM"];
        }
        set { Session["EmpresasPermissaoEdicaoModuloNPM"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.Processos = null;

                if ((this.UsuarioLogado != null && this.UsuarioLogado.PossuiPermissaoDeEditarModuloDNPM) == false)
                    Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

                this.VerificarPermissoes();

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

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDNPM.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

        if (this.ConfiguracaoModuloDNPM == null)
            Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.EmpresasPermissaoEdicaoModuloNPM = Empresa.ObterEmpresasQueOUsuarioPodeEditarDoModulo(moduloDNPM, this.UsuarioLogado);
        }

    }

    #region ________________ Métodos ________________

    private static bool CustomValidation(Object sender, X509Certificate cert,
    X509Chain chain, System.Net.Security.SslPolicyErrors error)
    {
        return true;
    }

    private void ImportarProcesso(string numeroProcesso)
    {
        lblNumeroProcessoTitulo.Text = numeroProcesso;

        ProcessoDNPM processo = ProcessoDNPM.getProcessoPeloNumero(numeroProcesso);

        //O processo já esta cadastrado no sistema
        if (processo != null && processo.Id > 0)
        {
            this.HabilitarCamposMensagemErroImportacao();

            lblMensagemProblema.Text = "O Processo " + numeroProcesso + " já está cadastrado no Sistema Sustentar. Não é possível importar processos já cadastrados no sistema.";
            lblImportacaoProcesso_popupextender.Show();
        }
        else
        {
            /* Adicionando Handler pro evento ServerCertificateValidationCallback 
         * que chama a função CustomValidation*/
            ServicePointManager.ServerCertificateValidationCallback += new
            System.Net.Security.RemoteCertificateValidationCallback(CustomValidation);

            string url = "https://sistemas.dnpm.gov.br/SCM/Extra/site/admin/dadosProcesso.aspx" + this.GetParametrosDNPM(numeroProcesso);

            //baixando codigo fonte do site e decodificando
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string source = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            string conteudoProcesso = source.Substring(source.IndexOf("<body>")).Replace("</body>", "");

            conteudoProcesso = conteudoProcesso.Substring(conteudoProcesso.IndexOf("<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" class=\"BordaTabela\" width=\"100%\">"));

            conteudoProcesso = conteudoProcesso.Substring(0, conteudoProcesso.LastIndexOf("</table>"));

            if (conteudoProcesso.LastIndexOf("</table>") <= 0)
            {
                this.HabilitarCamposMensagemErroImportacao();

                lblMensagemProblema.Text = "O número de processo " + numeroProcesso + " não corresponde a um processo válido no DNPM.";
                lblImportacaoProcesso_popupextender.Show();
            }
            else
            {
                conteudoProcesso = conteudoProcesso.Substring(0, conteudoProcesso.LastIndexOf("</table>")) + "</table>";

                lblConteudoProcesso.Text = conteudoProcesso;

                String h1Regex = "<td[^>]*?>(?<TagText>.*?)</td>";

                //obtendo a Empresa do Processo
                int indiceInicioEmpresa = source.IndexOf("Pessoas relacionadas");
                int indiceFimEmpresa = source.IndexOf("Número do processo <br /> de Cadastro");

                string sourceNomeEmpresa = source.Substring(indiceInicioEmpresa, (indiceFimEmpresa - indiceInicioEmpresa));

                MatchCollection mc = Regex.Matches(this.GetTags(sourceNomeEmpresa), h1Regex, RegexOptions.Singleline);

                string cnpjCpfEmpresa = mc[1].Groups["TagText"].Value.Replace("\n", "").Replace("\r", "").Trim();

                Empresa empresa = Empresa.ConsultarPorCNPJCPF(cnpjCpfEmpresa.Replace(".", "").Replace("/", "").Replace("-", "").Replace("  ", " ").Replace(" ", "").Trim());

                if (empresa != null && empresa.Id > 0)
                {
                    if (this.ConfiguracaoModuloDNPM.Tipo != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        //Usuario não possui permissao de alterar dados do DNPM da empresa do processo informado
                        if ((this.EmpresasPermissaoEdicaoModuloNPM == null || this.EmpresasPermissaoEdicaoModuloNPM.Count == 0) || (this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 && !this.EmpresasPermissaoEdicaoModuloNPM.Contains(empresa)))
                        {
                            this.HabilitarCamposMensagemErroImportacao();

                            lblMensagemProblema.Text = "O usuário logado não possui permissão de editar dados da empresa do processo " + numeroProcesso + " do módulo DNPM. Não é possível importar processos de empresas que o usuário não possua permissão.";
                            lblImportacaoProcesso_popupextender.Show();
                        }
                        else
                        {
                            this.HabilitarCamposImportacaoEventos();

                            hfIdEmpresa.Value = empresa.Id.ToString();
                            lblNumeroProcessoTitulo.Text = lblNumeroProcesso.Text = numeroProcesso;
                            lblImportacaoProcesso_popupextender.Show();
                        }
                    }
                    else
                    {
                        this.HabilitarCamposImportacaoEventos();

                        hfIdEmpresa.Value = empresa.Id.ToString();
                        lblNumeroProcessoTitulo.Text = lblNumeroProcesso.Text = numeroProcesso;
                        lblImportacaoProcesso_popupextender.Show();
                    }
                }
                else
                {
                    this.HabilitarCamposMensagemErroImportacao();

                    lblMensagemProblema.Text = "A Empresa do processo " + numeroProcesso + " não está cadastrada no Sistema Sustentar. Não é possível importar processos de empresas que não estejam cadastradas no sistema.";
                    lblImportacaoProcesso_popupextender.Show();
                }
            }

        }
    }

    private void HabilitarCamposMensagemErroImportacao()
    {
        mensagem_problema_regimes.Visible = true;
        regimes.Visible = false;
        btnImportarProcesso.Visible = false;
        btnContinuar.Visible = true;
    }

    private void HabilitarCamposImportacaoEventos()
    {
        mensagem_problema_regimes.Visible = false;
        regimes.Visible = true;
        btnImportarProcesso.Visible = true;
        btnContinuar.Visible = false;
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

    #endregion

    #region ________________ Eventos ________________

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Processos != null && this.Processos.Count > 0)
                this.Processos.RemoveAt(0);

            lblImportacaoProcesso_popupextender.Hide();

            if (this.Processos != null && this.Processos.Count > 0)
                this.ImportarProcesso(this.Processos[0]);

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

    protected void btnImportarProcesso_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarProcessoImportado();

            if (this.Processos != null && this.Processos.Count > 0)
                this.Processos.RemoveAt(0);

            lblImportacaoProcesso_popupextender.Hide();

            if (this.Processos != null && this.Processos.Count > 0)
                this.ImportarProcesso(this.Processos[0]);

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

    private void SalvarProcessoImportado()
    {
        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
        {
            if (this.UsuarioLogado.GrupoEconomico.VerificarSeExcedeuLimiteDeProcessosContratados())
            {
                msg.CriarMensagem("Você já atingiu o limite de Processos Minerários cadastrados. Para importar mais processos exclua processos antigos ou aumente seu limite para processos cadastrados.", "Atenção", MsgIcons.Informacao);
                return;
            }
        }

        ProcessoDNPM novoProcesso = new ProcessoDNPM();

        novoProcesso.Numero = lblNumeroProcessoTitulo.Text;
        novoProcesso.DataAbertura = hfDataAbertura.Value.ToDateTime();
        novoProcesso.Empresa = Empresa.ConsultarPorId(hfIdEmpresa.Value.ToInt32());
        novoProcesso.Observacoes = hfDetalhamento.Value;

        if (hfTipoFase.Value.Contains("Extração"))
            novoProcesso.RegimeDeCriacao = "Extração";

        else if (hfTipoFase.Value.Contains("Licenciamento"))
            novoProcesso.RegimeDeCriacao = "Extração";

        else
            novoProcesso.RegimeDeCriacao = "Autorização de pesquisa";

        //salvando as substancias do processo
        string[] substancias = hfSubstancias.Value.Split('#');

        if (substancias != null && substancias.Length > 0)
        {
            for (int i = 0; i < substancias.Length; i++)
            {
                Substancia substancia = Substancia.ConsultarPorNome(substancias[i]);

                if (substancia == null)
                    substancia = new Substancia();

                substancia.Nome = substancias[i];

                substancia = substancia.Salvar();

                if (novoProcesso.Substancias == null)
                    novoProcesso.Substancias = new List<Substancia>();

                if (!novoProcesso.Substancias.Contains(substancia))
                    novoProcesso.Substancias.Add(substancia);
            }

        }

        //Salvando a cidade e estado
        Estado estado = Estado.ConsultarPorId(this.RetornoNomeEstadoPelaSigla(hfEstado.Value.Replace("\n", "").Replace("  ", " ").Trim()));

        Cidade cidade = Cidade.ConsultarPorNome(hfCidade.Value.Replace("\n", "").Replace("  ", " ").Trim(), estado);        

        if (cidade != null && cidade.Id > 0)
            novoProcesso.Cidade = cidade;

        novoProcesso = novoProcesso.Salvar();

        //Pegando os eventos
        string[] eventos = hfCodigosEventosMarcados.Value.Split('#');

        int idLicenciamento = 0;
        int idExtracao = 0;
        int idGuia = 0;
        int idRal = 0;

        if (eventos != null && eventos.Length > 0)
        {
            for (int i = eventos.Length - 1; i > -1; i--)
            {
                if (eventos[i].Trim() != "")
                {
                    string[] codigosDados = eventos[i].Split('&');

                    //pegando o codigo dos eventos
                    string codigoEvento = codigosDados[0].Trim();

                    string[] dadosEventos = codigosDados[1].Trim().Split('-');

                    string dataEvento = dadosEventos[1].Trim();
                    string descricaoEvento = dadosEventos[0].Trim();

                    EventoDNPM evento = new EventoDNPM();

                    evento.Data = dataEvento.ToDateTime();
                    evento.Descricao = descricaoEvento;
                    evento.ProcessoDNPM = novoProcesso;

                    if (codigoEvento.ToInt32() > 0)
                    {
                        //Requerimento de Pesquisa - codigo 100 ou codigo 1274
                        if (codigoEvento.ToInt32() == 100 || codigoEvento.ToInt32() == 1274)
                        {
                            RequerimentoPesquisa requerimentoPesquisa = new RequerimentoPesquisa();
                            requerimentoPesquisa.DataRequerimento = dataEvento.ToDateTime();
                            requerimentoPesquisa.ProcessoDNPM = novoProcesso;

                            requerimentoPesquisa = requerimentoPesquisa.Salvar();

                            evento.Atualizado = true;
                        }


                        //Requerimento de Lavra - codigo 350 ou 1275
                        if (codigoEvento.ToInt32() == 350 || codigoEvento.ToInt32() == 1275)
                        {
                            RequerimentoLavra requerimentoLavra = new RequerimentoLavra();
                            requerimentoLavra.Data = dataEvento.ToDateTime();
                            requerimentoLavra.ProcessoDNPM = novoProcesso;

                            requerimentoLavra = requerimentoLavra.Salvar();

                            evento.Atualizado = true;
                        }

                        //Alvará de Pesquisa - codigo 322 ou codigo 323 ou codigo 321
                        if (codigoEvento.ToInt32() == 322 || codigoEvento.ToInt32() == 323 || codigoEvento.ToInt32() == 321)
                        {
                            AlvaraPesquisa alvaraPesquisa = new AlvaraPesquisa();
                            alvaraPesquisa.DataPublicacao = dataEvento.ToDateTime();

                            alvaraPesquisa.AnosValidade = codigoEvento.ToInt32() == 322 ? 2 : codigoEvento.ToInt32() == 323 ? 3 : 1;

                            Vencimento vencimento = new Vencimento();
                            vencimento.Data = dataEvento.ToDateTime().AddYears(+alvaraPesquisa.AnosValidade);

                            vencimento = vencimento.Salvar();

                            alvaraPesquisa.Vencimento = vencimento;

                            alvaraPesquisa.ProcessoDNPM = novoProcesso;

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

                        //Alvará de Pesquisa sem vencimento codigo 176
                        if (codigoEvento.ToInt32() == 176)
                        {
                            AlvaraPesquisa alvaraPesquisa = new AlvaraPesquisa();
                            alvaraPesquisa.DataPublicacao = dataEvento.ToDateTime();

                            alvaraPesquisa.ProcessoDNPM = novoProcesso;

                            alvaraPesquisa = alvaraPesquisa.Salvar();

                            evento.Atualizado = true;
                        }

                        //Concessão de Lavra - codigo 1278 ou 400
                        if (codigoEvento.ToInt32() == 1278 || codigoEvento.ToInt32() == 400 || codigoEvento.ToInt32() == 481)
                        {
                            ConcessaoLavra concessaoLavra = new ConcessaoLavra();
                            concessaoLavra.Data = dataEvento.ToDateTime();
                            concessaoLavra.ProcessoDNPM = novoProcesso;

                            concessaoLavra = concessaoLavra.Salvar();

                            //se nao existir ral criar um
                            if (novoProcesso != null)
                            {
                                RAL ral;

                                if (idRal > 0)
                                    ral = RAL.ConsultarPorId(idRal);
                                else
                                    ral = new RAL();

                                ral.ProcessoDNPM = novoProcesso;

                                if (ral.Vencimentos == null)
                                    ral.Vencimentos = new List<Vencimento>();

                                Vencimento v = new Vencimento();
                                v.Status = Status.ConsultarPorId(1);
                                v.Data = new DateTime(dataEvento.ToDateTime().AddYears(1).Year, 3, 15);
                                ral.Vencimentos.Add(v.Salvar());
                                ral = ral.Salvar();
                                idRal = ral.Id;
                            }

                            evento.Atualizado = true;
                        }


                        //Guia de Utilização - codigo 624
                        if (codigoEvento.ToInt32() == 624 || codigoEvento.ToInt32() == 283)
                        {
                            idGuia = 0;
                            GuiaUtilizacao guiaUtilizacao = new GuiaUtilizacao();
                            guiaUtilizacao.DataRequerimento = dataEvento.ToDateTime();
                            guiaUtilizacao.ProcessoDNPM = novoProcesso;

                            guiaUtilizacao = guiaUtilizacao.Salvar();

                            idGuia = guiaUtilizacao.Id;

                            evento.Atualizado = true;
                        }

                        //Data Emissao da guia de utilização
                        if (codigoEvento.ToInt32() == 625 || codigoEvento.ToInt32() == 285)
                        {
                            if (idGuia > 0)
                            {
                                GuiaUtilizacao guiaUtilizacao = GuiaUtilizacao.ConsultarPorId(idGuia);

                                if (guiaUtilizacao != null)
                                {
                                    guiaUtilizacao.DataEmissao = dataEvento.ToDateTime();
                                    guiaUtilizacao.ProcessoDNPM = novoProcesso;

                                    guiaUtilizacao = guiaUtilizacao.Salvar();

                                    idGuia = guiaUtilizacao.Id;

                                    //se nao existir ral criar um
                                    if (novoProcesso != null)
                                    {
                                        RAL ral;

                                        if (idRal > 0)
                                            ral = RAL.ConsultarPorId(idRal);
                                        else
                                            ral = new RAL();

                                        ral.ProcessoDNPM = novoProcesso;

                                        if (ral.Vencimentos == null)
                                            ral.Vencimentos = new List<Vencimento>();

                                        Vencimento v = new Vencimento();
                                        v.Status = Status.ConsultarPorId(1);
                                        v.Data = new DateTime(dataEvento.ToDateTime().AddYears(1).Year, 3, 15);
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
                            Licenciamento licenciamento = new Licenciamento();
                            licenciamento.DataAbertura = dataEvento.ToDateTime();
                            licenciamento.ProcessoDNPM = novoProcesso;

                            licenciamento = licenciamento.Salvar();

                            idLicenciamento = licenciamento.Id;

                            evento.Atualizado = true;
                        }

                        //Data de publicação do Licenciamento - codigo 730 
                        if (codigoEvento.ToInt32() == 730 )
                        {
                            if (idLicenciamento > 0)
                            {
                                Licenciamento licenciamento = Licenciamento.ConsultarPorId(idLicenciamento);

                                if (licenciamento != null)
                                {
                                    licenciamento.DataPublicacao = dataEvento.ToDateTime();
                                    licenciamento.ProcessoDNPM = novoProcesso;

                                    licenciamento = licenciamento.Salvar();

                                    idLicenciamento = licenciamento.Id;

                                    evento.Atualizado = true;
                                }
                            }
                        }

                        //Extração - codigo 820
                        if (codigoEvento.ToInt32() == 820)
                        {
                            Extracao extracao = new Extracao();
                            extracao.DataAbertura = dataEvento.ToDateTime();
                            extracao.ProcessoDNPM = novoProcesso;

                            extracao = extracao.Salvar();

                            idExtracao = extracao.Id;

                            evento.Atualizado = true;
                        }

                        //Data de publicação da extração - codigo 924
                        if (codigoEvento.ToInt32() == 924)
                        {
                            if (idExtracao > 0)
                            {
                                Extracao extracao = Extracao.ConsultarPorId(idExtracao);

                                if (extracao != null)
                                {
                                    extracao.DataPublicacao = dataEvento.ToDateTime();
                                    extracao.ProcessoDNPM = novoProcesso;

                                    extracao = extracao.Salvar();

                                    evento.Atualizado = true;
                                }
                            }
                        }
                    }

                    evento = evento.Salvar();

                    if (novoProcesso.EventosDNPM == null)
                        novoProcesso.EventosDNPM = new List<EventoDNPM>();

                    novoProcesso.EventosDNPM.Add(evento);

                }

            }

        }

        novoProcesso = novoProcesso.Salvar();

        //Se a configuraçao de permissoes for por processo adicionar o usuario logado como editor do processo importado
        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            novoProcesso.UsuariosEdicao = new List<Usuario>();

            if (novoProcesso.UsuariosVisualizacao != null && novoProcesso.UsuariosVisualizacao.Count > 0)
            {
                if (!novoProcesso.UsuariosVisualizacao.Contains(this.UsuarioLogado))
                    novoProcesso.UsuariosVisualizacao.Add(this.UsuarioLogado);
            }

            novoProcesso.UsuariosEdicao.Add(this.UsuarioLogado);

            novoProcesso = novoProcesso.Salvar();

        }

        msg.CriarMensagem("Processo Importado com sucesso!", "Sucesso", MsgIcons.Sucesso);

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

    protected void btnCancelarImportacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.Processos = null;

            lblImportacaoProcesso_popupextender.Hide();

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

    protected void btnIniciarImportacao_Click(object sender, EventArgs e)
    {
        try
        {
            if (tbxProcessos.Text.Trim() == "")
            {
                msg.CriarMensagem("Informe um número de processo para prosseguir com a importação!", "Informação", MsgIcons.Informacao);
                return;
            }

            string[] numerosProcessos = tbxProcessos.Text.Split(';');

            if (numerosProcessos != null && numerosProcessos.Length > 0)
            {
                for (int i = 0; i < numerosProcessos.Length; i++)
                {
                    if (numerosProcessos[i] != "")
                        this.Processos.Add(numerosProcessos[i].Replace(".", "").Replace("/", "").Replace("\n", "").Replace(",", "").RemoverCaracteresEspeciais().Trim());
                }
            }

            if (this.Processos != null && this.Processos.Count > 0)
            {
                this.ImportarProcesso(this.Processos[0]);
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

    protected void btnCancelarImportacao_PreRender(object sender, EventArgs e)
    {
        Button ibtn = (Button)sender;
        WebUtil.AdicionarConfirmacao(ibtn, "Este processo irá cancelar a importação de todos os processos informados. Deseja cancelar mesmo assim?");
    }

    #endregion

    #region ________________ Trigers ________________

    protected void btnIniciarImportacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRegimes);
    }

    #endregion

}