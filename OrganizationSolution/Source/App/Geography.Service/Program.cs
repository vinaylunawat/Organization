namespace Geography.Service
{
    using Framework.Configuration;
    using Framework.Configuration.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly, typeof(SecurityOptions).Assembly }, args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
