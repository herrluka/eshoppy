using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Interfaces
{
    public interface IClientManager
    {
        ShoppingClient ClientList { get; set; }
        IFinanceManager FinanceManager { get; set; }
        void RegisterUser(String name, String surname, String email, String phone, string address);
        void RegisterOrg(int tin, string name, string adress, string phoneNumber, string email);
        void ChangeUserAccount(IUser user, List<IAccount> accounts);
        void ChangeOrgAccount(IOrganization organization, List<IAccount> accounts);
        List<ITransaction> SearchHistory(IClient client, DateTime date, int transactionCategory);
        IClient GetClientById(Guid id);
        void AddFunds(IClient client, double amount, ICurrency currency, IEmailSender emailSender, ILogger logger);
    }
}
