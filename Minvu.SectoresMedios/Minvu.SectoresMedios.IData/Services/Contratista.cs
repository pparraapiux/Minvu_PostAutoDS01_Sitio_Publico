using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.ICE_Contratista;
using System.Xml;

namespace Minvu.SectoresMedios.IData.Services
{
    public class Contratista
    {
        public static XmlDocument obtenerContratista(int pRut)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                ICE_Contratista.ContratistaSoapClient con = new ICE_Contratista.ContratistaSoapClient();
                Entrada entrada = new Entrada();

                entrada.Rut = pRut;

                string strXml = con.ObtenerContratistaPorRut(entrada);
                xmlDoc.LoadXml(strXml);

                return xmlDoc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
