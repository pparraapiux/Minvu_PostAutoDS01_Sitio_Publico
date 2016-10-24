using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper.Enums
{
    public static class WorkflowEnums
    {
        public const string COD_APLICACION_RUKAN = "RUKAN";

        public static class CodigoTipoEvento
        {
            public const string PRYPREING = "PRYPREING";
            public const string INGPRY = "INGPRY";
            public const string RUKANPRY = "RUKANPRY";
            public const string PRYAPR = "PRYAPR";
            public const string ACTPRY = "ACTPRY";
            public const string PRYRZD = "PRYRZD";
            public const string PRYCOB = "PRYCOB";
            public const string ACTEVA = "ACTEVA";
            public const string PRYELIPRY = "PRYELIPRY";
            public const string PRYCOI = "PRYCOI";
            public const string PRYPRESEL = "PRYPRESEL";
            public const string PRYPOS = "PRYPOS";
            public const string PRYPOSACT = "PRYPOSACT";
        }

        public static class TipoObjetoWorkflow
        {
            public const int Persona = 1;
            public const int Grupo = 2;
            public const int Inscripcion = 3;
            public const int Postulacion = 4;
            public const int Subsidio = 5;
            public const int Proyecto = 6;
            public const int Vivienda = 7;
        }

        public static class CodigoTipoEventoWorkflow
        {
            public const int PostulacionIndividual = 12; //Evento para postulacion individual
            public const int PostulacionEnGrupo = 13;    //Evento para postulacion de integrante de grupo
            public const int PostulacionColectiva = 35;  //Evento para postulación de grupo
            public const int ActualizacionPostulacionIndividual = 190;
            public const int ActualizacionPostulacionDeGrupo = 188;
            public const int ActualizacionPostulacionIntegranteDeGrupo = 189;
        }

        public static class CodigoEstadoWorkflow
        {
            public const int POSVIG = 18;
            public const int POSEPS = 19;
            public const int POSSEL = 20;
            public const int POSNOS = 21;
            public const int POSEPA = 22;
            public const int POSAAP = 23;
            public const int POSARZ = 24;
            public const int POSREN = 25;
            public const int POSPRC = 26;
            public const int POSCAN = 46;
            public const int POSLSG = 47;
            public const int POSPRR = 48;
            public const int POSPEL = 32; 
        }

        public static class ComentarioEventoWorkflow
        {
            public const string PostulaciónIndividual = "Postulación Individual";
            public const string PostulaciónEnGrupo = "Postulación en Grupo";
            public const string PostulaciónColectiva = "Postulación Colectiva";
            public const string ActualizaciónDePostulaciónIndividual = "Actualización de Postulación Individual";
            public const string ActualizaciónDePostulaciónEnGrupo = "Actualización de Postulación Integrante de Grupo";
            public const string ActualizaciónDePostulaciónGrupal = "Actualización de Postulación de Grupo";
        }

    }
}
