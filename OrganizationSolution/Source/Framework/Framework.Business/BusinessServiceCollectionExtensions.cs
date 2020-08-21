

namespace Framework.Business
{
    using Framework.Business.Extension;
    using Framework.Business.Manager;
    using Framework.Configuration;
    using EnsureThat;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(assemblies, nameof(assemblies));

            var updatedServices = AutoMapper.ServiceCollectionExtensions.AddAutoMapper(
                services,
                (config) =>
                {
                    config.DisableConstructorMapping();
                },
                assemblies);

            if (ApplicationConfiguration.IsDevelopment)
            {
                var mapperConfiguration = updatedServices.BuildServiceProvider().GetRequiredService<AutoMapper.IConfigurationProvider>();
                mapperConfiguration.AssertConfigurationIsValid();
            }

            return updatedServices;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Assembly assembly)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return AddAutoMapper(services, new[] { assembly });
        }

        public static IServiceCollection AddManagers(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArgExtensions.HasItems(assemblies, nameof(assemblies));

            return services.RegisterValidators(assemblies, serviceLifetime).RegisterManagers(assemblies, serviceLifetime);
        }

        public static IServiceCollection AddManagers(this IServiceCollection services, Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return AddManagers(services, new[] { assembly }, serviceLifetime);
        }       

        private static IServiceCollection RegisterManagers(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime serviceLifetime)
        {
            var managers = assemblies.SelectMany(x => x.ExportedTypes
                .Where(t => typeof(IManagerBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));

            foreach (var manager in managers)
            {
                var interfaceName = manager.GetInterface("I" + manager.Name);

                switch (serviceLifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceName, manager);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceName, manager);
                        break;

                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceName, manager);
                        break;

                    default:
                        throw new InvalidOperationException("The service lifetime provided while registering managers is not supported.");
                }
            }

            return services;
        }

        private static IServiceCollection RegisterValidators(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime serviceLifetime)
        {
            var validators = assemblies.SelectMany(x => x.ExportedTypes
                .Where(t => typeof(IValidator).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));

            foreach (var validator in validators)
            {
                switch (serviceLifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(validator);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(validator);
                        break;

                    case ServiceLifetime.Transient:
                        services.AddTransient(validator);
                        break;

                    default:
                        throw new InvalidOperationException("The service lifetime provided while registering managers is not supported.");
                }
            }

            return services;
        }

        
    }
}
