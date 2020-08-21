namespace Framework.Service
{
    using EnsureThat;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="SwaggerServicesExtensions" />.
    /// </summary>
    public static class SwaggerServicesExtensions
    {
        /// <summary>
        /// Defines the SwaggerSecurityType.
        /// </summary>
        private const string SwaggerSecurityType = "Bearer";

        /// <summary>
        /// The AddSwaggerWithComments.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="apiName">The apiName<see cref="string"/>.</param>
        /// <param name="apiVersion">The apiVersion<see cref="string"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSwaggerWithComments(this IServiceCollection services, string apiName, string apiVersion, IEnumerable<Assembly> assemblies)
        {
            EnsureArg.IsNotNull(assemblies, nameof(assemblies));
            EnsureArg.IsNotNullOrWhiteSpace(apiName, nameof(apiName));
            EnsureArg.IsNotNullOrWhiteSpace(apiVersion, nameof(apiVersion));

            services.AddSwaggerGen(swaggerOptions =>
            {
                var swaggerInfo = new OpenApiInfo
                {
                    Title = apiName,
                    Version = apiVersion,
                    Description = string.Empty,
                    Contact = new OpenApiContact(),
                    License = new OpenApiLicense()
                };

                swaggerOptions.OperationFilter<AddTenantIdHeaderParameter>();
                swaggerOptions.SwaggerDoc(apiVersion, swaggerInfo);

                foreach (var assembly in assemblies)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
                    if (File.Exists(xmlPath))
                    {
                        swaggerOptions.IncludeXmlComments(xmlPath);
                        swaggerOptions.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}{e.ActionDescriptor.RouteValues["action"]}");
                    }
                }

                if (!swaggerOptions.SwaggerGeneratorOptions.SecuritySchemes.ContainsKey(SwaggerSecurityType))
                {

                    swaggerOptions.AddSecurityDefinition(SwaggerSecurityType, new OpenApiSecurityScheme
                    {
                        Name = SwaggerSecurityType,
                        Type = SecuritySchemeType.Http,
                        Scheme = SwaggerSecurityType.ToLower(),
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme.",
                    });
                }

                swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = SwaggerSecurityType,
                                }
                            },
                            new string[] {}
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// The AddSwaggerWithComments.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="apiName">The apiName<see cref="string"/>.</param>
        /// <param name="apiVersion">The apiVersion<see cref="string"/>.</param>
        /// <param name="assembly">The assembly<see cref="Assembly"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="Assembly[]"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSwaggerWithComments(this IServiceCollection services, string apiName, string apiVersion, Assembly assembly, params Assembly[] assemblies)
        {
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return AddSwaggerWithComments(services, apiName, apiVersion, assemblies.Prepend(assembly));
        }
    }
}
