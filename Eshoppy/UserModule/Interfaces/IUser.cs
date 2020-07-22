using Eshoppy.FinanceModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Interfaces
{
    public interface IUser : IClient
    {
        String Surname { get; set; }
        IAccount GetAccountByAccountNumber(int accountNumber);
    }
}
