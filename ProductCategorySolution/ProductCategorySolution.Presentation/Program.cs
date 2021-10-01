using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProductCategory.Service;
using Serilog;

namespace ProductCategorySolution
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                 .WriteTo.Debug()
                 .WriteTo.Console(outputTemplate: SeriLogConstants.LogTemplate)
                 .WriteTo.File(SeriLogConstants.FilePath, Serilog.Events.LogEventLevel.Error));
    }
}
