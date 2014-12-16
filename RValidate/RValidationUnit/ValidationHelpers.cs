using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RValidate;

namespace RValidationUnit
{
    public static class ValidationHelpers
    {
        public static ValidationContext GetValidationContext(this object value, string propertyToBind)
        {
            return new ValidationContext(value)
            {
                DisplayName = propertyToBind,
                MemberName = propertyToBind,
            };
        }

        public static ValidationContext GetValidationContext(this object value, string propertyToBind, string displayName)
        {
            return new ValidationContext(value)
            {
                DisplayName = displayName,
                MemberName = propertyToBind,
            };
        }

        public static ValidationResult Validate<T>(this T attribute, Func<object> objectFactory, object propertyValue, string propertyName)
            where T : RValidationBaseAttribute
        {
            var obj = objectFactory();
            return attribute.GetValidationResult(propertyValue, obj.GetValidationContext(propertyName));
        }

        public static ValidationResult Validate<T>(this T attribute, Func<object> objectFactory, string propertyName)
            where T : RValidationBaseAttribute
        {
            var obj = objectFactory();
            var value = obj.GetType().GetProperties().First(l => l.Name == propertyName).GetValue(obj);
            return attribute.GetValidationResult(value, obj.GetValidationContext(propertyName));
        }

        public static ValidationResult Validate<T>(this T attribute, Func<object> objectFactory, string propertyName, string displayName)
            where T : RValidationBaseAttribute
        {
            var obj = objectFactory();
            var value = obj.GetType().GetProperties().First(l => l.Name == propertyName).GetValue(obj);
            return attribute.GetValidationResult(value, obj.GetValidationContext(propertyName, displayName));
        }

        public static ValidationResult Validate<T>(this T attribute, Func<object> objectFactory, object propertyValue, string propertyName, string displayName)
            where T : RValidationBaseAttribute
        {
            var obj = objectFactory();
            return attribute.GetValidationResult(propertyValue, obj.GetValidationContext(propertyName, displayName));
        }
    }
}
