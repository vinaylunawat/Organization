namespace Geography.Business.Business.State.Models
{
    using Framework.Business.Models;
    using Framework.Business.Models.Models;

    public class StateCreateModel : ModelWithCodeName, IModelWithCode
    {
        public StateCreateModel()
        {
        }

        public StateCreateModel(string code, string name, string countryCode)
            : base(code, name)
        {
            CountryCode = countryCode;
        }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>The country identifier.</value>
        public string CountryCode { get; set; }
    }
}
