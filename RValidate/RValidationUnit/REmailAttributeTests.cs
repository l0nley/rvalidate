using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class REmailAttributeTests
    {
        [Test]
        public void REmailAttributePositive()
        {
            var result = new REmailAddressAttribute().Validate(() => new { Email = "hofvl0nley@gmail.com" }, "Email");
            Assert.IsNull(result);
        }

        [Test]
        public void REmailAttributeNegative()
        {
            var result = new REmailAddressAttribute().Validate(() => new { Email = "bademail.com" }, "Email");
            Assert.IsNotNull(result);
        }

        [Test]
        public void REmailAttributeDisplayValueCorrect()
        {
            var result = new REmailAddressAttribute().Validate(() => new { Email = "bademail.com" }, "Email", "E-mail");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("E-mail"));
        }
    }
}