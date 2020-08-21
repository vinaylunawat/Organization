namespace Framework.Security.Authorization
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IAuthorizeClient" />.
    /// </summary>
    public interface IAuthorizeClient
    {
        /// <summary>
        /// The GetUserRights.
        /// </summary>
        /// <param name="applicationModuleCode">The applicationModuleCode<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{SecurityRightIdentifierModel}}"/>.</returns>
        Task<IEnumerable<SecurityRightIdentifierModel>> GetUserRights(string applicationModuleCode);
    }
}
