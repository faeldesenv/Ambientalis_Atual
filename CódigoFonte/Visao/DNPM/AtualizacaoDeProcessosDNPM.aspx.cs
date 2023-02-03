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

public partial class DNPM_AtualizacaoDeProcessosDNPM : PageBase
{
    Transacao transacao = new Transacao();
    Msg msg = new Msg();

    private int Contador
    {
        get
        {
            if ((int)Session["contador_processos"] < 0)
                Session["contador_processos"] = 0;
            return (int)Session["contador_processos"];
        }
        set { Session["contador_processos"] = value; }
    }

    private int Indice
    {
        get
        {
            if ((int)Session["indice_processos"] < 0)
                Session["indice_processos"] = 0;
            return (int)Session["indice_processos"];
        }
        set { Session["indice_processos"] = value; }
    }

    private bool Parado
    {
        get
        {
            if (Session["parado_bruscamente"] == null)
                Session["parado_bruscamente"] = false;
            return (bool)Session["parado_bruscamente"];
        }
        set { Session["parado_bruscamente"] = value; }
    }

    private IList<ProcessoDNPM> Processos
    {
        get
        {
            if (Session["processos_dnpm_atualizacoes"] == null)
                Session["processos_dnpm_atualizacoes"] = new List<ProcessoDNPM>();
            return Session["processos_dnpm_atualizacoes"] as List<ProcessoDNPM>;
        }
        set { Session["processos_dnpm_atualizacoes"] = value; }
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

    //Processos que o usuário edita     
    public IList<ProcessoDNPM> ProcessosPermissaoEdicaoModuloDNPM
    {
        get
        {
            if (Session["ProcessosPermissaoEdicaoModuloDNPM"] == null)
                return null;
            else
                return (IList<ProcessoDNPM>)Session["ProcessosPermissaoEdicaoModuloDNPM"];
        }
        set { Session["ProcessosPermissaoEdicaoModuloDNPM"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.VerificarPermissoes();
                this.IniciarComponentes();
                this.CarregarCampos();                
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

    #region __________________ Métodos __________________

    private void VerificarPermissoes()
    {
        ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");
        this.LimparSessoesPermissoes();

        if (this.UsuarioLogado != null && this.UsuarioLogado.GrupoEconomico != null && this.UsuarioLogado.GrupoEconomico.Id > 0)
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.UsuarioLogado.GrupoEconomico.Id, moduloDNPM.Id);
        else
            this.ConfiguracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

        if (this.ConfiguracaoModuloDNPM == null)
            Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL)
        {
            if ((this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral == null || this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count == 0) || (this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && !this.ConfiguracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this.UsuarioLogado)))
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

            this.EmpresasPermissaoEdicaoModuloNPM = null;
            this.ProcessosPermissaoEdicaoModuloDNPM = null;
        }

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            this.CarregarSessoesEmpresasDeEdicaoEVisualizacao();

            if (this.EmpresasPermissaoEdicaoModuloNPM == null || this.EmpresasPermissaoEdicaoModuloNPM.Count == 0)
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");

            this.ProcessosPermissaoEdicaoModuloDNPM = null;
        }

        if (this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
        {
            this.EmpresasPermissaoEdicaoModuloNPM = null;

            this.CarregarSessoesProcessosDNPMDeEdicaoEVisualizacao();

            if (this.ProcessosPermissaoEdicaoModuloDNPM == null || this.ProcessosPermissaoEdicaoModuloDNPM.Count == 0)
                Response.Redirect("../Acesso/PermissaoInsufuciente.aspx");
        }
    }

    private void LimparSessoesPermissoes()
    {
        this.EmpresasPermissaoEdicaoModuloNPM = null;
        this.ProcessosPermissaoEdicaoModuloDNPM = null;
    }

    private void CarregarSessoesEmpresasDeEdicaoEVisualizacao()
    {
        IList<Empresa> empresas = Empresa.ConsultarTodos();

        ModuloPermissao modulo = ModuloPermissao.ConsultarPorNome("DNPM");

        if (empresas != null && empresas.Count > 0)
        {
            foreach (Empresa empresa in empresas)
            {
                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, modulo.Id);

                if (empresaPermissao != null)
                {
                    //empresas com permissão de edição
                    if (empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0 && empresaPermissao.UsuariosEdicao.Contains(this.UsuarioLogado))
                    {
                        if (this.EmpresasPermissaoEdicaoModuloNPM == null)
                            this.EmpresasPermissaoEdicaoModuloNPM = new List<Empresa>();

                        if (!this.EmpresasPermissaoEdicaoModuloNPM.Contains(empresa))
                            this.EmpresasPermissaoEdicaoModuloNPM.Add(empresa);
                    }
                }
            }
        }
    }

    private void CarregarSessoesProcessosDNPMDeEdicaoEVisualizacao()
    {
        IList<ProcessoDNPM> processos = ProcessoDNPM.ConsultarTodos();

        if (processos != null && processos.Count > 0)
        {
            foreach (ProcessoDNPM processo in processos)
            {
                //processos com permissão de edição
                if (processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0 && processo.UsuariosEdicao.Contains(this.UsuarioLogado))
                {
                    if (this.ProcessosPermissaoEdicaoModuloDNPM == null)
                        this.ProcessosPermissaoEdicaoModuloDNPM = new List<ProcessoDNPM>();

                    if (!this.ProcessosPermissaoEdicaoModuloDNPM.Contains(processo))
                        this.ProcessosPermissaoEdicaoModuloDNPM.Add(processo);
                }
            }
        }
    }

    private void IniciarComponentes()
    {        
        btnParar.Text = "Parar";
        this.Contador = 0;
        this.Processos = null;
        this.Indice = 0;
        Timer1.Enabled = false;
    }

    private void CarregarCampos()
    {
        IList<GrupoEconomico> gs = GrupoEconomico.ConsultarGruposAtivos();
        ddlGrupoEconomico.DataValueField = "Id";
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataSource = gs != null ? gs : new List<GrupoEconomico>();
        ddlGrupoEconomico.DataBind();
        ddlGrupoEconomico.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        this.ddlClientes_SelectedIndexChanged(null, null);
    }

    private void CarregarEmpresas()
    {
        ddlEmpresa.Items.Clear();
        ddlEmpresa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Empresa> empresas;

        //Carregando as empresas de acordo com a configuração de permissão
        if (this.ConfiguracaoModuloDNPM != null && this.ConfiguracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                empresas = this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 ? this.EmpresasPermissaoEdicaoModuloNPM.Where(x => x.GrupoEconomico != null && x.GrupoEconomico.Id == ddlGrupoEconomico.SelectedValue.ToInt32()).ToList() : new List<Empresa>();
            else
                empresas = this.EmpresasPermissaoEdicaoModuloNPM != null && this.EmpresasPermissaoEdicaoModuloNPM.Count > 0 ? this.EmpresasPermissaoEdicaoModuloNPM : new List<Empresa>();
        }
        else
        {
            GrupoEconomico c = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
            empresas = c != null && c.Empresas != null ? c.Empresas : new List<Empresa>();
        }

        if (empresas != null && empresas.Count > 0)
        {
            empresas = empresas.OrderBy(x => x.Nome).ToList();
            foreach (Empresa emp in empresas)
            {
                if (emp.DadosPessoa.GetType() == typeof(DadosJuridica))
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosJuridica)emp.DadosPessoa).Cnpj, emp.Id.ToString()));
                else
                    ddlEmpresa.Items.Add(new ListItem(emp.Nome + " - " + ((DadosFisica)emp.DadosPessoa).Cpf, emp.Id.ToString()));
            }
        }
    }

    private void ZerarCampos()
    {
        btnParar.Text = "Parar";
        this.Contador = 0;
        this.Processos = null;
        this.Indice = 0;
        lblProcesso.Text = "Processando.";
    }

    private static bool CustomValidation(Object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
    {
        return true;
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
            Contador++;
        }

        
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

    #endregion

    #region __________________ Eventos __________________

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEmpresas();
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

    protected void btnBaixarAtualizacoes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGrupoEconomico.SelectedValue.ToInt32() <= 0) 
            {
                msg.CriarMensagem("Selecione um grupo econômico para prosseguir com a atualização de processos", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ZerarCampos();
            
            this.Processos = ProcessoDNPM.ConsultarProcessosDoClienteOuGrupo(ddlGrupoEconomico.SelectedValue.ToInt32(), ddlEmpresa.SelectedValue.ToInt32(), this.ConfiguracaoModuloDNPM.Tipo, this.EmpresasPermissaoEdicaoModuloNPM, this.ProcessosPermissaoEdicaoModuloDNPM);

            if (this.Processos == null || this.Processos.Count == 0) 
            {
                msg.CriarMensagem("Não há processos para ser atualizados da Empresa ou Grupo Econômico selecionados; ou você não possui permissão para atualizar estes processos.", "Informação", MsgIcons.Informacao);
                return;
            }

            LinkButton1_ModalPopupExtender.Show();

            this.Indice = this.Processos.Count;
            Timer1.Enabled = true;
            lblAguarde.Text = "Aguarde, o processo pode levar alguns minutos.";
            this.Parado = false;
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

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            if (this.Processos != null && this.Processos.Count > 0)
            {
                lblProcesso.Text = "Atualizando processos: " + (Indice - Processos.Count + 1) + " de " + Indice;
                this.AtualizarProcesso(Processos[0]);
                this.Processos.RemoveAt(0);
            }
            else
            {
                Timer1.Enabled = false;

                if (this.Parado)
                {
                    msg.CriarMensagem("Atualização parada com sucesso", "Sucesso", MsgIcons.Sucesso);
                }
                else 
                {
                    msg.CriarMensagem("Atualizado(s) " + Indice + " processo(s).", "Sucesso", MsgIcons.Sucesso);
                }                
                
                ZerarCampos();
                lblAguarde.Text = "";
                lblProcesso.Text = "Concluído.";
                btnParar.Text = "Concluído";
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
            Timer1.Enabled = false;
        }
        finally
        {
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnParar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(btnBaixarAtualizacoes, "Click", upAtualizar);
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Processos = null;
            LinkButton1_ModalPopupExtender.Hide();

            if (btnParar.Text == "Parar")
            {
                this.Parado = true;
            } 
            else
                this.Parado = false;
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

    protected void btnBaixarAtualizacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(btnBaixarAtualizacoes, "Click", upAtualizar);
    }

    #endregion
    
}