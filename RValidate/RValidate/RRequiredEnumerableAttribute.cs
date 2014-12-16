using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Validates, that collection should have at least one member and not be null
    /// </summary>
    public class RRequiredEnumerableAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="RRequiredEnumerableAttribute"/>
        /// </summary>
        /// <param name="methods">Methods, on which validation should be performed</param>
        public RRequiredEnumerableAttribute(params RHttpMethods[] methods)
            : base(methods)
        {
            ErrorMessage = "{0} collection should contain at least one element";
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
            var prop = validationContext.ObjectType.GetProperties().Single(l => l.Name == validationContext.MemberName);
            if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) == false)
            {
                throw new ArgumentException("Property type must implement IEnumerable");
            }

            var val = value as IEnumerable;
            if (val != null && val.GetEnumerator().MoveNext())
            {
                return null;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}