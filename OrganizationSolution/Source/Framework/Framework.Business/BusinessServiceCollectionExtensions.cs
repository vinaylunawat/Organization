namespace Framework.Business
{
    using EnsureThat;
    using FluentValidation;
    using Framework.Business.Extension;
    using Framework.Business.Manager;
    using Framework.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="BusinessServiceCollectionExtensions" />.
    /// </summary>
    public static class BusinessServiceCollectionExtensions
    {
        /// <summary>
        /// The AddAutoMapper.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
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

        /// <summary>
        /// The AddAutoMapper.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="assembly">The assembly<see cref="Assembly"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Assembly assembly)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return AddAutoMapper(services, new[] { assembly });
        }

        /// <summary>
        /// The AddManagers.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <param name="serviceLifetime">The serviceLifetime<see cref="ServiceLifetime"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddManagers(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArgExtensions.HasItems(assemblies, nameof(assemblies));

            return services.RegisterValidators(assemblies, serviceLifetime).RegisterManagers(assemblies, serviceLifetime);
        }

        /// <summary>
        /// The AddManagers.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="assembly">The assembly<see cref="Assembly"/>.</param>
        /// <param name="serviceLifetime">The serviceLifetime<see cref="ServiceLifetime"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddManagers(this IServiceCollection services, Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return AddManagers(services, new[] { assembly }, serviceLifetime);
        }

        /// <summary>
        /// The RegisterManagers.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <param name="serviceLifetime">The serviceLifetime<see cref="ServiceLifetime"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
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

        /// <summary>
        /// The RegisterValidators.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <param name="serviceLifetime">The serviceLifetime<see cref="ServiceLifetime"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
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
