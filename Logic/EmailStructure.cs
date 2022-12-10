using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Logic
{
    public class EmailStructure
    {
        public bool sendMail(string emailPlayers, string subject, string body)
        {            

            //string EMAIL = ConfigurationManager.AppSettings["EMAIL"]; ----> Manda NULL
            //string PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];

            string displayName = "Game Loteria";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Properties.Settings.Default.EMAIL, displayName);
            mail.To.Add(emailPlayers);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587); 
            client.Credentials = new NetworkCredential(Properties.Settings.Default.EMAIL, Properties.Settings.Default.PASSWORD);
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
            catch (SmtpException)
            {
                return false;

            }
        }

     }
}
