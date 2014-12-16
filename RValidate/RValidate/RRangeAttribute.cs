using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Validate, that property projection to string and converted to numeric in specified range
    /// </summary>
    public class RRangeAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="RRangeAttribute"/>
        /// </summary>
        /// <param name="minValue">Low bound of range. Included in range.</param>
        /// <param name="maxValue">High bound of range. Included in range</param>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RRangeAttribute(int minValue, int maxValue, params RHttpMethods[] methods)
            : base(methods)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            ErrorMessage = "{0} should be in range from {1} to {2}";
        }

        /// <summary>
        /// Minimum value of property
        /// </summary>
        public int MinValue { get; private set; }

        /// <summary>
        /// Maximum value of property
        /// </summary>
        public int MaxValue { get; private set; }

        /// <summary>
        /// Error message formatting
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, MinValue, MaxValue);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var prop = validationContext.ObjectType.GetProperties().Single(l => l.Name == validationContext.MemberName);
            var stringified = string.Format("{0}", value);
            decimal valueToCompare;
            if (decimal.TryParse(stringified, out valueToCompare) == false)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return valueToCompare >= MinValue && valueToCompare <= MaxValue
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}