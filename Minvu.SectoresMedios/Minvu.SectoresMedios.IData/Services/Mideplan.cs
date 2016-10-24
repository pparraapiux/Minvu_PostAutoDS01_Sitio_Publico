using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ICE_Mideplan;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Configuration;

namespace Minvu.SectoresMedios.IData.Services
{
    public class Mideplan
    {
        public static ICE ObtenerFps(int rut, string dv)
        {
            Log.Instance.Info("Consulta en Mideplan.ObtenerFps RUT -> " + rut + "DV ->" + dv);
            ICE objResultadoMP = new ICE();
            fpsSoapClient mideplan = new fpsSoapClient();
            Entrada oEntrada = new Entrada();
            try
            {
                oEntrada.Rut = rut.ToString();
                oEntrada.Dv = dv;
                oEntrada.Ussist = 0;
                oEntrada.Periodo = Convert.ToInt32(ConfigurationManager.AppSettings["PersistenciaMideplan"]);
                oEntrada.Fecha_Corte = 20101122;
                objResultadoMP = mideplan.FichaProteccionSocial(oEntrada);

                return objResultadoMP;

            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error en PropiedadHabitacional.ListaPropiedadesHabitacionales  RUT -> " + rut + "DV ->" + dv , Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación el Ministerio de Desarrollo Social.",Ex.InnerException);
            }
        }

    }
}
