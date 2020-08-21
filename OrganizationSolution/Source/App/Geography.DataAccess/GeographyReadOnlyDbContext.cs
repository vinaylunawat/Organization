namespace Geography.DataAccess
{
    using Framework.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class GeographyReadOnlyDbContext : BaseReadOnlyDbContext<GeographyReadOnlyDbContext>
    {

        public GeographyReadOnlyDbContext(DbContextOptions<GeographyReadOnlyDbContext> options)
            : base(options)
        {
        }

        public override string SchemaName => GeographyDbContext.DefaultSchemaName;

        public override string MigrationTableName => GeographyDbContext.DefaultMigrationTableName;

        public override Assembly[] GetTypeAssemblies()
        {
            return new[] { typeof(GeographyReadOnlyDbContext).Assembly };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
