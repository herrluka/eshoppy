using Eshoppy.FinanceModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Interfaces
{
    public interface IClient
    {
        int Id { get; set; }
        List<IAccount> Accounts { get; set; }
    }
}
