using Eshoppy.TransactionModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Models
{
    public abstract class TransactionType : ITransactionType
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}
