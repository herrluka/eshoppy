using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Interfaces
{
    public interface IAccount
    {
        Guid Id { get; set; }
        int AccountNumber { get; set; }
        double Amount { get; set; }
        DateTime DateValid { get; set; }
        IBank Bank { get; set; }
        double CreditDebt { get; set; }
        bool CreditAvailable { get; set; }
        ICredit Credit { get; set; }
        
    }
}
