using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Models
{
    public class Account : IAccount
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime DateValid { get; set; }
        public IBank Bank { get; set; }
        public double CreditDebt { get; set; }
        public bool CreditAvailable { get; set; }
        public ICredit Credit { get; set; }

        public Account(DateTime dateValid, IBank bank, double amount)
        {
            Id = Guid.NewGuid();
            AccountNumber = Utils.Utils.getId();
            Amount = amount;
            DateValid = dateValid;
            Bank = bank;
            CreditDebt = 0;
            CreditAvailable = false;
            Credit = null;
        }

    }
}
