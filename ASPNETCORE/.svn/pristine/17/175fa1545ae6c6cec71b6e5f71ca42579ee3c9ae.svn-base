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

namespace WebApplication2.Service
{
    public class PeriodicBackgroundServiceConfig
    {
        public int Timeout { get; set; }
    }

    public class PeriodicBackgroundService : BackgroundService
    {
        private readonly IMemoryCache MemoryCache;
        private readonly PeriodicBackgroundServiceConfig Config;

        public PeriodicBackgroundService(IMemoryCache memoryCache, PeriodicBackgroundServiceConfig config)
        {
            this.MemoryCache = memoryCache;
            this.Config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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


    }
}
