using Eshoppy.SalesModule.Interfaces;
using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Interfaces
{
    public interface ITransactionManager
    {
        ITransaction CreateTransaction(DateTime date, int transactionCategory, Guid buyerId, Guid sellerId, IOffer offer, double transactionPrice, ITransactionType transaction, byte evaluation, IEmailSender emailSender);

    }
}
