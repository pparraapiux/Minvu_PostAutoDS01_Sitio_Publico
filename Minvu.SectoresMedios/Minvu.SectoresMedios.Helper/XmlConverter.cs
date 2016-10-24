using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minvu.SectoresMedios.Helper
{
    public class XmlConverter
    {
        public static string xmlConvert(Object objeto, Type type)
        {
            string salidastring = "";
            StringWriter VALOR = new StringWriter();
            XmlSerializer SERIALIZADOR = new XmlSerializer(type);
            SERIALIZADOR.Serialize(VALOR, objeto);
            salidastring = VALOR.ToString();
            return salidastring;

        }
    }
}
