﻿using EBazaar.UnitTests.Fakes;
using Eshoppy.FinanceModule;
using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.UserModule.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests
{
    [TestFixture]
    public class UserModuleNSubstituteTests
    {

        IClientManager clientManager;

        [SetUp]
        public void Init()
        {
            //Clients
            IClient client1 = new Organization(111, "FTN", "Adress1", "111-111", "ftn@ns.com");
            client1.Id = new Guid("00000000-0000-0000-0000-400000000001");
            IClient client2 = new Organization(111, "Bambi", "Adress2", "222-222", "bambi@rs.com");
            client2.Id = new Guid("00000000-0000-0000-0000-400000000002");
            IClient client3 = new User("user1", "suruser1", "user1@ns.com", "333-333", "Adress1");
            IClient client4 = new User("user2", "suruser2", "user2@ns.com", "444-444", "Adress2");

            List<IClient> list = new List<IClient>() { client1, client2, client3, client4 };
            ShoppingClient clientList = new ShoppingClient(list);

            var financeManager = Substitute.For<IFinanceManager>();
            clientManager = new ClientManager(clientList, financeManager);
        }

        [Test]
        public void AddFunds_CreditPaymentReceivedCall_Successful()
        {

            var client = clientManager.GetClientById(new Guid("00000000-0000-0000-0000-400000000002"));
            var account = new Account(DateTime.Now, new Bank("bnk", "addr", "mail@mail.com", "111-111"), 22);
            account.Credit = new Credit(200, 500, 0.15, 2, 7, true);
            client.Accounts.Add(account);
            var logger = new FakeLogger();
            var emailSender = new FakeEmailSender();

            clientManager.AddFunds(client, 20000, new DinarCurrency(), emailSender, logger);

            clientManager.FinanceManager.Received().CreditPayment(account.Id, 20000);

        }

        [Test]
        public void AddFunds_ConvertReceivedCall_Successful()
        {

            var client = clientManager.GetClientById(new Guid("00000000-0000-0000-0000-400000000002"));
            var account = new Account(DateTime.Now, new Bank("bnk", "addr", "mail@mail.com", "111-111"), 22);
            account.Credit = new Credit(200, 500, 0.15, 2, 7, true);
            client.Accounts.Add(account);
            var logger = new FakeLogger();
            var emailSender = new FakeEmailSender();
            var currency = new DolarCurrency(1.05);

            clientManager.AddFunds(client, 20000, currency, emailSender, logger);

            clientManager.FinanceManager.Received().Convert(currency, 20000);

        }

        [Test]
        public void AddFunds_AccountPaymentReceivedCall_Successful()
        {
            var client = new Organization(33, "Org", "Bulevar", "111-111", "org@org.org");
            var account = new Account(DateTime.Now, new Bank("bnk", "addr", "mail@mail.com", "111-111"), 22);
            client.Accounts.Add(account);
            var logger = new FakeLogger();
            var emailSender = new FakeEmailSender();

            clientManager.AddFunds(client, 20000, new DinarCurrency(), emailSender, logger);

            clientManager.FinanceManager.Received().AccountPayment(account.Id, 20000);
        }
    }
}
