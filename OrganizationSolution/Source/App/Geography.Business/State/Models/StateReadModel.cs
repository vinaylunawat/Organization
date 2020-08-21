namespace Geography.Business.Business.State.Models
{
    using Framework.Business.Models;

    /// <summary>
    /// Defines the <see cref="StateReadModel" />.
    /// </summary>
    public class StateReadModel : StateUpdateModel, IModelWithId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateReadModel"/> class.
        /// </summary>
        public StateReadModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateReadModel"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="countryCode">The countryCode<see cref="string"/>.</param>
        public StateReadModel(long id, string code, string name, string countryCode)
            : base(id, code, name, countryCode)
        {
        }
    }
}
