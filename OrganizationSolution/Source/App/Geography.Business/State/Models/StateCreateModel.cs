namespace Geography.Business.Business.State.Models
{
    using Framework.Business.Models;
    using Framework.Business.Models.Models;

    /// <summary>
    /// Defines the <see cref="StateCreateModel" />.
    /// </summary>
    public class StateCreateModel : ModelWithCodeName, IModelWithCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateCreateModel"/> class.
        /// </summary>
        public StateCreateModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateCreateModel"/> class.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="countryCode">The countryCode<see cref="string"/>.</param>
        public StateCreateModel(string code, string name, string countryCode)
            : base(code, name)
        {
            CountryCode = countryCode;
        }

        /// <summary>
        /// Gets or sets the country identifier..
        /// </summary>
        public string CountryCode { get; set; }
    }
}
