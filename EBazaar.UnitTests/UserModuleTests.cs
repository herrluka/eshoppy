﻿using Eshoppy.FinanceModule;
using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.TransactionModule.Models;
using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.UserModule.Models;
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
        IClientManager userManager;

        [SetUp]
        public void Init()
        {
            //Clients
            IClient client1 = new Organization(111, "FTN", "Adress1", "111-111", "ftn@ns.com");
            client1.Id = new Guid("00000000-0000-0000-0000-400000000001");
            IClient client2 = new Organization(111, "Bambi", "Adress2", "222-222", "bambi@rs.com");
            IClient client3 = new User("user1", "suruser1", "user1@ns.com", "333-333", "Adress1");
            IClient client4 = new User("user2", "suruser2", "user2@ns.com", "444-444", "Adress2");

            List<IClient> list = new List<IClient>() { client1, client2, client3, client4 };
            ShoppingClient clientList = new ShoppingClient(list);

            //Credits
            ICredit credit1 = new Credit(400, 500, 1.5, 2, 6);
            credit1.Id = new Guid("00000000-0000-0000-0000-500000000001");
            credit1.Available = false;
            ICredit credit2 = new Credit(300, 500, 2.8, 1, 2);
            credit2.Id = new Guid("00000000-0000-0000-0000-500000000002");
            ICredit credit3 = new Credit(200, 600, 1.6, 1, 3);
            credit3.Id = new Guid("00000000-0000-0000-0000-500000000003");
            ICredit credit4 = new Credit(100, 600, 1.7, 5, 6);
            credit4.Id = new Guid("00000000-0000-0000-0000-500000000004");

            //Banks
            IBank bank1 = new Bank("bank1", "addr1", "bank1@bank.rs", "555-555");
            bank1.CreditOffer = new List<ICredit>() { credit1, credit2 };
            IBank bank2 = new Bank("bank2", "addr2", "bank2@bank.rs", "666-666");
            bank2.CreditOffer = new List<ICredit>() { credit3 };
            IBank bank3 = new Bank("bank3", "addr3", "bank3@bank.rs", "777-777");
            bank3.CreditOffer = new List<ICredit>() { credit4 };

            List<IBank> banks = new List<IBank>() { bank1, bank2, bank3 };
            BankList bankList = new BankList(banks);

            var financeManager = new FinanceManager(clientList, bankList);
            userManager = new ClientManager(clientList, financeManager);
        }

        [Test]
        public void RegisterUser_CheckNumberOfUsers_Successful()
        {
            userManager.RegisterUser("user", "surUser", "usr@email.com", "111-111", "address");

            var expectedNumber = 5;

            Assert.AreEqual(expectedNumber, ((ClientManager)userManager).GetShoppingClient().Clients.Count);
        }

        [Test]
        public void RegisterUser_CheckNewUserName_Successful()
        {
            userManager.RegisterUser("user", "surUser", "usr@email.com", "111-111", "address");

            var expectedName = "user";

            var client = ((ClientManager)userManager).GetShoppingClient().Clients.Last();

            Assert.AreEqual(expectedName, ((User)client).Name);
        }

        [Test]
        public void RegisterOrg_CheckNumberOfOrgs_Successful()
        {
            userManager.RegisterOrg(1, "org", "address", "111-111", "usr@email.com");

            var expectedNumber = 5;

            Assert.AreEqual(expectedNumber, ((ClientManager)userManager).GetShoppingClient().Clients.Count);
        }

        [Test]
        public void RegisterOrg_CheckNewOrgTin_Successful()
        {
            userManager.RegisterOrg(44, "org", "address", "111-111", "usr@email.com");

            var expectedTin = 44;

            var client = ((ClientManager)userManager).GetShoppingClient().Clients.Last();

            Assert.AreEqual(expectedTin, ((Organization)client).Tin);
        }

        [TestCase(0, TestName = "Buying transactions")]
        [TestCase(1, TestName = "Sell transactions")]
        public void SearchHistory_NumberOfTransactionsBasedOnTransactionType_Successful(int transactionCategory)
        {
            ITransaction transaction1 = new Transaction(DateTime.Now, 0, null, null, 200, null, 1, 0);
            ITransaction transaction2 = new Transaction(DateTime.Now, 0, null, null, 400, null, 2, 0.5);
            ITransaction transaction3 = new Transaction(DateTime.Now, 1, null, null, 500, null, 1, 0);
            ITransaction transaction4 = new Transaction(DateTime.Now, 1, null, null, 100, null, 3, 0);
            IClient client = userManager.GetClientById(new Guid("00000000-0000-0000-0000-400000000001"));

            client.Transactions.Add(transaction1);
            client.Transactions.Add(transaction2);
            client.Transactions.Add(transaction3);
            client.Transactions.Add(transaction4);

            var transactions = userManager.SearchHistory(client, DateTime.Now.AddDays(-10), transactionCategory);

            var expectedNumber = 2;

            Assert.AreEqual(expectedNumber, transactions.Count);
        }


        [TestCase()]
        public void CreateUser_AddFunds_UnSuccessful()
        {
            var testClient = userManager.GetClientById(new Guid("00000000-0000-0000-0000-400000000001"));
            var dolar = new DolarCurrency(1.1);
            userManager.AddFunds(testClient, 200, dolar);
        }
    }
}