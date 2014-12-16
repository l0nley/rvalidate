using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RCompareAttributeTests 
    {
        [Test]
        public void RCompareAttributePositiveWithComparisonEqual()
        {
            var result = new RCompareAttribute("Property2").Validate(() => new { Property2 = 10 }, 10, "Property1");
            Assert.IsNull(result);
        }

        [Test]
        public void RCompareAttributePositiveWithComparisonLess()
        {
            var result = new RCompareAttribute("Property2", RComparison.Less).Validate(() => new { Property2 = 10 }, 8, "Property1");
            Assert.IsNull(result);
        }

        [Test]
        public void RCompareAttributePositiveWithComparisonMore()
        {
            var result = new RCompareAttribute("Property2", RComparison.More).Validate(() => new { Property2 = 8 }, 10, "Property1");
            Assert.IsNull(result);
        }

        [Test]
        public void RCompareAttributeNegativeWithComparisonEqual()
        {
            var result = new RCompareAttribute("Property2", RComparison.Less).Validate(() => new { Property2 = 8 }, 10, "Property1");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RCompareAttributeNegativeWithComparisonLess()
        {
            var result = new RCompareAttribute("Property2", RComparison.Less).Validate(() => new { Property2 = 8 }, 10, "Property1");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RCompareAttributeNegativeWithComparisonMore()
        {
            var result = new RCompareAttribute("Property2", RComparison.More).Validate(() => new { Property2 = 18 }, 10, "Property1");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RCompareAttributeDisplayValueCorrect()
        {
            var result = new RCompareAttribute("Property2").Validate(() => new { Property2 = 10 }, 11, "Property1", "SuperOne");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("SuperOne"));
        }
    }
}