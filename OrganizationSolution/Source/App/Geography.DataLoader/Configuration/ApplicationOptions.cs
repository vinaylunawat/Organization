namespace Geography.DataLoader.Configuration
{
    using Framework.Configuration;

    /// <summary>
    /// Defines the <see cref="ApplicationOptions" />.
    /// </summary>
    public class ApplicationOptions : ConfigurationOptions
    {
        /// <summary>
        /// Gets or sets the ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the ReadOnlyConnectionString.
        /// </summary>
        public string ReadOnlyConnectionString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DeleteDefaultSchema.
        /// </summary>
        public bool DeleteDefaultSchema { get; set; }
    }
}
