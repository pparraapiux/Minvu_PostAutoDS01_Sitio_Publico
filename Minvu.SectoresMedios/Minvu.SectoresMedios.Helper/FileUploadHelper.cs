using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Minvu.SectoresMedios.Helper
{
    public static class FileUploadHelper
    {
        public static void GuardarArchivo(string fullDir, HttpPostedFileBase file)
        {
            try
            {
                if(file != null)
                    file.SaveAs(fullDir);
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido realizar la carga del archivo. Por favor intente más tarde.", ex);
            }
        }

        public static void MoverArchivo(string rutaOrigen, string rutaDestino)
        {
            try
            {
                File.Move(rutaOrigen, rutaDestino);
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido mover el archivo. Por favor intente más tarde.", ex);
            }
        }

        public static string ObtenerRutaCompleta(string baseDir, string folder, Guid filename, string extension)
        {
            var directorio = Path.Combine(HostingEnvironment.MapPath(baseDir), folder);
            var fulldir = Path.Combine(directorio, filename + extension);
            Directory.CreateDirectory(directorio);
            return fulldir;
        }
    }
}
