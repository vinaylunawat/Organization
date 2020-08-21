namespace Framework.Test
{
    using EnsureThat;
    using Framework.Business.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="BaseTest{TFixture}" />.
    /// </summary>
    /// <typeparam name="TFixture">.</typeparam>
    public abstract class BaseTest<TFixture> : IClassFixture<TFixture>
        where TFixture : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest{TFixture}"/> class.
        /// </summary>
        /// <param name="testFixture">The testFixture<see cref="ITestFixture{TFixture}"/>.</param>
        protected BaseTest(ITestFixture<TFixture> testFixture)
        {
            EnsureArg.IsNotNull(testFixture, nameof(testFixture));

            TestFixture = testFixture;
            var loggerFactory = testFixture.ServiceProvider.GetRequiredService<ILoggerFactory>();
            Logger = loggerFactory.CreateLogger(GetType());

            Logger.LogInformation($"Test Class {GetType().Name} is intialized.");
        }

        /// <summary>
        /// Gets the TestFixture.
        /// </summary>
        protected ITestFixture<TFixture> TestFixture { get; }

        /// <summary>
        /// Gets the Logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// The AllModelsInTheCurrentNamespaceShouldSerialize.
        /// </summary>
        [Fact]
        public void AllModelsInTheCurrentNamespaceShouldSerialize()
        {
            var currentNamespace = typeof(BaseTest<>).Namespace;
            var prefix = currentNamespace.Substring(0, currentNamespace.IndexOf('.', StringComparison.OrdinalIgnoreCase));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.GetName().Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
            var modelTypes = assemblies.SelectMany(x => x.ExportedTypes
                .Where(t => typeof(IModel).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)).ToList();

            foreach (var type in modelTypes)
            {
                try
                {
                    var instance = Activator.CreateInstance(type);

                    var newtonSerialized = JsonConvert.SerializeObject(instance);
                    var newtonDeserialized = JsonConvert.DeserializeObject(newtonSerialized, type);

                    var xmlSerializer = new XmlSerializer(type);
                    var stringBuilder = new StringBuilder();

                    using (TextWriter writer = new StringWriter(stringBuilder))
                    {
                        xmlSerializer.Serialize(writer, instance);
                    }

                    var xmlSerialized = stringBuilder.ToString();
                    object output = null;
                    using (TextReader reader = new StringReader(xmlSerialized))
                    {
                        output = xmlSerializer.Deserialize(reader);
                    }

                    var xmlDeserialized = Convert.ChangeType(output, type, CultureInfo.InvariantCulture);

                    AssertExtensions.Equivalent(instance, newtonDeserialized);
                    AssertExtensions.Equivalent(instance, xmlDeserialized);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to serialized type {type}", ex);
                }
            }
        }
    }
}
