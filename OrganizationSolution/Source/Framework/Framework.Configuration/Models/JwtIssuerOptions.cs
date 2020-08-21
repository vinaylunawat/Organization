namespace Framework.Configuration.Models
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="JwtIssuerOptions" />.
    /// </summary>
    public class JwtIssuerOptions
    {
        /// <summary>
        /// Gets or sets the Issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the Subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets the Expiration.
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /// <summary>
        /// Gets the NotBefore.
        /// </summary>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>
        /// Gets the IssuedAt.
        /// </summary>
        public DateTime IssuedAt => DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the ValidFor.
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);

        /// <summary>
        /// Gets the JtiGenerator.
        /// </summary>
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
