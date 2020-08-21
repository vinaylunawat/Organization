namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public abstract class AbstractAppBuilder
    {
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
