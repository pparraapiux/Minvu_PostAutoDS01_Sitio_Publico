using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Minvu.SectoresMedios.IData.ORM;

namespace Minvu.SectoresMedios.IData.DAO.WorkflowMigra
{
    public class InstanciaObjetoWorkflowDAO
    {
        public static instancia_objeto_workflow obtenerInstaciaObjetoWorkflow(long pIdObjeto, int pIdTipoObjeto)
        {

            try
            {
                using (Workflow_MigraEntities contexto = new Workflow_MigraEntities())
                {
                    var instanciaObjeto = from ins in contexto.instancia_objeto_workflow
                                          where ins.id_objeto == pIdObjeto
                                             && ins.id_tipo_objeto == pIdTipoObjeto
                                          select ins;

                    return instanciaObjeto.FirstOrDefault<instancia_objeto_workflow>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int guardarInstanciaObjetoWorkflow(instancia_objeto_workflow pInstanciaObjetoWorkflow)
        {
            try
            {
                using(Workflow_MigraEntities contexto = new Workflow_MigraEntities())
                {
                    contexto.instancia_objeto_workflow.Add(pInstanciaObjetoWorkflow);
                    contexto.SaveChanges();

                    return (int)pInstanciaObjetoWorkflow.id_instancia_objeto;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
