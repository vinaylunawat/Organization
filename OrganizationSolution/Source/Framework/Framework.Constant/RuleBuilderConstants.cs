namespace Framework.Constant
{
    /// <summary>
    /// Defines the <see cref="RuleBuilderConstants" />.
    /// </summary>
    public static class RuleBuilderConstants
    {
        /// <summary>
        /// Defines the EmailValidationMessage.
        /// </summary>
        public const string EmailValidationMessage = "The specified email is not in the form required for an e-mail address.";

        /// <summary>
        /// Defines the EmailValidationRegex.
        /// </summary>
        public const string EmailValidationRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    }
}
