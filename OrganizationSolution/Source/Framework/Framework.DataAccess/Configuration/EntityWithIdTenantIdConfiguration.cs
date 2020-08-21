namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="EntityWithIdTenantIdConfiguration{TEntityWithIdTenantId}" />.
    /// </summary>
    /// <typeparam name="TEntityWithIdTenantId">.</typeparam>
    public abstract class EntityWithIdTenantIdConfiguration<TEntityWithIdTenantId> : EntityWithIdConfiguration<TEntityWithIdTenantId>
        where TEntityWithIdTenantId : EntityWithIdTenantId
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntityWithIdTenantId}"/>.</param>
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
