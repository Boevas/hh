using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
using WebApplication2.MiddleWare.LoggerManager;
//using Microsoft.Extensions.Logging;
namespace WebApplication2.Controllers
{
    /*
    public class ViewTemplateAPIController<T> : APIController<T>
    {
        private readonly ILoggerManager log;

        public ViewTemplateAPIController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.log = (ILoggerManager)serviceProvider.GetService(typeof(ILoggerManager));
        }

        [HttpGet("/[controller]")]
        public ViewResult Table()
        {
            try
            { 
                return View(Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
    }
    */
}
