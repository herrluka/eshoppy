using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests.Fakes
{
    public class FakeLogger : ILogger
    {
        public string ErrorMessage { get; set; }
        public void ErrorLogg(string message)
        {
            this.ErrorMessage = message;
        }

    }
}
