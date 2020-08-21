namespace Framework.Security
{
    using Framework.Configuration.Models;
    using Framework.Security.Factory;
    using Framework.Constant;
    using Microsoft.Extensions.DependencyInjection;    

    public static class ClientSecurityDIRegistration
    {
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
