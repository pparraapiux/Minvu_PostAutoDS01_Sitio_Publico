using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ICE_Ahorro_CajaLosAndes;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Minvu.SectoresMedios.IData.Services
{
    public class CajaLosAndes
    {
        public static ICE1 ConsultaSaldo(string Rut, string Dv, string Cuenta)
        {
            Log.Instance.Info("Consulta en CajaLosAndes.ConsultaSaldo RUT -> " + Rut + "DV ->" + Dv + "CUENTA -> " + Cuenta);
            Minvu_CajaLosAndes_orcCajaLosAndesSaldo_prt_CajaLosAndes_SaldoSoapClient bco = new Minvu_CajaLosAndes_orcCajaLosAndesSaldo_prt_CajaLosAndes_SaldoSoapClient();
            ICE entrada = new ICE();
            try
            {

                entrada.Rut = Convert.ToString(Rut);
                entrada.Dv = Dv;
                entrada.Ussist = "0";
                entrada.Mes = Convert.ToString(DateTime.Now.AddMonths(-1).Month);
                entrada.Cuenta = Cuenta;
                entrada.Periodo = ConfigurationManager.AppSettings["PersistenciaAhorroCajaLosAndes"];
                entrada.Agno = Convert.ToString(DateTime.Now.AddMonths(-1).Year);
                entrada.NLlamado = String.Empty;
                entrada.Programa_Habitacional = String.Empty;

                var resp = bco.InfoAhorro(entrada);

                return (resp);

            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en CajaLosAndes.ConsultaSaldo RUT -> " + Rut + "DV ->" + Dv + "CUENTA -> " + Cuenta, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio de Caja Los Andes.", Ex.InnerException);
            }

        }

        public static void Bloqueo(string rut, string dv, string nroCuenta, int idllamado)
        {
            try
            {
                ICE_AhorroCajaLosAndes_Bloqueo.Minvu_CajaLosAndes_orcCajaLosAndesBloqueo_prt_CajaLosAndes_BloqueoSoapClient bloq = new ICE_AhorroCajaLosAndes_Bloqueo.Minvu_CajaLosAndes_orcCajaLosAndesBloqueo_prt_CajaLosAndes_BloqueoSoapClient();
                ICE_AhorroCajaLosAndes_Bloqueo.ICE bloqueo = new ICE_AhorroCajaLosAndes_Bloqueo.ICE();
                DateTime fechaActual = DateTime.Now;

                bloqueo.Agno = fechaActual.Year.ToString();
                bloqueo.Cuenta = nroCuenta;
                bloqueo.Dv = dv;
                bloqueo.Rut = rut.ToString();
                bloqueo.Mes = (fechaActual.Month - 1).ToString();
                bloqueo.NLlamado = idllamado.ToString();
                bloqueo.Programa_Habitacional = "31";
                bloqueo.Ussist = "0";
                bloqueo.Periodo = "-1";

                ICE_AhorroCajaLosAndes_Bloqueo.ICE1 Resultado = bloq.Bloqueo_Cuenta(bloqueo);

            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en CajaLosAndes.bloqueo RUT -> " + rut + "DV -> " + dv + "CUENTA -> " + nroCuenta, Ex);
            }
        }
    }
}
