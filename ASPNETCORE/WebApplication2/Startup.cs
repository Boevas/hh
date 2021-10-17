using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using NLog;
using WebApplication2.MiddleWare;
using WebApplication2.Models;
using WebApplication2.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication2.Service;
using Microsoft.Extensions.Caching.Memory;
using WebApplication2.MiddleWare.LoggerManager;
using WebApplication2.Config;

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
                //appsettings.json -> ClassConfig Singleton
                {
                    services.AddSingleton<IDatabaseConfig>((serviceProvider) =>
                    {
                        return Configuration.GetSection("Database").Get<DatabaseConfig>();
                    });

                    services.AddSingleton<IMiddlewareRequestConfig>((serviceProvider) =>
                    {
                        return Configuration.GetSection("MiddlewareRequest").Get<MiddlewareRequestConfig>();
                    });

                    services.AddSingleton<IPeriodicBackgroundServiceConfig>((serviceProvider) =>
                    {
                        return Configuration.GetSection("PeriodicBackgroundService").Get<PeriodicBackgroundServiceConfig>();
                    });
                }

                //Log
                {
                    services.AddSingleton<ILoggerManager, BehaviorsNLog>();
                }

                //BackgroundService with Cache
                {
                    services.AddMemoryCache();
                    services.AddHostedService<PeriodicBackgroundService>();
                }

                //Business
                {
                    //DbContext
                    {
                        services.AddDbContext<AppContextDB>((serviceProvider, options) =>
                        {
                            options.UseNpgsql(Configuration.GetSection("Database")["ConnectionString"]);
                        });
                        services.AddScoped<DbContext, AppContextDB>();

                    }
                    
                    //Behavior API Repository's
                    {
                        services.AddScoped<IRepository<User>, BehaviorAPIRepository<User>>();
                        services.AddScoped<IRepository<Department>, BehaviorAPIRepository<Department>>();
                    }

                    //Behavior API Controller's
                    {
                        services.AddScoped<IApi<User>, BehaviorAPIController<User>>();
                        services.AddScoped<IApi<Department>, BehaviorAPIController<Department>>();
                    }

                    services.AddMvc();
                    services.AddControllers();
                }

                //Middleware
                {
                    services.AddTransient<MiddlewareRequest>();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
                
                app.UseRouting();
                app.UseStaticFiles();

                //Логирование запросов длительностью > XX сек.
                app.UseMiddleware<MiddlewareRequest>();

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
