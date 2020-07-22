using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.TransactionModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Interfaces
{
    public interface IClient
    {
        Guid Id { get; set; }
        String Name { get; set; }
        List<IAccount> Accounts { get; set; }
        String Email { get; set; }
        String PhoneNumber { get; set; }
        String Address { get; set; }
        List<ITransaction> Transactions { get; set; }
        List<IAccount> GetAccountsWithCreditAvailable();
    }
}
