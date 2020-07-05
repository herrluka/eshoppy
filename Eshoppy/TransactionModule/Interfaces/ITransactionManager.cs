using Eshoppy.SalesModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Interfaces
{
    public interface ITransactionManager
    {
        ITransaction CreateTransaction(Guid buyerId, Guid sellerId, IOffer offer, ITransactionType transaction, byte evaluation);

    }
}
