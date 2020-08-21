namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="BaseEntityConfiguration{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="entityTypeBuilder">The entityTypeBuilder<see cref="EntityTypeBuilder{TEntity}"/>.</param>
        public virtual void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            foreach (var foreignKey in entityTypeBuilder.Metadata.GetForeignKeys().Where(fk => !fk.IsOwnership))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
