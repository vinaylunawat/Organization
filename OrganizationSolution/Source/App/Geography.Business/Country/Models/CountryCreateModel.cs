namespace Geography.Business.Country.Models
{
    using Framework.Business.Models;
    using Framework.Business.Models.Models;

    public class CountryCreateModel : ModelWithCodeName, IModelWithCode
    {
        public CountryCreateModel()
        {
        }

        public CountryCreateModel(string code, string name,  string isoCode)
            : base(code, name)
        {
            IsoCode = isoCode;
        }

        /// <summary>
        /// Gets or sets the iso code.
        /// </summary>
        /// <value>The iso code.</value>
        public string IsoCode { get; set; }
    }
}
