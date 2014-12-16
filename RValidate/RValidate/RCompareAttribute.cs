using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RValidate
{
    /// <summary>
    /// Compares property with another one. Value <see cref="RComparison"/> ValueToCompare
    /// </summary>
    public class RCompareAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create an instance of <see cref="RCompareAttribute"/>
        /// </summary>
        /// <param name="propertyToCompare">Property name compare with</param>
        /// <param name="comparison">Comparison type</param>
        /// <param name="comparer">Custome compare type. Should implement <see cref="IComparer"/> interface</param>
        /// <param name="methods">Methods, with which validation will be performed</param>
        /// <exception cref="ArgumentException">If <paramref name="comparer"/> not implement <see cref="IComparer"/></exception>
        public RCompareAttribute( 
            string propertyToCompare, 
            RComparison comparison = RComparison.Equal,
            Type comparer = null, 
            params RHttpMethods[] methods) : base(methods)
        {
            ErrorMessage = "{0} should be {1} {2}";
            PropertyToCompare = propertyToCompare;
            Comparison = comparison;
            Comparer = comparer;
            if (Comparer == null)
            {
                return;
            }

            if (Comparer.GetInterfaces().Any(l => l == typeof(IComparer)) == false)
            {
                throw new ArgumentException("Comparer should implement IComparer interface.", "comparer");
            }
        }

        /// <summary>
        /// Property name to compare with
        /// </summary>
        public string PropertyToCompare { get; private set; }

        /// <summary>
        /// Type of camprison
        /// </summary>
        public RComparison Comparison { get; private set; }

        /// <summary>
        /// Comporator type
        /// </summary>
        public Type Comparer { get; private set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, Comparison, PropertyToCompare);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyToCompare = validationContext.ObjectType.GetProperty(PropertyToCompare);
            if (propertyToCompare == null)
            {
                throw new InvalidOperationException("Object should contain property, with wich comparison will be perfomed");
            }

            var valueToCompare = propertyToCompare.GetValue(validationContext.ObjectInstance);
            
            int result;
            if (Comparer != null)
            {
                var comparer = (IComparer)Activator.CreateInstance(Comparer);
                result = comparer.Compare(value, valueToCompare);
            }
            else
            {
                result = System.Collections.Comparer.Default.Compare(value, valueToCompare);
            }

            var met = false;
            switch (Comparison)
            {
                case RComparison.Equal:
                    met = result == 0;
                    break;
                case RComparison.Less:
                    met = result < 0;
                    break;
                case RComparison.More:
                    met = result > 0;
                    break;
            }

            return met
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}