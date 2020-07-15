using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.Utils
{
    public static class Utils
    {
        public static int id = 0;

        public static int getId()
        {
            id++;
            return id;
        }
    }
}
