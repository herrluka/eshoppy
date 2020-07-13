using Eshoppy.FinanceModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule
{
    public class BankList
    {
        public List<IBank> Banks { get; set; }

        public BankList(List<IBank> banks)
        {
            Banks = banks;
        }

        public void AddBank(IBank bank)
        {
            Banks.Add(bank);
        }
    }
}
