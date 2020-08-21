namespace Geography.Entity.Entities
{
    using Framework.Entity;
    using System.Collections.Generic;

    public class Country : EntityWithIdCodeName
    {
        public string IsoCode { get; set; }

        public IList<State> States { get; set; }        
    }     
}
