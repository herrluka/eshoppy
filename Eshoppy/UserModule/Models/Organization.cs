using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Models
{
    public class Organization : IOrganization
    {
        public Guid Id { get; set; }
        public int Tin { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public double AverageTransactionRate { get; set; }
        public List<IAccount> Accounts { get; set; }
        public List<ITransaction> Transactions { get; set ; }

        public Organization(int tin, string name, string address, string phoneNumber, string email)
        {
            Id = Guid.NewGuid();
            Tin = tin;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            AverageTransactionRate = 0;
            Accounts = new List<IAccount>();
            Transactions = new List<ITransaction>();
        }

        public List<IAccount> GetAccountsWithCreditAvailable()
        {
            List<IAccount> accounts = new List<IAccount>();

            foreach (IAccount a in Accounts)
            {
                if (a.CreditAvailable)
                {
                    accounts.Add(a);
                }
            }
            return accounts;
        }
    }
}
