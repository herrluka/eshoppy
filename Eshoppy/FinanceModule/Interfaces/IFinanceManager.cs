using Eshoppy.UserModule.Interfaces;
using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Interfaces
{
    public interface IFinanceManager
    {
        IAccount CreateAccount(DateTime dateValid, IBank bank, double amount);
        IBank CreateBank(string name, string address, string email, string phone);
        ICredit CreateCredit(double minAmount, double maxAmount, double interest, int minYears, int maxYears, bool available);
        IAccount GetAccountById(Guid accountId);
        bool AskCredit(Guid userId, double amount, Guid creditId, byte numberOfYears, IEmailSender emailSender, ILogger logger);
        void AccountPayment(Guid accountId, double amount);
        void CreditPayment(Guid accountId, double amount);
        double Convert(ICurrency currency, double amount);
        double? CheckBalance(Guid accountId);

    }
}
