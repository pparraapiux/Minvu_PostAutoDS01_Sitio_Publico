using Minvu.SectoresMedios.IData.DAO.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Domain.Managers
{
    public static class MessageManager
    {
        public static string ObtenerMensaje(string codigoMensaje)
        {
            return MessageManagerFactory.ObtenerMensaje(codigoMensaje);
        }
    }

    static class MessageManagerFactory
    {
        public static string ObtenerMensaje(string codigo)
        {
            return MessageManagerDAO.ObtenerMensaje(codigo);
        }
    }
}
