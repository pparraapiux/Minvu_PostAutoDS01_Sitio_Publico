using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.ICE_CRM_Obtener_Datos_Persona;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.Services
{
    public class Crm
    {
        public static SalidaType ConsultaDatosPersona(int Rut, string Dv)
        {
            Log.Instance.Info("Consulta en Crm.ConsultaDatosPersona RUT -> " + Rut + "DV ->" + Dv);
            SalidaType resultado = new SalidaType();
            RutType entrada = new RutType();

            try
            {
                entrada.Numero = Rut;
                entrada.DV = Dv;

                ObtenerCiudadanoClient info = new ObtenerCiudadanoClient();
                return info.ObtenerCiudadanoCrm(entrada);

            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error en Crm.ConsultaDatosPersona  RUT -> " + Rut + "DV ->" + Dv, Ex);
                throw Ex;
            }

        }
    }
}
