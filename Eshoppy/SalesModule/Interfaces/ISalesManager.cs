using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Interfaces
{
    public interface ISalesManager
    {
        void CreateProduct();
        void CreateOffer();
        void GetOffersByTrasportId();
        void GetOffersByProduct();
        void GetLowestOffer();
        void GetTransportCost();
        void UpdateOffer();

    }
}
