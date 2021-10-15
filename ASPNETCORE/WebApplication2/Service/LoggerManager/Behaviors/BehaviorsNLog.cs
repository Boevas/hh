using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.MiddleWare.LoggerManager
{
    using NLog;
    public class BehaviorsNLog : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public BehaviorsNLog()
        {
        }
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInformation(string message)
        {
            logger.Info(message);
        }
        public void LogWarning(string message)
        {
            logger.Warn(message);
        }
    }
}
