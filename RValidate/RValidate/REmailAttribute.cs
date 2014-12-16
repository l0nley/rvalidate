namespace RValidate
{
    /// <summary>
    /// Validate, that stringified projection of property is valid email address
    /// </summary>
    public class REmailAddressAttribute : RRegularExpressionAttribute
    {
        /// <summary>
        /// Create new instance of <see cref="REmailAddressAttribute"/>
        /// </summary>
        /// <param name="methods">Methods, with which validation will be performed</param>
        public REmailAddressAttribute(params RHttpMethods[] methods)
            : base(methods)
        {
            ErrorMessage = "{0} not looks like e-mail address";
            ValidationPattern =
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
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
    }
}