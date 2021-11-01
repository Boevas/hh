using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
using WebApplication2.MiddleWare.LoggerManager;
using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
namespace WebApplication2.Controllers
{
    public class TemplateControllerView<T>: TemplateController<T>
    {
        public TemplateControllerView(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet("/[controller]")]
        new public ViewResult Get()
        {
            try
            {
                return View("~/Views/Api/Get.cshtml", base.Get().Result);
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
        new public async Task<IActionResult> Post([FromForm] T obj)
        {
            try
            {
                if ( await base.Post(obj) is OkObjectResult)
                    return Redirect($"~/{this.GetType().Name.Replace("Controller","")}");

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
                return View("~/Views/Api/Put.cshtml", base.Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        
        [HttpPost("/[controller]/[action]")]
        new public async Task<IActionResult> Put([FromForm] T obj)
        {
            try
            {
                if (await base.Put(obj) is OkObjectResult)
                    return Redirect($"~/{this.GetType().Name.Replace("Controller", "")}");

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
                return View("~/Views/Api/Delete.cshtml", base.Get().Result);
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
                if (await base.Delete((obj as ModelId).Id) is OkObjectResult)
                    return Redirect($"~/{this.GetType().Name.Replace("Controller", "")}");

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
