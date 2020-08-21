namespace Geography.Business.AuthToken.Models
{
    using Framework.Business.Models;
    using Framework.Security.Models;

    public class AuthTokenModel : IModel
    {
        public AuthTokenModel(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }
    }
}
