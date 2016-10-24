using Minvu.SectoresMedios.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.PSSIM_DatosFuncionario;
using System.Xml;

namespace Minvu.SectoresMedios.IData.Services
{
    public static class WsPssimDatosFuncionario
    {
        public static int ObtenerRegionUsuarioPSSIM(string username)
        {
            funcionarioSoapClient client = new funcionarioSoapClient();
            try
            {
                var resp = client.ObtenerDatosFuncionario(username);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(resp);
                XmlNodeList listaNodos = xml.SelectNodes("/ICE/RESPUESTA");
                foreach(XmlNode nodos in listaNodos)
                {
                    resp = nodos["IDRegion"].InnerText;
                }
                return (int.Parse(resp));
            }
            catch (Exception Ex)
            {
                //Log.Instance.Info("Error en BancoEstado.ConsultaSaldo RUT -> " + Rut + "DV -> " + Dv + "CUENTA -> " + Cuenta, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio PSSIM.", Ex.InnerException);
            }
        }
    }
}
