namespace Geography.Business.Business.State.Models
{
    using Framework.Business.Models;
    using System;    

    public class StateReadModel : StateUpdateModel, IModelWithId
    {
        public StateReadModel()
        {
        }

        public StateReadModel(long id, string code, string name, string countryCode)
            : base(id, code, name, countryCode)
        {            
        }        
    }
}
