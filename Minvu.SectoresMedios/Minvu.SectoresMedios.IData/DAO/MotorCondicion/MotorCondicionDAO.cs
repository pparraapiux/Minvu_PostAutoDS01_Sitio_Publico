using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.DAO.MotorCondicion
{
    public static class MotorCondicionDAO
    {
        public static IEnumerable<Dictionary<string, object>> ObtenerCondiciones(int? LinProId, int? LlaId, int? DcbAplTip, int? DcbCbrTip, int? OfeId = null)
        {
            try
            {
                using (rukan_migraEntities contexto = new rukan_migraEntities())
                {
                    List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();

                    var result = contexto.RUKAN_MIGRA_USP_PRC_SELECCIONAR_CONDICION(LinProId, LlaId, OfeId, DcbAplTip, DcbCbrTip).ToList();
                    foreach (var item in result)
                    {
                        lista.Add(TypeHelper.ResultDictionary(item));
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Dictionary<string,object>> ObtenerParametros(int? IdCondicion)
        {
            try
            {
                using (rukan_migraEntities contexto = new rukan_migraEntities())
                {
                    List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();

                    var result = contexto.RUKAN_MIGRA_USP_PRC_SELECCIONAR_PARAMETROS(IdCondicion).ToList();
                    foreach (var item in result)
                    {
                        lista.Add(TypeHelper.ResultDictionary(item));
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
