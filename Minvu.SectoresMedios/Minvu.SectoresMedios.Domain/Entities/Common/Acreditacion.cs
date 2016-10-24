using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ORM;

namespace Minvu.SectoresMedios.Domain.Entities.Common
{
    public class Acreditacion
    {
        public int idAcreditacion { get; set; }
        public string DescripcionAcreditacion { get; set; }
        public int TipoAcreditacion { get; set; }
        public bool ObligatoriedadAcreditacion { get; set; }
        public bool seleccionAcreditacion { get; set; }
    }

    public class AcreditacionFactory
    {



    }
}
