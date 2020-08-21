namespace Geography.Business.Country
{
    /// <summary>
    /// Defines the CountryErrorCode.
    /// </summary>
    public enum CountryErrorCode
    {
        /// <summary>
        /// Unknown error
        /// </summary>
        UnknownError = 0,
        /// <summary>
        /// The identifier must be greater than zero
        /// </summary>
        IdMustBeGreaterThanZero,
        /// <summary>
        /// The identifier does not exist
        /// </summary>
        IdDoesNotExist,
        /// <summary>
        /// The iso code required
        /// </summary>
        IsoCodeRequired,
        /// <summary>
        /// The iso code too long
        /// </summary>
        IsoCodeTooLong,
        /// <summary>
        /// Iso Code not unique
        /// </summary>
        IsoCodeNotUnique,
        /// <summary>
        /// State is Attached to this country
        /// </summary>
        StateAttached,
        /// <summary>
        /// The name is required
        /// </summary>
        NameRequired,
        /// <summary>
        /// The name is too long
        /// </summary>
        NameTooLong,
        /// <summary>
        /// Id not unique
        /// </summary>
        IdNotUnique,

        /// <summary>
        /// Defines the CodeRequired.
        /// </summary>
        CodeRequired,

        /// <summary>
        /// Defines the CodeTooLong.
        /// </summary>
        CodeTooLong,

        /// <summary>
        /// Defines the DescriptionRequired.
        /// </summary>
        DescriptionRequired,

        /// <summary>
        /// Defines the DescriptionTooLong.
        /// </summary>
        DescriptionTooLong,

        /// <summary>
        /// Defines the CodeNotUnique.
        /// </summary>
        CodeNotUnique
    }
}
