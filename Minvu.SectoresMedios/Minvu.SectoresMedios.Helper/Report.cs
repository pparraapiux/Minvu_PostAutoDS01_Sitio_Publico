using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper
{
    public class Report
    {
        private ReportViewer reporte = new ReportViewer();
        public struct parametro
        {
            public parametro(string N, string V)
            {
                nombre = N;
                valor = V;
            }

            private string nombre;

            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }
            private string valor;

            public string Valor
            {
                get { return valor; }
                set { valor = value; }
            }


        }


        public Report(String ServidorReporte, String ServerUrl, String ReportPath, parametro[] Parametros)
        {
            reporte.ServerReport.ReportServerUrl = new System.Uri(String.Concat(ServidorReporte, ServerUrl));
            reporte.ServerReport.ReportPath = ReportPath;



            List<ReportParameter> parametros = new List<ReportParameter>();

            foreach (parametro p in Parametros)
            {
                //Logger.Escribir.Debug(String.Format("AddParametro : {0}=>{1}", p.Nombre, p.Valor));
                parametros.Add(new ReportParameter(p.Nombre, p.Valor));

            }
            reporte.ServerReport.SetParameters(parametros);
            reporte.ServerReport.Refresh();

        }

        public void DescagarPDF(string nombreArchivo)
        {
            //Logger.Escribir.Debug("DescagarPDF");

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string deviceInfo = " <DeviceInfo>" +
                                " <OutputFormat>PDF</OutputFormat>" +
                                " <PageWidth>9.8in</PageWidth>" +
                                "  <PageHeight>11.5in</PageHeight>" +
                                "  <MarginTop>0.0in</MarginTop>" +
                                "  <MarginLeft>1.3in</MarginLeft>" +
                                "  <MarginRight>0.0in</MarginRight>" +
                                "  <MarginBottom>0.in</MarginBottom>" +
                                "</DeviceInfo>";

            byte[] bytes = reporte.ServerReport.Render("PDF",
                                                        deviceInfo,
                                                        out mimeType,
                                                        out encoding,
                                                        out extension,
                                                        out streamids,
                                                        out warnings);


            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = mimeType;
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivo + "." + extension);
            System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
            System.Web.HttpContext.Current.Response.End();
        }

        public void AbrirPDF(string nombreArchivo)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string deviceInfo = " <DeviceInfo>" +
                                    " <OutputFormat>PDF</OutputFormat>" +
                                    " <PageWidth>9.8in</PageWidth>" +
                                    "  <PageHeight>11.5in</PageHeight>" +
                                    "  <MarginTop>0.0in</MarginTop>" +
                                    "  <MarginLeft>0.5in</MarginLeft>" +
                                    "  <MarginRight>0.5in</MarginRight>" +
                                    "  <MarginBottom>0.0in</MarginBottom>" +
                                    "</DeviceInfo>";

            byte[] bytes = reporte.ServerReport.Render("PDF",
                                                        deviceInfo,
                                                        out mimeType,
                                                        out encoding,
                                                        out extension,
                                                        out streamids,
                                                        out warnings);


            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = mimeType;
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "inline; filename=" + nombreArchivo + "." + extension);
            System.Web.HttpContext.Current.Response.BinaryWrite(bytes);

            //System.Web.HttpContext.Current.Response.End();



        }

        public byte[] ObtenerBytesPDF()
        {

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string deviceInfo = " <DeviceInfo>" +
                                    " <OutputFormat>PDF</OutputFormat>" +
                                    " <PageWidth>9.8in</PageWidth>" +
                                    "  <PageHeight>11.5in</PageHeight>" +
                                    "  <MarginTop>0.0in</MarginTop>" +
                                    "  <MarginLeft>0.5in</MarginLeft>" +
                                    "  <MarginRight>0.5in</MarginRight>" +
                                    "  <MarginBottom>0.0in</MarginBottom>" +
                                    "</DeviceInfo>";

            byte[] bytes = reporte.ServerReport.Render("PDF",
                                                        deviceInfo,
                                                        out mimeType,
                                                        out encoding,
                                                        out extension,
                                                        out streamids,
                                                        out warnings);



            return bytes;
        }
    }
}
