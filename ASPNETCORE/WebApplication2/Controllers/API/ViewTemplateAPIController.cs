using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
//using Microsoft.Extensions.Logging;
namespace WebApplication2.Controllers
{
    public class ViewTemplateAPIController<T> : TemplateAPIController<T>
    {
        public ViewTemplateAPIController(IRepository<T> _IRep) : base(_IRep) { }

        [HttpGet("/[controller]")]
        public ViewResult Table()
        {
            return View(Get().Result);
        }
    }
}
