using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Minvu.SectoresMedios.IData.ORM;
using System.Data.Entity.Core;
using Minvu.SectoresMedios.Helper;


namespace Minvu.SectoresMedios.IData.DAO
{
    public class ErrorDAO
    {

        public static int guardarErrorDB(LOG_ERROR_SITIO pLogError)
        {
            Log.Instance.Info("ErrorDAO.guardarErrorDB");
            using (rukan_migraEntities contexto = new rukan_migraEntities())
            {
                try
                {
                    contexto.LOG_ERROR_SITIO.Add(pLogError);
                    contexto.SaveChanges();
                    return pLogError.LOG_ID;
                }
                catch (OptimisticConcurrencyException)
                {
                    contexto.Entry<LOG_ERROR_SITIO>(pLogError).Reload();
                    contexto.SaveChanges();
                    return pLogError.LOG_ID;
                }
                catch (Exception Ex)
                {
                    Log.Instance.Error("Error ErrorDAO.guardarErrorDB", Ex);
                    throw Ex;
                }
            }
        }
            
    }
}
