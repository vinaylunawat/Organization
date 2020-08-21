namespace Geography.Business.AuthToken.Manager
{
    using Geography.Business.AuthToken.Models;
    using System.Threading.Tasks;
    public interface IAuthTokenManager
    {
        Task<AuthTokenModel> GenerateToken(string userId, string userName);
    }
}
