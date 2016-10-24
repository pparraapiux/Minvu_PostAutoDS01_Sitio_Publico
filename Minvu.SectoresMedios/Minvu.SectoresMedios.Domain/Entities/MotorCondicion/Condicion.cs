using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Minvu.SectoresMedios.IData.DALEntities.MotorCondicion;

namespace Minvu.SectoresMedios.Domain.Entities.MotorCondicion
{
    [Serializable]
    public class Condicion
    {
        public int idCondicion { get; set; }
        public string nombreCondicion { get; set; }
        public List<ParametroCondicion> listaParametros { get; set; }
        public int numParametro { get; set; }
        public int tipoError { get; set; }

        public Condicion() { listaParametros = new List<ParametroCondicion>(); }

        //public Condicion(CondicionDALEntity p)
        //{
        //    this.idCondicion = p.idCondicion;
        //    this.nombreCondicion = p.nombreCondicion;
        //    this.listaParametros = p.listaParametros;
        //    this.numParametro = p.numParametro;
        //    this.tipoError = p.tipoError;
        //}
    }
}
