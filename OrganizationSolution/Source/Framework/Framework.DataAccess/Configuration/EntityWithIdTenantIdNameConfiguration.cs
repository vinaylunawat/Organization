namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="EntityWithIdTenantIdNameConfiguration{TEntityWithIdTenantIdName}" />.
    /// </summary>
    /// <typeparam name="TEntityWithIdTenantIdName">.</typeparam>
    public abstract class EntityWithIdTenantIdNameConfiguration<TEntityWithIdTenantIdName> : EntityWithIdTenantIdConfiguration<TEntityWithIdTenantIdName>
        where TEntityWithIdTenantIdName : EntityWithIdTenantIdName
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntityWithIdTenantIdName}"/>.</param>
        public override void Configure(EntityTypeBuilder<TEntityWithIdTenantIdName> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .HasMaxLength(BaseConstants.DataLengths.Name)
                .IsRequired();
        }
    }
}
