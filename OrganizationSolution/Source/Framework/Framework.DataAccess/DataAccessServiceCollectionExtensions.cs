namespace Framework.DataAccess
{
    using EnsureThat;
    using Framework.DataAccess.Repository;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class DataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(assembly, nameof(assembly));
            return AddRepositories(services, new[] { assembly }, serviceLifetime);
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            EnsureArg.IsNotNull(services, nameof(services));            
            return services.RegisterRepositories(assemblies, serviceLifetime);
        }
        private static IServiceCollection RegisterRepositories(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime serviceLifetime)
        {
            var repositories = assemblies.SelectMany(x => x.ExportedTypes
                .Where(t => typeof(IRepositoryBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));

            foreach (var repository in repositories)
            {
                var interfaceName = repository.GetInterface("I" + repository.Name);

                switch (serviceLifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceName, repository);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceName, repository);
                        break;

                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceName, repository);
                        break;

                    default:
                        throw new InvalidOperationException("The service lifetime provided while registering repository is not supported.");
                }
            }

            return services;
        }
    }
}
