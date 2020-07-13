using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Interfaces
{
    public interface IOffer
    {
        Guid Id { get; set; }
        List<IProduct> Products { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateValid { get; set; }
        List<ITransport> AvailableTransports { get; set; }
        double OfferPrice { get; set; }
        double TransportPrice { get; set; }
        double GetPriceAsSumOfProducts(DateTime now);
        double CheckDiscount(DateTime now);
        int GetNumberOfProducts();
    }
}
