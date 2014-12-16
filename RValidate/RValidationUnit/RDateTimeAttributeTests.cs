using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RDateTimeAttributeTests
    {
        [Test]
        public void RDateTimeAttributePositive()
        {
            var result = new RDateTimeAttribute().Validate(() => new { Date = DateTime.Now }, "Date");
            Assert.IsNull(result);
        }

        [Test]
        public void RDateTimeAttributeNegative()
        {
            var result = new RDateTimeAttribute().Validate(() => new { Date = "Not a date" }, "Date");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RDateTimeAttributeDisplayValueCorrect()
        {
            var result = new RDateTimeAttribute().Validate(() => new { Date = "Not a date" }, "Date", "Purchase date");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Purchase date"));
        }
    }
}