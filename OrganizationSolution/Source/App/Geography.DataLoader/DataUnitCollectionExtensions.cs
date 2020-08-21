namespace Geography.DataLoader
{
    using EnsureThat;
    using Framework.DataLoader;
    using Geography.DataLoader.DataUnits;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="DataUnitCollectionExtensions" />.
    /// </summary>
    public static class DataUnitCollectionExtensions
    {
        /// <summary>
        /// The AddDataUnits.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDataUnits(this IServiceCollection services)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            services.AddTransient<IDataUnit, CountryDataUnit>();
            return services;
        }
    }
}
