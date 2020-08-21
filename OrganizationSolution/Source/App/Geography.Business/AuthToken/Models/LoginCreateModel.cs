namespace Geography.Business.AuthToken.Models
{
    using Framework.Business.Models;
    public class LoginCreateModel : IModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
