namespace Geography.Business.Country.Models
{
    using Framework.Business.Models;

    /// <summary>
    /// Defines the <see cref="CountryUpdateModel" />.
    /// </summary>
    public class CountryUpdateModel : CountryCreateModel, IModelWithId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryUpdateModel"/> class.
        /// </summary>
        public CountryUpdateModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryUpdateModel"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        public CountryUpdateModel(long id, string code, string name, string isoCode)
            : base(code, name, isoCode)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public long Id { get; set; }
    }
}
