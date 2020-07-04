using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Services;
using Eshoppy.SalesModule.Interfaces;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.TransactionModule.Models;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule
{
    public class TransactionManager : ITransactionManager
    {
        private IClientManager clientManager;
        private TransactionList transactionList;

        public TransactionManager(IClientManager clientManager, TransactionList transactionList)
        {
            this.clientManager = clientManager;
            this.transactionList = transactionList;
        }

        public ITransaction CreateTransaction(Guid buyerId, Guid selerId, IOffer offer, ITransactionType transactionType, byte evaluation)
        {
            ITransaction transaction = new Transaction();
            transaction.Id = new Guid();
            IClient buyer = clientManager.GetClientById(buyerId);
            IClient seler = clientManager.GetClientById(selerId);

            transaction.TransactionDate = new DateTime();
            transaction.TransactionEvaluation = evaluation;
            transaction.TransactionPrice = offer.OrderPrice + offer.TransportPrice;
            double discount = offer.CheckDiscount();
            transaction.Discount = discount;

            IAccount accountWithEnoughMoney = null;

            foreach (IAccount account in buyer.Accounts)
            {
                if (account.Amount > transaction.TransactionPrice)
                {
                    accountWithEnoughMoney = account;
                    break;
                }
            }

            if (accountWithEnoughMoney != null)
            {
                if (transactionType is WithoutInstalmentsTransactionType)
                {
                    accountWithEnoughMoney.Amount -= transaction.TransactionPrice * (1 - transaction.Discount);
                    buyer.Accounts[0].Amount += transaction.TransactionPrice * (1 - transaction.Discount);
                    transactionList.Transactions.Add(transaction);
                    transaction.TransactionCategory = 0;
                    buyer.Transactions.Add(transaction);
                    transaction.TransactionCategory = 1;
                    seler.Transactions.Add(transaction);
                    FinanceService.SendEmail(buyer, "Transaction was sucessfull");
                }
                else if ( transactionType is InstalmentsTransactionType)
                {
                    accountWithEnoughMoney.Amount -= ((InstalmentsTransactionType)transactionType).InstalmentPrice * transaction.Discount;
                    buyer.Accounts[0].Amount += ((InstalmentsTransactionType)transactionType).InstalmentPrice * transaction.Discount;
                    transactionList.Transactions.Add(transaction);
                    transaction.TransactionCategory = 0;
                    buyer.Transactions.Add(transaction);
                    transaction.TransactionCategory = 1;
                    seler.Transactions.Add(transaction);
                    FinanceService.SendEmail(buyer, "Transaction was sucessfull");
                }
            }
            else
            {
                FinanceService.SendEmail(buyer, "On your accounts there is not enough money");
                return null;
            }

            return transaction;
        }

       
    }
}
