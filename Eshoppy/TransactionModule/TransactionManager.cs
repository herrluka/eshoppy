using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.SalesModule.Interfaces;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.TransactionModule.Models;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule
{
    public class TransactionManager : ITransactionManager
    {
        public IClientManager ClientManager { get; set; }
        private TransactionList TransactionList { get; set; }

        public TransactionManager(IClientManager clientManager, TransactionList transactionList)
        {
            this.ClientManager = clientManager;
            this.TransactionList = transactionList;
        }

        public ITransaction CreateTransaction(DateTime date, int transactionCategory, Guid buyerId, Guid sellerId, IOffer offer, double transactionPrice, ITransactionType transactionType, byte evaluation, IEmailSender emailSender)
        {
            ITransaction transaction = new Transaction(date, transactionCategory, null, null, transactionPrice, null, evaluation, 0);
            transaction.Buyer = ClientManager.GetClientById(buyerId);
            transaction.Seller = ClientManager.GetClientById(sellerId);

            transaction.TransactionPrice = offer.OfferPrice + offer.TransportPrice;
            double discount = offer.CheckDiscount(DateTime.Now);
            transaction.Discount = discount;

            IAccount accountWithEnoughMoney = null;

            foreach (IAccount account in transaction.Buyer.Accounts)
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
                    transaction.Buyer.Accounts[0].Amount += transaction.TransactionPrice * (1 - transaction.Discount);
                    transaction.TransactionCategory = 0;
                    transaction.Buyer.Transactions.Add(transaction);
                    transaction.TransactionCategory = 1;
                    transaction.Seller.Transactions.Add(transaction);
                    TransactionList.AddTransaction(transaction);
                    emailSender.SendEmail("Transaction was sucessfull", transaction.Buyer.Email);
                }
                else if ( transactionType is InstalmentsTransactionType)
                {
                    accountWithEnoughMoney.Amount -= ((InstalmentsTransactionType)transactionType).InstalmentPrice * transaction.Discount;
                    transaction.Buyer.Accounts[0].Amount += ((InstalmentsTransactionType)transactionType).InstalmentPrice * transaction.Discount;
                    transaction.TransactionCategory = 0;
                    transaction.Buyer.Transactions.Add(transaction);
                    transaction.TransactionCategory = 1;
                    transaction.Seller.Transactions.Add(transaction);
                    TransactionList.AddTransaction(transaction);
                    emailSender.SendEmail("Transaction was sucessfull", transaction.Buyer.Email);
                }
            }
            else
            {
                emailSender.SendEmail("On your accounts there is not enough money", transaction.Buyer.Email);
                return null;
            }

            return transaction;
        }

       
    }
}
