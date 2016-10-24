using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ICE_Ahorro_SCOTIABANK;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.Services
{
    public class Scotiabank
    {
        public static ICE ConsultaSaldo(string Rut, string Dv, string Cuenta, int Periodo)
        {
            Log.Instance.Info("Consulta en Scotiabank.ConsultaSaldo RUT -> " + Rut + "DV ->" + Dv + "CUENTA -> " + Cuenta);
            BDD_orc_infahorro_prt_bdd_infahorroSoapClient bco = new BDD_orc_infahorro_prt_bdd_infahorroSoapClient();
            Infahorro entrada = new Infahorro();
            try
            {
                entrada.Rut = Convert.ToInt32(Rut);
                entrada.Dv = Dv;
                entrada.Ussist = Convert.ToInt32(ConfigurationManager.AppSettings["TramitePisee"]);
                entrada.Mes = DateTime.Now.AddMonths(-1).Month.ToString();
                entrada.Cuenta = Cuenta;
                entrada.Periodo = Convert.ToInt32(ConfigurationManager.AppSettings["PersistenciaAhorroScotiabank"]);
                entrada.Ano = DateTime.Now.AddMonths(-1).Year;
                entrada.Nllamado = "0";
                entrada.Programahabitacional = "0";

                var resp = bco.ope_prt_bdd_infahorro(entrada);

                return (resp);
            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error en Scotiabank.ConsultaSaldo RUT -> " + Rut + "DV ->" + Dv + "CUENTA -> " + Cuenta, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio de Banco Scotiabank / Del Desarrollo.", Ex.InnerException);
            }

        }

        public static void Bloqueo(string rut, string dv, string nroCuenta, int idllamado)
        {
            try
            {
                ICE_Ahorro_SCOTIABANK_Bloqueo.BDD_orc_bloqueo_Prt_bdd_bloqueoSoapClient bloq = new ICE_Ahorro_SCOTIABANK_Bloqueo.BDD_orc_bloqueo_Prt_bdd_bloqueoSoapClient();
                ICE_Ahorro_SCOTIABANK_Bloqueo.Bloqueo bloqueo = new ICE_Ahorro_SCOTIABANK_Bloqueo.Bloqueo();
                DateTime fechaActual = DateTime.Now;

                bloqueo.Ano = fechaActual.Year.ToString();
                bloqueo.Cuenta = nroCuenta;
                bloqueo.Dv = dv;
                bloqueo.Rut = rut.ToString();
                bloqueo.Mes = (fechaActual.Month - 1).ToString();
                bloqueo.Nllamado = idllamado.ToString();
                bloqueo.Programahabitacional = "31";
                bloqueo.Ussist = "0";
                bloqueo.Periodo = "-1";

                ICE_Ahorro_SCOTIABANK_Bloqueo.Ope_prt_bdd_bloqueoResponsePart Resultado = bloq.Ope_prt_bdd_bloqueo(bloqueo);

            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en Scotiabank.bloqueo RUT -> " + rut + "DV -> " + dv + "CUENTA -> " + nroCuenta, Ex);
            }
        }
    }
}
