namespace Framework.Configuration
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AbstractAppBuilder" />.
    /// </summary>
    public abstract class AbstractAppBuilder
    {
        /// <summary>
        /// The ConfigureApp.
        /// </summary>
        /// <param name="hostingEnvironmentName">The hostingEnvironmentName<see cref="string"/>.</param>
        /// <param name="configurationBuilder">The configurationBuilder<see cref="IConfigurationBuilder"/>.</param>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/>.</param>
        /// <param name="configurationOptions">The configurationOptions<see cref="List{IConfigurationOptions}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        public virtual void ConfigureApp(string hostingEnvironmentName, IConfigurationBuilder configurationBuilder, IEnumerable<Type> types, List<IConfigurationOptions> configurationOptions, string[] args = null)
        {
            configurationBuilder.AddAppConfiguration(hostingEnvironmentName, args);
            var configurationRoot = configurationBuilder.Build();
            foreach (var optionType in types)
            {
                var instance = (ConfigurationOptions)Activator.CreateInstance(optionType);
                configurationRoot.GetSection(instance.SectionName).Bind(instance);
                configurationOptions.Add(instance);
            }
        }
    }
}
