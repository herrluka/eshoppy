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
        int Id { get; set; }
        DateTime TransactionDate { get; set; }
        int TransactionCategory { get; set; }
        IClient Buyer { get; set; }
        IClient Seler { get; set; }
    }
}
