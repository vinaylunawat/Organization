namespace Framework.DataAccess
{
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="ModelBuilderExtensions" />.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// The ConfigureTypes.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <returns>The <see cref="ModelBuilder"/>.</returns>
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

        /// <summary>
        /// The ConfigureTypes.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
        /// <param name="assembly">The assembly<see cref="Assembly"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="Assembly[]"/>.</param>
        /// <returns>The <see cref="ModelBuilder"/>.</returns>
        public static ModelBuilder ConfigureTypes(this ModelBuilder modelBuilder, Assembly assembly, params Assembly[] assemblies)
        {
            EnsureArg.IsNotNull(modelBuilder, nameof(modelBuilder));
            EnsureArg.IsNotNull(assembly, nameof(assembly));

            return ConfigureTypes(modelBuilder, assemblies.Prepend(assembly));
        }
    }
}
