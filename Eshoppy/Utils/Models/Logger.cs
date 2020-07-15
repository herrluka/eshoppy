using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.Utils.Models
{
    public class Logger : ILogger
    {
        public void ErrorLogg(string message)
        {
            Console.WriteLine(message);
        }

    }
}
