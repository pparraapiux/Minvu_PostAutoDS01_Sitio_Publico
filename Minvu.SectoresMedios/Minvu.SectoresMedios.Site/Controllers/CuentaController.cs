using Minvu.SectoresMedios.IData.ICE_Consulta_Serie_Cedula_Identidad;
using Minvu.SectoresMedios.IData.Services;
using Minvu.SectoresMedios.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.SectoresMedios.Site.Controllers
{
    public class CuentaController : Controller
    {
        //
        // GET: /Cuenta/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InicioSesion()
        {
            return View();
        }

        public ActionResult Login(Cuenta cuenta)
        {
            cuenta.Id = cuenta.Id.Trim();
            if (cuenta.Id.IndexOf('-') > 0)
            {
                string[] partesRut = cuenta.Id.Split('-');
                if (partesRut.Length == 2)
                {
                    partesRut[0] = partesRut[0].Replace(".", "");
                    int rut = 0;
                    if (int.TryParse(partesRut[0], out rut))
                    {
                        SalidaType salida = RegistroCivil.ObtenerNumeroSerie(rut.ToString(), partesRut[1], cuenta.NroSerie);
                        if (salida.Respuesta != null && salida.Respuesta.NumeroSerie != null)
                        {
                            Minvu.SectoresMedios.IData.ICE_RegistroCivil.ICE datosRegCivil = RegistroCivil.ObtenerDatosRegistroCivil(rut.ToString(), partesRut[1]);
                            Session["rut"] = rut.ToString();
                            Session["nombre"] = datosRegCivil.minvuRutData.persona.nombres + " " + datosRegCivil.minvuRutData.persona.apPaterno + " " + datosRegCivil.minvuRutData.persona.apMaterno;
                            return View("../Home/Index");
                        }
                        else
                        {
                            return View("ErrorLogin");
                        }
                    }
                    else
                    {
                        return View("ErrorLogin");
                    }
                }
                else
                {
                    return View("ErrorLogin");
                }
            }
            else
            {
                return View("ErrorLogin");
            }
        }

        public ActionResult CierreSesion()
        {
            Session["rut"] = null;
            Session["nombre"] = null;
            return View("../Home/Index");
        }
	}
}