using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Persistencia.Utilitarios
{
    public class PersistenciaUtil
    {
        public static bool IsInWebContext
        {
            get { return HttpContext.Current != null; }
        }
    }
}
