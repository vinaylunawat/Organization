namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// Defines the <see cref="AuditableEntityConfiguration{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class AuditableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
        where TEntity : AuditableEntity
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntity}"/>.</param>
        public override void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            // shadow property
            entityTypeBuilder.Property<DateTime>(AuditableEntity.CreateDateTimePropertyName)
                .ValueGeneratedNever()
                .IsRequired();

            // shadow property
            entityTypeBuilder.Property<DateTime?>(AuditableEntity.UpdateDateTimePropertyName)
                .ValueGeneratedNever()
                .IsRequired(false);
        }
    }
}
