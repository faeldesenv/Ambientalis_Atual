﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;
using System.Reflection;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;

namespace Persistencia.Fabrica
{
    /// <summary>
    /// Classe responsável por criar conexões com o banco de dados utilizando a 
    /// tecnologia NHibernate
    /// </summary>
    public class SessionFactoryNHibernate
    {
        private NHibernate.Cfg.Configuration _objConf = null;

        public SessionFactoryNHibernate() { }

        /// <summary>
        /// Obtém a instância Singleton da Fábrica de Sessões do NHibernate
        /// </summary>
        /// <returns>A fábrica de sesões do NHibernate</returns>
        public ISessionFactory GetSessionFactory(int idConfig)
        {
            return idConfig > -1 ? this.CriarConexao(idConfig) : null;
        }

        /// <summary>
        /// Cria a conexão utilizando a configuração e instanciando a fábrica
        /// de sessões
        /// </summary>
        private ISessionFactory CriarConexao(int idConfig)
        {
            if (idConfig < 0)
                throw new ArgumentException("Id não pode ser negativo");
            try
            {
                _objConf = new NHibernate.Cfg.Configuration();

                String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

                //verifica se a a aplicaçao é web
                if (HttpContext.Current != null)
                    _objConf.Configure(path + "/App_Data/ConfiguracoesBanco/" + idConfig.ToString() + ".xml");
                else
                    _objConf.Configure(path + "/Utilitarios/ConfiguracoesBanco/" + idConfig.ToString() + ".xml");


                _objConf.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(Assembly.Load("Modelo")));


                ////########################################################################################################
                ////########################################################################################################
                CriarBanco.CriarBancoComNHibernate(ref _objConf, idConfig);
                ////########################################################################################################
                ////########################################################################################################
                ////##################### IMPORTANTE: LEMBRE DE COMENTAR E DAR REBUILD #####################################

                return _objConf.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
