using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Domain.Entities.MotorCondicion
{
    [Serializable]
    public class MensajeErrorBE
    {
        public int tipoError { get; set; }
        public string mensaje { get; set; }

        public MensajeErrorBE() { }

        public MensajeErrorBE(int tipoError, string mensaje)
        {
            this.tipoError = tipoError;
            this.mensaje = mensaje;
        }
    }
}
