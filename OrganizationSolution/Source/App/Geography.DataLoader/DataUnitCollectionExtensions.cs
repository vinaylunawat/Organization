namespace Geography.DataLoader
{
    using Geography.DataLoader.DataUnits;
    using Framework.DataLoader;
    using EnsureThat;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataUnitCollectionExtensions
    {
        public static IServiceCollection AddDataUnits(this IServiceCollection services)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            services.AddTransient<IDataUnit, CountryDataUnit>();
            return services;
        }
    }
}
