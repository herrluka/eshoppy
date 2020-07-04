using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.SalesModule.Interfaces
{
    public interface ITransport
    {
        Guid Id { get; set; }
        String Name { get; set; }
        double TransportCoefficient { get; set; }
    }
}
