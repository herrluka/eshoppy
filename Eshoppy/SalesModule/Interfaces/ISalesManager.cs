using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Interfaces
{
    public interface ISalesManager
    {
        IProduct CreateProduct(string name, double price, double availableQuantity);
        IOffer CreateOffer(List<IProduct> products, DateTime dateCreated, DateTime dateValid, List<ITransport> transports);
        List<IOffer> GetOffersByTrasportId(Guid transportId);
        List<IOffer> GetOffersByProduct(Guid productId);
        IOffer GetLowestOffer();
        void GetTransportCost(IOffer offer, ITransport transport);
        void UpdateOffer(IOffer offer, List<IProduct> products = null, DateTime? dateCreated = null, DateTime? dateValid = null, List<ITransport> transports = null, double? price = null, double? transportPrice = null);

    }
}
