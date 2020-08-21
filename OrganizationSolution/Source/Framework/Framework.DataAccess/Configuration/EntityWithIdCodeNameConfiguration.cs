namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="EntityWithIdCodeNameConfiguration{TEntityWithIdCodeName}" />.
    /// </summary>
    /// <typeparam name="TEntityWithIdCodeName">.</typeparam>
    public abstract class EntityWithIdCodeNameConfiguration<TEntityWithIdCodeName> : EntityWithIdCodeConfiguration<TEntityWithIdCodeName>
        where TEntityWithIdCodeName : EntityWithIdCodeName
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntityWithIdCodeName}"/>.</param>
        public override void Configure(EntityTypeBuilder<TEntityWithIdCodeName> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .HasMaxLength(BaseConstants.DataLengths.Name)
                .IsRequired();
        }
    }
}
