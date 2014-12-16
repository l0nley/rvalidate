using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RValidate
{
    /// <summary>
    /// Validates that property projection to string have valid password strength
    /// </summary>
    public class RPasswordAttribute : RValidationBaseAttribute
    {
        /// <summary>
        /// Create new instance of <see cref="RPasswordAttribute"/>
        /// </summary>
        /// <param name="minimalStrengthRequired">Minimal strength for password</param>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public RPasswordAttribute(RPasswordStrength minimalStrengthRequired, params RHttpMethods[] methods)
            : base(methods)
        {
            MinimalStrengthRequired = minimalStrengthRequired;
            ErrorMessage = "{0} should have strength greater or equal {1}";
        }

        public RPasswordStrength MinimalStrengthRequired { get; private set; }

        /// <summary>
        /// Error message formatting
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, MinimalStrengthRequired);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringified = string.Format("{0}", value);
            var strength = CalculateScore(stringified);
            return strength >= MinimalStrengthRequired
                ? null
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }

        private static RPasswordStrength CalculateScore(string password)
        {
            var score = 1;

            if (password.Length < 1)
            {
                return RPasswordStrength.Blank;
            }

            if (password.Length < 4)
            {
                return RPasswordStrength.VeryWeak;
            }

            if (password.Length >= 6)
            {
                score++;
            }

            if (password.Length >= 12)
            {
                score++;
            }

            if (Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
                Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            return (RPasswordStrength)score;
        }
    }
}