namespace Framework.Service
{
    public class SwaggerConfigurationModel
    {
        public SwaggerConfigurationModel(string apiVersion, string apiName, bool alwaysShowInSwaggerUI = false)
        {
            ApiVersion = apiVersion;
            ApiName = apiName;
            AlwaysShowInSwaggerUI = alwaysShowInSwaggerUI;
        }

        /// <summary>
        /// Gets a value indicating whether [always show in swagger UI].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always show in swagger UI]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysShowInSwaggerUI { get; }

        /// <summary>
        /// Gets the API version.
        /// </summary>
        /// <value>
        /// The API version.
        /// </value>
        public string ApiVersion { get; }

        /// <summary>
        /// Gets the name of the API.
        /// </summary>
        /// <value>
        /// The name of the API.
        /// </value>
        public string ApiName { get; }
    }
}
