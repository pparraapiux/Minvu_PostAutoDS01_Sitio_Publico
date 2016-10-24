using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Domain.Abstract
{
    public interface IFormularioValidate
    {
       List<object> Validate(object _object);
    }
}
