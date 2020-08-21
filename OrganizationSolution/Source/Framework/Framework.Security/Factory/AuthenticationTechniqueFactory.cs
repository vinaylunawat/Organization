namespace Framework.Security.Factory
{
    using Framework.Configuration.Models;
    using Framework.Constant;
    using Framework.Security.Abstraction;
    using Framework.Security.Strategy;
    using Microsoft.Extensions.DependencyInjection;

    public class AuthenticationTechniqueFactory
    {
        public static IAuthentication GetAuthentication(AuthenticationTechnique authenticationTechnique, IServiceCollection services, JwtIssuerOptions jwtOptions, AuthSettings authSettings)
        {
            IAuthentication authentication = null;
            if (authenticationTechnique == AuthenticationTechnique.JWTToken)
            {
                authentication = new JwtTokenAuthentication(services, jwtOptions, authSettings);
            }
            return authentication;
        }
    }
}
