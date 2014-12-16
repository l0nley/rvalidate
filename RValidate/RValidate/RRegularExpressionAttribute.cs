using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RValidate
{
    /// <summary>
    /// Validates, that proerty string projection match pattern
    /// </summary>
    public class RRegularExpressionAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Creates new instance if <see cref="RRegularExpressionAttribute"/>
        /// </summary>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RRegularExpressionAttribute(params RHttpMethods[] methods) : base(methods)
        {
            ErrorMessage = "{0} have invalid value.";
            TicksToWait = 200;
        }

        /// <summary>
        /// Pattern to validate with
        /// </summary>
        public string ValidationPattern { get; set; }

        /// <summary>
        /// Options to validate with
        /// </summary>
        public RegexOptions MatchOptions { get; set; }

        /// <summary>
        /// Defines how long can validation be performed
        /// </summary>
        public long TicksToWait { get; set; }

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
            try
            {
                return Regex.IsMatch(stringified, ValidationPattern, MatchOptions, new TimeSpan(TicksToWait))
                    ? null
                    : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }
        }
    }
}