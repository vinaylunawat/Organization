namespace Framework.DataLoader
{
    using Framework.Configuration;

    /// <summary>
    /// Defines the <see cref="DataLoaderOptions" />.
    /// </summary>
    public class DataLoaderOptions : ConfigurationOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether IncludeDemoData.
        /// </summary>
        public bool IncludeDemoData { get; set; }
    }
}
