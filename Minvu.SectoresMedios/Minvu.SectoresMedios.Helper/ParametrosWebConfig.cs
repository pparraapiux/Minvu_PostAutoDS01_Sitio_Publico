using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Minvu.SectoresMedios.Helper
{
    public static class ParametrosWebConfig
    {
        private static XDocument _xml;
        public static string ObtenerParametroPorClave(string key)
        {
            string valor = ConfigurationManager.AppSettings.Get(key);
            return valor;
        }

        public static Dictionary<string, string> ObtenerParametrosPorPrograma(string programa)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            string[] keys = ConfigurationManager.AppSettings.AllKeys;
            foreach(string llave in keys)
            {
                if(llave.Contains(programa + "_"))
                {
                    parametros.Add(llave, ConfigurationManager.AppSettings.Get(llave));
                }
            }
            return parametros;
        }

        static ParametrosWebConfig()
        {
            string FullAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()).Substring(6);
            string xmlPath = FullAppPath.Substring(0, FullAppPath.IndexOf("bin") - 1);
            _xml = XDocument.Load(xmlPath + "\\Parametros.xml");
        }

        public static string ObtenerParametroXML(string ParamName, string programa)
        {
            string dict = null;
            IEnumerable<XElement> xElements = GetProgramas(programa);
            foreach (XElement elemento in xElements.Elements("parameter"))
            {
                if (elemento.Attribute("key").Value == ParamName)
                {
                    dict = elemento.Value;
                }
            }
            return dict;
        }

        public static Dictionary<string, string> ObtenerParametrosPorProgramaXML(string programa)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            IEnumerable<XElement> xElements = GetProgramas();
            foreach (XElement elemento in xElements.Elements("parameter"))
            {
                dict.Add(elemento.Attribute("key").Value, elemento.Value);
            }
            return dict;
        }

        private static IEnumerable<XElement> GetProgramas(string NombrePrograma = null)
        {
            IEnumerable<XElement> resultado = null;
            if (NombrePrograma == null)
            {
                resultado = from res in _xml.Elements("parameterRoot").Elements("program")
                            select res;
            }
            else
            {
                resultado = from res in _xml.Elements("parameterRoot").Elements("program")
                            where (string)res.Attribute("name") == NombrePrograma
                            select res;
            }

            return resultado;
        }
    }
}
