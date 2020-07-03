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
        Guid Id { get; set; }
        List<IAccount> Accounts { get; set; }
        String Email { get; set; }
        String Phone { get; set; }
        String Address { get; set; }
    }
}
