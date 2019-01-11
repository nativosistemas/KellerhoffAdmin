using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace SitioBase.clases
{
    /// <summary>
    /// Summary description for cMail
    /// </summary>
    public class cMail
    {
        public cMail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool enviarMail(string pCorreoMail, string pAsunto, string pCuerpo) 
        {
            bool resultado = true;
            try
            {
                String mail = System.Configuration.ConfigurationManager.AppSettings["mailRegistracion"].ToString();
                String mail_from = System.Configuration.ConfigurationManager.AppSettings["mail_from"].ToString();
                String mail_pass = System.Configuration.ConfigurationManager.AppSettings["mail_pass"].ToString();
                //SmtpClient smtp = new System.Net.Mail.SmtpClient();

                MailMessage correo = new System.Net.Mail.MailMessage();
                string asunto = pAsunto;
                correo.From = new MailAddress(mail_from);
                correo.To.Add(pCorreoMail);
                correo.Subject = asunto;
                correo.Body = pCuerpo;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;


                SmtpClient smtp = new System.Net.Mail.SmtpClient("186.153.136.19", 25);

                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(mail_from, mail_pass);
                //   smtp.EnableSsl = true;

                smtp.Send(correo);
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }
        //public static bool enviarMail_viejo(string pFrom, string pCorreoMail, string pAsunto, string pCuerpo)
        //{
        //    bool resultado = true;
        //    SmtpClient smtpClient = new SmtpClient();

        //    MailMessage m = new MailMessage(pFrom, // From
        //        pCorreoMail, // To
        //        pAsunto, // Subject
        //       pCuerpo); // Body
        //    m.IsBodyHtml = true;
        //    try
        //    {
        //        smtpClient.Send(m);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //    }
        //    return resultado;

        //}
    }
}