using Eshoppy.SalesModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Models
{
    public class Transport : ITransport
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public double TransportCoefficient { get; set; }

        public Transport(string name, double transportCoefficient)
        {
            Id = Guid.NewGuid();
            Name = name;
            TransportCoefficient = transportCoefficient;
        }
    }
}
