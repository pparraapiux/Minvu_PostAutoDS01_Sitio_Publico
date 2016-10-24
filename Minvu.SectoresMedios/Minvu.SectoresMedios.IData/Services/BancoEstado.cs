using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ICE_Ahorro_ESTADO;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Configuration;

namespace Minvu.SectoresMedios.IData.Services
{
    public class BancoEstado
    {
        public static ICE ConsultaSaldo(string Rut, string Dv, string Cuenta)
        {
            Log.Instance.Info("Consulta en BancoEstado.ConsultaSaldo RUT -> " + Rut + "DV -> " + Dv + "CUENTA -> " + Cuenta);
            BCOESTADO_orc_infahorro_prt_bcoestado_infahorroSoapClient bco = new BCOESTADO_orc_infahorro_prt_bcoestado_infahorroSoapClient();
            Infahorro entrada = new Infahorro();
            try
            {

                entrada.Rut = Convert.ToInt32(Rut);
                entrada.Dv = Dv;
                entrada.Ussist = 0;
                entrada.Mes = DateTime.Now.AddMonths(-1).Month;
                entrada.Cuenta = Cuenta;
                entrada.Periodo = Convert.ToDouble(ConfigurationManager.AppSettings["PersistenciaAhorroEstado"]);
                entrada.Ano = DateTime.Now.AddMonths(-1).Year;
                entrada.Nllamado = String.Empty;
                entrada.Programahabitacional = String.Empty;
                var resp = bco.ope_prt_bcoestado_infahorro(entrada);

                return (resp);
            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en BancoEstado.ConsultaSaldo RUT -> " + Rut + "DV -> " + Dv + "CUENTA -> " + Cuenta, Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con el Servicio de Banco Estado.", Ex.InnerException);
            }

        }

        public static void Bloqueo(string rut, string dv, string nroCuenta, int idllamado)
        {
            try
            {
                ICE_Ahorro_ESTADO_Bloqueo.BCOESTADO_orc_bloqueo_prt_bcoestado_bloqueoSoapClient bloq = new ICE_Ahorro_ESTADO_Bloqueo.BCOESTADO_orc_bloqueo_prt_bcoestado_bloqueoSoapClient();
                ICE_Ahorro_ESTADO_Bloqueo.Bloqueo bloqueo = new ICE_Ahorro_ESTADO_Bloqueo.Bloqueo();
                DateTime fechaActual = DateTime.Now;

                bloqueo.Ano = fechaActual.Year;
                bloqueo.Cuenta = nroCuenta;
                bloqueo.Dv = dv;
                bloqueo.Rut = Convert.ToInt32(rut);
                bloqueo.Mes = fechaActual.Month - 1;
                bloqueo.Nllamado = idllamado.ToString();
                bloqueo.Programahabitacional = "31";
                bloqueo.Ussist = 0;
                bloqueo.Periodo = -1;

                ICE_Ahorro_ESTADO_Bloqueo.ICE Resultado = bloq.ope_prt_bcoestado_bloqueo(bloqueo);

            }
            catch (Exception Ex)
            {
                Log.Instance.Info("Error en BancoEstado.bloqueo RUT -> " + rut + "DV -> " + dv + "CUENTA -> " + nroCuenta, Ex);
            }
        }
    }
}
