namespace Geography.DataAccess.Configuration
{
    using Geography.Entity.Entities;
    using Framework.DataAccess.Configuration;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Framework.Constant;

    public class CountryConfiguration : EntityWithIdCodeNameConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            entityTypeBuilder.Property(x => x.IsoCode)
                .HasMaxLength(BaseConstants.DataLengths.Code)
                .IsRequired();
        }
    }
}
