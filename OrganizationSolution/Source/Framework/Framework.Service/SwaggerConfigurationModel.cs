namespace Framework.Service
{
    /// <summary>
    /// Defines the <see cref="SwaggerConfigurationModel" />.
    /// </summary>
    public class SwaggerConfigurationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerConfigurationModel"/> class.
        /// </summary>
        /// <param name="apiVersion">The apiVersion<see cref="string"/>.</param>
        /// <param name="apiName">The apiName<see cref="string"/>.</param>
        /// <param name="alwaysShowInSwaggerUI">The alwaysShowInSwaggerUI<see cref="bool"/>.</param>
        public SwaggerConfigurationModel(string apiVersion, string apiName, bool alwaysShowInSwaggerUI = false)
        {
            ApiVersion = apiVersion;
            ApiName = apiName;
            AlwaysShowInSwaggerUI = alwaysShowInSwaggerUI;
        }

        /// <summary>
        /// Gets a value indicating whether [always show in swagger UI]..
        /// </summary>
        public bool AlwaysShowInSwaggerUI { get; }

        /// <summary>
        /// Gets the API version..
        /// </summary>
        public string ApiVersion { get; }

        /// <summary>
        /// Gets the name of the API..
        /// </summary>
        public string ApiName { get; }
    }
}
