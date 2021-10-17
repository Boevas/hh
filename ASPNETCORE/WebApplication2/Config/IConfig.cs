using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Config
{

    public interface IDatabaseConfig
    {
        string ConnectionString { get; set; }
    }

    public interface IMiddlewareRequestConfig
    {
        int TimeoutMilliseconds { get; set; }
    }
    public interface IPeriodicBackgroundServiceConfig
    {
        int TimeoutMilliseconds { get; set;}
    }
}
