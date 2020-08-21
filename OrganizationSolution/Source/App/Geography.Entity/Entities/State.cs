namespace Geography.Entity.Entities
{
    using Framework.Entity;

    public sealed class State : EntityWithIdCodeName
    {
        public long CountryId { get; set; }

        public Country Country { get; set; }

    }
}
