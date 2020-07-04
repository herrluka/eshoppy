using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.TransactionModule.Interfaces
{
    public interface ITransactionType
    {
        Guid Id { get; set; }
        String Name { get; set; }
    }
}
