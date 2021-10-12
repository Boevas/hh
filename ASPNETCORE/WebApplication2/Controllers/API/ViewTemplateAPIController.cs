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
    public class ViewTemplateAPIController<T> : TemplateAPIController<T>
    {
        private readonly IRepository<T> IRep;
        private readonly ILoggerManager log;

        public ViewTemplateAPIController(IRepository<T> _IRep, ILoggerManager _log) : base(_IRep, _log)
        {
            this.IRep = _IRep;
            this.log = _log;
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
}
