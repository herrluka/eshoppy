using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Models
{
    public class Organization : IOrganization
    {
        public int Id { get; set; }
        public int Tin { get; set; }
        public String Name { get; set; }
        public String Adress { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public double AverageTransactionRate { get; set; }
        public List<IAccount> Accounts { get; set; }

        public Organization(int id, int tin, string name, string adress, string phoneNumber, string email)
        {
            Id = id;
            Tin = tin;
            Name = name;
            Adress = adress;
            PhoneNumber = phoneNumber;
            Email = email;
            AverageTransactionRate = 0;
            Accounts = new List<IAccount>();
        }
    }
}
