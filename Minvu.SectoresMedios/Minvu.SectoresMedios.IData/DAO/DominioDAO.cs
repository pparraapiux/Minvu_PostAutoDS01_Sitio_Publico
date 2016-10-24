using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.ORM;

namespace Minvu.SectoresMedios.IData.DAO
{
    public class DominioDAO
    {
        public static List<RUKAN_MIGRA_usp_con_DOMINIO_Result> obtenerValoresPorDominio(string dominio)
        {
            try
            {
                using (rukan_migraEntities contexto = new rukan_migraEntities())
                {
                    return contexto.RUKAN_MIGRA_usp_con_DOMINIO(dominio).ToList();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
    }
}
