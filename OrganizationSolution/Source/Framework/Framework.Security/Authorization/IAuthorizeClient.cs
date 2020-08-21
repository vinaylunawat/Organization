namespace Framework.Security.Authorization
{
    using System.Collections.Generic;    
    using System.Threading.Tasks;

    public interface IAuthorizeClient
    {
        Task<IEnumerable<SecurityRightIdentifierModel>> GetUserRights(string applicationModuleCode);
    }
}
