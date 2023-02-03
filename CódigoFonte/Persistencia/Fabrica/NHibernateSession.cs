using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Persistencia.Fabrica
{
    public class NHibernateSession
    {
        private ISession sessao;
        public ISession Sessao
        {
            get { return sessao; }
            set { sessao = value; }
        }
    }
}
