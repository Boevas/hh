using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using System.Diagnostics;
using System.Text;

using System.IO;
using WebApplication2.Models;
using WebApplication2.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication2.Service;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication2
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                Configuration.Bind("Project", new Config());

                services.AddHostedService
                    (serviceProvider =>
                        new PeriodicBackgroundService
                        (
                            serviceProvider.GetService<IMemoryCache>(), new PeriodicBackgroundServiceConfig{ Timeout = 1 * 60 * 60 * 1000 /* часы * мин * сек * милисек*/}
                        )
                    );
                services.AddMemoryCache();

                services.AddDbContext<AppContextDB>(options =>
                options.UseNpgsql(Config.ConnectionString));

                services.AddMvc();

                services.AddScoped<IRepository<User>, UsersRepositoryDB>();
                services.AddScoped<IRepository<Department>, DepartmentsRepositoryDB>();

                services.AddControllers();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseRouting();
                app.UseStaticFiles();



                //Логирование запросов длительностью > 1 сек.
                app.Use(async (context, next)=>
                    {
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        await next();
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
                );

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllers();
                });

            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
            }
        }
    }
}
