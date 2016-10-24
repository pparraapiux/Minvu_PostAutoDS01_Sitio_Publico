using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper.Enums.DS19
{
    public static class DS19Enums
    {
        public const int PRG_ID = 35;
        public const string AREA = "DS19";
        public const string CARPETA_PROYECTOS_ARCHIVOS = "proyectos";
        public const string BASE_DIR_DS19 = CommonEnums.FileUpload.BASE_DIR + "DS19/";

        public static class SolicitudDS19
        {
            public const int SOL_MOD = 1;
            public const int SOLICITUD_INGRESO = 280;
            public const int SOLICITUD_POSTULACION = 281;
            public const int SOLICITUD_ACTUALIZACION = 282;
            public const int SOL_EST = 5;
        }

        public static class Proyecto
        {
            public const int TIPO_PROYECTO = 136;
            public const int LIN_PRO_ID = 193;
            public const string BASE_DIR_PRY = BASE_DIR_DS19 + "proyectos/";
            public const int ACREDITACION_TIPO = 158;
            public const string RUTA_REPORT = "/Rukan/Report_DS19/RPT_ComprobanteIngresoProyectoDS19_Entidad";
            public const string RUTA_REPORT_POS = "/Rukan/Report_DS19/RPT_ComprobanteIngresoProyectoDS19_Serviu";
            
        }

        public static class ProyectoMotor
        {
            public const int TIPO_APLICACION = 75;
            public const int TIPO_COBERTURA = 2;
        }

        public static class Roles
        {
            public const string ROL_NACIONAL = "RUKAN_DS19_NACIONAL";
            public const string ROL_CONSULTA = "RUKAN_DS19_CONSULTA";
            public const string ROL_PROYECTO_SERVIU = "RUKAN_DS19_PROYECTO_SERVIU";
            public const string ROL_PROYECTO_ENTIDAD = "RUKAN_DS19_PROYECTO_ENTIDAD";
            public const string ROL_PROYECTO_EVALUADOR = "RUKAN_DS19_PROYECTO_EVALUADOR";
        }
    }
}
