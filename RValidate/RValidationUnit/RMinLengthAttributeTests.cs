using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RMinLengthAttributeTests
    {
        [Test]
        public void RMinLengthAttributePositive()
        {
            var result = new RMinLengthAttribute(2).Validate(() => new { String = "ho" }, "String");
            Assert.IsNull(result);
        }

        [Test]
        public void RMaxLengthAttributeCollectionPositive()
        {
            var result = new RMinLengthAttribute(2).Validate(() => new { Collection = new[] { 1, 2 } }, "Collection");
            Assert.IsNull(result);
        }

        [Test]
        public void RMinLengthAttributeNegative()
        {
            var result = new RMinLengthAttribute(3).Validate(() => new { String = "ho" }, "String");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RMaxLengthAttributeCollectionNegative()
        {
            var result = new RMinLengthAttribute(5).Validate(() => new { Collection = new[] { 1, 2, 3 } }, "Collection");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RMinLengthAttributeDisplayValueCorrect()
        {
            var result = new RMinLengthAttribute(3).Validate(() => new { String = "ho" }, "String", "Last Name");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Last Name"));
        }
    }
}