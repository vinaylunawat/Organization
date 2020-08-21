namespace Framework.Security.Factory
{
    using Framework.Configuration.Models;
    using Framework.Constant;
    using Framework.Security.Abstraction;
    using Framework.Security.Strategy;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="AuthenticationTechniqueFactory" />.
    /// </summary>
    public class AuthenticationTechniqueFactory
    {
        /// <summary>
        /// The GetAuthentication.
        /// </summary>
        /// <param name="authenticationTechnique">The authenticationTechnique<see cref="AuthenticationTechnique"/>.</param>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="jwtOptions">The jwtOptions<see cref="JwtIssuerOptions"/>.</param>
        /// <param name="authSettings">The authSettings<see cref="AuthSettings"/>.</param>
        /// <returns>The <see cref="IAuthentication"/>.</returns>
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
