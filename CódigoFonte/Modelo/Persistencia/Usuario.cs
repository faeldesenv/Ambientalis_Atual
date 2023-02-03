using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using System.Collections;

namespace Modelo
{
    public partial class Usuario : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Usuario ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Usuario classe = new Usuario();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Usuario>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Usuario ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Usuario>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Usuario Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Usuario>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Usuario SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Usuario>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Usuario> SalvarTodos(IList<Usuario> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Usuario>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Usuario> SalvarTodos(params Usuario[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Usuario>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Usuario>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Usuario>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Usuario> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Usuario obj = Activator.CreateInstance<Usuario>();
            return fabrica.GetDAOBase().ConsultarTodos<Usuario>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Usuario> ConsultarOrdemAcendente(int qtd)
        {
            Usuario ee = new Usuario();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Usuario> ConsultarOrdemDescendente(int qtd)
        {
            Usuario ee = new Usuario();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Usuario
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Usuario> Filtrar(int qtd)
        {
            Usuario estado = new Usuario();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Usuario Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Usuario</returns>
        public virtual Usuario UltimoInserido()
        {
            Usuario estado = new Usuario();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Usuario>(estado);
        }

        /// <summary>
        /// Consulta todos os Usuarios armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Usuarios armazenados ordenados pelo Nome</returns>
        public static IList<Usuario> ConsultarTodosOrdemAlfabetica()
        {
            Usuario aux = new Usuario();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(aux);
        }

        /// <summary>
        /// Verifica se existe o usuário passado como parâmetro
        /// </summary>
        /// <param name="user">O usuário a ser validado</param>
        /// <returns>true caso o usuário seja validado e fase caso não for</returns>
        public static bool ValidaUsuario(ref Usuario user)
        {
            Usuario aux = new Usuario();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Login", user.Login));
            aux.AdicionarFiltro(Filtros.Eq("Senha", user.Senha));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Usuario> users = fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(aux);
            if (users != null && users.Count > 0)
            {
                user = users[0];
                string fetch;
                //Fetch Menus
                if (user.GrupoEconomico != null)
                    foreach (Menu menu in user.GrupoEconomico.Menus)
                        fetch = menu.Nome;
                return true;
            }
            return false;
        }

        public static bool ExisteUsuarioComEsteLogin(Usuario user)
        {
            if (user != null)
            {
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                IList resultado = fabrica.GetDAOBase().ConsultaSQL("select login from usuarios where login='" + user.Login + "' and id<>" + user.Id);
                return resultado != null && resultado.Count > 0;
            }
            return false;
            //Usuario aux = new Usuario();
            //aux.AdicionarFiltro(Filtros.NaoIgual("Id", user.Id));
            //aux.AdicionarFiltro(Filtros.Eq("Login", user.Login));

            //IList<Usuario> users = fabrica.GetDAOBase().ExecutarHQL()ConsultarComFiltro<Usuario>(aux);
            //return users != null && users.Count > 0;
        }

        public static IList<Usuario> Filtrar(string nome, string login, string email)
        {
            Usuario aux = new Usuario();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));

            if (nome != null && nome != "")
                aux.AdicionarFiltro(Filtros.Like("Nome", nome));

            if (login != null && login != "")
                aux.AdicionarFiltro(Filtros.Like("Login", login));

            if (email != null && email != "")
                aux.AdicionarFiltro(Filtros.Like("Email", email));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(aux);
        }

        public virtual bool PossuiPermissaoDeEditarModuloDNPM
        {
            get
            {
                ModuloPermissao moduloDNPM = ModuloPermissao.ConsultarPorNome("DNPM");

                ConfiguracaoPermissaoModulo configuracaoModuloDNPM = null;

                if (this.GrupoEconomico != null && this.GrupoEconomico.Id > 0)
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.GrupoEconomico.Id, moduloDNPM.Id);
                else
                    configuracaoModuloDNPM = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDNPM.Id);

                if (configuracaoModuloDNPM != null && configuracaoModuloDNPM.Id > 0)
                {
                    Usuario usuario = null;

                    switch (configuracaoModuloDNPM.Tipo)
                    {
                        case 'G':

                            if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral == null)
                                return false;

                            if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral != null && configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Count > 0)
                            {
                                if (configuracaoModuloDNPM.UsuariosEdicaoModuloGeral.Contains(this))
                                    return true;
                            }
                            else
                                return false;

                            break;

                        case 'E':

                            IList<Empresa> empresas = Empresa.ConsultarTodos();

                            if (empresas != null && empresas.Count > 0)
                            {
                                foreach (Empresa empresa in empresas)
                                {
                                    EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDNPM.Id);

                                    if (empresaPermissao != null && empresaPermissao.Id > 0 && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0)
                                    {
                                        if (empresaPermissao.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }
                            }

                            break;

                        case 'P':

                            IList<ProcessoDNPM> processos = ProcessoDNPM.ConsultarTodos();
                            if (processos != null && processos.Count > 0)
                            {
                                foreach (ProcessoDNPM processo in processos)
                                {
                                    if (processo != null && processo.Id > 0 && processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
                                    {
                                        if (processo.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }
                            }
                            break;

                    }
                }

                return false;
            }
        }

        public virtual bool PossuiPermissaoDeEditarModuloMeioAmbiente
        {
            get
            {
                ModuloPermissao moduloMeioAmbiente = ModuloPermissao.ConsultarPorNome("Meio Ambiente");

                ConfiguracaoPermissaoModulo configuracaoModuloMA = null;

                if (this.GrupoEconomico != null && this.GrupoEconomico.Id > 0)
                    configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.GrupoEconomico.Id, moduloMeioAmbiente.Id);
                else
                    configuracaoModuloMA = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloMeioAmbiente.Id);

                if (configuracaoModuloMA != null && configuracaoModuloMA.Id > 0)
                {
                    Usuario usuario = null;

                    switch (configuracaoModuloMA.Tipo)
                    {
                        case 'G':

                            if (configuracaoModuloMA.UsuariosEdicaoModuloGeral == null)
                                return false;

                            if (configuracaoModuloMA.UsuariosEdicaoModuloGeral != null && configuracaoModuloMA.UsuariosEdicaoModuloGeral.Count > 0)
                            {
                                if (configuracaoModuloMA.UsuariosEdicaoModuloGeral.Contains(this))
                                    return true;
                            }
                            else
                                return false; 

                            break;

                        case 'E':

                            IList<Empresa> empresas = Empresa.ConsultarTodos();

                            if (empresas != null && empresas.Count > 0)
                            {
                                foreach (Empresa empresa in empresas)
                                {
                                    EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloMeioAmbiente.Id);

                                    if (empresaPermissao != null && empresaPermissao.Id > 0 && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0)
                                    {
                                        if (empresaPermissao.UsuariosEdicao.Contains(this))
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
                                    if (processo != null && processo.Id > 0 && processo.UsuariosEdicao != null && processo.UsuariosEdicao.Count > 0)
                                    {
                                        if (processo.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }
                            }

                            IList<CadastroTecnicoFederal> cadastros = CadastroTecnicoFederal.ConsultarTodos();
                            if (cadastros != null && cadastros.Count > 0)
                            {
                                foreach (CadastroTecnicoFederal cadastro in cadastros)
                                {
                                    if (cadastro != null && cadastro.Id > 0 && cadastro.UsuariosEdicao != null && cadastro.UsuariosEdicao.Count > 0)
                                    {
                                        if (cadastro.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }
                            }

                            IList<OutrosEmpresa> outros = OutrosEmpresa.ConsultarTodos();
                            if (outros != null && outros.Count > 0)
                            {
                                foreach (OutrosEmpresa outro in outros)
                                {
                                    if (outro != null && outro.Id > 0 && outro.UsuariosEdicao != null && outro.UsuariosEdicao.Count > 0)
                                    {
                                        if (outro.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }

                            }
                            break;
                    }
                }

                return false;
            }
        }

        public virtual bool PossuiPermissaoDeEditarModuloContratos
        {
            get
            {
                ModuloPermissao moduloContratos = ModuloPermissao.ConsultarPorNome("Contratos");

                ConfiguracaoPermissaoModulo configuracaoModuloContratos = null;

                if (this.GrupoEconomico != null && this.GrupoEconomico.Id > 0)
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.GrupoEconomico.Id, moduloContratos.Id);
                else
                    configuracaoModuloContratos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloContratos.Id);

                if (configuracaoModuloContratos != null && configuracaoModuloContratos.Id > 0)
                {
                    Usuario usuario = null;

                    switch (configuracaoModuloContratos.Tipo)
                    {
                        case 'G':

                            if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral == null)
                                return false;

                            if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral != null && configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Count > 0)
                            {
                                if (configuracaoModuloContratos.UsuariosEdicaoModuloGeral.Contains(this))
                                    return true;
                            }
                            else
                                return false;

                            break;

                        case 'E':

                            IList<Empresa> empresas = Empresa.ConsultarTodos();

                            if (empresas != null && empresas.Count > 0)
                            {
                                foreach (Empresa empresa in empresas)
                                {
                                    EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloContratos.Id);

                                    if (empresaPermissao != null && empresaPermissao.Id > 0 && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0)
                                    {
                                        if (empresaPermissao.UsuariosEdicao.Contains(this))
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
                                    if (setor != null && setor.Id > 0 && setor.UsuariosEdicao != null && setor.UsuariosEdicao.Count > 0)
                                    {
                                        if (setor.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }
                            }

                            break;
                    }
                }

                return false;

            }
        }

        public virtual bool PossuiPermissaoDeEditarModuloDiversos
        {
            get
            {
                ModuloPermissao moduloDiversos = ModuloPermissao.ConsultarPorNome("Diversos");

                ConfiguracaoPermissaoModulo configuracaoModuloDiversos = null;

                if (this.GrupoEconomico != null && this.GrupoEconomico.Id > 0)
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.GrupoEconomico.Id, moduloDiversos.Id);
                else
                    configuracaoModuloDiversos = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloDiversos.Id);

                if (configuracaoModuloDiversos != null && configuracaoModuloDiversos.Id > 0)
                {
                    Usuario usuario = null;

                    switch (configuracaoModuloDiversos.Tipo)
                    {
                        case 'G':

                            if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral == null)
                                return false;

                            if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral != null && configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Count > 0)
                            {
                                if (configuracaoModuloDiversos.UsuariosEdicaoModuloGeral.Contains(this))
                                    return true;
                            }
                            else
                                return false;

                            break;

                        case 'E':

                            IList<Empresa> empresas = Empresa.ConsultarTodos();

                            if (empresas != null && empresas.Count > 0)
                            {
                                foreach (Empresa empresa in empresas)
                                {
                                    EmpresaModuloPermissao empresaPermissao = EmpresaModuloPermissao.ConsultarPorEmpresaEModulo(empresa.Id, moduloDiversos.Id);

                                    if (empresaPermissao != null && empresaPermissao.Id > 0 && empresaPermissao.UsuariosEdicao != null && empresaPermissao.UsuariosEdicao.Count > 0)
                                    {
                                        if (empresaPermissao.UsuariosEdicao.Contains(this))
                                            return true;
                                    }
                                }
                            }

                            break;
                    }
                }

                return false;
            }
        }

        public virtual bool PossuiPermissaoDeEditarModuloGeral
        {
            get
            {
                ModuloPermissao moduloGeral = ModuloPermissao.ConsultarPorNome("Geral");

                ConfiguracaoPermissaoModulo configuracaoModuloGeral = null;

                if (this.GrupoEconomico != null && this.GrupoEconomico.Id > 0)
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorEmpEModulo(this.GrupoEconomico.Id, moduloGeral.Id);
                else
                    configuracaoModuloGeral = ConfiguracaoPermissaoModulo.ConsultarPorModulo(moduloGeral.Id);

                if (configuracaoModuloGeral != null && configuracaoModuloGeral.Id > 0)
                {
                    Usuario usuario = null;

                    switch (configuracaoModuloGeral.Tipo)
                    {
                        case 'G':

                            if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral == null)
                                return false;

                            if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral != null && configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Count > 0)
                            {
                                if (configuracaoModuloGeral.UsuariosEdicaoModuloGeral.Contains(this))
                                    return true;
                            }
                            else
                                return false;

                            break;
                    }
                }

                return false;
            }
        }

        public static IList<Usuario> ConsultarRelatorioPermissoes(int idGrupoEconomico, int idUsuario)
        {
            Usuario aux = new Usuario();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));

            if (idGrupoEconomico > 0) 
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("GrupoEconomico", "group"));
                aux.AdicionarFiltro(Filtros.Eq("group.Id", idGrupoEconomico));
            }

            if (idUsuario > 0)
            {
                aux.AdicionarFiltro(Filtros.Eq("Id", idUsuario));
            } 

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Usuario>(aux);
        }
    }
}
