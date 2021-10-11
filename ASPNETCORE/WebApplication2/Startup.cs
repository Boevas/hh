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

                services.AddSingleton<ILoggerManager, LoggerManagerNLog>();
                services.AddTransient<LogRequest>();
                
                services.AddControllers();
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
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseRouting();
                app.UseStaticFiles();

                //Логирование запросов длительностью > 1 сек.
                app.UseMiddleware<LogRequest>();

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
