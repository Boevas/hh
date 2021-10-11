using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


using NLog;
//using Microsoft.Extensions.Logging;


namespace WebApplication2.MiddleWare
{
    public class LogRequest : IMiddleware
    {
        public LogRequest()
        {
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
                if (ts.TotalSeconds > 1)
                {
                    LogManager.GetCurrentClassLogger().Warn
                    (
                        $"Request{GetDetailsHttpRequest(context.Request)}" +
                        $"\nElapsedTime: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}"
                    );
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
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
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
    }
}
