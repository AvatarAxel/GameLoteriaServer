using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
     public class EmailStructure
    {
        public bool sendMail(string to, string subject, string body)
        {
            string from = "GameLoteriaUV@outlook.com";
            string displayName = "Game Loteria";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, displayName);
            mail.To.Add(to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587); 
            client.Credentials = new NetworkCredential(from, "G@m3_UV_L0t3ri@");
            client.EnableSsl = true;

            try
            {
                client.Send(mail);
                return true;
            }
            catch (ArgumentNullException)
            {
               return false;
            }
            catch(InvalidOperationException)
            {
                return false;
            }

            catch(SmtpFailedRecipientsException) 
            {
                return false;
            }
        }
    }
}
