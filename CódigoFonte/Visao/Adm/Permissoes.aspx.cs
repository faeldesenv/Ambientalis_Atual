using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Permissoes_Permissoes : PageBase
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["idConfig"] = ddlSistema.SelectedValue;
                transacao.Abrir();
                this.CarregarCampos();
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                transacao.Fechar(ref msg);
                this.GetMBOX<MBOX>().Show(msg);
            }
        }
    }

    #region ____________________ Eventos _______________________

    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.CarregarGrid();            
            this.MarcarGridMenus();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            transacao.Abrir();
            this.SalvarPermissoes();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void dgr_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        WebUtil.CriarEventoOnMouseOverDoGridView(e);
    }

    protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = ddlSistema.SelectedValue;
            transacao.Abrir();

            if (ddlSistema.SelectedValue.ToInt32() > 0)
            {
                
                base_sustentar.Visible = false;
                
                grid_menus.Visible = false;
                this.CarregarGridAmbientalis();
                this.CarregarSetoresAmbientalis();
                this.CarregarUsuariosBaseAmbientalis();
            }
            else
            {                
                base_sustentar.Visible = true;
                
                grid_menus.Visible = true;
            }

            this.CarregarCampos();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }        

    protected void dgrModulos_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            transacao.Abrir();
            dgrModulos.CurrentPageIndex = e.NewPageIndex;
            this.CarregarGrid();
            this.MarcarGridMenus();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
            this.GetMBOX<MBOX>().Show(msg);
        }
    }

    protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void dgrSetores_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            
        }
    }

    #endregion

    #region ____________________ Métodos _______________________
     
    private void CarregarCampos()
    {
        this.CarregarGrid();
        this.CarregarClientes();        
        this.MarcarGridMenus();
    }

    private void MarcarGridMenus()
    {
        GrupoEconomico cliente = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());
        if (cliente != null && cliente.Id > 0) 
        {
            if (cliente.GestaoCompartilhada)
                rblTipoPermissaoPor.SelectedValue = "C";
            else
                rblTipoPermissaoPor.SelectedValue = "N";            

            foreach (DataGridItem item in dgrModulos.Items)
            {
                ((CheckBox)item.FindControl("ckbExcluir")).Checked =
                    cliente.ModulosPermissao != null && cliente.ModulosPermissao.Contains(new Modelo.ModuloPermissao(dgrModulos.DataKeys[item.ItemIndex].ToString().ToInt32()));                
            }
        }
    }

    private void CarregarSetoresAmbientalis()
    {
        
    }

    private void CarregarGrid()
    {
        dgrModulos.DataSource = ModuloPermissao.ConsultarTodosOrdemPrioridade(null);
        dgrModulos.DataBind();
    }

    private void CarregarGridAmbientalis()
    {
        
    }

    private void CarregarClientes()
    {
        ddlCliente.DataTextField = "Nome";
        ddlCliente.DataValueField = "Id";

        IList<GrupoEconomico> clientes = GrupoEconomico.ConsultarTodosOrdemAlfabetica();
        GrupoEconomico aux = new GrupoEconomico();
        aux.Nome = "-- Selecione --";
        clientes.Insert(0, aux);

        ddlCliente.DataSource = clientes;
        ddlCliente.DataBind();
    }

    private void CarregarUsuariosBaseAmbientalis()
    {       
        
    }

    private void SalvarPermissoes()
    {
        if (ddlSistema.SelectedValue.ToInt32() > 0)
        {

        }
        else 
        {
            GrupoEconomico cliente = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());
            if (cliente == null)
            {
                msg.CriarMensagem("É necessário selecionar ao menos um cliente", "Informação", MsgIcons.Informacao);
                return;
            }

            cliente.GestaoCompartilhada = rblTipoPermissaoPor.SelectedValue == "C";

            IList<Modelo.ModuloPermissao> modulos = this.GetModulosSelecionados();
            cliente.ModulosPermissao = modulos;

            cliente = cliente.Salvar();
            this.RemmoverPermissoesRemovidasDosUsuariosDoGrupo(cliente);
        }
        
        msg.CriarMensagem("Permissões salvas com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void RemmoverPermissoesRemovidasDosUsuariosDoGrupo(GrupoEconomico cliente)
    {
        if (cliente.Usuarios != null && cliente.Usuarios.Count > 0) 
        {
            foreach (Usuario usuario in cliente.Usuarios)
            {
                if (usuario.ModulosPermissao != null && usuario.ModulosPermissao.Count > 0) 
                {
                    if (cliente.ModulosPermissao == null || cliente.ModulosPermissao.Count == 0)
                        usuario.ModulosPermissao = new List<ModuloPermissao>();
                    else 
                    {
                        for (int i = usuario.ModulosPermissao.Count - 1; i > -1; i--)
                        {
                            if (!cliente.ModulosPermissao.Contains(usuario.ModulosPermissao[i]))
                                usuario.ModulosPermissao.Remove(usuario.ModulosPermissao[i]);
                        }
                    }
                    
                }

                usuario.Salvar();
            }

            cliente.Salvar();

        }
    }

    private IList<Setor> GetSetoresSelecionados()
    {
        IList<Setor> setores = new List<Setor>();    

        return setores;
    }

    private IList<Modelo.ModuloPermissao> GetModulosSelecionados()
    {
        IList<Modelo.ModuloPermissao> modulos = new List<Modelo.ModuloPermissao>();

        foreach (DataGridItem item in dgrModulos.Items) 
        {
            if (((CheckBox)item.FindControl("ckbExcluir")).Checked) 
            {
                Modelo.ModuloPermissao moduloAux = Modelo.ModuloPermissao.ConsultarPorId(dgrModulos.DataKeys[item.ItemIndex].ToString().ToInt32());

                modulos.Add(moduloAux);

                //se foi selecionado como gestão compartilhada verificar se antigamente era gestao comum e foi alterada para gestão compartilhada, se sim verificar se existe mais de um usuário editando
                //os mesmos dados, se sim, limpar as listas de usuários para que o grupo selecione o usuário responsável pela edição dos dados.
                if (rblTipoPermissaoPor.SelectedValue == "C") 
                {
                    //Modulo Geral
                    if (moduloAux.Nome == "Geral") 
                    {
                        ConfiguracaoPermissaoModulo configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlCliente.SelectedValue.ToInt32(), moduloAux.Id);

                        if (configuracaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 1) 
                        {
                            configuracaoModuloGeral.UsuariosEdicaoModuloGeral = null;
                            configuracaoModuloGeral = configuracaoModuloGeral.Salvar();
                        }
                    }


                    //Módulo DNPM
                    if (moduloAux.Nome == "DNPM") 
                    {
                        ConfiguracaoPermissaoModulo configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlCliente.SelectedValue.ToInt32(), moduloAux.Id);

                        if (configuracaoModuloDNPM != null) 
                        {
                            if (configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.GERAL) 
                            {
                                if (configuracaoModuloDNPM != null && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 1)
                                {
                                    configuracaoModuloDNPM.UsuariosEdicaoModuloGeral = null;
                                    configuracaoModuloDNPM = configuracaoModuloDNPM.Salvar();
                                }
                            }

                            if (configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA) 
                            {
                                GrupoEconomico grupoDNPM = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                if (grupoDNPM.Empresas != null && grupoDNPM.Empresas.Count > 0) 
                                {
                                    foreach (Empresa empresa in grupoDNPM.Empresas)
                                    {
                                        EmpresaModuloPermissao empresaPermissaoDNPM = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloAux.Id);

                                        if (empresaPermissaoDNPM != null && empresaPermissaoDNPM.UsuariosEdicao != null && empresaPermissaoDNPM.UsuariosEdicao.Count > 0 && empresaPermissaoDNPM.UsuariosEdicao.Count > 1)
                                        {
                                            empresaPermissaoDNPM.UsuariosEdicao = null;
                                            empresaPermissaoDNPM = empresaPermissaoDNPM.Salvar();
                                        }
                                    }
                                }
                            }

                            if (configuracaoModuloDNPM.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO) 
                            {
                                GrupoEconomico grupoDNPM = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                IList<ProcessoDNPM> processosDoGrupo = ProcessoDNPM.ConsultarProcessosDoCliente(grupoDNPM);

                                if (processosDoGrupo != null && processosDoGrupo.Count > 0)
                                {
                                    foreach (ProcessoDNPM processo in processosDoGrupo)
                                    {
                                        if (processo != null && processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0 && processo.UsuariosEdicao.Count > 1)
                                        {
                                            processo.UsuariosEdicao = null;
                                            processo.Salvar();
                                        }
                                    }
                                }
                            }
                        }
                    }


                    //Módulo Meio Ambiente
                    if (moduloAux.Nome == "Meio Ambiente") 
                    {
                        ConfiguracaoPermissaoModulo configuracaoModuloMeioAmbiente = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlCliente.SelectedValue.ToInt32(), moduloAux.Id);

                        if (configuracaoModuloMeioAmbiente != null)
                        {
                            if (configuracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.GERAL)
                            {
                                if (configuracaoModuloMeioAmbiente != null && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral != null && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral.Count > 1)
                                {
                                    configuracaoModuloMeioAmbiente.UsuariosEdicaoModuloGeral = null;
                                    configuracaoModuloMeioAmbiente = configuracaoModuloMeioAmbiente.Salvar();
                                }
                            }

                            if (configuracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                            {
                                GrupoEconomico grupoMA = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                if (grupoMA.Empresas != null && grupoMA.Empresas.Count > 0)
                                {
                                    foreach (Empresa empresa in grupoMA.Empresas)
                                    {
                                        EmpresaModuloPermissao empresaPermissaoMA = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloAux.Id);

                                        if (empresaPermissaoMA != null && empresaPermissaoMA.UsuariosEdicao != null && empresaPermissaoMA.UsuariosEdicao.Count > 0 && empresaPermissaoMA.UsuariosEdicao.Count > 1)
                                        {
                                            empresaPermissaoMA.UsuariosEdicao = null;
                                            empresaPermissaoMA = empresaPermissaoMA.Salvar();
                                        }
                                    }
                                }
                            }

                            if (configuracaoModuloMeioAmbiente.Tipo == ConfiguracaoPermissaoModulo.PORPROCESSO)
                            {
                                GrupoEconomico grupoMA = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                //Processos
                                IList<Processo> processosDoGrupo = Processo.ConsultarProcessosDoCliente(grupoMA);
                                if (processosDoGrupo != null && processosDoGrupo.Count > 0)
                                {
                                    foreach (Processo processo in processosDoGrupo)
                                    {
                                        if (processo != null && processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0 && processo.UsuariosEdicao.Count > 1)
                                        {
                                            processo.UsuariosEdicao = null;
                                            processo.Salvar();
                                        }
                                    }
                                }


                                //Cadastros Técnicos
                                IList<CadastroTecnicoFederal> cadastrosDoGrupo = CadastroTecnicoFederal.ConsultarPorGrupoEconomico(ddlCliente.SelectedValue.ToInt32());
                                if (cadastrosDoGrupo != null && cadastrosDoGrupo.Count > 0)
                                {
                                    foreach (CadastroTecnicoFederal cadastro in cadastrosDoGrupo)
                                    {
                                        if (cadastro != null && cadastro.UsuariosEdicao != null && cadastro.UsuariosEdicao.Count > 0 && cadastro.UsuariosEdicao.Count > 1)
                                        {
                                            cadastro.UsuariosEdicao = null;
                                            cadastro.Salvar();
                                        }
                                    }
                                }


                                //Outros Empresa
                                IList<OutrosEmpresa> outros = OutrosEmpresa.ConsultarPorGrupoEconomico(ddlCliente.SelectedValue.ToInt32());
                                if (outros != null && outros.Count > 0)
                                {
                                    foreach (OutrosEmpresa outro in outros)
                                    {
                                        if (outro != null && outro.UsuariosEdicao != null && outro.UsuariosEdicao.Count > 0 && outro.UsuariosEdicao.Count > 1)
                                        {
                                            outro.UsuariosEdicao = null;
                                            outro.Salvar();
                                        }
                                    }
                                }
                            }
                        }
                    }


                    //Módulo Contratos
                    if (moduloAux.Nome == "Contratos")
                    {
                        ConfiguracaoPermissaoModulo configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlCliente.SelectedValue.ToInt32(), moduloAux.Id);

                        if (configuracaoModuloContratos != null)
                        {
                            if (configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
                            {
                                if (configuracaoModuloContratos != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 1)
                                {
                                    configuracaoModuloContratos.UsuariosEdicaoModuloGeral = null;
                                    configuracaoModuloContratos = configuracaoModuloContratos.Salvar();
                                }
                            }

                            if (configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                            {
                                GrupoEconomico grupoContratos = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                if (grupoContratos.Empresas != null && grupoContratos.Empresas.Count > 0)
                                {
                                    foreach (Empresa empresa in grupoContratos.Empresas)
                                    {
                                        EmpresaModuloPermissao empresaPermissaoContratos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloAux.Id);

                                        if (empresaPermissaoContratos != null && empresaPermissaoContratos.UsuariosEdicao != null && empresaPermissaoContratos.UsuariosEdicao.Count > 0 && empresaPermissaoContratos.UsuariosEdicao.Count > 1)
                                        {
                                            empresaPermissaoContratos.UsuariosEdicao = null;
                                            empresaPermissaoContratos = empresaPermissaoContratos.Salvar();
                                        }
                                    }
                                }
                            }

                            if (configuracaoModuloContratos.Tipo == ConfiguracaoPermissaoModulo.PORSETOR)
                            {
                                GrupoEconomico grupoContratos = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                IList<Setor> setores = Setor.ConsultarSetoresDoCliente(grupoContratos);

                                if (setores != null && setores.Count > 0)
                                {
                                    foreach (Setor setor in setores)
                                    {
                                        if (setor != null && setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0 && setor.UsuariosEdicao.Count > 1)
                                        {
                                            setor.UsuariosEdicao = null;
                                            setor.Salvar();
                                        }
                                    }
                                }
                            }
                        }
                    }


                    //Módulo Diversos
                    if (moduloAux.Nome == "Diversos")
                    {
                        ConfiguracaoPermissaoModulo configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(ddlCliente.SelectedValue.ToInt32(), moduloAux.Id);

                        if (configuracaoModuloDiversos != null)
                        {
                            if (configuracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.GERAL)
                            {
                                if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0 && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 1)
                                {
                                    configuracaoModuloDiversos.UsuariosEdicaoModuloGeral = null;
                                    configuracaoModuloDiversos = configuracaoModuloDiversos.Salvar();
                                }
                            }

                            if (configuracaoModuloDiversos.Tipo == ConfiguracaoPermissaoModulo.POREMPRESA)
                            {
                                GrupoEconomico grupoDiversos = GrupoEconomico.ConsultarPorId(ddlCliente.SelectedValue.ToInt32());

                                if (grupoDiversos.Empresas != null && grupoDiversos.Empresas.Count > 0)
                                {
                                    foreach (Empresa empresa in grupoDiversos.Empresas)
                                    {
                                        EmpresaModuloPermissao empresaPermissaoDiversos = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloAux.Id);

                                        if (empresaPermissaoDiversos != null && empresaPermissaoDiversos.UsuariosEdicao != null && empresaPermissaoDiversos.UsuariosEdicao.Count > 0 && empresaPermissaoDiversos.UsuariosEdicao.Count > 1)
                                        {
                                            empresaPermissaoDiversos.UsuariosEdicao = null;
                                            empresaPermissaoDiversos = empresaPermissaoDiversos.Salvar();
                                        }
                                    }
                                }
                            }                           
                        }
                    }
                }
            }
                
        }


        return modulos;
    }

    private IList<Modelo.ModuloPermissao> GetModulosSelecionadosAmbientalis()
    {
        IList<Modelo.ModuloPermissao> modulos = new List<Modelo.ModuloPermissao>();  

        return modulos;
    }

    private void MarcarPermissoesDoUsuarioAmbientalis(Usuario usuario)
    {
        if (usuario != null && usuario.Id > 0)
        {
            
        }
    }

    private void MarcarPermissoesGridSetores(Usuario usuario)
    {
        
    }

    #endregion        
    
}