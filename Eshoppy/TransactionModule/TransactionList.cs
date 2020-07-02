using Eshoppy.TransactionModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule
{
    public class TransactionList
    {
        public List<ITransaction> transactions { get; set; }
    }
}
