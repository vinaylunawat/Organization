namespace Framework.Configuration.Models
{
    using Framework.Configuration;

    public class SecurityOptions: ConfigurationOptions
    {
        public AuthSettings AuthSettings { get; set; }

        public JwtIssuerOptions JwtIssuerOptions { get; set; }
    }

         
}
