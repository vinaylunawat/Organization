namespace Framework.DataAccess.Configuration
{
    using Framework.Entity;
    using System.Linq;    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            foreach (var foreignKey in entityTypeBuilder.Metadata.GetForeignKeys().Where(fk => !fk.IsOwnership))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
