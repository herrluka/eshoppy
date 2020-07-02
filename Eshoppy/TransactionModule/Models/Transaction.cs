using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Models
{
    public class Transaction : ITransaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionCategory { get; set; }
        public IClient Buyer { get; set; }
        public IClient Seler { get; set; }

    }
}
