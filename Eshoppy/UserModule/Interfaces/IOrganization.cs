using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Interfaces
{
    public interface IOrganization : IClient
    {
        int Tin { get; set; }
        double AverageTransactionRate { get; set; }
    }
}
