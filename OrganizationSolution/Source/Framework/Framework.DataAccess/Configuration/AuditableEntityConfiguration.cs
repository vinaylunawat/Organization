namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public abstract class AuditableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
        where TEntity : AuditableEntity
    {
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
