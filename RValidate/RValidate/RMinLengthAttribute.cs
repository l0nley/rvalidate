using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Validates, that property have length greater or equal than defined
    /// </summary>
    public class RMinLengthAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create new instance of <see cref="RMaxLengthAttribute"/>
        /// </summary>
        /// <param name="minLength">Minimum allowed length</param>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RMinLengthAttribute(int minLength, params RHttpMethods[] methods)
            : base(methods)
        {
            MinLength = minLength;
            ErrorMessage = "{0} length should be greater than {1}";
        }

        /// <summary>
        /// Maximum length
        /// </summary>
        public int MinLength { get; private set; }

        /// <summary>
        /// Error message formatting
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, MinLength);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            var count = ((IEnumerable)value).Cast<object>().Count();

            return count >= MinLength
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}