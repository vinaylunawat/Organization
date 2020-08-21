namespace Framework.Security
{
    using Framework.Configuration.Models;
    using Framework.Security.Models;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="JwtFactory" />.
    /// </summary>
    public sealed class JwtFactory : IJwtFactory
    {
        /// <summary>
        /// Defines the _jwtTokenHandler.
        /// </summary>
        private readonly IJwtTokenHandler _jwtTokenHandler;

        /// <summary>
        /// Defines the _jwtOptions.
        /// </summary>
        private readonly JwtIssuerOptions _jwtOptions;

        /// <summary>
        /// Defines the _authSettings.
        /// </summary>
        private readonly AuthSettings _authSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtFactory"/> class.
        /// </summary>
        /// <param name="jwtTokenHandler">The jwtTokenHandler<see cref="IJwtTokenHandler"/>.</param>
        /// <param name="jwtOptions">The jwtOptions<see cref="JwtIssuerOptions"/>.</param>
        /// <param name="authSettings">The authSettings<see cref="AuthSettings"/>.</param>
        public JwtFactory(IJwtTokenHandler jwtTokenHandler, JwtIssuerOptions jwtOptions, AuthSettings authSettings)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _jwtOptions = jwtOptions;
            _authSettings = authSettings;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        /// <summary>
        /// The GenerateEncodedToken.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        /// <param name="userName">The userName<see cref="string"/>.</param>
        /// <param name="customClaimList">The customClaimList<see cref="Dictionary{string, string}"/>.</param>
        /// <returns>The <see cref="Task{AccessToken}"/>.</returns>
        public async Task<AccessToken> GenerateEncodedToken(string id, string userName, Dictionary<string, string> customClaimList)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
             };

            foreach (var claim in customClaimList)
            {
                claims.Add(new Claim(claim.Key.ToString(), claim.Value));
            }

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                signingCredentials);

            return new AccessToken(_jwtTokenHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
        }

        /// <summary>
        /// The ToUnixEpochDate.
        /// </summary>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        /// <summary>
        /// The ThrowIfInvalidOptions.
        /// </summary>
        /// <param name="options">The options<see cref="JwtIssuerOptions"/>.</param>
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
