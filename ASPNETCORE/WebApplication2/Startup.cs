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
                //appsettings.json -> Class Config & IOptions<Config>
                {
                    //Configuration.Bind ("Project", new Config());
                    services.Configure<Config>(Configuration.GetSection("Project"));
                    services.AddSingleton<Config>();

                    services.AddOptions();
                }

                //Log
                {
                    services.AddSingleton<ILoggerManager, LoggerManagerNLog>();
                }

                //BackgroundService with Cache
                {
                    services.AddMemoryCache();
                    services.AddHostedService<PeriodicBackgroundService>();
                }

                //Business
                {
                    services.AddDbContext<AppContextDB>((serviceProvider, options) =>
                    {
                        options.UseNpgsql(Configuration.GetSection("Project")["ConnectionString"]);
                    });

                    services.AddMvc();

                    services.AddScoped<DbContext, AppContextDB>();
                    services.AddScoped<IRepository<User>, TemplateRepositoryDB<User>>();
                    services.AddScoped<IRepository<Department>, TemplateRepositoryDB<Department>>();

                    services.AddScoped<IApi<User>, APIController<User>>();
                    services.AddScoped<IApi<Department>, APIController<Department>>();

                    services.AddControllers();
                }

                //Middleware
                {
                    services.AddTransient<MiddlewareRequestTimeout>();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
            }
        }

        public void Configure(IServiceProvider serviceProvider,IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                //IApi<User> Iapi = (IApi<User>)serviceProvider.GetService(typeof(IApi<User>));
                app.UseRouting();
                app.UseStaticFiles();

                //����������� �������� ������������� > XX ���.
                app.UseMiddleware<MiddlewareRequestTimeout>();

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
