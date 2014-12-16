using System;
using System.ComponentModel.DataAnnotations;

namespace RValidate
{
    /// <summary>
    /// Validates, that string projection of property is valid URL
    /// </summary>
    public class RUrlAttribute : RValidationBaseAttribute
    {
        public RUrlAttribute(params RHttpMethods[] methods) : base(methods)
        {
            ErrorMessage = "{0} not look like valid URL";
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringified = string.Format("{0}", value);
            return Uri.IsWellFormedUriString(stringified, UriKind.RelativeOrAbsolute)
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}