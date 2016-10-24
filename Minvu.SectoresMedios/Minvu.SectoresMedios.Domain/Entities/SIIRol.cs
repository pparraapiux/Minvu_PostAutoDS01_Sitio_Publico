using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minvu.SectoresMedios.Domain.Entities
{
    public class SIIRol
    {
        public string RespuestaXml { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public bool EsValido()
        {
            if (String.IsNullOrEmpty(Estado) || Estado != "00") return false;

            return true;
        }

        /// <summary>
        /// Obtiene un rol segun los parametros ingresados.
        /// 
        /// 123-456789
        /// 
        /// 123 => Manzana
        /// 456789 => Predio
        /// 
        /// </summary>
        /// <param name="comuna"></param>
        /// <param name="manzana">Codigo que antecede el Guion en el ROL</param>
        /// <param name="predio">Codigo que precede el Guion en el ROL</param>
        /// <param name="periodo"></param>
        /// <param name="CodigoTramite"></param>
        /// <returns>Objeto SIIRol con los datos obtenidos.</returns>
        public static SIIRol ObtenerSIIRol(int comuna, string manzana, string predio, int periodo, int CodigoTramite)
        {
            string ComunaSII = (ComunaDAO.ObtenerComunaSII_por_Comuna(comuna)??0).ToString();
            SIIRol rol = new SIIRol();
            try
            {
                Minvu.SectoresMedios.IData.WSSII_Rol.ICERESPUESTARESP_HDR resultado = PropiedadHabitacional.ObtenerXMLServiciosSIIRolRespuestaPropiedad(ComunaSII, manzana, predio, periodo, CodigoTramite);
                XmlSerializer serializer = new XmlSerializer(resultado.GetType());
                StringWriter sw = new StringWriter();
                serializer.Serialize(sw, resultado);

                

                rol.RespuestaXml = sw.ToString();

                NumberFormatInfo providerPuntaje = new NumberFormatInfo();
                providerPuntaje.CurrencyDecimalSeparator = ".";
                string estado = "";
                string descripcion = null;
                if (resultado != null && resultado.ESTADO != null)
                {
                    estado = resultado.ESTADO;
                    descripcion = resultado.GLOSA;
                }
                rol.Descripcion = descripcion;
                rol.Estado = estado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rol;
        }
    }
}
