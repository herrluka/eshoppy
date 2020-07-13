using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Interfaces
{
    public interface ICredit
    {
        Guid Id { get; set; }
        double MinAmount { get; set; }
        double MaxAmount { get; set; }
        double Interest { get; set; }
        int MinYears { get; set; }
        int MaxYears { get; set; }
        bool Available { get; set; }
    }
}
