namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityWithIdTenantIdConfiguration<TEntityWithIdTenantId> : EntityWithIdConfiguration<TEntityWithIdTenantId>
        where TEntityWithIdTenantId : EntityWithIdTenantId
    {
        public override void Configure(EntityTypeBuilder<TEntityWithIdTenantId> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.TenantId)
                .ValueGeneratedNever()
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);                                
        }
    }
}
