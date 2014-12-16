using System;
using System.ComponentModel.DataAnnotations;

namespace RValidate
{
    /// <summary>
    /// Validate, that property in date range
    /// </summary>
    public class RDateInRangeAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="RDateInRangeAttribute"/>
        /// </summary>
        /// <param name="yearStart">Period start year</param>
        /// <param name="monthStart">Period start month</param>
        /// <param name="dayStart">Period start day of month</param>
        /// <param name="yearEnd">Period end year</param>
        /// <param name="monthEnd">Period end month</param>
        /// <param name="dayEnd">Period end day</param>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RDateInRangeAttribute(
            int yearStart, 
            int monthStart, 
            int dayStart,
            int yearEnd,
            int monthEnd,
            int dayEnd,
            params RHttpMethods[] methods)
            : base(methods)
        {
            ErrorMessage = "{0} shoud be in range of {1} to {2}";
            PeriodStart = new DateTime(yearStart, monthStart, dayStart);
            PeriodEnd = new DateTime(yearEnd, monthEnd, dayEnd);
        }

        /// <summary>
        /// Period starting date
        /// </summary>
        public DateTime PeriodStart { get; private set; }

        /// <summary>
        /// Period end date
        /// </summary>
        public DateTime PeriodEnd { get; private set; }

        /// <summary>
        /// Error message formatting
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, PeriodStart, PeriodEnd);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueToCompare = Convert.ToDateTime(value);
            if (valueToCompare <= PeriodEnd && valueToCompare >= PeriodStart)
            {
                return null;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}