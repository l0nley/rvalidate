using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Validates, that property have no default value of property type
    /// </summary>
    public class RRequiredAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create attributes. If no methods specified, then PUT and POST methods will be added by default
        /// </summary>
        /// <param name="methods">Methods, on which validation should be performed</param>
        public RRequiredAttribute(params RHttpMethods[] methods)
            : base(methods)
        {
            ErrorMessage = "{0} is required";
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
            var propertyInfo = validationContext.ObjectType.GetProperties().Single(l => l.Name == validationContext.MemberName);
            
            if (propertyInfo.PropertyType.IsValueType == false)
            {
                return value == null 
                    ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName }) 
                    : null;
            }

            return Equals(Activator.CreateInstance(propertyInfo.PropertyType), value)
                ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName })
                : null;
        }
    }
}