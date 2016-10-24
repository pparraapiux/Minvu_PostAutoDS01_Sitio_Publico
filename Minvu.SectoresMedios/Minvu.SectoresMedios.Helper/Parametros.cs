using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace Minvu.SectoresMedios.Helper
{
    public class Parametro
    {
        private XDocument xmlFile;

        private static Parametro instancia;

        private Parametro()
        {
            string FullAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()).Substring(6);
            string xmlPath = FullAppPath.Substring(0, FullAppPath.IndexOf("bin") - 1);
            xmlFile = XDocument.Load(xmlPath + "\\Parametros.xml");
        }

        public static Parametro Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Parametro();
                }
                return instancia;
            }
        }

        private XDocument XmlFile
        {
            get { return xmlFile; }
            set { xmlFile = value; }
        }

        /// <summary>
        /// Retorna un DataTable[KEY;VALUE] con los parametros que corresponda al nombre
        /// </summary>
        /// <param name="NombreParametro"></param>
        /// <returns></returns>
        public DataTable ObtenerParametro(string NombreParametro)
        {
            return ConvertirADataTable(GetParametro(NombreParametro));
        }

        /// <summary>
        /// Retorna un DataTable[KEY;VALUE] con los parametros que corresponda al nombre
        /// </summary>
        /// <param name="NombreParametro"></param>
        /// <returns></returns>
        public DataTable ObtenerParametro(string NombreParametro, string filtroPrograma)
        {
            return ConvertirADataTable(GetParametro(NombreParametro), filtroPrograma);
        }

        /// <summary>
        /// Retorna el ID del Parametro y valor buscado 
        /// </summary>
        /// <param name="xElements"></param>
        /// <returns></returns>
        public string ObtenerID(string NombreParametro, string Valor)
        {
            string ID = String.Empty;

            if (Valor != String.Empty)
            {
                foreach (XElement elemento in GetParametro(NombreParametro).Elements("valor"))
                {
                    if (elemento.Value.ToUpper() == Valor.ToUpper())
                        ID = elemento.Attribute("key").Value;
                }

            }
            else
            {
                ID = "0";
            }
            return ID;

        }

        public string ObtenerID(string NombreParametro, string Valor, string filtroPrograma)
        {
            string ID = String.Empty;

            if (Valor != String.Empty)
            {
                foreach (XElement elemento in GetParametro(NombreParametro).Elements("valor"))
                {
                    if (elemento.Value.ToUpper() == Valor.ToUpper() && elemento.Attribute("programa").Value == filtroPrograma)
                        ID = elemento.Attribute("key").Value;
                }

            }
            else
            {
                ID = "0";
            }
            return ID;

        }

        /// <summary>
        /// Restorna el Valor del Parametro y Id buscado
        /// </summary>
        /// <param name="NombreParametro"></param>
        /// <returns></returns>
        public string ObtnerValor(string NombreParametro, string ID)
        {
            string Valor = String.Empty;

            if (ID != String.Empty)
            {
                foreach (XElement elemento in GetParametro(NombreParametro).Elements("valor"))
                {
                    if (elemento.Attribute("key").Value.ToUpper() == ID.ToUpper())
                        Valor = elemento.Value;
                }
            }

            return Valor;

        }

        private IEnumerable<XElement> GetParametro(string NombreParametro)
        {
            var resultado = from res in xmlFile.Elements("parametros").Elements("parametro")
                            where (string)res.Attribute("id") == NombreParametro
                            select res;


            return resultado;

        }

        private DataTable ConvertirADataTable(IEnumerable<XElement> xElements)
        {
            DataTable data = new DataTable();

            data.Columns.Add("KEY");
            data.Columns.Add("VALUE");

            foreach (XElement elemeto in xElements.Elements("valor"))
            {
                DataRow fila = data.NewRow();
                fila[0] = elemeto.Attribute("key").Value;
                fila[1] = elemeto.Value;
                data.Rows.Add(fila);
            }

            return data;

        }

        /// <summary>
        ///  Filtra por programa
        /// </summary>
        /// <param name="xElements"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        private DataTable ConvertirADataTable(IEnumerable<XElement> xElements, string filtro)
        {
            DataTable data = new DataTable();

            data.Columns.Add("KEY");
            data.Columns.Add("VALUE");

            foreach (XElement elemeto in xElements.Elements("valor"))
            {
                if (elemeto.Attribute("programa").Value == filtro)
                {
                    DataRow fila = data.NewRow();
                    fila[0] = elemeto.Attribute("key").Value;
                    fila[1] = elemeto.Value;
                    data.Rows.Add(fila);
                }
            }

            return data;

        }

    }
}
