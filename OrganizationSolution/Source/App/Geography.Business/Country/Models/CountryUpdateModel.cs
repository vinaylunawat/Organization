namespace Geography.Business.Country.Models
{
    using Framework.Business.Models;

    public class CountryUpdateModel : CountryCreateModel, IModelWithId
    {
        public CountryUpdateModel()
        {
        }

        public CountryUpdateModel(long id, string code, string name, string isoCode)
            : base(code, name, isoCode)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }
    }
}
