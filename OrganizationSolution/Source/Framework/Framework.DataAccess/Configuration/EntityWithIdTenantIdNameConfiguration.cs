namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityWithIdTenantIdNameConfiguration<TEntityWithIdTenantIdName> : EntityWithIdTenantIdConfiguration<TEntityWithIdTenantIdName>
        where TEntityWithIdTenantIdName : EntityWithIdTenantIdName
    {
        public override void Configure(EntityTypeBuilder<TEntityWithIdTenantIdName> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .HasMaxLength(BaseConstants.DataLengths.Name)
                .IsRequired();
        }
    }
}
