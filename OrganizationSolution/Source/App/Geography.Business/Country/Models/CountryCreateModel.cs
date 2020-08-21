namespace Geography.Business.Country.Models
{
    using Framework.Business.Models;
    using Framework.Business.Models.Models;

    /// <summary>
    /// Defines the <see cref="CountryCreateModel" />.
    /// </summary>
    public class CountryCreateModel : ModelWithCodeName, IModelWithCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryCreateModel"/> class.
        /// </summary>
        public CountryCreateModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryCreateModel"/> class.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        public CountryCreateModel(string code, string name, string isoCode)
            : base(code, name)
        {
            IsoCode = isoCode;
        }

        /// <summary>
        /// Gets or sets the iso code..
        /// </summary>
        public string IsoCode { get; set; }
    }
}
