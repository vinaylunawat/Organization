namespace Geography.DataAccess
{
    using DataAccess.Repository;
    using Framework.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="ClientDbDIRegistration" />.
    /// </summary>
    public static class ClientDbDIRegistration
    {
        /// <summary>
        /// Configures the client db context services.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the client services to.</param>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        /// <param name="readOnlyConnectionString">The readOnlyConnectionString<see cref="string"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureDbServices(this IServiceCollection services, string connectionString, string readOnlyConnectionString)
        {
            services.AddDbContext<GeographyDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            services.AddDbContext<GeographyReadOnlyDbContext>(options => options.UseSqlServer(readOnlyConnectionString), ServiceLifetime.Scoped);
            services.AddRepositories(typeof(CountryQueryRepository).Assembly);
            return services;
        }
    }
}
