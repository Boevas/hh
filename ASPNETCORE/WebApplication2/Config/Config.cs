using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Config
{
    public class DatabaseConfig: IDatabaseConfig
    {
        [Required]
        public string ConnectionString { get; set; }
    }

    public class MiddlewareRequestConfig: IMiddlewareRequestConfig
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TimeoutMilliseconds { get; set; }
    }
    public class PeriodicBackgroundServiceConfig : IPeriodicBackgroundServiceConfig
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TimeoutMilliseconds { get; set; }
    }
}
