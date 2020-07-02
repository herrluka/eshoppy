using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Services
{
    public static class UserService
    {
        public static IAccount GetAccountByAccountNumber(IClient client, int accountNumber)
        {
            foreach (IAccount account in client.Accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }
}
