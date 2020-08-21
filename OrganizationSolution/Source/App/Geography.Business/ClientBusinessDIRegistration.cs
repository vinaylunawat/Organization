namespace Geography.Business
{
    using Geography.Business.Country;
    using Geography.Business.Country.Manager;
    using Framework.Business;
    using Microsoft.Extensions.DependencyInjection;     

    public static class ClientBusinessDIRegistration
    {
        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CountryMappingProfile).Assembly);
            services.AddManagers(typeof(CountryQueryManager).Assembly);
            return services;
        }         
    }
}
