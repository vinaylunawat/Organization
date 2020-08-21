namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="EntityWithIdNameConfiguration{TEntityWithIdName}" />.
    /// </summary>
    /// <typeparam name="TEntityWithIdName">.</typeparam>
    public abstract class EntityWithIdNameConfiguration<TEntityWithIdName> : EntityWithIdConfiguration<TEntityWithIdName>
        where TEntityWithIdName : EntityWithIdName
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntityWithIdName}"/>.</param>
        public override void Configure(EntityTypeBuilder<TEntityWithIdName> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .HasMaxLength(BaseConstants.DataLengths.Name)
                .IsRequired();
        }
    }
}
