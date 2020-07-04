﻿using Eshoppy.SalesModule.Interfaces;
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
        public double OrderPrice { get; set; }
        public double TransportPrice { get; set; }

        public Offer(List<IProduct> products, DateTime dateCreated, DateTime dateValid, List<ITransport> transports, double price, double transportPrice)
        {
            Id = new Guid();
            Products = products;
            DateCreated = dateCreated;
            DateValid = dateValid;
            AvailableTransports = transports;
            OrderPrice = price;
            TransportPrice = transportPrice;
        }
    }
}
