using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Minvu.SectoresMedios.Helper
{
    public static class CommonHelper
    {
        public static bool CompruebaRol(string rol, Dictionary<string, bool> d)
        {
            //HttpContext.Current.Session[""] = 0;
            return d.First(x => x.Key == rol).Value;
        }
    }
}
