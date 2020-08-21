namespace Geography.Service.Controllers.Command
{
    using System;
    using System.Threading.Tasks;
    using Geography.Business.AuthToken.Manager;
    using Geography.Business.AuthToken.Models;
    using Framework.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Consumes(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [CommandRoute]
    public class AuthController : ControllerBase
    {
        private readonly IAuthTokenManager _authTokenManager;
        public AuthController(IAuthTokenManager authTokenManager)
        {
            _authTokenManager = authTokenManager;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Token))]
        [ProducesResponseType(typeof(AuthTokenModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Token([FromBody] LoginCreateModel loginCreateModel)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserIdFromCredentials(loginCreateModel.UserName, loginCreateModel.Password);

                if (userId == -1)
                {
                    return await Task.FromResult(BadRequest(loginCreateModel)).ConfigureAwait(false);
                }
                var result = await _authTokenManager.GenerateToken(userId.ToString(), loginCreateModel.UserName).ConfigureAwait(false);
                return StatusCode(StatusCodes.Status200OK, result);
            }

            return BadRequest();
        }

        private int GetUserIdFromCredentials(string userName, string password)
        {
            return userName.Equals("string") && password.Equals("string") ? new Random().Next(1000) : -1;
        }
    }
}