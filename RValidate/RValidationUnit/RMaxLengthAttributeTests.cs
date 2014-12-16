using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RMaxLengthAttributeTests
    {
        [Test]
        public void RMaxLengthAttributePositive()
        {
            var result = new RMaxLengthAttribute(2).Validate(() => new { String = "ho" }, "String");
            Assert.IsNull(result);
        }

        [Test]
        public void RMaxLengthAttributeCollectionPositive()
        {
            var result = new RMaxLengthAttribute(2).Validate(() => new { Collection = new[] { 1, 2 } }, "Collection");
            Assert.IsNull(result);
        }

        [Test]
        public void RMaxLengthAttributeNegative()
        {
            var result = new RMaxLengthAttribute(1).Validate(() => new { String = "ho" }, "String");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RMaxLengthAttributeCollectionNegative()
        {
            var result = new RMaxLengthAttribute(2).Validate(() => new { Collection = new[] { 1, 2, 3 } }, "Collection");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RMaxLengthAttributeDisplayValueCorrect()
        {
            var result = new RMaxLengthAttribute(1).Validate(() => new { String = "ho" }, "String", "First Name");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("First Name"));
        }
    }
}