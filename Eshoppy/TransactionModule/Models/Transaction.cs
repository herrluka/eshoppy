using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Models
{
    public class Transaction : ITransaction
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionCategory { get; set; }
        public IClient Buyer { get; set; }
        public IClient Seller { get; set; }
        public double TransactionPrice { get; set; }
        public ITransactionType TransactionType { get; set; }
        public byte TransactionEvaluation { get; set; }
        public double Discount { get; set; }

        public Transaction(DateTime transactionDate, int transactionCategory, IClient buyer, IClient seller, double transactionPrice, ITransactionType transactionType, byte transactionEvaluation, double discount)
        {
            Id = Guid.NewGuid();
            TransactionDate = transactionDate;
            TransactionCategory = transactionCategory;
            Buyer = buyer;
            Seller = seller;
            TransactionPrice = transactionPrice;
            TransactionType = transactionType;
            TransactionEvaluation = transactionEvaluation;
            Discount = discount;
        }
    }
}
