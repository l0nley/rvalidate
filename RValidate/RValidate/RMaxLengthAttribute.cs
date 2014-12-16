using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Validate, that property have length less or equal supplied.
    /// Property that verified, should be castable to IEnumerable interface
    /// </summary>
    public class RMaxLengthAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create new instance of <see cref="RMaxLengthAttribute"/>
        /// </summary>
        /// <param name="maxLength">Maximum allowed length</param>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RMaxLengthAttribute(int maxLength, params RHttpMethods[] methods) : base(methods)
        {
            MaxLength = maxLength;
            ErrorMessage = "{0} length should be less than {1}";
        }

        /// <summary>
        /// Maximum length
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        /// Error message formatting
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, MaxLength);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            var count = ((IEnumerable)value).Cast<object>().Count();

            return count <= MaxLength
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}