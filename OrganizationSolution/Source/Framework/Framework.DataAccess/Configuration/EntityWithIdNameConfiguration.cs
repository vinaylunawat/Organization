namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityWithIdNameConfiguration<TEntityWithIdName> : EntityWithIdConfiguration<TEntityWithIdName>
        where TEntityWithIdName : EntityWithIdName
    {
        public override void Configure(EntityTypeBuilder<TEntityWithIdName> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .HasMaxLength(BaseConstants.DataLengths.Name)
                .IsRequired();
        }
    }
}
