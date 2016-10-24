using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.ORM;
using System.Data.Entity.Core.Objects;
using System.Transactions;

namespace Minvu.SectoresMedios.IData.DAO.WorkflowMigra
{
    public static class WorkflowGestorDAO
    {
        public static void RegistrarEvento(string codigo_aplicacion, string codigo_tipo_evento, string codigo_tipo_objeto, int id_tipo_objeto, long id_objeto, int id_tipo_evento, long id_objeto_hijo, long id_objeto_padre, long id_operacion, string comentario_evento, DateTime fecha_evento, string id_usuario, string lista_adjuntos, string codigo_grupo)
        {
            using (Workflow_MigraEntities wf = new Workflow_MigraEntities())
            {
                //wf.Database.Connection.Open();
                if (id_tipo_objeto == 0)
                    id_tipo_objeto = ObtenerIdTipoObjeto(codigo_aplicacion, codigo_tipo_objeto, wf);

                // viene en 0 generalmente
                if (id_tipo_evento == 0)
                    id_tipo_evento = ObtenerIdTipoEvento(codigo_tipo_evento, wf);

                int id_instancia_objeto = 0;

                while (VerificarInstancia(id_tipo_objeto, id_objeto, out id_instancia_objeto, wf) == 1)
                {
                    RegistrarInstancia(codigo_aplicacion, codigo_tipo_objeto, id_objeto, 0L, wf);
                }

                int id_estado = ObtenerIdEstado(id_tipo_evento, wf);

                RegistrarEvento(id_instancia_objeto, id_tipo_evento, id_operacion, id_estado, fecha_evento, comentario_evento, id_usuario, codigo_grupo, wf);

                //int id_evento = Convert.ToInt32(outid_evento.Value);

                CambiarEstadoObjeto(id_instancia_objeto, id_estado, wf);
            }
        }

        private static int ObtenerIdEstado(int id_tipo_evento, Workflow_MigraEntities wf)
        {
            try
            {
                var result = wf.pa_Malla_Estados_Eventos_Gestor_ObtenerDetalleTipoEvento(id_tipo_evento).FirstOrDefault();
                return result.id_estado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int ObtenerIdTipoObjeto(string codigo_aplicacion, string codigo_tipo_objeto, Workflow_MigraEntities wf)
        {
            try
            {
                var outparam = new ObjectParameter("id_tipo_objeto", typeof(int));
                wf.pa_Workflow_Gestor_Obtener_id_tipo_objeto(codigo_aplicacion, codigo_tipo_objeto, outparam);
                if (outparam.Value != DBNull.Value)
                    return Convert.ToInt32(outparam.Value);
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int ObtenerIdTipoEvento(string codigo_tipo_evento, Workflow_MigraEntities wf)
        {
            try
            {
                var outparam = new ObjectParameter("id_tipo_evento", typeof(int));
                wf.pa_Workflow_Gestor_Obtener_id_tipo_evento(codigo_tipo_evento, outparam);
                if (outparam.Value != DBNull.Value)
                    return Convert.ToInt32(outparam.Value);
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void RegistrarEvento(int id_instancia_objeto, int id_tipo_evento, long id_operacion, int id_estado, DateTime fecha_evento, string comentario_evento, string id_usuario, string codigo_grupo, Workflow_MigraEntities wf)
        {
            try
            {
                var outid_evento = new ObjectParameter("id_evento", typeof(int));
                wf.pa_Workflow_Gestor_RegistrarEvento(id_instancia_objeto, id_tipo_evento, id_operacion, id_estado, fecha_evento, comentario_evento, id_usuario, codigo_grupo, outid_evento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int VerificarInstancia(int id_tipo_objeto, long id_objeto, out int id_instancia_objeto, Workflow_MigraEntities wf)
        {
            try
            {
                var outresultado = new ObjectParameter("resultado", typeof(int));
                var outid_instancia_objeto = new ObjectParameter("id_instancia_objeto", typeof(int));
                var outid_estado = new ObjectParameter("id_estado", typeof(int));
                wf.pa_Evento_Gestor_VerificarInstancia(id_tipo_objeto, id_objeto, outresultado, outid_instancia_objeto, outid_estado);
                if (outid_instancia_objeto.Value != DBNull.Value)
                    id_instancia_objeto = Convert.ToInt32(outid_instancia_objeto.Value);
                else
                    id_instancia_objeto = 0;

                if (outresultado.Value != DBNull.Value)
                    return Convert.ToInt32(outresultado.Value);
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void RegistrarInstancia(string codigo_aplicacion, string codigo_tipo_objeto, long id_objeto, long id_objeto_padre, Workflow_MigraEntities wf)
        {
            var out1 = new ObjectParameter("id_tipo_evento", typeof(int));
            var out2 = new ObjectParameter("id_tipo_objeto", typeof(int));
            wf.pa_Workflow_Gestor_RegistrarInstancia(codigo_aplicacion, codigo_tipo_objeto, (long?)id_objeto, (int?)id_objeto_padre, out1, out2);
        }

        private static void CambiarEstadoObjeto(int id_instancia_objeto, int id_estado, Workflow_MigraEntities wf)
        {
            pa_Workflow_Gestor_CambiarEstadoObjeto_Result obj = null;
            obj = wf.pa_Workflow_Gestor_CambiarEstadoObjeto(id_instancia_objeto, id_estado).FirstOrDefault();

            using (rukan_migraEntities context = new rukan_migraEntities())
            {
                if (obj != null)
                {
                    switch (obj.parametro_cambio_estado)
                    {
                        case "Inscripcion":
                            context.pa_Cambiar_Estado_Inscripcion_i_u((int?)obj.id_objeto, obj.codigo_estado, obj.descripcion_estado);
                            break;
                        case "Interesado":
                            context.pa_Cambiar_Estado_Interesado_i_u((int?)obj.id_objeto, obj.codigo_estado, obj.descripcion_estado);
                            break;
                        case "Postulacion":
                            context.pa_Cambiar_Estado_Postulacion_i_u((int?)obj.id_objeto, obj.codigo_estado, obj.descripcion_estado);
                            break;
                        case "Subsidio":
                            context.pa_Cambiar_Estado_Subsidio_i_u((int?)obj.id_objeto, obj.codigo_estado, obj.descripcion_estado);
                            break;
                        case "Vivienda":
                            context.pa_Cambiar_Estado_Vivienda_i_u((int?)obj.id_objeto, obj.codigo_estado, obj.descripcion_estado);
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}
