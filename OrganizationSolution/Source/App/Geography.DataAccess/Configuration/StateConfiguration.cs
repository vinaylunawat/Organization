namespace Geography.DataAccess.Configuration
{
    using Framework.DataAccess.Configuration;
    using Geography.Entity.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="StateConfiguration" />.
    /// </summary>
    public class StateConfiguration : EntityWithIdCodeNameConfiguration<State>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{State}"/>.</param>
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
