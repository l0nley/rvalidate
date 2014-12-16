using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RCreditCardNumberAttributeTests
    {
        [Test]
        public void RCreditCardNumberAttributePositive()
        {
            var result = new RCreditCardAttribute().Validate(() => new { CardNo = "377109223207725" }, "CardNo");
            Assert.IsNull(result);
        }

        [Test]
        public void RCreditCardNumberAttributeNegative()
        {
            var result = new RCreditCardAttribute().Validate(() => new { CardNo = "bad number" }, "CardNo");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RCreditCardNumberAttributeDisplayValueCorrect()
        {
            var result = new RCreditCardAttribute().Validate(() => new { CardNo = "bad number" }, "CardNo", "Credit Card No");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Credit Card No"));
        }
    }
}