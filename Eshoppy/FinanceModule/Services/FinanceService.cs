using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Services
{
    public static class FinanceService
    {
        public static int id = 0;
        public static int getId()
        {
            id++;
            return id;
        }

        public static List<IAccount> GetAccounts(IClient client)
        {
            List<IAccount> accounts = new List<IAccount>();

            foreach (IAccount a in client.Accounts)
            {
                if (a.CreditAvailable)
                {
                    accounts.Add(a);
                }
            }
            return accounts;
        }

        public static ICredit GetCreditById(List<IBank> banks, Guid creditId)
        {
            foreach (IBank bank in banks)
            {
                foreach (ICredit credit in bank.CreditOffer)
                {
                    if (credit.Id.Equals(creditId))
                    {
                        return credit;
                    }
                }
            }
            return null;
        }

        public static void SendEmail(IClient client, string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("luka.radujevic@gmail.com");
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
