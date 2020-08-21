namespace Framework.Service.Extension
{
    using Framework.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Rewrite;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="SwaggerConfigurationExtension" />.
    /// </summary>
    public static class SwaggerConfigurationExtension
    {
        /// <summary>
        /// The UseSwagger.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="apiVersion">The apiVersion<see cref="string"/>.</param>
        /// <param name="apiName">The apiName<see cref="string"/>.</param>
        /// <param name="alwaysShowInSwaggerUI">The alwaysShowInSwaggerUI<see cref="bool"/>.</param>
        public static void UseSwagger(this IApplicationBuilder app, string apiVersion, string apiName, bool alwaysShowInSwaggerUI = false)
        {
            ConfigureSwaggerUI(app, (swaggerUIOptions) =>
            {
                AddSwaggerEndpointToUi(swaggerUIOptions, new[] { new SwaggerConfigurationModel(apiVersion, apiName, alwaysShowInSwaggerUI) });
            });
        }

        /// <summary>
        /// The UseSwagger.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="swaggerConfigurationModels">The swaggerConfigurationModels<see cref="IEnumerable{SwaggerConfigurationModel}"/>.</param>
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

        /// <summary>
        /// The ConfigureSwaggerUI.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="configureEndPoints">The configureEndPoints<see cref="Action{SwaggerUIOptions}"/>.</param>
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

        /// <summary>
        /// The AddSwaggerEndpointToUi.
        /// </summary>
        /// <param name="swaggerUIOptions">The swaggerUIOptions<see cref="SwaggerUIOptions"/>.</param>
        /// <param name="swaggerConfigurations">The swaggerConfigurations<see cref="IEnumerable{SwaggerConfigurationModel}"/>.</param>
        private static void AddSwaggerEndpointToUi(SwaggerUIOptions swaggerUIOptions, IEnumerable<SwaggerConfigurationModel> swaggerConfigurations)
        {
            foreach (var swaggerConfiguration in swaggerConfigurations)
            {
                swaggerUIOptions.SwaggerEndpoint($"/swagger/{swaggerConfiguration.ApiVersion}/swagger.json", $"{swaggerConfiguration.ApiName} {swaggerConfiguration.ApiVersion}");
            }
        }
    }
}
