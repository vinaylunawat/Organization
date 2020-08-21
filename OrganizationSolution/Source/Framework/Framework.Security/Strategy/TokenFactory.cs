namespace Framework.Security
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="TokenFactory" />.
    /// </summary>
    public sealed class TokenFactory : ITokenFactory
    {
        /// <summary>
        /// The GenerateToken.
        /// </summary>
        /// <param name="size">The size<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
