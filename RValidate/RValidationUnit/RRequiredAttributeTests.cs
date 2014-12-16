using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RRequiredAttributeTests
    {
        [Test]
        public void RRequiredAttributeValueTypePositive()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = 10 }, "Property");
            Assert.IsNull(result);
        }

        [Test]
        public void RRequiredAttributeReferenceTypePositive()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = new { SomeProperty = 20 } }, "Property");
            Assert.IsNull(result);
        }

        [Test]
        public void RRequiredAttributeNullableTypePositive()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = (int?)10 }, "Property");
            Assert.IsNull(result);
        }


        [Test]
        public void RRequiredAttributeValueTypeNegative()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = default(int) }, "Property");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RRequiredAttributeReferenceTypeNegative()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = default(RRequiredAttributeTests) }, "Property");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RRequiredAttributeNullableTypeNegative()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = default(int?) }, "Property");
            Assert.IsNotNull(result);
        }
       
        [Test]
        public void RRequiredAttributeDisplayValueCorrect()
        {
            var result = new RRequiredAttribute().Validate(() => new { Property = default(RRequiredAttributeTests) }, "Property", "Class");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Class"));
        }
    }
}