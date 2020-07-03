using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Models
{
    public class DolarCurrency : Currency
    {
        public DolarCurrency(double multiplyFactor)
        {
            MultiplyFactor = multiplyFactor;
        }
    }
}
