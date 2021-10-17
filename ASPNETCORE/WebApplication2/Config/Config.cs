using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Config
{
    public class DatabaseConfig: IDatabaseConfig
    {
        public string ConnectionString { get; set; }
    }

    public class MiddlewareRequestConfig: IMiddlewareRequestConfig
    {
        public int TimeoutMilliseconds { get; set; }
    }
    public class PeriodicBackgroundServiceConfig : IPeriodicBackgroundServiceConfig
    {
        public int TimeoutMilliseconds { get; set; }
    }
}
