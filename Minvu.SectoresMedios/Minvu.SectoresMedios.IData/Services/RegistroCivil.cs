using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.Services
{
    public class RegistroCivil
    {
        public static ICE_RegistroCivil.ICE ObtenerDatosRegistroCivil(string rut, string dv)
        {
            try
            {
                Log.Instance.Info("Consulta en RegistroCivil.ObtenerDatosRegistroCivil  RUT -> " + rut + "DV ->" + dv);
                ICE_RegistroCivil.REGCIVIL_orc_datos_persona_prt_regcivil_info_personaSoapClient conexion = new ICE_RegistroCivil.REGCIVIL_orc_datos_persona_prt_regcivil_info_personaSoapClient();
                ICE_RegistroCivil.Infopersona info = new ICE_RegistroCivil.Infopersona();

                info.Rut = Convert.ToInt64(rut);
                info.Dv = dv;
                info.Ussist = Convert.ToInt32(ConfigurationManager.AppSettings["Usisst_SRCEI"]);
                info.Periodo = Convert.ToInt32(ConfigurationManager.AppSettings["PersistenciaRegistroCivil"]);

                ICE_RegistroCivil.ICE respuesta = conexion.ope_prt_regcivil_info_persona(info);

                if (respuesta.minvuRutData != null)
                {
                    return respuesta;
                }
                else
                    return null;
            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error en RegistroCivil.ObtenerDatosRegistroCivil  RUT -> " + rut + "DV ->" + dv, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio de Registro civil e identificación", Ex.InnerException);
            }

        }

        public static ICE_Consulta_Serie_Cedula_Identidad.SalidaType ObtenerNumeroSerie(string rut, string dv, string serie)
        {
            try
            {
                ICE_Consulta_Serie_Cedula_Identidad.EntradaType entrada = new ICE_Consulta_Serie_Cedula_Identidad.EntradaType();
                entrada.Rut = new ICE_Consulta_Serie_Cedula_Identidad.RutType();
                entrada.Rut.Numero = Convert.ToInt32(rut);
                entrada.Rut.DV = dv;
                entrada.NumeroSerie = serie;
                ICE_Consulta_Serie_Cedula_Identidad.ValidarDocumentoClient validaDocumento = new ICE_Consulta_Serie_Cedula_Identidad.ValidarDocumentoClient();
                ICE_Consulta_Serie_Cedula_Identidad.SalidaType respuesta = validaDocumento.EstadoDocumento(entrada);
                return respuesta;
            }
            catch (Exception Ex)
            {
                //Logger.Escribir.Error(" Minvu.PlataformaSubsidio.Datos.WebService.RegistroCivil.ObtenerDatosRegistroCivil : " + Ex.ToString());
                //ServiceException SerEx = new ServiceException(Ex.ToString());
                //Ex.MensajeDeExcepcion = "Ha ocurrido un error de comunicación con el Registro Civil.";
                throw Ex;
            }

        }
    }
}
