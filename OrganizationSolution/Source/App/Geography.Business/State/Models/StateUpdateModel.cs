namespace Geography.Business.Business.State.Models
{
    using Framework.Business.Models;

    /// <summary>
    /// Defines the <see cref="StateUpdateModel" />.
    /// </summary>
    public class StateUpdateModel : StateCreateModel, IModelWithId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateUpdateModel"/> class.
        /// </summary>
        public StateUpdateModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateUpdateModel"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="countryCode">The countryCode<see cref="string"/>.</param>
        public StateUpdateModel(long id, string code, string name, string countryCode)
            : base(code, name, countryCode)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public long Id { get; set; }
    }
}
