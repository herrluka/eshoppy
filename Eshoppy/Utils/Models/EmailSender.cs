using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.Utils.Models
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string message, string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Properties.Settings.Default.username);
            mail.To.Add(email);
            mail.Subject = "Test Mail";
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.username, Properties.Settings.Default.password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
