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

    /// <summary>
    /// Defines the <see cref="BaseDbContext{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public abstract class BaseDbContext<T> : DbContext
        where T : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext{T}"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{T}"/>.</param>
        public BaseDbContext(DbContextOptions<T> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the SchemaName.
        /// </summary>
        public abstract string SchemaName { get; }

        /// <summary>
        /// Gets the MigrationTableName.
        /// </summary>
        public abstract string MigrationTableName { get; }

        /// <summary>
        /// The GetTypeAssemblies.
        /// </summary>
        /// <returns>The <see cref="Assembly[]"/>.</returns>
        public abstract Assembly[] GetTypeAssemblies();

        /// <summary>
        /// The SaveChangesAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditableProperties();

            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// The SaveChangesAsync.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">The acceptAllChangesOnSuccess<see cref="bool"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetAuditableProperties();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// The SaveChanges.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int SaveChanges()
        {
            SetAuditableProperties();

            return base.SaveChanges();
        }

        /// <summary>
        /// The SaveChanges.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">The acceptAllChangesOnSuccess<see cref="bool"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditableProperties();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// The OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName)
                .ConfigureTypes(GetTypeAssemblies());
        }

        /// <summary>
        /// The SetAuditableProperties.
        /// </summary>
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
