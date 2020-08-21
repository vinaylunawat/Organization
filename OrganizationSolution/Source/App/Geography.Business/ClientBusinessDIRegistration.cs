namespace Geography.Business
{
    using Framework.Business;
    using Geography.Business.Country;
    using Geography.Business.Country.Manager;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="ClientBusinessDIRegistration" />.
    /// </summary>
    public static class ClientBusinessDIRegistration
    {
        /// <summary>
        /// The ConfigureBusinessServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CountryMappingProfile).Assembly);
            services.AddManagers(typeof(CountryQueryManager).Assembly);
            return services;
        }
    }
}
