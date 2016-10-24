using Minvu.SectoresMedios.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.DAO.MotorCondicion;
using Minvu.SectoresMedios.Helper;

namespace Minvu.SectoresMedios.Domain.Entities.MotorCondicion
{
    public class MotorCondicion
    {
        #region Propiedades
        public int lineaProceso { get; set; }
        public int? oferta { get; set; }
        public int? llamado { get; set; }
        public int aplicacion_tipo { get; set; }
        public int cobertura_tipo { get; set; }
        public List<Condicion> listaCondiciones { get; set; }
        public List<MensajeErrorBE> listaMensajesError { get; set; }
        public Dictionary<string, object> parametrosRetorno { get; set; }
        public Type objectType { get; set; }
        public Object objetoPrograma { get; set; }
        #endregion

        #region Constructores
        public MotorCondicion(Object ObjetoPrograma, int aplicacionTipo, int coberturaTipo, int linProId, int? llaId = null, int? ofeId = null)
        {
            this.lineaProceso = linProId;
            this.oferta = ofeId;
            this.llamado = llaId;
            this.aplicacion_tipo = aplicacionTipo;
            this.cobertura_tipo = coberturaTipo;
            this.listaCondiciones = new List<Condicion>();
            this.listaMensajesError = new List<MensajeErrorBE>();
            this.parametrosRetorno = new Dictionary<string, object>();
            this.objectType = ObjetoPrograma.GetType();
            this.objetoPrograma = ObjetoPrograma;
            this.listaCondiciones = MotorCondicionFactory.ObtenerCondiciones(aplicacionTipo, coberturaTipo, linProId, llaId, ofeId).ToList();
        }
        #endregion
    }

    public static class MotorCondicionFactory
    {
        public static IEnumerable<Condicion> ObtenerCondiciones(int aplicacionTipo, int coberturaTipo, int linProId, int? llaId = null, int? ofeId = null)
        {
            var resultado = MotorCondicionDAO.ObtenerCondiciones(linProId, llaId, aplicacionTipo, coberturaTipo);

            foreach(var item in resultado)
            {
                List<ParametroCondicion> parametros = new List<ParametroCondicion>();
                if((int)TypeHelper.ConvertToIntNullable(item["NUM_PARAMETRO"]) > 0)
                {
                    parametros = ObtenerParametros(TypeHelper.ConvertToIntNullable(item["ID_CONDICION"])).ToList();
                }

                yield return new Condicion 
                { 
                    idCondicion = (int)TypeHelper.ConvertToIntNullable(item["ID_CONDICION"]),
                    nombreCondicion = TypeHelper.ConvertObjectToString(item["NOM_CONDICION"]),
                    numParametro = (int)TypeHelper.ConvertToIntNullable(item["NUM_PARAMETRO"]),
                    tipoError = (int)TypeHelper.ConvertToIntNullable(item["ID_ERROR"]),
                    listaParametros = parametros
                };
            }
        }

        public static IEnumerable<ParametroCondicion> ObtenerParametros(int? idCondicion)
        {
            var resultado = MotorCondicionDAO.ObtenerParametros(idCondicion);
            foreach (var item in resultado)
            {
                yield return new ParametroCondicion
                {
                    nombreParametro = TypeHelper.ConvertObjectToString(item["NOM_PARAMETRO"]),
                    valorParametro = TypeHelper.ConvertObjectToString(item["VAL_PARAMETRO"])
                };
            }
        }
    }
}
