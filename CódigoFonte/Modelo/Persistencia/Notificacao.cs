using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Notificacao : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Notificacao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Notificacao classe = new Notificacao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Notificacao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Notificacao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Notificacao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Notificacao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();

            //calcular a data
            if (this.Vencimento == null || this.Vencimento.Data <= SqlDate.MinValue)
                this.Data = SqlDate.MinValue;

            else if (this.Template == TemplateNotificacao.GuiaUtilizacao)
                this.Data = this.Vencimento.Data.AddDays(-this.DiasAviso).AddDays(-60);

            else if (this.Template == TemplateNotificacao.ValidadeLicenciamento)
                this.Data = this.Vencimento.Data.AddDays(-this.DiasAviso).AddDays(-60);

            else if (this.Template == TemplateNotificacao.TemplateLicenca)
                this.Data = this.Vencimento.Data.AddDays(-this.DiasAviso).AddDays(-120);

            else
                this.Data = this.Vencimento.Data.AddDays(-this.DiasAviso);

            return fabrica.GetDAOBase().Salvar<Notificacao>(this);
        }


        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Notificacao> SalvarTodos(IList<Notificacao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Notificacao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Notificacao> SalvarTodos(params Notificacao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Notificacao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Notificacao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Notificacao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Notificacao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Notificacao obj = Activator.CreateInstance<Notificacao>();
            return fabrica.GetDAOBase().ConsultarTodos<Notificacao>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Notificacao> ConsultarOrdemAcendente(int qtd)
        {
            Notificacao ee = new Notificacao();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Notificacao> ConsultarOrdemDescendente(int qtd)
        {
            Notificacao ee = new Notificacao();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Ibama
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Notificacao> Filtrar(int qtd)
        {
            Notificacao estado = new Notificacao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Ibama Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Ibama</returns>
        public virtual Notificacao UltimoInserido()
        {
            Notificacao estado = new Notificacao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Notificacao>(estado);
        }

        public static IList<Notificacao> FiltrarNotificacoes(DateTime data, Usuario usuario, ConfiguracaoPermissaoModulo configuracaoModuloDiversos, IList<Empresa> empresasPermissaoModuloDiversos, ConfiguracaoPermissaoModulo configuracaoModuloDNPM, IList<Empresa> empresasPermissaoModuloDNPM, IList<ProcessoDNPM> processosPermissaoModuloDNPM, ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente, IList<Empresa> empresasPermissaoModuloMeioAmbiente, IList<Processo> processosPermissaoModuloMeioAmbiente, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissaoModuloMeioAmbiente, IList<OutrosEmpresa> outrosEmpresasPermissaoModuloMeioAmbiente, ConfiguracaoPermissaoModulo configuracaoModuloContratos, IList<Empresa> empresasPermissaoModuloContratos, IList<Setor> setoresPermissaoModuloContratos)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Maior("Data", SqlDate.MinValue));
            aux.AdicionarFiltro(Filtros.Eq("Data", data));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);

            if (lista != null && lista.Count > 0 && usuario != null)
                lista = Notificacao.VerificarPermissoesNotificacoes(lista, usuario, configuracaoModuloDiversos, empresasPermissaoModuloDiversos, configuracaoModuloDNPM, empresasPermissaoModuloDNPM, processosPermissaoModuloDNPM, configuracaoModuloMeioAmbiente, empresasPermissaoModuloMeioAmbiente, processosPermissaoModuloMeioAmbiente, cadastrosTecnicosPermissaoModuloMeioAmbiente, outrosEmpresasPermissaoModuloMeioAmbiente, configuracaoModuloContratos, empresasPermissaoModuloContratos, setoresPermissaoModuloContratos);

            return lista;
        }

        private static IList<Notificacao> VerificarPermissoesNotificacoes(IList<Notificacao> notificacoes, Usuario usuario, ConfiguracaoPermissaoModulo configuracaoModuloDiversos, IList<Empresa> empresasPermissaoModuloDiversos, ConfiguracaoPermissaoModulo configuracaoModuloDNPM, IList<Empresa> empresasPermissaoModuloDNPM, IList<ProcessoDNPM> processosPermissaoModuloDNPM, ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente, IList<Empresa> empresasPermissaoModuloMeioAmbiente, IList<Processo> processosPermissaoModuloMeioAmbiente, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissaoModuloMeioAmbiente, IList<OutrosEmpresa> outrosEmpresasPermissaoModuloMeioAmbiente, ConfiguracaoPermissaoModulo configuracaoModuloContratos, IList<Empresa> empresasPermissaoModuloContratos, IList<Setor> setoresPermissaoModuloContratos)
        {
            IList<Notificacao> notificacoesAux = new List<Notificacao>();
            notificacoesAux = notificacoes;

            for (int i = notificacoes.Count - 1; i > -1; i--)
            {
                if (notificacoes[i].Modulo == null || notificacoes[i].Modulo == "")
                {
                    notificacoesAux.Remove(notificacoes[i]);
                    continue;
                }

                //Modulo DNPM
                if (notificacoes[i].Modulo == "DNPM")
                {
                    if (configuracaoModuloDNPM == null)
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloDNPM != null && configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL && configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count > 0 && !configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloDNPM != null && configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        if (empresasPermissaoModuloDNPM == null || empresasPermissaoModuloDNPM.Count == 0)
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                        else if (notificacoes[i].GetEmpresa == null || notificacoes[i].GetEmpresa.Id <= 0 || !empresasPermissaoModuloDNPM.Contains(notificacoes[i].GetEmpresa))
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                    }

                    if (configuracaoModuloDNPM != null && configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                    {
                        if (processosPermissaoModuloDNPM == null || processosPermissaoModuloDNPM.Count == 0)
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                        else if (notificacoes[i].GetProcessoDNPM == null || notificacoes[i].GetProcessoDNPM.Id <= 0 || !processosPermissaoModuloDNPM.Contains(notificacoes[i].GetProcessoDNPM))
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                    }
                }


                //Modulo Meio Ambiente
                if (notificacoes[i].Modulo == "Meio Ambiente")
                {
                    if (configuracaoModuloMeioAmbiente == null)
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloMeioAmbiente != null && configuracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL && configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count > 0 && !configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloMeioAmbiente != null && configuracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        if (empresasPermissaoModuloMeioAmbiente == null || empresasPermissaoModuloMeioAmbiente.Count == 0)
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                        else if (notificacoes[i].GetEmpresa == null || notificacoes[i].GetEmpresa.Id <= 0 || !empresasPermissaoModuloMeioAmbiente.Contains(notificacoes[i].GetEmpresa))
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                    }

                    if (configuracaoModuloMeioAmbiente != null && configuracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                    {
                        //Outros de Empresa
                        if (notificacoes[i].Template == TemplateNotificacao.TemplateOutrosEmpresa)
                        {
                            if (outrosEmpresasPermissaoModuloMeioAmbiente == null || outrosEmpresasPermissaoModuloMeioAmbiente.Count == 0)
                            {
                                notificacoesAux.Remove(notificacoes[i]);
                                continue;
                            }
                            else if (notificacoes[i].GetOutroEmpresa == null || notificacoes[i].GetOutroEmpresa.Id <= 0 || !outrosEmpresasPermissaoModuloMeioAmbiente.Contains(notificacoes[i].GetOutroEmpresa))
                            {
                                notificacoesAux.Remove(notificacoes[i]);
                                continue;
                            }
                        }

                        //Cadastro Técnico Federal                        
                        if (notificacoes[i].Template == TemplateNotificacao.TemplateRelatorioCTF || notificacoes[i].Template == TemplateNotificacao.TemplatePagamentoCTF || notificacoes[i].Template == TemplateNotificacao.TemplateCertificadoCTF)
                        {
                            if (cadastrosTecnicosPermissaoModuloMeioAmbiente == null || cadastrosTecnicosPermissaoModuloMeioAmbiente.Count == 0)
                            {
                                notificacoesAux.Remove(notificacoes[i]);
                                continue;
                            }
                            else if (notificacoes[i].GetCadastroTecnico == null || notificacoes[i].GetCadastroTecnico.Id <= 0 || !cadastrosTecnicosPermissaoModuloMeioAmbiente.Contains(notificacoes[i].GetCadastroTecnico))
                            {
                                notificacoesAux.Remove(notificacoes[i]);
                                continue;
                            }
                        }

                        //Processos De Meio Ambiente                  
                        if (notificacoes[i].Template == TemplateNotificacao.TemplateCondicionante || notificacoes[i].Template == TemplateNotificacao.TemplateLicenca || notificacoes[i].Template == TemplateNotificacao.TemplateOutrosProcesso)
                        {
                            if (processosPermissaoModuloMeioAmbiente == null || processosPermissaoModuloMeioAmbiente.Count == 0)
                            {
                                notificacoesAux.Remove(notificacoes[i]);
                                continue;
                            }
                            else if (notificacoes[i].GetProcessoMeioAmbiente == null || notificacoes[i].GetProcessoMeioAmbiente.Id <= 0 || !processosPermissaoModuloMeioAmbiente.Contains(notificacoes[i].GetProcessoMeioAmbiente))
                            {
                                notificacoesAux.Remove(notificacoes[i]);
                                continue;
                            }
                        }
                    }
                }


                //Modulo Contratos
                if (notificacoes[i].Modulo == "Contratos")
                {
                    if (configuracaoModuloContratos == null)
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloContratos != null && configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL && configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count > 0 && !configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloContratos != null && configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        if (empresasPermissaoModuloContratos == null || empresasPermissaoModuloContratos.Count == 0)
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                        else if (notificacoes[i].GetContrato == null || notificacoes[i].GetContrato.Id <= 0 || notificacoes[i].GetContrato.Empresa == null || !empresasPermissaoModuloContratos.Contains(notificacoes[i].GetContrato.Empresa))
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                    }

                    if (configuracaoModuloContratos != null && configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
                    {
                        if (setoresPermissaoModuloContratos == null || setoresPermissaoModuloContratos.Count == 0)
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                        else if (notificacoes[i].GetContrato == null || notificacoes[i].GetContrato.Id <= 0 || notificacoes[i].GetContrato.Setor == null || !setoresPermissaoModuloContratos.Contains(notificacoes[i].GetContrato.Setor))
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }

                    }

                }


                //Modulo Diversos
                if (notificacoes[i].Modulo == "Diversos")
                {
                    if (configuracaoModuloDiversos == null)
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL && configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral != null && configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count > 0 && !configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Contains(usuario))
                    {
                        notificacoesAux.Remove(notificacoes[i]);
                        continue;
                    }

                    if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                    {
                        if (empresasPermissaoModuloDiversos == null || empresasPermissaoModuloDiversos.Count == 0)
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                        else if (notificacoes[i].GetEmpresa == null || notificacoes[i].GetEmpresa.Id <= 0 || !empresasPermissaoModuloDiversos.Contains(notificacoes[i].GetEmpresa))
                        {
                            notificacoesAux.Remove(notificacoes[i]);
                            continue;
                        }
                    }
                }

            }

            return notificacoesAux;

        }

        public virtual CadastroTecnicoFederal GetCadastroTecnico
        {
            get
            {
                if (this.Template == TemplateNotificacao.TemplateRelatorioCTF)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.EntregaRelatorioAnual != null ? vencimento.EntregaRelatorioAnual : null;
                }

                if (this.Template == TemplateNotificacao.TemplatePagamentoCTF)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.TaxaTrimestral != null ? vencimento.TaxaTrimestral : null;
                }

                if (this.Template == TemplateNotificacao.TemplateCertificadoCTF)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.CertificadoRegularidade != null ? vencimento.CertificadoRegularidade : null;
                }

                return null;
            }
        }

        public virtual Processo GetProcessoMeioAmbiente
        {
            get
            {
                if (this.Template == TemplateNotificacao.TemplateCondicionante)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);

                    if (vencimento != null && vencimento.Condicional != null)
                    {
                        return Condicionante.ConsultarPorId(this.Vencimento.Condicional.Id).GetProcesso; ;
                    }
                    return null;
                }

                if (this.Template == TemplateNotificacao.TemplateLicenca)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.Licenca != null && vencimento.Licenca.Processo != null ? vencimento.Licenca.Processo : null;
                }

                if (this.Template == TemplateNotificacao.TemplateOutrosProcesso)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);

                    if (vencimento != null && vencimento.Condicional != null)
                    {
                        OutrosProcesso outros = OutrosProcesso.ConsultarPorId(this.Vencimento.Condicional.Id);
                        return outros != null && outros.Processo != null ? outros.Processo : null;
                    }

                    return null;
                }

                return null;
            }
        }

        public virtual ContratoDiverso GetContrato
        {
            get
            {
                if (this.Template == TemplateNotificacao.TemplateVencimentoContratoDiverso)
                {
                    VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.ContratoDiverso != null ? vencimento.ContratoDiverso : null;
                }

                if (this.Template == TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso)
                {
                    VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.Reajuste != null ? vencimento.Reajuste : null;
                }

                return null;
            }
        }

        public virtual ProcessoDNPM GetProcessoDNPM
        {
            get
            {
                if (this.Template == TemplateNotificacao.DIPEM)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.DIPEM != null && vencimento.DIPEM.ProcessoDNPM != null ? vencimento.DIPEM.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.Exigencia)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.Exigencia != null ? vencimento.Exigencia.Regime != null && vencimento.Exigencia.Regime.ProcessoDNPM != null ? vencimento.Exigencia.Regime.ProcessoDNPM : vencimento.Exigencia.GuiaUtilizacao != null && vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM != null ? vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM : null : null;
                }

                if (this.Template == TemplateNotificacao.GuiaUtilizacao)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.GuiaDeUtilizacao != null && vencimento.GuiaDeUtilizacao.ProcessoDNPM != null ? vencimento.GuiaDeUtilizacao.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.InicioPesquisa)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.NotificacaoPesquisaDNPM != null && vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM != null ? vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.LimiteRenuncia)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.LimiteRenuncia != null && vencimento.LimiteRenuncia.ProcessoDNPM != null ? vencimento.LimiteRenuncia.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.RAL)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.Ral != null && vencimento.Ral.ProcessoDNPM != null ? vencimento.Ral.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.TemplateRequerimentoEmissaoPosse)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.RequerimentoImissaoPosse != null && vencimento.RequerimentoImissaoPosse.ProcessoDNPM != null ? vencimento.RequerimentoImissaoPosse.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.TemplateRequerimentoLavra)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.RequerimentoLavraAlvaraPesquisa != null && vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null ? vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.TemplateRequerimentoLPTotal)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.RequerimentoLPTotal != null && vencimento.RequerimentoLPTotal.ProcessoDNPM != null ? vencimento.RequerimentoLPTotal.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.TemplateTaxaAnualHectare)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.TaxaAnualPorHectare != null && vencimento.TaxaAnualPorHectare.ProcessoDNPM != null ? vencimento.TaxaAnualPorHectare.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.ValidadeAlvaraPesquisa)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.AlvaraPesquisa != null && vencimento.AlvaraPesquisa.ProcessoDNPM != null ? vencimento.AlvaraPesquisa.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.ValidadeEntregaProtocolo)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.EntregaLicencaOuProtocolo != null && vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM != null ? vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.ValidadeExtracao)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.Extracao != null && vencimento.Extracao.ProcessoDNPM != null ? vencimento.Extracao.ProcessoDNPM : null;
                }

                if (this.Template == TemplateNotificacao.ValidadeLicenciamento)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);
                    return vencimento.Licenciamento != null && vencimento.Licenciamento.ProcessoDNPM != null ? vencimento.Licenciamento.ProcessoDNPM : null;
                }

                return null;
            }
        }

        public virtual OutrosEmpresa GetOutroEmpresa
        {
            get
            {
                if (this.Template == TemplateNotificacao.TemplateOutrosEmpresa)
                {
                    Vencimento vencimento = Vencimento.ConsultarPorId(this.Vencimento.Id);

                    if (vencimento != null && vencimento.Condicional != null)
                    {
                        return OutrosEmpresa.ConsultarPorId(vencimento.Condicional.Id);
                    }

                    return null;
                }

                return null;
            }
        }

        public static bool PossuiNotificacaoNoDia(DateTime data, Usuario usuario, ConfiguracaoPermissaoModulo configuracaoModuloDiversos, IList<Empresa> empresasPermissaoModuloDiversos, ConfiguracaoPermissaoModulo configuracaoModuloDNPM, IList<Empresa> empresasPermissaoModuloDNPM, IList<ProcessoDNPM> processosPermissaoModuloDNPM, ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente, IList<Empresa> empresasPermissaoModuloMeioAmbiente, IList<Processo> processosPermissaoModuloMeioAmbiente, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissaoModuloMeioAmbiente, IList<OutrosEmpresa> outrosEmpresasPermissaoModuloMeioAmbiente, ConfiguracaoPermissaoModulo configuracaoModuloContratos, IList<Empresa> empresasPermissaoModuloContratos, IList<Setor> setoresPermissaoModuloContratos)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Eq("Data", data));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);

            if (lista != null && lista.Count > 0 && usuario != null)
                lista = Notificacao.VerificarPermissoesNotificacoes(lista, usuario, configuracaoModuloDiversos, empresasPermissaoModuloDiversos, configuracaoModuloDNPM, empresasPermissaoModuloDNPM, processosPermissaoModuloDNPM, configuracaoModuloMeioAmbiente, empresasPermissaoModuloMeioAmbiente, processosPermissaoModuloMeioAmbiente, cadastrosTecnicosPermissaoModuloMeioAmbiente, outrosEmpresasPermissaoModuloMeioAmbiente, configuracaoModuloContratos, empresasPermissaoModuloContratos, setoresPermissaoModuloContratos);

            if (lista != null && lista.Count > 0)
                return true;

            return false;
        }

        public static bool PossuiNotificacaoNaoEnviadaNoDia(DateTime data, Usuario usuario, ConfiguracaoPermissaoModulo configuracaoModuloDiversos, IList<Empresa> empresasPermissaoModuloDiversos, ConfiguracaoPermissaoModulo configuracaoModuloDNPM, IList<Empresa> empresasPermissaoModuloDNPM, IList<ProcessoDNPM> processosPermissaoModuloDNPM, ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente, IList<Empresa> empresasPermissaoModuloMeioAmbiente, IList<Processo> processosPermissaoModuloMeioAmbiente, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissaoModuloMeioAmbiente, IList<OutrosEmpresa> outrosEmpresasPermissaoModuloMeioAmbiente, ConfiguracaoPermissaoModulo configuracaoModuloContratos, IList<Empresa> empresasPermissaoModuloContratos, IList<Setor> setoresPermissaoModuloContratos)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Eq("Data", data));
            aux.AdicionarFiltro(Filtros.MaiorEntreDuasPropriedades("Data", "DataUltimoEnvio"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);

            if (lista != null && lista.Count > 0 && usuario != null)
                lista = Notificacao.VerificarPermissoesNotificacoes(lista, usuario, configuracaoModuloDiversos, empresasPermissaoModuloDiversos, configuracaoModuloDNPM, empresasPermissaoModuloDNPM, processosPermissaoModuloDNPM, configuracaoModuloMeioAmbiente, empresasPermissaoModuloMeioAmbiente, processosPermissaoModuloMeioAmbiente, cadastrosTecnicosPermissaoModuloMeioAmbiente, outrosEmpresasPermissaoModuloMeioAmbiente, configuracaoModuloContratos, empresasPermissaoModuloContratos, setoresPermissaoModuloContratos);

            if (lista != null && lista.Count > 0)
                return true;

            return false;
        }

        public virtual Empresa GetEmpresa
        {
            get
            {
                return this.Vencimento != null ? this.Vencimento.GetEmpresa : null;
            }
        }

        public virtual GrupoEconomico GetGrupoEconomico
        {
            get
            {
                return this.Vencimento != null ? this.Vencimento.GetGrupoEconomico : null;
            }
        }

        public virtual string GetMessageTemplate(TemplateNotificacao template, bool retirarMensagemVencimentoStatusDiferenteDeCumprido)
        {
            if (template == null)
                return string.Empty;
            string messageTemplate = template.Template;
            //Pega as chaves que estão definidas na mensagem da notificação. Ex: #nome=Hugo
            string[] chaves = this.GetMensagem.Split(';');

            foreach (string aux in chaves)
                //Verifica se há chave e valor, caso contrário, não realizar a troca
                if (aux.Split('=').Length == 2)
                {
                    //Altera as chaves definidas na notificação pelos valores definidos na própria
                    string chave = aux.Split('=')[0];
                    string valor = aux.Split('=')[1];
                    if (retirarMensagemVencimentoStatusDiferenteDeCumprido && chave == "#MENSAGEM")
                    {
                        valor = "";
                    }
                    messageTemplate = messageTemplate.Replace(chave, valor);
                }
            return messageTemplate;
        }

        public static IList<Notificacao> FiltrarNotificacoes(DateTime dataDePeriodo, DateTime dataAtehPeriodo)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.IsNotNull("Vencimento"));
            aux.AdicionarFiltro(Filtros.Maior("Data", SqlDate.MinValue));
            aux.AdicionarFiltro(Filtros.Between("Data", dataDePeriodo, dataAtehPeriodo));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);
            IList<Notificacao> listaRetorno = new List<Notificacao>();
            if (lista != null)
                foreach (Notificacao not in lista)
                {
                    if (not.GetEmpresa != null && not.GetEmpresa.GrupoEconomico.Ativo && not.GetEmpresa.GrupoEconomico.AtivoLogus && not.GetEmpresa.GrupoEconomico.AtivoAmbientalis && !not.GetEmpresa.GrupoEconomico.Cancelado)
                        listaRetorno.Add(not);
                }
            return listaRetorno;
        }

        public static void ExcluirNotificacoesAvulsas()
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.IsNull("Vencimento"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> notis = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);
            if (notis != null)
                foreach (Notificacao noti in notis)
                    noti.Excluir();
        }

        public static IList<Notificacao> GetNotificacoesNaoEnviadasAteData(DateTime data)
        {

            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.IsNotNull("Vencimento"));
            aux.AdicionarFiltro(Filtros.FetchJoin("Vencimento"));
            aux.AdicionarFiltro(Filtros.Maior("Data", SqlDate.MinValue));
            aux.AdicionarFiltro(Filtros.MenorOuIgual("Data", data));
            aux.AdicionarFiltro(Filtros.MaiorEntreDuasPropriedades("Data", "DataUltimoEnvio"));


            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);

            if (lista != null)
                lista = lista.Where(x => x.GetEmpresa != null && x.GetEmpresa.Ativo && x.GetEmpresa.GrupoEconomico.Ativo && x.GetEmpresa.GrupoEconomico.AtivoLogus && x.GetEmpresa.GrupoEconomico.AtivoAmbientalis && !x.GetEmpresa.GrupoEconomico.Cancelado).ToList();

            return lista;
        }

        public static IList<Notificacao> GetNotificacoesNaoEnviadasNaProximaSemana(DateTime dataDe, DateTime dataAte)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.IsNotNull("Vencimento"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            aux.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
            aux.AdicionarFiltro(Filtros.MaiorEntreDuasPropriedades("Data", "DataUltimoEnvio"));
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);
            IList<Notificacao> listaRetorno = new List<Notificacao>();
            if (lista != null)
                foreach (Notificacao not in lista)
                {
                    if (not.GetEmpresa != null && not.GetEmpresa.Ativo && not.GetEmpresa.GrupoEconomico.Ativo && not.GetEmpresa.GrupoEconomico.AtivoLogus && not.GetEmpresa.GrupoEconomico.AtivoAmbientalis && !not.GetEmpresa.GrupoEconomico.Cancelado)
                        listaRetorno.Add(not);
                }
            return listaRetorno;
        }

        private string FormatarData(DateTime date)
        {
            if (date <= SqlDate.MinValue || date >= SqlDate.MaxValue)
                return "-- Não informada --";
            else
                return date.ToShortDateString();
        }

        public virtual string GetMensagem
        {
            get
            {
                try
                {

                    //DNPM
                    if (this.Template == TemplateNotificacao.Exigencia)
                    {
                        if (this.Vencimento.Exigencia.Regime != null)
                            return ("#EMPRESA=" + this.Vencimento.Exigencia.Regime.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.Exigencia.Regime.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.Exigencia.Regime.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.Exigencia.Regime.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PROTOCOLO=" + this.Vencimento.Exigencia.GetUltimoVencimento.ProtocoloAtendimento + ";#DATAPUBLICACAO=" + this.FormatarData(this.Vencimento.Exigencia.DataPublicacao) + ";#DESCRICAO=" + this.Vencimento.Exigencia.Descricao + ";#DIASPRAZO=" + this.Vencimento.Exigencia.DiasPrazo + ";#VENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.Exigencia.Regime.ProcessoDNPM != null && this.Vencimento.Exigencia.Regime.ProcessoDNPM.Cidade != null && this.Vencimento.Exigencia.Regime.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.Exigencia.Regime.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.Exigencia.Regime.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.Exigencia.Regime.ProcessoDNPM != null ? this.Vencimento.Exigencia.Regime.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                        else if (this.Vencimento.Exigencia.GuiaUtilizacao != null)
                            return ("#EMPRESA=" + this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PROTOCOLO=" + this.Vencimento.Exigencia.GetUltimoVencimento.ProtocoloAtendimento + ";#DATAPUBLICACAO=" + this.FormatarData(this.Vencimento.Exigencia.DataPublicacao) + ";#DESCRICAO=" + this.Vencimento.Exigencia.Descricao + ";#DIASPRAZO=" + this.Vencimento.Exigencia.DiasPrazo + ";#VENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM != null && this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Cidade != null && this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM != null ? this.Vencimento.Exigencia.GuiaUtilizacao.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.ValidadeExtracao)
                    {
                        return ("#EMPRESA=" + this.Vencimento.Extracao.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.Extracao.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.Extracao.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.Extracao.ProcessoDNPM.GetNumeroProcessoComMascara + ";#EXTRACAO=" + this.Vencimento.Extracao.NumeroExtracao + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.Extracao.DataPublicacao) + ";#DATAABERTURA=" + this.FormatarData(this.Vencimento.Extracao.DataAbertura) + ";#NUMEROLICENCA=" + this.Vencimento.Extracao.NumeroLicenca + ";#LICENCAVALIDA=" + this.FormatarData(this.Vencimento.Extracao.ValidadeLicenca) + ";#VENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.Extracao.ProcessoDNPM != null && this.Vencimento.Extracao.ProcessoDNPM.Cidade != null && this.Vencimento.Extracao.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.Extracao.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.Extracao.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.Extracao.ProcessoDNPM != null ? this.Vencimento.Extracao.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.ValidadeLicenciamento)
                    {
                        return ("#EMPRESA=" + this.Vencimento.Licenciamento.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.Licenciamento.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.Licenciamento.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.Licenciamento.ProcessoDNPM.GetNumeroProcessoComMascara + ";#NUMERO=" + this.Vencimento.Licenciamento.Numero + ";#ABERTURA=" + this.FormatarData(this.Vencimento.Licenciamento.DataAbertura) + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.Licenciamento.DataPublicacao) + ";#VENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.Licenciamento.ProcessoDNPM != null && this.Vencimento.Licenciamento.ProcessoDNPM.Cidade != null && this.Vencimento.Licenciamento.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.Licenciamento.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.Licenciamento.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.Licenciamento.ProcessoDNPM != null ? this.Vencimento.Licenciamento.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.ValidadeEntregaProtocolo)
                    {
                        return ("#EMPRESA=" + this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.GetNumeroProcessoComMascara + ";#NUMERO=" + this.Vencimento.EntregaLicencaOuProtocolo.Numero + ";#ABERTURA=" + this.FormatarData(this.Vencimento.EntregaLicencaOuProtocolo.DataAbertura) + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.EntregaLicencaOuProtocolo.DataPublicacao) + ";#DATALIMITE=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM != null && this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade != null && this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM != null ? this.Vencimento.EntregaLicencaOuProtocolo.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateRequerimentoEmissaoPosse)
                    {
                        return ("#EMPRESA=" + this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.GetNumeroProcessoComMascara + ";#DATAPUBLICACAO=" + this.FormatarData(this.Vencimento.RequerimentoImissaoPosse.Data) + ";#NUMEROPORTARIALAVRA=" + this.Vencimento.RequerimentoImissaoPosse.NumeroPortariaLavra + ";#VALIDADE=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM != null && this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Cidade != null && this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM != null ? this.Vencimento.RequerimentoImissaoPosse.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.ValidadeAlvaraPesquisa)
                    {
                        return ("#EMPRESA=" + this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.AlvaraPesquisa.ProcessoDNPM.GetNumeroProcessoComMascara + ";#DATAPUBLICACAO=" + this.FormatarData(this.Vencimento.AlvaraPesquisa.DataPublicacao) + ";#ENTREGARELATORIOPESQUISA=" + this.FormatarData(this.Vencimento.AlvaraPesquisa.DataEntregaRelatorio) + ";#APROVACAOEM=" + this.FormatarData(this.Vencimento.AlvaraPesquisa.DataAprovacaoRelatorio) + ";#VALIDADE=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.AlvaraPesquisa.ProcessoDNPM != null && this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Cidade != null && this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.AlvaraPesquisa.ProcessoDNPM != null ? this.Vencimento.AlvaraPesquisa.ProcessoDNPM.Identificacao : " - ") + ";#PRAZOLIMITE=" + this.FormatarData(this.Vencimento.Data.AddDays(-60)) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.DIPEM)
                    {
                        return ("#EMPRESA=" + this.Vencimento.DIPEM.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.DIPEM.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.DIPEM.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.DIPEM.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.DIPEM.DataPublicacao) + ";#ENTREGARELATORIOPESQUISA=" + this.FormatarData(this.Vencimento.DIPEM.DataEntregaRelatorio) + ";#APROVACAOEM=" + this.FormatarData(this.Vencimento.DIPEM.DataAprovacaoRelatorio) + ";#DATAVENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.DIPEM.ProcessoDNPM != null && this.Vencimento.DIPEM.ProcessoDNPM.Cidade != null && this.Vencimento.DIPEM.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.DIPEM.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.DIPEM.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.DIPEM.ProcessoDNPM != null ? this.Vencimento.DIPEM.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.InicioPesquisa)
                    {
                        return ("#EMPRESA=" + this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.NotificacaoPesquisaDNPM.DataPublicacao) + ";#ENTREGARELATORIOPESQUISA=" + this.FormatarData(this.Vencimento.NotificacaoPesquisaDNPM.DataEntregaRelatorio) + ";#APROVACAOEM=" + this.FormatarData(this.Vencimento.NotificacaoPesquisaDNPM.DataAprovacaoRelatorio) + ";#DATALIMITE=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM != null && this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade != null && this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM != null ? this.Vencimento.NotificacaoPesquisaDNPM.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateTaxaAnualHectare)
                    {
                        return ("#EMPRESA=" + this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.TaxaAnualPorHectare.DataPublicacao) + ";#ENTREGARELATORIOPESQUISA=" + this.FormatarData(this.Vencimento.TaxaAnualPorHectare.DataEntregaRelatorio) + ";#APROVACAOEM=" + this.FormatarData(this.Vencimento.TaxaAnualPorHectare.DataAprovacaoRelatorio) + ";#DATALIMITE=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM != null && this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Cidade != null && this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM != null ? this.Vencimento.TaxaAnualPorHectare.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateRequerimentoLavra)
                    {
                        return ("#EMPRESA=" + this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.RequerimentoLavraAlvaraPesquisa.DataPublicacao) + ";#ENTREGARELATORIOPESQUISA=" + this.FormatarData(this.Vencimento.RequerimentoLavraAlvaraPesquisa.DataEntregaRelatorio) + ";#APROVACAOEM=" + this.FormatarData(this.Vencimento.RequerimentoLavraAlvaraPesquisa.DataAprovacaoRelatorio) + ";#DATALIMITE=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null && this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade != null && this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM != null ? this.Vencimento.RequerimentoLavraAlvaraPesquisa.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.GuiaUtilizacao)
                    {
                        return ("#EMPRESA=" + this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.GetNumeroProcessoComMascara + ";#DATAREQUERIMENTO=" + this.FormatarData(this.Vencimento.GuiaDeUtilizacao.DataRequerimento) + ";#EMISSAO=" + this.FormatarData(this.Vencimento.GuiaDeUtilizacao.DataEmissao) + ";#VALIDADE=" + this.FormatarData(this.Vencimento.Data) + ";#LIMITERENOVACAO=" + this.FormatarData(this.Vencimento.GuiaDeUtilizacao.DataLimiteRequerimento) + ";#LOCAL=" + (this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM != null && this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Cidade != null && this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM != null ? this.Vencimento.GuiaDeUtilizacao.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateRequerimentoLPTotal)
                    {
                        return ("#EMPRESA=" + this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.GetNumeroProcessoComMascara + ";#PUBLICACAO=" + this.FormatarData(this.Vencimento.RequerimentoLPTotal.DataPublicacao) + ";#ENTREGARELATORIOPESQUISA=" + this.FormatarData(this.Vencimento.RequerimentoLPTotal.DataEntregaRelatorio) + ";#APROVACAOEM=" + this.FormatarData(this.Vencimento.RequerimentoLPTotal.DataAprovacaoRelatorio) + ";#DATAVENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LOCAL=" + (this.Vencimento.RequerimentoLPTotal.ProcessoDNPM != null && this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Cidade != null && this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.RequerimentoLPTotal.ProcessoDNPM != null ? this.Vencimento.RequerimentoLPTotal.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.RAL)
                    {
                        return ("#EMPRESA=" + this.Vencimento.Ral.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.Ral.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.Ral.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.Ral.ProcessoDNPM.GetNumeroProcessoComMascara + ";#VENCIMENTO=" + this.FormatarData(this.Vencimento.Ral.GetUltimoVencimento.Data) + ";#LOCAL=" + (this.Vencimento.Ral.ProcessoDNPM != null && this.Vencimento.Ral.ProcessoDNPM.Cidade != null && this.Vencimento.Ral.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.Ral.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.Ral.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#APELIDOAREA=" + (this.Vencimento.Ral.ProcessoDNPM != null ? this.Vencimento.Ral.ProcessoDNPM.Identificacao : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.LimiteRenuncia)
                    {
                        return ("#EMPRESA=" + this.Vencimento.LimiteRenuncia.ProcessoDNPM.Empresa.Nome + " - " + this.Vencimento.LimiteRenuncia.ProcessoDNPM.Empresa.ObterCnpjCpf(this.Vencimento.LimiteRenuncia.ProcessoDNPM.Empresa.DadosPessoa) + ";#PROCESSODNPM=" + this.Vencimento.LimiteRenuncia.ProcessoDNPM.GetNumeroProcessoComMascara + ";#APELIDOAREA=" + this.Vencimento.LimiteRenuncia.ProcessoDNPM.Identificacao + ";#ALVARA=" + this.Vencimento.LimiteRenuncia.Numero + ";#LOCAL=" + (this.Vencimento.LimiteRenuncia.ProcessoDNPM.Cidade != null && this.Vencimento.LimiteRenuncia.ProcessoDNPM.Cidade.Estado != null ? this.Vencimento.LimiteRenuncia.ProcessoDNPM.Cidade.Nome + " - " + this.Vencimento.LimiteRenuncia.ProcessoDNPM.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#VALIDADE=" + this.FormatarData(this.Vencimento.Data) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }


                    //Meio Ambiente
                    if (this.Template == TemplateNotificacao.TemplateCondicionante)
                    {
                        Condicionante condicionante = Condicionante.ConsultarPorId(this.Vencimento.Condicional.Id);
                        return ("#ORGAO=" + condicionante.GetOrgaoAmbiental + ";#PROCESSO=" + condicionante.GetNumeroProcesso + ";#EMPRESA=" + condicionante.GetEmpresa + " - " + condicionante.GetProcesso.Empresa.ObterCnpjCpf(condicionante.GetProcesso.Empresa.DadosPessoa) + ";#STATUS=" + condicionante.GetUltimoVencimento.Status.Nome + ";#NUMEROCONDICIONANTE=" + condicionante.Numero + ";#DESCRICAO=" + condicionante.Descricao + ";#DATAVENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#LICENCA=" + condicionante.Licenca.Numero + ";#TIPOLIC=" + condicionante.Licenca.TipoLicenca.Nome + ";#DESCLIC=" + condicionante.Licenca.Descricao + ";#LOCAL=" + (condicionante.Licenca.Cidade != null && condicionante.Licenca.Cidade.Estado != null ? condicionante.Licenca.Cidade.Nome + " - " + condicionante.Licenca.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateLicenca)
                    {
                        return ("#ORGAO=" + this.Vencimento.Licenca.Processo.OrgaoAmbiental.Nome + ";#PROCESSO=" + this.Vencimento.Licenca.Processo.Numero + ";#EMPRESA=" + this.Vencimento.Licenca.Processo.Empresa.Nome + " - " + this.Vencimento.Licenca.Processo.Empresa.ObterCnpjCpf(this.Vencimento.Licenca.Processo.Empresa.DadosPessoa) + ";#NUMERO=" + this.Vencimento.Licenca.Numero + ";#TIPOLICENÇA=" + this.Vencimento.Licenca.TipoLicenca.Nome + ";#DATARETIRADA=" + this.FormatarData(this.Vencimento.Licenca.DataRetirada)
                            + ";#VALIDADE=" + this.FormatarData(this.Vencimento.Data) + ";#LIMITERENOVACAO=" + this.FormatarData(this.Vencimento.Licenca.PrazoLimiteRenovacao) + ";#DESCRICAO=" + this.Vencimento.Licenca.Descricao + ";#LOCAL=" + (this.Vencimento.Licenca.Cidade != null && this.Vencimento.Licenca.Cidade.Estado != null ? this.Vencimento.Licenca.Cidade.Nome + " - " + this.Vencimento.Licenca.Cidade.Estado.PegarSiglaEstado() : " - ") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateOutrosEmpresa)
                    {
                        OutrosEmpresa outros = OutrosEmpresa.ConsultarPorId(this.Vencimento.Condicional.Id);
                        return ("#ORGAO=" + outros.OrgaoAmbiental.Nome + ";#EMPRESA=" + outros.Empresa.Nome + " - " + outros.Empresa.ObterCnpjCpf(outros.Empresa.DadosPessoa) + ";#DESCRICAO=" + outros.Descricao + ";#DATAVENCIMENTO=" + this.FormatarData(outros.GetUltimoVencimento.Data) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateOutrosProcesso)
                    {
                        OutrosProcesso outros = OutrosProcesso.ConsultarPorId(this.Vencimento.Condicional.Id);
                        return ("#ORGAO=" + outros.Processo.OrgaoAmbiental.Nome + ";#PROCESSO=" + outros.Processo.Numero + ";#EMPRESA=" + outros.Processo.Empresa.Nome + " - " + outros.Processo.Empresa.ObterCnpjCpf(outros.Processo.Empresa.DadosPessoa) + ";#DESCRICAO=" + outros.Descricao + ";#DATAVENCIMENTO=" + this.FormatarData(outros.GetUltimoVencimento.Data) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateRelatorioCTF)
                    {
                        return ("#EMPRESA=" + this.Vencimento.EntregaRelatorioAnual.Empresa.Nome + " - " + this.Vencimento.EntregaRelatorioAnual.Empresa.ObterCnpjCpf(this.Vencimento.EntregaRelatorioAnual.Empresa.DadosPessoa) + ";#ATIVIDADES=" + this.Vencimento.EntregaRelatorioAnual.Atividade + ";#NUMEROLICENCA=" + this.Vencimento.EntregaRelatorioAnual.NumeroLicenca + ";#VALIDADELICEN=" + this.FormatarData(this.Vencimento.EntregaRelatorioAnual.ValidadeLicenca) + ";#OBSERVACOES=" + this.Vencimento.EntregaRelatorioAnual.Observacoes + ";#DATAENTREGA=" + this.FormatarData(this.Vencimento.Data) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplatePagamentoCTF)
                    {
                        return ("#EMPRESA=" + this.Vencimento.TaxaTrimestral.Empresa.Nome + " - " + this.Vencimento.TaxaTrimestral.Empresa.ObterCnpjCpf(this.Vencimento.TaxaTrimestral.Empresa.DadosPessoa) + ";#SENHA=" + this.Vencimento.TaxaTrimestral.Senha + ";#ATIVIDADES=" + this.Vencimento.TaxaTrimestral.Atividade + ";#NUMEROLICENCA=" + this.Vencimento.TaxaTrimestral.NumeroLicenca + ";#VALIDADELICEN=" + this.FormatarData(this.Vencimento.TaxaTrimestral.ValidadeLicenca) + ";#OBSERVACOES=" + this.Vencimento.TaxaTrimestral.Observacoes + ";#DATAPAGAMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateCertificadoCTF)
                    {
                        return ("#EMPRESA=" + this.Vencimento.CertificadoRegularidade.Empresa.Nome + " - " + this.Vencimento.CertificadoRegularidade.Empresa.ObterCnpjCpf(this.Vencimento.CertificadoRegularidade.Empresa.DadosPessoa) + ";#SENHA=" + this.Vencimento.CertificadoRegularidade.Senha + ";#ATIVIDADES=" + this.Vencimento.CertificadoRegularidade.Atividade + ";#NUMEROLICENCA=" + this.Vencimento.CertificadoRegularidade.NumeroLicenca + ";#VALIDADELICEN=" + this.FormatarData(this.Vencimento.CertificadoRegularidade.ValidadeLicenca) + ";#OBSERVACOES=" + this.Vencimento.CertificadoRegularidade.Observacoes + ";#DATAVENCIMENTO=" + this.FormatarData(this.Vencimento.Data) + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    //Vencimentos Diversos
                    if (this.Template == TemplateNotificacao.TemplateVencimentoDiverso)
                    {
                        VencimentoDiverso vencimento = VencimentoDiverso.ConsultarPorId(this.Vencimento.Id);
                        return ("#EMPRESA=" + vencimento.Diverso.Empresa.Nome + " - " + vencimento.Diverso.Empresa.ObterCnpjCpf(vencimento.Diverso.Empresa.DadosPessoa) + ";#TIPOVENC=" + vencimento.Diverso.TipoDiverso.Nome + ";#STATUS=" + vencimento.StatusDiverso.Nome + ";#DESCRICAO=" + vencimento.Diverso.Descricao + ";#DATAVENCIMENTO=" + this.FormatarData(vencimento.Data) + ";#DETALHAMENTO=" + vencimento.Diverso.Detalhamento + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    //Vencimento de Contratos Diversos
                    if (this.Template == TemplateNotificacao.TemplateVencimentoContratoDiverso)
                    {
                        VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(this.Vencimento.Id);
                        return ("#EMPRESA=" + vencimento.ContratoDiverso.Empresa.Nome + " - " + vencimento.ContratoDiverso.Empresa.ObterCnpjCpf(vencimento.ContratoDiverso.Empresa.DadosPessoa) + ";#COMO=" + vencimento.ContratoDiverso.Como + ";#FORCLI=" + (vencimento.ContratoDiverso.Como == "Contratante" ? "Fornecedor(Contratada):" : "Cliente(Contratante):") + ";#FORNECEDOR=" + (vencimento.ContratoDiverso.Como == "Contratante" ? vencimento.ContratoDiverso.Fornecedor != null ? vencimento.ContratoDiverso.Fornecedor.Nome : "Não definido" : vencimento.ContratoDiverso.Cliente != null ? vencimento.ContratoDiverso.Cliente.Nome : "Não definido") + ";#CONTRATOSTATUS=" + vencimento.ContratoDiverso.StatusContratoDiverso.Nome + ";#CODIGO=" + vencimento.ContratoDiverso.Numero + ";#ABERTURACONTRATO=" + this.FormatarData(vencimento.ContratoDiverso.DataAbertura) + ";#OBJETO=" + vencimento.ContratoDiverso.Objeto + ";#CENTROCUSTO=" + (vencimento.ContratoDiverso.CentroCusto != null ? vencimento.ContratoDiverso.CentroCusto.Nome : "Não definido") + ";#SETOR=" + (vencimento.ContratoDiverso.Setor != null ? vencimento.ContratoDiverso.Setor.Nome : "Não definido") + ";#INDICEREAJUSTE=" + (vencimento.ContratoDiverso.IndiceFinanceiro != null ? vencimento.ContratoDiverso.IndiceFinanceiro.Nome : "Não definido") + ";#DATAVENCIMENTO=" + this.FormatarData(vencimento.Data) + ";#VALOR=" + (vencimento.ContratoDiverso != null ? "R$ " + Convert.ToDecimal(vencimento.ContratoDiverso.Valor).ToString("N2") : "") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    if (this.Template == TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso)
                    {
                        VencimentoContratoDiverso vencimento = VencimentoContratoDiverso.ConsultarPorId(this.Vencimento.Id);
                        return ("#EMPRESA=" + vencimento.Reajuste.Empresa.Nome + " - " + vencimento.Reajuste.Empresa.ObterCnpjCpf(vencimento.Reajuste.Empresa.DadosPessoa) + ";#COMO=" + vencimento.Reajuste.Como + ";#FORCLI=" + (vencimento.Reajuste.Como == "Contratante" ? "Fornecedor(Contratada):" : "Cliente(Contratante):") + ";#FORNECEDOR=" + (vencimento.Reajuste.Como == "Contratante" ? vencimento.Reajuste.Fornecedor != null ? vencimento.Reajuste.Fornecedor.Nome : "Não definido" : vencimento.Reajuste.Cliente != null ? vencimento.Reajuste.Cliente.Nome : "Não definido") + ";#CONTRATOSTATUS=" + vencimento.Reajuste.StatusContratoDiverso.Nome + ";#CODIGO=" + vencimento.Reajuste.Numero + ";#ABERTURACONTRATO=" + this.FormatarData(vencimento.Reajuste.DataAbertura) + ";#OBJETO=" + vencimento.Reajuste.Objeto + ";#CENTROCUSTO=" + (vencimento.Reajuste.CentroCusto != null ? vencimento.Reajuste.CentroCusto.Nome : "Não definido") + ";#SETOR=" + (vencimento.Reajuste.Setor != null ? vencimento.Reajuste.Setor.Nome : "Não definido") + ";#INDICEREAJUSTE=" + (vencimento.Reajuste.IndiceFinanceiro != null ? vencimento.Reajuste.IndiceFinanceiro.Nome : "Não definido") + ";#DATAVENCIMENTO=" + this.FormatarData(vencimento.Data) + ";#VALOR=" + (vencimento.Reajuste != null ? "R$ " + Convert.ToDecimal(vencimento.Reajuste.Valor).ToString("N2") : "") + ";#MENSAGEM=" + "A data deste vencimento foi atingida, porém seu status ainda não foi atualizado.");
                    }

                    return "";

                }
                catch (Exception)
                {
                    return "Erro ao retornar a mensagem da notificação.";
                }

            }
        }

        public static IList<Notificacao> AcertarDataDeUltimoEnvioDasNotificacoes()
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Maior("Enviado", 0));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);

        }

        public static IList<Notificacao> Consultar(string emails, string template, Usuario usuario, ConfiguracaoPermissaoModulo configuracaoModuloDiversos, IList<Empresa> empresasPermissaoModuloDiversos, ConfiguracaoPermissaoModulo configuracaoModuloDNPM, IList<Empresa> empresasPermissaoModuloDNPM, IList<ProcessoDNPM> processosPermissaoModuloDNPM, ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente, IList<Empresa> empresasPermissaoModuloMeioAmbiente, IList<Processo> processosPermissaoModuloMeioAmbiente, IList<CadastroTecnicoFederal> cadastrosTecnicosPermissaoModuloMeioAmbiente, IList<OutrosEmpresa> outrosEmpresasPermissaoModuloMeioAmbiente, ConfiguracaoPermissaoModulo configuracaoModuloContratos, IList<Empresa> empresasPermissaoModuloContratos, IList<Setor> setoresPermissaoModuloContratos)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Like("Emails", emails));

            if (template != "")
            {
                aux.AdicionarFiltro(Filtros.Eq("Template", template));
            }

            aux.AdicionarFiltro(Filtros.IsNotNull("Vencimento"));
            aux.AdicionarFiltro(Filtros.Maior("Data", SqlDate.MinValue));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Notificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);
            IList<Notificacao> listaRetorno = new List<Notificacao>();

            if (lista != null && lista.Count > 0 && usuario != null)
                lista = Notificacao.VerificarPermissoesNotificacoes(lista, usuario, configuracaoModuloDiversos, empresasPermissaoModuloDiversos, configuracaoModuloDNPM, empresasPermissaoModuloDNPM, processosPermissaoModuloDNPM, configuracaoModuloMeioAmbiente, empresasPermissaoModuloMeioAmbiente, processosPermissaoModuloMeioAmbiente, cadastrosTecnicosPermissaoModuloMeioAmbiente, outrosEmpresasPermissaoModuloMeioAmbiente, configuracaoModuloContratos, empresasPermissaoModuloContratos, setoresPermissaoModuloContratos);

            if (lista != null && lista.Count > 0)
                foreach (Notificacao not in lista)
                {
                    if (not.GetEmpresa != null && not.GetEmpresa.GrupoEconomico.Ativo && not.GetEmpresa.GrupoEconomico.AtivoLogus && not.GetEmpresa.GrupoEconomico.AtivoAmbientalis)
                        listaRetorno.Add(not);
                }
            return listaRetorno;
        }

        public virtual string GetTipoTemplate
        {
            get
            {
                try
                {

                    //DNPM
                    if (this.Template == TemplateNotificacao.Exigencia)
                    {
                        return "Exigência";
                    }

                    if (this.Template == TemplateNotificacao.ValidadeExtracao)
                    {

                        return "Validade da Extração";
                    }

                    if (this.Template == TemplateNotificacao.ValidadeLicenciamento)
                    {
                        return "Validade do Licenciamento";
                    }

                    if (this.Template == TemplateNotificacao.ValidadeEntregaProtocolo)
                    {
                        return "Validade da Entrega do Licenciamento ou Protocolo";
                    }

                    if (this.Template == TemplateNotificacao.TemplateRequerimentoEmissaoPosse)
                    {
                        return "Requerimento de Imissão de Posse";
                    }

                    if (this.Template == TemplateNotificacao.ValidadeAlvaraPesquisa)
                    {
                        return "Validade do Alvará de Pesquisa";
                    }

                    if (this.Template == TemplateNotificacao.DIPEM)
                    {
                        return "DIPEM";
                    }

                    if (this.Template == TemplateNotificacao.InicioPesquisa)
                    {
                        return "Início de Pesquisa";
                    }

                    if (this.Template == TemplateNotificacao.TemplateTaxaAnualHectare)
                    {
                        return "Taxa Anual por Hectare";
                    }

                    if (this.Template == TemplateNotificacao.TemplateRequerimentoLavra)
                    {
                        return "Requerimento de Lavra";
                    }

                    if (this.Template == TemplateNotificacao.GuiaUtilizacao)
                    {
                        return "Guia de Utilização";
                    }

                    if (this.Template == TemplateNotificacao.TemplateRequerimentoLPTotal)
                    {
                        return "Requerimento de LP Total";
                    }

                    if (this.Template == TemplateNotificacao.RAL)
                    {
                        return "RAL";
                    }

                    if (this.Template == TemplateNotificacao.LimiteRenuncia)
                    {
                        return "Renúncia de Alvará de Pesquisa";
                    }


                    //Meio Ambiente
                    if (this.Template == TemplateNotificacao.TemplateCondicionante)
                    {
                        return "Condicionante";
                    }

                    if (this.Template == TemplateNotificacao.TemplateLicenca)
                    {
                        return "Licença";
                    }

                    if (this.Template == TemplateNotificacao.TemplateOutrosEmpresa)
                    {
                        return "Outros de Empresa";
                    }

                    if (this.Template == TemplateNotificacao.TemplateOutrosProcesso)
                    {
                        return "Outros de Processo";
                    }

                    if (this.Template == TemplateNotificacao.TemplateRelatorioCTF)
                    {
                        return "Entrega do Relatório do CTF";
                    }

                    if (this.Template == TemplateNotificacao.TemplatePagamentoCTF)
                    {
                        return "Pagamento do CTF";
                    }

                    if (this.Template == TemplateNotificacao.TemplateCertificadoCTF)
                    {
                        return "Certificado do CTF";
                    }

                    //Vencimentos Diversos
                    if (this.Template == TemplateNotificacao.TemplateVencimentoDiverso)
                    {
                        return "Vencimento Diverso";
                    }

                    //Vencimento de Contratos Diversos
                    if (this.Template == TemplateNotificacao.TemplateVencimentoContratoDiverso)
                    {
                        return "Vencimento de Contrato";
                    }

                    if (this.Template == TemplateNotificacao.TemplateVencimentoRejusteContratoDiverso)
                    {
                        return "Reajuste de Contrato";
                    }

                    return this.Template;

                }
                catch (Exception)
                {
                    return "Erro ao retornar a mensagem da notificação.";
                }

            }
        }

        public virtual string GetDescricaoNotificacao
        {
            get
            {
                return "Notificação" + (this.Vencimento.GetTipo != null && this.Vencimento.GetTipo != "" ? " do Vencimento de " + this.Vencimento.GetTipo : string.Empty) + (this.GetEmpresa != null ? " da Empresa " + this.GetEmpresa.Nome : string.Empty);
            }
        }

        public static IList<Notificacao> ConsultarEntreOSIdsQuePossuamOEmail(int inicio, int fim, string email)
        {
            Notificacao aux = new Notificacao();
            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Like("Emails", email));                        

            aux.AdicionarFiltro(Filtros.IsNotNull("Vencimento"));

            aux.AdicionarFiltro(Filtros.MaiorOuIgual("Id", inicio));
            aux.AdicionarFiltro(Filtros.MenorOuIgual("Id", fim));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Notificacao>(aux);
            
        }
    }
}
