using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using System.Web.UI.WebControls;
using System.IO;

namespace Utilitarios
{
    public static class Permissoes
    {
        public static bool UsuarioPossuiAcessoUrl(Usuario user, string url)
        {
            if (user != null)                                
                if (url.Contains("/Relatorios/") && Permissoes.UsuarioPossuiAlgumRelatorio(user))
                    return true;                
                else
                { 
                    if (url.Contains("Site/Index.aspx"))
                        return true;

                    if (url.Contains("Permissoes/Permissoes.aspx") && user.UsuarioAdministrador)
                        return true;

                    ModuloPermissao moduloDaTela = null;

                    if (url.Contains("DNPM"))
                    {
                        moduloDaTela = ModuloPermissao.ConsultarPorNome("DNPM");

                        if (Permissoes.UsuarioPossuiAcessoModuloDNPM(user, moduloDaTela))
                            return true;
                    }


                    if (url.Contains("Processo") || url.Contains("Licenca") || url.Contains("OrgaoAmbiental"))
                    {
                        moduloDaTela = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

                        if (Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(user, moduloDaTela))
                            return true;
                    }


                    if (url.Contains("VencimentosContrato") || url.Contains("Clientes") || url.Contains("Fornecedores"))
                    {
                        moduloDaTela = ModuloPermissao.ConsultarPorNome("Contratos");

                        if (Permissoes.UsuarioPossuiAcessoModuloContratos(user, moduloDaTela))
                            return true;
                    }


                    if (url.Contains("VencimentosDiversos"))
                    {
                        moduloDaTela = ModuloPermissao.ConsultarPorNome("Diversos");

                        if (Permissoes.UsuarioPossuiAcessoModuloDiversos(user, moduloDaTela))
                            return true;
                    }


                    if (url.Contains("Empresa") || url.Contains("Usuario") || url.Contains("Consultora") || url.Contains("Notificacao") || url.Contains("GrupoEconomico"))
                    {
                        moduloDaTela = ModuloPermissao.ConsultarPorNome("Geral");

                        if (Permissoes.UsuarioPossuiAcessoModuloGeral(user, moduloDaTela))
                            return true;
                    }

                }

            return false;
        }

        public static bool UsuarioPossuiAcessoModuloDNPM(Usuario user, ModuloPermissao moduloDnpm)
        {
            ConfiguracaoPermissaoModulo configuracaoModuloDnpm = null;

            if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                configuracaoModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloDnpm.Id);
            else
                configuracaoModuloDnpm = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDnpm.Id);

            if (configuracaoModuloDnpm != null && configuracaoModuloDnpm.Id > 0)
            {
                switch (configuracaoModuloDnpm.Tipo)
                {
                    case 'G':

                        if ((configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral.Count == 0) || configuracaoModuloDnpm.UsuariosVisualizacaoModuloGeral.Contains(user))
                            return true;

                        break;

                    case 'E':

                        IList<Empresa> empresas = Empresa.ConsultarTodos();

                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {
                                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDnpm.Id);

                                if (empresaPermissao != null && empresaPermissao.Id > 0)
                                {
                                    if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || empresaPermissao.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }
                            }
                        }

                        break;

                    case 'P':

                        IList<ProcessoDNPM> processosDnpm = ProcessoDNPM.ConsultarTodos();

                        if (processosDnpm != null && processosDnpm.Count > 0)
                        {
                            foreach (ProcessoDNPM processoDnpm in processosDnpm)
                            {
                                if (processoDnpm != null && processoDnpm.Id > 0)
                                {
                                    if ((processoDnpm.UsuariosVisualizacao == null || processoDnpm.UsuariosVisualizacao.Count == 0) || processoDnpm.UsuariosVisualizacao.Contains(user))
                                        return true;

                                }
                            }
                        }
                        break;

                }
            }

            return false;
        }

        public static bool UsuarioPossuiAcessoModuloMeioAmbiente(Usuario user, ModuloPermissao moduloMeioAmbiente)
        {
            ConfiguracaoPermissaoModulo configuracaoModuloMA = null;

            if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloMeioAmbiente.Id);
            else
                configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloMeioAmbiente.Id);

            if (configuracaoModuloMA != null && configuracaoModuloMA.Id > 0)
            {
                switch (configuracaoModuloMA.Tipo)
                {
                    case 'G':

                        if ((configuracaoModuloMA.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloMA.UsuariosVisualizacaoModuloGeral.Count == 0) || configuracaoModuloMA.UsuariosVisualizacaoModuloGeral.Contains(user))
                            return true;

                        break;

                    case 'E':

                        IList<Empresa> empresas = Empresa.ConsultarTodos();

                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {
                                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloMeioAmbiente.Id);

                                if (empresaPermissao != null && empresaPermissao.Id > 0)
                                {
                                    if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || empresaPermissao.UsuariosVisualizacao.Contains(user))
                                        return true;

                                }
                            }
                        }

                        break;

                    case 'P':

                        IList<Processo> processos = Processo.ConsultarTodos();
                        if (processos != null && processos.Count > 0)
                        {
                            foreach (Processo processo in processos)
                            {
                                if (processo != null && processo.Id > 0)
                                {
                                    if ((processo.UsuariosVisualizacao == null || processo.UsuariosVisualizacao.Count == 0) || processo.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }
                            }
                        }

                        IList<CadastroTecnicoFederal> cadastros = CadastroTecnicoFederal.ConsultarTodos();
                        if (cadastros != null && cadastros.Count > 0)
                        {
                            foreach (CadastroTecnicoFederal cadastro in cadastros)
                            {
                                if (cadastro != null && cadastro.Id > 0)
                                {
                                    if ((cadastro.UsuariosVisualizacao == null || cadastro.UsuariosVisualizacao.Count == 0) || cadastro.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }

                            }
                        }

                        IList<OutrosEmpresa> outrosEmps = OutrosEmpresa.ConsultarTodos();
                        if (outrosEmps != null && outrosEmps.Count > 0)
                        {
                            foreach (OutrosEmpresa outroEmp in outrosEmps)
                            {
                                if (outroEmp != null && outroEmp.Id > 0)
                                {
                                    if ((outroEmp.UsuariosVisualizacao == null || outroEmp.UsuariosVisualizacao.Count == 0) || outroEmp.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }

                            }
                        }

                        break;
                }
            }

            return false;
        }

        public static bool UsuarioPossuiAcessoModuloContratos(Usuario user, ModuloPermissao moduloContratos)
        {
            ConfiguracaoPermissaoModulo configuracaoModuloContratos = null;

            if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloContratos.Id);
            else
                configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloContratos.Id);

            if (configuracaoModuloContratos != null && configuracaoModuloContratos.Id > 0)
            {
                switch (configuracaoModuloContratos.Tipo)
                {
                    case 'G':

                        if ((configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Count == 0) || configuracaoModuloContratos.UsuariosVisualizacaoModuloGeral.Contains(user))
                            return true;

                        break;

                    case 'E':

                        IList<Empresa> empresas = Empresa.ConsultarTodos();

                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {
                                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloContratos.Id);

                                if (empresaPermissao != null && empresaPermissao.Id > 0)
                                {
                                    if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || empresaPermissao.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }
                            }
                        }

                        break;

                    case 'S':

                        IList<Setor> setores = Setor.ConsultarTodos();

                        if (setores != null && setores.Count > 0)
                        {
                            foreach (Setor setor in setores)
                            {
                                if (setor != null && setor.Id > 0)
                                {
                                    if ((setor.UsuariosVisualizacao == null || setor.UsuariosVisualizacao.Count == 0) || setor.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }
                            }
                        }
                        break;

                }
            }

            return false;
        }

        public static bool UsuarioPossuiAcessoModuloDiversos(Usuario user, ModuloPermissao moduloDiversos)
        {
            ConfiguracaoPermissaoModulo configuracaoModuloDiversos = null;

            if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloDiversos.Id);
            else
                configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

            if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.Id > 0)
            {
                switch (configuracaoModuloDiversos.Tipo)
                {
                    case 'G':

                        if ((configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Count == 0) || configuracaoModuloDiversos.UsuariosVisualizacaoModuloGeral.Contains(user))
                            return true;

                        break;

                    case 'E':

                        IList<Empresa> empresas = Empresa.ConsultarTodos();

                        if (empresas != null && empresas.Count > 0)
                        {
                            foreach (Empresa empresa in empresas)
                            {
                                EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDiversos.Id);

                                if (empresaPermissao != null && empresaPermissao.Id > 0)
                                {
                                    if ((empresaPermissao.UsuariosVisualizacao == null || empresaPermissao.UsuariosVisualizacao.Count == 0) || empresaPermissao.UsuariosVisualizacao.Contains(user))
                                        return true;
                                }
                            }
                        }

                        break;

                }
            }

            return false;
        }

        public static bool UsuarioPossuiAcessoModuloGeral(Usuario user, ModuloPermissao moduloGeral)
        {
            ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

            if (user.GrupoEconomico != null && user.GrupoEconomico.Id > 0)
                configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(user.GrupoEconomico.Id, moduloGeral.Id);
            else
                configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloGeral.Id);

            if (configuracaoModuloGeral != null && configuracaoModuloGeral.Id > 0)
            {
                switch (configuracaoModuloGeral.Tipo)
                {
                    case 'G':

                        if ((configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral == null || configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Count == 0) || configuracaoModuloGeral.UsuariosVisualizacaoModuloGeral.Contains(user))
                            return true;

                        break;
                }
            }

            return false;
        }

        public static void ValidarControle(ImageButton imageButton, bool usuarioEditor)
        {
            imageButton.Enabled = usuarioEditor;
            imageButton.ImageUrl = imageButton.Enabled ? imageButton.ImageUrl :
                Path.GetDirectoryName(imageButton.ImageUrl) + "/" + Path.GetFileNameWithoutExtension(imageButton.ImageUrl) + "_d" + Path.GetExtension(imageButton.ImageUrl);
        }

        public static void ValidarControle(Button button, bool usuarioEditor)
        {
            button.Enabled = usuarioEditor;
        }

        public static void ValidarControle(LinkButton linkButton, bool usuarioEditor)
        {
            linkButton.Enabled = usuarioEditor;
        }

        public static bool UsuarioPossuiAlgumRelatorio(Usuario usuario)
        {
            if (usuario != null)
                if (usuario.Administrador != null)
                    return true;
                else
                {
                    IList<ModuloPermissao> modulosUsuario = Permissoes.ObterModulosQueOUsuarioTemAcesso(usuario.ConsultarPorId());                    

                    foreach (Modelo.ModuloPermissao modulo in modulosUsuario)
                    {
                        foreach (Modelo.Menu menu in modulo.Menus)
                        {
                            if (menu.Relatorio)
                                return true;
                        }
                    }
                }


            return false;
        }

        public static IList<ModuloPermissao> ObterModulosQueOUsuarioTemAcesso(Usuario usuario)
        {
            IList<ModuloPermissao> retorno = new List<ModuloPermissao>();

            //Adicionando modulo geral
            ModuloPermissao moduloGeral = ModuloPermissao.ConsultarPorNome("Geral");
            if (Permissoes.UsuarioPossuiAcessoModuloGeral(usuario, moduloGeral))
                retorno.Add(moduloGeral);

            //Adicionando modulo DNPM
            ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");
            if (Permissoes.UsuarioPossuiAcessoModuloDNPM(usuario, moduloDNPM))
                retorno.Add(moduloDNPM);

            //Adicionando modulo meio ambiente
            ModuloPermissao moduloMeioAmbiente = ModuloPermissao.ConsultarPorNome("Meio Ambiente");
            if (Permissoes.UsuarioPossuiAcessoModuloMeioAmbiente(usuario, moduloMeioAmbiente))
                retorno.Add(moduloMeioAmbiente);

            //Adicionando modulo contratos
            ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");
            if (Permissoes.UsuarioPossuiAcessoModuloContratos(usuario, moduloContratos))
                retorno.Add(moduloContratos);

            //Adicionando modulo diversos
            ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");
            if (Permissoes.UsuarioPossuiAcessoModuloDiversos(usuario, moduloDiversos))
                retorno.Add(moduloDiversos);

            return retorno;
        }

        public static bool TestarAcessoMenu(Usuario user, string url)
        {
            IList<ModuloPermissao> modulosUsuario = ModuloPermissao.RecarregarModulos(user.ConsultarPorId().ModulosPermissao);

            //if (user == null && user.GrupoEconomico == null || modulosUsuario == null || modulosUsuario.Count == 0)
            //    return false;

            if (url.Contains("Site/Index.aspx"))
                return true;

            string urlAux = "";
            string pesquisaCadastro = "";

            if (url.Contains("DNPM/ConsultaDOU.aspx") || url.Contains("DNPM/Eventos.aspx"))
            {
                urlAux = "DNPM/DNPM.aspx";
                pesquisaCadastro = "Pesquisa";
            }
            else if (url.Contains("OrgaoAmbiental/PesquisarOrgaosAmbientais.aspx") || url.Contains("OrgaoAmbiental/CadastroOrgaoAmbiental.aspx") || url.Contains("Licenca/ManterTipoLicenca.aspx") || url.Contains("Licenca/PesquisarTipoLicencas.aspx") || url.Contains("Processo/ConsultaDOUMeioAmbiente.aspx"))
            {
                urlAux = "Processo/Processos.aspx";
                pesquisaCadastro = "Pesquisa";
            }
            else if (url.Contains("Vencimentos/CadastroVencimentosContrato.aspx") || url.Contains("Vencimentos/PesquisaVencimentosContrato.aspx") || url.Contains("Clientes/CadastroClientes.aspx") || url.Contains("Clientes/PesquisarClientes.aspx") || url.Contains("Fornecedores/CadastrarFornecedores.aspx") || url.Contains("Fornecedores/PesquisarFornecedores.aspx"))
            {
                urlAux = "Vencimentos/CadastroVencimentosContrato.aspx";
                pesquisaCadastro = "Pesquisa";
            }
            else if (url.Contains("Usuario/TrocarSenha.aspx") || url.Contains("Usuario/ManterUsuario.aspx") || url.Contains("Usuario/PesquisarUsuarios.aspx") || url.Contains("Auditoria/Auditoria.aspx"))
            {
                urlAux = "Usuario/ManterUsuario.aspx";
                pesquisaCadastro = "Pesquisa";
            }
            else if (url.Contains("Notificacao/Notificacoes.aspx") || url.Contains("Notificacao/Emails.aspx"))
            {
                urlAux = "Notificacao/Notificacoes.aspx";
                pesquisaCadastro = "Cadastro";
            }
            else if (url.Contains("http://sustentar.inf.br/wiki-sistema-sustentar/index.php?title=P%C3%A1gina_principal"))
            {
                urlAux = "http://sustentar.inf.br/wiki-sistema-sustentar/index.php?title=P%C3%A1gina_principal";
                pesquisaCadastro = "Pesquisa";
            }

            foreach (Modelo.ModuloPermissao modulo in modulosUsuario)
            {
                foreach (Modelo.Menu menu in modulo.Menus)
                {
                    if (pesquisaCadastro == "Pesquisa")
                    {
                        if (menu.UrlPesquisa != null && menu.UrlPesquisa != "" && urlAux.IsNotNullOrEmpty() && menu.UrlPesquisa.Contains(urlAux))
                            return true;
                    }
                    else if (pesquisaCadastro == "Cadastro")
                    {
                        if (menu.UrlCadastro != null && menu.UrlCadastro != "" && menu.UrlCadastro.Contains(urlAux))
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
