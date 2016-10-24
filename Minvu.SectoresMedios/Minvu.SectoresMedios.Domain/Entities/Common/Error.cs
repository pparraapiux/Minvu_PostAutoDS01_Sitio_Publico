using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.SectoresMedios.IData.Services;
using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.ORM;
using Minvu.SectoresMedios.IData.DAO;

namespace Minvu.SectoresMedios.Domain.Entities.Common
{
    public class Error
    {
        public int idError { get; set; }
    }

    public static class ErrorFactory
    {
        public static Error GuardarErrorEnDB(Exception ex, string username)
        {
            try
            {
                LOG_ERROR_SITIO logErrorDA = new LOG_ERROR_SITIO();
                logErrorDA.FECHA = DateTime.Now;
                if (ex.InnerException != null)
                    logErrorDA.XML_ERR = FormatearMensajeError(ex.Message, ex.StackTrace, username, ex.InnerException.Message, ex.InnerException.StackTrace);
                else
                    logErrorDA.XML_ERR = FormatearMensajeError(ex.Message, ex.StackTrace, username);

                if(logErrorDA.XML_ERR.Length > 3998)
                    logErrorDA.XML_ERR = logErrorDA.XML_ERR.Substring(0, 3998);

                var pIdError = ErrorDAO.guardarErrorDB(logErrorDA);

                return new Error { idError = pIdError };
            }
            catch
            {
                return new Error { idError = 0 };
            }
        }

        private static string FormatearMensajeError(string message, string StackTrace, string usuario, string innerMessage = "", string innerStackTrace = "" )
        {
            StringBuilder textoError = new StringBuilder();
            textoError.Append("<ErrorSM>");
            textoError.Append("<User>" + usuario + "</User>");
            textoError.Append("<ExceptionMessage>" + message + "</ExceptionMessage>");
            textoError.Append("<StackTrace>" + StackTrace + "</StackTrace>");
            if (!string.IsNullOrEmpty(innerMessage))
                textoError.Append("<InnerException>" + innerMessage + "</InnerException>");
            if (!string.IsNullOrEmpty(innerStackTrace))
                textoError.Append("<InnerStackTrace>" + innerStackTrace + "</InnerStackTrace>");
            textoError.Append("</ErrorSM>");

            return textoError.ToString();
        }

    }
}
