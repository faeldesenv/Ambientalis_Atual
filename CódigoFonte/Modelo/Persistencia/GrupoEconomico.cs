using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class GrupoEconomico : Pessoa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static GrupoEconomico ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GrupoEconomico classe = new GrupoEconomico();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<GrupoEconomico>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual GrupoEconomico ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<GrupoEconomico>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual GrupoEconomico Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<GrupoEconomico>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual GrupoEconomico SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<GrupoEconomico>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<GrupoEconomico> SalvarTodos(IList<GrupoEconomico> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<GrupoEconomico>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<GrupoEconomico> SalvarTodos(params GrupoEconomico[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<GrupoEconomico>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<GrupoEconomico>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<GrupoEconomico>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<GrupoEconomico> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            GrupoEconomico obj = Activator.CreateInstance<GrupoEconomico>();
            return fabrica.GetDAOBase().ConsultarTodos<GrupoEconomico>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<GrupoEconomico> ConsultarOrdemAcendente(int qtd)
        {
            GrupoEconomico ee = new GrupoEconomico();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<GrupoEconomico> ConsultarOrdemDescendente(int qtd)
        {
            GrupoEconomico ee = new GrupoEconomico();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cliente
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<GrupoEconomico> Filtrar(int qtd)
        {
            GrupoEconomico estado = new GrupoEconomico();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cliente Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cliente</returns>
        public virtual GrupoEconomico UltimoInserido()
        {
            GrupoEconomico estado = new GrupoEconomico();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<GrupoEconomico>(estado);
        }

        /// <summary>
        /// Consulta todos os Clientes armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Clientes armazenados ordenados pelo Nome</returns>
        public static IList<GrupoEconomico> ConsultarTodosOrdemAlfabetica()
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(aux);
        }

        public static IList<GrupoEconomico> Filtrar(string nome, string site, string responsavel)
        {
            GrupoEconomico cliente = new GrupoEconomico();
            if (!string.IsNullOrEmpty(nome))
                cliente.AdicionarFiltro(Filtros.Like("Nome", nome));
            if (!string.IsNullOrEmpty(responsavel))
                cliente.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));
            if (!string.IsNullOrEmpty(site))
                cliente.AdicionarFiltro(Filtros.Like("Site", site));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(cliente);
        }

        public static IList<GrupoEconomico> Filtrar(string nome, string responsavel, int status, string cpfCnpj, Cidade cidade, int pendencia, int cancelado, Estado estado)
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (!string.IsNullOrEmpty(nome))
                aux.AdicionarFiltro(Filtros.Like("Nome", nome));
            if (!string.IsNullOrEmpty(responsavel))
                aux.AdicionarFiltro(Filtros.Like("Responsavel", responsavel));

            if (pendencia > 0)
                aux.AdicionarFiltro(Filtros.Eq((pendencia == 1 ? "AtivoLogus" : pendencia == 2 ? "AtivoAmbientalis" : "TermoAceito"), false));

            if (status > 0)
                aux.AdicionarFiltro(Filtros.Eq("Ativo", status == 1));

            if (cancelado > 0)
                aux.AdicionarFiltro(Filtros.Eq("Cancelado", cancelado == 1));

            if (!string.IsNullOrEmpty(cpfCnpj))
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dp"));
                aux.AdicionarFiltro(Filtros.Ou(Filtros.Like("dp.Cnpj", cpfCnpj), Filtros.Like("dp.Cpf", cpfCnpj)));
            }

            if (cidade != null && cidade.Id > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Endereco", "end"));
                aux.AdicionarFiltro(Filtros.CriarAlias("end.Cidade", "cid"));
                aux.AdicionarFiltro(Filtros.Eq("cid.Id", cidade.Id));
            }

            if (estado != null && estado.Id > 0 && cidade == null)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Endereco", "ed"));
                aux.AdicionarFiltro(Filtros.CriarAlias("ed.Cidade", "cd"));
                aux.AdicionarFiltro(Filtros.CriarAlias("cd.Estado", "es"));
                aux.AdicionarFiltro(Filtros.Eq("es.Id", estado.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(aux);
        }

        public static bool ConsultarCnpjCpfJaCadastrado(GrupoEconomico g, string CnpjCpf, int tipo)   //tipo = 1 para pessoa fisica e tipo = 2 para pessoa juridica
        {
            GrupoEconomico grupo = new GrupoEconomico();

            grupo.AdicionarFiltro(Filtros.NaoIgual("Id", g != null ? g.Id : 0));
            if (tipo == 1)
            {
                grupo.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dpf"));
                grupo.AdicionarFiltro(Filtros.Eq("dpf.Cpf", CnpjCpf));
            }

            if (tipo == 2)
            {
                grupo.AdicionarFiltro(Filtros.CriarAlias("DadosPessoa", "dpj"));
                grupo.AdicionarFiltro(Filtros.Eq("dpj.Cnpj", CnpjCpf));
            }
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<GrupoEconomico> grupos = fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(grupo);
            return grupos != null && grupos.Count > 0;
        }

        public static IList<GrupoEconomico> FiltrarGruposEconomicos(int idAdministrador, DateTime dataDe, DateTime dataAteh, DateTime dataCancelamentoDe, DateTime dataCancelamentoAte, int possuiUsuario, int ativo, int cancelado)
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (idAdministrador > 0)
                aux.AdicionarFiltro(Filtros.Eq("Administrador.Id", idAdministrador));
            aux.AdicionarFiltro(Filtros.Between("DataCadastro", dataDe, dataAteh));
            if (dataCancelamentoDe != SqlDate.MinValue && dataCancelamentoAte != SqlDate.MaxValue)
                aux.AdicionarFiltro(Filtros.Between("DataCancelamento", dataCancelamentoDe, dataCancelamentoAte));
            if (ativo > 0)
                aux.AdicionarFiltro(Filtros.Eq("Ativo", ativo == 1));
            if (cancelado > 0)
                aux.AdicionarFiltro(Filtros.Eq("Cancelado", cancelado == 1));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<GrupoEconomico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(aux);
            bool possuiUsuarioAux = false;
            if (possuiUsuario > 0 && retorno != null && retorno.Count > 0)
                for (int index = retorno.Count - 1; index >= 0; index--)
                {
                    possuiUsuarioAux = retorno[index].PossuiUsuario;
                    if ((possuiUsuario == 1 && !possuiUsuarioAux) ||
                        possuiUsuario == 2 && possuiUsuarioAux)
                        retorno.RemoveAt(index);
                }
            return retorno;
        }

        /// <summary>
        /// Verifica se o corrente Grupo Economico Possui usuário associados
        /// </summary>
        public virtual bool PossuiUsuario
        {
            get
            {
                Usuario aux = new Usuario();
                aux.AdicionarFiltro(Filtros.Eq("GrupoEconomico.Id", this.Id));
                aux.AdicionarFiltro(Filtros.Count("Id"));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                return ((int)fabrica.GetDAOBase().ConsultarProjecao(aux)) > 0;
            }
        }

        public virtual int GetQuantidadeUsuariosDoGrupo(GrupoEconomico grupo, IList<Usuario> usuarios)
        {
            int contadorUsuarios = 0;
            if (usuarios != null && usuarios.Count > 0 && grupo != null && grupo.Id > 0)
            {
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario.GrupoEconomico != null && usuario.GrupoEconomico.Id == grupo.Id)
                        contadorUsuarios = contadorUsuarios + 1;
                }
            }
            return contadorUsuarios;
        }

        public virtual new bool Ativo
        {
            get
            {
                return this.AtivoLogus && this.AtivoAmbientalis;
            }
        }

        public static IList<GrupoEconomico> ConsultarGruposAtivos()
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.Eq("AtivoAmbientalis", true));
            aux.AdicionarFiltro(Filtros.Eq("AtivoLogus", true));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<GrupoEconomico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(aux);
            return retorno;
        }

        /// <summary>
        /// Verifica se a adição do usuário irá ultrapassar os limites de usuários do grupo econômico
        /// </summary>
        /// <param name="user">O usuário a ser adicionado</param>
        /// <returns>True caso ultrapasse e False caso não ultrapasse</returns>
        public static bool AtingiuLimiteUsuarios(Usuario user)
        {
            int numeroAtualUsuariosEdicao = user.GrupoEconomico.GetNumeroAtualUsuarios;

            //Se for edição de um usuário, tem que retirar ele da qtd de usuários atual
            //numeroAtualUsuariosEdicao -= user.Id > 0 && user.PermissaoEditar ? 1 : 0;

            //Adicionar o usuário na qtd para verificar se irá ultrapassar a qtd limite
            numeroAtualUsuariosEdicao += 1;

            //Se passou algum limite, return true
            return numeroAtualUsuariosEdicao > user.GrupoEconomico.LimiteUsuariosEdicao;
        }

        public virtual int GetNumeroAtualUsuarios
        {
            get
            {
                Usuario user = new Usuario();
                user.AdicionarFiltro(Filtros.Distinct());
                user.AdicionarFiltro(Filtros.Count("Id"));
                user.AdicionarFiltro(Filtros.Eq("GrupoEconomico.Id", this.Id));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                object qtdAux = fabrica.GetDAOBase().ConsultarProjecao(user);
                return Convert.ToInt32(qtdAux);
            }
        }

        public static IList<Pessoa> ConsultarTodosComoPessoas()
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pessoa>((Pessoa)aux);
        }

        public virtual bool VerificarSeExcedeuLimiteDeProcessosContratados()
        {
            if (this.LimiteProcessos <= 0)
                return false;

            int cont = 0;
            foreach (Empresa emp in this.Empresas)
            {
                cont += ((emp.Processos != null ? emp.Processos.Count : 0) + (emp.ProcessosDNPM != null ? emp.ProcessosDNPM.Count : 0));
            }

            if (cont >= this.LimiteProcessos)
                return true;

            return false;
        }

        public virtual int GetTotalProcessosDoGrupo
        {
            get
            {
                int contadorProcessos = 0;
                if (this.Empresas != null && this.Empresas.Count > 0)
                {
                    foreach (Empresa empresa in this.Empresas)
                    {
                        if (empresa.Processos != null && empresa.Processos.Count > 0)
                        {
                            contadorProcessos = contadorProcessos + empresa.Processos.Count;
                        }

                        if (empresa.ProcessosDNPM != null && empresa.ProcessosDNPM.Count > 0)
                        {
                            contadorProcessos = contadorProcessos + empresa.ProcessosDNPM.Count;
                        }
                    }
                }
                return contadorProcessos;
            }
        }

        public virtual int GetTotalProcessosAmbientaisDoGrupo
        {
            get
            {
                int contadorProcessos = 0;
                if (this.Empresas != null && this.Empresas.Count > 0)
                {
                    foreach (Empresa empresa in this.Empresas)
                    {
                        if (empresa.Processos != null && empresa.Processos.Count > 0)
                        {
                            contadorProcessos = contadorProcessos + empresa.Processos.Count;
                        }
                    }
                }
                return contadorProcessos;
            }
        }

        public virtual string GetAdministrador
        {
            get
            {
                return this.Administrador != null ? this.Administrador.Nome : "Não definido";
            }
        }

        public virtual string GetQuantidadeEmpresas
        {
            get
            {
                return this.Empresas != null && this.Empresas.Count > 0 ? this.Empresas.Count.ToString() : "0";
            }
        }

        public virtual string GetDataCancelamento
        {
            get
            {
                return this.DataCancelamento != SqlDate.MinValue ? this.DataCancelamento.ToString("dd/MM/yyyy") : "";
            }
        }

        public virtual int GetTotalProcessosMinerariosDoGrupo
        {
            get
            {
                int contadorProcessos = 0;
                if (this.Empresas != null && this.Empresas.Count > 0)
                {
                    foreach (Empresa empresa in this.Empresas)
                    {
                        if (empresa.ProcessosDNPM != null && empresa.ProcessosDNPM.Count > 0)
                        {
                            contadorProcessos = contadorProcessos + empresa.ProcessosDNPM.Count;
                        }
                    }
                }
                return contadorProcessos;
            }
        }

        public virtual int GetTotalContratosDiversosDoGrupo
        {
            get
            {
                int contadorContratos = 0;
                if (this.Empresas != null && this.Empresas.Count > 0)
                {
                    foreach (Empresa empresa in this.Empresas)
                    {
                        if (empresa.ContratosDiversos != null && empresa.ContratosDiversos.Count > 0)
                        {
                            contadorContratos = contadorContratos + empresa.ContratosDiversos.Count;
                        }
                    }
                }
                return contadorContratos;
            }
        }

        public virtual String GetNumeroCNPJeCPFComMascara
        {
            get
            {
                if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosJuridica))
                {
                    string numeroCnpj = ((DadosJuridica)this.DadosPessoa).Cnpj;
                    return numeroCnpj.Substring(0, 2) + "." + numeroCnpj.Substring(2, 3) + "." + numeroCnpj.Substring(5, 3) + "/" + numeroCnpj.Substring(8, 4) + "-" + numeroCnpj.Substring(12, 2);
                }
                else if (this.DadosPessoa != null && this.DadosPessoa.GetType() == typeof(DadosFisica))
                {
                    string numeroCpf = ((DadosFisica)this.DadosPessoa).Cpf;
                    return numeroCpf.Substring(0, 3) + "." + numeroCpf.Substring(3, 3) + "." + numeroCpf.Substring(6, 3) + "-" + numeroCpf.Substring(9, 2);
                }
                return "";

            }
        }

        public static GrupoEconomico ConsultarPorCpfCnpj(string CpfCnpj)
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Cpf", CpfCnpj), Filtros.Eq("Cnpj", CpfCnpj)));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<GrupoEconomico>(aux);
        }

        public static IList<GrupoEconomico> FiltrarGruposTeste()
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.Eq("GrupoTeste", true));            
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<GrupoEconomico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(aux);
            return retorno;
        }

        public static GrupoEconomico ConsultarGrupoTestePorCPFouCNPJ(string cnpjCpf)
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.Eq("GrupoTeste", true));   
            aux.AdicionarFiltro(Filtros.SubConsulta("DadosPessoa"));
            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Cpf", cnpjCpf), Filtros.Eq("Cnpj", cnpjCpf)));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<GrupoEconomico>(aux);
        }

        public virtual string GetAtivoLogus 
        {
            get 
            {
                return this.AtivoLogus ? "Sim" : "Não";
            }
        }

        public virtual string GetAtivoAmbientais
        {
            get
            {
                return this.AtivoAmbientalis ? "Sim" : "Não";
            }
        }

        public virtual string GetTermoAceito
        {
            get
            {
                return this.TermoAceito ? "Sim" : "Não";
            }
        }

        public static IList<GrupoEconomico> ConsultarGruposComPendenciasDeAtivacao()
        {
            GrupoEconomico aux = new GrupoEconomico();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());

            IList<Filtro> filtros = new List<Filtro>();
            
            filtros.Add(Filtros.Eq("AtivoAmbientalis", false));
            filtros.Add(Filtros.Eq("AtivoLogus", false));

            aux.AdicionarFiltro(Filtros.Ou(filtros.ToArray()));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<GrupoEconomico>(aux);
        }

        public virtual string GetQtEmpresasCadastradas
        {
            get
            {
                return this.Empresas != null && this.Empresas.Count > 0 ? this.Empresas.Count.ToString() : "0";
            }
        }

        
    }
}

