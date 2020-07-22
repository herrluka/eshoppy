using Eshoppy.TransactionModule;
using Eshoppy.UserModule.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBazaar.UnitTests.Fakes;
using Eshoppy.TransactionModule.Interfaces;
using Eshoppy.TransactionModule.Models;
using Eshoppy.SalesModule.Models;
using Eshoppy.SalesModule.Interfaces;

namespace EBazaar.UnitTests
{
    [TestFixture]
    public class TransactionModuleTests
    {
        ITransactionManager transactionManager;
        IOffer offer;

        [SetUp]
        public void Init()
        {
            ITransaction transaction1 = new Transaction(DateTime.Now, 0, null, null, 200, null, 1, 0);
            ITransaction transaction2 = new Transaction(DateTime.Now, 0, null, null, 400, null, 2, 0.5);
            ITransaction transaction3 = new Transaction(DateTime.Now, 1, null, null, 500, null, 1, 0);
            ITransaction transaction4 = new Transaction(DateTime.Now, 1, null, null, 100, null, 3, 0);

            List<ITransaction> list = new List<ITransaction>() { transaction1, transaction2, transaction3, transaction4 };
            TransactionList transactionList = new TransactionList(list);

            IClientManager clientManager = new FakeClientManager();
            transactionManager = new TransactionManager(clientManager, transactionList);

            var transport1 = new Transport("Transport1", 2);
            transport1.Id = new Guid("00000000-0000-0000-0000-100000000001");
            var transport2 = new Transport("Transport2", 3);
            transport2.Id = new Guid("00000000-0000-0000-0000-100000000002");
            var transport3 = new Transport("Transport3", 4);
            transport3.Id = new Guid("00000000-0000-0000-0000-100000000003");

            var product1 = new Product("Product1", 22.5, 5);
            product1.Id = new Guid("00000000-0000-0000-0000-200000000001");
            var product2 = new Product("Product2", 12.5, 10);
            product2.Id = new Guid("00000000-0000-0000-0000-200000000002");
            var product3 = new Product("Product3", 122.5, 23);
            product3.Id = new Guid("00000000-0000-0000-0000-200000000003");

            offer = new Offer(new List<IProduct>() { product1, product2, product3 }, DateTime.Now, DateTime.Now, new List<ITransport>() { transport1, transport2 });
        }

        [Test]
        public void CreateTransaction_AccountsWithoutEngoughMoney_SendEmail()
        {
            
            offer.OfferPrice = 500;
            var emailSender = new FakeEmailSender();
            transactionManager.CreateTransaction(DateTime.Now, 1, Guid.NewGuid(), Guid.NewGuid(), offer, 500, new WithoutInstalmentsTransactionType(), 3, emailSender);

            var expectedMessage = "On your accounts there is not enough money";

            Assert.AreEqual(expectedMessage, emailSender.Message);
        }
        

        [Test]
        public void CreateTransaction_SendEmailWithoutInstalmentsTransaction_Successful()
        {

            offer.OfferPrice = 20;
            var emailSender = new FakeEmailSender();
            transactionManager.CreateTransaction(DateTime.Now, 1, Guid.NewGuid(), Guid.NewGuid(), offer, 500, new WithoutInstalmentsTransactionType(), 3, emailSender);

            var expectedMessage = "Transaction was sucessfull";

            Assert.AreEqual(expectedMessage, emailSender.Message);
        }

        [Test]
        public void CreateTransaction_SendEmailInstalmentsTransaction_Successful()
        {

            offer.OfferPrice = 20;
            var emailSender = new FakeEmailSender();
            transactionManager.CreateTransaction(DateTime.Now, 1, Guid.NewGuid(), Guid.NewGuid(), offer, 500, new InstalmentsTransactionType(), 3, emailSender);

            var expectedMessage = "Transaction was sucessfull";

            Assert.AreEqual(expectedMessage, emailSender.Message);
        }
    }
}
