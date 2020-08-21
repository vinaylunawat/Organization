namespace Framework.DataAccess.Configuration
{
    using Framework.Constant;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="EntityWithIdCodeConfiguration{TEntityWithIdCode}" />.
    /// </summary>
    /// <typeparam name="TEntityWithIdCode">.</typeparam>
    public abstract class EntityWithIdCodeConfiguration<TEntityWithIdCode> : EntityWithIdConfiguration<TEntityWithIdCode>
        where TEntityWithIdCode : EntityWithIdCode
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntityWithIdCode}"/>.</param>
        public override void Configure(EntityTypeBuilder<TEntityWithIdCode> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Code)
                .HasMaxLength(BaseConstants.DataLengths.Code)
                .IsRequired();

            entityTypeBuilder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
