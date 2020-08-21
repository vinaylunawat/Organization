namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityWithIdCodeConfiguration<TEntityWithIdCode> : EntityWithIdConfiguration<TEntityWithIdCode>
        where TEntityWithIdCode : EntityWithIdCode
    {
        public override void Configure(EntityTypeBuilder<TEntityWithIdCode> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Code)
                .HasMaxLength(BaseConstants.DataLengths.Code)
                .IsRequired();

            entityTypeBuilder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
