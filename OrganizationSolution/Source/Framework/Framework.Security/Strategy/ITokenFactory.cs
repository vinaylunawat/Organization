namespace Framework.Security
{
    /// <summary>
    /// Defines the <see cref="ITokenFactory" />.
    /// </summary>
    public interface ITokenFactory
    {
        /// <summary>
        /// The GenerateToken.
        /// </summary>
        /// <param name="size">The size<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string GenerateToken(int size = 32);
    }
}
