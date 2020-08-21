namespace Geography.Service.Controllers.Query
{
    using EnsureThat;
    using Framework.Service;
    using Geography.Business.Country.Manager;
    using Geography.Business.Country.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CountryQueryController" />.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [QueryRoute]
    public class CountryQueryController : ControllerBase
    {
        /// <summary>
        /// Defines the _manager.
        /// </summary>
        private readonly ICountryQueryManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryQueryController"/> class.
        /// </summary>
        /// <param name="manager">The manager<see cref="ICountryQueryManager"/>.</param>
        public CountryQueryController(ICountryQueryManager manager)
        {
            EnsureArg.IsNotNull(manager, nameof(manager));

            _manager = manager;
        }

        /// <summary>
        /// Gets all Country.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet(nameof(GetAll))]
        [ProducesResponseType(typeof(IEnumerable<CountryReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _manager.GetAllAsync().ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Gets the Country by Id.
        /// </summary>
        /// <param name="id">A single id.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet(nameof(GetById))]
        [ProducesResponseType(typeof(CountryReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([Required, FromQuery] long id)
        {
            var result = await _manager.GetByIdAsync(id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result.SingleOrDefault());
        }

        /// <summary>
        /// Gets the Country by Id.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet(nameof(GetByIds))]
        [ProducesResponseType(typeof(IEnumerable<CountryReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIds([Required, FromQuery] IEnumerable<long> ids)
        {
            var result = await _manager.GetByIdAsync(ids).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Gets the country by IsoCode.
        /// </summary>
        /// <param name="isoCode">The IsoCode of country to fetch the country.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet(nameof(GetByIsoCode))]
        [ProducesResponseType(typeof(CountryReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIsoCode([Required, FromQuery] string isoCode)
        {
            var result = await _manager.GetByIsoCodeAsync(isoCode).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result.SingleOrDefault());
        }

        /// <summary>
        /// Gets the country(s) by IsoCode(s).
        /// </summary>
        /// <param name="isoCodes">The IsoCode(s) of country(s) to fetch the countrys.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet(nameof(GetByIsoCodes))]
        [ProducesResponseType(typeof(IEnumerable<CountryReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIsoCodes([Required, FromQuery] IEnumerable<string> isoCodes)
        {
            var result = await _manager.GetByIsoCodeAsync(isoCodes).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
