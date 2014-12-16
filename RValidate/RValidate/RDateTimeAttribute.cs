using System;
using System.ComponentModel.DataAnnotations;

namespace RValidate
{
    /// <summary>
    /// Validate, that stringified projection of property can be parsed as DateTime type
    /// </summary>
    public class RDateTimeAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create new instance of <see cref="RDateTimeAttribute"/>
        /// </summary>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RDateTimeAttribute(params RHttpMethods[] methods) : base(methods)
        {
            ErrorMessage = "{0} is not look like date";
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
            var stringified = string.Format("{0}", value);
            DateTime result;
            return DateTime.TryParse(stringified, out result)
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}