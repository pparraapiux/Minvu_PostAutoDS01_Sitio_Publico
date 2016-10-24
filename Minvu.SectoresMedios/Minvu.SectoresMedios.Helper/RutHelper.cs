using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minvu.SectoresMedios.Helper
{
    public static class RutHelper
    {
        public static string formatearRut(string cuerpoRut, string dv)
        {
            if (!string.IsNullOrEmpty(cuerpoRut) && !string.IsNullOrEmpty(dv))
            {
                int rut = int.Parse(cuerpoRut);
                return rut.ToString("N0") + "-" + dv;
            }
            else
                return null;
        }

        public static string cuerpoRut(string RutCompleto)
        {
            if (!string.IsNullOrEmpty(RutCompleto))
            {
                string rutSinPuntos = RutCompleto.Replace(".", "");
                string rutSinPuntosSinGuion = rutSinPuntos.Replace("-", "");
                string rut = rutSinPuntosSinGuion.Substring(0, rutSinPuntosSinGuion.Length - 1);
                return rut;
            }
            else
                return null;
        }

        public static string dvRut(string RutCompleto)
        {
            if (!string.IsNullOrEmpty(RutCompleto))
            {
                string rutSinPuntos = RutCompleto.Replace(".", "");
                string rutSinPuntosSinGuion = rutSinPuntos.Replace("-", "");
                string digito = rutSinPuntosSinGuion.Substring(rutSinPuntosSinGuion.Length - 1);
                return digito;
            }
            else
                return null;
        }

        public static bool validarRut(string pRut, string pDigito)
        {
            int cuerpo;
            int digitoAux;
            int contador;
            int multiplo;
            int acumulador;
            string rutDigito;

            try
            {
                if (pDigito != null)
                    pDigito = pDigito.ToUpper();

                cuerpo = int.Parse(pRut);
            }
            catch
            {
                return false;
            }

            contador = 2;
            acumulador = 0;

            while (cuerpo != 0)
            {
                multiplo = (cuerpo % 10) * contador;
                acumulador += multiplo;
                cuerpo = cuerpo / 10;
                contador++;
                if (contador == 8)
                {
                    contador = 2;
                }
            }

            digitoAux = 11 - (acumulador % 11);
            rutDigito = digitoAux.ToString().Trim().ToUpper();

            if (digitoAux == 10)
            {
                rutDigito = "K";
            }

            if (digitoAux == 11)
            {
                rutDigito = "0";
            }

            if (rutDigito == pDigito && !(int.Parse(pRut) == 0))
            {
                return true;
            }
            return false;
        }
    }
}