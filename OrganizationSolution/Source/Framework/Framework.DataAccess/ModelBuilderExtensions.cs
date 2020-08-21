namespace Framework.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;    

    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ConfigureTypes(this ModelBuilder modelBuilder, IEnumerable<Assembly> assemblies)
        {
            EnsureArg.IsNotNull(modelBuilder, nameof(modelBuilder));
            EnsureArg.IsNotNull(assemblies, nameof(assemblies));

            foreach (var assembly in assemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }

            return modelBuilder;
        }

        public static ModelBuilder ConfigureTypes(this ModelBuilder modelBuilder, Assembly assembly, params Assembly[] assemblies)
        {
            EnsureArg.IsNotNull(modelBuilder, nameof(modelBuilder));
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return ConfigureTypes(modelBuilder, assemblies.Prepend(assembly));
        }        
    }
}
