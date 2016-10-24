using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Minvu.SectoresMedios.Site.Utils
{
    public class LayoutInfo
    {
        public static string ObtenerVersion()
        {
            string version = "v";
            Assembly ass = Assembly.GetExecutingAssembly();
            AssemblyName an = ass.GetName();
            version += an.Version.ToString();
            return version;
        }
    }
}