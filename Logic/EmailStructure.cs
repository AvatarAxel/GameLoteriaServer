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
        public string sendMail(string to, string subject, string body)
        {
            string message = "Error sending this email. Please verify the data or try again later";
            string from = "GameLoteriaUV@outlook.com";
            string displayName = "Game Loteria";

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.office365.com", 587); 
                client.Credentials = new NetworkCredential(from, "G@m3_UV_L0t3ri@");
                client.EnableSsl = true;

                client.Send(mail);

            }
            catch (Exception ex)
            {
                message = ex.Message + "Please verify your internet connection and that your data is correct and try again.";
            }

            return message;
        }
    }
}
