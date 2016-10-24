using Minvu.SectoresMedios.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.DAO.Managers
{
    public static class MessageManagerDAO
    {
        public static string ObtenerMensaje(string codigo)
        {
            try
            {
                using (rukan_migraEntities context = new rukan_migraEntities())
                {
                    return context.Mensaje_SeleccionarPorCodigo(codigo).FirstOrDefault<string>();
                }
            }
            catch (Exception ex)
            {
                string messageException = String.Format("Ha ocurrido un error al intentar acceder al repositorio de mensajes, para obtener el mensaje: '{0}'", codigo);
                throw new Exception(messageException, ex);
            }
        }
    }
}
