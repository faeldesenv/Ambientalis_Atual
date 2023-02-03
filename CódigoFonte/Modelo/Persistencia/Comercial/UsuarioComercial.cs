using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Modelo;
using Persistencia.Filtros;
using System.Collections;

namespace Modelo
{
    public partial class UsuarioComercial : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static UsuarioComercial ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            UsuarioComercial classe = new UsuarioComercial();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<UsuarioComercial>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual UsuarioComercial ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<UsuarioComercial>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual UsuarioComercial Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<UsuarioComercial>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual UsuarioComercial SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<UsuarioComercial>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<UsuarioComercial> SalvarTodos(IList<UsuarioComercial> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<UsuarioComercial>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<UsuarioComercial> SalvarTodos(params UsuarioComercial[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<UsuarioComercial>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<UsuarioComercial>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<UsuarioComercial>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<UsuarioComercial> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            UsuarioComercial obj = Activator.CreateInstance<UsuarioComercial>();
            return fabrica.GetDAOBase().ConsultarTodos<UsuarioComercial>(obj);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<UsuarioComercial> Filtrar(int qtd)
        {
            UsuarioComercial cidade = new UsuarioComercial();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<UsuarioComercial>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual UsuarioComercial UltimoInserido()
        {
            UsuarioComercial ArquivoFisico = new UsuarioComercial();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<UsuarioComercial>(ArquivoFisico);
        }

        /// <summary>
        /// Retorna um bool se o usuário existe
        /// </summary>
        /// <returns>Bool</returns>
        public static bool ExisteUsuarioComEsteLogin(UsuarioComercial user)
        {
            if (user != null)
            {
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                IList resultado = fabrica.GetDAOBase().ConsultaSQL("select login from usuarios_comerciais where login='" + user.Login + "' and id<>" + user.Id);
                return resultado != null && resultado.Count > 0;
            }
            return false;
        }


        public static UsuarioComercial ValidaUsuario(ref UsuarioComercial user)
        {
            UsuarioComercial aux = new UsuarioComercial();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Login", user.Login));
            aux.AdicionarFiltro(Filtros.Eq("Senha", user.Senha));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<UsuarioComercial> users = fabrica.GetDAOBase().ConsultarComFiltro<UsuarioComercial>(aux);
            if (users != null && users.Count > 0)
            {
                return users[0];
            }
            return null;
        }
    }
}