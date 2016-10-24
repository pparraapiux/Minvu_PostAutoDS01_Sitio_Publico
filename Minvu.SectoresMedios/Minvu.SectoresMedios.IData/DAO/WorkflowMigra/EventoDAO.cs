using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.ORM;

namespace Minvu.SectoresMedios.IData.DAO.WorkflowMigra
{
    public class EventoDAO
    {
        public static int guardarEventoWorkflow(evento pEvento)
        {
            try
            {
                using (Workflow_MigraEntities contexto = new Workflow_MigraEntities())
                {
                    contexto.evento.Add(pEvento);
                    contexto.SaveChanges();

                    return (int)pEvento.id_evento;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
