namespace Geography.DataAccess
{
    using Framework.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class GeographyDbContext : BaseDbContext<GeographyDbContext>
    {
        public const string DefaultSchemaName = "Geography";

        public const string DefaultMigrationTableName = "Geography_migrations";        

        public GeographyDbContext(DbContextOptions<GeographyDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public override string SchemaName
        {
            get { return DefaultSchemaName; }
        }

        public override string MigrationTableName
        {
            get { return DefaultMigrationTableName; }
        }

        public override Assembly[] GetTypeAssemblies()
        {
            return new[] { typeof(GeographyDbContext).Assembly };
        }
    }
}
