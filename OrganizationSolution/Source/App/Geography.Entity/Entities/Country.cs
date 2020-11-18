namespace Geography.Entity.Entities
{
    using Framework.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Country" />.
    /// </summary>
    public class Country : EntityWithIdCodeName
    {
        /// <summary>
        /// Gets or sets the IsoCode.
        /// </summary>
        public string IsoCode { get; set; }

        /// <summary>
        /// Gets or sets the States.
        /// </summary>
        public IList<State> States { get; set; }

        public string Test { get; set; }
    }
}
