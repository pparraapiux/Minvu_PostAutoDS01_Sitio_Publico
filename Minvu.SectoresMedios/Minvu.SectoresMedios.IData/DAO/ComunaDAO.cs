using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Minvu.SectoresMedios.IData.ORM;
using Minvu.SectoresMedios.Helper;

namespace Minvu.SectoresMedios.IData.DAO
{
    public class ComunaDAO
    {
        public static List<RUKAN_MIGRA_usp_con_Comuna_Result> obtenerComunas(int id_region, int id_provincia)
        {
            try
            {
                using (rukan_migraEntities contexto = new rukan_migraEntities())
                {
                    return contexto.RUKAN_MIGRA_usp_con_Comuna(id_region, id_provincia).Where(x => !x.COM_DES.StartsWith("Ex-") && !x.COM_DES.Contains("Santiago Sur") && !x.COM_DES.Contains("Santiago Oeste")).ToList();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static int? ObtenerComunaSII_por_Comuna(int Comuna)
        {   
            using (rukan_migraEntities contexto = new rukan_migraEntities())
            {
                return contexto.RUKAN_MIGRA_usp_con_COMUNASII_X_IDCOMUNA(Comuna).FirstOrDefault();
            }
        }


        public static pa_Comuna_i_s_Result ObtenerComuna(int idComuna)
        {
            using (rukan_migraEntities context = new rukan_migraEntities())
            {
                return context.pa_Comuna_i_s(idComuna).FirstOrDefault<pa_Comuna_i_s_Result>();

            }
        }

        public static RUKAN_MIGRA_USP_CON_COMUNA_PROVINCIA_REGION_Result ObtieneRegionProvinciaComuna(int idComuna)
        {
            using (rukan_migraEntities context = new rukan_migraEntities())
            {
                return context.RUKAN_MIGRA_USP_CON_COMUNA_PROVINCIA_REGION(idComuna).FirstOrDefault<RUKAN_MIGRA_USP_CON_COMUNA_PROVINCIA_REGION_Result>();

            }
        }
    }
}
