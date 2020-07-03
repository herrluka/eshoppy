using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Models
{
    public class EuroCurrency : Currency
    {
        public EuroCurrency(double multiplyFactor)
        {
            MultiplyFactor = multiplyFactor;
        }
    }
}
