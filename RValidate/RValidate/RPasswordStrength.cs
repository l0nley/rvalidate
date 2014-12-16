namespace RValidate
{
    /// <summary>
    /// Password strength
    /// </summary>
    public enum RPasswordStrength
    {
        /// <summary>
        /// Password is not acceptable - it is blank!
        /// </summary>
        Blank = 0,

        /// <summary>
        /// Password is very weak - its length less than 4 symbols
        /// </summary>
        VeryWeak = 1,

        /// <summary>
        /// Weak password
        /// </summary>
        Weak = 2,

        /// <summary>
        /// Medium password strength
        /// </summary>
        Medium = 3,

        /// <summary>
        /// Strong password
        /// </summary>
        Strong = 4,

        /// <summary>
        /// Very strong password
        /// </summary>
        VeryStrong = 5
    }
}