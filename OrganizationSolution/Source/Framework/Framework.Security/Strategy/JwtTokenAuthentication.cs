namespace Framework.Security.Strategy
{
    using Framework.Configuration.Models;
    using Framework.Security.Abstraction;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class JwtTokenAuthentication : IAuthentication
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly AuthSettings _authSettings;

        public JwtTokenAuthentication(IServiceCollection serviceCollection, JwtIssuerOptions jwtOptions, AuthSettings authSettings)
        {
            _serviceCollection = serviceCollection;
            _jwtOptions = jwtOptions;
            _authSettings = authSettings;
            Initialize();
        }

        private void Initialize()
        {
            _serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.ClaimsIssuer = _jwtOptions.Issuer;
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtOptions.Issuer,
                        
                        ValidateAudience = true,
                        ValidAudience = _jwtOptions.Audience,
                        
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey)),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = false,                        
                    };
                    jwtBearerOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
