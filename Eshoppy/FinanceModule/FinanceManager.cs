using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule
{
    public class FinanceManager : IFinanceManager
    {
        private ShoppingClient clientList;
        private BankList bankList;

        public FinanceManager(ShoppingClient clientList, BankList bankList)
        {
            this.clientList = clientList;
            this.bankList = bankList;
        }

        public IAccount CreateAccount(DateTime dateValid, IBank bank, double amount)
        {
            return new Account(dateValid, bank, amount);
        }

        public IBank CreateBank(string name, string address, string email, string phone)
        {
            return new Bank(name, address, email, phone);
        }

        public ICredit CreateCredit(double minAmount, double maxAmount, double interest, int minYears, int maxYears)
        {
            return new Credit(minAmount, maxAmount, interest, minYears, maxYears);
        }

        public IAccount GetAccountById(Guid accountId)
        {
            foreach (IClient client in clientList.Clients)
            {
                foreach (IAccount account in client.Accounts)
                {
                    if (account.Id == accountId)
                    {
                        return account;
                    }
                }
            }
            return null;
        }

        public bool AskCredit(Guid userId, double amount, Guid creditId, byte numberOfYears)
        {

            List<IAccount> accounts;
            IClient client = null;

            foreach (IClient c in this.clientList.Clients)
            {
                if (c.Id.Equals(userId))
                {
                    client = c;
                    break;
                }
            }

            if (client != null)
            {
                accounts = client.GetAccountsWithCreditAvailable();

                if (accounts.Count == 0)
                {
                    throw new NullReferenceException("There is not available credit");
                }

                ICredit credit = GetCreditById(creditId);
                foreach (IAccount a in accounts)
                {
                    if (credit.Available)
                    {
                        if (numberOfYears > credit.MinYears &&
                            numberOfYears < credit.MaxYears &&
                            amount > credit.MinAmount &&
                            amount < credit.MaxAmount)
                        {
                            a.Amount += amount;
                            a.CreditDebt += amount * credit.Interest;
                            //Utils.Utils.SendEmail(client, "Credit is approved.");
                            return true;
                        }
                        else
                        {
                            Console.Error.Write("Conditions not fulfilled");
                            return false;
                        }
                    }
                    else
                    {
                        Console.Error.Write("Credit is not available");
                        return false;
                    }
                }
            }
            else 
            {
                throw new NullReferenceException("Bad client id");
            }
            Console.Error.Write("Credit is denied");
            Utils.Utils.SendEmail(client, "Credit is denied");
            return false;

        }

        public void AccountPayment(Guid accountId, double amount)
        {
            IAccount account = GetAccountById(accountId);
            if (account != null)
            {
                account.Amount += amount;
            }
            else
            {
                throw new ArgumentNullException("Bad account id provided");
            }
        }

        public void CreditPayment(Guid accountId, double amount)
        {
            IAccount account = GetAccountById(accountId);
            if(account != null)
            {
                account.CreditDebt -= amount;
            }
            else
            {
                throw new NullReferenceException("Account not found");
            }
        }

         public double Convert(ICurrency currency, double amount)
        {
            return amount * currency.MultiplyFactor;
        }

        public double? CheckBalance(Guid accountID)
        {
            foreach (IClient client in clientList.Clients)
            {
                foreach (IAccount account in client.Accounts)
                {
                    if (account.Id == accountID)
                    {
                        return account.Amount;
                    }
                }
            }
            return null;
        }

        public ICredit GetCreditById(Guid creditId)
        {
            foreach (IBank bank in bankList.Banks)
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
    }
}
