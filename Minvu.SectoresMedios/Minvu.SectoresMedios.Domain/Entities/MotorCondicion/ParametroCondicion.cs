using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Domain.Entities.MotorCondicion
{
    [Serializable]
    public class ParametroCondicion
    {
        public string nombreParametro { get; set; }
        public string valorParametro { get; set; }
    }
}
