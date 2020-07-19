using Eshoppy.FinanceModule.Interfaces;
using Eshoppy.FinanceModule.Models;
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
    public class FinanceModuleNSubstituteTests
    {
        [Test]
        public void AccountPayment_AccountPaymentWithNegativeAmount_ThrowException()
        {
            IFinanceManager financeManager = Substitute.For<IFinanceManager>();

            financeManager
                .When(x => x.AccountPayment(Arg.Any<Guid>(),
                                            Arg.Is<double>(y => y < 0)))
                .Do(x => { throw new ArgumentOutOfRangeException("Amount cannot be smaller or equal than 0"); });

            Assert.Throws<ArgumentOutOfRangeException>(() => financeManager.AccountPayment(new Guid(), -100));
        }

        [Test]
        public void AccountPayment_CheckAmountGreaterThanZero_Valid()
        {
            IFinanceManager financeManager = Substitute.For<IFinanceManager>();

            financeManager.AccountPayment(new Guid(), 200);

            financeManager.Received().AccountPayment(Arg.Any<Guid>(), Arg.Is<double>(x => x > 0));
        }
    }
}
