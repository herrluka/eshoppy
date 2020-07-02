using Eshoppy.FinanceModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Models
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
    }
}
