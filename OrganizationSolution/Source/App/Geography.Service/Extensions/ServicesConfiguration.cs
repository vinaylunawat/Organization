namespace Geography.Service
{
    using Geography.Business;
    using Geography.Business.Country.Models;
    using Geography.DataAccess;
    using Framework.Configuration.Models;
    using Framework.Security;
    using Framework.Security.Factory;
    using Microsoft.Extensions.DependencyInjection;
    using Framework.Constant;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Framework.Service;

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services)
        {
            var v = string.Empty;
            var serviceProvider = services.BuildServiceProvider();            
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
            return services
                .ConfigureDbServices(applicationOptions.ConnectionString, applicationOptions.ReadOnlyConnectionString)
                .ConfigureBusinessServices();
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            var swaggerAssemblies = new[] { typeof(Program).Assembly, typeof(CountryCreateModel).Assembly, typeof(Model).Assembly };
            services.AddSwaggerWithComments(ApiConstants.ApiName, ApiConstants.ApiVersion, swaggerAssemblies);
            services.AddSwaggerWithComments(ApiConstants.JobsApiName, ApiConstants.JobsApiVersion, swaggerAssemblies);
            return services;
        }
        public static IServiceCollection ConfigureJwtSecurity(this IServiceCollection services)
        {
            services.ConfigureSecurityServices();
            var serviceProvider = services.BuildServiceProvider();
            var securityOptions = serviceProvider.GetRequiredService<SecurityOptions>();
            AuthenticationTechniqueFactory.GetAuthentication(AuthenticationTechnique.JWTToken, services, securityOptions.JwtIssuerOptions, securityOptions.AuthSettings);
            return services;
        }
    }
}
