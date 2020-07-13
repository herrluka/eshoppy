using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests
{
    [TestFixture]
    public class UserModuleTests
    {
        IClientManager manager;

        [SetUp]
        public void Init()
        {
            //manager = new ClientManager()
        }
        [Test]
        public void CreateUser_CheckName_Successful()
        {

        }
    }
}
