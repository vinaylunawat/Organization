namespace Framework.DataAccess
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class BaseDbContext<T> : DbContext
        where T : DbContext
    {
        public BaseDbContext(DbContextOptions<T> options)
            : base(options)
        {
        }

        public abstract string SchemaName { get; }

        public abstract string MigrationTableName { get; }

        public abstract Assembly[] GetTypeAssemblies();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditableProperties();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetAuditableProperties();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            SetAuditableProperties();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditableProperties();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName)
                .ConfigureTypes(GetTypeAssemblies());
        }

        private void SetAuditableProperties()
        {
            // get entries that are being Added or Updated
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.Entity is AuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditableEntity;

                var now = DateTime.UtcNow;
                entry.Property(AuditableEntity.UpdateDateTimePropertyName).CurrentValue = now;
                if (entry.State == EntityState.Added)
                {
                    entry.Property(AuditableEntity.CreateDateTimePropertyName).CurrentValue = now;
                }
            }
        }
    }
}
