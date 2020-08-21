namespace Framework.Configuration.Models
{
    using Framework.Configuration;
    using System;

    public class ApplicationOptions : ConfigurationOptions
    {
        public string ConnectionString { get; set; }

        public string ReadOnlyConnectionString { get; set; }

        public bool DeleteDefaultSchema { get; set; }

        public string StorageConnectionString { get; set; }

        public Uri UserServiceBaseUri { get; set; }

    }
}
