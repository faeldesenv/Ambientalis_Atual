﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using NHibernate.Exceptions;
using Persistencia.Filtros;
using System.Web;
using System.Runtime.Remoting.Messaging;
using Persistencia.Utilitarios;
using System.Collections;

namespace Persistencia.DAO
{
    /// <summary>
    /// Classe base de acesso a dados com os métodos gerais contidos
    /// em IDAOBase implementados utilizando a tecnologia NHibernate
    /// </summary>
    public class DAOBaseNHibernate : IDAOBase
    {
        #region _____________ MÉTODOS _______________

        #region ________ Salvar ________

        /// <summary>
        /// Inclui um novo objeto ou altera um objeto existente. Se o id estiver vazio
        /// o objeto é incluido, se o id estiver preenchido e existir, o objeto é alterado,
        /// se o id estiver preenchido e não existeri é lançada a exceção
        /// StaleStateException que deve ser tratada na interface
        /// </summary>
        /// <param name="o">O objeto a ser salvo com o id preenchido</param>
        /// <param name="idConfig">O Id da configuraçao do banco</param>
        /// <returns>O objeto salvo</returns>
        public T Salvar<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                o = (T)this.SalvarMultiEmpresa(o);
                o = (T)sessao.Merge(o);
                return o;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Salva um Objeto com Id predefinido
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"> O Obejto a ser Salvo</param>
        /// <returns>o Obejto Salvo</returns>
        public T SalvarComId<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                if (o.Id < 1)
                    throw new ArgumentException("O id não pode ser negativo ou igual a zero!");
                T objAux = (T)sessao.Get(o.GetType(), o.Id);
                if (objAux == null)
                {
                    this.SalvarMultiEmpresa(o);
                    sessao.Save(o, o.Id);
                    return o;
                }
                else
                    throw new ArgumentException("O objeto não pode ser salvo pois já existe um objeto armazenado com este id");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Dada uma lista de objetos, em cada um: Se o id estiver vazio
        /// o objeto é incluido, se o id estiver preenchido e existir, o objeto é alterado,
        /// se o id estiver preenchido e não existir é lançada a exceção
        /// StaleStateException que deve ser tratada na interface
        /// </summary>
        /// <param name="lista">A lista de objetos serem salvos com o id preenchido</param>
        /// <param name="idConfig">O Id da configuraçao do banco</param>
        /// <returns>A lista de objetos salvos</returns>
        public IList<T> SalvarTodos<T>(IList<T> lista) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    lista[i] = (T)this.SalvarMultiEmpresa((ObjetoBase)lista[i]);
                    lista[i] = (T)sessao.Merge(lista[i]);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region ________ Consultar ________

        /// <summary>
        /// Consulta um objeto por um ID
        /// </summary>
        /// <param name="o">O objeto com o Id preenchido</param>
        /// <param name="idConfig">O Id da configuraçao do bando</param>
        /// <returns>O objeto consultado ou nulo</returns>
        public T ConsultarPorID<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                o = (T)this.VerificarMultiEmpresa(o);
                o = (T)sessao.Get(o.GetType(), o.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
            return o;
        }

        /// <summary>
        /// Executa uma consulta com filtro, porém retorna um único resultado
        /// </summary>
        /// <param name="o">O objeto a ser consultado com sua lista de filtros preenchida</param>
        /// <param name="idConfig">O Id da configuraçao do bando</param>
        /// <returns>O único resultado, null se não existir ou lança HibernateException se tiver mais de um</returns>
        public T ConsultarUnicoComFiltro<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                o = (T)this.VerificarMultiEmpresa(o);
                //cria um criteria onde os filtros vão ser adicionados
                ICriteria criteria = sessao.CreateCriteria(o.GetType());

                //Percorre a lista de filtros do objeto da consulta e adiciona no criteria
                //de acordo com a instância de Filtro passada

                foreach (Filtro filtro in o.ListaFiltros)
                    filtro.adicionarFiltro(ref criteria);

                return criteria.UniqueResult<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Executa uma consulta com filtro
        /// </summary>
        /// <param name="o">O objeto a ser consultado com sua lista de filtros preenchida</param>
        /// <param name="idConfig">O Id da configuraçao do bando</param>
        /// <returns>A lista de objetos retornados pelo BD</returns>
        public IList<T> ConsultarComFiltro<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;

            try
            {
                o = (T)this.VerificarMultiEmpresa(o);
                //cria um criteria onde os filtros vão ser adicionados
                ICriteria criteria = sessao.CreateCriteria(o.GetType());

                //Percorre a lista de filtros do objeto da consulta e adiciona no criteria
                //de acordo com a instância de Filtro passada

                foreach (Filtro filtro in o.ListaFiltros)
                    filtro.adicionarFiltro(ref criteria);

                return criteria.List<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtém todos os objetos de um determinado tipo no banco de dados
        /// </summary>
        /// <param name="o">O tipo de objeto a ser consultado</param>
        /// <param name="idConfig">O Id da configuraçao do bando</param>
        /// <returns>A lista dos objetos retornados pelo BD</returns>
        public IList<T> ConsultarTodos<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                o = (T)this.VerificarMultiEmpresa(o);
                ICriteria criteria = sessao.CreateCriteria(o.GetType());

                //Para adicionar ao menos o filtro de multiEmpresa
                foreach (Filtro filtro in o.ListaFiltros)
                    filtro.adicionarFiltro(ref criteria);

                return criteria.List<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Executa um consulta com filtro, porem retorna um objeto como resultado
        /// </summary>
        /// <param name="o">O Objeto que possui a lista de filtros para a consulta</param>
        /// <returns>Um valor que representa o resultado dos filtros</returns>
        public object ConsultarProjecao(ObjetoBase o)
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                o = this.VerificarMultiEmpresa(o);
                //cria um criteria onde os filtros vão ser adicionados
                ICriteria criteria = sessao.CreateCriteria(o.GetType());

                //Percorre a lista de filtros do objeto da consulta e adiciona no criteria
                //de acordo com a instância de Filtro passada
                foreach (Filtro filtro in o.ListaFiltros)
                    filtro.adicionarFiltro(ref criteria);

                return criteria.UniqueResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        /// <summary>
        /// Destaca um objeto da sessão
        /// </summary>
        /// <param name="o">O objeto a ser destacado</param>
        /// <returns>Retorna true caso tenha sucesso, false se não houve sucesso</returns>
        public bool DestacarObjeto<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                sessao.Evict(o);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Exclui um objeto por um id, se o id não existir é lançada a exceção
        /// StaleStateException que deve ser tratada na interface
        /// </summary>
        /// <param name="idConfig">O Id da configuraçao do banco</param>
        /// <param name="o">O objeto a ser excluído com o id preenchido</param>
        public bool Excluir<T>(T o) where T : ObjetoBase
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;
            try
            {
                o = (T)this.VerificarMultiEmpresa(o);
                string nome = o.GetType().Name;
                string id = o.Id.ToString();
                o = sessao.Get<T>(o.Id);
                if (o == null)
                    throw new ArgumentException("Id \"" + id + "\" não encontrado para o objeto: " + nome);
                sessao.Delete(o);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Executa uma string SQl na Sessão
        /// </summary>
        /// <param name="s">A Sql a ser executada</param>
        /// <param name="sess">A sessão</param>
        /// <returns>Não tem retorno</returns>
        public void ExecutarComandoSql(string s)
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;

            try
            {
                sessao.CreateSQLQuery(s).ExecuteUpdate();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region __________ MULTI-EMPRESA ____________

        private ObjetoBase VerificarMultiEmpresa(ObjetoBase obj)
        {
            if ((PersistenciaUtil.IsInWebContext ? HttpContext.Current.Session["idEmp"] : CallContext.GetData("idEmp")) != null)
                if ((PersistenciaUtil.IsInWebContext ? HttpContext.Current.Session["idEmp"] : CallContext.GetData("idEmp")).ToString().ToUpper() != "TODOS")
                {
                    IList<ConfigMultiEmpresa> lista = this.RetornarListaMultiEmpresa();
                    if (lista == null || this.TestarNaoContem(obj, lista))
                        obj.ListaFiltros.Insert(0, new FiltroEq("Emp", PersistenciaUtil.IsInWebContext ? Convert.ToInt32(HttpContext.Current.Session["idEmp"].ToString()) : Convert.ToInt32(CallContext.GetData("idEmp").ToString())));
                }
            return obj;
        }

        private IList<ConfigMultiEmpresa> RetornarListaMultiEmpresa()
        {
            //Se não houver objetos de configuração na sessão web ou desktop é necessário realizar uma consulta
            if ((PersistenciaUtil.IsInWebContext && HttpContext.Current.Session["ConfigMultiEmpresa"] == null) || (!PersistenciaUtil.IsInWebContext && CallContext.GetData("ConfigMultiEmpresa") == null))
            {
                IList<ConfigMultiEmpresa> configs = this.ConsultarMultiEmpresas(new ConfigMultiEmpresa());
                if (PersistenciaUtil.IsInWebContext)
                    HttpContext.Current.Session["ConfigMultiEmpresa"] = configs;
                else
                    CallContext.SetData("ConfigMultiEmpresa", configs);
            }

            IList<ConfigMultiEmpresa> lista = PersistenciaUtil.IsInWebContext ? (List<ConfigMultiEmpresa>)HttpContext.Current.Session["ConfigMultiEmpresa"] : (List<ConfigMultiEmpresa>)CallContext.GetData("ConfigMultiEmpresa");
            if (lista == null)
                lista = new List<ConfigMultiEmpresa>();
            return lista;
        }

        private bool TestarNaoContem(ObjetoBase obj, IList<ConfigMultiEmpresa> lista)
        {
            string name = obj.GetType().Name.ToUpper();
            foreach (ConfigMultiEmpresa item in lista)
                if (name == item.Entidade.ToUpper())
                    return false;
            return true;
        }

        private ObjetoBase SalvarMultiEmpresa(ObjetoBase o)
        {
            ObjetoBase obj = (ObjetoBase)o;
            IList<ConfigMultiEmpresa> lista = this.RetornarListaMultiEmpresa();
            obj.Emp = lista == null || this.TestarNaoContem(obj, lista) ?
                (PersistenciaUtil.IsInWebContext ?
                HttpContext.Current.Session["idEmp"] != null ? Convert.ToInt32(HttpContext.Current.Session["idEmp"]) : obj.Emp
                : CallContext.GetData("idEmp") != null ? Convert.ToInt32(CallContext.GetData("idEmp")) : obj.Emp) :
                obj.Emp;
            return obj;
        }

        public IList<ConfigMultiEmpresa> ConsultarMultiEmpresas(ConfigMultiEmpresa o)
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;

            IList<ConfigMultiEmpresa> listaResultado;
            try
            {
                //Cria um criteria onde os filtros vão ser adicionados
                ICriteria criteria = sessao.CreateCriteria(o.GetType());

                //Percorre a lista de filtros do objeto da consulta e adiciona no criteria
                //de acordo com a instância de Filtro passada
                foreach (Filtro filtro in o.ListaFiltros)
                    filtro.adicionarFiltro(ref criteria);

                listaResultado = criteria.List<ConfigMultiEmpresa>();
                return listaResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public IList ConsultaSQL(string SQL)
        {
            ISession sessao = NHibernateSessionManager.Instance.ContextSession;

            try
            {
                IList ss = sessao.CreateSQLQuery(SQL).List();
                return ss;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
