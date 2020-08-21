namespace Framework.Security.Models
{
    using System;

    public class TokenResultModel
    {
        public TokenResultModel(string token,DateTime tokenValidity)
        {
            Token = token;
            TokenValidity = tokenValidity;
        }
        public string Token { get; }
        public DateTime TokenValidity { get; } 
    }
}
