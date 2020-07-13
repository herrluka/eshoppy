using Eshoppy.SalesModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Models
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public double Price{ get; set; }
        public double AvailableQuantity { get; set; }

        public Product(string name, double price, double availableQuantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            AvailableQuantity = availableQuantity;
        }
    }
}
