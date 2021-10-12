using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Targets;
using WebApplication2.MiddleWare.LoggerManager;

namespace WebApplication2.Service
{
    public class PeriodicBackgroundServiceConfig
    {
        public int Timeout { get; set; }
    }

    public class PeriodicBackgroundService : BackgroundService
    {
        private readonly PeriodicBackgroundServiceConfig Config;
        private readonly ILoggerManager log;
        private readonly IMemoryCache MemoryCache;
        

        public PeriodicBackgroundService( PeriodicBackgroundServiceConfig config, ILoggerManager log, IMemoryCache memoryCache)
        {
            this.Config = config;
            this.log = log;
            this.MemoryCache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            { 
                while (false == stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                    
                        //TODO
                    }
                    catch { }
                    finally
                    {
                        await Task.Delay(Config.Timeout, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
        }
    }
}
