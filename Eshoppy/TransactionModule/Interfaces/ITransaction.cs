using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Interfaces
{
    public interface ITransaction
    {
        Guid Id { get; set; }
        DateTime TransactionDate { get; set; }
        int TransactionCategory { get; set; }
        IClient Buyer { get; set; }
        IClient Seller { get; set; }
        double TransactionPrice { get; set; }
        ITransactionType TransactionType { get; set; }
        byte TransactionEvaluation { get; set; }
        double Discount { get; set; }
    }
}
