using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.ORM;
using System.Data.Entity.Core.Objects;
using Minvu.SectoresMedios.Helper;

namespace Minvu.SectoresMedios.IData.DAO.Common
{
    public static class CommonDAO
    {
        public static int? ObtenerProvinciaPorComuna(int? comId)
        {
            try
            {
                using (rukan_migraEntities context = new rukan_migraEntities())
                {
                    return context.RUKAN_MIGRA_PROVINCIA_SELECCIONARPORCOMUNA(comId).FirstOrDefault();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
