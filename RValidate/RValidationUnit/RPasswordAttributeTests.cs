using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RPasswordAttributeTests
    {
        [Test]
        public void RPasswordAttributeBlankPositive()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Blank).Validate(() => new { Password = "1234" }, "Password");
            Assert.IsNull(result);
        }

        [Test]
        public void RPasswordAttributeMediumPositive()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Medium).Validate(() => new { Password = "superpassword" }, "Password");
            Assert.IsNull(result);
        }

        [Test]
        public void RPasswordAttributeStrongPositive()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Strong).Validate(() => new { Password = "sUperMegaHeroPassword24567$lkajdflkjsdf#" }, "Password");
            Assert.IsNull(result);
        }

        [Test]
        public void RPasswordAttributeVeryStrongPositive()
        {
            var result = new RPasswordAttribute(RPasswordStrength.VeryStrong).Validate(() => new { Password = "sUperMegaHeroPassword24567$lkajdflkjsdf#" }, "Password");
            Assert.IsNull(result);
        }

        [Test]
        public void RPasswordAttributeVeryWeakPositive()
        {
            var result = new RPasswordAttribute(RPasswordStrength.VeryWeak).Validate(() => new { Password = "some" }, "Password");
            Assert.IsNull(result);
        }

        [Test]
        public void RPasswordAttributeWeakPositive()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Weak).Validate(() => new { Password = "somasdae" }, "Password");
            Assert.IsNull(result);
        }

        [Test]
        public void RPasswordAttributeMediumNegative()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Medium).Validate(() => new { Password = "some" }, "Password");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RPasswordAttributeStrongNegative()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Strong).Validate(() => new { Password = "some" }, "Password");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RPasswordAttributeVeryStrongNegative()
        {
            var result = new RPasswordAttribute(RPasswordStrength.VeryStrong).Validate(() => new { Password = "some" }, "Password");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RPasswordAttributeVeryWeakNegative()
        {
            var result = new RPasswordAttribute(RPasswordStrength.VeryWeak).Validate(() => new { Password = string.Empty }, "Password");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RPasswordAttributeWeakNegative()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Weak).Validate(() => new { Password = "some" }, "Password");
            Assert.IsNotNull(result);
        }


        [Test]
        public void RPasswordAttributeDisplayValueCorrect()
        {
            var result = new RPasswordAttribute(RPasswordStrength.Medium).Validate(() => new { Password = "some" }, "Password", "Repeat Password");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage.Contains("Repeat Password"));
        }
    }
}