using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Adress { get; set; }
        public List<IAccount> Accounts { get; set; }

        public User(int id, string name, string surname, string email, string phone, string adress)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Adress = adress;
            Accounts = new List<IAccount>();
        }
    }
}
