using Eshoppy.FinanceModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.FinanceModule.Models
{
    public class Bank : IBank
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public List<ICredit> CreditOffer { get; set; }

        public Bank(string name, string address, string email, string phone)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
            CreditOffer = new List<ICredit>();
        }
    }
}
