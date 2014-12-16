using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Validate that string projection of property is a valid mod10 credit card
    /// </summary>
    public class RCreditCardAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create new instance of <see cref="RCreditCardAttribute"/>
        /// </summary>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RCreditCardAttribute(params RHttpMethods[] methods)
            : base(methods)
        {
            ErrorMessage = "{0} not looks like be a valid credit card number";
        }

        /// <summary>
        /// Error message formatting
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringified = string.Format("{0}", value)
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .Replace("\t", string.Empty);
            if (stringified.All(char.IsNumber) == false)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            var sumOfDigits = stringified
                    .Reverse()
                    .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                .Sum(e => e / 10 + e % 10);

            return sumOfDigits % 10 == 0
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}