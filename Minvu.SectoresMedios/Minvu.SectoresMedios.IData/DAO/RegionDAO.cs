using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Minvu.SectoresMedios.IData.ORM;
using Minvu.SectoresMedios.Helper;

namespace Minvu.SectoresMedios.IData.DAO
{
    public class RegionDAO
    {
        public static List<RUKAN_MIGRA_USP_CON_REGION_SELECCION_TODOS_DS01_Result> obtenerRegiones()
        {
            try
            {
                using (rukan_migraEntities contexto = new rukan_migraEntities())
                {
                    return contexto.RUKAN_MIGRA_USP_CON_REGION_SELECCION_TODOS_DS01().ToList();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}

