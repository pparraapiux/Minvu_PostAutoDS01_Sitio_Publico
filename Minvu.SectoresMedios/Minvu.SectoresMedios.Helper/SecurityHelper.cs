using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper
{
    public static class SecurityHelper
    {
        public static string Encrypt(string strCadena)
        {
            byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(strCadena);
            return Convert.ToBase64String(Buffer);
        }

        public static string Decrypt(string strCadena)
        {
            byte[] Buffer = Convert.FromBase64String(strCadena);
            return System.Text.ASCIIEncoding.ASCII.GetString(Buffer);
        }

        public static string GenerarGUID()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
