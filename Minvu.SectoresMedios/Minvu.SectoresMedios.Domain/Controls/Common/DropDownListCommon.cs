using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Minvu.SectoresMedios.IData.DAO;
using Minvu.SectoresMedios.IData.ORM;
using Minvu.SectoresMedios.Helper;

namespace Minvu.SectoresMedios.Domain.Controls.Common
{
    public static class DropDownListCommon
    {
        public static List<SelectListItem> LlenarDDLPorDictionary(Dictionary<string, string> o)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "-- Seleccione --", Value = "" });
            foreach(var x in o)
            {
                lista.Add(new SelectListItem { Text = x.Value, Value = x.Key });
            }
            lista = lista.OrderBy(x => x.Text).ToList();
            return lista;
        }

        public static List<SelectListItem> LlenarDDLPorDictionary(Dictionary<string, object> o)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "-- Seleccione --", Value = "" });
            foreach (var x in o)
            {
                lista.Add(new SelectListItem { Text = TypeHelper.ConvertObjectToString(x.Value), Value = x.Key });
            }
            lista = lista.OrderBy(x => x.Text).ToList();
            return lista;
        }

        public static List<SelectListItem> DDLVacio()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Text = "-- Seleccione --", Value = "" });
            return lista;
        }

        public static List<SelectListItem> ObtenerRegionesDDL()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            var listaPA = RegionDAO.obtenerRegiones();
            lista.Add(new SelectListItem { Text = "-- Seleccione --", Value = "" });

            foreach (var _region in listaPA)
            {
                lista.Add(new SelectListItem
                {
                    Text = _region.VALUE,
                    Value = _region.KEY.ToString()
                });
            }
            return lista;
        }

        public static List<SelectListItem> ObtenerComunasDDL(int regId, int prvId)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            List<RUKAN_MIGRA_usp_con_Comuna_Result> listaPA = ComunaDAO.obtenerComunas(regId, prvId);
            lista.Add(new SelectListItem { Text = "-- Seleccione --", Value = "" });

            foreach (RUKAN_MIGRA_usp_con_Comuna_Result _comuna in listaPA)
            {
                lista.Add(new SelectListItem
                {
                    Text = _comuna.COM_DES,
                    Value = _comuna.COM_ID.ToString()
                });
            }
            return lista;
        }

        public static List<SelectListItem> ObtenerDDLPorDominio(string domIdDom)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            List<RUKAN_MIGRA_usp_con_DOMINIO_Result> dominio = DominioDAO.obtenerValoresPorDominio(domIdDom);
            lista.Add(new SelectListItem { Text = "-- Seleccione --", Value = "" });
            foreach (var _dom in dominio)
            {
                lista.Add(new SelectListItem
                {
                    Text = _dom.VALUE,
                    Value = _dom.KEY
                });
            }
            lista = lista.OrderBy(x => x.Text).ToList();
            return lista;
        }

        public static IEnumerable<SelectListItem> CrearListaDDLPorDictionary(Dictionary<string, object> d)
        {
            d.Add("", "-- Seleccione --");
            d.OrderBy(x => int.Parse(x.Key));
            foreach(var item in d)
            {
                yield return new SelectListItem { Text = item.Value.ToString(), Value = item.Key };
            }
        }
    }
}
