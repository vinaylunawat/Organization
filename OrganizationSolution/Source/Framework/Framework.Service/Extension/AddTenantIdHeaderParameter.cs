namespace Framework.Service
{
    using Framework.Constant;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AddTenantIdHeaderParameter" />.
    /// </summary>
    public class AddTenantIdHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// The Apply.
        /// </summary>
        /// <param name="operation">The operation<see cref="OpenApiOperation"/>.</param>
        /// <param name="context">The context<see cref="OperationFilterContext"/>.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = SecurityConstants.TenantIdHeaderKey,
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema() { Type = "string" }
            });
        }
    }
}
