using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RRangeAttributeTests
    {
        [Test]
        public void RRangeAttributePositive()
        {
            var result = new RRangeAttribute(0, 10).Validate(() => new { Value = 5 }, "Value");
            Assert.IsNull(result);
        }

        [Test]
        public void RRangeAttributeNegative()
        {
            var result = new RRangeAttribute(0, 10).Validate(() => new { Value = 25 }, "Value");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RRangeAttributeDisplayValueCorrect()
        {
            var result = new RRangeAttribute(0, 10).Validate(() => new { Value = 25 }, "Value", "Count");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Count"));
        }
    }
}