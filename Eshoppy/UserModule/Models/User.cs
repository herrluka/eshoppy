using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Address { get; set; }
        public List<IAccount> Accounts { get; set; }
        public List<ITransaction> Transactions { get; set; }

        public User(string name, string surname, string email, string phone, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phone;
            Address = address;
            Accounts = new List<IAccount>();
            Transactions = new List<ITransaction>();
        }

        public IAccount GetAccountByAccountNumber(int accountNumber)
        {
            foreach (IAccount account in this.Accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
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
