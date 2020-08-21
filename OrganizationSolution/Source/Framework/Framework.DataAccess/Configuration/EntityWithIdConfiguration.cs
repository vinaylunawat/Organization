namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityWithIdConfiguration<TEntityWithId> : AuditableEntityConfiguration<TEntityWithId>
        where TEntityWithId : EntityWithId
    {
        public override void Configure(EntityTypeBuilder<TEntityWithId> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Id);
        }
    }
}
