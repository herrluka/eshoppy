using Eshoppy.FinanceModule;
using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.FinanceModule.Services;
using Eshoppy.TransactionModule;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.UserModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule
{
    public class ClientManager : IClientManager
    {
        private ShoppingClient clientList;
        private TransactionList transactionList;
        private FinanceManager financeManager;

        public ClientManager(ShoppingClient clientList, TransactionList transactionList, FinanceManager financeManager)
        {
            this.clientList = clientList;
            this.transactionList = transactionList;
            this.financeManager = financeManager;
        }

        public void RegisterUser(String name, String surname, String email, String phone, string adress)
        {
            IUser user = new User(name, surname, email, phone, adress);
            this.clientList.AddClient(user);
        }

        public void RegisterOrg(int tin, string name, string adress, string phoneNumber, string email)
        {
            IOrganization organization = new Organization(tin, name, adress, phoneNumber, email);
            this.clientList.AddClient(organization);
        }

        public void ChangeUserAccount(IUser user, List<IAccount> accounts)
        {
            foreach (IAccount userAccount in user.Accounts)
            {
                foreach (IAccount newAccount in accounts)
                if (userAccount.Id.Equals(newAccount.Id))
                    {
                        userAccount.AccountNumber = newAccount.AccountNumber;
                    }
            }
        }

        public void ChangeOrgAccount(IOrganization organization, List<IAccount> accounts)
        {
            foreach (IAccount organizationAccount in organization.Accounts)
            {
                foreach (IAccount newAccount in accounts)
                    if (organizationAccount.Id.Equals(newAccount.Id))
                    {
                        organizationAccount.AccountNumber = newAccount.AccountNumber;
                    }
            }
        }

        public List<ITransaction> SearchHistory(IClient client, DateTime date, int transactionCategory)
        {
            List<ITransaction> transactions = new List<ITransaction>();
            if (transactionCategory == 0)
            {
                foreach (ITransaction transaction in transactionList.Transactions)
                {
                    if (DateTime.Compare(transaction.TransactionDate, date) > 0 && transaction.Buyer.Equals(client))
                    {
                        transactions.Add(transaction);
                    }
                }
                return transactions;
            }
            else if (transactionCategory == 1)
            {
                foreach (ITransaction transaction in transactionList.Transactions)
                {
                    if (DateTime.Compare(transaction.TransactionDate, date) > 0 && transaction.Seler.Equals(client))
                    {
                        transactions.Add(transaction);
                    }
                }
                return transactions;
            }
            else
            {
                throw new Exception("You can only add 1 or 0 as parameter for transaction category");
            }
        }

        public IClient GetClientById(Guid id)
        {
            foreach (IClient client in this.clientList.Clients)
            {
                if (client.Id.Equals(id))
                {
                    return client;
                }
            }
            return null;
        }


        public void AddFunds(IClient client, double amount, ICurrency currency)
        {
            if (client.Accounts.Count > 0)
            {
                IAccount account = client.Accounts[0];
                if (!(currency is DinarCurrency))
                {
                    amount *= currency.MultiplyFactor;
                }

                if (client is IOrganization)
                {
                    if (amount < 10000)
                    {
                        Console.Error.Write("Amount cannot be smaller than 10000 for organizations");
                        throw new Exception("Amount cannot be smaller than 10000 for organizations");
                    }
                }

                if (account.Credit != null)
                {
                    financeManager.CreditPayment(account.Id, amount);
                    FinanceService.SendEmail(client, "Credit debt is reduced.");
                }
                else
                {
                    financeManager.AccountPayment(account.Id, amount);
                }
            }
            else
            {
                throw new Exception("This user has 0 accounts");
            }
        }
    }
}
