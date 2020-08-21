namespace Geography.Business.Business.State.Models
{
    using Framework.Business.Models;    

    public class StateUpdateModel : StateCreateModel,IModelWithId
    {
        public StateUpdateModel()
        {
        }

        public StateUpdateModel(long id, string code, string name, string countryCode)
            : base(code, name, countryCode)
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
