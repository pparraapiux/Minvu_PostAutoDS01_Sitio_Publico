using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ORM;
using Minvu.SectoresMedios.IData.ICE_Propiedad_Habitacional;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.Services
{
    public class PropiedadHabitacional
    {
        #region <Const RegistroTecnico>
        private const string CONSTRUCTOR = "CONSTRUCTOR";
        #endregion <Const RegistroTecnico>
        public static ArrayOfICEPROPIEDADPROPIEDAD[] ListaPropiedadesHabitacionales(int rut, string dv)
        {
            try
            {
                Log.Instance.Info("Contulta PropiedadHabitacional.ListaPropiedadesHabitacionales  RUT -> " + rut + "DV ->" + dv);
                SII_orc_propiedades_habitacionales_prt_sii_prop_habitacionalesComunaSoapClient sii = new SII_orc_propiedades_habitacionales_prt_sii_prop_habitacionalesComunaSoapClient();
                Propiedades_habitacionales ciudadano = new Propiedades_habitacionales();
                ICE respuesta = new ICE();

                ciudadano.Rut = rut.ToString();
                ciudadano.Dv = dv;
                ciudadano.Periodo = Convert.ToInt32(ConfigurationManager.AppSettings["PersistenciaSII"]); //Persistencia
                ciudadano.Ussist = ConfigurationManager.AppSettings["Usisst_SII"];

                respuesta = sii.Propiedades_Habitacionales(ciudadano);
                
                if(respuesta.RESPUESTA != null)
                {
                     return respuesta.RESPUESTA;
                }
                else if(respuesta.RESULTADO.ESTADO == "05" || respuesta.RESULTADO.CODIGO == "0")
                {
                    return null;
                }
                else
                    throw new Exception("Error de Conexión con SII");
            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error en PropiedadHabitacional.ListaPropiedadesHabitacionales  RUT -> " + rut + "DV ->" + dv, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio de Impuestos Internos.", Ex.InnerException);
            }
        }

        public static WSSII_Rol.ICERESPUESTARESP_HDR ObtenerXMLServiciosSIIRolRespuestaPropiedad(string comuna, string manzana, string predio, int periodo, int CodigoTramite)
        { 
            WSSII_Rol.ICERESPUESTARESP_HDR resultado = null;
            try
            {
                WSSII_Rol.DatosRol proDatosRol = new WSSII_Rol.DatosRol();
                proDatosRol.Comuna = comuna;
                proDatosRol.Manzana = manzana;
                proDatosRol.Predio = predio;
                proDatosRol.Periodo = periodo.ToString();
                proDatosRol.Ussist = Convert.ToString(CodigoTramite);

                WSSII_Rol.SII_orc_inf_rol_prt_Datos_RolSoapClient servicio = new WSSII_Rol.SII_orc_inf_rol_prt_Datos_RolSoapClient();

                resultado = servicio.prt_Inf_rol(proDatosRol).RESPUESTA.RESP_HDR;

                return (resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    
        public string ObtenerXMLServicioRegistroTecnico(string rut, string digitoVerificador, string accion)
        {
            string resultado = "";
            try
            {
                RegistroTecnico.RegContratistasSoapClient servicio = new RegistroTecnico.RegContratistasSoapClient();

                RegistroTecnico.Contratista entrada = new RegistroTecnico.Contratista();

                entrada.Rut = Convert.ToInt32(rut);
                entrada.Accion = CONSTRUCTOR;

                resultado = servicio.EstadoContratista(entrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;

        }
    }
}
