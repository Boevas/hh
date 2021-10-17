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
using Microsoft.Extensions.Options;
using WebApplication2.Config;

namespace WebApplication2.Service
{
    public class PeriodicBackgroundService : BackgroundService
    {
        private readonly IPeriodicBackgroundServiceConfig config;
        private readonly ILoggerManager log;
        private readonly IMemoryCache MemoryCache;
        

        public PeriodicBackgroundService( ILoggerManager log, IMemoryCache memoryCache, IPeriodicBackgroundServiceConfig config)
        {
            this.config = config;
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
                        await Task.Delay(config.TimeoutMilliseconds, stoppingToken);
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
