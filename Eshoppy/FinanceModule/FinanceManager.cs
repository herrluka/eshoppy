﻿using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.FinanceModule.Services;
using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
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

        public void AccountPayment(Guid accountId, double amount)
        {
            IAccount account = GetAccountById(accountId);
            if (account != null)
            {
                account.Amount += amount;
            }
            else
            {
                throw new Exception("Bad account id provided");
            }
        }

        public bool AskCredit(Guid userId, double amount, Guid creditId, byte numberOfYears)
        {
            
            List<IAccount> accounts = new List<IAccount>();
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
                accounts = FinanceService.GetAccounts(client);
                ICredit credit = FinanceService.GetCreditById(bankList.Banks, creditId);
                foreach (IAccount a in accounts)
                {
                    if (a.CreditAvailable && credit.Available)
                    {
                        if (credit.MinYears < numberOfYears &&
                            numberOfYears < credit.MaxYears &&
                            amount > credit.MinAmount)
                        {
                            a.Amount += amount;
                            a.CreditDebt += amount * credit.Interest;
                            FinanceService.SendEmail(client, "Credit is approved.");
                            return true;
                        }
                    }
                }
            }
            Console.Error.Write("Credit is denied");
            FinanceService.SendEmail(client, "Credit is denied");
            return false;

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

        public double Convert(ICurrency currency, double amount)
        {
            return amount * currency.MultiplyFactor;
        }

        public IAccount CreateAccount(DateTime dateValid, IBank bank)
        {
            return new Account(dateValid, bank);
        }

        public IBank CreateBank(string name, string address, string email, string phone)
        {
            return new Bank(name, address, email, phone);
        }

        public ICredit CreateCredit(double minAmount, double interest, int minYears, int maxYears, IBank bank)
        {
            return new Credit(minAmount, interest, minYears, maxYears, bank);
        }

        public void CreditPayment(Guid accountId, double amount)
        {
            IAccount account = GetAccountById(accountId);
            account.CreditDebt -= amount;
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
    }
}
