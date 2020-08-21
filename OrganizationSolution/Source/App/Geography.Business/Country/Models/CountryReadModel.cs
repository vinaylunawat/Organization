namespace Geography.Business.Country.Models
{
    using Framework.Business.Models;

    /// <summary>
    /// Defines the <see cref="CountryReadModel" />.
    /// </summary>
    public class CountryReadModel : CountryUpdateModel, IModelWithId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryReadModel"/> class.
        /// </summary>
        public CountryReadModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryReadModel"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        public CountryReadModel(long id, string code, string name, string isoCode)
            : base(id, code, name, isoCode)
        {
        }
    }
}
