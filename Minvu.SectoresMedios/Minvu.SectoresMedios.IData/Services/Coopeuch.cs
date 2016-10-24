using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ICE_Ahorro_COOPEUCH;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.Services
{
    public class Coopeuch
    {
        public static ICE1 ConsultaSaldo(string Rut, string Dv, string Cuenta)
        {
            Log.Instance.Info("Consulta en Coopeuch.ConsultaSaldo RUT -> " + Rut + "DV ->" + Dv + "CUENTA -> " + Cuenta);
            Minvu_Coopeuch_orc_minvu_coopeuch_saldo_prt_coopeuch_infoAhorroSoapClient bco = new Minvu_Coopeuch_orc_minvu_coopeuch_saldo_prt_coopeuch_infoAhorroSoapClient();
            ICE entrada = new ICE();
            try
            {

                entrada.Rut = Rut.ToString();
                entrada.Dv = Dv;
                entrada.Ussist = ConfigurationManager.AppSettings["TramitePisee"];
                entrada.Mes = DateTime.Now.AddMonths(-1).Month.ToString();
                entrada.Cuenta = Cuenta;
                entrada.Periodo = ConfigurationManager.AppSettings["PersistenciaAhorroCoopeuch"];
                entrada.Agno = DateTime.Now.AddMonths(-1).Year.ToString();
                entrada.Nllamado = String.Empty;
                entrada.Programa_Habitacional = String.Empty;

                var resp = bco.infoAhorro(entrada);

                return (resp);
            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en Coopeuch.ConsultaSaldo RUT -> " + Rut + "DV ->" + Dv + "CUENTA -> " + Cuenta, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio de Coopeuch.", Ex.InnerException);
            }


        }

        public static void Bloqueo(string rut, string dv, string nroCuenta, int idllamado)
        {
            try
            {
                ICE_Ahorro_COOPEUCH_Bloqueo.Minvu_Coopeuch_orc_minvu_coopeuch_bloqueo_prt_coopeuch_bloqueoSoapClient bloq = new ICE_Ahorro_COOPEUCH_Bloqueo.Minvu_Coopeuch_orc_minvu_coopeuch_bloqueo_prt_coopeuch_bloqueoSoapClient();
                ICE_Ahorro_COOPEUCH_Bloqueo.ICE bloqueo = new ICE_Ahorro_COOPEUCH_Bloqueo.ICE();
                DateTime fechaActual = DateTime.Now;

                bloqueo.Agno = fechaActual.Year.ToString();
                bloqueo.Cuenta = nroCuenta;
                bloqueo.Dv = dv;
                bloqueo.Rut = rut.ToString();
                bloqueo.Mes = (fechaActual.Month - 1).ToString();
                bloqueo.Nllamado = idllamado.ToString();
                bloqueo.Programa_Habitacional = "31";
                bloqueo.Ussist = "0";
                bloqueo.Periodo = "-1";

                ICE_Ahorro_COOPEUCH_Bloqueo.ICE1 Resultado = bloq.BloqueoSaldo(bloqueo);

            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en Coopeuch.bloqueo RUT -> " + rut + "DV -> " + dv + "CUENTA -> " + nroCuenta, Ex);
            }
        }
    }
}
