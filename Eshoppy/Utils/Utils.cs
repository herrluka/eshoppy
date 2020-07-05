using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.Utils
{
    public static class Utils
    {
        public static int id = 0;

        public static int getId()
        {
            id++;
            return id;
        }

        public static void SendEmail(IClient client, string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("luka036test@gmail.com");
            mail.To.Add(client.Email);
            mail.Subject = "Test Mail";
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.username, Properties.Settings.Default.password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
