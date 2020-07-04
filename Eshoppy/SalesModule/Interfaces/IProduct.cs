using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Interfaces
{
    public interface IProduct
    {
        Guid Id { get; set; }
        String Name { get; set; }
        double Price { get; set; }
        double AvailableQuantity { get; set; }
    }
}
