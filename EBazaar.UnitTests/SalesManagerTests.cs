using Eshoppy.SalesModule;
using Eshoppy.SalesModule.Interfaces;
using Eshoppy.SalesModule.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests
{
    [TestFixture]
    public class SalesManagerTests
    {
        SalesManager manager;

        [SetUp]
        public void Init()
        {
            var list = new List<IOffer>();
            var offerList = new ListOffers(list);
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

            var offer1 = new Offer(new List<IProduct>() { product1, product2, product3 }, DateTime.Now, DateTime.Now, new List<ITransport>() { transport1, transport2 });
            offer1.Id = new Guid("00000000-0000-0000-0000-300000000001");
            offer1.OfferPrice = 50;
            var offer2 = new Offer(new List<IProduct>() { product2, product3 }, DateTime.Now, DateTime.Now, new List<ITransport>() { transport2 });
            offer2.OfferPrice = 30;
            offer2.Id = new Guid("00000000-0000-0000-0000-300000000002");
            var offer3 = new Offer(new List<IProduct>() { product3 }, DateTime.Now, DateTime.Now, new List<ITransport>() { transport3 });
            offer3.OfferPrice = 10;
            offer3.Id = new Guid("00000000-0000-0000-0000-300000000003");
            offerList.Offers.Add(offer1);
            offerList.Offers.Add(offer2);
            offerList.Offers.Add(offer3);
            manager = new SalesManager(offerList);
        }

        [Test]
        public void CreateProduct_ValidateCreation_Successful()
        {
            IProduct product = new Product("prod", 22.5, 44.5);
            IProduct productTest = manager.CreateProduct("prod", 22.5, 44.5);
            Assert.AreEqual(product.Name, productTest.Name);
        }

        [Test]
        public void CheckDiscount_OfferOldRule_Successful()
        {
            var offer = new Offer(new List<IProduct>(), DateTime.Now.AddDays(-61), DateTime.Now, new List<ITransport>());
            var discount = offer.CheckDiscount(DateTime.Now);
            Assert.AreEqual(discount, 0.12);
        }

        [TestCase(1, TestName = "January")]
        [TestCase(12, TestName = "December")]
        public void CheckDiscount_MonthsOfDiscountPlusOfferOldRule_Successful(int month)
        {
            var offer = new Offer(new List<IProduct>(), new DateTime(DateTime.Now.Year - 1, 1, DateTime.Now.Day), DateTime.Now, new List<ITransport>());
            var discount = offer.CheckDiscount(new DateTime(2020, month, 15));
            Assert.AreEqual(discount, 0.17);
        }

        [TestCase(4, TestName = "April")]
        [TestCase(9, TestName = "Septemper")]
        public void CheckDiscount_MonthOfDiscountRuleValidation_Unsuccessful(int month)
        {
            var offer = new Offer(new List<IProduct>(), new DateTime(DateTime.Now.Year - 1, 1, DateTime.Now.Day), DateTime.Now, new List<ITransport>());
            var discount = offer.CheckDiscount(new DateTime(2020, month, 15));
            Assert.AreEqual(discount, 0.12);
        }

        [Test]
        public void CheckDiscount_NumberOfProductsRulePlusOfferOldRule_Successful()
        {
            var product1 = new Product("Product1", 22.5, 5);
            var product2 = new Product("Product2", 12.5, 10);
            var product3 = new Product("Product3", 122.5, 23);
            var product4 = new Product("Product4", 122.5, 23);
            var offer = new Offer(new List<IProduct>() { product1, product2, product3, product4 }, new DateTime(DateTime.Now.Year - 1, 1, DateTime.Now.Day), DateTime.Now, new List<ITransport>());
            var discount = offer.CheckDiscount(new DateTime(2020, 2, 15));
            Assert.AreEqual(discount, 0.17);
        }

        [Test]
        public void CreateOffer_ValidateCreation_Successful()
        {
            var product1 = new Product("Product1", 2, 5);
            var product2 = new Product("Product2", 3, 10);
            var product3 = new Product("Product3", 4, 23);
            var product4 = new Product("Product4", 5, 23);
            var offer = new Offer(new List<IProduct>() { product1, product2, product3, product4 }, new DateTime(DateTime.Now.Year - 1, 1, DateTime.Now.Day), DateTime.Now, new List<ITransport>());
            offer.OfferPrice = offer.GetPriceAsSumOfProducts(new DateTime(2020, 1, 1));

            var expected_price = (product1.Price + product2.Price + product3.Price + product4.Price) * (1 - 0.22);

            Assert.AreEqual(offer.OfferPrice, expected_price);
        }

        [Test]
        public void GetOffersByTransportID_NumberOfOffers_Successful()
        {
            var offersNumber = manager.GetOffersByTrasportId(new Guid("00000000-0000-0000-0000-100000000002")).Count;

            var expectedNumber = 2;

            Assert.AreEqual(offersNumber, expectedNumber);
        }

        [Test]
        public void GetOffersByProduct_NumberOfOffers_Successful()
        {
            var offersNumber = manager.GetOffersByProduct(new Guid("00000000-0000-0000-0000-200000000002")).Count;

            var expectedNumber = 2;

            Assert.AreEqual(offersNumber, expectedNumber);
        }

        [Test]
        public void GetLowestOffer_ValidateId_Successful()
        {
            var offer = manager.GetLowestOffer();

            var id = new Guid("00000000-0000-0000-0000-300000000003");

            Assert.AreEqual(offer.Id, id);
        }

        [Test]
        public void GetTransportCost_ValidateCost_Successful()
        {
            IOffer offer = manager.GetLowestOffer();
            ITransport transport = offer.AvailableTransports.ElementAt(0);
            manager.GetTransportCost(offer, transport);

            var expected = transport.TransportCoefficient * offer.OfferPrice;

            Assert.AreEqual(expected, offer.TransportPrice);
        }

        [Test]
        public void UpdateOffer_ListOfProductsChange_Successful()
        {
            var product1 = new Product("NewProduct1", 22.5, 5);
            var product2 = new Product("NewProduct2", 12.5, 10);
            var product3 = new Product("NewProduct3", 122.5, 23);
            var product4 = new Product("NewProduct4", 122.5, 23);
            var product5 = new Product("NewProduct5", 122.5, 23);

            var offer = manager.GetLowestOffer();

            manager.UpdateOffer(offer, products: new List<IProduct>() { product1, product2, product3, product4, product5});

            var expectedNumberOfProducts = 5;

            Assert.AreEqual(offer.Products.Count, expectedNumberOfProducts);
        }

        [Test]
        public void UpdateOffer_ListOfProductsChangePriceUnchanged_Sucessful()
        {
            var product1 = new Product("NewProduct1", 22.5, 5);
            var product2 = new Product("NewProduct2", 12.5, 10);
            var product3 = new Product("NewProduct3", 122.5, 23);
            var product4 = new Product("NewProduct4", 122.5, 23);
            var product5 = new Product("NewProduct5", 122.5, 23);

            var offer = manager.GetLowestOffer();
            offer.OfferPrice = 500;

            manager.UpdateOffer(offer, products: new List<IProduct>() { product1, product2, product3, product4, product5 });

            var expectedNumberOfProducts = 500;

            Assert.AreEqual(offer.OfferPrice, expectedNumberOfProducts);
        }

        [Test]
        public void UpdateOffer_DateValidChanged_Successful()
        {
            var offer = manager.GetLowestOffer();
            var now = DateTime.Now;
            manager.UpdateOffer(offer, dateValid: now);

            Assert.AreEqual(now, offer.DateValid);
        }
    }
}
