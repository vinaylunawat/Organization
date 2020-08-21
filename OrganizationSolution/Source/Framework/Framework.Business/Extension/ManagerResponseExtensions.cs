namespace Framework.Business.Extension
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ManagerResponseExtensions" />.
    /// </summary>
    public static class ManagerResponseExtensions
    {
        /// <summary>
        /// The ToStatusCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="successCode">The successCode<see cref="int"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="int"/>.</param>
        /// <param name="notFoundErrorCode">The notFoundErrorCode<see cref="int"/>.</param>
        /// <returns>The <see cref="ObjectResult"/>.</returns>
        public static ObjectResult ToStatusCode<TErrorCode>(this ManagerResponseBase<TErrorCode> managerResponse, int successCode = StatusCodes.Status200OK, int errorCode = StatusCodes.Status400BadRequest, int notFoundErrorCode = StatusCodes.Status404NotFound)
            where TErrorCode : struct, Enum
        {
            if (managerResponse.HasError)
            {
                return new ObjectResult(managerResponse)
                {
                    StatusCode = errorCode
                };
            }
            else
            {
                return new ObjectResult(managerResponse)
                {
                    StatusCode = successCode
                };
            }
        }

        /// <summary>
        /// The ToStatusCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <typeparam name="TResult">.</typeparam>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponseTyped{TErrorCode, TResult}"/>.</param>
        /// <param name="successCode">The successCode<see cref="int"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="int"/>.</param>
        /// <param name="notFoundErrorCode">The notFoundErrorCode<see cref="int"/>.</param>
        /// <returns>The <see cref="ObjectResult"/>.</returns>
        public static ObjectResult ToStatusCode<TErrorCode, TResult>(this ManagerResponseTyped<TErrorCode, TResult> managerResponse, int successCode = StatusCodes.Status200OK, int errorCode = StatusCodes.Status400BadRequest, int notFoundErrorCode = StatusCodes.Status404NotFound)
            where TErrorCode : struct, Enum
        {
            if (managerResponse.HasError)
            {
                return new ObjectResult(managerResponse)
                {
                    StatusCode = errorCode
                };
            }
            if (!managerResponse.Results.Any())
            {
                return new ObjectResult(managerResponse)
                {
                    StatusCode = notFoundErrorCode
                };
            }
            else
            {
                return new ObjectResult(managerResponse)
                {
                    StatusCode = successCode,
                    Value = managerResponse
                };
            }
        }
    }
}
