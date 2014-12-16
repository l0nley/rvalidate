using System;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RUrlAttributeTests
    {
        [Test]
        public void RUrlAttributeHttpPositive()
        {
            var result = new RUrlAttribute().Validate(() => new { Url = "http://rvalidate.com " }, "Url");
            Assert.IsNull(result);
        }

         [Test]
        public void RUrlAttributeHttpsPositive()
        {
            var result = new RUrlAttribute().Validate(() => new { Url = "https://rvalidate.com " }, "Url");
            Assert.IsNull(result);
        }

        [Test]
        public void RUrlAttributeNegative()
        {
            var result = new RUrlAttribute().Validate(() => new { Url = "not a url" }, "Url");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RUrlAttributeDisplayValueCorrect()
        {
            var result = new RUrlAttribute().Validate(() => new { Url = "not a url" }, "Url", "Address");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Address"));
        }
    }
}