using Eshoppy.UserModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule
{
    public class ShoppingClient
    {
        public List<IClient> Clients;

        public ShoppingClient(List<IClient> list)
        {
            Clients = list;
        }

        public void AddClient(IClient client)
        {
            foreach (IClient c in Clients ){
                if (c.Id == client.Id)
                {
                    throw new Exception("There is client with same id");
                }
            }
            Clients.Add(client);
        }
    }
}

