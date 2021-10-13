using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Service
{
    public class Config
    { 
        public string ConnectionString{ get; set; }
        public int MiddlewareRequestTimeoutMilliseconds { get; set; }
    }
}
