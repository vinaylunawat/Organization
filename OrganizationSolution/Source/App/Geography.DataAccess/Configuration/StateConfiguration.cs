namespace Geography.DataAccess.Configuration
{
    using Geography.Entity.Entities;
    using Framework.DataAccess.Configuration;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StateConfiguration : EntityWithIdCodeNameConfiguration<State>
    {
        public override void Configure(EntityTypeBuilder<State> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.HasOne(x => x.Country)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.CountryId)
                .IsRequired();
        }
    }
}
