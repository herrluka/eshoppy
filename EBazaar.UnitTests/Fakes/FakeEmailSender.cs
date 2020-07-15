using Eshoppy.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests.Fakes
{
    public class FakeEmailSender : IEmailSender
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public void SendEmail(string message, string email)
        {
            this.Message = message;
            this.Email = email;
        }
    }
}
