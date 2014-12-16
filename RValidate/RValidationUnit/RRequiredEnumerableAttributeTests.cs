using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RRequiredEnumerableAttributeTests
    {
        [Test]
        public void RRequiredEnumerableAttributePositive()
        {
            var result = new RRequiredEnumerableAttribute().Validate(() => new { Collection = new[] { 1, 2, 3 } }, "Collection");
            Assert.IsNull(result);
        }

        [Test]
        public void RRequiredEnumerableAttributeNegative()
        {
            var result = new RRequiredEnumerableAttribute().Validate(() => new { Collection = new int[0] }, "Collection");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RRequiredEnumerableAttributeNullNegative()
        {
            var result = new RRequiredEnumerableAttribute().Validate(() => new { Collection = default(int[]) }, "Collection");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RRequiredEnumerableAttributeDisplayValueCorrect()
        {
            var result = new RRequiredEnumerableAttribute().Validate(() => new { Collection = new int[0] }, "Collection", "Items");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Items"));
        }
    }
}