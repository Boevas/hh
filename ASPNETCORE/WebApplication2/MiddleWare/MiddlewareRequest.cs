using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


using NLog;
using WebApplication2.MiddleWare.LoggerManager;
using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication2.Service;
using WebApplication2.Config;

namespace WebApplication2.MiddleWare
{
    public class MiddlewareRequest : IMiddleware
    {
        private readonly ILoggerManager log;
        private readonly IMiddlewareRequestConfig config;

        public MiddlewareRequest(IServiceProvider serviceProvider, IMiddlewareRequestConfig config)
        {
            this.log = (ILoggerManager)serviceProvider.GetService(typeof(ILoggerManager));
            this.config = config;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                await next.Invoke(context);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalMilliseconds > config.TimeoutMilliseconds)
                {
                    log.LogWarning
                    (
                        $"Request{GetDetailsHttpRequest(context.Request)}" +
                        $"\nElapsedTime: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}"
                    );
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
        }

        private string GetDetailsHttpRequest(HttpRequest request)
        {
            try
            {
                string baseUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString.Value}";
                StringBuilder sbHeaders = new StringBuilder();
                foreach (var header in request.Headers)
                    sbHeaders.Append($"{header.Key}: {header.Value}\n");

                string body = "no-body";
                if (request.Body.CanSeek)
                {
                    request.Body.Seek(0, SeekOrigin.Begin);
                    using StreamReader sr = new StreamReader(request.Body);
                    body = sr.ReadToEnd();
                }

                return $"{request.Protocol} {request.Method} {baseUrl}\n\n{sbHeaders}\n{body}";
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
    }
}
