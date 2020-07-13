using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Interfaces
{
    public class Credit : ICredit
    {
        public Guid Id { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        public double Interest { get; set; }
        public int MinYears { get; set; }
        public int MaxYears { get; set; }
        public bool Available { get; set; }

        public Credit(double minAmount, double maxAmount, double interest, int minYears, int maxYears)
        {
            Id = Guid.NewGuid();
            MinAmount = minAmount;
            MaxAmount = maxAmount;
            Interest = interest;
            MinYears = minYears;
            MaxYears = maxYears;
        }
    }
}
