using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.UserModule.Models;
using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests.Fakes
{
    public class FakeClientManager : IClientManager
    {
        public ShoppingClient ClientList { get; set; }
        public IFinanceManager FinanceManager { get; set; }

        public void AddFunds(IClient client, double amount, ICurrency currency, IEmailSender emailSender, ILogger logger)
        {
            throw new NotImplementedException();
        }

        public void ChangeOrgAccount(IOrganization organization, List<IAccount> accounts)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserAccount(IUser user, List<IAccount> accounts)
        {
            throw new NotImplementedException();
        }

        public IClient GetClientById(Guid id)
        {
            IClient client = new User("Usr", "SurUsr", "usr@usr.com", "111-111", "addr");
            client.Accounts.Add(new Account(DateTime.Now, null, 50));
            return client;
        }

        public void RegisterOrg(int tin, string name, string adress, string phoneNumber, string email)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(string name, string surname, string email, string phone, string address)
        {
            throw new NotImplementedException();
        }

        public List<ITransaction> SearchHistory(IClient client, DateTime date, int transactionCategory)
        {
            throw new NotImplementedException();
        }
    }
}
