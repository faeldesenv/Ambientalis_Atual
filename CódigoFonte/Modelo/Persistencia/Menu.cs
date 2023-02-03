using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Menu : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Menu ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Menu classe = new Menu();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Menu>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Menu ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Menu>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Menu Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Menu>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Menu SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Menu>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Menu> SalvarTodos(IList<Menu> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Menu>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Menu> SalvarTodos(params Menu[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Menu>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Menu>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Menu>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Menu> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Menu obj = Activator.CreateInstance<Menu>();
            return fabrica.GetDAOBase().ConsultarTodos<Menu>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Menu> ConsultarOrdemAcendente(int qtd)
        {
            Menu ee = new Menu();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            ee.AdicionarFiltro(Filtros.NaoIgual("Relatorio", true));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Menu> ConsultarOrdemDescendente(int qtd)
        {
            Menu ee = new Menu();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            ee.AdicionarFiltro(Filtros.NaoIgual("Relatorio", true));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Menu
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Menu> Filtrar(int qtd)
        {
            Menu estado = new Menu();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Menu Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Menu</returns>
        public virtual Menu UltimoInserido()
        {
            Menu estado = new Menu();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Menu>(estado);
        }

        /// <summary>
        /// Consulta todos os Menus armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Menus armazenados ordenados pelo Nome</returns>
        public static IList<Menu> ConsultarTodosOrdemAlfabetica()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.NaoIgual("Relatorio", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarOrdemAlfabetica()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarEmOrdemAlfabeticaSemRelatorios()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.NaoIgual("Relatorio", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarTodosOrdemPrioridade()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.NaoIgual("Relatorio", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarRelatoriosPorOrdemPrioridade()
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.NaoIgual("Relatorio", false));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        /// <summary>
        /// Retorna os menus de um cliente
        /// </summary>
        /// <param name="cliente">O Cliente</param>
        /// <returns>Uma lista de menus que o cliente possui</returns>
        public static IList<Menu> GetMenusDoCliente(GrupoEconomico grupo)
        {
            Menu menu = new Menu();
            menu.AdicionarFiltro(Filtros.Distinct());
            menu.AdicionarFiltro(Filtros.Eq("GrupoEconomico.Id", grupo != null ? grupo.Id : 0));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(menu);
        }

        /// <summary>
        /// Retorna um código HTML que representa o menu a ser exibido nas telas do sistema
        /// </summary>
        /// <param name="usuarioLogado">O usuário que possui as permissões de carregamento dos menus</param>
        /// <returns>Um código HTML que representa o correte Menu</returns>
        public virtual string GetHtmlMenu(Usuario usuarioLogado)
        {
            if (usuarioLogado != null)
            {
                StringBuilder builder = new StringBuilder();
                bool menuUnico = string.IsNullOrEmpty(this.UrlCadastro) || string.IsNullOrEmpty(this.UrlPesquisa);//|| !usuarioLogado.PermissaoEditar;

                //Se o menu tiver somente cadastro e o usuário não tiver permissão de editar, não carregar menu algum
                //if (menuUnico && string.IsNullOrEmpty(this.UrlPesquisa) )//!usuarioLogado.PermissaoEditar)
                //    return string.Empty;                

                if (menuUnico)
                {
                    if (this.Nome == "Manual")
                    {

                        builder.Append("<li class=\"" + this.Nome.Replace(" ", "").Trim() + "\"onclick=\"window.open('http://sustentar.web552.kinghost.net/wiki-sistema-sustentar/index.php?title=P%C3%A1gina_principal')\">");
                    }
                    else
                    {
                        builder.Append("<li class=\"" + this.Nome.Replace(" ", "").Trim() + (this.Nome.Replace(" ", "").Trim() == "DNPM" || this.Nome == "Usuário" || this.Nome == "Meio Ambiente" || this.Nome == "Contratos" || this.Nome == "Notificação" ? "'\">" : "\"onclick=\"document.location='../" +
                            this.UrlPesquisa + "'\">"));
                    }
                }
                else
                    builder.Append("<li class=\"" + this.Nome.Replace(" ", "").Trim() + "\">");
                builder.Append("<img src=\"../imagens/icone_" + this.Nome.Replace(" ", "").Trim() + ".png\" />");
                builder.Append("<div class=\"menu_texto\">" + this.Nome + "</div>");

                if (this.Nome.Replace(" ", "").Trim() == "DNPM")
                {
                    builder.Append("<ul class=\"menu_ul\">");
                    builder.Append("<li onclick=\"document.location='../" + "DNPM/DNPM.aspx" + "'\">Processos</li>");
                    builder.Append("<li onclick=\"document.location='../" + "DNPM/ConsultaDOU.aspx" + "'\">Consulta DOU</li>");
                    //builder.Append("<li onclick=\"document.location='../" + "DNPM/Eventos.aspx" + "'\">Eventos DNPM</li>");
                    //if (usuarioLogado.PossuiPermissaoDeEditarModuloDNPM) 
                    //{
                    //    builder.Append("<li onclick=\"document.location='../" + "DNPM/AtualizacaoDeProcessosDNPM.aspx" + "'\">Atualizar Processos DNPM</li>");
                    //    builder.Append("<li onclick=\"document.location='../" + "DNPM/ImportacaoProcessosDNPM.aspx" + "'\">Importar Processos DNPM</li>");
                    //}
                        
                    builder.Append("</ul>");
                }

                if (this.Nome == "Meio Ambiente")
                {
                    builder.Append("<ul class=\"menu_ul\">");
                    builder.Append("<li onclick=\"document.location='../" + "Processo/Processos.aspx" + "'\">Processos</li>");
                    builder.Append("<li onclick=\"document.location='../" + "Processo/ConsultaDOUMeioAmbiente.aspx" + "'\">Consulta DOU</li>");

                    if (usuarioLogado.PossuiPermissaoDeEditarModuloMeioAmbiente)
                    {
                        builder.Append("<li class=\"" + "OrgãoAmbiental" + "'\">Órgão Ambiental");
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "OrgaoAmbiental/CadastroOrgaoAmbiental.aspx" + "'\">Cadastrar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "OrgaoAmbiental/PesquisarOrgaosAmbientais.aspx" + "'\">Pesquisar</li>");
                        builder.Append("</ul></li>");
                        builder.Append("<li class=\"" + "TipodeLicença" + "'\">Tipo de Licença");
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "Licenca/ManterTipoLicenca.aspx" + "'\">Cadastrar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Licenca/PesquisarTipoLicencas.aspx" + "'\">Pesquisar</li>");
                        builder.Append("</ul></li>");
                    }
                    else
                    {
                        builder.Append("<li onclick=\"document.location='../" + "OrgaoAmbiental/PesquisarOrgaosAmbientais.aspx" + "'\">Órgão Ambiental</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Licenca/PesquisarTipoLicencas.aspx" + "'\">Tipo de Licença</li>");
                    }
                    
                    builder.Append("</ul>");
                }

                if (this.Nome == "Contratos")
                {
                    builder.Append("<ul class=\"menu_ul\">");

                    if (usuarioLogado.PossuiPermissaoDeEditarModuloContratos)
                    {
                        builder.Append("<li class=\"" + "Contrato" + "'\">Contratos");
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "Vencimentos/PesquisaVencimentosContrato.aspx" + "'\">Pesquisar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Vencimentos/CadastroVencimentosContrato.aspx" + "'\">Cadastrar</li>");
                        builder.Append("</ul></li>");
                        builder.Append("<li class=\"" + "Cliente" + "'\">Clientes");
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "Clientes/PesquisarClientes.aspx" + "'\">Pesquisar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Clientes/CadastroClientes.aspx" + "'\">Cadastrar</li>");
                        builder.Append("</ul></li>");
                        builder.Append("<li class=\"" + "Fornecedor" + "'\">Fornecedores");
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "Fornecedores/PesquisarFornecedores.aspx" + "'\">Pesquisar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Fornecedores/CadastrarFornecedores.aspx" + "'\">Cadastrar</li>");
                        builder.Append("</ul></li>");
                    }
                    else
                    {
                        builder.Append("<li onclick=\"document.location='../" + "Vencimentos/PesquisaVencimentosContrato.aspx" + "'\">Contratos</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Clientes/PesquisarClientes.aspx" + "'\">Clientes</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Fornecedores/PesquisarFornecedores.aspx" + "'\">Fornecedores</li>");
                    }

                    builder.Append("</ul>");
                    
                }

                if (this.Nome == "Notificação")
                {
                    builder.Append("<ul class=\"menu_ul\">");
                    builder.Append("<li onclick=\"document.location='../" + "Notificacao/Notificacoes.aspx" + "'\">Notificações</li>");
                    builder.Append("<li onclick=\"document.location='../" + "Notificacao/Emails.aspx" + "'\">E-mails</li>");
                    builder.Append("</ul>");
                }

                if (this.Nome == "Usuário")
                {
                    if (usuarioLogado.UsuarioAdministrador)
                    {
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "Usuario/ManterUsuario.aspx" + "'\">Cadastrar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Usuario/PesquisarUsuarios.aspx" + "'\">Pesquisar</li>");
                        builder.Append("<li onclick=\"document.location='../" + "Usuario/TrocarSenha.aspx" + "'\">Trocar Senha</li>");                        
                        builder.Append("</ul>");
                    }
                    else
                    {
                        builder.Append("<ul class=\"menu_ul\">");
                        builder.Append("<li onclick=\"document.location='../" + "Usuario/TrocarSenha.aspx" + "'\">Trocar Senha</li>");
                        builder.Append("</ul>");
                    }
                }

                if (!menuUnico)
                {
                    if (this.Nome == "Diversos")
                    {
                        builder.Append("<ul class=\"menu_ul\">");

                        if (usuarioLogado.PossuiPermissaoDeEditarModuloDiversos)
                        {
                            builder.Append("<li onclick=\"document.location='../" + this.UrlCadastro + "'\">Cadastrar</li>");
                            builder.Append("<li onclick=\"document.location='../" + this.UrlPesquisa + "'\">Pesquisar</li>");
                        }                            
                        else
                            builder.Append("<li onclick=\"document.location='../" + this.UrlPesquisa + "'\">Pesquisar</li>");

                        builder.Append("</ul>");
                    }
                    else 
                    {
                        builder.Append("<ul class=\"menu_ul\">");

                        if (usuarioLogado.PossuiPermissaoDeEditarModuloGeral) 
                        {
                            builder.Append("<li onclick=\"document.location='../" + this.UrlCadastro + "'\">Cadastrar</li>");
                            builder.Append("<li onclick=\"document.location='../" + this.UrlPesquisa + "'\">Pesquisar</li>");
                        }
                        else
                            builder.Append("<li onclick=\"document.location='../" + this.UrlPesquisa + "'\">Pesquisar</li>");

                        builder.Append("</ul>");

                    }

                }

                builder.Append("</li>");
                return builder.ToString();
            }

            return string.Empty;
        }

        public static string GetHtmlMenuRelatorio(Usuario usuarioLogado)
        {
            return "<li class=\"Relatório\"onclick=\"document.location='../Relatorios/FiltrosRelatorios.aspx'\">" +
                    "<img src=\"../imagens/icone_Relatorios.png\" />" +
                    "<div class=\"menu_texto\">Relatórios</div>" +
                    "</li>"; 
        }        

        public static IList<Menu> ConsultarTodosOrdemAlfabetica(GrupoEconomico grupo)
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.SubConsulta("GruposEconomicos"));
            aux.AdicionarFiltro(Filtros.Eq("Id", grupo != null ? grupo.Id : 0));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarTodosOrdemPrioridade(GrupoEconomico grupo, IList<ModuloPermissao> modulosUsuarioLogado)
        {
            Menu aux = new Menu();
            
            if (grupo != null && grupo.Id > 0) 
            {
                if (modulosUsuarioLogado == null || modulosUsuarioLogado.Count <= 0)
                    return new List<Menu>();

                aux.AdicionarFiltro(Filtros.CriarAlias("ModuloPermissao", "modPerm"));
                Filtro[] filtros = new Filtro[modulosUsuarioLogado.Count];
                for (int index = 0; index < modulosUsuarioLogado.Count; index++)
                    filtros[index] = Filtros.Eq("modPerm.Id", modulosUsuarioLogado[index].Id);
                aux.AdicionarFiltro(Filtros.Ou(filtros));
                
            }
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            //aux.AdicionarFiltro(Filtros.SubConsulta("GruposEconomicos"));
            //aux.AdicionarFiltro(Filtros.Eq("Id", grupo != null ? grupo.Id : 0));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarModulosUsuarioOrdemPrioridade(Usuario usuario, IList<ModuloPermissao> modulosUsuarioLogado)
        {
            Menu aux = new Menu();

            if (usuario != null && usuario.Id > 0)
            {
                if (modulosUsuarioLogado == null || modulosUsuarioLogado.Count <= 0)
                    return new List<Menu>();

                aux.AdicionarFiltro(Filtros.CriarAlias("ModuloPermissao", "modPerm"));
                Filtro[] filtros = new Filtro[modulosUsuarioLogado.Count];
                for (int index = 0; index < modulosUsuarioLogado.Count; index++)
                    filtros[index] = Filtros.Eq("modPerm.Id", modulosUsuarioLogado[index].Id);
                aux.AdicionarFiltro(Filtros.Ou(filtros));

            }
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            //aux.AdicionarFiltro(Filtros.SubConsulta("GruposEconomicos"));
            //aux.AdicionarFiltro(Filtros.Eq("Id", grupo != null ? grupo.Id : 0));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> ConsultarTodosQueNaoForemRelatoriosPorOrdemPrioridade()
        {
            Menu aux = new Menu();                        
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
            aux.AdicionarFiltro(Filtros.Eq("Relatorio", false));            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
        }

        public static IList<Menu> FiltrarRelatoriosDoUsuario(Usuario usuario, IList<ModuloPermissao> modulosUsuarioLogado)
        {
            if (usuario != null)
            {
                Menu aux = new Menu();
                aux.AdicionarFiltro(Filtros.Distinct());
                aux.AdicionarFiltro(Filtros.SetOrderAsc("Prioridade"));
                aux.AdicionarFiltro(Filtros.Eq("Relatorio", true));
                
                    if (modulosUsuarioLogado == null || modulosUsuarioLogado.Count <= 0)
                        return new List<Menu>();

                    aux.AdicionarFiltro(Filtros.CriarAlias("ModuloPermissao", "modPerm"));
                    Filtro[] filtros = new Filtro[modulosUsuarioLogado.Count];
                    for (int index = 0; index < modulosUsuarioLogado.Count; index++)
                        filtros[index] = Filtros.Eq("modPerm.Id", modulosUsuarioLogado[index].Id);
                    aux.AdicionarFiltro(Filtros.Ou(filtros));

                    //aux.AdicionarFiltro(Filtros.SubConsulta("GruposEconomicos"));
                    //aux.AdicionarFiltro(Filtros.Eq("Id", usuario.GrupoEconomico.Id));
               
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                return fabrica.GetDAOBase().ConsultarComFiltro<Menu>(aux);
            }
            return new List<Menu>();
        }

        public static Menu ConsultarPorPath(string urlTela)
        {
            Menu aux = new Menu();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("UrlPesquisa", urlTela));
            IList<Menu> telas = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<Menu>(aux);
            if (telas.Count > 0)
                return telas[0];
            return null;
        }
        
    }
}
