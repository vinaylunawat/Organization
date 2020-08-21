namespace Framework.Security
{
    using Framework.Security.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IJwtFactory" />.
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// The GenerateEncodedToken.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        /// <param name="userName">The userName<see cref="string"/>.</param>
        /// <param name="customClaimList">The customClaimList<see cref="Dictionary{string, string}"/>.</param>
        /// <returns>The <see cref="Task{AccessToken}"/>.</returns>
        Task<AccessToken> GenerateEncodedToken(string id, string userName, Dictionary<string, string> customClaimList);
    }
}
