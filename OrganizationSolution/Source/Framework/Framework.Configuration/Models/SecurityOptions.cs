namespace Framework.Configuration.Models
{
    using Framework.Configuration;

    /// <summary>
    /// Defines the <see cref="SecurityOptions" />.
    /// </summary>
    public class SecurityOptions : ConfigurationOptions
    {
        /// <summary>
        /// Gets or sets the AuthSettings.
        /// </summary>
        public AuthSettings AuthSettings { get; set; }

        /// <summary>
        /// Gets or sets the JwtIssuerOptions.
        /// </summary>
        public JwtIssuerOptions JwtIssuerOptions { get; set; }
    }
}
