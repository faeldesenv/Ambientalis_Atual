using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class TemplateNotificacao : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static TemplateNotificacao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TemplateNotificacao classe = new TemplateNotificacao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<TemplateNotificacao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual TemplateNotificacao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<TemplateNotificacao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual TemplateNotificacao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<TemplateNotificacao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual TemplateNotificacao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<TemplateNotificacao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TemplateNotificacao> SalvarTodos(IList<TemplateNotificacao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TemplateNotificacao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TemplateNotificacao> SalvarTodos(params TemplateNotificacao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TemplateNotificacao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<TemplateNotificacao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<TemplateNotificacao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<TemplateNotificacao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TemplateNotificacao obj = Activator.CreateInstance<TemplateNotificacao>();
            return fabrica.GetDAOBase().ConsultarTodos<TemplateNotificacao>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<TemplateNotificacao> ConsultarOrdemAcendente(int qtd)
        {
            TemplateNotificacao ee = new TemplateNotificacao();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TemplateNotificacao>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<TemplateNotificacao> ConsultarOrdemDescendente(int qtd)
        {
            TemplateNotificacao ee = new TemplateNotificacao();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TemplateNotificacao>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de TemplateNotificacao
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<TemplateNotificacao> Filtrar(int qtd)
        {
            TemplateNotificacao estado = new TemplateNotificacao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TemplateNotificacao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo TemplateNotificacao Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo TemplateNotificacao</returns>
        public virtual TemplateNotificacao UltimoInserido()
        {
            TemplateNotificacao estado = new TemplateNotificacao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<TemplateNotificacao>(estado);
        }

        /// <summary>
        /// Consulta todos os TemplateNotificacaos armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os TemplateNotificacaos armazenados ordenados pelo Nome</returns>
        public static IList<TemplateNotificacao> ConsultarTodosOrdemAlfabetica()
        {
            TemplateNotificacao aux = new TemplateNotificacao();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TemplateNotificacao>(aux);
        }

        /// <summary>
        /// Retorna o template da notificação
        /// </summary>
        /// <param name="notificao">A notificação que contém a referência para o template</param>
        /// <returns>O template da notificação</returns>
        public static TemplateNotificacao GetTemplateNotificao(Notificacao notificao)
        {
            TemplateNotificacao aux = new TemplateNotificacao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.MaxResults(1));
            aux.AdicionarFiltro(Filtros.Eq("Nome", notificao.Template));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<TemplateNotificacao>(aux);
        }

        public virtual string GetTipo
        {
            get
            {                
                    //DNPM
                if (this.Nome == "Exigencia")
                    {
                        return "Exigência";
                    }

                if (this.Nome == "ValidadeExtracao")
                    {
                        return "Validade da Extração";
                    }

                if (this.Nome == "ValidadeLicenciamento")
                    {
                        return "Validade do Licenciamento";
                    }

                    if (this.Nome == "ValidadeEntregaProtocolo")
                    {
                        return "Validade da Entrega do Licenciamento ou Protocolo";
                    }

                    if (this.Nome == "RequerimentoEmissaoPosse")
                    {
                        return "Requerimento de Imissão de Posse";
                    }

                    if (this.Nome == "ValidadeAlvaraPesquisa")
                    {
                        return "Validade do Alvará de Pesquisa";
                    }

                    if (this.Nome == "DIPEM")
                    {
                        return "DIPEM";
                    }

                    if (this.Nome == "InicioPesquisa")
                    {
                        return "Início de Pesquisa";
                    }

                    if (this.Nome == "TaxaAnualHectare")
                    {
                        return "Taxa Anual por Hectare";
                    }

                    if (this.Nome == "RequerimentoLavra")
                    {
                        return "Requerimento de Lavra";
                    }

                    if (this.Nome == "GuiaUtilizacao")
                    {
                        return "Guia de Utilização";
                    }

                    if (this.Nome == "RequerimentoLPPoligonal")
                    {
                        return "Requerimento de LP Total";
                    }

                    if (this.Nome == "RAL")
                    {
                        return "RAL";
                    }

                    if (this.Nome == "LimiteRenuncia")
                    {
                        return "Renúncia de Alvará de Pesquisa";
                    }


                    //Meio Ambiente
                    if (this.Nome == "Condicionante")
                    {
                        return "Condicionante";
                    }

                    if (this.Nome == "Licenca")
                    {
                        return "Licença";
                    }

                    if (this.Nome == "OutrosEmpresa")
                    {
                        return "Outros de Empresa";
                    }

                    if (this.Nome == "OutrosProcesso")
                    {
                        return "Outros de Processo";
                    }

                    if (this.Nome == "RelatorioCTF")
                    {
                        return "Entrega do Relatório do CTF";
                    }

                    if (this.Nome == "PagamentoCTF")
                    {
                        return "Pagamento do CTF";
                    }

                    if (this.Nome == "CertificadoCTF")
                    {
                        return "Certificado do CTF";
                    }

                    //Vencimentos Diversos
                    if (this.Nome == "VencimentoDiverso")
                    {
                        return "Vencimento Diverso";
                    }

                    //Vencimento de Contratos Diversos
                    if (this.Nome == "TemplateVencimentoContratoDiverso")
                    {
                        return "Vencimento de Contrato";
                    }

                    if (this.Nome == "TemplateVencimentoRejusteContratoDiverso")
                    {
                        return "Reajuste de Contrato";
                    }
                    return this.Nome;
            }
        }

        public static IList<TemplateNotificacao> ConsultarTiposTemplate()
        {
            TemplateNotificacao aux = new TemplateNotificacao();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<TemplateNotificacao> lista = fabrica.GetDAOBase().ConsultarComFiltro<TemplateNotificacao>(aux);            

            return lista;
        }

        private static IList<TemplateNotificacao> FiltrarTemplatesPorPermissoesModulosUsuario(IList<TemplateNotificacao> lista, IList<ModuloPermissao> modulosUsuario)
        {
            if (modulosUsuario == null || modulosUsuario.Count == 0)
                return new List<TemplateNotificacao>();

            IList<TemplateNotificacao> listaAux = new List<TemplateNotificacao>();

            foreach (TemplateNotificacao template in lista)
            {
                if (template.GetModuloPorNomeTemplate() != null && modulosUsuario.Contains(template.GetModuloPorNomeTemplate()))
                    listaAux.Add(template);
            }

            return listaAux;

        }

        public virtual ModuloPermissao GetModuloPorNomeTemplate()
        {
            string nomeModulo = "";
            
                //DNPM
                if (this.Nome == "Exigencia" || this.Nome == "ValidadeExtracao" || this.Nome == "ValidadeLicenciamento" || this.Nome == "ValidadeEntregaProtocolo" || this.Nome == "RequerimentoEmissaoPosse" || this.Nome == "ValidadeAlvaraPesquisa" || this.Nome == "DIPEM" || this.Nome == "InicioPesquisa" || this.Nome == "TaxaAnualHectare" || this.Nome == "RequerimentoLavra" || this.Nome == "GuiaUtilizacao" || this.Nome == "RequerimentoLPPoligonal" || this.Nome == "RAL" || this.Nome == "LimiteRenuncia")
                {
                    nomeModulo = "DNPM";
                }

                //Meio Ambiente
                if (this.Nome == "Condicionante" || this.Nome == "Licenca" || this.Nome == "OutrosEmpresa" || this.Nome == "OutrosProcesso" || this.Nome == "RelatorioCTF" || this.Nome == "PagamentoCTF" || this.Nome == "CertificadoCTF")
                {
                    nomeModulo = "Meio Ambiente";
                }

                //Diversos
                if (this.Nome == "VencimentoDiverso")
                {
                    nomeModulo = "Diversos";
                }

                //Contratos Diversos
                if (this.Nome == "TemplateVencimentoContratoDiverso" || this.Nome == "TemplateVencimentoRejusteContratoDiverso")
                {
                    nomeModulo = "Contratos";
                }                
                return nomeModulo != "" ? ModuloPermissao.ConsultarPorNome(nomeModulo) : null; 
            
        }
    }
}
