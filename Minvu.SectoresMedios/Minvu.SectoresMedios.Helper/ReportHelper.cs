using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper
{
    public class ReportHelper
    {
        private ReportViewer reporte = new ReportViewer();

        public ReportHelper(string rutaReporte, Dictionary<string,string> parametros, string rutaServidor = null, string rutaReportServer = null)
        {
            if(string.IsNullOrEmpty(rutaServidor) && string.IsNullOrEmpty(rutaReportServer))
            {
                rutaServidor = ConfigurationManager.AppSettings["RutaServidorReporte"];
                rutaReportServer = ConfigurationManager.AppSettings["URL_Report_Server"];
            }

            reporte.ServerReport.ReportServerUrl = new System.Uri(rutaServidor + rutaReportServer);
            reporte.ServerReport.ReportPath = rutaReporte;

            List<ReportParameter> listaParametros = new List<ReportParameter>();

            foreach(var param in parametros)
            {
                listaParametros.Add(new ReportParameter(param.Key, param.Value));
            }

            reporte.ServerReport.SetParameters(listaParametros);
            reporte.ServerReport.Refresh();
        }

        public byte[] ObtenerPDFBinario()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string deviceInfo = " <DeviceInfo>" +
                                    " <OutputFormat>PDF</OutputFormat>" +
                                    " <PageWidth>8.5in</PageWidth>" +
                                    "  <PageHeight>11in</PageHeight>" +
                                    "  <MarginTop>0.2in</MarginTop>" +
                                    "  <MarginLeft>0.2in</MarginLeft>" +
                                    "  <MarginRight>0.0in</MarginRight>" +
                                    "  <MarginBottom>0.0in</MarginBottom>" +
                                    "</DeviceInfo>";

            byte[] bytes = reporte.ServerReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
            return bytes;
        }
    }
}
