namespace Framework.Security.Models
{
    /// <summary>
    /// Defines the <see cref="AccessToken" />.
    /// </summary>
    public sealed class AccessToken
    {
        /// <summary>
        /// Gets the Token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets the ExpiresIn.
        /// </summary>
        public int ExpiresIn { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> class.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <param name="expiresIn">The expiresIn<see cref="int"/>.</param>
        public AccessToken(string token, int expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }
    }
}
