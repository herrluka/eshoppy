using Eshoppy.FinanceModule;
using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
using Eshoppy.UserModule;
using Eshoppy.UserModule.Interfaces;
using Eshoppy.UserModule.Models;
using Eshoppy.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBazaar.UnitTests
{
    [TestFixture]
    public class FinanceModuleTests
    {
        IFinanceManager manager;

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

            //Credits
            ICredit credit1 = new Credit(400, 500, 1.5, 2, 6, true);
            credit1.Id = new Guid("00000000-0000-0000-0000-500000000001");
            credit1.Available = false;
            ICredit credit2 = new Credit(300, 500, 2.8, 1, 2, false);
            credit2.Id = new Guid("00000000-0000-0000-0000-500000000002");
            ICredit credit3 = new Credit(200, 600,  1.6, 1, 3, true);
            credit3.Id = new Guid("00000000-0000-0000-0000-500000000003");
            ICredit credit4 = new Credit(100, 600, 1.7, 5, 6, true);
            credit4.Id = new Guid("00000000-0000-0000-0000-500000000004");

            //Banks
            IBank bank1 = new Bank("bank1", "addr1", "bank1@bank.rs", "555-555");
            bank1.CreditOffer = new List<ICredit>() { credit1, credit2};
            IBank bank2 = new Bank("bank2", "addr2", "bank2@bank.rs", "666-666");
            bank2.CreditOffer = new List<ICredit>() { credit3};
            IBank bank3 = new Bank("bank3", "addr3", "bank3@bank.rs", "777-777");
            bank3.CreditOffer = new List<ICredit>() { credit4};

            List<IBank> banks = new List<IBank>() { bank1, bank2, bank3 };
            BankList bankList = new BankList(banks);

            //Accounts
            IAccount account1 = new Account(new DateTime(2021, 9, 5), bank1, 100);
            account1.Id = new Guid("00000000-0000-0000-0000-300000000001");
            account1.CreditDebt = 500;
            account1.CreditAvailable = false;
            IAccount account2 = new Account(new DateTime(2021, 3, 6), bank2, 200);
            account2.Id = new Guid("00000000-0000-0000-0000-300000000002");
            IAccount account3 = new Account(new DateTime(2022, 5, 1), bank3, 300);
            account3.Id = new Guid("00000000-0000-0000-0000-300000000003");
            IAccount account4 = new Account(new DateTime(2022, 1, 1), bank1, 400);
            account4.Id = new Guid("00000000-0000-0000-0000-300000000004");
            IAccount account5 = new Account(new DateTime(2022, 5, 6), bank1, 500);
            account5.Id = new Guid("00000000-0000-0000-0000-300000000005");

            client1.Accounts = new List<IAccount>() { account1 };
            client2.Accounts = new List<IAccount>() { account2, account3 };
            client3.Accounts = new List<IAccount>() { account4};
            client4.Accounts = new List<IAccount>() { account5};

            ShoppingClient clientList = new ShoppingClient(list);

            manager = new FinanceManager(clientList, bankList);
        }

        [Test]
        public void RegisterUser_CheckDateValid_Successful()
        {
            var now = DateTime.Now;

            var account = manager.CreateAccount(now, new Bank("bank", "address", "email", "111-111"), 200);

            var expectedDate = now;

            Assert.AreEqual(account.DateValid, expectedDate);
        }

        [Test]
        public void CreateBank_CheckCheckAddress_Successful()
        {

            var bank = manager.CreateBank("bank", "address", "email", "111-111");

            var expectedAddress = "address";

            Assert.AreEqual(bank.Address, expectedAddress);
        }


        [TestCase(100, 200, 2.5, 5, 8, true)]
        [TestCase(500, 300, 1.5, 8, 3, false)]
        public void CreateCredit_CheckAreEqual_Successful(double minAmount, double maxAmount, double interest, int maxYears, int minYears, bool available)
        {

            var credit = manager.CreateCredit(minAmount, maxAmount, interest, maxYears, minYears, available);

            ICredit expectedCredit = new Credit(minAmount, maxAmount, interest, maxYears, minYears, available);
            Assert.That(credit.Equals(expectedCredit));
        }

        [Test]
        public void GetId_CheckValue_Successful()
        {

            var id = Utils.getId();
            var nextId = Utils.getId();

            Assert.AreEqual(id + 1, nextId);
        }


        [Test]
        public void GetAccountById_CheckDateValid_Successful()
        {

            var account = manager.GetAccountById(new Guid("00000000-0000-0000-0000-300000000001"));

            var expectedDateValid = new DateTime(2021, 9, 5);

            Assert.AreEqual(expectedDateValid, account.DateValid);
        }

        [Test]
        public void GetAccountById_CheckIfExists_Unsuccessful()
        {

            var account = manager.GetAccountById(new Guid("00000000-0000-0005-0000-300000000001"));

            Assert.IsNull(account);
        }

        [Test]
        public void CheckBallance_ValidateAmount_Successful()
        {

            var amount = manager.CheckBalance(new Guid("00000000-0000-0000-0000-300000000001"));

            var expectedAmount = 100;

            Assert.AreEqual(expectedAmount, amount);
        }

        [Test]
        public void CheckBallance_CheckIfAccountExsists_Unsuccessful()
        {

            var amount = manager.CheckBalance(new Guid("00000000-0000-0300-0000-300000000001"));

            Assert.IsNull(amount);
        }

        [Test]
        public void Convert_ValidateAmountInDinars_Successful()
        {
            var dolar = new DolarCurrency(1.01);
            var amount = manager.Convert(dolar, 50);

            var expectedAmount = 50 * 1.01;

            Assert.AreEqual(expectedAmount, amount);
        }

        [Test]
        public void CreditPayment_ValidateCreditDebt_Successful()
        {

            manager.CreditPayment(new Guid("00000000-0000-0000-0000-300000000001"), 200);
            var account = manager.GetAccountById(new Guid("00000000-0000-0000-0000-300000000001"));
            var expectedAmount = 300;

            Assert.AreEqual(expectedAmount, account.CreditDebt);
        }

        [Test]
        public void CreditPayment_AccountNotExists_ThrowError()
        {
            Assert.Throws<NullReferenceException>(() => manager.CreditPayment(new Guid("00000000-0000-0030-0000-300000000001"), 200));
        }

        [Test]
        public void AccountPayment_ValidateAmount_Successful()
        {
            manager.AccountPayment(new Guid("00000000-0000-0000-0000-300000000001"), 200);
            var account = manager.GetAccountById(new Guid("00000000-0000-0000-0000-300000000001"));
            var expectedAmount = 300;

            Assert.AreEqual(expectedAmount, account.Amount);
        }

        [Test]
        public void AccountPayment_AccountNotExsists_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => manager.AccountPayment(new Guid("00000000-0000-0030-0000-300000000001"), 200));
        }

        [TestCase("00000000-0000-0000-0000-300000000001", TestName = "Check if user exsists")]
        [TestCase("00000000-0000-0000-0000-500000000001", TestName = "Check if credit exists")]
        public void AskCredit_EnitiyNotExist_Exception(String creditId)
        {
            Assert.Throws<NullReferenceException>(() => manager.AskCredit(new Guid("00000000-0000-0000-0000-300000000001"), 200, new Guid(creditId), 5));
        }


        [Test]
        public void AskCredit_CheckCreditIsAvailable_Unsuccessful()
        {
            IAccount account = manager.GetAccountById(new Guid("00000000-0000-0000-0000-300000000001"));
            account.CreditAvailable = true;
            var approved = manager.AskCredit(new Guid("00000000-0000-0000-0000-400000000001"), 200, new Guid("00000000-0000-0000-0000-500000000001"), 5);

            Assert.IsFalse(approved);
        }

        [TestCase(200, 1, true, TestName = "Min years are bigger than requested number of years")]
        [TestCase(200, 7, true, TestName = "Max years are smaller than requested number of years")]
        [TestCase(1000, 4, true, TestName = "Min amount is bigger than requested")]
        [TestCase(10, 4, true, TestName = "Max amount is smaller than requested ")]
        [TestCase(450, 4, false, TestName = "Credit is not availa")]
        public void AskCredit_ConditionFail_False(double amount, byte years, bool available)
        {
            IAccount account = manager.GetAccountById(new Guid("00000000-0000-0000-0000-300000000001"));
            account.CreditAvailable = true;
            ICredit credit = ((FinanceManager)manager).GetCreditById(new Guid("00000000-0000-0000-0000-500000000001"));
            credit.Available = available;

            var approved = manager.AskCredit(new Guid("00000000-0000-0000-0000-400000000001"), amount, new Guid("00000000-0000-0000-0000-500000000001"), years);

            Assert.IsFalse(approved);
        }


        [Test]
        public void AskCredit_Successful_True()
        {
            IAccount account = manager.GetAccountById(new Guid("00000000-0000-0000-0000-300000000001"));
            account.CreditAvailable = true;
            ICredit credit = ((FinanceManager)manager).GetCreditById(new Guid("00000000-0000-0000-0000-500000000001"));
            credit.Available = true;
            var approved = manager.AskCredit(new Guid("00000000-0000-0000-0000-400000000001"), 450, new Guid("00000000-0000-0000-0000-500000000001"), 4);

            Assert.IsTrue(approved);
        }
    }
}
