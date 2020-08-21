namespace Framework.Security.Models
{
    using System;

    /// <summary>
    /// Defines the <see cref="TokenResultModel" />.
    /// </summary>
    public class TokenResultModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenResultModel"/> class.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <param name="tokenValidity">The tokenValidity<see cref="DateTime"/>.</param>
        public TokenResultModel(string token, DateTime tokenValidity)
        {
            Token = token;
            TokenValidity = tokenValidity;
        }

        /// <summary>
        /// Gets the Token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets the TokenValidity.
        /// </summary>
        public DateTime TokenValidity { get; }
    }
}
