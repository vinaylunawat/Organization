namespace Geography.Entity.Entities
{
    using Framework.Entity;

    /// <summary>
    /// Defines the <see cref="State" />.
    /// </summary>
    public sealed class State : EntityWithIdCodeName
    {
        /// <summary>
        /// Gets or sets the CountryId.
        /// </summary>
        public long CountryId { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public Country Country { get; set; }
    }
}
