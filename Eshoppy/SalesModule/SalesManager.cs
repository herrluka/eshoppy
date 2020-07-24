using Eshoppy.SalesModule.Interfaces;
using Eshoppy.SalesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule
{
    public class SalesManager : ISalesManager
    {
        public ListOffers Offers { get; set; }

        public SalesManager(ListOffers offers)
        {
            this.Offers = offers;
        }

        public IOffer CreateOffer(List<IProduct> products, DateTime dateCreated, DateTime dateValid, List<ITransport> transports)
        {
            IOffer offer = new Offer(products, dateCreated, dateValid, transports);
            offer.OfferPrice = offer.GetPriceAsSumOfProducts(DateTime.Now);
            Offers.Offers.Add(offer);
            return offer;
        }

        public IProduct CreateProduct(string name, double price, double availableQuantity)
        {
            return new Product(name, price, availableQuantity);
        }

        public IOffer GetLowestOffer()
        {
            return Offers.Offers.OrderBy(o => o.OfferPrice).First();
        }

        public List<IOffer> GetOffersByProduct(Guid productId)
        {
            List<IOffer> returnOffers = new List<IOffer>();

            foreach (IOffer offer in Offers.Offers)
            {
                foreach (IProduct product in offer.Products)
                {
                    if (product.Id.Equals(productId))
                    {
                        returnOffers.Add(offer);
                        break;
                    }
                }
            }

            return returnOffers;
        }

        public List<IOffer> GetOffersByTrasportId(Guid transportId)
        {
            List<IOffer> returnOffers = new List<IOffer>();
            foreach (IOffer offer in Offers.Offers)
            {
                foreach (ITransport transport in offer.AvailableTransports)
                {
                    if (transport.Id.Equals(transportId))
                    {
                        returnOffers.Add(offer);
                        break;
                    }
                }
            }
            return returnOffers;
        }

        public void GetTransportCost(IOffer offer, ITransport transport)
        {
            offer.TransportPrice = transport.TransportCoefficient * offer.OfferPrice;
        }

        public void UpdateOffer(IOffer offer, List<IProduct> products = null, DateTime? dateCreated = null, DateTime? dateValid = null, List<ITransport> transports = null, double? price = null, double? transportPrice = null)
        {
            if (products != null)
            {
                offer.Products = products;
            }

            if (dateCreated != null)
            {
                offer.DateCreated = (DateTime)dateCreated;
            }

            if (dateValid != null)
            {
                offer.DateValid = (DateTime)dateValid;
            }

            if (transports != null)
            {
                offer.AvailableTransports = transports;
            }

            if (price != null)
            {
                offer.OfferPrice = (double)price;
            }

            if (transportPrice != null)
            {
                offer.TransportPrice = (double)transportPrice;
            }

        }
    }
}
