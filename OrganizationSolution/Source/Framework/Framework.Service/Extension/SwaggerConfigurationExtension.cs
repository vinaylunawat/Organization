namespace Framework.Service.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Configuration;    
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Rewrite;
    using Swashbuckle.AspNetCore.SwaggerUI;

    public static class SwaggerConfigurationExtension
    {
        public static void UseSwagger(this IApplicationBuilder app, string apiVersion, string apiName, bool alwaysShowInSwaggerUI = false)
        {
            ConfigureSwaggerUI(app, (swaggerUIOptions) =>
            {
                AddSwaggerEndpointToUi(swaggerUIOptions, new[] { new SwaggerConfigurationModel(apiVersion, apiName, alwaysShowInSwaggerUI) });
            });
        }

        public static void UseSwagger(this IApplicationBuilder app, IEnumerable<SwaggerConfigurationModel> swaggerConfigurationModels)
        {
            ConfigureSwaggerUI(app, (swaggerUIOptions) =>
            {
                if (ApplicationConfiguration.IsDevelopment)
                {
                    // add all endpoints in dev
                    AddSwaggerEndpointToUi(swaggerUIOptions, swaggerConfigurationModels);
                }
                else
                {
                    // add only items to the swagger ui that are listed to always show
                    var swaggerItemsToShow = swaggerConfigurationModels.Where(item => item.AlwaysShowInSwaggerUI);
                    AddSwaggerEndpointToUi(swaggerUIOptions, swaggerItemsToShow);
                }
            });
        }

        private static void ConfigureSwaggerUI(this IApplicationBuilder app, Action<SwaggerUIOptions> configureEndPoints)
        {
            app.UseSwagger();
            //var securityOptions = app.ApplicationServices.GetRequiredService<SecurityOptions>();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                configureEndPoints(swaggerUIOptions);
                swaggerUIOptions.DisplayOperationId();
                swaggerUIOptions.DocExpansion(DocExpansion.None);                
            });

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
        }

        private static void AddSwaggerEndpointToUi(SwaggerUIOptions swaggerUIOptions, IEnumerable<SwaggerConfigurationModel> swaggerConfigurations)
        {
            foreach (var swaggerConfiguration in swaggerConfigurations)
            {
                swaggerUIOptions.SwaggerEndpoint($"/swagger/{swaggerConfiguration.ApiVersion}/swagger.json", $"{swaggerConfiguration.ApiName} {swaggerConfiguration.ApiVersion}");
            }
        }
    }
}
