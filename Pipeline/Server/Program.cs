
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Serilog;
using Serilog.Extensions.Logging;
namespace ServerPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration().WriteTo.File("consoleapp.log").CreateLogger();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Log.log")
                .CreateLogger();

            var configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var serviceProvider = new ServiceCollection()
               .AddLogging(logging =>
               {
                   logging.AddConsole();
                   logging.AddSerilog();
               })
               .AddSingleton<IConfiguration>(configurationBuilder.Build())
               .AddSingleton<Pipeline>()
               .BuildServiceProvider();

            var PipelineService = serviceProvider.GetService<Pipeline>();
            PipelineService.Start().Wait();
        }
   }
}
