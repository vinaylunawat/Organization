﻿namespace Geography.Service.Controllers.Command
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Geography.Business.Country;
    using Geography.Business.Country.Manager;
    using Geography.Business.Country.Models;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Service;
    using EnsureThat;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [ApiExplorerSettings(GroupName = ApiConstants.ApiVersion)]
    [Consumes(SupportedContentTypes.Json, SupportedContentTypes.Xml)]
    [CommandRoute]    
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class CountryCommandController : ControllerBase
    {
        private readonly ICountryCommandManager _manager;

        public CountryCommandController(ICountryCommandManager manager)
        {
            EnsureArg.IsNotNull(manager, nameof(manager));
            _manager = manager;
        }

        /// <summary>
        /// Creates the specified countries.
        /// </summary>
        /// <param name="countries">The countries.</param>
        /// <returns>
        /// <see cref="ManagerResponse{CountryErrorCode}"/> representing the result of the operation.
        /// </returns>
        [HttpPost(nameof(Create))]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([Required] IEnumerable<CountryCreateModel> countries)
        {
            var result = await _manager.CreateAsync(countries).ConfigureAwait(false);

            return result.ToStatusCode();
        }

        /// <summary>
        /// Creates the countries, if not exists
        /// </summary>
        /// <param name="countries">The countries.</param>
        /// <returns>
        /// A <see cref="ManagerResponse{TErrorCode}"/> representing the result of the operation.
        /// </returns>
        [HttpPost(nameof(CreateIfNotExistByCode))]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateIfNotExistByCode([Required] IEnumerable<CountryCreateModel> countries)
        {
            var result = await _manager.CreateIfNotExistByCodeAsync(countries).ConfigureAwait(false);
            return result.ToStatusCode();
        }

        /// <summary>
        /// Creates or update the countries by code
        /// </summary>
        /// <param name="countries">The countries.</param>
        /// <returns><see cref="ManagerResponse{TErrorCode}"/> representing the result of the operation.</returns>
        [HttpPost(nameof(CreateOrUpdateByCode))]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateByCode([Required] IEnumerable<CountryUpdateModel> models)
        {
            var result = await _manager.CreateOrUpdateByCodeAsync(models).ConfigureAwait(false);
            return result.ToStatusCode();
        }

        /// <summary>
        /// Updates the specified Country(s).
        /// </summary>
        /// <param name="countryUpdateModels">Country.</param>
        /// <returns>
        /// A <see cref="ManagerResponse{CountryErrorCode}"/> representing the result of the operation.
        /// </returns>
        [HttpPut(nameof(Update))]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([Required] IEnumerable<CountryUpdateModel> countryUpdateModels)
        {
            var result = await _manager.UpdateAsync(countryUpdateModels).ConfigureAwait(false);

            return result.ToStatusCode();
        }

        /// <summary>
        /// Deletes the by IsoCode.
        /// </summary>
        /// <param name="isoCodes">The IsoCodes.</param>
        /// <returns>
        /// A <see cref="ManagerResponse{CountryErrorCode}"/> representing the result of the operation.
        /// </returns>
        [HttpDelete(nameof(DeleteByCode))]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByCode([Required] IEnumerable<string> isoCodes)
        {
            var result = await _manager.DeleteByIsoCodeAsync(isoCodes).ConfigureAwait(false);
            return result.ToStatusCode();
        }

        /// <summary>
        /// Deletes the by Id.
        /// </summary>
        /// <param name="ids">The Ids.</param>
        /// <returns>
        /// A <see cref="ManagerResponse{CountryErrorCode}"/> representing the result of the operation.
        /// </returns>
        [HttpDelete(nameof(DeleteById))]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ManagerResponse<CountryErrorCode>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById([Required] IEnumerable<long> ids)
        {
            var result = await _manager.DeleteByIdAsync(ids).ConfigureAwait(false);
            return result.ToStatusCode();
        }
    }
}