namespace Geography.Service.Controllers.Query
{
    using Geography.Business.Country.Manager;
    using Geography.Business.Country.Models;
    using Framework.Service;
    using EnsureThat;    
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [QueryRoute]    
    public class CountryQueryController : ControllerBase
    {
        private readonly ICountryQueryManager _manager;

        public CountryQueryController(ICountryQueryManager manager)
        {
            EnsureArg.IsNotNull(manager, nameof(manager));

            _manager = manager;
        }

        /// <summary>
        /// Gets all Country.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerable{CountryReadModel}"/> representing the result of the operation.
        /// </returns>
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
        /// Gets the Country by Id
        /// </summary>
        /// <param name="id">A single id.</param>
        /// <returns>
        /// A <see cref="CountryReadModel"/> representing the result of the operation.
        /// </returns>
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
        /// Gets the Country by Id
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        /// A <see cref="IEnumerable{CountryReadModel}"/> representing the result of the operation.
        /// </returns>
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
        /// <returns>
        /// A <see cref="CountryReadModel"/> representing the result of the operation.
        /// </returns>
        [HttpGet(nameof(GetByIsoCode))]
        [ProducesResponseType(typeof(CountryReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIsoCode([Required, FromQuery]string isoCode)
        {
            var result = await _manager.GetByIsoCodeAsync(isoCode).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result.SingleOrDefault());
        }

        /// <summary>
        /// Gets the country(s) by IsoCode(s).
        /// </summary>
        /// <param name="isoCodes">The IsoCode(s) of country(s) to fetch the countrys.</param>
        /// <returns>
        /// A <see cref="IEnumerable{CountryReadModel}"/> representing the result of the operation.
        /// </returns>
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