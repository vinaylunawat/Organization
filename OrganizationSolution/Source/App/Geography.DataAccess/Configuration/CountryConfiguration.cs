namespace Geography.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.DataAccess.Configuration;
    using Geography.Entity.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="CountryConfiguration" />.
    /// </summary>
    public class CountryConfiguration : EntityWithIdCodeNameConfiguration<Country>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{Country}"/>.</param>
        public override void Configure(EntityTypeBuilder<Country> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            entityTypeBuilder.Property(x => x.IsoCode)
                .HasMaxLength(BaseConstants.DataLengths.Code)
                .IsRequired();
        }
    }
}
