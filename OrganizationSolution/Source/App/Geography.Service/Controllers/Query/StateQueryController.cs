namespace Geography.Service.Controllers.Query
{
    using EnsureThat;
    using Framework.Service;
    using Geography.Business.Business.State.Models;
    using Geography.Business.State.Manager;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="StateQueryController" />.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [QueryRoute]


    public class StateQueryController : ControllerBase
    {
        /// <summary>
        /// Defines the _manager.
        /// </summary>
        private readonly IStateQueryManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateQueryController"/> class.
        /// </summary>
        /// <param name="manager">The manager<see cref="IStateQueryManager"/>.</param>
        public StateQueryController(IStateQueryManager manager)
        {
            EnsureArg.IsNotNull(manager, nameof(manager));

            _manager = manager;
        }

        /// <summary>
        /// Gets all Country.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet(nameof(GetAll))]
        [ProducesResponseType(typeof(IEnumerable<StateReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _manager.GetAllAsync().ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
