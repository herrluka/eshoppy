using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Interfaces
{
    public interface IFinanceManager
    {
        void CreateAccount();
        void CreateBank();
        void CreateCredit();
        void GetAccountById();
        void AskCredit();
        void AccountPayment();
        void CreditPayment();
        void Convert();
        void CheckBalance();

    }
}
