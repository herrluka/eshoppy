using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Interfaces
{
    public interface IBank
    {
        Guid Id { get; set; }
        String Name { get; set; }
        String Address { get; set; }
        String Email { get; set; }
        String Phone { get; set; }
        List<ICredit> CreditOffer { get; set; }
    }
}
