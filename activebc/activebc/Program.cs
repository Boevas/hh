using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
namespace activebc
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            using var serviceProvider = new ServiceCollection()
               .AddLogging(logging =>
               {
                   logging.AddConsole();
               })
               .AddSingleton<IConfiguration>(configurationBuilder.Build())
               .AddSingleton<TcpProxy>()
               .BuildServiceProvider();

            var proxyService = serviceProvider.GetService<TcpProxy>();
            proxyService.Start();
        }
   }
}
