using Minvu.SectoresMedios.IData.DAO.WorkflowMigra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Domain.Managers
{
    public static class WorkflowManager
    {
        public static void RegistrarEvento(string codigo_aplicacion, string codigo_tipo_evento, string codigo_tipo_objeto, int id_tipo_objeto, long id_objeto, int id_tipo_evento, long id_objeto_hijo, long id_objeto_padre, long id_operacion, string comentario_evento, DateTime fecha_evento, string id_usuario, string lista_adjuntos = "", string codigo_grupo = "")
        {
            WorkflowGestorDAO.RegistrarEvento(codigo_aplicacion, codigo_tipo_evento, codigo_tipo_objeto, id_tipo_objeto, id_objeto, id_tipo_evento, id_objeto_hijo, id_objeto_padre, id_operacion, comentario_evento, fecha_evento, id_usuario, lista_adjuntos, codigo_grupo);
        }
    }
}
