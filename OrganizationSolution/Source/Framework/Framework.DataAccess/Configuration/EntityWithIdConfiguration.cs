namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="EntityWithIdConfiguration{TEntityWithId}" />.
    /// </summary>
    /// <typeparam name="TEntityWithId">.</typeparam>
    public abstract class EntityWithIdConfiguration<TEntityWithId> : AuditableEntityConfiguration<TEntityWithId>
        where TEntityWithId : EntityWithId
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntityWithId}"/>.</param>
        public override void Configure(EntityTypeBuilder<TEntityWithId> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Id);
        }
    }
}
