using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper.Enums
{
    public static class CommonEnums
    {
        public static class FileUpload
        {
            public const string BASE_DIR = "~/Upload/";
        }

        public static class MessageEnums
        {
            public const string NO_EXISTE_CONDICION = "NO_EXISTE_CONDICION";
            public const string MODALIDAD_INGRESO_DS19 = "MODALIDAD_INGRESO_DS19";
            public const string MODALIDAD_REPETICION_DOC_DS19 = "MODALIDAD_REPETICION_DOC_DS19";
            public const string VALOR_MAXIMO_VIV_VUL_DS19 = "VALOR_MAXIMO_VIV_VUL_DS19";
            public const string VALOR_MAXIMO_VIV_VUL_EX_DS19 = "VALOR_MAXIMO_VIV_VUL_EX_DS19";
            public const string VALOR_MAXIMO_VIV_MED_EX_DS19 = "VALOR_MAXIMO_VIV_MED_EX_DS19";
            public const string VALOR_MAXIMO_VIV_MED_DS19 = "VALOR_MAXIMO_VIV_MED_DS19";
            public const string VAL_PORC_VIV_VUL_DS19 = "VAL_PORC_VIV_VUL_DS19";
            public const string VAL_PORC_VIV_VUL_40_DS19 = "VAL_PORC_VIV_VUL_40_DS19";
            public const string VAL_PORC_VIV_VUL_60_DS19 = "VAL_PORC_VIV_VUL_60_DS19";
            public const string VAL_NUM_VIV_PERM_DS19 = "VAL_NUM_VIV_PERM_DS19";
            public const string VAL_NUM_VIV_MAX_DS19 = "VAL_NUM_VIV_MAX_DS19";
            public const string VAL_CANT_VIV_DET_DS19 = "VAL_CANT_VIV_DET_DS19";
            public const string VAL_DOC_PRY_TIP_DS19 = "VAL_DOC_PRY_TIP_DS19";
            public const string VAL_DOC_PERM_EDIF_DS19 = "VAL_DOC_PERM_EDIF_DS19";
            public const string VAL_DOC_ANTE_PRY_DS19 = "VAL_DOC_ANTE_PRY_DS19";
            public const string VAL_DOC_COMPRB_PRY_DS19 = "VAL_DOC_COMPRB_PRY_DS19";
            public const string VAL_COMPRB_REVINDP_DS19 = "VAL_COMPRB_REVINDP_DS19";
            public const string VAL_PRECIO_PORC_VIV1_DS19 = "VAL_PRECIO_PORC_VIV1_DS19";
            public const string VAL_PRECIO_PORC_VIV2_DS19 = "VAL_PRECIO_PORC_VIV2_DS19";
            public const string VAL_MIN_PRECIO_VIV_DS19 = "VAL_MIN_PRECIO_VIV_DS19";
            public const string VAL_MIN_PRECIO_TIP_DS19 = "VAL_MIN_PRECIO_TIP_DS19";
            public const string VAL_MTS_VIV_MIN_CASA_DS19 = "VAL_MTS_VIV_MIN_CASA_DS19";
            public const string VAL_MTS_VIV_MIN_DEPA_DS19 = "VAL_MTS_VIV_MIN_DEPA_DS19";
            public const string VAL_MTS_VIV_MAX_DS19 = "VAL_MTS_VIV_MAX_DS19";
            public const string VAL_ING_TIP_VIV_DS19 = "VAL_ING_TIP_VIV_DS19";
            public const string VALIDA_REGION_ENT_PROY_DS19 = "VALIDA_REGION_ENT_PROY_DS19";
        }

        public static class TipoMensaje
        {
            public const int ALERTA = 1;
            public const int ERROR = 2;
            public const int EXITO = 3;
            public const int INFO = 4;
        }

        public static class EstadoProyecto
        {
            /// <summary>
            /// Representa el estado de proyecto en preparación
            /// </summary>
            public const string PROYECTO_EN_PREPARACION = "En preparación";

            /// <summary>
            /// Representa el estado de proyecto beneficado
            /// </summary>
            public const string PROYECTO_BENEFICIADO = "Beneficiado";

            /// <summary>
            /// Representa el estado de proyecto EN CREACIÓN
            /// </summary>
            public const string PROYECTO_EN_CREACION = "En Creación";

            /// <summary>
            /// Representa el estado de proyecto ELIMINADO
            /// </summary>
            public const string PROYECTO_ELIMINADO = "Eliminado";

            /// <summary>
            /// Representa el estado de proyecto Ingresado
            /// </summary>
            public const string PROYECTO_INGRESADO = "Ingresado";
            /// <summary>
            /// Representa el codigo de proyecto Ingresado
            /// </summary>
            public const string PROYECTO_INGRESADO_COD = "PRYING";
            public const string PROYECTO_PRE_INGRESADO_COD = "PRYPRI";
            public const string PROYECTO_PRE_INGRESADO = "Pre-Ingresado";
            public const string PROYECTO_POSTULANDO = "Postulando";
            public const string PROYECTO_POSTULANDO_COD = "PRYPOS";
        }

        public static class TipoFolio
        {
            public const int FOLIO_SOLICITUD = 1;
            public const int FOLIO_GRUPO = 2;
        }

        public static class TipoGrupo
        {
            public const int GRUPO = 1;
            public const int PROYECTO = 2;
            public const int PROYECTOINDIVIDUAL = 5;
        }

        public static class CodigoTipoSolicitud
        {
            public const string SolicitudPostulacionIndividual = "11";
            public const string SolicitudPostulacionGrupal = "12";
            public const string SolicitudActualizacionPostulacionGrupal = "36";
            public const string SolicitudActualizacionPostulacionColectiva = "37";
            public const string SolicitudActualizacionPostulacionIndividual = "40";
        }

        public static class CodigoEstadoSolicitud
        {
            public const int EnBorrador = 1;
            public const int Grabada = 2;
            public const int Abierta = 3;
            public const int Cerrada = 4;
            public const int Enviada = 5;
        }

        public static class CodigoEstadoPostulacion
        {
            /// <summary>
            /// Postulación Vigente
            /// </summary>
            public const string POSVIG = "POSVIG";
            
            /// <summary>
            /// Postulación Renunciada
            /// </summary>
            public const string POSREN = "POSREN";

            /// <summary>
            /// Postulación Seleccionada
            /// </summary>
            public const string POSSEL = "POSSEL";

            /// <summary>
            /// Postulación No Seleccionada
            /// </summary>
            public const string POSNOS = "POSNOS";

            /// <summary>
            /// Postulación Procesada
            /// </summary>
            public const string POSPRC = "POSPRC";

            /// <summary>
            /// Postulación Cancelada
            /// </summary>
            public const string POSCAN = "POSCAN";
        }

        public static class CodigoEstadoIntegranteGrupo
        {
            public const int Vigente = 1;
            public const int Beneficiado = 6;
            public const int Eliminado = 2;
        }

        public static class CodigoEstadoLlamado
        {
            public const int Abierto = 1;
            public const int Cerrado = 2;
            public const int EnCreación = 3;
            public const int Programado = 4;
        }

        public static class CodigoTipoPostulacion
        {
            public const string PostulacionGrupal = "5";
            public const string PostulacionColectiva = "2";
            public const string PostulacionIndividual = "1";
        }

        public static class CodigoProyecto
        {
            public const int MejoramientoEntorno = 93;
            public const int ObrasNuevasEntorno = 94;
            public const int IntervencionIntegralEntorno = 115;
            public const int MejoramientoVivienda = 95;
            public const int AmpliacionVivienda = 96;
        }

        public static class TipoPostulacion
        {
            public const int Individual = 1;
            public const int Colectiva = 2;
            public const int Apelacion = 3;
            public const int Proyectos = 4;
            public const int Grupo = 5;
            public const int Leasing = 6;
        }
    }
}
