namespace Framework.Security
{
    using Framework.Configuration.Models;
    using Framework.Constant;
    using Framework.Security.Factory;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="ClientSecurityDIRegistration" />.
    /// </summary>
    public static class ClientSecurityDIRegistration
    {
        /// <summary>
        /// The ConfigureSecurityServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureSecurityServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var securityOptions = serviceProvider.GetRequiredService<SecurityOptions>();
            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddTransient<IJwtFactory>((serviceprovider) =>
            {
                var jwtTokenHandler = serviceprovider.GetRequiredService<IJwtTokenHandler>();
                return new JwtFactory(jwtTokenHandler, securityOptions.JwtIssuerOptions, securityOptions.AuthSettings);
            });

            AuthenticationTechniqueFactory.GetAuthentication(AuthenticationTechnique.JWTToken, services, securityOptions.JwtIssuerOptions, securityOptions.AuthSettings);
            return services;
        }
    }
}
