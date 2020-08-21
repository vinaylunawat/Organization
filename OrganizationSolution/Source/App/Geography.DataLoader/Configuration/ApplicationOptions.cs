namespace Geography.DataLoader.Configuration
{    
    using Framework.Configuration;

    public class ApplicationOptions : ConfigurationOptions
    {
        public string ConnectionString { get; set; }

        public string ReadOnlyConnectionString { get; set; }

        public bool DeleteDefaultSchema { get; set; }
    }
}
