namespace Geography.Service
{
    using Framework.Service;
    using Framework.Service.Extension;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.ConfigureClientServices();
            services.ConfigureSwagger();
            //services.ConfigureJwtSecurity();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.AddProblemDetailsSupport();

            app.UseSwagger(new[]
                   {
                      new SwaggerConfigurationModel(ApiConstants.ApiVersion, ApiConstants.ApiName, true),
                      new SwaggerConfigurationModel(ApiConstants.JobsApiVersion, ApiConstants.JobsApiName, false)
                    });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
