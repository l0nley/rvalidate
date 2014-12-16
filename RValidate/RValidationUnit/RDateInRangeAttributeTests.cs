using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RDateInRangeAttributeTests
    {
        [Test]
        public void RDateInRangeAttributePositive()
        {
            var result = new RDateInRangeAttribute(2014, 1, 1, 2015, 12, 31)
                .Validate(() => new { Date = DateTime.Now }, "Date");
            Assert.IsNull(result);
        }

        [Test]
        public void RDateInRangeAttributeNegative()
        {
            var result = new RDateInRangeAttribute(2000, 1, 1, 2000, 12, 31)
                 .Validate(() => new { Date = DateTime.Now }, "Date");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RDateInRangeAttributeDisplayValueCorrect()
        {
            var result = new RDateInRangeAttribute(2000, 1, 1, 2000, 12, 31)
                .Validate(() => new { Date = DateTime.Now }, "Date", "Purchase Date");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Purchase Date"));
        }
    }
}