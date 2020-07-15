using Eshoppy.SalesModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Models
{
    public class Offer : IOffer
    {
        public Guid Id { get; set; }
        public List<IProduct> Products { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateValid { get; set; }
        public List<ITransport> AvailableTransports { get; set; }
        public double OfferPrice { get; set; }
        public double TransportPrice { get; set; }

        public Offer(List<IProduct> products, DateTime dateCreated, DateTime dateValid, List<ITransport> transports)
        {
            Id = Guid.NewGuid();
            Products = products;
            DateCreated = dateCreated;
            DateValid = dateValid;
            AvailableTransports = transports;
        }

        public double GetPriceAsSumOfProducts(DateTime now)
        {
            double sum = 0;

            foreach (IProduct prod in Products)
            {
                sum += prod.Price;
            }

            double discount = CheckDiscount(now);

            if (discount != 0)
            {
                sum *= (1 - discount);
            }
            return sum;
        }

        public double CheckDiscount(DateTime now)
        {
            double discount = 0;
            if ((now - this.DateCreated).TotalDays > 60)
            {
                discount += 0.12;
            }

            if (now.Month == 12 || now.Month == 1)
            {
                discount += 0.05;
            }

            if (GetNumberOfProducts() > 3)
            {
                discount += 0.05;
            }

            return Math.Round(discount, 2);
        }

        public int GetNumberOfProducts()
        {
            return Products.Count;
        }

    }     
}
