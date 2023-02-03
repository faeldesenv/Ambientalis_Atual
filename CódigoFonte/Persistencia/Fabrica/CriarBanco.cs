using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;

namespace Persistencia.Fabrica
{
    internal class CriarBanco
    {
        public static void CriarBancoComNHibernate(ref NHibernate.Cfg.Configuration _objConf, int idConfig)
        {
            //CRIAR BANCO AUTOMATICAMENTE
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

            //StreamReader str = new StreamReader(ConfigurationManager.AppSettings["pathAplicacao"].ToString() + "/App_Data/ConfiguracoesBanco/" + idConfig.ToString() + ".xml");
            StreamReader str = new StreamReader(path + "/App_Data/ConfiguracoesBanco/" + idConfig.ToString() + ".xml");
            string xml = str.ReadToEnd();
            if (xml.Contains("Source=aragom;"))
            {
                var schemaExport = new SchemaExport(_objConf);
                schemaExport.Execute(true, true, false);
            } 
        }
       

    }
}
