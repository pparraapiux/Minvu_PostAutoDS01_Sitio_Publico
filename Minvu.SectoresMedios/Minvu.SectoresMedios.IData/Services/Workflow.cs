using Minvu.SectoresMedios.Helper;
using Minvu.SectoresMedios.IData.ICE_WorkFlowGestor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.IData.Services
{
    public class WorkFlow
    {
        public static string RegistraEvento(
            string pCodigoAplicacion, string pCodigoTipoEvento, string pCodigoTipoObjeto,
            int pIdTipoObjeto, int pIdObjeto, int pIdTipoEvento, int pIdObjetoHijo,
            int pIdObjetoPadre, int pIdOperacion, string pComentarioEvento,
            DateTime pFechaEvento, string pidUsuario, string pListaAdjuntos,
            string pCodigoGrupo
            )
        {
            try
            {
                Log.Instance.Info("Registrar en WorkFlow.RegistraEvento");
                WorkFlowGestorSoapClient client = new WorkFlowGestorSoapClient();
                Entrada entrada = new Entrada()
                {
                    codigo_aplicacion = pCodigoAplicacion,
                    codigo_tipo_evento = pCodigoTipoEvento,
                    codigo_tipo_objeto = pCodigoTipoObjeto,
                    codigo_grupo = pCodigoGrupo,
                    comentario_evento = pComentarioEvento,
                    fecha_evento = pFechaEvento,
                    id_objeto = pIdObjeto,
                    id_objeto_hijo = pIdObjetoHijo,
                    id_objeto_padre = pIdObjetoPadre,
                    id_operacion = pIdOperacion,
                    id_tipo_evento = pIdTipoEvento,
                    id_tipo_objeto = pIdTipoObjeto,
                    id_usuario = pidUsuario,
                    lista_adjuntos = pListaAdjuntos
                };
                return client.RegistrarEvento(entrada);

            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error Registrar en WorkFlow.RegistraEvento", Ex);
                throw Ex;
            }

        }
    }
}
