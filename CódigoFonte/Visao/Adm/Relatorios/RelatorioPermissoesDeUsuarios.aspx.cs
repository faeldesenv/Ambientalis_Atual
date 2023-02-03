using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Adm_Relatorios_RelatorioPermissoesDeUsuarios : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();
                this.CarregarCampos();
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunasAdm(grvRelatorio, ckbColunas, this.Page);
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Alert.Show(msg.Mensagem);
            }
    }

    #region ______________Métodos______________

    private void CarregarCampos()
    {
        this.CarregarGruposEconomicos();
    }

    private void CarregarGruposEconomicos()
    {
        ddlGrupoEconomico.DataTextField = "Nome";
        ddlGrupoEconomico.DataValueField = "Id";

        IList<GrupoEconomico> grupos = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        GrupoEconomico grupoAux = new GrupoEconomico();
        grupoAux.Nome = "-- Todos --";
        grupos.Insert(0, grupoAux);

        ddlGrupoEconomico.DataSource = grupos;
        ddlGrupoEconomico.DataBind();

        ddlGrupoEconomico.SelectedIndex = 0;
    }

    private void CarregarUsuariosAmbientalis()
    {
        ddlUsuario.DataTextField = "Nome";
        ddlUsuario.DataValueField = "Id";

        IList<Usuario> usuarios = Usuario.ConsultarTodosOrdemAlfabetica();

        Usuario usuarioAux = new Usuario();
        usuarioAux.Nome = "-- Todos --";
        usuarios.Insert(0, usuarioAux);

        ddlUsuario.DataSource = usuarios;
        ddlUsuario.DataBind();

        ddlUsuario.SelectedIndex = 0;
    }

    private void CarregarUsuariosSustentar(int idGrupoEconomico)
    {
        ddlUsuario.DataTextField = "Nome";
        ddlUsuario.DataValueField = "Id";

        GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(idGrupoEconomico);

        IList<Usuario> usuarios = grupo != null && grupo.Usuarios != null ? grupo.Usuarios : new List<Usuario>();

        Usuario usuarioAux = new Usuario();
        usuarioAux.Nome = "-- Todos --";
        usuarios.Insert(0, usuarioAux);

        ddlUsuario.DataSource = usuarios;
        ddlUsuario.DataBind();

        ddlUsuario.SelectedIndex = 0;
    }

    #endregion    

    #region ______________Bindins______________

    public string BindNome(object o) 
    {
        UsuarioPermissao usuario = (UsuarioPermissao)o;
        return usuario.nome;
    }

    public string BindPermissoesUsuario(object o)
    {
        UsuarioPermissao usuario = (UsuarioPermissao)o;
        return usuario.permissoes.ToString();
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            if (ddlSistema.SelectedValue.ToInt32() > 0)
            {
                ddlGrupoEconomico.SelectedIndex = 0;
                ddlGrupoEconomico.Enabled = false;
                this.CarregarUsuariosAmbientalis();
            }
            else 
            {
                ddlGrupoEconomico.Enabled = true;
                this.CarregarGruposEconomicos();
                this.CarregarUsuariosSustentar(0);
            }
                
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            Alert.Show(msg.Mensagem);
        }
    }        

    protected void ddlGrupoEconomico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            this.CarregarUsuariosSustentar(ddlGrupoEconomico.SelectedValue.ToInt32());
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            Alert.Show(msg.Mensagem);
        }
    }
    
    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            if (ddlSistema.SelectedValue.ToInt32() == 0 && ddlGrupoEconomico.SelectedValue.ToInt32() <= 0) 
            {
                msg.CriarMensagem("Selecione um Grupo Econômico para poder gerar o relatório", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarRelatorioPermissoes();            
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            Alert.Show(msg.Mensagem);
        }
    }

    private void CarregarRelatorioPermissoes()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<UsuarioPermissao> usuariosPermissoes = new List<UsuarioPermissao>();

        this.ObterPermissoesDosUsuarios(usuariosPermissoes);

        grvRelatorio.DataSource = usuariosPermissoes;
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        string descricaoSistema = ddlSistema.SelectedIndex != 0 ? "Ambientalis" : "Sustentar";        
        string descricaoGrupo = ddlGrupoEconomico.SelectedIndex != 0 ? ddlGrupoEconomico.SelectedItem.Text: "Todos";
        string descricaoUsuario = ddlUsuario.SelectedIndex != 0 ? ddlUsuario.SelectedItem.Text : "Todos";

        CtrlHeader.InsertFiltroEsquerda("Sistema", descricaoSistema);

        if (ddlSistema.SelectedValue.ToInt32() <= 0)
            CtrlHeader.InsertFiltroEsquerda("Grupo Econômico", descricaoGrupo);

        CtrlHeader.InsertFiltroCentro("Usuário", descricaoUsuario);
        

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Permissões por Usuários");

        RelatorioUtil.OcultarFiltros(this.Page); 
    }

    private void ObterPermissoesDosUsuarios(IList<UsuarioPermissao> usuariosPermissoes)
    {
        IList<Usuario> usuarios = Usuario.ConsultarRelatorioPermissoes(ddlGrupoEconomico.SelectedValue.ToInt32(), ddlUsuario.SelectedValue.ToInt32());

        IList<ModuloPermissao> modulosDoGrupo = new List<ModuloPermissao>();

        if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
            modulosDoGrupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32()).ModulosPermissao;
        else
            modulosDoGrupo = ModuloPermissao.ConsultarTodos();

        ModuloPermissao moduloAux = null;

        if (modulosDoGrupo != null && modulosDoGrupo.Count > 0)
        {    
            //Adicionando todos os usuários na lista
            foreach (Usuario usuario in usuarios)
                usuariosPermissoes.Add(new UsuarioPermissao(usuario.Id, usuario.Nome, ""));            

            IList<Usuario> usuariosAux;

            //Pegando todas as empresas do sistema
            IList<Empresa> empresas;

            if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
            {
                GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                empresas = grupo != null && grupo.Empresas != null && grupo.Empresas.Count > 0 ? grupo.Empresas : new List<Empresa>();
            }
            else
                empresas = Empresa.ConsultarTodosOrdemAlfabetica();


            //Salvando Configuracao Modulo Geral
            moduloAux = ModuloPermissao.ConsultarPorNome("Geral");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

                if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlGrupoEconomico.SelectedValue.ToInt32(), ModuloPermissao.ConsultarPorNome("Geral").Id);
                else
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(ModuloPermissao.ConsultarPorNome("Geral").Id);

                if (configuracaoModuloGeral != null) 
                {
                    //Pegando o usuario com permissao de visualizar e editar o modulo geral
                    if (configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Count == 0)
                        usuariosAux = usuarios;
                    else
                        usuariosAux = configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral;

                    foreach (Usuario usuario in usuariosAux)
                    {
                        if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario)) 
                        {
                            if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Contains(usuario))
                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Módulo Geral</strong> - Permissão de Edição e Visualização<br />");
                            else
                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Módulo Geral</strong> - Permissão de Visualização<br />");
                        }                        
                    }
                }  
                
            }

            //Configuracao Modulo DNPM
            moduloAux = ModuloPermissao.ConsultarPorNome("DNPM");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloDNPM = null;

                if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlGrupoEconomico.SelectedValue.ToInt32(), moduloAux.Id);
                else
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloDNPM != null) 
                {
                    //Configuração Modulo DNPM modo geral
                    if (configuracaoModuloDNPM.Tipo == 'G')
                    {
                        //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                        if (configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral.Count == 0)
                            usuariosAux = usuarios;
                        else
                            usuariosAux = configuracaoModuloDNPM.UsuariosVisualizacaoModuloGeral;

                        foreach (Usuario usuario in usuariosAux)
                        {
                            if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                            {
                                if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(usuario))
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo ANM</strong> - Permissão de Edição e Visualização<br />");
                                else
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo ANM</strong> - Permissão de Visualização<br />");
                            }                            
                        }
                    }

                    //Configuração Modulo DNPM por empresa
                    if (configuracaoModuloDNPM.Tipo == 'E')
                    {
                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {
                                empresa.ConsultarPorId();
                                EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("DNPM").Id);

                                if (empresaPermissaoDNPM != null) 
                                {
                                    //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                                    if (empresaPermissaoDNPM.UsuariosVisualizacao == null || empresaPermissaoDNPM.UsuariosVisualizacao.Count == 0)
                                        usuariosAux = usuarios;
                                    else
                                        usuariosAux = empresaPermissaoDNPM.UsuariosVisualizacao;

                                    foreach (Usuario usuario in usuariosAux)
                                    {
                                        if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                        {
                                            //Adiciona o titulo somente antes da primeira empresa
                                            if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo ANM (Controle por Empresa)</strong><br />") == false)
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo ANM (Controle por Empresa)</strong><br />");

                                            if (empresaPermissaoDNPM.UsuariosEdicao != null && empresaPermissaoDNPM.UsuariosEdicao.Count > 0 && empresaPermissaoDNPM.UsuariosEdicao.Contains(usuario))
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                            else
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");
                                        }                                        
                                    }
                                } 
                            }
                        }
                    }


                    //Configuração Modulo DNPM por processo                    
                    if (configuracaoModuloDNPM.Tipo == 'P')
                    {
                        IList<ProcessoDNPM> processos;

                        if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                        {
                            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                            processos = grupo != null ? ProcessoDNPM.ConsultarProcessosDoCliente(grupo) : new List<ProcessoDNPM>();
                        }
                        else
                            processos = ProcessoDNPM.ConsultarTodos();

                        if (processos != null && processos.Count > 0)
                        {
                            foreach (ProcessoDNPM processoAux in processos)
                            {
                                //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                                if (processoAux.UsuariosVisualizacao == null || processoAux.UsuariosVisualizacao.Count == 0)
                                    usuariosAux = usuarios;
                                else
                                    usuariosAux = processoAux.UsuariosVisualizacao;

                                foreach (Usuario usuario in usuariosAux)
                                {
                                    if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                    {
                                        //Adiciona o titulo somente antes do primeiro processo
                                        if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo ANM (Controle por Processo)</strong><br />") == false)
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo ANM (Controle por Processo)</strong><br />");

                                        if (processoAux.UsuariosEdicao != null && processoAux.UsuariosEdicao.Count > 0 && processoAux.UsuariosEdicao.Contains(usuario))
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Processo:</strong> " + processoAux.GetNumeroProcessoComMascara + " - Permissão de Edição e Visualização<br />");
                                        else
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Processo:</strong> " + processoAux.GetNumeroProcessoComMascara + " - Permissão de Visualização<br />");
                                    }                                    
                                }
                            }
                        }
                    }
                }                
            }


            //Configuracao Modulo Meio Ambiente
            moduloAux = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente = null;

                if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                    configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlGrupoEconomico.SelectedValue.ToInt32(), moduloAux.Id);
                else
                    configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloMeioAmbiente != null) 
                {
                    //Configuração Modulo Meio Ambiente modo geral                
                    if (configuracaoModuloMeioAmbiente.Tipo == 'G')
                    {
                        //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                        if (configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral.Count == 0)
                            usuariosAux = usuarios;
                        else
                            usuariosAux = configuracaoModuloMeioAmbiente.UsuariosVisualizacaoModuloGeral;

                        foreach (Usuario usuario in usuariosAux)
                        {
                            if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                            {
                                if (configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Contains(usuario))
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Meio Ambiente</strong> - Permissão de Edição e Visualização<br />");
                                else
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Meio Ambiente</strong> - Permissão de Visualização<br />");
                            }                            
                        }
                    }

                    //Configuração Modulo Meio Ambiente por empresa                
                    if (configuracaoModuloMeioAmbiente.Tipo == 'E')
                    {
                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {                                
                                EmpresaModuloPermissao empresaPermissaoMeioAmbiente = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Meio Ambiente").Id);

                                if (empresaPermissaoMeioAmbiente != null) 
                                {
                                    //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                                    if (empresaPermissaoMeioAmbiente.UsuariosVisualizacao == null || empresaPermissaoMeioAmbiente.UsuariosVisualizacao.Count == 0)
                                        usuariosAux = usuarios;
                                    else
                                        usuariosAux = empresaPermissaoMeioAmbiente.UsuariosVisualizacao;

                                    foreach (Usuario usuario in usuariosAux)
                                    {
                                        if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                        {
                                            //Adiciona o titulo somente antes da primeira empresa
                                            if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Empresa)</strong><br />") == false)
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Meio Ambiente (Controle por Empresa)</strong><br />");

                                            if (empresaPermissaoMeioAmbiente.UsuariosEdicao != null && empresaPermissaoMeioAmbiente.UsuariosEdicao.Count > 0 && empresaPermissaoMeioAmbiente.UsuariosEdicao.Contains(usuario))
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                            else
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");
                                        }                                        
                                    }
                                }  
                            }
                        }
                    }


                    //Configuração Modulo Meio Ambiente por processo                    
                    if (configuracaoModuloMeioAmbiente.Tipo == 'P')
                    {
                        //Processos
                        IList<Processo> processos;

                        if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                        {
                            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                            processos = grupo != null ? Processo.ConsultarProcessosDoCliente(grupo) : new List<Processo>();
                        }
                        else
                            processos = Processo.ConsultarTodos();

                        if (processos != null && processos.Count > 0)
                        {
                            foreach (Processo processoAux in processos)
                            {   
                                //Pegando o usuario com permissao de visualizar e editar o modulo dnpm
                                if (processoAux.UsuariosVisualizacao == null || processoAux.UsuariosVisualizacao.Count == 0)
                                    usuariosAux = usuarios;
                                else
                                    usuariosAux = processoAux.UsuariosVisualizacao;

                                foreach (Usuario usuario in usuariosAux)
                                {
                                    if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                    {
                                        //Adiciona o titulo somente antes do primeiro processo
                                        if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />") == false)
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />");

                                        if (processoAux.UsuariosEdicao != null && processoAux.UsuariosEdicao.Count > 0 && processoAux.UsuariosEdicao.Contains(usuario))
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Processo:</strong> " + processoAux.Numero + " - Permissão de Edição e Visualização<br />");
                                        else
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Processo:</strong> " + processoAux.Numero + " - Permissão de Visualização<br />");
                                    }                                    
                                }
                            }
                        }


                        //Cadastros Tecnicos
                        IList<CadastroTecnicoFederal> cadastros;

                        if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                        {
                            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                            cadastros = grupo != null ? CadastroTecnicoFederal.ConsultarPorGrupoEconomico(grupo.Id) : new List<CadastroTecnicoFederal>();
                        }
                        else
                            cadastros = CadastroTecnicoFederal.ConsultarTodos();

                        if (cadastros != null && cadastros.Count > 0)
                        {
                            foreach (CadastroTecnicoFederal cadastroAux in cadastros)
                            {
                                //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                                if (cadastroAux.UsuariosVisualizacao == null || cadastroAux.UsuariosVisualizacao.Count == 0)
                                    usuariosAux = usuarios;
                                else
                                    usuariosAux = cadastroAux.UsuariosVisualizacao;

                                foreach (Usuario usuario in usuariosAux)
                                {
                                    if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                    {
                                        //Adiciona o titulo somente antes do primeiro cadastro
                                        if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />") == false)
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />");

                                        if (cadastroAux.UsuariosEdicao != null && cadastroAux.UsuariosEdicao.Count > 0 && cadastroAux.UsuariosEdicao.Contains(usuario))
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Cadastro Técnico Federal:</strong> " + cadastroAux.GetNomeEmpresa + " - Permissão de Edição e Visualização<br />");
                                        else
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Cadastro Técnico Federal:</strong> " + cadastroAux.GetNomeEmpresa + " - Permissão de Visualização<br />");
                                    }                                    
                                }
                            }
                        }


                        //Outros de Empresa                        
                        IList<OutrosEmpresa> outrosEmpresa;

                        if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                        {
                            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                            outrosEmpresa = grupo != null ? OutrosEmpresa.ConsultarPorGrupoEconomico(grupo.Id) : new List<OutrosEmpresa>();
                        }
                        else
                            outrosEmpresa = OutrosEmpresa.ConsultarTodos();

                        if (outrosEmpresa != null && outrosEmpresa.Count > 0)
                        {
                            foreach (OutrosEmpresa outrosAux in outrosEmpresa)
                            {
                                //Pegando o usuario com permissao de visualizar e editar o modulo meio ambiente
                                if (outrosAux.UsuariosVisualizacao == null || outrosAux.UsuariosVisualizacao.Count == 0)
                                    usuariosAux = usuarios;
                                else
                                    usuariosAux = outrosAux.UsuariosVisualizacao;

                                foreach (Usuario usuario in usuariosAux)
                                {
                                    if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                    {
                                        //Adiciona o titulo somente antes do primeiro outro de empresa
                                        if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />") == false)
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Meio Ambiente (Controle por Processo)</strong><br />");

                                        if (outrosAux.UsuariosEdicao != null && outrosAux.UsuariosEdicao.Count > 0 && outrosAux.UsuariosEdicao.Contains(usuario))
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Outro de Empresa:</strong> " + outrosAux.GetNomeEmpresa + " - Permissão de Edição e Visualização<br />");
                                        else
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Outro de Empresa:</strong> " + outrosAux.GetNomeEmpresa + " - Permissão de Visualização<br />");
                                    }                                    
                                }
                            }
                        }
                    }
                }                  
            }

            //Configuracao Modulo Contratos
            moduloAux = ModuloPermissao.ConsultarPorNome("Contratos");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloContratos = null;

                if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlGrupoEconomico.SelectedValue.ToInt32(), moduloAux.Id);
                else
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloContratos != null) 
                {
                    //Configuração Modulo Contratos modo geral
                    if (configuracaoModuloContratos.Tipo == 'G')
                    {
                        //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                        if (configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count == 0)
                            usuariosAux = usuarios;
                        else
                            usuariosAux = configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral;

                        foreach (Usuario usuario in usuariosAux)
                        {
                            if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                            {
                                if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(usuario))
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Contratos</strong> - Permissão de Edição e Visualização<br />");
                                else
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Contratos</strong> - Permissão de Visualização<br />");
                            }                            
                        }
                    }

                    //Configuração Modulo Contratos por empresa                    
                    if (configuracaoModuloContratos.Tipo == 'E')
                    {
                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {                                
                                EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Contratos").Id);

                                if (empresaPermissaoContratos != null) 
                                {
                                    //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                                    if (empresaPermissaoContratos.UsuariosVisualizacao == null || empresaPermissaoContratos.UsuariosVisualizacao.Count == 0)
                                        usuariosAux = usuarios;
                                    else
                                        usuariosAux = empresaPermissaoContratos.UsuariosVisualizacao;

                                    foreach (Usuario usuario in usuariosAux)
                                    {
                                        if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                        {
                                            //Adiciona o titulo somente antes da primeira empresa
                                            if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Contratos (Controle por Empresa)</strong><br />") == false)
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Contratos (Controle por Empresa)</strong><br />");

                                            if (empresaPermissaoContratos.UsuariosEdicao != null && empresaPermissaoContratos.UsuariosEdicao.Count > 0 && empresaPermissaoContratos.UsuariosEdicao.Contains(usuario))
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                            else
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");
                                        }                                        
                                    }
                                }                                
                            }
                        }
                    }


                    //Configuração Modulo Contratos por setor                      
                    if (configuracaoModuloContratos.Tipo == 'S')
                    {
                        IList<Setor> setores;

                        if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                        {
                            GrupoEconomico grupo = GrupoEconomico.ConsultarPorId(ddlGrupoEconomico.SelectedValue.ToInt32());
                            setores = grupo != null ? Setor.ConsultarSetoresDoCliente(grupo) : new List<Setor>();
                        }
                        else
                            setores = Setor.ConsultarTodos();

                        if (setores != null && setores.Count > 0)
                        {
                            foreach (Setor setorAux in setores)
                            {
                                //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                                if (setorAux.UsuariosVisualizacao == null || setorAux.UsuariosVisualizacao.Count == 0)
                                    usuariosAux = usuarios;
                                else
                                    usuariosAux = setorAux.UsuariosVisualizacao;

                                foreach (Usuario usuario in usuariosAux)
                                {
                                    if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                    {
                                        //Adiciona o titulo somente antes do primeiro setor
                                        if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Contratos (Controle por Setor)</strong><br />") == false)
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Contratos (Controle por Setor)</strong><br />");

                                        if (setorAux.UsuariosEdicao != null && setorAux.UsuariosEdicao.Count > 0 && setorAux.UsuariosEdicao.Contains(usuario))
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Setor:</strong> " + setorAux.Nome + " - Permissão de Edição e Visualização<br />");
                                        else
                                            usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Setor:</strong> " + setorAux.Nome + " - Permissão de Visualização<br />");
                                    }                                    
                                }
                            }
                        }
                    }
                }
                
            }

            //Configuracao Modulo Diversos
            moduloAux = ModuloPermissao.ConsultarPorNome("Diversos");
            if (modulosDoGrupo.Contains(moduloAux))
            {
                ConfiguracaoPermissaoModulo configuracaoModuloDiversos = null;

                if (ddlSistema.SelectedValue.ToInt32() <= 0 && ddlGrupoEconomico.SelectedValue.ToInt32() > 0)
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlGrupoEconomico.SelectedValue.ToInt32(), moduloAux.Id);
                else
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloAux.Id);

                if (configuracaoModuloDiversos != null) 
                {
                    //Configuração Modulo Diversos modo geral
                    if (configuracaoModuloDiversos.Tipo == 'G')
                    {
                        //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                        if (configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count == 0)
                            usuariosAux = usuarios;
                        else
                            usuariosAux = configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral;

                        foreach (Usuario usuario in usuariosAux)
                        {
                            if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                            {
                                if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(usuario))
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Diversos</strong> - Permissão de Edição e Visualização<br />");
                                else
                                    usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Diversos</strong> - Permissão de Visualização<br />");
                            }                            
                        }
                    }

                    //Configuração Modulo Diversos por Empresa                    
                    if (configuracaoModuloDiversos.Tipo == 'E')
                    {
                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {                                
                                EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, ModuloPermissao.ConsultarPorNome("Diversos").Id);

                                if (empresaPermissaoDiversos != null) 
                                {
                                    //Pegando o usuario com permissao de visualizar e editar o modulo contratos
                                    if (empresaPermissaoDiversos.UsuariosVisualizacao == null || empresaPermissaoDiversos.UsuariosVisualizacao.Count == 0)
                                        usuariosAux = usuarios;
                                    else
                                        usuariosAux = empresaPermissaoDiversos.UsuariosVisualizacao;

                                    foreach (Usuario usuario in usuariosAux)
                                    {
                                        if (usuarios != null && usuarios.Count > 0 && usuarios.Contains(usuario))
                                        {
                                            //Adiciona o titulo somente antes da primeira empresa
                                            if (usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.ToString().Contains("<br /><strong>Módulo Diversos (Controle por Empresa)</strong><br />") == false)
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<br /><strong>Módulo Diversos (Controle por Empresa)</strong><br />");

                                            if (empresaPermissaoDiversos.UsuariosEdicao != null && empresaPermissaoDiversos.UsuariosEdicao.Count > 0 && empresaPermissaoDiversos.UsuariosEdicao.Contains(usuario))
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Edição e Visualização<br />");
                                            else
                                                usuariosPermissoes[usuariosPermissoes.IndexOf(new UsuarioPermissao(usuario.Id))].permissoes.Append("<strong>Empresa:</strong> " + empresa.Nome + " - Permissão de Visualização<br />");
                                        }                                        
                                    }
                                }                                
                            }
                        }
                    }
                }
            }           
        }
    }

    #endregion

    #region --------------- Inner Class ---------------

    public class UsuarioPermissao 
    {
        public string nome;
        public StringBuilder permissoes;
        public int id;

        public UsuarioPermissao(int id, string nome, string permissoes) 
        {
            this.id = id;
            this.nome = nome;

            if (this.permissoes == null)
                this.permissoes = new StringBuilder();

            this.permissoes.Append(permissoes);
        }

        public UsuarioPermissao(int id)
        {
            this.id = id;            
        }

        public override bool Equals(object obj)
        {
            try
            {
                if (this.id == 0 && ((UsuarioPermissao)obj).id == 0)
                    return this == obj;
                else
                    return this.id == ((UsuarioPermissao)obj).id;
            }
            catch (Exception) { return base.Equals(obj); }
        }
    }

    #endregion
}