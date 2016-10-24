using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Minvu.SectoresMedios.Helper
{
    public class Email
    {
        public static bool EnviarCorreoFinProceso(string idSolicitud, string asunto,
                                string destinatario, string destinatariocc, string Mensaje, string NombrePostulante, string path, string Comprobante)
        {
            try
            {
                Log.Instance.Info("Inicio Email.EnviarCorreoFinProceso idSolicitud -> " + idSolicitud);

                MailMessage sMensaje = new MailMessage();
                sMensaje.From = new MailAddress("postulacionenlinea@minvu.cl", "Ministerio de Vivienda y Urbanismo");
                sMensaje.To.Add(destinatario);
                sMensaje.Subject = asunto;
                sMensaje.IsBodyHtml = false;
                if (!String.IsNullOrEmpty(destinatariocc))
                    sMensaje.CC.Add(destinatariocc);
                Attachment Data = new Attachment(path + @"\Certificados\" + Comprobante, MediaTypeNames.Application.Pdf);
                sMensaje.Attachments.Add(Data);
                string cuerpoMensaje = Mensaje;
                string correo = String.Empty;
                correo = string.Format(cuerpoMensaje, NombrePostulante);

                AlternateView html = AlternateView.CreateAlternateViewFromString(correo, Encoding.UTF8, "text/html");
                LinkedResource logo = new LinkedResource(path + "/Content/Images/logo_reserva_horas.jpg"),
                         logofooter = new LinkedResource(path + "/Content/Images/footer_reserva_horas.jpg");

                logo.ContentId = "Minvu_logo";
                logofooter.ContentId = "Minvu_logo_footer";

                html.LinkedResources.Add(logo);
                html.LinkedResources.Add(logofooter);
                sMensaje.AlternateViews.Add(html);

                var smtp = new SmtpClient
                {
                    Host = "TAURUS",
                };

                smtp.Send(sMensaje);
                return true;               


            }
            catch (Exception ex)
            {
                Log.Instance.Error("Error en Email.EnviarCorreoFinProceso idSolicitud -> " + idSolicitud, ex);
                return false;
            }
        }


    }
}
