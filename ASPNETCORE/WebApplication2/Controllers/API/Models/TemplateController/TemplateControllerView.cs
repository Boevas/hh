using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
using WebApplication2.MiddleWare.LoggerManager;
using Microsoft.AspNetCore.Http;
using System.Reflection;

//using Microsoft.Extensions.Logging;
namespace WebApplication2.Controllers
{
    public class TemplateControllerView<T> : Controller
    {
        private readonly IApi<T> Iapi;
        protected readonly ILoggerManager log;
        public TemplateControllerView(IServiceProvider serviceProvider)
        {
            this.Iapi = (IApi<T>)serviceProvider.GetService(typeof(IApi<T>));
            this.log = (ILoggerManager)serviceProvider.GetService(typeof(ILoggerManager));
        }

        [HttpGet("/[controller]")]
        public ViewResult Get()
        {
            try
            {
                return View("~/Views/Api/Get.cshtml", Iapi.Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet("/[controller]/[action]")]
        public IActionResult Post()
        {
            try
            {
                return View("~/Views/Api/Post.cshtml", typeof(T));
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        [HttpPost("/[controller]/[action]")]
        public async Task<IActionResult> Post([FromForm] T obj)
        {
            try
            {
                if ( await Iapi.Post(obj) is OkObjectResult)
                    return Redirect($"~/{this.ControllerContext.ActionDescriptor.ControllerName}");

                return StatusCode(StatusCodes.Status500InternalServerError, obj);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        
        [HttpGet("/[controller]/[action]")]
        public ViewResult Put()
        {
            try
            {
                return View("~/Views/Api/Put.cshtml", Iapi.Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        
        [HttpPost("/[controller]/[action]")]
        public async Task<IActionResult> Put([FromForm] T obj)
        {
            try
            {
                if (await Iapi.Put(obj) is OkObjectResult)
                    return Redirect($"~/{this.ControllerContext.ActionDescriptor.ControllerName}");

                return StatusCode(StatusCodes.Status500InternalServerError, obj);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet("/[controller]/[action]")]
        public ViewResult Delete()
        {
            try
            {
                return View("~/Views/Api/Delete.cshtml", Iapi.Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        [HttpPost("/[controller]/[action]")]
        public async Task<IActionResult> Delete([FromForm] T obj)
        {
            try
            {
                if (await Iapi.Delete((obj as ModelId).Id) is OkObjectResult)
                    return Redirect($"~/{this.ControllerContext.ActionDescriptor.ControllerName}");

                return StatusCode(StatusCodes.Status500InternalServerError, obj);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
    }
}
