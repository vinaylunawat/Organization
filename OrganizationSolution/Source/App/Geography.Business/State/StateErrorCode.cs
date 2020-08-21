namespace Geography.Business.Business.State
{
    /// <summary>
    /// Defines the StateErrorCode.
    /// </summary>
    public enum StateErrorCode
    {
        /// <summary>
        /// Unknown error
        /// </summary>
        UnknownError = 0,
        /// <summary>
        /// State name is required
        /// </summary>
        NameRequired,
        /// <summary>
        /// State name is too long
        /// </summary>
        NameTooLong,
        /// <summary>
        /// Code required
        /// </summary>
        CodeRequired,
        /// <summary>
        /// Code too long
        /// </summary>
        CodeTooLong,
        /// <summary>
        /// Code not unique
        /// </summary>
        CodeNotUnique,
        /// <summary>
        /// Country Code is required
        /// </summary>
        CountryCodeRequired,
        /// <summary>
        /// Country Code does not exist
        /// </summary>
        CountryCodeDoesNotExist,
        /// <summary>
        /// Id Must Be Greater Than Zero
        /// </summary>
        IdMustBeGreaterThanZero,
        /// <summary>
        /// Id does not exists
        /// </summary>
        IdDoesNotExist,
        /// <summary>
        /// Id not unique
        /// </summary>
        IdNotUnique
    }
}
